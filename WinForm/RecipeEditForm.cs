using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TechRecipeAX;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Entity;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System.Data.Entity.Infrastructure;
using DevExpress.Utils.Svg;

namespace TecxRecipeAX
{
	public partial class RecipeEditForm: XtraForm
	{
        private RecipeDbContext _context;
        private BindingList<RecipeStage> recipeStageBindingList;
        private List<BaseRecipeStage> baseRecipeStages;
        private List<Recipe> recipes;
        private Recipe selectedRecipe;
        private bool _isEditingName = false;

        public event EventHandler DataUpdated;

        public RecipeEditForm(RecipeDbContext context)
        {
            InitializeComponent();
            _context = context;
            this.Load += (s, e) => Initialize(); // выносим всю инициализацию
        }

        #region Init
        private void Initialize()
        {
            LoadRecipesToCombobox();
            LoadBaseRecipeStages();
            if (recipes.Any()) SelectRecipe(recipes.First());
            ValidateAllRecipeStagesParameters();
        }
        #endregion

        #region Table

        private void GridViewStages_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != nameof(RecipeStage.baseRecipeStageId)) return;
            var stage = gridViewStages.GetRow(e.RowHandle) as RecipeStage;
            if (stage == null) return;

            var newBaseStage = baseRecipeStages.FirstOrDefault(bs => bs.id == (int)e.Value);
            if (newBaseStage == null) return;

            if (_context.RecipeParameters.Any(rp => rp.recipeStageId == stage.id))
            {
                var confirm = MessageBox.Show("Все параметры будут заменены. Продолжить?", "Подтверждение", MessageBoxButtons.YesNo);
                if (confirm != DialogResult.Yes)
                {
                    gridViewStages.RefreshRow(e.RowHandle);
                    return;
                }
            }

            stage.baseRecipeStageId = newBaseStage.id;
            stage.baseRecipeStage = newBaseStage;
            stage.name = newBaseStage.name;
            stage.description = newBaseStage.description;

            UpdateRecipeStageParameters(stage, newBaseStage);
            _context.SaveChanges();

            gridViewStages.RefreshRow(e.RowHandle);
        }

        private void GridViewStages_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateButtonsState();
        }

        private void gridViewStages_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is RecipeStage stage)
            {
                _context.SaveChanges();
            }
        }

        private void gridViewStages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedStage();
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                gridViewStages.PostEditor();
                gridViewStages.UpdateCurrentRow();
                e.Handled = true;
            }
        }
        private void StageLookup_EditValueChanged(object sender, EventArgs e)
        {
            gridViewStages.PostEditor(); // Применяет значение в ячейку
            gridViewStages.UpdateCurrentRow(); // Сохраняет изменения
        }

        #endregion

        #region ComboBox

        private void LoadRecipesToCombobox()
        {
            recipes = _context.Recipes.Include(r => r.RecipeStages).ToList();

            if (!recipes.Any())
            {
                var defaultRecipe = new Recipe { name = "Default Recipe" };
                _context.Recipes.Add(defaultRecipe);
                _context.SaveChanges();
                recipes = _context.Recipes.Include(r => r.RecipeStages).ToList();
            }

            comboBoxRecipes.Properties.DataSource = recipes;
            comboBoxRecipes.Properties.DisplayMember = "name";
            comboBoxRecipes.Properties.ValueMember = "id";

            // Удаляем все лишние колонки, оставляем только name
            comboBoxRecipes.Properties.Columns.Clear();
            comboBoxRecipes.Properties.Columns.Add(new LookUpColumnInfo("name", "Рецепт"));

            // Устанавливаем 0-й элемент по умолчанию
            if (recipes.Count > 0)
            {
                comboBoxRecipes.EditValue = recipes[0].id;
            }
        }

        private void comboBoxRecipes_EditValueChanged(object sender, EventArgs e)
        {
            var selectedId = comboBoxRecipes.EditValue as int?;
            if (selectedId != null)
            {
                var recipe = recipes.FirstOrDefault(r => r.id == selectedId);
                if (recipe != null)
                    SelectRecipe(recipe);
            }
        }

        private void comboBoxRecipes_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isEditingName && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SaveEditedRecipeName();
            }
        }

        #endregion

        #region Buttons

        // Создать новый рецепт
        private void btnAddRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                // Генерируем уникальное имя
                int recipeCount = _context.Recipes.Count();
                string newRecipeName = $"New Recipe {recipeCount + 1}";

                // Создаем новый рецепт
                Recipe newRecipe = new Recipe { name = newRecipeName };

                _context.Recipes.Add(newRecipe);
                _context.SaveChanges();

                // Обновляем комбобокс рецептов
                LoadRecipesToCombobox();

                // Выбираем новый рецепт
                comboBoxRecipes.EditValue = newRecipe.id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении рецепта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Удалить рецепт
        private void btnDeleteRecipe_Click(object sender, EventArgs e)
        {
            if (selectedRecipe == null) return;

            // Запрашиваем подтверждение
            if (XtraMessageBox.Show(
                $"Вы действительно хотите удалить рецепт \"{selectedRecipe.name}\" со всеми стадиями и параметрами?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                // Загружаем стадии рецепта
                var stagesToDelete = _context.RecipeStages.Where(rs => rs.recipeId == selectedRecipe.id).ToList();

                // Для каждой стадии загружаем и удаляем параметры
                foreach (var stage in stagesToDelete)
                {
                    var parametersToDelete = _context.RecipeParameters.Where(rp => rp.recipeStageId == stage.id).ToList();
                    _context.RecipeParameters.RemoveRange(parametersToDelete);
                }

                // Удаляем стадии
                _context.RecipeStages.RemoveRange(stagesToDelete);
                // Удаляем сам рецепт
                _context.Recipes.Remove(selectedRecipe);
                // Сохраняем изменения
                _context.SaveChanges();

                selectedRecipe = null;
                //textBoxRecipeName.Text = "";

                // Обновляем интерфейс
                LoadRecipesToCombobox();
                SelectRecipe(selectedRecipe);
                //dataGridViewStages.DataSource = null;

                XtraMessageBox.Show("Рецепт удалён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Добавить стадию в рецепт
        private void btnAddStageToRecipe_Click(object sender, EventArgs e)
        {
            if (selectedRecipe == null)
            {
                XtraMessageBox.Show("Выберите рецепт.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var baseStage = _context.BaseRecipeStages.Include(b => b.BaseParameters).OrderBy(b => b.name).FirstOrDefault();
            if (baseStage == null)
            {
                XtraMessageBox.Show("Нет доступных базовых стадий.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nextStageNumber = recipeStageBindingList.Any() ? recipeStageBindingList.Max(s => s.stageNumb) + 1 : 1;
            var newStage = new RecipeStage
            {
                name = baseStage.name,
                description = baseStage.description,
                recipeId = selectedRecipe.id,
                baseRecipeStageId = baseStage.id,
                stageNumb = nextStageNumber
            };

            _context.RecipeStages.Add(newStage);
            _context.SaveChanges();

            var parameters = baseStage.BaseParameters.Select(bp => new RecipeParameter
            {
                name = bp.name,
                description = bp.description,
                baseParameterId = bp.id,
                recipeStageId = newStage.id,
                value = 0,
                active = true
            }).ToList();

            _context.RecipeParameters.AddRange(parameters);
            _context.SaveChanges();

            SelectRecipe(selectedRecipe); // Обновить привязку
        }

        // Удалить стадию из рецепта
        private void btnRemoveStage_Click(object sender, EventArgs e)
        {
            DeleteSelectedStage();
        }

        // Перемещение стадии вверх
        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveStage(-1);
        }

        // Перемещение стадии вниз
        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveStage(1);
        }

        // метод перемещения строки вверх/вниз
        private void MoveStage(int direction)
        {
            var index = gridViewStages.FocusedRowHandle;
            int newIndex = index + direction;

            if (index < 0 || newIndex < 0 || newIndex >= recipeStageBindingList.Count) return;

            var currentStage = recipeStageBindingList[index];
            var targetStage = recipeStageBindingList[newIndex];

            (currentStage.stageNumb, targetStage.stageNumb) = (targetStage.stageNumb, currentStage.stageNumb);

            _context.SaveChanges();
            SelectRecipe(selectedRecipe);
            gridViewStages.FocusedRowHandle = newIndex;
        }

        // делаем кнопки неактивными
        private void UpdateButtonsState()
        {
            var handle = gridViewStages.FocusedRowHandle;
            btnUp.Enabled = handle > 0;
            btnDown.Enabled = handle >= 0 && handle < recipeStageBindingList.Count - 1;
            btnRemoveStage.Enabled = handle >= 0;
        }

        // кнопка перечитать/синхронизировать параметры с базовыми стадиями
        private void btnSyncWithBaseStages_Click(object sender, EventArgs e)
        {
            ValidateAllRecipeStagesParameters();

            // Обновляем список стадий
            var updatedStages = _context.RecipeStages
                .Include(rs => rs.RecipeParameters)
                .Include(rs => rs.baseRecipeStage.BaseParameters)
                .Where(rs => rs.recipeId == selectedRecipe.id)
                .ToList();

            recipeStageBindingList.Clear();
            foreach (var stage in updatedStages)
                recipeStageBindingList.Add(stage);

            gridControlStages.RefreshDataSource();

            MessageBox.Show("Параметры стадий обновлены из базовых стадий.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // кнопка редактирования имени рецепта
        private void btnToggleEditName_Click(object sender, EventArgs e)
        {
            _isEditingName = !_isEditingName;
            comboBoxRecipes.Properties.TextEditStyle = _isEditingName ? TextEditStyles.Standard : TextEditStyles.DisableTextEditor;
    //        btnToggleEditName.ImageOptions.SvgImage = _isEditingName
    //? SvgImages.Actions.Lock // условно, иконка "lock"
    //: SvgImages.Programming.Edit; // условно, иконка "edit"

            //comboBoxRecipes.Properties.ReadOnly = !_isEditingName;

            if (_isEditingName)
                comboBoxRecipes.Focus();
        }

        #endregion

        #region Form

        private void RecipeEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_context.Dispose();
            DataUpdated?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Stuff

        private void SelectRecipe(Recipe recipe)
        {
            selectedRecipe = _context.Recipes
                .Include(r => r.RecipeStages.Select(rs => rs.RecipeParameters))
                .Include(r => r.RecipeStages.Select(rs => rs.baseRecipeStage.BaseParameters))
                .FirstOrDefault(r => r.id == recipe.id);

            if (selectedRecipe == null) return;

            var sortedStages = selectedRecipe.RecipeStages.OrderBy(s => s.stageNumb).ToList();
            recipeStageBindingList = new BindingList<RecipeStage>(sortedStages);
            gridControlStages.DataSource = recipeStageBindingList;

            UpdateButtonsState();
        }

        private void LoadBaseRecipeStages()
        {
            baseRecipeStages = _context.BaseRecipeStages.Include(b => b.BaseParameters).OrderBy(b => b.name).ToList();

            var stageLookup = new RepositoryItemLookUpEdit
            {
                DataSource = baseRecipeStages,
                DisplayMember = "name",
                ValueMember = "id",
                NullText = "[Выберите стадию]"
            };
            stageLookup.Columns.Add(new LookUpColumnInfo("name", "Стадия"));

            // Подписка на события
            stageLookup.EditValueChanged += StageLookup_EditValueChanged;

            gridViewStages.Columns[nameof(RecipeStage.baseRecipeStageId)].ColumnEdit = stageLookup;
        }

        // метод синхронизирует параметры текущей стадии рецепта (RecipeStage) с параметрами базовой стадии (BaseRecipeStage)
        private void UpdateRecipeStageParameters(RecipeStage stage, BaseRecipeStage baseStage)
        {
            var existing = _context.RecipeParameters.Where(p => p.recipeStageId == stage.id).ToList();
            var newParams = baseStage.BaseParameters.Select(bp =>
            {
                var match = existing.FirstOrDefault(ep => ep.baseParameterId == bp.id);
                return new RecipeParameter
                {
                    name = bp.name,
                    description = bp.description,
                    baseParameterId = bp.id,
                    recipeStageId = stage.id,
                    value = match?.value ?? 0.0,
                    active = match?.active ?? true
                };
            }).ToList();

            _context.RecipeParameters.RemoveRange(existing);
            _context.RecipeParameters.AddRange(newParams);
        }

        // Метод ValidateAllRecipeStagesParameters обновляет все параметры всех стадий текущего рецепта,
        // чтобы они соответствовали базовой структуре (базовым параметрам), при этом сохраняет уже введённые значения, если они совпадают по baseParameterId
        private void ValidateAllRecipeStagesParameters()
        {
            var stages = _context.RecipeStages
                .Include(rs => rs.RecipeParameters)
                .Include(rs => rs.baseRecipeStage.BaseParameters)
                .Where(rs => rs.recipeId == selectedRecipe.id)
                .ToList();

            foreach (var stage in stages)
                UpdateRecipeStageParameters(stage, stage.baseRecipeStage);

            _context.SaveChanges();
        }

        // удаление строки - стадии
        private void DeleteSelectedStage()
        {
            var rowHandle = gridViewStages.FocusedRowHandle;
            if (rowHandle < 0) return;

            var stage = gridViewStages.GetRow(rowHandle) as RecipeStage;
            if (stage == null) return;

            if (XtraMessageBox.Show($"Удалить стадию '{stage.name}' и её параметры?", "Удаление стадии",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _context.RecipeParameters.RemoveRange(stage.RecipeParameters);
            _context.RecipeStages.Remove(stage);
            _context.SaveChanges();

            RenumberStages(); // Перенумерация перед повторной загрузкой
            SelectRecipe(selectedRecipe);
        }

        // при удалении стадии обновляет поле stageNumb
        private void RenumberStages()
        {
            var stages = _context.RecipeStages.Where(s => s.recipeId == selectedRecipe.id).OrderBy(s => s.stageNumb).ToList();
            for (int i = 0; i < stages.Count; i++) stages[i].stageNumb = i + 1;
            _context.SaveChanges();
        }

        // сохранение имени рецепта
        private void SaveEditedRecipeName()
        {
            if (selectedRecipe == null) return;

            var newName = comboBoxRecipes.Text.Trim(); // Берем текст из LookUpEdit
            if (string.IsNullOrWhiteSpace(newName))
            {
                XtraMessageBox.Show("Имя рецепта не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_context.Recipes.Any(r => r.name == newName && r.id != selectedRecipe.id))
            {
                XtraMessageBox.Show("Рецепт с таким именем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedRecipe.name = newName;
            _context.SaveChanges();

            LoadRecipesToCombobox();
            comboBoxRecipes.EditValue = selectedRecipe.id;

            XtraMessageBox.Show("Имя рецепта обновлено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _isEditingName = false;
            comboBoxRecipes.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        }

        #endregion
    }
}
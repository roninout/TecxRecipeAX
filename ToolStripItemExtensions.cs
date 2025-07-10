using System;
using System.Windows.Forms;

namespace TecxRecipeAX
{
    public static class ToolStripItemExtensions
    {
        public static void SafeInvoke(this ToolStripItem item, Action action)
        {
            if (item == null) return;

            if (item.GetCurrentParent()?.InvokeRequired ?? false)
                item.GetCurrentParent().Invoke(action);
            else
                action();
        }
    }
}

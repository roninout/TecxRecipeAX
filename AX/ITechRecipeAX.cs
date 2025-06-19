using System;
using System.Runtime.InteropServices;

namespace TechRecipeAX
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]    
    [Guid("D2011371-CF5B-4CB0-BB05-FC4C3D4D0C30")]
    public interface ITechRecipeAX
    {
        [DispId(1)]
        string PageNameStrProperty { get; set; }

        [DispId(2)]
        int CurStageIntProperty { get; set; }

        [DispId(3)]
        int RecipeAccessIntProperty { get; set; }

        [DispId(4)]
        bool RecipeModeIntProperty { get; set; }
    }
}

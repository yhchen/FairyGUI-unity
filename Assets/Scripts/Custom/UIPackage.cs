namespace FairyGUI
{
    public partial class UIPackage
    {
        // oem: 是否在初始化中
        public static bool IsConstructing => _constructing > 0;
    }
}
namespace FairyGUI
{
    public partial class UIConfig
    {
        #region 多语言翻译用

        public static IGTranslateInterface TranslateInterface;

        #endregion

        /// <summary>
        /// 全屏界面顶部保留空隙
        /// </summary>
        public static float fFullScreenGapTop = 0;

        /// <summary>
        /// 全屏界面底部保留空隙
        /// </summary>
        public static float fFullScreenGapBot = 0;
        
        
        /// <summary>
        /// If disable DisplayObjectInfo. Developers will not be able to visually edit
        /// DisplayObject properties in Editor mode, nor will they be able to connect to
        /// the Poco SDK to get or set object properties
        /// </summary>
        public static bool disableDisplayObjectInfo = false;
    }
}
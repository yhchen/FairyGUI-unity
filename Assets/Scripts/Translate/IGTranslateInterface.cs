namespace FairyGUI
{
    /// <summary>
    /// 翻译接入接口，用于多语言翻译
    /// </summary>
    public interface IGTranslateInterface
    {
        /// <summary>
        /// 翻译为语言包Id
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="stringId">语言包Id</param>
        /// <returns></returns>
        public string TranslateStringId(GTextField textField, string stringId);

        /// <summary>
        /// 翻译为语言包Id
        /// </summary>
        /// <param name="inputTextField"></param>
        /// <param name="stringId">语言包Id</param>
        /// <returns></returns>
        public string TranslatePromptTextStringId(InputTextField inputTextField, string stringId);
    }
}
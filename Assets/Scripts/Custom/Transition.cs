namespace FairyGUI
{
    public partial class Transition
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Paused => this._paused;

        public PlayCompleteCallback onComplete
        {
            get => this._onComplete;
            set => this._onComplete = value;
        }
    }
}
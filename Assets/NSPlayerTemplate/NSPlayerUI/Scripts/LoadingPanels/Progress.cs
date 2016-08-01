namespace NSPlayerUI.LoadingPanels {
    public class Progress {
        /// <summary>
        /// プログレスイベントハンドラ
        /// </summary>
        public delegate void ProgressEventHandler(object sender, Progress progress);

        /// <summary>
        /// プログレスの状態が変更されたときのイベント
        /// </summary>
        public event ProgressEventHandler ChangedEvent = delegate { };

        /// <summary>
        /// プログレスの種類
        /// </summary>
        public enum ProgressType {
            Nothing,
            Downloading,
            LoadingModel,
            LoadingMotion,
            TakingThumbnail
        }

        /// <summary>
        /// 有効かどうか
        /// </summary>
        public bool Enabled {
            get {
                return _enabled;
            }
            set {
                _enabled = value;
                ChangedEvent.Invoke(this, this);
            }
        }
        private bool _enabled;

        /// <summary>
        /// プログレスバーを有効にするかどうか
        /// </summary>
        public bool ProgressBarEnabled {
            get {
                return _progressBarEnabled;
            }
            set {
                _progressBarEnabled = value;
                ChangedEvent.Invoke(this, this);
            }
        }
        private bool _progressBarEnabled;

        /// <summary>
        /// プログレスバーの値
        /// </summary>
        public float ProgressBarValue {
            get {
                return _progressBarValue;
            }
            set {
                _progressBarValue = value;
                ChangedEvent.Invoke(this, this);
            }
        }
        private float _progressBarValue;

        /// <summary>
        /// プログレスの種類
        /// </summary>
        public ProgressType Type {
            get {
                return _type;
            }
            set {
                _type = value;
                ChangedEvent.Invoke(this, this);
            }
        }
        private ProgressType _type;

        /// <summary>
        /// プログレスの初期化
        /// </summary>
        public void Reset() {
            Enabled = false;
            ProgressBarEnabled = false;
            ProgressBarValue = 0;
            Type = ProgressType.Nothing;
        }
    }
}

using UnityEngine;

namespace NSPlayerUI {
    /// <summary>
    /// UIの状態を保持するクラス
    /// </summary>
    public class Status {
        /// <summary>
        /// ステータスイベントハンドラ
        /// </summary>
        public delegate void StatusEventHandler(object sender, Status status);

        /// <summary>
        /// ステータスが変更されたときに呼び出されるイベント
        /// </summary>
        public StatusEventHandler ChangedEvent = delegate { };

        /// <summary>
        /// 操作パネルが表示されているかどうか
        /// </summary>
        public bool ActionPanelActived {
            get { return _actionPanelActived; }
            set { _actionPanelActived = value; InvokeChangedEvent(); }
        }
        private bool _actionPanelActived;

        /// <summary>
        /// アニメーションのコントロールができるかどうか
        /// </summary>
        public bool AnimationControllable {
            get { return _animationControllable; }
            set { _animationControllable = value; InvokeChangedEvent(); }
        }
        private bool _animationControllable;

        /// <summary>
        /// アニメーションを再生中かどうか
        /// </summary>
        public bool AnimationPlaying {
            get { return _animationPlaying; }
            set { _animationPlaying = value; InvokeChangedEvent(); }
        }
        private bool _animationPlaying;

        /// <summary>
        /// カメラモーションがコントロールできるかどうか
        /// </summary>
        public bool CameraMotionControllable {
            get { return _cameraMotionControllable; }
            set { _cameraMotionControllable = value; InvokeChangedEvent(); }
        }
        private bool _cameraMotionControllable;

        /// <summary>
        /// カメラモーションを再生中かどうか
        /// </summary>
        public bool CameraMotionPlaying {
            get { return _cameraMotionPlaying; }
            set { _cameraMotionPlaying = value; InvokeChangedEvent(); }
        }
        private bool _cameraMotionPlaying;

        /// <summary>
        /// 選択されているデフォルトモーション
        /// </summary>
        public DefaultMotionPanels.Type DefaultMotion {
            get { return _defaultMotion; }
            set { _defaultMotion = value; InvokeChangedEvent(); }
        }
        private DefaultMotionPanels.Type _defaultMotion;

        /// <summary>
        /// デフォルトモーションが有効かどうか
        /// </summary>
        public bool DefaultMotionEnabled {
            get { return _defaultMotionEnabled; }
            set { _defaultMotionEnabled = value; InvokeChangedEvent(); }
        }
        private bool _defaultMotionEnabled;

        /// <summary>
        /// デフォルトモーションパネルが表示されているかどうか
        /// </summary>
        public bool DefaultMotionPanelActived {
            get { return _defaultMotionPanelActived; }
            set { _defaultMotionPanelActived = value; InvokeChangedEvent(); }
        }
        private bool _defaultMotionPanelActived;

        /// <summary>
        /// フルスクリーンかどうか
        /// </summary>
        public bool FullScreen {
            get { return _fullScreen; }
            set { _fullScreen = value; InvokeChangedEvent(); }
        }
        private bool _fullScreen;

        /// <summary>
        /// 使い方パネルが表示されているかどうか
        /// </summary>
        public bool HowToPanelActived {
            get { return _howToPanelActived; }
            set { _howToPanelActived = value; InvokeChangedEvent(); }
        }
        private bool _howToPanelActived;

        /// <summary>
        /// ロードの進行状況
        /// </summary>
        public LoadingPanels.Progress LoadingProgress { get; private set; }

        /// <summary>
        /// 設定パネルが表示されてるかどうか
        /// </summary>
        public bool SettingPanelActived {
            get { return _settingPanelActived; }
            set { _settingPanelActived = value; InvokeChangedEvent(); }
        }
        private bool _settingPanelActived;

        /// <summary>
        /// ステータスが変更されたイベントを実行する。
        /// </summary>
        private void InvokeChangedEvent() {
            if (!_disableEvent) {
                ChangedEvent.Invoke(this, this);
            }
        }

        /// <summary>
        /// ステータスイベントの発火を無効化するかどうか
        /// </summary>
        private bool _disableEvent;

        /// <summary>
        /// クラスを初期化する。
        /// </summary>
        public Status() {
            LoadingProgress = new LoadingPanels.Progress();
            Reset();
        }

        /// <summary>
        /// ステータスを初期化する。
        /// </summary>
        public void Reset() {
            _disableEvent = true;
            
            ActionPanelActived = false;
            AnimationControllable = false;
            AnimationPlaying = false;
            CameraMotionControllable = false;
            CameraMotionPlaying = false;
            DefaultMotion = DefaultMotionPanels.Type.Normal;
            DefaultMotionEnabled = false;
            DefaultMotionPanelActived = false;
            FullScreen = false;
            HowToPanelActived = false;
            LoadingProgress.Reset();
            SettingPanelActived = false;

            _disableEvent = false;
            InvokeChangedEvent();
        }
    }
}

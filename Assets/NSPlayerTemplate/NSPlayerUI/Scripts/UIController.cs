using UnityEngine;

namespace NSPlayerUI {
    /// <summary>
    /// ユーザインターフェイスのコントローラ
    /// </summary>
    public class UIController : MonoBehaviour {
        /// <summary>
        /// クリックイベントハンドラ
        /// </summary>
        public delegate void ClickEventHandler(object sender);

        /// <summary>
        /// リセットイベントハンドラ
        /// </summary>
        public delegate void ResetEventHandler();

        /// <summary>
        /// トグルイベントハンドラ
        /// </summary>
        public delegate void ToggleEventHandler(object sender, bool toggle);

        /// <summary>
        /// デフォルトモーションイベントハンドラ
        /// </summary>
        public delegate void DefaultMotionEventHandler(object sender, DefaultMotionPanels.Type type);

        /// <summary>
        /// ユーザー入力関連のイベントハンドラ
        /// </summary>
        public delegate void InputEventHandler(object sender, InputPanels.InputEventArgs e);

        /// <summary>
        /// シークバー関連のイベントハンドラ
        /// </summary
        public delegate void SeekBarEventHandler(object sender, ToolBars.SeekBarEventArgs e);

        /// <summary>
        /// カメラモーションの状態を切り替えたときに呼び出されるイベント。
        /// </summary>
        public ToggleEventHandler CameraMotionToggledEvent = delegate { };

        /// <summary>
        /// カメラリセットをクリックした時に呼び出されるイベント。
        /// </summary>
        public ClickEventHandler CameraResetClickedEvent = delegate { };

        /// <summary>
        /// デフォルトモーションを変更した時に呼び出されるイベント。
        /// </summary>
        public DefaultMotionEventHandler DefaultMotionChangedEvent = delegate { };

        /// <summary>
        /// ユーザが操作をした時に呼び出されるイベント
        /// </summary>
        public InputEventHandler InputEvent = delegate { };

        /// <summary>
        /// リセットを実行した時に呼び出されるイベント
        /// </summary>
        public ResetEventHandler ResetEvent = delegate { };

        /// <summary>
        /// アニメーションの再生状態を切り替えた時に呼び出されるイベント
        /// </summary>
        public ToggleEventHandler AnimationToggledEvent = delegate { };

        /// <summary>
        /// シークバーのシーク状態を変更した時に呼び出されるイベント
        /// </summary>
        public SeekBarEventHandler SeekBarSoughtEvent = delegate { };

        /// <summary>
        /// ツールバーの状態
        /// </summary>
        public Status Status {
            get {
                if (_status == null) {
                    _status = new Status();
                }
                return _status; 
            }
        }
        private Status _status;

        /// <summary>
        /// プレイヤーの設定
        /// </summary>
        public Setting Setting {
            get {
                if (_setting == null) {
                    _setting = new Setting();
                }
                return _setting;
            }
        }
        private Setting _setting;

        /// <summary>
        /// シークバー
        /// </summary>
        private ToolBars.SeekBar _seekBar;

        private void Awake() {
            FindUI();
        }

        /// <summary>
        /// UIを探す。
        /// </summary>
        private void FindUI() {
            _seekBar = transform.FindChild("ToolBar/SeekBar").GetComponent<ToolBars.SeekBar>();
        }

        /// <summary>
        /// カメラの再生状態切り替えイベントを実行する。
        /// </summary>
        /// <param name="sender">イベントの送信元</param>
        /// <param name="toggle">状態</param>
        public void InvokeCameraMotionToggledEvent(object sender, bool toggle) {
            CameraMotionToggledEvent.Invoke(sender, toggle);
        }

        /// <summary>
        /// カメラの再生状態切り替えイベントを実行する。
        /// </summary>
        /// <param name="sender">イベントの送信元</param>
        public void InvokeCameraResetClikedEvent(object sender) {
            CameraResetClickedEvent.Invoke(sender);
        }

        /// <summary>
        /// デフォルトモーション変更のイベントを実行する。
        /// </summary>
        /// <param name="sender">イベントの送信元</param>
        /// <param name="type">モーションの種類</param>
        public void InvokeDefaultMotionChangedEvent(object sender, DefaultMotionPanels.Type type) {
            DefaultMotionChangedEvent.Invoke(sender, type);
        }

        /// <summary>
        /// ユーザ入力イベントを実行する。
        /// </summary>
        /// <param name="sender">イベントの送信元</param>
        /// <param name="e">イベントデータ</param>
        public void InvokeInputEvent(object sender, InputPanels.InputEventArgs e) {
            InputEvent.Invoke(sender, e);
        }

        /// <summary>
        /// 再生状態の切り替えイベントを実行する。
        /// </summary>
        /// <param name="sender">イベントの送信元</param>
        /// <param name="e">イベントデータ</param>
        public void InvokeAnimationToggledEvent(object sender, bool toggle) {
            AnimationToggledEvent.Invoke(sender, toggle);
        }

        /// <summary>
        /// シークバーの状態を変更するイベントを実行する。
        /// </summary>
        /// <param name="sender">イベントの送信元</param>
        /// <param name="e">イベントのデータ</param>
        public void InvokeSeekBarSoughtEvent(object sender, ToolBars.SeekBarEventArgs e) {
            SeekBarSoughtEvent.Invoke(sender, e);
        }

        /// <summary>
        /// UIを初期化する。
        /// </summary>
        public void Reset() {
            SetSeekBarPosition(0);
            Status.Reset();
            ResetEvent.Invoke();
        }

        /// <summary>
        /// シークバーのポジションをセットする。
        /// </summary>
        /// <param name="normalizedValue">正規化されたシークバーの値</param>
        public void SetSeekBarPosition(float normalizedValue) {
            _seekBar.Seek(normalizedValue);
        }
    }
}

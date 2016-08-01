using UnityEngine;
using NSPlayerUI.ActionPanels;

namespace NSPlayerUI {
    public class ActionPanel : Utils.UIMonoBehaviour {
        /// <summary>
        /// ボタンが有効なときに適用される色
        /// </summary>
        private readonly Color32 ENABLED_COLOR = new Color32(255, 255, 255, 223);

        /// <summary>
        /// ボタンが無効なときに適用される色
        /// </summary>
        private readonly Color32 DISABLED_COLOR = new Color32(255, 255, 255, 50);

        /// <summary>
        /// アクションパネルのモード
        /// </summary>
        private enum Mode {
            Movement = 0, Rotation, Magnification
        }

        /// <summary>
        /// アクションパネルのモード設定状態
        /// </summary>
        private Mode _currentMode;

        private void Awake() {
            UIController.Status.ChangedEvent += OnUIStatusChanged;
        }

        private void Start() {
            _currentMode = Mode.Movement;
            UpdateActionPanel();
        }

        /// <summary>
        /// アクションモードを切り替える時の処理
        /// </summary>
        public void OnNextActionModeClicked() {
            _currentMode++;
            if (_currentMode == Mode.Magnification + 1) { _currentMode = Mode.Movement; }
            UpdateActionPanel();
        }

        /// <summary>
        /// 上ボタンをクリックしたときの処理
        /// </summary>
        public void OnUpClicked() {
            CreateInputEvent(new InputValue(Screen.width / 10 * -1, 0));
        }

        /// <summary>
        /// 上ボタンをクリックしたときの処理
        /// </summary>
        public void OnDownClicked() {
            CreateInputEvent(new InputValue(Screen.width / 10, 0));
        }

        /// <summary>
        /// 上ボタンをクリックしたときの処理
        /// </summary>
        public void OnRightClicked() {
            CreateInputEvent(new InputValue(0, Screen.width / 10 * -1));
        }

        /// <summary>
        /// 上ボタンをクリックしたときの処理
        /// </summary>
        public void OnLeftClicked() {
            CreateInputEvent(new InputValue(0, Screen.width / 10));
        }

        /// <summary>
        /// UIの変更を処理するイベント
        /// </summary>
        private void OnUIStatusChanged(object sender, Status status) {
            gameObject.SetActive(status.ActionPanelActived);
        }

        /// <summary>
        /// 入力イベントを生成する。
        /// </summary>
        private void CreateInputEvent(InputValue value) {
            switch (_currentMode) {
                case Mode.Movement:
                    UIController.InvokeInputEvent(this, new InputPanels.InputEventArgs(value.Horizon, value.Vertical, 0, InputPanels.Button.RIGHT));
                    break;
                case Mode.Rotation:
                    UIController.InvokeInputEvent(this, new InputPanels.InputEventArgs(value.Horizon, value.Vertical, 0, InputPanels.Button.LEFT));
                    break;
                case Mode.Magnification:
                    UIController.InvokeInputEvent(this, new InputPanels.InputEventArgs( 0, 0, value.Vertical * -1, InputPanels.Button.NONE));
                    break;
            }
        }

        /// <summary>
        /// アクションパネルの表示を更新する。
        /// </summary>
        private void UpdateActionPanel() {
            UpdateModePanel();
            UpdateActionChangeButton();
            UpdateControllButtons();
        }

        /// <summary>
        /// アクション変更ボタンの表示を更新する。
        /// </summary>
        private void UpdateActionChangeButton() {
            transform.FindChild("Buttons/Movement").gameObject.SetActive(_currentMode == Mode.Movement);
            transform.FindChild("Buttons/Rotation").gameObject.SetActive(_currentMode == Mode.Rotation);
            transform.FindChild("Buttons/Magnification").gameObject.SetActive(_currentMode == Mode.Magnification);
        }

        /// <summary>
        /// コントロールボタンの表示を更新する。
        /// </summary>
        private void UpdateControllButtons() {
            Color col = (_currentMode == Mode.Magnification) ? DISABLED_COLOR : ENABLED_COLOR ;
            transform.FindChild("Buttons/Right").GetComponent<UnityEngine.UI.Image>().color = col;
            transform.FindChild("Buttons/Left").GetComponent<UnityEngine.UI.Image>().color = col;
        }

        /// <summary>
        /// モードパネル表示を更新する。
        /// </summary>
        private void UpdateModePanel() {
            transform.FindChild("ModePanels/Movement").gameObject.SetActive(_currentMode == Mode.Movement);
            transform.FindChild("ModePanels/Rotation").gameObject.SetActive(_currentMode == Mode.Rotation);
            transform.FindChild("ModePanels/Magnification").gameObject.SetActive(_currentMode == Mode.Magnification);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

namespace NSPlayerUI {
    public class ToolBar : Utils.UIMonoBehaviour {
        /// <summary>
        /// ツールバーのボタン一覧
        /// </summary>
        private Dictionary<string, GameObject> _buttons;

        /// <summary>
        /// シークバー
        /// </summary>
        private GameObject _seekBar;

        private void Awake() {
            AssignEvent();
            FindUI();
        }

        /// <summary>
        /// イベントを追加する。
        /// </summary>
        private void AssignEvent() {
            UIController.Status.ChangedEvent += OnUIStatusChanged;
        }

        /// <summary>
        /// UIを探す。
        /// </summary>
        private void FindUI() {
            _buttons = new Dictionary<string, GameObject>();
            foreach (Transform button in transform.FindChild("Buttons")) {
                _buttons.Add(button.name, button.gameObject);
            }
            _seekBar = transform.FindChild("SeekBar").gameObject;
        }

        /// <summary>
        /// アクションパネルの開閉ボタンがクリックされたときの処理
        /// </summary>
        public void OnActionPanelOpenOrCloseClicked() {
            UIController.Status.ActionPanelActived = !UIController.Status.ActionPanelActived;
        }

        /// <summary>
        /// カメラの再生/一時停止ボタンがクリックされたときの処理
        /// </summary>
        public void OnCameraPlayOrPauseClicked() {
            UIController.InvokeCameraMotionToggledEvent(this, !UIController.Status.CameraMotionPlaying);
        }

        /// <summary>
        /// デフォルトモーションボタンがクリックされたときの処理
        /// </summary>
        public void OnDefaultMotionCliked() {
            UIController.Status.DefaultMotionPanelActived = !UIController.Status.DefaultMotionPanelActived;
        }

        /// <summary>
        /// 再生/一時停止ボタンがクリックされたときの処理
        /// </summary>
        public void OnPlayOrPauseClicked() {
            UIController.InvokeAnimationToggledEvent(this, !UIController.Status.AnimationPlaying);
        }

        /// <summary>
        /// カメラの初期化ボタンがクリックされたときの処理
        /// </summary>
        public void OnCameraResetCliked() {
            UIController.InvokeCameraResetClikedEvent(this);
        }

        /// <summary>
        /// フルスクリーン/ウィンドウモードボタンがクリックされたときの処理
        /// </summary>
        public void OnFullScreenOrWindowClicked() {
            UIController.Status.FullScreen = !UIController.Status.FullScreen;
        }

        /// <summary>
        /// ヘルプの開閉ボタンクリックされたときの処理
        /// </summary>
        public void OnHelpOpenOrCloseClicked() {
            UIController.Status.SettingPanelActived = false;
            UIController.Status.HowToPanelActived = !UIController.Status.HowToPanelActived;
        }

        /// <summary>
        /// 設定の開閉ボタンがクリックされたときの処理
        /// </summary>
        public void OnSettingOpenOrCloseClicked() {
            UIController.Status.HowToPanelActived = false;
            UIController.Status.SettingPanelActived = !UIController.Status.SettingPanelActived;
        }

        /// <summary>
        /// UIの状態が変更されたときの処理
        /// </summary>
        private void OnUIStatusChanged(object sender, Status status) {
            UpdateToolBar(status);
        }

        /// <summary>
        /// ツールバーの状態を更新する。
        /// </summary>
        /// <param name="status">プレイヤーUIの状態</param>
        public void UpdateToolBar(Status status) {
            _seekBar.SetActive(status.AnimationControllable);

            _buttons["ActionPanelOpen"].SetActive(!status.ActionPanelActived);
            _buttons["ActionPanelClose"].SetActive(status.ActionPanelActived);

            _buttons["CameraPause"].SetActive(status.CameraMotionControllable && status.CameraMotionPlaying);
            _buttons["CameraPlay"].SetActive(status.CameraMotionControllable && !status.CameraMotionPlaying);

            _buttons["Pause"].SetActive(status.AnimationControllable && status.AnimationPlaying);
            _buttons["Play"].SetActive(status.AnimationControllable && !status.AnimationPlaying);

            _buttons["FullScreen"].gameObject.SetActive(!status.FullScreen);
            _buttons["Window"].gameObject.SetActive(status.FullScreen);

            _buttons["SettingOpen"].SetActive(!status.SettingPanelActived);
            _buttons["SettingClose"].SetActive(status.SettingPanelActived);

            _buttons["HelpOpen"].SetActive(!status.HowToPanelActived);
            _buttons["HelpClose"].SetActive(status.HowToPanelActived);

            _buttons["Normal"].SetActive(status.DefaultMotionEnabled && status.DefaultMotion == DefaultMotionPanels.Type.Normal);
            _buttons["Hand"].SetActive(status.DefaultMotionEnabled && status.DefaultMotion == DefaultMotionPanels.Type.Hand);
            _buttons["Walk"].SetActive(status.DefaultMotionEnabled && status.DefaultMotion == DefaultMotionPanels.Type.Walk);
            _buttons["Run"].SetActive(status.DefaultMotionEnabled && status.DefaultMotion == DefaultMotionPanels.Type.Run);
        }
    }
}

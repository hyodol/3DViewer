using UnityEngine;
using NSPlayerTemplate.Models;

namespace NSPlayerTemplate.Controllers {
    public class TemplateController : MonoBehaviour {
        /// <summary>
        /// NSPlayerTemplateで制御するカメラ (未指定の場合はメインカメラが制御対象のカメラとなる。)
        /// </summary>
        public Camera TargetCamera;

        /// <summary>
        /// カメラのデフォルト焦点 (未指定の場合は [0,0,0])
        /// </summary>
        public Vector3 DefaultCameraFocus;

        /// <summary>
        /// UIのゲームオブジェクト
        /// </summary>
        protected GameObject UI {
            get {
                if (_ui == null) {
                    _ui = transform.FindChild("NSPlayerUI").gameObject;
                }
                return _ui;
            }
        }
        private GameObject _ui;

        /// <summary>
        /// UIコントローラ
        /// </summary>
        protected NSPlayerUI.UIController UIController {
            get {
                if (_uiController == null) {
                    _uiController = _ui.GetComponent<NSPlayerUI.UIController>();
                }
                return _uiController;
            }
        }
        private NSPlayerUI.UIController _uiController;

        /// <summary>
        /// ジンバル制御カメラ
        /// </summary>
        protected GimbalCamera GimbalCamera {
            get {
                if (_gimbalCamera == null) {
                    Camera camera    = (TargetCamera != null) ? TargetCamera : Camera.main;
                    _gimbalCamera = new GimbalCamera(camera, DefaultCameraFocus);
                }
                return _gimbalCamera;
            }
        }
        private GimbalCamera _gimbalCamera;

        private void Awake() {
            UI.SetActive(true);
            AssignEvents();
        }

        private void Start() {
            UIController.Reset();
            UpdateSetting();
            UIController.Status.CameraMotionControllable = true;
        }

        /// <summary>
        /// イベントを登録する。
        /// </summary>
        private void AssignEvents() {
            UIController.CameraMotionToggledEvent += OnUICameraMotionToggled;
            UIController.CameraResetClickedEvent  += OnUICameraResetClicked;
            UIController.InputEvent               += OnUIInput;
            UIController.Setting.ChangedEvent     += OnUISettingChanged;
        }

        /// <summary>
        /// カメラモーションのトグルイベントを処理する。
        /// </summary>
        private void OnUICameraMotionToggled(object sender, bool toggle) {
            GimbalCamera.AutoRotation = toggle;
            UIController.Status.CameraMotionPlaying = toggle;
        }

        /// <summary>
        /// カメラリセットボタンのクリックイベントを処理する。
        /// </summary>
        private void OnUICameraResetClicked(object sender) {
            GimbalCamera.Reset();
            UIController.Status.CameraMotionPlaying = false;
        }

        /// <summary>
        /// UIの入力イベントを処理する。
        /// </summary>
        private void OnUIInput(object sender, NSPlayerUI.InputPanels.InputEventArgs e) {
            switch (e.Button) {
                case NSPlayerUI.InputPanels.Button.RIGHT:
                    GimbalCamera.Move(e.NormalizedDelta * -1);
                    break;
                case NSPlayerUI.InputPanels.Button.LEFT:
                    GimbalCamera.Rotate(e.NormalizedDelta * -1);
                    break;
            }

            if (e.Delta.z != 0.0f) {
                if (e.Delta.z < 0) {
                    GimbalCamera.ZoomOut();
                } else {
                    GimbalCamera.ZoomIn();
                }
            }

            GimbalCamera.AutoRotation = false;
            UIController.Status.CameraMotionPlaying = false;
        }

        /// <summary>
        /// UIの設定変更イベントを処理する。
        /// </summary>
        private void OnUISettingChanged(object sender, NSPlayerUI.Setting setting) {
            UpdateSetting();
        }

        /// <summary>
        /// 設定を更新する。
        /// </summary>
        private void UpdateSetting() {
            QualitySettings.SetQualityLevel(UIController.Setting.GraphycsQuality);
            GimbalCamera.CameraMovingSensitivity = 0.6f + (0.4f * _uiController.Setting.CameraSensitivity);
        }
    }
}

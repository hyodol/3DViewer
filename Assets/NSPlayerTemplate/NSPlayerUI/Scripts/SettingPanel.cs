using UnityEngine.UI;

namespace NSPlayerUI {
    public class SettingPanel : Utils.UIMonoBehaviour {
        /// <summary>
        /// 操作感設定のスライダー
        /// </summary>
        private Slider _sensitivitySlider;

        /// <summary>
        /// クオリティ設定のスライダー
        /// </summary>
        private Slider _qualitySlider;

        private void Awake() {
            FindUI();
            UIController.Status.ChangedEvent += OnUIStatusChanged;
        }

        private void Start() {
            InitializeUI();
        }

        /// <summary>
        /// UIを探す。
        /// </summary>
        private void FindUI() {
            _sensitivitySlider = transform.FindChild("Sensitivity").GetComponentInChildren<Slider>();
            _qualitySlider = transform.FindChild("Quality").GetComponentInChildren<Slider>();
        }

        /// <summary>
        /// UIを初期化する。
        /// </summary>
        private void InitializeUI() {
            _sensitivitySlider.value = UIController.Setting.CameraSensitivity * _sensitivitySlider.maxValue;

            _qualitySlider.maxValue = UIController.Setting.GraphycsQualityMax - 1;
            _qualitySlider.value = UIController.Setting.GraphycsQuality;
        }
        /// <summary>
        /// 操作感度を変更イベントを処理する。
        /// </summary>
        public void OnSensitivityChanged() {
            if (_sensitivitySlider != null) {
                UIController.Setting.CameraSensitivity = _sensitivitySlider.value / _sensitivitySlider.maxValue;
            }
        }

        /// <summary>
        /// クオリティ変更イベントを処理する。
        /// </summary>
        public void OnQualityChanged() {
            if (_qualitySlider != null) {
                UIController.Setting.GraphycsQuality = (int)_qualitySlider.value;
            }
        }

        /// <summary>
        /// UIの変更を処理するイベント
        /// </summary>
        private void OnUIStatusChanged(object sender, Status status) {
            gameObject.SetActive(status.SettingPanelActived);
        }
    }
}

using UnityEngine;

namespace NSPlayerUI {
    public class Setting {
        private const string CAMERA_SENSITIVITY_KEY = "NSPlayerUI.Setting.CameraSensitivity";
        private const string GRAPHYCS_QUALITY_KEY = "NSPlayerUI.Setting.GraphycsQuality";

        /// <summary>
        /// セッティングイベントハンドラ
        /// </summary>
        public delegate void SettingEventHandler(object sender, Setting setting);

        /// <summary>
        /// 設定が変更されたときに呼び出されるイベント
        /// </summary>
        public SettingEventHandler ChangedEvent = delegate { };
        
        /// <summary>
        /// カメラの操作感度
        /// </summary>
        public float CameraSensitivity {
            get {
                return PlayerPrefs.GetFloat(CAMERA_SENSITIVITY_KEY, 0.6f);
            }
            set {
                float sensitivity = Mathf.Clamp(value, 0f, 1f);
                PlayerPrefs.SetFloat(CAMERA_SENSITIVITY_KEY, sensitivity);
                InvokeChangedEvent();
            }
        }

        /// <summary>
        /// クオリティ
        /// </summary>
        public int GraphycsQuality {
            get {
                return PlayerPrefs.GetInt(GRAPHYCS_QUALITY_KEY, GraphycsQualityMax - 1);
            }
            set {
                int level = Mathf.Clamp(value, 0, GraphycsQualityMax - 1);
                PlayerPrefs.SetInt(GRAPHYCS_QUALITY_KEY, level);
                InvokeChangedEvent();
            }
        }

        /// <summary>
        /// クオリティ設定の最大値
        /// </summary>
        public int GraphycsQualityMax {
            get {
                return QualitySettings.names.Length;
            }
        }

        /// <summary>
        /// 設定変更のイベントを発火する。
        /// </summary>
        private void InvokeChangedEvent() {
            ChangedEvent.Invoke(this, this);
        }
    }
}

using UnityEngine;

namespace NSPlayerUI.Utils {
    public abstract class UIMonoBehaviour : MonoBehaviour {
        /// <summary>
        /// NSPlayerUIの名前
        /// </summary>
        public const string NS_PLAYER_UI_NAME = "NSPlayerUI";

        /// <summary>
        /// UIコントローラ
        /// </summary>
        protected UIController UIController {
            get {
                if (_uiController == null) {
                    _uiController = FindUIController();
                }
                return _uiController;
            }
        }
        private UIController _uiController;

        /// <summary>
        /// UIコントローラを探す。
        /// </summary>
        /// <returns></returns>
        private UIController FindUIController() {
            if (transform.name == NS_PLAYER_UI_NAME) {
                return transform.GetComponent<UIController>();
            } else {
                return transform.GetComponentInParent<UIController>();
            }
        }
    }
}

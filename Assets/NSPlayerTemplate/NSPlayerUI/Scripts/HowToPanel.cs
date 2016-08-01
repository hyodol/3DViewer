using UnityEngine.EventSystems;

namespace NSPlayerUI {
    public class HowToPanel : Utils.UIMonoBehaviour, IPointerDownHandler {
        private void Awake() {
            AssignEvent();
        }

        /// <summary>
        /// イベントを追加する。
        /// </summary>
        private void AssignEvent() {
            UIController.InputEvent += OnUserInput;
            UIController.Status.ChangedEvent += OnUIStatusChanged;
        }

        /// <summary>
        /// ユーザが入力を実行した時に呼び出されるイベント
        /// </summary>
        private void OnUserInput(object sender, InputPanels.InputEventArgs e) {
            if (UIController.Status.HowToPanelActived) {
                UIController.Status.HowToPanelActived = false;
            }
        }

        /// <summary>
        /// ユーザが操作パネルをクリックしたときに呼び出されるイベント
        /// </summary>
        public void OnPointerDown(PointerEventData e) {
            UIController.Status.HowToPanelActived = false;
        }

        /// <summary>
        /// UIの変更を処理するイベント
        /// </summary>
        private void OnUIStatusChanged(object sender, Status status) {
            gameObject.SetActive(status.HowToPanelActived);
        }
    }
}

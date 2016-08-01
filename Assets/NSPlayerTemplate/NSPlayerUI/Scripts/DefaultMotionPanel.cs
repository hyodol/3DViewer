namespace NSPlayerUI {
    public class DefaultMotionPanel : Utils.UIMonoBehaviour {
        private void Awake() {
            AssignEvent();
        }

        /// <summary>
        /// イベントを追加する。
        /// </summary>
        private void AssignEvent() {
            UIController.Status.ChangedEvent += OnUIStatusChanged;
        }

        /// <summary>
        /// 標準ボタンをクリックしたときの処理
        /// </summary>
        public void OnNormalClicked() {
            UIController.InvokeDefaultMotionChangedEvent(this, DefaultMotionPanels.Type.Normal);
            UIController.Status.DefaultMotionPanelActived = false;
        }

        /// <summary>
        /// 手振りボタンをクリックしたときの処理
        /// </summary>
        public void OnHandClicked() {
            UIController.InvokeDefaultMotionChangedEvent(this, DefaultMotionPanels.Type.Hand);
            UIController.Status.DefaultMotionPanelActived = false;
        }

        /// <summary>
        /// 歩きボタンをクリックしたときの処理
        /// </summary>
        public void OnWalkClicked() {
            UIController.InvokeDefaultMotionChangedEvent(this, DefaultMotionPanels.Type.Walk);
            UIController.Status.DefaultMotionPanelActived = false;
        }

        /// <summary>
        /// 走りボタンをクリックしたときの処理
        /// </summary>
        public void OnRunClicked() {
            UIController.InvokeDefaultMotionChangedEvent(this, DefaultMotionPanels.Type.Run);
            UIController.Status.DefaultMotionPanelActived = false;
        }

        /// <summary>
        /// UIの変更を処理するイベント
        /// </summary>
        private void OnUIStatusChanged(object sender, Status status) {
            gameObject.SetActive(status.DefaultMotionPanelActived);
        }
    }
}

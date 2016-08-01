using UnityEngine;
using System.Collections.Generic;

namespace NSPlayerUI {
    public class LoadingPanel : Utils.UIMonoBehaviour {
        /// <summary>
        /// 進行状況ラベル一覧
        /// </summary>
        private Dictionary<string, GameObject> _progress;

        /// <summary>
        /// プログレスバー
        /// </summary>
        private UnityEngine.UI.Slider _progressBar;
        
        private void Awake() {
            AssignEvent();
            FindUI();
        }

        /// <summary>
        /// イベントを追加する。
        /// </summary>
        private void AssignEvent(){
            UIController.Status.LoadingProgress.ChangedEvent += OnLoadingProgressChanged;
        }

        /// <summary>
        /// UIを探す。
        /// </summary>
        private void FindUI() {
            _progress = new Dictionary<string,GameObject>();
            foreach (Transform child in transform.FindChild("Popup/Progress")) {
                _progress.Add(child.name, child.gameObject);
            }

            _progressBar = transform.FindChild("Popup/ProgressBar").GetComponent<UnityEngine.UI.Slider>();
        }

        private void OnLoadingProgressChanged(object sender, LoadingPanels.Progress progress) {
            gameObject.SetActive(progress.Enabled);
            _progress["Downloading"].SetActive(progress.Type == LoadingPanels.Progress.ProgressType.Downloading);
            _progress["LoadingModel"].SetActive(progress.Type == LoadingPanels.Progress.ProgressType.LoadingModel);
            _progress["LoadingMotion"].SetActive(progress.Type == LoadingPanels.Progress.ProgressType.LoadingMotion);
            _progress["TakingThumbnail"].SetActive(progress.Type == LoadingPanels.Progress.ProgressType.TakingThumbnail);
            _progressBar.value = progress.ProgressBarValue;
            _progressBar.gameObject.SetActive(progress.ProgressBarEnabled);
        }
    }
}

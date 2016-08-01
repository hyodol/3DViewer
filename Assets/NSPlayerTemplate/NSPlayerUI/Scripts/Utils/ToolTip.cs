using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace NSPlayerUI.Utils {
    /// <summary>
    /// ツールチップを表示するためのBehaviour
    /// </summary>
    public class ToolTip : Utils.UIMonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        /// <summary>
        /// ツールチップ
        /// </summary>
        private GameObject _toolTip;

        /// <summary>
        /// ポインターがツールチップ表示対象に入っているかどうか
        /// </summary>
        private bool _pointerEntered;

        private void Awake() {
            _toolTip = transform.FindChild("ToolTip").gameObject;
            AssginEvents();
        }

        /// <summary>
        /// イベントを追加する。
        /// </summary>
        private void AssginEvents() {
            UIController.ResetEvent += OnReset;
        }

        /// <summary>
        /// リセットイベントを処理する。
        /// </summary>
        private void OnReset() {
            Toggle(false);
        }

        public void OnPointerEnter(PointerEventData e) {
            _pointerEntered = true;
            StartCoroutine(ShowDelay());
        }

        public void OnPointerExit(PointerEventData e) {
            _pointerEntered = false;
            Toggle(false);
        }

        /// <summary>
        /// ツールチップを遅延表示する。
        /// </summary>
        /// <param name="delaySeconds">遅延する秒数</param>
        private IEnumerator ShowDelay(float delaySeconds = 0.75f) {
            while (delaySeconds > 0) {
                yield return new WaitForEndOfFrame();
                if (!_pointerEntered) {
                    yield break;
                }
                delaySeconds -= Time.deltaTime;
            }
            Toggle(true);
        }

        /// <summary>
        /// ツールチップの表示状態を切り替える。
        /// </summary>
        /// <param name="toggle">表示状態</param>
        private void Toggle(bool toggle) {
            if (_toolTip != null && _toolTip.activeSelf != toggle) {
                _toolTip.SetActive(toggle);
            }
        }
    }
}

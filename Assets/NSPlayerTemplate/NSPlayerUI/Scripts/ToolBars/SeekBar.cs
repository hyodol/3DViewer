using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NSPlayerUI.ToolBars {
    public class SeekBar : Utils.UIMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
        /// <summary>
        /// ポインタがシークバーに乗った時のスケール
        /// </summary>
        public Vector3 ActivedSeekBarScale;

        /// <summary>
        /// ポインタがシークバーに乗った時のハンドルスケール
        /// </summary>
        public Vector3 ActivedHandleScale;

        /// <summary>
        /// シークバーをシーク中かどうか
        /// </summary>
        public bool Seeking { get; private set; }

        /// <summary>
        /// シークハンドル
        /// </summary>
        private GameObject _handle;

        /// <summary>
        /// スライダーのコンポーネント
        /// </summary>
        private Slider _slider;

        private void Awake() {
            FindUI();
            AssignEvent();
        }

        /// <summary>
        /// UIを探す。
        /// </summary>
        private void FindUI() {
            _slider = GetComponent<Slider>();
            _handle = transform.FindChild("Handle").gameObject;
        }

        private void AssignEvent() {
            // スライダーの値が変更された時に呼び出されるイベント
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        /// <summary>
        /// シークバーにポインタが入った時に呼び出される関数。
        /// </summary>
        public void OnPointerEnter(PointerEventData e) {
            transform.SetSiblingIndex(100);
            transform.localScale = ActivedSeekBarScale;
            _handle.transform.localScale = ActivedHandleScale;
        }

        /// <summary>
        /// シークバーからポインタが離れた時に呼び出される関数。
        /// </summary>
        public void OnPointerExit(PointerEventData e) {
            transform.SetSiblingIndex(0);
            transform.localScale = Vector3.one;
            _handle.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// ポインターの操作を開始した時に呼び出される関数。
        /// </summary>
        public void OnPointerDown(PointerEventData e) {
            Seeking = true;
            UIController.InvokeSeekBarSoughtEvent(this, new SeekBarEventArgs(_slider.value, true, true, false));
        }

        /// <summary>
        /// ポインターの操作を終了した時に呼び出される関数。
        /// </summary
        public void OnPointerUp(PointerEventData e) {
            Seeking = false;
            UIController.InvokeSeekBarSoughtEvent(this, new SeekBarEventArgs(_slider.value, false, false, true));
        }

        /// <summary>
        /// スライダーの値が変更されたときに呼び出される関数
        /// </summary>
        private void OnSliderValueChanged(float value) {
            if (Seeking) {
                UIController.InvokeSeekBarSoughtEvent(this, new SeekBarEventArgs(value, true));
            }
        }

        /// <summary>
        /// シークバーを指定位置までシークする。
        /// </summary>
        /// <param name="normalizedValue">正規化されたシークバーの値</param>
        public void Seek(float normalizedValue) {
            if (_slider != null && !Seeking) {
                _slider.value = normalizedValue;
            }
        }
    }
}

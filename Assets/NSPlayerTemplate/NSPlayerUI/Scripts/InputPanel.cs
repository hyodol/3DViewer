using NSPlayerUI.InputPanels;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NSPlayerUI {
    /// <summary>
    /// ユーザの画面入力を受け取るためのクラス
    /// </summary>
    public class InputPanel : Utils.UIMonoBehaviour, IDragHandler {

        private void Update() {
            CheckMouseWheel();
        }

        /// <summary>
        /// ドラッグイベントを処理する。
        /// </summary>
        /// <param name="e">イベントデータ</param>
        public void OnDrag(PointerEventData e) {
            InvokeEvent(e.delta.x, e.delta.y, 0, ConvToNSPlayerButton(e.button));
        }

        /// <summary>
        /// マウスホイールのスクロール量をチェックする
        /// </summary>
        private void CheckMouseWheel() {
            float delta = Input.GetAxis("Mouse ScrollWheel");
            if (delta != 0.0f) {
                InvokeEvent(0, 0, delta);
            }
        }

        /// <summary>
        /// UnityのPointerEventDataのボタンをInputEventArgsのボタンに変換する。
        /// </summary>
        /// <param name="button">ボタンデータ</param>
        /// <returns>変換されたボタンデータ</returns>
        private Button ConvToNSPlayerButton(PointerEventData.InputButton button) {
            switch (button) {
                case PointerEventData.InputButton.Left:
                    return Button.LEFT;
                case PointerEventData.InputButton.Middle:
                    return Button.MIDDLE;
                case PointerEventData.InputButton.Right:
                    return Button.RIGHT;
                default:
                    return Button.NONE;
            }
        }

        /// <summary>
        /// イベントを発生させる。
        /// </summary>
        /// <param name="x">X変化量</param>
        /// <param name="y">Y変化量</param>
        /// <param name="z">Z変化量</param>
        /// <param name="button">入力時に押されているボタン</param>
        private void InvokeEvent(float x, float y, float z, Button button = Button.NONE) {
            UIController.InvokeInputEvent(this, new InputEventArgs(x, y, z, button));
        }
    }
}

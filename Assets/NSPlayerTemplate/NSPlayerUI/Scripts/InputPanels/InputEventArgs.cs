using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace NSPlayerUI.InputPanels {
    public class InputEventArgs {
        /// <summary>
        /// 入力時に押されているボタン
        /// </summary>
        public Button Button { get; private set; }

        /// <summary>
        /// 入力量
        /// </summary>
        public Vector3 Delta { get; private set; }

        /// <summary>
        /// 画面の幅で正規化した入力量(XとYのみ)
        /// </summary>
        public Vector2 NormalizedDelta {
            get {
                return new Vector2(Delta.x / Screen.width, Delta.y / Screen.height);
            }
        }

        /// <summary>
        /// クラスを初期化する。
        /// </summary>
        /// <param name="x">X変化量</param>
        /// <param name="y">Y変化量</param>
        /// <param name="z">Z変化量</param>
        /// <param name="button">入力時に押されているボタン</param>
        public InputEventArgs(float x, float y, float z, Button button = Button.NONE) {
            Delta = new Vector3(x, y, z);
            Button = button;
        }
    }
}

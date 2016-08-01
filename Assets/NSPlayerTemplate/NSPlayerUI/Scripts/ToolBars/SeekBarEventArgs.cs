using UnityEngine;
using System.Collections;

namespace NSPlayerUI.ToolBars {
    public class SeekBarEventArgs {
        /// <summary>
        /// シークバーの値
        /// </summary>
        public float Value { get; private set; }

        /// <summary>
        /// シーク中かどうか
        /// </summary>
        public bool Seeking { get; private set; }

        /// <summary>
        /// シークのはじめかどうか
        /// </summary>
        public bool First { get; private set; }

        /// <summary>
        /// シークの終わりかどうか
        /// </summary>
        public bool Last { get; private set; }

        /// <summary>
        /// クラスを初期化する。
        /// </summary>
        /// <param name="value">シークバーの値</param>
        /// <param name="seeking">シークバーの状態</param>
        /// <param name="first">シークのはじめかどうか</param>
        /// <param name="last">シークのおわりかどうか</param>
        public SeekBarEventArgs(float value, bool seeking, bool first = false, bool last = false) {
            Value = value;
            Seeking = seeking;
            First = first;
            Last = last;
        }
    }
}

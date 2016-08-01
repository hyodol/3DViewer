using UnityEngine;

namespace NSPlayerTemplate.Utils {
    /// <summary>
    /// UnityのUpdateイベントを拾うクラス
    /// </summary>
    public class UpdateEventListener : MonoBehaviour {
        /// <summary>
        /// Updateイベントハンドラ
        /// </summary>
        public delegate void UpdateEventHander(float deltaTime);

        /// <summary>
        /// Updateイベント
        /// </summary>
        public UpdateEventHander Updated = delegate { };

        private void Update() {
            Updated.Invoke(Time.deltaTime);
        }
    }
}

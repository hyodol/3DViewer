using UnityEngine;
using System.Collections;

namespace NSPlayerUI.LoadingPanels {
    public class LoadingAnimation : MonoBehaviour {
        /// <summary>
        /// スプライト名`Images/LoadingPanel`下にあるMultipleなスプライト名を指定する。
        /// </summary>
        private readonly string _spriteName = "Loading";

        /// <summary>
        /// スプライト
        /// </summary>
        private Sprite[] _splites;

        /// <summary>
        /// 現在表示中のスプライト番号
        /// </summary>
        private int _current;

        /// <summary>
        /// 次の更新までの時間
        /// </summary>
        private float _nextUpdate;

        private void Start() {
            _splites = Resources.LoadAll<Sprite>("Images/LoadingPanel/" + _spriteName);
        }

        private void Update() {
            _nextUpdate -= Time.deltaTime;
            if (_nextUpdate < 0) {
                _nextUpdate = 0.1f;
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = GetNextSplite();
            }
        }

        /// <summary>
        /// 次に表示すべきスプライトを取得する。
        /// </summary>
        /// <returns></returns>
        private Sprite GetNextSplite() {
            if (_current + 2 > _splites.Length) {
                _current = 0;
            }
            return _splites[_current++];
        }
    }
}

using UnityEngine;

namespace NSPlayerUI {
    /// <summary>
    /// スクリーンサイズを制御するコントローラ
    /// </summary>
    public class ScreenSizeContoller : Utils.UIMonoBehaviour {
        /// <summary>
        /// サイズを保持する構造体
        /// </summary>
        public struct Size { public int Width; public int Height; }

        /// <summary>
        /// デフォルトのスクリーンサイズ
        /// </summary>
        private Size _defaultScreenSize = new Size();

        /// <summary>
        /// フルスクリーン状態かどうか
        /// </summary>
        private bool _fullScreen;

        private void Awake() {
            _defaultScreenSize.Width = Screen.width;
            _defaultScreenSize.Height = Screen.height;
            _fullScreen = false;
            AssignEvent();
        }

        /// <summary>
        /// イベントを追加する。
        /// </summary>
        private void AssignEvent() {
            UIController.Status.ChangedEvent += OnUIStatusChanged;
        }

        private void Update() {
            if (_fullScreen != Screen.fullScreen) {
                _fullScreen = Screen.fullScreen;
                if (UIController.Status.FullScreen != _fullScreen) {
                    UIController.Status.FullScreen = _fullScreen;
                }
            }
        }

        /// <summary>
        /// UIの状態が変更されたときのイベントを処理する。
        /// </summary>
        private void OnUIStatusChanged(object sender, Status status) {
            if (_fullScreen != status.FullScreen) {
                if (status.FullScreen) {
                    ChangeToFullScreen();
                } else {
                    ChangeToWindowed();
                }
            }
        }

        /// <summary>
        /// フルスクリーンに変更する。
        /// </summary>
        private void ChangeToFullScreen() {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true, Screen.currentResolution.refreshRate);
        }

        /// <summary>
        /// ウィンドウモードに変更する。
        /// </summary>
        private void ChangeToWindowed() {
            Screen.SetResolution(_defaultScreenSize.Width, _defaultScreenSize.Height, false);
        }
    }
}

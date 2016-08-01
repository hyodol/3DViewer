using UnityEngine;

namespace NSPlayerTemplate.Models {
    /// <summary>
    /// ジンバル状に移動するカメラ
    /// </summary>
    public class GimbalCamera : GimbalBase {
        /// <summary>
        /// カメラの描画領域(手前側)
        /// </summary>
        private readonly float DefaultNearClippingPlane = 0.01f;

        /// <summary>
        /// カメラの描画領域(奥側)
        /// </summary>
        private readonly float DefaultFarClippingPlane = 1000f;

        /// <summary>
        /// モデルとカメラの最小距離
        /// </summary>
        private readonly float DefaultNearMovableScope = 0.01f;

        /// <summary>
        /// モデルとカメラの最大距離
        /// </summary>
        private readonly float DefaultFarMovableScope = 1000f;

        /// <summary>
        /// 自動回転が有効かどうか
        /// </summary>
        public bool AutoRotation;

        /// <summary>
        /// カメラの移動感度(0.0f - 1.0f)
        /// </summary>
        public float CameraMovingSensitivity = 0.9f;

        /// <summary>
        /// カメラ
        /// </summary>
        /// <value>カメラ</value>
        public Camera OriginalCamera {
            get {
                return OriginalGameObject.GetComponent<Camera>();
            }
        }

        /// <summary>
        /// カメラの背景色
        /// </summary>
        public Color BackgroundColor {
            get {
                return OriginalCamera.backgroundColor;
            }
            set {
                OriginalCamera.backgroundColor = value;
            }
        }

        /// <summary>
        /// カメラの焦点
        /// </summary>
        /// <value>カメラの焦点</value>
        public Vector3 Focus {
            get {
                return Center;
            }
            set {
                Center = value;
            }
        }

        /// <summary>
        /// 焦点距離の位置に存在するものがカメラに投影される幅をカメラの画角から計算する。
        /// </summary>
        /// <value>アスペクト比が1の時の幅</value>
        public float FocusDisplaySpan {
            get {
                return CalculateDisplaySpan(Offset);
            }
        }

        /// <summary>
        /// カメラの描画領域(手前側)
        /// </summary>
        /// <value>手前側のカメラ描画領域</value>
        public float NearClippingPlane {
            get {
                return OriginalCamera.nearClipPlane;
            }
            set {
                OriginalCamera.nearClipPlane = Mathf.Max(value, 0.01f);
            }
        }

        /// <summary>
        /// カメラの描画領域(奥側)
        /// </summary>
        /// <value>奥側のカメラ描画領域</value>
        public float FarClippingPlane {
            get {
                return OriginalCamera.farClipPlane;
            }
            set {
                OriginalCamera.farClipPlane = Mathf.Min(value, 100000);
            }
        }

        /// <summary>
        /// モデルとカメラの最小距離
        /// </summary>
        public float NearMovableScope { get; set; }

        /// <summary>
        /// モデルとカメラの最大距離
        /// </summary>
        public float FarMovableScope { get; set; }

        /// <summary>
        /// カメラの画角(度数法)
        /// </summary>
        /// <value>The view angle.</value>
        public float ViewAngle {
            get {
                return OriginalCamera.fieldOfView;
            }
            set {
                OriginalCamera.fieldOfView = Mathf.Clamp(value, 1.0f, 179.0f);
            }
        }

        /// <summary>
        /// 常に中心点を見続けるかどうか
        /// </summary>
        public override bool AlwaysLookingCenter {
            get {
                return true;
            }
        }

        /// <summary>
        /// 焦点移動力
        /// </summary>
        private Vector3 _movingPowerFocus;

        /// <summary>
        /// アングル回転力
        /// </summary>
        private Vector2 _movingPowerAngle;

        /// <summary>
        // オフセット移動力
        /// </summary>
        private float _movingPowerOffset;

        /// <summary>
        /// カメラの移動を開始した時間
        /// </summary>
        private float _movingTime;

        /// <summary>
        /// カメラが移動中かどうか
        /// </summary>
        private bool _moving;

        /// <summary>
        /// デフォルトのカメラ焦点
        /// </summary>
        private readonly Vector3 _defaultFocus;

        /// <summary>
        /// デフォルトのカメラ位置
        /// </summary>
        private readonly Vector3 _defaultPosition;

        /// <summary>
        /// クラスを初期化する。
        /// </summary>
        /// <param name="camera">制御対象のカメラ</param>
        public GimbalCamera(Camera camera, Vector3 defaultFocus) {
            OriginalGameObject = camera.gameObject;

            _defaultFocus = defaultFocus;
            _defaultPosition = camera.transform.position;

            InitializeCameraObject();
            Reset();
        }

        /// <summary>
        /// カメラを生成する。
        /// </summary>
        private void InitializeCameraObject() {
            OriginalGameObject.AddComponent<Utils.UpdateEventListener>().Updated += CameraUpdate; // カメラのアップデートイベントをフックする
        }

        /// <summary>
        /// カメラを初期化する。
        /// </summary>
        public void Reset() {
            Focus = _defaultFocus;
            Position = _defaultPosition;
            NearClippingPlane = DefaultNearClippingPlane;
            FarClippingPlane = DefaultFarClippingPlane;
            NearMovableScope = DefaultNearMovableScope;
            FarMovableScope = DefaultFarMovableScope;

            _movingPowerAngle = Vector2.zero;
            _movingPowerFocus = Vector2.zero;
            _movingPowerOffset = 0;

            AutoRotation = false;
        }

        /// <summary>
        /// アスペクト比が1の時、指定された位置のカメラ描画領域を計算する。
        /// </summary>
        /// <param name="offset">カメラの描画領域</param>
        public float CalculateDisplaySpan(float offset) {
            return Mathf.Abs(offset) * Mathf.Tan(ViewAngle * Mathf.Deg2Rad);
        }

        /// <summary>
        /// 目標焦点、目標角度、目標オフセットに向かってスムーズに移動させる。
        /// </summary>
        private void CameraUpdate(float deltaTime) {
            if (AutoRotation) {
                YAngle += Time.deltaTime * 30;
                return;
            }

            if (!_moving) {
                return;
            }
            float movingProgress = 1 - (_movingTime - deltaTime);
            _movingTime -= deltaTime;

            if (movingProgress < 1.0f) {
                Vector2 deltaAngle = Vector2.Lerp(Vector2.zero, _movingPowerAngle, movingProgress);
                Vector3 deltaFocus = Vector3.Lerp(Vector3.zero, _movingPowerFocus, movingProgress);
                float deltaOffset  = Mathf.Lerp(0, _movingPowerOffset, movingProgress);

                _movingPowerAngle  -= deltaAngle;
                _movingPowerFocus  -= deltaFocus;
                _movingPowerOffset -= deltaOffset;

                Focus  += deltaFocus;
                XAngle += deltaAngle.x;
                YAngle += deltaAngle.y;
                Offset = Mathf.Clamp(Offset + deltaOffset, NearMovableScope, FarMovableScope);
            }
            else {
                _moving = false;
            }
        }

        /// <summary>
        /// カメラの移動を開始する。
        /// </summary>
        private void BeginMovementOfCamera() {
            _movingTime = CameraMovingSensitivity;
            _moving = true;
        }

        /// <summary>
        /// ジンバルの中心点を平行移動する。
        /// 中心点がカメラに映る範囲を1と換算して移動する。
        /// </summary>
        /// <param name="normalizedDirection">0-1の間で正規化された移動量</param>
        public void Move(Vector2 normalizedDirection) {
            // 水平方向移動
            _movingPowerFocus += OriginalCamera.transform.right * OriginalCamera.aspect * FocusDisplaySpan * normalizedDirection.x;

            // 垂直方向移動
            _movingPowerFocus += OriginalCamera.transform.up * FocusDisplaySpan * normalizedDirection.y;

            BeginMovementOfCamera();
        }

        /// <summary>
        /// X回転方向は180度を1、Y回転方向は360度を1と換算して回転する。
        /// </summary>
        /// <param name="normalizedDirection">0-1の間で正規化された回転量</param>
        public void Rotate(Vector2 normalizedDirection) {
            _movingPowerAngle.x += normalizedDirection.y * 180;
            _movingPowerAngle.y += normalizedDirection.x * 360;
            BeginMovementOfCamera();
            
        }

        /// <summary>
        /// ズームイン
        /// </summary>
        public void ZoomIn() {
            _movingPowerOffset += Offset * -0.1f;
            BeginMovementOfCamera();
        }

        /// <summary>
        /// ズームアウト
        /// </summary>
        public void ZoomOut() {
            _movingPowerOffset += Offset * 0.1f;
            BeginMovementOfCamera();
        }
    }
}

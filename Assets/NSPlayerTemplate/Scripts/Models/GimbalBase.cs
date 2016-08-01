using UnityEngine;

namespace NSPlayerTemplate.Models {
    /// <summary>
    /// ジンバル状に移動するオブジェクトの座標を計算する基底クラス。
    /// </summary>
    public abstract class GimbalBase {
        /// <summary>
        /// 操作対象のゲームオブジェクト
        /// </summary>
        protected GameObject OriginalGameObject;

        /// <summary>
        /// オブジェクトの座標
        /// </summary>
        public Vector3 Position {
            get {
                // X軸回転、Y軸回転、焦点距離から座標を計算する。
                float x = Mathf.Cos(_yAngleRadian) * Mathf.Cos(_xAngleRadian) * Offset + Center.x;
                float y = Mathf.Sin(_xAngleRadian) * Offset + Center.y;
                float z = Mathf.Sin(_yAngleRadian) * Mathf.Cos(_xAngleRadian) * Offset + Center.z;

                return new Vector3(x, y, z);
            }
            set {
                // X軸回転、Y軸回転、焦点距離を計算する。
                Vector3 distance = value - Center;

                YAngle = Mathf.Atan2(distance.z, distance.x) * Mathf.Rad2Deg;
                XAngle = Mathf.Atan2(distance.y, Mathf.Sqrt(Mathf.Pow(distance.x, 2f) + Mathf.Pow(distance.z, 2f))) * Mathf.Rad2Deg;

                Offset = (value - Center).magnitude;
            }
        }

        /// <summary>
        /// ジンバルの中心点
        /// </summary>
        public Vector3 Center {
            get {
                return _center;
            }
            set {
                _center = value;
                UpdateObjectPosition();
            }
        }
        private Vector3 _center;


        /// <summary>
        /// ジンバルの中心点からの距離
        /// </summary>
        public float Offset {
            get {
                return _offset;
            }
            set {
                _offset = (value >= 0.01f) ? value : 0.01f;
                UpdateObjectPosition();
            }
        }
        private float _offset;

        /// <summary>
        /// X軸を中心とする回転角度(度数法)
        /// </summary>
        public float XAngle {
            get {
                return Mathf.Rad2Deg * _xAngleRadian;
            }
            set {
                _xAngleRadian = Mathf.Deg2Rad * Mathf.Clamp(value, -89, 89); // ジンバルロック防止のため-89～89度で制限する。
                UpdateObjectPosition();
            }
        }
        private float _xAngleRadian;

        /// <summary>
        /// Y軸を中心とする回転角度(度数法)
        /// </summary>
        public float YAngle {
            get {
                return Mathf.Rad2Deg * _yAngleRadian;
            }
            set {
                if (0 < value && value < 360) {
                    _yAngleRadian = Mathf.Deg2Rad * value;
                }
                else {
                    float angle = value % 360;
                    _yAngleRadian = Mathf.Deg2Rad * ((angle < 0) ? 360 + angle : angle);
                }
                UpdateObjectPosition();
            }
        }
        private float _yAngleRadian;

        /// <summary>
        /// 常に中心点を見続けるかどうか
        /// </summary>
        public abstract bool AlwaysLookingCenter { get; }

        /// <summary>
        /// ゲームオブジェクトの位置を更新する。
        /// </summary>
        private void UpdateObjectPosition() {
            OriginalGameObject.transform.position = Position;
            if (AlwaysLookingCenter) {
                OriginalGameObject.transform.LookAt(Center);
            }
        }
    }
}

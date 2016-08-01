namespace NSPlayerUI.ActionPanels {
    /// <summary>
    /// 入力値を保持する構造体
    /// </summary>
    public class InputValue {
        /// <summary>
        /// 垂直入力値
        /// </summary>
        public int Vertical;

        /// <summary>
        /// 水平入力値
        /// </summary>
        public int Horizon;

        /// <summary>
        /// クラスを初期化する。
        /// </summary>
        /// <param name="vertical">垂直値</param>
        /// <param name="horizon">水平値</param>
        public InputValue(int vertical, int horizon) {
            Vertical = vertical;
            Horizon = horizon;
        }
    }
}

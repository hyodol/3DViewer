using System;

namespace NSPlayerUI.ToolBars {
    /// <summary>
    /// ツールバーの種類
    /// </summary>
    [System.Flags]
    public enum Type {
        None = 0x00,
        ModelMotion = 0x01,
        CameraMotion = 0x02
    }
}

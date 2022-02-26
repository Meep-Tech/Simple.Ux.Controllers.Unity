using UnityEngine;

namespace Simple.Ux.Controllers.Unity {
  /// <summary>
  /// Controls a simple ux element that can be placed in a column
  /// </summary>
  public interface IColumnElementController : IElementController {

    /// <summary>
    /// The column this is part of
    /// </summary>
    ColumnController Column {
      get;
    }

    /// <summary>
    /// The rectransform.
    /// </summary>
    RectTransform RectTransform {
      get;
    }
  }
}
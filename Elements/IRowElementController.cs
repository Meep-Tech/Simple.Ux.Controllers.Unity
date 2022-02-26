using UnityEngine;

namespace Simple.Ux.Controllers.Unity {
  /// <summary>
  /// Controls a simple ux element that can be placed in a row
  /// </summary>
  public interface IRowElementController : IElementController {

    /// <summary>
    /// The column this is part of
    /// </summary>
    RowController Row {
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
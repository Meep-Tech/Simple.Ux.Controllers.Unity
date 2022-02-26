using Simple.Ux.Data;

namespace Simple.Ux.Controllers.Unity {

  /// <summary>
  /// The base interface for an element of a View
  /// </summary>
  public interface IElementController {

    /// <summary>
    /// The parent that contains this element.
    /// </summary>
    public IElementContainerController Parent {
      get;
    }

    /// <summary>
    /// The controller for the view this is a part of
    /// </summary>
    ViewController View {
      get;
    }

    /// <summary>
    /// The element the controller represents
    /// </summary>
    IUxViewElement Element {
      get;
    }
  }
}
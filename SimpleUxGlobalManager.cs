using Simple.Ux.Controllers.Unity;
using UnityEngine;

namespace Simple.Ux.Managers.Unity {

  ///Manages global values for simple ux
  [DefaultExecutionOrder(-900)]
  public class SimpleUxGlobalManager : MonoBehaviour {

    #region Unity Inspector Set

    [SerializeField]

    public static ViewController DefaultViewPrefab {
      get;
      private set;
    } [SerializeField] ViewController _defaultViewPrefab;

    public static RectTransform DefaultTooltipPrefab {
      get;
      private set;
    } [SerializeField] RectTransform _defaultTooltipStylePrefab;

    #endregion

    /// <summary>
    /// The globals instance
    /// </summary>
    public static SimpleUxGlobalManager Globals {
      get;
      private set;
    }

    void Awake() {
      DefaultViewPrefab = _defaultViewPrefab;
      DefaultTooltipPrefab = _defaultTooltipStylePrefab;
      Globals = this;
    }
  }
}
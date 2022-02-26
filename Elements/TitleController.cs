using Simple.Ux.Data;
using Simple.Ux.Managers.Unity;
using UnityEngine;

namespace Simple.Ux.Controllers.Unity {
  public class TitleController : MonoBehaviour, IColumnElementController, IRowElementController {
    [SerializeField]
    TMPro.TextMeshProUGUI _titleText;

    ///<summary><inheritdoc/></summary>
    public ViewController View {
      get;
      internal set;
    }

    ///<summary><inheritdoc/></summary>
    public ColumnController Column {
      get;
      internal set;
    }

    ///<summary><inheritdoc/></summary>
    public RowController Row {
      get;
      internal set;
    }

    ///<summary><inheritdoc/></summary>
    public IElementContainerController Parent
      => (IElementContainerController)Column ?? Row;

    ///<summary><inheritdoc/></summary>
    public RectTransform RectTransform
      => __rectTransfrom ??= GetComponent<RectTransform>(); RectTransform __rectTransfrom;

    /// <summary>
    /// The title this represents
    /// </summary>
    public Title Title {
      get;
      private set;
    }

    /// <summary>
    /// If this is the Title for a Column Header
    /// </summary>
    public bool IsTopTitleForColumn {
      get;
      internal set;
    }

    ///<summary><inheritdoc/></summary>
    public IUxViewElement Element 
      => Title;

    internal void _initializeFor(Title titleData) {
      Title = titleData;
      _titleText.text = Column is not null 
        ? (IsTopTitleForColumn ? "///" : "//") + titleData.Text 
        : titleData.Text;

      // add tootltip
      if(!string.IsNullOrWhiteSpace(titleData.Tooltip)) {
        TooltipController tooltip = _titleText.gameObject.AddComponent<TooltipController>();
        tooltip.TooltipStylePrefab = SimpleUxGlobalManager.DefaultTooltipPrefab;
        tooltip.Text = titleData.Tooltip;
      }
    }
  }
}
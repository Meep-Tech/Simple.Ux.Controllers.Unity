using Simple.Ux.Data;
using Simple.Ux.Managers.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Simple.Ux.Controllers.Unity {
  public class PannelTabController : MonoBehaviour, IElementController {

    [SerializeField]
    TMPro.TextMeshProUGUI _titleText;

    [SerializeField]
    Button _tabButton;

    ///<summary><inheritdoc/></summary>
    public ViewController View {
      get;
      internal set;
    }

    /// <summary>
    /// The tab data
    /// </summary>
    public Pannel.Tab Tab
      => View.Data.GetTab(_key);

    internal string _key {
      get;
      private set;
    }

    ///<summary><inheritdoc/></summary>
    public IUxViewElement Element 
      => Tab;

    ///<summary><inheritdoc/></summary>
    public IElementContainerController Parent 
      => View;

    /// <summary>
    /// The pannel this tab controls.
    /// </summary>
    public PannelController Pannel 
      => View._pannels[_key];

    internal void _intializeFor(Pannel.Tab tabData) {
      _key = tabData.Key;
      _titleText.text = tabData.Name;

      // listener for active tab changing
      _tabButton.onClick.AddListener(() 
        => View._setActiveTab(Tab));

      // add tootltip
      if(!string.IsNullOrWhiteSpace(tabData.Tooltip)) {
        TooltipController tooltip = _titleText.gameObject.AddComponent<TooltipController>();
        tooltip.TooltipStylePrefab = SimpleUxGlobalManager.DefaultTooltipPrefab;
        tooltip.Text = tabData.Tooltip;
      }
    }
  }
}
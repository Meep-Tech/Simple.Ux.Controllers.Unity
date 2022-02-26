using UnityEngine;
using UnityEngine.EventSystems;

namespace Simple.Ux.Controllers.Unity.Utility {
  public class BringToFrontOnClickForUxMenuController : MonoBehaviour, IPointerDownHandler {

    /// <summary>
    /// What should be brought to the front when this is clicked.
    /// </summary>
    [SerializeField]
    RectTransform _target;

    public void OnPointerDown(PointerEventData eventData) {
      _target.SetAsLastSibling();
    }
  }
}
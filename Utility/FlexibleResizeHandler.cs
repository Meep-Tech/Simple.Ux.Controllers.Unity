using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Simple.Ux.Controllers.Unity.Utility {

  /// <summary>
  /// How this expands
  /// </summary>
  public enum HandlerType {
    TopRight,
    Right,
    BottomRight,
    Bottom,
    BottomLeft,
    Left,
    TopLeft,
    Top
  }

  /// <summary>
  /// Resizeable Handler for SimpleUx.
  /// </summary>
  [RequireComponent(typeof(EventTrigger))]
  public class FlexibleResizeHandler : MonoBehaviour {
    public HandlerType Type;
    public RectTransform Target;
    public Vector2 MinimumDimmensions 
      = new(50, 50);
    public Vector2 MaximumDimmensions 
      = new(800, 800);

    EventTrigger _eventTrigger;

    void Start() {
      _eventTrigger = GetComponent<EventTrigger>();
      _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
    }

    void OnDrag(BaseEventData data) {
      PointerEventData pointerEvent = (PointerEventData)data;
      RectTransform.Edge? horizontalEdge = null;
      RectTransform.Edge? verticalEdge = null;

      switch(Type) {
        case HandlerType.TopRight:
          horizontalEdge = RectTransform.Edge.Left;
          verticalEdge = RectTransform.Edge.Bottom;
          break;
        case HandlerType.Right:
          horizontalEdge = RectTransform.Edge.Left;
          break;
        case HandlerType.BottomRight:
          horizontalEdge = RectTransform.Edge.Left;
          verticalEdge = RectTransform.Edge.Top;
          break;
        case HandlerType.Bottom:
          verticalEdge = RectTransform.Edge.Top;
          break;
        case HandlerType.BottomLeft:
          horizontalEdge = RectTransform.Edge.Right;
          verticalEdge = RectTransform.Edge.Top;
          break;
        case HandlerType.Left:
          horizontalEdge = RectTransform.Edge.Right;
          break;
        case HandlerType.TopLeft:
          horizontalEdge = RectTransform.Edge.Right;
          verticalEdge = RectTransform.Edge.Bottom;
          break;
        case HandlerType.Top:
          verticalEdge = RectTransform.Edge.Bottom;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      if(horizontalEdge != null) {
        if(horizontalEdge == RectTransform.Edge.Right) {
          float newWidth = Mathf.Clamp(Target.sizeDelta.x - pointerEvent.delta.x, MinimumDimmensions.x, MaximumDimmensions.x);
          float deltaPosX = -(newWidth - Target.sizeDelta.x) * Target.pivot.x;

          Target.sizeDelta = new Vector2(newWidth, Target.sizeDelta.y);
          Target.anchoredPosition += new Vector2(deltaPosX, 0);
        }
        else {
          float newWidth = Mathf.Clamp(Target.sizeDelta.x + pointerEvent.delta.x, MinimumDimmensions.x, MaximumDimmensions.x);
          float deltaPosX = (newWidth - Target.sizeDelta.x) * Target.pivot.x;

          Target.sizeDelta = new Vector2(newWidth, Target.sizeDelta.y);
          Target.anchoredPosition += new Vector2(deltaPosX, 0);
        }
      }
      if(verticalEdge != null) {
        if(verticalEdge == RectTransform.Edge.Top) {
          float newHeight = Mathf.Clamp(Target.sizeDelta.y - pointerEvent.delta.y, MinimumDimmensions.y, MaximumDimmensions.y);
          float deltaPosY =0;

          Target.sizeDelta = new Vector2(Target.sizeDelta.x, newHeight);
          Target.anchoredPosition += new Vector2(0, deltaPosY);
        }
        else {
          float newHeight = Mathf.Clamp(Target.sizeDelta.y + pointerEvent.delta.y, MinimumDimmensions.y, MaximumDimmensions.y);
          float deltaPosY =0;

          Target.sizeDelta = new Vector2(Target.sizeDelta.x, newHeight);
          Target.anchoredPosition += new Vector2(0, deltaPosY);
        }
      }
    }
  }
  public static class FlexibleExtensions {
    public static void AddEventTrigger(this EventTrigger eventTrigger, UnityAction<BaseEventData> action,
        EventTriggerType triggerType) {
      EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
      trigger.AddListener(action);

      EventTrigger.Entry entry = new EventTrigger.Entry { callback = trigger, eventID = triggerType };
      eventTrigger.triggers.Add(entry);
    }
  }
}
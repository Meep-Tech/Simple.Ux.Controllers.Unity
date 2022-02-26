using Simple.Ux.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Simple.Ux.Controllers.Unity {

  public class RowController : MonoBehaviour, IColumnElementController, IElementContainerController {

    #region Unity Inspector Set Values

    [UnityEngine.Tooltip("The Prefab for Row Labels")]
    [SerializeField]
    TitleController _rowLabelPrefab;

    [SerializeField]
    [UnityEngine.Tooltip("where elements in this row are added")]
    internal RectTransform _elementsArea;

    [SerializeField]
    [UnityEngine.Tooltip("the row's scroll rect")]
    ScrollRect _scrollRect;

    [SerializeField]
    [UnityEngine.Tooltip("the row's overall rectransform")]
    RectTransform _rectTransform;

    #endregion

    public RectTransform RectTransform 
      => _rectTransform;

    public ViewController View {
      get;
      internal set;
    }

    /// <summary>
    /// The column this is in
    /// </summary>
    public ColumnController Column {
      get;
      internal set;
    }

    /// <summary>
    /// The row data
    /// </summary>
    public Row Row {
      get;
      private set;
    }

    /// <summary>
    /// The (optional) column title
    /// </summary>
    public TitleController Label {
      get;
      private set;
    }

    /// <summary>
    /// The rows of items in this column
    /// </summary>
    internal List<IElementController> _elements
      = new();

    ///<summary><inheritdoc/></summary>
    public IUxViewElement Element 
      => Row;

    ///<summary><inheritdoc/></summary>
    public IElementContainerController Parent
      => Column;

    readonly Dictionary<int, FieldController> _dirty
      = new();

    internal void _intializeFor(Row row) {
      Row = row;
      _scrollRect.viewport = Column._elementsArea;
      if(row.Label is not null) {
        Label = Instantiate(_rowLabelPrefab, _elementsArea);
        Label.Row = this;
        Label.IsTopTitleForColumn = true;
        Label._initializeFor(row.Label);
      }
    }

    internal FieldController _addField(DataField fieldData) {
      FieldController field = Instantiate(ViewController.FieldControllerPrefabs[fieldData.Type], _elementsArea);
      field.View = View;
      field.Row = this;
      field._intializeFor(fieldData);
      field.SwitchToRowMode(this);
      _elements.Add(field);

      if(fieldData.ShouldBeTrackedByView) {
        View._fields.Add(field.FieldData.DataKey.ToLower(), field);
      }

      return field;
    }
  }
}
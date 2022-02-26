using Simple.Ux.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simple.Ux.Controllers.Unity {

  public class ColumnController : MonoBehaviour, IElementController, IElementContainerController {

    #region Unity Inspector Set Values

    [UnityEngine.Tooltip("The Prefab for Column Titles")]
    [SerializeField]
    TitleController _columnTitleController;

    [UnityEngine.Tooltip("The Prefab for a Header inside of a Column")]
    [SerializeField]
    TitleController _inColumnHeaderController;

    [UnityEngine.Tooltip("The Prefab for a Row inside of a Column")]
    [SerializeField]
    RowController _rowController;

    [SerializeField]
    [UnityEngine.Tooltip("The component for the background image")]
    internal UnityEngine.UI.Image _backgroundImage;

    [SerializeField]
    [UnityEngine.Tooltip("where elements in this column are added")]
    internal RectTransform _elementsArea;

    [SerializeField]
    [UnityEngine.Tooltip("the column's scroll rect")]
    ScrollRect _scrollRect;

    #endregion

    public PannelController Pannel {
      get;
      internal set;
    }

    public ViewController View {
      get;
      internal set;
    }

    /// <summary>
    /// The (optional) column title
    /// </summary>
    public TitleController Title {
      get;
      private set;
    }

    /// <summary>
    /// The column this represents
    /// </summary>
    public Column Column {
      get;
      private set;
    }

    /// <summary>
    /// The rows of items in this column
    /// </summary>
    internal List<IElementController> _rows
      = new();

    public IUxViewElement Element 
      => Column;

    public IElementContainerController Parent
      => Pannel;

    internal void _intializeFor(Column column) {
      Column = column;
      _scrollRect.viewport = Pannel._columnArea;
      if(column.Title is not null) {
        Title = Instantiate(_columnTitleController, _elementsArea);
        Title.Column = this;
        Title.IsTopTitleForColumn = true;
        Title._initializeFor(column.Title);
      }
    }

    internal FieldController _addField(DataField fieldData) {
      FieldController field = Instantiate(ViewController.FieldControllerPrefabs[fieldData.Type], _elementsArea);
      field.View = View;
      field.Column = this;
      field._intializeFor(fieldData);
      _rows.Add(field);
      if(fieldData.ShouldBeTrackedByView) {
        View._fields.Add(field.FieldData.DataKey.ToLower(), field);
      }

      return field;
    }

    internal TitleController _addInColumnHeader(Title titleData) {
      TitleController header = Instantiate(_inColumnHeaderController, _elementsArea);
      header.View = View;
      header.Column = this;
      header.IsTopTitleForColumn = false;
      header._initializeFor(titleData);
      _rows.Add(header);

      return header;
    }

    internal RowController _addRow(Row rowData) {
      RowController row = Instantiate(_rowController, _elementsArea);
      row.View = View;
      row.Column = this;
      row._intializeFor(rowData);

      // add each field of the row:
      foreach(IUxViewElement element in rowData) {
        if(element is DataField fieldData) {
          FieldController field = row._addField(fieldData);
        } else
          throw new NotSupportedException($"Unknown Simple Ux Element for a Row: {element.GetType().FullName}");
      }

      _rows.Add(row);
      return row;
    }
  }
}
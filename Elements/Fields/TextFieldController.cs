﻿using Meep.Tech.Collections.Generic;
using Meep.Tech.Data;
using Simple.Ux.Data;
using Simple.Ux.Utility.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simple.Ux.Controllers.Unity {

  /// <summary>
  /// A simple ux controller for a text field
  /// </summary>
  public partial class TextFieldController : FieldController {

    [SerializeField]
    TMPro.TextMeshProUGUI _titleTextField;

    [SerializeField]
    TMPro.TMP_InputField _inputTextController;

    [SerializeField]
    TMPro.TextMeshProUGUI _inputTextField;

    [SerializeField]
    TMPro.TextMeshProUGUI _inputTextPlaceholder;

    [SerializeField]
    Image _inputTextBackground;

    public override HashSet<Type> ValidFieldDataTypes
      => base.ValidFieldDataTypes
        .Append(typeof(TextField));

    public override DataField.DisplayType DisplayType
      => DataField.DisplayType.Text;

    public override TitleController Title
      => _title ??= _titleTextField.GetComponent<TitleController>(); TitleController _title;

    public override object GetCurrentlyDisplayedValue()
      => _inputTextController.text;

    protected override void IntializeForFieldData() {
      RefreshCurrentDisplayForCurrentValue(FieldData.Value);
      if(FieldData.IsReadOnly) {
        _inputTextField.color = Color.white;
        _inputTextBackground.enabled = false;
        _inputTextPlaceholder.gameObject.SetActive(false);
        _inputTextController.richText = true;
        _inputTextController.lineType = TMPro.TMP_InputField.LineType.MultiLineNewline;

        RectTransform.pivot = RectTransform.pivot.ReplaceY(1);
        RectTransform.anchorMin = new Vector2(0, 1);
        RectTransform.anchorMax = new Vector2(1, 1);
        _inputTextController.readOnly = true;
        _inputTextController.isRichTextEditingAllowed = false;
      } else {
        if(FieldData is TextField textField) {
          _inputTextPlaceholder.text = textField.PlaceholderText;
        }
      }
    }

    protected override void RefreshCurrentDisplayForCurrentValue(object newValue) {
      _inputTextController.text = newValue as string;
    }

    protected override void AddOnChangeListener(DataField dataField) {
      _inputTextController.onValueChanged.AddListener(_ => OnFieldChanged());
    }

    protected override void SetFieldValid(bool toValid = true) {
      _inputTextBackground.color = toValid ? ValidFieldInputBackgroundColor : InvalidFieldInputBackgroundColor;
    }

    protected override void SetFieldEnabled(bool toEnabled = true) {
      _inputTextController.readOnly = FieldData.IsReadOnly || !toEnabled;
    }

    protected internal override void SwitchToRowMode(RowController forRow) {
      base.SwitchToRowMode(forRow);
      //_inputTextController.GetComponent<ContentSizeFitter>().horizontalFit 
        //= ContentSizeFitter.FitMode.PreferredSize;
    }
  }
}

using System;
using UnityEngine;
using UnityEditor;
using static External.Wrappers.CommonWrappers;

namespace External.EditorTools
{
     internal class RenamingTool: EditorWindow
     {
          private const string InputFieldName = "InputField_0";

          private string _baseName = null;
          private Transform[] _selectedTransforms;

          internal Transform[] SelectedTransforms
          {
               set => _selectedTransforms ??= _selectedTransforms = value;
          }

          private bool IsReturnButtonDown() => Event.current is { type: EventType.KeyDown, keyCode: KeyCode.Return };
          private bool IsEscapeButtonDown() => Event.current is { type: EventType.KeyDown, keyCode: KeyCode.Escape };

          // Don't swap those parts!
          private void OnGUI()
          {
               CurrentEventHandlers:
               CheckCondition(IsReturnButtonDown, then: TryRenameSelected);
               CheckCondition(IsEscapeButtonDown, then: this.Close);

               GuiStuff:
               GUILayout.Label($"Selected: {_selectedTransforms.Length}");
               GUI.SetNextControlName(InputFieldName);
               _baseName = GUILayout.TextField(_baseName);

               EditorGUI.FocusTextInControl(InputFieldName);
          }

          private void TryRenameSelected()
          {
               var command = new RenamingCommand();

               try
               {
                    command.Execute(_baseName);
               }
               catch(Exception ex)
               {
                    Debug.Log($"Could not rename selected transforms. Exception: {ex.Message}");
                    command.Undo();
               }
               finally
               {
                    this.Close();
               }
          }

          private void OnLostFocus() => this.Close();
     }
}

using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace External.EditorTools
{
     internal class RenamingToolInstantiator
     {
          private static Transform[] SelectedTransforms => Selection.GetTransforms(SelectionMode.ExcludePrefab);

          ///<summary>
          ///<example>Usage:</example> <c>Ctrl + E</c>
          ///<para/>
          ///      <remark>
          ///           todo: would be much better to implement this attribute like this:
          ///           <br/>
          ///           (with double tap, like in VS)
          ///           <br/>
          ///           [MenuItem("Tools/Hierarchy/Renaming Tool", KeyCode.Ctrl, KeyCode.E, KeyCodeE)]
          ///      </remark>
          ///</summary>
          [MenuItem("Tools/Hierarchy/Renaming Tool %E")]
          private static void TryInstanitiateTool()
          {
               var renamingTool = ScriptableObject.CreateInstance<RenamingTool>();
               renamingTool.SelectedTransforms = SelectedTransforms;
               renamingTool.titleContent = new GUIContent("Renaming Tool");
               renamingTool.ShowUtility();
          }

          [MenuItem("Tools/Hierarchy/Renaming Tool %E", isValidateFunction: true)]
          private static bool IsRenamingAvaliable()
          {
               return IsFocusedOnHierarchy && SelectedTransforms.Length > 0;
          }

          private static bool IsFocusedOnHierarchy
          {
               get
               {
                    string desiredTypeName = "UnityEditor.SceneHierarchyWindow";
                    var neighborOfDesiredType = typeof(UnityEditor.EditorWindow);
                    var coreModuleAssembly = Assembly.GetAssembly(neighborOfDesiredType);
                    var hierarchyWindowType = coreModuleAssembly.GetType(desiredTypeName);

                    return EditorWindow.focusedWindow.GetType() == hierarchyWindowType;
               }
          }
     }
}

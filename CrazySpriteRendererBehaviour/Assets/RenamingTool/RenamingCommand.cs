using System.Linq;
using UnityEngine;
using UnityEditor;

namespace External.EditorTools
{
     internal class RenamingCommand: IEditorCommand
     {
          private Transform[] _selectedTransformsCache;

          public void Execute(string baseName = null)
          {
               CacheSelectedItems();
               RememberItemsCondition();
               RenameSelectedItems(baseName);
          }

          public void Undo() => UnityEditor.Undo.RevertAllInCurrentGroup();

          private void CacheSelectedItems()
          {
               _selectedTransformsCache = Selection.GetTransforms(SelectionMode.ExcludePrefab);
          }

          private void RememberItemsCondition()
          {
               foreach(Transform item in _selectedTransformsCache)
               {
                    UnityEditor.Undo.RegisterCompleteObjectUndo(item.gameObject, "renaming selection");
               }
          }

          private void RenameSelectedItems(string baseName = null)
          {
               var sortedTransforms = _selectedTransformsCache.OrderBy(item => item.GetSiblingIndex());

               baseName ??= sortedTransforms.First().name;
               var transformsEnumerator = sortedTransforms.GetEnumerator();

               int counter = 0;
               while(transformsEnumerator.MoveNext())
               {
                    transformsEnumerator.Current.name = $"{baseName}_{counter++}";
                    EditorUtility.SetDirty(transformsEnumerator.Current);
               }
          }
     }
}

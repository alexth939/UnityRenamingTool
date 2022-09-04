namespace External.EditorTools
{
     internal interface IEditorCommand
     {
          void Execute(object argument) { }
          void Undo();
     }
}

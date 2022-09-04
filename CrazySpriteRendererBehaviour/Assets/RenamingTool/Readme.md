remarks:
1. the UnityEditor.Undo class has very confusing and unexpected behaviour.
  betta don't mess with it.
2. the UnityEngine.Event.current has weird behaviour too:
  (revealed case) when calling it in OnGui method,
  it works as expected ONLY if u'll handle incoming events BEFORE rendering anu gui stuff.
3. todo: maybe migrate to unreal?
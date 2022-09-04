using System;

namespace External.Wrappers
{
     public class CommonWrappers
     {
          public static void CheckCondition(Func<bool> condition, Action then)
          {
               then = condition() ? then : null;
               then?.Invoke();
          }
     }
}

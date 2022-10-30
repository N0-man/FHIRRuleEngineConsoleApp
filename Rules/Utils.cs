using RulesEngine.Extensions;
using RulesEngine.Models;

namespace ConsoleApp.Rules
{
  class Utils
  {
    public static void PrintResults(List<RuleResultTree> resultList)
    {
      bool outcome = false;

      outcome = resultList.TrueForAll(r => r.IsSuccess);

      resultList.OnSuccess((eventName) =>
      {
        Console.WriteLine($"Result '{eventName}' ✅");
        // foreach (var rs in resultList)
        // {
        //   Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(rs.ActionResult));
        //   Console.WriteLine(rs.ActionResult.Output);
        // }
        outcome = true;
      });

      resultList.OnFail(() =>
      {
        foreach (var rs in resultList)
        {
          Console.WriteLine(rs.ExceptionMessage);
        }
        outcome = false;
      });

      Console.WriteLine($"Test outcome: {outcome}.");
    }

    public static void Log(object o)
    {
      Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(o));
    }
  }

}
using System.Dynamic;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using RulesEngine.Models;

namespace ConsoleApp.Engine
{
  class GoalsRulesEngine
  {
    public static void Validate(Goal goal)
    {
      //Initialise Workflows to Execute: HealthPlan + Workflow per Goal Measure Code
      List<string> workFlows = new List<string>() { "HealthPlan" };
      workFlows.AddRange(RulesHelper.GetGoalMeasureCodes(goal.Target));

      var bre = InitializeRuleEngine(workFlows);

      // Evaluate each workflow
      foreach (var workflow in workFlows)
      {
        Console.WriteLine("Running Workflow: " + workflow);
        dynamic data = new ExpandoObject();

        if (workflow == "HealthPlan")
        {
          data.goalMeasureCode = RulesHelper.GetGoalMeasureCodes(goal.Target);
          data.goalCode = RulesHelper.GetGoalCode(goal);
        }
        else
        {
          // data.detail = RulesHelper.GetMeasure(goal.Target, workflow);
          data.due = RulesHelper.GetDue(goal.Target, workflow);
        }
        var inputs = new dynamic[] { data };
        List<RuleResultTree> resultList = bre.ExecuteAllRulesAsync(workflow, inputs).Result;

        Utils.PrintResults(resultList);
      }
    }

    private static RulesEngine.RulesEngine InitializeRuleEngine(List<string> fileNames)
    {
      List<Workflow> workflows = new List<Workflow>();
      var reSettingsWithCustomTypes = new ReSettings { CustomTypes = new Type[] { typeof(RulesHelper) } };

      foreach (var file in fileNames)
      {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{file}.json", SearchOption.AllDirectories);
        if (files == null || files.Length == 0)
          throw new Exception($"Rules file {file}.json not found.");

        var fileData = File.ReadAllText(files[0]);
        workflows.AddRange(JsonConvert.DeserializeObject<List<Workflow>>(fileData));
      }

      return new RulesEngine.RulesEngine(workflows.ToArray(), reSettingsWithCustomTypes);
    }

  }

}
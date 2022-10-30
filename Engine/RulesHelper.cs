using Hl7.Fhir.Model;
using static Hl7.Fhir.Model.Goal;

namespace ConsoleApp.Engine
{

  class RulesHelper
  {
    public static bool CheckContains(List<string> inputValues, string csvAllowedValues)
    {
      if (inputValues.Count == 0 || String.IsNullOrEmpty(csvAllowedValues))
        return false;

      var allowedValues = csvAllowedValues.Split(',').ToList();
      foreach (var value in inputValues)
      {
        if (!allowedValues.Contains(value)) return false;
      }
      return true;
    }

    public static List<string> GetGoalMeasureCodes(List<TargetComponent> target)
    {
      //TODO: Add null checks
      return target.Select(t => t.Measure.Coding[0].Code).ToList<string>();
    }

    public static DataType GetDetail(List<TargetComponent> target, string measureCode)
    {
      //TODO: Add null checks
      return target.First(t => t.Measure.Coding[0].Code == measureCode).Detail;
    }

    public static DataType? GetDue(List<TargetComponent> target, string measureCode)
    {
      //TODO: Add null checks
      var due = target.First(t => t.Measure.Coding[0].Code == measureCode).Due;
      if (due == null) return null;
      if (due.GetType() == typeof(Hl7.Fhir.Model.Duration))
        return (Hl7.Fhir.Model.Duration)due;
      else return (Hl7.Fhir.Model.Date)due;
    }

    public static string GetGoalCode(Goal goal)
    {
      //TODO: Add null checks
      return goal.Category[0].Coding[0].Code;
    }


  }

}
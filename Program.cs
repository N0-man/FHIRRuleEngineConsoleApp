using ConsoleApp;
using ConsoleApp.Engine;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

/*
- FIXME: Unable to RETURN context (success and error) from individual rule
*/

Console.WriteLine("Hello, This is FhirCarePlan ConsoleApp");

var inputHealthPlan = FileReader.ReadFhirInputFile("careplan.json");

FhirJsonParser fjp = new FhirJsonParser();
CarePlan myHealthPlan = fjp.Parse<CarePlan>(inputHealthPlan);

Console.WriteLine("Running HealthPlanRulesEngine...");
foreach (var item in myHealthPlan.Goal)
{
  Goal? goal = myHealthPlan.FindContainedResource(item) as Goal;
  if (goal != null)
    GoalsRulesEngine.Validate(goal);
}

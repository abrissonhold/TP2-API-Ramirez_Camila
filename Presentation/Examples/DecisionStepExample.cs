using Application.Request;
using Swashbuckle.AspNetCore.Filters;

public class DecisionStepExample : IExamplesProvider<DecisionStepRequest>
{
    public DecisionStepRequest GetExamples() => new()
    {
        Id = 1,
        User = 1,
        Status = 2,
        Observation = "Proyecto aprobado con modificaciones menores"
    };
}
using System.ComponentModel;

namespace Estimatz.Entities.Room
{
    public enum EstimateType
    {
        [Description("Estimativa padrão")]
        Default = 0,

        [Description("Estimativa trivariada")]
        Trivariate = 1
    }
}

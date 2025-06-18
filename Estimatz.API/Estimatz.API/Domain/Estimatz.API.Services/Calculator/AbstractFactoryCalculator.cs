using Estimatz.API.Entities.Room;

namespace Estimatz.API.Services.Calculator
{
    public static class AbstractFactoryCalculator
    {
        public static ICalculator GetFactory(EstimateType estimateType)
        {
            if (estimateType == EstimateType.Default)
                return new DefaultCalculator();

            if(estimateType == EstimateType.Trivariate)
                return new TrivariateCalculator();

            throw new ArgumentException("Erro: Não foi possível determinar o tipo de estimativa");
        }        
    }
}

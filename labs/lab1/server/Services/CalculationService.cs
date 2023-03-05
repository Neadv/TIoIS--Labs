using Grpc.Core;

namespace Lab1Server.Services
{
    public class CalculationService : Calculator.CalculatorBase
    {
        public override Task<CalculationReply> CalculateSumBetween(CalculationRequest request, ServerCallContext context)
        {
            if (request.From > request.To)
                throw new ArgumentOutOfRangeException();
            
            int result = 0;
            for (int i = request.From; i <= request.To; i++)
                result += i;
            
            return Task.FromResult(new CalculationReply
            {
                Sum = result
            });
        }
    }
}
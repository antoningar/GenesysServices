using Grpc.Core;

namespace Overdrawn.Services;

public class BalanceService : Balance.BalanceBase
{
    public override Task<BalanceResponse> GetBalance(BalanceRequest request, ServerCallContext context)
    {
        Random random = new();
        double randomValue = (random.NextDouble() * 20000) - 10000;
        float randomFloat = (float)randomValue;
        
        return Task.FromResult(new BalanceResponse
        {
            Balance = randomFloat 
        });
    }
}
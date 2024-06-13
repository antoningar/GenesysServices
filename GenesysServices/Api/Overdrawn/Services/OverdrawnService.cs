using Grpc.Net.Client;
using OverdrawnClient;

namespace Api.Overdrawn.Services;

public class OverdrawnService : IOverdrawnService
{
    public float GetClientBalance(string clientId)
    {
        using GrpcChannel channel = GrpcChannel.ForAddress(
            "https://localhost:7162");
        
        Balance.BalanceClient client = new(channel);
        BalanceResponse response = client.GetBalance(new BalanceRequest()
        {
            ClientId = clientId
        });
        
        return response.Balance;
    }
}
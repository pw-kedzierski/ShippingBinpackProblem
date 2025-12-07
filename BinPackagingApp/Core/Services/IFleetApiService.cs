using BinPackingApp.Core.Models;

namespace BinPackingApp.Core.Services;

public interface IFleetApiService
{
    Task<AnchorageResponse> GetRandomFleetAsync();
}

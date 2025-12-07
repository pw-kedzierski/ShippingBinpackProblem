using System.Net.Http.Json;
using BinPackingApp.Core.Models;

namespace BinPackingApp.Core.Services;

public class FleetApiService(HttpClient httpClient) : IFleetApiService
{
    private readonly HttpClient _httpClient = httpClient;
    private static readonly System.Text.Json.JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
    };

    public async Task<AnchorageResponse> GetRandomFleetAsync()
    {
        try
        {
            var fleetData = await _httpClient.GetFromJsonAsync<AnchorageResponse>("/api/fleets/random", JsonOptions)
                ?? throw new InvalidOperationException("API returned null response");

            if (fleetData.AnchorageSize == null)
            {
                throw new InvalidOperationException("AnchorageSize is missing in API response");
            }

            if (fleetData.AnchorageSize.Width <= 0 || fleetData.AnchorageSize.Height <= 0)
            {
                throw new InvalidOperationException(
                    $"Invalid anchorage size: {fleetData.AnchorageSize.Width}x{fleetData.AnchorageSize.Height}");
            }

            if (fleetData.Fleets == null || fleetData.Fleets.Count == 0)
            {
                throw new InvalidOperationException("No fleets in API response");
            }

            foreach (var fleet in fleetData.Fleets)
            {
                if (fleet.SingleShipDimensions == null)
                {
                    throw new InvalidOperationException($"Fleet {fleet.ShipDesignation} has null dimensions");
                }

                if (fleet.SingleShipDimensions.Width <= 0 || fleet.SingleShipDimensions.Height <= 0)
                {
                    throw new InvalidOperationException(
                        $"Fleet {fleet.ShipDesignation} has invalid dimensions: {fleet.SingleShipDimensions.Width}x{fleet.SingleShipDimensions.Height}");
                }

                if (fleet.ShipCount <= 0)
                {
                    throw new InvalidOperationException(
                        $"Fleet {fleet.ShipDesignation} has invalid count: {fleet.ShipCount}");
                }
            }

            return fleetData;
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to fetch fleet data. Network error: {ex.Message}", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("Request timeout. Please check your internet connection.", ex);
        }
        catch (System.Text.Json.JsonException ex)
        {
            throw new InvalidOperationException($"Failed to parse JSON response: {ex.Message}", ex);
        }
    }
}

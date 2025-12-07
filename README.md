# Shipping Bin Packing Problem

A modern, interactive web application for solving the bin packing problem in maritime logistics. This Blazor WebAssembly application allows users to manually arrange ships within an anchorage area using an intuitive drag-and-drop interface.

## 🚢 Overview

This application addresses the classic bin packing optimization problem in the context of ship anchorage management. Users receive random fleet configurations from an external API and must efficiently place vessels within a constrained anchorage area, maximizing space utilization while preventing collisions.

## ✨ Features

- **Interactive Drag-and-Drop Interface**: Intuitively place ships by dragging them from the palette onto the anchorage grid
- **Ship Rotation**: Double-click any placed ship to rotate it 90 degrees for better space utilization
- **Real-time Collision Detection**: Automatic validation prevents ships from overlapping
- **Boundary Checking**: Ships cannot be placed outside the anchorage boundaries
- **Visual Feedback**: Color-coded ships with clear dimension labels
- **Dynamic Fleet Loading**: Fetch new random fleet configurations from the external API
- **Ship Management**: Remove placed ships and return them to the available pool
- **Responsive Design**: Modern, clean UI built with Bootstrap

## 🏗️ Architecture

The solution consists of three main components:

### 1. **BinPackingApp** (Blazor WebAssembly)
The frontend application providing the interactive user interface:
- **Components**: `AnchorageGrid`, `ShipPalette`
- **Core Services**: `BinPackService`, `FleetApiService`
- **Models**: `PlacedShip`, `UnplacedShip`, `AnchorageResponse`

### 2. **Api** (ASP.NET Core Web API)
A proxy API server that:
- Fetches fleet data from the external ESA API (`https://esa.instech.no/api/fleets/random`)
- Handles CORS for the Blazor WebAssembly client
- Provides error handling and timeout management

### 3. **Tests** (xUnit)
Comprehensive unit tests covering:
- Ship placement validation
- Collision detection
- Boundary checking
- Rotation logic
- Ship removal operations

## 📋 Prerequisites

- **.NET 10.0 SDK** or later
- A modern web browser (Chrome, Firefox, Edge, Safari)
- Internet connection (for fetching fleet data from external API)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd ShippingBinpackProblem
```

### 2. Run the API Proxy

First, start the API proxy server in a terminal:

```bash
dotnet run --project Api/BinPackingApp.Api.csproj
```

The API will start on `http://localhost:5052` (or the port specified in `launchSettings.json`).

### 3. Run the Blazor Application

In a separate terminal, start the Blazor WebAssembly application:

```bash
dotnet run --project BinPackagingApp/BinPackingApp.csproj
```

The application will be available at the URL shown in the terminal (typically `http://localhost:5000` or `https://localhost:5001`).

### 4. Open in Browser

Navigate to the URL provided by the Blazor application. The app will automatically load a random fleet configuration.

## 🎮 How to Use

1. **Load a Fleet**: The application automatically loads a random fleet on startup. Click "Try again!" to load a new configuration.

2. **Place Ships**: 
   - Drag ships from the palette (right side) onto the anchorage grid (left side)
   - Ships will snap to grid positions
   - Invalid placements (collisions or out of bounds) are automatically rejected

3. **Rotate Ships**: 
   - Double-click any placed ship to rotate it 90 degrees
   - Rotation is validated automatically - ships that would exceed boundaries won't rotate

4. **Move Ships**: 
   - Drag already-placed ships to reposition them within the anchorage

5. **Remove Ships**: 
   - Click the "×" button on any placed ship to remove it and return it to the available pool

6. **Track Progress**: 
   - The status bar shows how many ships are placed and how many remain

## 🧪 Running Tests

Execute the test suite with:

```bash
dotnet test
```

For more detailed output:

```bash
dotnet test --verbosity normal
```

## 📁 Project Structure

```
ShippingBinpackProblem/
├── Api/                          # ASP.NET Core API proxy
│   ├── Program.cs                # API configuration and endpoints
│   └── BinPackingApp.Api.csproj
├── BinPackagingApp/              # Blazor WebAssembly application
│   ├── Components/              # Razor components
│   │   ├── AnchorageGrid.razor  # Main grid component
│   │   └── ShipPalette.razor    # Ship selection palette
│   ├── Core/
│   │   ├── Models/              # Data models
│   │   └── Services/            # Business logic services
│   ├── Pages/                   # Application pages
│   │   └── AnchoragePage.razor  # Main page
│   └── wwwroot/                 # Static assets
├── Tests/                       # Unit tests
│   └── BinPackServiceTests.cs
└── README.md
```

## 🔧 Core Services

### `BinPackService`
Handles the core bin packing logic:
- `CanPlaceShip()`: Validates if a ship can be placed at a given position
- `PlaceShip()`: Places or updates a ship's position
- `RemoveShip()`: Removes a ship from the anchorage
- `GetShipAt()`: Finds a ship at specific coordinates

### `FleetApiService`
Manages communication with the fleet API:
- `GetRandomFleetAsync()`: Fetches random fleet configurations
- Handles API errors and timeouts gracefully

## 🎨 Technical Details

- **Frontend Framework**: Blazor WebAssembly (.NET 10.0)
- **UI Framework**: Bootstrap 5
- **Backend**: ASP.NET Core Minimal API
- **Testing**: xUnit
- **Language**: C# 12
- **Architecture**: Clean separation of concerns with service interfaces

## 🌐 API Integration

The application integrates with the ESA Fleet API:
- **Endpoint**: `https://esa.instech.no/api/fleets/random`
- **Proxy**: The local API server acts as a CORS proxy
- **Response Format**: JSON containing anchorage dimensions and fleet configurations

## 🐛 Troubleshooting

### API Connection Issues
- Ensure the API proxy is running before starting the Blazor app
- Check your internet connection
- Verify the external API is accessible: `https://esa.instech.no/api/fleets/random`

### Port Conflicts
- If port 5052 is in use, modify `Api/Properties/launchSettings.json`
- Update the base address in `BinPackagingApp/Program.cs` if the API port changes

### Build Errors
- Ensure you have .NET 10.0 SDK installed
- Run `dotnet restore` to restore NuGet packages
- Clear build artifacts: `dotnet clean` then `dotnet build`

## 📝 License

This project is provided as-is for educational and demonstration purposes.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit issues, fork the repository, and create pull requests.

## 📚 Related Concepts

- **Bin Packing Problem**: A combinatorial optimization problem that involves packing objects of different sizes into containers
- **2D Bin Packing**: Extension of bin packing to two dimensions (width and height)
- **Interactive Optimization**: Combining algorithmic solutions with human intuition for complex spatial problems

---

**Built with ❤️ using Blazor WebAssembly and .NET 10**

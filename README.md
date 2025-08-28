# 2D Black Hole Simulation

Physics simulation of light rays (photons) around a black hole using Einstein's General Relativity equations. 
Made with C# and Forms this demonstrates gravitational lensing, event horizons, and photon spheres.

![Black Hole Simulation Demo](demo.gif)

## Features

- **Real-time ray tracing** with Schwarzschild metric
- **Interactive controls** for different ray spawn modes
- **Adjustable black hole mass** via settings slider
- **Draggable black hole position** when settings are active
- **Visual representation** of event horizon and photon sphere
- **Physics-accurate scaling** from pixels to astronomical distances

--- 

## Physics & Mathematics

### Schwarzschild Metric

The simulation uses the Schwarzschild solution to Einstein's field equations in spherical coordinates:

```
ds² = -(1 - rs/r)c²dt² + (1 - rs/r)⁻¹dr² + r²dφ²
```

Where:
- `rs` = Schwarzschild radius (event horizon)
- `r` = radial distance from black hole center
- `φ` = angular coordinate
- `c` = speed of light

### Schwarzschild Radius

The event horizon radius is calculated as:

```
rs = 2GM/c²
```

Where:
- `G` = Gravitational constant (6.674 × 10⁻¹¹ m³/(kg⋅s²))
- `M` = Mass of the black hole (default: 8.54 × 10³⁶ kg ≈ Sagittarius A*)
- `c` = Speed of light (2.998 × 10⁸ m/s)

### Geodesic Equations

Light follows null geodesics in curved spacetime. The equations of motion are:

```
d²r/dλ² = -(rs/2r²)(1-rs/r)(dt/dλ)² + (rs/2r²f)(dr/dλ)² + (r-rs)(dφ/dλ)²

d²φ/dλ² = -2(dr/dλ)(dφ/dλ)/r

dt/dλ = E/f
```

Where:
- `λ` = affine parameter along the geodesic
- `f = 1 - rs/r` = Schwarzschild factor
- `E` = conserved energy parameter

### Numerical Integration

The simulation uses the **Runge-Kutta 4th order (RK4)** method to integrate the geodesic equations:

```csharp
double[] k1 = Geodesic(y0);
double[] k2 = Geodesic(Add(y0, k1, dt/2));
double[] k3 = Geodesic(Add(y0, k2, dt/2));
double[] k4 = Geodesic(Add(y0, k3, dt));

for (int i = 0; i < 4; i++)
    y0[i] += (dt/6.0) * (k1[i] + 2*k2[i] + 2*k3[i] + k4[i]);
```

### Scale Conversion

The simulation converts between pixel coordinates and physical distances:

- **1 pixel** = 4 × 10⁸ meters (400 million meters)
- **Sagittarius A* Schwarzschild radius** ≈ 2.4 × 10¹⁰ meters
- **Photon sphere radius** = 1.5 × rs ≈ 3.6 × 10¹⁰ meters

---

## Controls

### Ray Spawn Modes

1. **Normal Mode**: Multiple parallel rays from the left edge
2. **Single Ray Mode**: One ray at a specific impact parameter
3. **Point Source Mode**: Rays emanating from a single point in multiple directions

### Interactive Features

- **Settings Button**: Opens/closes the settings panel
- **Mass Slider**: Adjusts black hole mass (1-100 scale, 50 = original mass)
- **Mouse Drag**: When settings are open, drag to move the black hole position

### Mass Scale Reference

- **Slider = 1**: ~86,000 solar masses (intermediate-mass black hole)
- **Slider = 50**: ~4.3 million solar masses (Sagittarius A*)
- **Slider = 100**: ~8.6 million solar masses (large supermassive black hole)

---

## Technical Implementation

### Architecture

- **Form1.cs**: Main application window and physics engine
- **Ray.cs**: Individual photon trajectory calculation
- **Form1.Designer.cs**: UI component definitions

### Key Constants

```csharp
public const double G = 6.67430e-11;     // Gravitational constant
public const double c = 299792458.0;     // Speed of light
public const double mass = 8.54e36;      // Default mass (Sagittarius A*)
public const double metersPerPixel = 4e8; // Scale factor
```

## Getting started

### Prerequisites

- .NET 9.0 or later
- Windows operating system
- Visual Studio or VS Code with C# extension

### Build Instructions

```bash
git clone https://github.com/tf664/2D-Black-Hole-Simulation.git
cd 2D-Black-Hole-Simulation
dotnet build
dotnet run
```

Or open `blackhole.sln` in Visual Studio and run the project.

## Scientific Accuracy

- Correct Schwarzschild metric
- Proper geodesic equations
- Event horizon physics
- Photon sphere at r = 1.5rs
- Realistic mass scaling
- Gravitational time dilation effects

### Limitations

- 2D projection of 3D spacetime
- Classical ray optics (no quantum effects)
- No accretion disk or matter interactions
- Simplified visualization of complex 4D geometry
- Simplified visualization of complext 4D geometry
    -> 2D curved paths that photons follow
    -> Reality: These paths are the projection of 4D geodesics onto 3D space, themselves curved by the geometry of 4D spacetime

## References

- Initial concept inspired by @kava010
- Einstein, A. (1915). "Die Feldgleichungen der Gravitation"
- Schwarzschild, K. (1916). "Über das Gravitationsfeld eines Massenpunktes"
- Chandrasekhar, S. (1983). "The Mathematical Theory of Black Holes"
- Misner, Thorne, Wheeler (1973). "Gravitation"

---

## TODO

1. **Add visual preview**
2. **Add mathematic explainations**
Mathematical Reality:
The full Schwarzschild metric involves all 4 coordinates:
```
ds² = -(1-rs/r)c²dt² + (1-rs/r)⁻¹dr² + r²(dθ² + sin²θdφ²)
```




# System Info Viewer (C# Project)

A Windows Forms application that displays detailed system information such as CPU, GPU, RAM, drivers, and temperatures using WMI and OpenHardwareMonitor.

## 📋 Features

- CPU info (all available properties + live temperature)
- GPU info (properties + live temperature)
- RAM info
- Installed drivers
- System summary (via `systeminfo`)
- Real-time monitoring with charts
- Clean Windows Forms interface with Metro UI

## ⚙ Requirements

- Windows OS
- Visual Studio

## 📦 Setup Instructions

1. **Clone or Download the Project**

2. **Download OpenHardwareMonitorLib**
   - Go to: [https://openhardwaremonitor.org/downloads/](https://openhardwaremonitor.org/downloads/)
   - Download and extract the ZIP file.
   - Copy `OpenHardwareMonitorLib.dll` to your project directory.

3. **Add Reference in Visual Studio**
   - Right-click on **References** → **Add Reference**
   - Go to **Browse** tab → select `OpenHardwareMonitorLib.dll` → **OK**

4. **Build and Run**
   - Press `F5` or click **Start** in Visual Studio.

After adding the library, you can build and run the project as usual.

## 🖼 Screens

The app provides different forms to display:
- CPU & GPU temperature charts
- RAM specs
- Driver list
- System info via command-line tools

## 📁 Project Structure Images

### 🧠 Main Interface
![Main UI](images/main-ui.png)

### 🌡 CPU Temperature
![CPU Temperature](images/cpu-temp.png)

### 🎮 GPU Chart
![GPU Chart](images/gpu-chart.png)

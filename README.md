# 🚀 MetaCleaner

> Clean your files’ metadata — manually or automatically.

## ✨ Features

- **Drag-n-Drop & Click** to clean or read metadata  
- **Manual Cleaning**  
  - **Clean & Replace** (overwrite original)  
  - **Clean & Save As…** (save to new location)  
- **Folder Automation**  
  - Watch multiple folders in real time  
  - Initial batch clean on Apply (with live progress)  
  - Randomized timestamps for privacy  
- **Office & Universal Support**  
  - `.docx` / `.xlsx` via Open XML SDK  
  - All other formats  
- **Metadata Reader** tab for raw metadata inspection  
- **System Tray** integration: starts minimized when automation is enabled  

## ⚙️ Installation

1. Download the **latest release** from the [Releases](https://github.com/RRRiderrr/MetaCleaner/releases) page.  
2. Extract the .rar-file.  
3. Double-click **MetaCleaner.exe** to run.

_No build or development environment required._

## 🚀 Usage

### Automation Tab

1. Check **Enable auto-clean folders**  
2. **Add** or **Remove** folders to watch  
3. Click **Apply** → initial batch clean runs in background  

### Clean File Metadata Tab

- **Drag** files onto the panel or **click** to browse  
- Choose **Clean & Replace** or **Clean & Save As…**

### Read File Metadata Tab

- **Drag** a single file or **click** to browse  
- Inspect metadata in the read-only text box  

### System Tray

- With automation on, minimizing or closing hides the window to tray  
- Tray icon → **Open** or **Exit**

## 📝 Configuration

All settings (auto-clean flag and folder list) are stored in `config.json` next to the executable. You can edit this file manually if needed.

## 📦 Dependencies

- **.NET Framework 4.7.2** (pre-installed on most Windows 10/11 machines)  
- Bundled libraries:
  - DocumentFormat.OpenXml  
  - Newtonsoft.Json  
  - exiftool.exe and support files  

## 📜 License

Released under the **MIT License**.

---

*Made by Rider*  

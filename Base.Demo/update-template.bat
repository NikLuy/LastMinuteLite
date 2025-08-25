@echo off
echo ========================================
echo   Base.Blazor Template Update Script
echo ========================================
echo.

echo [1/3] Uninstalling existing template...
dotnet new uninstall "D:\Repos\Templates\Base.Blazor" 2>nul
if %errorlevel% equ 0 (
    echo  Template uninstalled successfully
) else (
    echo  No existing template found or uninstall failed
)
echo.

echo [2/3] Installing updated template...
dotnet new install "D:\Repos\Templates\Base.Blazor"
if %errorlevel% equ 0 (
    echo  Template installed successfully
) else (
    echo  Template installation failed
    pause
    exit /b 1
)
echo.

echo [3/3] Verifying template installation...
dotnet new list base-blazor
echo.

echo ========================================
echo   Template update completed successfully!
echo ========================================
echo.
echo Usage: dotnet new base-blazor -n YourProjectName
echo.
pause

# Base.Blazor Template Update Script
# Updates the template installation with latest changes

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   Base.Blazor Template Update Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host

Write-Host "[1/3] Uninstalling existing template..." -ForegroundColor Yellow
try {
    $result = dotnet new uninstall "D:\Repos\Templates\Base.Blazor" 2>$null
    if ($LASTEXITCODE -eq 0) {
        Write-Host " Template uninstalled successfully" -ForegroundColor Green
    } else {
        Write-Host " No existing template found" -ForegroundColor DarkYellow
    }
} catch {
    Write-Host " Uninstall operation completed" -ForegroundColor DarkYellow
}
Write-Host

Write-Host "[2/3] Installing updated template..." -ForegroundColor Yellow
$installResult = dotnet new install "D:\Repos\Templates\Base.Blazor"
if ($LASTEXITCODE -eq 0) {
    Write-Host " Template installed successfully" -ForegroundColor Green
} else {
    Write-Host " Template installation failed" -ForegroundColor Red
    Write-Host "Exit code: $LASTEXITCODE" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}
Write-Host

Write-Host "[3/3] Verifying template installation..." -ForegroundColor Yellow
dotnet new list base-blazor
Write-Host

Write-Host "========================================" -ForegroundColor Green
Write-Host "   Template update completed successfully!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host
Write-Host "Usage: " -NoNewline
Write-Host "dotnet new base-blazor -n YourProjectName" -ForegroundColor Cyan
Write-Host

# Optional: Show template parameters
Write-Host "Available parameters:" -ForegroundColor DarkCyan
Write-Host "  --IncludeEntityFramework true/false" -ForegroundColor Gray
Write-Host "  --DatabaseProvider SqlServer/Sqlite/PostgreSQL" -ForegroundColor Gray
Write-Host "  --IncludeSharedProject true/false" -ForegroundColor Gray
Write-Host "  --TargetFramework net8.0/net9.0" -ForegroundColor Gray
Write-Host

Read-Host "Press Enter to exit"

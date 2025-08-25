# Quick Template Test Script
# Tests the template by creating a sample project

$testProjectName = "TestBase$(Get-Date -Format 'yyyyMMddHHmmss')"
$testPath = "D:\Temp\TemplateTests"

Write-Host "Testing Base.Blazor template..." -ForegroundColor Cyan
Write-Host

# Ensure test directory exists
if (!(Test-Path $testPath)) {
    New-Item -ItemType Directory -Path $testPath -Force | Out-Null
}

try {
    Set-Location $testPath
    
    Write-Host "Creating test project: $testProjectName" -ForegroundColor Yellow
    dotnet new base-blazor -n $testProjectName --IncludeEntityFramework false
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host " Test project created successfully" -ForegroundColor Green
        Write-Host "Location: $testPath\$testProjectName" -ForegroundColor Gray
        
        # Create Logs directory for Serilog
        New-Item -ItemType Directory -Path "$testPath\$testProjectName\Logs" -Force | Out-Null
        Write-Host " Logs directory created for Serilog" -ForegroundColor Green
        
        Write-Host
        Write-Host "To run the test project:" -ForegroundColor DarkCyan
        Write-Host "  cd "$testPath\$testProjectName"" -ForegroundColor Gray
        Write-Host "  dotnet run" -ForegroundColor Gray
        
        $cleanup = Read-Host "Delete test project? (y/N)"
        if ($cleanup -eq 'y' -or $cleanup -eq 'Y') {
            Remove-Item -Recurse -Force "$testPath\$testProjectName"
            Write-Host " Test project cleaned up" -ForegroundColor Green
        }
    } else {
        Write-Host " Test project creation failed" -ForegroundColor Red
    }
} finally {
    Set-Location "D:\Repos\Templates\Base.Blazor"
}

Read-Host "Press Enter to exit"

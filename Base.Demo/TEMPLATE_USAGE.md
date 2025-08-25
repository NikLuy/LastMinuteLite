# Base Blazor Template

Your Blazor project has been successfully converted into a reusable **Base.Blazor** template!

## What was created:

1. **Template Configuration** (.template.config/template.json)
   - Renamed to ase-blazor template with generic naming
   - Supports Entity Framework inclusion/exclusion
   - Supports different database providers (SQL Server, SQLite, PostgreSQL)
   - Supports shared project reference inclusion/exclusion

2. **Template Documentation** (.template.config/README.md)
   - Updated usage instructions and examples

3. **Modified Project Files**
   - Updated .csproj with conditional package references
   - Updated Program.cs with conditional Entity Framework configuration

## Template Features:

### Parameters:
- --TargetFramework or -tf: Choose .NET version (net8.0 or net9.0)
- --IncludeEntityFramework or -ef: Include Entity Framework Core (true/false)
- --DatabaseProvider or -db: Database provider (SqlServer, Sqlite, PostgreSQL)
- --IncludeSharedProject or -shared: Include shared project reference (true/false)

## Usage Examples:

### Basic project:
`ash
dotnet new base-blazor -n MyNewApp
`

### Project without Entity Framework:
`ash
dotnet new base-blazor -n MyNewApp --IncludeEntityFramework false
`

### Project with SQLite:
`ash
dotnet new base-blazor -n MyNewApp --IncludeEntityFramework true --DatabaseProvider Sqlite
`

### Project without shared project reference:
`ash
dotnet new base-blazor -n MyNewApp --IncludeSharedProject false
`

## Template Management:

### List installed templates:
`ash
dotnet new list
`

### Uninstall template:
`ash
dotnet new uninstall "D:\Repos\Templates\Base.Blazor"
`

### Reinstall template (after changes):
`ash
dotnet new uninstall "D:\Repos\Templates\Base.Blazor"
dotnet new install "D:\Repos\Templates\Base.Blazor"
`

Or simply run update-template.ps1 or update-template.bat

## Template Location:

**Source**: D:\Repos\Templates\Base.Blazor\

## Next Steps:

1. **Customize**: Edit the template files in D:\Repos\Templates\Base.Blazor\ as needed
2. **Share**: You can package and distribute this template via NuGet
3. **Version control**: The entire Base.Blazor folder should be tracked in version control
4. **Extend**: Add more parameters and conditional content as your needs grow

Your **Base.Blazor** template is now ready to use as a foundation for creating consistent Blazor Server applications!

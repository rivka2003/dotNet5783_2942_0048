namespace DalApi;
using DO;
using System.Reflection;
using static DalApi.DalConfig;

public static class Factory
{
    public static IDal? Get()
    {
        string dalType = s_dalName
            ?? throw new DalConfigException($"DAL name is not extracted from the configuration");

        Dictionary<string, string> data = s_dalPackages[dalType]
           ?? throw new DalConfigException($"Package for {dalType} is not found in packages list");

        string projectName = data["project"];
        string namespaceName = data["namespace"];
        string className = data["class"];
   
        try
        {
            Assembly.Load(projectName ?? throw new DalConfigException($"Package {projectName} is null"));
        }
        catch (Exception)
        {
            throw new DalConfigException("Failed to load {dal}.dll package");
        }

        Type? type = Type.GetType($"{namespaceName}.{className}, {projectName}")
            ?? throw new DalConfigException($"Class {namespaceName}.{projectName} was not found in {projectName}.dll");

        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                   .GetValue(null) as IDal
            ?? throw new DalConfigException($"Class {className} is not singleton or Instance property not found");
    }
}


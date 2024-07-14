// Copyright Epic Games, Inc. All Rights Reserved.

using System;
using System.IO;
using UnrealBuildTool;

public class opecv_plus : ModuleRules
{
	 private string ThirdPartyPath
    {
        get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "../ThirdParty")); }
    }
	public opecv_plus(ReadOnlyTargetRules Target) : base(Target)
	{
		// PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		
		// Type = ModuleType.External;
		var opencvRelativePath = ThirdPartyPath + "/opecv_plusLibrary/";

		PublicIncludePaths.AddRange(
			new string[] {
				// ... add public include paths required here ...
			}
			);
				
		
		PrivateIncludePaths.AddRange(
			new string[] {
				// "ThirdParty/opecv_plusLibrary/include",
				// ... add other private include paths required here ...
			}
			);
			
		
		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
				"opecv_plusLibrary",
				"Projects"
				// ... add other public dependencies that you statically link with here ...
			}
			);
			
		
		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				// ... add private dependencies that you statically link with here ...	
			}
			);
		
		
		DynamicallyLoadedModuleNames.AddRange(
			new string[]
			{
				// ... add any modules that your module loads dynamically here ...
			}
			);
			
		
		// var opencvRelativePath = ThirdPartyPath + "/opecv_plusLibrary/";
		PublicIncludePaths.Add(Path.Combine(opencvRelativePath, "include"));

		string importedLibraryPath = Path.Combine(opencvRelativePath, "x64","Release");
        string[] importedLibraries = Directory.GetFiles(importedLibraryPath, "*.lib");
        foreach (string item in importedLibraries)
            {
                PublicAdditionalLibraries.Add(Path.Combine(importedLibraryPath, item));
            }
        
		string dynamicLibraryPath = Path.Combine(opencvRelativePath, "x64","Release");
            string[] dynamicLibraries = Directory.GetFiles(dynamicLibraryPath, "*.dll");
            //PublicRuntimeLibraryPaths.Add(importedLibraryPath);//如果PublicAdditionalLibraries中指定的是导入库文件名，则这里需要指定导入库路径。

            foreach (string item in dynamicLibraries)
            {

                string dynamicLibraryName = Path.GetFileName(item);
                PublicDelayLoadDLLs.Add(dynamicLibraryName);
                RuntimeDependencies.Add(Path.Combine("$(PluginDir)/Binaries/ThirdParty/opecv_plusLibrary/Win64/", dynamicLibraryName), item);
            }
	}
}

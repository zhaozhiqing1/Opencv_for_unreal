// Fill out your copyright notice in the Description page of Project Settings.

using System.IO;
using UnrealBuildTool;

public class opecv_plusLibrary : ModuleRules
{
	public opecv_plusLibrary(ReadOnlyTargetRules Target) : base(Target)
	{

		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		// 新添加对象
		Type = ModuleType.External;

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			// Add the import library

			PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "include"));
			PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "x64", "Release", "ExampleLibrary.lib"));

			// Delay-load the DLL, so we can load it from the right place first
			PublicDelayLoadDLLs.Add("ExampleLibrary.dll");

			// Ensure that the DLL is staged along with the executable
			RuntimeDependencies.Add("$(PluginDir)/Binaries/ThirdParty/opecv_plusLibrary/Win64/ExampleLibrary.dll");
        
			PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "x64", "Release", "opencv_world480.lib"));

            // Delay-load the DLL, so we can load it from the right place first
            PublicDelayLoadDLLs.Add("opencv_world480.dll");

            // Ensure that the DLL is staged along with the executable
            RuntimeDependencies.Add("$(PluginDir)/Binaries/ThirdParty/opecv_plusLibrary/Win64/opencv_world480.dll");
       
			PublicDelayLoadDLLs.Add("opencv_videoio_msmf480_64.dll");

			// Ensure that the DLL is staged along with the executable
			RuntimeDependencies.Add("$(PluginDir)/Binaries/ThirdParty/opecv_plusLibrary/Win64/opencv_videoio_msmf480_64.dll");
			// Delay-load the DLL, so we can load it from the right place first
			PublicDelayLoadDLLs.Add("opencv_videoio_ffmpeg480_64.dll");

			// Ensure that the DLL is staged along with the executable
			RuntimeDependencies.Add("$(PluginDir)/Binaries/ThirdParty/opecv_plusLibrary/Win64/opencv_videoio_ffmpeg480_64.dll");

		}
        else if (Target.Platform == UnrealTargetPlatform.Mac)
        {
            PublicDelayLoadDLLs.Add(Path.Combine(ModuleDirectory, "Mac", "Release", "libExampleLibrary.dylib"));
            RuntimeDependencies.Add("$(PluginDir)/Source/ThirdParty/opecv_plusLibrary/Mac/Release/libExampleLibrary.dylib");
        }
        else if (Target.Platform == UnrealTargetPlatform.Linux)
		{
			string ExampleSoPath = Path.Combine("$(PluginDir)", "Binaries", "ThirdParty", "opecv_plusLibrary", "Linux", "x86_64-unknown-linux-gnu", "libExampleLibrary.so");
			PublicAdditionalLibraries.Add(ExampleSoPath);
			PublicDelayLoadDLLs.Add(ExampleSoPath);
			RuntimeDependencies.Add(ExampleSoPath);
		}
	}
}

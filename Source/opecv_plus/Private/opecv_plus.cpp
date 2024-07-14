// Copyright Epic Games, Inc. All Rights Reserved.

#include "opecv_plus.h"
#include "Core.h"
#include "Modules/ModuleManager.h"
#include "Interfaces/IPluginManager.h"
#include "opecv_plusLibrary/ExampleLibrary.h"

#define LOCTEXT_NAMESPACE "Fopecv_plusModule"

void Fopecv_plusModule::StartupModule()
{
	// This code will execute after your module is loaded into memory; the exact timing is specified in the .uplugin file per-module

	// Get the base directory of this plugin
	FString BaseDir = IPluginManager::Get().FindPlugin("opecv_plus")->GetBaseDir();

	// Add on the relative location of the third party dll and load it
	FString LibraryPath;
	FString Opencv_worldPath;
	FString Opencv_msmPath;
	FString Opencv_ffmegPath;
#if PLATFORM_WINDOWS
	// 务必手动添加动态库
	LibraryPath = FPaths::Combine(*BaseDir, TEXT("Binaries/ThirdParty/opecv_plusLibrary/Win64/ExampleLibrary.dll"));
	Opencv_worldPath = FPaths::Combine(*BaseDir, TEXT("Binaries/ThirdParty/opecv_plusLibrary/Win64/opencv_world480.dll"));
	Opencv_msmPath = FPaths::Combine(*BaseDir, TEXT("Binaries/ThirdParty/opecv_plusLibrary/Win64/opencv_videoio_msmf480_64.dll"));
	Opencv_ffmegPath = FPaths::Combine(*BaseDir, TEXT("Binaries/ThirdParty/opecv_plusLibrary/Win64/opencv_videoio_msmf480_64.dll"));
#elif PLATFORM_MAC
    // LibraryPath = FPaths::Combine(*BaseDir, TEXT("Source/ThirdParty/opecv_plusLibrary/Mac/Release/libExampleLibrary.dylib"));
#elif PLATFORM_LINUX
	// LibraryPath = FPaths::Combine(*BaseDir, TEXT("Binaries/ThirdParty/opecv_plusLibrary/Linux/x86_64-unknown-linux-gnu/libExampleLibrary.so"));
#endif // PLATFORM_WINDOWS

	ExampleLibraryHandle = !LibraryPath.IsEmpty() ? FPlatformProcess::GetDllHandle(*LibraryPath) : nullptr;
	Opencv_world = !Opencv_worldPath.IsEmpty() ? FPlatformProcess::GetDllHandle(*Opencv_worldPath) : nullptr;
	Opencv_msm = !Opencv_msmPath.IsEmpty() ? FPlatformProcess::GetDllHandle(*Opencv_msmPath) : nullptr;
	Opencv_ffmeg = !Opencv_ffmegPath.IsEmpty() ? FPlatformProcess::GetDllHandle(*Opencv_ffmegPath) : nullptr;
	if (ExampleLibraryHandle && Opencv_world &&Opencv_ffmeg&&Opencv_msm)
	{
		// Call the test function in the third party library that opens a message box
		// ExampleLibraryFunction();
	}
	else
	{
		FMessageDialog::Open(EAppMsgType::Ok, LOCTEXT("ThirdPartyLibraryError", "Failed to load example third party library"));
	}
}

void Fopecv_plusModule::ShutdownModule()
{
	// This function may be called during shutdown to clean up your module.  For modules that support dynamic reloading,
	// we call this function before unloading the module.

	// Free the dll handle
	FPlatformProcess::FreeDllHandle(ExampleLibraryHandle);
	ExampleLibraryHandle = nullptr;
	FPlatformProcess::FreeDllHandle(Opencv_world);
	Opencv_world = nullptr;
	FPlatformProcess::FreeDllHandle(Opencv_msm);
	Opencv_msm = nullptr;
	FPlatformProcess::FreeDllHandle(Opencv_ffmeg);
	Opencv_ffmeg = nullptr;
}

#undef LOCTEXT_NAMESPACE
	
IMPLEMENT_MODULE(Fopecv_plusModule, opecv_plus)

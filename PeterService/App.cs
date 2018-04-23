using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using PeterService.ViewModels;

namespace PeterService
{
    public class App : MvxApplication
    {
        public App()
        {
        }

		public override void Initialize()
		{
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterNavigationServiceAppStart<MainViewModel>();
		}
	}
}

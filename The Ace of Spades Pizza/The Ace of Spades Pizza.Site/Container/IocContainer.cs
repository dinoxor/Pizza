﻿using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
namespace The_Ace_of_Spades_Pizza.Site.Container
{
    public class IocContainer
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
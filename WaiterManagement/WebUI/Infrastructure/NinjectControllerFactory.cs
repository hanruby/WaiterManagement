﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using ClassLib.DataStructures;
using DataAccess;
using Ninject;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IBaseDataAccess>().To<DataAccessClass>().InSingletonScope();

            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();
        }
    }
}
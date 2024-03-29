﻿using Castle.MicroKernel;
using Castle.Windsor;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentManagementWebApp.Container
{
	public class WindsorControllerFactory : DefaultControllerFactory
	{
        readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }
        [HandleError]
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType != null && container.Kernel.HasComponent(controllerType))
                    return (IController)container.Resolve(controllerType);
                return null;//Sửa đổi vì nếu để câu lệnh dưới sẽ dính exception
                            //return base.GetControllerInstance(requestContext, controllerType);
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex, "Error was sent from [WindsorControllerFactory]");
                return null;
            }
        }

        public override void ReleaseController(IController controller)
        {
            container.Release(controller);
        }
    }
}
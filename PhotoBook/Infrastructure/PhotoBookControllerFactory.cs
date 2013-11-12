using System;
using StructureMap;
using System.Web.Mvc;

namespace PhotoBook.Infrastructure
{
    public class PhotoBookControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return ObjectFactory.GetInstance(controllerType) as IController;
        }
    }
}
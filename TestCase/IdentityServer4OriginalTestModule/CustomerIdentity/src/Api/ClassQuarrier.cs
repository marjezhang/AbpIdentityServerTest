using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api
{
    public class ClassQuarrier
    {
//        private readonly IEnumerable<IAuthorizationHandler> _handlers;

//        public ClassQuarrier(IEnumerable<IAuthorizationHandler> handlers)
//        {
//            _handlers = handlers;
//        }

        private readonly IEnumerable<InterfaceMy> _nterfaceMies;

        public ClassQuarrier(IEnumerable<InterfaceMy> nterfaceMies)
        {
            _nterfaceMies = nterfaceMies;
        }

        public void Todo()
        {
            Debug.Print("ok");
        }
    }


    public interface InterfaceMy
    {
        
    }

    public class Myclass1 : InterfaceMy
    {

    }


    public class MyClass2 : InterfaceMy
    {

    }

    public class My2AuthorizationHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            
            return Task.CompletedTask;
        }
    }

    public class My3AuthorizationHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            
            return Task.CompletedTask;
        }
    }

}

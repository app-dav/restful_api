using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Interfaces;
using Microsoft.Practices.Unity;

namespace Notes.Domain
{
    public class ServiceFactory
    {
        static UnityContainer _container;

        static ServiceFactory()
        {
            _container = new UnityContainer();
            _container.RegisterType<INoteService, NoteService>();    
        }

        public static INoteService GetNoteService()
        {
            return _container.Resolve<INoteService>();
        }
    }
}

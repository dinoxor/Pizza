using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using The_Ace_of_Spades_Pizza.Data.Model;
using The_Ace_of_Spades_Pizza.Data.Repository;
using Castle.MicroKernel.SubSystems.Configuration;

namespace The_Ace_of_Spades_Pizza.Site.Container
{
    public class ContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                .For<IRepository<Order>>()
                .ImplementedBy<OrderRepository>()
                .LifestyleTransient());

            container.Register(
                Component
                .For<IRepository<Pizza>>()
                .ImplementedBy<PizzaRepository>()
                .LifestyleTransient());

            container.Register(
                Component
                .For<IRepository<Customer>>()
                .ImplementedBy<CustomerRepository>()
                .LifestyleTransient());
        }
    }
}
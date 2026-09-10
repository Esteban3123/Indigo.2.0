using Application.Events.Models;
using Application.Events.Models.Customer;
using System;

namespace Application.Events.Serializers
{
    public class Customer : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : Customer
        /// </summary>
        /// <param name="customerEntity"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.Customer customerEntity = obj as Domain.Entities.Customer;

            MCustomer customer = new MCustomer();
            MethodsCustomer methodsCustomer = new MethodsCustomer();
            customer.Nit = customerEntity.Nit;
            customer.Name = customerEntity.Name;
            customer.EPSCode = customerEntity.EPSCode;
            customer.ThirdParty = methodsCustomer.getThirdParty(customerEntity);
            customer.MainAccountReceivable = methodsCustomer.getMainAccountReceivable(customerEntity);
            customer.Retentions = methodsCustomer.getRetentions(customerEntity);
            customer.Term = customerEntity.Term;
            customer.State = Convert.ToInt16(customerEntity.State);
            customer.CreationUser = customerEntity.CreationUser;
            customer.CreationDate = Convert.ToString(customerEntity.CreationDate);
            customer.ModificationUser = customerEntity.ModificationUser;
            customer.ModificationDate = Convert.ToString(customerEntity.ModificationDate);
            return customer;
        }
    }
}

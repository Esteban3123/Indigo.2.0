'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Julian Cardozo
' Created          : 06-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 11-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region
Partial Class GlosasService

#Region "Customer"
    ''' <summary>
    ''' elimina un cliente
    ''' </summary>
    ''' <param name="Customer">cliente</param>
    ''' <param name="session">objeto session</param>
    ''' <returns></returns>
    Public Function DeleteCustomer(Customer As Customer, session As SessionValues) As ActionResult Implements IGlosasService.DeleteCustomer
        Using CustomerAdmin As ICustomerAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICustomerAdminService)()
            Return CustomerAdmin.DeleteCustomer(Customer, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' obtener un cliente
    ''' </summary>
    ''' <param name="codeCustomer">codigo cliente</param>
    ''' <returns>un cliente</returns>
    Public Function GetCustomer(codeCustomer As String, session As SessionValues) As Customer Implements IGlosasService.GetCustomer
        Using CustomerAdmin As ICustomerAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICustomerAdminService)()
            Return CustomerAdmin.GetCustomer(codeCustomer, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' lista de todos los cliente
    ''' </summary>
    ''' <returns>lista de clientes</returns>
    Public Function ListCustomerAll(session As SessionValues) As List(Of Customer) Implements IGlosasService.ListCustomerAll
        Using CustomerAdmin As ICustomerAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICustomerAdminService)()
            Return CustomerAdmin.ListCustomerAll(session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' guarda un cliente
    ''' </summary>
    ''' <param name="Customer">cliente</param>
    ''' <param name="session">mensaje auditoria</param>
    ''' <returns></returns>
    Public Function SaveCustomer(Customer As Customer, session As SessionValues) As ActionResult(Of Customer) Implements IGlosasService.SaveCustomer
        Using CustomerAdmin As ICustomerAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICustomerAdminService)()
            Return CustomerAdmin.SaveCustomer(Customer, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' obtener un cliente
    ''' </summary>
    ''' <param name="codeCustomer"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerWithouState(codeCustomer As String, session As SessionValues) As Customer Implements IGlosasCustomer.GetCustomerWithouState
        Using CustomerAdmin As ICustomerAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICustomerAdminService)()
            Return CustomerAdmin.GetCustomerWithouState(codeCustomer, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el cliente por Nit De Tercero
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerByNit(Nit As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Customer Implements IGlosasCustomer.GetCustomerByNit
        Using GetSupplierByNit As ICustomerAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICustomerAdminService)()
            Return GetSupplierByNit.GetCustomerByNit(Nit)
        End Using
    End Function


    Public Function GetCustomerById(Id As String, session As SessionValues) As Customer Implements IGlosasCustomer.GetCustomerById
        Using GetSupplierByNit As ICustomerAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICustomerAdminService)()
            Return GetSupplierByNit.GetCustomerById(Id)
        End Using
    End Function
#End Region

End Class

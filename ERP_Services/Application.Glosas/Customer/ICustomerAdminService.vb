'***********************************************************************
' Assembly         : Application.Glosas
' Author           : JulianCardozo
' Created          : 11-03-2011
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-04-11
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region
Public Interface ICustomerAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Lists the Customer all.
    ''' </summary>
    ''' <returns></returns>
    Function ListCustomerAll(audit As AuditMessage) As List(Of Customer)

    ''' <summary>
    ''' Elimina un Customer
    ''' </summary>
    ''' <param name="Customer">el Grupo</param>
    ''' <returns></returns>
    Function DeleteCustomer(ByVal Customer As Customer, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' graba un Customer
    ''' </summary>
    ''' <param name="Customer">el Customer</param>
    ''' <returns></returns>
    Function SaveCustomer(Customer As Customer, audit As AuditMessage) As ActionResult(Of Customer)
    ''' <summary>
    ''' consulta un Customer especifico
    ''' </summary>
    ''' <param name="codeCustomer">el codigo del Customer</param>
    ''' <returns></returns>
    Function GetCustomer(ByVal codeCustomer As String, audit As AuditMessage) As Customer

    ''' <summary>
    ''' consulta un Customer especifico sin tener encuenta el estado
    ''' </summary>
    ''' <param name="codeCustomer">el codigo del Customer</param>
    ''' <returns></returns>
    Function GetCustomerWithouState(codeCustomer As String, audit As AuditMessage) As Customer

    ''' <summary>
    ''' consulta para retornar un Proveedor teniendo en cuenta el Nit Del Tercero
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    Function GetCustomerByNit(ByVal Nit As String) As Customer

    ''' <summary>
    ''' consulta un cliente por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCustomerById(Id As String) As Customer

End Interface

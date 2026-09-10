Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasCustomer

#Region "Customer"

    ''' <summary>
    ''' Lists the Customer all.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListCustomerAll(ByVal session As SessionValues) As List(Of Customer)
    ''' <summary>
    ''' Elimina un Customer
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCustomer(ByVal Customer As Customer, ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' guarda un cliente
    ''' </summary>
    ''' <param name="Customer">cliente</param>
    ''' <param name="session">mensaje auditoria</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCustomer(Customer As Customer, session As SessionValues) As ActionResult(Of Customer)
    ''' <summary>
    ''' consulta un Customer especifico
    ''' </summary>
    ''' <param name="codeCustomer">el codigo del SpecificConcept</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCustomer(ByVal codeCustomer As String, ByVal session As SessionValues) As Customer
    ''' <summary>
    ''' consulta un Customer especifico sinn tener encuenta el estado
    ''' </summary>
    ''' <param name="codeCustomer">el codigo del SpecificConcept</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCustomerWithouState(ByVal codeCustomer As String, ByVal session As SessionValues) As Customer

    ''' <summary>
    ''' consulta para retornar un cliente teniendo en cuenta el Nit Del Tercero
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    <OperationContract()> _
    Function GetCustomerByNit(ByVal Nit As String, ByVal session As SessionValues) As Customer

    ''' <summary>
    ''' consulta un cliente por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCustomerById(Id As String, ByVal session As SessionValues) As Customer
#End Region

End Interface

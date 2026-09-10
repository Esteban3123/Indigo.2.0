#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface ISupplierService

    <OperationContract()> _
    Function ListAllBSupplier(Empresa As String) As List(Of Supplier)

    <OperationContract()> _
    Function DeleteSupplier(ByVal Supplier As Supplier, ByVal listSupplierDistributionLines As List(Of Domain.Entities.SuppliersDistributionLines), listSupplierDetailType As List(Of Domain.Entities.SupplierDetailType), ByVal session As SessionValues) As ActionResult

    <OperationContract()> _
    Function SaveSupplier(ByVal Supplier As Supplier, ByVal listSupplierDistributionLines As List(Of Domain.Entities.SuppliersDistributionLines), listSupplierDetailType As List(Of Domain.Entities.SupplierDetailType), ByVal mode As Boolean, ByVal session As SessionValues) As ActionResult(Of Supplier)

    <OperationContract()> _
    Function GetSupplier(ByVal codeSupplier As String, ByVal session As SessionValues) As Supplier

    <OperationContract()> _
    Function GetSupplierById(ByVal id As Integer, ByVal session As SessionValues) As Supplier

    ''' <summary>
    ''' Obtiene un proveedor por id del tercero
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetSupplierByIdThirdParty(ByVal Id As Integer, ByVal session As SessionValues) As Domain.Entities.Supplier

    ''' <summary>
    ''' Obtiene un proveedor por id del tercero e id de la cuenta contable
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    ''' <param name="IdAccountAccounting">The identifier account accounting.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetSupplierByIdThirdPartyAndIdAccountAccounting(ByVal IdThird As Integer, ByVal IdAccountAccounting As Integer, ByVal session As SessionValues) As Domain.Entities.Supplier

    <OperationContract()> _
    Function GetThirdPartyById(ByVal id As Integer, ByVal session As SessionValues) As ThirdParty

    ''' <summary>
    ''' consulta para retornar un fabricante teniendo en cuenta el id
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    <OperationContract()> _
    Function GetThirdPartyByIdSupplier(ByVal Id As Integer, ByVal session As SessionValues) As ThirdParty

    <OperationContract()> _
    Function GetSupplierByIdThirdPartyWithThirdAdded(Id As Integer, ByVal session As SessionValues) As Domain.Entities.Supplier
    ''' <summary>
    ''' consulta para retornar un Proveedor teniendo en cuenta el Nit Del Tercero
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    <OperationContract()> _
    Function GetSupplierByNitThirdParty(ByVal Id As String, ByVal session As SessionValues) As Supplier

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal session As SessionValues) As ActionResult(Of Supplier)

End Interface

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IFixedAssetPhysicalAssetService

    ''' <summary>
    ''' Obtiene el registro por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetPhysicalAssetById(Id As Integer) As Domain.Entities.FixedAssetPhysicalAsset

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFixedAssetPhysicalAsset(FixedAssetActiveOutput As Domain.Entities.FixedAssetPhysicalAsset, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPhysicalAsset)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="Plate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetPhysicalAssetByPlate(Plate As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPhysicalAsset)

    ''' <summary>
    ''' Obtiene el balance de activos fijos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="InitialPlate"></param>
    ''' <param name="FinalPlate"></param>
    ''' <param name="InitialCatalog"></param>
    ''' <param name="FinalCatalog"></param>
    ''' <param name="InitialGroup"></param>
    ''' <param name="FinalGroup"></param>
    ''' <param name="InitialLocation"></param>
    ''' <param name="FinalLocation"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFixedAssetBalance(ByVal Year As Integer, ByVal Month As Integer, ByVal InitialPlate As String, ByVal FinalPlate As String, ByVal InitialCatalog As String, ByVal FinalCatalog As String, ByVal InitialGroup As String, ByVal FinalGroup As String, ByVal InitialLocation As String, ByVal FinalLocation As String, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Obtiene el kardex de activos fijos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="InitialPlate"></param>
    ''' <param name="FinalPlate"></param>
    ''' <param name="InitialCatalog"></param>
    ''' <param name="FinalCatalog"></param>
    ''' <param name="InitialGroup"></param>
    ''' <param name="FinalGroup"></param>
    ''' <param name="InitialLocation"></param>
    ''' <param name="FinalLocation"></param>
    ''' <param name="InitialResponsible"></param>
    ''' <param name="FinalResponsible"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFixedAssetKardex(ByVal Year As Integer, ByVal Month As Integer, ByVal InitialPlate As String, ByVal FinalPlate As String, ByVal InitialCatalog As String, ByVal FinalCatalog As String, ByVal InitialGroup As String, ByVal FinalGroup As String, ByVal InitialLocation As String, ByVal FinalLocation As String, ByVal InitialResponsible As String, ByVal FinalResponsible As String, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Confirma la finalización de contratos leasing
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmLeasingContractsFinalization(ListIds As List(Of Integer), OperatingUnitId As Integer, Year As Integer, Month As Integer, CompanyNit As String, audit As AuditMessage) As ActionResult

End Interface

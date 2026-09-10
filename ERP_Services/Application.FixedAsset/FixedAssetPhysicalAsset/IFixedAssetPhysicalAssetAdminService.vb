#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetPhysicalAssetAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el registro por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetPhysicalAssetById(ByVal Id As Integer) As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetPhysicalAsset(ByVal FixedAssetPhysicalAsset As FixedAssetPhysicalAsset, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetPhysicalAsset)

    ''' <summary>
    ''' Obtiene el registro por placa
    ''' </summary>
    ''' <param name="Plate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetPhysicalAssetByPlate(ByVal Plate As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetPhysicalAsset)

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
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetFixedAssetBalance(ByVal Year As Integer, ByVal Month As Integer, ByVal InitialPlate As String, ByVal FinalPlate As String, ByVal InitialCatalog As String, ByVal FinalCatalog As String, ByVal InitialGroup As String, ByVal FinalGroup As String, ByVal InitialLocation As String, ByVal FinalLocation As String, session As SessionValues) As DataSet

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
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function GetFixedAssetKardex(ByVal Year As Integer, ByVal Month As Integer, ByVal InitialPlate As String, ByVal FinalPlate As String, ByVal InitialCatalog As String, ByVal FinalCatalog As String, ByVal InitialGroup As String, ByVal FinalGroup As String, ByVal InitialLocation As String, ByVal FinalLocation As String, ByVal InitialResponsible As String, ByVal FinalResponsible As String, session As SessionValues) As DataSet

    ''' <summary>
    ''' Confirma la finalización de contratos leasing
    ''' </summary>
    ''' <returns></returns>
    Function ConfirmLeasingContractsFinalization(ListIds As List(Of Integer), OperatingUnitId As Integer, Year As Integer, Month As Integer, CompanyNit As String, audit As AuditMessage) As ActionResult

End Interface

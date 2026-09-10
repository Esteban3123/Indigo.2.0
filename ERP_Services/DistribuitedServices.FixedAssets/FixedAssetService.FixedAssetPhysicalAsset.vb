Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    Public Function GetFixedAssetPhysicalAssetById(Id As Integer) As Domain.Entities.FixedAssetPhysicalAsset Implements IFixedAssetPhysicalAssetService.GetFixedAssetPhysicalAssetById
        Using service As IFixedAssetPhysicalAssetAdminService = Container.Current.Resolve(Of IFixedAssetPhysicalAssetAdminService)()
            Return service.GetFixedAssetPhysicalAssetById(Id)
        End Using
    End Function

    Public Function GetFixedAssetPhysicalAssetByPlate(Plate As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPhysicalAsset) Implements IFixedAssetPhysicalAssetService.GetFixedAssetPhysicalAssetByPlate
        Using service As IFixedAssetPhysicalAssetAdminService = Container.Current.Resolve(Of IFixedAssetPhysicalAssetAdminService)()
            Return service.GetFixedAssetPhysicalAssetByPlate(Plate, audit)
        End Using
        'Return Me._fixedAssetPhysicalAssetAdminService.GetFixedAssetPhysicalAssetByPlate(Plate, audit)
    End Function

    Public Function SaveFixedAssetPhysicalAsset(FixedAssetActiveOutput As Domain.Entities.FixedAssetPhysicalAsset, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPhysicalAsset) Implements IFixedAssetPhysicalAssetService.SaveFixedAssetPhysicalAsset
        Using service As IFixedAssetPhysicalAssetAdminService = Container.Current.Resolve(Of IFixedAssetPhysicalAssetAdminService)()
            Return service.SaveFixedAssetPhysicalAsset(FixedAssetActiveOutput, audit)
        End Using
        'Return Me._fixedAssetPhysicalAssetAdminService.SaveFixedAssetPhysicalAsset(FixedAssetActiveOutput, audit)
    End Function

    Public Function ConfirmLeasingContractsFinalization(ListIds As List(Of Integer), OperatingUnitId As Integer, Year As Integer, Month As Integer, CompanyNit As String, audit As AuditMessage) As ActionResult Implements IFixedAssetPhysicalAssetService.ConfirmLeasingContractsFinalization
        Using service As IFixedAssetPhysicalAssetAdminService = Container.Current.Resolve(Of IFixedAssetPhysicalAssetAdminService)()
            Return service.ConfirmLeasingContractsFinalization(ListIds, OperatingUnitId, Year, Month, CompanyNit, audit)
        End Using
        'Return Me._fixedAssetPhysicalAssetAdminService.ConfirmLeasingContractsFinalization(ListIds, OperatingUnitId, Year, Month, CompanyNit, audit)
    End Function

    Public Function GetFixedAssetBalance(ByVal Year As Integer, ByVal Month As Integer, ByVal InitialPlate As String, ByVal FinalPlate As String, ByVal InitialCatalog As String, ByVal FinalCatalog As String, ByVal InitialGroup As String, ByVal FinalGroup As String, ByVal InitialLocation As String, ByVal FinalLocation As String, Session As SessionValues) As DataSet Implements IFixedAssetPhysicalAssetService.GetFixedAssetBalance
        Using service As IFixedAssetPhysicalAssetAdminService = Container.Current.Resolve(Of IFixedAssetPhysicalAssetAdminService)()
            Return service.GetFixedAssetBalance(Year, Month, InitialPlate, FinalPlate, InitialCatalog, FinalCatalog, InitialGroup, FinalGroup, InitialLocation, FinalLocation, Session)
        End Using
        'Return Me._fixedAssetPhysicalAssetAdminService.GetFixedAssetBalance(Year, Month, InitialPlate, FinalPlate, InitialCatalog, FinalCatalog, InitialGroup, FinalGroup, InitialLocation, FinalLocation, Session)
    End Function

    Public Function GetFixedAssetKardex(ByVal Year As Integer, ByVal Month As Integer, ByVal InitialPlate As String, ByVal FinalPlate As String, ByVal InitialCatalog As String, ByVal FinalCatalog As String, ByVal InitialGroup As String, ByVal FinalGroup As String, ByVal InitialLocation As String, ByVal FinalLocation As String, ByVal InitialResponsible As String, ByVal FinalResponsible As String, Session As SessionValues) As DataSet Implements IFixedAssetPhysicalAssetService.GetFixedAssetKardex
        Using service As IFixedAssetPhysicalAssetAdminService = Container.Current.Resolve(Of IFixedAssetPhysicalAssetAdminService)()
            Return service.GetFixedAssetKardex(Year, Month, InitialPlate, FinalPlate, InitialCatalog, FinalCatalog, InitialGroup, FinalGroup, InitialLocation, FinalLocation, InitialResponsible, FinalResponsible, Session)
        End Using
        'Return Me._fixedAssetPhysicalAssetAdminService.GetFixedAssetKardex(Year, Month, InitialPlate, FinalPlate, InitialCatalog, FinalCatalog, InitialGroup, FinalGroup, InitialLocation, FinalLocation, InitialResponsible, FinalResponsible, Session)
    End Function


End Class

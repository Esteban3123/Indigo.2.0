Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports DistributedServices.Maintenance
Imports Domain.Entities

Partial Class MaintanceService
    Implements IEquipmentRegistrationService

    Public Function DeleteEquipmentRegistration(Empresa As String, EquipmentRegistration As Domain.Entities.EquipmentRegistration, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IEquipmentRegistrationService.DeleteEquipmentRegistration
        Using AreaAdmin As IEquipmentRegistrationAdminService = Container.Current.Resolve(Of IEquipmentRegistrationAdminService)()
            Return AreaAdmin.DeleteEquipmentRegistration(EquipmentRegistration, audit)
        End Using
    End Function

    Public Function GetEquipmentRegistration(Empresa As String, codeEquipmentRegistration As String) As Domain.Entities.EquipmentRegistration Implements IEquipmentRegistrationService.GetEquipmentRegistration
        Using AreaAdmin As IEquipmentRegistrationAdminService = Container.Current.Resolve(Of IEquipmentRegistrationAdminService)()
            Return AreaAdmin.GetEquipmentRegistration(codeEquipmentRegistration)
        End Using
    End Function

    Public Function ListAllEquipmentRegistration(Empresa As String) As List(Of Domain.Entities.EquipmentRegistration) Implements IEquipmentRegistrationService.ListAllEquipmentRegistration
        Using AreaAdmin As IEquipmentRegistrationAdminService = Container.Current.Resolve(Of IEquipmentRegistrationAdminService)()
            Return AreaAdmin.ListAllEquipmentRegistration()
        End Using
    End Function

    Public Function SaveEquipmentRegistration(Empresa As String, EquipmentRegistration As Domain.Entities.EquipmentRegistration, _Idsecuence As Long, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Entities.EquipmentRegistration) Implements IEquipmentRegistrationService.SaveEquipmentRegistration
        Using AreaAdmin As IEquipmentRegistrationAdminService = Container.Current.Resolve(Of IEquipmentRegistrationAdminService)()
            Return AreaAdmin.SaveEquipmentRegistration(EquipmentRegistration, _Idsecuence, audit)
        End Using
    End Function

    Public Function ListAccesoryEquipmentType(Empresa As String, Type As Integer) As List(Of Domain.Entities.AccesoryDetail) Implements IEquipmentRegistrationService.ListAccesoryEquipmentType
        Using AreaAdmin As IEquipmentRegistrationAdminService = Container.Current.Resolve(Of IEquipmentRegistrationAdminService)()
            Return AreaAdmin.ListAccesoryEquipmentType(Type)
        End Using
    End Function

    Public Function ListConsumableEquipmentType(Empresa As String, Type As Integer) As List(Of Domain.Entities.ConsumableDetail) Implements IEquipmentRegistrationService.ListConsumableEquipmentType
        Using AreaAdmin As IEquipmentRegistrationAdminService = Container.Current.Resolve(Of IEquipmentRegistrationAdminService)()
            Return AreaAdmin.ListConsumableEquipmentType(Type)
        End Using
    End Function

    Public Function ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration(Empresa As String, fixedAssetPhysicalAssetId As Integer, equipmentRegistration As Integer) As List(Of TechnicalLogDetail) Implements IEquipmentRegistrationService.ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration
        Using AreaAdmin As IEquipmentRegistrationAdminService = Container.Current.Resolve(Of IEquipmentRegistrationAdminService)()
            Return AreaAdmin.ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration(fixedAssetPhysicalAssetId, equipmentRegistration)
        End Using
    End Function
End Class

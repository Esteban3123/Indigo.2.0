Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function ListAllEquipmentRequirement(Empresa As String) As List(Of Domain.Entities.EquipmentRequirement) Implements IMaintenanceService.ListAllEquipmentRequirement
        Using AccesoryAdmin As IEquipmentRequirementAdminService = Container.Current.Resolve(Of IEquipmentRequirementAdminService)()
            Return AccesoryAdmin.ListAllEquipmentRequirement
        End Using
    End Function

End Class

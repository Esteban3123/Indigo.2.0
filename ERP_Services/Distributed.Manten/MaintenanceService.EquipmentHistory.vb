Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function ListAllEquipmentHistory(Empresa As String) As List(Of Domain.Entities.EquipmentHistory) Implements IEquipmentHistoryService.ListAllEquipmentHistory
        Using EquipmentAdmin As IEquipmentHistoryAdminService = Container.Current.Resolve(Of IEquipmentHistoryAdminService)()
            Return EquipmentAdmin.ListAllEquipmentHistory()
        End Using
    End Function

End Class

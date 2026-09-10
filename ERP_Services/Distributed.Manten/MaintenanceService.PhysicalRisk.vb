Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function ListAllPhysicalRisk(Empresa As String) As List(Of Domain.Entities.PhysicalRisk) Implements IPhysicalRiskService.ListAllPhysicalRisk
        Using InsuranceAdmin As IPhysicalRiskAdminService = Container.Current.Resolve(Of IPhysicalRiskAdminService)()
            Return InsuranceAdmin.ListAllPhysicalRisk()
        End Using
    End Function

End Class

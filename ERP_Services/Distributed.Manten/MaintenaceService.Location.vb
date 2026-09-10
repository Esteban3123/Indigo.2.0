Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance

Partial Class MaintanceService

    Public Function DeleteLocation(Empresa As String, Location As Domain.Maintenance.Entities.Location, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements ILocationService.DeleteLocation
        Using LocationAdmin As ILocationAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of ILocationAdminService)()
            Return LocationAdmin.DeleteLocation(Location, audit)
        End Using
    End Function

    Public Function GetLocation(Empresa As String, codeLocation As String) As Domain.Maintenance.Entities.Location Implements ILocationService.GetLocation
        Using LocationAdmin As ILocationAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of ILocationAdminService)()
            Return LocationAdmin.GetLocation(codeLocation)
        End Using
    End Function

    Public Function ListAllLocation(Empresa As String) As List(Of Domain.Maintenance.Entities.Location) Implements ILocationService.ListAllLocation
        Using LocationAdmin As ILocationAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of ILocationAdminService)()
            Return LocationAdmin.ListAllLocation()
        End Using
    End Function

    Public Function ListLocation(Empresa As String) As List(Of Domain.Maintenance.Entities.Location) Implements ILocationService.ListLocation
        Using LocationAdmin As ILocationAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of ILocationAdminService)()
            Return LocationAdmin.ListLocation()
        End Using
    End Function

    Public Function SaveLocation(Empresa As String, Location As List(Of Domain.Maintenance.Entities.Location), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements ILocationService.SaveLocation
        Using LocationAdmin As ILocationAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of ILocationAdminService)()
            Return LocationAdmin.SaveLocation(Location, audit)
        End Using
    End Function
End Class

Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance

Partial Class MaintanceService

    Public Function ListAllLocationType(Empresa As String) As List(Of Domain.Maintenance.Entities.LocationType) Implements ILocationTypeService.ListAllLocationType
        Using LocationTypeAdmin As ILocationTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of ILocationTypeAdminService)()
            Return LocationTypeAdmin.ListAllLocationType()
        End Using
    End Function

End Class

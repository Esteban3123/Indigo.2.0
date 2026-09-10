'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 16/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Domain.Entities
Imports Microsoft.Practices.Unity

Partial Class ContractService

    ''' <summary>
    ''' lista los homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="IpsServiceId"></param>
    ''' <returns></returns>
    Public Function ListCupsHomologationByIpsServiceId(IpsServiceId As Integer) As List(Of Domain.Entities.CupsHomologation) Implements IContractServiceCupsHomologation.ListCupsHomologationByIpsServiceId
        Using service As ICupsHomologationAdminService = Container.Current.Resolve(Of ICupsHomologationAdminService)()
            Return service.ListCupsHomologationByIpsServiceId(IpsServiceId)
        End Using
        'Return _cupsHomologationAdminService.ListCupsHomologationByIpsServiceId(IpsServiceId)
    End Function

    Public Function GetHomologationCupsByListCUPS(listCupsId As List(Of Integer), CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, Optional IPSServiceId As Integer = 0, Optional ManualType As Integer = 0) As Domain.Base.Entities.ActionResult(Of List(Of CupsHomologation)) Implements IContractServiceCupsHomologation.GetHomologationCupsByListCUPS
        Using service As ICupsHomologationAdminService = Container.Current.Resolve(Of ICupsHomologationAdminService)()
            Return service.GetHomologationCupsByListCUPS(listCupsId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, IPSServiceId, ManualType)
        End Using
        'Return _cupsHomologationAdminService.GetHomologationCupsByListCUPS(listCupsId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, IPSServiceId, ManualType)
    End Function

    ''' <summary>
    ''' metodo para obterner las homologaciones del cups
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="CupsId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    Public Function GetHomologationCups(CareGroupId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, Optional IPSServiceId As Integer = 0, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.CupsHomologation)) Implements IContractServiceCupsHomologation.GetHomologationCups
        Using service As ICupsHomologationAdminService = Container.Current.Resolve(Of ICupsHomologationAdminService)()
            Return service.GetHomologationCups(CareGroupId, CupsId, FunctionalUnitId, Specialty, ServiceDate, IPSServiceId, ManualType, RiasId, ContractDescriptionId)
        End Using
        'Return _cupsHomologationAdminService.GetHomologationCups(CareGroupId, CupsId, FunctionalUnitId, Specialty, ServiceDate, IPSServiceId, ManualType)
    End Function

    ''' <summary>
    ''' Obtiene el listado que tiene
    ''' asociado la entidad CUPS
    ''' </summary>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListCupsHomologationByCupsEntityId(cupsEntityId As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.CupsHomologation)) Implements IContractServiceCupsHomologation.GetListCupsHomologationByCupsEntityId
        Using service As ICupsHomologationAdminService = Container.Current.Resolve(Of ICupsHomologationAdminService)()
            Return service.GetListCupsHomologationByCupsEntityId(cupsEntityId)
        End Using
        'Return _cupsHomologationAdminService.GetListCupsHomologationByCupsEntityId(cupsEntityId)
    End Function

End Class

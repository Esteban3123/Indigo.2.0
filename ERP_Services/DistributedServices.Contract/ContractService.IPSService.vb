'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService

    Public Function ChangeStateIPSService(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.IPSService) Implements IContractIPSService.ChangeStateIPSService
        Using service As IIPSServiceAdminService = Container.Current.Resolve(Of IIPSServiceAdminService)()
            Return service.ChangeStateIPSService(code, state, audit)
        End Using
        'Return Me._ipsServiceAdminService.ChangeStateIPSService(code, state, audit)
    End Function

    Public Function DeleteIPSService(IPSService As Domain.Entities.IPSService, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractIPSService.DeleteIPSService
        Using service As IIPSServiceAdminService = Container.Current.Resolve(Of IIPSServiceAdminService)()
            Return service.DeleteIPSService(IPSService, audit)
        End Using
        'Return Me._ipsServiceAdminService.DeleteIPSService(IPSService, audit)
    End Function

    Public Function GetIPSService(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.IPSService) Implements IContractIPSService.GetIPSService
        Using service As IIPSServiceAdminService = Container.Current.Resolve(Of IIPSServiceAdminService)()
            Return service.GetIPSService(code, audit)
        End Using
        'Return Me._ipsServiceAdminService.GetIPSService(code, audit)
    End Function

    Public Function GetIPSServiceById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.IPSService) Implements IContractIPSService.GetIPSServiceById
        Using service As IIPSServiceAdminService = Container.Current.Resolve(Of IIPSServiceAdminService)()
            Return service.GetIPSServiceById(id, audit)
        End Using
        'Return Me._ipsServiceAdminService.GetIPSServiceById(id, audit)
    End Function

    Public Function SaveIPSService(IPSService As Domain.Entities.IPSService, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.IPSService) Implements IContractIPSService.SaveIPSService
        Using service As IIPSServiceAdminService = Container.Current.Resolve(Of IIPSServiceAdminService)()
            Return service.SaveIPSService(IPSService, audit, idSequense)
        End Using
        'Return Me._ipsServiceAdminService.SaveIPSService(IPSService, audit, idSequense)
    End Function

    Public Function GetCupsHomologationByIPSServiceId(idIPSService As Integer) As List(Of Domain.Entities.CupsHomologation) Implements IContractIPSService.GetCupsHomologationByIPSServiceId
        Using service As IIPSServiceAdminService = Container.Current.Resolve(Of IIPSServiceAdminService)()
            Return service.GetCupsHomologationByIPSServiceId(idIPSService)
        End Using
        'Return _ipsServiceAdminService.GetCupsHomologationByIPSServiceId(idIPSService)
    End Function

    Public Function CopyAndPasteIPSService(data As List(Of List(Of String)), ServiceManual As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.SurgicalProcedureService), List(Of Tuple(Of String, Integer))) Implements IContractIPSService.CopyAndPasteIPSService
        Using service As IIPSServiceAdminService = Container.Current.Resolve(Of IIPSServiceAdminService)()
            Return service.CopyAndPasteIPSService(data, ServiceManual)
        End Using
        'Return _ipsServiceAdminService.CopyAndPasteIPSService(data, ServiceManual)
    End Function

    ''' <summary>
    ''' obtiene los servicvios ips qx por id ips padre, opcionalmente se puede filtra por una clase en especifico
    ''' </summary>
    ''' <param name="parentId"></param>
    ''' <param name="classService"></param>
    ''' <returns></returns>
    Function GetSurgicalProcedureServiceByParentIPSId(parentId As Integer, Optional classService As EClassService = 0) As ActionResult(Of List(Of IPSService)) Implements IContractIPSService.GetSurgicalProcedureServiceByParentIPSId
        Using service As IIPSServiceAdminService = Container.Current.Resolve(Of IIPSServiceAdminService)()
            Return service.GetSurgicalProcedureServiceByParentIPSId(parentId, classService)
        End Using
    End Function
End Class

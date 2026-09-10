'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Infrastructure.CrossCutting.IOC

Partial Class CommonERPService

    Public Function ChangeStateEconomicActivity(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.EconomicActivity) Implements ICommonERPEconomicActivity.ChangeStateEconomicActivity
        Using economicActivityAdminService As IEconomicActivityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEconomicActivityAdminService)()
            Return economicActivityAdminService.ChangeStateEconomicActivity(code, state, session.AuditMessageWcf)
        End Using
    End Function

    Public Function DeleteEconomicActivity(EconomicActivity As Domain.Entities.EconomicActivity, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult Implements ICommonERPEconomicActivity.DeleteEconomicActivity
        Using economicActivityAdminService As IEconomicActivityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEconomicActivityAdminService)()
            Return economicActivityAdminService.DeleteEconomicActivity(EconomicActivity, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetEconomicActivity(code As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.EconomicActivity) Implements ICommonERPEconomicActivity.GetEconomicActivity
        Using economicActivityAdminService As IEconomicActivityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEconomicActivityAdminService)()
            Return economicActivityAdminService.GetEconomicActivity(code, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetEconomicActivityById(id As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.EconomicActivity) Implements ICommonERPEconomicActivity.GetEconomicActivityById
        Using economicActivityAdminService As IEconomicActivityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEconomicActivityAdminService)()
            Return economicActivityAdminService.GetEconomicActivityById(id, session.AuditMessageWcf)
        End Using
    End Function

    Public Function SaveEconomicActivity(EconomicActivity As Domain.Entities.EconomicActivity, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.EconomicActivity) Implements ICommonERPEconomicActivity.SaveEconomicActivity
        Using economicActivityAdminService As IEconomicActivityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEconomicActivityAdminService)()
            Return economicActivityAdminService.SaveEconomicActivity(EconomicActivity, session.AuditMessageWcf)
        End Using
    End Function

End Class

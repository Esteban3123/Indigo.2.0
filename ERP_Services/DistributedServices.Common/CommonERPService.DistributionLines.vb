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
    Implements ICommonERPSuppliersDistributionLines

    ''' <summary>
    ''' Elimina una linea de distribucion
    ''' </summary>
    ''' <param name="distributionLines"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDistributionLines(distributionLines As Domain.Entities.DistributionLines, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult Implements ICommonERPDistributionLines.DeleteDistributionLines
        Using distributionLinesAdminService As IDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDistributionLinesAdminService)()
            Return distributionLinesAdminService.DeleteDistributionLines(distributionLines, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una linea de distribucion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistributionLines(code As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DistributionLines) Implements ICommonERPDistributionLines.GetDistributionLines
        Using distributionLinesAdminService As IDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDistributionLinesAdminService)()
            Return distributionLinesAdminService.GetDistributionLines(code, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistributionLinesById(id As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DistributionLines) Implements ICommonERPDistributionLines.GetDistributionLinesById
        Using distributionLinesAdminService As IDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDistributionLinesAdminService)()
            Return distributionLinesAdminService.GetDistributionLinesById(id, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza una linea de distribucion
    ''' </summary>
    ''' <param name="distributionLines"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDistributionLines(distributionLines As Domain.Entities.DistributionLines, session As Infrastructure.CrossCutting.Base.SessionValues, Optional ByVal idSequense As Int64 = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DistributionLines) Implements ICommonERPDistributionLines.SaveDistributionLines
        Using distributionLinesAdminService As IDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDistributionLinesAdminService)()
            Return distributionLinesAdminService.SaveDistributionLines(distributionLines, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado a la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateDistributionLines(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DistributionLines) Implements ICommonERPDistributionLines.ChangeStateDistributionLines
        Using distributionLinesAdminService As IDistributionLinesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDistributionLinesAdminService)()
            Return distributionLinesAdminService.ChangeStateDistributionLines(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class

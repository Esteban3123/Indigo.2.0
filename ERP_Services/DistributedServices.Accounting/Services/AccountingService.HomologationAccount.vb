#Region "Imports"
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class AccountingService

#Region "Methods"

    ''' <summary>
    ''' Guarda una homologacion de cuentas
    ''' </summary>
    ''' <param name="ListHomologationAccount"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHomologationAccount(ListHomologationAccount As List(Of Domain.Entities.HomologationAccount)) As Domain.Base.Entities.ActionResult Implements IAccountingHomologationAccount.SaveHomologationAccount
        Using service As IHomologationAccountAdminService = Container.Current.Resolve(Of IHomologationAccountAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveHomologationAccount(ListHomologationAccount, audit)
        End Using
        'Return Me._homologationAccountAdminService.SaveHomologationAccount(ListHomologationAccount, audit)
    End Function

#End Region

End Class

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Accounting
Imports Microsoft.Practices.Unity
#End Region

Partial Public Class AccountingService

#Region "Functions"
    ''' <summary>
    ''' Funmcion para obtener el parametro contable
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingAccount(ByVal idOperationUnit As Integer) As Domain.Entities.GeneralLedgerSettings Implements IAccountingSettingAccount.GetSettingAccount
        Using service As ISettingAccountAdminService = Container.Current.Resolve(Of ISettingAccountAdminService)()
            Return service.GetSettingAccount(idOperationUnit)
        End Using
        'Return _SettingAccountAdminService.GetSettingAccount(idOperationUnit)
    End Function

    ''' <summary>
    ''' Saves the setting account.
    ''' </summary>
    ''' <param name="settingAccount">The setting account.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveSettingAccount(settingAccount As Domain.Entities.GeneralLedgerSettings) As Domain.Base.Entities.ActionResult(Of Domain.Entities.GeneralLedgerSettings) Implements IAccountingSettingAccount.SaveSettingAccount
        Using service As ISettingAccountAdminService = Container.Current.Resolve(Of ISettingAccountAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveSettingAccount(settingAccount, audit)
        End Using
        'Return _SettingAccountAdminService.SaveSettingAccount(settingAccount, audit)
    End Function
#End Region

End Class

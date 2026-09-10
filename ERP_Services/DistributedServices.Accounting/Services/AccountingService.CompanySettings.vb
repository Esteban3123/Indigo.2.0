Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Accounting
Imports Microsoft.Practices.Unity
Imports System.Globalization
Imports Domain.Entities

Partial Public Class AccountingService
    ''' <summary>
    ''' Funcion para obtenetr los parametros de la empresa
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCompanySettings() As Domain.Entities.CompanySettings Implements IAccountingCompanySettings.GetCompanySettings
        Using service As ICompanySettingsAdminService = Container.Current.Resolve(Of ICompanySettingsAdminService)()
            Return service.GetCompanySettings()
        End Using
        'Return _AccountingCompnaySettings.GetCompanySettings()
    End Function

    ''' <summary>
    ''' Saves the company settings.
    ''' </summary>
    ''' <param name="settings">The settings.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveCompanySettings(settings As Domain.Entities.CompanySettings) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanySettings) Implements IAccountingCompanySettings.SaveCompanySettings
        Using service As ICompanySettingsAdminService = Container.Current.Resolve(Of ICompanySettingsAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveCompanySettings(settings, audit)
        End Using
        'Return _AccountingCompnaySettings.SaveCompanySettings(settings, audit)
    End Function

    ''' <summary>
    ''' retorna el Formato de la moneda dependiendo la momenda oficial del sistema, guardada en company settings
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrency() As Currency Implements IAccountingCompanySettings.GetOfficialCurrency
        Using service As ICompanySettingsAdminService = Container.Current.Resolve(Of ICompanySettingsAdminService)()
            Return service.GetOfficialCurrency()
        End Using
    End Function
End Class

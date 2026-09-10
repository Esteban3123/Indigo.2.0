Imports Domain.Entities
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class AccountingService

    Public Function GetSettingsExogenousInformation(audit As AuditMessage) As List(Of SettingsExogenousInformation) Implements IAccountingSettingsExogenousInformation.GetSettingsExogenousInformation
        Using service As ISettingsExogenousInformationAdminService = Container.Current.Resolve(Of ISettingsExogenousInformationAdminService)()
            Return service.GetSettingsExogenousInformation()
        End Using
        'Return _SettingsExogenousInformationAdminService.GetSettingsExogenousInformation()
    End Function

    Public Function SaveSettingsExogenousInformation(ListSettingsExogenousInformation As List(Of SettingsExogenousInformation), audit As AuditMessage) As ActionResult(Of List(Of SettingsExogenousInformation)) Implements IAccountingSettingsExogenousInformation.SaveSettingsExogenousInformation
        Using service As ISettingsExogenousInformationAdminService = Container.Current.Resolve(Of ISettingsExogenousInformationAdminService)()
            Return service.SaveSettingsExogenousInformation(ListSettingsExogenousInformation, audit)
        End Using
        'Return _SettingsExogenousInformationAdminService.SaveSettingsExogenousInformation(ListSettingsExogenousInformation, audit)
    End Function

End Class

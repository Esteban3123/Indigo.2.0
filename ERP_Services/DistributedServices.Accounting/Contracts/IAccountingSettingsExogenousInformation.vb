#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IAccountingSettingsExogenousInformation

    <OperationContract()>
    Function SaveSettingsExogenousInformation(ListSettingsExogenousInformation As List(Of SettingsExogenousInformation), audit As AuditMessage) As ActionResult(Of List(Of SettingsExogenousInformation))

    <OperationContract()>
    Function GetSettingsExogenousInformation(audit As AuditMessage) As List(Of SettingsExogenousInformation)
End Interface

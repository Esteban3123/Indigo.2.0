Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.AccountManagement.Model
Imports Infrastructure.Data.Xpo.AccountManagementRespository

Public Interface IDashboardAccountManagement
    ReadOnly Property MyTag As Object

    ReadOnly Property AttentionCenterCode As String

#Region "XPO"
    Property TransfersXpo As List(Of GetUserFolios)

    Property AttentionCenterXpo As XPInstantFeedbackSource

    Property ManagementAreasXpo As List(Of ManagementAreasXpo)

    Property TraceabilityDatasource As List(Of VFolioTraceabilityProperties)

    Property AdmissionsXpo As LinqInstantFeedbackSource

    Property PatientsXpo As XPInstantFeedbackSource

#End Region
End Interface

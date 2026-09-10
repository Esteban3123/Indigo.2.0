Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.AccountManagementRespository
Imports Infrastructure.Data.Xpo.SecurityRepository

Public Interface IAccountAcceptance
    ReadOnly Property MyTag As Object

#Region "XPO"
    Property AttentionCenterXpo As XPInstantFeedbackSource

    Property UsersXpo As LinqInstantFeedbackSource

    Property RejectionReasonDatasource As List(Of RejectionReason)

    Property ManagementAreasDatasource As List(Of ManagementAreas)
#End Region

End Interface

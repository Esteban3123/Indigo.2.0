#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.AccountManagement.Model
#End Region

Public Interface IDashboardAccountAssignment

    ReadOnly Property MyTag As Object

    ReadOnly Property CareCenterCode As String

#Region "XPO"
    Property CareCenterXpo As XPInstantFeedbackSource

    Property PendingAssignmentDatasource As XPInstantFeedbackSource
#End Region

End Interface

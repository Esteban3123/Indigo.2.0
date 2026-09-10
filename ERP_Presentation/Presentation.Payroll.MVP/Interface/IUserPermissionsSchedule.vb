Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq

Public Interface IUserPermissionSchedule

    Inherits IcrudBase

    WriteOnly Property SLERoleDataSource As XPInstantFeedbackSource
    WriteOnly Property SLEUserDataSource As LinqInstantFeedbackSource
    WriteOnly Property SLEUserTypeDataSource As Object
    WriteOnly Property SLEFunctionalUnitDataSource As XPInstantFeedbackSource
    WriteOnly Property GCFunctionalUnitDataSource As XPInstantFeedbackSource
    WriteOnly Property SLEPositionDataSource As XPInstantFeedbackSource

End Interface

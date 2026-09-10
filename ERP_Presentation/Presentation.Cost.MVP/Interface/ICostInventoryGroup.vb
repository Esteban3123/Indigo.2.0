#Region "Libraries imported"

Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Entities

#End Region

Public Interface ICostInventoryGroup
    Inherits IcrudBase

#Region "Fields"

    ReadOnly Property MyTag As Object

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    WriteOnly Property ActionsOnControls As Boolean

    Property Sequence As CostSecuence

#End Region

#Region "Properties"

    Property Code As String

    Property Name As String

    Property MeasurementUnitId As Integer

    Property Description As String

    Property Status As Boolean

#End Region

#Region "XPO"

    Property MeasurementUnitXpo As XPInstantFeedbackSource

#End Region

End Interface
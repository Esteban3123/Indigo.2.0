'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/12/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports Presentation.Controls
Imports DevExpress.Xpo

#End Region

Public Interface ICostLogisticsProductionCenterRecord
    Inherits IcrudBase

#Region "Members"

    Property Code As String
    Property ProductionCenterId As Int32
    Property RecordDate As DateTime
    Property Description As String
    Property Status As String
    Property ListLogisticsProductionCenterRecordDetail As List(Of CostLogisticsProductionCenterRecordDetail)
    Property Sequence As CostSecuence
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ReadOnly Property MyTag As String
    Property ProductionCenterLogisticXpo As XPInstantFeedbackSource
    Property ProductionCenterTargetXpo As XPInstantFeedbackSource
    Property MeasurementUnitXpo As XPInstantFeedbackSource
    WriteOnly Property ActionsOnControls As Boolean
    Sub LoadControls()
    Sub CleanControls()
    Sub AssigningValues()
    Sub CleanPopUp()

#End Region

End Interface

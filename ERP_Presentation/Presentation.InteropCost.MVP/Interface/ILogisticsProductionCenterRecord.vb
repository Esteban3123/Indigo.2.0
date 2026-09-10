'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Juan F. Tamayo
' Created          : 2016-11-5
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

#End Region

Public Interface ILogisticsProductionCenterRecord
    Inherits IcrudBase

#Region "Members"

    Property Code As String
    Property ProductionCenterId As Int32
    Property RecordDate As DateTime
    Property Description As String
    Property Status As String
    Property ListLogisticsProductionCenterRecordDetail As List(Of LogisticsProductionCenterRecordDetail)

    Property Sequence As InteropCostSecuence
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ReadOnly Property MyTag As String
    Property ProductionCenterLogisticsDataSource As Object
    Property ProductionCenterTargetDataSource As Object
    Property MeasurementUnitDataSource As Object
    WriteOnly Property ActionsOnControls As Boolean

    Sub LoadControls()
    Sub CleanControls()
    Sub AssigningValues()
    Sub CleanPopUp()

#End Region

End Interface

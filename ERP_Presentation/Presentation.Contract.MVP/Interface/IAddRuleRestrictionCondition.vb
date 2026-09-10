'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Giovanny Plazas Lozano
' Created          : 16/12/2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IAddRuleRestrictionCondition
    Inherits ICrudBase

    Property Operator1 As Byte?
    Property Operator2 As Byte?
    Property FunctionalUnit1 As Integer?
    Property FunctionalUnit2 As Integer?
    Property FunctionalUnitType1 As Byte?
    Property FunctionalUnitType2 As Byte?
    Property StayType1 As String
    Property StayType2 As String
    Property ManualRateType1 As Byte?
    Property ManualRateType2 As Byte?
    Property QxGroup1 As Integer?
    Property QxGroup2 As Integer?
    Property UVRRange1 As Integer?
    Property UVRRange2 As Integer?
#Region "Datasource"
    WriteOnly Property DataSourceOperators As List(Of Tuple(Of Byte, String))
    Property DataSourceFunctionalUnit1 As XPInstantFeedbackSource
    Property DataSourceFunctionalUnit2 As XPInstantFeedbackSource
    WriteOnly Property DataSourceFunctionalUnitType As List(Of Tuple(Of Byte, String))
    Property DataSourceStayType1 As XPInstantFeedbackSource
    Property DataSourceStayType2 As XPInstantFeedbackSource
    WriteOnly Property DataSourceManualRateType As List(Of Tuple(Of Byte, String))
    Property DataSourceQxGroup1 As XPInstantFeedbackSource
    Property DataSourceQxGroup2 As XPInstantFeedbackSource
    Property DataSourceUVRRange1 As XPInstantFeedbackSource
    Property DataSourceUVRRange2 As XPInstantFeedbackSource
#End Region

End Interface

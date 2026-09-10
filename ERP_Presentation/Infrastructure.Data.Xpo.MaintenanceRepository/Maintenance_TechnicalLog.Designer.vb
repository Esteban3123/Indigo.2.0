Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.TechnicalLog")>
Partial Public Class Maintenance_TechnicalLog
        Inherits XPLiteObject
        Dim fId As Integer
        <Key(True)>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property
        Dim fCode As String
        <Size(20)>
        Public Property Code() As String
            Get
                Return fCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Code", fCode, value)
            End Set
        End Property
        Dim fName As String
        <Size(50)>
        Public Property Name() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", fName, value)
            End Set
        End Property
        Dim fState As Boolean
        <Indexed(Name:="IX_TechnicalLog_State")>
        Public Property State() As Boolean
            Get
                Return fState
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("State", fState, value)
            End Set
        End Property
        <Association("Maintenance_TechnicalLogDetailReferencesMaintenance_TechnicalLog")>
        Public ReadOnly Property Maintenance_TechnicalLogDetails() As XPCollection(Of Maintenance_TechnicalLogDetail)
            Get
                Return GetCollection(Of Maintenance_TechnicalLogDetail)("Maintenance_TechnicalLogDetails")
            End Get
        End Property
        <Association("Maintenance_TechnicalLogMeasurementUnitDetailReferencesMaintenance_TechnicalLog")>
        Public ReadOnly Property Maintenance_TechnicalLogMeasurementUnitDetails() As XPCollection(Of Maintenance_TechnicalLogMeasurementUnitDetail)
            Get
                Return GetCollection(Of Maintenance_TechnicalLogMeasurementUnitDetail)("Maintenance_TechnicalLogMeasurementUnitDetails")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemTechnicalLogReferencesMaintenance_TechnicalLog")>
        Public ReadOnly Property FixedAsset_FixedAssetItemTechnicalLogs() As XPCollection(Of FixedAsset_FixedAssetItemTechnicalLog)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemTechnicalLog)("FixedAsset_FixedAssetItemTechnicalLogs")
            End Get
        End Property
End Class
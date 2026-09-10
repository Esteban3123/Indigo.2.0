Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.MeasurementUnit")>
Public Class Maintenance_MeasurementUnit
    Inherits XPLiteObject

#Region "Members"

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
    <Indexed(Name:="IX_MeasurementUnit", Unique:=True)>
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
    <Size(25)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fAbbreviation As String
    <Size(10)>
    Public Property Abbreviation() As String
        Get
            Return fAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Abbreviation", fAbbreviation, value)
        End Set
    End Property
    Dim fType As String
    <Size(2)>
    Public Property Type() As String
        Get
            Return fType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Type", fType, value)
        End Set
    End Property
    Dim fState As Boolean
    <Indexed(Name:="IX_MeasurementUnit_State")>
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

#End Region

#Region "Associations Members"

    <Association("Maintenance_TechnicalLogDetailReferencesMaintenance_MeasurementUnit")>
    Public ReadOnly Property Maintenance_TechnicalLogDetails() As XPCollection(Of Maintenance_TechnicalLogDetail)
        Get
            Return GetCollection(Of Maintenance_TechnicalLogDetail)("Maintenance_TechnicalLogDetails")
        End Get
    End Property
    <Association("Maintenance_TechnicalLogMeasurementUnitDetailReferencesMaintenance_MeasurementUnit")>
    Public ReadOnly Property Maintenance_TechnicalLogMeasurementUnitDetails() As XPCollection(Of Maintenance_TechnicalLogMeasurementUnitDetail)
        Get
            Return GetCollection(Of Maintenance_TechnicalLogMeasurementUnitDetail)("Maintenance_TechnicalLogMeasurementUnitDetails")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Alarcon
' Created          : 18/01/2023
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewComponentsNPT")>
Partial Public Class ViewListComponentsNPTXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Dim fIdHCPARNUTC As Integer
    Public Property IdHCPARNUTC() As Integer
        Get
            Return fIdHCPARNUTC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdHCPARNUTC", fIdHCPARNUTC, value)
        End Set
    End Property

    Dim fAtcId As Integer
    Public Property AtcId() As Integer
        Get
            Return fAtcId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AtcId", fAtcId, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fAbbreviationName As String
    Public Property AbbreviationName() As String
        Get
            Return fAbbreviationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AbbreviationName", fAbbreviationName, value)
        End Set
    End Property

    Dim fUnitType As Integer
    Public Property UnitType() As Integer
        Get
            Return fUnitType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitType", fUnitType, value)
        End Set
    End Property

    Dim fMeasurementUnitId As String
    Public Property MeasurementUnitId() As String
        Get
            Return fMeasurementUnitId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitId", fMeasurementUnitId, value)
        End Set
    End Property

End Class
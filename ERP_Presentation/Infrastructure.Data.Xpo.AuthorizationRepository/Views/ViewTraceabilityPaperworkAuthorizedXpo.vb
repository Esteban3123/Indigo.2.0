'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/06/2020
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Authorization.ViewTraceabilityPaperworkAuthorized")>
Public Class ViewTraceabilityPaperworkAuthorizedXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fTraceabilityPaperworkEventsId As Integer
    <Key(True)>
    Public Property TraceabilityPaperworkEventsId() As Integer
        Get
            Return fTraceabilityPaperworkEventsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkEventsId", fTraceabilityPaperworkEventsId, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkId As Integer
    Public Property TraceabilityPaperworkId() As Integer
        Get
            Return fTraceabilityPaperworkId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkId", fTraceabilityPaperworkId, value)
        End Set
    End Property

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
        End Set
    End Property

    Dim fFunctionalUnitCode As String
    Public Property FunctionalUnitCode() As String
        Get
            Return fFunctionalUnitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCode", fFunctionalUnitCode, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
    End Property

    Dim fServiceCode As String
    Public Property ServiceCode() As String
        Get
            Return fServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCode", fServiceCode, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fRequestDate As DateTime
    Public Property RequestDate() As DateTime
        Get
            Return fRequestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RequestDate", fRequestDate, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fAuthorizationNumber As String
    Public Property AuthorizationNumber() As String
        Get
            Return fAuthorizationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationNumber", fAuthorizationNumber, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class

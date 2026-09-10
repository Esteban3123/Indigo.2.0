'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/07/2020
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
<Persistent("Authorization.ViewAcceptanceAuthorization")>
Public Class ViewAcceptanceAuthorizationXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
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

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientCodeName As String
    Public Property PatientCodeName() As String
        Get
            Return fPatientCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCodeName", fPatientCodeName, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
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

    Dim fServiceCodeName As String
    Public Property ServiceCodeName() As String
        Get
            Return fServiceCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCodeName", fServiceCodeName, value)
        End Set
    End Property

    Dim fFunctionalUnitId As Integer
    Public Property FunctionalUnitId() As Integer
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FunctionalUnitId", fFunctionalUnitId, value)
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

    Dim fFunctionalUnitName As String
    Public Property FunctionalUnitName() As String
        Get
            Return fFunctionalUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitName", fFunctionalUnitName, value)
        End Set
    End Property

    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
        End Set
    End Property

    Dim fFunctionalUnitTargetId As Integer
    Public Property FunctionalUnitTargetId() As Integer
        Get
            Return fFunctionalUnitTargetId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FunctionalUnitTargetId", fFunctionalUnitTargetId, value)
        End Set
    End Property

    Dim fFunctionalUnitTargetCode As String
    Public Property FunctionalUnitTargetCode() As String
        Get
            Return fFunctionalUnitTargetCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitTargetCode", fFunctionalUnitTargetCode, value)
        End Set
    End Property

    Dim fFunctionalUnitTargetName As String
    Public Property FunctionalUnitTargetName() As String
        Get
            Return fFunctionalUnitTargetName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitTargetName", fFunctionalUnitTargetName, value)
        End Set
    End Property

    Dim fFunctionalUnitTargetCodeName As String
    Public Property FunctionalUnitTargetCodeName() As String
        Get
            Return fFunctionalUnitTargetCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitTargetCodeName", fFunctionalUnitTargetCodeName, value)
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

    Dim fCareCenterTargetCode As String
    Public Property CareCenterTargetCode() As String
        Get
            Return fCareCenterTargetCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterTargetCode", fCareCenterTargetCode, value)
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

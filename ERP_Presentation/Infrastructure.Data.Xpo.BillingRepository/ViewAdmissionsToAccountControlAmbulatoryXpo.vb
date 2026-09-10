'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/02/2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Billing.ViewAdmissionsToAccountControlAmbulatory")>
Public Class ViewAdmissionsToAccountControlAmbulatoryXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fAdmissionNumber As String
    <Indexed(Name:="IDX_V1", Unique:=True)>
    <Key(True)>
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fAdmissionDate As DateTime
    Public Property AdmissionDate() As DateTime
        Get
            Return fAdmissionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdmissionDate", fAdmissionDate, value)
        End Set
    End Property

    Dim fPatientIdentification As String
    Public Property PatientIdentification() As String
        Get
            Return fPatientIdentification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientIdentification", fPatientIdentification, value)
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

    Dim fPatientDescription As String
    Public Property PatientDescription() As String
        Get
            Return fPatientDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientDescription", fPatientDescription, value)
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

    Dim fFuntionalUnitDescription As String
    Public Property FuntionalUnitDescription() As String
        Get
            Return fFuntionalUnitDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FuntionalUnitDescription", fFuntionalUnitDescription, value)
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

    Dim fCareCenterName As String
    Public Property CareCenterName() As String
        Get
            Return fCareCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterName", fCareCenterName, value)
        End Set
    End Property

    Dim fCareCenterDescription As String
    Public Property CareCenterDescription() As String
        Get
            Return fCareCenterDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterDescription", fCareCenterDescription, value)
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

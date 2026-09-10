'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/03/2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Billing.ViewListAppointments")>
Public Class ViewListAppointmentsXpo
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

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fAdmissionCode As String
    Public Property AdmissionCode() As String
        Get
            Return fAdmissionCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionCode", fAdmissionCode, value)
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

    Dim fCareCenterDescription As String
    Public Property CareCenterDescription() As String
        Get
            Return fCareCenterDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterDescription", fCareCenterDescription, value)
        End Set
    End Property

    Dim fInitialDate As Date
    Public Property InitialDate() As Date
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As Date
    Public Property EndDate() As Date
        Get
            Return fEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fSpecialtyCode As String
    Public Property SpecialtyCode() As String
        Get
            Return fSpecialtyCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SpecialtyCode", fSpecialtyCode, value)
        End Set
    End Property

    Dim fSpecialtyDescription As String
    Public Property SpecialtyDescription() As String
        Get
            Return fSpecialtyDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SpecialtyDescription", fSpecialtyDescription, value)
        End Set
    End Property

    Dim fActivityCode As String
    Public Property ActivityCode() As String
        Get
            Return fActivityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityCode", fActivityCode, value)
        End Set
    End Property

    Dim fActivityDescription As String
    Public Property ActivityDescription() As String
        Get
            Return fActivityDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityDescription", fActivityDescription, value)
        End Set
    End Property

    Dim fProfessionalCode As String
    Public Property ProfessionalCode() As String
        Get
            Return fProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalCode", fProfessionalCode, value)
        End Set
    End Property

    Dim fProfessionalDescription As String
    Public Property ProfessionalDescription() As String
        Get
            Return fProfessionalDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalDescription", fProfessionalDescription, value)
        End Set
    End Property

    Dim fStatusCode As String
    Public Property StatusCode() As String
        Get
            Return fStatusCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusCode", fStatusCode, value)
        End Set
    End Property

    Dim fStatusDescription As String
    Public Property StatusDescription() As String
        Get
            Return fStatusDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusDescription", fStatusDescription, value)
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

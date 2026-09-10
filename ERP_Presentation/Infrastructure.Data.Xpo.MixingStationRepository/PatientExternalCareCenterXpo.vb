'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.PatientExternalCareCenter")>
Partial Public Class PatientExternalCareCenterXpo
    Inherits XPLiteObject

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

    Dim fIdentificationNumber As String
    Public Property IdentificationNumber() As String
        Get
            Return fIdentificationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationNumber", fIdentificationNumber, value)
        End Set
    End Property

    Dim fIdentificationTypeId As Integer
    Public Property IdentificationTypeId() As Integer
        Get
            Return fIdentificationTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdentificationTypeId", fIdentificationTypeId, value)
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

    Dim fLastName As String
    Public Property LastName() As String
        Get
            Return fLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastName", fLastName, value)
        End Set
    End Property

    Dim fGenderTypeId As Integer
    Public Property GenderTypeId() As Integer
        Get
            Return fGenderTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GenderTypeId", fGenderTypeId, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fPatientMobileNumber As String
    Public Property PatientMobileNumber() As String
        Get
            Return fPatientMobileNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientMobileNumber", fPatientMobileNumber, value)
        End Set
    End Property

    Dim fPatientEmail As String
    Public Property PatientEmail() As String
        Get
            Return fPatientEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientEmail", fPatientEmail, value)
        End Set
    End Property

    Dim fExternalFunctionalUnit As String
    Public Property ExternalFunctionalUnit() As String
        Get
            Return fExternalFunctionalUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ExternalFunctionalUnit", fExternalFunctionalUnit, value)
        End Set
    End Property

    Dim fPatientBed As String
    Public Property PatientBed() As String
        Get
            Return fPatientBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientBed", fPatientBed, value)
        End Set
    End Property

    <PersistentAlias("CONCAT(IdentificationNumber, ' - ', Name, ' ', LastName)")>
    Public ReadOnly Property IdentificationName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("IdentificationName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <Association("PatientReferencesPECC", GetType(RequestUnitDoseExternalCareCenterPatientXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterPatientXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)("RequestUnitDoseExternalCareCenterPatientXpo")
        End Get
    End Property

#End Region

End Class
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

<Persistent("Billing.ViewAdministrativeNotesAccountControlAmbulatory")>
Public Class ViewAdministrativeNotesAccountControlAmbulatoryXpo
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

    Dim fSelection As Boolean
    Public Property Selection() As Boolean
        Get
            Return fSelection
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Selection", fSelection, value)
        End Set
    End Property

    Dim fMedicalOrderDate As Date
    Public Property MedicalOrderDate() As Date
        Get
            Return fMedicalOrderDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("MedicalOrderDate", fMedicalOrderDate, value)
        End Set
    End Property

    Dim fCupsCode As String
    Public Property CupsCode() As String
        Get
            Return fCupsCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsCode", fCupsCode, value)
        End Set
    End Property

    Dim fCupsName As String
    Public Property CupsName() As String
        Get
            Return fCupsName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsName", fCupsName, value)
        End Set
    End Property

    Dim fCupsDescription As String
    Public Property CupsDescription() As String
        Get
            Return fCupsDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsDescription", fCupsDescription, value)
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

    Dim fProfessionalName As String
    Public Property ProfessionalName() As String
        Get
            Return fProfessionalName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalName", fProfessionalName, value)
        End Set
    End Property

    Dim fProfessionalEspecialty As String
    Public Property ProfessionalEspecialty() As String
        Get
            Return fProfessionalEspecialty
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalEspecialty", fProfessionalEspecialty, value)
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

    Dim fProfessionalNit As String
    Public Property ProfessionalNit() As String
        Get
            Return fProfessionalNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionalNit", fProfessionalNit, value)
        End Set
    End Property

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fFolioNumber As String
    Public Property FolioNumber() As String
        Get
            Return fFolioNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FolioNumber", fFolioNumber, value)
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

    Dim fApplyRIAS As Boolean
    Public Property ApplyRIAS() As Boolean
        Get
            Return fApplyRIAS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyRIAS", fApplyRIAS, value)
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

    Dim fFunctionalUnitDescription As String
    Public Property FunctionalUnitDescription() As String
        Get
            Return fFunctionalUnitDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitDescription", fFunctionalUnitDescription, value)
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

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fServiceOrderId As Integer
    Public Property ServiceOrderId() As Integer
        Get
            Return fServiceOrderId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderId", fServiceOrderId, value)
        End Set
    End Property

    Dim fRiasCupsId As Integer
    Public Property RiasCupsId() As Integer
        Get
            Return fRiasCupsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RiasCupsId", fRiasCupsId, value)
        End Set
    End Property

    Dim fRiasDescription As String
    Public Property RiasDescription() As String
        Get
            Return fRiasDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RiasDescription", fRiasDescription, value)
        End Set
    End Property

    Dim fVariable As String
    Public Property Variable() As String
        Get
            Return fVariable
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Variable", fVariable, value)
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

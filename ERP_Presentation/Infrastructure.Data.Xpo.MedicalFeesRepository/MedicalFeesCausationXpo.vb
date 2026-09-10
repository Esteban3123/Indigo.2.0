'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MedicalFeesRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2014
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
<Persistent("MedicalFees.MedicalFeesCausation")> _
Public Class MedicalFeesCausationXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    <Size(10)> _
    <Persistent("AdmissionNumber")> _
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fPatientCode As String
    <Size(15)> _
    <Persistent("PatientCode")> _
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fHealthProfessionalCode As String
    <Size(20)> _
    <Persistent("HealthProfessionalCode")> _
    Public Property HealthProfessionalCode() As String
        Get
            Return fHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthProfessionalCode", fHealthProfessionalCode, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("MedicalFeesCausationReferencesThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fMedicalFeesContractId As MedicalFeesContractXpo
    <Association("MedicalFeesCausationReferencesMedicalFeesContract")> _
    Public Property MedicalFeesContractId() As MedicalFeesContractXpo
        Get
            Return fMedicalFeesContractId
        End Get
        Set(ByVal value As MedicalFeesContractXpo)
            SetPropertyValue(Of MedicalFeesContractXpo)("MedicalFeesContractId", fMedicalFeesContractId, value)
        End Set
    End Property

    Dim fCausationDate As DateTime
    <Persistent("CausationDate")> _
    Public Property CausationDate() As DateTime
        Get
            Return fCausationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CausationDate", fCausationDate, value)
        End Set
    End Property

    Dim fServiceOrderId As ServiceOrderXpo
    <Association("MedicalFeesCausationReferencesServiceOrder")> _
    Public Property ServiceOrderId() As ServiceOrderXpo
        Get
            Return fServiceOrderId
        End Get
        Set(ByVal value As ServiceOrderXpo)
            SetPropertyValue(Of ServiceOrderXpo)("ServiceOrderId", fServiceOrderId, value)
        End Set
    End Property

    Dim fServiceOrderDetailId As ServiceOrderDetailXpo
    <Association("MedicalFeesCausationReferencesServiceOrderDetail")> _
    Public Property ServiceOrderDetailId() As ServiceOrderDetailXpo
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As ServiceOrderDetailXpo)
            SetPropertyValue(Of ServiceOrderDetailXpo)("ServiceOrderDetailId", fServiceOrderDetailId, value)
        End Set
    End Property

    Dim fAmountPayable As Decimal
    <Size(100)> _
    <Persistent("AmountPayable")> _
    Public Property AmountPayable() As Decimal
        Get
            Return fAmountPayable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountPayable", fAmountPayable, value)
        End Set
    End Property

    Dim fMedicalFeesContractValue As Decimal
    <Size(100)> _
    <Persistent("MedicalFeesContractValue")> _
    Public Property MedicalFeesContractValue() As Decimal
        Get
            Return fMedicalFeesContractValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MedicalFeesContractValue", fMedicalFeesContractValue, value)
        End Set
    End Property

    Dim fInvoiceQuantity As Integer
    <Persistent("InvoiceQuantity")> _
    Public Property InvoiceQuantity() As Integer
        Get
            Return fInvoiceQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceQuantity", fInvoiceQuantity, value)
        End Set
    End Property

    Dim fTotalAmountPayable As Decimal
    <Size(100)> _
    <Persistent("TotalAmountPayable")> _
    Public Property TotalAmountPayable() As Decimal
        Get
            Return fTotalAmountPayable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAmountPayable", fTotalAmountPayable, value)
        End Set
    End Property

    Dim fMedicalFeePaid As Boolean
    <Persistent("MedicalFeePaid")> _
    Public Property MedicalFeePaid() As Boolean
        Get
            Return fMedicalFeePaid
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MedicalFeePaid", fMedicalFeePaid, value)
        End Set
    End Property

    Dim fInvoiceReversal As Boolean
    <Persistent("InvoiceReversal")> _
    Public Property InvoiceReversal() As Boolean
        Get
            Return fInvoiceReversal
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InvoiceReversal", fInvoiceReversal, value)
        End Set
    End Property

    Dim fReassessmentForReversal As Boolean
    <Persistent("ReassessmentForReversal")> _
    Public Property ReassessmentForReversal() As Boolean
        Get
            Return fReassessmentForReversal
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ReassessmentForReversal", fReassessmentForReversal, value)
        End Set
    End Property

    Dim fReassessmentForObjection As Boolean
    <Persistent("ReassessmentForObjection")> _
    Public Property ReassessmentForObjection() As Boolean
        Get
            Return fReassessmentForObjection
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ReassessmentForObjection", fReassessmentForObjection, value)
        End Set
    End Property

    Dim fObjectionAccepted As Boolean
    <Persistent("ObjectionAccepted")> _
    Public Property ObjectionAccepted() As Boolean
        Get
            Return fObjectionAccepted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ObjectionAccepted", fObjectionAccepted, value)
        End Set
    End Property

    Dim fStatus As Integer
    <Persistent("Status")> _
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    'columna que devuelve el codigo y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(AdmissionNumber,' - '),PatientCode)")>
    Public ReadOnly Property AdmissionNumberPatientCode() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("AdmissionNumberPatientCode"))
        End Get
    End Property

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

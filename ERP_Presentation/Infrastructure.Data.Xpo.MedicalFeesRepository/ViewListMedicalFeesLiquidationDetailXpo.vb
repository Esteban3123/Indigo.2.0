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
<Persistent("MedicalFees.ViewListMedicalFeesLiquidationDetail")> _
Public Class ViewListMedicalFeesLiquidationDetailXpo
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
    <Persistent("PatientCode")> _
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fThirdPartyDescription As String
    <Persistent("ThirdPartyDescription")> _
    Public Property ThirdPartyDescription() As String
        Get
            Return fThirdPartyDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyDescription", fThirdPartyDescription, value)
        End Set
    End Property

    Dim fServiceOrderCode As String
    <Persistent("ServiceOrderCode")> _
    Public Property ServiceOrderCode() As String
        Get
            Return fServiceOrderCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceOrderCode", fServiceOrderCode, value)
        End Set
    End Property

    Dim fIPSServiceName As String
    <Persistent("IPSServiceName")> _
    Public Property IPSServiceName() As String
        Get
            Return fIPSServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceName", fIPSServiceName, value)
        End Set
    End Property

    Dim fAmountPayable As Decimal
    <Persistent("AmountPayable")> _
    Public Property AmountPayable() As Decimal
        Get
            Return fAmountPayable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountPayable", fAmountPayable, value)
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
    <Persistent("TotalAmountPayable")> _
    Public Property TotalAmountPayable() As Decimal
        Get
            Return fTotalAmountPayable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAmountPayable", fTotalAmountPayable, value)
        End Set
    End Property

    Dim fMedicalFeesContractValue As Decimal
    <Persistent("MedicalFeesContractValue")> _
    Public Property MedicalFeesContractValue() As Decimal
        Get
            Return fMedicalFeesContractValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MedicalFeesContractValue", fMedicalFeesContractValue, value)
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

    Dim fStatusCausation As Integer
    <Persistent("StatusCausation")> _
    Public Property StatusCausation() As Integer
        Get
            Return fStatusCausation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("StatusCausation", fStatusCausation, value)
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

    Dim fLiquidationType As Integer
    <Persistent("LiquidationType")> _
    Public Property LiquidationType() As Integer
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LiquidationType", fLiquidationType, value)
        End Set
    End Property

    Dim fMedicalFeesLiquidacionId As Integer
    <Persistent("MedicalFeesLiquidacionId")> _
    Public Property MedicalFeesLiquidacionId() As Integer
        Get
            Return fMedicalFeesLiquidacionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MedicalFeesLiquidacionId", fMedicalFeesLiquidacionId, value)
        End Set
    End Property

    Dim fMedicalFeesCausationId As Integer
    <Persistent("MedicalFeesCausationId")> _
    Public Property MedicalFeesCausationId() As Integer
        Get
            Return fMedicalFeesCausationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MedicalFeesCausationId", fMedicalFeesCausationId, value)
        End Set
    End Property

    Dim fServiceDate As DateTime
    <Persistent("ServiceDate")>
    Public Property ServiceDate() As DateTime
        Get
            Return fServiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServiceDate", fServiceDate, value)
        End Set
    End Property

    Dim fInvoiceId As Integer
    Public Property InvoiceId() As Integer
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fInvoiceDetailId As Integer
    Public Property InvoiceDetailId() As Integer
        Get
            Return fInvoiceDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceDetailId", fInvoiceDetailId, value)
        End Set
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

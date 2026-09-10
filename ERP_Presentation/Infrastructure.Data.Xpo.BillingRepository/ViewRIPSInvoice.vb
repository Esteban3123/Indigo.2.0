'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/12/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

#Region "Structure"

Public Structure RIPSKey

    <Persistent("InvoiceId")>
    Public Property InvoiceId As Integer

    <Persistent("RadicatedConsecutive")>
    Public Property RadicatedConsecutive As Integer?

End Structure

#End Region

''' <summary>
''' asociacion entre procedureCups y MarketingUnitCups usado en los servicios Xpo
''' </summary>
<Persistent("Billing.ViewRIPSInvoice")>
Public Class ViewRIPSInvoice
    Inherits XPLiteObject

#Region "Members"

    <Key(), Persistent()>
    Public Property Key As RIPSKey

    Dim fInvoiceId As Integer
    Public Property InvoiceId() As Integer
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
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

    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fHealthAdministratorId As Integer?
    Public Property HealthAdministratorId() As Integer?
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fHealthAdministratorCodeName As String
    Public Property HealthAdministratorCodeName() As String
        Get
            Return fHealthAdministratorCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorCodeName", fHealthAdministratorCodeName, value)
        End Set
    End Property

    Dim fThirdPartyNitName As String
    Public Property ThirdPartyNitName() As String
        Get
            Return fThirdPartyNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNitName", fThirdPartyNitName, value)
        End Set
    End Property

    Dim fCareGroupId As Integer
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
        End Set
    End Property

    Dim fTotalInvoice As Decimal
    Public Property TotalInvoice() As Decimal
        Get
            Return fTotalInvoice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalInvoice", fTotalInvoice, value)
        End Set
    End Property

    Dim fInvoiceDate As Date
    Public Property InvoiceDate() As Date
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property

    Dim fThirdPartySalesValue As Decimal
    Public Property ThirdPartySalesValue() As Decimal
        Get
            Return fThirdPartySalesValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartySalesValue", fThirdPartySalesValue, value)
        End Set
    End Property

    Dim fTotalPatientSalesPrice As Decimal
    Public Property TotalPatientSalesPrice() As Decimal
        Get
            Return fTotalPatientSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPatientSalesPrice", fTotalPatientSalesPrice, value)
        End Set
    End Property

    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
        End Set
    End Property

    Dim fStateName As String
    Public Property StateName() As String
        Get
            Return fStateName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StateName", fStateName, value)
        End Set
    End Property

    Dim fInvoiceRadicateId As Integer?
    Public Property InvoiceRadicateId() As Integer?
        Get
            Return fInvoiceRadicateId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("InvoiceRadicateId", fInvoiceRadicateId, value)
        End Set
    End Property

    Dim fRadicatedConsecutive As Integer?
    Public Property RadicatedConsecutive() As Integer?
        Get
            Return fRadicatedConsecutive
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("RadicatedConsecutive", fRadicatedConsecutive, value)
        End Set
    End Property

    Dim fCapitationInitialDate As Date?
    Public Property CapitationInitialDate() As Date?
        Get
            Return fCapitationInitialDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("CapitationInitialDate", fCapitationInitialDate, value)
        End Set
    End Property

    Dim fCapitationEndDate As Date?
    Public Property CapitationEndDate() As Date?
        Get
            Return fCapitationEndDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("CapitationEndDate", fCapitationEndDate, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer?
    Public Property ThirdPartyId() As Integer?
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fInvoiceCategoryId As Integer
    Public Property InvoiceCategoryId() As Integer
        Get
            Return fInvoiceCategoryId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceCategoryId", fInvoiceCategoryId, value)
        End Set
    End Property

    Dim fCODCENATE As Integer
    Public Property CODCENATE() As String
        Get
            Return fCODCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCENATE", fCODCENATE, value)
        End Set
    End Property

    Dim fContractId As Integer
    Public Property ContractId() As Integer
        Get
            Return fContractId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractId", fContractId, value)
        End Set
    End Property

    Dim fITIPORIES As Integer
    Public Property ITIPORIES() As Integer
        Get
            Return fITIPORIES
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ITIPORIES", fITIPORIES, value)
        End Set
    End Property

    Dim fICAUSAING As Integer
    Public Property ICAUSAING() As Integer
        Get
            Return fICAUSAING
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ICAUSAING", fICAUSAING, value)
        End Set
    End Property

    Dim fIDADPOBESPE As Integer

    Private _checkValue As Boolean
    <NonPersistent>
    Public Property CheckValue() As Boolean
        Get
            Return _checkValue
        End Get
        Set(ByVal value As Boolean)
            _checkValue = value
        End Set
    End Property

    Dim fMainDiagnosticCode As String
    Public Property MainDiagnosticCode() As String
        Get
            Return fMainDiagnosticCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainDiagnosticCode", fMainDiagnosticCode, value)
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

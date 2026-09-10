Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("MedicalFees.MedicalFeesCausation")> _
Public Class MedicalFeesCausationReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
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
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property
    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fMedicalFeesContractId As Integer
    Public Property MedicalFeesContractId() As Integer
        Get
            Return fMedicalFeesContractId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MedicalFeesContractId", fMedicalFeesContractId, value)
        End Set
    End Property
    Dim fCausationDate As DateTime
    Public Property CausationDate() As DateTime
        Get
            Return fCausationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CausationDate", fCausationDate, value)
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
    Dim fServiceOrderId As BillingServiceOrderXpo
    <Association("MedicalFees_MedicalFeesCausationReferencesBilling_ServiceOrder")> _
    Public Property ServiceOrderId() As BillingServiceOrderXpo
        Get
            Return fServiceOrderId
        End Get
        Set(ByVal value As BillingServiceOrderXpo)
            SetPropertyValue(Of BillingServiceOrderXpo)("ServiceOrderId", fServiceOrderId, value)
        End Set
    End Property
    Dim fServiceOrderDetailId As BillingServiceOrderDetailXpo
    <Association("MedicalFees_MedicalFeesCausationReferencesBilling_ServiceOrderDetail")> _
    Public Property ServiceOrderDetailId() As BillingServiceOrderDetailXpo
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As BillingServiceOrderDetailXpo)
            SetPropertyValue(Of BillingServiceOrderDetailXpo)("ServiceOrderDetailId", fServiceOrderDetailId, value)
        End Set
    End Property
    Dim fServiceOrderDetailSurgicalId As Integer
    Public Property ServiceOrderDetailSurgicalId() As Integer
        Get
            Return fServiceOrderDetailSurgicalId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderDetailSurgicalId", fServiceOrderDetailSurgicalId, value)
        End Set
    End Property
    Dim fAmountPayable As Decimal
    Public Property AmountPayable() As Decimal
        Get
            Return fAmountPayable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountPayable", fAmountPayable, value)
        End Set
    End Property
    Dim fMedicalFeesContractValue As Decimal
    Public Property MedicalFeesContractValue() As Decimal
        Get
            Return fMedicalFeesContractValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MedicalFeesContractValue", fMedicalFeesContractValue, value)
        End Set
    End Property
    Dim fInvoiceQuantity As Integer
    Public Property InvoiceQuantity() As Integer
        Get
            Return fInvoiceQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceQuantity", fInvoiceQuantity, value)
        End Set
    End Property
    Dim fTotalAmountPayable As Decimal
    Public Property TotalAmountPayable() As Decimal
        Get
            Return fTotalAmountPayable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAmountPayable", fTotalAmountPayable, value)
        End Set
    End Property
    Dim fMedicalFeePaid As Boolean
    Public Property MedicalFeePaid() As Boolean
        Get
            Return fMedicalFeePaid
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("MedicalFeePaid", fMedicalFeePaid, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fInvoiceReversal As Boolean
    Public Property InvoiceReversal() As Boolean
        Get
            Return fInvoiceReversal
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InvoiceReversal", fInvoiceReversal, value)
        End Set
    End Property
    Dim fObjectionAccepted As Boolean
    Public Property ObjectionAccepted() As Boolean
        Get
            Return fObjectionAccepted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ObjectionAccepted", fObjectionAccepted, value)
        End Set
    End Property
    Dim fValueObjectionAccepted As Decimal
    Public Property ValueObjectionAccepted() As Decimal
        Get
            Return fValueObjectionAccepted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueObjectionAccepted", fValueObjectionAccepted, value)
        End Set
    End Property
    Dim fReassessmentForReversal As Boolean
    Public Property ReassessmentForReversal() As Boolean
        Get
            Return fReassessmentForReversal
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ReassessmentForReversal", fReassessmentForReversal, value)
        End Set
    End Property
    Dim fReassessmentForObjection As Boolean
    Public Property ReassessmentForObjection() As Boolean
        Get
            Return fReassessmentForObjection
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ReassessmentForObjection", fReassessmentForObjection, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
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
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fLiquidationUser As String
    <Size(20)> _
    Public Property LiquidationUser() As String
        Get
            Return fLiquidationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LiquidationUser", fLiquidationUser, value)
        End Set
    End Property
    Dim fLiquidationDate As String
    <Size(20)> _
    Public Property LiquidationDate() As String
        Get
            Return fLiquidationDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LiquidationDate", fLiquidationDate, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
    <Association("MedicalFees_MedicalFeesLiquidationDetailReferencesMedicalFees_MedicalFeesCausation", GetType(MedicalFeesLiquidationDetailReportXpo))> _
    Public ReadOnly Property MedicalFees_MedicalFeesLiquidationDetails() As XPCollection(Of MedicalFeesLiquidationDetailReportXpo)
        Get
            Return GetCollection(Of MedicalFeesLiquidationDetailReportXpo)("MedicalFees_MedicalFeesLiquidationDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

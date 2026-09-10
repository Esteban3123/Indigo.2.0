Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ServiceOrder")> _
Public Class BillingServiceOrderXpo
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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
    Dim fOrderDate As DateTime
    Public Property OrderDate() As DateTime
        Get
            Return fOrderDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("OrderDate", fOrderDate, value)
        End Set
    End Property
    Dim fEntityCode As String
    <Size(20)> _
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
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
    Dim fEntityName As String
    <Size(250)> _
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property
    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
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
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fInvoicedUser As String
    <Size(20)> _
    Public Property InvoicedUser() As String
        Get
            Return fInvoicedUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoicedUser", fInvoicedUser, value)
        End Set
    End Property
    Dim fInvoicedDate As DateTime
    Public Property InvoicedDate() As DateTime
        Get
            Return fInvoicedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoicedDate", fInvoicedDate, value)
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
    <Association("Billing_ServiceOrderDetailReferencesBilling_ServiceOrder", GetType(BillingServiceOrderDetailXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetails() As XPCollection(Of BillingServiceOrderDetailXpo)
        Get
            Return GetCollection(Of BillingServiceOrderDetailXpo)("Billing_ServiceOrderDetails")
        End Get
    End Property
    <Association("MedicalFees_MedicalFeesCausationReferencesBilling_ServiceOrder", GetType(MedicalFeesCausationReportXpo))> _
    Public ReadOnly Property MedicalFees_MedicalFeesCausations() As XPCollection(Of MedicalFeesCausationReportXpo)
        Get
            Return GetCollection(Of MedicalFeesCausationReportXpo)("MedicalFees_MedicalFeesCausations")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

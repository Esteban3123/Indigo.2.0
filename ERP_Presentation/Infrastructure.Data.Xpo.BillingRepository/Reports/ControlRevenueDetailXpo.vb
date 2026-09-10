Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.RevenueControlDetail")> _
Public Class ControlRevenueDetailXpo
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
    Dim fRevenueControlId As ControlRevenueXpo
    <Association("Billing_RevenueControlDetailReferencesBilling_RevenueControl")> _
    Public Property RevenueControlId() As ControlRevenueXpo
        Get
            Return fRevenueControlId
        End Get
        Set(ByVal value As ControlRevenueXpo)
            SetPropertyValue(Of ControlRevenueXpo)("RevenueControlId", fRevenueControlId, value)
        End Set
    End Property
    Dim fBillingAuthorizationId As Integer
    Public Property BillingAuthorizationId() As Integer
        Get
            Return fBillingAuthorizationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BillingAuthorizationId", fBillingAuthorizationId, value)
        End Set
    End Property
    Dim fFolioOrder As Byte
    Public Property FolioOrder() As Byte
        Get
            Return fFolioOrder
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FolioOrder", fFolioOrder, value)
        End Set
    End Property
    Dim fFolioType As Byte
    Public Property FolioType() As Byte
        Get
            Return fFolioType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FolioType", fFolioType, value)
        End Set
    End Property
    Dim fContractEntityId As Integer
    Public Property ContractEntityId() As Integer
        Get
            Return fContractEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractEntityId", fContractEntityId, value)
        End Set
    End Property
    Dim fHealthAdministratorId As Integer
    Public Property HealthAdministratorId() As Integer
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthAdministratorId", fHealthAdministratorId, value)
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
    Dim fCareGroupId As Integer
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property
    Dim fTotalFolio As Decimal
    Public Property TotalFolio() As Decimal
        Get
            Return fTotalFolio
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalFolio", fTotalFolio, value)
        End Set
    End Property
    Dim fResponsibleRecoveryFee As Byte
    Public Property ResponsibleRecoveryFee() As Byte
        Get
            Return fResponsibleRecoveryFee
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ResponsibleRecoveryFee", fResponsibleRecoveryFee, value)
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
    Dim fPatientDiscount As Decimal
    Public Property PatientDiscount() As Decimal
        Get
            Return fPatientDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientDiscount", fPatientDiscount, value)
        End Set
    End Property
    Dim fPatientDiscountPercentage As Decimal
    Public Property PatientDiscountPercentage() As Decimal
        Get
            Return fPatientDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientDiscountPercentage", fPatientDiscountPercentage, value)
        End Set
    End Property
    Dim fTotalPatientWithDiscount As Decimal
    Public Property TotalPatientWithDiscount() As Decimal
        Get
            Return fTotalPatientWithDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPatientWithDiscount", fTotalPatientWithDiscount, value)
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
    <Association("Billing_ServiceOrderDetailDistributionReferencesBilling_RevenueControlDetail", GetType(OrderServiceDetailDistributionXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetailDistributions() As XPCollection(Of OrderServiceDetailDistributionXpo)
        Get
            Return GetCollection(Of OrderServiceDetailDistributionXpo)("Billing_ServiceOrderDetailDistributions")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

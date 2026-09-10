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

Public Structure QxKey

    <Persistent("ServiceOrderDetailId")> _
    Public Property ServiceOrderDetailId As Integer

    <Persistent("ServiceOrderDetailSurgicalId")> _
    Public Property ServiceOrderDetailSurgicalId As Integer

    <Persistent("MedicalFeesCausationId")> _
    Public Property MedicalFeesCausationId As Integer

End Structure

#End Region

''' <summary>
''' asociacion entre procedureCups y MarketingUnitCups usado en los servicios Xpo
''' </summary>
<Persistent("Billing.ViewListSurgicalAndPackage")>
Public Class ViewListSurgicalAndPackageXpo
    Inherits XPLiteObject

#Region "Members"

    <Key(), Persistent()>
    Public Property Key As QxKey

    Dim fServiceOrderDetailSurgicalId As Integer
    Public Property ServiceOrderDetailSurgicalId() As Integer
        Get
            Return fServiceOrderDetailSurgicalId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderDetailSurgicalId", fServiceOrderDetailSurgicalId, value)
        End Set
    End Property

    Dim fServiceOrderDetailId As Integer
    Public Property ServiceOrderDetailId() As Integer
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderDetailId", fServiceOrderDetailId, value)
        End Set
    End Property

    Dim fRateManualSalePrice As Decimal
    Public Property RateManualSalePrice() As Decimal
        Get
            Return fRateManualSalePrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateManualSalePrice", fRateManualSalePrice, value)
        End Set
    End Property

    Dim fInvoicedQuantity As Integer
    Public Property InvoicedQuantity() As Integer
        Get
            Return fInvoicedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoicedQuantity", fInvoicedQuantity, value)
        End Set
    End Property

    Dim fIPSServiceId As Integer
    Public Property IPSServiceId() As Integer
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPSServiceId", fIPSServiceId, value)
        End Set
    End Property

    Dim fIPSServiceSODId As Integer
    Public Property IPSServiceSODId() As Integer
        Get
            Return fIPSServiceSODId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPSServiceSODId", fIPSServiceSODId, value)
        End Set
    End Property

    Dim fIPSServiceDescription As String
    <Size(20)>
    Public Property IPSServiceDescription() As String
        Get
            Return fIPSServiceDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceDescription", fIPSServiceDescription, value)
        End Set
    End Property

    Dim fIPSServiceDescriptionSOD As String
    <Size(20)>
    Public Property IPSServiceDescriptionSOD() As String
        Get
            Return fIPSServiceDescriptionSOD
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceDescriptionSOD", fIPSServiceDescriptionSOD, value)
        End Set
    End Property

    Dim fThirdPartyDescription As String
    <Size(100)>
    Public Property ThirdPartyDescription() As String
        Get
            Return fThirdPartyDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyDescription", fThirdPartyDescription, value)
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

    Dim fCupsEntityId As Integer
    Public Property CupsEntityId() As Integer
        Get
            Return fCupsEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CupsEntityId", fCupsEntityId, value)
        End Set
    End Property

    Dim fCupsEntityDescription As String
    <Size(20)>
    Public Property CupsEntityDescription() As String
        Get
            Return fCupsEntityDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsEntityDescription", fCupsEntityDescription, value)
        End Set
    End Property

    Dim fBillingGroupId As Integer
    Public Property BillingGroupId() As Integer
        Get
            Return fBillingGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BillingGroupId", fBillingGroupId, value)
        End Set
    End Property

    Dim fBillingGroupDescription As String
    <Size(100)>
    Public Property BillingGroupDescription() As String
        Get
            Return fBillingGroupDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillingGroupDescription", fBillingGroupDescription, value)
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

    Dim fStatusMedicalFeesCausation As Byte
    Public Property StatusMedicalFeesCausation() As Byte
        Get
            Return fStatusMedicalFeesCausation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatusMedicalFeesCausation", fStatusMedicalFeesCausation, value)
        End Set
    End Property

    Dim fSelectOption As Boolean
    Public Property SelectOption() As Boolean
        Get
            Return fSelectOption
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SelectOption", fSelectOption, value)
        End Set
    End Property

    Dim fOnlyMedicalFees As Boolean
    Public Property OnlyMedicalFees() As Boolean
        Get
            Return fOnlyMedicalFees
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OnlyMedicalFees", fOnlyMedicalFees, value)
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

    Dim fRateManualId As Integer
    Public Property RateManualId() As Integer
        Get
            Return fRateManualId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RateManualId", fRateManualId, value)
        End Set
    End Property

    Dim fBillingConceptId As Integer
    Public Property BillingConceptId() As Integer
        Get
            Return fBillingConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BillingConceptId", fBillingConceptId, value)
        End Set
    End Property

    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fRateManualDetailSurgicalId As Integer
    Public Property RateManualDetailSurgicalId() As Integer
        Get
            Return fRateManualDetailSurgicalId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RateManualDetailSurgicalId", fRateManualDetailSurgicalId, value)
        End Set
    End Property

    Dim fGrandTotalSalesPrice As Decimal
    Public Property GrandTotalSalesPrice() As Decimal
        Get
            Return fGrandTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrandTotalSalesPrice", fGrandTotalSalesPrice, value)
        End Set
    End Property

    Dim fTotalSalesPrice As Decimal
    Public Property TotalSalesPrice() As Decimal
        Get
            Return fTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPrice", fTotalSalesPrice, value)
        End Set
    End Property

    Dim fSubTotalSalesPrice As Decimal
    Public Property SubTotalSalesPrice() As Decimal
        Get
            Return fSubTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalSalesPrice", fSubTotalSalesPrice, value)
        End Set
    End Property

    Dim fThirdPartyDiscountPercentage As Decimal
    Public Property ThirdPartyDiscountPercentage() As Decimal
        Get
            Return fThirdPartyDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscountPercentage", fThirdPartyDiscountPercentage, value)
        End Set
    End Property

    Dim fCostValue As Decimal
    Public Property CostValue() As Decimal
        Get
            Return fCostValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CostValue", fCostValue, value)
        End Set
    End Property

    Dim fPresentation As Integer
    Public Property Presentation() As Integer
        Get
            Return fPresentation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Presentation", fPresentation, value)
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

    Dim fTotalAmountPayable As Decimal
    Public Property TotalAmountPayable() As Decimal
        Get
            Return fTotalAmountPayable
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAmountPayable", fTotalAmountPayable, value)
        End Set
    End Property

    Dim fTotalAmountPayableReal As Decimal
    Public Property TotalAmountPayableReal() As Decimal
        Get
            Return fTotalAmountPayableReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAmountPayableReal", fTotalAmountPayableReal, value)
        End Set
    End Property

    Dim fPercentageCashed As Decimal
    Public Property PercentageCashed() As Decimal
        Get
            Return fPercentageCashed
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageCashed", fPercentageCashed, value)
        End Set
    End Property

    Dim fPerformsHealthProfessionalCode As String
    <Size(100)>
    Public Property PerformsHealthProfessionalCode() As String
        Get
            Return fPerformsHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsHealthProfessionalCode", fPerformsHealthProfessionalCode, value)
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

    Dim fServiceOrderId As Integer
    Public Property ServiceOrderId() As Integer
        Get
            Return fServiceOrderId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderId", fServiceOrderId, value)
        End Set
    End Property

    Dim fMedicalFeesCausationId As Integer
    Public Property MedicalFeesCausationId() As Integer
        Get
            Return fMedicalFeesCausationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MedicalFeesCausationId", fMedicalFeesCausationId, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(100)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fModificationUser As String
    <Size(100)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fConfirmationUser As String
    <Size(100)>
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property

    Dim fServiceDate As DateTime
    Public Property ServiceDate() As DateTime
        Get
            Return fServiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServiceDate", fServiceDate, value)
        End Set
    End Property

    Dim fLiquidationType As Integer
    Public Property LiquidationType() As Integer
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LiquidationType", fLiquidationType, value)
        End Set
    End Property

    Dim fPerformsProfessionalSpecialty As String
    Public Property PerformsProfessionalSpecialty() As String
        Get
            Return fPerformsProfessionalSpecialty
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("PerformsProfessionalSpecialty", fPerformsProfessionalSpecialty, value)
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

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
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

    Dim fMedicalFeesContractCodeName As String
    Public Property MedicalFeesContractCodeName() As String
        Get
            Return fMedicalFeesContractCodeName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("MedicalFeesContractCodeName", fMedicalFeesContractCodeName, value)
        End Set
    End Property

    Dim fIncomeMainAccountId As Integer
    Public Property IncomeMainAccountId() As Integer
        Get
            Return fIncomeMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncomeMainAccountId", fIncomeMainAccountId, value)
        End Set
    End Property

    Dim fFunctionalUnitDescription As String
    <Size(100)>
    Public Property FunctionalUnitDescription() As String
        Get
            Return fFunctionalUnitDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitDescription", fFunctionalUnitDescription, value)
        End Set
    End Property

    Dim fCreationUserServiceOrder As String
    <Size(100)>
    Public Property CreationUserServiceOrder() As String
        Get
            Return fCreationUserServiceOrder
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUserServiceOrder", fCreationUserServiceOrder, value)
        End Set
    End Property

    Dim fRateManualType As Integer
    Public Property RateManualType() As Integer
        Get
            Return fRateManualType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RateManualType", fRateManualType, value)
        End Set
    End Property

    Dim fColor As Integer
    Public Property Color As Integer
        Get
            Return fColor
        End Get
        Set(value As Integer)
            fColor = value
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

    Dim fIncludeServiceOrderDetailId As Integer
    Public Property IncludeServiceOrderDetailId() As Integer
        Get
            Return fIncludeServiceOrderDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncludeServiceOrderDetailId", fIncludeServiceOrderDetailId, value)
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

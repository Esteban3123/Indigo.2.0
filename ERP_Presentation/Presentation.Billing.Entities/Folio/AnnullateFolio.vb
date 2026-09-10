Imports Presentation.Billing.Entities

Public Class AnnullateFolio
    Implements IFolio

    Public Property InvoiceId As Integer? Implements IFolio.InvoiceId

    Public Property Observation As String Implements IFolio.Observation

    Public Property InvoiceCategoryId As Integer? Implements IFolio.InvoiceCategoryId

    Public Property InvoiceDate As Date Implements IFolio.InvoiceDate

    Public Property InvoicedUser As String Implements IFolio.InvoicedUser

    Public Property InvoiceCategoryCodeName As String Implements IFolio.InvoiceCategoryCodeName

    Public Property AdmissionNumber As String Implements IFolio.AdmissionNumber

    Public Property InvoiceNumber As String Implements IFolio.InvoiceNumber

    Public Property FolioType As Byte Implements IFolio.FolioType

    Public Property HealthAdministratorId As Integer? Implements IFolio.HealthAdministratorId

    Public Property HealthAdministratorCodeName As String Implements IFolio.HealthAdministratorCodeName

    Public Property ThirdPartyId As Int32 Implements IFolio.ThirdPartyId

    Public Property ThirdPartyNitName As String Implements IFolio.ThirdPartyNitName

    Public Property CareGroupId As Int32 Implements IFolio.CareGroupId

    Public Property CaregroupEntityType As Byte Implements IFolio.CaregroupEntityType

    Public Property CareGroupCodeName As String Implements IFolio.CareGroupCodeName

    Public Property TotalFolio As String Implements IFolio.TotalFolio

    Public Property ResponsibleRecoveryFee As Byte Implements IFolio.ResponsibleRecoveryFee

    Public Property PatientDiscountPercentage As Decimal Implements IFolio.PatientDiscountPercentage

    Public Property PatientDiscount As Decimal Implements IFolio.PatientDiscount

    Public Property TotalPatientSalesPrice As Decimal Implements IFolio.TotalPatientSalesPrice

    Public Property TotalPatientWithDiscount As Decimal Implements IFolio.TotalPatientWithDiscount

    Public Property VoucherValue As Decimal Implements IFolio.VoucherValue

    Public Property Status As Byte Implements IFolio.Status

    Public Property ThirdPartyPatientId As Integer Implements IFolio.ThirdPartyPatientId

    Public Property FolioDetails As List(Of FolioDetail)

    Public Property RevenueControlDetailId As Integer Implements IFolio.RevenueControlDetailId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property BillingAuthorizationId As Integer? Implements IFolio.BillingAuthorizationId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CareGroupCostCenterId As Integer Implements IFolio.CareGroupCostCenterId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property StatusFolioId As Integer? Implements IFolio.StatusFolioId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property LiquidationType As Byte Implements IFolio.LiquidationType
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property FolioOrder As Byte Implements IFolio.FolioOrder
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ContractId As Integer? Implements IFolio.ContractId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ContractEntityCodeName As String Implements IFolio.ContractEntityCodeName
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property PatientCode As String Implements IFolio.PatientCode
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ContractEntityId As Integer? Implements IFolio.ContractEntityId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ContractCodeName As String Implements IFolio.ContractCodeName
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AdmissionNumberPatient As String Implements IFolio.AdmissionNumberPatient
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ThirdPartyHealthAdministrator As Integer? Implements IFolio.ThirdPartyHealthAdministrator
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property StatusFolioName As String Implements IFolio.StatusFolioName
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Details As IEnumerable(Of IFolioDetail) Implements IFolio.Details
        Get
            Return Me.FolioDetails
        End Get
        Set(value As IEnumerable(Of IFolioDetail))
            Me.FolioDetails = value
        End Set
    End Property

    Public Property ThirdPartyResponsibleQuotaNitName As String Implements IFolio.ThirdPartyResponsibleQuotaNitName
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property PatientQuotaResponsibleThirdPartyId As Integer? Implements IFolio.PatientQuotaResponsibleThirdPartyId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CurrencyAbbreviation As String Implements IFolio.CurrencyAbbreviation
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property IsMasterAccount As Byte Implements IFolio.IsMasterAccount
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property FeeNotCollectedId As Integer? Implements IFolio.FeeNotCollectedId
        Get
            Return Nothing
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property BasicbillingCopayId As Integer? Implements IFolio.BasicbillingCopayId
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ApplyLogicThirdPartyBeneficiary As Boolean Implements IFolio.ApplyLogicThirdPartyBeneficiary
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub New()
        Me.FolioDetails = New List(Of FolioDetail)()
    End Sub
End Class

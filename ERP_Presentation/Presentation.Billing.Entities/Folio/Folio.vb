Imports Presentation.Billing.Entities

Public Class Folio
    Implements IFolio

    Public Sub New()
        Me.FolioDetails = New List(Of FolioDetail)()
    End Sub

    Public Property RevenueControlDetailId As Integer Implements IFolio.RevenueControlDetailId

    Public Property AdmissionNumber As String Implements IFolio.AdmissionNumber

    Public Property BillingAuthorizationId As Integer? Implements IFolio.BillingAuthorizationId

    Public Property ThirdPartyId As Integer Implements IFolio.ThirdPartyId

    Public Property ThirdPartyNitName As String Implements IFolio.ThirdPartyNitName

    Public Property CareGroupId As Integer Implements IFolio.CareGroupId

    Public Property CaregroupEntityType As Byte Implements IFolio.CaregroupEntityType

    Public Property CareGroupCodeName As String Implements IFolio.CareGroupCodeName

    Public Property CareGroupCostCenterId As Integer Implements IFolio.CareGroupCostCenterId

    Public Property FolioType As Byte Implements IFolio.FolioType

    Public Property Observation As String Implements IFolio.Observation

    Public Property TotalFolio As String Implements IFolio.TotalFolio

    Public Property ResponsibleRecoveryFee As Byte Implements IFolio.ResponsibleRecoveryFee

    Public Property PatientDiscountPercentage As Decimal Implements IFolio.PatientDiscountPercentage

    Public Property PatientDiscount As Decimal Implements IFolio.PatientDiscount

    Public Property TotalPatientSalesPrice As Decimal Implements IFolio.TotalPatientSalesPrice

    Public Property TotalPatientWithDiscount As Decimal Implements IFolio.TotalPatientWithDiscount

    Public Property VoucherValue As Decimal Implements IFolio.VoucherValue

    Public Property Status As Byte Implements IFolio.Status

    Public Property StatusFolioId As Integer? Implements IFolio.StatusFolioId

    Public Property LiquidationType As Byte Implements IFolio.LiquidationType

    Public Property FolioOrder As Byte Implements IFolio.FolioOrder

    Public Property ContractId As Integer? Implements IFolio.ContractId

    Public Property ContractEntityCodeName As String Implements IFolio.ContractEntityCodeName

    Public Property InvoiceCategoryId As Integer? Implements IFolio.InvoiceCategoryId

    Public Property PatientCode As String Implements IFolio.PatientCode

    Public Property ContractEntityId As Integer? Implements IFolio.ContractEntityId

    Public Property ContractCodeName As String Implements IFolio.ContractCodeName

    Public Property InvoiceCategoryCodeName As String Implements IFolio.InvoiceCategoryCodeName

    Public Property AdmissionNumberPatient As String Implements IFolio.AdmissionNumberPatient

    Public Property ThirdPartyPatientId As Integer Implements IFolio.ThirdPartyPatientId

    Public Property HealthAdministratorId As Integer? Implements IFolio.HealthAdministratorId

    Public Property HealthAdministratorCodeName As String Implements IFolio.HealthAdministratorCodeName

    Public Property ThirdPartyHealthAdministrator As Integer? Implements IFolio.ThirdPartyHealthAdministrator

    Public Property InvoiceId As Integer? Implements IFolio.InvoiceId

    Public Property InvoiceNumber As String Implements IFolio.InvoiceNumber

    Public Property InvoiceDate As DateTime Implements IFolio.InvoiceDate

    Public Property InvoicedUser As String Implements IFolio.InvoicedUser

    Public Property StatusFolioName As String Implements IFolio.StatusFolioName

    Public Property IsMasterAccount As Byte Implements IFolio.IsMasterAccount

    Public Property ThirdPartyResponsibleQuotaNitName As String Implements IFolio.ThirdPartyResponsibleQuotaNitName

    Public Property PatientQuotaResponsibleThirdPartyId As Integer? Implements IFolio.PatientQuotaResponsibleThirdPartyId

    Public Property FolioDetails As List(Of FolioDetail)

    Public Property CurrencyAbbreviation As String Implements IFolio.CurrencyAbbreviation


    Public Property Details As IEnumerable(Of IFolioDetail) Implements IFolio.Details
        Get
            Return FolioDetails
        End Get
        Set(value As IEnumerable(Of IFolioDetail))
            Throw New NotImplementedException()
        End Set
    End Property

    Public ReadOnly Property FeeNotCollectedId As Integer? Implements IFolio.FeeNotCollectedId
        Get
            Return Nothing
        End Get
    End Property

    Public Property BasicbillingCopayId As Integer? Implements IFolio.BasicbillingCopayId
    Public Property ApplyLogicThirdPartyBeneficiary As Boolean Implements IFolio.ApplyLogicThirdPartyBeneficiary
End Class

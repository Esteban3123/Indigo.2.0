Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.AccountPayableDetailConceptLiquidation")> _
Public Class PaymentsAccountPayableConceptLiquidationXpo
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
    Dim fAccountPayableDetailConceptId As PaymentsAccountPayableDetailConceptXpoP
    <Association("Payments_AccountPayableDetailConceptLiquidationReferencesPayments_AccountPayableDetailConcept")> _
    Public Property AccountPayableDetailConceptId() As PaymentsAccountPayableDetailConceptXpoP
        Get
            Return fAccountPayableDetailConceptId
        End Get
        Set(ByVal value As PaymentsAccountPayableDetailConceptXpoP)
            SetPropertyValue(Of PaymentsAccountPayableDetailConceptXpoP)("AccountPayableDetailConceptId", fAccountPayableDetailConceptId, value)
        End Set
    End Property
    'Ingresos
    Dim fTotalIncome As Decimal
    Public Property TotalIncome() As Decimal
        Get
            Return fTotalIncome
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalIncome", fTotalIncome, value)
        End Set
    End Property

    'Ingresos no constitutivos de renta ni ganancia ocasional
    Dim fPaymentCompulsoryHealth As Decimal
    Public Property PaymentCompulsoryHealth() As Decimal
        Get
            Return fPaymentCompulsoryHealth
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentCompulsoryHealth", fPaymentCompulsoryHealth, value)
        End Set
    End Property
    Dim fPaymentCompulsoryHealthReal As Decimal
    Public Property PaymentCompulsoryHealthReal() As Decimal
        Get
            Return fPaymentCompulsoryHealthReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentCompulsoryHealthReal", fPaymentCompulsoryHealthReal, value)
        End Set
    End Property
    Dim fPensionFundContribution As Decimal
    Public Property PensionFundContribution() As Decimal
        Get
            Return fPensionFundContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionFundContribution", fPensionFundContribution, value)
        End Set
    End Property
    Dim fPensionFundContributionReal As Decimal
    Public Property PensionFundContributionReal() As Decimal
        Get
            Return fPensionFundContributionReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionFundContributionReal", fPensionFundContributionReal, value)
        End Set
    End Property
    Dim fSolidarityPensionFund As Decimal
    Public Property SolidarityPensionFund() As Decimal
        Get
            Return fSolidarityPensionFund
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SolidarityPensionFund", fSolidarityPensionFund, value)
        End Set
    End Property
    Dim fSolidarityPensionFundReal As Decimal
    Public Property SolidarityPensionFundReal() As Decimal
        Get
            Return fSolidarityPensionFundReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SolidarityPensionFundReal", fSolidarityPensionFundReal, value)
        End Set
    End Property
    Dim fPensionByIndividualSavingsRegime As Decimal
    Public Property PensionByIndividualSavingsRegime() As Decimal
        Get
            Return fPensionByIndividualSavingsRegime
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionByIndividualSavingsRegime", fPensionByIndividualSavingsRegime, value)
        End Set
    End Property
    Dim fPensionByIndividualSavingsRegimeReal As Decimal
    Public Property PensionByIndividualSavingsRegimeReal() As Decimal
        Get
            Return fPensionByIndividualSavingsRegimeReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionByIndividualSavingsRegimeReal", fPensionByIndividualSavingsRegimeReal, value)
        End Set
    End Property

    'Deducciones
    Dim fHousingLoanInterest As Decimal
    Public Property HousingLoanInterest() As Decimal
        Get
            Return fHousingLoanInterest
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HousingLoanInterest", fHousingLoanInterest, value)
        End Set
    End Property
    Dim fHousingLoanInterestReal As Decimal
    Public Property HousingLoanInterestReal() As Decimal
        Get
            Return fHousingLoanInterestReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HousingLoanInterestReal", fHousingLoanInterestReal, value)
        End Set
    End Property
    Dim fPaymentForDependent As Decimal
    Public Property PaymentForDependent() As Decimal
        Get
            Return fPaymentForDependent
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentForDependent", fPaymentForDependent, value)
        End Set
    End Property
    Dim fPaymentForDependentReal As Decimal
    Public Property PaymentForDependentReal() As Decimal
        Get
            Return fPaymentForDependentReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentForDependentReal", fPaymentForDependentReal, value)
        End Set
    End Property
    Dim fPaymentPrepaidMedical As Decimal
    Public Property PaymentPrepaidMedical() As Decimal
        Get
            Return fPaymentPrepaidMedical
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentPrepaidMedical", fPaymentPrepaidMedical, value)
        End Set
    End Property
    Dim fPaymentPrepaidMedicalReal As Decimal
    Public Property PaymentPrepaidMedicalReal() As Decimal
        Get
            Return fPaymentPrepaidMedicalReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentPrepaidMedicalReal", fPaymentPrepaidMedicalReal, value)
        End Set
    End Property
    Dim fOccupationalRiskContribution As Decimal
    Public Property OccupationalRiskContribution() As Decimal
        Get
            Return fOccupationalRiskContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("OccupationalRiskContribution", fOccupationalRiskContribution, value)
        End Set
    End Property
    Dim fOccupationalRiskContributionReal As Decimal
    Public Property OccupationalRiskContributionReal() As Decimal
        Get
            Return fOccupationalRiskContributionReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("OccupationalRiskContributionReal", fOccupationalRiskContributionReal, value)
        End Set
    End Property

    'Total deducciones
    Dim fTotalDeduction As Decimal
    Public Property TotalDeduction() As Decimal
        Get
            Return fTotalDeduction
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDeduction", fTotalDeduction, value)
        End Set
    End Property
    Dim fTotalDeductionReal As Decimal
    Public Property TotalDeductionReal() As Decimal
        Get
            Return fTotalDeductionReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDeductionReal", fTotalDeductionReal, value)
        End Set
    End Property

    'Rentas Exentas
    Dim fVoluntaryPensionFundContribution As Decimal
    Public Property VoluntaryPensionFundContribution() As Decimal
        Get
            Return fVoluntaryPensionFundContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VoluntaryPensionFundContribution", fVoluntaryPensionFundContribution, value)
        End Set
    End Property
    Dim fVoluntaryPensionFundContributionReal As Decimal
    Public Property VoluntaryPensionFundContributionReal() As Decimal
        Get
            Return fVoluntaryPensionFundContributionReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VoluntaryPensionFundContributionReal", fVoluntaryPensionFundContributionReal, value)
        End Set
    End Property
    Dim fContributionAccountAFC As Decimal
    Public Property ContributionAccountAFC() As Decimal
        Get
            Return fContributionAccountAFC
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ContributionAccountAFC", fContributionAccountAFC, value)
        End Set
    End Property
    Dim fContributionAccountAFCReal As Decimal
    Public Property ContributionAccountAFCReal() As Decimal
        Get
            Return fContributionAccountAFCReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ContributionAccountAFCReal", fContributionAccountAFCReal, value)
        End Set
    End Property

    'Total Rentas exentas
    Dim fTotalIncomeExempt As Decimal
    Public Property TotalIncomeExempt() As Decimal
        Get
            Return fTotalIncomeExempt
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalIncomeExempt", fTotalIncomeExempt, value)
        End Set
    End Property
    Dim fTotalIncomeExemptReal As Decimal
    Public Property TotalIncomeExemptReal() As Decimal
        Get
            Return fTotalIncomeExemptReal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalIncomeExemptReal", fTotalIncomeExemptReal, value)
        End Set
    End Property

    'Renta Exenta del 25%
    Dim fExemptIncome As Decimal
    Public Property ExemptIncome() As Decimal
        Get
            Return fExemptIncome
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExemptIncome", fExemptIncome, value)
        End Set
    End Property

    'SubTotal antes de validar el tope de deducciones y rentas exentas
    Dim fSubTotal As Decimal
    Public Property SubTotal() As Decimal
        Get
            Return fSubTotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotal", fSubTotal, value)
        End Set
    End Property

    'Valor máximo que podrá restarse por rentas exentas y deducciones
    Dim fMaxDeductionsAndRentExents As Decimal
    Public Property MaxDeductionsAndRentExents() As Decimal
        Get
            Return fMaxDeductionsAndRentExents
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaxDeductionsAndRentExents", fMaxDeductionsAndRentExents, value)
        End Set
    End Property

    'Base gravable
    Dim fTaxableBase As Decimal
    Public Property TaxableBase() As Decimal
        Get
            Return fTaxableBase
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxableBase", fTaxableBase, value)
        End Set
    End Property
    Dim fApplyRetention As Decimal
    Public Property ApplyRetention() As Decimal
        Get
            Return fApplyRetention
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ApplyRetention", fApplyRetention, value)
        End Set
    End Property
    Dim fRetentionValue383 As Decimal
    Public Property RetentionValue383() As Decimal
        Get
            Return fRetentionValue383
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionValue383", fRetentionValue383, value)
        End Set
    End Property
    Dim fRetentionValue384 As Decimal
    Public Property RetentionValue384() As Decimal
        Get
            Return fRetentionValue384
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionValue384", fRetentionValue384, value)
        End Set
    End Property

    'Valores necesarios para los calculos
    Dim fUVT As Decimal
    Public Property UVT() As Decimal
        Get
            Return fUVT
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UVT", fUVT, value)
        End Set
    End Property
    Dim fSMLV As Decimal
    Public Property SMLV() As Decimal
        Get
            Return fSMLV
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SMLV", fSMLV, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

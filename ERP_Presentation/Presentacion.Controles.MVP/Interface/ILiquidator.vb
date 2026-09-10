'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface ILiquidator
    Inherits ICrudBase

    Property FeeCommissionServiceData As Decimal

    Property AccumulatedIncome As Decimal

    Property TotalIncomeMonthlyData As Decimal

    Property RequiredContributions As Decimal

    Property VoluntaryContributions As Decimal

    Property PensionByIndividualSavingsRegime As Decimal

    Property SolidarityPension As Decimal

    Property AccountContributions As Decimal

    Property TotalIncomeExent As Decimal

    Property PaymentHealthObligatory As Decimal

    Property PreparedHealth As Decimal

    Property Dependent As Decimal

    Property Interests As Decimal

    Property RiskWork As Decimal

    Property TotalDeductions As Decimal

    Property TotalNoConstitutive As Decimal

    Property RetArt383 As Decimal

    Property PreviousRetArt383 As Decimal

    Property FinalRetention As Decimal
End Interface

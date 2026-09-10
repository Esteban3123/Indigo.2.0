Imports System.Runtime.Serialization
Partial Public Class BillingConcept

    ''' <summary>
    ''' Descripción de la cuenta contable para la cuenta nif de reconocimiento de ingresos pendientes por facturar
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property IncomeRecognitionPendingBillingMainAccountDescription As String

    <DataMember>
    Property CopayMainAccountDescription As String

    <DataMember>
    Property RecoveryFixedAmountMainAccountDescription As String

    ''' <summary>
    ''' Porcentaje de IVA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property PercentageIVA As Decimal

    ''' <summary>
    ''' Id Concepto de Retención en Fuente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionIdTax As Integer

    ''' <summary>
    ''' Porcentaje de Retención en Fuente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionPercentageTax As Decimal

    ''' <summary>
    ''' Base de Retención en Fuente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionBaseTax As Decimal

    ''' <summary>
    ''' Id Concepto de Retención de ICA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionIdICA As Integer

    ''' <summary>
    ''' Porcentaje de Retención de ICA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionPercentageICA As Decimal

    ''' <summary>
    ''' Base de Retención de ICA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionBaseICA As Decimal

    ''' <summary>
    ''' conteo de los servicios secundarios que el concepte contiene
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CountSecondaryService As Integer


End Class

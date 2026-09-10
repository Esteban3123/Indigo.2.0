#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class FixedAssetPhysicalAsset

    <DataMember()>
    Public Property SupplierDescription As String

    <DataMember()>
    Public Property TrademarkDescription As String

    <DataMember()>
    Public Property PolicyDescription As String

    <DataMember()>
    Public Property ItemDescription As String

    <DataMember()>
    Public Property CodeItem As String

    <DataMember()>
    Public Property MainAccountDescription As String

    <DataMember()>
    Public Property LocationDescription As String

    <DataMember()>
    Public Property ResponsibleDescription As String

    <DataMember()>
    Public Property ClassLocation As Integer

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

End Class

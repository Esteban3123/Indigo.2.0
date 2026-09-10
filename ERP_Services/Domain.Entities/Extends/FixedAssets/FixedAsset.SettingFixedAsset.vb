#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class SettingFixedAsset

    ''' <summary>
    ''' Código nombre articulo
    ''' </summary>
    <DataMember()>
    Public Property FreightIVAPercentage As Decimal?

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property ServiceMainAccountNumberName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property IVAFreightAccountPayableConceptCodeName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property FreightAccountPayableConceptCodeName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property IVARetentionAccountPayableConceptCodeName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property IVAAccountPayableConceptCodeName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property TransferJournalVoucherCodeName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property ReclassificationJournalVoucherCodeName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property DevolutionJournalVoucherCodeName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property SalesMainAccountCodeName As String

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    <DataMember()>
    Public Property ReplacementMainAccountCodeName As String

    ''' <summary>
    ''' Nos permite saber si la propiedad de moneda puede editarse siempre y cuando no existan documentos de entrada ni traslados de activos
    ''' </summary>
    <DataMember()>
    Public Property CurrencyFieldEnabled As Boolean

End Class

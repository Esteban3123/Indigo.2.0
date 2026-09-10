#Region "Imports"
Imports System.Runtime.Serialization
#End Region


Partial Public Class SettingFixedAsset

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameAditionAccountingVoucher As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameDepreciationAccountingVoucher As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameIngressAccountingVoucher As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameOutputAccountingVoucher As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre del tipo de comprobante
    ''' </summary>
    <DataMember()>
    Public Property CodeNameIntangibleAssetAmortizationVoucher As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameOtherIngressAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameDonationAccountingAccount As String
    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameTransferPropertyAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameOtherConceptsAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameRecuperationAccountingAccount As String

    <DataMember()>
    Public Property NameResponsible As String

    ''' <summary>
    ''' Obtiene nombre de la moneda y abreviacion
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CurrencyName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de comprobante contable de Valorización/Desvalorización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CodeNameValorizationDevaluationAccountingVoucher As String

End Class

Imports System.Runtime.Serialization

Partial Public Class Warehouse

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property CostCenterDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable debito
    ''' </summary>
    <DataMember()>
    Public Property MainAccountThirdPartyDebitDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable credito
    ''' </summary>
    <DataMember()>
    Public Property MainAccountThirdPartyCreditDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del proveedor
    ''' </summary>
    <DataMember>
    Property CodeNameSupplier As String

    ''' <summary>
    ''' Obtiene o establece centro de atencion
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CenterAttentionDescription As String

#End Region

End Class

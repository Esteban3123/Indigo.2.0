#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class FixedAssetInitialBalanceItem

    ''' <summary>
    ''' Código nombre articulo
    ''' </summary>
    <DataMember()>
    Public Property ItemCodeName As String

    ''' <summary>
    ''' Código nombre localización
    ''' </summary>
    <DataMember()>
    Public Property LocationCodeName As String

    ''' <summary>
    ''' Código nombre responsable
    ''' </summary>
    <DataMember()>
    Public Property ResponsibleCodeName As String

    ''' <summary>
    ''' Código nombre proveedor
    ''' </summary>
    <DataMember()>
    Public Property SupplierCodeName As String

    ''' <summary>
    ''' Código nombre marca
    ''' </summary>
    <DataMember()>
    Public Property TrademarkCodeName As String

    ''' <summary>
    ''' Código nombre poliza
    ''' </summary>
    <DataMember()>
    Public Property PolicyCodeName As String

    ''' <summary>
    ''' Código y nombre del estado del activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property StatusAssetCodeName As String

End Class

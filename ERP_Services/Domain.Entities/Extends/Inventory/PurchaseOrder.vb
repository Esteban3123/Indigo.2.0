Imports System.Runtime.Serialization

Partial Public Class PurchaseOrder

#Region "Properties"

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property DescriptionSupplier As String

    ''' <summary>
    ''' Descripcion del Almacen
    ''' </summary>
    <DataMember()>
    Public Property DescriptionWarehouse As String

    ''' <summary>
    ''' Numero del contrato asociado
    ''' </summary>
    <DataMember()>
    Public Property ContractNumber As String

    ''' <summary>
    ''' Identifica si el contrato asociado maneja productos
    ''' </summary>
    <DataMember()>
    Public Property ContractManageProducts As Boolean

    ''' <summary>
    ''' Prefijo del almacen
    ''' </summary>
    <DataMember()>
    Public Property Prefix As String

    ''' <summary>
    ''' Desconfirma
    ''' </summary>
    <DataMember()>
    Public Property UnConfirm As Boolean

    <DataMember()>
    Public Property remissionCodes As New List(Of String)()

    <DataMember()>
    Public Property entranceVoucherCodes As New List(Of String)()

    ''' <summary>
    ''' Identifica si hay algun producto parcialmente legalalizado
    ''' </summary>
    <DataMember()>
    Public Property partly As Boolean

#End Region

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

#End Region

End Class

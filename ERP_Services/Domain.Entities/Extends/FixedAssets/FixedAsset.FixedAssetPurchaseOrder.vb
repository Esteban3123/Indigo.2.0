Imports System.Runtime.Serialization

Partial Public Class FixedAssetPurchaseOrder

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NameSuplier As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la unidad funcional que solicita
    ''' </summary>
    <DataMember()>
    Public Property RequestedFunctionalUnitName As String

    ''' <summary>
    ''' Indica si la orden de compra ha sido desconfirmada.
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IsDesconfirmed As Boolean

    ''' <summary>
    ''' Identifica si hay algunos de los detalles de la orden estan parcialmente legalizados (cruzados)
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IsPartiallyLegalized As Boolean

    ''' <summary>
    ''' Lista de códigos asociados a las entradas de activos fijos.
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property FixedAssetEntryCodes As New List(Of String)()


    ''' <summary>
    ''' Lista de códigos asociados a las remisiones de entrada de activos fijos.
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property FixedAssetRemissionEntranceCodes As New List(Of String)()


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

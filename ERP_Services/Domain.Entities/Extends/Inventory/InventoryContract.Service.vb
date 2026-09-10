Imports System.Runtime.Serialization

Partial Public Class InventoryContract

#Region "Properties"

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property DescriptionSupplier As String

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property DescriptionContractType As String

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

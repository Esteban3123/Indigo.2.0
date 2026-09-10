Imports System.Runtime.Serialization

Partial Public Class InventoryContractModification

#Region "Properties"

    ''' <summary>
    ''' Numero del contrato asociado
    ''' </summary>
    <DataMember()>
    Public Property ContractNumber As String

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

#End Region

End Class

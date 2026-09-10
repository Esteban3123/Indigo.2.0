Imports System.Runtime.Serialization

Partial Public Class InventoryContractAssignment

#Region "Properties"

    ''' <summary>
    ''' Numero del contrato asociado
    ''' </summary>
    <DataMember()>
    Public Property ContractNumber As String

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property DescriptionSupplierTransferor As String

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property DescriptionSupplierAssignee As String

#End Region

End Class

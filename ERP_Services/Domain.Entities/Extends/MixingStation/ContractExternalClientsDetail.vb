Imports System.Runtime.Serialization

Partial Public Class ContractExternalClientsDetail
    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del componente: medicamento, insumo o producto
    ''' </summary>
    <DataMember()>
    Public Property SourceCodeName As String

    ''' <summary>
    ''' Obtiene o establece el nombre del componente: medicamento, insumo o producto
    ''' </summary>
    <DataMember()>
    Public Property SourceName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de componente
    ''' </summary>
    <DataMember()>
    Public Property TypeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de preparación
    ''' </summary>
    <DataMember()>
    Public Property SuppliedByName As String

End Class

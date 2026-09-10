Imports System.Runtime.Serialization

Partial Public Class RelatedSupplieMedicine
    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de item
    ''' </summary>
    <DataMember()>
    Public Property NameItemType As String

    ''' <summary>
    ''' Obtiene o establece el codigo del insumo o el medicamento
    ''' </summary>
    <DataMember()>
    Public Property SourceCode As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del insumo o el medicamento
    ''' </summary>
    <DataMember()>
    Public Property SourceName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del insumo o el medicamento
    ''' </summary>
    <DataMember()>
    Public Property Agregado As Boolean
End Class

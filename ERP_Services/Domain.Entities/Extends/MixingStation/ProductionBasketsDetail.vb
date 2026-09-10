Imports System.Runtime.Serialization

Partial Public Class ProductionBasketsDetail
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
    ''' Obtiene o establece la descripcion de la unidad de medida
    ''' </summary>
    <DataMember()>
    Public Property MeasureUnitDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de componente
    ''' </summary>
    <DataMember()>
    Public Property ComponentTypeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de preparación
    ''' </summary>
    <DataMember()>
    Public Property PreparationTypeName As String

End Class

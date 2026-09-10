Imports System.Runtime.Serialization

Partial Public Class FilingUnit

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de radicacion
    ''' </summary>
    <DataMember()>
    Public Property FilingUnitDescription As String

    ''' <summary>
    ''' Obtiene o establece si tiene relacion para poder agregar usuarios (True = tiene relacion, False = No tiene relacion)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ContainsRelationship As Boolean

    ''' <summary>
    ''' Obtiene o establece si tiene agregado usuarios el item seleccionado (True = Tiene usuarios agregados, False = No tiene usuarios agregados)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ContainsUsers As Boolean

    ''' <summary>
    ''' propiedad donde se concatena el codigo con el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CodeName As String

    ''' <summary>
    ''' establece si el registro es hijo o de ultimo nivel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()> _
    Public Property IsSon As Boolean

#End Region

End Class

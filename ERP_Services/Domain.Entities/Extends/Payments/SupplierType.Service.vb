Imports System.Runtime.Serialization

Partial Public Class SupplierType

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de radicacion
    ''' </summary>
    <DataMember()>
    Public Property SupplierTypeDescription As String

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

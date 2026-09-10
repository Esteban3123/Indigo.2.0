#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class InventoryRequestDevolutionDetail

    ''' <summary>
    ''' Propiedad que contiene el código de la solicitud que se desea devolver
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Public Property RequestCode As String

    ''' <summary>
    ''' Propiedad que contiene el codigo y el nombre de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Public Property ProductoCodeName As String

    ''' <summary>
    ''' Propiedad que contiene el tipo de solicitud del detalle de la devolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Public Property RequestType As Byte

    ''' <summary>
    ''' Propiedad que contiene la cantidad pendiente de la solicitud de la devolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Public Property OutstandingQuantity As Integer

End Class

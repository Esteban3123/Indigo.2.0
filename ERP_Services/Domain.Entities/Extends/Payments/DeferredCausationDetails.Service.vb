Imports System.Runtime.Serialization

Partial Public Class DeferredCausationDetails

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el modo: editar o agregar
    ''' </summary>
    <DataMember()>
    Public Property Mode As Boolean

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property NumberNameMainAccount As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionCostCenter As String

#End Region

End Class

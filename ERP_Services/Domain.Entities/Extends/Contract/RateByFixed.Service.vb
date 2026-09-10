Imports System.Runtime.Serialization

Partial Public Class RateByFixed

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de liquidacion
    ''' </summary>
    <DataMember()>
    Public Property LiquidationTypeName As String

    ''' <summary>
    ''' Obtiene o establece el id del tipo de liquidacion
    ''' </summary>
    <DataMember()>
    Public Property LiquidationType As Integer

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de manual tarifario
    ''' </summary>
    <DataMember()>
    Public Property ManualTypeName As String

    ''' <summary>
    ''' Obtiene o establece si permite cambiar los valores cuando se esta facturando
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AllowValueChange As Boolean

#End Region

End Class

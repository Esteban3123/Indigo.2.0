Imports System.Runtime.Serialization

Partial Public Class DeferredCausation

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable
    ''' </summary>
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

    ''' <summary>
    ''' id de la moneda de la cxp
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CurrencyId As Integer


    ''' <summary>
    ''' abreviacion de la moneda de la cxp
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CurrencyAbbreviation As String

#End Region

End Class

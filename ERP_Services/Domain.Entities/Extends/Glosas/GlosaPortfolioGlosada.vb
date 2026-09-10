Imports System.Runtime.Serialization

Partial Public Class GlosaPortfolioGlosada

#Region "Manual Properties"

    ''' <summary>
    ''' Seleccionado
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property Selection() As Boolean

    ''' <summary>
    ''' Propiedad para saber si la factura es saldo inicial o no.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property OpeningBalance() As Boolean

#End Region

End Class

Imports System.Runtime.Serialization
Partial Public Class RadicateInvoiceD
   
#Region "Manual Properties"

    Private _selection As Boolean

    <DataMember()>
    Public Property Selection() As Boolean
        Get
            Return _selection
        End Get
        Set(value As Boolean)
            _selection = value
        End Set
    End Property

    <DataMember>
    Property InvoiceValueFacade As Decimal

    <DataMember>
    Property CodeNameInvoiceCategory As String

    <DataMember>
    Property CurrencyAbbreviation As String

    ''' <summary>
    ''' Codigo unico de validacion RIPS electronicos
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CUV As String

#End Region
End Class

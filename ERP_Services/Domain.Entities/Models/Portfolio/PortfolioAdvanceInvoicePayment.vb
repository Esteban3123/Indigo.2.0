
Imports System.Runtime.Serialization
Imports System.Text

<DataContract()>
Public Class PortfolioAdvanceInvoicePayment

    Const PROCESS_NAME As String = "ListPortfolioAdvanceCrossing"

    ''' <summary>
    ''' List Of Advance to cross with a cxc
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property ListPortfolioAdvance As List(Of PortfolioAdvance)
    ''' <summary>
    ''' Total Value to Cross
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property TotalCrossingValue As Decimal
    ''' <summary>
    ''' currency's Id of Entity
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property CurrencyId As Integer
    ''' <summary>
    ''' Id of Entity who run the Crossing, Example: Id of BasicBilling
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property AccountReceivableId As Integer
    ''' <summary>
    ''' Name of entity who run the crossing, Example: BasicBilling
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property EntityName As String

    ''' <summary>
    ''' this function make the xml to send in SP_SavePortfolioTransfer
    ''' </summary>
    ''' <returns></returns>
    Public Function ToXML() As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        For Each item In ListPortfolioAdvance
            builder.Append("<" & PROCESS_NAME & ">")
            builder.Append(String.Format(formatXml, "Id", item.Id))
            builder.Append(String.Format(formatXml, "Code", item.Code))
            builder.Append(String.Format(formatXml, "CrossingValue", item.CrossingValue.ToString().Replace(",", ".")))
            builder.Append(String.Format(formatXml, "EntityName", EntityName))
            builder.Append("</" & PROCESS_NAME & ">")
        Next
        Return builder.ToString()
    End Function
End Class

<DataContract()>
Public Class RunCrossingProcessEventArgs
    Inherits EventArgs
    <DataMember()>
    Property ListPortfolioAdvanceInvoicePaymentAs As PortfolioAdvanceInvoicePayment
End Class
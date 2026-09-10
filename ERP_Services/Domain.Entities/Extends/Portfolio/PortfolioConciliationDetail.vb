#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class PortfolioConciliationDetail

    ''' <summary>
    ''' </summary>
    <DataMember()>
    Public Property DescInvoiceNumber As String

    ''' <summary>
    ''' </summary>
    <DataMember()>
    Public Property DescInvoiceDate As Date?

    ''' <summary>
    ''' </summary>
    <DataMember()>
    Public Property DescRadicatedNumber As String

    ''' <summary>
    ''' </summary>
    <DataMember()>
    Public Property DescRadicatedDate As Date?

    ''' <summary>
    ''' </summary>
    <DataMember()>
    Public Property DescDocumentValue As Decimal?

    ''' <summary>
    ''' </summary>
    <DataMember()>
    Public Property DescValueGlosado As Decimal?

    ''' <summary>
    ''' </summary>
    <DataMember()>
    Public Property DescInvoiceValueEntity As Decimal?


    ''' <summary>
    ''' </summary>
    <DataMember()>
    Public Property DescInvoiceStateEntity As String

End Class
Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Partial Public Class InvoiceEntityCapitated

#Region "Properties"

    <DataMember()>
    Public Property FullNameCareGroup As String

    <DataMember()>
    Public Property CodeNameCategory As String

    <DataMember()>
    Public Property ReversalReasonId As Integer?

    <DataMember()>
    Public Property ReversalDescription As String

    <DataMember()>
    Public Property PreviousInvoiceNumberRips As String

    <DataMember()>
    Public Property SubTotalValue As Decimal

    <DataMember()>
    Public Property ListPortfolioAdvance As List(Of PortfolioAdvance)

#End Region

#Region "Methods"

    Public Function ToXML(session As SessionValues) As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")

        Dim tiposRecaudoProps As String() = {"CopaymentAmount", "ModeratingFeeAmount", "SharedPaymentAmount"}
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) o.PropertyType.Namespace.Equals("System") AndAlso Not tiposRecaudoProps.Contains(o.Name))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(Me).ToString().ConvertToXmlText())))
        Next

        If Me.ListPortfolioAdvance?.Any() Then
            For Each item In Me.ListPortfolioAdvance
                builder.Append("<ListPortfolioAdvanceCrossing>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("<Code>" & item.Code & "</Code>")
                builder.Append("<CrossingValue>" & item.CrossingValue.ToString().Replace(",", ".") & "</CrossingValue>")
                builder.Append("<CashReceiptDetailIdTmp>" & item.CashReceiptDetailIdTmp & "</CashReceiptDetailIdTmp>")
                builder.Append("</ListPortfolioAdvanceCrossing>")

            Next
        End If

        'Tipos de recaudo: Copago, Cuota Moderadora, Pagos Compartidos
        builder.Append(String.Format(formatXml, "CopaymentAmount", Utils.CleanFields(Me.CopaymentAmount.ToString(System.Globalization.CultureInfo.InvariantCulture))))
        builder.Append(String.Format(formatXml, "ModeratingFeeAmount", Utils.CleanFields(Me.ModeratingFeeAmount.ToString(System.Globalization.CultureInfo.InvariantCulture))))
        builder.Append(String.Format(formatXml, "SharedPaymentAmount", Utils.CleanFields(Me.SharedPaymentAmount.ToString(System.Globalization.CultureInfo.InvariantCulture))))

        builder.Append(String.Format(formatXml, "FilePath", System.IO.Path.Combine(Utils.GetPathElectronicDocuments(), session.TransactionalContainer)))
        builder.Append(String.Format(formatXml, "ChangeTracker", Me.ChangeTracker.State.ToString()))
        builder.Append(String.Format(formatXml, "CompanyType", session.AuditMessageWcf.CompanyType))

        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

#End Region

End Class

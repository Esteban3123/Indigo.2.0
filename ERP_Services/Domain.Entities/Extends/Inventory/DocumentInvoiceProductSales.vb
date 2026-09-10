Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class DocumentInvoiceProductSales
    <DataMember>
    Property NitNameThirdParty As String
    <DataMember>
    Property CodeNameWareHouse As String
    <DataMember>
    Property NameBillingAuthorization As String

    <DataMember>
    Property CodeNameFunctionalUnit As String

    ''' <summary>
    ''' Descripción de la sucursal
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameBranchOffice As String

    ''' <summary>
    ''' Codigo y nombre de la condicion de ventas
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameConditionSales As String

    ''' <summary>
    ''' Codigo y nombre de la actividad economica
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameEconomicActivity As String

    ''' <summary>
    ''' Porcentaje de retencion de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property WithholdingIvaPercentage As Decimal

    <DataMember()>
    Public Property RoundService As Integer = 1 'Ajuste al peso por defecto

    Public Sub CalculateTotals(CurrentCompanyContributionType As Byte, SupplierContributionType As Byte, AllowIca As Boolean, AllowIcaTop As Boolean, IcaTopValue As Decimal)
        If DocumentInvoiceProductSalesDetail.Count > 0 Then
            For Each item In DocumentInvoiceProductSalesDetail
                item.SubTotalValue = item.SubTotalValue
                item.DiscountValue = item.DiscountValue
                item.IvaValue = item.IvaValue
                item.TotalValue = item.TotalValue
                item.RTFValue = item.RTFValue
            Next

            Value = DocumentInvoiceProductSalesDetail.Sum(Function(x) x.SubTotalValue)
            ValueTax = DocumentInvoiceProductSalesDetail.Sum(Function(x) x.IvaValue)
            ValueDiscount = DocumentInvoiceProductSalesDetail.Sum(Function(x) x.DiscountValue)
            'RetentionSource = CDec(Utils.RoundValue(DocumentInvoiceProductSalesDetail.Sum(Function(x) x.RTFValue), RoundService))
        Else
            Value = 0
            ValueTax = 0
            ValueDiscount = 0
            'RetentionSource = 0
        End If

        If AllowIca Then
            If AllowIcaTop AndAlso Value <= IcaTopValue Then
                WithholdingICA = 0
            Else
                WithholdingICA = CDec(Utils.RoundValue((Value - ValueDiscount) * (IcaPercentage / 100), RoundService))
            End If
        Else
            WithholdingICA = 0
        End If

        If SupplierContributionType = 0 OrElse CurrentCompanyContributionType <= SupplierContributionType Then
            WithholdingTax = 0
        Else
            WithholdingTax = CDec(Utils.RoundValue((ValueTax + FreightIVAValue) * (WithholdingIvaPercentage / 100), RoundService))
        End If

        'If EntranceVoucherOtherDeduction.Count > 0 Then
        '    For Each i In EntranceVoucherOtherDeduction
        '        If i.Type = 1 Then
        '            i.Value = (Value - ValueDiscount) * (i.WithholdingPercentage / 100)
        '        End If
        '    Next
        '    RetentionOther = EntranceVoucherOtherDeduction.Sum(Function(x) IIf(x.Type = 1, x.Value, 0))
        '    DeductionOther = EntranceVoucherOtherDeduction.Sum(Function(x) IIf(x.Type = 2, x.Value, 0))
        'Else
        'RetentionOther = 0
        'DeductionOther = 0
        'End If

        'FreightValue = FreightValue
        'FreightIVAValue = FreightIVAValue

        'TotalValue = Value + ValueTax - ValueDiscount - WithholdingTax - WithholdingICA - RetentionSource - RetentionOther - DeductionOther - DistrictTax + FreightIVAValue + FreightValue
        TotalValue = Value + ValueTax - ValueDiscount - WithholdingTax - WithholdingICA - RetentionSource - DistrictTax + FreightIVAValue + FreightValue
    End Sub

End Class

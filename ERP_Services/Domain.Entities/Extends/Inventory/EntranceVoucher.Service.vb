Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class EntranceVoucher

#Region "Properties"

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property DescriptionSupplier As String

    ''' <summary>
    ''' Descripcion del almacen
    ''' </summary>
    <DataMember()>
    Public Property DescriptionWarehouse As String

    ''' <summary>
    ''' Descripcion del tipo de proveedor
    ''' </summary>
    <DataMember()>
    Public Property DescriptionSupplierType As String

    ''' <summary>
    ''' Prefijo del almacen
    ''' </summary>
    <DataMember()>
    Public Property Prefix As String

    ''' <summary>
    ''' Porcentaje de retencion de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property WithholdingIvaPercentage As Decimal

    ''' <summary>
    ''' Obtiene o establece la descripcion de la resolucion de documento soporte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionDocumentSupport As String

#End Region

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

#End Region

#Region "Methods"

    Public Sub CalculateTotals(CurrentCompanyContributionType As Byte, SupplierContributionType As Byte, AllowIca As Boolean, AllowIcaTop As Boolean, IcaTopValue As Decimal)
        If EntranceVoucherDetail.Count > 0 Then
            Value = EntranceVoucherDetail.Sum(Function(x) x.SubTotalValue)
            ValueTax = EntranceVoucherDetail.Sum(Function(x) x.IvaValue)
            ValueDiscount = EntranceVoucherDetail.Sum(Function(x) x.DiscountValue)
            RetentionSource = CDec(Utils.RoundValue(EntranceVoucherDetail.Sum(Function(x) x.RTFValue), RoundService))
        Else
            Value = 0
            ValueTax = 0
            ValueDiscount = 0
            RetentionSource = 0
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

        If EntranceVoucherOtherDeduction.Count > 0 Then
            For Each i In EntranceVoucherOtherDeduction
                If i.Type = 1 Then
                    i.Value = (Value - ValueDiscount) * (i.WithholdingPercentage / 100)
                End If
            Next
            RetentionOther = EntranceVoucherOtherDeduction.Sum(Function(x) IIf(x.Type = 1, x.Value, 0))
            DeductionOther = EntranceVoucherOtherDeduction.Sum(Function(x) IIf(x.Type = 2, x.Value, 0))
        Else
            RetentionOther = 0
            DeductionOther = 0
        End If

        TotalValue = Value + ValueTax - ValueDiscount - WithholdingTax - WithholdingICA - RetentionSource - RetentionOther - DeductionOther - DistrictTax + FreightIVAValue + FreightValue
    End Sub

#End Region

End Class

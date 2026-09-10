Imports Infrastructure.CrossCutting.Base

Public Class CtrTotalVoucher

    ''' <summary>
    ''' Delegado de la funcion que retorna el valor del comprobante y el valor de la tasa por mil
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function TotalVoucherDelegate() As Tuple(Of Decimal, Decimal, Decimal)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _functionTotalVoucher As TotalVoucherDelegate

    Private _codeISO4217 As String

#Region "Properties"

    ''' <summary>
    ''' Establece el valor total del comprobante
    ''' </summary>
    ''' <value>
    ''' The _total value.
    ''' </value>
    Private WriteOnly Property TotalValue As Decimal
        Set(value As Decimal)
            INDlblAdvanceValue.Text = Utils.GetMoneyWithISO4217(value, _codeISO4217)
        End Set
    End Property

    ''' <summary>
    ''' Establece el valor total del iva
    ''' </summary>
    ''' <value>
    ''' The _total value.
    ''' </value>
    Private WriteOnly Property ValueIVA As Decimal
        Set(value As Decimal)
            INDlblValueIVA.Text = Utils.GetMoneyWithISO4217(value, _codeISO4217)
        End Set
    End Property

    ''' <summary>
    ''' Establece el valor del comprobante sin tasa por mil
    ''' </summary>
    ''' <value>
    ''' The voucher value.
    ''' </value>
    Private WriteOnly Property VoucherValue As Decimal
        Set(value As Decimal)
            INDlblValueNet.Text = Utils.GetMoneyWithISO4217(value, _codeISO4217)
        End Set
    End Property

    ''' <summary>
    ''' Establece el valor de la tasa por mil
    ''' </summary>
    ''' <value>
    ''' The tax by mil value.
    ''' </value>
    Private WriteOnly Property TaxByMilValue As Decimal
        Set(value As Decimal)
            INDlblXMilValue.Text = Utils.GetMoneyWithISO4217(value, _codeISO4217)
        End Set
    End Property

    ''' <summary>
    ''' codigo ISO4217
    ''' </summary>
    Public WriteOnly Property CodeISO4217 As String
        Set(value As String)
            _codeISO4217 = value
        End Set
    End Property

#End Region

#Region "Functions"

    ''' <summary>
    ''' Asigna el delegado que se da al ejecutar para obtener el valor del comprobante
    ''' </summary>
    Public Sub SetVoucherValueFunction(functionTotalVoucher As TotalVoucherDelegate)
        _functionTotalVoucher = functionTotalVoucher
    End Sub

    ''' <summary>
    ''' Muestra el valor del comprobante de egreso
    ''' </summary>
    Public Sub PrintValueVoucher()
        _codeISO4217 = If(String.IsNullOrEmpty(_codeISO4217), SessionValues.Instance.CurrencyISO4217, _codeISO4217)
        If _functionTotalVoucher IsNot Nothing Then
            Dim value As Tuple(Of Decimal, Decimal, Decimal) = _functionTotalVoucher()
            TotalValue = value.Item1
            VoucherValue = value.Item1 - value.Item2
            TaxByMilValue = value.Item2
            ValueIVA = value.Item3
        End If
    End Sub

#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem2)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblXMilValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblAdvanceValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblValueNet)
    End Sub

End Class

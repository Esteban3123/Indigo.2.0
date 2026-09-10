Imports System.ComponentModel
Imports DevExpress.XtraEditors.Mask
Imports Infrastructure.CrossCutting.Base

Public Class CtrTotalPayrollLiquidation

#Region "Delegates"
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion (Neto, Descuento, Iva, Total Factura)
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of Double, Double, Double)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate

    Private Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Properties"
    Private _currencyAbbreviation As String
    Public Property CurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(_currencyAbbreviation), Indigo?.CurrencyISO4217, _currencyAbbreviation)
        End Get
        Set(value As String)
            _currencyAbbreviation = value
        End Set
    End Property

    WriteOnly Property NetoValue As Double
        Set(value As Double)
            INDPceTotalValue.EditValue = value
            INDPceTotalValue.Text = Utils.GetMoneyWithISO4217(value, CurrencyAbbreviation)
            INDPceTotalValue.ToolTip = "Valor Total a Pagar: " + INDPceTotalValue.Text.Trim()
        End Set
    End Property

    WriteOnly Property DeductedValue As Double
        Set(value As Double)
            INDPceDeductedValue.EditValue = value
            INDPceDeductedValue.Text = Utils.GetMoneyWithISO4217(value, CurrencyAbbreviation)
            INDPceDeductedValue.ToolTip = "Deducido: " + INDPceDeductedValue.Text.Trim()
        End Set
    End Property

    WriteOnly Property AccruedValue As Double
        Set(value As Double)
            INDPceAccruedValue.EditValue = value
            INDPceAccruedValue.Text = Utils.GetMoneyWithISO4217(value, CurrencyAbbreviation)
            INDPceAccruedValue.ToolTip = "Devengado: " + INDPceAccruedValue.Text.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado en el valor
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <BrowsableAttribute(False)>
    Public Property PopupContainerControlTotalValue As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.INDPceTotalValue.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.INDPceTotalValue.Properties.PopupControl = value
        End Set
    End Property

    ''' <summary>
    ''' Cambia el nombre a el label Valor Factura
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property NameValueBill As String
        Get
            Return INDPceInvoiceValueTitle.Text
        End Get
        Set(value As String)
            INDPceInvoiceValueTitle.Text = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Asigna el delegado que se da al ejecutar para obtener el valor del comprobante
    ''' </summary>
    Public Sub SetInfoFunction(setInfoDelegate As SetInfoDelegate)
        _setInfoDelegate = setInfoDelegate
    End Sub

    ''' <summary>
    ''' Muestra el valor del comprobante de egreso
    ''' </summary>
    Public Sub PrintInfo()
        If _setInfoDelegate IsNot Nothing Then
            Dim value As Tuple(Of Double, Double, Double) = _setInfoDelegate()

            NetoValue = FormatCurrency(value.Item1, 2)
            AccruedValue = FormatCurrency(value.Item2, 2)
            DeductedValue = FormatCurrency(value.Item3, 2)
        End If
    End Sub
#End Region

#Region "Events"
    Private Sub INDPceTotalValue_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceTotalValue.QueryPopUp
        RaiseEvent OpenPopupValue(sender, e)
    End Sub

    Event OpenPopupValue(sender As Object, e As CancelEventArgs)
#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
        PrintInfo()
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceInvoiceValueTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceTotalValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PopupContainerEdit1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceAccruedValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PopupContainerEdit6)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceDeductedValue)
    End Sub

End Class

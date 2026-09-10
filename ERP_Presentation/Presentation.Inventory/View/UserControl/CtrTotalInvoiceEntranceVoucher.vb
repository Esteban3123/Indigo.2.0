Imports System.ComponentModel
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base

Public Class CtrTotalInvoiceEntranceVoucher

#Region "Delegates"
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion (Neto, Descuento, Iva, Total Factura)
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String, String, String, String)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate

    ''' <summary>
    ''' formato de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyNumbertFormat As Globalization.NumberFormatInfo

#End Region

#Region "Properties"

    WriteOnly Property NetoValue As String
        Set(value As String)
            INDPceNetoValue.EditValue = value
            INDPceNetoValue.ToolTip = "Valor Neto: " + INDPceIvaValue.Text.Trim()
        End Set
    End Property

    WriteOnly Property DiscountValue As String
        Set(value As String)
            INDPceDiscountValue.EditValue = value
            INDPceDiscountValue.ToolTip = "Descuento: " + INDPceIvaValue.Text.Trim()
        End Set
    End Property

    WriteOnly Property IvaValue As String
        Set(value As String)
            INDPceIvaValue.EditValue = value
            INDPceIvaValue.ToolTip = "IVA: " + INDPceIvaValue.Text.Trim()
        End Set
    End Property

    WriteOnly Property TotalValue As String
        Set(value As String)
            INDPceTotalValue.EditValue = value
            INDPceTotalValue.ToolTip = "Total: " + INDPceIvaValue.Text.Trim()
        End Set
    End Property

    WriteOnly Property AccountPayableConsecutive As String
        Set(value As String)
            INDPceAccountPayableConsecutive.EditValue = value
            INDPceAccountPayableConsecutive.ToolTip = "Consecutivo " + value
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

    Private _codeISO4217 As String
    ''' <summary>
    ''' codigo ISO4217
    ''' </summary>
    Public WriteOnly Property CodeISO4217 As String
        Set(value As String)
            _codeISO4217 = value
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
            _codeISO4217 = If(String.IsNullOrEmpty(_codeISO4217), SessionValues.Instance.CurrencyISO4217, _codeISO4217)
            Dim value As Tuple(Of String, String, String, String) = _setInfoDelegate()

            CurrencyNumbertFormat = If(CurrencyNumbertFormat, _codeISO4217?.GetNumberFormat)
            NetoValue = Utils.GetMoneyWithISO4217(value.Item1, _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
            DiscountValue = Utils.GetMoneyWithISO4217(value.Item2, _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
            IvaValue = Utils.GetMoneyWithISO4217(value.Item3, _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
            TotalValue = Utils.GetMoneyWithISO4217(value.Item4, _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
        End If
    End Sub
#End Region

#Region "Events"
    Private Sub INDPceTotalValue_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceTotalValue.QueryPopUp
        RaiseEvent OpenPopupValue(sender, e)
    End Sub

    Private Sub INDPceAccountPayableConsecutive_Click(sender As Object, e As EventArgs) Handles INDPceAccountPayableConsecutive.Click
        'Se valida si existe el consevutivo y se extrae solo el código
        If Not String.IsNullOrEmpty(INDPceAccountPayableConsecutive.EditValue?.ToString()) Then
            Dim consecutiveValue = INDPceAccountPayableConsecutive.EditValue.ToString()
            Dim codeToCopy = If(consecutiveValue.StartsWith("CxP: "), consecutiveValue.Substring(5), consecutiveValue)
            Clipboard.SetText(codeToCopy)
            RaiseEvent ConsecutiveCopied(codeToCopy)
        End If
    End Sub

    Event OpenPopupValue(sender As Object, e As CancelEventArgs)
    Event ConsecutiveCopied(consecutive As String)
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceInvoiceValueTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceTotalValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PopupContainerEdit1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceNetoValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PopupContainerEdit4)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceDiscountValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PopupContainerEdit6)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceIvaValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceAccountPayableConsecutive)
    End Sub

End Class

Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.FormBase
Public Class CtrContractTotalInfo

#Region "Delegates"
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String, String, String, String)
    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate
#End Region

#Region "Properties"
    WriteOnly Property IvaValue As String
        Set(value As String)
            INDPceIvaValue.Text = TextIvaValue + value.Trim()
            INDPceIvaValue.ToolTip = TextIvaValue + value.Trim()
        End Set
    End Property

    WriteOnly Property DiscountValue As String
        Set(value As String)
            INDPceDiscountValue.Text = TextDiscountValue + value.Trim()
            INDPceDiscountValue.ToolTip = TextDiscountValue + value.Trim()
        End Set
    End Property

    WriteOnly Property TotalValue As String
        Set(value As String)
            INDPceTotalValue.Text = value
            INDPceTotalValue.ToolTip = "Total: " + value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Texto del control de iva value
    ''' </summary>
    ''' <remarks></remarks>
    Private _textNetValue As String
    Property TextNetValue As String
        Get
            Return _textNetValue
        End Get
        Set(value As String)
            _textNetValue = value
        End Set
    End Property

    WriteOnly Property NetValue As String
        Set(value As String)
            INDPceNetValue.Text = TextNetValue + value.Trim()
            INDPceNetValue.ToolTip = TextNetValue + value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Texto del control de iva value
    ''' </summary>
    ''' <remarks></remarks>
    Private _textIvaValue As String
    Property TextIvaValue As String
        Get
            Return _textIvaValue
        End Get
        Set(value As String)
            _textIvaValue = value
        End Set
    End Property

    ''' <summary>
    ''' Texto del control de iva value
    ''' </summary>
    ''' <remarks></remarks>
    Private _textDiscountValue As String
    Property TextDiscountValue As String
        Get
            Return _textDiscountValue
        End Get
        Set(value As String)
            _textDiscountValue = value
        End Set
    End Property

    ''' <summary>
    ''' recibe un listado de tipo string que define el EditMask y de tipo MaskType que define el tipo de mascara
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property MaskTotalValue As String
        Set(value As String)
            INDPceTotalValue.Properties.Mask.EditMask = value
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

    Private _codeISO4217 As String
    ''' <summary>
    ''' codigo ISO4217
    ''' </summary>
    Public WriteOnly Property CodeISO4217 As String
        Set(value As String)
            _codeISO4217 = value
        End Set
    End Property

    Property CurrencyNumbertFormat As Globalization.NumberFormatInfo
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
            IvaValue = Utils.GetMoneyWithISO4217(value.Item1, _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
            DiscountValue = Utils.GetMoneyWithISO4217(value.Item2, _codeISO4217)
            TotalValue = Utils.GetMoneyWithISO4217(value.Item3, _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
            NetValue = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value.Item4),
                                                    "0.00", value.Item4), _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceTotalValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceIvaValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceDiscountValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceNetValue)
    End Sub

    Private Sub INDPceTotalValue_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceTotalValue.QueryPopUp
        RaiseEvent OpenPopupValue(sender, e)
    End Sub

    Event OpenPopupValue(sender As Object, e As CancelEventArgs)
End Class

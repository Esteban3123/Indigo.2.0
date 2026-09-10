Imports System.ComponentModel
Imports DevExpress.XtraEditors.Mask
Imports Infrastructure.CrossCutting.Base

Public Class CtrTotalInvoice

#Region "Delegates"
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion (Neto, Descuento, Iva, Total Factura)
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String, String, String, String, Integer)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate
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

    ''' <summary>
    ''' abreviacion de la moneda
    ''' </summary>
    Private _currencyAbbreviation As String
    Public Property CurrencyAbbreviation As String
        Get
            Return If(_currencyAbbreviation, SessionValues.Instance.CurrencyISO4217)
        End Get
        Set(value As String)
            _currencyAbbreviation = value
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
            Dim value As Tuple(Of String, String, String, String, Integer) = _setInfoDelegate()
            Dim decimals As Byte = 0

            If {1, 2}.Contains(value.Item5) Then
                decimals = value.Item5
            End If

            NetoValue = Utils.GetMoneyWithISO4217(value.Item1, CurrencyAbbreviation, decimals)
            DiscountValue = Utils.GetMoneyWithISO4217(value.Item2, CurrencyAbbreviation, decimals)
            IvaValue = Utils.GetMoneyWithISO4217(value.Item3, CurrencyAbbreviation, decimals)
            TotalValue = Utils.GetMoneyWithISO4217(value.Item4, CurrencyAbbreviation, decimals)
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
    End Sub

End Class

Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources

Public Class CtrServiceOrderInfo

    ''' <summary>
    ''' Delegado de la funcion que establece la informacion
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String, String, Decimal)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate

    Public Event OpenPopupValue(sender As Object, e As CancelEventArgs)


#Region "Properties"
    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <BrowsableAttribute(False)> _
    Public Property PopupContainerControl As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.PcePopUpEdit.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.PcePopUpEdit.Properties.PopupControl = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado en el valor
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <BrowsableAttribute(False)> _
    Public Property PopupContainerControlValue As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.INDPceValue.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.INDPceValue.Properties.PopupControl = value
        End Set
    End Property

    WriteOnly Property Patient As String
        Set(value As String)
            PcePopUpEdit.Text = value.Trim()
            PcePopUpEdit.ToolTip = value.Trim()
        End Set
    End Property

    WriteOnly Property ServiceValue As String
        Set(value As String)
            INDPceValue.Text = value
        End Set
    End Property

    ''' <summary>
    ''' valor del impuesto del IVA
    ''' </summary>
    WriteOnly Property TaxValue As String
        Set(value As String)
            INDLbTaxValue.Text = value
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
            Dim value As Tuple(Of String, String, Decimal) = _setInfoDelegate()
            Patient = value.Item1
            ServiceValue = value.Item2
            If value?.Item3 <> 0 Then
                Me.LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                TaxValue = $"{ResourceManager.GetString("TaxName")} {Format(value.Item3, "c2")}"
            Else
                Me.TaxValue = Format(0, "c2")
                Me.LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub
#End Region

    Private Sub INDPceValue_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceValue.QueryPopUp
        RaiseEvent OpenPopupValue(sender, e)
    End Sub

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PcePopUpEdit)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLbTaxValue)
    End Sub

End Class

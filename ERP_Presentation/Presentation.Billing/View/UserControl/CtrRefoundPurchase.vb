Imports System.ComponentModel
Imports DevExpress.XtraEditors.Mask

Public Class CtrRefoundPurchase

#Region "Delegates"
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion (Valor Devolucion, Completa = true/Parcial = flase)
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of Decimal, Boolean?)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate
#End Region

#Region "Properties"
    WriteOnly Property DevolutionValue As String
        Set(value As String)
            INDPceDevolutionValue.EditValue = value
            INDPceDevolutionValue.ToolTip = "Valor Devolución: " + INDPceDevolutionValue.Text.Trim()
        End Set
    End Property

    WriteOnly Property DevolutionType As Boolean?
        Set(value As Boolean?)
            If value Is Nothing Then
                INDPceDevolutionType.Text = String.Empty
                INDPceDevolutionType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
                LayoutControl2.BackColor = System.Drawing.Color.Transparent
                LayoutControlItem3.AppearanceItemCaption.BackColor = System.Drawing.Color.Transparent
            ElseIf value Then
                INDPceDevolutionType.Text = "Devolución Total"
                INDPceDevolutionType.Properties.Appearance.BackColor = System.Drawing.Color.Green
                LayoutControl2.BackColor = System.Drawing.Color.Green
                LayoutControlItem3.AppearanceItemCaption.BackColor = System.Drawing.Color.Green
            Else
                INDPceDevolutionType.Text = "Devolución Parcial"
                INDPceDevolutionType.Properties.Appearance.BackColor = System.Drawing.Color.MediumTurquoise
                LayoutControl2.BackColor = System.Drawing.Color.MediumTurquoise
                LayoutControlItem3.AppearanceItemCaption.BackColor = System.Drawing.Color.MediumTurquoise

            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado en el valor
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <BrowsableAttribute(False)> _
    Public Property PopupContainerControlTotalValue As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.INDPceDevolutionValue.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.INDPceDevolutionValue.Properties.PopupControl = value
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
            Dim value As Tuple(Of Decimal, Boolean?) = _setInfoDelegate()

            DevolutionValue = FormatCurrency(value.Item1, 2)
            DevolutionType = value.Item2
        End If
    End Sub
#End Region

#Region "Events"
    Private Sub INDPceTotalValue_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceDevolutionValue.QueryPopUp
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceDevolutionValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceInvoiceValueTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem2)
        'Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDPceDevolutionType)
        'Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
    End Sub

End Class

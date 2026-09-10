Imports Presentation.Base.Extension

Public Class CtrBillingCostProduct

#Region "Properties"
    ''' <summary>
    ''' Establece el valor del subTotal
    ''' </summary>
    Private WriteOnly Property SubTotalValue As Decimal
        Set(value As Decimal)
            INDlblSubTotal.Text = value.MoneyFormat(0).ToString()
        End Set
    End Property

    ''' <summary>
    ''' Delegado para obtener el valor del subtotal
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function DelegateSubTotalValue() As Decimal

    ''' <summary>
    ''' Función del tipo del delegado
    ''' </summary>
    Public _functionSubTotalValue As DelegateSubTotalValue

#End Region

#Region "Events"
    ''' <summary>
    ''' Handles the Load event of the CtrCostProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrCostProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
#End Region

#Region "Methods and functions"

    ''' <summary>
    ''' Sets the function delegate.
    ''' </summary>
    Public Sub SetFunctionDelegate(functionSubTotalValue As DelegateSubTotalValue)
        _functionSubTotalValue = functionSubTotalValue
    End Sub

    ''' <summary>
    ''' Prints the value.
    ''' </summary>
    Public Sub PrintValue()
        If _functionSubTotalValue IsNot Nothing Then
            SubTotalValue = _functionSubTotalValue()
        End If
    End Sub

#End Region

    Private Sub CtrDebitCredit_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblSubTotal)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LabelControl2)

    End Sub

End Class

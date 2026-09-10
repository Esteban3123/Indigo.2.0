Public Class CtrInfoControlOutpatientServices
    ''' <summary>
    ''' Delegado de la funcion que establece la informacion (Neto, Descuento, Iva, Total Factura)
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String, String, String)

    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate

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
            Dim value As Tuple(Of String, String, String) = _setInfoDelegate()
            INDLblValue.Text = FormatCurrency(value.Item1, 2)
            INDLblCareCenter.Text = value.Item2
            INDLblFunctionalUnit.Text = value.Item3
        End If
    End Sub
#End Region

    'Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    '    AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    'End Sub

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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem4)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem5)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLblValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LabelControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLblCareCenter)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LabelControl2)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLblFunctionalUnit)
    End Sub

    Private Sub INDLblCareCenter_Click(sender As Object, e As EventArgs) Handles INDLblCareCenter.Click

    End Sub
End Class

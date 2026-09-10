Public Class CtrYearMonthHorizontal

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor de la cuenta por pagar y el valor de la factura
    ''' </summary>
    Public Delegate Function InfoDelegate() As Tuple(Of Integer, Integer)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    Private _functionInfo As InfoDelegate

#End Region

#Region "Methods"
    ''' <summary>
    ''' Asigna el delegado que se va ejecutar para obtener el valor cxp y el valor factura
    ''' </summary>
    ''' <param name="functionInfo"></param>
    ''' <remarks></remarks>
    Public Sub SetInfo(functionInfo As InfoDelegate)
        _functionInfo = functionInfo
    End Sub

    ''' <summary>
    ''' Metodo para refrescar la fecha (Mes y año)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshInfo()
        If _functionInfo IsNot Nothing Then
            Dim tuplaInfo = _functionInfo()
            If tuplaInfo.Item1 = 0 AndAlso tuplaInfo.Item2 = 0 Then
                INDlbMonth.Text = "No Seleccionado"
                INDlbYear.Text = "No Seleccionado"
            Else
                INDlbMonth.Text = MonthName(tuplaInfo.Item1).ToUpper()
                INDlbYear.Text = tuplaInfo.Item2.ToString()
            End If
        End If
    End Sub

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciYear)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbYear)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciMonth)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbMonth)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbTitle)
    End Sub
#End Region

End Class

Imports Presentation.Base.Extension
Public Class CtrValuePaymentsDeductions

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor de la cuenta por pagar, el valor de pagos y el valor de deducciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function TotalValuesDelegate() As Tuple(Of Decimal, Decimal, Decimal)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionTotalValues As TotalValuesDelegate

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna el delegado que se va ejecutar para obtener el valor cxp y el valor factura
    ''' </summary>
    ''' <param name="functionTotalValues"></param>
    ''' <remarks></remarks>
    Public Sub SetTotalValues(functionTotalValues As TotalValuesDelegate)
        _functionTotalValues = functionTotalValues
    End Sub

    ''' <summary>
    ''' Metodo para refrescar los valores - THREAD-SAFE
    ''' </summary>
    ''' <remarks>
    ''' Puede ser llamado desde cualquier thread (UI thread o background thread).
    ''' Automáticamente detecta y hace Invoke al UI thread si es necesario.
    ''' </remarks>
    Public Sub RefreshTotalValues()
        Try
            ' *** VERIFICAR SI ESTAMOS EN EL THREAD CORRECTO ***
            If INDlbValueCxP.InvokeRequired Then
                ' ❌ Estamos en un background thread - invocar en UI thread
                INDlbValueCxP.Invoke(New Action(AddressOf RefreshTotalValuesInternal))
            Else
                ' ✅ Estamos en el UI thread - ejecutar directamente
                RefreshTotalValuesInternal()
            End If
            
        Catch ex As Exception
            ' Manejo de error: intentar con BeginInvoke como fallback
            System.Diagnostics.Debug.WriteLine($"RefreshTotalValues Error: {ex.Message}")
            Try
                If Not INDlbValueCxP.IsDisposed Then
                    INDlbValueCxP.BeginInvoke(New Action(AddressOf RefreshTotalValuesInternal))
                End If
            Catch innerEx As Exception
                System.Diagnostics.Debug.WriteLine($"RefreshTotalValues BeginInvoke Error: {innerEx.Message}")
                ' Silenciar si el control está siendo destruido
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' Lógica interna de actualización - SOLO debe ejecutarse desde el UI thread
    ''' </summary>
    ''' <remarks>
    ''' No llamar directamente - usar RefreshTotalValues() que maneja thread-safety
    ''' </remarks>
    Private Sub RefreshTotalValuesInternal()
        Try
            If _functionTotalValues IsNot Nothing Then
                Dim tuplaTotalValues = _functionTotalValues()
                
                ' Actualizar controles solo si no están dispuestos
                If Not INDlbValueCxP.IsDisposed Then
                    INDlbValueCxP.Text = tuplaTotalValues.Item1.MoneyFormat(0)
                End If
                
                If Not INDlbValuePayments.IsDisposed Then
                    INDlbValuePayments.Text = tuplaTotalValues.Item2.MoneyFormat(0)
                End If
                
                If Not INDlbValueDeductions.IsDisposed Then
                    INDlbValueDeductions.Text = tuplaTotalValues.Item3.MoneyFormat(0)
                End If
            End If
            
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"RefreshTotalValuesInternal Error: {ex.Message}")
        End Try
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem4)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem5)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem6)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemValueCxP)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValueCxp)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemPayments)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemDeductions)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValuePayments)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValueDeductions)
    End Sub

    Private Sub INDlbItemValueCxP_Click(sender As Object, e As EventArgs) Handles INDlbItemValueCxP.Click

    End Sub

    Private Sub INDlbValueDeductions_Click(sender As Object, e As EventArgs) Handles INDlbValueDeductions.Click

    End Sub
End Class

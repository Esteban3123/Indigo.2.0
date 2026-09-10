Imports Presentation.Base.Extension
Public Class CtrResumptionHoliday

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor de la salida de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function DaysDelegate() As Tuple(Of Integer)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionTotalValues As DaysDelegate

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna el delegado que se va ejecutar para obtener el valor cxp y el valor factura
    ''' </summary>
    ''' <param name="functionTotalValues"></param>
    ''' <remarks></remarks>
    Public Sub SetTotalValues(functionTotalValues As DaysDelegate)
        _functionTotalValues = functionTotalValues
    End Sub

    ''' <summary>
    ''' Metodo para refrescar los valores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshTotalValues()
        If _functionTotalValues IsNot Nothing Then
            Dim tuplaTotalValues = _functionTotalValues()
            INDlbValue.Text = tuplaTotalValues.Item1.ToString
        End If
    End Sub

#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

End Class

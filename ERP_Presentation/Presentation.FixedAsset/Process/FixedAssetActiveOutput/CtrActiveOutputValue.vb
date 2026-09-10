Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.Extension
Public Class CtrActiveOutputValue

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor de la salida de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function TotalValuesDelegate() As Tuple(Of Decimal)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionTotalValues As TotalValuesDelegate

    ''' <summary>
    ''' formato de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyNumbertFormat As Globalization.NumberFormatInfo

#End Region

#Region "Properties"

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
    ''' Asigna el delegado que se va ejecutar para obtener el valor cxp y el valor factura
    ''' </summary>
    ''' <param name="functionTotalValues"></param>
    ''' <remarks></remarks>
    Public Sub SetTotalValues(functionTotalValues As TotalValuesDelegate)
        _functionTotalValues = functionTotalValues
    End Sub

    ''' <summary>
    ''' Metodo para refrescar los valores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshTotalValues()
        If _functionTotalValues IsNot Nothing Then
            _codeISO4217 = If(String.IsNullOrEmpty(_codeISO4217), SessionValues.Instance.CurrencyISO4217, _codeISO4217)
            Dim tuplaTotalValues = _functionTotalValues()

            CurrencyNumbertFormat = If(CurrencyNumbertFormat, _codeISO4217?.GetNumberFormat)
            INDlbValue.Text = Utils.GetMoneyWithISO4217(tuplaTotalValues.Item1, _codeISO4217, CurrencyNumbertFormat?.CurrencyDecimalDigits)
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem2)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbText)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValue)
    End Sub

End Class

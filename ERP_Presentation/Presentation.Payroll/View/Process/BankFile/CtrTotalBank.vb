Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.Extension
Public Class CtrTotalBank

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

    Private Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene o asigna el valor  de la moneda parametrizada
    ''' </summary>
    Private _currencyAbbreviation As String
    Public Property CurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(_currencyAbbreviation), Indigo?.CurrencyISO4217, _currencyAbbreviation)
        End Get
        Set(value As String)
            _currencyAbbreviation = value
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
            Dim tuplaTotalValues = _functionTotalValues()
            INDlbValue.Text = Utils.GetMoneyWithISO4217(tuplaTotalValues.Item1, CurrencyAbbreviation)
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

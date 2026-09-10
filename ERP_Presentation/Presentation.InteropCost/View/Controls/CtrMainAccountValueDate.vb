#Region "Imports"

Imports Presentation.Base.Extension
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP

#End Region

Public Class CtrMainAccountValueDate

#Region "Fields"

    ''' <summary>
    ''' Saldo
    ''' </summary>
    Private _balance As Decimal
    ''' <summary>
    ''' Valor total
    ''' </summary>
    Private _value As Decimal
    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    Private Indigo As SessionValues = SessionValues.Instance


    ''' <summary>
    ''' Valor de  abreviacion de moneda
    ''' </summary>
    Private _currencyAbbreviation As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el mes del periodo
    ''' </summary>
    ''' <value>Mes del periodo</value>
    ''' <returns>El mes del periodo</returns>
    Public Property Month As Integer

    ''' <summary>
    ''' Obtiene o asigna el año del periodo
    ''' </summary>
    ''' <value>Año del periodo</value>
    ''' <returns>El año del periodo</returns>
    Public Property Year As Integer

    ''' <summary>
    ''' Obtiene o asigna el saldo a distribuir
    ''' </summary>
    ''' <value>Saldo a distribuir</value>
    ''' <returns>El saldo a distribuir</returns>
    Public Property Balance As Decimal
        Get
            Return _balance
        End Get
        Set(value As Decimal)
            _balance = value
            INDlblBalance.Text = Utils.GetMoneyWithISO4217(value, If(String.IsNullOrEmpty(_currencyAbbreviation), Indigo?.CurrencyISO4217, _currencyAbbreviation))
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor total a distribuir
    ''' </summary>
    ''' <value>Valor a distribuir</value>
    ''' <returns>El valor a distribuir</returns>
    Public Property Value As Decimal
        Get
            Return _value
        End Get
        Set(value As Decimal)
            _value = value
            INDlblValue.Text = Utils.GetMoneyWithISO4217(value, If(String.IsNullOrEmpty(_currencyAbbreviation), Indigo?.CurrencyISO4217, _currencyAbbreviation))
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o asigna el valor  de la moneda parametrizada
    ''' </summary>
    ''' <value>Valor a distribuir</value>
    ''' <returns>El valor a distribuir</returns>
    Public Property CurrencyAbbreviation As String
        Get
            Return _currencyAbbreviation
        End Get
        Set(value As String)
            _currencyAbbreviation = value
            value = value
            Balance = Balance
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Da formato a la fecha del periodo y la carga en los controles
    ''' </summary>
    Public Sub LoadDate()
        Dim ci As System.Globalization.CultureInfo = Indigo.Culture
        Dim dtfi As System.Globalization.DateTimeFormatInfo = ci.DateTimeFormat
        If Month <> 0 Then
            INDsbMonth.Text = Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(Month), Microsoft.VisualBasic.VbStrConv.ProperCase)
        End If
        INDsbYear.Text = Year
    End Sub

#End Region

#Region "Handlers"
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblText)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblBalance)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDsbYear)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDsbMonth)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LabelControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblValue)
    End Sub

#End Region

End Class
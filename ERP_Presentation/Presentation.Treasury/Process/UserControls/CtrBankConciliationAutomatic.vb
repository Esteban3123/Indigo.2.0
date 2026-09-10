#Region "Imports"
Imports Infrastructure.CrossCutting.Base
#End Region

Public Class CtrBankConciliationAutomatic

#Region "Properties"
    ''' <summary>
    ''' Variable que contiene la abreviación de la moneda
    ''' </summary>
    Public _currencyAbbreviation As String
    ''' <summary>
    ''' formato de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyNumbertFormat As Globalization.NumberFormatInfo

    ''' <summary>
    ''' Propiedad que asigna el nombre de la entidad bancaria
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityBankAccountName As String
        Get
            Return LEntityBankAccount.Text
        End Get
        Set(value As String)
            LEntityBankAccount.Text = "Cuenta bancaria: " & value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que asigna el mes
    ''' </summary>
    ''' <returns></returns>
    Public Property Month As String
        Get
            Return LMonth.Text
        End Get
        Set(value As String)
            LMonth.Text = "Mes: " & value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que asigna el valor a reconciliar
    ''' </summary>
    Private _differenceReconcile As Decimal
    Public Property DifferenceReconcile As Decimal
        Get
            Return _differenceReconcile
        End Get
        Set(value As Decimal)
            _differenceReconcile = value
            UpdateDifferenceReconcileLabel()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que asigna el saldo final del extracto
    ''' </summary>
    Private _finalStatementBalance As Decimal
    Public Property FinalStatementBalance As Decimal
        Get
            Return _finalStatementBalance
        End Get
        Set(value As Decimal)
            _finalStatementBalance = value
            UpdateFinalStatementBalanceLabel()
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Restablece los valores de los controles
    ''' </summary>
    Public Sub CleanControls()
        _currencyAbbreviation = Nothing
        EntityBankAccountName = Nothing
        Month = Nothing
        DifferenceReconcile = 0
        FinalStatementBalance = 0
    End Sub
    ''' <summary>
    ''' Asigna el formado de la moneda
    ''' </summary>
    Public Sub PrintInfo()
        UpdateDifferenceReconcileLabel()
        UpdateFinalStatementBalanceLabel()
    End Sub

    ''' <summary>
    ''' Muestra los valores con el símbolo de moneda correspondiente
    ''' </summary>
    Public Sub SetCurrencyUI()
        _currencyAbbreviation = If(String.IsNullOrEmpty(_currencyAbbreviation), SessionValues.Instance.CurrencyISO4217, _currencyAbbreviation)
        CurrencyNumbertFormat = If(CurrencyNumbertFormat, _currencyAbbreviation?.GetNumberFormat)
    End Sub

    ''' <summary>
    ''' Muestra el texto con los valores del Ctr
    ''' </summary>
    Private Sub UpdateDifferenceReconcileLabel()
        If _currencyAbbreviation Is Nothing Then SetCurrencyUI()
        LDifferenceReconcile.Text = String.Format("Diferencia a conciliar: {0}", Utils.GetMoneyWithISO4217(_differenceReconcile, _currencyAbbreviation, CurrencyNumbertFormat?.CurrencyDecimalDigits))
    End Sub

    ''' <summary>
    ''' Muestra el texto con los valores del Ctr
    ''' </summary>
    Private Sub UpdateFinalStatementBalanceLabel()
        If _currencyAbbreviation Is Nothing Then SetCurrencyUI()
        LFinalStatementBalance.Text = String.Format("Saldo final extracto: {0}", Utils.GetMoneyWithISO4217(_finalStatementBalance, _currencyAbbreviation, CurrencyNumbertFormat?.CurrencyDecimalDigits))
    End Sub

#End Region

#Region "Events"

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LEntityBankAccount)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LMonth)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LDifferenceReconcile)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LFinalStatementBalance)
    End Sub

#End Region

End Class

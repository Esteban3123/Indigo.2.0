Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.Extension

Public Class CtrCostDistributionLabor

#Region "Globals"
    ''' <summary>
    ''' Variable con los datos de la Sesión
    ''' </summary>
    Private Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Properties"
    ''' <summary>
    ''' Sets the total accrued.
    ''' </summary>
    Private _totalAccruedValue As Decimal
    Public WriteOnly Property TotalAccrued As Decimal
        Set(value As Decimal)
            If _settingCost IsNot Nothing AndAlso _settingCost.Id > 0 Then
                _totalAccruedValue = value
                If _settingCost.CostEstimateLabor = 1 Then
                    LciItemSuperior.Text = "Total Devengado"
                    lblTotalItemSuperior.Text = Utils.GetMoneyWithISO4217(_totalAccruedValue, OriginCurrencyAbbreviation)
                Else
                    LciInferior.Text = "Total Devengado"
                    lblTotalItemInferior.Text = Utils.GetMoneyWithISO4217(_totalAccruedValue, OriginCurrencyAbbreviation)
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Sets the total accrued more.
    ''' </summary>
    Private _totalAccruedPatronalesValue As Decimal
    Public WriteOnly Property TotalAccruedPatronales As Decimal
        Set(value As Decimal)
            If _settingCost IsNot Nothing AndAlso _settingCost.Id > 0 Then
                _totalAccruedPatronalesValue = value
                If _settingCost.CostEstimateLabor = 1 Then
                    LciInferior.Text = "Devengado + Patronales"
                    lblTotalItemInferior.Text = Utils.GetMoneyWithISO4217(_totalAccruedPatronalesValue, OriginCurrencyAbbreviation)
                Else
                    LciItemSuperior.Text = "Devengado + Pat."
                    lblTotalItemSuperior.Text = Utils.GetMoneyWithISO4217(_totalAccruedPatronalesValue, OriginCurrencyAbbreviation)
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Asigna el valor a distribuir
    ''' </summary>
    Private _valueToDistributed As Decimal
    Public WriteOnly Property ValueToDistributed As Decimal
        Set(value As Decimal)
            _valueToDistributed = value
            LciItemValueToDistributed.Text = "Valor a Distribuir"
            lblItemValueToDistributed.Text = Utils.GetMoneyWithISO4217(Math.Round(_valueToDistributed, 2), OfficialCurrencyAbbreviation)
        End Set
    End Property

    Private _maximunHours As Integer
    ''' <summary>
    ''' Sets the hour labor.
    ''' </summary>
    ''' <value>
    ''' The hour labor.
    ''' </value>
    Public Property MaximunHours As Integer
        Get
            Return _maximunHours
        End Get
        Set(value As Integer)
            _maximunHours = value
            lblHorasLaboradas.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the month.
    ''' </summary>
    Property Month As Integer

    ''' <summary>
    ''' Gets or sets the year.
    ''' </summary>
    Property Year As Integer

    Private _settingCost As Domain.Entities.CostSetting
    WriteOnly Property SettingCost As Domain.Entities.CostSetting
        Set(value As Domain.Entities.CostSetting)
            _settingCost = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor  de la moneda oficial
    ''' </summary>
    Private _officialCurrencyAbbreviation As String
    Public Property OfficialCurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(_officialCurrencyAbbreviation), Indigo?.CurrencyISO4217, _officialCurrencyAbbreviation)
        End Get
        Set(value As String)
            _officialCurrencyAbbreviation = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor  de la moneda del documento origen
    ''' </summary>
    Private _originCurrencyAbbreviation As String
    Public Property OriginCurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(_originCurrencyAbbreviation), Indigo?.CurrencyISO4217, _originCurrencyAbbreviation)
        End Get
        Set(value As String)
            _originCurrencyAbbreviation = value
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Handles the Load event of the CtrDistributionLabor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrDistributionLabor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CleanControls()
    End Sub

    ''' <summary>
    ''' Método que se encarga de limpiar el Ctr
    ''' </summary>
    Public Sub CleanControls()
        OriginCurrencyAbbreviation = Indigo?.CurrencyISO4217
        OfficialCurrencyAbbreviation = Indigo?.CurrencyISO4217
        TotalAccrued = 0
        TotalAccruedPatronales = 0
        MaximunHours = 0
        ValueToDistributed = 0
    End Sub

    ''' <summary>
    ''' Método que muestra los valores del Control acorde a las monedas respectivas
    ''' </summary>
    Public Sub PrintInfo()
        TotalAccrued = _totalAccruedValue
        TotalAccruedPatronales = _totalAccruedPatronalesValue
        ValueToDistributed = _valueToDistributed
    End Sub

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LciItemSuperior)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LciInferior)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(lblTotalItemSuperior)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(lblTotalItemInferior)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(lblHorasLaboradas)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LciItemValueToDistributed)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(lblItemValueToDistributed)
    End Sub
#End Region


End Class

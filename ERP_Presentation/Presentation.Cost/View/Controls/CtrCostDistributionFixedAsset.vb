Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.Extension
Public Class CtrCostDistributionFixedAsset

    Public Sub New()
        InitializeComponent()
        ci = Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture
        dtfi = ci.DateTimeFormat
    End Sub

    Private ci As System.Globalization.CultureInfo
    Private dtfi As System.Globalization.DateTimeFormatInfo

    Private _deprecationValue As Decimal
    Public Property DeprecationValue As Decimal
        Get
            Return _deprecationValue
        End Get
        Set(value As Decimal)
            _deprecationValue = value
            LblDeprecationValue.Text = Utils.GetMoneyWithISO4217(value, CurrencyAbbreviation)
        End Set
    End Property


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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem4)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LblName)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LblDeprecationValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LblMonth)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LblYear)

    End Sub



    Private _month As Integer
    ''' <summary>
    ''' Gets or sets the month.
    ''' </summary>
    Property Month As Integer
        Get
            Return _month
        End Get
        Set(value As Integer)
            _month = value
            If value <> 0 Then
                LblMonth.Text = Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(Month), Microsoft.VisualBasic.VbStrConv.ProperCase)
            End If
        End Set
    End Property

    Private _year As Integer
    ''' <summary>
    ''' Gets or sets the year.
    ''' </summary>
    Property Year As Integer
        Get
            Return _year
        End Get
        Set(value As Integer)
            _year = value
            LblYear.Text = value
        End Set
    End Property

    Private _settingCost As Domain.Entities.CostSetting
    WriteOnly Property SettingCost As Domain.Entities.CostSetting
        Set(value As Domain.Entities.CostSetting)
            _settingCost = value
        End Set
    End Property

    Private Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Abreviacion de la moneda
    ''' </summary>
    Private _currencyAbbreviation As String
    Public Property CurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(_currencyAbbreviation), Indigo?.CurrencyISO4217, _currencyAbbreviation)
        End Get
        Set(value As String)
            _currencyAbbreviation = value
            DeprecationValue = DeprecationValue
        End Set
    End Property


    Private Sub CtrDistributionFixedAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LblDeprecationValue_Click(sender As Object, e As EventArgs) Handles LblDeprecationValue.Click

    End Sub
End Class

Imports Presentation.Base.Extension
Public Class CtrDistributionFixedAsset

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

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
            LblDeprecationValue.Text = value.MoneyFormat(0)
        End Set
    End Property


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

    Private _settingCost As Domain.Entities.InteropCostSetting
    WriteOnly Property SettingCost As Domain.Entities.InteropCostSetting
        Set(value As Domain.Entities.InteropCostSetting)
            _settingCost = value
        End Set
    End Property

    Private Sub CtrDistributionFixedAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

Imports Presentation.Base.Extension

Public Class CtrDistributionLabor

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LciItemSuperior)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LciInferior)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(lblTotalItemSuperior)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(lblTotalItemInferior)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(lblHorasLaboradas)
    End Sub

    ''' <summary>
    ''' Sets the total accrued.
    ''' </summary>
    ''' <value>
    ''' The total accrued.
    ''' </value>
    Public WriteOnly Property TotalAccrued As Decimal
        Set(value As Decimal)
            If _settingCost IsNot Nothing AndAlso _settingCost.Id > 0 Then
                If _settingCost.CostEstimateLabor = 1 Then
                    LciItemSuperior.Text = "Total Devengado"
                    lblTotalItemSuperior.Text = value.MoneyFormat(0)
                Else
                    LciInferior.Text = "Total Devengado"
                    lblTotalItemInferior.Text = value.MoneyFormat(0)
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Sets the total accrued more.
    ''' </summary>
    ''' <value>
    ''' The total accrued more.
    ''' </value>
    Public WriteOnly Property TotalAccruedPatronales As Decimal
        Set(value As Decimal)
            If _settingCost IsNot Nothing AndAlso _settingCost.Id > 0 Then
                If _settingCost.CostEstimateLabor = 1 Then
                    LciInferior.Text = "Devengado + Patronales"
                    lblTotalItemInferior.Text = value.MoneyFormat(0)
                Else
                    LciItemSuperior.Text = "Devengado + Pat."
                    lblTotalItemSuperior.Text = value.MoneyFormat(0)
                End If
            End If
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
    Private _settingCost As Domain.Entities.InteropCostSetting
    WriteOnly Property SettingCost As Domain.Entities.InteropCostSetting
        Set(value As Domain.Entities.InteropCostSetting)
            _settingCost = value
        End Set
    End Property

    ''' <summary>
    ''' Handles the Load event of the CtrDistributionLabor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrDistributionLabor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class

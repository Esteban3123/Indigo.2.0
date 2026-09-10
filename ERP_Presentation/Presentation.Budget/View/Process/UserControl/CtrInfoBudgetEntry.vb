Imports Presentation.Base.Extension
Imports Infrastructure.CrossCutting.Base

Public Class CtrInfoBudgetEntry

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor
    ''' Entidad presupuestal, vigencia, estado vigencia, valor vigencia y el valor actual digitado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function InfoDelegate() As Tuple(Of String, String, String, Decimal, Decimal)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionInfo As InfoDelegate

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna el delegado que se va ejecutar para obtener el valor cxp y el valor factura
    ''' </summary>
    ''' <param name="functionInfo"></param>
    ''' <remarks></remarks>
    Public Sub SetTotalValues(functionInfo As InfoDelegate)
        _functionInfo = functionInfo
    End Sub

    ''' <summary>
    ''' Metodo para refrescar los valores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshInfo()
        If _functionInfo IsNot Nothing Then
            Dim tuplaInfo = _functionInfo()
            INDlbBudgetaryEntity.Text = tuplaInfo.Item1
            INDlbValidity.Text = tuplaInfo.Item2
            INDlbStatus.Text = tuplaInfo.Item3
            INDlbValue.Text = tuplaInfo.Item4.MoneyFormat(0)

            'Se calcula el valor que falta para completar el 100%
            INDpbValues.ToolTip = "Faltante para completar el presupuesto: " + (tuplaInfo.Item4 - tuplaInfo.Item5).ToString("C0")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que refresca el progressBar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshProgressBar()
        If _functionInfo IsNot Nothing Then
            Dim tuplaInfo = _functionInfo()
            'Valor de la vigencia
            Dim valueValidity As Decimal = tuplaInfo.Item4
            'Valor digitado para calcular el porcetaje del ProgressBar
            Dim valueDigitate As Decimal = tuplaInfo.Item5
            'Se calcula el porcentaje del ProgressBar
            Dim percentage As Decimal = 0
            If valueValidity > 0 Then
                percentage = (100 * valueDigitate / 100) / valueValidity
                'percentage = Utils.RoundValue((100 * valueDigitate / 100) / valueValidity, Utils.RoundLevel.Unit)
            End If
            percentage = percentage * 100
            percentage = Utils.RoundValue(percentage, Utils.RoundLevel.Unit)

            INDpbValues.EditValue = percentage
            INDpbValues.Properties.PercentView = True
            INDpbValues.Properties.ShowTitle = True
            INDpbValues.PerformStep()
            INDpbValues.Update()

            'Se calcula el valor que falta para completar el 100%
            INDpbValues.ToolTip = "Faltante para completar el presupuesto: " + (valueValidity - valueDigitate).ToString("C0")
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(PanelControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbBudgetaryEntity)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemValidity)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValidity)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemStatus)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbStatus)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValue)


    End Sub

End Class

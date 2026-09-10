Public Class CtrDeterioro

#Region "Propiedades"

    ''' <summary>
    ''' Muestra el tiempo de recaudo de la cartera
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Expectative As Integer
        Get
            Return TxtTiempoRecaudo.EditValue
        End Get
    End Property
    ''' <summary>
    ''' Muestra la tasa VPN
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TasaVPN As Decimal
        Get
            Return CDec(TxtTasaVPN.EditValue) * 100
        End Get
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Método para cambiar los controles de acuerdo a si Deterioro por Clasificación
    ''' </summary>
    ''' <param name="courtDate">Fecha de corte para concatenar en el label</param>
    Friend Sub ChangeToDeteriorationByClassification(courtDate As Date?)
        LciRecaudo.Text = "Deterioro por Clasificación"
        If courtDate.HasValue Then
            LciTasa.Text = "Corte: " & courtDate.Value.ToString("dd/MM/yyyy")
        Else
            LciTasa.Text = "Corte: "
        End If
        TxtTiempoRecaudo.Visible = False
        TxtTasaVPN.Visible = False
    End Sub

    ''' <summary>
    ''' Método para restaurar los controles a su estado original
    ''' </summary>
    Friend Sub RestoreToDefaultState()
        LciRecaudo.Text = "Tiempo  Recaudo del Flujo Futuro (Meses)"
        LciTasa.Text = "Tasa Definida para Aplicación del VPN"
        TxtTiempoRecaudo.Visible = True
        TxtTasaVPN.Visible = True
    End Sub

    ''' <summary>
    ''' Método para limpiar los controles del Ctr
    ''' </summary>
    Friend Sub CleanControls()
        TxtTasaVPN.EditValue = 0
        TxtTiempoRecaudo.EditValue = 0
    End Sub

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LciTasa)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LciRecaudo)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LcDeterioro)
    End Sub
#End Region

#Region "EditValueChanging"
    Private Sub TxtTiempoRecaudo_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles TxtTiempoRecaudo.EditValueChanging
        RaiseEvent OnEditValueChangingTiempoRecaudo(e.NewValue)
    End Sub

    Private Sub TxtTasaVPN_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles TxtTasaVPN.EditValueChanging
        RaiseEvent OnEditValueChangingTasaVPN(e.NewValue)
    End Sub
#End Region

    Public Event OnEditValueChangingTiempoRecaudo(value As Integer)
    Public Event OnEditValueChangingTasaVPN(value As Decimal)

End Class

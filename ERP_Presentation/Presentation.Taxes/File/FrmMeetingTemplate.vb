Imports Presentation.Base
Imports Presentation.Controls

Public Class FrmMeetingTemplate

    Private Sub FrmMeetingTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActionsOnControls = False
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
    End Sub

    Public WriteOnly Property ActionsOnControls() As Boolean
        Set(value As Boolean)
            ' Datos Principales
            LayoutControlGroup1.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDTxtDescription.Enabled = value
            BarraBotones.StatusRecordVisible = value
            LayoutControlGroup1.EndUpdate()

        End Set
    End Property

    Private Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If Not String.IsNullOrEmpty(INDbtnCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                ActionsOnControls = True
            End If
        End If
    End Sub
End Class
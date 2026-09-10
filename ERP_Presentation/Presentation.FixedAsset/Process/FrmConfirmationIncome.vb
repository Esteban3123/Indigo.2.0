Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls

Public Class FrmConfirmationIncome

    Public WriteOnly Property ActionsOnControls() As Boolean
        Set(value As Boolean)
            ' Datos Principales
            INDLcBlockConfirmation.BeginUpdate()
            INDDeStarDate.Enabled = value
            INDDeEndDate.Enabled = value
            INDRgProcess.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDLcBlockConfirmation.EndUpdate()

        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String 'Implements Base.IcrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private Sub FrmConfirmationIncome_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDDeStarDate.Enabled Then
            INDDeStarDate.Focus()
        End If
    End Sub

    Private Sub FrmConfirmationIncome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActionsOnControls = False
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        Mensaje(EeventViewerImages.Advertencia) = "No Existen Activos con Estado NIIF (Activo) y Método de Depreciación (Unidad de Producción)"
    End Sub
End Class
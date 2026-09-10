Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

Public Class PopUpActionQualityControl
    Implements IQualityControl

#Region "Variables"
    ''' <summary>
    ''' Tipo de Acción del Menu
    ''' </summary>
    Public TypeAction As Integer

    ''' <summary>
    ''' indica si el popup de Causa de Reproceso por primera vez para cargar el datasource
    ''' </summary>
    Public _openPopUpReprocessingCause As Boolean

    ''' <summary>
    ''' Referencia la presentador (MixingStation.Package)
    ''' </summary>
    Private _presenter As PDashboardQualityControl

    Public _OpenPopUpQuantityRemaining As Boolean

	''' <summary>
	''' Indica si el PopUp se está abriendo desde el Plan de producción NPT
	''' </summary>
	Public _OpenPopUpProductionPlanNPT As Boolean

	''' <summary>
	''' Guardara el id de la baja del remanente
	''' </summary>
	Public Property causeRejectionId As Integer

	''' <summary>
	''' Guardara las observaciones de la baja del remanente
	''' </summary>
	Public Property observations As String
#End Region

#Region "Properties"
	Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de Causa de Reproceso
    ''' </summary>
    Property ReprocessingCauseDatasource As XPInstantFeedbackSource Implements IQualityControl.ReprocessingCauseDatasource
        Get
            Return CType(INDGleCauseRejection.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGleCauseRejection.Properties.DataSource = value
        End Set
    End Property

    Private WriteOnly Property ICrudBase_Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property
#End Region

#Region "Event"
    Public Event ActionQualityControl(sender As Object, e As EventArgs)

    Public Sub Buscar() Implements ICrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga los controles del Popup
    ''' </summary>
    Private Sub Loadcontrols()
        Select Case TypeAction
            Case 1
                Me.Text = "Reprocesar Producto"
                Me.INDlciCauseRejection.Text = "Causa de Reprocesamiento"
                INDlcProductClassification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlciCauseRejection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Case 2
                If _OpenPopUpQuantityRemaining Then
                    Me.Text = "Bajas de remanente"

                ElseIf _OpenPopUpProductionPlanNPT Then
                    Me.Text = "Motivos de anulación"
                    INDlciCauseRejection.Text = "Motivos"

                Else
                    Me.Text = "Rechazar Producto"
                End If

                INDlcProductClassification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlciCauseRejection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Case 3, 4
                Me.Text = "Liberar Producto"
                INDlcProductClassification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlciCauseRejection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End Select
    End Sub
#End Region

#Region "Events"

#Region "KeyDown"
    ''' <summary>
    ''' Close form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpChangeLabel_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Load"
    ''' <summary>
    ''' Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpChangeLabel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _presenter = New PDashboardQualityControl
        Loadcontrols()
    End Sub

#End Region

#Region "Click"
    Private Sub INDsbAceptar_Click(sender As Object, e As EventArgs) Handles INDsbAceptar.Click
        If Not {3, 4}.Contains(TypeAction) Then
            If INDGleCauseRejection.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = $"Debe seleccionar una causa de {If(TypeAction = 1, "reprocesamiento", "rechazo")}"
                INDMeObservations.Focus()
                Exit Sub
            End If

            If INDMeObservations.Text = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = "El campo Observaciones no está diligenciado"
                INDMeObservations.Focus()
                Exit Sub
            ElseIf INDMeObservations.Text.Trim.Length < 10 Then
                Mensaje(EeventViewerImages.Advertencia) = "El campo Observaciones debe tener minimo 10 caracteres"
                INDMeObservations.Focus()
                Exit Sub
            End If
        End If

        Me.causeRejectionId = INDGleCauseRejection.EditValue
        Me.observations = INDMeObservations.Text
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub INDsbCancel_Click(sender As Object, e As EventArgs) Handles INDsbCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDGleCauseRejection control.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleCauseRejection_QueryPopUp_1(sender As Object, e As CancelEventArgs) Handles INDGleCauseRejection.QueryPopUp
        If Not _openPopUpReprocessingCause Then
            ReprocessingCauseDatasource = _presenter.InitializeReprocessingCause(TypeAction)
            _openPopUpReprocessingCause = True
        End If
    End Sub
#End Region

#End Region

End Class
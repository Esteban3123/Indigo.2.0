#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Billing.MVP
Imports System.Collections.Concurrent
Imports Domain.Entities
#End Region

Public Class FrmPopupAddJustification

#Region "Variables"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PControlOutpatientServices

    ''' <summary>
    ''' formulario padre
    ''' </summary>
    Public FormFather As FrmAccountControl

    ''' <summary>
    ''' Los elementos a los cuales se les va agregar una justificacion
    ''' </summary>
    Private _itemsToJustify As List(Of AccountControlJustification)

#End Region

#Region "Properties"
    ''' <summary>
    ''' propiedad que toma el valor del Id de la justificacion
    ''' </summary>
    ''' <returns></returns>
    Private Property JustificationId As Integer?
        Get
            Return INDsleJustification.EditValue
        End Get
        Set(value As Integer?)
            INDsleJustification.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que pasa los datos de la lista de elementos a justificar
    ''' </summary>
    Public WriteOnly Property ListDetail As List(Of AccountControlJustification)
        Set(value As List(Of AccountControlJustification))
            _itemsToJustify = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        JustificationId = Nothing
        INDsleJustification.Properties.NullText = String.Empty
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#End Region

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupWareHouse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        If _presenter Is Nothing Then _presenter = New PControlOutpatientServices()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        Me.JustificationId = Nothing
    End Sub

#Region "QueryPopup"
    ''' <summary>
    ''' consulta las justificaciones por usuarios autorizados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReasonType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleJustification.QueryPopUp
        If INDsleJustification.Properties.DataSource Is Nothing Then
            INDsleJustification.Properties.DataSource = _presenter.GetJustificationByUserCode(SessionValues.Instance.AuditMessageWcf.CodeUser)
        End If
    End Sub
#End Region

#End Region

#Region "Click"

    ''' <summary>
    ''' Click en Guardar (guarda en la tabla interfaz de justificaciones)
    ''' </summary>
    Private Async Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Try
            AsyncLoader(True)
            Dim Str = New StringBuilder()

            If JustificationId Is Nothing Then
                Str.AppendLine("Falta llenar el siguiente campo: Justificación")
                INDsleJustification.Focus()
            End If
            If Not _itemsToJustify.Any Then
                Str.AppendLine("No hay Items para justificar")
            End If
            If Str.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = $"{Str.ToString()}"
                Exit Sub
            End If

            Parallel.ForEach(_itemsToJustify, Sub(x)
                                                  If x IsNot Nothing Then
                                                      x.JustificationId = JustificationId
                                                  End If
                                              End Sub)

            Using model As New MAccountControl(FormFather.Tag)
                Dim Result = Await model.SaveAccountControlJustification(_itemsToJustify)
                If Result Is Nothing OrElse Not Result.StateResult Then
                    Me.DialogResult = Windows.Forms.DialogResult.Abort
                    Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                    AsyncLoader(False)
                    Exit Sub
                End If
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Informacion) = Result?.Message
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Close()
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en Deshacer de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region


End Class
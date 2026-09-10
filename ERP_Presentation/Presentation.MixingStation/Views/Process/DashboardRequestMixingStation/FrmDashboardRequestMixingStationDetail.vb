'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/02/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Concurrent
Imports System.Dynamic
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmDashboardRequestMixingStationDetail

#Region "Variables"

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardRequestMixingStation

    ''' <summary>
    ''' Item seleccionado en la rejilla
    ''' </summary>
    Public ViewListDashboardRequestMixingStationXpo As ViewListDashboardRequestMixingStationXpo

    ''' <summary>
    ''' Id de las Solicitudes 
    ''' </summary>
    Public requestIds As String

    ''' <summary>
    ''' Items Seleccionados 
    ''' </summary>
    Public Items As List(Of ViewListDashboardRequestMixingStationXpo)

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para cargar nuevamente la rejilla principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ReloadPrincipalGridArgs(sender As Object, e As EventArgs)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Consulta el paquete
    ''' </summary>
    Private Sub GetDetails()
        INDgcDetail.DataSource = Nothing
        INDviewDetail.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = Presenter.ListViewListDashboardRequestMixingStationDetail(requestIds)
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetail.SafeInvoke(Sub()
                                                                     INDviewDetail.HideLoadingPanel()
                                                                     INDgcDetail.DataSource = result
                                                                     INDviewDetail.SelectAll()
                                                                 End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetail.SafeInvoke(Sub()
                                                                     INDviewDetail.HideLoadingPanel()
                                                                     Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                 End Sub)
                                      End If
                                  End Try
                              End Sub, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Método que guarda un paquete
    ''' </summary>
    Public Async Sub Guardar(isAnnular As Boolean)
        If CType(INDgcDetail.DataSource, List(Of ViewListDashboardRequestMixingStationDetailXpo)) Is Nothing OrElse CType(INDgcDetail.DataSource, List(Of ViewListDashboardRequestMixingStationDetailXpo)).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay detalles para procesar"
            Exit Sub
        End If

        If isAnnular = False Then
            If ViewListDashboardRequestMixingStationXpo.ProductionLineId = Nothing OrElse ViewListDashboardRequestMixingStationXpo.ProductionLineId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Para poder confirmar se debe asignar la línea de producción en la rejilla principal"
                Exit Sub
            End If

            If (From x In INDviewDetail.GetSelectedRows() Select DirectCast(INDviewDetail.GetRow(x), ViewListDashboardRequestMixingStationDetailXpo)).Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Para poder confirmar debe seleccionar al menos un item de la rejilla"
                Exit Sub
            End If

            'Dim listItemsSelected = (From x In INDviewDetail.GetSelectedRows() Where Not INDviewDetail.IsGroupRow(x) Select DirectCast(INDviewDetail.GetRow(x), ViewListDashboardRequestMixingStationDetailXpo)).ToList()
            Dim message As New StringBuilder
            message.AppendLine("Se va a enviar a procesar a central de mezclas los items seleccionados en la rejilla.")
            'listItemsSelected.ForEach(Sub(x) message.AppendLine("- " + If(Not String.IsNullOrEmpty(x.PatientCodeName), x.PatientCodeName, x.ItemCodeName)))
            message.AppendLine("¿Desea Continuar?")

            If MessageIndigo.Show(message.ToString(), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            ConfirmRequest()
        End If

        If isAnnular Then

            Dim listItems = CType(INDgcDetail.DataSource, List(Of ViewListDashboardRequestMixingStationDetailXpo))
            Dim _message = IIf(listItems.Any(Function(x) Not String.IsNullOrEmpty(x.CodeSusceptibleMixingStation)), "Al anular se envían todos los items al dashboard de farmacia", "Va a anular todos los items")

            If MessageIndigo.Show($"{_message}, Desea Continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            Dim args = AssigningValues(isAnnular)
                Me.AsyncLoader(True)
                Try
                    Using model As New MDashboardRequestMixingStation(Me.Tag.ToString())
                        Dim result = Await model.SP_ProcessMixingStation(args)
                        Me.AsyncLoader(False)
                        If result.StateResult Then
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                            RaiseEvent ReloadPrincipalGridArgs(Nothing, Nothing)
                            GetDetails()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    End Using
                Catch ex As Exception
                    Me.AsyncLoader(False)
                    Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
                End Try
            End If
    End Sub

    ''' <summary>
    ''' Procesa la Solicitud de Central de Mezclas
    ''' </summary>
    Private Async Sub ConfirmRequest()
        Dim listItems = (From x In INDviewDetail.GetSelectedRows() Where Not INDviewDetail.IsGroupRow(x) Select DirectCast(INDviewDetail.GetRow(x), ViewListDashboardRequestMixingStationDetailXpo)).ToList()
        Dim listIds = (From x In listItems Select x.RequestMixingStationDetailId).ToList()
        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardRequestMixingStation(Me.Tag.ToString())
                Dim result = Await model.ProcessRequestMixingStation(listIds)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "El proceso se completo correctamente"
                    RaiseEvent ReloadPrincipalGridArgs(Nothing, Nothing)
                    GetDetails()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
        Me.Close()
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Function AssigningValues(isAnnular As Boolean) As Object
        Dim args As Object = New ExpandoObject()
        Dim myListDetail As New ConcurrentBag(Of Object)()
        Dim listItems As List(Of ViewListDashboardRequestMixingStationDetailXpo) = Nothing

        If isAnnular = False Then
            listItems = (From x In INDviewDetail.GetSelectedRows() Where Not INDviewDetail.IsGroupRow(x) Select DirectCast(INDviewDetail.GetRow(x), ViewListDashboardRequestMixingStationDetailXpo)).ToList()
            listItems.ForEach(Sub(item) item.SetStatus = 2)

            For Each item In CType(INDgcDetail.DataSource, List(Of ViewListDashboardRequestMixingStationDetailXpo)).ToList()
                If (From x In listItems Where x.Equals(item) Select x).Count = 0 Then
                    item.SetStatus = 3
                    listItems.Add(item)
                End If
            Next
        Else
            listItems = CType(INDgcDetail.DataSource, List(Of ViewListDashboardRequestMixingStationDetailXpo))
            listItems.ForEach(Sub(item) item.SetStatus = 3)
        End If

        If listItems IsNot Nothing AndAlso listItems.Count > 0 Then
            For Each item In listItems
                Dim detail As Object = New ExpandoObject()
                detail.RequestMixingStationDetailPatientsId = item.RequestMixingStationDetailPatientsId
                detail.RequestMixingStationDetailId = item.RequestMixingStationDetailId
                detail.RequestType = ViewListDashboardRequestMixingStationXpo.RequestType
                detail.Status = item.SetStatus
                detail.CodeSusceptibleMixingStation = item.CodeSusceptibleMixingStation
                myListDetail.Add(detail)
            Next
        End If

        args.Details = myListDetail
        Return args
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardRequestMixingStationDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmAnnular)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True

        If (From x In Items Where x.RequestType = 1).Count > 0 Then
            INDcolBed.Visible = True
            INDcolAdministrationRoute.Visible = True
            INDcolPatient.Visible = True
        End If
        setGroupText()
        Presenter = New PDashboardRequestMixingStation
        GetDetails()
    End Sub

    Private Sub setGroupText()
        Dim groupByRequestCode = (From so In Items Group so By so.RequestCode Into Group
                                  Select New ViewListDashboardRequestMixingStationXpo With {.RequestCode = Group(0).RequestCode}).ToList()
        Dim groupResults = (From x In groupByRequestCode Select x.RequestCode).ToList()
        Dim stringsCodes = String.Join(",", groupResults.ToArray())
        Dim text As String = stringsCodes

        If stringsCodes.Length > 20 Then text = $"{stringsCodes.Substring(0, 20)}..."

        INDlygPrincipalInformation.Text = $"No. solicitud: {text}"
        INDlygPrincipalInformation.OptionsToolTip.ToolTip = $"No. solicitud: {stringsCodes}"
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardRequestMixingStationDetail_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "KeyDown"

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardRequestMixingStationDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Click confirmar
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Click anular
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Guardar(True)
    End Sub

#End Region

End Class
#Region "Imports"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.MaintenanceRepository
Imports Presentation.Base
Imports Presentation.Maintenance.MVP

#End Region

Public Class FrmAssignResponsible

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim _presenter As PDashboardMaintenance

    ''' <summary>
    ''' Catalogos de los items seleccionados
    ''' </summary>
    Dim _itemCatalogFilters As String

    ''' <summary>
    ''' Identifica el tipo de proceso que se esta realizando
    ''' </summary>
    Public ProcessType As Byte

    ''' <summary>
    ''' Rol del responsable actual seleccionado
    ''' </summary>
    Public ResponsibleRole As Byte

    ''' <summary>
    ''' Entidad que se envia desde los diferentes procesos
    ''' </summary>
    Public ListViewListRequestsXpo As List(Of ViewListRequestsXpo)

#End Region

#Region "Event"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

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
    ''' Guarda una reasignación de usuarios
    ''' </summary>
    Private Async Sub Guardar()
        If INDsleMaintenanceResponsible.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un usuario"
            Exit Sub
        End If

        Dim ListWorkOrder As New List(Of WorkOrder)
        For Each ViewListRequestsXpo In ListViewListRequestsXpo
            Dim WorkOrder = New WorkOrder
            With WorkOrder
                .ProcessType = ProcessType
                If ViewListRequestsXpo.WorkOrderId IsNot Nothing Then
                    .Id = ViewListRequestsXpo.WorkOrderId
                    .Consecutive = ViewListRequestsXpo.WorkOrderCode
                End If
                .OperatingUnitId = ViewListRequestsXpo.OperatingUnitId
                .BranchOfficeId = ViewListRequestsXpo.BranchOfficeId
                .PhysicalAssetId = ViewListRequestsXpo.PhysicalAssetId
                .MaintenanceResponsibleId = INDsleMaintenanceResponsible.EditValue
                .RequestDate = ViewListRequestsXpo.DateFailure
                .ProgramDate = ViewListRequestsXpo.ProgramDate
                .Description = ViewListRequestsXpo.Description
                .State = 1
                .EntityId = ViewListRequestsXpo.Id
                .EntityCode = ViewListRequestsXpo.Code
                .EntityName = "MaintenanceFailureRequest"
            End With
            ListWorkOrder.Add(WorkOrder)
        Next

        Try
            Using model As New MWorkOrder("")
                Me.AsyncLoader(True)
                Dim result = Await model.SaveWorkOrderAsync(ListWorkOrder)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    RaiseEvent ReturnModalArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _presenter = New PDashboardMaintenance()
        If Me.ListViewListRequestsXpo IsNot Nothing Then
            Dim itemCatalogs = Me.ListViewListRequestsXpo.GroupBy(Function(r) r.ItemCatalogId).OrderBy(Function(g) g.Key).Select(Function(g) g.Key)
            Me._itemCatalogFilters = String.Join(",%,", itemCatalogs)
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleINDsleScheduleTemplateUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMaintenanceResponsible.QueryPopUp
        If INDsleMaintenanceResponsible.Properties.DataSource Is Nothing Then
            INDsleMaintenanceResponsible.Properties.DataSource = _presenter.InitializeListMaintenanceResponsible(ResponsibleRole, _itemCatalogFilters)
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de plantilla de turnos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleINDsleScheduleTemplateUsers_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleMaintenanceResponsible.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(2210, Nothing, True)
            INDsleMaintenanceResponsible.Properties.DataSource = _presenter.InitializeListMaintenanceResponsible(ResponsibleRole, _itemCatalogFilters)
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignUser_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleMaintenanceResponsible.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        Guardar()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignUser_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleINDsleScheduleTemplateUsers_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleMaintenanceResponsible.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAccept.Focus()
        End If
    End Sub

#End Region

#End Region

End Class
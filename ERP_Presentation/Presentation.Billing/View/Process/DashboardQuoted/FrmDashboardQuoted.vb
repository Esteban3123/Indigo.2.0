'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/07/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Text
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP

#End Region

Public Class FrmDashboardQuoted

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardQuoted

    ''' <summary>
    ''' Variable que se utiliza para obtener los códigos de centro de atención y realizar las consulta con los filtros
    ''' </summary>
    Private CareCenterFilters As String

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
    ''' Carga las solicitudes
    ''' </summary>
    Private Sub LoadInformation()
        If INDgcInformation.DataSource IsNot Nothing OrElse (INDddbMenuType.Text <> "A" AndAlso INDddbMenuType.Text <> "I") Then
            Exit Sub
        End If
        INDviewInformation.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListViewDashboardQuotedHospitable(CareCenterFilters, INDddbMenuType.Text)
                                  INDgcInformation.BeginInvoke(Sub()
                                                                   INDgcInformation.DataSource = result
                                                                   INDviewInformation.HideLoadingPanel()
                                                               End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        INDgcInformation.DataSource = Nothing
        LoadInformation()
    End Sub

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        'Se valida si el usuario tiene permiso para mostrar los check del search de centro de atención
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        INDviewSearchCareCenter.OptionsSelection.MultiSelect = If(BarraBotones.PermissionsForm.ContainsKey(96), True, False)

        Dim ListActions As New List(Of eAcciones)

        If BarraBotones.PermissionsForm.ContainsKey(106) Then 'si tiene permiso de generar cotización
            ListActions.Add(eAcciones.GenerateQuoted)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(107) Then 'si tiene permiso de asociar cotización
            ListActions.Add(eAcciones.AssociateQuoted)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(108) Then 'si tiene permiso de no cotizar
            ListActions.Add(eAcciones.NotQuoted)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(7) Then 'si tiene permiso de confirmar
            ListActions.Add(eAcciones.ConfirmItem)
        End If

        If ListActions.Count > 0 Then 'si hay menu se asigna al gridview 
            IndigoGridView1.SetListAcction(INDviewInformation, ListActions)

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewInformation.Columns
                If col.Name = "colactions" Then
                    col.Width = 100
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Método que abre el form de cotizaciones
    ''' </summary>
    Private Sub OpenFormQuotation()
        'Se obtienen los registros seleccionados de la rejilla para validar que se haya seleccionado registros del mismo paciente
        Dim list = (From x In INDviewInformation.GetSelectedRows() Where INDviewInformation.IsGroupRow(x) = False Select CType(INDviewInformation.GetRow(x), ViewDashboardQuotedHospitableXpo)).ToList()
        If list.Count = 0 Then
            list = {CType(INDviewInformation.GetFocusedRow(), ViewDashboardQuotedHospitableXpo)}.ToList()
        End If

        'Se valida que hayan escogido el mismo paciente para realizar la cotización
        Dim patientCode = list(0).PatientCode
        If (From x In list Where x.PatientCode = patientCode).Count <> list.Count Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede generar la cotización porque se ha seleccionado diferentes pacientes"
            Exit Sub
        End If

        'Se valida si algun item ya seleccionado tiene una cotización
        Dim listQuoted = (From x In list Where x.QuotationId <> Nothing AndAlso x.QuotationId > 0 Select x).ToList()
        If listQuoted IsNot Nothing AndAlso listQuoted.Count > 0 Then
            Dim message As New StringBuilder
            listQuoted.ForEach(Sub(item) message.AppendLine("El servicio seleccionado " + item.ServiceCode + " ya tiene una cotización asociada"))
            Mensaje(EeventViewerImages.Advertencia) = message.ToString()
            Exit Sub
        End If

        Using formulario As New FrmQuotation()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.FlagDashboardQuoted = True
            formulario.CodeAdmission = If(list?.Count > 1, String.Empty, list?.FirstOrDefault.AdmissionNumber)
            formulario.PatientCode = patientCode
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 900
            formulario.Height = 800
            formulario.IsDashboardQuoted = True
            formulario._quotationType = If(list?.FirstOrDefault?.ItemType = "I", 1, 2)
            formulario.ListDetailsDashboardQuoted = list
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al abrir el form de cotizaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnModalArgs(sender As Object, e As QuotationEventArgs)
        Guardar(1, e)
    End Sub

    ''' <summary>
    ''' Método para asignar el estado de no cotizado al item
    ''' </summary>
    Private Async Sub Guardar(actionStatus As Integer, Optional e As QuotationEventArgs = Nothing)
        If e IsNot Nothing AndAlso e.Quotation.Status = 3 Then
            BeginReloadDatasource()
            Exit Sub
        End If

        'Se obtienen los registros seleccionados de la rejilla
        Dim list = (From x In INDviewInformation.GetSelectedRows() Where INDviewInformation.IsGroupRow(x) = False Select CType(INDviewInformation.GetRow(x), ViewDashboardQuotedHospitableXpo)).ToList()
        If list.Count = 0 Then
            list = {CType(INDviewInformation.GetFocusedRow(), ViewDashboardQuotedHospitableXpo)}.ToList()
        End If

        'Si se va a confirmar se valida que tenga asociada una cotización en estado confirmada
        If actionStatus = 2 Then
            Dim listTemp = (From x In list Where x.QuotationId = Nothing OrElse x.QuotationId = 0 Select x).ToList()
            If listTemp IsNot Nothing AndAlso listTemp.Count > 0 Then
                Dim message As New StringBuilder
                listTemp.ForEach(Sub(item) message.AppendLine("El servicio seleccionado " + item.ServiceCode + " no tiene una cotización asociada"))
                Mensaje(EeventViewerImages.Advertencia) = message.ToString()
                Exit Sub
            End If

            listTemp = (From x In list Where x.QuotationId <> Nothing AndAlso x.QuotationId > 0 AndAlso x.QuotationStatus <> 2 Select x).ToList()
            If listTemp IsNot Nothing AndAlso listTemp.Count > 0 Then
                Dim message As New StringBuilder
                listTemp.ForEach(Sub(item) message.AppendLine("El servicio seleccionado " + item.ServiceCode + " tiene asociada una cotización sin confirmar"))
                Mensaje(EeventViewerImages.Advertencia) = message.ToString()
                Exit Sub
            End If
        End If

        'Se recorre el listado para enviar la info a los servicios
        Dim listDashboardQuoted As New List(Of DashboardQuoted)
        For Each item In list
            Dim dashboardQuoted As New DashboardQuoted
            With dashboardQuoted
                .Id = item.DashboardQuotedId
                .AdmissionNumber = item.AdmissionNumber
                .ServiceCode = item.ServiceCode
                .Type = item.ServiceType
                .PatientCode = item.PatientCode
                .CareCenterCode = item.CareCenterCode
                .RequestDate = item.RequestDate
                .RequestQuantity = item.Quantity
                .FunctionalUnitCode = item.FunctionalUnitCode
                .EntityId = item.EntityId
                .EntityName = item.EntityName
                .CareGroupId = item.CareGroupId
                .HealthAdministratorId = item.HealthAdministratorId
                .ProfessionalCode = item.ProfessionalCode
                .QuotationId = If(e IsNot Nothing, e.Quotation.Id, item.QuotationId)
                .Status = actionStatus
            End With
            listDashboardQuoted.Add(dashboardQuoted)
        Next

        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardQuoted("")
                Dim result = Await model.SP_SaveDashboardQuoted(listDashboardQuoted)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    BeginReloadDatasource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que abre el form modal para asociar una cotización
    ''' </summary>
    Private Sub OpenFormAssociateQuotation()
        'Se obtienen los registros seleccionados de la rejilla para validar que se haya seleccionado registros del mismo paciente
        Dim list = (From x In INDviewInformation.GetSelectedRows() Where INDviewInformation.IsGroupRow(x) = False Select CType(INDviewInformation.GetRow(x), ViewDashboardQuotedHospitableXpo)).ToList()
        If list.Count = 0 Then
            list = {CType(INDviewInformation.GetFocusedRow(), ViewDashboardQuotedHospitableXpo)}.ToList()
        End If

        'Se valida que hayan escogido el mismo paciente para realizar la cotización
        Dim patientCode = list(0).PatientCode
        If (From x In list Where x.PatientCode = patientCode).Count <> list.Count Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede asociar una cotización porque se ha seleccionado diferentes pacientes"
            Exit Sub
        End If

        'Se valida si algun item ya seleccionado tiene una cotización
        Dim listQuoted = (From x In list Where x.QuotationId <> Nothing AndAlso x.QuotationId > 0 Select x).ToList()
        If listQuoted IsNot Nothing AndAlso listQuoted.Count > 0 Then
            Dim message As New StringBuilder
            listQuoted.ForEach(Sub(item) message.AppendLine("El servicio seleccionado " + item.ServiceCode + " ya tiene una cotización asociada"))
            Mensaje(EeventViewerImages.Advertencia) = message.ToString()
            Exit Sub
        End If

        Using Formulario As New FrmImportQuotation()
            AddHandler Formulario.ImportEvent, AddressOf ImportInfo
            Formulario.PatientCode = patientCode
            Formulario.ListCupsEntityId = (From x In list Select x.ServiceId).ToList()
            Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1054
            Formulario.Height = 500
            Formulario.IsDashboardQuoted = True
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que se ejecuta aceptar la importación de la cotización
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub ImportInfo(e As AddImportQuotationServiceOrderDetail)
        If e.ListQuotationServiceOrderDetailXpo IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo.Count > 0 Then
            Dim args As New QuotationEventArgs
            Dim quotation As New Quotation
            quotation.Id = e.ListQuotationServiceOrderDetailXpo(0).QuotationId.Id
            args.Quotation = quotation
            Guardar(1, args)
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardQuoted_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetActionsColumns()
        Me.ToolBar.Hide()
        Presenter = New PDashboardQuoted()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardQuoted_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareCenter.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleCareCenter.Size.Width - 11, 0)
        INDsleCareCenter.Properties.PopupFormSize = New System.Drawing.Size(INDsleCareCenter.Size.Width - 11, 0)
        INDsleCareCenter.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If INDsleCareCenter.Properties.DataSource Is Nothing Then
            INDsleCareCenter.Properties.DataSource = Presenter.InitializeCareCenter()
        End If
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el search de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDsleCareCenter.CloseUp
        'Se obtienen los códigos de centro de atención seleccionados
        CareCenterFilters = String.Join(",", (From x In INDviewSearchCareCenter.GetSelectedRows() Select "'" & DirectCast(INDviewSearchCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.CODCENATE & "'"))

        'Se valida si seleccionaron items del search
        If String.IsNullOrEmpty(CareCenterFilters) Then
            INDsleCareCenter.Properties.NullText = "Seleccione un Centro de Atención"
        Else
            INDsleCareCenter.Properties.NullText = INDviewSearchCareCenter.GetSelectedRows().Count().ToString() + " Item Seleccionados"
        End If

        BeginReloadDatasource()
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "GenerateQuoted"
                OpenFormQuotation()
            Case "AssociateQuoted"
                OpenFormAssociateQuotation()
            Case "NotQuoted"
                Guardar(3)
            Case "ConfirmItem"
                Guardar(2)
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "GenerateQuoted"
                OpenFormQuotation()
            Case "AssociateQuoted"
                OpenFormAssociateQuotation()
            Case "NotQuoted"
                Guardar(3)
            Case "ConfirmItem"
                Guardar(2)
        End Select
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Evento que se dispara al presionar refrescar del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar intrahospitalario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarIntraHospitable_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarIntraHospitable.ItemClick
        INDddbMenuType.Text = "I"
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar ambulatorio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarAmbulatory_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarAmbulatory.ItemClick
        INDddbMenuType.Text = "A"
        BeginReloadDatasource()
    End Sub

#End Region

#End Region

End Class
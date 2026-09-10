'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/03/2021
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
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Repository
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmDetailRequest

    Public Sub New()
        InitializeComponent()
        emptyEditor = New RepositoryItemButtonEdit()
        emptyEditor.Buttons.Clear()
        emptyEditor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        INDgcDetails.RepositoryItems.Add(emptyEditor)
    End Sub

#Region "Variables"

    ''' <summary>
    ''' items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedItems As List(Of ViewListDetailPatientsXpo)
        Get
            Dim selected = INDviewDetails.GetSelectedRows()
            Dim focused = INDviewDetails.GetFocusedObject(Of ViewListDetailPatientsXpo)

            If Not selected.Any() AndAlso focused IsNot Nothing Then
                Return {focused}.ToList()
            ElseIf selected IsNot Nothing Then
                Return INDviewDetails.GetSelectedRows() _
                   .Where(Function(m) Not INDviewDetails.IsGroupRow(m)) _
                   .Select(Function(m) CType(INDviewDetails.GetRow(m), ViewListDetailPatientsXpo)) _
                   .ToList()
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PCampaigns

    ''' <summary>
    ''' Obtiene los permisos del usuario para el formulario principal
    ''' </summary>
    Public PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' String de ids que representan a los detalles agrupados, son los ids de los pacientes
    ''' </summary>
    Public StringIds As String

    ''' <summary>
    ''' String de ids que representan a los detalles agrupados, son los ids de los pacientes
    ''' </summary>
    Public RequestMixingStationDetailId As Integer

    ''' <summary>
    ''' Variable que almacena el estado de la campaña
    ''' </summary>
    Public CampaignStatus As Byte?

    ''' <summary>
    ''' estado interno de la campaña, variable necesaria para validacion si debe gestionar materia prima
    ''' </summary>
    Public InternalState As Byte?
#End Region

#Region "Events"

#Region "Event"

    ''' <summary>
    ''' Evento para cargar nuevamente la campaña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ReloadCampaignArgs(sender As Object, e As EventArgs)

#End Region

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

    ''' <summary>
    ''' Establece el texto de la ventan
    ''' </summary>
    Public WriteOnly Property SetTitleWindow() As String
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Establce el texto del grupo
    ''' </summary>
    Public WriteOnly Property SetLabelGroup() As String
        Set(value As String)
            INDlygDetails.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Formulario padre, donde se encuentra alojado el control
    ''' </summary>
    Public Property FormOwner As ICampaign
    Public Property IsWorkingAreaAssigned As Boolean

#End Region

#Region "Methods"

    ''' <summary>
    ''' Indica si se muestra el progressbar
    ''' </summary>
    ''' <param name="isAsyncOperation">Valor que indica si se esta realizando una operación asíncrona</param>
    Private Sub IsAsyncOperation(Optional ByVal isAsyncOperation As Boolean = True)
        If isAsyncOperation Then
            INDpanelProgressbar.Dock = DockStyle.Top
            INDlygDetails.Enabled = False
            INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlygDetails.Enabled = True
            INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDpanelProgressbar.Dock = DockStyle.None
        End If
    End Sub

    ''' <summary>
    ''' Consulta el paquete
    ''' </summary>
    Private Sub GetDetail()
        INDgcDetails.DataSource = Nothing
        INDviewDetails.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = Presenter.ListViewListDetailPatients(RequestMixingStationDetailId)
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetails.SafeInvoke(Sub()
                                                                      SetShoworHideColumn(result)
                                                                      INDviewDetails.HideLoadingPanel()
                                                                      INDgcDetails.DataSource = result.GroupBy(Function(m) m.BatchCode).Select(Function(m) m.FirstOrDefault())

                                                                      Dim actions = INDviewDetails.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
                                                                      If actions IsNot Nothing Then
                                                                          If result IsNot Nothing AndAlso result.All(Function(m) {3, 5, 6}.Contains(m.Status)) OrElse Not result.Any(Function(x) x.HasProductionDeffect) Then
                                                                              actions.HideColumn()
                                                                          Else
                                                                              actions.ShowColumn()
                                                                          End If
                                                                      End If
                                                                  End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetails.SafeInvoke(Sub()
                                                                      INDviewDetails.HideLoadingPanel()
                                                                      Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                  End Sub)
                                      End If
                                  End Try
                              End Sub, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns(Optional CampaignS As Byte? = Nothing)
        Dim ListActions As New List(Of eAcciones)

        If PermissionsForm.ContainsKey(8) Then 'si tiene permiso de anular
            ListActions.Add(eAcciones.Annular)
        End If

        If CampaignS IsNot Nothing AndAlso CampaignS = 5 Then
            ListActions.Add(eAcciones.ManageRawMaterial)
            ListActions.Add(eAcciones.ValidateTag)
            ListActions.Add(eAcciones.Observation)
            ListActions.Add(eAcciones.ProcessfinishedProduct)
            ListActions.Add(eAcciones.DefectRegister)
        End If

        If ListActions.Count > 0 Then 'si hay menu se asigna al gridview 
            IndigoGridView1.SetListAcction(INDviewDetails, ListActions)

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewDetails.Columns
                If col.Name = "colActions" Then
                    col.Width = 100
                    col.Visible = True
                End If
            Next
        End If
    End Sub

    Private Sub SetShoworHideColumn(Result As List(Of ViewListDetailPatientsXpo))
        INDviewDetails.BeginUpdate()
        If Result Is Nothing OrElse Result.Count = 0 Then
            INDviewDetails.EndUpdate()
            Exit Sub
        End If

        HideAllColumns()

        If CampaignStatus <> 1 Then
            INDColBatchCode.ShowColumn(0)
        End If

        If Result.FirstOrDefault.RequestMixingStationDetailPatientsId IsNot Nothing Then
            INDColPatientCodeName.ShowColumn(1)
            INDColFunctionalUnitCodeName.ShowColumn(2)
            INDColBed.ShowColumn(3)
            INDColAdministrationRouteCodeName.ShowColumn(4)
            INDColStatusNameHCPRESCRA.ShowColumn(5)
            INDColDoseType.ShowColumn(6)
            INDColRequestType.ShowColumn(7)
            INDColTagType.ShowColumn(8)
            INDColObservations.ShowColumn(9)
            INDColStatus.ShowColumn(10)
            INDColInfo.ShowColumn(11)
        ElseIf Not String.IsNullOrEmpty(Result.FirstOrDefault.ATCCode) Then
            INDColATCCode.ShowColumn(1)
            INDColATCName.ShowColumn(2)
            INDColDoseType.ShowColumn(3)
            INDColRequestType.ShowColumn(4)
            INDColTagType.ShowColumn(5)
            INDColObservations.ShowColumn(6)
            INDColStatus.ShowColumn(7)
            INDColInfo.ShowColumn(8)
        Else
            INDColCodePackage.ShowColumn(1)
            INDColNamePackage.ShowColumn(2)
            INDColDoseType.ShowColumn(3)
            INDColRequestType.ShowColumn(4)
            INDColTagType.ShowColumn(5)
            INDColObservations.ShowColumn(6)
            INDColStatus.ShowColumn(7)
            INDColInfo.ShowColumn(8)
        End If
        INDviewDetails.EndUpdate()
    End Sub

    Private Sub HideAllColumns()
        INDColBatchCode.HideColumn()
        INDColPatientCodeName.HideColumn()
        INDColFunctionalUnitCodeName.HideColumn()
        INDColBed.HideColumn()
        INDColAdministrationRouteCodeName.HideColumn()
        INDColStatusNameHCPRESCRA.HideColumn()
        INDColCodePackage.HideColumn()
        INDColNamePackage.HideColumn()
        INDColATCCode.HideColumn()
        INDColATCName.HideColumn()
        INDColStatus.HideColumn()
    End Sub

    ''' <summary>
    ''' Anula los pacientes
    ''' </summary>
    Public Async Sub AnnularPatients()
        If MessageIndigo.Show("Desea anular los registros seleccionados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim args = AssigningValues()
        IsAsyncOperation()
        Try
            Using model As New MCampaign("")
                Dim result = Await model.AnnulatePatients(args)
                IsAsyncOperation(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    RaiseEvent ReloadCampaignArgs(Nothing, Nothing)
                    GetDetail()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            IsAsyncOperation(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método para Gestionar materia prima a Paquete Elaborado 
    ''' </summary>
    Private Sub OpenFormManageRawMaterial()
        If INDBindRequest() = False Then Exit Sub
        Using formulario As New FrmManageRawMaterial()
            'Se obtienen los registros seleccionados
            Dim listItems = (From x In INDviewDetails.GetSelectedRows() Where Not INDviewDetails.IsGroupRow(x) Select DirectCast(INDviewDetails.GetRow(x), ViewListDetailPatientsXpo)).ToList()
            Dim productsPackageIds As List(Of Integer) = (From t In listItems Select t.Id).ToList()
            formulario.RequestMixingStationDetailId = RequestMixingStationDetailId
            formulario.PermissionsForm = PermissionsForm
            formulario.Quantity = listItems.Count
            formulario.productPackageIds = productsPackageIds
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            AddHandler formulario.ActionCompleted, AddressOf GetDetail
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' validar control.
    ''' </summary>
    ''' <returns></returns>
    Public Function INDBindRequest() As Boolean
        Dim listItems = (From x In INDviewDetails.GetSelectedRows() Where Not INDviewDetails.IsGroupRow(x) Select DirectCast(INDviewDetails.GetRow(x), ViewListDetailPatientsXpo)).ToList()
        'Si se selecciona al menos un item que tenga orden de produccion
        If InternalState <> 4 Then
            Mensaje(EeventViewerImages.Informacion) = "La validación de lotes debe haber culminado con por lo menos una Orden de traslado"
            Return False
        End If
        If listItems.Any(Function(i) i.CampaignRawMaterialExist > 0) Then
            Mensaje(EeventViewerImages.Informacion) = "Ya existe paquetes con gestión de materia prima"
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Function AssigningValues() As Object
        Dim args As Object = New ExpandoObject()
        Dim myListDetail As New ConcurrentBag(Of Object)()

        Dim listItems = (From x In INDviewDetails.GetSelectedRows() Where Not INDviewDetails.IsGroupRow(x) Select DirectCast(INDviewDetails.GetRow(x), ViewListDetailPatientsXpo)).ToList()

        If listItems IsNot Nothing AndAlso listItems.Count > 0 Then
            For Each item In listItems
                Dim detail As Object = New ExpandoObject()
                detail.RequestPackageDetailStatusId = item.Id
                detail.RequestMixingStationDetailPatientsId = item.RequestMixingStationDetailPatientsId
                detail.RequestMixingStationDetailId = item.RequestMixingStationDetailId
                detail.Status = 6
                myListDetail.Add(detail)
            Next
        End If

        args.Details = myListDetail
        Return args
    End Function

    ''' <summary>
    ''' Funcion para asignar el usuario y la fecha de la verificacion de la etiqueta
    ''' </summary>
    ''' <param name="tagGrid"></param>
    Private Async Sub ValidateTag(tagGrid As String)
        If String.IsNullOrEmpty(tagGrid) Then
            Exit Sub
        End If

        If MessageIndigo.Show("Desea verificar la etiqueta?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Try
            Dim selected = SelectedItems
            Dim idList = selected.Select(Function(m) m.Id).ToList()
            Dim listRequestPackageDetailStatus = Await AssignValuesByIds(idList, Nothing, tagGrid)

            If listRequestPackageDetailStatus Is Nothing OrElse listRequestPackageDetailStatus.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se han encontrado registro para actualizar, verifique e intente nuevamente"
                Exit Sub
            End If

            If selected.Any(Function(m) Not m.LabelType.HasValue) Then
                Mensaje(EeventViewerImages.Advertencia) = "Hay paquetes seleccionados a los cuales no se le ha gestionado la etiqueta"
                Exit Sub
            End If

            Dim Result = Await SavePackageDetailStatus(listRequestPackageDetailStatus)

            If Result.StateResult Then
                Mensaje(EeventViewerImages.Informacion) = "Proceso realizado con éxito"
            Else
                Mensaje(EeventViewerImages.Advertencia) = Result.Message
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Funcion para guardar o actualizar la observacion por producto
    ''' </summary>
    ''' <param name="Observations"></param>
    Private Async Sub AssignObservations(Observations As String)
        If Observations = Nothing Then
            Exit Sub
        End If

        If MessageIndigo.Show("Desea agregar la observación? Si ya tiene una, esta será reemplazada", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Try
            Dim IdList = (From x In INDviewDetails.GetSelectedRows() Where Not INDviewDetails.IsGroupRow(x) Select DirectCast(INDviewDetails.GetRow(x), ViewListDetailPatientsXpo).Id).ToList()

            Dim ListRequestPackageDetailStatus = Await AssignValuesByIds(IdList, Observations)

            If ListRequestPackageDetailStatus Is Nothing OrElse ListRequestPackageDetailStatus.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se han encontrado registro para actualizar, verifique e intente nuevamente"
                Exit Sub
            End If

            Dim Result = Await SavePackageDetailStatus(ListRequestPackageDetailStatus)

            If Result.StateResult Then
                RaiseEvent ReloadCampaignArgs(Nothing, Nothing)
                GetDetail()
                Mensaje(EeventViewerImages.Informacion) = "Proceso Realizado con Exito"
            Else
                Mensaje(EeventViewerImages.Advertencia) = Result.Message
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' funcion para asignar valores para guardar o actualizar la Entidad
    ''' </summary>
    ''' <param name="ids"></param>
    ''' <param name="Observation"></param>
    ''' <param name="tagGrid"></param>
    ''' <returns></returns>
    Private Async Function AssignValuesByIds(ids As List(Of Integer), Optional Observation As String = Nothing, Optional tagGrid As String = Nothing) As Task(Of List(Of RequestPackageDetailStatus))
        Try
            Using model As New MCampaign("")
                Dim requetsPackageStatus = Await model.GetRequestMixingDetailStatusByIdsAsync(ids)

                If requetsPackageStatus Is Nothing OrElse Not requetsPackageStatus.Any() Then
                    Return New List(Of RequestPackageDetailStatus)
                End If

                requetsPackageStatus.ForEach(Sub(x As RequestPackageDetailStatus)
                                                 With x
                                                     If Not String.IsNullOrEmpty(tagGrid) AndAlso tagGrid = "ProcessfinishedProduct" Then
                                                         .Status = 2
                                                     End If
                                                     If Not String.IsNullOrEmpty(Observation) Then
                                                         .Observations = Observation
                                                     End If
                                                     If Not String.IsNullOrEmpty(tagGrid) AndAlso (tagGrid = "ValidateTag" Or (tagGrid = "ProcessfinishedProduct" And x.VerificationTagUser Is Nothing And x.VerificationTagDate Is Nothing)) Then
                                                         .VerificationTagUser = Presenter._sessionValues.UserIndigo
                                                         .VerificationTagDate = Date.Now()
                                                     End If
                                                 End With
                                             End Sub)
                Return requetsPackageStatus
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            Return New List(Of RequestPackageDetailStatus)
        End Try
    End Function


    ''' <summary>
    '''Actualiza el registro de RequestPackageDetailStatus
    ''' </summary>
    ''' <param name="ListRequestPackageDetailStatus"></param>
    ''' <returns></returns>
    Private Async Function SavePackageDetailStatus(ListRequestPackageDetailStatus As List(Of RequestPackageDetailStatus)) As Task(Of Domain.Base.Entities.ActionResult)
        Try
            Using model As New MCampaign("")
                IsAsyncOperation()
                Dim result = Await model.SavePackageDetailStatus(Nothing, ListRequestPackageDetailStatus)
                IsAsyncOperation(False)
                Return result
            End Using
        Catch ex As Exception
            IsAsyncOperation(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            Return New Domain.Base.Entities.ActionResult With {.StateResult = False, .Message = ex.Message.ToString()}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para ejecutar la opcion de Procesar Producto terminado
    ''' </summary>
    ''' <param name="tagGrid"></param>
    Private Async Sub ProcessfinishedProduct(tagGrid As String)
        If MessageIndigo.Show("Esta seguro de cambiar a Estado Producto Terminado?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If InternalState <> 4 Then
            Mensaje(EeventViewerImages.Informacion) = "La validación de lotes debe haber culminado con por lo menos una Orden de traslado en estado Confirmado"
            Exit Sub
        End If

        Try
            ' se ejecuta la funcion para gestionar materia prima
            Dim AutomaticResult = Await _automaticManageRawMaterial()
            If AutomaticResult Is Nothing OrElse AutomaticResult.StateResult = False Then
                If Not String.IsNullOrEmpty(AutomaticResult.Message) Then
                    Mensaje(EeventViewerImages.Advertencia) = AutomaticResult.Message
                End If
                Exit Sub
            End If

            Dim ids = (From x In INDviewDetails.GetSelectedRows() Where Not INDviewDetails.IsGroupRow(x) Select DirectCast(INDviewDetails.GetRow(x), ViewListDetailPatientsXpo).Id).ToList()
            Dim ListRequestPackageDetailStatus = Await AssignValuesByIds(ids, Nothing, tagGrid)

            If ListRequestPackageDetailStatus Is Nothing OrElse ListRequestPackageDetailStatus.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se han encontrado registro para actualizar, verifique e intente nuevamente"
                Exit Sub
            End If

            Dim Result = Await SavePackageDetailStatus(ListRequestPackageDetailStatus)

            If Result.StateResult Then
                RaiseEvent ReloadCampaignArgs(Nothing, Nothing)
                GetDetail()
                Mensaje(EeventViewerImages.Informacion) = "Proceso Realizado con Exito"
            Else
                Mensaje(EeventViewerImages.Advertencia) = Result.Message
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Funcion para gestionar la materia prima automaticamente, re-utiliza metodos del formulario gestion de materia prima
    ''' </summary>
    ''' <returns></returns>
    Private Async Function _automaticManageRawMaterial() As Task(Of Domain.Base.Entities.ActionResult)
        Using formulario As New FrmManageRawMaterial()
            Dim listItems = (From x In INDviewDetails.GetSelectedRows() Where Not INDviewDetails.IsGroupRow(x) Select DirectCast(INDviewDetails.GetRow(x), ViewListDetailPatientsXpo)).ToList()
            Dim productsPackageIds As List(Of Integer) = (From t In listItems Where t.CampaignRawMaterialExist = 0 Select t.Id).ToList()
            IsAsyncOperation()
            If productsPackageIds.Count = 0 Then
                Return New Domain.Base.Entities.ActionResult With {.StateResult = True, .Message = "Todos los paquetes ya tenian Gestion de Materia Prima"}
            End If
            Return Await formulario.AutomaticManageRawMaterial(RequestMixingStationDetailId, PermissionsForm, productsPackageIds.Count, productsPackageIds)
            IsAsyncOperation(False)
        End Using
    End Function

    ''' <summary>
    ''' Defect classification popup
    ''' </summary>
    Private Sub OpenDefectClassificationPopup()
        Dim selected = SelectedItems

        If selected.Any(Function(m) m.Status <> 2) Then
            Mensaje(EeventViewerImages.Advertencia) = "Acción no disponible para productos no terminados"
            Return
        End If

        Using frm As New FrmPopupDefectClassification()
            frm.RequestPackageDetailStatusIds = selected?.Select(Function(m) m.Id)?.ToList()
            frm.UnitDoseTypeCodeName = selected(0).UnitDoseTypeCodeName
            frm.UnitDoseClass = selected(0).UnitDoseClass
            frm.BatchCodes = selected.Select(Function(f) f.BatchCode + ", ").ToList()
            frm.FormState = FrmPopupDefectClassification.eFormState.Production
            frm.Width = Screen.PrimaryScreen.WorkingArea.Width * 0.8
            frm.Height = Screen.PrimaryScreen.WorkingArea.Height * 0.9
            Dim tr As New FrmTransparent(frm, False)
            tr.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDpanelProgressbar.Dock = DockStyle.None
        Presenter = New PCampaigns()
        SetActionsColumns(CampaignStatus)
        'AddHandler IndigoGridView1.RepositoryItemPopupContainerEdit.QueryPopUp, AddressOf QueryPopupRepositoryActions
        GetDetail()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "MenuContext"
    'QueryPopUpActionButtons
    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim tagGrid As String = ""
        If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
            tagGrid = sender.Tag.ToString()
        Else
            tagGrid = sender.Text
        End If

        If CampaignStatus.HasValue AndAlso CampaignStatus = 5 AndAlso Not IsWorkingAreaAssigned Then
            Mensaje(EeventViewerImages.Advertencia) = "Liberación de Línea Pendiente para la Campaña"
            Return
        End If
        Dim _selectedItems = SelectedItems.Where(Function(x) {3, 5, 6}.Contains(x.Status)).ToList()

        If _selectedItems.Count > 0 Then
            If _selectedItems.Count = 1 Then
                Mensaje(EeventViewerImages.Advertencia) = $"Acción no permitida, el producto ya se encuentra en estado: {_selectedItems.First.StatusName}"
            Else
                Mensaje(EeventViewerImages.Advertencia) = $"Acción no permitida, hay productos en estado liberado o rechazado"
            End If
            Return
        End If

        Select Case tagGrid
            Case "Anular", "Annular"
                AnnularPatients()
            Case "Gestionar Materia Prima", "ManageRawMaterial"
                OpenFormManageRawMaterial()
            Case "Validar Etiqueta", "ValidateTag"
                ValidateTag(tagGrid)
            Case "Observaciones", "Observation"
                ShowObservationsPopup()
            Case "ProcessfinishedProduct"
                ProcessfinishedProduct(tagGrid)
            Case "DefectRegister"
                OpenDefectClassificationPopup()
        End Select
    End Sub

#End Region

#Region "PopupMenuShowing"
    ''' <summary>
    ''' PopupMenuShowing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewDetails_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewDetails.PopupMenuShowing
        IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
        Dim items = SelectedItems

        If items.All(Function(m) m.Status = 2) Then

            If items.All(Function(m) m.HasProductionDeffect) Then
                Dim buttonDefect = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.DefectRegister)))
                If buttonDefect IsNot Nothing And Not items.Any(Function(f) f.FlagQualityDefect) Then buttonDefect.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

        ElseIf items.All(Function(m) m.Status <> 2) Then
            Dim buttonManageRawMaterial = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ManageRawMaterial)))
            If buttonManageRawMaterial IsNot Nothing Then buttonManageRawMaterial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            Dim buttonValidateTag = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ValidateTag)))
            If buttonValidateTag IsNot Nothing Then buttonValidateTag.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            Dim buttonObservation = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Observation)))
            If buttonObservation IsNot Nothing Then buttonObservation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            Dim buttonProcessfinishedProduct = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ProcessfinishedProduct)))
            If buttonProcessfinishedProduct IsNot Nothing Then buttonProcessfinishedProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            Dim buttonAnnular = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Annular)))
            If items.All(Function(m) {4, 7}.Contains(If(m.StatusHCPRESCRA, 0)) OrElse m.FECALTPAC IsNot Nothing) Then
                buttonAnnular.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

            If items.All(Function(x) {3, 5, 6}.Contains(x.Status)) Then
                IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
            End If

        End If
    End Sub
#End Region

#Region "QueryPopPup"
    ''' <summary>
    ''' Query Popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPpceInfo_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDPpceInfo.QueryPopUp
        Dim selectedItem = INDviewDetails.GetFocusedObject(Of ViewListDetailPatientsXpo)()

        If selectedItem IsNot Nothing Then
            CtrMoreInfoElaborationParameter1.SetData(selectedItem.RequestMixingStationDetailId)
        End If
    End Sub
#End Region

    ''' <summary>
    ''' abre modal para escribir la observacion
    ''' </summary>
    Private Sub ShowObservationsPopup()
        Using formulario As New FrmPopupObservations()
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 800, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 500)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) = DialogResult.OK Then
                AssignObservations(formulario.Observations)
            End If
        End Using
    End Sub

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)
        Dim items = SelectedItems
        Dim count As Integer = 0

        If items.All(Function(m) m.Status <> 6 AndAlso m.Status <> 5 AndAlso m.Status <> 3) Then
            If items.All(Function(m) m.Status = 2) Then

                If items.All(Function(m) m.HasProductionDeffect) Then
                    Dim buttonDefect = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.DefectRegister)))
                    If buttonDefect IsNot Nothing AndAlso Not items.Any(Function(f) f.FlagQualityDefect) Then
                        buttonDefect.Visible = True
                        count += 1
                    End If
                End If

            ElseIf items.All(Function(m) m.Status <> 2) Then
                Dim buttonManageRawMaterial = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ManageRawMaterial)))
                If buttonManageRawMaterial IsNot Nothing Then
                    buttonManageRawMaterial.Visible = True
                    count += 1
                End If

                Dim buttonValidateTag = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ValidateTag)))
                If buttonValidateTag IsNot Nothing Then
                    buttonValidateTag.Visible = True
                    count += 1
                End If

                Dim buttonObservation = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Observation)))
                If buttonObservation IsNot Nothing Then
                    buttonObservation.Visible = True
                    count += 1
                End If

                Dim buttonProcessfinishedProduct = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ProcessfinishedProduct)))
                If buttonProcessfinishedProduct IsNot Nothing Then
                    buttonProcessfinishedProduct.Visible = True
                    count += 1
                End If
                If items.All(Function(m) {4, 7}.Contains(If(m.StatusHCPRESCRA, 0)) OrElse m.FECALTPAC IsNot Nothing) Then
                    Dim buttonAnnular = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Annular)))
                    If buttonAnnular IsNot Nothing Then
                        buttonAnnular.Visible = True
                        count += 1
                    End If
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Acción no permitida"
        End If

        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * count)
    End Sub

    Private emptyEditor As RepositoryItemButtonEdit

#End Region

End Class
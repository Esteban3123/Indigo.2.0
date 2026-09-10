#Region "Imports"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Imports Domain.Entities
Imports IndigoSingleton
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Presentation.Authorization.MVP
Imports Presentation.Base
Imports Presentation.Common
Imports Presentation.Controls

#End Region

Public Class FrmDashboardManagementMedicalOrder
    Implements IDashboardManagementMedicalOrder

#Region "Globals"

    ''' <summary>
    ''' Variables de sesión del EHR
    ''' </summary>
    Private _valoresSesion As IndigoValoresSesion

    ''' <summary>
    ''' Presentador del formulario
    ''' </summary>
    Private _presenter As PDashboardManagementMedicalOrder

    ''' <summary>
    ''' Splash de espera mientras se realiza un proceso
    ''' </summary>
    Private _waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)

    ''' <summary>
    ''' Id de la razón de cancelación
    ''' </summary>
    Private _cancellationReasonsId As Integer?

    ''' <summary>
    ''' Detalle de la razón de cancelación
    ''' </summary>
    Private _cancellationReasonsObservations As String

    Private lastCriteriaRequests As String

    Private CareCenterFilters As String

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

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

#Region "Datasource"

    Public Property CentersXpo As XPInstantFeedbackSource Implements IDashboardManagementMedicalOrder.CentersXpo
        Get
            Return INDSleCareCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCareCenter.Properties.DataSource = value
        End Set
    End Property

    Public Property ListRequestsForManagementMedicalOrderXpo As List(Of ViewListRequestsForManagementMedicalOrderXpo) Implements IDashboardManagementMedicalOrder.ListRequestsForManagementMedicalOrderXpo
        Get
            Return INDgcRequests.DataSource
        End Get
        Set(value As List(Of ViewListRequestsForManagementMedicalOrderXpo))
            INDgcRequests.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga las solicitudes
    ''' </summary>
    Private Sub LoadRequests(Optional loadData As Boolean = False)
        If INDgcRequests.DataSource IsNot Nothing And loadData = False Then
            Exit Sub
        End If
        INDgvRequests.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListRequestsForManagementMedicalOrderXpo) = Nothing
                                  Try
                                      Dim topRows As Integer = 50000
                                      lastCriteriaRequests = INDgvRequests.ActiveFilterString
                                      Dim filter As String = "CareCenterCode in (" & _selectorCareCenter.GetKeys & ")"

                                      If lastCriteriaRequests IsNot Nothing AndAlso lastCriteriaRequests <> "" Then
                                          filter = String.Format("{0} and {1}", filter, lastCriteriaRequests)
                                      End If

                                      result = _presenter.ListViewListRequestsForManagementMedicalOrder(filter, topRows)
                                      INDgcRequests.BeginInvoke(Sub()
                                                                    If (result Is Nothing OrElse Not result.Any()) AndAlso Not String.IsNullOrEmpty(lastCriteriaRequests) Then
                                                                        INDgvRequests.ActiveFilterString = String.Empty
                                                                        LoadRequests(True)
                                                                        Exit Sub
                                                                    End If
                                                                    INDgcRequests.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                    INDgvRequests.HideLoadingPanel()
                                                                End Sub)
                                  Catch ex As Exception
                                      INDgcRequests.BeginInvoke(Sub()
                                                                    INDgvRequests.HideLoadingPanel()
                                                                    Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                End Sub)
                                  End Try
                              End Sub)
    End Sub

    Private Sub INDviewRequests_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDgvRequests.ColumnFilterChanged
        If lastCriteriaRequests <> DirectCast(sender, GridView).ActiveFilterString Then
            LoadRequests(True)
        End If
    End Sub

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub LoadDatasource()
        If String.IsNullOrEmpty(_selectorCareCenter.GetKeys()) Then
            Exit Sub
        End If

        ListRequestsForManagementMedicalOrderXpo = Nothing
        LoadRequests()
    End Sub

    Private Sub CleanControls()
        _cancellationReasonsId = Nothing
        _cancellationReasonsObservations = Nothing
    End Sub

    ''' <summary>
    ''' Método que me devuelve cuando se cierra el modal de motivos de cancelación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnCancellationReasonArgs(sender As Object, e As AddCancellationReasonEventArgs)
        _cancellationReasonsId = e.CancellationReasonsId
        _cancellationReasonsObservations = e.CancellationReasonsObservations
        SaveCancellationReason(1)
    End Sub

    ''' <summary>
    ''' Guarda un motivo de cancelación
    ''' </summary>
    Private Async Sub SaveCancellationReason(Status As Byte)
        If ListRequestsForManagementMedicalOrderXpo IsNot Nothing AndAlso ListRequestsForManagementMedicalOrderXpo.Any(Function(d) d.SelectOption) Then
            Dim listManagementMedicalOrder As New List(Of ManagementMedicalOrder)
            For Each ViewListRequestsXpo In ListRequestsForManagementMedicalOrderXpo.Where(Function(d) d.SelectOption)
                listManagementMedicalOrder.Add(New ManagementMedicalOrder With
                {
                    .OperatingUnitId = BarraBotones.OperatingUnit.Id,
                    .EntityName = ViewListRequestsXpo.EntityName,
                    .EntityId = ViewListRequestsXpo.EntityId,
                    .CareCenterCode = ViewListRequestsXpo.CareCenterCode,
                    .FunctionalUnitCode = ViewListRequestsXpo.FunctionalUnitCode,
                    .AdmissionNumber = ViewListRequestsXpo.AdmissionNumber,
                    .Folio = ViewListRequestsXpo.Folio,
                    .PatientCode = ViewListRequestsXpo.PatientCode,
                    .ProfessionalCode = ViewListRequestsXpo.ProfessionalCode,
                    .RequestDate = ViewListRequestsXpo.RequestDate,
                    .RequestQuantity = ViewListRequestsXpo.Quantity,
                    .Type = ViewListRequestsXpo.Type,
                    .ItemCode = ViewListRequestsXpo.ItemCodeOriginal,
                    .CancellationReasonsId = _cancellationReasonsId,
                    .CancellationReasonsObservations = _cancellationReasonsObservations,
                    .Status = Status
                })
            Next

            Try
                Using model As New MDashboardManagementMedicalOrder(Me.Tag.ToString())
                    Me.AsyncLoader(True)
                    Dim result = Await model.SaveManagementMedicalOrder(listManagementMedicalOrder)
                    Me.AsyncLoader(False)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                        LoadDatasource()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    CleanControls()
                End Using
            Catch ex As Exception
                Me.AsyncLoader(False)
                Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        End If
    End Sub

    Private Sub PrintListWithdrawal()
        If Not ListRequestsForManagementMedicalOrderXpo.Any(Function(d) d.SelectOption AndAlso d.Status = 1) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud desistida"
            Exit Sub
        End If

        Try
            Dim reportDef As New Reporter.rptListWithdrawal
            AddHandler reportDef.AfterPrint, Sub()
                                                 If _waitForm.IsSplashFormVisible Then
                                                     _waitForm.CloseWaitForm()
                                                 End If
                                             End Sub
            ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, ListRequestsForManagementMedicalOrderXpo.Where(Function(d) d.SelectOption AndAlso d.Status = 1).ToList())
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            If _waitForm.IsSplashFormVisible Then
                _waitForm.CloseWaitForm()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Configurar y obtener un objeto openFileDialog
    ''' </summary>
    Private Sub GetOpenFileDialog(ByRef fileOpener As OpenFileDialog)
        fileOpener.CheckPathExists = True
        fileOpener.CheckFileExists = True
        'fileOpener.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.GIF)|*.bmp;*.jpg;*.jpeg;*.GIF|" + _
        '   "PNG files (*.png)|*.png|text files (*.text)|*.txt|doc files (*.doc)|*.doc|docx files (*.docx)|*.docx|pdf files (*.pdf)|*.pdf"
        fileOpener.Multiselect = False
        fileOpener.AddExtension = True
        fileOpener.ValidateNames = True
        'fileOpener.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        fileOpener.InitialDirectory = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder()
    End Sub

#Region "Multiselect"

    ''' <summary>
    ''' Metodo que se encarga de poner o no visible el check en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VisibleCheck()
        If ListRequestsForManagementMedicalOrderXpo IsNot Nothing AndAlso ListRequestsForManagementMedicalOrderXpo.Any Then
            If ListRequestsForManagementMedicalOrderXpo.Where(Function(item) item.SelectOption = True).Count = ListRequestsForManagementMedicalOrderXpo.Count Then
                Me.INDgvRequests_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.check
            Else
                Me.INDgvRequests_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona todo el grupo o todo el subGrupo
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptions(optionCheck As Integer, Optional selectGroupRow As Boolean = True)
        Dim view As GridView = INDgvRequests
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    If selectGroupRow Then
                        GetChildsRows(view, listHandlesSelected(i), optionCheck)
                    End If
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    row.SelectOption = optionCheck
                End If
            Next
        End If

        VisibleCheck()
    End Sub

    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, value As Decimal)
        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, value)
            Else
                Dim row As ViewListRequestsForManagementMedicalOrderXpo = view.GetRow(childHandle)
                row.SelectOption = If(value = 0, False, True)
            End If
        Next
    End Sub

#End Region

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardManagementMedicalOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Se valida si el usuario tiene permiso para mostrar los check del search de centro de atención
        BarraBotones.ActualizarPermisosBarra(Me.MyTag)

        'Inicializamos la referencia
        _presenter = New PDashboardManagementMedicalOrder(Me)
        _valoresSesion = IndigoValoresSesion.Instancia

        'De acuerdo con los permisos se permite o no seleccionar muchos centros de atención
        INDGvCareCenter.OptionsSelection.MultiSelect = If(BarraBotones.PermissionsForm.ContainsKey(96), True, False)
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardManagementMedicalOrder_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleCareCenter.Properties.PopupFormMinSize = New System.Drawing.Size(INDSleCareCenter.Size.Width - 11, 0)
        INDSleCareCenter.Properties.PopupFormSize = New System.Drawing.Size(INDSleCareCenter.Size.Width - 11, 0)
        INDSleCareCenter.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCareCenter.QueryPopUp
        _presenter.InitializeCareCenter()
    End Sub

#End Region

#Region "SelectionChanged"

    Private Sub INDgvRequests_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDgvRequests.SelectionChanged
        If e.Action = CollectionChangeAction.Remove OrElse e.Action = CollectionChangeAction.Refresh Then
            If ListRequestsForManagementMedicalOrderXpo IsNot Nothing Then
                For Each viewRequest In ListRequestsForManagementMedicalOrderXpo.Where(Function(d) d.SelectOption)
                    viewRequest.SelectOption = False
                Next
            End If
        End If
        SelectOptions(1, False)
    End Sub

#End Region

#Region "MouseDoubleClick"

    Private Sub INDgcRequests_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcRequests.MouseDoubleClick
        If ListRequestsForManagementMedicalOrderXpo IsNot Nothing AndAlso ListRequestsForManagementMedicalOrderXpo.Any() Then
            Dim hitPoint = Me.INDgvRequests.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDgvRequests_SelectOption") Then
                    Dim listFilterXpCollection = INDgvRequests.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDgvRequests_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        cont = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                        If cont = ListRequestsForManagementMedicalOrderXpo.Count Then
                            Me.INDgvRequests_SelectOption.Image = Global.Presentation.Authorization.My.Resources.Resources.check
                        End If
                    End If
                    Me.INDgcRequests.RefreshDataSource()
                    Me.INDgcRequests.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvRequests_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgvRequests.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then
            INDBbiSelection.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBbiUnSelection.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDBbiSelection.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDBbiUnSelection.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

        Dim quantitySelected As Integer = 0
        If ListRequestsForManagementMedicalOrderXpo IsNot Nothing AndAlso ListRequestsForManagementMedicalOrderXpo.Any(Function(d) d.SelectOption) Then
            quantitySelected = ListRequestsForManagementMedicalOrderXpo.Where(Function(d) d.SelectOption).Count
        End If

        INDbbiOpenRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        If quantitySelected = 1 Then
            INDbbiOpenRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        INDBbiConfirm.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        If quantitySelected > 0 AndAlso BarraBotones.PermissionsForm.ContainsKey(7) Then 'Si tiene permiso de confirmar
            If ListRequestsForManagementMedicalOrderXpo.Any(Function(d) d.SelectOption AndAlso d.Status = 0) Then
                INDBbiConfirm.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If

        INDBbiCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPrintWithdrawal.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiAttach.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        If quantitySelected > 0 AndAlso BarraBotones.PermissionsForm.ContainsKey(99) Then 'Si tiene permiso de cancelar
            If ListRequestsForManagementMedicalOrderXpo.Any(Function(d) d.SelectOption AndAlso d.Status <> 0) Then
                If ListRequestsForManagementMedicalOrderXpo.Any(Function(d) d.SelectOption AndAlso d.Status = 1) Then
                    INDBbiPrintWithdrawal.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    If quantitySelected = 1 Then
                        INDBbiAttach.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    End If
                End If
            Else
                INDBbiCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If

        INDBbiPrintMedicalRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPrintNursingRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPrintAccountSupport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiPrintOrders.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        If quantitySelected = 1 AndAlso BarraBotones.PermissionsForm.ContainsKey(23) Then 'Si tiene permiso de imprimir
            INDBbiPrintMedicalRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBbiPrintNursingRecord.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBbiPrintAccountSupport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBbiPrintOrders.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        Dim View = CType(sender, GridView)
        INDPopMenuActions2.Manager = BarManager2
        INDPopMenuActions2.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        LoadDatasource()
    End Sub

    Private Sub INDBbiSelection_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiSelection.ItemClick
        SelectOptions(1)
    End Sub

    Private Sub INDBbiUnselection_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiUnSelection.ItemClick
        SelectOptions(0)
    End Sub

    Private Sub INDbbiOpenRequest_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiOpenRequest.ItemClick
        'Se obtiene el registro que tiene el foco y al cual se va a realizar la cancelación
        Dim ViewListRequestsXpo = ListRequestsForManagementMedicalOrderXpo.FirstOrDefault(Function(d) d.SelectOption)
        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Using Formulario As New PopUpRequests(ViewListRequestsXpo.EntityName, ViewListRequestsXpo.EntityId, ViewListRequestsXpo.ItemCodeOriginal)
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 780
            Formulario.Height = 768
            Dim frm As New FrmTransparent(Formulario, False)
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub INDBbiConfirm_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiConfirm.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            SaveCancellationReason(3)
        End If
    End Sub

    Private Sub INDBbiCancel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiCancel.ItemClick
        Using formulario As New PopUpCancellationReasons()
            AddHandler formulario.ReturnCancellationReasonArgs, AddressOf ReturnCancellationReasonArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 400
            formulario.ToolBar.Visible = False
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub INDBbiPrintWithdrawal_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintWithdrawal.ItemClick
        PrintListWithdrawal()
    End Sub

    Private Async Sub INDBbiAttach_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiAttach.ItemClick
        'Se obtiene el registro que tiene el foco y al cual se va a realizar la cancelación
        Dim ViewListRequestsXpo = ListRequestsForManagementMedicalOrderXpo.FirstOrDefault(Function(d) d.SelectOption)
        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Try
            Dim fileOpener As New OpenFileDialog()
            GetOpenFileDialog(fileOpener)
            If (fileOpener.ShowDialog() = DialogResult.OK) Then
                Dim managementMedicalOrder As New ManagementMedicalOrder With
                {
                    .EntityName = ViewListRequestsXpo.EntityName,
                    .EntityId = ViewListRequestsXpo.EntityId
                }

                Dim fileInfo = New IO.FileInfo(fileOpener.FileName)
                Dim attachment As New Attachment With
                {
                    .FormId = 2178,
                    .EntityName = ViewListRequestsXpo.EntityName,
                    .EntityId = ViewListRequestsXpo.EntityId,
                    .Name = fileInfo.Name,
                    .Extension = fileInfo.Extension,
                    .Description = String.Format("Desistimiento {0}: {1}", If(ViewListRequestsXpo.Type = 1, "Servicio", "Producto"), ViewListRequestsXpo.ItemCodeName),
                    .FileAttached = System.IO.File.ReadAllBytes(fileOpener.FileName)
                }

                Using model As New MDashboardManagementMedicalOrder(Me.Tag.ToString())
                    Me.AsyncLoader(True)
                    Dim result = Await model.SaveManagementMedicalOrderWithdrawal(managementMedicalOrder, attachment)
                    Me.AsyncLoader(False)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                        LoadDatasource()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    CleanControls()
                End Using
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Sub INDBbiPrintMedicalRecord_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintMedicalRecord.ItemClick
        'Se obtiene el registro que tiene el foco y al cual se va a realizar la cancelación
        Dim ViewListRequestsXpo = ListRequestsForManagementMedicalOrderXpo.FirstOrDefault(Function(d) d.SelectOption)
        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Using Dialogo As New IndigoEmergentes.frmHCImpresionDialogo(ViewListRequestsXpo.PatientCode, ViewListRequestsXpo.AdmissionNumber, ViewListRequestsXpo.Folio, IndigoEmergentes.frmHCImpresionDialogo.eOrigen.Impresiones, ViewListRequestsXpo.TypeClinicalHistory, ViewListRequestsXpo.CareCenterCode, _valoresSesion.EmpresaIndigo, String.Empty, String.Empty, IndigoEmergentes.frmHCImpresionDialogo.eAperturaControl.DashboardPaciente)
            Dialogo.INDAtencionInicialParto = False
            Dialogo.INDAtencionRecienNacido = False
            Dialogo.INDCodigoEmpresaRegistro = _valoresSesion.EmpresaIndigo
            Dialogo.StartPosition = FormStartPosition.CenterScreen
            Dialogo.ShowDialog()
            Dialogo.Dispose()
        End Using
    End Sub

    Private Sub INDBbiPrintNursingRecord_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintNursingRecord.ItemClick
        'Se obtiene el registro que tiene el foco y al cual se va a realizar la cancelación
        Dim ViewListRequestsXpo = ListRequestsForManagementMedicalOrderXpo.FirstOrDefault(Function(d) d.SelectOption)
        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Using Dialogo As New IndigoeHistorias.frmHCConsultaEnfermeria(ViewListRequestsXpo.PatientCode, ViewListRequestsXpo.AdmissionNumber, String.Empty, IndigoeHistorias.frmHCConsultaEnfermeria.eAperturaControl.DashboardPaciente)
            Dialogo.StartPosition = FormStartPosition.CenterScreen
            Dialogo.ShowDialog()
            Dialogo.Dispose()
        End Using
    End Sub

    Private Sub INDBbiPrintAccountSupport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintAccountSupport.ItemClick
        'Se obtiene el registro que tiene el foco y al cual se va a realizar la cancelación
        Dim ViewListRequestsXpo = ListRequestsForManagementMedicalOrderXpo.FirstOrDefault(Function(d) d.SelectOption)
        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Using Dialogo As New IndigoeHistorias.frmHCConsultaControlCuentas(ViewListRequestsXpo.PatientCode, ViewListRequestsXpo.AdmissionNumber, ViewListRequestsXpo.TypeClinicalHistory, String.Empty, IndigoeHistorias.frmHCConsultaControlCuentas.eAperturaControl.DashboardPaciente)
            Dialogo.Text = "Soporte de Cuentas"
            Dialogo.StartPosition = FormStartPosition.CenterScreen
            Dialogo.ShowDialog()
            Dialogo.Dispose()
        End Using
    End Sub

    Private Sub INDBbiPrintOrders_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintOrders.ItemClick
        'Se obtiene el registro que tiene el foco y al cual se va a realizar la cancelación
        Dim ViewListRequestsXpo = ListRequestsForManagementMedicalOrderXpo.FirstOrDefault(Function(d) d.SelectOption)
        If ViewListRequestsXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una solicitud"
            Exit Sub
        End If

        Dim IsNewborn = ViewListRequestsXpo.TypeClinicalHistory = 8
        Using Dialogo As New IndigoeHistorias.frmHCListarOrdenes()
            Dialogo.INDCargar(ViewListRequestsXpo.PatientCode, ViewListRequestsXpo.AdmissionNumber, ViewListRequestsXpo.Folio, ViewListRequestsXpo.TypeClinicalHistory, ViewListRequestsXpo.CareCenterCode, IsNewborn, IsNewborn, IndigoeHistorias.frmHCListarOrdenes.eAperturaControl.DashboardPaciente)
            Dialogo.Text = "Ordenes a Imprimir"
            Dialogo.StartPosition = FormStartPosition.CenterScreen
            Dialogo.ShowDialog()
            Dialogo.Dispose()
        End Using
    End Sub

#End Region

#Region "Selector"

    Private _selectorCareCenter As SelectorCache = New SelectorCache("CODCENATE", "NOMCENATE")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCareCenter.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCareCenter" Then
                e.Value = _selectorCareCenter.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCareCenter.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCareCenter" Then
                selector = _selectorCareCenter
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCareCenter.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleCareCenter" Then
            If String.IsNullOrEmpty(_selectorCareCenter.ToString) Then
                searchLookupEdit.Properties.NullText = "Seleccione un Centro de Atención"
            Else
                searchLookupEdit.Properties.NullText = String.Format("{0} Items Seleccionados", _selectorCareCenter.Count())
            End If
            INDgcRequests.DataSource = Nothing
            LoadDatasource()
        End If
    End Sub

#End Region

#End Region

End Class
Imports Infrastructure.CrossCutting.Base
Imports Presentation.AccountManagement.MVP
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid
Imports System.Drawing
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing.Drawing2D
Imports DevExpress.Data.Linq
Imports Domain.AccountManagement.Model
Imports Infrastructure.Data.Xpo.AccountManagementRespository
Imports System.Windows.Forms

Public Class FrmDashboardAccountManagement
    Implements IDashboardAccountManagement

    ''' <summary>
    ''' Presentador del formulario
    ''' </summary>
    Private _presenter As PDashboardAccountManagement

    Private waitForm As New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, DevExpress.XtraSplashScreen.ParentType.UserControl)



    Public Sub New()
        InitializeComponent()

    End Sub

#Region "Variables"
    ''' <summary>
    ''' Código del usuario logeado
    ''' </summary>
    Dim userCode = SessionValues.Instance.UserIndigo

    ''' <summary>
    ''' Flag que determina si se está mostrando un popup
    ''' </summary>
    Private _isPopupMenuShowing As Boolean

    ''' <summary>
    ''' Variable global que guarda la lista total de las alertas
    ''' </summary>
    Private alertsList As New List(Of FolioAlert)

    ''' <summary>
    ''' Variable global que guarda el ultimo folio clickeado en su columna de alertas
    ''' </summary>
    Private rcdId As New Integer

    Private _listFilterTypes As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Referencia al popup de alertas actualmente abierto (para evitar múltiples instancias)
    ''' </summary>
    Private _currentAlertsPopup As FrmAlertsPopup = Nothing
#End Region

#Region "Properties"

    ''' <summary>
    ''' Tag identificador del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IDashboardAccountManagement.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propieda para los mensajes de eventos.
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

    ''' <summary>
    ''' Propiedad que obtiene el ID del centro de atención seleccionado.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AttentionCenterCode As String Implements IDashboardAccountManagement.AttentionCenterCode
        Get
            Return INDSleAttentionCenter.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene el identificador del tipo de consulta seleccionada por el usuario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TypeFilterSelected As Integer
        Get
            Return INDSleFilterType.EditValue '
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene el número de ingreso o identificación del paciente del selector de consulta
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TraceabilityFilterSelected As String
        Get
            Return INDSleFilter.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Área de gestión seleccionada - Ahora manejada por FrmFolioTransferPopup
    ''' </summary>
    Private _managementAreaSelected As Integer

    ''' <summary>
    ''' Usuario seleccionado - Ahora manejado por FrmFolioTransferPopup
    ''' </summary>
    Private _userSelected As String


    ''' <summary>
    ''' propiedad que obtiene la pestaña actual del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrentTabName
        Get
            Return INDTcgAccountManagement.SelectedTabPage.Name
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene el texto 
    ''' </summary>
    ''' <returns></returns>
    Public Property AlertComments As String
        Get
            Return INDTeNewAlert_Comment.Text
        End Get
        Set(value As String)
            INDTeNewAlert_Comment.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la tupla para el selector de tipos de consulta
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ListFilterTypes As List(Of Tuple(Of Byte, String))
        Get
            If _listFilterTypes Is Nothing Then
                _listFilterTypes = New List(Of Tuple(Of Byte, String))
                _listFilterTypes.Add(New Tuple(Of Byte, String)(1, "Paciente"))
                _listFilterTypes.Add(New Tuple(Of Byte, String)(0, "Ingreso"))
            End If
            Return _listFilterTypes
        End Get
    End Property
#End Region

#Region "Datasource"

    ''' <summary>
    ''' Datasource de las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    Public Property AttentionCenterXpo As XPInstantFeedbackSource Implements IDashboardAccountManagement.AttentionCenterXpo
        Get
            Return CType(INDSleAttentionCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAttentionCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del tab de Traslados
    ''' </summary>
    ''' <returns></returns>
    Public Property TransferXpo As List(Of GetUserFolios) Implements IDashboardAccountManagement.TransfersXpo
        Get
            Return CType(INDGcTransfers.DataSource, List(Of GetUserFolios))
        End Get
        Set(value As List(Of GetUserFolios))
            INDGcTransfers.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del tab de Trazabilidad
    ''' </summary>
    ''' <returns></returns>
    Public Property TraceabilityDatasource As List(Of VFolioTraceabilityProperties) Implements IDashboardAccountManagement.TraceabilityDatasource
        Get
            Return CType(INDGcTraceability.DataSource, List(Of VFolioTraceabilityProperties))
        End Get
        Set(value As List(Of VFolioTraceabilityProperties))
            INDGcTraceability.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del SLE para consulta
    ''' </summary>
    ''' <returns></returns>
    Public Property AdmissionsXpo As LinqInstantFeedbackSource Implements IDashboardAccountManagement.AdmissionsXpo
        Get
            Return CType(INDSleFilter.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDSleFilter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del SLE para consulta
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientsXpo As XPInstantFeedbackSource Implements IDashboardAccountManagement.PatientsXpo
        Get
            Return CType(INDSleFilter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleFilter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de las areas de gestión
    ''' </summary>
    Public Property ManagementAreasXpo As List(Of ManagementAreasXpo) Implements IDashboardAccountManagement.ManagementAreasXpo

#End Region

#Region "Handlers"

#Region "Load"
    ''' <summary>
    ''' Evento de carga del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardAccountManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        INDLciFilter.HideControl(True)
        _presenter = New PDashboardAccountManagement(Me)
        _presenter.InitializeAttentionCenter()

        InitializeTuples()
        Dim alertsTask = Task.Run(Async Function()
                                      Using model As New MDashboardAccountManagement()
                                          Dim alerts = Await model.GetAllAlerts()
                                          If alerts.StateResult Then
                                              alertsList = alerts.ObjectEmbbeded
                                          Else
                                              alertsList = New List(Of FolioAlert)
                                          End If
                                      End Using
                                  End Function)
    End Sub
#End Region

#Region "Shown"
    ''' <summary>
    ''' Evento de visualización del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardAccountManagement_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        INDSleAttentionCenter.Focus()
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento de selección del centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleAttentionCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAttentionCenter.EditValueChanged
        CleanControls()
        Await BeginReloadDatasource(CurrentTabName)
    End Sub

    ''' <summary>
    ''' Evento de selección del tipo de consulta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFilterType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleFilterType.EditValueChanged
        ChangeEditorInfo()
    End Sub

    Private Async Sub INDSleFilter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleFilter.EditValueChanged
        Await UpdateTraceabilityDatasource()
    End Sub

#End Region

#Region "SelectedPageChanged"
    ''' <summary>
    ''' Evento de cambio de la pestaña/página
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDTcgAccountManagement_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgAccountManagement.SelectedPageChanged
        Await BeginReloadDatasource(e.Page.Name)
    End Sub
#End Region

#Region "Selector"
    ''' <summary>
    ''' Selector que guarda las filas checkeadas 
    ''' </summary>
    Private _selectorTransfers As SelectorCache = New SelectorCache("Id", "AdmissionNumber", "RevenueControlDetailId")

    ''' <summary>
    ''' Evento de la columna de selección grupal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GCTransfer_Check(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvTransfers.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvTransfers" Then
                e.Value = _selectorTransfers.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento click en cualquier celda del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Gv_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles INDGvTransfers.RowCellClick
        Dim view = CType(sender, GridView)

        If e.Column Is GCTransferAlert Then
            HandleAlertClick(view, e.RowHandle)
            Return
        End If

        If Not _isPopupMenuShowing Then
            HandleRowSelection(view, e.RowHandle)
        End If

        _isPopupMenuShowing = False
    End Sub
#End Region

#Region "ItemClick"
    ''' <summary>
    ''' Evento click del botón refrescar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRefresh.ItemClick
        CleanControls()
        Await BeginReloadDatasource(CurrentTabName)
    End Sub

    ''' <summary>
    ''' Evento click en el boton Trasladar del menú popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiTransferFolio_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiTransferFolio.ItemClick
        If ManagementAreasXpo Is Nothing Then
            _presenter.GetManagementAreas()
        End If

        ' Crear y mostrar el formulario de traslado
        Dim frmTransfer As New FrmFolioTransferPopup()
        frmTransfer.ManagementAreasXpo = ManagementAreasXpo
        frmTransfer.StartPosition = FormStartPosition.Manual

        ' Posicionar el formulario
        Dim mousePosition As Point = Me.MousePosition
        mousePosition.Offset(10, 10)
        frmTransfer.Location = mousePosition

        ' Mostrar el formulario y procesar si se aceptó
        frmTransfer.ShowDialog()

        If frmTransfer.Accepted Then
            _managementAreaSelected = frmTransfer.ManagementAreaSelected
            _userSelected = frmTransfer.UserSelected

            ' Ejecutar el traslado
            INDSbTransfer_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Método para procesar el traslado de folios
    ''' </summary>
    Private Async Sub INDSbTransfer_Click(sender As Object, e As EventArgs)
        Try
            AsyncLoader(True)

            Dim folioTransfers = BuildFolioTransferList()
            If folioTransfers.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se seleccionaron folios para transferir."
                Return
            End If

            Await RequestAndHandleTransfer(folioTransfers)
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}"
        Finally
            AsyncLoader(False)
        End Try
    End Sub


    ''' <summary>
    ''' Evento click del boton de guardado de una alerta autogestionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbAddNewAlert_Click(sender As Object, e As EventArgs) Handles INDSbAddNewAlert.Click
        Dim newAlert = New FolioAlert With {
        .RevenueControlDetailId = rcdId,
        .Comments = AlertComments
        }

        Using model As New MDashboardAccountManagement
            Dim res = Await model.SaveFolioAlert(newAlert, SessionValues.Instance.AuditMessageWcf)
            If res.StateResult Then
                Mensaje(EeventViewerImages.Informacion) = "Se ha creado tu alerta exitosamente."
                alertsList.Add(res.ObjectEmbbeded)

                ' Cerrar el popup de agregar alerta
                INDPccAddAlert.HidePopup()

                If _currentAlertsPopup IsNot Nothing AndAlso Not _currentAlertsPopup.IsDisposed Then
                    Dim currentAlerts = alertsList.Where(Function(a) a.RevenueControlDetailId = rcdId AndAlso a.Status = True).ToList()
                    Dim previousAlerts = alertsList.Where(Function(a) a.RevenueControlDetailId = rcdId AndAlso a.Status = False).ToList()

                    _currentAlertsPopup.CurrentAlertsDatasource = currentAlerts
                    _currentAlertsPopup.PreviousAlertsDatasource = previousAlerts
                    _currentAlertsPopup.RefreshGrids()
                End If

                TransferXpo = Nothing
                alertsList = Nothing

                Await BeginReloadDatasource(CurrentTabName)
            ElseIf Not String.IsNullOrEmpty(res.Message) Then
                Mensaje(EeventViewerImages.MensajeError) = res.Message
            End If
            AlertComments = Nothing
        End Using
    End Sub

    ''' <summary>
    ''' Evento click del botón de Limpiar en el tab de Trazabilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbCleanFilter_Click(sender As Object, e As EventArgs) Handles INDSbCleanFilter.Click
        Try
            AdmissionsXpo = Nothing
            PatientsXpo = Nothing
            INDSleFilterType.EditValue = Nothing


            If INDSleFilter IsNot Nothing Then
                INDSleFilter.EditValue = Nothing
            End If

            'Ajuste asincronico
            Await BeginReloadDatasource(CurrentTabName)

            INDLciFilter.HideControl(True)
        Catch ex As Exception
            Dim exc = ex
        End Try
    End Sub
#End Region

#Region "PopupMenuShowing"
    ''' <summary>
    ''' Evento que escucha cuando se va a mostrar un popup en el gridview de traslados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDView_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDGvTransfers.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If

        INDBbiTransferFolio.Visibility = BarItemVisibility.Never

        Dim selector = GetSelector(True)

        If selector.Count > 0 Then
            INDBbiTransferFolio.Visibility = BarItemVisibility.Always
        End If

        _isPopupMenuShowing = True
        Dim view = CType(sender, GridView)
        INDPmRowActions.Manager = BarManager
        INDPmRowActions.ShowPopup(view.GridControl.PointToScreen(e.Point))
    End Sub
#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Manejador del evento de suspensión de alerta desde el popup
    ''' </summary>
    Private Async Sub OnSuspendAlertRequested(sender As Object, e As FolioAlertEventArgs)
        If e.FolioAlert IsNot Nothing Then
            Await SuspendAlert(e.FolioAlert)
        End If
    End Sub

#End Region

#Region "CustomDrawCell"
    ''' <summary>
    ''' Evento que escucha el renderizado de una celda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GridView_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles INDGvTransfers.CustomDrawCell
        ' Early return: solo procesar columna IsOnTime
        If e.Column.FieldName <> "IsOnTime" Then Exit Sub

        Dim view As GridView = CType(sender, GridView)
        Dim cellValue = view.GetRowCellValue(e.RowHandle, e.Column)

        ' Validar que el valor esté cargado y no sea nulo
        If cellValue Is Nothing OrElse TypeOf cellValue Is DevExpress.Data.NotLoadedObject Then Exit Sub

        ' Conversión segura del valor
        Dim value As Integer
        If Not Integer.TryParse(cellValue.ToString(), value) Then Exit Sub

        ' Aplicar colores según el valor del semáforo
        Select Case value
            Case 1 ' Verde - A tiempo
                e.Appearance.BackColor = Color.FromArgb(198, 239, 206)
                e.Appearance.BackColor2 = Color.FromArgb(155, 217, 155)
            Case 2 ' Amarillo - Advertencia
                e.Appearance.BackColor = Color.FromArgb(255, 235, 156)
                e.Appearance.BackColor2 = Color.FromArgb(255, 217, 102)
            Case 3 ' Rojo - Crítico
                e.Appearance.BackColor = Color.FromArgb(255, 199, 206)
                e.Appearance.BackColor2 = Color.FromArgb(255, 102, 102)
            Case Else
                Exit Sub ' No colorear si el valor no es 1, 2 o 3
        End Select

        ' Configurar gradiente y ocultar texto
        e.Appearance.GradientMode = LinearGradientMode.Vertical
        e.DisplayText = String.Empty
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método para limpiar todos los controles
    ''' </summary>
    Private Sub CleanControls()
        TransferXpo = Nothing
        ManagementAreasXpo = Nothing
        TraceabilityDatasource = Nothing
        AdmissionsXpo = Nothing
        PatientsXpo = Nothing
        alertsList = Nothing
        rcdId = Nothing
    End Sub

    ''' <summary>
    ''' Actualiza el datasource de cada Tab
    ''' </summary>
    ''' <param name="selectedPage"></param>
    Private Async Function BeginReloadDatasource(selectedPage As String) As Task
        If AttentionCenterCode = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione un centro de atención"
            Exit Function
        End If
        Select Case selectedPage
            Case INDLcgTransfers.Name 'Traslados
                Using model As New MDashboardAccountManagement()

                    If TransferXpo Is Nothing Then
                        Dim foliosList = Await model.GetUserFolios(userCode)
                        If foliosList?.Count > 0 Then
                            TransferXpo = foliosList
                        End If
                    End If

                    Dim alertsTask = Task.Run(Async Function()

                                                  Dim alerts = Await model.GetAllAlerts()
                                                  If alerts.StateResult Then
                                                      alertsList = alerts.ObjectEmbbeded
                                                  Else
                                                      alertsList = New List(Of FolioAlert)
                                                  End If

                                              End Function)
                End Using
            Case INDLcgTraceability.Name
                _presenter.ListFolioEventsTraceability(AttentionCenterCode, userCode)
        End Select
    End Function

    ''' <summary>
    ''' Guarda la selección de cada check por cada tab
    ''' </summary>
    ''' <param name="selection"></param>
    ''' <returns></returns>
    Private Function GetSelector(selection As Boolean) As SelectorCache
        Dim selector As New SelectorCache("", "")
        Select Case CurrentTabName
            Case INDLcgTransfers.Name 'Traslados
                If selection Then
                    _selectorTransfers.SetValue(INDGvTransfers.GetFocusedRow, True)
                End If
                selector = _selectorTransfers
        End Select
        Return selector
    End Function

    ''' <summary>
    ''' Construye la lista de folios que serán enviados a traslado
    ''' </summary>
    ''' <returns></returns>
    Private Function BuildFolioTransferList() As List(Of FolioTransfer)
        Dim selector = GetSelector(False)
        Dim list As New List(Of FolioTransfer)

        If selector.Count > 0 Then
            Dim keys = selector.GetKeysToArray()
            For Each key In keys
                Dim folio = New FolioTransfer With {
                    .PreviousUser = SessionValues.Instance.UserIndigo,
                    .ReceivingUser = _userSelected,
                    .AdmissionNumber = selector.GetValueByKey(key, "AdmissionNumber"),
                    .RevenueControlDetailId = selector.GetValueByKey(key, "RevenueControlDetailId"),
                    .ManagementAreaId = _managementAreaSelected
                }
                list.Add(folio)
            Next
        End If

        Return list
    End Function

    ''' <summary>
    ''' Solicitud y manejo de la respuesta de traslado de folio
    ''' </summary>
    ''' <param name="folioTransfers"></param>
    ''' <returns></returns>
    Private Async Function RequestAndHandleTransfer(folioTransfers As List(Of FolioTransfer)) As Task
        Using model As New MDashboardAccountManagement()
            Dim result = Await model.RequestFolioTransferToUser(folioTransfers, SessionValues.Instance.AuditMessageWcf)

            If result.StateResult Then
                Mensaje(EeventViewerImages.Informacion) = "Folios transferidos con éxito, a espera de su aprobación."
                CleanControls()
                Await BeginReloadDatasource(CurrentTabName)
            Else
                If result.MessageResult Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                ElseIf result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' Lógica para cuando se hace clic en la columna de alertas
    ''' </summary>
    Private Sub HandleAlertClick(view As GridView, rowHandle As Integer)
        If rowHandle < 0 Then Exit Sub

        rcdId = CType(view.GetRowCellValue(rowHandle, "RevenueControlDetailId"), Integer)

        Dim currentAlerts As List(Of FolioAlert) = Nothing
        Dim previousAlerts As List(Of FolioAlert) = Nothing

        If alertsList IsNot Nothing AndAlso alertsList.Count > 0 Then
            Dim hasAlert = alertsList.Any(Function(a) a.RevenueControlDetailId = rcdId)
            If hasAlert Then
                currentAlerts = alertsList.Where(Function(a) a.RevenueControlDetailId = rcdId AndAlso a.Status = True).ToList()
                previousAlerts = alertsList.Where(Function(a) a.RevenueControlDetailId = rcdId AndAlso a.Status = False).ToList()
            End If
        End If

        ' Si ya hay un popup abierto, traerlo al frente y actualizar sus datos
        If _currentAlertsPopup IsNot Nothing AndAlso Not _currentAlertsPopup.IsDisposed Then
            _currentAlertsPopup.CurrentAlertsDatasource = currentAlerts
            _currentAlertsPopup.PreviousAlertsDatasource = previousAlerts
            _currentAlertsPopup.RefreshGrids()
            _currentAlertsPopup.BringToFront()
            _currentAlertsPopup.Activate()
            Return
        End If

        ' Crear y mostrar el formulario de alertas
        Dim frmAlerts As New FrmAlertsPopup()
        frmAlerts.CurrentAlertsDatasource = currentAlerts
        frmAlerts.PreviousAlertsDatasource = previousAlerts
        frmAlerts.AddAlertPopup = INDPccAddAlert
        frmAlerts.StartPosition = FormStartPosition.Manual

        ' Posicionar el formulario
        Dim mousePosition As Point = Me.MousePosition
        mousePosition.Offset(10, 10)
        frmAlerts.Location = mousePosition

        ' Suscribirse al evento de suspensión de alerta
        AddHandler frmAlerts.SuspendAlertRequested, AddressOf OnSuspendAlertRequested

        ' Suscribirse al evento de cierre para limpiar la referencia
        AddHandler frmAlerts.FormClosed, Sub(s, ev)
                                             _currentAlertsPopup = Nothing
                                         End Sub

        ' Guardar referencia al popup actual
        _currentAlertsPopup = frmAlerts

        ' Mostrar el formulario de forma no-modal para que Deactivate funcione
        ' Esto permite que el formulario se cierre al hacer clic fuera
        frmAlerts.Show(Me)
    End Sub


    ''' <summary>
    ''' Manejo de selección de fila y refresco del selector
    ''' </summary>
    Private Sub HandleRowSelection(view As GridView, rowHandle As Integer)
        Dim selector As SelectorCache = Nothing

        If view.Name = "INDGvTransfers" Then
            selector = _selectorTransfers
        End If

        If selector Is Nothing Then Exit Sub

        If rowHandle >= 0 Then
            Dim row = view.GetRow(rowHandle)
            selector.SetValue(row)
            view.RefreshRow(rowHandle)
        Else
            selector.Clear()
            view.RefreshData()
        End If
    End Sub


    ''' <summary>
    ''' Llama la función de suspensión de alertas
    ''' </summary>
    ''' <param name="folioAlertSelected"></param>
    Private Async Function SuspendAlert(folioAlertSelected As FolioAlert) As Task
        Using model As New MDashboardAccountManagement
            Dim res = Await model.SuspendFolioAlert(folioAlertSelected, SessionValues.Instance.AuditMessageWcf)
            If res.StateResult Then
                ' Actualizar el estado en la lista local
                Dim alert = alertsList.FirstOrDefault(Function(a) a.Id = folioAlertSelected.Id)
                If alert IsNot Nothing Then
                    alert.Status = False
                End If

                CleanControls()
                Await BeginReloadDatasource(CurrentTabName)

                Mensaje(EeventViewerImages.Informacion) = res.Message
            Else
                Mensaje(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Function

    ''' <summary>
    ''' Inicializa la data fija de los selectores
    ''' </summary>
    Private Sub InitializeTuples()
        INDSleFilterType.Properties.DataSource = ListFilterTypes
    End Sub

    ''' <summary>
    ''' Maneja la selección de un tipo de consulta y modifica el selector siguiente de acuerdo al tipo de consulta seleccionada
    ''' </summary>
    Private Sub ChangeEditorInfo()
        Dim type = TypeFilterSelected
        INDSleFilter.Properties.View.Columns.Clear()
        If type = 1 Then
            INDLciFilter.Text = "Paciente:"
            INDSleFilter.Properties.DisplayMember = "CodeFullName"
            INDSleFilter.Properties.ValueMember = "IPCODPACI"

            ' Agrega columnas para pacientes
            Dim col1 As New Columns.GridColumn()
            col1.FieldName = "IPCODPACI"
            col1.Caption = "Código"
            col1.Visible = True
            col1.VisibleIndex = 0

            Dim col2 As New Columns.GridColumn()
            col2.FieldName = "IPNOMCOMP"
            col2.Caption = "Nombre Completo"
            col2.Visible = True
            col2.VisibleIndex = 1

            INDSleFilter.Properties.View.Columns.Add(col1)
            INDSleFilter.Properties.View.Columns.Add(col2)
            INDSleFilter.Refresh()
            _presenter.ListAllPatients()

        ElseIf type = 0 Then
            INDLciFilter.Text = "Ingreso:"
            INDSleFilter.Properties.DisplayMember = "FullNameAdmission"
            INDSleFilter.Properties.ValueMember = "AdmissionCode"

            ' Agrega columnas para ingresos
            Dim col1 As New Columns.GridColumn()
            col1.FieldName = "AdmissionCode"
            col1.Caption = "Código"
            col1.Visible = True
            col1.VisibleIndex = 0

            Dim col2 As New Columns.GridColumn()
            col2.FieldName = "PatientName"
            col2.Caption = "Nombre Paciente"
            col2.Visible = True
            col2.VisibleIndex = 1

            INDSleFilter.Properties.View.Columns.Add(col1)
            INDSleFilter.Properties.View.Columns.Add(col2)
            INDSleFilter.Refresh()
            _presenter.ListAllAdmissions()
        End If

        INDLciFilter.HideControl(False)
        INDSleFilter.Properties.View.RefreshData()
        INDSleFilter.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Evento que carga el datasource en base a los criterios de consulta
    ''' </summary>
    Private Async Function UpdateTraceabilityDatasource() As Task
        Dim type = TypeFilterSelected
        Try
            Using model As New MDashboardAccountManagement()
                If type = 1 Then
                    Dim res = Await model.ListFolioEventsTraceabilityByPatient(AttentionCenterCode, TraceabilityFilterSelected, userCode)
                    INDGcTraceability.DataSource = res
                ElseIf type = 0 Then
                    Dim res = Await model.ListFolioEventsTraceabilityByAdmission(AttentionCenterCode, TraceabilityFilterSelected, userCode)
                    INDGcTraceability.DataSource = res
                End If
                INDGcTraceability.RefreshDataSource()
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = $"Ocurrió un error consultando la trazabilidad de folios: {ex.Message}"
        End Try
    End Function

#End Region





End Class
#Region "Imports"
Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports Domain.AccountManagement.Model
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.AccountManagementRespository
Imports Presentation.AccountManagement.MVP
Imports Presentation.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports Domain.Entities
#End Region

Public Class FrmDashboardAccountAssignment
    Implements IDashboardAccountAssignment

#Region "Globals"
    Public Sub New()
        InitializeComponent()
    End Sub
    ''' <summary>
    ''' Presentador del formulario
    ''' </summary>
    Private _presenter As PDashboardAccountAssignment
    ''' <summary>
    ''' Acciones para el segmento de Extracto Bancario y Libro de Bancos
    ''' </summary>
    Private _assigmentActions As New List(Of eAcciones)
    ''' <summary>
    ''' Variable que almacena el tipo de ingreso parametrizado en Gestión de Cuentas
    ''' </summary>
    Private _entryType As String
    ''' <summary>
    ''' Lista de los usuarios facturadores para poder gestionar los controles y el guardado
    ''' </summary>
    Private _usersAssignmentList As List(Of UsersAssignment)
    ''' <summary>
    ''' Selector que guarda las filas checkeadas del grid de asignaciones pendientes
    ''' </summary>
    Private _selectorPendingAssignment As SelectorCache = New SelectorCache("AdmissionNumber", "PatientCode", "TypeIncome", "Folio", "IncomeStatus", "UserCreation")
    ''' <summary>
    ''' Lista de objetos que almacena los ingresos a asignar manualmente
    ''' </summary>
    Private _listEntryDistribution As New List(Of AutomaticEntryDistribution)
    ''' <summary>
    ''' Lista de objetos que almacena los ingresos a asignar automáticamente
    ''' </summary>
    Private _listAutomaticEntryMessage As New List(Of AutomaticDistributionMessage)

#End Region

#Region "DataSource"
    ''' <summary>
    ''' Datasource de las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    Public Property CareCenterXpo As XPInstantFeedbackSource Implements IDashboardAccountAssignment.CareCenterXpo
        Get
            Return CType(INDSleCareCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCareCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los ingresos pendientes de asignación automática
    ''' </summary>
    ''' <returns></returns>
    Public Property PendingAssignmentDatasource As XPInstantFeedbackSource Implements IDashboardAccountAssignment.PendingAssignmentDatasource
        Get
            Return CType(INDGcPendingAssignment.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcPendingAssignment.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los usuarios habilitados en Párametros de gestión de cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Property UsersDatasource As List(Of UsersAssignment)
        Get
            Return CType(INDSleUsers.Properties.DataSource, List(Of UsersAssignment))
        End Get
        Set(value As List(Of UsersAssignment))
            INDSleUsers.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el Id del usuario al cuál se asignarán los ingresos
    ''' </summary>
    ''' <returns></returns>
    Public Property UserId As Integer
        Get
            Return INDSleUsers.EditValue
        End Get
        Set(value As Integer)
            INDSleUsers.EditValue = value
        End Set
    End Property

#End Region

#Region "Properties"
    ''' <summary>
    ''' Id del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IDashboardAccountAssignment.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Id de la Unidad Operativa
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CareCenterCode As String Implements IDashboardAccountAssignment.CareCenterCode
        Get
            If INDSleCareCenter.EditValue = "Seleccione un Centro de Atención" Then
                Return String.Empty
            Else
                Return INDSleCareCenter.EditValue
            End If
        End Get
    End Property
    ''' <summary>
    ''' 
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

#Region "Load"
    ''' <summary>
    ''' Cargue del formulario
    ''' </summary>
    Private Async Sub FrmDashboardAccountAssignment_Load() Handles MyBase.Load
        Me.ToolBar.Hide()
        _presenter = New PDashboardAccountAssignment(Me)
        'Obtenemos el parámetro de Tipo de Ingreso
        Await GetAccountManagerParameter(Me.BarraBotones.OperatingUnitValue)
        'Acciones a la rejilla
        _assigmentActions.Add(eAcciones.AssignPatient)
        _assigmentActions.Add(eAcciones.AssignAutomatically)
        IndigoGridView1.SetListAcction(INDGvPendingAssignment, _assigmentActions)
        INDGvPendingAssignment.Columns.ColumnByName("colActions").Visible = False
    End Sub
#End Region

#Region "ContexMenuActions"
    ''' <summary>
    ''' Menú de acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PendingAssignment_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim senderTag = sender.Tag.ToString
        Dim barItem As BarButtonItem = TryCast(sender, BarButtonItem)

        If barItem IsNot Nothing AndAlso barItem.Links.Count > 0 Then
            Select Case senderTag
                Case "AssignPatient"
                    ManualAssignment(barItem)
                Case "AssignAutomatically"
                    AutomaticAssignment()
            End Select
        End If
    End Sub

#End Region

#Region "SelectionCache Events"
    ''' <summary>
    ''' Evento que maneja el estado de la columna unbound de selección (checkbox).
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvPendingAssignment_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles INDGvPendingAssignment.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view IsNot Nothing AndAlso view.Name = "INDGvPendingAssignment" Then
                e.Value = _selectorPendingAssignment.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento click en cualquier celda del GridView.
    ''' Maneja la selección/deselección de filas.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvPendingAssignment_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles INDGvPendingAssignment.RowCellClick
        HandleRowSelection(TryCast(sender, GridView), e.RowHandle)
    End Sub

    ''' <summary>
    ''' Maneja la selección de una fila y actualiza el selector.
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="rowHandle"></param>
    Private Sub HandleRowSelection(view As GridView, rowHandle As Integer)
        If view Is Nothing OrElse rowHandle < 0 Then
            Exit Sub
        End If

        Dim row = view.GetRow(rowHandle)
        If row IsNot Nothing Then
            _selectorPendingAssignment.SetValue(row)
            view.RefreshRow(rowHandle)
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Construye la lista de asignaciones automáticas usando el selector
    ''' </summary>
    Private Function BuildAutomaticAssignmentList() As List(Of AutomaticDistributionMessage)
        Dim list As New List(Of AutomaticDistributionMessage)

        If _selectorPendingAssignment.Count > 0 Then
            Dim keys = _selectorPendingAssignment.GetKeysToArray()
            For Each key In keys
                Dim automaticEntry = New AutomaticDistributionMessage With {
                    .AdmissionNumber = key,
                    .PatientCode = _selectorPendingAssignment.GetValueByKey(key, "PatientCode"),
                    .EntryStatus = _selectorPendingAssignment.GetValueByKey(key, "IncomeStatus"),
                    .CreationUser = _selectorPendingAssignment.GetValueByKey(key, "UserCreation"),
                    .CreationDate = GetServerDate(),
                    .EntryType = _selectorPendingAssignment.GetValueByKey(key, "TypeIncome")
                }
                list.Add(automaticEntry)
            Next
        End If

        Return list
    End Function

    ''' <summary>
    ''' Construye la lista de asignaciones manuales usando el selector
    ''' </summary>
    Private Function BuildManualAssignmentList() As List(Of AutomaticEntryDistribution)
        Dim list As New List(Of AutomaticEntryDistribution)

        If _selectorPendingAssignment.Count > 0 Then
            Dim keys = _selectorPendingAssignment.GetKeysToArray()
            For Each key In keys
                Dim entryDistribution = New AutomaticEntryDistribution With {
                    .AssignedUserId = UserId,
                    .AdmissionNumber = key,
                    .AssignmentDate = GetServerDate(),
                    .EntryType = _selectorPendingAssignment.GetValueByKey(key, "TypeIncome")
                }
                list.Add(entryDistribution)
            Next
        End If

        Return list
    End Function

    ''' <summary>
    ''' Método que asigna los pacientes manualmente
    ''' </summary>
    Private Async Sub GenerateManualAssignment()
        Try
            ' Validar que hay folios asociados
            Dim keys = _selectorPendingAssignment.GetKeysToArray()
            Dim entriesWithoutFolios As New List(Of String)

            For Each key In keys
                Dim folio = _selectorPendingAssignment.GetValueByKey(key, "Folio")
                If folio IsNot Nothing AndAlso folio.ToString() = "Folios no asociados" Then
                    entriesWithoutFolios.Add(_selectorPendingAssignment.GetValueByKey(key, "AdmissionNumber"))
                End If
            Next

            If entriesWithoutFolios.Count > 0 Then
                Mensaje(EeventViewerImages.MensajeError) = "No existen folios para los ingresos: " & String.Join(", ", entriesWithoutFolios) & vbCrLf & "Desmarquelos y vuelva a intentarlo."
                Return
            End If

            ' Construir lista y asignar
            Dim assignmentList = BuildManualAssignmentList()

            Using model As New MDashboardAccountAssignment(CStr(MyTag))
                AsyncLoader(True)
                Dim res = Await model.GenerateManualAssignment(assignmentList)
                If res.StateResult Then
                    ' Limpiar selector y refrescar
                    _selectorPendingAssignment.Clear()
                    _presenter.GetPendingAssignmentDatasource(CareCenterCode, _entryType)
                    Mensaje(EeventViewerImages.Informacion) = res.Message
                Else
                    Mensaje(EeventViewerImages.MensajeError) = res.Message
                End If
            End Using
        Catch ex As Exception
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Método que asigna los paciente automáticamente
    ''' </summary>
    Private Async Sub GenerateAutomaticAssignment()
        Try
            ' Construir lista y asignar
            Dim assignmentList = BuildAutomaticAssignmentList()

            Using model As New MDashboardAccountAssignment(CStr(MyTag))
                AsyncLoader(True)
                Dim res = Await model.GenerateAutomaticAssignment(assignmentList)
                If res.StateResult Then
                    ' Limpiar selector y refrescar
                    _selectorPendingAssignment.Clear()
                    _presenter.GetPendingAssignmentDatasource(CareCenterCode, _entryType)
                    Mensaje(EeventViewerImages.Informacion) = res.Message
                Else
                    Mensaje(EeventViewerImages.MensajeError) = res.Message
                End If
            End Using
        Catch ex As Exception
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Función que obtiene el parámetro de Tipo de Ingreso y la lista de Usuarios de asignación
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetAccountManagerParameter(ByVal idOperativeUnit As Integer) As Task
        Using Model As New MAccountManagementParameters(CStr(Me.Tag))
            Dim resulOperation = Await Model.GetAccountManagementParameters(idOperativeUnit)
            If resulOperation IsNot Nothing Then
                _entryType = resulOperation.ObjectEmbbeded.EntryType
                _usersAssignmentList = resulOperation.ObjectEmbbeded.usersAssignment
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se encuentra parametrizado el Tipo de Ingreso en Parámetros de Gestión de Cuentas"
            End If
        End Using
    End Function
    ''' <summary>
    ''' Prepara el datasource de usuarios basado en las filas seleccionadas
    ''' </summary>
    Private Sub PrepareUsersDatasource()
        If _selectorPendingAssignment.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un paciente para asignar."
            Return
        End If

        ' Obtener tipos de ingreso distintos
        Dim keys = _selectorPendingAssignment.GetKeysToArray()
        Dim distinctTypes As New HashSet(Of Integer)

        For Each key In keys
            distinctTypes.Add(_selectorPendingAssignment.GetValueByKey(key, "TypeIncome"))
        Next

        ' Validar que todos sean del mismo tipo
        If distinctTypes.Count = 1 Then
            Dim entryType = distinctTypes.First()
            UsersDatasource = _usersAssignmentList.Where(Function(x) x.Status = True AndAlso x.EntryType = entryType AndAlso Not x.IsRemoved).ToList()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar pacientes con el mismo tipo de ingreso para poder asignarlos"
        End If
    End Sub
    ''' <summary>
    ''' Método que asigna el paciente manualmente
    ''' </summary>
    Private Sub ManualAssignment(barItem As BarButtonItem)
        If barItem IsNot Nothing AndAlso barItem.Links.Count > 0 Then
            INDPcUserReassignment.Manager = barItem.Manager
            barItem.DropDownControl = INDPcUserReassignment

            'Preparar el datasource de usuarios basado en las filas seleccionadas
            PrepareUsersDatasource()
            INDPcUserReassignment.Show()
            INDSleUsers.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Método que asigna el paciente de forma automática
    ''' </summary>
    Private Sub AutomaticAssignment()
        ' Validar que haya pacientes seleccionados en el selector
        If _selectorPendingAssignment.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un paciente para asignar automáticamente."
            Return
        End If

        'Generamos la asignación automática
        GenerateAutomaticAssignment()
    End Sub

#End Region

#Region "Events"
    ''' <summary>
    ''' Método que actualiza el DataSource acorde a la unidad operativa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareCenter.EditValueChanged
        If CareCenterCode <> String.Empty AndAlso _entryType <> String.Empty Then
            ' Limpiar el selector cuando cambia el datasource
            _selectorPendingAssignment.Clear()
            _presenter.GetPendingAssignmentDatasource(CareCenterCode, _entryType)
        End If
    End Sub

    ''' <summary>
    ''' Método que carga el DataSource de los Centros de Atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PendingAssignment_(sender As Object, e As EventArgs) Handles INDSleCareCenter.QueryPopUp
        If CareCenterXpo Is Nothing Then
            _presenter.InitializeCareCenterXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento para refrescar el Datasource
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiRefresh_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiRefresh.ItemClick
        If CareCenterCode <> String.Empty AndAlso _entryType <> String.Empty Then
            ' Limpiar el selector al refrescar
            _selectorPendingAssignment.Clear()
            _presenter.GetPendingAssignmentDatasource(CareCenterCode, _entryType)
        End If
    End Sub

    ''' <summary>
    ''' Evento para cerrar el PopUp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPcUserReassignment_Leave(sender As Object, e As EventArgs) Handles INDPcUserReassignment.Leave
        INDPcUserReassignment.Hide()
    End Sub

    ''' <summary>
    ''' Evento Click para generar la asignación manual del paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        GenerateManualAssignment()
    End Sub

#End Region


End Class
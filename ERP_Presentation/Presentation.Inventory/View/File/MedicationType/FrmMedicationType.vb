'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-11-12
' Description      : Clase que representa el formulario de tipo de medicamento
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Resources
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Infrastructure.CrossCutting.Base
Imports ResourceManager = Infrastructure.CrossCutting.Resources.ResourceManager
Imports Domain.Base.Entities
Imports System.Windows.Forms
#End Region


Public Class FrmMedicationType
    Implements IMedicationType, ICustomizableForm

#Region "Variables"

    ''' <summary>
    ''' Indica si el formulario está en modo búsqueda.
    ''' </summary>
    Dim searchMode As Boolean

    ''' <summary>
    ''' Presentador del tipo de medicamento
    ''' </summary>
    Dim presenter As PPMedicationType

    ''' <summary>
    ''' Entidad de tipo de medicamento.
    ''' </summary>
    Dim medicationType As MedicationType

    ''' <summary>
    ''' Registro bloqueado para inventario.
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' ID de la unidad operativa seleccionada.
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Nombre del módulo al que pertenece el formulario.
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Secuencia numérica del formulario.
    ''' </summary>
    Private _sequence As InventorySequence

    ''' <summary>
    ''' ID de la configuración de secuencia seleccionada.
    ''' </summary>
    Private _idCurrentSequence As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el código de un tipo de medicamento.
    ''' </summary>
    Public Property Code As String Implements IMedicationType.Code
        Get
            If INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre del tipo de medicamento
    ''' </summary>
    Private Property Name As String Implements IMedicationType.Name
        Get
            Return INDMeName.EditValue
        End Get
        Set(value As String)
            INDMeName.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el estado del tipo de medicamento
    ''' </summary>
    Public Property Status As Boolean Implements IMedicationType.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = If(value, eActionsStatusRecords.Active, eActionsStatusRecords.Inactive)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario.
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IMedicationType.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia numérica del formulario.
    ''' </summary>
    Public Property Sequence As InventorySequence Implements IMedicationType.Sequence
        Get
            Return _sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In _sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que retorna el control de diseño.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMedicationType.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que permite habilitar o deshabilitar controles.
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IMedicationType.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDMeName.Enabled = value
            INDLcRoot.EndUpdate()
            If value Then
                INDMeName.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para mostrar mensajes con diferentes iconos.
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            Select Case Icono
                Case EeventViewerImages.Advertencia
                    MessageIndigo.Show(value, MessageType.Warning, Me.Text)
                Case EeventViewerImages.Informacion
                    MessageIndigo.Show(value, MessageType.Information, Me.Text)
                Case EeventViewerImages.MensajeError
                    MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End Select
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Accion de buscar en la barra de botones
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Guarda un nuevo tipo de documento
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of MedicationType) = Await presenter.SaveMedicationTypeAsync(Me.medicationType, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If medicationType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                    End If
                End If
                medicationType = result.ObjectEmbbeded
                Deshacer()
            Else
                INDBtnCode.Enabled = False
            End If
            ShowMessage(result.StatusCode) = result.Message
        Catch ex As Exception
            INDBtnCode.Enabled = False
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Asigna codigod si la secuencia no es manual, para nuevo tipo de documento
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence.IsManual Then
            Deshacer()
        Else
            NewMedicationType()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles del formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina un tipo de documento
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If medicationType IsNot Nothing AndAlso medicationType.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result As ActionResult = Await presenter.DeleteMedicationTypeAsync(medicationType)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Deshacer()
                    Else
                        INDBtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                Catch ex As Exception
                    INDBtnCode.Enabled = False
                    Throw ex
                Finally
                    AsyncLoader(False)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Despliega un popup para buscar un tipo de documento
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If Not BarraBotones.PermiteConsultar Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda()
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        With FormSearchObjects
            .ListaColumnas = {
                New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}
            }.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMedicationType
            .FormParent = Me
            .ShowSearch()
        End With
        searchMode = True
    End Sub


    ''' <summary>
    ''' Logica de boton de actulizar no utilizada en este caso
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub
#End Region

#Region "Handlers"

#Region "Bar button"


    ''' <summary>
    ''' Carga permisos al iniciar la barra de botones.
    ''' </summary>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString())
    End Sub

    ''' <summary>
    ''' Maneja el evento de carga del formulario.
    ''' Inicializa valores y carga datos necesarios.
    ''' </summary>
    Private Sub FrmMedicationType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        indigo = SessionValues.Instance
        presenter = New PPMedicationType(Me)
        presenter.GetSequence()
        presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Libera recursos al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        presenter = Nothing
        record = Nothing
        medicationType = Nothing
    End Sub

    ''' <summary>
    ''' Maneja el evento de búsqueda de la barra de botones.
    ''' Se invoca tanto al hacer clic en el botón de buscar como al presionar INDBtnCode.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Maneja el evento de guardado de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Maneja el evento de eliminación de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Maneja el evento de actualización de la barra de botones.
    ''' Guarda los cambios actuales.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Maneja el evento de activar/desactivar un registro.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        ChangeState()
    End Sub

    ''' <summary>
    ''' Maneja el evento de nuevo registro
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Maneja el evento de deshacer en la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        searchMode = False
        Deshacer()
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Maneja el evento KeyDown del botón INDBtnCode. 
    ''' Permite realizar acciones cuando se presionan teclas específicas.
    ''' </summary>
    Private Function INDBtnCode_KeyDownAsync(sender As Object, e As KeyEventArgs) As Task Handles INDBtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Function
            End If
            If _sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica está configurada como manual, por favor digite un código."
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    NewMedicationType()
                Else
                    LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Function
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga el estado de los botones de la barra.
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpia los controles de la interfaz de usuario de forma asíncrona.
    ''' </summary>
    Private Async Sub CleanControls()
        INDLcRoot.BeginUpdate()
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Status = True
        Code = Nothing
        Name = Nothing
        medicationType = Nothing
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        INDLcRoot.EndUpdate()
        Await DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado si existe.
    ''' </summary>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Retorna el valor de la búsqueda de forma asíncrona.
    ''' </summary>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Not String.IsNullOrEmpty(Code) Then
            Await LoadControls()
            If Not INDBtnCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Crea un nuevo tipo de medicamento
    ''' </summary>
    Private Async Function NewMedicationType() As Task
        medicationType = New MedicationType() With {.Status = True}
        If _sequence.IsManual Then
            ActionsOnControls = True
            BarraBotones.PrepareToolbar(eAction.OnlySave)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If _sequence.Scope.Equals("O") Then
                _idCurrentSequence = _sequence.InventorySequenceDetail(0).Id
            ElseIf _sequence.Scope.Equals("OU") Then
                If _sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = _idOperativeUnit) Then
                    _idCurrentSequence = _sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = _idOperativeUnit).Id
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If _sequence.Sequential Then
                Code = ResourceManager.GetString("LabelOrTextboxNew")
                ActionsOnControls = True
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If DicSequense IsNot Nothing AndAlso DicSequense.Count > 0 Then
                    If DicSequense(CInt(_idCurrentSequence)).Count > 0 Then
                        Code = DicSequense(CInt(_idCurrentSequence))(0)
                        ActionsOnControls = True
                        BarraBotones.PrepareToolbar(eAction.OnlySave)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Tag))
                            DicSequense(CInt(_idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(_idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If DicSequense(CInt(_idCurrentSequence)) IsNot Nothing AndAlso DicSequense(CInt(_idCurrentSequence)).Count > 0 Then
                            Code = DicSequense(CInt(_idCurrentSequence))(0)
                            ActionsOnControls = True
                            BarraBotones.PrepareToolbar(eAction.OnlySave)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Code = ResourceManager.GetString("LabelOrTextboxNew")
                    ActionsOnControls = True
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function


    ''' <summary>
    ''' Carga los controles con datos del registro de forma asíncrona.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                AsyncLoader(True)
                Dim resultOperation = Await presenter.GetMedicationTypeByCode(Code)
                INDLcRoot.BeginUpdate()
                medicationType = resultOperation.ObjectEmbbeded
                If medicationType IsNot Nothing AndAlso medicationType.Id > 0 Then
                    Me.BarraBotones.StatusRecordVisible = True

                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(medicationType.Id))
                        With medicationType
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            Name = .Name
                            Status = .Status
                        End With
                        If record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = medicationType.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewMedicationType
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDBtnCode.Focus()
                    End If
                End If
                INDLcRoot.EndUpdate()
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function


    ''' <summary>
    ''' Asigna valores al objeto que se enviará.
    ''' </summary>
    Private Sub AssigningValues()
        With medicationType
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Name
            .Status = Status
        End With
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad de forma asíncrona.
    ''' </summary>
    Private Async Function ChangeState() As Task
        If medicationType IsNot Nothing Then
            Try
                AsyncLoader(True)
                Dim state As Boolean = Not medicationType.Status
                Dim result As ActionResult(Of MedicationType) = Await presenter.ChangeStateMedicationType(medicationType, state)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    medicationType = result.ObjectEmbbeded
                Else
                    INDBtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

#End Region

End Class
'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 21-11-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : Formulario Registro Soporte RIPS
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Windows.Forms
Imports DevExpress.XtraLayout.Utils
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls

#End Region

Public Class FrmRIPSsupportrecord
    Implements ICrudBase
    Implements IRIPSSupportRecord

#Region "CONSTANTS"

    ''' <summary>Constante del módulo.</summary>
    Private Const MODULE_NAME As String = "Billing"

    ''' <summary>Estado de ingreso: Abierto.</summary>
    Private Const ADMISSION_STATUS_OPEN As String = " "

    ''' <summary>Estado de ingreso: Parcial.</summary>
    Private Const ADMISSION_STATUS_PARTIAL As String = "P"

    ''' <summary>Estado de ingreso: Bloqueado.</summary>
    Private Const ADMISSION_STATUS_BLOCKED As String = "B"

    ''' <summary>Estado de ingreso: Facturado.</summary>
    Private Const ADMISSION_STATUS_INVOICED As String = "F"

    ''' <summary>Tipo de bloqueo especial que permite operaciones RIPS.</summary>
    Private Const INCOME_LOCK_TYPE_ALLOWED As Integer = 1

    ''' <summary>Estado RIPS: Registrado/Guardado.</summary>
    Private Const RIPS_STATUS_REGISTERED As Byte = 0

    ''' <summary>Estado RIPS: Confirmado.</summary>
    Private Const RIPS_STATUS_CONFIRMED As Byte = 1

    ''' <summary>Estado RIPS: Anulado.</summary>
    Private Const RIPS_STATUS_CANCELLED As Byte = 2

#End Region

#Region "BUILDER"

    ''' <summary>
    ''' Inicializa una nueva instancia del formulario FrmRIPSsupportrecord.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "GLOBALS"

    ''' <summary>Registro de bloqueo activo para control de concurrencia.</summary>
    Private _record As BlockRecordBilling

    ''' <summary>Sesión de usuario de Indigo con información de autenticación.</summary>
    Private _indigoSession As SessionValues

    ''' <summary>Indica si el formulario está en modo búsqueda.</summary>
    Private _searchMode As Boolean

    ''' <summary>Entidad RIPS en edición actual.</summary>
    Private _RIPSSupportRecord As RIPSSupportRecord

    ''' <summary>Popup de detalle para agregar estancias hospitalarias.</summary>
    Private _popupRIPSDetails As FrmPopUpRIPSsupportrecordDetails

    ''' <summary>Identificador de la unidad operativa seleccionada.</summary>
    Private _operatingUnitId As Integer

    ''' <summary>Presentador del formulario (patrón MVP).</summary>
    Private _presenter As PRIPSSupportRecord

    ''' <summary>DTO con información del ingreso seleccionado actualmente.</summary>
    Private _selectedAdmissionInfo As AdmissionInfoDTO

    ''' <summary>Bandera para evitar procesamiento de eventos durante la carga inicial.</summary>
    Private _isInitializing As Boolean = True

    ''' <summary>Bandera para evitar recursión al limpiar la selección de ingreso.</summary>
    Private _isClearingAdmission As Boolean = False

    ''' <summary>Secuencia de facturación configurada para generar códigos.</summary>
    Private _sequence As BillingSequence

    ''' <summary>Identificador del detalle de secuencia actual en uso.</summary>
    Private _idCurrentSequence As Long

#End Region

#Region "PROPERTIES"

    ''' <summary>Identificador del formulario.</summary>
    Public ReadOnly Property MyTag As Object Implements IRIPSSupportRecord.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>Layout raíz expuesto al presentador.</summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRIPSSupportRecord.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>Wrapper para mensajes globales.</summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>Unidad operativa activa.</summary>
    Public Property OperatingUnitId() As Integer Implements IRIPSSupportRecord.OperatingUnitId
        Get
            Return Me._operatingUnitId
        End Get
        Set(value As Integer)
            Me._operatingUnitId = value
        End Set
    End Property

    ''' <summary>Código del registro de soporte.</summary>
    Public Property Code As String Implements IRIPSSupportRecord.Code
        Get
            Return RIPSBteCode.EditValue
        End Get
        Set(value As String)
            RIPSBteCode.EditValue = value
        End Set
    End Property

    ''' <summary>Fecha del registro en cabecera.</summary>
    Public Property RecordDate As Date Implements IRIPSSupportRecord.RecordDate
        Get
            Return RIPSDteDate.EditValue
        End Get
        Set(value As Date)
            RIPSDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>Ingreso asociado.</summary>
    Public Property AdmissionNumber As String Implements IRIPSSupportRecord.AdmissionNumber
        Get
            Return RIPSSleAdmission.EditValue
        End Get
        Set(value As String)
            RIPSSleAdmission.EditValue = value
        End Set
    End Property

    ''' <summary>Secuencia configurada para el código.</summary>
    Public Property Sequence As BillingSequence Implements IRIPSSupportRecord.Sequence
        Get
            Return _sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                DicSequense.Clear()
                For Each seq As BillingSequenceDetail In _sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
#End Region

#Region "FORM LIFECYCLE"

    ''' <summary>
    ''' Maneja el evento de cierre del formulario para liberar recursos.
    ''' </summary>
    ''' <param name="sender">Objeto que origina el evento.</param>
    ''' <param name="e">Argumentos del evento de cierre.</param>
    Private Async Sub FrmRIPSsupportrecord_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Liberar bloqueo de registro si existe
        Await DeleteBlockedRecord()

        ' Limpiar todos los controles y entidades
        CleanControls()

        ' Liberar recursos del popup
        If _popupRIPSDetails IsNot Nothing Then
            RemoveHandler _popupRIPSDetails.DetailAdded, AddressOf OnPopupDetailAdded
            _popupRIPSDetails.Dispose()
            _popupRIPSDetails = Nothing
        End If

        ' Limpiar referencias adicionales que no maneja CleanControls
        _presenter = Nothing
        _sequence = Nothing
    End Sub

#End Region

#Region "CRUD"

    ''' <summary>
    ''' Implementa la funcionalidad de búsqueda de registros RIPS.
    ''' Abre el formulario de búsqueda para seleccionar un registro existente.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios actuales y restablece el formulario a su estado inicial.
    ''' Limpia todos los controles y prepara la barra de herramientas según el modo.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        ' Limpiar todos los controles y entidades
        CleanControls()

        ' Deshabilitar controles excepto código
        DisableFormControls()

        ' Preparar la barra de herramientas según el modo actual
        If Not _searchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            End If
        End If

        RIPSBteCode.Focus()
    End Sub

    ''' <summary>
    ''' Implementa la funcionalidad de eliminación de registros RIPS.
    ''' </summary>
    ''' <remarks>Pendiente de implementación según requerimientos de negocio.</remarks>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar
        ' TODO: Implementar eliminación lógica del registro RIPS
        ' Considerar: validar estado, confirmar acción, actualizar estado a inactivo
        Mensaje(EeventViewerImages.Advertencia) = "Funcionalidad de eliminación no implementada"
    End Sub

    ''' <summary>
    ''' Guarda el registro RIPS actual de forma asíncrona.
    ''' Realiza validaciones previas antes de persistir los datos.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        Await SaveRIPSsupportrecordAsync()
    End Sub

    ''' <summary>
    ''' Crea un nuevo registro RIPS.
    ''' Valida la configuración de secuencia antes de proceder.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If

        If Me._sequence.IsManual Then
            Deshacer()
        Else
            ' Secuencia automática: genera código y habilita controles
            Await NewRIPSSupportRecord()
        End If
    End Sub

#End Region

#Region "METHODS"

    ''' <summary>
    ''' Carga el registro seleccionado desde la base de datos, aplica bloqueo de concurrencia
    ''' y actualiza la interfaz de usuario con los datos recuperados.
    ''' </summary>
    ''' <returns>Task que representa la operación asíncrona de carga.</returns>
    ''' <remarks>
    ''' Funcionalidades:
    ''' - Valida permisos de consulta
    ''' - Recupera el registro RIPS por código
    ''' - Implementa bloqueo optimista para edición concurrente
    ''' - Muestra información de auditoría
    ''' - Permite crear nuevo registro si no existe (secuencia manual)
    ''' </remarks>
    Private Async Function LoadControls() As Task
        If String.IsNullOrWhiteSpace(Code) Then
            Return
        End If

        If Not BarraBotones.PermiteConsultar Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Return
        End If

        Try
            AsyncLoader(True)

            ' Recuperar el registro RIPS por código
            Using model As New MRIPSSupportRecord(MyTag)
                _RIPSSupportRecord = Await model.GetRIPSSupportRecordByCode(Code.Trim())
            End Using

            If _RIPSSupportRecord IsNot Nothing AndAlso _RIPSSupportRecord.Id > 0 Then
                BarraBotones.StatusRecordVisible = True

                ' Gestionar bloqueo de registro para control de concurrencia
                Using modelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                    _record = Await modelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_RIPSSupportRecord.Id))

                    If _record Is Nothing OrElse _record.Id = 0 Then
                        ' No existe bloqueo previo, crear uno nuevo
                        Dim newBlock = New BlockRecordBilling With {
                            .BlockDate = Date.Now,
                            .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                            .NameUser = indigo.UserIndigoName,
                            .IdForm = Me.Tag,
                            .CodUser = indigo.UserIndigo,
                            .IdRecord = _RIPSSupportRecord.Id
                        }
                        Dim blockResult = Await modelRecord.SaveBlockRecord(newBlock)
                        If blockResult IsNot Nothing Then
                            _record = blockResult.ObjectEmbbeded
                        End If
                    Else
                        ' El registro está bloqueado por otro usuario
                        If Not _record.CodUser.Equals(indigo.UserIndigo, StringComparison.OrdinalIgnoreCase) Then
                            Dim lockMessage = $"El registro está en uso por {_record.NameUser} desde {_record.BlockDate:g}"
                            BarraBotones.ShowXtraMessage(lockMessage, ImagesXtraLabel.Warning, _record.CodUser)
                        End If
                    End If
                End Using

                ' Cargar datos en controles
                Code = _RIPSSupportRecord.Code
                RIPSCheHospitalStay.Checked = _RIPSSupportRecord.RIPSHospitalStayRecord

                ' Cargar información del ingreso asociado si existe
                If Not String.IsNullOrWhiteSpace(_RIPSSupportRecord.AdmissionNumber) Then
                    Await LoadAdmissionInfoAsync(_RIPSSupportRecord.AdmissionNumber)

                    ' Asignar el ingreso al control después de cargar la información
                    ' Esto evita disparar el evento EditValueChanged innecesariamente
                    _isClearingAdmission = True
                    Try
                        RIPSSleAdmission.EditValue = _RIPSSupportRecord.AdmissionNumber.Trim()
                    Finally
                        _isClearingAdmission = False
                    End Try

                    ' Configurar controles de fecha basados en el ingreso cargado
                    If _selectedAdmissionInfo IsNot Nothing Then
                        RIPSDteDate.Properties.MinValue = _selectedAdmissionInfo.AdmissionDate
                        RIPSDteDate.Properties.MaxValue = GetDateServer()
                    End If
                End If

                ' Asignar la fecha del registro (después de configurar los límites)
                RecordDate = _RIPSSupportRecord.OrderDate

                ' Cargar detalles de estancia hospitalaria en el grid
                LoadRIPSDetailsToGrid()

                ' Configurar controles y barra de botones según el estado del registro
                ConfigureFormByRecordStatus()
            Else
                ' El registro no existe
                If _sequence IsNot Nothing AndAlso _sequence.IsManual Then
                    ' Secuencia manual: crear nuevo registro con el código ingresado
                    _RIPSSupportRecord = New RIPSSupportRecord()
                    EnableFormControls()
                    SetControlsReadOnly(False)
                    RIPSBteCode.Enabled = False
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    RIPSSleAdmission.Focus()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El registro solicitado no existe."
                    Code = String.Empty
                    DisableFormControls()
                    RIPSBteCode.Focus()
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al cargar el registro: {ex.Message}"
            DisableFormControls()
            RIPSBteCode.Focus()
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Carga la información del ingreso asociado al registro RIPS de forma asíncrona.
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso a consultar.</param>
    Private Async Function LoadAdmissionInfoAsync(admissionNumber As String) As Task
        Await Task.Run(
            Sub()
                Using model As New MLiquidation()
                    Dim admissionCollection = model.GetAdmissionByNumIngres(admissionNumber)
                    If admissionCollection IsNot Nothing AndAlso admissionCollection.Count > 0 Then
                        _selectedAdmissionInfo = MapToAdmissionDTO(admissionCollection(0))
                    End If
                End Using
            End Sub)
    End Function

    ''' <summary>
    ''' Instancia un registro nuevo y aplica la secuencia automática.
    ''' Habilita los controles del formulario al obtener el código.
    ''' </summary>
    Private Async Function NewRIPSSupportRecord() As Task
        Try
            AsyncLoader(True)

            ' Limpiar antes de crear nuevo
            CleanControls()

            _RIPSSupportRecord = New RIPSSupportRecord() With {.Status = True}

            Dim codeAssigned As Boolean = False

            If _sequence.IsManual Then
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                codeAssigned = True
            Else
                If _sequence.Scope.Equals("O") Then
                    _idCurrentSequence = _sequence.BillingSequenceDetail(0).Id
                ElseIf _sequence.Scope.Equals("OU") Then
                    If _sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me.OperatingUnitId) Then
                        _idCurrentSequence = _sequence.BillingSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me.OperatingUnitId).Id
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        DisableFormControls()
                        Exit Function
                    End If
                End If

                If _sequence.Sequential Then
                    Code = ResourceManager.GetString("LabelOrTextboxNew")
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    codeAssigned = True
                Else
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            codeAssigned = True
                        Else
                            AsyncLoader(True)
                            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                                Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                            End Using
                            AsyncLoader(False)
                            If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                                codeAssigned = True
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            End If
                        End If
                    Else
                        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        codeAssigned = True
                    End If
                End If
            End If

            ' Si se asignó código, habilitar controles y configurar barra
            If codeAssigned Then
                EnableFormControls()
                SetControlsReadOnly(False)
                RIPSBteCode.Enabled = False ' El código ya está asignado, no editable
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                RIPSSleAdmission.Focus()
            Else
                DisableFormControls()
                RIPSBteCode.Focus()
            End If
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>Configura los grupos del panel lateral.</summary>
    Private Sub InitializeNavigationPanel()
        'CtrNavigationControlPanel1.AddNavigationGroup("1. Datos Principales", "RIPSLcgMainData")
        'CtrNavigationControlPanel1.AddNavigationGroup("2. Estancia Hospitalaria", "RIPSLcgHospitalStay")
    End Sub

    ''' <summary>
    ''' Restablece el estado base del formulario limpiando todos los controles de UI
    ''' y las entidades en memoria para preparar un nuevo registro o deshacer cambios.
    ''' </summary>
    ''' <remarks>
    ''' Este método debe llamarse en los siguientes escenarios:
    ''' - Al crear un nuevo registro (Nuevo)
    ''' - Al deshacer cambios (Deshacer)
    ''' - Antes de cargar un registro diferente (ReturnValue de búsqueda)
    ''' - Al cerrar el formulario (FormClosing)
    ''' </remarks>
    Private Sub CleanControls()
        ' Activar bandera para evitar eventos recursivos al limpiar el ingreso
        _isClearingAdmission = True

        Try
            ' Control de código - solo limpiar valor
            RIPSBteCode.Text = String.Empty

            ' Control de ingreso - solo limpiar valor
            RIPSSleAdmission.EditValue = Nothing
            RIPSSleAdmission.DisplayNullText = String.Empty

            ' Control de fecha - restablecer a fecha actual
            Dim serverDate = GetDateServer()
            RIPSDteDate.EditValue = serverDate
            RIPSDteDate.Properties.MinValue = serverDate
            RIPSDteDate.Properties.MaxValue = serverDate

            ' Sección de estancia hospitalaria - limpiar valores
            RIPSCheHospitalStay.Checked = False
            RIPSGcHospitalStay.DataSource = Nothing

            ' Limpiar entidades en memoria
            _RIPSSupportRecord = Nothing
            _selectedAdmissionInfo = Nothing
            _record = Nothing
            _idCurrentSequence = 0
            _popupRIPSDetails = Nothing

            ' Limpiar barra de botones y auditoría
            BarraBotones.StatusRecordVisible = False
            BarraBotones.CleanAuditBasic()

            ' Actualizar visibilidad de secciones
            UpdateRIPSSections()
        Finally
            _isClearingAdmission = False
        End Try
    End Sub

    ''' <summary>Sincroniza la sección de estancias con la UI.</summary>
    Private Sub UpdateRIPSSections()
        ' En el futuro, si se agregan más secciones RIPS, este OR se ampliará
        Dim anyRIPSSectionChecked As Boolean = RIPSCheHospitalStay.Checked

        ' Si no hay ninguna sección RIPS seleccionada, el grupo "Estancia Hospitalaria"
        ' (segmento Registro RIPS para Estancia) debe ser invisible para el usuario.
        RIPSLcgHospitalStay.Visibility = If(anyRIPSSectionChecked,
                                            LayoutVisibility.Always,
                                            LayoutVisibility.Never)

        ' Actualiza el estado del botón de agregar registro según el ingreso seleccionado
        UpdateAddRecordButtonState()
    End Sub

    ''' <summary>Habilita el botón de detalles según el contexto.</summary>
    Private Sub UpdateAddRecordButtonState()
        RIPSBtnAddRecord.Enabled = (_selectedAdmissionInfo IsNot Nothing) AndAlso RIPSCheHospitalStay.Checked
    End Sub

    ''' <summary>
    ''' Carga los detalles del registro RIPS actual en el grid de estancias hospitalarias.
    ''' Además, inicia la carga de propiedades extendidas en segundo plano.
    ''' </summary>
    Private Sub LoadRIPSDetailsToGrid()
        If _RIPSSupportRecord Is Nothing OrElse _RIPSSupportRecord.RIPSSupportRecordDetail Is Nothing Then
            RIPSGcHospitalStay.DataSource = Nothing
            Return
        End If

        ' Convertir a lista para poder modificar las propiedades extendidas
        Dim detailsList = _RIPSSupportRecord.RIPSSupportRecordDetail.ToList()

        ' Asignar al grid (las propiedades extendidas ya vienen del backend)
        RIPSGcHospitalStay.DataSource = detailsList
        RIPSGvHospitalStay.RefreshData()
    End Sub

    ''' <summary>
    ''' Deshabilita todos los controles del formulario excepto el de Código.
    ''' Se usa al iniciar el formulario o al deshacer cambios.
    ''' </summary>
    Private Sub DisableFormControls()
        ' Solo código habilitado
        RIPSBteCode.Enabled = True

        ' Demás controles deshabilitados
        RIPSSleAdmission.Enabled = False
        RIPSDteDate.Enabled = False
        RIPSCheHospitalStay.Enabled = False
        RIPSBtnAddRecord.Enabled = False
        RIPSGcHospitalStay.Enabled = True
    End Sub

    ''' <summary>
    ''' Habilita todos los controles del formulario.
    ''' Se usa cuando ya se tiene un código válido seleccionado.
    ''' </summary>
    Private Sub EnableFormControls()
        RIPSBteCode.Enabled = True
        RIPSSleAdmission.Enabled = True
        RIPSDteDate.Enabled = True
        RIPSCheHospitalStay.Enabled = True
        RIPSGcHospitalStay.Enabled = True
        ' El botón de agregar depende de la selección de ingreso
        UpdateAddRecordButtonState()
    End Sub

    ''' <summary>
    ''' Inicializa el grid de estancias hospitalarias agregando columnas adicionales
    ''' invisibles para mostrar en el popup de "Más Información".
    ''' </summary>
    Private Sub InitializeHospitalStayGrid()
        ' Agregar columnas adicionales invisibles para MoreInfoColumns
        ' Estas columnas se mostrarán en el popup "+ Info" del grid

        ' Código CUPS
        Dim colCUPSCode As New DevExpress.XtraGrid.Columns.GridColumn()
        colCUPSCode.Caption = "Código CUPS"
        colCUPSCode.FieldName = "CUPSCode"
        colCUPSCode.Name = "colCUPSCode"
        colCUPSCode.Visible = False
        colCUPSCode.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colCUPSCode)

        ' Código Unidad Funcional
        Dim colFunctionalUnitCode As New DevExpress.XtraGrid.Columns.GridColumn()
        colFunctionalUnitCode.Caption = "Unidad Funcional"
        colFunctionalUnitCode.FieldName = "FunctionalUnitCode"
        colFunctionalUnitCode.Name = "colFunctionalUnitCode"
        colFunctionalUnitCode.Visible = False
        colFunctionalUnitCode.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colFunctionalUnitCode)

        ' Tipo de Estancia
        Dim colTypeStay As New DevExpress.XtraGrid.Columns.GridColumn()
        colTypeStay.Caption = "Tipo Estancia"
        colTypeStay.FieldName = "TypeStay"
        colTypeStay.Name = "colTypeStay"
        colTypeStay.Visible = False
        colTypeStay.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colTypeStay)

        ' Código Profesional
        Dim colProfessionalCode As New DevExpress.XtraGrid.Columns.GridColumn()
        colProfessionalCode.Caption = "Código Profesional"
        colProfessionalCode.FieldName = "ProfessionalCode"
        colProfessionalCode.Name = "colProfessionalCode"
        colProfessionalCode.Visible = False
        colProfessionalCode.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colProfessionalCode)

        ' NIT Médico
        Dim colNitMedico As New DevExpress.XtraGrid.Columns.GridColumn()
        colNitMedico.Caption = "NIT Médico"
        colNitMedico.FieldName = "NitMedico"
        colNitMedico.Name = "colNitMedico"
        colNitMedico.Visible = False
        colNitMedico.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colNitMedico)

        ' Especialidad
        Dim colSpecialtyCode As New DevExpress.XtraGrid.Columns.GridColumn()
        colSpecialtyCode.Caption = "Especialidad"
        colSpecialtyCode.FieldName = "SpecialtyCode"
        colSpecialtyCode.Name = "colSpecialtyCode"
        colSpecialtyCode.Visible = False
        colSpecialtyCode.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colSpecialtyCode)

        ' Diagnóstico Principal
        Dim colMainDiagnosis As New DevExpress.XtraGrid.Columns.GridColumn()
        colMainDiagnosis.Caption = "Dx Principal"
        colMainDiagnosis.FieldName = "MainDiagnosisId"
        colMainDiagnosis.Name = "colMainDiagnosis"
        colMainDiagnosis.Visible = False
        colMainDiagnosis.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colMainDiagnosis)

        ' Diagnóstico Relacionado
        Dim colRelatedDiagnosis As New DevExpress.XtraGrid.Columns.GridColumn()
        colRelatedDiagnosis.Caption = "Dx Relacionado"
        colRelatedDiagnosis.FieldName = "RelatedDiagnosisId"
        colRelatedDiagnosis.Name = "colRelatedDiagnosis"
        colRelatedDiagnosis.Visible = False
        colRelatedDiagnosis.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colRelatedDiagnosis)

        ' Condición de Egreso
        Dim colDischargeCondition As New DevExpress.XtraGrid.Columns.GridColumn()
        colDischargeCondition.Caption = "Condición Egreso"
        colDischargeCondition.FieldName = "DischargeCondition"
        colDischargeCondition.Name = "colDischargeCondition"
        colDischargeCondition.Visible = False
        colDischargeCondition.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colDischargeCondition)

        ' Código Paciente
        Dim colPatientCode As New DevExpress.XtraGrid.Columns.GridColumn()
        colPatientCode.Caption = "Código Paciente"
        colPatientCode.FieldName = "PatientCode"
        colPatientCode.Name = "colPatientCode"
        colPatientCode.Visible = False
        colPatientCode.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colPatientCode)

        ' Número de Ingreso
        Dim colAdmissionNumber As New DevExpress.XtraGrid.Columns.GridColumn()
        colAdmissionNumber.Caption = "Nro. Ingreso"
        colAdmissionNumber.FieldName = "AdmissionNumber"
        colAdmissionNumber.Name = "colAdmissionNumber"
        colAdmissionNumber.Visible = False
        colAdmissionNumber.OptionsColumn.AllowEdit = False
        RIPSGvHospitalStay.Columns.Add(colAdmissionNumber)

        ' Configurar MoreInfoColumns usando el componente IndigoGridView
        IndigoGridView1.MoreInfoColunmns(RIPSGvHospitalStay)

        ' Configurar acciones del grid (Editar y Eliminar)
        Dim listActions As New List(Of eAcciones)
        listActions.Add(eAcciones.Remove)
        listActions.Add(eAcciones.Edit)

        IndigoGridView1.SetListAcction(RIPSGvHospitalStay, listActions)

        ' Ajustar ancho de la columna de acciones
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In RIPSGvHospitalStay.Columns
            If col.Name = "colActions" Then
                col.Width = 100
                Exit For
            End If
        Next
    End Sub

    ''' <summary>Inicializa el origen de datos del selector de ingresos.</summary>
    Private Async Function InitializeAdmissionControlAsync() As Task
        _indigoSession = SessionValues.Instance

        Await Task.Factory.StartNew(
            Sub()
                Using model As New MServiceOrder(MyTag.ToString())
                    Dim ds = model.GetViewAdmissionOpenAndPartialServiceOrder()
                    RIPSSleAdmission.SafeInvoke(
                        Sub()
                            RIPSSleAdmission.Datasource = ds
                        End Sub)
                End Using
            End Sub)

        RIPSSleAdmission.FuncQueryOnKeyEnterPressed = AddressOf RIPSSleAdmission_KeyDown
    End Function

    ''' <summary>
    ''' Aplica las validaciones del ingreso seleccionado y configura los controles de fecha.
    ''' </summary>
    ''' <param name="admissionDTO">DTO con información del ingreso.</param>
    Private Sub ProcessAdmissionSelection(admissionDTO As AdmissionInfoDTO)
        If admissionDTO Is Nothing Then
            ClearAdmissionSelection()
            Return
        End If

        ' Valida que el ingreso no esté facturado
        If admissionDTO.Status = "F" Then
            Mensaje(EeventViewerImages.Advertencia) = "El ingreso seleccionado ya se encuentra facturado"
            ClearAdmissionSelection()
            Return
        End If

        ' Guarda la información de la admisión seleccionada
        _selectedAdmissionInfo = admissionDTO

        ' Actualiza el estado del botón de agregar registro según las secciones activas
        UpdateAddRecordButtonState()

        ' Configurar fecha con la lógica de FrmOrdersService
        ' - Valor por defecto: fecha actual del servidor
        ' - MinValue: fecha de ingreso
        ' - MaxValue: fecha actual (no permite fechas futuras)
        RIPSDteDate.EditValue = GetDateServer()
        RIPSDteDate.Properties.MinValue = admissionDTO.AdmissionDate
        RIPSDteDate.Properties.MaxValue = CDate(RIPSDteDate.EditValue)

        RIPSDteDate.Focus()
    End Sub

    ''' <summary>
    ''' Limpia la selección de ingreso y restablece los controles relacionados.
    ''' Usa una bandera para evitar recursión con el evento EditValueChanged.
    ''' </summary>
    Private Sub ClearAdmissionSelection()
        ' Evitar recursión si ya estamos limpiando
        If _isClearingAdmission Then
            Return
        End If

        Try
            _isClearingAdmission = True

            _selectedAdmissionInfo = Nothing
            RIPSSleAdmission.EditValue = Nothing
            RIPSSleAdmission.DisplayNullText = String.Empty
            UpdateAddRecordButtonState()
        Finally
            _isClearingAdmission = False
        End Try

        RIPSSleAdmission.Focus()
    End Sub

    ''' <summary>
    ''' Convierte la entidad XPO del ingreso en un DTO local mediante reflexión.
    ''' </summary>
    ''' <param name="admissionObject">Objeto de admisión XPO a mapear.</param>
    ''' <returns>DTO con la información del ingreso mapeada, o Nothing si el objeto es nulo.</returns>
    ''' <remarks>
    ''' Utiliza reflexión para evitar dependencias directas con entidades XPO
    ''' y maneja propiedades calculadas (PersistentAlias) de forma segura.
    ''' </remarks>
    Private Function MapToAdmissionDTO(admissionObject As Object) As AdmissionInfoDTO
        If admissionObject Is Nothing Then
            Return Nothing
        End If

        Dim dto As New AdmissionInfoDTO()
        Dim objType = admissionObject.GetType()

        ' Helper para mapear propiedades String de forma segura
        Dim mapString = Sub(propName As String, setter As Action(Of String))
                            Try
                                Dim prop = objType.GetProperty(propName)
                                If prop IsNot Nothing AndAlso prop.CanRead Then
                                    ' Evitar propiedades ReadOnly calculadas que pueden causar StackOverflow
                                    ' Solo leer si es escribible o si es una propiedad simple de solo lectura
                                    Dim val = prop.GetValue(admissionObject)
                                    If val IsNot Nothing Then setter(val.ToString())
                                End If
                            Catch ex As Exception
                                ' Log silencioso para debugging - no afecta flujo principal
                                System.Diagnostics.Debug.WriteLine($"MapToAdmissionDTO: Error mapeando '{propName}': {ex.Message}")
                            End Try
                        End Sub

        ' Helper para mapear propiedades Integer de forma segura
        Dim mapInt = Sub(propName As String, setter As Action(Of Integer))
                         Try
                             Dim prop = objType.GetProperty(propName)
                             If prop IsNot Nothing AndAlso prop.CanRead Then
                                 Dim val = prop.GetValue(admissionObject)
                                 If val IsNot Nothing Then setter(CInt(val))
                             End If
                         Catch ex As Exception
                             System.Diagnostics.Debug.WriteLine($"MapToAdmissionDTO: Error mapeando '{propName}': {ex.Message}")
                         End Try
                     End Sub

        ' Helper para mapear propiedades Integer? (nullable) de forma segura
        Dim mapIntNullable = Sub(propName As String, setter As Action(Of Integer?))
                                 Try
                                     Dim prop = objType.GetProperty(propName)
                                     If prop IsNot Nothing AndAlso prop.CanRead Then
                                         Dim val = prop.GetValue(admissionObject)
                                         If val IsNot Nothing Then setter(CInt(val))
                                     End If
                                 Catch ex As Exception
                                     System.Diagnostics.Debug.WriteLine($"MapToAdmissionDTO: Error mapeando '{propName}': {ex.Message}")
                                 End Try
                             End Sub

        ' Helper para mapear propiedades DateTime de forma segura
        Dim mapDateTime = Sub(propName As String, setter As Action(Of DateTime))
                              Try
                                  Dim prop = objType.GetProperty(propName)
                                  If prop IsNot Nothing AndAlso prop.CanRead Then
                                      Dim val = prop.GetValue(admissionObject)
                                      If val IsNot Nothing AndAlso TypeOf val Is DateTime Then setter(CDate(val))
                                  End If
                              Catch ex As Exception
                                  System.Diagnostics.Debug.WriteLine($"MapToAdmissionDTO: Error mapeando '{propName}': {ex.Message}")
                              End Try
                          End Sub

        ' Información básica de admisión
        mapString("AdmissionCode", Sub(v) dto.AdmissionCode = v)
        mapString("AdmissionCodeWithOutTrim", Sub(v) dto.AdmissionCodeWithOutTrim = v)
        mapDateTime("AdmissionDate", Sub(v) dto.AdmissionDate = v)
        mapString("Status", Sub(v) dto.Status = v)
        mapString("StatusName", Sub(v) dto.StatusName = v)
        mapInt("AdmissionType", Sub(v) dto.AdmissionType = v)
        mapString("AdmissionTypeName", Sub(v) dto.AdmissionTypeName = v)
        mapInt("AdmissionCaregroupId", Sub(v) dto.AdmissionCaregroupId = v)
        mapInt("AdmissionReason", Sub(v) dto.AdmissionReason = v)
        mapInt("AdmissionRiskType", Sub(v) dto.AdmissionRiskType = v)
        mapInt("PlaceEntry", Sub(v) dto.PlaceEntry = v)
        mapString("BedStay", Sub(v) dto.BedStay = v)
        mapInt("LiquidationType", Sub(v) dto.LiquidationType = v)
        mapString("AuthorizationNumber", Sub(v) dto.AuthorizationNumber = v)

        ' Información de entidad y administradora
        mapString("EntityCode", Sub(v) dto.EntityCode = v)
        mapString("EntityName", Sub(v) dto.EntityName = v)
        mapInt("HealthAdministratorId", Sub(v) dto.HealthAdministratorId = v)
        mapString("CareGroupCodeName", Sub(v) dto.CareGroupCodeName = v)
        mapString("AdmissionCareGroupCodeName", Sub(v) dto.AdmissionCareGroupCodeName = v)

        ' Información de paciente
        mapString("PatientCode", Sub(v) dto.PatientCode = v)
        mapString("PatientName", Sub(v) dto.PatientName = v)
        mapInt("PatientType", Sub(v) dto.PatientType = v)
        mapInt("PatientAfiliation", Sub(v) dto.PatientAfiliation = v)
        mapInt("PatientCareGroupId", Sub(v) dto.PatientCareGroupId = v)

        ' Información de centro de atención y unidad funcional
        mapString("AdmissionCentAtencCodeName", Sub(v) dto.AdmissionCentAtencCodeName = v)
        mapString("AdmissionUniFuncCodeName", Sub(v) dto.AdmissionUniFuncCodeName = v)
        mapString("UniFuncEgre", Sub(v) dto.UniFuncEgre = v)

        ' Información adicional
        mapIntNullable("RevenueControlId", Sub(v) dto.RevenueControlId = v)
        mapIntNullable("ThirdPartyPatientId", Sub(v) dto.ThirdPartyPatientId = v)
        mapIntNullable("IncomeLockType", Sub(v) dto.IncomeLockType = v)

        Return dto
    End Function


    ''' <summary>
    ''' Guarda el registro de soporte RIPS de forma asíncrona.
    ''' Realiza validaciones completas del ingreso, estado y datos requeridos
    ''' antes de persistir en base de datos.
    ''' </summary>
    ''' <returns>Task que representa la operación asíncrona.</returns>
    ''' <remarks>
    ''' Validaciones realizadas:
    ''' - Existencia de ingreso seleccionado
    ''' - Estado válido del ingreso (abierto, parcial, o bloqueado con tipo especial)
    ''' - Grupo de atención asociado
    ''' - Al menos un detalle de estancia hospitalaria
    ''' - Fecha de registro válida
    ''' </remarks>
    Private Async Function SaveRIPSsupportrecordAsync() As Task
        Try
            AsyncLoader(True)

            ' Validar que existe un ingreso seleccionado
            If _selectedAdmissionInfo Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un ingreso antes de guardar"
                Return
            End If

            Dim admissionState = _selectedAdmissionInfo.Status

            ' Validar que el ingreso no esté facturado
            If admissionState = ADMISSION_STATUS_INVOICED Then
                Mensaje(EeventViewerImages.Advertencia) = "El ingreso seleccionado ya se encuentra facturado"
                Return
            End If

            ' Validar que el ingreso esté en un estado válido para generar registros RIPS
            ' Se permite cuando:
            ' 1. El ingreso está abierto (" ")
            ' 2. El ingreso está parcial ("P")
            ' 3. El ingreso está bloqueado ("B") pero con IncomeLockType = 1 (bloqueo especial permitido)
            Dim isValidState As Boolean = ValidateAdmissionState(admissionState)

            If Not isValidState Then
                Mensaje(EeventViewerImages.Advertencia) = "El ingreso debe estar en estado abierto o parcial para generar registros RIPS"
                Return
            End If

            ' Validar grupo de atención
            If _selectedAdmissionInfo.AdmissionCaregroupId <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El ingreso no tiene asociado un grupo de atención"
                Return
            End If

            ' Validar fecha del registro
            If RecordDate = DateTime.MinValue OrElse RecordDate = Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha del registro no es válida"
                Return
            End If

            ' Validar que la fecha no sea futura
            Dim serverDate = GetDateServer()
            If RecordDate.Date > serverDate.Date Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha del registro no puede ser posterior a la fecha actual"
                Return
            End If

            ' Validar que la fecha no sea anterior a la fecha de ingreso
            If RecordDate.Date < _selectedAdmissionInfo.AdmissionDate.Date Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha del registro no puede ser anterior a la fecha de ingreso del paciente"
                Return
            End If

            ' Validar sección RIPS seleccionada
            If Not RIPSCheHospitalStay.Checked Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una sección RIPS (Estancia Hospitalaria)"
                Return
            End If

            ' Obtener los detalles del grid
            Dim detailsList = TryCast(RIPSGcHospitalStay.DataSource, List(Of RIPSSupportRecordDetail))
            If detailsList Is Nothing OrElse detailsList.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos un registro de estancia hospitalaria"
                Return
            End If

            ' Validar sesión de usuario
            If _indigoSession Is Nothing Then
                _indigoSession = SessionValues.Instance
            End If

            ' Determinar si es nuevo o actualización
            Dim isUpdate As Boolean = _RIPSSupportRecord IsNot Nothing AndAlso _RIPSSupportRecord.Id > 0
            Dim supportRecord As RIPSSupportRecord

            If isUpdate Then
                ' Actualización: usar el registro existente
                supportRecord = _RIPSSupportRecord
                supportRecord.OrderDate = RecordDate
                supportRecord.AdmissionNumber = _selectedAdmissionInfo.AdmissionCode
                supportRecord.RIPSHospitalStayRecord = RIPSCheHospitalStay.Checked
                supportRecord.ModificationUser = _indigoSession.UserIndigo
                supportRecord.ModificationDate = serverDate

                ' IMPORTANTE: Preservar el ChangeTracker original para que el servicio
                ' identifique el registro como existente y ejecute UPDATE en lugar de INSERT
                If supportRecord.ChangeTracker Is Nothing Then
                    supportRecord.ChangeTracker = New ObjectChangeTracker()
                End If
                supportRecord.ChangeTracker.State = ObjectState.Modified

                ' Limpiar detalles existentes y agregar los nuevos
                supportRecord.RIPSSupportRecordDetail.Clear()
            Else
                ' Nuevo registro
                supportRecord = New RIPSSupportRecord() With {
                    .Code = Code.Trim(),
                    .OrderDate = RecordDate,
                    .AdmissionNumber = _selectedAdmissionInfo.AdmissionCode,
                    .RIPSHospitalStayRecord = RIPSCheHospitalStay.Checked,
                    .Status = 0,
                    .CreationUser = _indigoSession.UserIndigo,
                    .CreationDate = serverDate,
                    .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added}
                }
            End If

            ' Agregar los detalles a la entidad
            For Each detail In detailsList
                ' Si el detalle es nuevo (Id = 0) o si estamos actualizando, marcar como Added
                ' El servicio gestionará la lógica de inserción/actualización
                If detail.Id = 0 OrElse isUpdate Then
                    detail.ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added}
                End If
                supportRecord.RIPSSupportRecordDetail.Add(detail)
            Next

            ' Guardar usando el modelo
            Dim result As ActionResult(Of RIPSSupportRecord)
            Using model As New MRIPSSupportRecord(CStr(MyTag))
                result = Await model.SaveRIPSSupportRecord(supportRecord, BarraBotones.OperatingUnitValue, _indigoSession.AuditMessageWcf, _idCurrentSequence)
            End Using

            ' Nota: StateResult = True indica éxito, False indica error
            If result IsNot Nothing AndAlso result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing Then
                Dim savedCode = result.ObjectEmbbeded.Code
                Dim successMessage = If(isUpdate, "actualizado", "guardado")
                Mensaje(EeventViewerImages.Informacion) = If(String.IsNullOrEmpty(result.Message),
                                                             $"Registro {savedCode} {successMessage} exitosamente",
                                                             result.Message)

                ' Después de guardar/actualizar exitosamente: limpiar y preparar para nuevo registro
                CleanControls()
                DisableFormControls()
                BarraBotones.PrepareToolbar(eAction.NewAndFind)
                RIPSBteCode.Focus()
            Else
                Dim errorMessage = If(result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.Message),
                                      result.Message,
                                      "Error desconocido al guardar el registro")
                Mensaje(EeventViewerImages.MensajeError) = errorMessage
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al guardar el registro RIPS: {ex.Message}"
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Valida si el estado del ingreso permite crear registros RIPS.
    ''' </summary>
    ''' <param name="admissionState">Estado actual del ingreso.</param>
    ''' <returns>True si el estado es válido para crear RIPS; False en caso contrario.</returns>
    Private Function ValidateAdmissionState(admissionState As String) As Boolean
        ' Estados abierto o parcial: siempre válidos
        If admissionState = ADMISSION_STATUS_OPEN OrElse admissionState = ADMISSION_STATUS_PARTIAL Then
            Return True
        End If

        ' Estado bloqueado pero con tipo de bloqueo especial (IncomeLockType = 1): válido
        If admissionState = ADMISSION_STATUS_BLOCKED AndAlso
           _selectedAdmissionInfo.IncomeLockType.HasValue AndAlso
           _selectedAdmissionInfo.IncomeLockType.Value = INCOME_LOCK_TYPE_ALLOWED Then
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' Configura el formulario (controles y barra de botones) según el estado del registro RIPS.
    ''' </summary>
    ''' <remarks>
    ''' Estados y acciones permitidas:
    ''' - Status = 0 (Registrado): Puede confirmar, deshacer, guardar
    ''' - Status = 1 (Confirmado): Solo puede anular, deshacer
    ''' - Status = 2 (Anulado): Solo puede deshacer, todos los controles readonly/disabled
    ''' </remarks>
    Private Sub ConfigureFormByRecordStatus()
        If _RIPSSupportRecord Is Nothing OrElse _RIPSSupportRecord.Id = 0 Then
            Return
        End If

        ' Mostrar información de auditoría siempre que hay un registro existente
        BarraBotones.StatusRecordVisible = True
        BarraBotones.CleanAuditBasic()
        BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), _RIPSSupportRecord.CreationUser)
        BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), _RIPSSupportRecord.CreationDate)

        ' Solo mostrar modificación si existe información
        If Not String.IsNullOrWhiteSpace(_RIPSSupportRecord.ModificationUser) Then
            BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), _RIPSSupportRecord.ModificationUser)
        End If
        If _RIPSSupportRecord.ModificationDate <> DateTime.MinValue Then
            BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), _RIPSSupportRecord.ModificationDate)
        End If

        ' Establecer el estado del registro en la barra de botones
        BarraBotones.StatusRecord = _RIPSSupportRecord.Status.ToString()

        Dim recordStatus = _RIPSSupportRecord.Status

        Select Case recordStatus
            Case RIPS_STATUS_REGISTERED
                ' Status = 0: Registrado - Puede confirmar, deshacer, guardar
                EnableFormControls()
                SetControlsReadOnly(False)
                RIPSBteCode.Enabled = False ' Código no editable
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                ' Mostrar botón confirmar si tiene permisos
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False

            Case RIPS_STATUS_CONFIRMED
                ' Status = 1: Confirmado - Solo puede anular, deshacer
                ' Todos los controles readonly Y deshabilitados
                DisableFormControls()
                SetControlsReadOnly(True)
                ' Asegurar que el botón de agregar registro esté deshabilitado
                RIPSBtnAddRecord.Enabled = False
                RIPSBteCode.Enabled = False
                BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                ' Mostrar botón anular si tiene permisos
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False

            Case RIPS_STATUS_CANCELLED
                ' Status = 2: Anulado - Solo puede deshacer, todo readonly/disabled
                DisableFormControls()
                SetControlsReadOnly(True)
                RIPSBteCode.Enabled = False
                BarraBotones.PrepareToolbar(eAction.OnlyUndo)

            Case Else
                ' Estado desconocido: comportamiento por defecto
                EnableFormControls()
                SetControlsReadOnly(False)
                RIPSBteCode.Enabled = False
                BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        End Select
    End Sub

    ''' <summary>
    ''' Establece los controles en modo solo lectura o editable según el parámetro.
    ''' </summary>
    ''' <param name="value">True para modo solo lectura, False para editable.</param>
    Private Sub SetControlsReadOnly(value As Boolean)
        RIPSSleAdmission.IsReadOnly = value
        RIPSDteDate.Properties.ReadOnly = value
        RIPSCheHospitalStay.Properties.ReadOnly = value
        RIPSGcHospitalStay.Enabled = Not value
        ' El botón de agregar registro también debe estar deshabilitado cuando es readonly
        RIPSBtnAddRecord.Enabled = Not value AndAlso (_selectedAdmissionInfo IsNot Nothing) AndAlso RIPSCheHospitalStay.Checked
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones.
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {
            .StatusValue = "0",
            .StatusName = "Registrado",
            .StatusColor = System.Drawing.Color.FromArgb(104, 33, 204)
        })
        listStates.Add(New StatusRecord With {
            .StatusValue = "1",
            .StatusName = "Confirmado",
            .StatusColor = System.Drawing.Color.FromArgb(0, 122, 204)
        })
        listStates.Add(New StatusRecord With {
            .StatusValue = "2",
            .StatusName = "Anulado",
            .StatusColor = System.Drawing.Color.FromArgb(231, 76, 60)
        })
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Confirma el registro RIPS cambiando su estado a Confirmado (Status = 1).
    ''' </summary>
    Public Async Sub Confirmar()
        If _RIPSSupportRecord Is Nothing OrElse _RIPSSupportRecord.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay un registro para confirmar"
            Return
        End If

        If _RIPSSupportRecord.Status <> RIPS_STATUS_REGISTERED Then
            Mensaje(EeventViewerImages.Advertencia) = "Solo se pueden confirmar registros en estado Registrado"
            Return
        End If

        Try
            AsyncLoader(True)

            ' Confirmar registro usando método específico que solo cambia el estado
            Dim result As ActionResult(Of RIPSSupportRecord)
            Using model As New MRIPSSupportRecord(CStr(MyTag))
                result = Await model.ConfirmRIPSSupportRecord(_RIPSSupportRecord.Id, indigo.AuditMessageWcf)
            End Using

            If result IsNot Nothing AndAlso result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing Then
                Dim confirmedCode = result.ObjectEmbbeded.Code
                Mensaje(EeventViewerImages.Informacion) = $"Registro {confirmedCode} confirmado exitosamente"

                ' Después de confirmar: limpiar y preparar para nuevo registro
                CleanControls()
                DisableFormControls()
                BarraBotones.PrepareToolbar(eAction.NewAndFind)
                RIPSBteCode.Focus()
            Else
                Dim errorMessage = If(result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.Message),
                                      result.Message,
                                      "Error desconocido al confirmar el registro")
                Mensaje(EeventViewerImages.MensajeError) = errorMessage
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al confirmar el registro: {ex.Message}"
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Anula el registro RIPS cambiando su estado a Anulado (Status = 2).
    ''' </summary>
    Public Async Sub Anular()
        If _RIPSSupportRecord Is Nothing OrElse _RIPSSupportRecord.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay un registro para anular"
            Return
        End If

        If _RIPSSupportRecord.Status <> RIPS_STATUS_CONFIRMED Then
            Mensaje(EeventViewerImages.Advertencia) = "Solo se pueden anular registros en estado Confirmado"
            Return
        End If

        ' Confirmar acción con el usuario
        Dim confirmResult = MessageIndigo.Show("¿Está seguro que desea anular el registro RIPS?",
                                                 MessageType.Question,
                                                 Me.Text,
                                                 Botones.SiNo,
                                                 "")

        If confirmResult <> DialogResult.Yes Then
            Return
        End If

        Try
            AsyncLoader(True)

            ' Actualizar estado a Anulado
            _RIPSSupportRecord.Status = RIPS_STATUS_CANCELLED
            _RIPSSupportRecord.ModificationUser = indigo.UserIndigo
            _RIPSSupportRecord.ModificationDate = GetDateServer()

            ' Preservar ChangeTracker para UPDATE
            If _RIPSSupportRecord.ChangeTracker Is Nothing Then
                _RIPSSupportRecord.ChangeTracker = New ObjectChangeTracker()
            End If
            _RIPSSupportRecord.ChangeTracker.State = ObjectState.Modified

            ' Guardar cambios
            Dim result As ActionResult(Of RIPSSupportRecord)
            Using model As New MRIPSSupportRecord(CStr(MyTag))
                result = Await model.SaveRIPSSupportRecord(_RIPSSupportRecord, BarraBotones.OperatingUnitValue, indigo.AuditMessageWcf)
            End Using

            If result IsNot Nothing AndAlso result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing Then
                _RIPSSupportRecord = result.ObjectEmbbeded
                Mensaje(EeventViewerImages.Informacion) = $"Registro {_RIPSSupportRecord.Code} anulado exitosamente"

                ' Reconfigurar formulario según nuevo estado
                ConfigureFormByRecordStatus()
            Else
                Dim errorMessage = If(result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.Message),
                                      result.Message,
                                      "Error desconocido al anular el registro")
                Mensaje(EeventViewerImages.MensajeError) = errorMessage
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al anular el registro: {ex.Message}"
        Finally
            AsyncLoader(False)
        End Try
    End Sub
#End Region

#Region "EVENTS"
    ''' <summary>
    ''' Evento load de la barra de usuarios.
    ''' Inicializa permisos, presentador, controles y grid de estancias.
    ''' </summary>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Try
            _isInitializing = True

            ' Configurar permisos y barra de herramientas
            BarraBotones.ActualizarPermisosBarra(CStr(MyTag))
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)

            ' Configurar estados disponibles para el registro RIPS
            ' Los valores 0, 1, 2 corresponden a Registrado, Confirmado, Anulado
            BarraBotones.StatesWhitActions = {RIPS_STATUS_REGISTERED, RIPS_STATUS_CONFIRMED, RIPS_STATUS_CANCELLED}

            ' Cargar lista de estados para mostrar en la barra de botones
            LoadStatus()

            Me.OperatingUnitId = BarraBotones.OperatingUnitValue

            ' Inicializar presentador y secuencia
            _presenter = New PRIPSSupportRecord(Me, MyTag)
            _presenter.GetSequense()

            ' Inicializa el control de ingreso con el mismo origen de datos que FrmOrdersService
            Await InitializeAdmissionControlAsync()

            ' Fecha: inicial al cargar el formulario
            RIPSDteDate.EditValue = GetDateServer()
            RIPSDteDate.Properties.MinValue = CDate(RIPSDteDate.EditValue)
            RIPSDteDate.Properties.MaxValue = CDate(RIPSDteDate.EditValue)

            ' Inicializar grid de estancias hospitalarias con columnas adicionales
            InitializeHospitalStayGrid()

            ' Inicialmente, sin secciones RIPS seleccionadas, se oculta "Estancia Hospitalaria"
            UpdateRIPSSections()

            ' Al inicio: solo código habilitado, demás controles deshabilitados
            DisableFormControls()
            RIPSBteCode.Focus()
        Finally
            _isInitializing = False
        End Try
    End Sub

    ''' <summary>
    ''' Evento click del botón buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, RIPSBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Evento click del botón deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento click del botón guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento click del botón actualizar.
    ''' Usa el mismo método de guardado ya que el servicio gestiona la lógica
    ''' de inserción/actualización basándose en el Id del registro.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento click del botón confirmar.
    ''' Cambia el estado del registro a Confirmado.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        BarraBotones.Focus()
        Confirmar()
    End Sub

    ''' <summary>
    ''' Evento click del botón anular.
    ''' Cambia el estado del registro a Anulado.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        BarraBotones.Focus()
        Anular()
    End Sub

    ''' <summary>
    ''' Evento click del botón agregar registro de estancia hospitalaria.
    ''' Abre el popup de detalles para agregar nuevos registros de estancia.
    ''' </summary>
    ''' <param name="sender">Objeto que origina el evento.</param>
    ''' <param name="e">Argumentos del evento.</param>
    Private Sub RIPSBtnAddRecord_Click(sender As Object, e As EventArgs) Handles RIPSBtnAddRecord.Click
        ' Validar que haya un ingreso seleccionado
        If RIPSSleAdmission.EditValue Is Nothing OrElse _selectedAdmissionInfo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un ingreso válido"
            RIPSSleAdmission.Focus()
            Return
        End If

        ' Validar estado del ingreso antes de agregar registros
        If _selectedAdmissionInfo.Status = ADMISSION_STATUS_INVOICED Then
            Mensaje(EeventViewerImages.Advertencia) = "No se pueden agregar registros a un ingreso facturado"
            Return
        End If

        Try
            ' Crear o reutilizar el popup de detalles
            If _popupRIPSDetails Is Nothing OrElse _popupRIPSDetails.IsDisposed Then
                _popupRIPSDetails = New FrmPopUpRIPSsupportrecordDetails()
                ' Suscribirse al evento para actualizar el grid en tiempo real
                AddHandler _popupRIPSDetails.DetailAdded, AddressOf OnPopupDetailAdded
            End If

            ' Pasar datos del ingreso al popup usando el DTO almacenado
            _popupRIPSDetails.AdmissionCode = _selectedAdmissionInfo.AdmissionCode
            _popupRIPSDetails.AdmissionDate = _selectedAdmissionInfo.AdmissionDate
            _popupRIPSDetails.PatientCode = _selectedAdmissionInfo.PatientCode

            ' Mostrar popup y procesar resultado
            If _popupRIPSDetails.ShowDialog(Me) = DialogResult.OK Then
                ProcessPopupResult()
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al abrir el formulario de detalles: {ex.Message}"
        End Try
    End Sub

    ''' <summary>
    ''' Manejador del evento DetailAdded del popup.
    ''' Actualiza el grid de estancias hospitalarias en tiempo real cuando se agrega un detalle.
    ''' </summary>
    ''' <param name="detail">El detalle de estancia hospitalaria agregado.</param>
    Private Sub OnPopupDetailAdded(detail As RIPSSupportRecordDetail)
        If detail Is Nothing Then Return

        Try
            ' Obtener la lista actual del grid o crear una nueva
            Dim currentList = TryCast(RIPSGcHospitalStay.DataSource, List(Of RIPSSupportRecordDetail))
            If currentList Is Nothing Then
                currentList = New List(Of RIPSSupportRecordDetail)()
            End If

            ' Validar duplicados por fecha antes de agregar
            Dim isDuplicate = currentList.Any(Function(d) d.InitialDate = detail.InitialDate AndAlso
                                                          d.FinalDate = detail.FinalDate AndAlso
                                                          d.Id = 0)
            If Not isDuplicate Then
                ' Agregar el nuevo detalle
                currentList.Add(detail)

                ' Actualizar el datasource y refrescar el grid
                RIPSGcHospitalStay.DataSource = Nothing
                RIPSGcHospitalStay.DataSource = currentList
                RIPSGvHospitalStay.RefreshData()
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"OnPopupDetailAdded: Error al actualizar grid: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Procesa el resultado del popup de detalles y actualiza el grid de estancias.
    ''' Maneja tanto la adición de nuevos detalles como la actualización de existentes.
    ''' </summary>
    Private Sub ProcessPopupResult()
        If _popupRIPSDetails Is Nothing Then Return

        ' Obtener los detalles agregados/actualizados desde el popup
        Dim newDetails = _popupRIPSDetails.AddedDetailRecord
        If newDetails Is Nothing OrElse newDetails.Count = 0 Then
            Return
        End If

        ' Obtener la lista actual del grid o crear una nueva
        Dim currentList = TryCast(RIPSGcHospitalStay.DataSource, List(Of RIPSSupportRecordDetail))
        If currentList Is Nothing Then
            currentList = New List(Of RIPSSupportRecordDetail)()
        End If

        ' Procesar cada detalle del popup
        Dim addedCount As Integer = 0
        Dim updatedCount As Integer = 0
        Dim editIndexFromPopup As Integer = _popupRIPSDetails.EditIndex
        Dim isEditModeFromPopup As Boolean = _popupRIPSDetails.IsEditMode

        For Each newDetail In newDetails
            ' Si tiene Id, es una edición de un detalle guardado
            If newDetail.Id > 0 Then
                Dim existingIndex = currentList.FindIndex(Function(d) d.Id = newDetail.Id)
                If existingIndex >= 0 Then
                    ' Reemplazar el detalle existente con el actualizado
                    currentList(existingIndex) = newDetail
                    updatedCount += 1
                Else
                    ' No se encontró por Id, agregar como nuevo
                    currentList.Add(newDetail)
                    addedCount += 1
                End If
            ElseIf isEditModeFromPopup AndAlso editIndexFromPopup >= 0 AndAlso editIndexFromPopup < currentList.Count Then
                ' Es una edición de un detalle nuevo (sin guardar), actualizar por índice
                currentList(editIndexFromPopup) = newDetail
                updatedCount += 1
            Else
                ' Es un nuevo detalle, validar duplicados por fecha
                Dim isDuplicate = currentList.Any(Function(d) d.InitialDate = newDetail.InitialDate AndAlso
                                                              d.FinalDate = newDetail.FinalDate AndAlso
                                                              d.Id = 0)
                If Not isDuplicate Then
                    currentList.Add(newDetail)
                    addedCount += 1
                End If
            End If
        Next

        ' Actualizar el datasource y refrescar el grid si hubo cambios
        If addedCount > 0 OrElse updatedCount > 0 Then
            RIPSGcHospitalStay.DataSource = Nothing
            RIPSGcHospitalStay.DataSource = currentList
            RIPSGvHospitalStay.RefreshData()

            If updatedCount > 0 Then
                Mensaje(EeventViewerImages.Informacion) = $"Registro de estancia hospitalaria actualizado"
            End If
        ElseIf newDetails.Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Los registros seleccionados ya existen en la lista"
        End If
    End Sub

    ''' <summary>
    ''' Evento de cambio de estado para la sección "Estancia Hospitalaria"
    ''' dentro del segmento "Registro RIPS".
    ''' - Cuando está desmarcada, la sección Estancia Hospitalaria se oculta.
    ''' - Cuando se marca, se muestra la sección; el botón Agregar Registro
    '''   seguirá dependiendo además de la validación del ingreso.
    ''' </summary>
    Private Sub RIPSCheHospitalStay_CheckedChanged(sender As Object, e As EventArgs) Handles RIPSCheHospitalStay.CheckedChanged
        UpdateRIPSSections()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de ingreso RIPSSleAdmission
    ''' cuando se selecciona un ingreso desde la lista desplegable.
    ''' Maneja la validación y configuración de controles cuando se selecciona un ingreso.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RIPSSleAdmission_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles RIPSSleAdmission.EditValueChanged
        ' Evitar procesar eventos durante la inicialización o limpieza para prevenir StackOverflow
        If _isInitializing OrElse _isClearingAdmission Then
            Return
        End If

        If RIPSSleAdmission.EditValue IsNot Nothing Then

            Dim admissionDTO As AdmissionInfoDTO = Nothing

            Using model As New MLiquidation()
                Dim admissionCode As String = RIPSSleAdmission.EditValue.ToString()
                Dim admissionCollection = model.GetAdmissionByNumIngres(admissionCode)

                If admissionCollection IsNot Nothing AndAlso admissionCollection.Count > 0 Then
                    admissionDTO = MapToAdmissionDTO(admissionCollection(0))
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se pudo obtener la información del ingreso seleccionado"
                    ClearAdmissionSelection()
                    Return
                End If
            End Using

            ' Procesa la selección usando el método centralizado
            ProcessAdmissionSelection(admissionDTO)
        Else
            ' Si se limpia la selección, deshabilita el botón y limpia datos
            ClearAdmissionSelection()
        End If
    End Sub

    ''' <summary>Valida el ingreso digitado manualmente.</summary>
    Private Function RIPSSleAdmission_KeyDown(code As String) As Object
        Using model As New MLiquidation()
            Dim admissionCollection = model.GetAdmissionByNumIngres(code)

            If admissionCollection IsNot Nothing AndAlso admissionCollection.Count > 0 Then
                Dim admissionDTO = MapToAdmissionDTO(admissionCollection(0))
                ProcessAdmissionSelection(admissionDTO)
            Else
                Mensaje(EeventViewerImages.Advertencia) = "El número del ingreso no existe"
                ClearAdmissionSelection()
            End If
        End Using

        Return Nothing
    End Function

    ''' <summary>
    ''' Abre el formulario de búsqueda de registros RIPS existentes.
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        _searchMode = True

        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda()
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        With FormSearchObjects
            .ListaColumnas = {
                New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 120},
                New ColumnInfo With {.Caption = "Ingreso", .FieldName = "AdmissionNumber", .ColumnWidth = 250},
                New ColumnInfo With {.Caption = "Fecha Registro RIPS", .FieldName = "OrderDate", .ColumnWidth = 150, .ColumnFormatType = DevExpress.Utils.FormatType.DateTime, .ColumnFormat = "g"},
                New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusFullName", .ColumnWidth = 140}
            }.ToList()

            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListAllRIPSSupportRecords
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Procesa el valor retornado desde el formulario de búsqueda.
    ''' Limpia el formulario y carga el registro seleccionado con controles habilitados.
    ''' </summary>
    ''' <param name="ReturnValue">Código del registro seleccionado.</param>
    ''' <param name="ReturnObject">Objeto completo del registro (no utilizado).</param>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        ' Liberar bloqueo del registro anterior si existe
        Await DeleteBlockedRecord()

        ' Limpiar formulario antes de cargar nuevo registro
        CleanControls()

        ' Cargar el registro seleccionado
        Code = ReturnValue
        If Not String.IsNullOrEmpty(Code) Then
            ' LoadControls habilitará los controles internamente
            Await LoadControls()
        Else
            ' Si no hay código, mantener controles deshabilitados
            DisableFormControls()
        End If
    End Sub

    ''' <summary>
    ''' Libera el bloqueo del registro actual si pertenece al usuario activo.
    ''' </summary>
    Private Async Function DeleteBlockedRecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function


    ''' <summary>
    ''' Actualiza el estado de los botones según si existen datos en el formulario.
    ''' </summary>
    ''' <param name="existeDatos">Indica si existen datos cargados en el formulario.</param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        If existeDatos Then
            BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        Else
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Maneja las acciones del grid de estancias hospitalarias (Editar y Eliminar).
    ''' </summary>
    ''' <param name="sender">Objeto que origina el evento.</param>
    ''' <param name="e">Argumentos del evento.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        ' Validar que haya un registro RIPS cargado
        If _RIPSSupportRecord Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay un registro RIPS cargado"
            Return
        End If

        ' Validar estado del registro RIPS (solo para registros guardados)
        ' Los registros nuevos (Id = 0) pueden tener detalles modificados/eliminados
        If _RIPSSupportRecord.Id > 0 AndAlso _RIPSSupportRecord.Status <> RIPS_STATUS_REGISTERED Then
            If _RIPSSupportRecord.Status = RIPS_STATUS_CONFIRMED Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque está confirmado"
            ElseIf _RIPSSupportRecord.Status = RIPS_STATUS_CANCELLED Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque está anulado"
            End If
            Return
        End If

        ' Obtener el detalle seleccionado
        Dim selectedDetail = TryCast(RIPSGvHospitalStay.GetFocusedRow(), RIPSSupportRecordDetail)
        If selectedDetail Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un registro de estancia hospitalaria"
            Return
        End If

        ' Obtener la lista actual de detalles
        Dim detailsList As List(Of RIPSSupportRecordDetail)
        If RIPSGcHospitalStay.DataSource IsNot Nothing Then
            detailsList = DirectCast(RIPSGcHospitalStay.DataSource, List(Of RIPSSupportRecordDetail))
        Else
            detailsList = New List(Of RIPSSupportRecordDetail)()
        End If

        ' Obtener el índice del detalle seleccionado
        Dim detailIndex = detailsList.IndexOf(selectedDetail)
        If detailIndex < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo encontrar el registro seleccionado"
            Return
        End If

        ' Procesar según la acción seleccionada
        Select Case sender.Tag?.ToString()
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                EditHospitalStayDetail(selectedDetail, detailIndex)

            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                RemoveHospitalStayDetail(selectedDetail, detailIndex, detailsList)

            Case Else
                Mensaje(EeventViewerImages.Advertencia) = "Acción no reconocida"
        End Select
    End Sub

    ''' <summary>
    ''' Edita un detalle de estancia hospitalaria abriendo el popup en modo edición.
    ''' </summary>
    ''' <param name="detail">Detalle a editar.</param>
    ''' <param name="index">Índice del detalle en la lista.</param>
    Private Sub EditHospitalStayDetail(detail As RIPSSupportRecordDetail, index As Integer)
        ' Validar que haya un ingreso seleccionado
        If RIPSSleAdmission.EditValue Is Nothing OrElse _selectedAdmissionInfo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un ingreso válido"
            Return
        End If

        Try
            ' Crear o reutilizar el popup de detalles
            If _popupRIPSDetails Is Nothing OrElse _popupRIPSDetails.IsDisposed Then
                _popupRIPSDetails = New FrmPopUpRIPSsupportrecordDetails()
                ' Suscribirse al evento para actualizar el grid en tiempo real
                AddHandler _popupRIPSDetails.DetailAdded, AddressOf OnPopupDetailAdded
            End If

            ' Pasar datos del ingreso al popup
            _popupRIPSDetails.AdmissionCode = _selectedAdmissionInfo.AdmissionCode
            _popupRIPSDetails.AdmissionDate = _selectedAdmissionInfo.AdmissionDate
            _popupRIPSDetails.PatientCode = _selectedAdmissionInfo.PatientCode

            ' Configurar modo edición
            _popupRIPSDetails.SetDetailToEdit(detail, index)

            ' Mostrar popup y procesar resultado
            If _popupRIPSDetails.ShowDialog(Me) = DialogResult.OK Then
                ProcessPopupResult()
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al editar el registro de estancia: {ex.Message}"
        End Try
    End Sub

    ''' <summary>
    ''' Elimina un detalle de estancia hospitalaria de la lista.
    ''' </summary>
    ''' <param name="detail">Detalle a eliminar.</param>
    ''' <param name="index">Índice del detalle en la lista.</param>
    ''' <param name="detailsList">Lista de detalles actual.</param>
    Private Sub RemoveHospitalStayDetail(detail As RIPSSupportRecordDetail, index As Integer, detailsList As List(Of RIPSSupportRecordDetail))
        ' Confirmar eliminación
        If MessageIndigo.Show("¿Está seguro que desea eliminar este registro de estancia hospitalaria?",
                               MessageType.Question,
                               Me.Text,
                               Botones.SiNo,
                               "") <> DialogResult.Yes Then
            Return
        End If

        Try
            ' Si el detalle tiene Id (ya guardado en BD), marcarlo como eliminado
            If detail.Id > 0 Then
                ' Preservar ChangeTracker si existe
                If detail.ChangeTracker Is Nothing Then
                    detail.ChangeTracker = New ObjectChangeTracker()
                End If
                detail.ChangeTracker.State = ObjectState.Deleted
            End If

            ' Remover de la lista
            detailsList.RemoveAt(index)

            ' Actualizar el DataSource del grid
            RIPSGcHospitalStay.DataSource = Nothing
            RIPSGcHospitalStay.DataSource = detailsList
            RIPSGvHospitalStay.RefreshData()

            Mensaje(EeventViewerImages.Informacion) = "Registro de estancia hospitalaria eliminado"

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al eliminar el registro: {ex.Message}"
        End Try
    End Sub

    ''' <summary>
    ''' Maneja la tecla Enter en el control de código.
    ''' - Secuencia manual: busca o crea registro con el código digitado.
    ''' - Secuencia automática: crea nuevo o busca según si hay código.
    ''' </summary>
    Private Async Sub RIPSBteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles RIPSBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If

            If Me._sequence.IsManual Then
                ' Secuencia manual: el usuario debe digitar el código
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    ' Código digitado: cargar registro o crear nuevo
                    ' LoadControls habilitará los controles internamente
                    Await LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica está configurada como manual, por favor digite un código"
                    RIPSBteCode.Focus()
                End If
            ElseIf String.IsNullOrEmpty(Code) Then
                ' Secuencia automática sin código: crear nuevo registro
                ' NewRIPSSupportRecord habilitará los controles internamente
                Await Me.NewRIPSSupportRecord()
            Else
                ' Secuencia automática con código: buscar registro existente
                ' LoadControls habilitará los controles internamente
                Await LoadControls()
            End If
        End If
    End Sub
#End Region

#Region "DTO Classes"

    ''' <summary>
    ''' DTO para mapear información de admisión desde ViewAdmissionsToLiquidation
    ''' </summary>
    Private Class AdmissionInfoDTO
        ' Información básica de admisión
        Public Property AdmissionCode As String
        Public Property AdmissionCodeWithOutTrim As String
        Public Property AdmissionDate As DateTime
        Public Property Status As String
        Public Property StatusName As String
        Public Property AdmissionType As Integer
        Public Property AdmissionTypeName As String
        Public Property AdmissionCaregroupId As Integer
        Public Property AdmissionReason As Integer
        Public Property AdmissionRiskType As Integer
        Public Property PlaceEntry As Integer
        Public Property BedStay As String
        Public Property LiquidationType As Integer
        Public Property AuthorizationNumber As String

        ' Información de entidad y administradora
        Public Property EntityCode As String
        Public Property EntityName As String
        Public Property HealthAdministratorId As Integer
        Public Property CareGroupCodeName As String
        Public Property AdmissionCareGroupCodeName As String

        ' Información de paciente
        Public Property PatientCode As String
        Public Property PatientName As String
        Public Property PatientType As Integer
        Public Property PatientAfiliation As Integer
        Public Property PatientCareGroupId As Integer

        ' Información de centro de atención y unidad funcional
        Public Property AdmissionCentAtencCodeName As String
        Public Property AdmissionUniFuncCodeName As String
        Public Property UniFuncEgre As String

        ' Información adicional
        Public Property RevenueControlId As Integer?
        Public Property ThirdPartyPatientId As Integer?
        Public Property IncomeLockType As Integer?
    End Class

#End Region

End Class


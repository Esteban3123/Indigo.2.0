#Region "Imports"

Imports System.Drawing
Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmPharmaceuticalDispensingTransfer
    Implements IPharmaceuticalDispensingTransfer, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Const MODULE_NAME = "Inventory"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    Dim _presenter As PPharmaceuticalDispensingTransfer

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Dim _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Dim _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    Dim _settingsInventory As SettingInventory

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Dim _blockRecord As BlockRecordInventory

    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    Dim _admission As Object

    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    Dim _admissionDestination As Object

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    Dim _pharmaceuticalDispensingTransfer As PharmaceuticalDispensingTransfer

    ''' <summary>
    ''' listado del detalles
    ''' </summary>
    Dim _listPharmaceuticalDispensingTransferDetail As List(Of PharmaceuticalDispensingTransferDetail)

    ''' <summary>
    ''' listado del detalles a eliminar
    ''' </summary>
    Dim _listDeletePharmaceuticalDispensingTransferDetail As List(Of PharmaceuticalDispensingTransferDetail)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IPharmaceuticalDispensingTransfer.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPharmaceuticalDispensingTransfer.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPharmaceuticalDispensingTransfer.ActionsOnControls
        Set(value As Boolean)
            INDLcPharmaceuticalDispensingTransfer.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = value
            INDSleWarehouse.Enabled = value
            INDSleAdmissionNumber.Enabled = value
            INDSleAdmissionNumber.Search.Enabled = value
            INDSleAdmissionNumberDestination.Enabled = value
            INDSleAdmissionNumberDestination.Search.Enabled = value
            INDMeDetail.Enabled = value

            INDBtnAdd.Enabled = False
            INDGcProducts.Enabled = value
            INDGcProducts.Enabled = value

            INDLcPharmaceuticalDispensingTransfer.EndUpdate()
            If value Then
                INDDteDate.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As InventorySequence Implements IPharmaceuticalDispensingTransfer.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' codigo del documento
    ''' </summary>
    Public Property Code As String Implements IPharmaceuticalDispensingTransfer.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' fecha del documento
    ''' </summary>
    Public Property DocumentDate As Date Implements IPharmaceuticalDispensingTransfer.DocumentDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As Date)
            INDDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    Public Property WarehouseId As Integer Implements IPharmaceuticalDispensingTransfer.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' detalle
    ''' </summary>
    Public Property Observation As String Implements IPharmaceuticalDispensingTransfer.Observation
        Get
            Return INDMeDetail.EditValue
        End Get
        Set(value As String)
            INDMeDetail.EditValue = value
        End Set
    End Property

    Public Property Status As Byte Implements IPharmaceuticalDispensingTransfer.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Datasource de Almacenes
    ''' </summary>
    Public Property WareHouseDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IPharmaceuticalDispensingTransfer.WareHouseDatasource
        Get
            Return CType(INDSleWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud Base"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Ingreso", .FieldName = "AdmissionNumber", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Ingreso Destino", .FieldName = "AdmissionNumberDestination", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPharmaceuticalDispensingTransfer
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewPharmaceuticalDispensingTransfer()
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Me._pharmaceuticalDispensingTransfer IsNot Nothing AndAlso Me._pharmaceuticalDispensingTransfer.Status < 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If

            If _admission Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un Ingreso Origen"
                Exit Sub
            End If

            If _admissionDestination Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un Ingreso Destino"
                Exit Sub
            End If

            If _admission.AdmissionCode = _admissionDestination.AdmissionCode Then
                Mensaje(EeventViewerImages.Advertencia) = "El ingreso origen debe ser diferente del ingreso destino"
                Exit Sub
            End If

            If _admission.PatientCode <> _admissionDestination.PatientCode Then
                Mensaje(EeventViewerImages.Advertencia) = "El paciente del ingreso origen debe ser el mismo del ingreso destino"
                Exit Sub
            End If

            If Me._listPharmaceuticalDispensingTransferDetail Is Nothing OrElse _listPharmaceuticalDispensingTransferDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay detalles a trasladar."
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MPharmaceuticalDispensingTransfer(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SavePharmaceuticalDispensingTransfer(Me._pharmaceuticalDispensingTransfer)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    _pharmaceuticalDispensingTransfer = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _pharmaceuticalDispensingTransfer.Id, 0, _pharmaceuticalDispensingTransfer.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _pharmaceuticalDispensingTransfer.Id, 0, _pharmaceuticalDispensingTransfer.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _pharmaceuticalDispensingTransfer.Id, 0, _pharmaceuticalDispensingTransfer.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _pharmaceuticalDispensingTransfer.Id, 0, _pharmaceuticalDispensingTransfer.Id)
                    End Select
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    If _pharmaceuticalDispensingTransfer.Id > 0 Then
                        _pharmaceuticalDispensingTransfer = Await model.GetPharmaceuticalDispensingTransferByCode(_pharmaceuticalDispensingTransfer.Code)
                    Else
                        Dim prefix = _pharmaceuticalDispensingTransfer.Prefix
                        _pharmaceuticalDispensingTransfer = New PharmaceuticalDispensingTransfer
                        _pharmaceuticalDispensingTransfer.Prefix = prefix
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)

        IndigoGridView1.SetListAcction(INDGvProducts, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "colActions" Then
                col.OptionsColumn.FixedWidth = True
            End If
        Next
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
                Deshacer()
                Exit Function
            End If

            Me.ValidateDate()
        End Using
    End Function

    ''' <summary>
    ''' Consulta la fecha de los parametros y establece la fecha minima y maxima de la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(_settingsInventory.Year.ToString + "/" + _settingsInventory.Month.ToString + "/01")
        INDDteDate.Properties.MinValue = dateMin
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del popup de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdmission()
        INDTxtAdmissionCode.Text = String.Empty
        INDTxtStay.Text = String.Empty
        INDTxtPatient.Text = String.Empty
        INDTxtAdmissionDate.Text = String.Empty
        INDTxtAdmissionType.Text = String.Empty
        INDTxtAdmissionPlace.Text = String.Empty
        INDTxtLiquidationType.Text = String.Empty
        INDTxtEntity.Text = String.Empty
        INDTxtBenefitsPlan.Text = String.Empty
        INDTxtAuthorizationNumber.Text = String.Empty
        INDTxtResponsibleName.Text = String.Empty
        INDTxtResponsiblePhone.Text = String.Empty
    End Sub

    ''' <summary>
    ''' limpiar controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDLcPharmaceuticalDispensingTransfer.BeginUpdate()
        Await DeleteBlockedRecord()

        INDBteCode.EditValue = Nothing
        INDDteDate.EditValue = GetDateServer()
        INDSleWarehouse.EditValue = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty
        INDSleAdmissionNumber.SetNullText(String.Empty)
        INDSleAdmissionNumber.IsReadOnly = False
        INDSleAdmissionNumberDestination.SetNullText(String.Empty)
        INDSleAdmissionNumberDestination.IsReadOnly = False
        INDMeDetail.EditValue = Nothing
        Status = 1

        CleanControlsAdmission()
        INDGcProducts.DataSource = Nothing
        INDGcProducts.DataSource = Nothing

        _doc = Nothing
        _admission = Nothing
        _admissionDestination = Nothing
        _pharmaceuticalDispensingTransfer = Nothing
        _listPharmaceuticalDispensingTransferDetail = Nothing

        Me.ValidateDate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        INDBtnAdd.Enabled = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        INDLcPharmaceuticalDispensingTransfer.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me._pharmaceuticalDispensingTransfer.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _blockRecord = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me._pharmaceuticalDispensingTransfer.Id}
                Dim operation = Await model.SaveBlockRecord(_blockRecord)
                _blockRecord = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                _blockRecord = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Await model.DeleteBlockRecord(_blockRecord)
            End Using
            _blockRecord = Nothing
        End If
    End Function

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._pharmaceuticalDispensingTransfer.Code, Me._pharmaceuticalDispensingTransfer.DocumentDate, Me._pharmaceuticalDispensingTransfer.AdmissionNumber.Trim(), Me._pharmaceuticalDispensingTransfer.AdmissionNumberDestination.Trim(), Me._pharmaceuticalDispensingTransfer.Observation),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._pharmaceuticalDispensingTransfer.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._pharmaceuticalDispensingTransfer.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._pharmaceuticalDispensingTransfer.Code, Me._pharmaceuticalDispensingTransfer.DocumentDate, Me._pharmaceuticalDispensingTransfer.AdmissionNumber.Trim(), Me._pharmaceuticalDispensingTransfer.AdmissionNumberDestination.Trim(), Me._pharmaceuticalDispensingTransfer.Observation)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._pharmaceuticalDispensingTransfer.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        If Me._sequence IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
            Return Me._sequence.InventorySequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewPharmaceuticalDispensingTransfer() As Task
        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
            Exit Function
        End If

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        BarraBotones.StatusRecord = "1"
        BarraBotones.StatusRecordVisible = True
        Me._pharmaceuticalDispensingTransfer = New PharmaceuticalDispensingTransfer With {.Status = 1}
    End Function

    ''' <summary>
    ''' Consulta y carga la admisión
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RunSetAdmission() As Task
        If _pharmaceuticalDispensingTransfer Is Nothing Then
            Return Task.FromResult(Of Object)(0)
        End If

        Return Task.Factory.StartNew(Sub()
                                         Using modelServiceOrder As New Billing.MVP.MServiceOrder(Me.MyTag)
                                             _admission = modelServiceOrder.GetAdmissionByServiceOrder(_pharmaceuticalDispensingTransfer.AdmissionNumber.Trim())
                                             If _admission IsNot Nothing Then
                                                 If INDSleAdmissionNumber.InvokeRequired Then
                                                     INDSleAdmissionNumber.BeginInvoke(Sub()
                                                                                           Me.INDSleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), _admission.AdmissionCode.ToString().Trim(), _admission.PatientCode.ToString().Trim(), _admission.PatientName.ToString().Trim()))
                                                                                       End Sub)
                                                 Else
                                                     Me.INDSleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), _admission.AdmissionCode.ToString().Trim(), _admission.PatientCode.ToString().Trim(), _admission.PatientName.ToString().Trim()))
                                                 End If
                                             End If

                                             _admissionDestination = modelServiceOrder.GetAdmissionByServiceOrder(_pharmaceuticalDispensingTransfer.AdmissionNumberDestination.Trim())
                                             If _admissionDestination IsNot Nothing Then
                                                 If INDSleAdmissionNumberDestination.InvokeRequired Then
                                                     INDSleAdmissionNumber.BeginInvoke(Sub()
                                                                                           Me.INDSleAdmissionNumberDestination.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), _admissionDestination.AdmissionCode.ToString().Trim(), _admissionDestination.PatientCode.ToString().Trim(), _admissionDestination.PatientName.ToString().Trim()))
                                                                                       End Sub)
                                                 Else
                                                     Me.INDSleAdmissionNumberDestination.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), _admissionDestination.AdmissionCode.ToString().Trim(), _admissionDestination.PatientCode.ToString().Trim(), _admissionDestination.PatientName.ToString().Trim()))
                                                 End If
                                             End If
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' metodo para establecer los datos del ingreso
    ''' </summary>
    ''' <param name="admission"></param>
    ''' <remarks></remarks>
    Private Sub SetAdmission(admission As Object)
        Me.CleanControlsAdmission()
        If admission Is Nothing Then
            Exit Sub
        End If

        With admission
            INDTxtAdmissionCode.Text = .AdmissionCode.ToString().Trim()
            If .AdmissionDate IsNot Nothing Then
                INDTxtAdmissionDate.Text = CDate(.AdmissionDate).ToLongDateString()
            End If
            If .AdmissionType IsNot Nothing Then
                INDTxtAdmissionType.Text = .AdmissionType.ToString().Trim()
            End If
            If .AuthorizationNumber IsNot Nothing Then
                INDTxtAuthorizationNumber.Text = .AuthorizationNumber.ToString().Trim()
            End If
            If .AdmissionType.ToString().Trim() <> ResourceManager.GetString("OutpatientRevenue", MODULE_NAME) Then
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                If .BedStay IsNot Nothing Then
                    INDTxtStay.Text = .BedStay.ToString().Trim()
                End If
            Else
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            If .BenefitPlan IsNot Nothing Then
                INDTxtBenefitsPlan.Text = .BenefitPlan.ToString().Trim()
            End If
            If .EntityCode IsNot Nothing And .EntityName IsNot Nothing Then
                INDTxtEntity.Text = .EntityCode.ToString().Trim() + " - " + .EntityName.ToString().Trim()
            End If
            If .LiquidationType IsNot Nothing Then
                INDTxtLiquidationType.Text = .LiquidationType.ToString().Trim()
            End If
            If .PatientCode IsNot Nothing And .PatientName IsNot Nothing Then
                INDTxtPatient.Text = .PatientCode.ToString().Trim() + " - " + .PatientName.ToString().Trim()
            End If
            If .PlaceEntry IsNot Nothing Then
                INDTxtAdmissionPlace.Text = .PlaceEntry.ToString().Trim()
            End If
            If .ResponsibleName IsNot Nothing Then
                INDTxtResponsibleName.Text = .ResponsibleName.ToString().Trim()
            End If
            If .ResponsiblePhone IsNot Nothing Then
                INDTxtResponsiblePhone.Text = .ResponsiblePhone.ToString().Trim()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para cargar datos en los controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPharmaceuticalDispensingTransfer(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDLcPharmaceuticalDispensingTransfer.BeginUpdate()
                    _pharmaceuticalDispensingTransfer = Await Model.GetPharmaceuticalDispensingTransferByCode(INDBteCode.Text.Trim)
                    If _pharmaceuticalDispensingTransfer IsNot Nothing AndAlso _pharmaceuticalDispensingTransfer.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_pharmaceuticalDispensingTransfer.Id))
                            With _pharmaceuticalDispensingTransfer
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                If .Status <> 1 Then
                                    INDDteDate.Properties.MinValue = .DocumentDate
                                End If

                                Code = .Code
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                DocumentDate = .DocumentDate
                                WarehouseId = .WarehouseId
                                INDSleWarehouse.Properties.NullText = .WarehouseCodeName
                                INDSleAdmissionNumber.Search.EditValue = .AdmissionNumber
                                INDSleAdmissionNumberDestination.Search.EditValue = .AdmissionNumberDestination
                                Observation = .Observation
                                Me.Status = .Status

                                Await RunSetAdmission()

                                _listPharmaceuticalDispensingTransferDetail = .PharmaceuticalDispensingTransferDetail.ToList()
                                INDGcProducts.DataSource = Nothing
                                INDGcProducts.DataSource = _listPharmaceuticalDispensingTransferDetail
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._pharmaceuticalDispensingTransfer.Code)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _pharmaceuticalDispensingTransfer.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If


                            Select Case _pharmaceuticalDispensingTransfer.Status
                                Case 1
                                    INDSleWarehouse.Properties.ReadOnly = True
                                    INDSleAdmissionNumber.IsReadOnly = True
                                    INDBtnAdd.Enabled = True

                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                Case Else
                                    ReadOnlyControls(True)

                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                            End Select

                            INDDteDate.Focus()
                            Me.BarraBotones.SetDocuments(_pharmaceuticalDispensingTransfer.Id, Me.Tag.ToString(), Nothing, GetType(TransferOrder).Name)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _pharmaceuticalDispensingTransfer.Id, 0, _pharmaceuticalDispensingTransfer.Id)

                            ActionsOnControls = True
                            AsyncLoader(False)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPharmaceuticalDispensingTransfer()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            Deshacer()
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcPharmaceuticalDispensingTransfer.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' metodo para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With Me._pharmaceuticalDispensingTransfer
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .DocumentDate = DocumentDate
            .WarehouseId = WarehouseId
            .WarehouseCodeName = INDSleWarehouse.Text
            .AdmissionNumber = Me._admission.AdmissionCode.ToString().Trim()
            .AdmissionNumberDestination = Me._admissionDestination.AdmissionCode.ToString().Trim()
            .Observation = Observation

            For Each item In _listPharmaceuticalDispensingTransferDetail
                .PharmaceuticalDispensingTransferDetail.Add(item)
            Next
            If _listDeletePharmaceuticalDispensingTransferDetail IsNot Nothing AndAlso _listDeletePharmaceuticalDispensingTransferDetail.Count > 0 Then
                For Each item In _listDeletePharmaceuticalDispensingTransferDetail
                    .PharmaceuticalDispensingTransferDetail.Add(item)
                Next
            End If

            If _pharmaceuticalDispensingTransfer.Id > 0 Then
                _pharmaceuticalDispensingTransfer.MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddPharmaceuticalDispensingTransferDetail(sender As Object, e As AddPharmaceuticalDispensingTransferDetailEventArgs)
        Try
            If _listPharmaceuticalDispensingTransferDetail Is Nothing Then
                _listPharmaceuticalDispensingTransferDetail = New List(Of PharmaceuticalDispensingTransferDetail)
            End If

            Dim errors As New StringBuilder
            For Each pharmaceuticalDispensingTransferDetail In e.ListPharmaceuticalDispensingTransferDetail
                If _listPharmaceuticalDispensingTransferDetail.Any(Function(d) d.PharmaceuticalDispensingDetailBatchSerialId = pharmaceuticalDispensingTransferDetail.PharmaceuticalDispensingDetailBatchSerialId) Then
                    errors.AppendLine(String.Format("El producto '{0}' de la dispensación '{1}' ya se encuentra agregado a la lista.", pharmaceuticalDispensingTransferDetail.ProductCodeName, pharmaceuticalDispensingTransferDetail.PharmaceuticalDispensingCode))
                    Continue For
                End If

                _listPharmaceuticalDispensingTransferDetail.Add(pharmaceuticalDispensingTransferDetail)
            Next

            If errors.Length > 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If

            If _listPharmaceuticalDispensingTransferDetail.Any Then
                INDSleWarehouse.Properties.ReadOnly = True
                INDSleAdmissionNumber.IsReadOnly = True
            End If

            INDGcProducts.DataSource = Nothing
            INDGcProducts.DataSource = _listPharmaceuticalDispensingTransferDetail
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim _itemPharmaceuticalDispensingTransferDetail = DirectCast(INDGvProducts.GetFocusedRow(), PharmaceuticalDispensingTransferDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _itemPharmaceuticalDispensingTransferDetail.Id > 0 Then
                If _listDeletePharmaceuticalDispensingTransferDetail Is Nothing Then
                    _listDeletePharmaceuticalDispensingTransferDetail = New List(Of PharmaceuticalDispensingTransferDetail)
                End If
                _itemPharmaceuticalDispensingTransferDetail.MarkAsDeleted()
                _listDeletePharmaceuticalDispensingTransferDetail.Add(_itemPharmaceuticalDispensingTransferDetail)
            End If

            _listPharmaceuticalDispensingTransferDetail.Remove(_itemPharmaceuticalDispensingTransferDetail)
            INDGcProducts.DataSource = Nothing
            INDGcProducts.DataSource = _listPharmaceuticalDispensingTransferDetail

            If _listPharmaceuticalDispensingTransferDetail.Count = 0 Then
                INDSleWarehouse.Properties.ReadOnly = False
                INDSleAdmissionNumber.IsReadOnly = False
            End If
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Async Sub FrmPharmaceuticalDispensingTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcPharmaceuticalDispensingTransfer, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PPharmaceuticalDispensingTransfer(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await Me.LoadParameters()

        AddActionsColumns()
        Deshacer()
        LoadStatus()

        AddHandler Me.INDSleAdmissionNumber.Search.KeyDown, AddressOf INDSleAdmissionNumber_KeyDown
        AddHandler Me.INDSleAdmissionNumber.Search.QueryPopUp, AddressOf INDSleAdmissionNumber_QueryPopUp
        AddHandler Me.INDSleAdmissionNumber.Search.ButtonClick, AddressOf INDSleAdmissionNumber_ButtonClick

        AddHandler Me.INDSleAdmissionNumberDestination.Search.KeyDown, AddressOf INDSleAdmissionNumberDestination_KeyDown
        AddHandler Me.INDSleAdmissionNumberDestination.Search.QueryPopUp, AddressOf INDSleAdmissionNumberDestination_QueryPopUp
        AddHandler Me.INDSleAdmissionNumberDestination.Search.ButtonClick, AddressOf INDSleAdmissionNumberDestination_ButtonClick
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _sequence = Nothing
        _settingsInventory = Nothing
        _blockRecord = Nothing
        _admission = Nothing
        _admissionDestination = Nothing
        _pharmaceuticalDispensingTransfer = Nothing
        _listPharmaceuticalDispensingTransferDetail = Nothing
        _listDeletePharmaceuticalDispensingTransferDetail = Nothing
        _varImp = Nothing
    End Sub


#End Region

#Region "Activated"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Async Sub FrmPharmaceuticalDispensingTransfer_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewPharmaceuticalDispensingTransfer()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub INDSleAdmissionNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDSleAdmissionNumberDestination.Focus()
        End If
    End Sub

    Private Sub INDSleAdmissionNumberDestination_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDMeDetail.Focus()
        End If
    End Sub

    'Private Sub INDMeDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDetail.KeyDown
    '    If e.KeyCode = System.Windows.Forms.Keys.Enter Then
    '        INDPceProducts.Focus()
    '        INDPceProducts.ShowPopup()
    '        INDSleProduct.Focus()
    '    End If
    'End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If WareHouseDatasource Is Nothing Then
            _presenter.LoadWareHouse()
        End If
    End Sub

    Private Sub INDSleAdmissionNumber_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs)
        If INDSleAdmissionNumber.IsReadOnly = True Then
            Exit Sub
        End If
        If INDSleAdmissionNumber.Datasource Is Nothing Then
            Using model As New Billing.MVP.MServiceOrder(MyTag.ToString())
                INDSleAdmissionNumber.Datasource = model.GetViewAdmissionServiceOrder
            End Using
        End If
    End Sub

    Private Sub INDSleAdmissionNumberDestination_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleAdmissionNumberDestination.IsReadOnly = True OrElse _admission Is Nothing Then
            Exit Sub
        End If
        If INDSleAdmissionNumberDestination.Datasource Is Nothing Then
            Using model As New Billing.MVP.MServiceOrder(MyTag.ToString())
                INDSleAdmissionNumberDestination.Datasource = model.GetViewAdmissionServiceOrderByPatient(_admission.PatientCode)
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmStores With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.LoadWareHouse()
        End If
    End Sub

    Private Sub INDSleAdmissionNumber_ButtonClick(sender As Object, e As EventArgs)
        If _admission Is Nothing Then
            Exit Sub
        End If
        SetAdmission(_admission)
        INDSleAdmissionNumber.PopupContainerControl = INDPccMoreInfoAdmission
    End Sub

    Private Sub INDSleAdmissionNumberDestination_ButtonClick(sender As Object, e As EventArgs)
        If _admissionDestination Is Nothing Then
            Exit Sub
        End If
        SetAdmission(_admissionDestination)
        INDSleAdmissionNumberDestination.PopupContainerControl = INDPccMoreInfoAdmission
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDRptSeDevolutionQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If

        Dim PharmaceuticalDispensingDetailBatchSerial = DirectCast(INDGvProducts.GetFocusedRow, PharmaceuticalDispensingDetailBatchSerial)
        If e.NewValue > PharmaceuticalDispensingDetailBatchSerial.OutstandingQuantity Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser mayor a la cantidad del producto"
            e.Cancel = True
            Exit Sub
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        INDBtnAdd.Enabled = False
        Me._pharmaceuticalDispensingTransfer.Prefix = Nothing

        If Me.INDSleWarehouse.EditValue IsNot Nothing Then
            If _admission IsNot Nothing Then
                INDBtnAdd.Enabled = True
            End If

            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = _presenter.GetWareHouseById(Me.INDSleWarehouse.EditValue)
                If store IsNot Nothing Then
                    Me._idCurrentSequence = Me.GetIdSequenceByPrefix(store.Prefix)
                    Me._pharmaceuticalDispensingTransfer.Prefix = store.Prefix
                End If
            End If
        End If
    End Sub

#End Region

#Region "NewSelectedValue"

    Private Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDSleAdmissionNumber.NewSelectedValue
        _admission = e.AdmissionObject
        Me.INDSleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), _admission.AdmissionCode.ToString().Trim(), _admission.PatientCode.ToString().Trim(), _admission.PatientName.ToString().Trim()))
        INDBtnAdd.Enabled = If(INDSleWarehouse.EditValue IsNot Nothing, True, False)

        INDSleAdmissionNumberDestination.SetEditValue = Nothing
        INDSleAdmissionNumberDestination.SetNullText(String.Empty)
        INDSleAdmissionNumberDestination.Datasource = Nothing
    End Sub

    Private Sub INDSleAdmissionNumberDestination_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDSleAdmissionNumberDestination.NewSelectedValue
        _admissionDestination = e.AdmissionObject
        Me.INDSleAdmissionNumberDestination.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), _admissionDestination.AdmissionCode.ToString().Trim(), _admissionDestination.PatientCode.ToString().Trim(), _admissionDestination.PatientName.ToString().Trim()))
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If _pharmaceuticalDispensingTransfer Is Nothing OrElse _pharmaceuticalDispensingTransfer.Status <> 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el traslado de dispensación por ingreso"
            Exit Sub
        End If

        Using formulario As New PopupPharmaceuticalDispensingTransferDetail
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddPharmaceuticalDispensingTransferDetail, AddressOf ReturnAddPharmaceuticalDispensingTransferDetail
            formulario.Size = New Drawing.Size(800, 780)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.WarehouseId = WarehouseId
            formulario.AdmissionNumber = _admission.AdmissionCode.ToString().Trim()
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Click_ButtonAction"

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        DeleteDetail()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._pharmaceuticalDispensingTransfer IsNot Nothing AndAlso Me._pharmaceuticalDispensingTransfer.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#End Region

#Region "Buttons Bar"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _pharmaceuticalDispensingTransfer.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _pharmaceuticalDispensingTransfer.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _pharmaceuticalDispensingTransfer.Status = 3
            _varImp = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _pharmaceuticalDispensingTransfer.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _pharmaceuticalDispensingTransfer.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _pharmaceuticalDispensingTransfer.Id, 0, _pharmaceuticalDispensingTransfer.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class
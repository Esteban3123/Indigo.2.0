#Region "Imports"

Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Maintenance.MVP

#End Region

Public Class FrmWorkOrderModal
    Implements ICrudBase, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PWorkOrder

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' secuencia
    ''' </summary>
    Dim _sequence As Domain.Entities.MaintenanceSequence

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _blockRecord As BlockRecordMaintenance

    ''' <summary>
    ''' entidad de la orden de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    Private _workOrder As WorkOrder

    ''' <summary>
    ''' Listado de las actividades realizadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listWorkOrderActivities As List(Of WorkOrderActivities)

    ''' <summary>
    ''' Listado de las actividades eliminadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteWorkOrderActivities As List(Of WorkOrderActivities)

    ''' <summary>
    ''' Listado de los consumibles usados
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listWorkOrderConsumables As List(Of WorkOrderConsumables)

    ''' <summary>
    ''' Listado de los consumibles eliminadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteWorkOrderConsumables As List(Of WorkOrderConsumables)

    ''' <summary>
    ''' Listado de las herramientas usadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listWorkOrderTools As List(Of WorkOrderTools)

    ''' <summary>
    ''' Listado de las herramientas eliminadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteWorkOrderTools As List(Of WorkOrderTools)

    ''' <summary>
    ''' Listado de los insumos usados
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listWorkOrderSupplies As List(Of WorkOrderSupplies)

    ''' <summary>
    ''' Listado de los insumos eliminadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteWorkOrderSupplies As List(Of WorkOrderSupplies)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' 1 = Guardar
    ''' 2 = Actualizar
    ''' 3 = Anular
    ''' 4 = Guardar y Confirmar
    ''' 5 = Actualizar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

    ''' <summary>
    ''' Variable que permite determina si se esta cargando información del protocolo
    ''' </summary>
    Dim _loadingProtocol As Boolean

#End Region

#Region "Event"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()

            INDTxtCode.Enabled = Not value
            INDDeRequestDate.Enabled = value
            INDDeProgramDate.Enabled = value
            INDSleBranchOffice.Enabled = value
            INDSleProtocol.Enabled = value
            INDSlePhysicalAsset.Enabled = value
            INDSleMaintenanceResponsible.Enabled = value
            INDMeDescription.Enabled = value

            INDGcActivities.Enabled = value
            INDGcConsumables.Enabled = value
            INDGcTools.Enabled = value
            INDGcSupplies.Enabled = value

            INDLcRoot.EndUpdate()
            If value Then
                INDDeRequestDate.Focus()
            Else
                INDTxtCode.Focus()
            End If
        End Set
    End Property

    Public WriteOnly Property WorkOrder As WorkOrder
        Set(value As WorkOrder)
            Me._workOrder = value
        End Set
    End Property

    Public Property Sequense As MaintenanceSequence
        Get
            Return Me._sequence
        End Get
        Set(value As MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property Code As String
        Get
            If (INDTxtCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtCode.Text
            End If
        End Get
        Set(value As String)
            INDTxtCode.Text = value
        End Set
    End Property

    Public Property Status As Byte
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
    ''' Establece las unidades de tiempo
    ''' </summary>
    ''' <remarks></remarks>
    Private _ListUnitTime As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListUnitTime As List(Of Tuple(Of Byte, String))
        Get
            If _ListUnitTime Is Nothing Then
                _ListUnitTime = New List(Of Tuple(Of Byte, String))
                _ListUnitTime.Add(New Tuple(Of Byte, String)(1, "Minutos"))
                _ListUnitTime.Add(New Tuple(Of Byte, String)(2, "Horas"))
                _ListUnitTime.Add(New Tuple(Of Byte, String)(3, "Días"))
            End If
            Return _ListUnitTime
        End Get
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Tipo De orden", .FieldName = "OrderTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Alm. Origen", .FieldName = "SourceWarehouseId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Despachado", .FieldName = "DispatchToDescription", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Destino", .FieldName = "TargetDescription", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListWorkOrder
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDTxtCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDTxtCode.Enabled = False
        End If
    End Sub

    Public Async Sub Deshacer() Implements ICrudBase.Deshacer
        Await CleanControls()
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewWorkOrder()
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If _workOrder IsNot Nothing AndAlso _workOrder.State <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MWorkOrder(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result = Await model.SaveWorkOrderAsync(New List(Of WorkOrder) From {Me._workOrder})
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    Me._workOrder = result.ObjectEmbbeded.FirstOrDefault
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _workOrder.Id, 0, _workOrder.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _workOrder.Id, 0, _workOrder.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _workOrder.Id, 0, _workOrder.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _workOrder.Id, 0, _workOrder.Id)
                        Case 5
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _workOrder.Id, 0, _workOrder.Id)
                    End Select

                    Await CleanControls()
                    RaiseEvent ReturnModalArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDTxtCode.Enabled = False
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

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateDelivered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StateInTransit"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Private Async Function CleanControls() As Task
        INDLcRoot.BeginUpdate()
        Await DeleteBlockedRecord()

        Code = String.Empty
        INDDeRequestDate.EditValue = Nothing
        INDDeProgramDate.EditValue = Nothing
        INDSleBranchOffice.EditValue = Nothing
        INDSleBranchOffice.Properties.NullText = String.Empty
        INDSleProtocol.EditValue = Nothing
        INDSleProtocol.Properties.NullText = String.Empty
        INDSlePhysicalAsset.EditValue = Nothing
        INDSlePhysicalAsset.Properties.NullText = Nothing
        INDSleMaintenanceResponsible.EditValue = Nothing
        INDSleMaintenanceResponsible.Properties.NullText = String.Empty
        INDMeDescription.EditValue = Nothing
        Status = 1

        INDGcActivities.DataSource = Nothing
        INDGcActivities.RefreshDataSource()
        INDGcConsumables.DataSource = Nothing
        INDGcConsumables.RefreshDataSource()
        INDGcTools.DataSource = Nothing
        INDGcTools.RefreshDataSource()
        INDGcSupplies.DataSource = Nothing
        INDGcSupplies.RefreshDataSource()

        Me._doc = Nothing
        _workOrder = Nothing
        _listWorkOrderActivities = Nothing
        _listDeleteWorkOrderActivities = Nothing
        _listWorkOrderConsumables = Nothing
        _listDeleteWorkOrderConsumables = Nothing
        _listWorkOrderTools = Nothing
        _listDeleteWorkOrderTools = Nothing
        _listWorkOrderSupplies = Nothing
        _listDeleteWorkOrderSupplies = Nothing
        _loadingProtocol = False

        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        INDLcRoot.EndUpdate()

        'If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        'Else
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'End If
    End Function

    Private Async Sub GenerateBlockRecord()
        Using model As New MWorkOrder(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me._workOrder.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _blockRecord = New BlockRecordMaintenance With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me._workOrder.Id}
                Dim operation = Await model.SaveBlockRecord(_blockRecord)
                _blockRecord = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                _blockRecord = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MWorkOrder(Me.Tag.ToString())
                Await model.DeleteBlockRecord(_blockRecord)
            End Using
            _blockRecord = Nothing
        End If
    End Function

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._workOrder.Consecutive, Me._workOrder.BrachOfficeCodeName, Me._workOrder.ProtocolCodeName, Me._workOrder.MaintenanceResponsibleCodeName),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._workOrder.Consecutive & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._workOrder.Consecutive),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._workOrder.Consecutive, Me._workOrder.BrachOfficeCodeName, Me._workOrder.ProtocolCodeName, Me._workOrder.MaintenanceResponsibleCodeName)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._workOrder.Consecutive)
            Return Me._doc
        End If
    End Function

    Private Async Function NewWorkOrder() As Task
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._operativeUnitId) Then
                    Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._operativeUnitId).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    'Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.Close()
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MWorkOrder(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        BarraBotones.StatusRecord = "1"
        BarraBotones.StatusRecordVisible = True
        Me._workOrder = New Domain.Entities.WorkOrder With {.State = 1}
    End Function

    Private Sub LoadWorkOrder()
        With _workOrder
            LayoutControls.SetCustomFieldsValue(.CustomProperties)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnullateUser)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnullateDate)

            _listWorkOrderActivities = .WorkOrderActivities.ToList()
            _listWorkOrderConsumables = .WorkOrderConsumables.ToList()
            _listWorkOrderTools = .WorkOrderTools.ToList()
            _listWorkOrderSupplies = .WorkOrderSupplies.ToList()

            Code = .Consecutive
            INDDeRequestDate.EditValue = .RequestDate
            INDDeProgramDate.EditValue = .ProgramDate
            INDSleBranchOffice.EditValue = .BranchOfficeId
            INDSleBranchOffice.Properties.NullText = .BrachOfficeCodeName
            INDSleProtocol.EditValue = .ProtocolId
            INDSleProtocol.Properties.NullText = .ProtocolCodeName
            INDSlePhysicalAsset.EditValue = .PhysicalAssetId
            INDSlePhysicalAsset.Properties.NullText = .PhysicalAssetDescription
            INDSleMaintenanceResponsible.EditValue = .MaintenanceResponsibleId
            INDSleMaintenanceResponsible.Properties.NullText = .MaintenanceResponsibleCodeName
            INDMeDescription.EditValue = .Description
            Status = .State

            Me.BarraBotones.StatusRecordVisible = True
        End With
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Using model As New MWorkOrder(CStr(Me.Tag))
                AsyncLoader(True)
                INDLcRoot.BeginUpdate()
                _workOrder = Await model.GetWorkOrder(INDTxtCode.Text.Trim)
                If _workOrder IsNot Nothing AndAlso _workOrder.Id > 0 Then
                    _blockRecord = Await model.GetBlockRecord(CStr(Me.Tag), CStr(_workOrder.Id))
                    Me.LoadWorkOrder()
                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._workOrder.Consecutive)
                    If _blockRecord.Id = 0 Then
                        _blockRecord = (Await model.SaveBlockRecord(
                            New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _workOrder.Id})
                            ).ObjectEmbbeded
                    Else
                        Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                    End If

                    Select Case Me._workOrder.State
                        Case 1
                            If Me._workOrder.Id > 0 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnularWithoutUndoAndFind)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
                            End If
                        Case Else
                            ReadOnlyControls(True)

                            Me.BarraBotones.PrepareToolbar(eAction.None)
                    End Select

                    INDDeRequestDate.Focus()
                    Me.BarraBotones.SetDocuments(_workOrder.Id, Me.Tag.ToString(), Nothing, GetType(WorkOrder).Name)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, _workOrder.Id, 0, _workOrder.Id)

                    ActionsOnControls = True
                    AsyncLoader(False)
                Else
                    AsyncLoader(False)
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                End If
                INDLcRoot.EndUpdate()
            End Using
        End If
    End Function

    Private Sub AssigningValues()
        With _workOrder
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .Consecutive = Code
            .RequestDate = INDDeRequestDate.EditValue
            .ProgramDate = INDDeProgramDate.EditValue
            .BranchOfficeId = INDSleBranchOffice.EditValue
            .ProtocolId = INDSleProtocol.EditValue
            .PhysicalAssetId = INDSlePhysicalAsset.EditValue
            .MaintenanceResponsibleId = INDSleMaintenanceResponsible.EditValue
            .Description = INDMeDescription.EditValue

            If _listWorkOrderActivities IsNot Nothing AndAlso _listWorkOrderActivities.Any Then
                For Each item In _listWorkOrderActivities
                    .WorkOrderActivities.Add(item)
                Next
            End If
            If _listDeleteWorkOrderActivities IsNot Nothing AndAlso _listDeleteWorkOrderActivities.Any Then
                For Each item In _listDeleteWorkOrderActivities
                    .WorkOrderActivities.Add(item)
                Next
            End If

            If _listWorkOrderConsumables IsNot Nothing AndAlso _listWorkOrderConsumables.Any Then
                For Each item In _listWorkOrderConsumables
                    .WorkOrderConsumables.Add(item)
                Next
            End If
            If _listDeleteWorkOrderConsumables IsNot Nothing AndAlso _listDeleteWorkOrderConsumables.Any Then
                For Each item In _listDeleteWorkOrderConsumables
                    .WorkOrderConsumables.Add(item)
                Next
            End If

            If _listWorkOrderTools IsNot Nothing AndAlso _listWorkOrderTools.Any Then
                For Each item In _listWorkOrderTools
                    .WorkOrderTools.Add(item)
                Next
            End If
            If _listDeleteWorkOrderTools IsNot Nothing AndAlso _listDeleteWorkOrderTools.Any Then
                For Each item In _listDeleteWorkOrderTools
                    .WorkOrderTools.Add(item)
                Next
            End If

            If _listWorkOrderSupplies IsNot Nothing AndAlso _listWorkOrderSupplies.Any Then
                For Each item In _listWorkOrderSupplies
                    .WorkOrderSupplies.Add(item)
                Next
            End If
            If _listDeleteWorkOrderSupplies IsNot Nothing AndAlso _listDeleteWorkOrderSupplies.Any Then
                For Each item In _listDeleteWorkOrderSupplies
                    .WorkOrderSupplies.Add(item)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Sub LoadProtocol()
        _loadingProtocol = True

        Dim protocolActivities As New List(Of ProtocolActivities)
        Dim protocolConsumables As New List(Of ProtocolConsumables)
        Dim protocolTools As New List(Of ProtocolTools)
        Dim protocolSupplies As New List(Of ProtocolSupplier)

        Try
            INDGvActivities.ShowLoadingPanel()
            INDGvConsumables.ShowLoadingPanel()
            INDGvTools.ShowLoadingPanel()
            INDGvSupplies.ShowLoadingPanel()

            Dim protocolXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.GetProtocolById(INDSleProtocol.EditValue)
            If protocolXpo IsNot Nothing Then
                If protocolXpo.Maintenance_ProtocolActivitiess.Count > 0 Then
                    INDLcRoot.SafeInvoke(Sub()
                                             INDLygActivities.HideControl(False)
                                         End Sub)

                    If _listWorkOrderActivities Is Nothing Then
                        _listWorkOrderActivities = New List(Of WorkOrderActivities)
                    End If

                    For Each protocolActivityXpo In protocolXpo.Maintenance_ProtocolActivitiess
                        Dim protocolActivity = New ProtocolActivities()
                        protocolActivity.Id = protocolActivityXpo.Id
                        protocolActivity.Time = protocolActivityXpo.Time
                        protocolActivity.Unit = protocolActivityXpo.Unit
                        protocolActivity.Activity = protocolActivityXpo.Activity
                        protocolActivities.Add(protocolActivity)

                        Dim workOrderActivity = _listWorkOrderActivities.Where(Function(a) a.ProtocolActivityId = protocolActivityXpo.Id).FirstOrDefault
                        If workOrderActivity IsNot Nothing Then
                            protocolActivity.Selected = True
                            protocolActivity.Time = workOrderActivity.Time
                            protocolActivity.Unit = workOrderActivity.Unit
                        End If
                    Next
                End If

                If protocolXpo.Maintenance_ProtocolConsumabless.Count > 0 Then
                    INDLcRoot.SafeInvoke(Sub()
                                             INDLygConsumables.HideControl(False)
                                         End Sub)

                    If _listWorkOrderConsumables Is Nothing Then
                        _listWorkOrderConsumables = New List(Of WorkOrderConsumables)
                    End If

                    For Each protocolConsumableXpo In protocolXpo.Maintenance_ProtocolConsumabless
                        Dim protocolConsumable = New ProtocolConsumables()
                        protocolConsumable.Id = protocolConsumableXpo.Id
                        protocolConsumable.ConsumableCode = protocolConsumableXpo.ConsumableId.Code
                        protocolConsumable.ConsumableName = protocolConsumableXpo.ConsumableId.Name
                        protocolConsumables.Add(protocolConsumable)

                        Dim workOrderConsumable = _listWorkOrderConsumables.Where(Function(a) a.ProtocolConsumableId = protocolConsumableXpo.Id).FirstOrDefault
                        If workOrderConsumable IsNot Nothing Then
                            protocolConsumable.Selected = True
                        End If
                    Next
                End If

                If protocolXpo.Maintenance_ProtocolToolss.Count > 0 Then
                    INDLcRoot.SafeInvoke(Sub()
                                             INDLygTools.HideControl(False)
                                         End Sub)

                    If _listWorkOrderTools Is Nothing Then
                        _listWorkOrderTools = New List(Of WorkOrderTools)
                    End If

                    For Each protocolToolXpo In protocolXpo.Maintenance_ProtocolToolss
                        Dim protocolTool = New ProtocolTools()
                        protocolTool.Id = protocolToolXpo.Id
                        protocolTool.PhysicalAssetPlate = protocolToolXpo.FixedAssetPhysicalAssetId.Plate
                        protocolTool.PhysicalAssetItemName = protocolToolXpo.FixedAssetPhysicalAssetId.ItemId.Description
                        protocolTools.Add(protocolTool)

                        Dim workOrderTool = _listWorkOrderTools.Where(Function(a) a.ProtocolToolId = protocolToolXpo.Id).FirstOrDefault
                        If workOrderTool IsNot Nothing Then
                            protocolTool.Selected = True
                        End If
                    Next
                End If

                If protocolXpo.Maintenance_ProtocolSuppliers.Count > 0 Then
                    INDLcRoot.SafeInvoke(Sub()
                                             INDLygSupplies.HideControl(False)
                                         End Sub)

                    If _listWorkOrderSupplies Is Nothing Then
                        _listWorkOrderSupplies = New List(Of WorkOrderSupplies)
                    End If

                    For Each protocolSupplyXpo In protocolXpo.Maintenance_ProtocolSuppliers
                        Dim protocolSupply = New ProtocolSupplier()
                        protocolSupply.Id = protocolSupplyXpo.Id
                        protocolSupply.ProductCode = protocolSupplyXpo.ProductId.Code
                        protocolSupply.ProductName = protocolSupplyXpo.ProductId.Name
                        protocolSupplies.Add(protocolSupply)

                        Dim workOrderSupply = _listWorkOrderSupplies.Where(Function(a) a.ProtocolSupplyId = protocolSupplyXpo.Id).FirstOrDefault
                        If workOrderSupply IsNot Nothing Then
                            protocolSupply.Selected = True
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            _loadingProtocol = False

            INDGcActivities.SafeInvoke(Sub()
                                           INDGcActivities.DataSource = protocolActivities
                                           INDGcActivities.RefreshDataSource()
                                           INDGvActivities.HideLoadingPanel()
                                       End Sub)

            INDGcConsumables.SafeInvoke(Sub()
                                            INDGcConsumables.DataSource = protocolConsumables
                                            INDGcConsumables.RefreshDataSource()
                                            INDGvConsumables.HideLoadingPanel()
                                        End Sub)

            INDGcTools.SafeInvoke(Sub()
                                      INDGcTools.DataSource = protocolTools
                                      INDGcTools.RefreshDataSource()
                                      INDGvTools.HideLoadingPanel()
                                  End Sub)

            INDGcSupplies.SafeInvoke(Sub()
                                         INDGcSupplies.DataSource = protocolSupplies
                                         INDGcSupplies.RefreshDataSource()
                                         INDGvSupplies.HideLoadingPanel()
                                     End Sub)
        End Try
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Async Sub FrmWorkOrderDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PWorkOrder()
        '******************************
        Me._operativeUnitId = Me.BarraBotones.OperatingUnitValue

        If Me._workOrder IsNot Nothing Then
            If Me._workOrder.Id > 0 Then
                Me.Code = Me._workOrder.Consecutive
                Await LoadControls()
            Else
                Me._workOrder.Consecutive = ResourceManager.GetString("LabelOrTextboxNew")
                Me.LoadWorkOrder()
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        End If

        LoadStatus()
    End Sub

    Private Sub FrmWorkOrderDetail_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _operativeUnitId = Nothing
        _idCurrentSequence = Nothing
        _sequence = Nothing
        _blockRecord = Nothing
        _workOrder = Nothing
        _listWorkOrderActivities = Nothing
        _listDeleteWorkOrderActivities = Nothing
        _listWorkOrderConsumables = Nothing
        _listDeleteWorkOrderConsumables = Nothing
        _listWorkOrderTools = Nothing
        _listDeleteWorkOrderTools = Nothing
        _listWorkOrderSupplies = Nothing
        _listDeleteWorkOrderSupplies = Nothing
        _varImp = Nothing
        _loadingProtocol = Nothing
    End Sub

#End Region

#Region "Activated"

    Private Sub FrmWorkOrderDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        INDSleActivitiesUnit.DataSource = ListUnitTime
        '******************************
        If INDTxtCode.Text Is String.Empty Then
            INDTxtCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Async Sub FrmWorkOrderDetail_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtCode.KeyDown
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
                    Await Me.NewWorkOrder()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleBranchOffice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBranchOffice.QueryPopUp
        If INDSleBranchOffice.Properties.DataSource Is Nothing Then
            INDSleBranchOffice.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.ListBranchOfficeByState(True)
        End If
    End Sub

    Private Sub INDSleProtocol_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProtocol.QueryPopUp
        If INDSleProtocol.Properties.DataSource Is Nothing Then
            INDSleProtocol.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceProtocolByStatus(True)
        End If
    End Sub

    Private Sub INDSleMaintenanceResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleMaintenanceResponsible.QueryPopUp
        If INDSleMaintenanceResponsible.Properties.DataSource Is Nothing Then
            INDSleMaintenanceResponsible.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceResponsibleByStatus(True)
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSlePhysicalAsset_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlePhysicalAsset.ButtonClick
        OpenForm("574", Nothing, True)
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDSeActivitiesTime_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSeActivitiesTime.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) OrElse e.NewValue <= 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleProtocol_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleProtocol.EditValueChanging
        If Me._loadingProtocol Then
            Mensaje(EeventViewerImages.Advertencia) = "Se esta cargando la información del protocolo"
            e.Cancel = True
        End If
    End Sub

    Private Sub INDSleProtocol_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProtocol.EditValueChanged
        INDLygActivities.HideControl(True)
        INDLygConsumables.HideControl(True)
        INDLygTools.HideControl(True)
        INDLygSupplies.HideControl(True)

        If INDSleProtocol.EditValue IsNot Nothing Then
            If Me._workOrder.ProtocolId Is Nothing OrElse Me._workOrder.ProtocolId <> INDSleProtocol.EditValue Then
                If _listWorkOrderActivities IsNot Nothing AndAlso _listWorkOrderActivities.Any() Then
                    If _listDeleteWorkOrderActivities Is Nothing Then
                        _listDeleteWorkOrderActivities = New List(Of WorkOrderActivities)
                    End If
                    While _listWorkOrderActivities.Count > 0
                        If _listWorkOrderActivities(0).Id > 0 Then
                            _listWorkOrderActivities(0).MarkAsDeleted()
                            _listDeleteWorkOrderActivities.Add(_listWorkOrderActivities(0))
                        End If
                        _listWorkOrderActivities.Remove(_listWorkOrderActivities(0))
                    End While
                End If
                If _listWorkOrderConsumables IsNot Nothing AndAlso _listWorkOrderConsumables.Any() Then
                    If _listDeleteWorkOrderConsumables Is Nothing Then
                        _listDeleteWorkOrderConsumables = New List(Of WorkOrderConsumables)
                    End If
                    While _listWorkOrderConsumables.Count > 0
                        If _listWorkOrderConsumables(0).Id > 0 Then
                            _listWorkOrderConsumables(0).MarkAsDeleted()
                            _listDeleteWorkOrderConsumables.Add(_listWorkOrderConsumables(0))
                        End If
                        _listWorkOrderConsumables.Remove(_listWorkOrderConsumables(0))
                    End While
                End If
                If _listWorkOrderTools IsNot Nothing AndAlso _listWorkOrderTools.Any() Then
                    If _listDeleteWorkOrderTools Is Nothing Then
                        _listDeleteWorkOrderTools = New List(Of WorkOrderTools)
                    End If
                    While _listWorkOrderTools.Count > 0
                        If _listWorkOrderTools(0).Id > 0 Then
                            _listWorkOrderTools(0).MarkAsDeleted()
                            _listDeleteWorkOrderTools.Add(_listWorkOrderTools(0))
                        End If
                        _listWorkOrderTools.Remove(_listWorkOrderTools(0))
                    End While
                End If
                If _listWorkOrderSupplies IsNot Nothing AndAlso _listWorkOrderSupplies.Any() Then
                    If _listDeleteWorkOrderSupplies Is Nothing Then
                        _listDeleteWorkOrderSupplies = New List(Of WorkOrderSupplies)
                    End If
                    While _listWorkOrderSupplies.Count > 0
                        If _listWorkOrderSupplies(0).Id > 0 Then
                            _listWorkOrderSupplies(0).MarkAsDeleted()
                            _listDeleteWorkOrderSupplies.Add(_listWorkOrderSupplies(0))
                        End If
                        _listWorkOrderSupplies.Remove(_listWorkOrderSupplies(0))
                    End While
                End If

                Me._workOrder.ProtocolId = INDSleProtocol.EditValue
            End If

            Task.Factory.StartNew(AddressOf LoadProtocol)
        End If
    End Sub

    Private Sub INDSeActivitiesTime_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeActivitiesTime.EditValueChanged, INDSleActivitiesUnit.EditValueChanged
        Dim protocolActivity = CType(INDGvActivities.GetFocusedRow(), ProtocolActivities)
        If protocolActivity.Selected Then
            Dim workOrderActivity = _listWorkOrderActivities.Where(Function(a) a.ProtocolActivityId = protocolActivity.Id).FirstOrDefault()
            If workOrderActivity IsNot Nothing Then
                If sender.GetType() = GetType(DevExpress.XtraEditors.SpinEdit) Then
                    workOrderActivity.Time = CType(sender, DevExpress.XtraEditors.SpinEdit).EditValue
                ElseIf sender.GetType() = GetType(DevExpress.XtraEditors.SearchLookUpEdit) Then
                    workOrderActivity.Unit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit).EditValue
                End If
            End If
        End If
    End Sub

#End Region

#Region "CheckedChanged"

    Private Sub INDChkActivitiesSelect_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkActivitiesSelect.CheckedChanged
        Dim control As CheckEdit = CType(sender, CheckEdit)
        Dim protocolActivity = CType(INDGvActivities.GetFocusedRow(), ProtocolActivities)
        If control.Checked Then
            _listWorkOrderActivities.Add(New WorkOrderActivities() With
                                        {
                                            .ProtocolActivityId = protocolActivity.Id,
                                            .Time = protocolActivity.Time,
                                            .Unit = protocolActivity.Unit
                                        })
        Else
            Dim workOrderActivity = _listWorkOrderActivities.Where(Function(a) a.ProtocolActivityId = protocolActivity.Id).FirstOrDefault()
            If workOrderActivity IsNot Nothing Then
                If workOrderActivity.Id > 0 Then
                    workOrderActivity.MarkAsDeleted()
                    _listDeleteWorkOrderActivities.Add(workOrderActivity)
                End If
                _listWorkOrderActivities.Remove(workOrderActivity)
            End If
        End If
    End Sub

    Private Sub INDChkConsumablesSelect_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkConsumablesSelect.CheckedChanged
        Dim control As CheckEdit = CType(sender, CheckEdit)
        Dim protocolConsumable = CType(INDGvConsumables.GetFocusedRow(), ProtocolConsumables)
        If control.Checked Then
            _listWorkOrderConsumables.Add(New WorkOrderConsumables() With
                                        {
                                            .ProtocolConsumableId = protocolConsumable.Id
                                        })
        Else
            Dim workOrderConsumable = _listWorkOrderConsumables.Where(Function(a) a.ProtocolConsumableId = protocolConsumable.Id).FirstOrDefault()
            If workOrderConsumable IsNot Nothing Then
                If workOrderConsumable.Id > 0 Then
                    workOrderConsumable.MarkAsDeleted()
                    _listDeleteWorkOrderConsumables.Add(workOrderConsumable)
                End If
                _listWorkOrderConsumables.Remove(workOrderConsumable)
            End If
        End If
    End Sub

    Private Sub INDChkToolsSelect_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkToolsSelect.CheckedChanged
        Dim control As CheckEdit = CType(sender, CheckEdit)
        Dim protocolTool = CType(INDGvTools.GetFocusedRow(), ProtocolTools)
        If control.Checked Then
            _listWorkOrderTools.Add(New WorkOrderTools() With
                                        {
                                            .ProtocolToolId = protocolTool.Id
                                        })
        Else
            Dim workOrderTool = _listWorkOrderTools.Where(Function(a) a.ProtocolToolId = protocolTool.Id).FirstOrDefault()
            If workOrderTool IsNot Nothing Then
                If workOrderTool.Id > 0 Then
                    workOrderTool.MarkAsDeleted()
                    _listDeleteWorkOrderTools.Add(workOrderTool)
                End If
                _listWorkOrderTools.Remove(workOrderTool)
            End If
        End If
    End Sub

    Private Sub INDChkSuppliesSelect_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkSuppliesSelect.CheckedChanged
        Dim control As CheckEdit = CType(sender, CheckEdit)
        Dim protocolSupply = CType(INDGvSupplies.GetFocusedRow(), ProtocolSupplier)
        If control.Checked Then
            _listWorkOrderSupplies.Add(New WorkOrderSupplies() With
                                        {
                                            .ProtocolSupplyId = protocolSupply.Id
                                        })
        Else
            Dim workOrderSupply = _listWorkOrderSupplies.Where(Function(a) a.ProtocolSupplyId = protocolSupply.Id).FirstOrDefault()
            If workOrderSupply IsNot Nothing Then
                If workOrderSupply.Id > 0 Then
                    workOrderSupply.MarkAsDeleted()
                    _listDeleteWorkOrderSupplies.Add(workOrderSupply)
                End If
                _listWorkOrderSupplies.Remove(workOrderSupply)
            End If
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"

    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._workOrder IsNot Nothing AndAlso Me._workOrder.Id > 0 Then
            If (MessageIndigo.Show(BaseClass.obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, BaseClass.obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDTxtCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDTxtCode.Text = Me.IdEntity.Trim()
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
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.None)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDTxtCode.ButtonClick
        OpenSearch()
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
        _workOrder.State = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _workOrder.State = 1
        _varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _workOrder.State = 3
            _varImp = 3

            Using PopUpAnnulmentReason As New PopUpAnnulmentReasonWorkOrder()
                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    _workOrder.ReversalReasonId = PopUpAnnulmentReason.ReversalReasonId
                    _workOrder.DescriptionReversal = PopUpAnnulmentReason.ReversalDescription
                End If
            End Using

            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' metodo para guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _workOrder.State = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' metodo para actualizar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _workOrder.State = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _workOrder.Id, 0, _workOrder.Id)
    End Sub

#End Region

End Class
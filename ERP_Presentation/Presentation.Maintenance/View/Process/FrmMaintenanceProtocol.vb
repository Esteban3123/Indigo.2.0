'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Diego André Roldán Lozano
' Created          : 2018-08-31
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Maintenance.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.Data.Xpo

Public Class FrmMaintenanceProtocol
    Implements IcrudBase

#Region "Propiedades"
    ''' <summary>
    ''' Secuencia numerica
    ''' </summary>
    Private _sequence As MaintenanceSequence
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordMaintenance
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"
    ''' <summary>
    ''' entidad de protocolo de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private maintenanceProtocol As MaintenanceProtocol

    Private activityModify As ProtocolActivities = Nothing

    Private model As New MProtocolMaintenance(Me.Tag)

    Private _maintenanceParameter As Task(Of MaintenanceParameter)

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Public Property Status As Boolean
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public WriteOnly Property Mensaje(status As eStatusResult) As String
        Set(value As String)
            If status = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf status = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf status = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()

            INDTxtCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDSleEquipment.Enabled = value
            INDGleType.Enabled = value
            INDceColor.Enabled = value
            INDSleResponsible.Enabled = value
            INDtxtTotalHours.Enabled = value

            INDPceActivity.Enabled = value
            INDSleConsumible.Enabled = value
            INDSleHerramientas.Enabled = value
            INDSleInsumos.Enabled = value

            INDGcActivities.Enabled = value
            INDGcConsumible.Enabled = value
            INDGcHerramientas.Enabled = value
            INDGcInsumos.Enabled = value

            INDBtnAddActivity.Enabled = value
            INDBtnAddConsumible.Enabled = value
            INDBtnAddInsumo.Enabled = value
            INDBtnAddHerramientas.Enabled = value

            INDLcRoot.EndUpdate()
            If value Then
                INDTxtName.Focus()
            Else
                INDTxtCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
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

    ''' <summary>
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
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
#End Region

#Region "Handlers"
    Private Sub FrmMaintenanceProtocol_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        GetSequence()
        LoadStatus()
        LoadMaintenanceParameters()
        LoadUnits()
        loadTypes()
        loadActionViews()
        Deshacer()
    End Sub

    Private Sub FrmMaintenanceProtocol_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDTxtCode.Text Is String.Empty Then
            INDTxtCode.Focus()
        End If
    End Sub

    Private Sub FrmMaintenanceProtocol_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub FrmMaintenanceProtocol_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        maintenanceProtocol = Nothing
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.maintenanceProtocol IsNot Nothing AndAlso Me.maintenanceProtocol.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDTxtCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDTxtCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtCode.KeyDown
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
                    Await Me.NewMaintenanceProtocol()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "Crud"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of MaintenanceProtocol) = Await model.SaveMaintenanceProtocolAsync(Me.maintenanceProtocol, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If maintenanceProtocol.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.MaintenanceSequenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me.maintenanceProtocol = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
            Else
                AsyncLoader(False)
                INDTxtCode.Enabled = False
            End If
            Mensaje(result.StatusCode) = result.Message
        Catch ex As Exception
            AsyncLoader(False)
            INDTxtCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewMaintenanceProtocol()
        End If
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If Me.maintenanceProtocol IsNot Nothing AndAlso Me.maintenanceProtocol.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await model.DeleteMaintenanceProtocol(Me.maintenanceProtocol)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDTxtCode.Enabled = False
                    End If
                    Mensaje(result.StatusCode) = result.Message
                Catch ex As Exception
                    AsyncLoader(False)
                    INDTxtCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
#End Region

#Region "Functions"
    Public Sub GetSequence()
        Task.Factory.StartNew(Async Sub()
                                  Using model As New MProtocolMaintenance(Me.MyTag)
                                      Me.Sequense = Await model.GetSequense()
                                  End Using
                              End Sub)
    End Sub

    Private Sub loadTypes()
        Dim types As New List(Of Tuple(Of Byte, String))()
        types.Add(New Tuple(Of Byte, String)(1, "Pruebas de seguridad"))
        types.Add(New Tuple(Of Byte, String)(2, "Verificación y Calibración"))
        types.Add(New Tuple(Of Byte, String)(3, "Matenimiento Preventivo"))
        types.Add(New Tuple(Of Byte, String)(4, "Mantenimiento Correctivo"))
        types.Add(New Tuple(Of Byte, String)(5, "Otros"))
        INDGleType.Properties.DataSource = types
    End Sub

    Private Sub loadActionViews()
        IndigoGridView1.SetListAcction(INDGvActivities, {eAcciones.Remove, eAcciones.Edit}.ToList())
        IndigoGridView2.SetListAcction(INDGvConsumible, {eAcciones.Remove}.ToList())
        IndigoGridView3.SetListAcction(INDGvHerramientas, {eAcciones.Remove}.ToList())
        IndigoGridView4.SetListAcction(INDGvInsumos, {eAcciones.Remove}.ToList())

        INDGvActivities.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
        INDGvConsumible.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
        INDGvHerramientas.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
        INDGvInsumos.Columns.FirstOrDefault(Function(o) o.Name = "colActions").Width = 100
    End Sub

    Private Sub LoadMaintenanceParameters()
        'Task.Factory.StartNew(Sub()
        '                          Using mParameter As New MMaintenanceParameter(Me.MyTag)
        '                              _maintenanceParameter = mParameter.ListMaintenanceParameterAsync()
        '                          End Using
        '                      End Sub)
        Using mParameter As New MMaintenanceParameter(Me.MyTag)
            _maintenanceParameter = mParameter.ListMaintenanceParameterAsync()
        End Using
    End Sub
    Private Sub LoadUnits()
        Dim units As New List(Of Tuple(Of Byte, String))()
        units.Add(New Tuple(Of Byte, String)(1, "Minutos"))
        units.Add(New Tuple(Of Byte, String)(2, "Horas"))
        'units.Add(New Tuple(Of Byte, String)(3, "Días"))
        INDGleUnit.Properties.DataSource = units
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.35)},
                              New ColumnInfo() With {.Caption = "Artículo", .FieldName = "FixedAssetItemName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.35)},
                              New ColumnInfo() With {.Caption = "Tipo Responsable", .FieldName = "ResponsibleTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.35)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StateName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}
            }.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMaintenanceProtocol
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDTxtCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDTxtCode.Enabled = False
        End If
    End Sub

    Private Sub CleanControls()
        INDLcRoot.BeginUpdate()

        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        Status = True
        Code = String.Empty
        INDTxtName.EditValue = Nothing
        INDSleConsumible.EditValue = Nothing
        INDSleEquipment.EditValue = Nothing
        INDGleType.EditValue = Nothing
        INDceColor.EditValue = Nothing
        INDSleResponsible.EditValue = Nothing
        INDMeActivity.EditValue = Nothing
        INDspnUnit.EditValue = 0

        INDGcActivities.DataSource = Nothing
        INDSleConsumible.EditValue = Nothing
        INDGcConsumible.DataSource = Nothing
        INDSleHerramientas.EditValue = Nothing
        INDGcHerramientas.DataSource = Nothing
        INDSleInsumos.EditValue = Nothing
        INDGcInsumos.DataSource = Nothing

        INDSleEquipment.Properties.NullText = String.Empty
        INDSleResponsible.Properties.NullText = String.Empty

        INDLcRoot.EndUpdate()

        maintenanceProtocol = Nothing
        activityModify = Nothing

        _consumibleSelected = Nothing
        _toolSelected = Nothing
        _supplieSelected = Nothing

        DeleteBlockedRecord()
    End Sub

    Private Sub AssigningValues()
        With maintenanceProtocol
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = INDTxtName.Text
            .FixedAssetItemId = INDSleEquipment.EditValue
            .ResponsibleTypeId = INDSleResponsible.EditValue
            .ConsumabeDescription = ""
            .SupplyDescription = ""
            .ToolsDescription = ""
            .Type = INDGleType.EditValue
            .Color = DirectCast(INDceColor.EditValue, System.Drawing.Color).ToArgb()
            .State = Status = CBool(eActionsStatusRecords.Active)
        End With
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewMaintenanceProtocol() As Task
        maintenanceProtocol = New MaintenanceProtocol()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MProtocolMaintenance(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    Private Async Sub DeleteBlockedRecord()
        Using Model As New MProtocolMaintenance(CStr(Me.Tag))
            If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End If
        End Using
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Me.BarraBotones.StatusRecordVisible = True
            Using model As New MProtocolMaintenance(CStr(Me.Tag))
                AsyncLoader(True)
                maintenanceProtocol = Await model.GetMaintenanceProtocol(INDTxtCode.Text.Trim)
                If maintenanceProtocol IsNot Nothing AndAlso maintenanceProtocol.Id > 0 Then
                    _record = Await model.GetBlockRecord(CStr(Me.Tag), CStr(maintenanceProtocol.Id))
                    With maintenanceProtocol
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        Code = .Code
                        INDTxtName.Text = .Name
                        INDSleEquipment.EditValue = .FixedAssetItemId
                        INDSleResponsible.EditValue = .ResponsibleTypeId
                        INDSleEquipment.Properties.NullText = .FixedAssetItemName
                        INDSleResponsible.Properties.NullText = .ResponsibleTypeName
                        INDGleType.EditValue = .Type
                        INDceColor.EditValue = System.Drawing.Color.FromArgb(.Color)

                        INDGcActivities.DataSource = .ProtocolActivities.ToList()
                        INDGcConsumible.DataSource = .ProtocolConsumables.ToList()
                        INDGcHerramientas.DataSource = .ProtocolTools.ToList()
                        INDGcInsumos.DataSource = .ProtocolSupplier.ToList()

                        Status = .State
                    End With
                    INDGcActivities.RefreshDataSource()
                    INDGcConsumible.RefreshDataSource()
                    INDGcHerramientas.RefreshDataSource()
                    INDGcInsumos.RefreshDataSource()
                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.maintenanceProtocol.Code)
                    If _record.Id = 0 Then
                        _record = (Await model.SaveBlockRecord(
                            New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = maintenanceProtocol.Id})
                            ).ObjectEmbbeded
                    Else
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(maintenanceProtocol.Id)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    AsyncLoader(False)
                    ActionsOnControls = True
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewMaintenanceProtocol()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDTxtCode.Focus()
                    End If
                End If
            End Using
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.maintenanceProtocol.Code, Me.maintenanceProtocol.FixedAssetItemName),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.maintenanceProtocol.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.maintenanceProtocol.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.maintenanceProtocol.Code, Me.maintenanceProtocol.FixedAssetItemName)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.maintenanceProtocol.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.maintenanceProtocol.Code) Then
            Try
                AsyncLoader(True)
                Dim Result = Await model.ChangeState(Me.maintenanceProtocol.Code, Status = eActionsStatusRecords.Active)
                Select Case Result.StatusCode
                    Case eStatusResult.SUCCESS
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me.maintenanceProtocol = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Case eStatusResult.WARNING
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Case eStatusResult.EXCEPTION
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                End Select
                AsyncLoader(False)
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
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
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub INDSleEquipment_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEquipment.QueryPopUp
        If INDSleEquipment.Properties.DataSource Is Nothing Then
            INDSleEquipment.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True)
        End If
    End Sub

    Private Sub INDSleResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsible.QueryPopUp
        If INDSleResponsible.Properties.DataSource Is Nothing Then
            INDSleResponsible.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleType() 'XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleByStatus(True)
        End If
    End Sub

    Private Sub INDSleConsumible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleConsumible.QueryPopUp
        If INDSleConsumible.Properties.DataSource Is Nothing Then
            INDSleConsumible.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListConsumibleByStatus(True)
        End If
    End Sub

    Private Sub INDSleHerramientas_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleHerramientas.QueryPopUp
        If INDSleHerramientas.Properties.DataSource Is Nothing Then
            INDSleHerramientas.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListAllFixedAssetPhysicalAsset()
        End If
    End Sub

    Private Sub INDSleInsumos_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInsumos.QueryPopUp
        If INDSleInsumos.Properties.DataSource Is Nothing Then
            INDSleInsumos.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListInventoryProductByProductTypeClasses({"4"}.ToList())
        End If
    End Sub

    Private _consumibleSelected As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_Consumable
    Private Sub INDSlvConsumible_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDSlvConsumible.RowClick
        Dim obj = DirectCast(INDSlvConsumible.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _consumibleSelected = obj.OriginalRow
        End If
    End Sub

    Private Sub INDBtnAddConsumible_Click(sender As Object, e As EventArgs) Handles INDBtnAddConsumible.Click
        If INDSleConsumible.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Consumible"
            INDSleConsumible.Focus()
            Return
        End If
        If maintenanceProtocol.ProtocolConsumables.Any(Function(o) o.ConsumableId = CInt(INDSleConsumible.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Consumible seleccionado ya se encuentra en el listado"
            INDSleConsumible.Focus()
            Return
        End If
        If _consumibleSelected IsNot Nothing Then
            maintenanceProtocol.ProtocolConsumables.Add(New ProtocolConsumables() With {.ConsumableId = _consumibleSelected.Id, .ConsumableCode = _consumibleSelected.Code, .ConsumableName = _consumibleSelected.Name})
            INDGcConsumible.DataSource = maintenanceProtocol.ProtocolConsumables.ToList()
            INDGcConsumible.RefreshDataSource()

            INDSleConsumible.EditValue = Nothing
            _consumibleSelected = Nothing
            INDSleConsumible.Focus()
        End If
        'Dim obj = DirectCast(INDSleConsumible.Properties.View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        'If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
        '    Dim regi As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_Consumable = obj.OriginalRow
        '    maintenanceProtocol.ProtocolConsumables.Add(New ProtocolConsumables() With {.ConsumableId = regi.Id, .ConsumableCode = regi.Code, .ConsumableName = regi.Name})
        '    INDGcConsumible.DataSource = maintenanceProtocol.ProtocolConsumables.ToList()
        '    INDGcConsumible.RefreshDataSource()

        '    INDSleConsumible.EditValue = Nothing
        '    INDSleConsumible.Focus()
        'End If
    End Sub

    Private _toolSelected As Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetXpo
    Private Sub INDSlvTools_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDSlvTools.RowClick
        Dim obj = DirectCast(INDSlvTools.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _toolSelected = obj.OriginalRow
        End If
    End Sub

    Private Sub INDBtnAddHerramientas_Click(sender As Object, e As EventArgs) Handles INDBtnAddHerramientas.Click
        If INDSleHerramientas.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Herramienta"
            INDSleHerramientas.Focus()
            Return
        End If
        If maintenanceProtocol.ProtocolTools.Any(Function(o) o.FixedAssetPhysicalAssetId = CInt(INDSleHerramientas.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "La herramienta seleccionada ya se encuentra en el listado"
            INDSleHerramientas.Focus()
            Return
        End If
        If _toolSelected IsNot Nothing Then
            maintenanceProtocol.ProtocolTools.Add(New ProtocolTools() With {.FixedAssetPhysicalAssetId = _toolSelected.Id, .PhysicalAssetPlate = _toolSelected.Plate, .PhysicalAssetItemName = _toolSelected.ItemId.Description})
            INDGcHerramientas.DataSource = maintenanceProtocol.ProtocolTools.ToList()
            INDGcHerramientas.RefreshDataSource()

            INDSleHerramientas.EditValue = Nothing
            _toolSelected = Nothing
            INDSleHerramientas.Focus()
        End If
        'Dim obj = DirectCast(INDSleHerramientas.Properties.View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        'If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
        '    Dim regi As Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetXpo = obj.OriginalRow
        '    maintenanceProtocol.ProtocolTools.Add(New ProtocolTools() With {.FixedAssetPhysicalAssetId = regi.Id, .PhysicalAssetPlate = regi.Plate, .PhysicalAssetItemName = regi.ItemId.Description})
        '    INDGcHerramientas.DataSource = maintenanceProtocol.ProtocolTools.ToList()
        '    INDGcHerramientas.RefreshDataSource()

        '    INDSleHerramientas.EditValue = Nothing
        '    INDSleHerramientas.Focus()
        'End If
    End Sub

    Private _supplieSelected As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo
    Private Sub INDSlvSupplies_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDSlvSupplies.RowClick
        Dim obj = DirectCast(INDSlvSupplies.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _supplieSelected = obj.OriginalRow
        End If
    End Sub

    Private Sub INDBtnAddInsumo_Click(sender As Object, e As EventArgs) Handles INDBtnAddInsumo.Click
        If INDSleInsumos.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Consumible"
            INDSleInsumos.Focus()
            Return
        End If
        If maintenanceProtocol.ProtocolSupplier.Any(Function(o) o.ProductId = CInt(INDSleInsumos.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = "El insumo seleccionado ya se encuentra en el listado"
            INDSleInsumos.Focus()
            Return
        End If
        If _supplieSelected IsNot Nothing Then
            maintenanceProtocol.ProtocolSupplier.Add(New ProtocolSupplier() With {.ProductId = _supplieSelected.Id, .ProductCode = _supplieSelected.Code, .ProductName = _supplieSelected.Name})
            INDGcInsumos.DataSource = maintenanceProtocol.ProtocolSupplier.ToList()
            INDGcInsumos.RefreshDataSource()

            INDSleInsumos.EditValue = Nothing
            _supplieSelected = Nothing
            INDSleInsumos.Focus()
        End If
        'Dim obj = DirectCast(INDSleInsumos.Properties.View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        'If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
        '    Dim regi As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo = obj.OriginalRow
        '    maintenanceProtocol.ProtocolSupplier.Add(New ProtocolSupplier() With {.ProductId = regi.Id, .ProductCode = regi.Code, .ProductName = regi.Name})
        '    INDGcInsumos.DataSource = maintenanceProtocol.ProtocolSupplier.ToList()
        '    INDGcInsumos.RefreshDataSource()

        '    INDSleInsumos.EditValue = Nothing
        '    INDSleInsumos.Focus()
        'End If
    End Sub

    Private Sub INDSleEquipment_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleEquipment.EditValueChanged
        If INDSleEquipment.EditValue IsNot Nothing AndAlso INDGvEquipment.GetFocusedRow() IsNot Nothing Then
            'Application.DoEvents()
            Dim obj = DirectCast(INDGvEquipment.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
            If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                Dim regi As Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetEquipmentXpo = obj.OriginalRow
                'INDTxtTrademark.Text = regi.item
            End If

        Else

        End If
    End Sub

    Private Sub INDBtnAddActivity_Click(sender As Object, e As EventArgs) Handles INDBtnAddActivity.Click
        If INDMeActivity.Text.Equals(String.Empty) Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor llene todos los datos"
            INDPceActivity.ShowPopup()
            Return
        End If
        If _maintenanceParameter.Result.TimeProtocolRequire Then
            If CDec(INDspnUnit.EditValue) = 0 OrElse INDGleUnit.EditValue Is Nothing _
                OrElse INDspnUnit.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Por favor llene todos los datos"
                INDPceActivity.ShowPopup()
                Return
            End If
        End If
        Dim newProtocolActivity As New ProtocolActivities()
        If activityModify IsNot Nothing Then
            newProtocolActivity = activityModify
        End If
        With newProtocolActivity
            .Activity = INDMeActivity.Text
            .Time = IIf(_maintenanceParameter.Result.TimeProtocolRequire, CInt(INDspnUnit.EditValue), Nothing)
            .Unit = IIf(_maintenanceParameter.Result.TimeProtocolRequire, CByte(INDGleUnit.EditValue), Nothing)
            .TimeName = IIf(_maintenanceParameter.Result.TimeProtocolRequire, $"{CInt(INDspnUnit.EditValue)} {INDGleUnit.Text}", Nothing)
        End With
        If newProtocolActivity.Id > 0 Then
            newProtocolActivity.MarkAsModified()
        End If
        'maintenanceProtocol.ProtocolActivities.Add(New ProtocolActivities() With {.Activity = INDMeActivity.Text, .ActivityDescription = INDMeActivity.Text, .Time = CInt(INDspnUnit.EditValue), .Unit = INDGleUnit.EditValue, .TimeName = $"{CInt(INDspnUnit.EditValue)} {INDGleUnit.Text}"})
        maintenanceProtocol.ProtocolActivities.Add(newProtocolActivity)
        INDGcActivities.DataSource = maintenanceProtocol.ProtocolActivities.ToList()
        INDGcActivities.RefreshDataSource()
        INDMeActivity.EditValue = Nothing
        INDspnUnit.EditValue = 0
        INDPceActivity.ShowPopup()
        INDMeActivity.Focus()
    End Sub

    Private Async Sub INDPceActivity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceActivity.QueryPopUp
        If _maintenanceParameter Is Nothing OrElse Not _maintenanceParameter.IsCompleted Then
            INDPgrPanel.BringToFront()
        End If
        Dim parameter As MaintenanceParameter = Await _maintenanceParameter
        If parameter Is Nothing OrElse parameter.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron parámetros de Mantenimiento"
            Return
        End If
        If parameter.TimeProtocolRequire Then
            INDLciTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDColTime.Visible = True
        Else
            INDLciTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDColTime.Visible = False
        End If
        INDPgrPanel.SendToBack()
        INDMeActivity.Focus()
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If sender.Tag = "Edit" Then

            activityModify = INDGvActivities.GetFocusedRow()
            INDMeActivity.Text = activityModify.Activity
            INDspnUnit.EditValue = activityModify.Time
            INDGleUnit.EditValue = activityModify.Unit

            INDPceActivity.ShowPopup()

        ElseIf sender.Tag = "Remove" Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                CType(INDGvActivities.GetFocusedRow(), ProtocolActivities).MarkAsDeleted()
                INDGcActivities.DataSource = maintenanceProtocol.ProtocolActivities.ToList()
                INDGcActivities.RefreshDataSource()
                If maintenanceProtocol.Id > 0 Then
                    maintenanceProtocol.MarkAsModified()
                End If
            End If
        End If
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDGvConsumible.GetFocusedRow(), ProtocolConsumables).MarkAsDeleted()
            INDGcConsumible.DataSource = maintenanceProtocol.ProtocolConsumables.ToList()
            INDGcConsumible.RefreshDataSource()
            If maintenanceProtocol.Id > 0 Then
                maintenanceProtocol.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDGvHerramientas.GetFocusedRow(), ProtocolTools).MarkAsDeleted()
            INDGcHerramientas.DataSource = maintenanceProtocol.ProtocolTools.ToList()
            INDGcHerramientas.RefreshDataSource()
            If maintenanceProtocol.Id > 0 Then
                maintenanceProtocol.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction, IndigoGridView4.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDGvInsumos.GetFocusedRow(), ProtocolSupplier).MarkAsDeleted()
            INDGcInsumos.DataSource = maintenanceProtocol.ProtocolSupplier.ToList()
            INDGcInsumos.RefreshDataSource()
            If maintenanceProtocol.Id > 0 Then
                maintenanceProtocol.MarkAsModified()
            End If
        End If
    End Sub

    Private Sub INDSleResponsible_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtTotalHours.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDPceActivity.ShowPopup()
            INDMeActivity.Focus()
        End If
    End Sub

    Private Sub INDPceActivity_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceActivity.Closed
        activityModify = Nothing
    End Sub

    Private Async Sub INDGcActivities_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcActivities.DataSourceChanged
        Dim parameter As MaintenanceParameter = Await _maintenanceParameter
        If INDGcActivities.DataSource Is Nothing Then
            INDtxtTotalHours.EditValue = "0 Horas"
        Else
            'protocolActivities
            If parameter.TimeProtocolRequire Then
                Dim minutos As Integer = CType(INDGcActivities.DataSource, IEnumerable(Of ProtocolActivities)) _
                .Where(Function(m) m.Unit IsNot Nothing AndAlso m.Unit.Value = 1) _
                .Sum(Function(m) m.Time.Value)
                Dim horas As Integer = CType(INDGcActivities.DataSource, IEnumerable(Of ProtocolActivities)) _
                    .Where(Function(m) m.Unit IsNot Nothing AndAlso m.Unit.Value = 2) _
                    .Sum(Function(m) m.Time.Value)
                If minutos > 59 Then
                    Dim h As Integer = Math.Truncate(minutos / 60)
                    minutos = minutos - (h * 60)
                    horas += h
                End If
                INDtxtTotalHours.EditValue = $"{horas.ToString()}:{If(minutos < 10, "0" & minutos, minutos.ToString())} Horas"
            Else
                INDtxtTotalHours.EditValue = "0 Horas"
            End If
        End If
    End Sub
#End Region

End Class
'***********************************************************************
' Assembly         : Presentation.Maintenance
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 04-02-2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Maintenance.MVP

#End Region

Public Class FrmMaintenanceFailureRequest
    Implements IMaintenanceFailureRequest

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PMaintenanceFailureRequest

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Dim _sequence As MaintenanceSequence

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _blockRecord As BlockRecordMaintenance

    ''' <summary>
    ''' Represa el Objeto entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _maintenanceFailureRequest As MaintenanceFailureRequest

    ''' <summary>
    ''' Listado de activos afectados a reportar
    ''' </summary>
    Dim _listMaintenanceFailureRequestDetail As List(Of MaintenanceFailureRequestDetail)

    ''' <summary>
    ''' Listado de activos afectados a eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteMaintenanceFailureRequestDetail As List(Of MaintenanceFailureRequestDetail)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim _indexEditRecord As Integer

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim _maintenanceFailureRequestDetail As MaintenanceFailureRequestDetail

    Dim PhysicalAssetPlate As String

    Dim ItemDescription As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As String Implements IMaintenanceFailureRequest.MyTag
        Get
            Return Me.Tag.ToString
        End Get
    End Property

    ''' <summary>
    ''' Layout del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMaintenanceFailureRequest.MyLayoutControl
        Get
            Return LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Activa e inactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IMaintenanceFailureRequest.ActionsOnControls
        Set(value As Boolean)
            INDlyMaintenanceFailureRequest.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDSleBranchOffice.Enabled = value
            INDdteFailureRequestDate.Enabled = value
            INDGleTypeRequest.Enabled = value
            INDGleReport.Enabled = value
            INDtxtNameOther.Enabled = value
            INDmemoObservation.Enabled = value
            INDpceAddFixedAsset.Enabled = value

            INDlyMaintenanceFailureRequest.EndUpdate()
            If value Then
                INDdteFailureRequestDate.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la configuración de secuencia numerica asignada al funcional
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As Domain.Entities.MaintenanceSequence Implements IMaintenanceFailureRequest.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IMaintenanceFailureRequest.Code
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Public Property FailureRequestDate As Date Implements IMaintenanceFailureRequest.FailureRequestDate
        Get
            Return INDdteFailureRequestDate.EditValue
        End Get
        Set(value As Date)
            INDdteFailureRequestDate.EditValue = value
        End Set
    End Property

    Public Property NameOther As String Implements IMaintenanceFailureRequest.NameOther
        Get
            Return INDtxtNameOther.EditValue
        End Get
        Set(value As String)
            INDtxtNameOther.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comentarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observation As String Implements IMaintenanceFailureRequest.Observation
        Get
            Return INDmemoObservation.EditValue
        End Get
        Set(value As String)
            INDmemoObservation.EditValue = value
        End Set
    End Property

#End Region

#Region "DataSource"

    Private _FillingTypeRequest As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeRequest As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeRequest Is Nothing Then
                _FillingTypeRequest = New List(Of Tuple(Of Integer, String))
                _FillingTypeRequest.Add(New Tuple(Of Integer, String)(1, "Falla General"))
                _FillingTypeRequest.Add(New Tuple(Of Integer, String)(2, "Revisión"))
                _FillingTypeRequest.Add(New Tuple(Of Integer, String)(3, "Otro"))
            End If
            Return _FillingTypeRequest
        End Get
    End Property

    Private _FillingReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReport Is Nothing Then
                _FillingReport = New List(Of Tuple(Of Integer, String))
                _FillingReport.Add(New Tuple(Of Integer, String)(1, "Usuario del Sistema"))
                _FillingReport.Add(New Tuple(Of Integer, String)(2, "Otro"))
            End If
            Return _FillingReport
        End Get
    End Property

    Private _FillingClass As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingClass As List(Of Tuple(Of Integer, String))
        Get
            If _FillingClass Is Nothing Then
                _FillingClass = New List(Of Tuple(Of Integer, String))
                _FillingClass.Add(New Tuple(Of Integer, String)(1, "Activo"))
                _FillingClass.Add(New Tuple(Of Integer, String)(2, "Parte de Activo"))
            End If
            Return _FillingClass
        End Get
    End Property

    Public Property ItemXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return INDSlItem.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlItem.Properties.DataSource = value
        End Set
    End Property

    Public Property PartsXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return INDslParts.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslParts.Properties.DataSource = value
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Codigo", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha Reporte", .FieldName = "DateFailure", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Observación", .FieldName = "Observation", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMaintenanceFailureRequest
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
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
            Await NewMaintenanceFailureRequest()
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If _maintenanceFailureRequest IsNot Nothing AndAlso _maintenanceFailureRequest.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If

            If _listMaintenanceFailureRequestDetail Is Nothing OrElse _listMaintenanceFailureRequestDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un ítem."
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MMaintenanceFailureRequest(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveMaintenanceFailureRequestAsync(Me._maintenanceFailureRequest, Me._idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _maintenanceFailureRequest.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If _maintenanceFailureRequest.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        ElseIf _maintenanceFailureRequest.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                        End If
                    ElseIf _maintenanceFailureRequest.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If _maintenanceFailureRequest.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf _maintenanceFailureRequest.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                        ElseIf _maintenanceFailureRequest.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    Me._maintenanceFailureRequest = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                Else
                    INDBteCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Result.MessageResult(0) IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString()
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
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
        Set(ByVal value As String)

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
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvFailureRequest, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvFailureRequest.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        Me.INDGleTypeRequest.Properties.DataSource = FillingTypeRequest
        Me.INDGleReport.Properties.DataSource = FillingReport
        Me.INDsleTransactionClass.Properties.DataSource = FillingClass
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlyMaintenanceFailureRequest.BeginUpdate()
        Await DeleteBlockedRecord()

        Code = String.Empty
        FailureRequestDate = Date.Now()
        INDSleBranchOffice.EditValue = Nothing
        INDSleBranchOffice.Properties.NullText = String.Empty
        INDGleTypeRequest.Text = String.Empty
        INDGleReport.Text = String.Empty
        NameOther = Nothing
        Observation = Nothing

        INDpceAddFixedAsset.Enabled = False
        INDGcFailureRequestDetail.DataSource = Nothing
        CleanControlsPopup()

        _doc = Nothing
        _maintenanceFailureRequest = Nothing
        _listMaintenanceFailureRequestDetail = Nothing
        _listDeleteMaintenanceFailureRequestDetail = Nothing
        _maintenanceFailureRequestDetail = Nothing

        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        INDlyMaintenanceFailureRequest.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Private Sub CleanControlsPopup()
        _maintenanceFailureRequestDetail = Nothing

        INDSlItem.EditValue = Nothing
        INDslParts.EditValue = Nothing
        INDsleTransactionClass.EditValue = Nothing
        INDMeDetailDescription.EditValue = Nothing

        INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._maintenanceFailureRequest.Code, Me._maintenanceFailureRequest.DateFailure),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._maintenanceFailureRequest.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._maintenanceFailureRequest.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._maintenanceFailureRequest.Code, Me._maintenanceFailureRequest.DateFailure)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._maintenanceFailureRequest.Code)
            Return Me._doc
        End If
    End Function

    Private Async Function NewMaintenanceFailureRequest() As Task
        If Me._idOperativeUnit = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una unidad operativa"
            Exit Function
        End If

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
                        Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
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
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        BarraBotones.StatusRecord = "1"
        BarraBotones.StatusRecordVisible = True
        Me._maintenanceFailureRequest = New MaintenanceFailureRequest() With {.Status = 1}
    End Function

    ''' <summary>
    ''' carga los controles con la informacion
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
                Using Model As New MMaintenanceFailureRequest(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlyMaintenanceFailureRequest.BeginUpdate()
                    _maintenanceFailureRequest = Await Model.GetMaintenanceFailureRequestByCodeAsync(INDBteCode.Text)
                    If _maintenanceFailureRequest IsNot Nothing AndAlso _maintenanceFailureRequest.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_maintenanceFailureRequest.Id))
                            With _maintenanceFailureRequest
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                INDdteFailureRequestDate.EditValue = .DateFailure
                                INDSleBranchOffice.EditValue = .BranchOfficeId
                                INDSleBranchOffice.Properties.NullText = .BranchOfficeCodeName
                                INDGleTypeRequest.Text = .TypeRequest
                                INDGleReport.Text = .Report
                                NameOther = .NameOther
                                Observation = .Observation
                                BarraBotones.StatusRecord = .Status.ToString()

                                _listMaintenanceFailureRequestDetail = .MaintenanceFailureRequestDetail.ToList()
                                INDGcFailureRequestDetail.DataSource = Nothing
                                INDGcFailureRequestDetail.DataSource = _listMaintenanceFailureRequestDetail
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._maintenanceFailureRequest.Code)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _maintenanceFailureRequest.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If

                            Select Case _maintenanceFailureRequest.Status
                                Case 1
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                Case Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    ReadOnlyControls(True)
                            End Select

                            Me.BarraBotones.SetDocuments(_maintenanceFailureRequest.Id, Me.Tag.ToString(), Nothing, GetType(MaintenanceFailureRequest).Name)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _maintenanceFailureRequest.Id, 0, _maintenanceFailureRequest.Id)

                            ActionsOnControls = True
                            AsyncLoader(False)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewMaintenanceFailureRequest()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            Deshacer()
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlyMaintenanceFailureRequest.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _maintenanceFailureRequest
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .DateFailure = INDdteFailureRequestDate.Text
            .BranchOfficeId = INDSleBranchOffice.EditValue
            .TypeRequest = INDGleTypeRequest.EditValue
            .Report = INDGleReport.EditValue
            .NameOther = NameOther
            .Observation = Observation

            .MaintenanceFailureRequestDetail.Clear()
            If _listMaintenanceFailureRequestDetail IsNot Nothing AndAlso _listMaintenanceFailureRequestDetail.Count > 0 Then
                _listMaintenanceFailureRequestDetail.ForEach(Sub(item)
                                                                 .MaintenanceFailureRequestDetail.Add(item)
                                                             End Sub)
            End If
            If _listDeleteMaintenanceFailureRequestDetail IsNot Nothing Then
                _listDeleteMaintenanceFailureRequestDetail.ForEach(Sub(item)
                                                                       .MaintenanceFailureRequestDetail.Add(item)
                                                                   End Sub)
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        If _maintenanceFailureRequest IsNot Nothing AndAlso _maintenanceFailureRequest.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If

            If _listMaintenanceFailureRequestDetail Is Nothing OrElse _listMaintenanceFailureRequestDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un ítem."
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MMaintenanceFailureRequest(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.ConfirmMaintenanceFailureRequestAsync(_maintenanceFailureRequest, _idCurrentSequence)
                If Result.StatusCode = eStatusResult.SUCCESS Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    AsyncLoader(False)
                    ' Me.FixedAssetTransaction = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If Result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    ElseIf Result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

#Region "Details"

    ''' <summary>
    ''' Agrega el activo o parte afectada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddFixedAsset()
        Dim errors = ValidateControlsFixed()
        If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If _listMaintenanceFailureRequestDetail IsNot Nothing AndAlso _listMaintenanceFailureRequestDetail.Count > 0 Then
            If (From l In _listMaintenanceFailureRequestDetail Where l.PhysicalAssetId = INDSlItem.EditValue Select l).Count > 0 Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "El articulo ya se encuentra en el listado."
                Exit Sub
            End If
        End If

        Dim MaintenanceFailureRequestDetail As New MaintenanceFailureRequestDetail
        With MaintenanceFailureRequestDetail
            .TransactionClass = INDsleTransactionClass.EditValue
            .PhysicalAssetId = INDSlItem.EditValue
            .Plate = PhysicalAssetPlate
            .NameArticle = ItemDescription
            .Description = INDMeDetailDescription.EditValue
            If INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .NameClassPart = "Parte de Activo"
                .PhysicalAssetPartsId = INDslParts.EditValue
                .PartsName = INDslParts.Text
            Else
                .NameClassPart = "Activo"
                .PartsName = String.Empty
            End If
        End With
        If _listMaintenanceFailureRequestDetail Is Nothing Then
            _listMaintenanceFailureRequestDetail = New List(Of MaintenanceFailureRequestDetail)
        End If
        Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        _listMaintenanceFailureRequestDetail.Add(MaintenanceFailureRequestDetail)
        INDGcFailureRequestDetail.DataSource = _listMaintenanceFailureRequestDetail
        INDGcFailureRequestDetail.RefreshDataSource()
        CleanControlsPopup()
    End Sub

    Private Function ValidateControlsFixed() As String
        Dim errors As New Text.StringBuilder
        If INDsleTransactionClass.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una Clase")
        End If
        If INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSlItem.EditValue = Nothing Then
                errors.AppendLine("Debe seleccionar un Activo")
            End If
        End If

        If INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDslParts.EditValue = Nothing Then
                errors.AppendLine("Debe seleccionar una Parte")
            End If
        End If

        If INDGleReport.EditValue = 2 Then
            If INDtxtNameOther.EditValue = Nothing Then
                errors.AppendLine("El campo nombre se encuentra vacío")
            End If
        End If

        Return errors.ToString
    End Function

    ''' <summary>
    ''' Elimina el activo afectado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        Dim Ild As MaintenanceFailureRequestDetail = CType(INDGvFailureRequest.GetFocusedRow, MaintenanceFailureRequestDetail)
        If Ild.Id <> 0 Then
            If _listDeleteMaintenanceFailureRequestDetail Is Nothing Then
                _listDeleteMaintenanceFailureRequestDetail = New List(Of MaintenanceFailureRequestDetail)
            End If
            Ild.MarkAsDeleted()
            _listDeleteMaintenanceFailureRequestDetail.Add(Ild)
        End If
        _listMaintenanceFailureRequestDetail.Remove(Ild)
        INDGcFailureRequestDetail.DataSource = Nothing
        INDGcFailureRequestDetail.DataSource = _listMaintenanceFailureRequestDetail
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMaintenanceFailureRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyMaintenanceFailureRequest, True)
        ''****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        _presenter = New PMaintenanceFailureRequest(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        AddActionsColumns()
        InitializeTuples()
        LoadStatus()
        Deshacer()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _blockRecord = Nothing
        _maintenanceFailureRequest = Nothing
        _listMaintenanceFailureRequestDetail = Nothing
        _listDeleteMaintenanceFailureRequestDetail = Nothing
        _maintenanceFailureRequestDetail = Nothing

        _FillingClass = Nothing
        _FillingTypeRequest = Nothing
        _FillingReport = Nothing
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

    Private Async Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewMaintenanceFailureRequest()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleBranchOffice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBranchOffice.QueryPopUp
        If INDSleBranchOffice.Properties.DataSource Is Nothing Then
            INDSleBranchOffice.Properties.DataSource = _presenter.LoadBranchOffice()
        End If
    End Sub

    Private Sub INDSlItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlItem.QueryPopUp
        If ItemXpo Is Nothing Then
            Using model As New MMaintenanceFailureRequest(MyTag)
                ItemXpo = model.InitializeItem()
            End Using
        End If
    End Sub

    Private Sub INDslParts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDslParts.QueryPopUp
        If PartsXpo Is Nothing Then
            Using model As New MMaintenanceFailureRequest(MyTag)
                PartsXpo = model.InitializeParts()
            End Using
        End If
    End Sub

#End Region

#Region "Click"
    Private Sub INDbtnAddFixedAsset_Click(sender As Object, e As EventArgs) Handles INDbtnAddFixedAsset.Click
        AddFixedAsset()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReport.EditValueChanged
        If (CStr(INDGleReport.EditValue)) = "" Then
            INDGleReport.EditValue = 0
        End If

        If INDGleReport.EditValue = 2 Then
            INDtxtNameOther.EditValue = Nothing
            INDLcNameOther.HideControl(False)
        Else
            INDtxtNameOther.EditValue = Nothing
            INDLcNameOther.HideControl(True)
        End If
    End Sub

    Private Sub INDsleTransactionClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTransactionClass.EditValueChanged
        If INDsleTransactionClass.EditValue = 1 Then
            INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    Private Sub INDSlItem_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlItem.EditValueChanged
        If ItemXpo IsNot Nothing Then
            Dim FixedAssetPhysicalAssetXpo = DirectCast(DirectCast(viewItem.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetFixedAssetPhysicalAssetXpo)
            If FixedAssetPhysicalAssetXpo IsNot Nothing Then
                PhysicalAssetPlate = FixedAssetPhysicalAssetXpo.Plate
                ItemDescription = FixedAssetPhysicalAssetXpo.ItemId.Description
            End If
        End If
    End Sub

    Private Sub INDslParts_EditValueChanged(sender As Object, e As EventArgs) Handles INDslParts.EditValueChanged
        If PartsXpo IsNot Nothing Then
            Dim FixedAssetPhysicalAssetPartsXpo = DirectCast(DirectCast(viewParts.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetPartsXpo)
            If FixedAssetPhysicalAssetPartsXpo IsNot Nothing Then
                INDSlItem.EditValue = FixedAssetPhysicalAssetPartsXpo.PhysicalAssetId.Id
                PhysicalAssetPlate = FixedAssetPhysicalAssetPartsXpo.PhysicalAssetId.Plate
                ItemDescription = FixedAssetPhysicalAssetPartsXpo.PhysicalAssetId.ItemId.Description
            End If
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        RemoveDetail()
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveDetail()
        End Select
    End Sub

#End Region

#Region "IdEntityLoaded"

    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._maintenanceFailureRequest IsNot Nothing AndAlso Me._maintenanceFailureRequest.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
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
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _maintenanceFailureRequest.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _maintenanceFailureRequest.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _maintenanceFailureRequest.Status = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _maintenanceFailureRequest.Status = 2
            Confirmar()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _maintenanceFailureRequest.Status = 2
            Confirmar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.None, _maintenanceFailureRequest.Id, 0, _maintenanceFailureRequest.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub INDpceAddFixedAsset_EditValueChanged(sender As Object, e As EventArgs) Handles INDpceAddFixedAsset.EditValueChanged

    End Sub

#End Region

End Class
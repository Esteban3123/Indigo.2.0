'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Juan Carlos Bermudez
' Created          : 01-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmTransferOrderDevolution
    Implements ITransferOrderDevolution, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

#End Region

#Region "Globals"

    ''' <summary>
    ''' presenter de la devolucion de orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PTransferOrderDevolution

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
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Dim _blockRecord As BlockRecordInventory

    ''' <summary>
    ''' entidad de devolucion de orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private _transferOrderDevolution As TransferOrderDevolution

    ''' <summary>
    ''' listado del subdetalle de las ordenes de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private _listTransferOrderDetailBatchSerial As List(Of TransferOrderDetailBatchSerial)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim _indexEditRecord As Integer

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

    ''' <summary>
    ''' Variable que permite identificar si se esta cargando un registro
    ''' </summary>
    ''' <remarks></remarks>
    Dim _isLoading As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ITransferOrderDevolution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor MyLayoutControl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ITransferOrderDevolution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' esta propiedad establece el valor controlAcciones
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ITransferOrderDevolution.ActionsOnControls
        Set(value As Boolean)
            INDlcTransferOrderDevolution.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDsleTransferOrderId.Enabled = value
            INDmeDescription.Enabled = value
            INDGcProducts.Enabled = value

            BarraBotones.StatusRecordVisible = value

            INDlcTransferOrderDevolution.EndUpdate()
            If value Then
                INDdeDocumentDate.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    Public Property Sequense As InventorySequence Implements ITransferOrderDevolution.Sequense
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
    ''' obtiene o establece el codigo de la devolucion de orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements ITransferOrderDevolution.Code
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
    ''' Obtiene o establece la fecha de la devolucion de orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements ITransferOrderDevolution.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransferOrderId As Integer Implements ITransferOrderDevolution.TransferOrderId
        Get
            Return INDsleTransferOrderId.EditValue
        End Get
        Set(value As Integer)
            INDsleTransferOrderId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion de devolucion de orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements ITransferOrderDevolution.Description
        Get
            Return INDmeDescription.EditValue
        End Get
        Set(value As String)
            INDmeDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la devolucion de la orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements ITransferOrderDevolution.Status
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
    ''' Obtiene y asigna el objeto de tipo xpo de ordenes de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransferOrderXpo As XPInstantFeedbackSource Implements ITransferOrderDevolution.TransferOrderXpo
        Get
            Return INDsleTransferOrderId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTransferOrderId.Properties.DataSource = value
        End Set
    End Property

    Private _FillingOrderType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingOrderType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingOrderType Is Nothing Then
                _FillingOrderType = New List(Of Tuple(Of Integer, String))
                _FillingOrderType.Add(New Tuple(Of Integer, String)(1, "Traslado"))
                _FillingOrderType.Add(New Tuple(Of Integer, String)(2, "Consumo"))
                _FillingOrderType.Add(New Tuple(Of Integer, String)(3, "Traslado en Transito"))
            End If
            Return _FillingOrderType
        End Get
    End Property

    Private _FillingDispatchTo As List(Of Tuple(Of Byte?, String))
    Private ReadOnly Property FillingDispatchTo As List(Of Tuple(Of Byte?, String))
        Get
            If _FillingDispatchTo Is Nothing Then
                _FillingDispatchTo = New List(Of Tuple(Of Byte?, String))
                _FillingDispatchTo.Add(New Tuple(Of Byte?, String)(1, "Almacen"))
                _FillingDispatchTo.Add(New Tuple(Of Byte?, String)(2, "Unidad Funcional"))
            End If
            Return _FillingDispatchTo
        End Get
    End Property

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Orden Traslado", .FieldName = "TransferOrderId.Code", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListTransferOrderDevolution
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

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewTransferOrderDevolution()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If _transferOrderDevolution IsNot Nothing AndAlso _transferOrderDevolution.Status < 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If

            Dim errors As New StringBuilder
            Dim listDetailTmp = _listTransferOrderDetailBatchSerial.FindAll(Function(x) x.QuantityDeliver > 0)
            If listDetailTmp.Count = 0 Then
                errors.AppendLine(ResourceManager.GetString("QuantityReturn", MODULE_NAME))
            End If
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If
        End If

        Try
            AssigningValues()
            Using model As New MTransferOrderDevolution(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveTransferOrderDevolution(_transferOrderDevolution)
                AsyncLoader(False)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    If Not String.IsNullOrEmpty(result.MessageAux) Then
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageAux
                    End If

                    _transferOrderDevolution = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _transferOrderDevolution.Id, 0, _transferOrderDevolution.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _transferOrderDevolution.Id, 0, _transferOrderDevolution.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _transferOrderDevolution.Id, 0, _transferOrderDevolution.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _transferOrderDevolution.Id, 0, _transferOrderDevolution.Id)
                    End Select
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    If _transferOrderDevolution.Id > 0 Then
                        _transferOrderDevolution = Await model.GetTransferOrderDevolutionByCode(_transferOrderDevolution.Code)
                    Else
                        _transferOrderDevolution = New TransferOrderDevolution
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvProducts, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        INDsleOrderType.Properties.DataSource = FillingOrderType.ToList
        INDsleDispatchTo.Properties.DataSource = FillingDispatchTo.ToList
    End Sub

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
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
        INDdeDocumentDate.Properties.MinValue = dateMin
        INDdeDocumentDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlcTransferOrderDevolution.BeginUpdate()
        Await DeleteBlockedRecord()

        Code = String.Empty
        DocumentDate = GetDateServer()
        TransferOrderId = Nothing
        INDsleTransferOrderId.Properties.NullText = String.Empty
        Description = String.Empty
        Status = 1

        INDdeDocumentDateTransfer.EditValue = GetDateServer()
        INDsleOrderType.EditValue = Nothing
        INDsleDispatchTo.EditValue = Nothing
        INDsleSourceWarehouseId.EditValue = Nothing
        INDsleSourceWarehouseId.Properties.NullText = String.Empty
        INDsleTargetWarehouseId.EditValue = Nothing
        INDsleTargetWarehouseId.Properties.NullText = String.Empty
        INDsleTargetFunctionalUnitId.EditValue = Nothing
        INDsleTargetFunctionalUnitId.Properties.NullText = String.Empty
        INDsleAdjustmentConceptId.EditValue = Nothing
        INDsleAdjustmentConceptId.Properties.NullText = String.Empty
        INDsleThirdPartyId.EditValue = Nothing
        INDsleThirdPartyId.Properties.NullText = String.Empty

        INDGcProducts.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcProducts)

        _doc = Nothing
        _transferOrderDevolution = Nothing
        _listTransferOrderDetailBatchSerial = Nothing
        _isLoading = False

        Me.ValidateDate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        INDBtnAll.Enabled = False
        INDBtnNothing.Enabled = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        INDlcTransferOrderDevolution.EndUpdate()

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
            Dim result = Await model.GetBlockRecord(Me.Tag, Me._transferOrderDevolution.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _blockRecord = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me._transferOrderDevolution.Id}
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
    ''' Método que elimina el registro guardado para concurrencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Await model.DeleteBlockRecord(_blockRecord)
            End Using
            _blockRecord = Nothing
        End If
    End Function

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._transferOrderDevolution.Code, Me._transferOrderDevolution.OperatingUnitId, Me._transferOrderDevolution.DocumentDate, Me._transferOrderDevolution.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._transferOrderDevolution.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._transferOrderDevolution.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._transferOrderDevolution.Code, Me._transferOrderDevolution.OperatingUnitId, Me._transferOrderDevolution.DocumentDate, Me._transferOrderDevolution.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._transferOrderDevolution.Code)
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
    Private Async Function NewTransferOrderDevolution() As Task
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
        _transferOrderDevolution = New TransferOrderDevolution() With {.Status = 1}
    End Function

    ''' <summary>
    ''' Método que carga los controles de la devolucion de orden de traslado
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
                Using Model As New MTransferOrderDevolution(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlcTransferOrderDevolution.BeginUpdate()
                    _transferOrderDevolution = Await Model.GetTransferOrderDevolutionByCode(INDBteCode.Text.Trim)
                    If _transferOrderDevolution IsNot Nothing AndAlso _transferOrderDevolution.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_transferOrderDevolution.Id))
                            With _transferOrderDevolution
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                If .Status <> 1 Then
                                    INDdeDocumentDate.Properties.MinValue = .DocumentDate
                                End If

                                _isLoading = True
                                Code = .Code
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                DocumentDate = .DocumentDate
                                TransferOrderId = .TransferOrderId
                                INDsleTransferOrderId.Properties.NullText = .CodeTransferOrder
                                Description = .Description
                                Status = .Status.ToString()
                                _isLoading = False
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._transferOrderDevolution.Code)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _transferOrderDevolution.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If

                            Select Case _transferOrderDevolution.Status
                                Case 1
                                    INDBtnAll.Enabled = True
                                    INDBtnNothing.Enabled = True

                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                Case Else
                                    ReadOnlyControls(True)
                                    INDBtnAll.Enabled = False
                                    INDBtnNothing.Enabled = False

                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                            End Select

                            INDdeDocumentDate.Focus()
                            Me.BarraBotones.SetDocuments(_transferOrderDevolution.Id, Me.Tag.ToString(), Nothing, GetType(TransferOrderDevolution).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _transferOrderDevolution.Id, 0, _transferOrderDevolution.Id)

                            ActionsOnControls = True
                            AsyncLoader(False)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewTransferOrderDevolution()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlcTransferOrderDevolution.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' metodo para asignar los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _transferOrderDevolution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .DocumentDate = DocumentDate
            .TransferOrderId = TransferOrderId
            .Description = Description

            If .Id = 0 Then
                For Each item In _listTransferOrderDetailBatchSerial.FindAll(Function(x) x.QuantityDeliver > 0)
                    Dim transferOrderDevolutionDetail As New TransferOrderDevolutionDetail
                    transferOrderDevolutionDetail.Quantity = item.QuantityDeliver
                    transferOrderDevolutionDetail.TransferOrderDetailBatchSerialId = item.Id
                    .TransferOrderDevolutionDetail.Add(transferOrderDevolutionDetail)
                Next
            Else
                For Each item In _listTransferOrderDetailBatchSerial
                    Dim detail = .TransferOrderDevolutionDetail.FirstOrDefault(Function(x) x.TransferOrderDetailBatchSerialId = item.Id)
                    If detail IsNot Nothing Then
                        If item.QuantityDeliver > 0 Then
                            detail.Quantity = item.QuantityDeliver
                        Else
                            detail.ChangeTracker.State = ObjectState.Deleted
                        End If
                    Else
                        If item.QuantityDeliver > 0 Then
                            Dim transferOrderDevolutionDetail As New TransferOrderDevolutionDetail
                            transferOrderDevolutionDetail.Quantity = item.QuantityDeliver
                            transferOrderDevolutionDetail.TransferOrderDetailBatchSerialId = item.Id
                            .TransferOrderDevolutionDetail.Add(transferOrderDevolutionDetail)
                        End If
                    End If
                Next
            End If
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmTransferOrderDevolution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcTransferOrderDevolution, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PTransferOrderDevolution(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDGcProducts)
        Await Me.LoadParameters()

        AddActionsColumns()
        InitializeTuples()
        Deshacer()
        LoadStatus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _sequence = Nothing
        _settingsInventory = Nothing
        _blockRecord = Nothing
        _transferOrderDevolution = Nothing
        _listTransferOrderDetailBatchSerial = Nothing
        _indexEditRecord = Nothing
        _varImp = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmTransferOrderDevolution_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmTransferOrderDevolution_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control para cargar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
                    Await Me.NewTransferOrderDevolution()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QUeryPopUp"

    ''' <summary>
    ''' Carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTransferOrderId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleTransferOrderId.QueryPopUp
        If TransferOrderXpo Is Nothing Then
            _presenter.InitializeTransferOrder()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTransferOrderId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTransferOrderId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmStores With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeTransferOrder()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se dispara al cambiar el valor de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleTransferOrderId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTransferOrderId.EditValueChanged
        Dim transferOrder As New TransferOrder

        If INDsleTransferOrderId.EditValue IsNot Nothing AndAlso INDsleTransferOrderId.EditValue > 0 Then
            Using model As New MTransferOrderDevolution(MyTag)
                Using modelTransferOrder As New MTransferOrder(MyTag)
                    _listTransferOrderDetailBatchSerial = Await model.ListTransferOrderDetailBatchSerialByTransferOrderId(TransferOrderId, Not _isLoading)
                    If _transferOrderDevolution IsNot Nothing AndAlso _transferOrderDevolution.TransferOrderDevolutionDetail IsNot Nothing Then
                        For Each item In _transferOrderDevolution.TransferOrderDevolutionDetail
                            Dim outputDetail = _listTransferOrderDetailBatchSerial.FirstOrDefault(Function(x) x.Id = item.TransferOrderDetailBatchSerialId)
                            If outputDetail IsNot Nothing Then
                                outputDetail.QuantityDeliver = item.Quantity
                            End If
                        Next
                    End If

                    INDBtnAll.Enabled = Not _isLoading
                    INDBtnNothing.Enabled = Not _isLoading
                    INDGcProducts.DataSource = Nothing
                    INDGcProducts.DataSource = _listTransferOrderDetailBatchSerial

                    transferOrder = modelTransferOrder.GetTransferOrderById(TransferOrderId)
                    _transferOrderDevolution.Prefix = transferOrder.Prefix
                    INDdeDocumentDateTransfer.EditValue = transferOrder.DocumentDate
                    INDdeDocumentDateTransfer.Properties.ReadOnly = True
                    INDsleOrderType.EditValue = transferOrder.OrderType
                    INDsleOrderType.Properties.ReadOnly = True
                    INDsleSourceWarehouseId.EditValue = transferOrder.SourceWarehouseId
                    INDsleSourceWarehouseId.Properties.NullText = transferOrder.DescriptionSourceWarehouse
                    INDsleSourceWarehouseId.Properties.ReadOnly = True
                    INDsleDispatchTo.EditValue = transferOrder.DispatchTo
                    INDsleDispatchTo.Properties.ReadOnly = True
                    INDsleTargetWarehouseId.EditValue = transferOrder.TargetWarehouseId
                    INDsleTargetWarehouseId.Properties.NullText = transferOrder.DescriptionTargetWarehouse
                    INDsleTargetWarehouseId.Properties.ReadOnly = True
                    INDsleTargetFunctionalUnitId.EditValue = transferOrder.TargetFunctionalUnitId
                    INDsleTargetFunctionalUnitId.Properties.NullText = transferOrder.DescriptionTargetFuntionalUnit
                    INDsleTargetFunctionalUnitId.Properties.ReadOnly = True
                    INDsleAdjustmentConceptId.EditValue = transferOrder.AdjustmentConceptId
                    INDsleAdjustmentConceptId.Properties.NullText = transferOrder.DescriptionAdjustmentConcept
                    INDsleAdjustmentConceptId.Properties.ReadOnly = True
                    INDsleThirdPartyId.EditValue = transferOrder.ThirdPartyId
                    INDsleThirdPartyId.Properties.NullText = transferOrder.DescirptionThirdParty
                    INDsleThirdPartyId.Properties.ReadOnly = True
                End Using
            End Using
        End If

        INDlciDispatchTo.HideControl(Not (transferOrder.OrderType = 2))
        INDlciTargetWarehouseId.HideControl(Not (transferOrder.DispatchTo = 1))
        INDlciTargetFunctionalUnitId.HideControl(Not (transferOrder.DispatchTo = 2))
        INDlciAdjustmentConceptId.HideControl(transferOrder.AdjustmentConceptId Is Nothing)
        INDlciThirdPartyId.HideControl(transferOrder.ThirdPartyId Is Nothing)
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' se dispara al cambiar el valor de cantidad a devolver de la rejilla de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDriseQuantityDeliver_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDriseQuantityDeliver.EditValueChanging
        Dim value As Integer
        Try
            value = Convert.ToInt32(e.NewValue)
        Catch ex As Exception
            Exit Sub
        End Try
        Dim outputDetail = DirectCast(INDGvProducts.GetFocusedRow, TransferOrderDetailBatchSerial)
        If outputDetail.OutstandingQuantity < value Then
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("MaxQuantity", MODULE_NAME), outputDetail.OutstandingQuantity.ToString())
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAll_Click(sender As Object, e As EventArgs) Handles INDBtnAll.Click
        Me.Cursor = ChangeCursorIndigo()
        For Each item In INDGvProducts.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of TransferOrderDetailBatchSerial)()
            item.QuantityDeliver = item.OutstandingQuantity
        Next
        Me.Cursor = ChageCursorDefault()
        INDGcProducts.RefreshDataSource()
    End Sub

    Private Sub INDBtnNothing_Click(sender As Object, e As EventArgs) Handles INDBtnNothing.Click
        Me.Cursor = ChangeCursorIndigo()
        For Each item In INDGvProducts.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of TransferOrderDetailBatchSerial)()
            item.QuantityDeliver = 0
        Next
        Me.Cursor = ChageCursorDefault()
        INDGcProducts.RefreshDataSource()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._transferOrderDevolution IsNot Nothing AndAlso Me._transferOrderDevolution.Id > 0 Then
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

#Region "Bar Buttons"

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
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _transferOrderDevolution.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _transferOrderDevolution.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _transferOrderDevolution.Status = 3
            _varImp = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _transferOrderDevolution.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _transferOrderDevolution.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _transferOrderDevolution.Id, 0, _transferOrderDevolution.Id)
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
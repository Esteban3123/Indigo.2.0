'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Diego A. Roldán Lozano
' Created          : 2021-09-08
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

Public Class FrmRawMaterialDevolution
    Implements IRawMaterialDevolution

#Region "Properties"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As MixingStationSequence

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordMixingStation

    ''' <summary>
    ''' Entity
    ''' </summary>
    Private _rawMaterialDevolution As RawMaterialDevolution

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Long

    ''' <summary>
    ''' presenter
    ''' </summary>
    Private _presenter As PRawMaterialDevolution

    ''' <summary>
    ''' bandera que indica cuando se están cargando los controles
    ''' </summary>
    Private _isLoading As Boolean = False

    ''' <summary>
    ''' Indica cuando se carga el formulario
    ''' </summary>
    Public Event LoadEndForm()

    ''' <summary>
    ''' Evento de acción completada
    ''' </summary>
    Public Event ActionCompleted()

    ''' <summary>
    ''' Layout control
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRawMaterialDevolution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' enable or disable controls
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRawMaterialDevolution.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()

            INDBeCode.Enabled = Not value
            INDDeDate.Enabled = value
            INDSleCampaign.Enabled = value
            INDSleProductionWarehouse.Enabled = value
            INDSleStockWarehouse.Enabled = value
            INDMeDetail.Enabled = value
            INDGcProducts.Enabled = value
            INDLcRoot.EndUpdate()

            If value Then
                INDDeDate.Focus()
            Else
                INDBeCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' get tag form
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IRawMaterialDevolution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Code
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IRawMaterialDevolution.Code
        Get
            If INDBeCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBeCode.Text.Trim()
            End If
        End Get
        Set(value As String)
            INDBeCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Mixing station sequence
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As MixingStationSequence Implements IRawMaterialDevolution.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As MixingStationSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As MixingStationSequenceDetail In Me._sequence.MixingStationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Message
    ''' </summary>
    ''' <param name="Icono"></param>
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

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As Date Implements IRawMaterialDevolution.DocumentDate
        Get
            Return INDDeDate.EditValue
        End Get
        Set(value As Date)
            INDDeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la campaña
    ''' </summary>
    ''' <returns></returns>
    Public Property CampaignDetailId As Integer? Implements IRawMaterialDevolution.CampaignDetailId
        Get
            Return INDSleCampaign.EditValue
        End Get
        Set(value As Integer?)
            INDSleCampaign.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' almacen de produccion
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductionWarehouseId As Integer? Implements IRawMaterialDevolution.ProductionWarehouseId
        Get
            Return INDSleProductionWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleProductionWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' almacen de stock
    ''' </summary>
    ''' <returns></returns>
    Public Property StockWarehouseId As Integer? Implements IRawMaterialDevolution.StockWarehouseId
        Get
            Return INDSleStockWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleStockWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' detalle
    ''' </summary>
    ''' <returns></returns>
    Public Property Detail As String Implements IRawMaterialDevolution.Detail
        Get
            Return INDMeDetail.Text
        End Get
        Set(value As String)
            INDMeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Estado
    ''' </summary>
    Public Property Status As Byte Implements IRawMaterialDevolution.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Detail of raw material devolution
    ''' </summary>
    ''' <returns></returns>
    Public Property RawMaterialDevolutionDetails As List(Of RawMaterialDevolutionDetail) Implements IRawMaterialDevolution.RawMaterialDevolutionDetails
        Get
            Return INDGcProducts.DataSource
        End Get
        Set(value As List(Of RawMaterialDevolutionDetail))
            INDGcProducts.DataSource = value
            INDGcProducts.RefreshDataSource()
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga una devolución de materia prima desde un modal
    ''' </summary>
    Public Async Function LoadRawMaterialDevolutionModal(campaignDetailId As Integer, campaignDetailName As String) As Task
        _isLoading = True
        AsyncLoader(True)
        Await Me.NewRawMaterialDevolution()
        Dim campaignDetail As CampaignDetailXpo = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService _
                                            .GetCollection(Of CampaignDetailXpo)(Function(m) m.Id = campaignDetailId).FirstOrDefault())

        Me.CampaignDetailId = campaignDetailId
        INDSleCampaign.Properties.NullText = campaignDetail.FullTitle
        INDSleCampaign.ReadOnly = True

        If Not String.IsNullOrEmpty(INDSleCampaign.EditValue) Then
            LoadRawMaterialDevolutionDetailsByCampaignDetailId(INDSleCampaign.EditValue)
        Else
            RawMaterialDevolutionDetails = Nothing
        End If
        Await LoadWareHouses(campaignDetail)

        AsyncLoader(False)
        INDBeCode.Enabled = False
        _isLoading = False
    End Function
    ''' <summary>
    ''' Limpia todos los controles
    ''' </summary>
    Public Sub CleanControls() Implements IRawMaterialDevolution.CleanControls
        INDLcRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Status = 1
        Code = String.Empty
        DocumentDate = GetDateServer()
        CampaignDetailId = Nothing
        ProductionWarehouseId = Nothing
        StockWarehouseId = Nothing
        Detail = Nothing

        INDSleCampaign.Properties.ReadOnly = False
        INDSleCampaign.Properties.NullText = String.Empty
        _rawMaterialDevolution = Nothing
        RawMaterialDevolutionDetails = Nothing
        INDColDevolutionQuantity.OptionsColumn.AllowEdit = True
        INDColDevolutionQuantity.OptionsColumn.AllowFocus = True
        ReadOnlyControls(False)

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Buscar
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then Return
        AssigningValues()

        Try
            Using model As New MRawMaterialDevolution(Tag)
                AsyncLoader(True)
                Dim result As ActionResult(Of RawMaterialDevolution) = Await model.SaveRawMaterialDevolutionAsync(_rawMaterialDevolution, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _rawMaterialDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not _sequence.IsManual AndAlso Not _sequence.Sequential Then
                            DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If
                    End If

                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                    RaiseEvent ActionCompleted()
                Else
                    AsyncLoader(False)
                    INDBeCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Anula un documento
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If ValidateControls() = False Then Return
        AssigningValues()

        Try
            Using model As New MRawMaterialDevolution(Tag)
                AsyncLoader(True)
                Dim result As ActionResult(Of RawMaterialDevolution) = Await model.AnnulateRawMaterialDevolutionAsync(_rawMaterialDevolution.Code)

                If result.StateResult Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                    RaiseEvent ActionCompleted()
                Else
                    AsyncLoader(False)
                    INDBeCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Guarda y confirma el documento
    ''' </summary>
    Private Async Sub SaveAndConfirm()
        If ValidateControls() = False Then Return
        AssigningValues()

        Try
            Using model As New MRawMaterialDevolution(Tag)
                AsyncLoader(True)
                Dim result As ActionResult(Of RawMaterialDevolution) = Await model.SaveAndConfirmRawMaterialDevolutionAsync(_rawMaterialDevolution, _idOperativeUnit, Me._idCurrentSequence)

                If result.StateResult Then
                    If _rawMaterialDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not _sequence.IsManual AndAlso Not _sequence.Sequential Then
                            DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If
                    End If

                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                    RaiseEvent ActionCompleted()
                Else
                    AsyncLoader(False)
                    INDBeCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Nuevo
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If

        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewRawMaterialDevolution()
        End If
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Abre el formulario de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                New ColumnInfo() With {
                    .Caption = "Código",
                    .FieldName = "Code",
                    .ColumnWidth = CInt(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)
                },
                New ColumnInfo() With {
                    .Caption = "Fecha",
                    .FieldName = "DocumentDate",
                    .ColumnWidth = CInt(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.28)
                },
                New ColumnInfo() With {
                    .Caption = "Campaña",
                    .FieldName = "CampaignDetail.FullTitle",
                    .ColumnWidth = CInt(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)
                },
                New ColumnInfo() With {
                    .Caption = "Estado",
                    .FieldName = "StatusName",
                    .ColumnWidth = CInt(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)
                }
            }.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListRawMaterialDevolution
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorna el valor seleccionado del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()

            If Not INDBeCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If

            INDBeCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' NA
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Inicia el formulario
    ''' </summary>
    Public Async Function InitForm() As Task
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        _presenter = New PRawMaterialDevolution(Me)

        IndigoGridControl1.RefreshGrid(INDGcProducts)
        'IndigoGridView1.SetListAcction(INDGvProducts, {eAcciones.Remove}.ToList())
        LoadStatus()
        Deshacer()
        Await _presenter.GetSequence()
        RaiseEvent LoadEndForm()
    End Function

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._rawMaterialDevolution.Code, Me._rawMaterialDevolution.DocumentDate.ToString("yyyyMMdd")),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._rawMaterialDevolution.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._rawMaterialDevolution.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._rawMaterialDevolution.Code, Me._rawMaterialDevolution.DocumentDate.ToString("yyyyMMdd"))
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._rawMaterialDevolution.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Function LoadControls() As Task Implements IRawMaterialDevolution.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using model As New MRawMaterialDevolution(Tag)
                    AsyncLoader(True)
                    Dim result = Await model.GetRawMaterialDevolutionByCode(Code)
                    _rawMaterialDevolution = result.ObjectEmbbeded
                    INDLcRoot.BeginUpdate()

                    If _rawMaterialDevolution IsNot Nothing AndAlso _rawMaterialDevolution.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using modelRecord As New MBlockRecordAndSequenceMixingStation(Tag)
                            _record = Await modelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_rawMaterialDevolution.Id))

                            _isLoading = True
                            With _rawMaterialDevolution
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                CampaignDetailId = .CampaignDetailId
                                Detail = .Detail
                                Status = .State

                                INDSleCampaign.Properties.NullText = .CampaignDetailName
                                INDSleProductionWarehouse.Properties.NullText = .ProductionWarehouseCodeName
                                INDSleStockWarehouse.Properties.NullText = .StockWarehouseCodeName
                                'RawMaterialDevolutionDetails = .RawMaterialDevolutionDetail.ToList()
                                LoadRawMaterialDevolutionDetails(.Id)
                            End With

                            INDSleCampaign.Properties.ReadOnly = True
                            Me.GetDocumentIndexed($"{Tag}_{_rawMaterialDevolution.Code}")

                            _isLoading = False
                            If _record.Id = 0 Then
                                _record = (Await modelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {
                                        .BlockDate = Date.Now,
                                        .ChangeTracker = New ObjectChangeTracker With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName,
                                        .IdForm = Me.Tag,
                                        .CodUser = Me.indigo.UserIndigo,
                                        .IdRecord = _rawMaterialDevolution.Id
                                    })
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(_rawMaterialDevolution.Id, Me.Tag.ToString(), Nothing, GetType(RawMaterialDevolution).Name)

                            Me.BarraBotones.StatusRecord = _rawMaterialDevolution.State.ToString()
                            Select Case _rawMaterialDevolution.State
                                Case 1
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                Case 2
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAnnular)
                                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                                    ReadOnlyControls(True)
                                    INDColDevolutionQuantity.OptionsColumn.AllowEdit = False
                                    INDColDevolutionQuantity.OptionsColumn.AllowFocus = False
                                Case Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                                    ReadOnlyControls(True)
                                    INDColDevolutionQuantity.OptionsColumn.AllowEdit = False
                                    INDColDevolutionQuantity.OptionsColumn.AllowFocus = False
                            End Select

                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewRawMaterialDevolution()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = "El Código de la devolución de materia prima no existe"
                            Code = String.Empty
                            INDBeCode.Focus()
                        End If
                    End If
                    INDLcRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBeCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Carga los detalles de la devolucion
    ''' </summary>
    ''' <param name="rawMaterialDevolutionId"></param>
    Private Sub LoadRawMaterialDevolutionDetails(rawMaterialDevolutionId As Integer)
        INDGvProducts.ShowLoadingPanel()
        Task.Factory.StartNew(Async Function()
                                  Using model As New MRawMaterialDevolution(Tag)
                                      Dim details = Await model.GetRawMaterialDevolutionDetailByRawMaterialDevolutionIdAsync(rawMaterialDevolutionId)

                                      SafeInvoke(Sub()
                                                     details.ForEach(Sub(item) _rawMaterialDevolution.RawMaterialDevolutionDetail.Add(item))
                                                     RawMaterialDevolutionDetails = details
                                                     INDGvProducts.HideLoadingPanel()
                                                 End Sub)
                                  End Using
                              End Function)
    End Sub

    ''' <summary>
    ''' asigna los valores a guardar
    ''' </summary>
    Public Sub AssigningValues() Implements IRawMaterialDevolution.AssigningValues
        With _rawMaterialDevolution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .CampaignDetailId = CampaignDetailId
            .Detail = Detail
        End With
    End Sub

    ''' <summary>
    ''' New entity
    ''' </summary>
    ''' <returns></returns>
    Private Async Function NewRawMaterialDevolution() As Task
        _rawMaterialDevolution = New RawMaterialDevolution With {.State = 1}
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Function
        End If
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequence))
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

        Status = 1
        BarraBotones.StatusRecordVisible = True
    End Function

    ''' <summary>
    ''' Carga los estados del formulario
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverced"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Carga los almacenes
    ''' </summary>
    Private Async Function LoadWareHouses(campaignDetail As CampaignDetailXpo) As Task
        If INDSleCampaign.EditValue Is Nothing Then
            INDSleStockWarehouse.Properties.NullText = String.Empty
            INDSleProductionWarehouse.Properties.NullText = String.Empty
        Else
            If campaignDetail IsNot Nothing Then
                AsyncLoader(True)
                Await Task.Factory.StartNew(Sub()
                                                Using model As New MCampaign(Tag)
                                                    Dim stockXpo = model.ListViewMixingStationWarehouse(campaignDetail.CampaignId.CMConfigurationId, eWarehouseMSType.MateriaPrimaStock)
                                                    Dim warenhouseXpo = model.ListViewMixingStationWarehouse(campaignDetail.CampaignId.CMConfigurationId, eWarehouseMSType.EnProceso)

                                                    SafeInvoke(Sub()
                                                                   INDSleStockWarehouse.Properties.NullText = stockXpo.CodeName
                                                                   INDSleProductionWarehouse.Properties.NullText = warenhouseXpo.CodeName

                                                                   _rawMaterialDevolution.ProductionWarehouseId = warenhouseXpo.Id_Warehouse
                                                                   _rawMaterialDevolution.StockWarehouseId = stockXpo.Id_Warehouse
                                                                   AsyncLoader(False)
                                                               End Sub)
                                                End Using
                                            End Sub)
            End If
        End If
    End Function

    ''' <summary>
    ''' Carga los detalles por campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    Private Async Sub LoadRawMaterialDevolutionDetailsByCampaignDetailId(campaignDetailId As Integer?)
        If campaignDetailId.HasValue Then
            INDGvProducts.ShowLoadingPanel()
            Using model As New MCampaign(Tag)
                Dim validations = Await model.GetCampaignDetailValidationByCampaignDetailId(campaignDetailId)

                If validations IsNot Nothing AndAlso validations.StateResult _
                    AndAlso validations.ObjectEmbbeded IsNot Nothing AndAlso validations.ObjectEmbbeded.Any() Then

                    Dim devolution = validations.ObjectEmbbeded.Select(Function(m) New RawMaterialDevolutionDetail With {
                        .RawMaterialDevolutionId = _rawMaterialDevolution.Id,
                        .CampaignDetailValidationId = m.Id,
                        .ProductId = m.ProductId,
                        .ProductCodeName = m.ProductFullName,
                        .BatchSerialId = m.BatchSerialId,
                        .BatchSerialCode = m.BatchSerialCode,
                        .DeliveredQuantity = m.DeliveredQuantity,
                        .DevolutionQuantity = m.DevolutionQuantity,
                        .Quantity = 0
                    }).ToList()

                    devolution.ForEach(Sub(m) _rawMaterialDevolution.RawMaterialDevolutionDetail.Add(m))
                    RawMaterialDevolutionDetails = devolution
                Else
                    RawMaterialDevolutionDetails = Nothing
                End If

                INDGvProducts.HideLoadingPanel()
            End Using
        End If
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Disposed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _record = Nothing
        _idOperativeUnit = Nothing
        _presenter = Nothing
        _rawMaterialDevolution = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
    End Sub

    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmRawMaterialDevolution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await InitForm()
    End Sub

    ''' <summary>
    ''' Keydown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBeCode_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDBeCode.KeyDown
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
                    Await Me.NewRawMaterialDevolution()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Activated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRawMaterialDevolution_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBeCode.Enabled Then
            INDBeCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Load datasource of campaign
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCampaign_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCampaign.QueryPopUp
        If INDSleCampaign.Properties.DataSource Is Nothing AndAlso Not INDSleCampaign.ReadOnly Then
            INDSleCampaign.Properties.DataSource = _presenter.ListAllCampaignsXPOByStatus({5})
        End If
    End Sub

    ''' <summary>
    ''' Canged campaign
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCampaign_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCampaign.EditValueChanged
        If Not _isLoading Then
            Dim campaignDetail = INDSleCampaign.GetFocusedObject(Of CampaignDetailXpo)

            If Not String.IsNullOrEmpty(INDSleCampaign.EditValue) Then
                LoadRawMaterialDevolutionDetailsByCampaignDetailId(INDSleCampaign.EditValue)
            Else
                RawMaterialDevolutionDetails = Nothing
            End If
            LoadWareHouses(campaignDetail)
        End If
    End Sub

    ''' <summary>
    ''' Validamos que las cantidades no sobrepasen a las máximas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptSpnDevolutionQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSpnDevolutionQuantity.EditValueChanging
        Dim rawMaterialDevolutionDetail = INDGvProducts.GetFocusedObject(Of RawMaterialDevolutionDetail)

        If rawMaterialDevolutionDetail IsNot Nothing _
            AndAlso (CInt(e.NewValue) + rawMaterialDevolutionDetail.DevolutionQuantity > rawMaterialDevolutionDetail.DeliveredQuantity OrElse CInt(e.NewValue) < 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Cantidad a devolver no permitida"
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "BarButton"
    ''' <summary>
    ''' Nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBeCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Guarda y confirma
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar, BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.SaveAndConfirm()
        End If
    End Sub

    ''' <summary>
    ''' Anula el documento
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Eliminar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MixingStationSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) 

    End Sub
#End Region

End Class
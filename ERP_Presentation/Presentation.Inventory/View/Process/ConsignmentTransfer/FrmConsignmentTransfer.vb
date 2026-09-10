'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego A. Roldán L.
' Created          : 2023-03-24
'
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Threading
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraSpreadsheet
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports System.Windows.Forms

Public Class FrmConsignmentTransfer
    Implements IConsignmentTransfer, IValidateDetails

    Public Sub New()
        InitializeComponent()
    End Sub

#Region "Properties"
    ''' <summary>
    ''' Request param prodict
    ''' </summary>
    Private _consignmentTransferProduct As ConsignmentTransferDetail = Nothing

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As InventorySequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Long

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInventory

    ''' <summary>
    ''' Representa el presentador de rangos uvr
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PConsignmentTransfer

    ''' <summary>
    ''' Xpo del almacén de origen seleccionado
    ''' </summary>
    Private _sourceWarehouse As WarehouseXpo = Nothing

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' representa la entidad de parámetros de solicitud
    ''' </summary>
    ''' <remarks></remarks>
    Private _consignmentTransfer As ConsignmentTransfer

    ''' <summary>
    ''' Estado del registro
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Byte Implements IConsignmentTransfer.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Layout principal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IConsignmentTransfer.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Habilita o deshabilita controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConsignmentTransfer.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            INDBeCode.Enabled = Not value
            INDDeDocumentDate.Enabled = value
            INDMeDescription.Enabled = value
            INDSleSourceWarehouse.Enabled = value
            INDSbAdd.Enabled = value
            INDsleCurrency.Enabled = value
            INDGcProducts.Enabled = value
            INDLcRoot.EndUpdate()

            If value Then
                INDDeDocumentDate.Focus()
            Else
                INDBeCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As String Implements IConsignmentTransfer.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IConsignmentTransfer.Code
        Get
            If (INDBeCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBeCode.Text
            End If
        End Get
        Set(value As String)
            INDBeCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As InventorySequence Implements IConsignmentTransfer.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Mensaje
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

    Public Property DocumentDate As Date Implements IConsignmentTransfer.DocumentDate
        Get
            Return INDDeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDDeDocumentDate.EditValue = value
        End Set
    End Property

    Public Property Description As String Implements IConsignmentTransfer.Description
        Get
            Return INDMeDescription.EditValue
        End Get
        Set(value As String)
            INDMeDescription.EditValue = value
        End Set
    End Property

    Public Property WareHouseId As Integer Implements IConsignmentTransfer.WareHouseId
        Get
            Return INDSleSourceWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDSleSourceWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la moneda del documento
    ''' </summary>
    ''' <param name="_currencyAbbreviation">Abreviación de la moneda</param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer Implements IConsignmentTransfer.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
            INDsleCurrency.Properties.NullText = _currencyAbbreviation
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyDatasource As XPInstantFeedbackSource Implements IConsignmentTransfer.CurrencyDatasource
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la abreviación de la moneda seleccionada
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String
        Get
            Return INDsleCurrency.Text
        End Get
    End Property

    ''' *******Variables importacion Excel*******

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private myStream As String = Nothing

    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private listErrosImportFile As List(Of String())

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Private rows As RowCollection

    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Private totalProcessedItems As Integer = 0

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsSend As Integer = 300

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Private listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

    '''<summary>
    '''Listado que almacena lso mensajes de los productos que se importan pero estan duplicados
    '''</summary>
    '''<remarks></remarks>
    Private duplicatedProductsFile As List(Of String)
#End Region

#Region "Methods and functions"
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
        Try
            If Not ValidateControls() Then
                Exit Sub
            End If

            AssigningValues()
            Using model As New MConsignmentTransfer(Tag.ToString())
                Me.AsyncLoader(True)
                Dim result = Await model.Save(_consignmentTransfer, _idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _consignmentTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._consignmentTransfer = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
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
    ''' Anular
    ''' </summary>
    Public Async Sub Anular()
        Try
            If Not ValidateControls() Then
                Exit Sub
            End If

            _consignmentTransfer.Status = 3
            Using model As New MConsignmentTransfer(Tag.ToString())
                Me.AsyncLoader(True)
                Dim result = Await model.Save(_consignmentTransfer, _idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _consignmentTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._consignmentTransfer = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
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
    Private Async Sub SaveOrUpdateAndConfirm()
        Try
            If Not ValidateControls() Then
                Exit Sub
            End If

            AssigningValues()
            Using model As New MConsignmentTransfer(Tag.ToString())
                Me.AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmAsync(_consignmentTransfer, _idCurrentSequence)
                If result.StateResult OrElse result.StateResultAux Then
                    If _consignmentTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._consignmentTransfer = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                ElseIf result.StateResult Then
                    _consignmentTransfer = result.ObjectEmbbeded
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
    ''' Crea una nueva entidad
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence?.IsManual Then
            Deshacer()
        Else
            Await NewRequestParam()
        End If
    End Sub

    ''' <summary>
    ''' Limpia el formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
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
                              New ColumnInfo() With {.Caption = "Fecha documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Almacén Origen", .FieldName = "WarehouseCodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListInventoryConsignmentTransfer
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
            If INDBeCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBeCode.Enabled = False
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
        ReadOnlyControls(False)
        Status = True
        Code = String.Empty
        DocumentDate = GetDateServer()
        Description = Nothing
        WareHouseId = Nothing
        INDSleSourceWarehouse.Properties.NullText = ""
        INDSbAdd.Enabled = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        INDGcProducts.DataSource = Nothing
        Me.CurrencyDatasource = Nothing
        Me.CurrencyId(Me.indigo?.CurrencyISO4217) = Me.indigo?.OfficialCurrencyId
        _consignmentTransfer = Nothing
        INDLcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    Private Sub AssigningValues()
        With _consignmentTransfer
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .WarehouseId = WareHouseId
            .Description = Description
            .OperatingUnitId = _idOperativeUnit
            .Status = Status
            .CurrencyId = Me.CurrencyId
        End With
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewRequestParam() As Task
        _consignmentTransfer = New ConsignmentTransfer With {.Status = 1}
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
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
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

            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If
    End Function

    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    Private Async Function LoadControls() As Task
        Try
            If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If

                Me.BarraBotones.StatusRecordVisible = True
                Using model As New MConsignmentTransfer(CStr(Me.Tag))
                    AsyncLoader(True)
                    _consignmentTransfer = Await model.GetByCodeAsync(INDBeCode.Text.Trim)
                    If _consignmentTransfer IsNot Nothing AndAlso _consignmentTransfer.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_consignmentTransfer.Id))
                            With _consignmentTransfer
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                Description = .Description
                                WareHouseId = .WarehouseId
                                Status = .Status
                            End With

                            Me.CurrencyId(_consignmentTransfer.Currency?.Abbreviation) = _consignmentTransfer.CurrencyId

                            INDSleSourceWarehouse.Properties.NullText = _consignmentTransfer.WarehouseCodeName

                            INDGcProducts.DataSource = _consignmentTransfer.ConsignmentTransferDetail.ToList()
                            INDGcProducts.RefreshDataSource()

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._consignmentTransfer.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _consignmentTransfer.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If

                            AsyncLoader(False)
                            ActionsOnControls = True
                            Me.BarraBotones.SetDocuments(_consignmentTransfer.Id)

                            Select Case _consignmentTransfer.Status
                                Case 1
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    ' Mostrar botón Importar si hay almacén origen
                                    If WareHouseId <> 0 Then
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                                    End If
                                Case Else
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                    INDSbAdd.Enabled = False
                                    ReadOnlyControls(True)
                            End Select
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _consignmentTransfer.Id, 0, _consignmentTransfer.Id)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewRequestParam()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBeCode.Focus()
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverse"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' actions grid
    ''' </summary>
    Private Sub addColumnActions()
        IndigoGridView1.SetListAcction(INDGvProducts, {eAcciones.Edit, eAcciones.Remove}.ToList())
        Dim col = INDGvProducts.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If col IsNot Nothing Then col.Width = 80
    End Sub

    ''' <summary>
    ''' Validate products
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateProductOnGrid(detailEdit As ConsignmentTransferDetail, warehouseId As Integer, productId As Integer) As Boolean Implements IValidateDetails.ValidateProductOnGrid
        If _consignmentTransfer.ConsignmentTransferDetail _
            .Any(Function(m) (detailEdit Is Nothing OrElse Not m.Equals(detailEdit)) AndAlso m.WarehouseId = warehouseId AndAlso m.ProductId = productId) Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Edit detail
    ''' </summary>
    Private Sub EditSelectedDetail()
        Dim consignmentDetail = INDGvProducts.GetFocusedObject(Of ConsignmentTransferDetail)()
        OpenConsignmentTransferDetail(consignmentDetail)
    End Sub

    ''' <summary>
    ''' Remove selected item
    ''' </summary>
    Private Sub RemoveSelectedDetail()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim item = INDGvProducts.GetFocusedObject(Of ConsignmentTransferDetail)()
        While item.ConsignmentTransferDetailBatchSerial.Any()
            item.ConsignmentTransferDetailBatchSerial(0).MarkAsDeleted()
        End While
        item.MarkAsDeleted()

        INDGcProducts.DataSource = _consignmentTransfer.ConsignmentTransferDetail.ToList()
        INDGcProducts.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Abre modal de detalle
    ''' </summary>
    ''' <param name="consigmentDetailEdit"></param>
    Private Sub OpenConsignmentTransferDetail(consigmentDetailEdit As ConsignmentTransferDetail)
        If _sourceWarehouse Is Nothing Then
            _sourceWarehouse = INDSleSourceWarehouse.GetFocusedObject(Of WarehouseXpo)()
        End If

        If _sourceWarehouse Is Nothing Then
            _sourceWarehouse = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of WarehouseXpo)($"Id = {INDSleSourceWarehouse.EditValue}")
        End If

        Using frm As New FrmPopupProductConsignmentTransfer()
            frm.SourceWarehouseId = _sourceWarehouse.Id
            frm.SourceSupplierWarehouseId = _sourceWarehouse.SupplierId.Id
            frm.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
            frm.SetOnValidateProducts(Me)

            AddHandler frm.OnAddConsignmentProduct, AddressOf AddConsignmentTransferDetail
            AddHandler frm.Shown, Sub()
                                      If consigmentDetailEdit IsNot Nothing Then
                                          frm.EditDetail(consigmentDetailEdit)
                                      End If
                                  End Sub

            Dim tr As New FrmTransparent(frm, False)
            tr.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Agrega detalle a la entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="consignmentDetail"></param>
    Private Sub AddConsignmentTransferDetail(sender As Object, consignmentDetail As ConsignmentTransferDetail)
        _consignmentTransfer.ConsignmentTransferDetail.Add(consignmentDetail)
        INDGcProducts.DataSource = _consignmentTransfer.ConsignmentTransferDetail.ToList()
        INDGcProducts.RefreshDataSource()
    End Sub

    ''' <summary>
    '''  Recibe las solicitudes de traslado seleccionadas desde el formulario de importación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub ReturnImportedRequestDetail(sender As Object, e As AddProductConsignmentTransfer)
        Try
            ' Mostrar barra de progreso
            AsyncLoader(True)

            ' Procesar en segundo plano
            Await Task.Run(Sub()
                               Dim product As InventoryProductXpo
                               Dim warehouse As WarehouseXpo

                               For Each Item In e.ListConsignmentTransferDetail
                                   product = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventoryProductXpo)($"Id = {Item.ProductId}")
                                   warehouse = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of WarehouseXpo)($"Id = {Item.WarehouseId}")

                                   If product Is Nothing Then
                                       Continue For
                                   End If

                                   Item.ProductCodeName = $"{product.Code} - {product.Name}"
                                   Item.ProductCost = product.ProductCost
                                   Item.WarehouseCodeName = If(warehouse IsNot Nothing, $"{warehouse.Code} - {warehouse.Name}", "")

                                   ' Asignar si el producto maneja lotes/series
                                   If product.ProductSubGroupId IsNot Nothing Then
                                       Item.HandlesBatch = product.ProductSubGroupId.HandlesBatch
                                   End If

                                   _consignmentTransfer.ConsignmentTransferDetail.Add(Item)
                               Next
                           End Sub)

            ' Actualizar interfaz en el hilo UI
            INDGcProducts.DataSource = Nothing
            INDGcProducts.DataSource = _consignmentTransfer.ConsignmentTransferDetail.ToList()
            INDGcProducts.RefreshDataSource()

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al importar productos: {ex.Message}"
        Finally
            ' Ocultar barra de progreso
            AsyncLoader(False)
        End Try
    End Sub
#End Region

#Region "Handlers"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmConsignmentTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        _idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        _doc = Nothing
        indigo = SessionValues.Instance
        _presenter = New PConsignmentTransfer(Me)
        _presenter.GetSequense()
        addColumnActions()
        LoadStatus()
        _consignmentTransfer = New ConsignmentTransfer With {.Status = 1}
        Me.CurrencyId(Me.indigo?.CurrencyISO4217) = Me.indigo?.OfficialCurrencyId
        Deshacer()
        '''Inicializa  el excel a exportar
        INDEsbDetails.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Almacén Destino", .Comment = "Digite el código del almacén destino"},
                            New ExcelColumn With {.Name = "Producto", .Comment = "Digite el codigo del producto"},
                            New ExcelColumn With {.Name = "Cantidad", .Comment = "Digite la cantidad necesitada"},
                            New ExcelColumn With {.Name = "Observación", .Comment = "Digite la observación"}
                        }
                    })
    End Sub

    ''' <summary>
    ''' Disposing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        _presenter = Nothing
        _consignmentTransfer = Nothing
    End Sub

    ''' <summary>
    ''' Closing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Key down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBeCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBeCode.KeyDown
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
                    Await NewRequestParam()
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
    Private Sub FrmBillingGroup_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBeCode.Text Is String.Empty Then
            INDBeCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._consignmentTransfer IsNot Nothing AndAlso Me._consignmentTransfer.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBeCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBeCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Button action
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If sender.Tag.ToString() = "Edit" Then
            EditSelectedDetail()
        ElseIf sender.Tag.ToString() = "Remove" Then
            RemoveSelectedDetail()
        End If
    End Sub

    ''' <summary>
    ''' Edit value change
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSourceWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSourceWarehouse.EditValueChanged
        _sourceWarehouse = INDSleSourceWarehouse.GetFocusedObject(Of WarehouseXpo)()
        INDSbAdd.Enabled = INDSleSourceWarehouse.EditValue IsNot Nothing

        ' Mostrar botón Importar si hay almacén seleccionado
        If INDSleSourceWarehouse.EditValue IsNot Nothing Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    ''' <summary>
    ''' Event when opening the coins popup - loads the datasource
    ''' </summary>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If Me.CurrencyDatasource Is Nothing Then
            _presenter.InitializeCurrency()
        End If
    End Sub

    ''' <summary>
    ''' Load supplie
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSourceWarehouse_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSourceWarehouse.QueryPopUp
        If INDSleSourceWarehouse.Properties.DataSource Is Nothing Then
            Dim filter = $"Status = True AND VirtualStore = 0 AND WarehouseConsignment = 1 AND CustodyStore = 0 AND TransitStore = 0 AND Inventory_WarehouseUsers[UserCode = '{indigo.UserIndigo}']"
            INDSleSourceWarehouse.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of WarehouseXpo)(filter)
        End If
    End Sub

    Private Sub INDRpPceBatch_Popup(sender As Object, e As EventArgs) Handles INDRptPceBatchSerial.Popup
        Dim detail = DirectCast(INDGvProducts.GetFocusedRow, ConsignmentTransferDetail)
        INDGcBatch.DataSource = detail.ConsignmentTransferDetailBatchSerial
        INDGcBatch.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Add product
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        OpenConsignmentTransferDetail(Nothing)
    End Sub

    ''' <summary>
    ''' datasource change para habilitar o deshabilitar el almacén de origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGcProducts_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcProducts.DataSourceChanged
        INDSleSourceWarehouse.ReadOnly = INDGvProducts.RowCount > 0
    End Sub

    ''' <summary>
    ''' Importa archivo excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnImportFileProducts_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Await ImportFileAsync()
    End Sub

    Private Async Function ImportFileAsync() As Task
        If INDSleSourceWarehouse.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un almacen de origen"
            Exit Function
        End If
        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        'Si el usuario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Exit Function
        End If
        AsyncLoader(True)

        Try
            'obtengo la ruta del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If
            AsyncLoader(True)
            Await LoadImportFileServices()
            ShowDuplicateProductsForm(duplicatedProductsFile)

            If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = $"El archivo presento error en {listErrosImportFile.Count} registros"

                Dim ErrorsExcel As New SpreadsheetControl
                ErrorsExcel.CreateNewDocument()
                ErrorsExcel.Document.Worksheets.ActiveWorksheet = ErrorsExcel.Document.Worksheets(0)
                Dim worksheet As Worksheet = ErrorsExcel.Document.Worksheets.ActiveWorksheet

                worksheet.Cells(0, 0).Value = "Almacén Destino"
                worksheet.Cells(0, 1).Value = "Producto"
                worksheet.Cells(0, 2).Value = "Cantidad"
                worksheet.Cells(0, 3).Value = "Observación"
                worksheet.DefaultColumnWidth = 250
                Dim rows = 1
                For Each dato As String() In listErrosImportFile
                    Dim Columns = 0
                    For Each item In dato
                        worksheet.Cells(rows, Columns).Value = item
                        Columns += 1
                    Next
                    rows += 1
                Next

                Dim fileName As String = System.IO.Path.GetTempPath() & "ErroresImportacion_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"
                ErrorsExcel.SaveDocument(fileName)
                System.Diagnostics.Process.Start(fileName)
            Else
                INDGcProducts.DataSource = Nothing
                INDGcProducts.DataSource = _consignmentTransfer.ConsignmentTransferDetail.ToList()
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
            AsyncLoader(False)
        End Try
        AsyncLoader(False)
    End Function

    Private Function LoadImportFileServices() As Task
        Return Task.Factory.StartNew(Sub()
                                         listErrosImportFile = New List(Of String())
                                         Dim ssc = New SpreadsheetControl()
                                         ssc.AllowDrop = False
                                         ssc.LoadDocument(myStream)

                                         Dim workBook As IWorkbook = ssc.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex <= 0 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                             Return
                                         End If
                                         Dim indexSend = 0
                                         totalProcessedItems = 0
                                         Dim totalItems = rows.LastUsedIndex
                                         While (totalItems + 1) > totalProcessedItems
                                             Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                                             indexSend = totalProcessedItems
                                             totalProcessedItems += quantityDetailsToProcess
                                             listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                             SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)
                                             Using model As New MConsignmentTransfer(Me.Tag.ToString())
                                                 Dim result = model.SetConsignmentTransferImportFile(listRows.ToList(), CInt(INDSleSourceWarehouse.EditValue))
                                                 If result.ListMessageResult IsNot Nothing And result.ListMessageResult.Count > 0 Then
                                                     listErrosImportFile.AddRange(result.ListMessageResult)
                                                 End If
                                                 If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                                                     ProcessConsignmentDetail(result.ObjectEmbbeded)
                                                 End If
                                             End Using
                                         End While
                                     End Sub)
    End Function

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(4)})
                                              End SyncLock
                                          End Sub)
    End Sub

    ''' <summary>
    ''' Valida duplicados en archivo excel y en el copy paste
    ''' </summary>
    ''' <param name="items"></param>
    ''' <returns></returns>
    Function ProcessConsignmentDetail(ByVal items As Object)
        If _consignmentTransfer Is Nothing Then
            _consignmentTransfer = New ConsignmentTransfer With {.Status = 1}
        End If

        duplicatedProductsFile = New List(Of String)()
        For Each item As ConsignmentTransferDetail In items
            Dim exists As Boolean = False
            For Each existingItem In _consignmentTransfer.ConsignmentTransferDetail
                If existingItem.ProductId = item.ProductId AndAlso existingItem.WarehouseId = item.WarehouseId Then
                    exists = True
                    Exit For
                End If
            Next

            If exists Then
                Dim message As String = ""
                message = $"El producto {item.ProductCodeName} ya existe en el almacén destino : {item.WarehouseCodeName} y no se agrego."
                duplicatedProductsFile.Add(message)
            Else
                _consignmentTransfer.ConsignmentTransferDetail.Add(item)
            End If
        Next
    End Function


    ''' <summary>
    ''' Muestra el resultado de duplicados en el cargue o copy paste excel
    ''' </summary>
    ''' <param name="duplicatedProductsFile"></param>
    Sub ShowDuplicateProductsForm(ByVal duplicatedProductsFile As List(Of String))
        If duplicatedProductsFile?.Count > 0 Then
            Using formulario As New FrmListErrors(duplicatedProductsFile)
                formulario.Title = "Resultado validación importación datos"
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

#Region "CopyPasteGridControl"
    Private Async Sub GetDataCopyPaste(sender, e)
        If sender.Equals(INDGcProducts) Then
            INDGcProducts.DataSource = Nothing
            If e.Rows(0).Item(0).Contains("Almacén Destino") Then
                e.Rows.Remove(e.Rows.ElementAt(0))
            End If

            AsyncLoader(True)
            Using Model As New MConsignmentTransfer(Me.Tag.ToString())
                Dim result = Await Model.SetConsignmentTransferDetailFromCopyPaste(e.Rows, CInt(INDSleSourceWarehouse.EditValue))
                'si ocurrio un error
                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End Using
                End If

                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    ProcessConsignmentDetail(result.ObjectEmbbeded)
                    ShowDuplicateProductsForm(duplicatedProductsFile)
                End If
                INDGcProducts.DataSource = _consignmentTransfer.ConsignmentTransferDetail.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
            End Using
            AsyncLoader(False)
        End If
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBeCode.ButtonClick
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
    ''' Save and confirm
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            SaveOrUpdateAndConfirm()
        End If
    End Sub

    ''' <summary>
    ''' Save and confirm
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            SaveOrUpdateAndConfirm()
        End If
    End Sub

    ''' <summary>
    ''' Anular
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Anular()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click imprimir.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _consignmentTransfer.Id, 0, _consignmentTransfer.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click importar informacion.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        ' Verificar que hay almacén seleccionado
        If INDSleSourceWarehouse.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un almacén origen antes de importar"
            Exit Sub
        End If

        ' Abrir formulario de importación de solicitudes
        Using frmImport As New FrmConsignmentTransferImport
            ' Conectar el evento que retornará los detalles seleccionados
            AddHandler frmImport.GetListConsignmentTransferDetail, AddressOf ReturnImportedRequestDetail

            ' Cambiar cursor a ocupado
            Me.Cursor = ChangeCursorIndigo()

            ' Configurar tamaño y posición del formulario
            frmImport.Size = New System.Drawing.Size(800, 700)
            frmImport.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

            ' Pasar parámetros de filtro
            frmImport.OrderType = 2  ' 2 = Traslado
            frmImport.DispatchTo = 2  ' 2 = Almacén
            frmImport.FilterFunctionalUnitWarehouse = WareHouseId  ' Filtrar por almacén origen
            frmImport.WarehouseId = WareHouseId  ' Almacén origen
            frmImport.ListConsignmentTransferDetailValidation = _consignmentTransfer.ConsignmentTransferDetail.ToList()

            ' IMPORTANTE: Cargar datos DESPUÉS de configurar las propiedades
            frmImport.LoadData()

            ' Mostrar formulario como modal con fondo transparente
            Dim transparent = New Base.FrmTransparent(frmImport, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' CopyPaste de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IndigoGridControl1_PasteToGrid(Sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        GetDataCopyPaste(Sender, e)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If _sequence?.Scope.Equals("OU") AndAlso _sequence.InventorySequenceDetail IsNot Nothing Then
                If Not _sequence.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

    Public Interface IValidateDetails
        Function ValidateProductOnGrid(detailEdit As ConsignmentTransferDetail, warehouseId As Integer, productId As Integer) As Boolean
    End Interface

End Class
#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress
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
Imports Presentation.Payroll
#End Region

Public Class FrmPurchaseRequest
    Implements IPurchaseRequest, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la solicitud de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IPurchaseRequest.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements IPurchaseRequest.DocumentDate
        Get
            Return INDDtDate.EditValue
        End Get
        Set(value As Date)
            INDDtDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPurchaseRequest.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    '''  Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IPurchaseRequest.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private Property _sequence As Domain.Entities.InventorySequence
    Public Property Sequense As InventorySequence Implements IPurchaseRequest.Sequense
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
    ''' Obtiene o establece una observacion de la solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observation As String Implements IPurchaseRequest.Observation
        Get
            Return INDMmObservation.EditValue
        End Get
        Set(value As String)
            INDMmObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de la solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequestType As Byte? Implements IPurchaseRequest.RequestType
        Get
            Return INDSleRequestType.EditValue
        End Get
        Set(value As Byte?)
            INDSleRequestType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IPurchaseRequest.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitId As Integer Implements IPurchaseRequest.FunctionalUnitId
        Get
            Return INDSleFunctionalUnit.EditValue
        End Get
        Set(value As Integer)
            INDSleFunctionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitXpo As XPInstantFeedbackSource Implements IPurchaseRequest.FunctionalUnitXpo
        Get
            Return INDSleFunctionalUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de los tipos de solicitud
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestTypeDataSource As XPInstantFeedbackSource Implements IPurchaseRequest.RequestTypeDataSource
        Get
            Return TryCast(INDSleRequestType.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRequestType.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "Globals"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PPurchaseRequest

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MPurchaseRequest

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim PurchaseRequest As PurchaseRequest

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemPurchaseRequestDetail As PurchaseRequestDetail

    ''' <summary>
    ''' Listado de eliminados de los detalles de solicitud de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeletePurchaseRequestDetail As List(Of PurchaseRequestDetail)

    ''' <summary>
    ''' Listado de los detalles de solicitud de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Property ListPurchaseRequestDetail As Domain.Entities.TrackableCollection(Of PurchaseRequestDetail)
        Get
            If INDGcRequestDetail Is Nothing Then
                Return New Domain.Entities.TrackableCollection(Of PurchaseRequestDetail)
            End If
            Return INDGcRequestDetail.DataSource
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of PurchaseRequestDetail))
            INDGcRequestDetail.DataSource = value
            If PurchaseRequest IsNot Nothing Then
                PurchaseRequest.PurchaseRequestDetail = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' flag para solo lectura
    ''' </summary>
    ''' <remarks></remarks>
    Dim OnlyRead As Boolean = False

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' Variable para saber si confirma (True = Si confirma, False = No confirma)
    ''' </summary>
    ''' <remarks></remarks>
    Dim banConfirm As Boolean

    ''' <summary>
    ''' Variable para saber si guardan y confirman, o si actualizan y confirman (True = GuardarConfirmar, False = ActualizarConfirmar)
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSaveAndConfirm As Boolean

    ''' <summary>
    ''' Variable para saber si anulan (True = Si anula, False = no confirma)
    ''' </summary>
    ''' <remarks></remarks>
    Dim banAnular As Boolean

    ''' <summary>
    ''' Variable que contiene la lista de tipos de solicitud
    ''' </summary>
    Dim ListRequestType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If PurchaseRequest IsNot Nothing AndAlso PurchaseRequest.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MPurchaseRequest(Me.Tag.ToString())
                    AsyncLoader(True)
                    PurchaseRequest.MarkAsDeleted()
                    Dim result = Await Model.DeletePurchaseRequest(PurchaseRequest)
                    If result.StateResult = True Then
                        Me.DeleteDocumentIndexed()
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDBtnCode.Enabled = False
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    '''  METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ListPurchaseRequestDetail Is Nothing OrElse ListPurchaseRequestDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay Productos agregados en la rejilla."
                Exit Sub
            Elseif Me.PurchaseRequest.Status = 2 AndAlso Not ListPurchaseRequestDetail.Any(Function(d) d.Status = 1) Then 'confirmar y no hay algun item aprobado
                Mensaje(EeventViewerImages.Advertencia) = "Debe aprobar al menos un ítem."
                Exit Sub
            End If
        End If
        If Me.PurchaseRequest.Status = 1 Then
            If ListPurchaseRequestDetail IsNot Nothing AndAlso ListPurchaseRequestDetail.Count > 0 AndAlso ListPurchaseRequestDetail.Any(Function(d) d.Status = 1) Then
                Me.PurchaseRequest.Status = 4 'En tramite
            End If
        End If
        AssigningValues()
        Try
            Using model As New MPurchaseRequest(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SavePurchaseRequest(PurchaseRequest, _idCurrentSequence, Me._sequence)
                If Result.StateResult = True Then
                    If PurchaseRequest.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense.Remove(_idCurrentSequence)
                            'Me.DicSequense(Me._sequense.InventorySequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            If banSaveAndConfirm Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        End If
                    ElseIf PurchaseRequest.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If banConfirm Then
                            If banSaveAndConfirm Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ConfirmationMessage")
                            End If
                        ElseIf banAnular Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If

                    End If
                    Me.PurchaseRequest = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, PurchaseRequest.Id, 0, PurchaseRequest.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, PurchaseRequest.Id, 0, PurchaseRequest.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, PurchaseRequest.Id, 0, PurchaseRequest.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, PurchaseRequest.Id, 0, PurchaseRequest.Id)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    If Result.MessageResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    ElseIf Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                    If PurchaseRequest.Id > 0 Then
                        PurchaseRequest = model.GetPurchaseRequestByCode(Code).Result.ObjectEmbbeded
                    Else
                        PurchaseRequest = New PurchaseRequest
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPurchaseRequest()
        End If
    End Sub

#End Region

#Region "Methods"

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
    ''' Carga los estados de la solicitud
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StatusAproved"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusProcessed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(186, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.PurchaseRequest.Code, Me.PurchaseRequest.OperatingUnitId, Me.PurchaseRequest.CreationDate, Me.PurchaseRequest.RequestType, Me.PurchaseRequest.Observation),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.PurchaseRequest.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.PurchaseRequest.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.PurchaseRequest.Code, Me.PurchaseRequest.OperatingUnitId, Me.PurchaseRequest.CreationDate, Me.PurchaseRequest.RequestType, Me.PurchaseRequest.Observation)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.PurchaseRequest.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        Try
            With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "CreationDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Tipo Solicitud", .FieldName = "RequestTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPurchaseRequest
            .FormParent = Me
            .ShowSearch()
        End With
        Catch ex As Exception
            Dim hola As Boolean
            hola = true
        End Try
        
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPurchaseRequest.ActionsOnControls
        Set(value As Boolean)
            INDLcPurchaseRequest.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDDtDate.Enabled = value
            INDSleRequestType.Enabled = value
            INDSleFunctionalUnit.Enabled = value
            INDMmObservation.Enabled = value
            INDSBtnAddProduct.Enabled = value
            INDGcRequestDetail.Enabled = value
            BarraBotones.StatusRecordVisible = true
            INDLcPurchaseRequest.EndUpdate()
            If value Then
                INDDtDate.Focus()
            Else
                INDBtnCode.Focus()
            End If

        End Set
    End Property

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If INDBtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        DeleteBlockedRecord()
        INDLcPurchaseRequest.BeginUpdate()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        'Me.BarraBotones.StatusRecordVisible = False

        PurchaseRequest = Nothing
        Code = String.Empty
        DocumentDate = Me.GetDateServer()
        RequestType = 0
        INDSleRequestType.Properties.NullText = String.Empty
        FunctionalUnitId = Nothing
        INDSleFunctionalUnit.Properties.NullText = String.Empty
        Observation = Nothing
        Status = 0

        ReadOnlyControls(False)
        ItemPurchaseRequestDetail = Nothing
        ListPurchaseRequestDetail = New Domain.Entities.TrackableCollection(Of PurchaseRequestDetail)
        ListDeletePurchaseRequestDetail = Nothing
        INDGcRequestDetail.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcRequestDetail)
        BarraBotones.ReassignOperatingUnit()
        INDLcPurchaseRequest.EndUpdate()
        ActionsOnControls = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPurchaseRequest() As Task
        PurchaseRequest = New PurchaseRequest()
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


    End Function

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With PurchaseRequest
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .CreationDate = DocumentDate
            .RequestTypeId = RequestType
            .FunctionalUnitId = FunctionalUnitId
            .Prefix = Me._prefixSelected
            .Observation = Observation
            If ListPurchaseRequestDetail IsNot Nothing AndAlso ListPurchaseRequestDetail.Count > 0 Then
                For Each itemDetail As PurchaseRequestDetail In ListPurchaseRequestDetail
                    .PurchaseRequestDetail.Add(itemDetail)
                Next
            End If

            If ListDeletePurchaseRequestDetail IsNot Nothing AndAlso ListDeletePurchaseRequestDetail.Count > 0 Then
                For Each itemDeleteDetail As PurchaseRequestDetail In ListDeletePurchaseRequestDetail
                    .PurchaseRequestDetail.Add(itemDeleteDetail)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Método que carga los controles de la solicitud de inventario
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
                Using Model As New MPurchaseRequest(CStr(Me.Tag))
                    AsyncLoader(True)
                    PurchaseRequest = (Await Model.GetPurchaseRequestByCode(INDBtnCode.Text.Trim)).ObjectEmbbeded
                    INDLcPurchaseRequest.BeginUpdate()
                    If PurchaseRequest IsNot Nothing AndAlso PurchaseRequest.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(PurchaseRequest.Id))
                            With PurchaseRequest
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                Me._idOperativeUnit = .OperatingUnitId
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                DocumentDate = .CreationDate
                                RequestType = .RequestTypeId
                                FunctionalUnitId = .FunctionalUnitId
                                INDSleFunctionalUnit.Properties.NullText = .DescriptionFunctionalUnit
                                Observation = .Observation
                                Me.Status = .Status

                                ListPurchaseRequestDetail = .PurchaseRequestDetail

                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.PurchaseRequest.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = PurchaseRequest.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            If Status = 1 OrElse Status = 4 Then 'registrado o en tramite
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)
                                INDSBtnAddProduct.Enabled = True
                            Else
                                INDSBtnAddProduct.Enabled = False
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.ReadOnlyControls(True)
                            End If

                            Me.BarraBotones.SetDocuments(PurchaseRequest.Id, Me.Tag.ToString(), Nothing, GetType(PurchaseRequest).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, PurchaseRequest.Id, 0, PurchaseRequest.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPurchaseRequest()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDLcPurchaseRequest.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If



        If Status = 2 Or Status = 3 Then
            Me.ActionsOnControls = False
            INDGcRequestDetail.Enabled = True
        End If
    End Function

    ''' <summary>
    ''' Método utilizado para adquirir losproductos retornados por el frontal de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupAddProduct(sender As Object, e As AddPurchaseRequestDetailEventArgs)
        If ListPurchaseRequestDetail Is Nothing Then
            ListPurchaseRequestDetail = New Domain.Entities.TrackableCollection(Of PurchaseRequestDetail)
        End If
        If e.EditMode = True Then
            ListPurchaseRequestDetail.Remove(ItemPurchaseRequestDetail)

            ListPurchaseRequestDetail.Insert(indexEditRecord, e.ItemPurchaseRequestDetail)
        Else
            ListPurchaseRequestDetail.Add(e.ItemPurchaseRequestDetail)
        End If
    End Sub

    ''' <summary>
    ''' Evento que administra el proceso de aprobacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupApproveDetail(sender As Object, e As AddPurchaseRequestDetailEventArgs)
    End Sub

    '''' <summary>
    '''' metodo para instanciar el formulario de agregar productos
    '''' </summary>
    '''' <remarks></remarks>
    Private Sub InstantiatePopup(OnlyRead As Boolean, _Detaill As PurchaseRequestDetail, _listProducts As Domain.Entities.TrackableCollection(Of PurchaseRequestDetail))
        If _Detaill IsNot Nothing AndAlso _Detaill.Id > 0 AndAlso _Detaill.Status > 0 AndAlso Me.Status < 2 Then
            Mensaje(EeventViewerImages.Informacion) = "No se permite editar el detalle porque esta " & _Detaill.StatusName
            Exit Sub
        End If
        Me.Cursor = ChangeCursorIndigo()
        If INDSleRequestType.EditValue = 1 Then
            Using formulario As New PopUpProductPurchaseRequest(OnlyRead, _Detaill, _listProducts)
                AddHandler formulario.AddProduct, AddressOf ReturnPopupAddProduct
                formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                formulario.ViewModeEditHold = True
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.Size = New Size(800, 700)
                Dim transparent = New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        ElseIf INDSleRequestType.EditValue = 2 Then
            Using formulario As New PopUpFixedAssetPurchaseRequest(OnlyRead, _Detaill, _listProducts)
                AddHandler formulario.AddFixedAssetItem, AddressOf ReturnPopupAddProduct
                formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                formulario.ViewModeEditHold = True
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.Size = New Size(800, 700)
                Dim transparent = New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        ElseIf INDSleRequestType.EditValue = 3 Then
            Using formulario As New PopUpOtherPurchaseRequest(OnlyRead, _Detaill, _listProducts)
                AddHandler formulario.AddOtherServiceItem, AddressOf ReturnPopupAddProduct
                formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                formulario.ViewModeEditHold = True
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.Size = New Size(800, 700)
                Dim transparent = New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
        
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        ItemPurchaseRequestDetail = DirectCast(INDGvRequestDetail.GetFocusedRow(), PurchaseRequestDetail)
        indexEditRecord = ListPurchaseRequestDetail.IndexOf(ItemPurchaseRequestDetail)
        InstantiatePopup(Status = 2 Or Status = 3, ItemPurchaseRequestDetail, ListPurchaseRequestDetail)
    End Sub

    ''' <summary>
    ''' Aprobar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ApproveDetail()
        ItemPurchaseRequestDetail = DirectCast(INDGvRequestDetail.GetFocusedRow(), PurchaseRequestDetail)
        indexEditRecord = ListPurchaseRequestDetail.IndexOf(ItemPurchaseRequestDetail)
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New PopUpApproveDetail(Status = 2 Or Status = 3, ItemPurchaseRequestDetail)
                AddHandler formulario.ApproveItem, AddressOf ReturnPopUpApproveDetail
                formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                formulario.ViewModeEditHold = True
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.Size = New Size(800, 700)
                Dim transparent = New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        ItemPurchaseRequestDetail = DirectCast(INDGvRequestDetail.GetFocusedRow(), PurchaseRequestDetail)
        If ItemPurchaseRequestDetail IsNot Nothing AndAlso ItemPurchaseRequestDetail.Id > 0 AndAlso ItemPurchaseRequestDetail.Status > 0 Then
            Mensaje(EeventViewerImages.Informacion) = "No se permite eliminar el detalle porque esta " & ItemPurchaseRequestDetail.StatusName
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            If ItemPurchaseRequestDetail.Id > 0 Then
                If ListDeletePurchaseRequestDetail Is Nothing Then
                    ListDeletePurchaseRequestDetail = New List(Of PurchaseRequestDetail)
                End If
                ItemPurchaseRequestDetail.MarkAsDeleted()
                ListDeletePurchaseRequestDetail.Add(ItemPurchaseRequestDetail)
            End If
            PurchaseRequest.PurchaseRequestDetail.Remove(ItemPurchaseRequestDetail)
        End If
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetails()
        If ListPurchaseRequestDetail IsNot Nothing AndAlso ListPurchaseRequestDetail.Count > 0 Then
            If MessageIndigo.Show("Se va a borrar los detalles de servicio adicionados. ¿Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me.PurchaseRequest IsNot Nothing Then Me.PurchaseRequest.RequestTypeId = INDSleRequestType.EditValue
                For Each ItemPurchaseRequestDetail In ListPurchaseRequestDetail.ToList
                    If ItemPurchaseRequestDetail.Id > 0 Then
                        If ListDeletePurchaseRequestDetail Is Nothing Then
                            ListDeletePurchaseRequestDetail = New List(Of PurchaseRequestDetail)
                        End If
                        ItemPurchaseRequestDetail.MarkAsDeleted()
                        ListDeletePurchaseRequestDetail.Add(ItemPurchaseRequestDetail)
                    End If
                    ListPurchaseRequestDetail.Remove(ItemPurchaseRequestDetail)
                    PurchaseRequest.PurchaseRequestDetail.Remove(ItemPurchaseRequestDetail)
                Next
            Else
                If Me.PurchaseRequest IsNot Nothing AndAlso Me.PurchaseRequest.RequestTypeId > 0 Then RequestType = Me.PurchaseRequest.RequestTypeId
            End If
        Else
            If Me.PurchaseRequest IsNot Nothing Then Me.PurchaseRequest.RequestTypeId = INDSleRequestType.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.PurchaseRequest IsNot Nothing AndAlso Me.PurchaseRequest.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        Presenter = Nothing
        Model = Nothing
        PurchaseRequest = Nothing
        _idOperativeUnit = Nothing
        _prefixSelected = Nothing
        _idCurrentSequence = Nothing
        ItemPurchaseRequestDetail = Nothing
        ListDeletePurchaseRequestDetail = Nothing
        OnlyRead = Nothing
        indexEditRecord = Nothing
        banConfirm = Nothing
        banSaveAndConfirm = Nothing
        banAnular = Nothing
        ListRequestType = Nothing
        varImp = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        IndigoGridView1.MoreInfoColunmns(INDGvRequestDetail)
        ' Agrega a la rejilla la columna de Acciones
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        
        if CType(Me, FormBase).BarraBotones.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ApproveItems)) Then
            ListActions.Add(eAcciones.Approve)
        End If
        
        IndigoGridView1.SetListAcction(INDGvRequestDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvRequestDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            ElseIf col.Name = "MoreInfo" Then
                col.Width = 50
            End If
        Next
        Me.LayoutControls.SetIsCustomizable(Me.INDLcPurchaseRequest, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PPurchaseRequest(Me)
        Presenter.GetSequense()
        Presenter.RequestType()
        LoadStatus()
        Deshacer()
        IndigoGridControl1.RefreshGrid(INDGcRequestDetail)
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se activa, direge el foco al control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRequest_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
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
    Private Sub FrmRequest_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Funcion para limitar cantidad de caracteres en control memoEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDMmObservation_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDMmObservation.EditValueChanging
        If e.NewValue Is Nothing Then
            Return
        End If
        Dim maxLength As Integer = 300
        Dim edit As DevExpress.XtraEditors.MemoEdit = TryCast(sender, DevExpress.XtraEditors.MemoEdit)
        For Each str As String In edit.Lines
            If str.Length > maxLength Then
                e.Cancel = True
                Return
            End If
        Next str
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnit_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleFunctionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmFunctionalUnit With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.FunctionalUnit()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control para cargar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBtnCode.KeyDown
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
                    Await Me.NewPurchaseRequest()
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

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleFunctionalUnit.QueryPopUp
        If INDSleFunctionalUnit.Properties.DataSource Is Nothing Then
            Presenter.FunctionalUnit()
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Abre el popup para agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDSBtnAddProduct.Click
        If OnlyRead = False Then
            InstantiatePopup(False, Nothing, ListPurchaseRequestDetail)
        End If
    End Sub

#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la solicitud de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                If Status = 2 Or Status = 3 Then
                Else
                    DeleteDetail()
                End If
            Case "Approve"
                ApproveDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
            Case "Approve"
                ApproveDetail()
        End Select
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        banConfirm = False
        banSaveAndConfirm = False
        banAnular = False
        PurchaseRequest.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
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
        banConfirm = False
        banSaveAndConfirm = False
        banAnular = False
        PurchaseRequest.Status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, PurchaseRequest.Id, 0, PurchaseRequest.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        banConfirm = True
        banSaveAndConfirm = False
        banAnular = False
        PurchaseRequest.Status = 2
        varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        banConfirm = False
        banSaveAndConfirm = False
        banAnular = True
        PurchaseRequest.Status = 3
        varImp = 4
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        banConfirm = True
        banSaveAndConfirm = True
        banAnular = False
        PurchaseRequest.Status = 2
        varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleRequestType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRequestType.EditValueChanged
        If INDSleRequestType.EditValue IsNot Nothing AndAlso INDSleRequestType.Text <> "" AndAlso INDSleRequestType.EditValue <> 0 AndAlso (Me.PurchaseRequest Is Nothing OrElse Me.PurchaseRequest.RequestTypeId = 0 OrElse Me.PurchaseRequest.RequestTypeId <> INDSleRequestType.EditValue) Then
            DeleteDetails()
        End If
    End Sub

#End Region

End Class
'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 26/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Accounting
Imports System.Text
Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress
Imports System.Globalization
Imports Presentation.Maintenance
Imports DevExpress.Spreadsheet
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class FrmInventoryControl
    Implements IInventoryControl, ICustomizableForm

#Region "Builder"
    ''' <summary>
    ''' Se inicializa una isntancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Globals"

    ''' <summary>
    ''' Variable que representa la entdad principal de devolucion de comprobante de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Dim inventoryControl As InventoryControl

    Dim ListInventoryControlDetail As List(Of InventoryControlDetail)

    Dim ListDeleteInventoryControlDetail As List(Of InventoryControlDetail)

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PInventoryControl

    ''' <summary>
    ''' flag para solo lectura
    ''' </summary>
    ''' <remarks></remarks>
    Dim OnlyRead As Boolean = False

    Dim parameter As SettingInventory

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemInventoryControlDetail As InventoryControlDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' Bandera que informa si el documento se va a anular
    ''' </summary>
    ''' <remarks></remarks>
    Dim anular As Boolean

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

    ''' <summary>
    ''' datasource para cargar la rejilla cuando se hizo desde importar 
    ''' </summary>
    ''' <remarks></remarks>
    Dim datasourceImporFile As XPInstantFeedbackSource = Nothing

    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrosImportFile As List(Of String)
    ''' <summary>
    ''' control donde se muestra el progreso de la operacion de importar archivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim progress As CtrProgress
    ''' <summary>
    ''' total de los items a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalItems As Integer
    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalProcessedItems As Integer = 0
    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsSend As Integer = 300
#End Region

#Region "Properties"

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Public Property _sequence As Domain.Entities.InventorySequence
    Public Property Sequense As InventorySequence Implements IInventoryControl.Sequense
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
    ''' Otiene o establece el valor del tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IInventoryControl.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que define el mensaje para mostrar al usuario
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Especifica como se va redondear
    ''' </summary>
    ''' <remarks></remarks>
    Private _listControlType As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListControlType As List(Of Tuple(Of Integer, String))
        Get
            If _listControlType Is Nothing Then
                _listControlType = New List(Of Tuple(Of Integer, String))
                _listControlType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("InventoryControl_BeginningBalance", NAME_MODULE)))
                _listControlType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("InventoryControl_PhysicalInventory", NAME_MODULE)))
                _listControlType.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("InventoryControl_CustodyStore", NAME_MODULE)))
                _listControlType.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("InventoryControl_ControlStore", NAME_MODULE)))
            End If
            Return _listControlType
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el control de layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInventoryControl.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Listado que contiene los almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListWareHouse As XPInstantFeedbackSource Implements IInventoryControl.ListWareHouse
        Get
            Return INDSleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IInventoryControl.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            BarraBotones.StatusRecord = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IInventoryControl.Code
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
    ''' Obtiene o establece el tipo del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ControlType As Integer? Implements IInventoryControl.ControlType
        Get
            Return INDGleControlType.EditValue
        End Get
        Set(value As Integer?)
            INDGleControlType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime? Implements IInventoryControl.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el almacen del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WarehouseId As Integer? Implements IInventoryControl.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    'Private _AdmissionNumber As String
    '''' <summary>
    '''' Obtiene o establece el id de control de ingreso
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Property AdmissionNumber As String Implements IInventoryControl.AdmissionNumber
    '    Get
    '        Return _AdmissionNumber
    '    End Get
    '    Set(value As Integer?)
    '        _AdmissionNumber = value
    '    End Set
    'End Property


    ''' <summary>
    ''' Habilita los controles necesarios para el funcionamiento del frontal
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInventoryControl.ActionsOnControls
        Set(value As Boolean)
            INDBtnCode.Enabled = Not value
            INDDteDocumentDate.Enabled = value
            INDSleWarehouse.Enabled = value
            INDGleControlType.Enabled = value
            INDBtnPhysicalInventory.Enabled = value
            INDBtnImportFile.Enabled = value
            LyGroupProductList.Enabled = value
            INDsleAdmissionNumber.Enabled = value
            INDsleAdmissionNumber.Search.Enabled = value
            If value Then
                INDGleControlType.Focus()
                'INDsleAdmissionNumber.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

#Region "Datasource Entity"
    ''' <summary>
    ''' Datasource de Ingreso del Paciente
    ''' </summary>
    Public Property AdmissionNumberDatasource As XPInstantFeedbackSource Implements IInventoryControl.AdmissionNumberDatasource
        Get
            Return CType(INDsleAdmissionNumber.Datasource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAdmissionNumber.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el número de ingreso al paciente de la dispensación farmacéutica
    ''' </summary>
    Public Property AdmissionNumber As String Implements IInventoryControl.AdmissionNumber
#End Region

#Region "Others"
    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object
    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Representa al id del grupo de atención que viene asociado a la admision
    ''' y se envia como parametro al form FrmPharmaceuticalDispensingDetail
    ''' </summary>
    ''' <remarks></remarks>
    Private CareGroupId As Integer

    ''' <summary>
    ''' tercero del paciente en la admision
    ''' </summary>
    Private _thirdPartyPatientId As Integer
    Private CareGroupCodeName As String
#End Region
#End Region

#Region "ICrud"

    ''' <summary>
    ''' Inicia una busqueda con el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios realziado e inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Inicializa el formulario para realizar una nueva entrada de registro
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewInventoryControl()
        End If
    End Sub

    ''' <summary>
    ''' Despliega el formulario de busqueda
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
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Tipo", .FieldName = "DocumentTypeName", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 80}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInventoryControl
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Método que anula el documento
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements ICrudBase.Eliminar
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            anular = True
            Guardar()
        End If
    End Sub

#End Region

#Region "BarButtons"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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
    ''' Evento que convoca el Método deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que convoca el Método OpenSearch para hacer la busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que convoca el Método Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar, BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que anula el documento sin confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Eliminar()
    End Sub

    ''' <summary>
    ''' Evento que convoca el Método Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Await NewInventoryControl()
    End Sub

    ''' <summary>
    ''' Evento que convoca el método de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        BarraBotones.Focus()
        Await SaveOrUpdateAndConfirm(1)
    End Sub

    ''' <summary>
    ''' Evento que convoca el método de actualizar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        BarraBotones.Focus()
        Await SaveOrUpdateAndConfirm(2)
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, inventoryControl.Id, 0, inventoryControl.Id)
    End Sub
#End Region

#Region "Methods"

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
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.inventoryControl.Code, Me.inventoryControl.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.inventoryControl.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.inventoryControl.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.inventoryControl.Code, Me.inventoryControl.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.inventoryControl.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        inventoryControl = Nothing
        ListInventoryControlDetail = New List(Of InventoryControlDetail)
        ListDeleteInventoryControlDetail = New List(Of InventoryControlDetail)

        Code = String.Empty
        DocumentDate = Nothing
        WarehouseId = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty
        ControlType = Nothing
        parameter = Nothing

        INDGcProducts.DataSource = Nothing
        ReadOnlyControls(False)
        OnlyRead = False
        anular = False

        ActionsOnControls = False
        INDColProduct.FieldName = "ProductCodeName"
        INDBtnAddProducts.Enabled = False
        INDGleControlType.Properties.ReadOnly = False
        INDSleWarehouse.Properties.ReadOnly = False
        INDDteDocumentDate.Properties.ReadOnly = False
        INDLyBtnPhysicalInventory.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        INDLyBtnAddProducts.Visibility = XtraLayout.Utils.LayoutVisibility.Always
        AdmissionNumber = Nothing
        'AdmissionNumber = Nothing
        INDsleAdmissionNumber.IsReadOnly = False
        Me.INDsleAdmissionNumber.SetNullText(String.Empty)
        Me.LciAdmissionNumber.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        INDBtnCode.Focus()
    End Sub

    ''' <summary>
    ''' Retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            parameter = Await Model.GetSettingInventory(BarraBotones.OperatingUnit.Id)
            If parameter Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Deshacer()
                Exit Function
            End If
        End Using
    End Function

    ''' <summary>
    ''' Genera una nueva entidad de inventorycontrol
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function NewInventoryControl() As Task
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Function
        End If

        LoadParameters()
        inventoryControl = New InventoryControl
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
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"
    End Function

    ''' <summary>
    ''' Método que carga los controles de la consulta del contrato
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
                Using Model As New MInventoryControl(CStr(Me.Tag))
                    AsyncLoader(True)
                    Await LoadParameters()
                    inventoryControl = Await Model.GetInventoryControlByCodeNoAdded(INDBtnCode.Text.Trim)
                    INDLyInventoryControl.BeginUpdate()
                    If inventoryControl IsNot Nothing AndAlso inventoryControl.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(inventoryControl.Id))
                            With inventoryControl
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                DocumentDate = .DocumentDate
                                WarehouseId = .WarehouseId
                                INDSleWarehouse.Properties.NullText = .DescriptionWarehouse
                                DocumentDate = .DocumentDate
                                ControlType = .DocumentType
                                INDsleAdmissionNumber.SetEditValue = .AdmissionNumber
                                Me.BarraBotones.StatusRecord = .Status.ToString()

                                Me.BarraBotones.StatusRecordVisible = True
                                If .Import AndAlso .Status = 2 Then
                                    INDColProduct.FieldName = "ProductId.CodeName"
                                    datasourceImporFile = Model.ListInventoryControlDetailByInventoryControlId(Me.inventoryControl.Id)
                                Else
                                    INDColProduct.FieldName = "ProductCodeName"
                                    ListInventoryControlDetail = Await Model.GetInventoryControlDetailByInventoryControlId(.Id)
                                End If
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.inventoryControl.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = inventoryControl.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            If Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)

                                OnlyRead = False
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.ReadOnlyControls(True)
                                OnlyRead = True
                                INDPccBatch.Enabled = True
                                LayoutControl1.Enabled = True
                                LayoutControlGroup2.Enabled = True
                                INDGcBatch.Enabled = True
                                For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
                                    If col.Name = "INDColBatchSerial" Then
                                        col.OptionsColumn.AllowEdit = True
                                    End If
                                Next
                            End If
                            Me.BarraBotones.SetDocuments(inventoryControl.Id, Me.Tag.ToString(), Nothing, GetType(InventoryControl).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If inventoryControl.Status > 1 Then
                                INDBtnAddProducts.Enabled = False
                                INDBtnImportFile.Enabled = False
                            Else
                                INDBtnAddProducts.Enabled = True
                                INDBtnImportFile.Enabled = True
                            End If
                            INDDteDocumentDate.Focus()
                            If Me.inventoryControl.Import AndAlso Me.inventoryControl.Status = 2 Then
                                INDGcProducts.DataSource = Nothing
                                IndigoGridControl1.AcceptXPO = True
                                INDGcProducts.RefreshDataSource()
                                INDGcProducts.DataSource = datasourceImporFile
                            Else
                                INDGcProducts.DataSource = ListInventoryControlDetail
                            End If
                            INDBtnPhysicalInventory.Enabled = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, inventoryControl.Id, 0, inventoryControl.Id)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewInventoryControl()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDLyInventoryControl.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.inventoryControl IsNot Nothing AndAlso Me.inventoryControl.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Método que toma el retorno del agregar productos, para asignarlos al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddProduct(sender As Object, e As AddProductInventoryControlEventArg)
        If ListInventoryControlDetail Is Nothing Then
            ListInventoryControlDetail = New List(Of InventoryControlDetail)
        End If
        If inventoryControl.InventoryControlDetail Is Nothing Then
            inventoryControl.InventoryControlDetail = New Domain.Entities.TrackableCollection(Of InventoryControlDetail)
        End If
        If e.EditMode Then
            ListInventoryControlDetail.Remove(ItemInventoryControlDetail)
            ListInventoryControlDetail.Insert(indexEditRecord, e.inventoryControlDetail)

            inventoryControl.InventoryControlDetail.Remove(ItemInventoryControlDetail)
            inventoryControl.InventoryControlDetail.Insert(indexEditRecord, e.inventoryControlDetail)
        Else
            ListInventoryControlDetail.Add(e.inventoryControlDetail)
            inventoryControl.InventoryControlDetail.Add(e.inventoryControlDetail)
        End If

        INDGleControlType.Properties.ReadOnly = True
        INDSleWarehouse.Properties.ReadOnly = True

        INDGcProducts.DataSource = ListInventoryControlDetail
        INDGcProducts.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As String
        Dim errors As New StringBuilder
        If Code Is Nothing Then
            errors.AppendLine(INDLyBtnCode.Text + ResourceManager.GetString("Empty"))
        End If
        If ControlType = 0 Then
            errors.AppendLine(INDLyGleControlType.Text + ResourceManager.GetString("Empty"))
        End If
        If WarehouseId = 0 Then
            errors.AppendLine(INDLySleWarehouse.Text + ResourceManager.GetString("Empty"))
        End If
        If DocumentDate Is Nothing Then
            errors.AppendLine(INDLyDteDocumentDate.Text + ResourceManager.GetString("Empty"))
        End If
        If ListInventoryControlDetail Is Nothing OrElse ListInventoryControlDetail.Count = 0 Then
            errors.AppendLine(ResourceManager.GetString("AddProductsInventoryControl", NAME_MODULE))
        End If
        If CDate(INDDteDocumentDate.EditValue).Year <> parameter.Year OrElse CDate(INDDteDocumentDate.EditValue).Month <> parameter.Month Then
            errors.AppendLine(ResourceManager.GetString("DateTimeInventorySettingsNoMatch", NAME_MODULE))
        End If
        'If ControlType = 3 AndAlso AdmissionNumber Is Nothing Then
        If ControlType = 3 AndAlso String.IsNullOrEmpty(AdmissionNumber) Then
            errors.AppendLine("Seleccione un ingreso")
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Guarda un registro de inventoryControl
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If inventoryControl IsNot Nothing AndAlso inventoryControl.Status < 3 Then
            Dim errors = ValidateControls()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            AssigningValues()
            Try
                Using Model As New MInventoryControl(MyTag)
                    AsyncLoader(True)
                    Dim result = Await Model.SaveInventoryControl(inventoryControl, _idCurrentSequence)

                    If result.StateResult = True Then
                        If inventoryControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            If inventoryControl.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        End If

                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDBtnCode.Enabled = False
                        If result.StateResult = False And result.StateResultAux = False Then
                            Mensaje(EeventViewerImages.MensajeError) = result.Message
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                        If inventoryControl.Id > 0 Then
                            Dim ic = Await Model.GetInventoryControl(Code)
                            inventoryControl = ic.ObjectEmbbeded
                        Else
                            inventoryControl = New InventoryControl
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' asigna los valroes a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With inventoryControl
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentType = ControlType
            .WarehouseId = WarehouseId
            .DocumentDate = DocumentDate
            .OperatingUnitId = _idOperativeUnit
            .AdmissionNumber = AdmissionNumber
            If anular Then
                .Status = 3
            Else
                .Status = 1
            End If

            .InventoryControlDetail.Clear()
            For Each item In ListInventoryControlDetail
                .InventoryControlDetail.Add(item)
            Next
            For Each item In ListDeleteInventoryControlDetail
                .InventoryControlDetail.Add(item)
            Next

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Guardar/Actualiza y confirma el documento de inventory control
    ''' </summary>
    ''' <param name="actions"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function SaveOrUpdateAndConfirm(actions As Integer) As Task
        Dim errors = ValidateControls()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Function
        End If
        AssigningValues()
        Try
            Using model As New MInventoryControl(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmInventoryControl(inventoryControl, _idCurrentSequence, actions)
                AsyncLoader(False)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                ElseIf result.StateResult = False Then
                    INDBtnCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    If inventoryControl.Id > 0 Then
                        Dim res = Await model.GetInventoryControl(Code)
                        inventoryControl = res.ObjectEmbbeded
                    Else
                        inventoryControl = New InventoryControl()
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Function

    Private Async Function ImportFile() As Task
        If ControlType Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe indicar el tipo de control a realizar"
            Exit Function
        End If
        If WarehouseId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un almacén"
            Exit Function
        End If
        If DocumentDate Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe indicar la fecha del documento"
            Exit Function
        End If
        If ListInventoryControlDetail IsNot Nothing AndAlso ListInventoryControlDetail.Count > 0 Then
            If MessageIndigo.Show("Se perderan los datos que estan en la rejilla, desea continuar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
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

        Try
            'obtengo la rura del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                Exit Function
            End If

            If MessageIndigo.Show("Desea continuar con el proceso de importación", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                ListInventoryControlDetail = New List(Of InventoryControlDetail)
                INDGcProducts.DataSource = Nothing
                INDGcProducts.DataSource = ListInventoryControlDetail

                Await Me.LoadImportFile(myStream)

                'si existen errores informamos al usurio y el proceso no continua
                If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                    ListInventoryControlDetail = New List(Of InventoryControlDetail)
                    INDGcProducts.DataSource = Nothing
                    INDGcProducts.DataSource = ListInventoryControlDetail

                    Mensaje(EeventViewerImages.MensajeError) = "El proceso presento los siguientes errores"
                    Using formulario As New FrmListErrors(listErrosImportFile)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End Using
                Else
                    INDGcProducts.DataSource = Nothing
                    INDGcProducts.DataSource = ListInventoryControlDetail

                    AsyncLoader(True)
                    inventoryControl = New InventoryControl
                    With inventoryControl
                        .DocumentType = ControlType
                        .WarehouseId = WarehouseId
                        .DocumentDate = DocumentDate
                        .Import = True
                        .OperatingUnitId = _idOperativeUnit
                    End With

                    If ListInventoryControlDetail IsNot Nothing AndAlso ListInventoryControlDetail.Count > 0 Then
                        For Each item In ListInventoryControlDetail
                            inventoryControl.InventoryControlDetail.Add(item)
                        Next
                    End If
                    AsyncLoader(False)
                End If
            End If
        Catch Ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(Ex)
        End Try
    End Function

    Private Function LoadImportFile(myStream As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         listErrosImportFile = New List(Of String)
                                         Dim ssc = New XtraSpreadsheet.SpreadsheetControl()
                                         ssc.AllowDrop = False
                                         ssc.LoadDocument(myStream)

                                         Dim workBook As IWorkbook = ssc.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex = 0 Then
                                             listErrosImportFile.Add("No se encontraron registros en el archivo")
                                             Exit Sub
                                         End If

                                         progress = New CtrProgress
                                         progress.SetInfoFunction(AddressOf getInfo)
                                         progress.PrintInfo()
                                         progress.Dock = DockStyle.Fill
                                         AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
                                         AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))

                                         totalProcessedItems = 0
                                         totalItems = rows.LastUsedIndex
                                         Dim indexSend = 0
                                         progress.SafeInvoke(Sub(x)
                                                                 x.SetTitle = "Registros Procesados"
                                                                 x.PrintInfo()
                                                             End Sub)

                                         Using trasparent = New FrmTransparent(Nothing, False)
                                             trasparent.SafeInvoke(Sub(f) f.ShowDialog())
                                             While (totalItems + 1) > totalProcessedItems
                                                 Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                                                 indexSend = totalProcessedItems
                                                 totalProcessedItems += quantityDetailsToProcess

                                                 listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                                 SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)

                                                 Using model As New MInventoryControl(Me.Tag.ToString())
                                                     Dim result = model.SetProductsInventoryControlImportFile(listRows.ToList(), WarehouseId, ControlType, DocumentDate, _idOperativeUnit)
                                                     If result.StateResult Then
                                                         If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Any() Then
                                                             listErrosImportFile.AddRange(result.MessageResult)
                                                         End If
                                                         If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                                                             If ListInventoryControlDetail IsNot Nothing AndAlso ListInventoryControlDetail.Count > 0 Then
                                                                 For Each detail In result.ObjectEmbbeded
                                                                     Dim inventoryControlDetail = ListInventoryControlDetail.Where(Function(icd) icd.ProductId = detail.ProductId).FirstOrDefault()
                                                                     If inventoryControlDetail Is Nothing Then
                                                                         ListInventoryControlDetail.Add(detail)
                                                                     Else
                                                                         inventoryControlDetail.Quantity += detail.Quantity
                                                                         For Each detailBatchSerial In detail.InventoryControlDetailBatchSerial
                                                                             Dim inventoryControlDetailBatchSerial = inventoryControlDetail.InventoryControlDetailBatchSerial.Where(Function(icdbs) icdbs.BatchSerialId.Equals(detailBatchSerial.BatchSerialId)).FirstOrDefault()
                                                                             If inventoryControlDetailBatchSerial Is Nothing Then
                                                                                 inventoryControlDetail.InventoryControlDetailBatchSerial.Add(New InventoryControlDetailBatchSerial With
                                                                                                                                             {
                                                                                                                                             .BatchSerialId = detailBatchSerial.BatchSerialId,
                                                                                                                                             .BatchSerialCode = detailBatchSerial.BatchSerialCode,
                                                                                                                                             .Quantity = detailBatchSerial.Quantity,
                                                                                                                                             .InventoryQuantity = detailBatchSerial.InventoryQuantity,
                                                                                                                                             .Status = detailBatchSerial.Status
                                                                                                                                             })
                                                                             Else
                                                                                 inventoryControlDetailBatchSerial.Quantity += detailBatchSerial.Quantity

                                                                                 ' Recalcular Status al sumar cantidades
                                                                                 If ControlType = 1 OrElse ControlType = 3 Then
                                                                                     inventoryControlDetailBatchSerial.Status = 0
                                                                                 Else
                                                                                     inventoryControlDetailBatchSerial.Status = If(inventoryControlDetailBatchSerial.Quantity <> inventoryControlDetailBatchSerial.InventoryQuantity, 1, 2)
                                                                                 End If
                                                                             End If
                                                                         Next
                                                                     End If
                                                                 Next
                                                             Else
                                                                 ListInventoryControlDetail = result.ObjectEmbbeded
                                                             End If
                                                         End If
                                                     Else
                                                         listErrosImportFile.Add(result.Message)
                                                     End If
                                                 End Using

                                                 progress.SafeInvoke(Sub(x) x.PrintInfo())
                                             End While
                                         End Using

                                         AdditionalControlPanel.SafeInvoke(Sub(x)
                                                                               x.Controls.Clear()
                                                                           End Sub)
                                     End Sub)
    End Function

    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function

    ''' <summary>
    ''' metodo para establecer las filas que se van a enviar a procesar
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    ''' <remarks></remarks>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(4)})
                                              End SyncLock
                                          End Sub)
    End Sub

    ''' <summary>
    ''' metodo para establecer los datos del ingreso
    ''' </summary>
    ''' <param name="record"></param>
    ''' <remarks></remarks>
    Private Sub SetAdmission(record As Object)
        If record Is Nothing Then
            Exit Sub
        End If
        admission = record
        With record
            Me.INDsleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), record.AdmissionCode.ToString().Trim(), record.PatientCode.ToString().Trim(), record.PatientName.ToString().Trim()))
            AdmissionNumber = .AdmissionCode.ToString() '.Trim()
            INDTxtAdmissionCode.Text = .AdmissionCode.ToString().Trim()
            If .AdmissionDate IsNot Nothing Then
                INDTxtAdmissionDate.Text = CDate(.AdmissionDate).ToLongDateString()
                'INDDteOrderDate.Properties.MinValue = CDate(.AdmissionDate)
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
            Select Case .TRATAESPECIA
                Case 2
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Renal"
                Case 3
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Oncológico"
                Case Else
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Ninguno"
            End Select

            Mensaje(EeventViewerImages.Informacion) = INDTxtAdmissionPopup.Text

            If .CareGroupId > 0 Then
                Using model As New Presentation.Contract.MVP.MCareGroup(MyTag)
                    Dim careGroup = model.GetCareGroupByIdSimple(.CareGroupId).ObjectEmbbeded
                    CareGroupId = careGroup.Id
                    CareGroupCodeName = careGroup.Code + " - " + careGroup.Name
                End Using
            End If

            'Se asigna el valor minimo y maximo de la fecha del documento
            'INDDteDocumentDate.Properties.MinValue = .AdmissionDate
            INDDteDocumentDate.Properties.MaxValue = GetDateServer()
        End With
    End Sub
    Private _popUpAdmissionLoaded As Boolean = False
    ''' <summary>
    ''' Consulta y carga la admisión
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RunSetAdmission() As Task
        Return Task.Factory.StartNew(Sub()
                                         Using modelServiceOrder As New Billing.MVP.MServiceOrder(Me.MyTag)
                                             Dim admissionTmp = modelServiceOrder.GetAdmissionByServiceOrder(AdmissionNumber)
                                             If Me.INDsleAdmissionNumber.InvokeRequired Then
                                                 Me.INDsleAdmissionNumber.BeginInvoke(Sub()
                                                                                          SetAdmission(admissionTmp)
                                                                                      End Sub)
                                             Else
                                                 SetAdmission(admissionTmp)
                                             End If
                                             _popUpAdmissionLoaded = True
                                             If admissionTmp IsNot Nothing Then
                                                 ValidatePatientThirdParty(admissionTmp.PatientCode)
                                             End If
                                         End Using
                                     End Sub)
    End Function
    ''' <summary>
    ''' Validates the patient third party.
    ''' </summary>
    ''' <param name="patientCode">The patient code.</param>
    ''' <returns></returns>
    Private Function ValidatePatientThirdParty(patientCode As String) As Boolean
        If Not String.IsNullOrEmpty(patientCode.Trim()) Then
            Using model As New MThirdParty(Me.Tag)
                Dim third As Domain.Entities.ThirdParty = model.GetThirdParty(patientCode.Trim())
                If third IsNot Nothing AndAlso third.Id > 0 Then
                    _thirdPartyPatientId = third.Id
                Else
                    Return False
                End If
            End Using
        Else
            Return False
        End If
        Return True
    End Function

#End Region

#Region "Handless"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        inventoryControl = Nothing
        ListInventoryControlDetail = Nothing
        ListDeleteInventoryControlDetail = Nothing
        record = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        OnlyRead = Nothing
        parameter = Nothing
        ItemInventoryControlDetail = Nothing
        indexEditRecord = Nothing
        anular = Nothing
        myStream = Nothing
        datasourceImporFile = Nothing
        listErrosImportFile = Nothing
        progress = Nothing
        totalItems = Nothing
        totalProcessedItems = Nothing
        rows = Nothing
        listRows = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInventoryControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        INDEsbProduct.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Producto"},
                            New ExcelColumn With {.Name = "Lote", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Fecha Vencimiento"},
                            New ExcelColumn With {.Name = "Cantidad"}
                        }
                    })

        IndigoGridView1.SetListAcction(INDGvProducts, {eAcciones.Edit, eAcciones.Remove}.ToList)
        IndigoGridControl1.RefreshGrid(INDGcProducts)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        AddHandler Me.INDsleAdmissionNumber.Search.KeyDown, AddressOf INDSleAdmissionNumber_KeyDown
        AddHandler Me.INDsleAdmissionNumber.Search.QueryPopUp, AddressOf INDSleAdmissionNumber_QueryPopUp
        INDsleAdmissionNumber.PopupContainerControl = INDPccMoreInfoAdmission
        INDsleAdmissionNumber.Search.Properties.View.GridControl.ForceInitialize()
        INDsleAdmissionNumber.Datasource = New List(Of ViewRevenueControl)
        INDsleAdmissionNumber.Search.Properties.View.PopulateColumns()
        INDsleAdmissionNumber.Search.Properties.ValueMember = "Key"
        INDsleAdmissionNumber.Search.Properties.DisplayMember = "FullNameAdmission"
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me.LayoutControls.SetIsCustomizable(Me.INDLyInventoryControl, True)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PInventoryControl(Me)
        Presenter.GetSequense()
        Presenter.LoadAdmissionNumber()
        INDGleControlType.Properties.DataSource = ListControlType

        LoadStatus()
        Deshacer()
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Evetno que se dispara cuando el formulario se activa, direge el foco al control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInventoryControl_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub
#End Region

#Region "FromClosing"
    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInventoryControl_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        If BarraBotones.OperatingUnit Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una unidad operativa válida."
            Exit Sub
        End If
        OpenSearch()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control para cargar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBtnCode.KeyDown
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
                    Await Me.NewInventoryControl()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
    ''' <summary>
    ''' Handles the KeyDown event of the INDSleAdmissionNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAdmissionNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDSleWarehouse.Focus()
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Realiza la consulta del combo de almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            Presenter.LoadListWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Evento que asigna el datasource al control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleControlType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDGleControlType.QueryPopUp
        If INDGleControlType.Properties.DataSource Is Nothing Then
            INDGleControlType.Properties.DataSource = ListControlType
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDSleAdmissionNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAdmissionNumber_QueryPopUp(sender As Object, e As CancelEventArgs)
        'INDsleAdmissionNumber.SetEditValue = AdmissionNumber
        INDsleAdmissionNumber.SetEditValue = AdmissionNumber
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Evento para instanciar el formulario de agregar productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDPcpAddProducts_Click(sender As Object, e As EventArgs) Handles INDBtnAddProducts.Click
        If OnlyRead = False Then
            If INDGleControlType.EditValue Is Nothing OrElse INDGleControlType.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar un Tipo de Inventario."
                Exit Sub
            End If
            If INDSleWarehouse.EditValue Is Nothing OrElse INDSleWarehouse.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar un Almacén."
                Exit Sub
            End If
            Me.Cursor = ChangeCursorIndigo()
            Using formulario As New PopupAddProductsInventoryControl(Nothing, ListInventoryControlDetail, IIf(INDGleControlType.EditValue = 2, True, False), INDSleWarehouse.EditValue)
                AddHandler formulario.AddInventoryControlDetail, AddressOf ReturnAddProduct


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
    ''' Despliega el formulario informativo de inventario fisico que contiene el almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnPhysicalInventory_Click(sender As Object, e As EventArgs) Handles INDBtnPhysicalInventory.Click
        If WarehouseId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar un Almacén."
            Exit Sub
        End If
        If (ControlType IsNot Nothing AndAlso ControlType = 3 And String.IsNullOrEmpty(AdmissionNumber)) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar un ingreso."
            Exit Sub
        End If
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New PopupPhysicalInventory(AdmissionNumber, WarehouseId)
            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.Size = New Size(800, 700)
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Await ImportFile()
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que dispone del control de inventario fisico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        If INDSleWarehouse.EditValue Is Nothing Then
            INDBtnImportFile.Enabled = False
            INDLyBtnPhysicalInventory.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        Else
            If INDGleControlType.EditValue IsNot Nothing And INDDteDocumentDate.EditValue IsNot Nothing Then
                INDBtnImportFile.Enabled = True
            End If
            INDLyBtnPhysicalInventory.Visibility = XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Dependiendo del valor si es Saldo inicial o Inventario fisico muestra la columan
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleControlType_EditValueChanged(sender As Object, le As EventArgs) Handles INDGleControlType.EditValueChanged
        INDSleWarehouse.Properties.DataSource = Nothing
        If INDGleControlType.EditValue Is Nothing Then
            INDBtnImportFile.Enabled = False
            Exit Sub
        End If
        If INDSleWarehouse.EditValue IsNot Nothing And INDDteDocumentDate.EditValue IsNot Nothing Then
            INDBtnImportFile.Enabled = True
        End If
        If ControlType.HasValue Then
            If ControlType.GetValueOrDefault = 3 Then
                LciAdmissionNumber.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDsleAdmissionNumber.SetEditValue = Nothing
                LciAdmissionNumber.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
        'If INDGleControlType.EditValue = 1 Then
        '    ColPhysicalInventory.Visible = False
        'Else
        '    ColPhysicalInventory.Visible = True
        '    ColPhysicalInventory.VisibleIndex = 3
        'End If
    End Sub

    Private Sub INDDteDocumentDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteDocumentDate.EditValueChanged
        If INDDteDocumentDate.EditValue IsNot Nothing Then
            If INDSleWarehouse.EditValue IsNot Nothing And INDGleControlType.EditValue IsNot Nothing Then
                INDBtnImportFile.Enabled = True
            End If
        Else
            INDBtnImportFile.Enabled = False
        End If
    End Sub

    ' <summary>
    ' Evento utilziado para valdiar la fecha con la fecha de los parametros del modulo
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    'Private Sub INDDteDocumentDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteDocumentDate.EditValueChanged
    '    If INDDteDocumentDate.EditValue Is Nothing OrElse parameter Is Nothing Then
    '        Exit Sub
    '    End If

    '    If CDate(INDDteDocumentDate.EditValue).Year <> parameter.Year OrElse CDate(INDDteDocumentDate.EditValue).Month <> parameter.Month Then
    '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddProductsInventoryControl", NAME_MODULE)
    '    End If
    'End Sub
#End Region

#Region "Click_ButtonAction"
    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos del comprobante cuando se acciona el boton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        ItemInventoryControlDetail = DirectCast(INDGvProducts.GetFocusedRow(), InventoryControlDetail)
        Select Case button.Tag.ToString
            Case "Edit"
                EditItem(ItemInventoryControlDetail)
            Case "Remove"
                RemoveItem(ItemInventoryControlDetail)
            Case Else
                Exit Select
        End Select
    End Sub

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos del comprobante cuando se muestra el menu de click derecho
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim action = sender.ToString
        ItemInventoryControlDetail = DirectCast(INDGvProducts.GetFocusedRow(), InventoryControlDetail)
        Select Case action
            Case "Editar"
                EditItem(ItemInventoryControlDetail)
            Case "Eliminar"
                RemoveItem(ItemInventoryControlDetail)
        End Select
    End Sub

    ''' <summary>
    ''' metodo ejecutado cuando se edita un item de la rejilla
    ''' </summary>
    Public Sub EditItem(ItemInventoryControlDetail As InventoryControlDetail)
        indexEditRecord = ListInventoryControlDetail.IndexOf(ItemInventoryControlDetail)
        Using formulario As New PopupAddProductsInventoryControl(ItemInventoryControlDetail, ListInventoryControlDetail, IIf(INDGleControlType.EditValue = 2, True, False), INDSleWarehouse.EditValue)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddInventoryControlDetail, AddressOf ReturnAddProduct
            formulario.Size = New Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.EditMode = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' metodo ejecutado cuando se elimina un item de la rejilla
    ''' </summary>
    Public Sub RemoveItem(ItemInventoryControlDetail As InventoryControlDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If ItemInventoryControlDetail.Id > 0 Then
                If ListDeleteInventoryControlDetail Is Nothing Then
                    ListDeleteInventoryControlDetail = New List(Of InventoryControlDetail)
                End If
                While ItemInventoryControlDetail.InventoryControlDetailBatchSerial.Count > 0
                    If ItemInventoryControlDetail.InventoryControlDetailBatchSerial(0).Id > 0 Then
                        ItemInventoryControlDetail.InventoryControlDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        ItemInventoryControlDetail.InventoryControlDetailBatchSerial.Remove(ItemInventoryControlDetail.InventoryControlDetailBatchSerial(0))
                    End If
                End While
                ItemInventoryControlDetail.MarkAsDeleted()
                ListDeleteInventoryControlDetail.Add(ItemInventoryControlDetail)
            End If
            ListInventoryControlDetail.Remove(ListInventoryControlDetail.Find(Function(x) x.ProductId = ItemInventoryControlDetail.ProductId))
            If ListInventoryControlDetail.Count = 0 AndAlso inventoryControl.Id = 0 Then
                INDSleWarehouse.Properties.ReadOnly = False
                INDGleControlType.Properties.ReadOnly = False
            End If
            INDGcProducts.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDPceBatch_Popup(sender As Object, e As EventArgs) Handles INDPceBatch.Popup
        If Me.inventoryControl.Status = 2 AndAlso Me.inventoryControl.Import Then
            Dim product = DirectCast(DirectCast(INDGvProducts.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.InventoryControlDetailXpo)
            Using model As New MInventoryControl(MyTag)
                INDColBatch.FieldName = "BatchSerialId.BatchCode"
                INDGcBatch.DataSource = model.ListInventoryControlDetailBatchSerialByInventoryControlDetailId(product.Id)
            End Using
        Else
            INDColBatch.FieldName = "BatchSerialCode"
            Dim product = DirectCast(INDGvProducts.GetFocusedRow, InventoryControlDetail)
            INDGcBatch.DataSource = Nothing
            INDGcBatch.DataSource = product.InventoryControlDetailBatchSerial
        End If
    End Sub
#End Region

#Region "NewSelectedValue"
    Private Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDsleAdmissionNumber.NewSelectedValue
        '
        'If e.AdmissionObject IsNot Nothing AndAlso e.AdmissionObject.GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
        If e.AdmissionObject IsNot Nothing AndAlso e.AdmissionObject.GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionNumber")) Then
            'AdmissionNumber = e.AdmissionObject.Key
            AdmissionNumber = e.AdmissionObject.AdmissionNumber.ToString().Trim()
            If Not ValidatePatientThirdParty(e.AdmissionObject.PatientCode) Then
                'Me.Mensaje(EeventViewerImages.Advertencia) = String.Format("El paciente {0} no está creado como tercero en Indigo VIE", String.Concat(e.AdmissionObject.PatientCode.ToString().Trim(), " - ", e.AdmissionObject.PatientName.ToString().Trim()))
                Me.Mensaje(EeventViewerImages.Advertencia) = String.Format("El paciente {0} no está creado como tercero en Indigo VIE", String.Concat(e.AdmissionObject.PatientCode.ToString().Trim(), " - ", e.AdmissionObject.IPNOMCOMP.ToString().Trim()))
                INDsleAdmissionNumber.Search.EditValue = Nothing
                INDsleAdmissionNumber.SetNullText(String.Empty)
                AdmissionNumber = Nothing
                INDBtnAddProducts.Enabled = False
                'AdmissionNumber = Nothing
                Exit Sub
            End If

            'valido que el ingreso no este facturado
            Using model As New MAdmissions(MyTag)
                'Dim admissionTmp = model.GetAdmissionsByCodeSimple(e.AdmissionObject.AdmissionCode.ToString().Trim()).ObjectEmbbeded
                Dim admissionTmp = model.GetAdmissionsByCodeSimple(e.AdmissionObject.AdmissionNumber.ToString().Trim()).ObjectEmbbeded
                If admissionTmp.IESTADOIN = "F" Then
                    Mensaje(EeventViewerImages.Advertencia) = "El ingreso esta facturado"
                    INDsleAdmissionNumber.Search.EditValue = Nothing
                    INDsleAdmissionNumber.SetNullText(String.Empty)
                    AdmissionNumber = Nothing
                    INDBtnAddProducts.Enabled = False
                    'AdmissionNumber = Nothing
                    Exit Sub
                End If
            End Using
        End If

        INDBtnAddProducts.Enabled = True
        'SetAdmission(e.AdmissionObject)

        RunSetAdmission()
        'ctrTmp.PrintInfo()
    End Sub

    Private Sub INDsleAdmissionNumber_MouseEnterAdmission(sender As Object, e As EventArgs) Handles INDsleAdmissionNumber.MouseEnterAdmission
        If admission IsNot Nothing Then
            INDFpAdmission.ShowBeakForm()
        End If
    End Sub
#End Region

#End Region



    Private Async Sub IndigoGridControl1_PasteToGrid(sender As XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If e.Rows.Count > 300 Then
            Mensaje(EeventViewerImages.Advertencia) = "Para procesar esta cantidad de información se debe hacer por medio de importación de archivos"
            Exit Sub
        End If
        If WarehouseId Is Nothing Or ControlType Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un almacen y un tipo para pegar información en la rejilla"
            Exit Sub
        End If
        INDGvProducts.ShowLoadingPanel()
        Me.Cursor = ChangeCursorIndigo()
        Using model As New MInventoryControl(MyTag)
            Dim result = Await model.SetProductsInventoryControlCopyPaste(e.Rows, WarehouseId, ControlType)
            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                If ListInventoryControlDetail IsNot Nothing AndAlso ListInventoryControlDetail.Count > 0 Then
                    Dim ListInventoryControlDetailTmp = (From s In result.ObjectEmbbeded Select s.ProductId).ToList.Distinct.ToList()
                    Dim ListInventoryControlDetailNotExist As New List(Of Integer)
                    For i As Integer = 0 To ListInventoryControlDetailTmp.Count - 1 Step 1
                        Dim item = i
                        Dim detail = ListInventoryControlDetail.Where(Function(x) x.ProductId = ListInventoryControlDetailTmp.Item((item))).FirstOrDefault()
                        If detail IsNot Nothing Then
                            Dim detailTmp = result.ObjectEmbbeded.Find(Function(x) x.ProductId = ListInventoryControlDetailTmp.Item((item)))
                            For Each itemBatch In detailTmp.InventoryControlDetailBatchSerial
                                'si el item no maneja lote agrego un error porque el producto esta repetido
                                If itemBatch.BatchSerialId Is Nothing Then
                                    result.MessageResult.Add("El producto " + detail.ProductCodeName + " ya esta agregado")
                                    Exit For
                                End If
                                'si el item maneja lote busco si ya lo tiene agregado, sino el item se agrega al producto queya esta en el listado
                                Dim batchExists = detail.InventoryControlDetailBatchSerial.Where(Function(x) x.BatchSerialId = itemBatch.BatchSerialId).FirstOrDefault()
                                If batchExists Is Nothing Then
                                    detail.InventoryControlDetailBatchSerial.Add(itemBatch.Clone())
                                Else
                                    result.MessageResult.Add("El producto " + detail.ProductCodeName + " ya tiene agregado el lote " + itemBatch.BatchSerialCode)
                                End If
                            Next

                        Else
                            ListInventoryControlDetailNotExist.Add(ListInventoryControlDetailTmp.Item((item)))
                        End If
                    Next
                    For i As Integer = 0 To ListInventoryControlDetailNotExist.Count - 1 Step 1
                        Dim item = i
                        ListInventoryControlDetail.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.ProductId = ListInventoryControlDetailNotExist.Item(item)))
                    Next
                Else
                    ListInventoryControlDetail = result.ObjectEmbbeded
                End If
            ElseIf Not String.IsNullOrEmpty(result.Message) Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
            If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDGvProducts.HideLoadingPanel()
            INDGcProducts.DataSource = Nothing
            INDGcProducts.DataSource = ListInventoryControlDetail
        End Using
    End Sub

    Private Sub INDsleAdmissionNumber_SearchOpenPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdmissionNumber.SearchOpenPopUp
        INDsleAdmissionNumber.Search.Properties.View.Columns(1).MaxWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(1).MinWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(2).MaxWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(2).MinWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(0).Visible = False
        INDsleAdmissionNumber.Search.Properties.View.Columns(4).Visible = False
        INDsleAdmissionNumber.Search.Properties.View.Columns(5).Visible = False
        INDsleAdmissionNumber.Search.Properties.View.Columns(1).Caption = "No. Ingreso"
        INDsleAdmissionNumber.Search.Properties.View.Columns(2).Caption = "Identificación"
        INDsleAdmissionNumber.Search.Properties.View.Columns(3).Caption = "Paciente"
    End Sub
End Class
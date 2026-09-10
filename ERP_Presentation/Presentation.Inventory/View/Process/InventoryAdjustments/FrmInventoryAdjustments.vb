'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 25-08-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports DevExpress.Xpo
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Common
Imports System.Windows.Forms
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.BillingRepository
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Accounting
Imports DevExpress
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports Presentation.Maintenance
Imports DevExpress.Spreadsheet
Imports Presentation.Controls.MVP



#End Region

Public Class FrmInventoryAdjustments
    Implements IInventoryAdjustments, ICustomizableForm

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
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PInventoryAdjustments

    ''' <summary>
    ''' Representa el Modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MInventoryAdjustments

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Objeto que representa los parametros del módulo
    ''' </summary>
    ''' <remarks></remarks>
    Dim parameter As SettingInventory

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' flag para solo lectura
    ''' </summary>
    ''' <remarks></remarks>
    Dim OnlyRead As Boolean = False

    ''' <summary>
    ''' Flag para evento load
    ''' </summary>
    ''' <remarks></remarks>
    Dim flagLoad As Boolean = False

    'Flag para Anular
    Dim flagAnullar As Boolean

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemInventoryAdjustmentDetail As InventoryAdjustmentDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' Variable que representa la entidad principal
    ''' </summary>
    ''' <remarks></remarks>
    Dim inventoryAdjustments As InventoryAdjustment

    ''' <summary>
    ''' Variable que representa la entidad de conceptos de ajuste de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Dim adjustmentConcept As AdjustmentConcept

    ''' <summary>
    ''' Variable que representa la entidad de control de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Dim inventoryControl As InventoryControl

    ''' <summary>
    ''' Propiedad que representa el arreglo de los detalles de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property listInventoryAdjustmentsDetail As List(Of InventoryAdjustmentDetail)

    ''' <summary>
    ''' listado del detalle de el ajuste de inventario para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listInventoryAdjustmentDetailDelete As List(Of InventoryAdjustmentDetail)

    ''' <summary>
    ''' Objeto que contiene el tercero
    ''' </summary>
    Dim ThirdPartyTmp As Domain.Entities.ThirdParty

#End Region

#Region "Properties"
    ''' <summary>
    ''' Establece el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInventoryAdjustments.MyLayoutControl
        Get
            Return MyTag
        End Get
    End Property

    ''' <summary>
    ''' Especifica como se va redondear
    ''' </summary>
    ''' <remarks></remarks>
    Private _listAdjustmentType As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListAdjustmentType As List(Of Tuple(Of Integer, String))
        Get
            If _listAdjustmentType Is Nothing Then
                _listAdjustmentType = New List(Of Tuple(Of Integer, String))
                _listAdjustmentType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("InventoryAdjustments_AjustmentTypeIn", NAME_MODULE)))
                _listAdjustmentType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("InventoryAdjustments_AjustmentTypeOut", NAME_MODULE)))
                _listAdjustmentType.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("InventoryAdjustments_AjustmentTypeFiscalInventory", NAME_MODULE)))
                _listAdjustmentType.Add(New Tuple(Of Integer, String)(4, "Custodia"))
            End If
            Return _listAdjustmentType
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Public Property _sequence As Domain.Entities.InventorySequence
    Public Property Sequense As InventorySequence Implements IInventoryAdjustments.Sequense
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
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Obtiene o establece los almacenes
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListWareHouse As XPInstantFeedbackSource Implements IInventoryAdjustments.ListWareHouse
        Get
            Return INDSleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los conceptos de ingreso/salida del almacen
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListConcepts As XPInstantFeedbackSource Implements IInventoryAdjustments.ListConcepts
        Get
            Return INDSleConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los centros de costo por xpo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListInventoryControl As XPInstantFeedbackSource Implements IInventoryAdjustments.ListInventoryControl
        Get
            Return INDSleInventoryControl.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleInventoryControl.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los terceros
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListThirdParty As XPInstantFeedbackSource Implements IInventoryAdjustments.ListThirdParty
        Get
            Return INDSleThirdParty.Datasource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParty.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y establece el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IInventoryAdjustments.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece los conceptos de ajuste de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdjustmentConceptId As Integer? Implements IInventoryAdjustments.AdjustmentConceptId
        Get
            Return INDSleConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Optiene o establece un codigo para el registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IInventoryAdjustments.Code
        Get
            Return INDBtnCode.EditValue
        End Get
        Set(value As String)
            INDBtnCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece un detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Detail As String Implements IInventoryAdjustments.Detail
        Get
            Return INDTxtDetail.Text
        End Get
        Set(value As String)
            INDTxtDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece una fecha de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime? Implements IInventoryAdjustments.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece un tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer? Implements IInventoryAdjustments.ThirdPartyId
        Get
            Return INDSleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDSleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establce un almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WareHouseId As Integer? Implements IInventoryAdjustments.WareHouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleWarehouse.EditValue = value
        End Set
    End Property



    ''' <summary>
    ''' Obtiene o establece el detalle de los productos agregados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListProducts As List(Of Object) Implements IInventoryAdjustments.ListProducts
        Get
            Return INDGcProducts.DataSource
        End Get
        Set(value As List(Of Object))
            INDGcProducts.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IInventoryAdjustments.Status
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property

    ''' <summary>
    ''' Habilita los controles necesarios para el funcionamiento del frontal
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInventoryAdjustments.ActionsOnControls
        Set(value As Boolean)
            INDLyInventoryAdjustments.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDDteDocumentDate.Enabled = value
            INDSleWarehouse.Enabled = value
            INDGleAdjustmentType.Enabled = value
            INDGcProducts.Enabled = value

            INDSleThirdParty.Enabled = value
            INDTxtDetail.Enabled = value
            INDSleConcept.Enabled = value
            INDSleCostCenter.Enabled = value
            INDLyInventoryAdjustments.EndUpdate()

            INDsleAdmissionNumber.Enabled = value
            INDsleAdmissionNumber.Search.Enabled = value

            If value Then
                INDGleAdjustmentType.Focus()
            Else
                INDBtnCode.Focus()
            End If

        End Set
    End Property

#Region "Datasource Entity"
    ''' <summary>
    ''' Datasource de Ingreso del Paciente
    ''' </summary>
    Public Property AdmissionNumberDatasource As XPInstantFeedbackSource Implements IInventoryAdjustments.AdmissionNumberDatasource
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
    Public Property AdmissionNumber As String Implements IInventoryAdjustments.AdmissionNumber
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

    ''' <summary>
    ''' Listado que contiene los detalles de control de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListInventoryControlDetail As Domain.Entities.TrackableCollection(Of InventoryControlDetailBatchSerial)
        Get
            Return INDGcPhysicalInventory.DataSource
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of InventoryControlDetailBatchSerial))
            INDGcPhysicalInventory.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el inventorycontrol
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property InventoryControlId As Integer? Implements IInventoryAdjustments.InventoryControlId
        Get
            Return INDSleInventoryControl.EditValue
        End Get
        Set(value As Integer?)
            INDSleInventoryControl.EditValue = value
        End Set
    End Property

    Public Property AdjustmentType As Integer? Implements IInventoryAdjustments.AdjustmentType

        Get
            Return INDGleAdjustmentType.EditValue
        End Get
        Set(value As Integer?)
            INDGleAdjustmentType.EditValue = value
        End Set

    End Property


#End Region

#Region "BarButtons"

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

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que deshacer los cambios en el form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botnes que abre el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que inicia un nuevo registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que Guarda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar, BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que Guarda y Confirma el Documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        BarraBotones.Focus()
        Await SaveOrUpdateAndConfirm(1)
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que Actualzia y Confirma el Documento
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
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, inventoryAdjustments.Id, 0, {inventoryAdjustments.Id, INDGleAdjustmentType.EditValue})
    End Sub
#End Region

#Region "Methods"
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
    ''' Método asignado de la barra de botones que despliega el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim listAdjustmentType As New List(Of Tuple(Of String, Byte))
        listAdjustmentType.Add(New Tuple(Of String, Byte)("Entrada", 1))
        listAdjustmentType.Add(New Tuple(Of String, Byte)("Salida", 2))
        listAdjustmentType.Add(New Tuple(Of String, Byte)("Inventario Físico", 3))
        listAdjustmentType.Add(New Tuple(Of String, Byte)("Custodia", 4))

        Dim listStatus As New List(Of Tuple(Of String, Byte))
        listStatus.Add(New Tuple(Of String, Byte)("Registrado", 1))
        listStatus.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        listStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Tipo Ajuste", .FieldName = "AdjustmentType", .ColumnWidth = 180, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listAdjustmentType},
                              New ColumnInfo With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = 80, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listStatus}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInventoryAdjustment
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.inventoryAdjustments.Code, Me.inventoryAdjustments.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.inventoryAdjustments.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.inventoryAdjustments.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.inventoryAdjustments.Code, Me.inventoryAdjustments.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.inventoryAdjustments.Code)
            Return Me._doc
        End If
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Método utilziado para realziar la busquda de los items guardads
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios realziado e inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Inicializa el formulario para realizar una nueva entrada de registro
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewInventoryAdjustment()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDLyInventoryAdjustments.BeginUpdate()
        DeleteBlockedRecord()

        Status = 0
        Code = String.Empty
        DocumentDate = Nothing
        AdjustmentType = Nothing
        INDSleThirdParty.EditValue = Nothing
        INDSleThirdParty.DisplayNullText = String.Empty
        AdmissionNumber = Nothing
        INDsleAdmissionNumber.SetEditValue = Nothing
        INDsleAdmissionNumber.SetNullText(String.Empty)
        LciAdmissionNumber.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        Detail = Nothing

        INDSleConcept.EditValue = Nothing
        INDSleConcept.Properties.NullText = String.Empty
        INDSleCostCenter.EditValue = Nothing
        INDSleCostCenter.Text = String.Empty
        INDLySleCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSleWarehouse.EditValue = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty

        INDSleInventoryControl.EditValue = Nothing
        INDSleInventoryControl.Properties.NullText = String.Empty
        INDTxtWarehouseInventoryControl.Text = String.Empty
        INDDteDocumentDateInventoryControl.EditValue = Nothing

        _searchMode = False
        OnlyRead = False
        flagLoad = False
        flagAnullar = False

        INDGcProducts.DataSource = Nothing

        Me._doc = Nothing
        inventoryControl = Nothing
        inventoryAdjustments = Nothing
        ListInventoryControlDetail = Nothing
        listInventoryAdjustmentsDetail = Nothing
        listInventoryAdjustmentDetailDelete = Nothing

        Me.ValidateDate()
        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        LyGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LyGroupProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LyInventoryControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LyInventoryControlProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDBtnCode.Focus()
        INDLyInventoryAdjustments.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Genera una nueva entidad de inventory adjustments
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function NewInventoryAdjustment() As Task
        If Me.parameter Is Nothing OrElse Me.parameter.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
            Exit Function
        End If

        INDDteDocumentDate.Properties.MaxValue = GetDateServer()
        inventoryAdjustments = New InventoryAdjustment()
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
        INDDteDocumentDate.Focus()
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
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            parameter = Await Model.GetSettingInventory(_idOperativeUnit)
            If parameter Is Nothing OrElse parameter.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
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
        If parameter Is Nothing OrElse parameter.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(parameter.Year.ToString + "/" + parameter.Month.ToString + "/01")
        INDDteDocumentDate.Properties.MinValue = dateMin
        INDDteDocumentDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        If BarraBotones.OperatingUnit Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una Unidad Operativa válida"
            Exit Sub
        End If
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para abrir el formulario indicado para modificar o agregar items
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' metodo para instanciar el formulario de concepto de recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InstantiatePopup(Detail As InventoryAdjustmentDetail, ListValidation As List(Of InventoryAdjustmentDetail))
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New FrmAddProductInventoryAdjustments(Detail, ListValidation, INDGleAdjustmentType.EditValue, AdmissionNumber)
            AddHandler formulario.AddInventoryAdjustmentDetail, AddressOf ReturnPopupAddProduct

            formulario.AdjustmentConcept = adjustmentConcept
            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            If WareHouseId Is Nothing Or WareHouseId = 0 Then
                Dim errors As New StringBuilder
                errors.AppendLine(INDLySleWarehouse.Text + " vacío")
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If
            formulario.WareHouseId = WareHouseId
            formulario.operatingUnitId = BarraBotones.OperatingUnitValue
            formulario.typeAdjustment = AdjustmentType
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.8
            formulario.Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.9
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método utilizado para adquirir losproductos retornados por el frontal de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupAddProduct(sender As Object, e As AddProductInventoryAdjustmentsEventArg)
        If listInventoryAdjustmentsDetail Is Nothing Then
            listInventoryAdjustmentsDetail = New List(Of InventoryAdjustmentDetail)
        End If
        If e.EditMode = True Then
            listInventoryAdjustmentsDetail.Remove(ItemInventoryAdjustmentDetail)
            listInventoryAdjustmentsDetail.Insert(indexEditRecord, e.inventoryAdjustmentDetail)
        Else
            listInventoryAdjustmentsDetail.Add(e.inventoryAdjustmentDetail)
        End If
        INDGcProducts.DataSource = Nothing
        INDGcProducts.DataSource = listInventoryAdjustmentsDetail
        INDGleAdjustmentType.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Método utilizado para guardar o actualziar la entidad de inventoryadjustment
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If inventoryAdjustments IsNot Nothing AndAlso inventoryAdjustments.Status < 3 Then
            Dim errors = ValidateControls()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MInventoryAdjustments(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveInventoryAdjustment(inventoryAdjustments, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    If inventoryAdjustments.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If inventoryAdjustments.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    inventoryAdjustments = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Create, inventoryAdjustments.Id, 0, {inventoryAdjustments.Id, INDGleAdjustmentType.EditValue})
                    _searchMode = False
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
                    If inventoryAdjustments.Id > 0 Then
                        Dim ev = Await model.GetInventoryAdjustment(Code)
                        inventoryAdjustments = ev.ObjectEmbbeded
                    Else
                        inventoryAdjustments = New InventoryAdjustment()
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
    ''' Consulta el concepto de ajuste
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function GetAdjustmentConcept() As Task
        Using Model As New MAdjustmentConcept("")
            Dim ac = Await Model.GetAdjustmentConceptById(INDSleConcept.EditValue)
            adjustmentConcept = ac.ObjectEmbbeded
        End Using
        If adjustmentConcept IsNot Nothing Then
            If adjustmentConcept.MainAccounts.HandlesCostCenter Then
                INDSleCostCenter.Text = adjustmentConcept.CostCenterCodeName
                INDSleCostCenter.Properties.ReadOnly = True
                INDLySleCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDSleCostCenter.Text = String.Empty
                INDLySleCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Else
            INDSleCostCenter.Text = String.Empty
            INDLySleCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Function

    ''' <summary>
    ''' asigna los valroes a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With inventoryAdjustments
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .DocumentDate = DocumentDate
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .AdjustmentType = AdjustmentType
            .Prefix = Me._prefixSelected
            .ThirdPartyId = ThirdPartyId
            .Description = Detail
            .AdmissionNumber = AdmissionNumber
            If .AdjustmentType = 4 Then
                .ThirdPartyId = Me._thirdPartyPatientId
            End If

            If .AdjustmentType = 3 Then
                .InventoryControlId = InventoryControlId
            Else
                .AdjustmentConceptId = AdjustmentConceptId
                .WarehouseId = WareHouseId
            End If

            .InventoryAdjustmentDetail.Clear()
            If listInventoryAdjustmentsDetail IsNot Nothing Then
                For Each item In listInventoryAdjustmentsDetail
                    .InventoryAdjustmentDetail.Add(item)
                Next
            End If
            If listInventoryAdjustmentDetailDelete IsNot Nothing Then
                For Each item In listInventoryAdjustmentDetailDelete
                    .InventoryAdjustmentDetail.Add(item)
                Next
            End If

            If ListInventoryControlDetail IsNot Nothing Then
                For Each _inventoryControlDetailBatchSerial In ListInventoryControlDetail
                    Dim _inventoryAdjustmentsControl = .InventoryAdjustmentControl.Where(Function(i) i.InventoryControlDetailBatchSerialId = _inventoryControlDetailBatchSerial.Id).FirstOrDefault()
                    If _inventoryAdjustmentsControl Is Nothing Then
                        If Not _inventoryControlDetailBatchSerial.Selected Then
                            Continue For
                        End If

                        _inventoryAdjustmentsControl = New InventoryAdjustmentControl
                        .InventoryAdjustmentControl.Add(_inventoryAdjustmentsControl)
                    Else
                        If Not _inventoryControlDetailBatchSerial.Selected Then
                            _inventoryAdjustmentsControl.MarkAsDeleted()
                        End If
                    End If

                    Dim QuantityAdjustment = _inventoryControlDetailBatchSerial.Quantity - _inventoryControlDetailBatchSerial.InventoryQuantity
                    Dim AdjustmentType = IIf(QuantityAdjustment > 0, 1, 2)
                    _inventoryAdjustmentsControl.InventoryControlDetailBatchSerialId = _inventoryControlDetailBatchSerial.Id
                    _inventoryAdjustmentsControl.AdjustmentType = AdjustmentType
                    _inventoryAdjustmentsControl.QuantityAdjustment = Math.Abs(QuantityAdjustment)
                Next
            End If

            If flagAnullar = False Then
                .Status = 1
            Else
                .Status = 3
            End If
        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As String
        Dim errors As New StringBuilder
        If BarraBotones.OperatingUnit Is Nothing Then
            errors.AppendLine("Debe seleccionar una Unidad Operativa válida")
        End If
        If Code Is Nothing Then
            errors.AppendLine(INDLyBtnCode.Text + ResourceManager.GetString("Empty"))
        End If
        If DocumentDate Is Nothing Then
            errors.AppendLine(INDLyDteDocumentDate.Text + ResourceManager.GetString("Empty"))
        End If
        If AdjustmentType Is Nothing Then
            errors.AppendLine(INDLyGleAdjustmentType.Text + ResourceManager.GetString("Empty"))
        End If
        If AdjustmentType <> 4 AndAlso (ThirdPartyId = 0 OrElse ThirdPartyId Is Nothing) Then
            errors.AppendLine(INDlciThirdParty.Text + ResourceManager.GetString("Empty"))
        End If
        If Detail Is Nothing OrElse Detail = String.Empty Then
            errors.AppendLine(INDLyTxtDetail.Text + ResourceManager.GetString("Empty"))
        End If

        Select Case AdjustmentType
            Case 0
                errors.AppendLine(INDLyGleAdjustmentType.Text + ResourceManager.GetString("Empty"))
            Case 3
                If InventoryControlId = 0 Then
                    errors.AppendLine(INDLySleInventoryControl.Text + ResourceManager.GetString("Empty"))
                End If
                If ListInventoryControlDetail Is Nothing OrElse ListInventoryControlDetail.Where(Function(i) i.Selected).Count = 0 Then
                    errors.AppendLine("Debe seleccionar los productos que va a ajustar.")
                End If
            Case 4
                If String.IsNullOrEmpty(AdmissionNumber) Then
                    errors.AppendLine("Seleccione un ingreso")
                End If
                If WareHouseId = 0 Then
                    errors.AppendLine(INDLySleWarehouse.Text + ResourceManager.GetString("Empty"))
                End If
                If listInventoryAdjustmentsDetail Is Nothing OrElse listInventoryAdjustmentsDetail.Count = 0 Then
                    errors.AppendLine(ResourceManager.GetString("AddDetailInventoryAdjustment", NAME_MODULE))
                End If
            Case Else
                If AdjustmentConceptId = 0 Then
                    errors.AppendLine(INDLySleConcept.Text + ResourceManager.GetString("Empty"))
                End If
                If WareHouseId = 0 Then
                    errors.AppendLine(INDLySleWarehouse.Text + ResourceManager.GetString("Empty"))
                End If
                If listInventoryAdjustmentsDetail Is Nothing OrElse listInventoryAdjustmentsDetail.Count = 0 Then
                    errors.AppendLine(ResourceManager.GetString("AddDetailInventoryAdjustment", NAME_MODULE))
                End If
        End Select

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Método utilizado para anular el registro
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements IcrudBase.Eliminar
        flagAnullar = True
        Guardar()
    End Sub

    ''' <summary>
    ''' Funcion que guarda/actualzia y confirma el documento
    ''' </summary>
    ''' <param name="actions"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveOrUpdateAndConfirm(actions As Integer) As Task
        Dim errors = ValidateControls()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Function
        End If
        AssigningValues()
        Try
            Using model As New MInventoryAdjustments(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmInventoryAdjustment(inventoryAdjustments, _idCurrentSequence, Me.BarraBotones.OperatingUnitValue, actions, Me._sequence)
                AsyncLoader(False)
                If result.StateResult = True And result.StateResultAux = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    inventoryAdjustments = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    _searchMode = False
                    If result.MessageResultAux IsNot Nothing AndAlso result.MessageResultAux.Count > 0 Then
                        ViewMessageValidationStock(result.MessageResultAux)
                    End If
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, inventoryAdjustments.Id, 0, {inventoryAdjustments.Id, INDGleAdjustmentType.EditValue})
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    inventoryAdjustments = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    _searchMode = False
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, inventoryAdjustments.Id, 0, {inventoryAdjustments.Id, INDGleAdjustmentType.EditValue})
                    Me.Deshacer()
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    If result.MessageResult IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If inventoryAdjustments.Id > 0 Then
                        Dim res = Await model.GetInventoryAdjustment(Code)
                        inventoryAdjustments = res.ObjectEmbbeded
                    Else
                        inventoryAdjustments = New InventoryAdjustment()
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Function

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


    ''' <summary>
    ''' Método que carga los controles de la consulta
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
                Using Model As New MInventoryAdjustments(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim result = (Await Model.GetInventoryAdjustment(INDBtnCode.Text.Trim))
                    If result IsNot Nothing Then
                        inventoryAdjustments = result.ObjectEmbbeded
                        INDLyInventoryAdjustments.BeginUpdate()
                        If inventoryAdjustments IsNot Nothing AndAlso inventoryAdjustments.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True
                            flagLoad = True
                            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                                record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(inventoryAdjustments.Id))
                                With inventoryAdjustments
                                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                    If .Status <> 1 Then
                                        INDDteDocumentDate.Properties.MinValue = .DocumentDate
                                    End If

                                    DocumentDate = .DocumentDate
                                    Me._idOperativeUnit = .OperatingUnitId
                                    BarraBotones.OperatingUnitValue = .OperatingUnitId
                                    AdjustmentType = .AdjustmentType
                                    ThirdPartyId = .ThirdPartyId
                                    INDSleThirdParty.DisplayNullText = .NameThirdParty
                                    Detail = .Description
                                    'INDsleAdmissionNumber.SetNullText(_pharmaceuticalDispensing.AdmissionNumber)
                                    'RunSetAdmission()
                                    INDsleAdmissionNumber.SetEditValue = .AdmissionNumber
                                    AdmissionNumber = .AdmissionNumber

                                    If AdjustmentType = 3 Then
                                        InventoryControlId = .InventoryControlId
                                        INDSleInventoryControl.Properties.NullText = .CodeNameInventoryControl
                                        INDTxtWarehouseInventoryControl.Text = .DescriptionWarehouse
                                        INDDteDocumentDateInventoryControl.EditValue = .DocumentDateInventoryControl
                                        Await LoadControlDetailPhysicalInventory(inventoryAdjustments.Id)
                                    ElseIf AdjustmentType = 4 Then
                                        _thirdPartyPatientId = .ThirdPartyId
                                        WareHouseId = .WarehouseId
                                        INDSleWarehouse.Properties.NullText = .DescriptionWarehouse
                                    Else
                                        AdjustmentConceptId = .AdjustmentConceptId
                                        If AdjustmentConceptId IsNot Nothing Then
                                            INDLySleConcept.ShowLayout()
                                            INDSleConcept.Properties.NullText = .CodeNameAdjustmentConcept
                                            ColConcept.HideColumn()
                                        Else
                                            INDLySleConcept.HideLayout()
                                            ColConcept.ShowColumn()
                                        End If
                                        WareHouseId = .WarehouseId
                                        INDSleWarehouse.Properties.NullText = .DescriptionWarehouse
                                    End If

                                    Me.BarraBotones.StatusRecord = .Status.ToString()
                                    Me.BarraBotones.StatusRecordVisible = True

                                    listInventoryAdjustmentsDetail = .InventoryAdjustmentDetail.ToList()
                                End With
                                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.inventoryAdjustments.Code)
                                If record.Id = 0 Then
                                    record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = inventoryAdjustments.Id})
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
                                End If

                                Me.BarraBotones.SetDocuments(inventoryAdjustments.Id, Me.Tag.ToString(), Nothing, GetType(InventoryAdjustment).Name)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                                Me.BarraBotones.PrintReport(PrintReportAction.None, inventoryAdjustments.Id, 0, {inventoryAdjustments.Id, INDGleAdjustmentType.EditValue})
                                AsyncLoader(False)
                                ActionsOnControls = True
                                INDGcProducts.DataSource = Nothing
                                INDGcProducts.DataSource = listInventoryAdjustmentsDetail
                                INDDteDocumentDate.Focus()
                            End Using
                        Else
                            AsyncLoader(False)
                            If Me._sequence.IsManual Then
                                Await Me.NewInventoryAdjustment()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                                Code = String.Empty
                                INDBtnCode.Focus()
                            End If
                        End If
                        INDLyInventoryAdjustments.EndUpdate()
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo para mostar un pop up con los mensajes de validacion por stock
    ''' </summary>
    ''' <param name="messagesValidationStock"></param>
    ''' <remarks></remarks>
    Private Sub ViewMessageValidationStock(messagesValidationStock As List(Of String))
        Using formulario As New FrmPopUpValidateStock
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Datasource = messagesValidationStock
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()

        If inventoryAdjustments.Status > 1 Then
            If inventoryAdjustments.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede editar el detalle porque el documento esta confirmado"
                Exit Sub
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede editar el detalle porque el documento esta anulado"
                Exit Sub
            End If
        End If

        If INDGleAdjustmentType.EditValue = 4 Then
            If String.IsNullOrEmpty(AdmissionNumber) Then
                Mensaje(EeventViewerImages.Informacion) = LciAdmissionNumber.Text + ResourceManager.GetString("Empty")
                Exit Sub
            End If
            If INDSleWarehouse.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Informacion) = INDLySleWarehouse.Text + ResourceManager.GetString("Empty")
                Exit Sub
            End If
        End If

        ItemInventoryAdjustmentDetail = DirectCast(INDGvProducts.GetFocusedRow(), InventoryAdjustmentDetail)

        indexEditRecord = listInventoryAdjustmentsDetail.IndexOf(ItemInventoryAdjustmentDetail)
        Using formulario As New FrmAddProductInventoryAdjustments(ItemInventoryAdjustmentDetail, listInventoryAdjustmentsDetail.ToList(), INDGleAdjustmentType.EditValue, AdmissionNumber)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddInventoryAdjustmentDetail, AddressOf ReturnPopupAddProduct
            formulario.AdjustmentConcept = adjustmentConcept
            formulario.Size = New Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.WareHouseId = WareHouseId
            formulario.operatingUnitId = BarraBotones.OperatingUnitValue
            formulario.typeAdjustment = AdjustmentType
            formulario.EditMode = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()

        If inventoryAdjustments.Status > 1 Then
            If inventoryAdjustments.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar el detalle porque el documento esta confirmado"
                Exit Sub
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar el detalle porque el documento esta anulado"
                Exit Sub
            End If
        End If
        ItemInventoryAdjustmentDetail = DirectCast(INDGvProducts.GetFocusedRow(), InventoryAdjustmentDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If ItemInventoryAdjustmentDetail.Id > 0 Then
                If listInventoryAdjustmentDetailDelete Is Nothing Then
                    listInventoryAdjustmentDetailDelete = New List(Of InventoryAdjustmentDetail)
                End If
                While ItemInventoryAdjustmentDetail.InventoryAdjustmentDetailBatchSerial.Count > 0
                    If ItemInventoryAdjustmentDetail.InventoryAdjustmentDetailBatchSerial(0).Id > 0 Then
                        ItemInventoryAdjustmentDetail.InventoryAdjustmentDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        ItemInventoryAdjustmentDetail.InventoryAdjustmentDetailBatchSerial.Remove(ItemInventoryAdjustmentDetail.InventoryAdjustmentDetailBatchSerial(0))
                    End If
                End While
                ItemInventoryAdjustmentDetail.MarkAsDeleted()
                listInventoryAdjustmentDetailDelete.Add(ItemInventoryAdjustmentDetail)
            End If
            listInventoryAdjustmentsDetail.Remove(ItemInventoryAdjustmentDetail)
            INDGcProducts.DataSource = Nothing
            INDGcProducts.DataSource = listInventoryAdjustmentsDetail
            'ctrTmp.PrintInfo()
        End If
    End Sub

    Private Async Function SearchThirdParty(ByVal nit As String) As Task(Of Domain.Entities.ThirdParty)
        Using ModelThird As New MThirdParty(MThirdParty.TAG)
            ThirdPartyTmp = Await ModelThird.GetThirdPartyAsync(nit)
            If ThirdPartyTmp IsNot Nothing AndAlso ThirdPartyTmp.Id > 0 Then
                INDTxtDetail.Focus()
            Else
                Me.INDSleThirdParty.DisplayNullText = String.Empty
                Me.INDSleThirdParty.EditValue = Nothing
                'Me.INDSleThirdParty.DisplayMember = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "El Tercero No Existe"
                INDSleThirdParty.Focus()
            End If
            Return ThirdPartyTmp
        End Using
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.inventoryAdjustments IsNot Nothing AndAlso Me.inventoryAdjustments.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Handless"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        _idOperativeUnit = Nothing
        _prefixSelected = Nothing
        Presenter = Nothing
        Model = Nothing
        _searchMode = Nothing
        parameter = Nothing
        _idCurrentSequence = Nothing
        OnlyRead = Nothing
        flagLoad = Nothing
        flagAnullar = Nothing
        ItemInventoryAdjustmentDetail = Nothing
        indexEditRecord = Nothing
        inventoryAdjustments = Nothing
        adjustmentConcept = Nothing
        inventoryControl = Nothing

        ListInventoryControlDetail = Nothing
        listInventoryAdjustmentsDetail = Nothing
        listInventoryAdjustmentDetailDelete = Nothing
        ThirdPartyTmp = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmInventoryAdjustments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ColInventoryQuantityPhysical.Caption = ResourceManager.GetString("ColInventoryQuantityPhysical", "Inventory")
        ColQuantityPhysical.Caption = ResourceManager.GetString("ColQuantityPhysical", "Inventory")
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        'IndigoGridView1.MoreInfoColunmns(INDGvProducts)
        ' Agrega a la rejilla la columna de Acciones
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvProducts, ListActions)
        IndigoGridView1.MoreInfoColunmns(INDGvProducts)
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

        Me.INDSleThirdParty.FuncQueryOnKeyEnterPressed = AddressOf Me.SearchThirdParty
        Me.INDSleThirdParty.View.OptionsView.ShowGroupPanel = False
        Me.LayoutControls.SetIsCustomizable(Me.INDLyInventoryAdjustments, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Await Me.LoadParameters()

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PInventoryAdjustments(Me)
        Presenter.GetSequense()
        Presenter.LoadAdmissionNumber()

        INDGleAdjustmentType.Properties.DataSource = ListAdjustmentType
        INDDteDocumentDate.Properties.MaxValue = GetDateServer()
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
    Private Sub FrmInventoryAdjustments_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
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
    Private Sub FrmInventoryAdjustments_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una Unidad Operativa válida"
            Exit Sub
        End If
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click para abrir el formulario y agregar o consultar nuevos almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmStores()
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click para abrir el formulario y agregar o consultar nuevos Terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmThirdParty()
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click para abrir el formulario y agregar o consultar nuevos Conceptos de ajuste de invtentario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmConceptsInventorySettings()
                OpenFormDialog(form)
            End Using
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
                    Await Me.NewInventoryAdjustment()
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

    Private Sub INDTxtDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles INDTxtDetail.KeyDown
        If e.KeyCode = Keys.Enter Then
            If LyGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleConcept.Focus()
            End If
            If LyInventoryControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleInventoryControl.Focus()
            End If
        End If
    End Sub

#End Region

#Region "QuertPopup"
    ''' <summary>
    ''' Carga los datos por xpo para traer los conceptos de ajuste de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleConcept.QueryPopUp
        If ListConcepts Is Nothing Then
            If Me.AdjustmentType = 4 Then
                INDSleConcept.Enabled = False
            Else
                Presenter.LoadListConceptsAdjustments(INDGleAdjustmentType.EditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDSleAdmissionNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAdmissionNumber_QueryPopUp(sender As Object, e As CancelEventArgs)
        INDsleAdmissionNumber.SetEditValue = Me.AdmissionNumber
    End Sub

    '''' <summary>
    '''' Despliega el formulario informativo de inventario fisico que contiene el almacen
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub INDBtnPhysicalInventory_Click(sender As Object, e As EventArgs) Handles INDBtnPhysicalInventory.Click
    '    If (AdjustmentType IsNot Nothing AndAlso AdjustmentType = 4 And RevenueControlId Is Nothing) Then
    '        Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar un ingreso."
    '        Exit Sub
    '    End If
    '    Me.Cursor = ChangeCursorIndigo()
    '    Using formulario As New PopupPhysicalInventory(RevenueControlId, WarehouseId)
    '        formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    '        formulario.StartPosition = FormStartPosition.CenterParent
    '        formulario.Size = New Size(800, 700)
    '        Dim transparent = New FrmTransparent(formulario, False)
    '        Me.Cursor = System.Windows.Forms.Cursors.Default
    '        transparent.ShowDialog(Me)
    '    End Using
    'End Sub



    ''' <summary>
    ''' Carga los datos por xpo para traer los almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If ListWareHouse Is Nothing Then
            Presenter.LoadListWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Carga los datos por xpo para traer los terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Datasource Is Nothing Then
            Presenter.LoadListThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Carga los dapos por xpo para traer los inventorycontrol por tipo invetnario fisico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleInventoryControl_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleInventoryControl.QueryPopUp
        If INDSleInventoryControl.EditValue IsNot Nothing OrElse INDSleInventoryControl.EditValue = 0 Then
            Presenter.LoadListInventoryControlByPhysical()
        End If
    End Sub
#End Region

#Region "EditValue"
    ''' <summary>
    ''' Evento para hacer control sobre el editvalue de los tiopos de ajuste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleAdjustmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAdjustmentType.EditValueChanged
        If INDGleAdjustmentType.EditValue Is Nothing Then
            Exit Sub
        End If

        ListConcepts = Nothing
        ListWareHouse = Nothing

        INDSleThirdParty.EditValue = Nothing
        INDSleThirdParty.DisplayNullText = String.Empty
        INDlciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        AdmissionNumber = Nothing
        INDsleAdmissionNumber.SetEditValue = Nothing
        INDsleAdmissionNumber.SetNullText(String.Empty)
        LciAdmissionNumber.Visibility = XtraLayout.Utils.LayoutVisibility.Never

        If AdjustmentConceptId Is Nothing Then
            INDLySleConcept.HideLayout()
            ColConcept.ShowColumn()
            ColConcept.VisibleIndex = 1
        Else
            ColConcept.HideColumn()
        End If

        INDLySleConcept.Enabled = True
        INDSleConcept.Enabled = True
        INDSleConcept.EditValue = Nothing
        INDSleConcept.Properties.NullText = String.Empty
        INDSleCostCenter.EditValue = Nothing
        INDSleCostCenter.Text = String.Empty
        INDLySleCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDSleWarehouse.EditValue = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty

        Select Case INDGleAdjustmentType.EditValue
            Case 1
                LyGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LyGroupProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LyInventoryControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LyInventoryControlProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case 2
                LyGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LyGroupProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LyInventoryControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LyInventoryControlProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case 3
                LyInventoryControlProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LyInventoryControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LyGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LyGroupProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case 4
                INDlciThirdParty.Visibility = XtraLayout.Utils.LayoutVisibility.Never
                LciAdmissionNumber.Visibility = XtraLayout.Utils.LayoutVisibility.Always

                INDSleConcept.Enabled = False
                INDLySleConcept.Enabled = False

                LyGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LyGroupProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LyInventoryControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LyInventoryControlProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case Else
                LyGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LyGroupProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LyInventoryControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LyInventoryControlProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End Select
    End Sub

    ''' <summary>
    ''' Evento que carga los conceptos de ajuste de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleConcept.EditValueChanged
        If INDSleConcept.EditValue Is String.Empty Then
            AdjustmentConceptId = Nothing
        End If
        If INDSleConcept.EditValue Is Nothing Then
            adjustmentConcept = Nothing
            Exit Sub
        End If
        GetAdjustmentConcept()
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    Private Async Function QueryInventoryControlEditValue() As Task

        Me.CleanInventoryControlDetailGrid()
        Me.inventoryControl = Nothing
        Me.CleanControlsInventoryControl()

        If Me.InventoryControlId Is Nothing Then
            Return
        End If

        Try
            Using Model As New MInventoryControl("")
                AsyncLoader(True)
                Dim result = Await Model.GetInventoryControlByIdAndStatusAsync(Me.InventoryControlId, 1)

                If result Is Nothing OrElse Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result?.Message
                    Me.InventoryControlId = Nothing
                    Return
                End If

                Me.SafeInvoke(Sub()
                                  Me.inventoryControl = result.ObjectEmbbeded
                                  INDDteDocumentDateInventoryControl.EditValue = Me.inventoryControl.DocumentDate
                                  INDTxtWarehouseInventoryControl.Text = Me.inventoryControl.DescriptionWarehouse

                                  If inventoryControl.InventoryControlDetail?.Any(Function(x) x.InventoryControlDetailBatchSerial.Any()) Then
                                      For Each icd In inventoryControl.InventoryControlDetail.Where(Function(x) x.InventoryControlDetailBatchSerial.Any())
                                          For Each Batch In icd.InventoryControlDetailBatchSerial
                                              ListInventoryControlDetail.Add(Batch)
                                          Next
                                      Next
                                  End If
                                  INDGcPhysicalInventory.RefreshDataSource()
                              End Sub)
            End Using
        Catch ex As Exception
            Me.inventoryControl = Nothing
            Me.CleanInventoryControlDetailGrid()
            Me.CleanControlsInventoryControl()
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Function

    Private Sub CleanInventoryControlDetailGrid()
        ListInventoryControlDetail = New Domain.Entities.TrackableCollection(Of InventoryControlDetailBatchSerial)
        INDGcPhysicalInventory.RefreshDataSource()
    End Sub

    Private Sub CleanControlsInventoryControl()
        INDDteDocumentDateInventoryControl.EditValue = GetDateServer()
        INDTxtWarehouseInventoryControl.Text = String.Empty
    End Sub


    Private Async Function LoadControlDetailPhysicalInventory(inventoryAdjustmentId As Integer) As Task

        Try
            Me.CleanInventoryControlDetailGrid()
            Using Model As New MInventoryControl("")
                AsyncLoader(True)
                Dim result = Await Model.GetInventoryAdjustmentControlByInventoryAdjustmentId(inventoryAdjustmentId)

                If result Is Nothing OrElse Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result?.Message
                    Return
                End If

                Dim detailBatch = result.ObjectEmbbeded

                If detailBatch.Any() Then

                    Await Task.Run(Sub()
                                       For Each item In detailBatch
                                           ListInventoryControlDetail.Add(item)
                                       Next

                                       For Each iac In inventoryAdjustments.InventoryAdjustmentControl
                                           Dim DetailInventoryAdjustmentControl = ListInventoryControlDetail.ToList().Where(Function(x) x.Id = iac.InventoryControlDetailBatchSerialId).FirstOrDefault()

                                           If DetailInventoryAdjustmentControl IsNot Nothing Then
                                               DetailInventoryAdjustmentControl.Selected = True
                                           End If
                                       Next
                                   End Sub)

                End If
            End Using
        Catch ex As Exception
            Me.CleanInventoryControlDetailGrid()
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            flagLoad = False
        End Try
    End Function

    ''' <summary>
    ''' Evento que carga los datos de control de inventario para ajustar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleInventoryControl_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleInventoryControl.EditValueChanged
        If flagLoad Then
            Exit Sub
        End If
        Await QueryInventoryControlEditValue()
    End Sub

    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        If Me.INDSleWarehouse.EditValue IsNot Nothing Then
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = If(INDSleWareHouseView.DataSource IsNot Nothing, DirectCast(DirectCast(INDSleWareHouseView.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me.inventoryAdjustments.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me.inventoryAdjustments.Prefix)
            End If
        End If
    End Sub

    Private Async Sub INDSleThirdParty_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDSleThirdParty.EditValueChanged
        If Me.INDSleThirdParty.EditValue IsNot Nothing AndAlso Me.INDSleThirdParty.EditValue > 0 Then
            Using ModelThird As New MThirdParty(MThirdParty.TAG)
                Me.ThirdPartyTmp = Await ModelThird.GetThirdPartyById(Me.INDSleThirdParty.EditValue)
            End Using
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
    Private Sub INDBtnAddProducts_Click(sender As Object, e As EventArgs) Handles INDBtnAddProducts.Click
        If OnlyRead = False Then
            If listInventoryAdjustmentsDetail Is Nothing Then
                listInventoryAdjustmentsDetail = New List(Of InventoryAdjustmentDetail)
            End If
            If INDGleAdjustmentType.EditValue = 2 Then
                If INDSleWarehouse.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Informacion) = INDSleWarehouse.Text + ResourceManager.GetString("Empty")
                    Exit Sub
                End If
            ElseIf INDGleAdjustmentType.EditValue = 4 Then
                If String.IsNullOrEmpty(AdmissionNumber) Then
                    Mensaje(EeventViewerImages.Informacion) = LciAdmissionNumber.Text + ResourceManager.GetString("Empty")
                    Exit Sub
                End If
                If INDSleWarehouse.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Informacion) = INDLySleWarehouse.Text + ResourceManager.GetString("Empty")
                    Exit Sub
                End If
            End If
            InstantiatePopup(Nothing, listInventoryAdjustmentsDetail.ToList())
        End If
    End Sub

    ''' <summary>
    ''' Evento de la barra de btones que Anula el documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        BarraBotones.Focus()
        Eliminar()
    End Sub
#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
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
                DeleteDetail()
        End Select
    End Sub

#Region "NewSelectedValue"
    Private Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDsleAdmissionNumber.NewSelectedValue
        '
        'If e.AdmissionObject IsNot Nothing AndAlso e.AdmissionObject.GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
        If e.AdmissionObject IsNot Nothing AndAlso e.AdmissionObject.GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionNumber")) Then
            'RevenueControlId = e.AdmissionObject.Key
            AdmissionNumber = e.AdmissionObject.AdmissionNumber.ToString().Trim()
            If Not ValidatePatientThirdParty(e.AdmissionObject.PatientCode) Then
                'Me.Mensaje(EeventViewerImages.Advertencia) = String.Format("El paciente {0} no está creado como tercero en Indigo VIE", String.Concat(e.AdmissionObject.PatientCode.ToString().Trim(), " - ", e.AdmissionObject.PatientName.ToString().Trim()))
                Me.Mensaje(EeventViewerImages.Advertencia) = String.Format("El paciente {0} no está creado como tercero en Indigo VIE", String.Concat(e.AdmissionObject.PatientCode.ToString().Trim(), " - ", e.AdmissionObject.IPNOMCOMP.ToString().Trim()))
                INDsleAdmissionNumber.Search.EditValue = Nothing
                INDsleAdmissionNumber.SetNullText(String.Empty)
                AdmissionNumber = Nothing
                INDBtnAddProducts.Enabled = False
                InventoryControlId = Nothing
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
                    'RevenueControlId = Nothing
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
        End Select
    End Sub

#End Region

#Region "ShowingEditor"
    ''' <summary>
    ''' Evento utilziado para controlar el estado del check si tiene autorizacion para chekear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGcPhysicalInventoryView_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGcPhysicalInventoryView.ShowingEditor
        Dim item As InventoryControlDetailBatchSerial = INDGcPhysicalInventoryView.GetFocusedRow()

        If item IsNot Nothing Then
            If item.StatusName = "Ajustado" Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "CheckStateChanged"
    ''' <summary>
    ''' Evento que valida el estado del check y aplica logica al cambio de estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemCheckEdit1_CheckStateChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit1.CheckStateChanged
        Dim item As InventoryControlDetailBatchSerial = INDGcPhysicalInventoryView.GetFocusedRow()
        If DirectCast(sender, DevExpress.XtraEditors.CheckEdit).CheckState Then
            Dim adjustmentControl As New InventoryAdjustmentControl
            adjustmentControl.InventoryControlDetailBatchSerialId = item.Id
            If item.Quantity > item.InventoryQuantity Then 'Sobrante o Entrada
                adjustmentControl.AdjustmentType = 1
            ElseIf item.Quantity < item.InventoryQuantity Then 'Faltante o Salida
                adjustmentControl.AdjustmentType = 2
            End If
            adjustmentControl.QuantityAdjustment = Math.Abs(item.InventoryQuantity - item.Quantity)

            inventoryAdjustments.InventoryAdjustmentControl.Add(adjustmentControl)
        Else
            If inventoryAdjustments.InventoryAdjustmentControl IsNot Nothing Then
                If inventoryAdjustments.InventoryAdjustmentControl.ToList().Find(Function(x) x.InventoryControlDetailBatchSerialId = item.Id).Id = 0 Then
                    inventoryAdjustments.InventoryAdjustmentControl.ToList().Remove(inventoryAdjustments.InventoryAdjustmentControl.ToList().Find(Function(x) x.InventoryControlDetailBatchSerialId = item.Id))
                Else
                    inventoryAdjustments.InventoryAdjustmentControl.ToList().Find(Function(x) x.InventoryControlDetailBatchSerialId = item.Id).MarkAsDeleted()
                End If
            End If
        End If
    End Sub
#End Region

#Region "OpenFormButtonClick"

    Private Sub INDSleThirdParty_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDSleThirdParty.OpenFormButtonClick
        OpenForm(532, Nothing, True)
        Presenter.LoadListThirdParty()
    End Sub

    Private Sub INDGcPhysicalInventory_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDGcPhysicalInventory.MouseDoubleClick
        If ListInventoryControlDetail IsNot Nothing AndAlso ListInventoryControlDetail.Count > 0 Then
            Dim hitPoint = Me.INDGcPhysicalInventoryView.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then

                    Dim listFilterXpCollection = INDGcPhysicalInventoryView.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.Selected = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.Selected = False
                        Next
                        Me.INDGclState.Image = My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.Selected = True
                        Next
                        Me.INDGclState.Image = My.Resources.Resources.check
                    End If
                    Me.INDGcPhysicalInventory.RefreshDataSource()
                    Me.INDGcPhysicalInventory.Invalidate()
                End If
            End If
        End If
    End Sub

    Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCheckEdit1.EditValueChanging
        Dim _InventoryControlDetail As InventoryControlDetailBatchSerial = INDGcPhysicalInventoryView.GetFocusedRow()
        If _InventoryControlDetail IsNot Nothing Then
            _InventoryControlDetail.Selected = e.NewValue
            INDGcPhysicalInventory.RefreshDataSource()

            If ListInventoryControlDetail IsNot Nothing AndAlso ListInventoryControlDetail.Count > 0 Then
                Dim listFilterXpCollection = INDGcPhysicalInventoryView.DataController.GetAllFilteredAndSortedRows()
                Dim cont As Integer = (From l In listFilterXpCollection Where l.Selected = True).Count
                If cont = listFilterXpCollection.Count Then
                    Me.INDGclState.Image = My.Resources.Resources.check
                Else
                    Me.INDGclState.Image = My.Resources.Resources.undcheck
                End If
            End If
        End If
    End Sub

#End Region

    Private Sub INDsleAdmissionNumber_SearchOpenPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdmissionNumber.SearchOpenPopUp
        INDsleAdmissionNumber.Search.Properties.View.Columns(1).MaxWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(1).MinWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(2).MaxWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(2).MinWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(6).MinWidth = 100
        INDsleAdmissionNumber.Search.Properties.View.Columns(6).MaxWidth = 200
        INDsleAdmissionNumber.Search.Properties.View.Columns(0).Visible = False
        INDsleAdmissionNumber.Search.Properties.View.Columns(5).Visible = False
        INDsleAdmissionNumber.Search.Properties.View.Columns(4).Visible = False
        INDsleAdmissionNumber.Search.Properties.View.Columns(1).Caption = "No. Ingreso"
        INDsleAdmissionNumber.Search.Properties.View.Columns(2).Caption = "Identificación"
        INDsleAdmissionNumber.Search.Properties.View.Columns(3).Caption = "Paciente"
        INDsleAdmissionNumber.Search.Properties.View.Columns(6).Caption = "Estado"
    End Sub

End Class
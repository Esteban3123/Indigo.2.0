'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 13/04/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress
Imports Presentation.Payroll
Imports Presentation.Common
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region

Public Class FrmTransferOrder
    Implements ITransferOrder, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Const NAME_MODULE As String = "Inventory"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PTransferOrder

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Permite saber si el usuario tiene permiso para validar cantidad cuando se importa
    ''' </summary>
    Dim _permissionValidateQuantity As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' secuencia
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
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _transferOrder As TransferOrder

    ''' <summary>
    ''' Listado de los detalles de orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listTransferOrderDetail As List(Of TransferOrderDetail)

    ''' <summary>
    ''' Listado de eliminados de los detalles de orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteTransferOrderDetail As List(Of TransferOrderDetail)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim _indexEditRecord As Integer

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim _itemTransferOrderDetail As TransferOrderDetail

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

    ''' <summary>
    ''' Unidad Funcional
    ''' </summary>
    Dim FunctionalUnit As PayrollFunctionalUnit

    Dim ChangedConceptFlag As Boolean = True

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ITransferOrder.MyTag
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ITransferOrder.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ITransferOrder.ActionsOnControls
        Set(value As Boolean)
            INDlcTransferOrder.BeginUpdate()

            INDbtnCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDsleOrderType.Enabled = value
            INDmeDescription.Enabled = value
            INDsleSourceWarehouseId.Enabled = value
            INDsleDispatchTo.Enabled = value
            INDsleTransitWarehouseId.Enabled = value
            INDsleTargetWarehouseId.Enabled = value
            INDsleTargetFunctionalUnitId.Enabled = value
            INDsleAdjustmentConceptId.Enabled = value
            INDsleThirdPartyId.Enabled = value
            INDgcProducts.Enabled = value

            BarraBotones.StatusRecordVisible = value

            If value Then
                INDdeDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
            INDlcTransferOrder.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Establece la visibilidad de los controles del popup
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsOnControlsPopupPhysical As Boolean
        Set(value As Boolean)
            INDlcTransferOrder.BeginUpdate()

            INDBtnAdd.Enabled = False
            INDPccDetailPhysicalInventory.Enabled = value
            INDGcDetailPhysicalInventory.Enabled = value

            INDlcTransferOrder.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Public Property Sequense As InventorySequence Implements ITransferOrder.Sequense
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
    '''  Obtiene o establece el consecutivo de la orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements ITransferOrder.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements ITransferOrder.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el el tipo de orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrderType As Byte Implements ITransferOrder.OrderType
        Get
            Return INDsleOrderType.EditValue
        End Get
        Set(value As Byte)
            INDsleOrderType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece una descripcion de la orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements ITransferOrder.Description
        Get
            Return INDmeDescription.EditValue
        End Get
        Set(value As String)
            INDmeDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del almacen de origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceWarehouseId As Integer? Implements ITransferOrder.SourceWarehouseId
        Get
            Return INDsleSourceWarehouseId.EditValue
        End Get
        Set(value As Integer?)
            INDsleSourceWarehouseId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece hacia donde se va a despachar los items
    ''' </summary>
    ''' <value>1 - Almacen 2 - Unidad Funcional</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DispatchTo As Byte? Implements ITransferOrder.DispatchTo
        Get
            Return CType(INDsleDispatchTo.EditValue, Byte?)
        End Get
        Set(value As Byte?)
            INDsleDispatchTo.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del Almacen de transito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransitWarehouseId As Integer? Implements ITransferOrder.TransitWarehouseId
        Get
            Return INDsleTransitWarehouseId.EditValue
        End Get
        Set(value As Integer?)
            INDsleTransitWarehouseId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del Almacen de destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetWarehouseId As Integer? Implements ITransferOrder.TargetWarehouseId
        Get
            Return INDsleTargetWarehouseId.EditValue
        End Get
        Set(value As Integer?)
            INDsleTargetWarehouseId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetFunctionalUnitId As Integer? Implements ITransferOrder.TargetFunctionalUnitId
        Get
            Return INDsleTargetFunctionalUnitId.EditValue
        End Get
        Set(value As Integer?)
            INDsleTargetFunctionalUnitId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del concepto de movimiento de inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdjustmentConceptId As Integer? Implements ITransferOrder.AdjustmentConceptId
        Get
            Return INDsleAdjustmentConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdjustmentConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer? Implements ITransferOrder.ThirdPartyId
        Get
            Return INDsleThirdPartyId.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdPartyId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements ITransferOrder.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

#End Region

#Region "Datasources"

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

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceWarehouseXpo As XPInstantFeedbackSource Implements ITransferOrder.SourceWarehouseXpo
        Get
            Return INDsleSourceWarehouseId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSourceWarehouseId.Properties.DataSource = value
        End Set
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

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransitWarehouseXpo As XPInstantFeedbackSource Implements ITransferOrder.TransitWarehouseXpo
        Get
            Return INDsleTransitWarehouseId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTransitWarehouseId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetWarehouseXpo As XPInstantFeedbackSource Implements ITransferOrder.TargetWarehouseXpo
        Get
            Return INDsleTargetWarehouseId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTargetWarehouseId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetFunctionalUnitXpo As XPInstantFeedbackSource Implements ITransferOrder.TargetFunctionalUnitXpo
        Get
            Return INDsleTargetFunctionalUnitId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTargetFunctionalUnitId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de concepto de movimiento de inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdjustmentConceptXpo As XPInstantFeedbackSource Implements ITransferOrder.AdjustmentConceptXpo
        Get
            Return INDsleAdjustmentConceptId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAdjustmentConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyXpo As XPInstantFeedbackSource Implements ITransferOrder.ThirdPartyXpo
        Get
            Return INDsleThirdPartyId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdPartyId.Properties.DataSource = value
        End Set
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Tipo De orden", .FieldName = "OrderTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Alm. Origen", .FieldName = "SourceWarehouseId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Despachado", .FieldName = "DispatchToDescription", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Destino", .FieldName = "TargetDescription", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListTransferOrder
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
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
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
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewTransferOrder()
        End If
    End Sub

    ''' <summary>
    '''  METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If _transferOrder IsNot Nothing AndAlso _transferOrder.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If

            If _listTransferOrderDetail Is Nothing OrElse _listTransferOrderDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay Productos agregados en la rejilla."
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MTransferOrder(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result = Await model.SaveTrasnferOrder(_transferOrder)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    If Not String.IsNullOrEmpty(result.MessageAux) Then
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageAux
                    End If

                    _transferOrder = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _transferOrder.Id, 0, _transferOrder.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _transferOrder.Id, 0, _transferOrder.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _transferOrder.Id, 0, _transferOrder.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _transferOrder.Id, 0, _transferOrder.Id)
                    End Select
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    If _transferOrder.Id > 0 Then
                        _transferOrder = Await model.GetTranferOrderByCode(_transferOrder.Code)
                    Else
                        Dim prefix = _transferOrder.Prefix
                        _transferOrder = New TransferOrder
                        _transferOrder.Prefix = prefix
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
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
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        INDsleOrderType.Properties.DataSource = FillingOrderType
        INDsleDispatchTo.Properties.DataSource = FillingDispatchTo
        INDRptIsConsignment.DataSource = {New Tuple(Of Boolean, String)(True, "SI"), New Tuple(Of Boolean, String)(False, "NO")}.ToList()
    End Sub

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateDelivered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StateInTransit"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
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
        If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(_settingsInventory.Year.ToString + "/" + _settingsInventory.Month.ToString + "/01")
        INDdeDocumentDate.Properties.MinValue = dateMin
        INDdeDocumentDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Valida que el producto a agregar tenga añadida la unidad funcional en el grupo al cual éste pertenece
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateProductGroup(Detail As TransferOrderDetail) As Boolean
        If _settingsInventory IsNot Nothing AndAlso _settingsInventory.AssociateCostMainAccount = 2 AndAlso DispatchTo = 2 Then
            Dim Group As ActionResult(Of ProductGroup)
            Dim ProductGroup As ProductGroup
            Dim listerror As New List(Of StringBuilder)
            Dim Product As InventoryProduct
            Dim errors As New StringBuilder

            If Detail IsNot Nothing Then
                Using model As New MInventoryProduct(Me.Tag)
                    Product = model.GetInventoryProductByIdSimpleToGroup(Detail.ProductId)
                End Using
                If Product IsNot Nothing Then
                    Using modelgroup As New MGroup(Me.Tag)
                        Group = modelgroup.GetProductGroupByIdSimple(Product.ProductGroupId)
                        ProductGroup = Group.ObjectEmbbeded
                    End Using
                    If ProductGroup IsNot Nothing Then
                        If Not ProductGroup.ProductGroupFunctionalUnit.Any(Function(x) x.FunctionalUnitId = TargetFunctionalUnitId) Then
                            Mensaje(EeventViewerImages.Advertencia) = $"La unidad funcional {FunctionalUnit.Descripcion} no se encuentra parametrizada en el grupo de inventario No {ProductGroup.Code}"
                            Return False
                        End If
                    End If
                End If
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlcTransferOrder.BeginUpdate()
        Await DeleteBlockedRecord()

        Code = String.Empty
        DocumentDate = Me.GetDateServer()
        OrderType = 1
        Description = Nothing
        SourceWarehouseId = Nothing
        INDsleSourceWarehouseId.Properties.NullText = String.Empty
        DispatchTo = 1
        TransitWarehouseId = Nothing
        INDsleTransitWarehouseId.Properties.NullText = String.Empty
        TargetWarehouseId = Nothing
        INDsleTargetWarehouseId.Properties.NullText = String.Empty
        TargetFunctionalUnitId = Nothing
        INDsleTargetFunctionalUnitId.Properties.NullText = String.Empty
        AdjustmentConceptId = Nothing
        INDsleAdjustmentConceptId.Properties.NullText = String.Empty
        ThirdPartyId = Nothing
        INDsleThirdPartyId.Properties.NullText = String.Empty
        Status = 1
        FunctionalUnit = Nothing
        TargetFunctionalUnitXpo = Nothing

        INDgcProducts.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDgcProducts)

        _doc = Nothing
        _transferOrder = Nothing
        _listTransferOrderDetail = Nothing
        _listDeleteTransferOrderDetail = Nothing
        _itemTransferOrderDetail = Nothing

        Me.ValidateDate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        INDBtnAdd.Enabled = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        INDlcTransferOrder.EndUpdate()

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
            Dim result = Await model.GetBlockRecord(Me.Tag, Me._transferOrder.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _blockRecord = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me._transferOrder.Id}
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
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._transferOrder.Code, Me._transferOrder.OperatingUnitId, Me._transferOrder.DocumentDate, Me._transferOrder.OrderType, Me._transferOrder.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._transferOrder.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._transferOrder.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._transferOrder.Code, Me._transferOrder.OperatingUnitId, Me._transferOrder.DocumentDate, Me._transferOrder.OrderType, Me._transferOrder.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._transferOrder.Code)
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
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewTransferOrder() As Task

        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
            Exit Function
        End If

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.SaveAndConfirmObligatory()
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
                Me.SaveAndConfirmObligatory()
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Me.SaveAndConfirmObligatory()
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
                            Me.SaveAndConfirmObligatory()
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
                    Me.SaveAndConfirmObligatory()
                End If
            End If
        End If

        BarraBotones.StatusRecord = "1"
        BarraBotones.StatusRecordVisible = True
        Me._transferOrder = New Domain.Entities.TransferOrder With {.Status = 1}

    End Function

    Private Sub SaveAndConfirmObligatory()
        If Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar)) Then
            Exit Sub
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar))
    End Sub

    ''' <summary>
    ''' Método que carga los controles de la orden de traslado
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
                Using Model As New MTransferOrder(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlcTransferOrder.BeginUpdate()
                    _transferOrder = Await Model.GetTranferOrderByCode(INDbtnCode.Text.Trim)
                    If _transferOrder IsNot Nothing AndAlso _transferOrder.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_transferOrder.Id))
                            With _transferOrder
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
                                    INDdeDocumentDate.Properties.MinValue = .DocumentDate
                                End If

                                Code = .Code
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                DocumentDate = .DocumentDate
                                OrderType = .OrderType
                                Description = .Description
                                SourceWarehouseId = .SourceWarehouseId
                                INDsleSourceWarehouseId.Properties.NullText = .DescriptionSourceWarehouse
                                DispatchTo = .DispatchTo
                                TransitWarehouseId = .TransitWarehouseId
                                INDsleTransitWarehouseId.Properties.NullText = .DescriptionTransitWarehouse
                                TargetWarehouseId = .TargetWarehouseId
                                INDsleTargetWarehouseId.Properties.NullText = .DescriptionTargetWarehouse
                                TargetFunctionalUnitId = .TargetFunctionalUnitId
                                INDsleTargetFunctionalUnitId.Properties.NullText = .DescriptionTargetFuntionalUnit
                                AdjustmentConceptId = .AdjustmentConceptId
                                ThirdPartyId = .ThirdPartyId
                                INDsleThirdPartyId.Properties.NullText = .DescirptionThirdParty
                                INDsleAdjustmentConceptId.Properties.NullText = .DescriptionAdjustmentConcept
                                Me.Status = .Status

                                _listTransferOrderDetail = .TransferOrderDetail.ToList()
                                INDgcProducts.DataSource = Nothing
                                INDgcProducts.DataSource = _listTransferOrderDetail
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._transferOrder.Code)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _transferOrder.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If


                            Select Case _transferOrder.Status
                                Case 1
                                    INDsleOrderType.Properties.ReadOnly = True
                                    INDsleSourceWarehouseId.Properties.ReadOnly = True
                                    INDsleDispatchTo.Properties.ReadOnly = True
                                    INDsleTransitWarehouseId.Properties.ReadOnly = True
                                    INDsleTargetWarehouseId.Properties.ReadOnly = True
                                    INDsleTargetFunctionalUnitId.Properties.ReadOnly = True
                                    INDBtnAdd.Enabled = True

                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                                Case Else
                                    ReadOnlyControls(True)

                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                                    If _transferOrder.Status = 4 Then
                                        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Confirmar, "Confirmar Recibido")
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                                    End If

                                    ActionsOnControlsPopupPhysical = True
                                    For iColumns = 0 To INDGvProducts.Columns.Count - 1
                                        If INDGvProducts.Columns(iColumns).Name = "INDGcoDetailPhysical" Then
                                            INDGvProducts.Columns(iColumns).OptionsColumn.AllowEdit = True
                                        End If
                                    Next
                            End Select

                            INDdeDocumentDate.Focus()
                            Me.BarraBotones.SetDocuments(_transferOrder.Id, Me.Tag.ToString(), Nothing, GetType(TransferOrder).Name)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _transferOrder.Id, 0, _transferOrder.Id)

                            ActionsOnControls = True
                            AsyncLoader(False)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewTransferOrder()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            Deshacer()
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlcTransferOrder.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _transferOrder
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .DocumentDate = DocumentDate
            .OrderType = OrderType
            .Description = Description
            .SourceWarehouseId = SourceWarehouseId
            .DispatchTo = DispatchTo
            .TransitWarehouseId = TransitWarehouseId
            .TargetWarehouseId = TargetWarehouseId
            .TargetFunctionalUnitId = TargetFunctionalUnitId
            .AdjustmentConceptId = AdjustmentConceptId
            .ThirdPartyId = ThirdPartyId

            For Each item In _listTransferOrderDetail
                .TransferOrderDetail.Add(item)
            Next
            If _listDeleteTransferOrderDetail IsNot Nothing AndAlso _listDeleteTransferOrderDetail.Count > 0 Then
                For Each item In _listDeleteTransferOrderDetail
                    .TransferOrderDetail.Add(item)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo que obtiene las ordenes de servicio y los contratos que se seleccionaron en el formulario para importar informacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetTransferOrderDetail(sender As Object, e As GetListTransferOrderDetailEventArgs)
        Dim errors As New StringBuilder
        Dim listTransferOrderDetailHandlesBatch As New List(Of TransferOrderDetail)
        Dim listTransferOrderDetailNotHandlesBatch As New List(Of TransferOrderDetail)

        Dim product As New InventoryProduct
        Dim dictionaryProduct As Dictionary(Of Integer, Domain.Entities.InventoryProduct) = New Dictionary(Of Integer, InventoryProduct)()

        For Each Item In e.ListTransferOrdeDetail
            If Item.ComponentType <> 3 Then 'Si no es un item de tipo producto
                listTransferOrderDetailHandlesBatch.Add(Item)
                Continue For
            End If

            If Not ValidateProductGroup(Item) Then
                Continue For
            End If

            'Si es un item de tipo producto
            If dictionaryProduct.ContainsKey(Item.ProductId) Then
                product = dictionaryProduct(Item.ProductId)
            Else
                Using model As New MInventoryProduct(Me.Tag)
                    product = model.GetInventoryProductByIdSimpleToGroup(Item.ProductId)
                    dictionaryProduct.Add(Item.ProductId, product)
                End Using
            End If

            Item.Value = product.ProductCost
            If product.ProductSubGroup.HandlesBatch Then
                listTransferOrderDetailHandlesBatch.Add(Item)
            Else
                Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
                    Dim PhysicalInventory = modelPhysical.GetPhysicalInventory(product.Id, SourceWarehouseId)
                    If PhysicalInventory Is Nothing Then
                        errors.AppendLine("El producto " + product.Code + " - " + product.Name + " no se encontro dentro del inventario fisico del almacén de origen")
                        Continue For
                    End If
                    If PhysicalInventory.Quantity = 0 Then
                        errors.AppendLine("El producto " + product.Code + " - " + product.Name + " no tiene cantidades en el inventario fisico")
                        Continue For
                    End If

                    Dim InventoryQuantity = PhysicalInventory.Quantity
                    If listTransferOrderDetailNotHandlesBatch.Any(Function(d) d.ProductId = Item.ProductId) Then
                        InventoryQuantity = InventoryQuantity - listTransferOrderDetailNotHandlesBatch.Where(Function(d) d.ProductId = Item.ProductId).Sum(Function(d) d.Quantity)
                    End If
                    If _listTransferOrderDetail IsNot Nothing AndAlso _listTransferOrderDetail.Any(Function(d) d.ProductId = Item.ProductId) Then
                        InventoryQuantity = InventoryQuantity - _listTransferOrderDetail.Where(Function(d) d.ProductId = Item.ProductId).Sum(Function(d) d.Quantity)
                    End If
                    If InventoryQuantity <= 0 Then
                        errors.AppendLine("El producto " + product.Code + " - " + product.Name + " ha superado las cantidades del inventario fisico")
                        Continue For
                    End If

                    Item.DescriptionProduct = product.Code + " - " + product.Name
                    Item.QuantityImport = If(InventoryQuantity > Item.QuantityImport, Item.QuantityImport, InventoryQuantity)
                    Item.Quantity = Item.QuantityImport
                    Item.InventoryQuantity = PhysicalInventory.Quantity
                    Item.ConsumptionUnit = product.PackingUnitDescription
                    Item.CostProduct = product.ProductCost

                    'Agregamos el lote así no tenga
                    Dim transferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                    transferOrderDetailBatchSerial.Quantity = Item.QuantityImport
                    transferOrderDetailBatchSerial.OutstandingQuantity = Item.QuantityImport
                    transferOrderDetailBatchSerial.CodeNameProduct = Item.DescriptionProduct
                    transferOrderDetailBatchSerial.PhysicalInventoryId = PhysicalInventory.Id
                    Item.TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)

                    listTransferOrderDetailNotHandlesBatch.Add(Item)
                End Using
            End If
        Next

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            'Exit Sub
        End If

        'agregamos los productos que no manejen lote a la rejilla directamente
        If listTransferOrderDetailNotHandlesBatch IsNot Nothing AndAlso listTransferOrderDetailNotHandlesBatch.Count > 0 Then
            Dim args As New AddProductTransferOrderDetailEventArgs
            args.ImportDataMode = True
            args.ListTransferOrderDetail = listTransferOrderDetailNotHandlesBatch
            ReturnAddTransferOrderDetail(Nothing, args)
        End If

        'abrimos el popup de agregar productos si el producto maneja lote
        If listTransferOrderDetailHandlesBatch IsNot Nothing AndAlso listTransferOrderDetailHandlesBatch.Count > 0 Then
            Using formulario As New FrmPopUpProductTransferOrder
                Me.Cursor = ChangeCursorIndigo()
                AddHandler formulario.AddTransferOrderDetail, AddressOf ReturnAddTransferOrderDetail
                formulario.Size = New Size(800, 730)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.PermissionValidateQuantity = _permissionValidateQuantity
                formulario.operatingUnitId = Me.BarraBotones.OperatingUnitValue
                formulario.WareHouseId = SourceWarehouseId
                formulario.ListTransferOrderDetailImportInfo = listTransferOrderDetailHandlesBatch
                formulario.ListTransferOrderDetailValidation = _listTransferOrderDetail
                formulario.ImportDataMode = True
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddTransferOrderDetail(sender As Object, e As AddProductTransferOrderDetailEventArgs)
        Try
            If _listTransferOrderDetail Is Nothing Then
                _listTransferOrderDetail = New List(Of TransferOrderDetail)
            End If

            If Not ValidateProductGroup(e.ItemTransferOrderDetail) Then
                Exit Sub
            End If

            If e.EditMode Then
                _listTransferOrderDetail.Remove(_itemTransferOrderDetail)
                _listTransferOrderDetail.Insert(_indexEditRecord, e.ItemTransferOrderDetail)
            ElseIf e.ImportDataMode Then
                _listTransferOrderDetail.AddRange(e.ListTransferOrderDetail)
            Else
                _listTransferOrderDetail.Add(e.ItemTransferOrderDetail)
            End If

            INDsleOrderType.Properties.ReadOnly = True
            INDsleSourceWarehouseId.Properties.ReadOnly = True
            INDsleDispatchTo.Properties.ReadOnly = True
            INDsleTargetWarehouseId.Properties.ReadOnly = True
            INDsleTransitWarehouseId.Properties.ReadOnly = True
            INDsleTargetFunctionalUnitId.Properties.ReadOnly = True

            INDgcProducts.DataSource = Nothing
            INDgcProducts.DataSource = _listTransferOrderDetail
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        _itemTransferOrderDetail = DirectCast(INDGvProducts.GetFocusedRow(), TransferOrderDetail)
        _indexEditRecord = _listTransferOrderDetail.IndexOf(_itemTransferOrderDetail)
        Using formulario As New FrmPopUpProductTransferOrder
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddTransferOrderDetail, AddressOf ReturnAddTransferOrderDetail
            formulario.Size = New Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.PermissionValidateQuantity = _permissionValidateQuantity
            formulario.operatingUnitId = Me.BarraBotones.OperatingUnitValue
            formulario.WareHouseId = SourceWarehouseId
            formulario.TransferOrderDetailEdit = _itemTransferOrderDetail
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
        _itemTransferOrderDetail = DirectCast(INDGvProducts.GetFocusedRow(), TransferOrderDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _itemTransferOrderDetail.Id > 0 Then
                If _listDeleteTransferOrderDetail Is Nothing Then
                    _listDeleteTransferOrderDetail = New List(Of TransferOrderDetail)
                End If
                While _itemTransferOrderDetail.TransferOrderDetailBatchSerial.Count > 0
                    If _itemTransferOrderDetail.TransferOrderDetailBatchSerial(0).Id > 0 Then
                        _itemTransferOrderDetail.TransferOrderDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        _itemTransferOrderDetail.TransferOrderDetailBatchSerial.Remove(_itemTransferOrderDetail.TransferOrderDetailBatchSerial(0))
                    End If
                End While
                _itemTransferOrderDetail.MarkAsDeleted()
                _listDeleteTransferOrderDetail.Add(_itemTransferOrderDetail)
            End If
            _listTransferOrderDetail.Remove(_itemTransferOrderDetail)
            INDgcProducts.DataSource = Nothing
            INDgcProducts.DataSource = _listTransferOrderDetail

            If _listTransferOrderDetail.Count = 0 Then
                INDsleOrderType.Properties.ReadOnly = False
                INDsleSourceWarehouseId.Properties.ReadOnly = False
                INDsleDispatchTo.Properties.ReadOnly = False
                INDsleTransitWarehouseId.Properties.ReadOnly = False
                INDsleTargetWarehouseId.Properties.ReadOnly = False
                INDsleTargetFunctionalUnitId.Properties.ReadOnly = False
            End If
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmTransferOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcTransferOrder, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PTransferOrder(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDgcProducts)
        ActionsOnControls = False
        Await Me.LoadParameters()

        AddActionsColumns()
        InitializeTuples()
        Deshacer()
        LoadStatus()

        'Se obtiene el permiso de validar cantidades
        _permissionValidateQuantity = BarraBotones.PermissionsForm.Where(Function(x) x.Key = 91).Count()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _permissionValidateQuantity = Nothing
        _idCurrentSequence = Nothing
        _sequence = Nothing
        _settingsInventory = Nothing
        _blockRecord = Nothing
        _transferOrder = Nothing
        _listTransferOrderDetail = Nothing
        _listDeleteTransferOrderDetail = Nothing
        _indexEditRecord = Nothing
        _itemTransferOrderDetail = Nothing
        _varImp = Nothing
        FunctionalUnit = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
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
    Private Async Sub FrmTransferOrder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewTransferOrder()
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
    Private Sub INDsleSourceWarehouseId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSourceWarehouseId.QueryPopUp
        If SourceWarehouseXpo Is Nothing Then
            _presenter.SourceWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTransitWarehouseId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTransitWarehouseId.QueryPopUp
        If TransitWarehouseXpo Is Nothing Then
            _presenter.TransitWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetWarehouseId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTargetWarehouseId.QueryPopUp
        If TargetWarehouseXpo Is Nothing Then
            _presenter.TargetWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetFunctionalUnitId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTargetFunctionalUnitId.QueryPopUp
        If TargetFunctionalUnitXpo Is Nothing Then
            _presenter.TargetFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdjustmentConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdjustmentConceptId.QueryPopUp
        If AdjustmentConceptXpo Is Nothing OrElse ChangedConceptFlag Then
            _presenter.InitializeAdjustmentConcept(IIf(DispatchTo = 2, Me.FunctionalUnit?.CostCenterId.Id, Nothing))
            ChangedConceptFlag = False
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdPartyId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdPartyId.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            _presenter.InitializeThirdParty()
        End If
    End Sub

    Private Sub INDRiPcePhysicalInventory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRiPcePhysicalInventory.QueryPopUp
        Dim transferOrderDetailTmp = DirectCast(INDGvProducts.GetFocusedRow, TransferOrderDetail)
        INDGcDetailPhysicalInventory.DataSource = transferOrderDetailTmp.TransferOrderDetailBatchSerial
    End Sub

#End Region

#Region "ButtonClick"

    '''' <summary>
    '''' Manejador del evento que se dispara al darle click sobre el boton del control
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub INDsleSourceWarehouseId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSourceWarehouseId.ButtonClick
    '    If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
    '        Dim size As System.Drawing.Size
    '        size.Width = 780
    '        size.Height = 768
    '        Using pop As New FrmTransparent(New FrmStores With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
    '            pop.Show()
    '        End Using
    '        _presenter.SourceWarehouse()
    '    End If
    'End Sub

    '''' <summary>
    '''' Manejador del evento que se dispara al darle click sobre el boton del control
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub INDsleTransitWarehouseId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTransitWarehouseId.ButtonClick
    '    If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
    '        Dim size As System.Drawing.Size
    '        size.Width = 780
    '        size.Height = 768
    '        Using pop As New FrmTransparent(New FrmStores With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
    '            pop.Show()
    '        End Using
    '        _presenter.TransitWarehouse()
    '    End If
    'End Sub

    '''' <summary>
    '''' Manejador del evento que se dispara al darle click sobre el boton del control
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub INDsleTargetWarehouseId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTargetWarehouseId.ButtonClick
    '    If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
    '        Dim size As System.Drawing.Size
    '        size.Width = 780
    '        size.Height = 768
    '        Using pop As New FrmTransparent(New FrmStores With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
    '            pop.Show()
    '        End Using
    '        _presenter.TargetWarehouse()
    '    End If
    'End Sub

    '''' <summary>
    '''' Manejador del evento que se dispara al darle click sobre el boton del control
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub INDsleTargetFunctionalUnitId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTargetFunctionalUnitId.ButtonClick
    '    If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
    '        Dim size As System.Drawing.Size
    '        size.Width = 780
    '        size.Height = 768
    '        Using pop As New FrmTransparent(New FrmFunctionalUnit With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
    '            pop.Show()
    '        End Using
    '        _presenter.TargetFunctionalUnit()
    '    End If
    'End Sub

    '''' <summary>
    '''' Manejador del evento que se dispara al darle click sobre el boton del control
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub INDsleAdjustmentConceptId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAdjustmentConceptId.ButtonClick
    '    If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
    '        Dim size As System.Drawing.Size
    '        size.Width = 780
    '        size.Height = 768
    '        Using pop As New FrmTransparent(New FrmConceptsInventorySettings With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
    '            pop.Show()
    '        End Using
    '        _presenter.InitializeAdjustmentConcept()
    '    End If
    'End Sub

    '''' <summary>
    '''' Manejador del evento que se dispara al darle click sobre el boton del control
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub INDsleThirdPartyId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdPartyId.ButtonClick
    '    If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
    '        Dim size As System.Drawing.Size
    '        size.Width = 780
    '        size.Height = 768
    '        Using pop As New FrmTransparent(New FrmThirdParty With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
    '            pop.Show()
    '        End Using
    '        _presenter.InitializeThirdParty()
    '    End If
    'End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se dispara al cambiar el valor del tipo de orden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOrderType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleOrderType.EditValueChanged
        'eliminar los items que se agregaron por el metodo de importar
        If _listTransferOrderDetail IsNot Nothing AndAlso _listTransferOrderDetail.Count > 0 Then
            Dim TempListTransferOrderDetail = _listTransferOrderDetail.FindAll(Function(x) x.InventoryRequestDetailId IsNot Nothing)
            If TempListTransferOrderDetail IsNot Nothing AndAlso TempListTransferOrderDetail.Count > 0 Then
                For Each Item In TempListTransferOrderDetail
                    If Item.Id > 0 Then
                        If _listDeleteTransferOrderDetail Is Nothing Then
                            _listDeleteTransferOrderDetail = New List(Of TransferOrderDetail)
                        End If
                        While Item.TransferOrderDetailBatchSerial.Count > 0
                            If Item.TransferOrderDetailBatchSerial(0).Id > 0 Then
                                Item.TransferOrderDetailBatchSerial(0).MarkAsDeleted()
                            Else
                                Item.TransferOrderDetailBatchSerial.Remove(Item.TransferOrderDetailBatchSerial(0))
                            End If
                        End While
                        Item.MarkAsDeleted()
                        _listDeleteTransferOrderDetail.Add(Item)
                    End If
                    _listTransferOrderDetail.Remove(Item)
                Next
                INDgcProducts.DataSource = Nothing
                INDgcProducts.DataSource = _listTransferOrderDetail
            End If
        End If

        SourceWarehouseXpo = Nothing
        If OrderType = 1 Then
            ' si tipo de orden de traslado es igual a "Traslado"
            INDlciDispatchTo.HideControl(True)
            INDlciTransitWarehouseId.HideControl(True)
            INDlciTargetWarehouseId.HideControl(False)
            INDlciTargetFunctionalUnitId.HideControl(True)
            INDlciAdjustmentConceptId.HideControl(True)
            INDlciThirdPartyId.HideControl(True)

            SourceWarehouseId = Nothing
            DispatchTo = 1
            TransitWarehouseId = Nothing
            TargetFunctionalUnitId = Nothing
            AdjustmentConceptId = Nothing
            ThirdPartyId = Nothing
        ElseIf OrderType = 2 Then
            ' si tipo de orden de traslado es igual a "Consumo"
            INDlciDispatchTo.HideControl(False)
            INDlciTransitWarehouseId.HideControl(True)
            INDlciTargetWarehouseId.HideControl(False)
            INDlciTargetFunctionalUnitId.HideControl(True)
            INDlciThirdPartyId.HideControl(True)
            If _settingsInventory IsNot Nothing AndAlso _settingsInventory.AssociateCostMainAccount = 2 Then
                INDlciAdjustmentConceptId.HideControl(True)
                ThirdPartyId = _settingsInventory.TransferOrderThirdPartyId
            Else
                INDlciAdjustmentConceptId.HideControl(False)
            End If

            SourceWarehouseId = Nothing
                DispatchTo = 1
                TransitWarehouseId = Nothing
                TargetFunctionalUnitId = Nothing
                ThirdPartyId = Nothing
            ElseIf OrderType = 3 Then
                ' si tipo de orden de traslado es igual a "Traslado en Transito"
                INDlciDispatchTo.HideControl(True)
            INDlciTransitWarehouseId.HideControl(False)
            INDlciTargetWarehouseId.HideControl(False)
            INDlciTargetFunctionalUnitId.HideControl(True)
            INDlciAdjustmentConceptId.HideControl(True)
            INDlciThirdPartyId.HideControl(True)

            SourceWarehouseId = Nothing
            DispatchTo = 1
            TargetFunctionalUnitId = Nothing
            AdjustmentConceptId = Nothing
            ThirdPartyId = Nothing
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor del almacen de origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceWarehouseId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSourceWarehouseId.EditValueChanged
        INDBtnAdd.Enabled = False
        Me._transferOrder.Prefix = Nothing
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        If SourceWarehouseId IsNot Nothing Then
            INDBtnAdd.Enabled = True

            If TargetFunctionalUnitId IsNot Nothing OrElse TargetWarehouseId IsNot Nothing Then
                If TargetWarehouseId IsNot Nothing Then
                    If SourceWarehouseId = TargetWarehouseId Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("TargetWarehouse", NAME_MODULE))
                        SourceWarehouseId = Nothing
                        INDsleSourceWarehouseId.Properties.NullText = String.Empty
                        INDsleSourceWarehouseId.Focus()
                        Exit Sub
                    End If
                End If

                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            End If

            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = _presenter.GetWarehouseByIdXpo(Me.INDsleSourceWarehouseId.EditValue)
                If store IsNot Nothing Then
                    Me._idCurrentSequence = Me.GetIdSequenceByPrefix(store.Prefix)
                    Me._transferOrder.Prefix = store.Prefix
                End If
            End If

            Dim whXpo As InventoryRepository.WarehouseXpo = INDsleSourceWarehouseId.Properties.View.GetFocusedObject(Of InventoryRepository.WarehouseXpo)()

            If whXpo Is Nothing Then
                whXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventoryRepository.WarehouseXpo)($"Id = {SourceWarehouseId}")
            End If

            If whXpo.WarehouseConsignment Then
                INDsleDispatchTo.ReadOnly = True
                INDsleDispatchTo.EditValue = CByte(2)
            Else
                INDsleDispatchTo.ReadOnly = False
            End If
        Else
            INDsleDispatchTo.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor del depachar a
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDispatchTo_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDispatchTo.EditValueChanged
        If DispatchTo = 1 Then
            INDlciTargetWarehouseId.HideControl(False)
            INDlciTargetFunctionalUnitId.HideControl(True)
            TargetFunctionalUnitId = Nothing
            Me.FunctionalUnit = Nothing
        ElseIf DispatchTo = 2 Then
            INDlciTargetWarehouseId.HideControl(True)
            INDlciTargetFunctionalUnitId.HideControl(False)

            TargetWarehouseId = Nothing
        End If
        ChangedConceptFlag = True
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor del almacen de destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetWarehouseId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTargetWarehouseId.EditValueChanged
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        If TargetWarehouseId IsNot Nothing AndAlso SourceWarehouseId IsNot Nothing Then
            If SourceWarehouseId = TargetWarehouseId Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("TargetWarehouse", NAME_MODULE))
                TargetWarehouseId = Nothing
                INDsleTargetWarehouseId.Properties.NullText = String.Empty
                INDsleTargetWarehouseId.Focus()
                Exit Sub
            End If

            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        End If
        Me.AdjustmentConceptId = Nothing
        Me.ChangedConceptFlag = True
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor de la unidad funcional de destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetFunctionalUnitId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTargetFunctionalUnitId.EditValueChanged
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        If TargetFunctionalUnitId IsNot Nothing AndAlso SourceWarehouseId IsNot Nothing Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            Me.FunctionalUnit = TryCast(TryCast(GridView1.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, PayrollFunctionalUnit)
        End If
        Me.AdjustmentConceptId = Nothing
        Me.ChangedConceptFlag = True
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor del concepto de movimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleAdjustmentConceptId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdjustmentConceptId.EditValueChanged
        If AdjustmentConceptId Is Nothing Then
            Me.AdjustmentConceptXpo = Nothing
            INDlciThirdPartyId.HideControl(True)
            ThirdPartyId = Nothing
            Exit Sub
        End If

        Using model As New MAdjustmentConcept(CStr(Me.Tag))
            Dim result = Await model.GetAdjustmentConceptById(AdjustmentConceptId)
            Dim conceptMovement = result.ObjectEmbbeded
            If conceptMovement IsNot Nothing AndAlso conceptMovement?.MainAccounts?.HandlesThirdParty Then
                INDlciThirdPartyId.HideControl(False)
            Else
                INDlciThirdPartyId.HideControl(True)
                ThirdPartyId = Nothing
            End If
        End Using
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    '''  Abre el popup para agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If _transferOrder Is Nothing OrElse _transferOrder.Status <> 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar la orden de traslado"
            Exit Sub
        End If

        Using formulario As New FrmPopUpProductTransferOrder
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddTransferOrderDetail, AddressOf ReturnAddTransferOrderDetail
            formulario.Size = New Size(800, 780)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.PermissionValidateQuantity = _permissionValidateQuantity
            formulario.operatingUnitId = Me.BarraBotones.OperatingUnitValue
            formulario.WareHouseId = SourceWarehouseId
            formulario.ListTransferOrderDetailValidation = _listTransferOrderDetail
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._transferOrder IsNot Nothing AndAlso Me._transferOrder.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
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
        _transferOrder.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _transferOrder.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _transferOrder.Status = 3
            _varImp = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _transferOrder.Status = If(OrderType = 3, 4, 2)
            _varImp = 4
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _transferOrder.Status = If(OrderType = 3, 4, 2)
            _varImp = 4
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_ClickLegalize() Handles BarraBotones.ClickConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _transferOrder.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _transferOrder.Id, 0, _transferOrder.Id)
    End Sub

    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmImportInfoTransferOrder
            AddHandler formulario.GetListTransferOrderDetail, AddressOf ReturnGetTransferOrderDetail
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.OrderType = If(OrderType = 2, 2, 1)
            formulario.DispatchTo = DispatchTo
            formulario.FilterFunctionalUnitWarehouse = If(DispatchTo = 1, TargetWarehouseId, TargetFunctionalUnitId)
            formulario.ListTransferOrderDetailValidation = _listTransferOrderDetail
            formulario.WarehouseId = SourceWarehouseId
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
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

    'Private Sub INDGdvSourceWarehouseId_CustomColumnDisplayText(sender As Object, e As XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGdvSourceWarehouseId.CustomColumnDisplayText
    '    If e.Column.Name = INDColInConsignment.Name AndAlso TypeOf e.Value Is Boolean Then
    '        e.DisplayText = If(e.Value = True, "SI", "NO")
    '    End If
    'End Sub

#End Region

End Class
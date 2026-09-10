'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/09/2014
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
Imports Presentation.Inventory.MVP
Imports Presentation.Common
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmStores
    Implements IWarehouse, ICustomizableForm


#Region "Properties"
    ''' <summary>
    ''' Request param prodict
    ''' </summary>
    Private _SourceProduct As ViewListConsignmentWarehouseProductsXpo = Nothing
    ''' <summary>
    ''' Obtiene o establece el id del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdUser As Integer? Implements IWarehouse.IdUser
        Get
            Return INDsleUsers.EditValue
        End Get
        Set(value As Integer?)
            INDsleUsers.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o estasblece el id del usuario de terceros
    ''' </summary>
    ''' <returns></returns>
    Public Property IdUserRequest As Integer? Implements IWarehouse.IdUserRequest
        Get
            Return INDsleUsersRequest.EditValue
        End Get
        Set(value As Integer?)
            INDsleUsersRequest.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IWarehouse.UserXpo
        Get
            Return CType(INDsleUsers.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleUsers.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de usuarios de terceros
    ''' </summary>
    ''' <returns></returns>
    Public Property UserRequestXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IWarehouse.UserRequestXpo
        Get
            Return CType(INDsleUsersRequest.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleUsersRequest.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IWarehouse.Status
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

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HandleRestrictedProducts As Boolean Implements IWarehouse.HandleRestrictedProducts
        Get
            Return INDSleHandleRestrictedProducts.EditValue
        End Get
        Set(value As Boolean)
            INDSleHandleRestrictedProducts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequence As InventorySequence Implements IWarehouse.Sequense
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IWarehouse.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IWarehouse.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IWarehouse.Code
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
    ''' Establece el datasource del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IWarehouse.CostCenterXpo
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IWarehouse.SupplierXpo
        Get
            Return CType(INDsleSupplier.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IWarehouse.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el prefijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Prefix As String Implements IWarehouse.Prefix
        Get
            Return INDtxtPrefix.Text
        End Get
        Set(value As String)
            INDtxtPrefix.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenter As Integer Implements IWarehouse.IdCostCenter
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierId As Integer Implements IWarehouse.SupplierId
        Get
            Return INDsleSupplier.EditValue
        End Get
        Set(value As Integer)
            INDsleSupplier.EditValue = value
        End Set
    End Property



    ''' <summary>
    ''' Establece el datasource de cuenta tercero credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyAccountCreditXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IWarehouse.ThirdPartyAccountCreditXpo
        Get
            Return INDsleMainAccountCredit.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccountCredit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasoruce de cuenta tercero debito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyAccountDebitXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IWarehouse.ThirdPartyAccountDebitXpo
        Get
            Return INDsleMainAccountDebit.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccountDebit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdThirdPartyAccountCredit As Integer? Implements IWarehouse.IdThirdPartyAccountCredit
        Get
            Return INDsleMainAccountCredit.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccountCredit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero debito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdThirdPartyAccountDebit As Integer? Implements IWarehouse.IdThirdPartyAccountDebit
        Get
            Return INDsleMainAccountDebit.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccountDebit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasoruce de centros de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CenterAttentions As DevExpress.Xpo.XPInstantFeedbackSource Implements IWarehouse.CenterAttentions
        Get
            Return INDSleCenterAttention.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCenterAttention.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el centro de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CenterAttentionCode As String Implements IWarehouse.CenterAttentionCode
        Get
            Return INDSleCenterAttention.EditValue
        End Get
        Set(value As String)
            INDSleCenterAttention.EditValue = value
        End Set
    End Property

    Public Property ListConsignimentwarehouseProducts As List(Of Infrastructure.Data.Xpo.InventoryRepository.View.ViewListConsignmentWarehouseProductsXpo) Implements IWarehouse.ListConsignimentwarehouseProducts
        Get
            Return INDGcDetails.DataSource
        End Get
        Set(value As List(Of Infrastructure.Data.Xpo.InventoryRepository.View.ViewListConsignmentWarehouseProductsXpo))
            INDGcDetails.DataSource = value
        End Set
    End Property

    Public Property ViewListConsignmentWarehouseProductsBatchSerial As List(Of Infrastructure.Data.Xpo.InventoryRepository.View.ViewListConsignmentWarehouseProductsBatchSerialXpo) Implements IWarehouse.ViewListConsignmentWarehouseProductsBatchSerialXpo
        Get
            Return INDGcBatch.DataSource
        End Get
        Set(value As List(Of Infrastructure.Data.Xpo.InventoryRepository.View.ViewListConsignmentWarehouseProductsBatchSerialXpo))
            INDGcBatch.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Variable que se utiliza para almacenar las nuevas restricciones
    ''' </summary>
    Private ListNewWarehouseRestrictedConditions As List(Of WarehouseRestrictedConditions)

    ''' <summary>
    ''' Variable que se utiliza para almacenar las restricciones eliminadas
    ''' </summary>
    Private ListDeleteWarehouseRestrictedConditions As List(Of WarehouseRestrictedConditions)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PWarehouse

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Representa la entidad almacen
    ''' </summary>
    ''' <remarks></remarks>
    Dim warehouse As Warehouse

    ''' <summary>
    ''' Listado de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListWarehouseUser As List(Of WarehouseUser)

    ''' <summary>
    ''' Lista de usuarios de terceros
    ''' </summary>
    Dim ListWarehouseUserRequest As List(Of WarehouseUserRequest)

    ''' <summary>
    ''' Listado de usuarios eliminados
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteWarehouseUser As List(Of WarehouseUser)
    ''' <summary>
    ''' Listado de usuarios eliminados de Terceros
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteWarehouseUserRequest As List(Of WarehouseUserRequest)
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
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Usuario xpo
    ''' </summary>
    ''' <remarks></remarks>
    Private _usersXpo As Infrastructure.Data.Xpo.SecurityRepository.UserXpo

    ''' <summary>
    ''' Usuario xpo
    ''' </summary>
    ''' <remarks></remarks>
    Private _usersRequestXpo As Infrastructure.Data.Xpo.SecurityRepository.UserRequestXpo

    ''' <summary>
    ''' Controla el editvalueChanged del control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private ban As Boolean = False

    Private decreaseMaximumLimit As List(Of DecreaseMaximumLimit)
#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me.warehouse IsNot Nothing AndAlso Me.warehouse.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MWarehouse(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteWarehouse(Me.warehouse)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        AssigningValues()
        '' valido que si tengo el parametro activo de restriccion pero no hay detalles
        If Me.warehouse.HandleRestrictedProducts AndAlso Not Me.warehouse.WarehouseRestrictedConditions.Count > 0 Then
            Me.Mensaje(EeventViewerImages.MensajeError) = "El registro no se puede guardar debido a que tiene activo el parámetro Maneja restricción pero no tiene detalles creados"
            Exit Sub
        End If
        Try
            Using Model As New MWarehouse(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of Warehouse) = Await Model.SaveWarehouse(Me.warehouse, decreaseMaximumLimit, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If warehouse.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.warehouse = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.88)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListWarehouse
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _sequence = Nothing
        warehouse = Nothing
        ListWarehouseUser = Nothing
        ListWarehouseUserRequest = Nothing
        ListDeleteWarehouseUser = Nothing
        ListDeleteWarehouseUserRequest = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        _usersXpo = Nothing
        _usersRequestXpo = Nothing
        ban = Nothing
        CenterAttentions = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmStores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyWareHouse, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PWarehouse(Me)
        Presenter.GetSequense()
        Me.LayoutControls.ResetLayouts()
        Me.AddActionsColumns()

        CreateWareHouseType()
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDRpPceBatch_Popup(sender As Object, e As EventArgs) Handles INDRpPceBatch.Popup
        Dim detail = DirectCast(INDGvDetails.GetFocusedRow, ViewListConsignmentWarehouseProductsXpo)
        Presenter.ListConsignimentwarehouseProductsBatchSerial(detail.WarehouseId, detail.ProductId)

    End Sub
    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplier.QueryPopUp
        If INDsleSupplier.Properties.DataSource Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tercero debito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountDebit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccountDebit.QueryPopUp
        If INDsleMainAccountDebit.Properties.DataSource Is Nothing Then
            Presenter.InitializeThirdPartyAccountDebit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tercero credito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountCredit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccountCredit.QueryPopUp
        If INDsleMainAccountCredit.Properties.DataSource Is Nothing Then
            Presenter.InitializeThirdPartyAccountCredit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUsers.QueryPopUp
        If INDsleUsers.Properties.DataSource Is Nothing Then
            Presenter.InitializeUsers()
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUsersResquet_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUsersRequest.QueryPopUp
        If INDsleUsersRequest.Properties.DataSource Is Nothing Then
            Presenter.InitializeUsersRequest()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centros de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCenterAttention_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCenterAttention.QueryPopUp
        If INDSleCenterAttention.Properties.DataSource Is Nothing Then
            Presenter.InitializeCenterAttentions()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tipo de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductType.QueryPopUp
        If INDSleProductType.Properties.DataSource Is Nothing Then
            Using model As New MProductType(Tag)
                INDSleProductType.Properties.DataSource = model.ListAllProductTypes()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de grupo de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductGroup.QueryPopUp
        If INDSleProductGroup.Properties.DataSource Is Nothing Then
            Using model As New Inventory.MVP.MGroup(Tag)
                INDSleProductGroup.Properties.DataSource = model.ListProductGroupsByState(True)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de subgrupo de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSubgroupProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSubgroupProduct.QueryPopUp
        If INDSleSubgroupProduct.Properties.DataSource Is Nothing Then
            Using model As New Inventory.MVP.MGroup(Tag)
                INDSleSubgroupProduct.Properties.DataSource = model.ListProductSubGroupsByStateAndHandlesBatch(True, True)
            End Using
        End If
    End Sub


    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de subgrupo de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleInventoryProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleInventoryProduct.QueryPopUp
        If INDSleSubgroupProduct.Properties.DataSource Is Nothing Then
            Using model As New Inventory.MVP.MGroup(Tag)
                INDSleInventoryProduct.Properties.DataSource = model.GetInventoryProductXpo(True)
            End Using
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmStores_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewWarehouse()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmStores_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de cuentas contables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountDebit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccountDebit.ButtonClick, INDsleMainAccountCredit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeMainAccounts()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Maintenance.FrmSupplier With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUsers_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUsers.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Security.FrmUsers With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeUsers()
        End If
    End Sub
    Private Sub INDsleUsersRequest_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUsersRequest.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Security.FrmUsers With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeUsers()
        End If
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        DeleteWarehouseUser()
    End Sub

    Private Sub IndigoGridView11_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView11.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        DeleteWarehouseConditions()
    End Sub

    Private Sub IndigoGridView111_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView111.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        DeleteWarehouseUserRequest()
    End Sub

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions, IndigoGridView2.Click_ButtonAction
        OpenConsignmentTransferDetail()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleCenterAttention_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCenterAttention.EditValueChanged
        If String.IsNullOrEmpty(INDSleCenterAttention.EditValue) Then
            CenterAttentionCode = Nothing
            INDSleCenterAttention.Properties.NullText = String.Empty
        End If
    End Sub


    Private Sub INDsleUsers_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUsers.EditValueChanged
        If INDsleUsers.EditValue IsNot Nothing AndAlso ban = False Then
            _usersXpo = DirectCast(DirectCast(viewUserSearch.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.SecurityRepository.UserXpo)
        End If
    End Sub

    Private Sub INDsleUsersRequest_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUsersRequest.EditValueChanged
        If INDsleUsersRequest.EditValue IsNot Nothing AndAlso ban = False Then
            _usersXpo = DirectCast(DirectCast(viewUserSearch1.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.SecurityRepository.UserXpo)
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddUser_Click(sender As Object, e As EventArgs) Handles INDbtnAddUser.Click
        If INDsleUsers.EditValue IsNot Nothing Then
            CreateWarehouseUser()
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedUser", NAME_MODULE)
            INDsleUsers.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar Usurio de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddUserResquest_Click(sender As Object, e As EventArgs) Handles INDbtnAddUserRequest.Click
        If INDsleUsersRequest.EditValue IsNot Nothing Then
            CreateWarehouseUserRequest()
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedUser", NAME_MODULE)
            INDsleUsersRequest.Focus()
        End If
    End Sub
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga el segmento autorizacion de solicitudes
    ''' </summary>
    Private Sub LoadUserAuthorizationRequest()
        Dim requiresAuthorization As String = Code

        'If requiresAuthorization.HasValue Then
        Dim RequestParam = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ViewRequestParamWarehouseXpo)($"WarehouseCode='{requiresAuthorization}'")

        'LayoutControlGroup1.BeginUpdate()
        If RequestParam IsNot Nothing Then
            If Not RequestParam.RequiredAuthorization Then
                INDlygAuthorizationRequest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDlygAuthorizationRequest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
        'LayoutControlGroup1.EndUpdate()
    End Sub
    ''' <summary>
    ''' Crea y asigna la lista de tipo almacen
    ''' combo de almacén virtual
    ''' </summary>
    Private Sub CreateWareHouseType()
        Dim listOptions As New List(Of Tuple(Of Byte, String))()
        listOptions.Add(New Tuple(Of Byte, String)(0, "Ninguna"))
        listOptions.Add(New Tuple(Of Byte, String)(1, "Almacén Virtual"))
        listOptions.Add(New Tuple(Of Byte, String)(2, "Almacén de Consignación"))
        listOptions.Add(New Tuple(Of Byte, String)(3, "Almacén de Custodia"))
        listOptions.Add(New Tuple(Of Byte, String)(4, "Almacén de Transito"))
        listOptions.Add(New Tuple(Of Byte, String)(5, "Almacén de Control"))
        listOptions.Add(New Tuple(Of Byte, String)(6, "Almacén de Remanentes"))
        Me.INDSleTypeWareHouse.Properties.DataSource = listOptions

        Dim RestrictedProductsOptions As New List(Of Tuple(Of Byte, String))()
        RestrictedProductsOptions.Add(New Tuple(Of Byte, String)(0, "No"))
        RestrictedProductsOptions.Add(New Tuple(Of Byte, String)(1, "Si"))
        Me.INDSleHandleRestrictedProducts.Properties.DataSource = RestrictedProductsOptions

        Dim ListRuleType = New List(Of Tuple(Of Integer, String))
        ListRuleType.Add(New Tuple(Of Integer, String)(1, "Tipo de producto"))
        ListRuleType.Add(New Tuple(Of Integer, String)(2, "Grupo de producto"))
        ListRuleType.Add(New Tuple(Of Integer, String)(3, "Subgrupo de producto"))
        ListRuleType.Add(New Tuple(Of Integer, String)(4, "Producto"))
        INDsleRuleType.Properties.DataSource = ListRuleType.ToList

        Dim ListRestriction = New List(Of Tuple(Of Integer, String))
        ListRestriction.Add(New Tuple(Of Integer, String)(1, "Habilita los productos"))
        ListRestriction.Add(New Tuple(Of Integer, String)(2, "Restringe los productos"))
        INDsleRestrictionType.Properties.DataSource = ListRestriction.ToList
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IWarehouse.ActionsOnControls
        Set(value As Boolean)
            INDlyWareHouse.BeginUpdate()

            INDbtnCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDtxtPrefix.Enabled = value
            INDsleCostCenter.Enabled = value
            INDsleSupplier.Enabled = value
            INDSleCenterAttention.Enabled = value
            INDsleMainAccountDebit.Enabled = value
            INDsleMainAccountCredit.Enabled = value
            INDSleTypeWareHouse.Enabled = value
            INDSleHandleRestrictedProducts.Enabled = value
            INDsleUsers.Enabled = value
            INDgcUsers.Enabled = value
            INDGcDetails.Enabled = value
            INDbtnAddUser.Enabled = value
            INDsleUsersRequest.Enabled = value
            INDbtnAddUserRequest.Enabled = value
            INDgcUsersRequest.Enabled = value


            INDlyWareHouse.EndUpdate()

            If value Then
                INDtxtDescription.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.warehouse IsNot Nothing AndAlso Me.warehouse.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.warehouse.Code, Me.warehouse.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.warehouse.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.warehouse.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.warehouse.Code, Me.warehouse.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.warehouse.Code)
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
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlyWareHouse.BeginUpdate()

        ActionsOnControls = False
        Await DeleteBlockedRecord()

        'Limpiar controles
        Code = String.Empty
        Description = String.Empty
        Prefix = String.Empty
        SupplierId = Nothing
        INDsleSupplier.Properties.NullText = String.Empty
        CenterAttentionCode = Nothing
        INDSleCenterAttention.Properties.NullText = String.Empty
        IdCostCenter = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty
        IdThirdPartyAccountCredit = Nothing
        INDsleMainAccountCredit.Properties.NullText = String.Empty
        IdThirdPartyAccountDebit = Nothing
        INDsleMainAccountDebit.Properties.NullText = String.Empty
        HandleRestrictedProducts = Nothing
        ListNewWarehouseRestrictedConditions = Nothing

        INDSleTypeWareHouse.EditValue = 0
        INDsleUsers.EditValue = Nothing
        INDgcUsers.DataSource = Nothing
        INDGcDetails.DataSource = Nothing
        INDsleUsersRequest.EditValue = Nothing
        INDgcUsersRequest.DataSource = Nothing
        ListWarehouseUser = Nothing
        ListWarehouseUserRequest = Nothing
        ListDeleteWarehouseUser = Nothing
        ListDeleteWarehouseUserRequest = Nothing
        decreaseMaximumLimit = Nothing
        INDgcProductControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlygRestricction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygAuthorizationRequest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDgcRates.DataSource = Nothing
        INDgcRates.RefreshDataSource()
        Me._doc = Nothing
        Me.warehouse = Nothing
        Me.Status = True

        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        INDlyWareHouse.EndUpdate()
        CleandControlsPopup()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With warehouse
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Description
            If Prefix Is Nothing Then
                .Prefix = ""
            Else
                .Prefix = Prefix
            End If
            .SupplierId = SupplierId
            .CodeCenterAttention = CenterAttentionCode
            .CostCenterId = IdCostCenter
            .LoanThirdPartyDebitAccountId = IdThirdPartyAccountDebit.Value
            .LoanThirdPartyCreditAccountId = IdThirdPartyAccountCredit.Value
            .WareHouseType = INDSleTypeWareHouse.EditValue
            .HandleRestrictedProducts = HandleRestrictedProducts

            If ListWarehouseUser IsNot Nothing Then
                For Each itemUser As WarehouseUser In ListWarehouseUser
                    .WarehouseUser.Add(itemUser)
                Next
            End If

            If ListWarehouseUserRequest IsNot Nothing Then
                For Each itemUser As WarehouseUserRequest In ListWarehouseUserRequest
                    .WarehouseUserRequest.Add(itemUser)
                Next
            End If

            If ListNewWarehouseRestrictedConditions IsNot Nothing Then
                For Each itemCondition As WarehouseRestrictedConditions In ListNewWarehouseRestrictedConditions
                    .WarehouseRestrictedConditions.Add(itemCondition)
                Next
            End If

            If Not HandleRestrictedProducts Then
                If ListNewWarehouseRestrictedConditions IsNot Nothing Then
                    For Each deleteItem In ListNewWarehouseRestrictedConditions
                        If Not deleteItem.Id > 0 Then
                            .WarehouseRestrictedConditions.Remove(deleteItem)
                        End If
                    Next
                End If
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MWarehouse(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetWarehouse(INDbtnCode.Text.Trim)
                    warehouse = resultOperation.ObjectEmbbeded
                    INDlyWareHouse.BeginUpdate()
                    If warehouse IsNot Nothing AndAlso warehouse.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(warehouse.Id))
                            With warehouse
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                Description = .Name
                                Prefix = .Prefix
                                SupplierId = .SupplierId
                                INDsleSupplier.Properties.NullText = .CodeNameSupplier
                                If .CodeCenterAttention IsNot Nothing Then
                                    CenterAttentionCode = .CodeCenterAttention.Trim()
                                End If
                                INDSleCenterAttention.Properties.NullText = .CenterAttentionDescription
                                IdCostCenter = .CostCenterId
                                INDsleCostCenter.Properties.NullText = .CostCenterDescription
                                IdThirdPartyAccountCredit = .LoanThirdPartyCreditAccountId
                                INDsleMainAccountCredit.Properties.NullText = .MainAccountThirdPartyCreditDescription
                                IdThirdPartyAccountDebit = .LoanThirdPartyDebitAccountId
                                INDsleMainAccountDebit.Properties.NullText = .MainAccountThirdPartyDebitDescription
                                HandleRestrictedProducts = .HandleRestrictedProducts
                                INDSleTypeWareHouse.EditValue = .WareHouseType

                                ValidationProductControl()
                                If ValidationProductControl() = True Then
                                    Presenter.ListConsignmentWarehouseProducts(.Id)
                                End If

                                ListWarehouseUser = .WarehouseUser.ToList
                                INDgcUsers.DataSource = Nothing
                                INDgcUsers.DataSource = ListWarehouseUser

                                ListWarehouseUserRequest = .WarehouseUserRequest.ToList
                                INDgcUsersRequest.DataSource = Nothing
                                INDgcUsersRequest.DataSource = ListWarehouseUserRequest

                                Await loadConditions()
                                Status = .Status
                                LoadUserAuthorizationRequest()
                            End With


                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.warehouse.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = warehouse.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(warehouse.Id, Me.Tag.ToString(), Nothing, GetType(Warehouse).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True

                        End Using
                    Else
                        AsyncLoader(False)
                        If resultOperation IsNot Nothing AndAlso resultOperation.StateResult = False AndAlso resultOperation.MessageResult.Count > 0 Then
                            Me.Mensaje(EeventViewerImages.Advertencia) = String.Join(" , ", resultOperation.MessageResult)
                        End If
                        If Me._sequence.IsManual Then
                            Await Me.NewWarehouse()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyWareHouse.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Public Async Function loadConditions() As Task
        If ListNewWarehouseRestrictedConditions Is Nothing Then
            ListNewWarehouseRestrictedConditions = New List(Of WarehouseRestrictedConditions)
        End If
        ListNewWarehouseRestrictedConditions = Await Task.Factory.StartNew(Function()
                                                                               Dim listXpo As XPCollection = Presenter.ListWarehouseConditions(warehouse.Id)

                                                                               If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                                                                                   For Each itemXpo As WarehouseRestrictedConditionsXpo In listXpo
                                                                                       Dim _warehouseRestrictedConditions As New WarehouseRestrictedConditions
                                                                                       _warehouseRestrictedConditions.StartTracking()
                                                                                       With _warehouseRestrictedConditions
                                                                                           .Id = itemXpo.Id
                                                                                           .WarehouseId = itemXpo.WarehouseId
                                                                                           .ConditionType = itemXpo.ConditionType

                                                                                           .ProductTypeId = itemXpo.ProductTypeId?.Id
                                                                                           .ProductGroupId = itemXpo.ProductGroupId?.Id
                                                                                           .ProductSubgroupId = itemXpo.ProductSubgroupId?.Id
                                                                                           .ProductId = itemXpo.ProductId?.Id
                                                                                           .RestrictionType = itemXpo.RestrictionType
                                                                                           .EntityName = itemXpo.EntityName
                                                                                           .MarkAsUnchanged()
                                                                                       End With

                                                                                       ListNewWarehouseRestrictedConditions.Add(_warehouseRestrictedConditions)
                                                                                   Next
                                                                               End If

                                                                               Return ListNewWarehouseRestrictedConditions
                                                                           End Function)
        INDgcRates.DataSource = ListNewWarehouseRestrictedConditions
    End Function
    ''' <summary>
    '''  valida si el almacen es de consignación y si lo es me carga los producto que tiene esta almacen
    ''' </summary>
    Public Function ValidationProductControl() As Boolean
        If INDSleTypeWareHouse.EditValue = 2 Then
            INDgcProductControl.Enabled = True
            INDgcProductControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Return True
        Else
            INDgcProductControl.Enabled = False
            INDgcProductControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Return False
        End If
    End Function
    Private Sub AddActionsColumns()
        IndigoGridView11.SetListAcction(viewRates, {eAcciones.Remove}.ToList())
        IndigoGridView2.SetListAcction(INDGvDetails, {eAcciones.Edit}.ToList())
        IndigoGridView1.SetListAcction(viewUsersGrid, {eAcciones.Remove}.ToList())
        IndigoGridView111.SetListAcction(viewUsersGrid1, {eAcciones.Remove}.ToList())

        IndigoGridControl1.RefreshGrid(INDgcUsers)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvDetails.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRates.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewWarehouse() As Task
        warehouse = New Warehouse() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
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
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
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

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.warehouse.Code) Then
            Try
                Using model As New MWarehouse(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.warehouse.Status
                    Dim result As ActionResult(Of Warehouse) = Await model.ChangeState(Me.warehouse.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.warehouse = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Crea el objeto para el listado de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateWarehouseUser()
        If ListWarehouseUser Is Nothing Then
            ListWarehouseUser = New List(Of WarehouseUser)
        Else
            Dim cont As Integer = ListWarehouseUser.FindAll(Function(item) item.UserId = IdUser).ToList().Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserDuplicated", NAME_MODULE)
                INDsleUsers.Focus()
                Exit Sub
            End If
        End If
        Dim warehouseUser As New WarehouseUser
        With warehouseUser
            .UserId = _usersXpo.Id
            .UserCode = _usersXpo.UserCode
            .FullNameUser = _usersXpo.IdPerson.Fullname
        End With
        ListWarehouseUser.Add(warehouseUser)
        warehouse.WarehouseUser.Add(warehouseUser)
		If warehouse.Id > 0 Then
			warehouse.MarkAsModified()
		End If

		INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = ListWarehouseUser
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UserAggregatedSatisfactory", NAME_MODULE)
        INDsleUsers.EditValue = Nothing
        INDsleUsers.Focus()
    End Sub
    ''' <summary>
    ''' Metodo que crea el objeto del listado de la rejilla o GridControl INDgcUsersRequest
    ''' </summary>
    Private Sub CreateWarehouseUserRequest()
        If ListWarehouseUserRequest Is Nothing Then
            ListWarehouseUserRequest = New List(Of WarehouseUserRequest)
        Else
            Dim cont As Integer = ListWarehouseUserRequest.FindAll(Function(item) item.UserId = IdUserRequest).ToList().Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserDuplicated", NAME_MODULE)
                INDsleUsersRequest.Focus()
                Exit Sub
            End If
        End If
        Dim warehouseUserRequest As New WarehouseUserRequest
        With warehouseUserRequest
            .UserId = _usersXpo.Id
            .UserCode = _usersXpo.UserCode
            .FullNameUser = _usersXpo.IdPerson.Fullname
        End With
        ListWarehouseUserRequest.Add(warehouseUserRequest)
        warehouse.WarehouseUserRequest.Add(warehouseUserRequest)
        If warehouse.Id > 0 Then
            warehouse.MarkAsModified()
        End If

        INDGcDetails.DataSource = Nothing


        INDgcUsersRequest.DataSource = Nothing
        INDgcUsersRequest.DataSource = ListWarehouseUserRequest
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UserAggregatedSatisfactory", NAME_MODULE)
        INDsleUsersRequest.EditValue = Nothing
        INDsleUsersRequest.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un usuario de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteWarehouseUser()
        Dim wu As WarehouseUser = CType(viewUsersGrid.GetFocusedRow, WarehouseUser)
        If wu.Id <> 0 Then
            If ListDeleteWarehouseUser Is Nothing Then
                ListDeleteWarehouseUser = New List(Of WarehouseUser)
            End If
            wu.MarkAsDeleted()
            ListDeleteWarehouseUser.Add(wu)
        End If
        ListWarehouseUser.Remove(wu)
        warehouse.WarehouseUser.Remove(wu)
        If warehouse.Id > 0 Then
            warehouse.MarkAsModified()
        End If

        INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = ListWarehouseUser
    End Sub

    Private Sub DeleteWarehouseUserRequest()
        Dim wu As WarehouseUserRequest = CType(viewUsersGrid1.GetFocusedRow, WarehouseUserRequest)
        If wu.Id <> 0 Then
            If ListDeleteWarehouseUserRequest Is Nothing Then
                ListDeleteWarehouseUserRequest = New List(Of WarehouseUserRequest)
            End If
            wu.MarkAsDeleted()
            ListDeleteWarehouseUserRequest.Add(wu)
        End If
        ListWarehouseUserRequest.Remove(wu)
        warehouse.WarehouseUserRequest.Remove(wu)
        If warehouse.Id > 0 Then
            warehouse.MarkAsModified()
        End If

        INDgcUsersRequest.DataSource = Nothing
        INDgcUsersRequest.DataSource = ListWarehouseUserRequest
    End Sub

    Private Sub DeleteWarehouseConditions()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim row = DirectCast(viewRates.GetFocusedRow(), WarehouseRestrictedConditions)
            Dim _indexEditRecord = Me.ListNewWarehouseRestrictedConditions.IndexOf(row)
            If ListNewWarehouseRestrictedConditions.Item(_indexEditRecord).Id > 0 Then
                ListNewWarehouseRestrictedConditions.Item(_indexEditRecord).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
            Else
                ListNewWarehouseRestrictedConditions.RemoveAt(_indexEditRecord)
            End If
            INDgcRates.DataSource = ListNewWarehouseRestrictedConditions.Where(Function(x) x.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList()
            INDgcRates.RefreshDataSource()
        End If
    End Sub

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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
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
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub INDSleTypeWareHouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleTypeWareHouse.EditValueChanged

        ValidationProductControl()
    End Sub

    Private Sub INDRpPceBatch_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDRpPceBatch.Closed
        INDGcBatch.DataSource = Nothing
        INDGcBatch.RefreshDataSource()
    End Sub



#End Region
    ''' <summary>l
    ''' Abre el PoPup para hacer el decremto del limite maximo del producto que esta en almacen de consignación 
    ''' </summary>   
    Private Sub OpenConsignmentTransferDetail()
        If _SourceProduct Is Nothing Then
            _SourceProduct = DirectCast(INDGvDetails.GetFocusedRow, ViewListConsignmentWarehouseProductsXpo)
        End If
        Using frm As New FrmPopupProductDecreaseMaximumLimit()
            frm._SourceProducts = _SourceProduct
            AddHandler frm.OnAddConsignmentProduct, AddressOf AddDecreaseMaximumLimit
            AddHandler frm.Shown, Sub()
                                      frm.EditDetail(_SourceProduct)
                                  End Sub

            Dim tr As New FrmTransparent(frm, False)
            tr.ShowDialog(Me)
        End Using
        _SourceProduct = Nothing
    End Sub

    ''' <summary>
    ''' Agrega el detalle a la entidad 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="Detail"></param>
    Private Sub AddDecreaseMaximumLimit(sender As Object, Detail As ViewListConsignmentWarehouseProductsXpo)
        If decreaseMaximumLimit Is Nothing Then decreaseMaximumLimit = New List(Of DecreaseMaximumLimit)
        If Detail.DecreaseQuantity > 0 Then
            decreaseMaximumLimit.Add(New DecreaseMaximumLimit With
                                     {
                                     .ProductId = Detail.ProductId,
                                     .WarehouseId = Detail.WarehouseId,
                                     .Quantity = Detail.DecreaseQuantity,
                                     .Justification = Detail.Justificaton
                                    })
        End If
        ListConsignimentwarehouseProducts.Where(Function(f) f.ProductId = Detail.ProductId AndAlso f.WarehouseId = Detail.WarehouseId).FirstOrDefault().QuantityMax = Detail.QuantityMax
        INDGcDetails.RefreshDataSource()
        INDGcDetails.DataSource = ListConsignimentwarehouseProducts.Where(Function(x) x.QuantityMax > 0).ToList()
    End Sub

    Private Sub INDSleHandleRestrictedProducts_EditVallueChanged(sender As Object, e As EventArgs) Handles INDSleHandleRestrictedProducts.EditValueChanged
        If HandleRestrictedProducts Then
            INDlygRestricction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlygRestricction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDbtnAddFirstCondition_Click(sender As Object, e As EventArgs) Handles INDbtnAddFirstCondition.Click
        ValidateControlsPopup()
        If ListNewWarehouseRestrictedConditions Is Nothing Then
            ListNewWarehouseRestrictedConditions = New List(Of WarehouseRestrictedConditions)
        End If
        Dim newCondition As WarehouseRestrictedConditions = New WarehouseRestrictedConditions()
        Dim entityName = ""
        Select Case INDsleRuleType.EditValue
            Case 1
                entityName = INDSleProductType.Text
            Case 2
                entityName = INDSleProductGroup.Text
            Case 3
                entityName = INDSleSubgroupProduct.Text
            Case 4
                entityName = INDSleInventoryProduct.Text
        End Select
        With newCondition
            .ConditionType = INDsleRuleType.EditValue
            .ProductTypeId = INDSleProductType.EditValue
            .ProductGroupId = INDSleProductGroup.EditValue
            .ProductSubgroupId = INDSleSubgroupProduct.EditValue
            .RestrictionType = INDsleRestrictionType.EditValue
            .ProductId = INDSleInventoryProduct.EditValue
            .EntityName = entityName
        End With
        If Not ListNewWarehouseRestrictedConditions.Contains(newCondition) Then
            ListNewWarehouseRestrictedConditions.Add(newCondition)
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Ya agregó una restricción con las mismas condiciones"
        End If
        INDgcRates.DataSource = ListNewWarehouseRestrictedConditions
        INDgcRates.RefreshDataSource()
        CleandControlsPopup()
    End Sub

    Public Sub CleandControlsPopup()
        INDsleRuleType.EditValue = Nothing
        INDSleProductType.EditValue = Nothing
        INDSleProductGroup.EditValue = Nothing
        INDSleSubgroupProduct.EditValue = Nothing
        INDsleRestrictionType.EditValue = Nothing
        INDSleInventoryProduct.EditValue = Nothing
        INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemInventoryProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Public Sub ValidateControlsPopup()
        If INDsleRuleType.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "El campo Condición es obligatorio"
            Exit Sub
        End If
        Select Case INDsleRuleType.EditValue
            Case 1
                If INDSleProductType.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Tipo de Producto es obligatorio"
                    Exit Sub
                End If
            Case 2
                If INDSleProductGroup.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Grupo de Producto es obligatorio"
                    Exit Sub
                End If
            Case 3
                If INDSleSubgroupProduct.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Subgrupo de Producto es obligatorio"
                    Exit Sub
                End If
            Case 4
                If INDSleInventoryProduct.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Producto es obligatorio"
                    Exit Sub
                End If
        End Select
        If INDsleRestrictionType.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "El campo tipo de restricción es obligatorio"
            Exit Sub
        End If
    End Sub

    Private Sub INDsleRuleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRuleType.EditValueChanged
        Select Case INDsleRuleType.EditValue
            Case 1
                INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemInventoryProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case 2
                INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemInventoryProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case 3
                INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemInventoryProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case 4
                INDlyItemPorductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPorductGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSubgroupProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemInventoryProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End Select
    End Sub

    Private Sub INDpceRates_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceRates.CloseUp
        CleandControlsPopup()
    End Sub
End Class
'***********************************************************************
' Assembly         : Presentation.Inventory
' Author           : Mariana Gonzalez
' Created          : 19-11-2025
'
' Description      : Formulario para importar solicitudes de traslado en consignación
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmConsignmentTransferImport

#Region "Events"
    ''' <summary>
    ''' Evento para obtener el listado del detalle del traslado
    ''' </summary>
    Public Event GetListConsignmentTransferDetail(sender As Object, e As AddProductConsignmentTransfer)
#End Region

#Region "Globals"
    ''' <summary>
    ''' Listado de solicitudes de traslado
    ''' </summary>
    Private listInventoryRequestDetailOther As New List(Of ViewListRequestDetailImport)

    ''' <summary>
    ''' Tipo de orden 
    ''' </summary>
    Private _orderType As Byte

    ''' <summary>
    ''' Despachar a
    ''' </summary>
    Private _dispatchTo As Byte?

    ''' <summary>
    ''' ID de la unidad funcional o almacén para filtrar las solicitudes
    ''' </summary>
    Private _filterFunctionalUnitWarehouse As Integer

    ''' <summary>
    ''' ID del almacén de origen
    ''' </summary>
    Private _warehouseId As Integer

    ''' <summary>
    ''' Lista que se llena desde la vista XPO
    ''' </summary>
    Private listInventoryRequestDetailOtherXpo As XPCollection

    ''' <summary>
    ''' Listado del detalle del traslado cuando se importa información
    ''' </summary>
    Private _listConsignmentTransferDetailImportInfo As List(Of ConsignmentTransferDetail)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad para asignar el tipo de orden de traslado
    ''' </summary>
    Public WriteOnly Property OrderType As Byte
        Set(value As Byte)
            _orderType = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar despachar a de orden de traslado
    ''' </summary>
    Public WriteOnly Property DispatchTo As Byte?
        Set(value As Byte?)
            _dispatchTo = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el ID de la unidad funcional o del almacén para filtrar las solicitudes
    ''' </summary>
    Public WriteOnly Property FilterFunctionalUnitWarehouse As Integer
        Set(value As Integer)
            _filterFunctionalUnitWarehouse = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el ID del almacén de origen
    ''' </summary>
    Public WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para mostrar mensajes
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    ''' <summary>
    ''' Propiedad para pasar el listado del detalle del traslado
    ''' </summary>
    Public Property ListConsignmentTransferDetailValidation As List(Of ConsignmentTransferDetail)
        Get
            Return _listConsignmentTransferDetailImportInfo
        End Get
        Set(value As List(Of ConsignmentTransferDetail))
            _listConsignmentTransferDetailImportInfo = value
        End Set
    End Property
#End Region

#Region "Load Data"
    ''' <summary>
    ''' Carga el origen de datos con las solicitudes de traslado filtradas
    ''' </summary>
    Private Sub LoadDataSource()
        Try
            IndigoGridControl1.RefreshGrid(INDGcProduct)

            If _filterFunctionalUnitWarehouse = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se ha configurado el almacén origen"
                Exit Sub
            End If

            Using model As New MBusqueda
                ' Filtrar por: almacén origen
                Dim filter() As Object = {_filterFunctionalUnitWarehouse}
                listInventoryRequestDetailOtherXpo = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).InventoryService.ListViewRequestDetailByWarehouse(
                    CInt(filter.ElementAt(0)))

                LoadListInventoryRequest(listInventoryRequestDetailOtherXpo)

                If listInventoryRequestDetailOther Is Nothing OrElse listInventoryRequestDetailOther.Count = 0 Then
                    Mensaje(EeventViewerImages.Informacion) = "No se encontraron solicitudes de traslado pendientes para el almacén seleccionado"
                End If

                INDGcProduct.DataSource = Nothing
                INDGcProduct.DataSource = listInventoryRequestDetailOther
                INDGcProduct.RefreshDataSource()
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error al cargar solicitudes: {ex.Message}"
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista de solicitudes desde XPO
    ''' </summary>
    Private Sub LoadListInventoryRequest(ByVal listViewRequest As XPCollection)
        listInventoryRequestDetailOther = New List(Of ViewListRequestDetailImport)
        If listViewRequest IsNot Nothing AndAlso listViewRequest.Count > 0 Then
            For Each itemXpo As ViewListRequestDetailImportXpo In listViewRequest
                Dim requestDetail As New ViewListRequestDetailImport
                With requestDetail
                    .InventoryRequestDetailType = itemXpo.InventoryRequestDetailType
                    .Code = itemXpo.Code
                    .DocumentDate = itemXpo.DocumentDate
                    .Row = itemXpo.Row
                    .ComponentType = itemXpo.ComponentType
                    .EntityId = itemXpo.EntityId
                    .SourceCode = itemXpo.SourceCode
                    .SourceCodeName = itemXpo.SourceCodeName
                    .QuantityRequested = itemXpo.QuantityRequested
                    .QuantityDelivered = itemXpo.QuantityDelivered
                    .OutstandingQuantity = itemXpo.OutstandingQuantity
                    .DescriptionProduct = itemXpo.DescriptionProduct
                    .TargetWarehouseId = itemXpo.TargetWarehouseId
                    .TargetFunctionalUnitId = itemXpo.TargetFunctionalUnitId
                    .SourceWarehouseId = itemXpo.SourceWarehouseId
                    .CUMSourceCodeName = itemXpo.CUMSourceCodeName
                    .Status = itemXpo.Status
                    .Activated = False
                    .ItemInvalid = False
                    .ComponentTypeName = itemXpo.ComponentTypeName
                End With
                listInventoryRequestDetailOther.Add(requestDetail)
            Next
        End If
    End Sub
#End Region

#Region "Handles"
    ''' <summary>
    ''' Se dispara al cargar el formulario
    ''' </summary>
    Private Sub FrmConsignmentTransferImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' NO cargar datos aquí, se cargan después de asignar propiedades
    End Sub

    ''' <summary>
    ''' Método público para cargar datos después de configurar propiedades
    ''' </summary>
    Public Sub LoadData()
        LoadDataSource()
    End Sub

    ''' <summary>
    ''' Cerrar formulario con tecla Escape
    ''' </summary>
    Private Sub FrmConsignmentTransferImport_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Activar o desactivar todos los items al hacer doble clic en la columna de checkbox
    ''' </summary>
    Private Sub INDGcProduct_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcProduct.MouseDoubleClick
        Dim hitPoint = Me.INDGvProduct.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then
                If INDGvProduct.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailImport).Where(Function(s) s.Activated).ToList().Count = INDGvProduct.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailImport).Count Then
                    INDGvProduct.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailImport).ForEach(Sub(x) x.Activated = False)
                Else
                    INDGvProduct.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailImport).ForEach(Sub(x) x.Activated = True)
                End If
                Me.INDGcProduct.RefreshDataSource()
            End If
            Me.INDGcProduct.Invalidate()
        End If
    End Sub

    ''' <summary>
    ''' Actualizar imagen de la columna cuando cambia el checkbox
    ''' </summary>
    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim request = DirectCast(INDGvProduct.GetFocusedRow(), ViewListRequestDetailImport)
        request.Activated = checkControl.EditValue
        SetImageActivateColumn()
        Me.INDGcProduct.RefreshDataSource()
        Me.INDGcProduct.Invalidate()
    End Sub

    ''' <summary>
    ''' Se dispara al hacer clic en el botón Aceptar
    ''' </summary>
    Private Sub INDSmbAccept_Click(sender As Object, e As EventArgs) Handles INDSmbAccept.Click
        Dim errors = ValidateRecords()
        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        Dim args As New AddProductConsignmentTransfer
        args.ListConsignmentTransferDetail = GenerateConsignmentTransferDetail()
        If args.ListConsignmentTransferDetail.Count > 0 Then
            Me.Close()
            RaiseEvent GetListConsignmentTransferDetail(Nothing, args)
        End If
    End Sub

    ''' <summary>
    ''' Disposing
    ''' </summary>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listInventoryRequestDetailOther = Nothing
        _listConsignmentTransferDetailImportInfo = Nothing
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Genera los detalles de traslado a partir de las solicitudes seleccionadas
    ''' </summary>
    Private Function GenerateConsignmentTransferDetail() As TrackableCollection(Of ConsignmentTransferDetail)
        Dim listConsignmentTransferDetailTmp As New TrackableCollection(Of ConsignmentTransferDetail)
        Dim consignmentDetailTmp As ConsignmentTransferDetail = Nothing

        ' Generar los detalles por solicitud de traslado
        For Each item In listInventoryRequestDetailOther.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            consignmentDetailTmp = New ConsignmentTransferDetail
            With consignmentDetailTmp
                .ProductId = item.EntityId
                .Quantity = item.OutstandingQuantity
                .WarehouseId = item.TargetWarehouseId
            End With
            listConsignmentTransferDetailTmp.Add(consignmentDetailTmp)
        Next

        Return listConsignmentTransferDetailTmp
    End Function

    ''' <summary>
    ''' Valida los datos para poder importarlos
    ''' </summary>
    Private Function ValidateRecords() As List(Of String)
        Dim errors As New List(Of String)

        Dim listRequestDetailActivated = listInventoryRequestDetailOther.FindAll(Function(x) x.Activated = True)
        If listRequestDetailActivated.Count = 0 Then
            errors.Add("Debe seleccionar al menos una solicitud de traslado")
        End If

        If errors.Count = 0 Then
            If _listConsignmentTransferDetailImportInfo IsNot Nothing AndAlso _listConsignmentTransferDetailImportInfo.Count > 0 Then
                ' Validar que el producto no esté con el mismo almacén destino en los items que están seleccionados
                For Each item In listRequestDetailActivated
                    Dim consignmentDetailTmp = _listConsignmentTransferDetailImportInfo.Find(Function(x) x.ProductId = item.EntityId AndAlso x.WarehouseId = item.TargetWarehouseId)
                    If consignmentDetailTmp IsNot Nothing Then
                        errors.Add($"El producto {item.DescriptionProduct} ya está agregado para este almacén destino")
                        item.ItemInvalid = True
                    End If
                Next
            End If
        End If

        Return errors
    End Function

    ''' <summary>
    ''' Método para establecer la imagen a la columna de activar o inactivar
    ''' </summary>
    Private Sub SetImageActivateColumn()
        Dim listActivated = listInventoryRequestDetailOther.FindAll(Function(x) x.Activated = True)
        If listActivated.Count = listInventoryRequestDetailOther.Count Then
            Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
        Else
            Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        End If
    End Sub
#End Region

End Class
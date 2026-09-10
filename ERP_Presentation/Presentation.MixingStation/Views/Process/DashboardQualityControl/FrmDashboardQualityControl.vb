'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Andrés Felipe Aros Escobar
' Created          : 28/09/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Dynamic
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.Utils
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.MixingStation.MVP


#End Region

Public Class FrmDashboardQualityControl

#Region "Variables"

    Private _selectedItems As List(Of ViewQualityControlXpo)
    Private _lockObject As New Object()
    Private ReadOnly Property SelectedItems As List(Of ViewQualityControlXpo)
        Get
            UpdateSelectedItems()
            Return _selectedItems
        End Get
    End Property

    ''' <summary>
    '''  Comienza intentando adquirir un bloqueo sobre un objeto compartido para asegurar que la manipulación de datos 
    '''  sea segura en un entorno multihilo, utilizando un tiempo máximo de espera de 2 segundos para obtener el bloqueo (recursos)
    ''' </summary>
    Private Async Sub UpdateAndVisualizeSelectionMasive()
        Dim lockAcquired As Boolean = False
        Try
            ' Intenta obtener el bloqueo con un tiempo máximo de espera de 2 segundos
            Monitor.TryEnter(_lockObject, TimeSpan.FromSeconds(2), lockAcquired)
            If Not lockAcquired Then
                Throw New Exception("No se puede obtener la informacion de las filas")
            End If
            If lockAcquired Then
                Dim selectedRows = INDgviewQualityControl.GetSelectedRows()
                INDgviewQualityControl.ClearSelection() ' Limpia la selección actual para simular desde el inicio
                ' Lista temporal para acumular los objetos seleccionados
                Dim tempSelectedItems As New HashSet(Of ViewQualityControlXpo)
                ' Comienza la actualización de la UI
                INDgviewQualityControl.OptionsBehavior.BeginUpdate()
                INDgviewQualityControl.ShowLoadingPanel()
                For Each handle As Integer In selectedRows
                    If handle >= 0 Then
                        INDgviewQualityControl.FocusedRowHandle = handle
                        INDgviewQualityControl.SelectRow(handle)
                        Dim row As Object = INDgviewQualityControl.GetRow(handle)
                        If TypeOf row IsNot DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                            Dim newRow = Await EnsureRowIsVisibleAndLoadData(handle)
                            If newRow IsNot Nothing Then
                                row = newRow.OriginalRow
                            Else
                            End If
                        Else
                            row = row.OriginalRow
                        End If
                        If row IsNot Nothing AndAlso TypeOf row Is ViewQualityControlXpo Then
                            If tempSelectedItems?.Any And Not tempSelectedItems.Contains(row) Then
                                tempSelectedItems.Add(row) ' Agrega el objeto a la lista temporal y selecciona la fila visualmente
                                Windows.Forms.Application.DoEvents()
                            End If
                        End If
                    End If
                Next
                ' Termina la actualización de la UI
                INDgviewQualityControl.HideLoadingPanel()
                INDgviewQualityControl.OptionsBehavior.EndUpdate()
                ' Actualiza la lista global de ítems seleccionados
                _selectedItems = tempSelectedItems.ToList()
            End If
        Finally
            ' Asegura que el bloqueo se libere si fue adquirido
            If lockAcquired Then
                Monitor.Exit(_lockObject)
            End If
        End Try

    End Sub

    ''' <summary>
    '''  Se usa para actualizar de forma segura y eficiente la lista de ítems seleccionados de la vista de la rejilla, 
    '''  asegurándose de manejar adecuadamente el acceso concurrente a recursos compartidos en un entorno multihilo.
    '''  Utiliza Monitor.TryEnter para intentar adquirir un bloqueo en un objeto, 
    '''  lo que evita que múltiples hilos modifiquen la lista de ítems seleccionados simultáneamente,
    '''  y procesa los ítems seleccionados solo si el bloqueo es adquirido dentro de un tiempo de espera de 2 segundos.
    ''' </summary>
    Private Async Sub UpdateSelectedItems()
        Dim lockAcquired As Boolean = False
        Try
            Monitor.TryEnter(_lockObject, TimeSpan.FromSeconds(2), lockAcquired)
            If Not lockAcquired Then
                Throw New Exception("No se puede obtener la informacion de las filas")
            End If
            If lockAcquired Then
                Dim tempSelectedItems As New HashSet(Of ViewQualityControlXpo)
                Dim selected = INDgviewQualityControl.GetSelectedRows()
                INDgviewQualityControl.OptionsBehavior.BeginUpdate()
                If selected?.Any Then
                    ' Define el tamaño del bloque para procesar grandes volúmenes de filas
                    Dim blockSize As Integer = 1000
                    ' Salida temprana si el número de ítems seleccionados es el esperado
                    If _selectedItems?.Count = IIf(selected.Length = 1, selected.Length, selected.Length - 1) Then
                        Exit Sub
                    End If
                    INDgviewQualityControl.ShowLoadingPanel()
                    ' Calcula el número total de bloques necesarios basado en el tamaño del bloque
                    Dim totalBlocks As Integer = Math.Ceiling(selected.Length / blockSize)
                    For blockIndex As Integer = 0 To totalBlocks - 1
                        Dim startIndex As Integer = blockIndex * blockSize
                        Dim endIndex As Integer = Math.Min(startIndex + blockSize, selected.Length)
                        For i As Integer = startIndex To endIndex - 1
                            Dim handle As Integer = selected(i)
                            If handle >= 0 Then
                                Dim row As Object = INDgviewQualityControl.GetRow(handle)
                                ' Asegura que la fila está visible y carga los datos necesarios
                                If TypeOf row IsNot DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                                    Dim newRow = Await EnsureRowIsVisibleAndLoadData(handle)
                                    If newRow IsNot Nothing Then
                                        tempSelectedItems.Add(newRow.OriginalRow)
                                    End If
                                Else
                                    tempSelectedItems.Add(row.OriginalRow)
                                End If
                            End If
                        Next
                    Next
                    INDgviewQualityControl.HideLoadingPanel()
                Else   ' Maneja la selección individual si no hay selecciones múltiples
                    Dim rowObject As Object = INDgviewQualityControl.GetFocusedRow()
                    If rowObject IsNot Nothing AndAlso TypeOf rowObject Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                        Dim newRow As ViewQualityControlXpo = rowObject.OriginalRow
                        tempSelectedItems.Add(newRow)
                    End If
                End If
                ' Actualiza la lista global de ítems seleccionados
                _selectedItems = tempSelectedItems.ToList()
                INDgviewQualityControl.OptionsBehavior.EndUpdate()
            End If
        Finally
            If lockAcquired Then
                Monitor.Exit(_lockObject)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene la informacion de la fila cargada 
    ''' </summary>
    ''' <param name="handle"></param>
    ''' <returns></returns>
    Private Async Function EnsureRowIsVisibleAndLoadData(handle As Integer) As Task(Of DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        INDgviewQualityControl.MakeRowVisible(handle)
        Dim startTime As DateTime = DateTime.Now
        Dim timeout As TimeSpan = TimeSpan.FromMilliseconds(500) '' esperar 0,5 segundos
        While True
            Dim row As Object = INDgviewQualityControl.GetRow(handle)
            If TypeOf row Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                Return row
            End If
            If DateTime.Now - startTime >= timeout Then
                Exit While
            End If
            Await Task.Delay(50) ' Uso de Task.Delay para evitar bloquear la UI
        End While
        Return Nothing
    End Function

    ''' <summary>
    '''  Valida que seleccion se realizo
    '''  sea  check principal  o el check individual 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgviewQualityControl_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs)
        Dim rowHandle As Integer = e.ControllerRow
        ' Verifica si el cambio de selección se debe al checkbox del encabezado

        If e.Action = CollectionChangeAction.Refresh Then
            If Not SelectedItems?.Exists(Function(x) {EUnitDoseTypeClass.Refilling, EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.ParenteralNutrition}.Contains(x.UnitDoseClass)) Then
                UpdateAndVisualizeSelectionMasive()
            Else
                INDgviewQualityControl.ClearSelection()
            End If
        ElseIf rowHandle >= 0 Then ' Verifica que el índice de fila sea válido
            Dim currentObject = INDgviewQualityControl.GetRow(rowHandle)

            If TypeOf currentObject Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                Dim id As Integer = currentObject.OriginalRow.Id
                If e.Action = CollectionChangeAction.Add AndAlso SelectedItems?.Any Then

                    If SelectedItems?.Exists(Function(x) {EUnitDoseTypeClass.Refilling, EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.ParenteralNutrition}.Contains(x.UnitDoseClass)) Then
                        Dim errors = ValidateItems(currentObject.OriginalRow)
                        If errors.Length > 1 Then
                            Mensaje(EeventViewerImages.Advertencia) = errors
                            INDgviewQualityControl.UnselectRow(rowHandle)
                            Exit Sub
                        End If
                    Else
                        If Not SelectedItems?.Exists(Function(x) x.Id = id) Then
                            SelectedItems.Add(currentObject.OriginalRow)
                        End If
                    End If

                ElseIf e.Action = CollectionChangeAction.Remove AndAlso SelectedItems?.Any Then
                    Dim itemToRemove = SelectedItems?.FirstOrDefault(Function(x) x.Id = id)
                    If itemToRemove IsNot Nothing Then
                        SelectedItems.Remove(itemToRemove)
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se realizan validacion sobre los items que estan siendo seleccionados
    ''' </summary>
    Private Function ValidateItems(Item As ViewQualityControlXpo) As String
        Dim Errors As New StringBuilder

        If Item.UnitDoseClass = EUnitDoseTypeClass.ParenteralNutrition Then
            If _selectedItems?.Any(Function(x) x.Id <> Item.Id) Then
                Errors.AppendLine("La clasificación de defectos de Nutrición parenteral se debe realizar por lote. No se permite seleccionar mas de 1 lote.")
            End If
        Else
            If _selectedItems?.Any(Function(x) x.Id <> Item.Id) Then
                Errors.AppendLine("La clasificación de defectos se debe realizar por principio activo")
            End If

            If _selectedItems?.Exists(Function(x) x.CampaignDetailId <> Item.CampaignDetailId And x.Id <> Item.Id) Then
                Errors.AppendLine("Solo se permite clasificación de defectos de una campaña")
            End If
        End If

        Return Errors.ToString()
    End Function

    Private ReadOnly Property SelectedItemsNoConformingProduct As List(Of ViewNonConformingProductXpo)
        Get
            Dim selected = INDGvProductoNoConforme.GetSelectedRows()
            Dim focused = INDGvProductoNoConforme.GetFocusedObject(Of ViewNonConformingProductXpo)

            If focused IsNot Nothing AndAlso Not selected.Contains(INDGvProductoNoConforme.FocusedRowHandle) Then
                Return {focused}.ToList()
            ElseIf selected IsNot Nothing Then
                Return selected _
                   .Where(Function(m) Not INDGvProductoNoConforme.IsGroupRow(m)) _
                   .Select(Function(m) CType(INDGvProductoNoConforme.GetRow(m), ViewNonConformingProductXpo)) _
                   .ToList()
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private ReadOnly Property SelectedItemsReadJusments As List(Of ViewReadjustmentsXpo)
        Get
            Return INDviewReadJusments.GetSelectedRows() _
                   .Where(Function(x) Not INDviewReadJusments.IsGroupRow(x)) _
                   .Select(Function(x) CType(INDviewReadJusments.GetRow(x), ViewReadjustmentsXpo)) _
                   .ToList()
        End Get
    End Property

    ''' <summary>
    ''' Rejilla de Control Producto Final
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedItemsProducts As List(Of ViewListFinalControlProductXpo)
        Get
            Dim selected = INDGvFinalProductControl.GetSelectedRows()
            Dim focused = INDGvFinalProductControl.GetFocusedObject(Of ViewListFinalControlProductXpo)

            If focused IsNot Nothing AndAlso Not selected.Contains(INDGvFinalProductControl.FocusedRowHandle) Then
                Return {focused}.ToList()
            ElseIf selected IsNot Nothing Then
                Return selected _
                   .Where(Function(m) Not INDGvFinalProductControl.IsGroupRow(m)) _
                   .Select(Function(m) CType(INDGvFinalProductControl.GetRow(m), ViewListFinalControlProductXpo)) _
                   .ToList()
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardQualityControl

    ''' <summary>
    ''' Id de la central de mezcla
    ''' </summary>
    Private CMConfigurationId As Integer

    ''' <summary>
    ''' Id de la linea de producción
    ''' </summary>
    Private ProductionLineId As Integer

    ''' <summary>
    ''' Representa la entidad Orden de Traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private _transferOrder As New TransferOrder

    ''' <summary>
    ''' Representa la entidad del tipo de dosis unitaria
    ''' </summary>
    ''' <remarks></remarks>
    Private _MSClassProductionLine As EUnitDoseTypeClass?

#End Region

#Region "Enumerations"

    ''' <summary>
    ''' Enum campo Status - RequestPackageDetailStatus 
    ''' </summary>
    Enum eStatus
        releasedProduct = 3
        reprocessedProduct = 4
        rejectedProduct = 5
    End Enum

    ''' <summary>
    ''' Enum campo QualityStatus - RequestPackageDetailStatus 
    ''' </summary>
    Enum eQualityStatus
        released = 1
        rejected = 2
        reprocessed = 3
        ReleaseAndRejectProduct = 4
    End Enum

    ''' <summary>
    ''' Enum Source para Saber desde que Dasboard se envia al Abrir Popup de Almacén 
    ''' </summary>
    Enum eSource
        DashboardQualityControl = 1
    End Enum

#End Region

#Region "Properties"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Entidad
    ''' </summary>
    Private _transitWarehouseId As Integer

    ''' <summary>
    ''' Entidad xpo que representa el Almacén el Almacén de Origen
    ''' </summary>
    Private _sourceWarenhouseXpo As ViewListWarehouseTypeXpo

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' Propiedad que contiene los Productos en Estado Picking 
    ''' </summary>
    Public Property ListFinalControlProduct As List(Of ViewListFinalControlProductXpo)
        Get
            Return INDGcFinalProductControl.DataSource
        End Get
        Set(value As List(Of ViewListFinalControlProductXpo))
            INDGcFinalProductControl.DataSource = value
            INDGcFinalProductControl.RefreshDataSource()
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlcgProductionOrder.Name 'Control de Calidad
                INDgviewQualityControl.ClearSelection()
                If _selectedItems?.Any() Then
                    _selectedItems.Clear()
                End If
                LoadQualityControl()
            Case INDlcgFinalProductControl.Name ' Control Final de Productos 
                INDGcFinalProductControl.DataSource = Nothing
                LoadFinalProductControl()
            Case INDLcProductoNoConforme.Name
                LoadNonConformingProduct()
            Case INDlcReadjustments.Name
                LoadReadjustments()
        End Select
    End Sub

    Private Async Sub LoadNonConformingProduct()
        Try
            INDGcNonConformingProduct.DataSource = Nothing
            INDGvProductoNoConforme.ShowLoadingPanel()
            INDGcNonConformingProduct.DataSource = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.GetCollection(Of ViewNonConformingProductXpo)(Function(m) m.ProductionLineId = ProductionLineId))
            INDGvProductoNoConforme.HideLoadingPanel()
        Catch ex As Exception
            INDGvProductoNoConforme.HideLoadingPanel()
            Throw ex
        End Try
    End Sub

    Private Async Function LoadReadjustments() As Task
        Try
            INDgcReadjustment.DataSource = Nothing
            INDviewReadJusments.ShowLoadingPanel()

            INDgcReadjustment.DataSource = Await Task.Factory.StartNew(Function() Presenter.ListViewReadjustments(ProductionLineId))
            INDviewReadJusments.HideLoadingPanel()
        Catch ex As Exception
            INDviewReadJusments.HideLoadingPanel()
            Throw ex
        End Try
    End Function



    ''' <summary>
    ''' Carga las ordenes de producción
    ''' </summary>
    Private Async Function LoadQualityControl() As Task

        If INDgcQualityCont.DataSource IsNot Nothing Then
            INDgcQualityCont.DataSource = Nothing
        End If
        INDgviewQualityControl.ShowLoadingPanel()
        Dim instantFeedbackSource As XPInstantFeedbackSource = Nothing
        ' Verificar si se requiere invocación desde otro hilo
        If Not INDgcQualityCont.InvokeRequired Then
            ' Acceder directamente a XPInstantFeedbackSource si estamos en el hilo principal
            instantFeedbackSource = Await Task.Run(Function() Presenter.ListViewQualityControl(ProductionLineId))
        Else
            ' Si estamos en otro hilo, usar Invoke para acceder a XPInstantFeedbackSource desde el hilo principal
            INDgcQualityCont.Invoke(Sub()
                                        instantFeedbackSource = Presenter.ListViewQualityControl(ProductionLineId)
                                    End Sub)
        End If
        INDgcQualityCont.DataSource = instantFeedbackSource
        INDgviewQualityControl.RefreshData()
        INDgviewQualityControl.HideLoadingPanel()
    End Function


    ''' <summary>
    ''' Carga el control final de productos centros de Atencion Externos
    ''' </summary>
    Private Async Function LoadFinalProductControl() As Task
        If INDGcFinalProductControl.DataSource IsNot Nothing Then
            Exit Function
        End If

        INDGcFinalProductControl.DataSource = Nothing
        INDGvFinalProductControl.ShowLoadingPanel()
        Dim Query = Await Task.Factory.StartNew(Function() Presenter.ListViewFinalControlProduct(CMConfigurationId, ProductionLineId))
        ListFinalControlProduct = Query
        INDGvFinalProductControl.HideLoadingPanel()
    End Function

    ''' <summary>
    '''  Actualiza el estado de calidad del Producto 
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusIds"></param>
    ''' <param name="Status"></param>
    Private Async Sub AcctionsMenuQualityStatus(requestPackageDetailStatusIds As List(Of Integer), Status As Byte, qualityStatus As Byte, Optional info As Object = Nothing)
        Try
            AsyncLoader(True)
            Using model As New MDashboardQualityControl(Me.Tag.ToString())
                Dim data As Object = New ExpandoObject()
                data.info = info
                data.status = Status
                data.qualityStatus = qualityStatus

                Dim Result = Await model.UpdateRequestPackageDetailStatusQS(requestPackageDetailStatusIds, data)
                If Result Is Nothing OrElse Result.StateResult = False Then
                    Mensaje(EeventViewerImages.MensajeError) = String.Format("No se pudo rechazar ningun Producto. info #:{0}", Result.Message)
                    AsyncLoader(False)
                    Exit Sub
                Else
                    Mensaje(EeventViewerImages.Informacion) = "El Proceso se completo correctamente"
                End If
                BeginReloadDatasource()
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    '''  Actualiza el almacén Solicitante 
    ''' </summary>
    ''' <param name="newWarehouseId"></param>
    ''' <param name="requestPackageStatusId"></param>
    Private Async Sub AcctionsMenuAssignWarehouse(newWarehouseId As Integer, requestPackageStatusId As List(Of Integer))
        Try
            AsyncLoader(True)
            Using model As New MDashboardQualityControl(Me.Tag.ToString())
                Dim Data As New Tuple(Of Integer, List(Of Integer))(newWarehouseId, requestPackageStatusId)
                Dim Result = Await model.UpdateAssignWarehouse(Data)
                If Result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Se ha modificado el almacén correctamente"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
                BeginReloadDatasource()
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Retorno del modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnEventArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Método que abre el Popup , y proceso de validacion
    ''' </summary>
    ''' <param name="TypeAction"></param>
    Private Async Sub AcctionsMenuQualityControl(requestPackageStatusIds As List(Of Integer), TypeAction As Integer)
        Me.Cursor = ChangeCursorIndigo()
        Using Model As New MDashboardQualityControl(Me.Tag)
            INDgviewQualityControl.ShowLoadingPanel()
            Dim _result = Await Model.ValidationDefectClassification(requestPackageStatusIds, TypeAction)
            INDgviewQualityControl.HideLoadingPanel()
            If _result Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Hubo problemas en la validación"
                Exit Sub
            End If
            If _result.StateResult = False Then
                If _result.MessageResult?.Count > 0 Then
                    Using formulario As New Controls.FrmListErrors(_result.MessageResult)
                        formulario.Title = _result.Message
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                Exit Sub
            End If
            requestPackageStatusIds = _result.ObjectEmbbeded
        End Using
        Using frm As New PopUpActionQualityControl()
            AddHandler frm.ActionQualityControl, AddressOf ReturnEventArgs
            frm.StartPosition = FormStartPosition.CenterParent
            frm.TypeAction = TypeAction
            Dim trasparent As New FrmTransparent(frm, False)
            Me.Cursor = Cursors.Default
            If trasparent.ShowDialog(Me) = DialogResult.OK Then
                Dim info = New With {.RejectCause = frm.INDGleCauseRejection.EditValue, .Observation = frm.INDMeObservations.Text.Trim()}
                Select Case TypeAction
                    Case 2
                        AcctionsMenuQualityStatus(requestPackageStatusIds, eStatus.rejectedProduct, eQualityStatus.rejected, info)
                    Case 1
                        AcctionsMenuQualityStatus(requestPackageStatusIds, eStatus.reprocessedProduct, eQualityStatus.reprocessed)
                    Case 3
                        AcctionsMenuQualityStatus(requestPackageStatusIds, eStatus.releasedProduct, eQualityStatus.released)
                    Case 4
                        AcctionsMenuQualityStatus(requestPackageStatusIds, eStatus.releasedProduct, eQualityStatus.ReleaseAndRejectProduct)
                End Select
            End If
        End Using
    End Sub

    ''' <summary>
    '''  Consulto el Almacén de Transito 
    ''' </summary>
    Private Async Function SetTransitWarehouse() As Task
        Try
            Using model As New MMixingStationSetting(Tag)
                Dim result = Await model.GetMixingStationSettingByOperativeUnitIdAsync(_idOperativeUnit)
                _transitWarehouseId = result.ObjectEmbbeded.TransitWarehouseId
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    '''  Consulto el Almacén: Tipo Almacén
    ''' </summary>
    Private Sub SetSourceWarehouse()
        _sourceWarenhouseXpo = Presenter.ListViewMixingSationWarehouse(CMConfigurationId, eWarehouseMSType.Terminado)
    End Sub

    ''' <summary>
    ''' Proceso orden de tralasdo
    ''' </summary>
    ''' <param name="targetWarehouseId">Almacén de Destino</param>
    ''' <param name="sourceWarenhouseId">Almacén de Origen</param>
    ''' <param name="_transitWarehouseId">Almacén de Transito</param>
    ''' <param name="campaignNumbers">Numeros de las Campañas</param>
    Private Async Sub SaveItems(targetWarehouseId As Integer, sourceWarenhouseId As Integer, _transitWarehouseId As Integer, campaignNumbers As String, listItems As List(Of ViewListFinalControlProductXpo))
        Try
            AssigningValues(targetWarehouseId, sourceWarenhouseId, _transitWarehouseId, campaignNumbers)
            Dim Data = ProductDetailsList(listItems)
            Dim args = WarehouseValues()

            Using model As New MDashboardQualityControl(Me.Tag)
                Me.AsyncLoader(True)
                Dim resultValidation As ActionResult = Await model.ValidateWarehouses(args)
                If resultValidation.StateResult Then
                    Dim result = Await model.SaveOrderTransferAsync(_transferOrder, Data)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    BeginReloadDatasource()
                Else
                    ShowMessage(EeventViewerImages.Advertencia) = resultValidation.Message
                End If
                Me.AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Asigno el Objeto para la Validacion
    ''' </summary>
    ''' <returns></returns>
    Private Function WarehouseValues() As Object
        Dim args As Object = New ExpandoObject()
        args.SourceWarehouseId = _transferOrder.SourceWarehouseId
        args.SourceTypeName = _sourceWarenhouseXpo.WarehouseTypeName
        args.TargetWarehouseId = _transferOrder.TargetWarehouseId
        args.TransitWarehouseId = _transferOrder.TransitWarehouseId
        args.FormModule = "FrmDashboardQualityControl"
        Return args
    End Function

    ''' <summary>
    ''' Asigan los Valores a la Entidad: TransferOrder
    ''' </summary>
    ''' <param name="targetWarehouseId"></param>
    ''' <param name="sourceWarenhouseId"></param>
    ''' <param name="_transitWarehouseId"></param>
    ''' <param name="campaignNumbers"></param>
    Private Sub AssigningValues(targetWarehouseId As Integer, sourceWarenhouseId As Integer, _transitWarehouseId As Integer, campaignNumbers As String)
        With _transferOrder
            Dim OperatingUnitId As Integer = Me.BarraBotones.OperatingUnitValue
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = OperatingUnitId
            .OrderType = 3 'Traslado en Transito
            .SourceWarehouseId = sourceWarenhouseId
            .DispatchTo = 1 'Almacén
            .TargetWarehouseId = targetWarehouseId
            .Status = 4 'En transito
            .TransitWarehouseId = _transitWarehouseId
            .Description = String.Format("{0}{1} {2}{3}", "Línea de Produccíon #", ProductionLineId, " Campaña #", campaignNumbers)
        End With
    End Sub

    ''' <summary>
    '''  Product Details
    ''' </summary>
    ''' <param name="ListItems"></param>
    ''' <returns></returns>
    Private Function ProductDetailsList(ListItems As List(Of ViewListFinalControlProductXpo)) As List(Of ViewListFinalControlProductModel)
        Dim ListProductDetail = New List(Of ViewListFinalControlProductModel)()

        ListItems.ForEach(Sub(ls As ViewListFinalControlProductXpo)
                              Dim productDetail = New ViewListFinalControlProductModel
                              With productDetail
                                  .Id = ls.Id
                                  .RequestPackageDetailStatusIds = If(String.IsNullOrWhiteSpace(ls.RequestPackageDetailStatusIds),
                                                                        New List(Of Integer),
                                                                        ls.RequestPackageDetailStatusIds.Split(","c).Select(Function(x) Convert.ToInt32(x.Trim())).ToList())
                                  .InventoryProductId = ls.ItemId
                                  .BatchSerialId = ls.BatchSerialId
                                  .PhysicalInventoryId = ls.PhysicalInventoryId
                                  .PhysicalInventoryWarehouseId = ls.PhysicalInventoryWarehouseId
                                  .InventoryQuantity = ls.InventoryQuantity
                                  .MSClassUnitDoseType = _MSClassProductionLine
                                  If {EUnitDoseTypeClass.Refilling, EUnitDoseTypeClass.Repackaging}.Contains(If(_MSClassProductionLine, -1)) Then
                                      .DeliveredQuantity = ls.InventoryQuantity
                                  Else
                                      .DeliveredQuantity = 1
                                  End If
                                  .ProductFullName = ls.ItemCodeName
                                  .CostProduct = ls.ProductCost
                                  .ConsumptionUnit = ls.PackageUnitDescription
                                  .CampaignDetailId = ls.CampaignDetailId
                                  ListProductDetail.Add(productDetail)
                              End With
                          End Sub)
        Return ListProductDetail
    End Function

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        BarraBotones.ActualizarPermisosBarra(Me.Tag)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.RejectProduct)
        If BarraBotones.PermissionsForm.ContainsKey(81) Then 'si tiene permiso 
            ListActions.Add(eAcciones.ReprocessProduct)
        End If
        ListActions.Add(eAcciones.ReleaseProduct)
        ListActions.Add(eAcciones.DefectRegister)
        ListActions.Add(eAcciones.ReleaseAndRejectProduct)

        'Acciones Control final de Productos Centro de Atención
        Dim ListAcionsFinalProduct As New List(Of eAcciones)
        ListAcionsFinalProduct.Add(eAcciones.TransferOrderProductControl)
        ListAcionsFinalProduct.Add(eAcciones.UpdateWarehouse)

        If ListActions.Count > 0 AndAlso ListAcionsFinalProduct.Count > 0 Then 'si hay menu se asigna al gridview 
            IndigoGridView1.SetListAcction(INDgviewQualityControl, ListActions)
            IndigoGridView2.SetListAcction(INDGvFinalProductControl, ListAcionsFinalProduct)

            Dim colQuality = INDgviewQualityControl.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
            If colQuality IsNot Nothing Then colQuality.Width = 100

            Dim colFinal = INDGvFinalProductControl.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
            If colFinal IsNot Nothing Then colFinal.Width = 100
        End If

        IndigoGridView1.MoreInfoColunmns(INDGvFinalProductControl)

        Dim col = INDGvProductoNoConforme.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If col IsNot Nothing Then col.Width = 100

        IndigoGridView3.SetListAcction(INDGvProductoNoConforme, {eAcciones.Reschedule}.ToList())
        IndigoGridView3.MoreInfoColunmns(INDGvProductoNoConforme)

        IndigoGridView4.MoreInfoColunmns(INDviewReadJusments)
        IndigoGridView4.SetListAcction(INDviewReadJusments, {eAcciones.Checkstatus, eAcciones.Remove}.ToList())

        Dim colReadjustment = INDviewReadJusments.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If colReadjustment IsNot Nothing Then colReadjustment.Width = 100
    End Sub
#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmDashboardQualityControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        SetActionsColumns()
        Await InitForm()
    End Sub
    ''' <summary>
    ''' Estados iniciales del formulario
    ''' </summary>
    Private Async Function InitForm() As Task
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PDashboardQualityControl()
        'Consuto Almacén de transito 
        Await SetTransitWarehouse()
        'Consulto Almacén de Origen 
        SetSourceWarehouse()
        INDsleCMProductionLine.Font = New Font("Segoe UI", 28, FontStyle.Regular)
        Initialize()
    End Function

    Private Sub Initialize()
        'Se adiciona el evento de saber que tipo de seleccion se realizo (masivo o individual)
        AddHandler INDgviewQualityControl.SelectionChanged, AddressOf INDgviewQualityControl_SelectionChanged
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardQualityControl_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCMProductionLine.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleCMProductionLine.Size.Width - 11, 0)
        INDsleCMProductionLine.Properties.PopupFormSize = New System.Drawing.Size(INDsleCMProductionLine.Size.Width - 11, 0)

        INDtcgInformation.SelectedTabPageIndex = 0
        INDsleCMProductionLine.Focus()
    End Sub

#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de mas información 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceMasInformacion_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceMasInformacion.QueryPopUp
        Dim selectedItem = INDgviewQualityControl.GetFocusedObject(Of ViewQualityControlXpo)()
        If selectedItem IsNot Nothing Then
            CtrMoreInfoProductControl2.LoadData(selectedItem.RequestMixingStationDetailId, selectedItem.Lot)
        End If
    End Sub

    ''' <summary>
    ''' Evento Control de mas información - Dashbaorad Control Producto Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceMoreInformation_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceMoreInformation.QueryPopUp
        Dim selectedItem = INDGvFinalProductControl.GetFocusedObject(Of ViewListFinalControlProductXpo)()
        If selectedItem IsNot Nothing Then
            INDGcRequestDetails.DataSource = Nothing
            INDGcRequestDetails.DataSource = Presenter.ListViewFinalControlProductById(selectedItem.Id)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara la cambiar el valor del control de Lineas de produccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCMProductionLine_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCMProductionLine.EditValueChanged
        If INDsleCMProductionLine.EditValue IsNot Nothing Then
            Dim viewXpo = INDviewSearchCMProductionLine.GetFocusedObject(Of ViewListCMProductionLineXpo)()
            If viewXpo IsNot Nothing Then
                CMConfigurationId = viewXpo.CMConfigurationId
                ProductionLineId = viewXpo.ProductionLineId
                BeginReloadDatasource()
                SetSourceWarehouse()

                Using model As New MProductionLine(Me.Tag)
                    Dim result = viewXpo.ProductionLineCodeName.Split("-")
                    Dim ProductionLine = Await model.GetProductionLineAsync(result(0).Trim)
                    If ProductionLine.ObjectEmbbeded IsNot Nothing Then
                        If ProductionLine.ObjectEmbbeded.ProductionLineUnitDoseType?.Any(Function(x) {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(x.MsClass)) Then 'Tipo reempaque

                            INDlcReadjustments.HideControl()
                            INDLcProductoNoConforme.HideControl()
                        Else

                            INDlcReadjustments.HideControl(False)
                            INDLcProductoNoConforme.HideControl(False)
                        End If

                        _MSClassProductionLine = ProductionLine.ObjectEmbbeded.ProductionLineUnitDoseType.FirstOrDefault().MsClass
                        HideOrShowColumns(_MSClassProductionLine)
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de mostrar las columnas para cada tipo de dosis unitaria
    ''' </summary>
    Private Sub HideOrShowColumns(_Msclass As Integer)
        Select Case _Msclass
            Case EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling
                GridColumn56.ShowColumn(6)
                GridColumn71.ShowColumn(7)
                GridColumn72.ShowColumn(7)
                INDColStateProd.HideColumn()
                GridColumn44.HideColumn()
                GridColumn43.HideColumn()
                GridColumn16.HideColumn()
            Case Else
                GridColumn56.HideColumn()
                GridColumn71.HideColumn()
                GridColumn72.HideColumn
                INDColStateProd.ShowColumn(1)
                GridColumn44.ShowColumn()
                GridColumn43.ShowColumn()
                GridColumn16.ShowColumn()
        End Select
    End Sub

#End Region

#Region "SelectPageChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la pagina de los tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtcgInformation_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDtcgInformation.SelectedPageChanged
        If INDsleCMProductionLine.EditValue Is Nothing Then
            Exit Sub
        End If

        Select Case e.Page.Name
            Case INDlcgProductionOrder.Name 'Control Calidad
                LoadQualityControl()
            Case INDlcgFinalProductControl.Name ' Control Final de Productos 
                INDGcFinalProductControl.DataSource = Nothing
                LoadFinalProductControl()
            Case INDLcProductoNoConforme.Name
                LoadNonConformingProduct()
            Case INDlcReadjustments.Name
                LoadReadjustments()
        End Select
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Evento que se dispara al presionar refrescar del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en Asignar Almacén Solicitante
    ''' </summary>
    Private Sub UpdateWarehouse()
        Using formulario As New FrmPopupWareHouse
            Me.Cursor = ChangeCursorIndigo()
            formulario.ViewModeEditHold = True
            formulario.INDSource = eSource.DashboardQualityControl
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)

            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)

            Dim newWarehouseId = (From x In formulario.CMWarehouse Select x.IdWarehouse).FirstOrDefault()
            If newWarehouseId.IsNull OrElse newWarehouseId = 0 Then
                Exit Sub
            End If

            Dim requestPackageStatusIds As List(Of Integer) = Nothing
            If {EUnitDoseTypeClass.Refilling, EUnitDoseTypeClass.Repackaging}.Contains(If(_MSClassProductionLine, -1)) Then
                requestPackageStatusIds = SelectedItemsProducts(0)?.RequestPackageDetailStatusIds.Split(","c).Select(Function(x) Convert.ToInt32(x.Trim())).ToList()
            Else
                requestPackageStatusIds = (From x In SelectedItemsProducts Select x.Id).ToList()
            End If

            AcctionsMenuAssignWarehouse(newWarehouseId, requestPackageStatusIds)
        End Using
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en Orden de traslado 
    ''' </summary>
    Private Sub ProcessTransferOrder()
        Dim listItems = (From x In SelectedItemsProducts Select x).ToList()
        Dim errors As New StringBuilder

        If _transitWarehouseId = 0 Then
            errors.AppendLine("* Debe establecer un Almacén de transito en los parámetros de central de Mezclas")
        End If

        If _sourceWarenhouseXpo Is Nothing Then
            errors.AppendLine("* No se encontro el tipo Almacén Terminado en la Central de Mezclas")
        End If

        If CMConfigurationId = 0 Then
            errors.AppendLine("* No se Encontro un tipo la Central de Mezclas")
        End If

        'Se valida que hayan seleccionado productos con bodega de dispensación o almacén.
        If (From x In listItems Where x.DispensingWarehouseId = 0 OrElse x.DispensingWarehouseId Is Nothing).Count > 0 Then
            errors.AppendLine("* Hay productos seleccionados sin bodega de dispensación asignados")
        End If

        'Se valida que hayan no hayan seleccionado diferentes Almacénes 
        Dim warehouseId = listItems(0).DispensingWarehouseId
        If (From x In listItems Where x.DispensingWarehouseId = warehouseId).Count <> listItems.Count Then
            errors.AppendLine("* No se puede seleccionar productos con diferentes almacénes")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        Dim campaignNumbersTmp = (From x In listItems Select x.CampaignNumber Distinct).ToList()
        Dim campaignNumbers = String.Join(" #", campaignNumbersTmp.ToArray())

        'Permite saber si se escogió YES en la pregunta siempre y cuando salga
        If MessageIndigo.Show("Se creará orden de traslado en tránsito con los productos seleccionados, ¿Desea Continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim targetWarehouseId = (From x In listItems Select x.DispensingWarehouseId).FirstOrDefault()
        SaveItems(targetWarehouseId, _sourceWarenhouseXpo.Id_Warehouse, _transitWarehouseId, campaignNumbers, listItems)
    End Sub

    ''' <summary>
    ''' Defect classification popup
    ''' </summary>
    Private Sub OpenDefectClassificationPopup(requestPackageStatusIds As List(Of Integer))
        Dim selected = SelectedItems
        If selected IsNot Nothing AndAlso selected.Any() Then
            Dim uniqueDoseClass = selected?.Select(Function(f) f.UnitDoseClass).Distinct.ToList()
            If uniqueDoseClass IsNot Nothing AndAlso uniqueDoseClass.Count > 1 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se pueden clasificar defectos con distintos tipos de dosis unitaria"
                Exit Sub
            End If

            Using frm As New FrmPopupDefectClassification()
                frm.RequestPackageDetailStatusIds = requestPackageStatusIds
                frm.UnitDoseTypeCodeName = selected(0).UnitDoseTypeCodeName
                frm.UnitDoseClass = selected(0).UnitDoseClass
                frm.BatchCodes = selected.Select(Function(f) f.Lot).ToList()
                frm.FormState = FrmPopupDefectClassification.eFormState.Quality
                frm.Width = Screen.PrimaryScreen.WorkingArea.Width * 0.8
                frm.Height = Screen.PrimaryScreen.WorkingArea.Height * 0.9
                Dim tr As New FrmTransparent(frm, False)
                If tr.ShowDialog(Me) = DialogResult.OK Then
                    BeginReloadDatasource()
                End If
            End Using
        End If
    End Sub

    Private Sub OpenTechnicalConceptReadJustments()
        Dim viewReadJustments = SelectedItemsReadJusments.FirstOrDefault()
        If viewReadJustments Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Item"
            Exit Sub
        End If
        Dim previousReadJustments As Boolean
        Using model As New MReadjustments(Me.Tag.ToString())
            Dim listReadJustments = model.GetReadjustmentsByRequestPackageStatus(viewReadJustments.RequestPackageDetailStatusId)
            previousReadJustments = If(listReadJustments.Count > 1, True, False)
        End Using
        Using frm As New FrmPopupTechnicalConceptReadjusments()
            frm.RequestPackageDetailStatusId = viewReadJustments.RequestPackageDetailStatusId
            frm.DosageName = viewReadJustments.DosageDescription
            frm.unitDoseClass = viewReadJustments.UnitDoseClass
            frm.DateExpired = viewReadJustments.ExpirationDate
            frm.BatchCode = viewReadJustments.BatchCode
            frm.Readjustment = previousReadJustments
            frm.ViewReadjustment = viewReadJustments
            frm.OperatingUnitId = _idOperativeUnit
            Dim form As New FrmTransparent(frm, False)
            form.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "MenuContex"

    ''' <summary>
    ''' Menu contextual para la rejilla de Control de calidad , Menu de acciones para la rejilla de Control de calidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim requestPackageStatusIds As List(Of Integer) = Nothing
        If {EUnitDoseTypeClass.Refilling, EUnitDoseTypeClass.Repackaging}.Contains(SelectedItems(0)?.UnitDoseClass) Then
            requestPackageStatusIds = SelectedItems(0)?.RequestPackageDetailStatusIds.Split(","c).Select(Function(x) Convert.ToInt32(x.Trim())).ToList()
        Else
            requestPackageStatusIds = (From x In SelectedItems Select x.Id).ToList()
        End If

        If Not requestPackageStatusIds.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione al menos un item de la rejilla"
            Return
        End If

        Select Case sender.Tag.ToString
            Case "RejectProduct", "Rechazar Producto"
                AcctionsMenuQualityControl(requestPackageStatusIds, 2)
            Case "ReprocessProduct", "Reprocesar Producto"
                AcctionsMenuQualityControl(requestPackageStatusIds, 1)
            Case "ReleaseProduct", "Liberar Producto"
                AcctionsMenuQualityControl(requestPackageStatusIds, 3)
            Case "DefectRegister"
                OpenDefectClassificationPopup(requestPackageStatusIds)
            Case "ReleaseAndRejectProduct"
                AcctionsMenuQualityControl(requestPackageStatusIds, eQualityStatus.ReleaseAndRejectProduct)
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla control final de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Dim listItems = (From x In SelectedItemsProducts Select x.Id).ToList()

        If Not listItems.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione al menos un item de la rejilla"
            Return
        End If
        Select Case (sender.Tag.ToString)
            Case "TransferOrderProductControl", "Procesar Orden de Traslado"
                ProcessTransferOrder()
            Case "UpdateWarehouse", "Modificar Almacén Solicitante"
                UpdateWarehouse()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual en la rejilla de readecuaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView4_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView4.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Checkstatus", "Verificar estado"
                OpenTechnicalConceptReadJustments()
        End Select
    End Sub

#End Region

#Region "ClickBack"

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDlyRoot.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    Private Sub INDSbRefresh_Click(sender As Object, e As EventArgs) Handles INDSbRefresh.Click
        BeginReloadDatasource()
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        Reschedule()
    End Sub

    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction, IndigoGridView4.ContexMenuActions
        Dim action = sender.Tag.ToString()
        If action = NameOf(eAcciones.Checkstatus) Then
            OpenTechnicalConceptReadJustments()
        End If
    End Sub

    Private Async Sub Reschedule()
        Try
            If MessageIndigo.Show("¿Está seguro que desea reprogramar los ítems seleccionados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            INDGvProductoNoConforme.ShowLoadingPanel()
            Dim items = SelectedItemsNoConformingProduct
            Dim ids = items.Select(Function(m) m.Id).ToList()

            Using model As New MDashboardQualityControl(Me.Tag)
                Dim res = Await model.RescheduleRequestMixingStationAsync(ids)
                INDGvProductoNoConforme.HideLoadingPanel()

                If res.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = $"Item reprogramado correctamente generando las solicitudes: {String.Join(", ", res.MessageResult)}"
                    BeginReloadDatasource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = res.Message
                End If
            End Using
        Catch ex As Exception
            INDGvProductoNoConforme.HideLoadingPanel()
            Throw ex
        End Try
    End Sub

    Private Sub INDviewReadJusments_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDviewReadJusments.PopupMenuShowing
        Dim ReadjusmentButton = IndigoGridView4.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Checkstatus)))
        ReadjusmentButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        If SelectedItemsReadJusments IsNot Nothing AndAlso SelectedItemsReadJusments.Any(Function(x) x.Status = 0) Then
            ReadjusmentButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

    End Sub



#End Region

#Region "PopupMenuShowing"
    ''' <summary>
    ''' evento para mostrar o no los botones de acciones segun la informacion de la vista (menu contextual)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgviewQualityControl_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDgviewQualityControl.PopupMenuShowing

        IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(m) m.Visibility = If(m.Tag <> NameOf(eAcciones.ReprocessProduct), DevExpress.XtraBars.BarItemVisibility.Never, DevExpress.XtraBars.BarItemVisibility.Always))

        Dim ReleaseProduct = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ReleaseProduct)))
        Dim DefectRegister = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.DefectRegister)))
        Dim ReprocessProduct = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ReprocessProduct)))
        Dim RejectProduct = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.RejectProduct)))
        Dim ReleaseAndRejectProduct = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ReleaseAndRejectProduct)))

        DefectRegister.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        ReleaseAndRejectProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        ReleaseProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        RejectProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        If SelectedItems?.Any AndAlso SelectedItems.Any(Function(x) x.FlagDefectClasificationCritical Is Nothing) Then
            If ReprocessProduct IsNot Nothing Then
                ReprocessProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

            If SelectedItems.Exists(Function(x) {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(x.UnitDoseClass)) Then
                ReleaseAndRejectProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        ElseIf SelectedItems.Any(Function(s) s.FlagDefectClasificationCritical IsNot Nothing AndAlso s.FlagDefectClasificationCritical) Then
            RejectProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            ReleaseProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            RejectProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            If SelectedItems.Exists(Function(x) {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(x.UnitDoseClass)) Then
                ReleaseAndRejectProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If
    End Sub

#End Region

#Region "QueryPopUpActionButtons"
    ''' <summary>
    ''' evento para mostrar o no los botones de acciones segun la informacion de la vista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(m) m.Visible = If(m.Tag <> NameOf(eAcciones.ReprocessProduct), False, True))

        Dim ReleaseProduct = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ReleaseProduct)))
        Dim DefectRegister = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.DefectRegister)))
        Dim ReprocessProduct = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ReprocessProduct)))
        Dim RejectProduct = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.RejectProduct)))
        Dim ReleaseAndRejectProduct = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ReleaseAndRejectProduct)))

        Dim count As Integer = 1
        If SelectedItems.Any(Function(x) x.FlagDefectClasificationCritical Is Nothing) Then
            If ReprocessProduct IsNot Nothing Then
                ReprocessProduct.Visible = False
                count -= 1
            End If

            If SelectedItems.Exists(Function(x) {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(x.UnitDoseClass)) Then
                ReleaseAndRejectProduct.Visible = True
                count += 1
            End If
        ElseIf SelectedItems.Any(Function(s) s.FlagDefectClasificationCritical IsNot Nothing And s.FlagDefectClasificationCritical) Then
            RejectProduct.Visible = True
            count += If(ReprocessProduct IsNot Nothing, 1, 0)
        Else

            If SelectedItems.Exists(Function(x) {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(x.UnitDoseClass)) Then
                ReleaseAndRejectProduct.Visible = True
                count += 1
            End If

            ReleaseProduct.Visible = True
            RejectProduct.Visible = True

            count += If(ReprocessProduct IsNot Nothing, 2, 1)
        End If

        DefectRegister.Visible = True
        count += 1
        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * count)
    End Sub

    ''' <summary>
    ''' evento para mostrar o no los botones de acciones segun la informacion de la vista -- Rejilla Readecuaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView4_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView4.QueryPopUpActionButtons
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        Dim ReadjusmentButton = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Checkstatus)))
        Dim RemoveButton = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Remove)))
        Dim count As Integer = 0

        ReadjusmentButton.Visible = False
        RemoveButton.Visible = False

        If SelectedItemsReadJusments.Count > 0 Then
            RemoveButton.Visible = True
            count += 1
            If SelectedItemsReadJusments.Any(Function(x) x.Status = 0) Then
                ReadjusmentButton.Visible = True
                count += 1
            End If
        Else
            Mensaje(EeventViewerImages.Informacion) = $"Seleccione un registro"
            Exit Sub
        End If


        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * count)
    End Sub

    Private Sub INDgviewQualityControl_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDgviewQualityControl.CustomUnboundColumnData
        If TypeOf e.Row Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
            Dim item As ViewQualityControlXpo = TryCast(e.Row.OriginalRow, ViewQualityControlXpo)
            If item IsNot Nothing Then
                If e.IsGetData AndAlso e.Column.Name = INDColStateProd.Name Then
                    Select Case item.FlagQualification
                        Case 3
                            e.Value = GetImage(My.Resources.cancel_16)
                        Case 2
                            e.Value = GetImage(My.Resources.warning_16x16)
                        Case 1
                            e.Value = GetImage(My.Resources.success_16x16)
                    End Select
                End If
            End If
        End If

    End Sub

    Private Function GetImage(img As Image) As Byte()
        Return DevExpress.XtraEditors.Controls.ByteImageConverter.ToByteArray(img, ImageFormat.Gif)
    End Function

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
        If Not INDgviewQualityControl.GridControl.InvokeRequired Then
            Dim info As ToolTipControlInfo = Nothing
            Dim view As GridView = INDgviewQualityControl
            Dim hi As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
            Dim row = view.GetRow(hi.RowHandle)
            Dim text As String = String.Empty

            If hi.Column IsNot Nothing AndAlso row IsNot Nothing AndAlso hi.Column.Name = INDColStateProd.Name Then
                Dim item = TryCast(row, ViewQualityControlXpo)

                If item IsNot Nothing Then
                    If item.FlagDefectClasificationCritical Is Nothing Then
                        text = "Sin clasificación de defectos"
                    ElseIf item.FlagDefectClasificationCritical.Value Then
                        text = "Producto con defectos"
                    Else
                        text = "Producto conforme"
                    End If
                End If
            Else
                Dim cellValue As Object = view.GetRowCellValue(hi.RowHandle, hi.Column)
                If Not TypeOf cellValue Is NotLoadedObject Then
                    text = cellValue?.ToString()
                End If
            End If

            info = New ToolTipControlInfo(row, text)
            If info IsNot Nothing Then e.Info = info
        Else
            INDgviewQualityControl.GridControl.Invoke(Sub() ToolTipController1_GetActiveObjectInfo(sender, e))
        End If
    End Sub

    Private Sub INDsleCMProductionLine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCMProductionLine.QueryPopUp
        If INDsleCMProductionLine.Properties.DataSource Is Nothing Then
            INDsleCMProductionLine.Properties.DataSource = New PDashboardProductionSchedule().InitializeCMProductionLine()
        End If
    End Sub

    Private Sub INDgcQualityCont_Click(sender As Object, e As EventArgs) Handles INDgcQualityCont.Click

    End Sub
#End Region

#End Region

End Class
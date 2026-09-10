'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Giovanny Plazas L
' Created          : 11-05-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Inventory.MVP
Imports Presentation.Controls
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmImportRemissionOut

#Region "EVENTS"
    ''' <summary>
    ''' evento para obtener el listado del detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetDocumentInvoiceProductSalesDetail(sender As Object, e As ImportDocumentInvoiceProductSalesDetailEventArgs)
#End Region

#Region "TUPLE"
    Dim _listEntranceSource As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListEntranceSource As List(Of Tuple(Of Byte, String))
        Get
            If _listEntranceSource Is Nothing Then
                _listEntranceSource = New List(Of Tuple(Of Byte, String))
                _listEntranceSource.Add(New Tuple(Of Byte, String)(1, "Remisiones de Salida"))
            End If
            Return _listEntranceSource
        End Get
    End Property
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' listado de las remisiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRemissionOutputDetailPhysical As New List(Of InventoryRemissionOutputDetailPhysicalReportXpo)
    ''' <summary>
    ''' id del cliente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _thirdPartyId As Integer

    ''' <summary>
    ''' código del usuario 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _codeUser As String

    ''' <summary>
    ''' presenter de ordenes de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PProductInvoice

    Private _warehouseId As Int32

    Private _contractExternalClientId As Integer

    Private _functionalUnitId As Integer

    Private _documentInvoiceProductSalesId As Integer?

    Private _listDocumentInvoiceProductSalesDetail As New List(Of DocumentInvoiceProductSalesDetail)

#End Region

#Region "PROPERTIES"

    Public WriteOnly Property Presenter As PProductInvoice
        Set(value As PProductInvoice)
            _presenter = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ThirdPartyId As Integer
        Set(value As Integer)
            _thirdPartyId = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el código del usuario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property codeUser As String
        Set(value As String)
            _codeUser = value
        End Set
    End Property

    Public WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
        End Set
    End Property

    Public WriteOnly Property ContractExternalClientId As Integer
        Set(value As Integer)
            _contractExternalClientId = value
        End Set
    End Property

    Public WriteOnly Property FunctionalUnitId As Integer
        Set(value As Integer)
            _functionalUnitId = value
        End Set
    End Property

    Public WriteOnly Property ListDocumentInvoiceProductSalesDetail As List(Of DocumentInvoiceProductSalesDetail)
        Set(value As List(Of DocumentInvoiceProductSalesDetail))
            If value IsNot Nothing Then
                _listDocumentInvoiceProductSalesDetail = New List(Of DocumentInvoiceProductSalesDetail)(value.ToArray())
            End If
        End Set
    End Property
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listRemissionOutputDetailPhysical = Nothing
        _thirdPartyId = Nothing
        _codeUser = Nothing
        _contractExternalClientId = Nothing
        _functionalUnitId = Nothing
        _warehouseId = Nothing
    End Sub

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportRemissionOuts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGleEntranceSource.Properties.DataSource = ListEntranceSource
        INDGleEntranceSource.EditValue = 1
        INDGleEntranceSource.Focus()
        IndigoGridControl1.RefreshGrid(INDGcImportInfo)
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' carga el datasource de las remisiones de salida a importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleRemissionSource_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleEntranceSource.EditValueChanged
        If INDGleEntranceSource.EditValue IsNot Nothing Then
            INDGvImportInfo.ShowLoadingPanel()
            Task.Factory.StartNew(Sub()
                                      If INDGleEntranceSource.EditValue = 1 Then
                                          listRemissionOutputDetailPhysical = _presenter.GetRemissionOutput(_warehouseId, _thirdPartyId).ToList()
                                          Me.SafeInvoke(Sub()
                                                            Dim _validate = New List(Of InventoryRemissionOutputDetailPhysicalReportXpo)

                                                            If _listDocumentInvoiceProductSalesDetail.Count > 0 Then
                                                                Dim lockMe As Object = New Object()
                                                                Parallel.ForEach(_listDocumentInvoiceProductSalesDetail, Sub(l)
                                                                                                                             If l.ImportSource Is Nothing OrElse l.ImportSource <> 1 Then
                                                                                                                                 Return
                                                                                                                             End If
                                                                                                                             Dim PhysicalId = (From h In l.DocumentInvoiceProductSalesDetailBatchSerial.ToList()
                                                                                                                                               Select h.PhysicalInventoryId).ToList()
                                                                                                                             _validate = (From k In listRemissionOutputDetailPhysical
                                                                                                                                          Where PhysicalId.Contains(k.PhysicalInventoryId.Id) Select k).ToList()
                                                                                                                             If _validate IsNot Nothing AndAlso _validate.Count > 0 Then

                                                                                                                                 _validate = (From k In _validate
                                                                                                                                              Where (k.RemissionOutputDetailId.RemissionOutputId.Code = l.SourceCode AndAlso l.DocumentInvoiceProductSalesDetailBatchSerial.Select(Function(y) y.RemissionOutputPhysicalId).ToList().Contains(k.Id)
                                                                                                                                                  ) Select k).ToList()
                                                                                                                             End If
                                                                                                                             If _validate.Count > 0 Then
                                                                                                                                 _validate.ForEach(Sub(y)
                                                                                                                                                       SyncLock lockMe
                                                                                                                                                           listRemissionOutputDetailPhysical.Remove(y)
                                                                                                                                                       End SyncLock
                                                                                                                                                   End Sub)
                                                                                                                             End If

                                                                                                                         End Sub)

                                                            End If

                                                            INDGcImportInfo.DataSource = Nothing
                                                            INDGcImportInfo.DataSource = listRemissionOutputDetailPhysical
                                                            INDGcImportInfo.RefreshDataSource()
                                                            INDGvImportInfo.HideLoadingPanel()
                                                        End Sub)
                                      End If
                                  End Sub)

        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Sub FrmImportInfo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' accion para importar los elementos de la rejilla a la factura de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            If listRemissionOutputDetailPhysical Is Nothing OrElse listRemissionOutputDetailPhysical.Count = 0 Then
                Exit Sub
            End If
            Using Model As New MProductInvoice(Tag)
                Me.AsyncLoader(True)
                Dim Ids = (From x In listRemissionOutputDetailPhysical Where x.Activated = True Select x.Id).ToList()
                Dim Result = Await Model.ImportRemissionOutput(Ids, _contractExternalClientId, _functionalUnitId)
                If Result Is Nothing Then
                    Me.Mensaje(EeventViewerImages.MensajeError) = "Se produjo un error en el proceso de Importación"
                    Me.Close()
                End If
                If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(Result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                If Result.StateResult = False Then
                    If Result.Message IsNot Nothing AndAlso Result.Message.Length > 0 Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                    Me.Close()
                    Exit Sub
                End If
                Dim args As New ImportDocumentInvoiceProductSalesDetailEventArgs
                args.ListDocumentInvoiceProductSalesDetail = Result.ObjectEmbbeded()
                If args.ListDocumentInvoiceProductSalesDetail.Count > 0 Then
                    Me.Close()
                    RaiseEvent GetDocumentInvoiceProductSalesDetail(Nothing, args)
                End If
            End Using
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
            Exit Sub
        Finally
            Me.AsyncLoader(False)
        End Try
    End Sub
#End Region

#Region "MouseDoubleClick"
    ''' <summary>
    ''' activa o desactiva todos los items del listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGcImportInfo_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcImportInfo.MouseDoubleClick
        Dim hitPoint = Me.INDGvImportInfo.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then
                If INDGleEntranceSource.EditValue = 1 Then
                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryRemissionOutputDetailPhysicalReportXpo).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryRemissionOutputDetailPhysicalReportXpo).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryRemissionOutputDetailPhysicalReportXpo).ForEach(Sub(x) x.Activated = False)
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryRemissionOutputDetailPhysicalReportXpo).ForEach(Sub(x) x.Activated = True)
                    End If
                    Me.INDGcImportInfo.RefreshDataSource()
                End If

            End If
            Me.INDGcImportInfo.Invalidate()
        End If
    End Sub
#End Region
#End Region

#Region "METHODS"
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' metodo para realizar Carga del cursor mientras se ejecuta alguna accion
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub AsyncLoader(Value As Boolean)
        If Value Then
            Cursor.Current = Cursors.WaitCursor
            Application.UseWaitCursor = Value
            INDGleEntranceSource.Enabled = False
            INDGcImportInfo.Enabled = False
            INDBtnAdd.Enabled = False
        Else
            Cursor.Current = Cursors.Default
            Application.UseWaitCursor = Value
            INDGleEntranceSource.Enabled = True
            INDGcImportInfo.Enabled = True
            INDBtnAdd.Enabled = True
        End If
    End Sub

#End Region
    ''' <summary>
    ''' metodo para seleccionar y deseleccionar Item.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        If INDGleEntranceSource.EditValue = 1 Then
            Dim Item = DirectCast(INDGvImportInfo.GetFocusedRow(), InventoryRemissionOutputDetailPhysicalReportXpo)
            Item.Activated = checkControl.EditValue
            SetImageActivateColumn(EEntranceVoucherSource.RemissionOutput)
        End If
        Me.INDGcImportInfo.RefreshDataSource()
        Me.INDGcImportInfo.Invalidate()
    End Sub

    ''' <summary>
    ''' metodo para establecer la imagen a la columna de activar o inactivar
    ''' </summary>
    ''' <param name="remissionSource"></param>
    ''' <remarks></remarks>
    Private Sub SetImageActivateColumn(remissionSource As EEntranceVoucherSource)
        If remissionSource = EEntranceVoucherSource.RemissionOutput Then
            Dim listActivated = listRemissionOutputDetailPhysical.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listRemissionOutputDetailPhysical.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    Enum EEntranceVoucherSource As Integer
        RemissionOutput = 1
    End Enum
End Class
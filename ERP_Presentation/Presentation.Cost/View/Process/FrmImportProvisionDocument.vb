'***********************************************************************
' Assembly         : Presentacion.Cost
' Author           : Andres Alarcon
' Created          : 21-11-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Presentation.Cost.MVP
Imports Infrastructure.Data.Xpo.CostRepository

#End Region

Public Class FrmImportProvisionDocument

#Region "EVENTS"

    ''' <summary>
    ''' evento para obtener el listado del detalle de la remision de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListProvisionDocument(sender As Object, e As GetListProvisionDocumentEventArgs)

#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Cost"

    ''' <summary>
    ''' Listado de las distribuciones que se van a agregar a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private ListProvisionDocument As New List(Of CostDistributionDirectCost)

    ''' <summary>
    ''' Listado de las distribuciones que ya estan agregadas en el form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private _ListProvisionDocumentValidation As New List(Of CostDistributionDirectCostLegalizedDocuments)

    ''' <summary>
    ''' Id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _SupplierId As Integer

    ''' <summary>
    ''' Id del elemento del costo
    ''' </summary>
    ''' <remarks></remarks>
    Private _GeneralExpenseId As Integer

#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Propiedad para asignar el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SupplierId As Integer
        Set(value As Integer)
            _SupplierId = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el id del elemento del costo
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property GeneralExpenseId As Integer
        Set(value As Integer)
            _GeneralExpenseId = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListProvisionDocumentValidation As List(Of CostDistributionDirectCostLegalizedDocuments)
        Set(value As List(Of CostDistributionDirectCostLegalizedDocuments))
            If value IsNot Nothing Then
                _ListProvisionDocumentValidation = New List(Of CostDistributionDirectCostLegalizedDocuments)(value.ToArray())
            End If
        End Set
    End Property

#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _SupplierId = Nothing
        _ListProvisionDocumentValidation = Nothing
        ListProvisionDocument = Nothing
    End Sub

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportProvisionDocuments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetProvisionDocument()
        IndigoGridControl1.RefreshGrid(INDGcImportInfo)
    End Sub

    Private Sub GetProvisionDocument()
        Using model As New MCostDistributionDirectCost(Me.Tag)
            Dim ListProvisionDocumentXpo = model.GetListViewProvisionDocument(_SupplierId, _GeneralExpenseId)
            If ListProvisionDocumentXpo IsNot Nothing Then
                LoadListPurcharseOrder(ListProvisionDocumentXpo)
                INDGcImportInfo.DataSource = Nothing
                INDGcImportInfo.DataSource = ListProvisionDocument
                INDGcImportInfo.RefreshDataSource()
            End If
        End Using
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
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim errors = ValidateRecords()
        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        Dim args As New GetListProvisionDocumentEventArgs
        args.ListProvisionDocument = GenerateLegalizedDocumentsDetail()

        If args.ListProvisionDocument.Count > 0 Then
            Me.Close()
            RaiseEvent GetListProvisionDocument(Nothing, args)
        End If
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

                If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseOrderDetail).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseOrderDetail).Count Then
                    INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseOrderDetail).ForEach(Sub(x) x.Activated = False)
                Else
                    INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseOrderDetail).ForEach(Sub(x) x.Activated = True)
                End If

                Me.INDGcImportInfo.RefreshDataSource()
            End If

            Me.INDGcImportInfo.Invalidate()
        End If
    End Sub
#End Region
#End Region

#Region "METHODS"

    ''' <summary>
    ''' metodo para generar los detalles de los documentos de provision a legalizar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateLegalizedDocumentsDetail() As List(Of CostDistributionDirectCostLegalizedDocuments)
        Dim ListCostDistributionDirectCostLegalizedDocumentsTmp As New List(Of CostDistributionDirectCostLegalizedDocuments)

        For Each item In ListProvisionDocument.FindAll(Function(x) x.Activated And Not x.ItemInvalid)
            Dim ProvisionDocumenttmp = New CostDistributionDirectCostLegalizedDocuments
            With ProvisionDocumenttmp

                .ProvisionDocumentId = item.Id
                .Code = item.Code
                .ConfirmDate = item.ConfirmDate
                .StatusName = item.StatusName
                .Value = item.Value

            End With
            ListCostDistributionDirectCostLegalizedDocumentsTmp.Add(ProvisionDocumenttmp)
        Next

        Return ListCostDistributionDirectCostLegalizedDocumentsTmp
    End Function

    ''' <summary>
    ''' valida los datos para poder importarlos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRecords() As List(Of String)
        Dim errors As New List(Of String)
        Dim listLegalizedDocumentsActivated = ListProvisionDocument.FindAll(Function(x) x.Activated = True)
        Dim ProductForValidation As InventoryProduct = Nothing

        If Not listLegalizedDocumentsActivated?.Any() Then
            errors.Add(ResourceManager.GetString("NoSelectedItem", MODULE_NAME))
        End If

        If Not errors?.Any() Then

            'valido que el producto no este con la misma remision de inventario en consignación
            'Dim _listEntranceVoucherDetailValidationTmp = listLegalizedDocumentsActivated.Where(Function(x) x.ConsignmentInventoryRemissionDetailBatchSerialId IsNot Nothing).ToList()
            'For Each item In _listEntranceVoucherDetailValidationTmp
            '    Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidationTmp.Find(Function(x) x.ConsignmentInventoryRemissionDetailBatchSerialId = item.Id)
            '    If entranceVoucherDetailTmp IsNot Nothing Then
            '        errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), entranceVoucherDetailTmp.ProductCodeName, ResourceManager.GetString("ConsignmentInventoryRemission", MODULE_NAME)))
            '        item.ItemInvalid = True
            '    End If
            'Next
        End If
        Return errors
    End Function

    ''' <summary>
    ''' Metodo que carga el listado de distribuciones de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListPurcharseOrder(listXpo As XPCollection)
        ListProvisionDocument = New List(Of CostDistributionDirectCost)
        For Each itemXpo As ViewListProvisionDocumentXpo In listXpo
            Dim provisionDocument As New CostDistributionDirectCost
            With provisionDocument

                .Id = itemXpo.Id
                .StatusName = itemXpo.StatusName
                .Code = itemXpo.Code
                .ConfirmDate = itemXpo.ConfirmDate
                .Value = itemXpo.Value

            End With

            ListProvisionDocument.Add(provisionDocument)
        Next
    End Sub

#End Region

    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim purcharse = DirectCast(INDGvImportInfo.GetFocusedRow(), CostDistributionDirectCost)
        purcharse.Activated = checkControl.EditValue
        SetImageActivateColumn()
        Me.INDGcImportInfo.RefreshDataSource()
        Me.INDGcImportInfo.Invalidate()
    End Sub

    ''' <summary>
    ''' metodo para establecer la imagen a la columna de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetImageActivateColumn()
        Dim listActivated = ListProvisionDocument.FindAll(Function(x) x.Activated)
        If listActivated.Count = ListProvisionDocument.Count Then
            Me.INDGclState.Image = Global.Presentation.Cost.My.Resources.Resources.check
        Else
            Me.INDGclState.Image = Global.Presentation.Cost.My.Resources.Resources.undcheck
        End If
    End Sub

End Class
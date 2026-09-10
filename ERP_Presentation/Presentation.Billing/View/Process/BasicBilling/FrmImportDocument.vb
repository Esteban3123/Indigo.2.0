'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 25-06-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Text
Imports Presentation.Base


#End Region

Public Class FrmImportDocument

#Region "EVENTS"
    ''' <summary>
    ''' evento para obtener el listado de los registro a importar 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListBasicBillingDetails(sender As Object, e As AddDocumentsEventArgs)
#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"

    ''' <summary>
    ''' Variable que almacena el datasource del PopUp de "importar" en facturacion basica
    ''' </summary>
    Private VListBasicBillingForImportXpo As Task(Of List(Of ViewListBasicBillingForImportXpo))

    ''' <summary>
    ''' listado de los detalles de facturacion basica
    ''' </summary>
    ''' <remarks></remarks>
    Private ListBasicBillingDetails As New List(Of BasicBillingDetail)

    ''' <summary>
    ''' listado de los obsequios de facturacion basica
    ''' </summary>
    ''' <remarks></remarks>
    Private ListBasicBillingGifts As New List(Of BasicBillingGifts)

    ''' <summary>
    ''' Entidad de facturacion basica
    ''' </summary>
    ''' <remarks></remarks>
    Private NewBasicBilling As New BasicBilling

    ''' <summary>
    ''' Dictionario para almacenar los registros de facturacion basica en el proceso de agregado
    ''' </summary>
    ''' <remarks></remarks>
    Private DictionaryBasicBilling As New Dictionary(Of Integer, BasicBilling)

    ''' <summary>
    ''' Modalidad de venta
    ''' </summary>
    ''' <remarks></remarks>
    Private _saleModality As Byte?

    ''' <summary>
    ''' Id del cliente
    ''' </summary>
    Private _customerId As Integer?

    ''' <summary>
    ''' Id de la moneda desde el formulario que llama a modal de importacion
    ''' </summary>
    Private _currencyId As Integer?
#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Propiedad para asignar el Id de la linea de distribucion del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SaleModality As Byte?
        Set(value As Byte?)
            _saleModality = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el Id del cliente
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CustomerId As Integer?
        Set(value As Integer?)
            _customerId = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica de la moneda
    ''' </summary>
    Public WriteOnly Property CurrencyId As Integer?
        Set(value As Integer?)
            _currencyId = value
        End Set
    End Property

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
#End Region

#Region "HANDLES"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListBasicBillingDetails = Nothing
        ListBasicBillingGifts = Nothing
        _saleModality = Nothing
        _customerId = Nothing
        _currencyId = Nothing
    End Sub

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportDocuments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridControl1.RefreshGrid(INDGcImportInfo)
        LoadDatasourceBasicBilling(_saleModality, _currencyId, _customerId)
    End Sub

    ''' <summary>s
    ''' Carga el Datasource con los registros
    ''' </summary>
    Private Sub LoadDatasourceBasicBilling(Optional SaleModality As Byte? = Nothing, Optional CurrencyId As Integer? = Nothing, Optional CustomerId As Integer? = Nothing)
        INDGvImportInfo.ShowLoadingPanel()
        VListBasicBillingForImportXpo = Task.Factory.StartNew(Function() As List(Of ViewListBasicBillingForImportXpo)
                                                                  Using model As New MBasicBilling(Me.Tag)
                                                                      Dim res = model.ListAllListBasicBillingForImport()
                                                                      If res IsNot Nothing AndAlso res.Any Then
                                                                          Return res
                                                                      Else
                                                                          Return Nothing
                                                                      End If
                                                                  End Using
                                                              End Function)
        Task.Factory.StartNew(Sub()
                                  Using model As New MBasicBilling(Me.Tag)
                                      Dim result = model.ListViewListBasicBillingForImport(False, SaleModality, CurrencyId, CustomerId)

                                      Me.SafeInvoke(Sub()
                                                        INDGcImportInfo.DataSource = result
                                                        INDGvImportInfo.HideLoadingPanel()
                                                    End Sub)

                                  End Using
                              End Sub)
    End Sub

#End Region

#Region "KeyDown"
    Private Sub FrmImportInfo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Show"

    Private Sub INDGvImportInfo_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles INDGvImportInfo.MasterRowGetRelationName
        e.RelationName = "DetailsImport"
    End Sub

    ''' <summary>
    ''' Funcion para cargar las sub tablas de los items
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDGvImportInfo_MasterRowGetChildListMixingStation(ByVal sender As Object, ByVal e As MasterRowGetChildListEventArgs) Handles INDGvImportInfo.MasterRowGetChildList
        INDGvImportInfo.ShowLoadingPanel()
        Dim data = (Await VListBasicBillingForImportXpo)
        If e.ChildList Is Nothing Then
            Dim obj As ViewListBasicBillingForImportXpo = INDGvImportInfo.GetFocusedRow()
            e.ChildList = data.FindAll(Function(m) m.BasicBillingId = obj.HeaderId)
            INDGvImportInfo.HideLoadingPanel()
        End If
    End Sub

    Private Sub INDGvImportInfo_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles INDGvImportInfo.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click

        If Not ValidateRecords() Then
            Exit Sub
        End If

        Await GenerateBasicBillingDetail()

        Dim args As New AddDocumentsEventArgs With {
            .ListBasicbillingDetail = ListBasicBillingDetails,
            .ListGiftsDetail = ListBasicBillingGifts,
            .ItemBasicBilling = DictionaryBasicBilling.FirstOrDefault.Value
        }

        If args.ListBasicbillingDetail.Any() Then
            Me.Close()
            RaiseEvent GetListBasicBillingDetails(Nothing, args)
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

                If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListBasicBillingForImportXpo).Where(Function(s) s.Selected).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListBasicBillingForImportXpo).Count Then
                    INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListBasicBillingForImportXpo).ForEach(Sub(x) x.Selected = False)
                Else
                    INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListBasicBillingForImportXpo).ForEach(Sub(x) x.Selected = True)
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
    ''' valida los datos para poder importarlos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRecords() As Boolean
        Dim errors As New StringBuilder

        Dim ListHeaders = VListBasicBillingForImportXpo.Result.FindAll(Function(x) x.Selected And x.HeaderId IsNot Nothing) 'Cabecera
        Dim ListAllDetails = VListBasicBillingForImportXpo.Result.FindAll(Function(x) x.Selected And x.HeaderId Is Nothing) 'Detalles

        If Not ListHeaders.Any() Then
            errors.AppendLine("No se ha seleccionado ningun documento para agregar")
        End If

        If Not ListAllDetails.Any() Then
            errors.AppendLine("No se ha seleccionado ningun detalle para agregar")
        End If

        If ListHeaders.Exists(Function(d)
                                  Return ListHeaders.Any(Function(x) x.CurrencyId <> d.CurrencyId OrElse
                                                                     x.SaleModality <> d.SaleModality OrElse
                                                                     x.CustomerId <> d.CustomerId)
                              End Function) Then 'Exists
            errors.AppendLine("No se pueden importar los detalles, los datos a agregar tienen diferente información relacionada")
        End If

        Dim Details = ListAllDetails.Where(Function(m) m.DetailType > 0).ToList() 'Detalles
        If Details.Exists(Function(d)
                              Return Details.Any(Function(x) x.DetailType = d.DetailType AndAlso
                                                             x.ItemCodeName = d.ItemCodeName AndAlso
                                                             x IsNot d)
                          End Function) Then
            errors.AppendLine("No se pueden importar los detalles, existen items duplicados")
        End If

        Dim Gifts = ListAllDetails.Where(Function(x) x.DetailType = Nothing And x.HeaderId Is Nothing).ToList() 'Obsequios
        If Gifts.Exists(Function(d)
                            Return Gifts.Any(Function(x) x.ItemCodeName = d.ItemCodeName AndAlso
                                                         x IsNot d)
                        End Function) Then
            errors.AppendLine("No se pueden importar los obsequios, existen obsequios duplicados")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Metodo para generar los detalles del registro de facturacion basica
    ''' </summary>
    Private Async Function GenerateBasicBillingDetail() As Task
        Try
            INDBtnAdd.Enabled = False
            INDGvImportInfo.ShowLoadingPanel()
            'Detalles seleccionados
            Dim ListDetails = VListBasicBillingForImportXpo.Result.FindAll(Function(x) x.Selected And x.HeaderId Is Nothing)

            For Each item In ListDetails
                Using model As New MBasicBilling(Me.Tag)

                    If Not DictionaryBasicBilling.ContainsKey(item.BasicBillingId) Then
                        Dim Result = Await model.GetBasicBillingByCode(item.DocumentCode)

                        If Result IsNot Nothing Then
                            DictionaryBasicBilling.Add(item.BasicBillingId, Result)
                        End If

                    End If

                    Dim basicBilling = DictionaryBasicBilling.FirstOrDefault(Function(x) x.Value.Id = item.BasicBillingId).Value
                    If basicBilling IsNot Nothing Then

                        If {1, 2, 3}.Contains(item.DetailType) Then
                            Dim detail = basicBilling.BasicBillingDetail.FirstOrDefault(Function(x) x.Id = item.DetailId)

                            Dim NewDetail As New BasicBillingDetail With {
                                .DetailType = detail.DetailType,
                                .ProductId = detail.ProductId,
                                .CodeName = detail.CodeName,
                                .BillingConceptId = detail.BillingConceptId,
                                .PhysicalAssetId = detail.PhysicalAssetId,
                                .PhysicalAssetPartId = detail.PhysicalAssetPartId,
                                .Quantity = detail.Quantity,
                                .Price = detail.Price,
                                .Value = detail.Value,
                                .PercentageDiscount = detail.PercentageDiscount,
                                .ValueDiscount = detail.ValueDiscount,
                                .PercentageIVA = detail.PercentageIVA,
                                .RetentionIdTax = detail.RetentionIdTax,
                                .RetentionBaseTax = detail.RetentionBaseTax,
                                .RetentionPercentageTax = detail.RetentionPercentageTax,
                                .WithholdingTax = detail.WithholdingTax,
                                .RetentionIdICA = detail.RetentionIdICA,
                                .RetentionBaseICA = detail.RetentionBaseICA,
                                .RetentionPercentageICA = detail.RetentionPercentageICA,
                                .WithholdingICA = detail.WithholdingICA,
                                .WarehouseId = detail.WarehouseId,
                                .WarehouseName = detail.WarehouseName,
                                .ServicesProvidedId = detail.ServicesProvidedId,
                                .ServicesProvidedName = detail.ServicesProvidedName,
                                .SupplierId = detail.SupplierId,
                                .SupplierName = detail.SupplierName,
                                .SalesExecutiveId = detail.SalesExecutiveId,
                                .SalesExecutiveName = detail.SalesExecutiveName,
                                .FeeId = detail.FeeId,
                                .FeeName = detail.FeeName,
                                .FunctionalUnitId = detail.FunctionalUnitId,
                                .FunctionalUnitIdName = detail.FunctionalUnitIdName,
                                .CostCenterId = detail.CostCenterId}

                            If detail.BasicBillingDetailItem.Any() Then
                                Dim ListItemDetail As New List(Of BasicBillingDetailItem)
                                For Each ItemDetail In detail.BasicBillingDetailItem
                                    Dim DetailItem As New BasicBillingDetailItem With {
                                        .PhysicalInventoryId = ItemDetail.PhysicalInventoryId,
                                        .Quantity = ItemDetail.Quantity,
                                        .BatchCode = ItemDetail.BatchCode}
                                    ListItemDetail.Add(DetailItem)
                                Next

                                ListItemDetail.ForEach(Sub(x)
                                                           NewDetail.BasicBillingDetailItem.Add(x)
                                                       End Sub)
                            End If

                            ListBasicBillingDetails.Add(NewDetail)
                        Else
                            Dim Gift = basicBilling.BasicBillingGifts.FirstOrDefault(Function(x) x.Id = item.DetailId)

                            Dim NewGift As New BasicBillingGifts With {
                                .ProductId = Gift.ProductId,
                                .ProductName = Gift.ProductName,
                                .Quantity = Gift.Quantity,
                                .WarehouseId = Gift.WarehouseId,
                                .CodeNameWarehouse = Gift.CodeNameWarehouse}

                            If Gift.BasicBillingGiftsItem.Any() Then
                                Dim GiftItemDetail As New List(Of BasicBillingGiftsItem)
                                For Each GiftDetail In Gift.BasicBillingGiftsItem
                                    Dim DetailGift As New BasicBillingGiftsItem With {
                                        .PhysicalInventoryId = GiftDetail.PhysicalInventoryId,
                                        .Quantity = GiftDetail.Quantity}
                                    GiftItemDetail.Add(DetailGift)
                                Next

                                GiftItemDetail.ForEach(Sub(g)
                                                           NewGift.BasicBillingGiftsItem.Add(g)
                                                       End Sub)
                            End If

                            ListBasicBillingGifts.Add(NewGift)
                        End If
                    End If
                End Using
            Next
            INDGvImportInfo.HideLoadingPanel()
        Catch ex As Exception
            INDBtnAdd.Enabled = True
            Throw
        End Try
    End Function

#End Region
#Region "EditValueChanged"

    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Me.INDGcImportInfo.BeginUpdate()
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim BasicBilling = CType(INDGvImportInfo.GetFocusedRow(), ViewListBasicBillingForImportXpo)

        If BasicBilling IsNot Nothing Then
            BasicBilling.Selected = checkControl.EditValue
            VListBasicBillingForImportXpo.Result.FirstOrDefault(Function(x) x.HeaderId = BasicBilling.HeaderId).Selected = checkControl.EditValue

            For Each item In VListBasicBillingForImportXpo.Result.FindAll(Function(m) m.BasicBillingId = BasicBilling.HeaderId)
                item.Selected = checkControl.EditValue
            Next

            Me.INDGcImportInfo.EndUpdate()
        End If
    End Sub

    Private Sub INDRIselectorDetail_EditValueChanged(sender As Object, e As EventArgs) Handles INDRIselectorDetail.EditValueChanged
        Me.INDGvImportInfoDetails.BeginUpdate()
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim BasicBillingDetail = CType(INDGvImportInfo.GetDetailView(INDGvImportInfo.FocusedRowHandle, 0), GridView).GetFocusedRow()

        If BasicBillingDetail IsNot Nothing Then
            BasicBillingDetail.Selected = checkControl.EditValue

            Dim BasicBilling = CType(INDGvImportInfo.GetFocusedRow(), ViewListBasicBillingForImportXpo)
            If BasicBilling IsNot Nothing Then
                INDGcImportInfo.BeginUpdate()
                BasicBilling.Selected = True
                VListBasicBillingForImportXpo.Result.FirstOrDefault(Function(x) x.HeaderId = BasicBilling.HeaderId).Selected = True
                INDGcImportInfo.EndUpdate()
            End If
        End If
        Me.INDGvImportInfoDetails.EndUpdate()
    End Sub

#End Region
End Class
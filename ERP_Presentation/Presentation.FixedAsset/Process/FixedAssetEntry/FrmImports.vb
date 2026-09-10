'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/05/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Maintenance
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Payments.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Inventory
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class FrmImports

#Region "Builder"

    ''' <summary>
    ''' Inicializa una instancia de la clase
    ''' </summary>
    ''' <param name="_currency"></param>
    Public Sub New(Optional _currency As Currency = Nothing)
        InitializeComponent()
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
    End Sub

#End Region

#Region "Properties"

    Private _supplierDistributionLineId As Integer
    Public Property SupplierDistributionLineId As Integer
        Get
            Return _supplierDistributionLineId
        End Get
        Set(value As Integer)
            _supplierDistributionLineId = value
        End Set
    End Property

    Private _getLocationResponsible As Integer
    Public Property GetLocationResponsible As Integer
        Get
            Return _getLocationResponsible
        End Get
        Set(value As Integer)
            _getLocationResponsible = value
        End Set
    End Property

    Private _entryDate As DateTime
    Public Property EntryDate As DateTime
        Get
            Return _entryDate
        End Get
        Set(value As DateTime)
            _entryDate = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    Private _listCompare As List(Of FixedAssetEntryItem)
    Public Property ListCompare As List(Of FixedAssetEntryItem)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetEntryItem))
            _listCompare = value
        End Set
    End Property

    Dim _declarant As Boolean
    Public WriteOnly Property Declarant As Boolean
        Set(value As Boolean)
            _declarant = value
        End Set
    End Property

    Public AdquisitionType As Integer

#End Region

#Region "Event"

    Public Event AddImportsInfoEventArgs(sender As Object, e As AddImportsInfo)

#End Region

#Region "Globals"

    Dim Presenter As PImports

    Dim ListFixedAssetRemissionEntranceItemXpo As List(Of FixedAssetRemissionEntranceItemXpo)

    Dim ListFixedAssetPurchaseOrderItemXpo As List(Of FixedAssetPurchaseOrderItemXpo)

    Dim ListFixedAssetEntryItem As List(Of FixedAssetEntryItem)

    Dim ListSource As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' variable de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

#End Region

#Region "Methods"

    Private Sub ImportInfoPurchaseOrder()
        Try
            AsyncLoader(True)
            'Se valida que al menos haya un item seleccionado en la rejilla
            If ListFixedAssetPurchaseOrderItemXpo Is Nothing OrElse ListFixedAssetPurchaseOrderItemXpo.Count = 0 OrElse ListFixedAssetPurchaseOrderItemXpo.Where(Function(item) item.SelectOption = True).Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item para poder importar."
                AsyncLoader(False)
                Exit Sub
            End If
            'Se valida que la orden que se selecciono no este en la rejilla principal
            If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then
                Dim ListErrors As New StringBuilder
                ListFixedAssetPurchaseOrderItemXpo.ForEach(Sub(item)
                                                               If item.SelectOption Then
                                                                   If ListCompare.Where(Function(itemCompare) itemCompare.ItemId = item.ItemId.Id).Count > 0 Then
                                                                       ListErrors.AppendLine("El articulo " + item.ItemId.CodeDescription + " ya existe en la lista.")
                                                                   End If
                                                               End If
                                                           End Sub)
                If ListErrors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
                    AsyncLoader(False)
                    Exit Sub
                End If
            End If
            'Se crean los objetos correspondenties para pegar en la rejilla
            CreateEntitiesWithPurchaseOrder()

            'Se realiza el llamado para ingresar toda la info necesaria
            Using formulario As New FrmFixedAssetEntryItem(_currency:=Me._headCurrency)
                Me.Cursor = ChangeCursorIndigo()
                AddHandler formulario.AddFixedAssetEntryItemEventArgs, AddressOf ReturnAddEventArgs
                formulario.ListFixedAssetEntryItem = ListFixedAssetEntryItem
                formulario.GetLocationResponsible = GetLocationResponsible
                formulario.EntryDate = EntryDate
                formulario.ImportData = True
                formulario.AdquisitionType = AdquisitionType
                formulario.Declarant = _declarant
                formulario.Size = New Drawing.Size(1090, 750)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Private Sub CreateEntitiesWithPurchaseOrder()
        'Se instancia el listado que se envia al form principal
        ListFixedAssetEntryItem = New List(Of FixedAssetEntryItem)
        'Listado de errores
        Dim ListErorrs As New StringBuilder

        For Each purchaseOrderItem In (From l In ListFixedAssetPurchaseOrderItemXpo Where l.SelectOption = True).ToList 'Se recorre los items que se seleccionaron
            'Se valida que el item no se haya agregado con anterioridad al listado que se envia al form principal
            If ListFixedAssetEntryItem.Where(Function(item) item.ItemId = purchaseOrderItem.ItemId.Id).Count > 0 Then
                ListErorrs.AppendLine("El articulo " + purchaseOrderItem.ItemId.CodeDescription + " de la orden " + purchaseOrderItem.PurchaseOrderId.Code + " ya ha sido agregado.")
                Continue For
            End If
            'Se valida si el articulo deprecia tenga los detalles contables
            If purchaseOrderItem.ItemId.AllowDepreciate Then 'Si permite depreciar
                If purchaseOrderItem.ItemId.FixedAssetItemDetailXpo Is Nothing OrElse purchaseOrderItem.ItemId.FixedAssetItemDetailXpo.Count = 0 Then
                    ListErorrs.AppendLine("El articulo " + purchaseOrderItem.ItemId.CodeDescription + " permite depreciar pero no tiene parametrizado los detalles contables.")
                    Continue For
                End If
            End If

            Dim fixedAssetEntryItem As New FixedAssetEntryItem
            With fixedAssetEntryItem
                .RemissionSource = 2 'Orden de compra
                .SourceCode = purchaseOrderItem.PurchaseOrderId.Code
                .PurchaseOrderItemId = purchaseOrderItem.Id
                .ItemId = purchaseOrderItem.ItemId.Id
                .ItemCodeName = purchaseOrderItem.ItemId.CodeDescription
                .IVAId = purchaseOrderItem.IVAId.Id
                .IVACodeName = purchaseOrderItem.IVAId.Code + " - " + purchaseOrderItem.IVAId.Name
                .TrademarkId = purchaseOrderItem.TrademarkId.Id
                .TrademarkCodeName = purchaseOrderItem.TrademarkId.CodeDescription
                .Model = purchaseOrderItem.Model
                '.PolicyId = purchaseOrderItem.PolicyId.Id
                '.PolicyCodeName = remissionEntranceItem.PolicyId.CodeName
                .Quantity = purchaseOrderItem.OutstandingQuantity
                .OutstandingQuantity = purchaseOrderItem.OutstandingQuantity
                .UnitValue = purchaseOrderItem.UnitValue
                .SubTotalValue = purchaseOrderItem.SubTotalValue
                .IvaPercentage = purchaseOrderItem.IvaPercentage
                .IvaValue = purchaseOrderItem.IvaValue
                .DiscountPercentage = purchaseOrderItem.DiscountPercentage
                .DiscountValue = purchaseOrderItem.DiscountValue
                .TotalValue = purchaseOrderItem.TotalValue
                .RTFPercentage = 0
                .RTFValue = 0
            End With
            ListFixedAssetEntryItem.Add(fixedAssetEntryItem)
        Next

        If ListErorrs.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ListErorrs.ToString
        End If
    End Sub

    Private Sub ReturnAddEventArgs(sender As Object, e As AddFixedAssetEntryItem)
        ListFixedAssetEntryItem = e.ListFixedAssetEntryItem
        'Se crea el objeto que se va a devolver
        Dim args As New AddImportsInfo
        args.ListFixedAssetEntryItem = ListFixedAssetEntryItem
        RaiseEvent AddImportsInfoEventArgs(Nothing, args)
        Me.Close()
    End Sub

    Private Sub ImportInfoRemissionEntrance()
        Try
            AsyncLoader(True)
            'Se valida que al menos haya un item seleccionado en la rejilla
            If ListFixedAssetRemissionEntranceItemXpo Is Nothing OrElse ListFixedAssetRemissionEntranceItemXpo.Count = 0 OrElse ListFixedAssetRemissionEntranceItemXpo.Where(Function(item) item.SelectOption = True).Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item para poder importar."
                AsyncLoader(False)
                Exit Sub
            End If
            'Se valida que la remisión que se selecciono no este en la rejilla principal
            If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then
                Dim ListErrors As New StringBuilder
                ListFixedAssetRemissionEntranceItemXpo.ForEach(Sub(item)
                                                                   If item.SelectOption Then
                                                                       If ListCompare.Where(Function(itemCompare) itemCompare.ItemId = item.ItemId.Id).Count > 0 Then
                                                                           ListErrors.AppendLine("El articulo " + item.ItemId.CodeDescription + " ya existe en la lista.")
                                                                       End If
                                                                   End If
                                                               End Sub)
                If ListErrors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
                    AsyncLoader(False)
                    Exit Sub
                End If
            End If
            'Se crean los objetos correspondenties para pegar en la rejilla
            CreateEntitiesWithRemissionEntrance()
            AsyncLoader(False)
            'Se crea el objeto que se va a devolver
            Dim args As New AddImportsInfo
            args.ListFixedAssetEntryItem = ListFixedAssetEntryItem
            RaiseEvent AddImportsInfoEventArgs(Nothing, args)
            Me.Close()
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Private Sub CreateEntitiesWithRemissionEntrance()
        'Se instancia el listado que se envia al form principal
        ListFixedAssetEntryItem = New List(Of FixedAssetEntryItem)
        'Listado de errores
        Dim ListErorrs As New StringBuilder

        For Each remissionEntranceItem In (From l In ListFixedAssetRemissionEntranceItemXpo Where l.SelectOption = True).ToList 'Se recorre los item que se seleccionaron

            'Se valida que el articulo no se haya agregado con anterioridad al listado que se le envia al form principal
            If ListFixedAssetEntryItem.Where(Function(item) item.ItemId = remissionEntranceItem.ItemId.Id).Count > 0 Then
                ListErorrs.AppendLine("El articulo " + remissionEntranceItem.ItemId.CodeDescription + " de la remisión " + remissionEntranceItem.RemissionEntranceId.Code + " ya ha sido agregado.")
                Continue For
            End If
            'Se valida si el articulo deprecia tenga los detalles contables
            If remissionEntranceItem.ItemId.AllowDepreciate Then 'Si permite depreciar
                If remissionEntranceItem.ItemId.FixedAssetItemDetailXpo Is Nothing OrElse remissionEntranceItem.ItemId.FixedAssetItemDetailXpo.Count = 0 Then
                    ListErorrs.AppendLine("El articulo " + remissionEntranceItem.ItemId.CodeDescription + " permite depreciar pero no tiene parametrizado los detalles contables.")
                    Continue For
                End If
            End If

            Dim fixedAssetEntryItem As New FixedAssetEntryItem
            With fixedAssetEntryItem

                .RemissionSource = 3 'Remision de entrada
                .SourceCode = remissionEntranceItem.RemissionEntranceId.Code
                .RemissionEntranceItemId = remissionEntranceItem.Id
                .ItemId = remissionEntranceItem.ItemId.Id
                .ItemCodeName = remissionEntranceItem.ItemId.CodeDescription
                .IVAId = remissionEntranceItem.IVAId.Id
                .IVACodeName = remissionEntranceItem.IVAId.Code + " - " + remissionEntranceItem.IVAId.Name
                .TrademarkId = remissionEntranceItem.TrademarkId.Id
                .TrademarkCodeName = remissionEntranceItem.TrademarkId.CodeDescription
                .Model = remissionEntranceItem.Model
                .PolicyId = remissionEntranceItem.PolicyId.Id
                .PolicyCodeName = remissionEntranceItem.PolicyId.CodeName
                .Quantity = remissionEntranceItem.OutstandingQuantity
                .OutstandingQuantity = remissionEntranceItem.OutstandingQuantity
                .UnitValue = remissionEntranceItem.UnitValue
                .SubTotalValue = remissionEntranceItem.SubTotalValue
                .IvaPercentage = remissionEntranceItem.IvaPercentage
                .IvaValue = remissionEntranceItem.IvaValue
                .DiscountPercentage = remissionEntranceItem.DiscountPercentage
                .DiscountValue = remissionEntranceItem.DiscountValue
                .TotalValue = remissionEntranceItem.TotalValue
                .RTFPercentage = 0
                .RTFValue = 0

                If remissionEntranceItem.FixedAssetRemissionEntranceItemDetailXpo IsNot Nothing AndAlso remissionEntranceItem.FixedAssetRemissionEntranceItemDetailXpo.Count > 0 Then
                    For Each remissionEntranceItemDetail In remissionEntranceItem.FixedAssetRemissionEntranceItemDetailXpo

                        Dim fixedAssetEntryItemDetail As New FixedAssetEntryItemDetail

                        fixedAssetEntryItemDetail.Plate = remissionEntranceItemDetail.Plate
                        fixedAssetEntryItemDetail.Serie = remissionEntranceItemDetail.Serie
                        fixedAssetEntryItemDetail.ReponsibleId = remissionEntranceItemDetail.ResponsibleId.Id
                        fixedAssetEntryItemDetail.ResponsibleCodeName = remissionEntranceItemDetail.ResponsibleId.CodeNitName
                        fixedAssetEntryItemDetail.LocationId = remissionEntranceItemDetail.LocationId.Id
                        fixedAssetEntryItemDetail.LocationCodeName = remissionEntranceItemDetail.LocationId.CodeName
                        fixedAssetEntryItemDetail.AdquisitionDate = remissionEntranceItemDetail.AdquisitionDate
                        fixedAssetEntryItemDetail.Depreciate = remissionEntranceItemDetail.Depreciate
                        fixedAssetEntryItemDetail.HandlesWarranty = remissionEntranceItemDetail.HandlesWarranty
                        fixedAssetEntryItemDetail.WarrantyExpirationDate = remissionEntranceItemDetail.WarrantyExpirationDate
                        fixedAssetEntryItemDetail.StatusAssetId = remissionEntranceItemDetail.StatusAssetId.Id
                        fixedAssetEntryItemDetail.StatusAssetCodeName = remissionEntranceItemDetail.StatusAssetId.CodeName

                        If remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBookXpo IsNot Nothing AndAlso remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBookXpo.Count > 0 Then
                            For Each remissionEntranceItemDetailBook In remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBookXpo
                                Dim fixedAssetEntryItemDetailBook As New FixedAssetEntryItemDetailBook
                                fixedAssetEntryItemDetailBook.LegalBookId = remissionEntranceItemDetailBook.LegalBookId.Id
                                fixedAssetEntryItemDetailBook.LegalBookCodeName = remissionEntranceItemDetailBook.LegalBookId.CodeName
                                fixedAssetEntryItemDetailBook.LifeTime = remissionEntranceItemDetailBook.LifeTime
                                fixedAssetEntryItemDetailBook.UnitLifeTime = remissionEntranceItemDetailBook.UnitLifeTime
                                fixedAssetEntryItemDetailBook.DepreciationType = remissionEntranceItemDetailBook.DepreciationType
                                fixedAssetEntryItemDetailBook.TotalProductionUnit = remissionEntranceItemDetailBook.TotalProductionUnit
                                fixedAssetEntryItemDetailBook.PercentageRescue = remissionEntranceItemDetailBook.PercentageRescue

                                fixedAssetEntryItemDetail.FixedAssetEntryItemDetailBook.Add(fixedAssetEntryItemDetailBook)
                            Next
                        End If

                        If remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPartXpo IsNot Nothing AndAlso remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPartXpo.Count > 0 Then
                            For Each remissionEntranceItemDetailPart In remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPartXpo
                                Dim fixedAssetEntryItemDetailPart As New FixedAssetEntryItemDetailPart
                                fixedAssetEntryItemDetailPart.PartAccesoriesConsumiblesId = remissionEntranceItemDetailPart.PartAccesoriesConsumiblesId.Id
                                fixedAssetEntryItemDetailPart.PartAccesoriesConsumablesCodeName = remissionEntranceItemDetailPart.PartAccesoriesConsumiblesId.CodeName
                                fixedAssetEntryItemDetailPart.DepreciatePart = remissionEntranceItemDetailPart.DepreciatePart
                                fixedAssetEntryItemDetailPart.Value = remissionEntranceItemDetailPart.Value

                                If remissionEntranceItemDetailPart.FixedAssetRemissionEntranceItemDetailPartBookXpo IsNot Nothing AndAlso remissionEntranceItemDetailPart.FixedAssetRemissionEntranceItemDetailPartBookXpo.Count > 0 Then
                                    For Each remissionEntranceItemDetailPartBook In remissionEntranceItemDetailPart.FixedAssetRemissionEntranceItemDetailPartBookXpo
                                        Dim fixedAssetEntryItemDetailPartBook As New FixedAssetEntryItemDetailPartBook
                                        fixedAssetEntryItemDetailPartBook.LegalBookId = remissionEntranceItemDetailPartBook.LegalBookId.Id
                                        fixedAssetEntryItemDetailPartBook.LegalBookCodeName = remissionEntranceItemDetailPartBook.LegalBookId.CodeName
                                        fixedAssetEntryItemDetailPartBook.LifeTime = remissionEntranceItemDetailPartBook.LifeTime
                                        fixedAssetEntryItemDetailPartBook.UnitLifeTime = remissionEntranceItemDetailPartBook.UnitLifeTime
                                        fixedAssetEntryItemDetailPartBook.DepreciationType = remissionEntranceItemDetailPartBook.DepreciationType
                                        fixedAssetEntryItemDetailPartBook.TotalProductionUnit = remissionEntranceItemDetailPartBook.TotalProductionUnit
                                        fixedAssetEntryItemDetailPartBook.PercentageRescue = remissionEntranceItemDetailPartBook.PercentageRescue

                                        fixedAssetEntryItemDetailPart.FixedAssetEntryItemDetailPartBook.Add(fixedAssetEntryItemDetailPartBook)
                                    Next
                                End If

                                fixedAssetEntryItemDetail.FixedAssetEntryItemDetailPart.Add(fixedAssetEntryItemDetailPart)
                            Next
                        End If

                        .FixedAssetEntryItemDetail.Add(fixedAssetEntryItemDetail)
                    Next
                End If

            End With

            ListFixedAssetEntryItem.Add(fixedAssetEntryItem)

        Next

        If ListErorrs.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ListErorrs.ToString
        End If
    End Sub

    Private Sub InitializeTuple()
        ListSource = New List(Of Tuple(Of Integer, String))
        ListSource.Add(New Tuple(Of Integer, String)(1, "Orden de Compra"))
        ListSource.Add(New Tuple(Of Integer, String)(2, " Remisión de Entrada"))
        INDsleSource.Properties.DataSource = ListSource.ToList
    End Sub

    Private Sub ValidateSource()
        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
        INDgcImports.DataSource = Nothing
        If INDsleSource.EditValue = 1 Then 'Orden de Compra
            INDcolCode.FieldName = "PurchaseOrderId.Code"
            ListFixedAssetPurchaseOrderItemXpo = Presenter.ListFixedAssetPurchaseOrderBySupplierDistributionLineId(SupplierDistributionLineId, Me._headCurrency.Id)
            If ListFixedAssetPurchaseOrderItemXpo IsNot Nothing AndAlso ListFixedAssetPurchaseOrderItemXpo.Count > 0 Then
                INDgcImports.DataSource = ListFixedAssetPurchaseOrderItemXpo
            End If
        Else 'Remisión de entrada
            If Me._headCurrency.Id = Me._indigo.OfficialCurrencyId Then
                INDcolCode.FieldName = "RemissionEntranceId.Code"
                ListFixedAssetRemissionEntranceItemXpo = Presenter.ListFixedAssetRemissionEntranceBySupplierDistributionLineId(SupplierDistributionLineId)
                If ListFixedAssetRemissionEntranceItemXpo IsNot Nothing AndAlso ListFixedAssetRemissionEntranceItemXpo.Count > 0 Then
                    INDgcImports.DataSource = ListFixedAssetRemissionEntranceItemXpo
                End If
            End If
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListFixedAssetRemissionEntranceItemXpo = Nothing
        ListFixedAssetPurchaseOrderItemXpo = Nothing
        ListFixedAssetEntryItem = Nothing
        ListSource = Nothing
    End Sub

    Private Sub FrmImports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuple()
        Presenter = New PImports()
        IndigoGridControl1.RefreshGrid(INDgcImports)
        INDgcImports.DataSource = Nothing
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmImports_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepChechSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepChechSelectOption.EditValueChanging
        If e IsNot Nothing Then
            If INDsleSource.EditValue = 1 Then 'Orden de compra
                Dim itemXpo As FixedAssetPurchaseOrderItemXpo = INDviewImports.GetFocusedRow()
                If itemXpo IsNot Nothing Then
                    itemXpo.SelectOption = e.NewValue
                    INDgcImports.RefreshDataSource()
                    If ListFixedAssetPurchaseOrderItemXpo.Where(Function(item) item.SelectOption = True).Count = ListFixedAssetPurchaseOrderItemXpo.Count Then
                        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                    Else
                        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                    End If
                End If
            Else 'Remisión de entrada
                Dim itemXpo As FixedAssetRemissionEntranceItemXpo = INDviewImports.GetFocusedRow()
                If itemXpo IsNot Nothing Then
                    itemXpo.SelectOption = e.NewValue
                    INDgcImports.RefreshDataSource()
                    If ListFixedAssetRemissionEntranceItemXpo.Where(Function(item) item.SelectOption = True).Count = ListFixedAssetRemissionEntranceItemXpo.Count Then
                        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                    Else
                        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleSource_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSource.EditValueChanged
        If INDsleSource.EditValue IsNot Nothing Then
            ValidateSource()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnImports_Click(sender As Object, e As EventArgs) Handles INDbtnImports.Click
        If INDsleSource.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un origen."
            Exit Sub
        End If
        If INDsleSource.EditValue = 1 Then 'Orden de compra
            ImportInfoPurchaseOrder()
        Else 'Remisión de entrada
            ImportInfoRemissionEntrance()
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"

    Private Sub INDgcImports_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcImports.MouseDoubleClick
        If INDsleSource.EditValue IsNot Nothing Then
            If INDsleSource.EditValue = 1 Then 'Orden de compra
                If ListFixedAssetPurchaseOrderItemXpo IsNot Nothing AndAlso ListFixedAssetPurchaseOrderItemXpo.Count > 0 Then
                    Dim hitPoint = Me.INDviewImports.CalcHitInfo(e.Location)
                    If hitPoint.Column IsNot Nothing Then
                        If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelect") Then

                            Dim listFilterXpCollection = INDviewImports.DataController.GetAllFilteredAndSortedRows()
                            Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                            If cont = listFilterXpCollection.Count Then
                                For Each item In listFilterXpCollection
                                    item.SelectOption = False
                                Next
                                Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                            Else
                                For Each item In listFilterXpCollection
                                    item.SelectOption = True
                                Next
                                Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                            End If
                            Me.INDgcImports.RefreshDataSource()
                            Me.INDgcImports.Invalidate()
                        End If
                    End If
                End If
            Else 'Remisión de entrada
                If ListFixedAssetRemissionEntranceItemXpo IsNot Nothing AndAlso ListFixedAssetRemissionEntranceItemXpo.Count > 0 Then
                    Dim hitPoint = Me.INDviewImports.CalcHitInfo(e.Location)
                    If hitPoint.Column IsNot Nothing Then
                        If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelect") Then

                            Dim listFilterXpCollection = INDviewImports.DataController.GetAllFilteredAndSortedRows()
                            Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                            If cont = listFilterXpCollection.Count Then
                                For Each item In listFilterXpCollection
                                    item.SelectOption = False
                                Next
                                Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                            Else
                                For Each item In listFilterXpCollection
                                    item.SelectOption = True
                                Next
                                Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                            End If
                            Me.INDgcImports.RefreshDataSource()
                            Me.INDgcImports.Invalidate()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmImports_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleSource.Focus()
    End Sub

#End Region

#End Region

End Class
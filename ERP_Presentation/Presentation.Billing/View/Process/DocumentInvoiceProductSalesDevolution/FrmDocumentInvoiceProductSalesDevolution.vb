'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/01/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Common.MVP
Imports Presentation.Inventory.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository
#End Region

Public Class FrmDocumentInvoiceProductSalesDevolution
    Implements IDocumentInvoiceProductSalesDevolution

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrRefoundPurchase()
        ctrTmp.SetInfoFunction(AddressOf getInfo)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDocumentInvoiceProductSalesDevolution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Habilita o inhabilita los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDocumentInvoiceProductSalesDevolution.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDsleWarehouse.Enabled = value
            INDmemoDetail.Enabled = value
            INDsleDocumentInvoiceProductSales.Enabled = value
            INDdteBillDate.Enabled = value
            INDtxtThirdParty.Enabled = value
            INDgcProducts.Enabled = value

            INDtxtSubTotal.Enabled = value
            INDtxtDiscountValue.Enabled = value
            INDtxtValueTax.Enabled = value
            INDtxtWhithholdigTaxValue.Enabled = value
            INDtxtWhithholdigICAValue.Enabled = value
            INDtxtWhithholdigSourceValue.Enabled = value
            INDtxtOtherRetention.Enabled = value
            INDtxtOtherDeduction.Enabled = value
            INDtxtTotalValue.Enabled = value
            INDlyRoot.EndUpdate()

            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IDocumentInvoiceProductSalesDevolution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la secuencia del formulario
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As BillingSequence Implements IDocumentInvoiceProductSalesDevolution.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As BillingSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As BillingSequenceDetail In Me._sequense.BillingSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IDocumentInvoiceProductSalesDevolution.Code
        Get
            If INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As Date? Implements IDocumentInvoiceProductSalesDevolution.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del almacen
    ''' </summary>
    ''' <returns></returns>
    Public Property WarehouseId As Integer Implements IDocumentInvoiceProductSalesDevolution.WarehouseId
        Get
            Return INDsleWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDsleWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del almacen
    ''' </summary>
    ''' <returns></returns>
    Public Property WarehouseXpo As XPInstantFeedbackSource Implements IDocumentInvoiceProductSalesDevolution.WarehouseXpo
        Get
            Return INDsleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleWarehouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Detalle
    ''' </summary>
    ''' <returns></returns>
    Public Property Detail As String Implements IDocumentInvoiceProductSalesDevolution.Detail
        Get
            Return INDmemoDetail.EditValue
        End Get
        Set(value As String)
            INDmemoDetail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la factura de productos
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentInvoiceProductSalesId As Integer Implements IDocumentInvoiceProductSalesDevolution.DocumentInvoiceProductSalesId
        Get
            Return INDsleDocumentInvoiceProductSales.EditValue
        End Get
        Set(value As Integer)
            INDsleDocumentInvoiceProductSales.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la factura de productos
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentInvoiceProductSalesDatasourceXpo As XPInstantFeedbackSource Implements IDocumentInvoiceProductSalesDevolution.DocumentInvoiceProductSalesDatasourceXpo
        Get
            Return INDsleDocumentInvoiceProductSales.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDocumentInvoiceProductSales.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de la factura
    ''' </summary>
    ''' <returns></returns>
    Public Property BillDate As Date? Implements IDocumentInvoiceProductSalesDevolution.BillDate
        Get
            Return INDdteBillDate.EditValue
        End Get
        Set(value As Date?)
            INDdteBillDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción del cliente de la factura
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyDescription As String Implements IDocumentInvoiceProductSalesDevolution.ThirdPartyDescription
        Get
            Return INDtxtThirdParty.EditValue
        End Get
        Set(value As String)
            INDtxtThirdParty.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Obtiene o establece el tipo de devolucion
    ''' </summary>
    Private DevolutionType As Boolean?

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As BillingSequence

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDocumentInvoiceProductSalesDevolution

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Entidad
    ''' </summary>
    Private DocumentInvoiceProductSalesDevolution As DocumentInvoiceProductSalesDevolution

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordBilling

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"

    ''' <summary>
    ''' Listado de detalles de la devolución
    ''' </summary>
    Private ListDocumentInvoiceProductSalesDevolutionDetail As List(Of DocumentInvoiceProductSalesDevolutionDetail)

    ''' <summary>
    ''' Listado de detalles eliminados de la devolución
    ''' </summary>
    Private ListDeleteDocumentInvoiceProductSalesDevolutionDetail As List(Of DocumentInvoiceProductSalesDevolutionDetail)

    ''' <summary>
    ''' Variable que representa el control agregado a la barra de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ctrTmp As CtrRefoundPurchase

    ''' <summary>
    ''' Porcentaje iva que viene desde parámetros de inventario
    ''' </summary>
    Dim WithholdingIvaPercentage As Decimal

    ''' <summary>
    ''' Porcentaje de flete que viene desde parámetros de inventario
    ''' </summary>
    Dim FreightIVAPercentage As Decimal

    ''' <summary>
    ''' Porcentaje ica que esta asociado al tercero
    ''' </summary>
    Dim IcaPercentage As Decimal = 0

    ''' <summary>
    ''' Tipo de contribución que esta asociado al tercero
    ''' </summary>
    Dim ContributionType As Byte

    ''' <summary>
    ''' Tarifa de retención asociado al tercero
    ''' </summary>
    Dim ThirdPartyIvaRetentionConceptRate As Decimal

    ''' <summary>
    ''' Tercero que se obtiene con el nit de la  compañía
    ''' </summary>
    Dim _currentCompany As ThirdParty

    ''' <summary>
    ''' Entidad xpo que representa a la factura de productos
    ''' </summary>
    Dim documentInvoiceProductSalesXpo As InventoryDocumentInvoiceProductSalesXpo

    ''' <summary>
    ''' Permite saber si se esta cargando desde el loadControls
    ''' </summary>
    Dim IsLoadControls As Boolean = False

#End Region

#Region "ICrudBase"

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If

        If DocumentInvoiceProductSalesDevolution.Status <> 3 Then
            If ListDocumentInvoiceProductSalesDevolutionDetail Is Nothing OrElse ListDocumentInvoiceProductSalesDevolutionDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay detalles para devolver"
                Exit Sub
            End If

            If (From x In ListDocumentInvoiceProductSalesDevolutionDetail Where x.QuantityDevolution = 0 Select x).Count = ListDocumentInvoiceProductSalesDevolutionDetail.Count Then
                Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado cantidades a devolver"
                Exit Sub
            End If
        End If

        Try
            AssigningValues()
            Using model As New MDocumentInvoiceProductSalesDevolution(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveDocumentInvoiceProductSalesDevolution(DocumentInvoiceProductSalesDevolution, _idCurrentSequense)
                If Result.StateResult = True Then
                    If DocumentInvoiceProductSalesDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me.Sequense.IsManual AndAlso Not Me.Sequense.Sequential Then
                            Me.DicSequense(Me.Sequense.BillingSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me.Sequense.Sequential Then
                            If DocumentInvoiceProductSalesDevolution.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf DocumentInvoiceProductSalesDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If DocumentInvoiceProductSalesDevolution.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf DocumentInvoiceProductSalesDevolution.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = Result.Message
                        ElseIf DocumentInvoiceProductSalesDevolution.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.DocumentInvoiceProductSalesDevolution = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequense Is Nothing Then
            Exit Sub
        End If
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewDocumentInvoiceProductSalesDevolution()
        End If
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Almacén", .FieldName = "WarehouseId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDocumentInvoiceProductSalesDevolution
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Actualiza los valores
    ''' </summary>
    Private Sub RefreshTotals()
        If DocumentInvoiceProductSalesDevolution IsNot Nothing Then
            DocumentInvoiceProductSalesDevolution.WithholdingIvaPercentage = WithholdingIvaPercentage

            DevolutionType = True

            For Each item In ListDocumentInvoiceProductSalesDevolutionDetail
                item.SubTotalValue = item.QuantityDevolution * item.SalePrice
                item.DiscountValue = item.SubTotalValue * item.DiscountPercentage / 100
                item.IvaValue = (item.SubTotalValue - item.DiscountValue) * (item.IvaPercentage / 100)
                item.RTFValue = (item.SubTotalValue - item.DiscountValue) * (item.RTFPercentage / 100)
                item.TotalValue = item.SubTotalValue + item.IvaValue - item.DiscountValue

                If IcaPercentage > 0 Then
                    item.WithholdingICA = CDec(Utils.RoundValue((item.SubTotalValue - item.DiscountValue) * (IcaPercentage / 100)))
                Else
                    item.WithholdingICA = 0
                End If

                If _currentCompany IsNot Nothing AndAlso _currentCompany.Id > 0 AndAlso (_currentCompany.ContributionType = 0 OrElse _currentCompany.ContributionType >= ContributionType) Then
                    item.WithholdingTax = 0
                Else
                    If ThirdPartyIvaRetentionConceptRate > 0 Then
                        item.WithholdingTax = item.IvaValue * (ThirdPartyIvaRetentionConceptRate / 100)
                    Else
                        item.WithholdingTax = 0
                    End If
                End If

                If item.Quantity <> item.QuantityDevolution Then
                    DevolutionType = False
                End If
            Next

            If ListDocumentInvoiceProductSalesDevolutionDetail IsNot Nothing AndAlso ListDocumentInvoiceProductSalesDevolutionDetail.Count > 0 Then
                DocumentInvoiceProductSalesDevolution.Value = ListDocumentInvoiceProductSalesDevolutionDetail.Sum(Function(x) x.SubTotalValue)
                DocumentInvoiceProductSalesDevolution.ValueTax = ListDocumentInvoiceProductSalesDevolutionDetail.Sum(Function(x) x.IvaValue)
                DocumentInvoiceProductSalesDevolution.ValueDiscount = ListDocumentInvoiceProductSalesDevolutionDetail.Sum(Function(x) x.DiscountValue)
                DocumentInvoiceProductSalesDevolution.RetentionSource = CDec(Utils.RoundValue(ListDocumentInvoiceProductSalesDevolutionDetail.Sum(Function(x) x.RTFValue)))
                DocumentInvoiceProductSalesDevolution.WithholdingTax = ListDocumentInvoiceProductSalesDevolutionDetail.Sum(Function(o) o.WithholdingTax)
                DocumentInvoiceProductSalesDevolution.WithholdingICA = ListDocumentInvoiceProductSalesDevolutionDetail.Sum(Function(o) o.WithholdingICA)
            Else
                DocumentInvoiceProductSalesDevolution.Value = 0
                DocumentInvoiceProductSalesDevolution.ValueTax = 0
                DocumentInvoiceProductSalesDevolution.ValueDiscount = 0
                DocumentInvoiceProductSalesDevolution.RetentionSource = 0
            End If

            DocumentInvoiceProductSalesDevolution.TotalValue = DocumentInvoiceProductSalesDevolution.Value + DocumentInvoiceProductSalesDevolution.ValueTax -
            DocumentInvoiceProductSalesDevolution.ValueDiscount - DocumentInvoiceProductSalesDevolution.WithholdingTax - DocumentInvoiceProductSalesDevolution.WithholdingICA -
            DocumentInvoiceProductSalesDevolution.RetentionSource - DocumentInvoiceProductSalesDevolution.DistrictTax +
            DocumentInvoiceProductSalesDevolution.FreightIVAValue + DocumentInvoiceProductSalesDevolution.FreightValue

            With DocumentInvoiceProductSalesDevolution
                INDtxtSubTotal.EditValue = .Value
                INDtxtDiscountValue.EditValue = .ValueDiscount
                INDtxtValueTax.EditValue = .ValueTax
                INDtxtWhithholdigTaxValue.EditValue = .WithholdingTax
                INDtxtWhithholdigICAValue.EditValue = .WithholdingICA
                INDtxtWhithholdigSourceValue.EditValue = .RetentionSource
                INDtxtOtherRetention.EditValue = 0
                INDtxtOtherDeduction.EditValue = 0
                INDtxtTotalValue.EditValue = .TotalValue
            End With
            ctrTmp.PrintInfo()
        End If
    End Sub

    ''' <summary>
    ''' Carga los parámetros de inventario
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadParameters() As Task
        Dim _tempOpretingUnitId As Integer
        If BarraBotones.OperatingUnit Is Nothing Then
            _tempOpretingUnitId = indigo.IndigoOperatingUnitId
        Else
            _tempOpretingUnitId = BarraBotones.OperatingUnit.Id
        End If
        Using Model As New MEntranceVoucher(Me.MyTag)
            Dim parameter = Await Model.GetSettingInventory(_tempOpretingUnitId)
            If parameter Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", "Inventory")
                Deshacer()
                Exit Function
            End If
            FreightIVAPercentage = parameter.IvaFreigthPercentage
            WithholdingIvaPercentage = parameter.WithholdingIvaPercentage

            Me._currentCompany = Await Model.GetThirdPartyAsync(Me.indigo.IndigoCompanyNit)
        End Using
    End Function

    ''' <summary>
    ''' Método que limpia los controles de 
    ''' </summary>
    Private Sub CleanControlsInvoice()
        DevolutionType = Nothing

        DocumentInvoiceProductSalesId = Nothing
        INDsleDocumentInvoiceProductSales.Properties.NullText = String.Empty
        DocumentInvoiceProductSalesDatasourceXpo = Nothing
        BillDate = Nothing
        ThirdPartyDescription = Nothing

        INDtxtSubTotal.EditValue = Nothing
        INDtxtDiscountValue.EditValue = Nothing
        INDtxtValueTax.EditValue = Nothing
        INDtxtWhithholdigTaxValue.EditValue = Nothing
        INDtxtWhithholdigICAValue.EditValue = Nothing
        INDtxtWhithholdigSourceValue.EditValue = Nothing
        INDtxtOtherRetention.EditValue = Nothing
        INDtxtOtherDeduction.EditValue = Nothing
        INDtxtTotalValue.EditValue = Nothing

        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total en el control de la parte superior
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of Decimal, Boolean?)
        Return New Tuple(Of Decimal, Boolean?)(INDtxtTotalValue.EditValue, DevolutionType)
    End Function

    ''' <summary>
    ''' Método que elimina los detalles de la rejilla cuando se cambia el valor del control de la factura o
    ''' cuando se cambia el valor del control del almacen
    ''' </summary>
    Private Sub DeleteDetails()
        If ListDocumentInvoiceProductSalesDevolutionDetail IsNot Nothing AndAlso ListDocumentInvoiceProductSalesDevolutionDetail.Count > 0 Then
            While ListDocumentInvoiceProductSalesDevolutionDetail.Count > 0 AndAlso (From x In ListDocumentInvoiceProductSalesDevolutionDetail Where x.Id > 0).Count > 0
                Dim entity = ListDocumentInvoiceProductSalesDevolutionDetail(0)
                ListDocumentInvoiceProductSalesDevolutionDetail.Remove(entity)
                entity.MarkAsDeleted()

                If ListDeleteDocumentInvoiceProductSalesDevolutionDetail Is Nothing Then
                    ListDeleteDocumentInvoiceProductSalesDevolutionDetail = New List(Of DocumentInvoiceProductSalesDevolutionDetail)
                End If
                ListDeleteDocumentInvoiceProductSalesDevolutionDetail.Add(entity)
            End While
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la factura de productos
    ''' </summary>
    Private Sub GetDocumentInvoiceProductSales()
        documentInvoiceProductSalesXpo = Presenter.GetDocumentInvoiceProductSales(DocumentInvoiceProductSalesId)
        INDdteBillDate.EditValue = documentInvoiceProductSalesXpo.InvoiceId.InvoiceDate
        INDtxtThirdParty.EditValue = documentInvoiceProductSalesXpo.ThirdPartyId.NitName
        INDsleDocumentInvoiceProductSales.Properties.NullText = documentInvoiceProductSalesXpo.InvoiceId.InvoiceNumber

        IcaPercentage = 0
        If documentInvoiceProductSalesXpo.ThirdPartyId.Ica Then
            IcaPercentage = documentInvoiceProductSalesXpo.ThirdPartyId.IcaPercentage
        End If
        ContributionType = documentInvoiceProductSalesXpo.ThirdPartyId.ContributionType

        ThirdPartyIvaRetentionConceptRate = 0
        If documentInvoiceProductSalesXpo.ThirdPartyId.IVARetentionConceptId IsNot Nothing Then
            ThirdPartyIvaRetentionConceptRate = documentInvoiceProductSalesXpo.ThirdPartyId.IVARetentionConceptId.Rate
        End If
        If documentInvoiceProductSalesXpo.ContractExternalClients IsNot Nothing AndAlso documentInvoiceProductSalesXpo.ContractExternalClients.Id > 0 Then
            INDlyIExternalContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtxtExternalContract.EditValue = documentInvoiceProductSalesXpo.ContractExternalClients.CodeNumber
        Else
            INDlyIExternalContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Método que carga los detalles de la devolución
    ''' </summary>
    Private Sub ListDetailsDocumentInvoiceProductSalesDevolution()
        CheckForIllegalCrossThreadCalls = False
        GetDocumentInvoiceProductSales()

        ListDocumentInvoiceProductSalesDevolutionDetail = New List(Of DocumentInvoiceProductSalesDevolutionDetail)

        Dim listDetails = Presenter.ListDetailsDocumentInvoiceProductSalesDevolution(DocumentInvoiceProductSalesDevolution.Id)
        If listDetails IsNot Nothing AndAlso listDetails.Count > 0 Then
            If listDetails.FirstOrDefault.ImportSource IsNot Nothing AndAlso listDetails.FirstOrDefault.ImportSource Then
                INDColRemissionOutput.VisibleIndex = 0
            Else
                INDColRemissionOutput.Visible = False
            End If
            For Each itemXpo In listDetails
                Dim detail As New DocumentInvoiceProductSalesDevolutionDetail
                detail.Id = itemXpo.DocumentInvoiceProductSalesDevolutionDetailId
                detail.DocumentInvoiceProductSalesDetailBatchSerialId = itemXpo.DocumentInvoiceProductSalesDetailBatchSerialId
                detail.ProductDescription = itemXpo.ProductDescription
                detail.BatchCode = itemXpo.BatchCode
                detail.Quantity = itemXpo.Quantity
                detail.QuantityDevolution = itemXpo.QuantityDevolution

                detail.SalePrice = itemXpo.SalePrice
                detail.IvaPercentage = itemXpo.IvaPercentage
                detail.DiscountPercentage = itemXpo.DiscountPercentage
                detail.RTFPercentage = itemXpo.RTFPercentage
                detail.SourceCode = itemXpo.SourceCode
                ListDocumentInvoiceProductSalesDevolutionDetail.Add(detail)
            Next

            RefreshTotals()
            ctrTmp.PrintInfo()
        End If

        INDviewProducts.HideLoadingPanel()
        INDgcProducts.DataSource = Nothing
        INDgcProducts.DataSource = ListDocumentInvoiceProductSalesDevolutionDetail
        INDgcProducts.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Método que carga la información de la factura
    ''' </summary>
    Private Sub LoadInformationBill()
        CheckForIllegalCrossThreadCalls = False
        DeleteDetails()
        GetDocumentInvoiceProductSales()

        INDtxtSubTotal.EditValue = documentInvoiceProductSalesXpo.Value
        INDtxtDiscountValue.EditValue = documentInvoiceProductSalesXpo.ValueDiscount
        INDtxtValueTax.EditValue = documentInvoiceProductSalesXpo.ValueTax
        INDtxtWhithholdigTaxValue.EditValue = documentInvoiceProductSalesXpo.WithholdingTax
        INDtxtWhithholdigICAValue.EditValue = documentInvoiceProductSalesXpo.WithholdingICA
        INDtxtWhithholdigSourceValue.EditValue = documentInvoiceProductSalesXpo.RetentionSource
        INDtxtOtherRetention.EditValue = 0
        INDtxtOtherDeduction.EditValue = 0
        INDtxtTotalValue.EditValue = documentInvoiceProductSalesXpo.TotalValue

        ListDocumentInvoiceProductSalesDevolutionDetail = New List(Of DocumentInvoiceProductSalesDevolutionDetail)

        Dim listDetails = Presenter.ListDetailsDocumentInvoiceProductSales(DocumentInvoiceProductSalesId)
        If listDetails IsNot Nothing AndAlso listDetails.Count > 0 Then
            If listDetails.FirstOrDefault.ImportSource IsNot Nothing AndAlso listDetails.FirstOrDefault.ImportSource Then
                INDColRemissionOutput.VisibleIndex = 0
            Else
                INDColRemissionOutput.Visible = False
            End If
            For Each itemXpo In listDetails
                Dim detail As New DocumentInvoiceProductSalesDevolutionDetail
                detail.DocumentInvoiceProductSalesDetailBatchSerialId = itemXpo.DocumentInvoiceProductSalesDetailBatchSerialId
                detail.ProductDescription = itemXpo.ProductDescription
                detail.BatchCode = itemXpo.BatchCode
                detail.Quantity = itemXpo.Quantity
                detail.QuantityDevolution = itemXpo.QuantityDevolution

                detail.SalePrice = itemXpo.SalePrice
                detail.IvaPercentage = itemXpo.IvaPercentage
                detail.DiscountPercentage = itemXpo.DiscountPercentage
                detail.RTFPercentage = itemXpo.RTFPercentage

                detail.SubTotalValue = detail.Quantity * detail.SalePrice
                detail.DiscountValue = detail.SubTotalValue * detail.DiscountPercentage / 100
                detail.IvaValue = (detail.SubTotalValue - detail.DiscountValue) * (detail.IvaPercentage / 100)
                detail.RTFValue = (detail.SubTotalValue - detail.DiscountValue) * (detail.RTFPercentage / 100)
                detail.SourceCode = itemXpo.SourceCode
                detail.TotalValue = detail.SubTotalValue + detail.IvaValue - detail.DiscountValue

                ListDocumentInvoiceProductSalesDevolutionDetail.Add(detail)
            Next

            DevolutionType = True
            ctrTmp.PrintInfo()
        End If

        INDviewProducts.HideLoadingPanel()
        INDgcProducts.DataSource = Nothing
        INDgcProducts.DataSource = ListDocumentInvoiceProductSalesDevolutionDetail
        INDgcProducts.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me.DocumentInvoiceProductSalesDevolution.Code, Me.DocumentInvoiceProductSalesDevolution.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.DocumentInvoiceProductSalesDevolution.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.DocumentInvoiceProductSalesDevolution.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me.DocumentInvoiceProductSalesDevolution.Code, Me.DocumentInvoiceProductSalesDevolution.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.DocumentInvoiceProductSalesDevolution.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        Using Model As New Presentation.Billing.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
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
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssigningValues()
        With DocumentInvoiceProductSalesDevolution
            .Code = Code
            .DocumentDate = DocumentDate
            .WarehouseId = WarehouseId
            .Detail = Detail
            .DocumentInvoiceProductSalesId = DocumentInvoiceProductSalesId
            .Value = INDtxtSubTotal.EditValue
            .ValueDiscount = INDtxtDiscountValue.EditValue
            .ValueTax = INDtxtValueTax.EditValue
            .WithholdingTax = INDtxtWhithholdigTaxValue.EditValue
            .WithholdingICA = INDtxtWhithholdigICAValue.EditValue
            .RetentionSource = INDtxtWhithholdigSourceValue.EditValue
            .RetentionOther = 0
            .DeductionOther = 0
            .DistrictTax = 0
            .TotalValue = INDtxtTotalValue.EditValue
            .OperatingUnitId = BarraBotones.OperatingUnitValue

            If ListDocumentInvoiceProductSalesDevolutionDetail IsNot Nothing AndAlso ListDocumentInvoiceProductSalesDevolutionDetail.Count > 0 Then
                ListDocumentInvoiceProductSalesDevolutionDetail.ForEach(Sub(item) .DocumentInvoiceProductSalesDevolutionDetail.Add(item))
            End If

            If ListDeleteDocumentInvoiceProductSalesDevolutionDetail IsNot Nothing AndAlso ListDeleteDocumentInvoiceProductSalesDevolutionDetail.Count > 0 Then
                ListDeleteDocumentInvoiceProductSalesDevolutionDetail.ForEach(Sub(item) .DocumentInvoiceProductSalesDevolutionDetail.Add(item))
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        SetReadOnly()
        CleanControlsInvoice()
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Code = Nothing
        DocumentDate = GetDateServer()
        WarehouseId = Nothing
        INDsleWarehouse.Properties.NullText = String.Empty
        Detail = Nothing
        INDgcProducts.DataSource = Nothing
        ListDocumentInvoiceProductSalesDevolutionDetail = Nothing
        ListDeleteDocumentInvoiceProductSalesDevolutionDetail = Nothing
        INDlyIExternalContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    Private Sub SetReadOnly()
        ReadOnlyControls(False)
        INDdteBillDate.Properties.ReadOnly = True
        INDtxtThirdParty.Properties.ReadOnly = True
        INDtxtSubTotal.Properties.ReadOnly = True
        INDtxtDiscountValue.Properties.ReadOnly = True
        INDtxtValueTax.Properties.ReadOnly = True
        INDtxtWhithholdigTaxValue.Properties.ReadOnly = True
        INDtxtWhithholdigICAValue.Properties.ReadOnly = True
        INDtxtWhithholdigSourceValue.Properties.ReadOnly = True
        INDtxtOtherDeduction.Properties.ReadOnly = True
        INDtxtOtherRetention.Properties.ReadOnly = True
        INDtxtTotalValue.Properties.ReadOnly = True
        INDtxtExternalContract.ReadOnly = True
    End Sub

    ''' <summary>
    ''' metodo para cargar controles al formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            Try
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If
                Using model As New MDocumentInvoiceProductSalesDevolution(MyTag)
                    AsyncLoader(True)
                    INDlyRoot.BeginUpdate()

                    Me.DocumentInvoiceProductSalesDevolution = (Await model.GetDocumentInvoiceProductSalesDevolution(Code)).ObjectEmbbeded
                    If Me.DocumentInvoiceProductSalesDevolution IsNot Nothing AndAlso Me.DocumentInvoiceProductSalesDevolution.Id > 0 Then
                        Using ModelRecord As New Presentation.Billing.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Me.DocumentInvoiceProductSalesDevolution.Id))

                            With Me.DocumentInvoiceProductSalesDevolution
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Me.BarraBotones.StatusRecordVisible = True
                                Me.BarraBotones.StatusRecord = .Status.ToString()
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case 2
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAnnular)
                                        ReadOnlyControls(True)
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                End Select

                                IsLoadControls = True
                                Code = .Code
                                DocumentDate = .DocumentDate
                                WarehouseId = .WarehouseId
                                INDsleWarehouse.Properties.NullText = .WarehouseDescription
                                Detail = .Detail
                                DocumentInvoiceProductSalesId = .DocumentInvoiceProductSalesId
                                IsLoadControls = False

                                INDtxtSubTotal.EditValue = .Value
                                INDtxtDiscountValue.EditValue = .ValueDiscount
                                INDtxtValueTax.EditValue = .ValueTax
                                INDtxtWhithholdigTaxValue.EditValue = .WithholdingTax
                                INDtxtWhithholdigICAValue.EditValue = .WithholdingICA
                                INDtxtWhithholdigSourceValue.EditValue = .RetentionSource
                                INDtxtTotalValue.EditValue = .TotalValue

                                INDviewProducts.ShowLoadingPanel()
                                Await Task.Factory.StartNew(AddressOf ListDetailsDocumentInvoiceProductSalesDevolution)
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.DocumentInvoiceProductSalesDevolution.Code)

                            If Me.record.Id = 0 Then
                                Me.record = (Await ModelRecord.SaveBlockRecord(
                                        New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                            .CodUser = Me.indigo.UserIndigo, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .IdRecord = Me.DocumentInvoiceProductSalesDevolution.Id})
                                        ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), Me.record.CodUser, Me.record.NameUser, Me.record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, Me.record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(Me.DocumentInvoiceProductSalesDevolution.Id, Me.Tag.ToString(), Nothing, GetType(BasicBilling).Name)

                            AsyncLoader(False)
                        End Using
                        ActionsOnControls = True
                    Else
                        AsyncLoader(False)
                        If Me.Sequense.IsManual Then
                            Await Me.NewDocumentInvoiceProductSalesDevolution()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If

                    INDlyRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateInvoiced"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewDocumentInvoiceProductSalesDevolution() As Task
        If Me._sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No existe secuencia numérica para el formulario"
            INDbtnCode.Focus()
            Exit Function
        End If
        DocumentInvoiceProductSalesDevolution = New DocumentInvoiceProductSalesDevolution
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.BillingSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.BillingSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.BillingSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Not Me._sequense.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New Presentation.Billing.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmDocumentInvoiceProductSalesDevolution_LoadAsync(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PDocumentInvoiceProductSalesDevolution(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        Await LoadParameters()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(302, Nothing, True)
            Presenter.ListWarehouse()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleWarehouse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleWarehouse.QueryPopUp
        If WarehouseXpo Is Nothing Then
            Presenter.ListWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de factura de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDocumentInvoiceProductSales_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDocumentInvoiceProductSales.QueryPopUp
        If DocumentInvoiceProductSalesDatasourceXpo Is Nothing AndAlso WarehouseId <> Nothing Then
            Presenter.ListDocumentInvoiceProductSales(WarehouseId)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDocumentInvoiceProductSales_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDocumentInvoiceProductSales.EditValueChanged
        If DocumentInvoiceProductSalesId <> Nothing AndAlso IsLoadControls = False Then
            INDviewProducts.ShowLoadingPanel()
            Task.Factory.StartNew(AddressOf LoadInformationBill)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleWarehouse.EditValueChanged
        If WarehouseId <> Nothing AndAlso IsLoadControls = False Then
            CleanControlsInvoice()
            DeleteDetails()
            INDgcProducts.DataSource = Nothing
            ListDocumentInvoiceProductSalesDevolutionDetail = Nothing
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnCode_KeyDownAsync(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If Me._sequense Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha terminado de cargar la secuencia numérica"
            Exit Sub
        End If
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    NewDocumentInvoiceProductSalesDevolution()
                Else
                    Await Me.LoadControls()
                End If
            End If
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Public Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.DocumentInvoiceProductSalesDevolution IsNot Nothing AndAlso Me.DocumentInvoiceProductSalesDevolution.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
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

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDocumentInvoiceProductSalesDevolution_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDocumentInvoiceProductSalesDevolution_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de la rejilla para la cantidad a devolver
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepSpeQuantityDevolution_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepSpeQuantityDevolution.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.NewValue) Then
            Dim entity As DocumentInvoiceProductSalesDevolutionDetail = INDviewProducts.GetFocusedRow()
            If CInt(e.NewValue) > CInt(entity.Quantity) Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser mayor a la cantidad"
                e.Cancel = True
                Exit Sub
            End If
            entity.QuantityDevolution = e.NewValue
            RefreshTotals()
        End If
    End Sub

#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        DocumentInvoiceProductSalesDevolution.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        DocumentInvoiceProductSalesDevolution.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        DocumentInvoiceProductSalesDevolution.Status = 2
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
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        DocumentInvoiceProductSalesDevolution.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BillingSequenceDetail IsNot Nothing Then
            If Me._sequense.BillingSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.BillingSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            DocumentInvoiceProductSalesDevolution.Status = 3
            Guardar()
        End If
    End Sub

#End Region

End Class
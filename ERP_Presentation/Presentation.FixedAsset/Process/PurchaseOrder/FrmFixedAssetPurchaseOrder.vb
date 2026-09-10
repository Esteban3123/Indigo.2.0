'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 19-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports System.Text
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Repository
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP
Imports Presentation.Inventory
Imports Presentation.Inventory.MVP
Imports Presentation.Maintenance.MVP

#End Region

Public Class FrmFixedAssetPurchaseOrder
    Implements IFixedAssetPurchaseOrder, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        _ctrTmp = New CtrTotalInvoiceEntranceVoucher()
        _ctrTmp.SetInfoFunction(AddressOf GetValuesList)
        _ctrTmp.PrintInfo()
        _ctrTmp.NameValueBill = "VALOR ORDEN"
        _ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_ctrTmp)
    End Sub

    ''' <summary>
    ''' Muestra los valores en el control
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetValuesList() As Tuple(Of String, String, String, String)
        Return New Tuple(Of String, String, String, String)(_netoValue, _discountValue, _ivaValue, _totalValue)
    End Function

#End Region

#Region "Globals"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const NAME_MODULE = "FixedAssets"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Private _paymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Representa a la entidad de parámetros de inventario
    ''' </summary>
    Public _fixedAssetSettingsXpo As SettingFixedAssetXpo

    ''' <summary>
    ''' presenter de remision de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PFixedAssetPurchaseOrder

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private _ctrTmp As CtrTotalInvoiceEntranceVoucher

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordFixedAsset

    ''' <summary>
    ''' entidad de remision de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private _purchaseOrder As FixedAssetPurchaseOrder

    ''' <summary>
    ''' entidad detallada de la orden de compra
    ''' </summary>
    Private _fixedAssetPurchaseOrderEquipment As FixedAssetPurchaseOrderItem

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se repitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Private _listFixedAssetPurchaseOrderEquipment As List(Of FixedAssetPurchaseOrderItem)

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos a eliminar
    ''' </summary>
    Private _listFixedAssetPurchaseOrderEquipmentValidationDelete As List(Of FixedAssetPurchaseOrderItem)

    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierId As Integer

    ''' <summary>
    ''' Variable que contiene la lista de tipos de ordenes
    ''' </summary>
    Private _listOrderType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de ordenes
    ''' </summary>
    Private _listWayPay As New List(Of Tuple(Of String, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de ordenes
    ''' </summary>
    Private _listWarranty As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Sumatoria de los descuentos de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Private _discountValue As Decimal

    ''' <summary>
    ''' Sumatoria del valor del iva de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Private _ivaValue As Decimal

    ''' <summary>
    ''' Total a pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _totalValue As Decimal

    ''' <summary>
    ''' Sumatoria del valor neto de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Private _netoValue As Decimal

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _varImp As Integer

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Private _indexEditRecord As Integer

    ''' <summary>
    ''' Permite controlar cuando se carga desde el loadControls
    ''' </summary>
    Private _isLoading As Boolean

    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _roundingType As Decimal = 1

    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _tRM As TRM

#End Region

#Region "Variables"

    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IFixedAssetPurchaseOrder.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetPurchaseOrder.ActionsOnControls
        Set(value As Boolean)
            INDlcReferralEntry.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = value
            INDdteDeliverDate.Enabled = value
            INDSleSupplierDistributionLine.Enabled = value
            INDsleCurrency.Enabled = value
            INDMeDetail.Enabled = value
            INDTxtQuotationNumber.Enabled = value
            INDSleOrderType.Enabled = value
            INDSleWayPay.Enabled = value
            INDSleWarranty.Enabled = value
            INDsleRequestedFunctionalUnit.Enabled = value
            INDTxtDeliveryPlace.Enabled = value
            'INDTxtDeadlineDays.Enabled = value
            INDBtnAdd.Enabled = value
            INDGcReferralEntry.Enabled = value

            INDSleBudgetaryEntityId.Enabled = value
            INDSleBudgetaryValidityId.Enabled = value
            INDsleAvailability.Enabled = value
            INDbtnAddAvailability.Enabled = value
            INDgcAvailability.Enabled = value

            INDlcReferralEntry.EndUpdate()
            If value = False Then
                INDLciOtherTypeOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization
                INDLciOtherTypeOrder.AllowHide = True
                INDBteCode.Focus()
            Else
                INDSleSupplierDistributionLine.Focus()
            End If

        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements IFixedAssetPurchaseOrder.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequense As Domain.Entities.FixedAssetSequence Implements IFixedAssetPurchaseOrder.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

#Region "Properties"

    ''' <summary>
    ''' codigo de la remision
    ''' </summary>
    Public Property Code As String Implements IFixedAssetPurchaseOrder.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' fecha de la remision
    ''' </summary>
    Public Property PurchaseOrderDate As DateTime? Implements IFixedAssetPurchaseOrder.PurchaseOrderDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    Public Property SupplierDistributionLineId As Integer? Implements IFixedAssetPurchaseOrder.SupplierDistributionLineId
        Get
            Return INDSleSupplierDistributionLine.EditValue
        End Get
        Set(value As Integer?)
            INDSleSupplierDistributionLine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la moneda, recibe como parametro opcional la abreviacion para cuando se postula manualmente el Id
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer Implements IFixedAssetPurchaseOrder.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
            INDsleCurrency.Properties.NullText = _currencyAbbreviation
            SetCurrencyUI(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establce el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyDatasource As XPInstantFeedbackSource Implements IFixedAssetPurchaseOrder.CurrencyDatasource
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Moneda seleccionada carga registro cuando se ha desplegado el combo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencySelected As Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo
        Get
            Return TryCast(TryCast(INDGvCurrencyAdvance.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)
        End Get
    End Property

    ''' <summary>
    ''' propiedad para tomar la abreviacion de la moneda y guardarla temp para cuando se necesite editar
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String
        Get
            Return INDsleCurrency.Text
        End Get
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el trm
    ''' </summary>
    ''' <returns></returns>
    Private Property TRM As TRM
        Get
            Return _tRM
        End Get
        Set(value As TRM)
            _tRM = value
        End Set
    End Property

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    Public Property Description As String Implements IFixedAssetPurchaseOrder.Description
        Get
            Return INDMeDetail.Text
        End Get
        Set(value As String)
            INDMeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' numero de la cotizacion
    ''' </summary>
    ''' <returns></returns>
    Public Property QuotationNumber As String Implements IFixedAssetPurchaseOrder.QuotationNumber
        Get
            Return INDTxtQuotationNumber.Text
        End Get
        Set(value As String)
            INDTxtQuotationNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de orden
    ''' </summary>
    ''' <returns></returns>
    Public Property OrderType As Integer? Implements IFixedAssetPurchaseOrder.OrderType
        Get
            Return INDSleOrderType.EditValue
        End Get
        Set(value As Integer?)
            INDSleOrderType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' otro tipo de orden
    ''' </summary>
    ''' <returns></returns>
    Public Property OtherOrderType As String Implements IFixedAssetPurchaseOrder.OtherOrderType
        Get
            Return INDTxtOtherTypeOrder.Text
        End Get
        Set(value As String)
            INDTxtOtherTypeOrder.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Forma de pago
    ''' </summary>
    ''' <returns></returns>
    Public Property WayPay As String Implements IFixedAssetPurchaseOrder.WayPay
        Get
            Return INDSleWayPay.EditValue
        End Get
        Set(value As String)
            INDSleWayPay.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Garantia
    ''' </summary>
    ''' <returns></returns>
    Public Property Warranty As String Implements IFixedAssetPurchaseOrder.Warranty
        Get
            Return INDSleWarranty.EditValue
        End Get
        Set(value As String)
            INDSleWarranty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de entrega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeliverDate As Date? Implements IFixedAssetPurchaseOrder.DeliverDate
        Get
            Return INDdteDeliverDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDeliverDate.EditValue = value
        End Set
    End Property

    Public Property RequestedFunctionalUnitId As Integer? Implements IFixedAssetPurchaseOrder.RequestedFunctionalUnitId
        Get
            Return INDsleRequestedFunctionalUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleRequestedFunctionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' lugar de entrega
    ''' </summary>
    ''' <returns></returns>
    Public Property DeliveryPlace As String Implements IFixedAssetPurchaseOrder.DeliveryPlace
        Get
            Return INDTxtDeliveryPlace.Text
        End Get
        Set(value As String)
            INDTxtDeliveryPlace.Text = value
        End Set
    End Property

    ''' <summary>
    ''' dias de plazo del proveedor
    ''' </summary>
    ''' <returns></returns>
    Public Property DeadlineDays As String Implements IFixedAssetPurchaseOrder.DeadlineDays
        Get
            Return INDTxtDeadlineDays.Text
        End Get
        Set(value As String)
            INDTxtDeadlineDays.Text = value
        End Set
    End Property

    Dim _datasourceacquisition As List(Of Tuple(Of Integer, String))
    ReadOnly Property DatasourceAcquisition As List(Of Tuple(Of Integer, String))
        Get
            If _datasourceacquisition Is Nothing Then
                _datasourceacquisition = New List(Of Tuple(Of Integer, String))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(2, "Leasing"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(3, "Comodato"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(4, "Donado por Particulares"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(5, "Traspaso de Bienes"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(6, "Otro Concepto"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(7, "Apoyo Tecnológico"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(8, "Recuperación"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(9, "Consignación"))
            End If
            Return _datasourceacquisition
        End Get
    End Property

    ''' <summary>
    ''' datasource de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleSupplierDistributionLine.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSupplierDistributionLine.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de Unidad funcional
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestedFunctionalUnitXPO As XPInstantFeedbackSource Implements IFixedAssetPurchaseOrder.RequestedFunctionalUnitXPO
        Get
            Return CType(INDsleRequestedFunctionalUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRequestedFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

#Region "Budget Interface"

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Dim _budgetInterface As Boolean

            If _paymentsSettingPaymentsXpo IsNot Nothing AndAlso _paymentsSettingPaymentsXpo.BudgetInterface Then
                _budgetInterface = True
            End If

            Return _budgetInterface
        End Get
    End Property

    Public ReadOnly Property CommitmentBudgetInterface As Boolean
        Get
            Return If(_fixedAssetSettingsXpo Is Nothing, False, _fixedAssetSettingsXpo.CommitmentBudgetInterface)
        End Get
    End Property

    Public Property BudgetaryEntityId As Integer?
        Get
            Return If(String.IsNullOrEmpty(INDSleBudgetaryEntityId.EditValue), Nothing, INDSleBudgetaryEntityId.EditValue)
        End Get
        Set(value As Integer?)
            INDSleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As DevExpress.Xpo.XPCollection Implements IFixedAssetPurchaseOrder.BudgetaryEntityXpo
        Get
            Return INDSleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDSleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer?
        Get
            Return If(String.IsNullOrEmpty(INDSleBudgetaryValidityId.EditValue), Nothing, INDSleBudgetaryValidityId.EditValue)
        End Get
        Set(value As Integer?)
            INDSleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPCollection Implements IFixedAssetPurchaseOrder.BudgetaryValidityXpo
        Get
            Return INDSleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDSleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property AvailabilityXpo As XPInstantFeedbackSource Implements IFixedAssetPurchaseOrder.AvailabilityXpo
        Get
            Return INDsleAvailability.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAvailability.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Representa el listado de detalles de disponibilidades
    ''' </summary>
    Public Property ListPurchaseOrderAvailability As List(Of FixedAssetPurchaseOrderAvailability) Implements IFixedAssetPurchaseOrder.ListPurchaseOrderAvailability
        Get
            Return INDgcAvailability.DataSource
        End Get
        Set(value As List(Of FixedAssetPurchaseOrderAvailability))
            INDgcAvailability.DataSource = value
            INDgcAvailability.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Representa el listado de eliminados de detalles de disponibilidades
    ''' </summary>
    Private ListDeletePurchaseOrderAvailability As List(Of FixedAssetPurchaseOrderAvailability)

#End Region

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If _purchaseOrder.Status <> 3 Then
            If ValidateControls() Then
                If INDGvReferralEntry.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un (1) detalle."
                    Exit Sub
                End If

                If Not ValidateInterfaceBudget() Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If

        Try
            AssigningValues()
            Using model As New MFixedAssetPurchaseOrder(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SavePurchaseOrder(_purchaseOrder, _idCurrentSequence, Me._sequence)
                If Result.StateResult = True Then
                    If _purchaseOrder.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            If _purchaseOrder.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf _purchaseOrder.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If _purchaseOrder.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf _purchaseOrder.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = Result.Message
                        ElseIf _purchaseOrder.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me._purchaseOrder = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _purchaseOrder.Id, 0, _purchaseOrder.Id, _idOperativeUnit)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _purchaseOrder.Id, 0, _purchaseOrder.Id, _idOperativeUnit)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _purchaseOrder.Id, 0, _purchaseOrder.Id, _idOperativeUnit)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _purchaseOrder.Id, 0, _purchaseOrder.Id, _idOperativeUnit)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Result.MessageResult IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.MessageResult(0)) Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEquipmentEntry()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadPaymentsSetting()
        Try
            AsyncLoader(True)
            'Se obtiene los parámetros de pagos por unidad operativa
            If BarraBotones.OperatingUnitValue <> Nothing AndAlso BarraBotones.OperatingUnitValue > 0 Then
                _paymentsSettingPaymentsXpo = _presenter.GetSettingsPaymentsByOperatingUnitId(BarraBotones.OperatingUnitValue)
                _fixedAssetSettingsXpo = _presenter.GetSettingsFixedAssetByOperatingUnitId(BarraBotones.OperatingUnitValue)
            End If

            ShowHideBudgetInterface()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Se presentó un error al cargar los datos de la unidad operativa"
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Método que muestra/oculta el control de compromiso
    ''' </summary>
    Private Sub ShowHideBudgetInterface()
        INDLciBudgetaryEntityId.AllowHide = Not (BudgetInterface AndAlso CommitmentBudgetInterface)
        INDLciBudgetaryEntityId.ShowInCustomizationForm = Not (BudgetInterface AndAlso CommitmentBudgetInterface)
        INDLciBudgetaryValidityId.AllowHide = Not (BudgetInterface AndAlso CommitmentBudgetInterface)
        INDLciBudgetaryValidityId.ShowInCustomizationForm = Not (BudgetInterface AndAlso CommitmentBudgetInterface)
        INDlygBudget.Visibility = If(BudgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    Private Sub CleanBudgetInterface(level As Integer)
        If level < 1 Then
            BudgetaryEntityId = Nothing
            INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        End If
        If level < 2 Then
            BudgetaryValidityId = Nothing
            INDSleBudgetaryValidityId.Properties.NullText = String.Empty
            BudgetaryValidityXpo = Nothing
        End If
        If level < 3 Then
            If Not _isLoading Then
                If ListPurchaseOrderAvailability IsNot Nothing AndAlso ListPurchaseOrderAvailability.Any() Then
                    If ListDeletePurchaseOrderAvailability Is Nothing Then
                        ListDeletePurchaseOrderAvailability = New List(Of FixedAssetPurchaseOrderAvailability)
                    End If

                    For Each PurchaseOrderAvailability In ListPurchaseOrderAvailability
                        If PurchaseOrderAvailability.Id > 0 Then
                            ListDeletePurchaseOrderAvailability.Add(PurchaseOrderAvailability)
                        End If
                    Next
                End If
                ListPurchaseOrderAvailability = Nothing
                _purchaseOrder.FixedAssetPurchaseOrderAvailability.Clear()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultEntity()
        If BudgetaryEntityXpo IsNot Nothing AndAlso BudgetaryEntityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryEntityXpo Where l.Status = 1 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryEntityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para activar los controles de presupuesto
    ''' </summary>
    Private Sub EnableBudgetInterface()
        If _purchaseOrder IsNot Nothing AndAlso _purchaseOrder.Status <> 1 Then
            Exit Sub
        End If

        Dim status As Boolean = False
        If ListPurchaseOrderAvailability IsNot Nothing AndAlso ListPurchaseOrderAvailability.Any() Then
            status = True
        End If

        INDSleBudgetaryEntityId.Properties.ReadOnly = status
        INDSleBudgetaryValidityId.Properties.ReadOnly = status
    End Sub

    ''' <summary>
    ''' Metodo que valida la interfaz presupuestal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateInterfaceBudget() As Boolean
        If BudgetInterface Then
            Dim listErrors As New StringBuilder

            If CommitmentBudgetInterface Then
                If ListPurchaseOrderAvailability Is Nothing OrElse Not ListPurchaseOrderAvailability.Any(Function(d) d.Value > 0) Then
                    listErrors.AppendLine("Debe agregar al menos una disponibilidad con el cual realizar el compromiso presupuestal")
                End If
            End If

            If ListPurchaseOrderAvailability IsNot Nothing AndAlso ListPurchaseOrderAvailability.Any Then
                Dim purchaseOrderValue As Decimal = Math.Round(_totalValue, 0, MidpointRounding.AwayFromZero)
                Dim commitmentValue As Decimal = ListPurchaseOrderAvailability.Where(Function(d) d.Value > 0).Sum(Function(d) d.Value)
                If commitmentValue > purchaseOrderValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales superan el valor de la orden de compra")
                ElseIf commitmentValue < purchaseOrderValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales son menores al valor de la orden de compra")
                End If
            End If

            If listErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Elimina una disponibilidad
    ''' </summary>
    Private Sub DeleteAvailability()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim entity As FixedAssetPurchaseOrderAvailability = INDviewAvailability.GetFocusedRow()
        ListPurchaseOrderAvailability.Remove(entity)

        If entity.Id > 0 Then
            If ListDeletePurchaseOrderAvailability Is Nothing Then
                ListDeletePurchaseOrderAvailability = New List(Of FixedAssetPurchaseOrderAvailability)
            End If
            entity.MarkAsDeleted()
            ListDeletePurchaseOrderAvailability.Add(entity)
        End If

        EnableBudgetInterface()
        INDgcAvailability.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub HideButtonImportInformation()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = Not (OrderType IsNot Nothing AndAlso OrderType.GetValueOrDefault = 1 AndAlso SupplierDistributionLineId IsNot Nothing AndAlso SupplierDistributionLineId.GetValueOrDefault > 0)
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddRemissionEntranceDetail(sender As Object, e As AddEquipmentPurchaseOrderEventArgs)
        If _listFixedAssetPurchaseOrderEquipment Is Nothing Then
            _listFixedAssetPurchaseOrderEquipment = New List(Of FixedAssetPurchaseOrderItem)
        End If

        If e.EditMode = True Then
            _listFixedAssetPurchaseOrderEquipment.Remove(_fixedAssetPurchaseOrderEquipment)
            _listFixedAssetPurchaseOrderEquipment.Insert(_indexEditRecord, e.FixedAssetPurchaseOrderEquipment)
        ElseIf e.ImportDataMode Then
            For Each poe In e.ListFixedAssetPurchaseOrderEquipment
                _listFixedAssetPurchaseOrderEquipment.Add(poe)
            Next
        Else
            _listFixedAssetPurchaseOrderEquipment.Add(e.FixedAssetPurchaseOrderEquipment)
        End If

        RefreshValues(False)

        INDGcReferralEntry.DataSource = Nothing
        INDGcReferralEntry.DataSource = _listFixedAssetPurchaseOrderEquipment
    End Sub

    ''' <summary>
    ''' Refresca los valores del control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshValues(CleanValues As Boolean)
        _netoValue = 0
        _ivaValue = 0
        _discountValue = 0
        _totalValue = 0
        If CleanValues = False Then
            _netoValue = _listFixedAssetPurchaseOrderEquipment.Sum(Function(item) item.SubTotalValue)
            _ivaValue = _listFixedAssetPurchaseOrderEquipment.Sum(Function(item) item.IvaValue)
            _discountValue = _listFixedAssetPurchaseOrderEquipment.Sum(Function(item) item.DiscountValue)
            _totalValue = _listFixedAssetPurchaseOrderEquipment.Sum(Function(item) item.TotalValue)
        End If
        _ctrTmp.PrintInfo()
    End Sub

    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 150},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Detail", .ColumnWidth = 230},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "PurchaseOrderDate", .ColumnWidth = 150},
                              New ColumnInfo() With {.Caption = "Fecha Entrega", .FieldName = "DeliverDate", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Proveedor", .FieldName = "SupplierDistributionLineId.IdSupplier.CodeName", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = 75},
                              New ColumnInfo() With {.Caption = "Valor", .ColumnFormat = "n2", .ColumnFormatType = DevExpress.Utils.FormatType.Numeric, .FieldName = "TotalValue", .ColumnWidth = 150}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPurchaseOrder
            .ValorSolicitado = "Code"
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvReferralEntry, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvReferralEntry.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        Dim ListActions2 As New List(Of eAcciones)
        ListActions2.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDviewAvailability, ListActions2)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewAvailability.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceFixedAsset(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequenceFixedAsset(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me._purchaseOrder.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _record = New BlockRecordFixedAsset With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me._purchaseOrder.Id}
                Dim operation = Await model.SaveBlockRecord(_record)
                _record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                _record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._purchaseOrder.Code, INDSleSupplierDistributionLine.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._purchaseOrder.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._purchaseOrder.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._purchaseOrder.Code, INDSleSupplierDistributionLine.Text)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._purchaseOrder.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        'Tipo de orden
        _listOrderType = New List(Of Tuple(Of Integer, String))
        _listOrderType.Add(New Tuple(Of Integer, String)(1, "Compra / Artículos"))
        _listOrderType.Add(New Tuple(Of Integer, String)(2, "Servicios / Mantenimiento"))
        _listOrderType.Add(New Tuple(Of Integer, String)(3, "Otro"))
        INDSleOrderType.Properties.DataSource = _listOrderType.ToList

        _listWayPay = New List(Of Tuple(Of String, String))
        _listWayPay.Add(New Tuple(Of String, String)("Credito", "Credito"))
        _listWayPay.Add(New Tuple(Of String, String)("Contado", "Contado"))
        INDSleWayPay.Properties.DataSource = _listWayPay.ToList

        _listWarranty = New List(Of Tuple(Of Integer, String))
        _listWarranty.Add(New Tuple(Of Integer, String)(1, "Si"))
        _listWarranty.Add(New Tuple(Of Integer, String)(0, "No"))
        INDSleWarranty.Properties.DataSource = _listWarranty.ToList

    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewEquipmentEntry() As Task
        Me._purchaseOrder = New FixedAssetPurchaseOrder()
        PurchaseOrderDate = GetDateServer()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
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
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If

        If BudgetInterface Then
            _presenter.InitializeBudgetaryEntity()
            Me.SetFirstOrDefaultEntity()
        End If
    End Function

    ''' <summary>
    ''' carga los controles con la informacion
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
                Using Model As New MFixedAssetPurchaseOrder(CStr(Me.Tag))
                    AsyncLoader(True)
                    _purchaseOrder = (Await Model.GetFixedAssetPurchaseOrderByCode(INDBteCode.Text.Trim)).ObjectEmbbeded
                    INDlcReferralEntry.BeginUpdate()
                    If _purchaseOrder IsNot Nothing AndAlso _purchaseOrder.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_purchaseOrder.Id))

                            _isLoading = True
                            With _purchaseOrder
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDSleSupplierDistributionLine.Properties.NullText = .NameSuplier
                                PurchaseOrderDate = .PurchaseOrderDate
                                DeliverDate = .DeliverDate
                                Dim x = .Currency
                                Me.CurrencyId(.Currency?.Abbreviation) = .CurrencyId
                                Await Me.ValidateCurrencyEditValue(False, .CurrencyId)
                                INDMeDetail.EditValue = .Detail
                                QuotationNumber = .QuotationNumber
                                OrderType = .OrderType
                                OtherOrderType = .OtherOrderType
                                WayPay = .WayPay
                                Warranty = .Warranty
                                RequestedFunctionalUnitId = .RequestedFunctionalUnitId
                                INDsleRequestedFunctionalUnit.Properties.NullText = .RequestedFunctionalUnitName
                                DeliveryPlace = .DeliveryPlace

                                BarraBotones.StatusRecord = .Status.ToString()

                                _listFixedAssetPurchaseOrderEquipment = .FixedAssetPurchaseOrderItem.ToList()
                                INDGcReferralEntry.DataSource = Nothing
                                INDGcReferralEntry.DataSource = _listFixedAssetPurchaseOrderEquipment

                                If BudgetInterface Then
                                    BudgetaryEntityId = .BudgetaryEntityId
                                    INDSleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                    BudgetaryValidityId = .BudgetaryValidityId
                                    INDSleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                    ListPurchaseOrderAvailability = .FixedAssetPurchaseOrderAvailability.ToList()
                                End If
                                RefreshValues(False)
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._purchaseOrder.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _purchaseOrder.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If

                            If _purchaseOrder.IsPartiallyLegalized Then
                                INDcolOutstandingQuantity.ShowColumn ''cantidad pendiente
                                INDcolAmount.HideColumn '' cantidad inicial
                            Else
                                INDcolOutstandingQuantity.HideColumn
                                INDcolAmount.ShowColumn
                            End If

                            ActionsOnControls = True
                            If _purchaseOrder.Status = 1 Then ''estado registrado
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                HideButtonImportInformation()
                                If _purchaseOrder.IsPartiallyLegalized Then
                                    ReadOnlyControls(True)
                                End If
                            ElseIf _purchaseOrder.Status = 2 Then 'estado confirmado
                                ReadOnlyControls(True)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyDisconfirm)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                ReadOnlyControls(True)
                            End If

                            If _purchaseOrder.IsPartiallyLegalized Then
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                            End If

                            '''Validacion de ocultacion de accion de desconfirmar
                            If _purchaseOrder.FixedAssetEntryCodes?.Any() OrElse _purchaseOrder.FixedAssetRemissionEntranceCodes?.Any() Then
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
                                Dim fixedAssetEntryCodes As String = If(_purchaseOrder.FixedAssetEntryCodes?.Any(), String.Join(", ", _purchaseOrder.FixedAssetEntryCodes), "Ningún ingreso de activos")
                                Dim remissionCodes As String = If(_purchaseOrder.FixedAssetRemissionEntranceCodes?.Any(), String.Join(", ", _purchaseOrder.FixedAssetRemissionEntranceCodes), "Ninguna remisión de entrada")
                                Mensaje(EeventViewerImages.Advertencia) = "La orden de compra " & _purchaseOrder.Code & " tiene todos los productos legalizados" & vbCrLf &
                                          "en los ingresos de activos: " & fixedAssetEntryCodes & " y/o en la remisión de entrada: " & remissionCodes & ""
                            End If


                            EnableBudgetInterface()
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _purchaseOrder.Id, 0, _purchaseOrder.Id, _idOperativeUnit)
                            Me.BarraBotones.SetDocuments(_purchaseOrder.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetPurchaseOrder).Name)
                            _isLoading = False
                            AsyncLoader(False)

                            INDBteCode.Enabled = False
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEquipmentEntry()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlcReferralEntry.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Function LoadReport() As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(PrintReportAction.None, _purchaseOrder.Id, 0, _purchaseOrder.Id, _idOperativeUnit))
                                     End Sub)
    End Function

    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlcReferralEntry.BeginUpdate()
        ReadOnlyControls(False)
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Code = String.Empty
        PurchaseOrderDate = GetDateServer()
        DeliverDate = Nothing
        SupplierDistributionLineId = Nothing
        INDSleSupplierDistributionLine.Properties.NullText = String.Empty
        Me.CurrencyDatasource = Nothing
        Me.CurrencyId(Me.indigo?.CurrencyISO4217) = Me.indigo?.OfficialCurrencyId
        Description = String.Empty
        QuotationNumber = String.Empty
        OrderType = Nothing
        OtherOrderType = String.Empty
        WayPay = String.Empty
        Warranty = String.Empty
        RequestedFunctionalUnitId = Nothing
        INDsleRequestedFunctionalUnit.Properties.NullText = String.Empty
        DeliveryPlace = String.Empty
        DeadlineDays = String.Empty
        HideButtonImportInformation()

        INDGcReferralEntry.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcReferralEntry)
        _listFixedAssetPurchaseOrderEquipment = Nothing
        _listFixedAssetPurchaseOrderEquipmentValidationDelete = Nothing
        _purchaseOrder = Nothing

        ListPurchaseOrderAvailability = Nothing
        ListDeletePurchaseOrderAvailability = Nothing
        INDgcAvailability.DataSource = Nothing
        INDsleAvailability.EditValue = Nothing

        _isLoading = False
        RefreshValues(True)
        BarraBotones.StatusRecord = "1"
        BarraBotones.StatusRecordVisible = False
        ActionsOnControls = False
        INDlcReferralEntry.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' asigna los valroes a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _purchaseOrder
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = _idOperativeUnit
            .Code = Code
            .PurchaseOrderDate = PurchaseOrderDate
            .DeliverDate = DeliverDate
            .SupplierDistributionLineId = INDSleSupplierDistributionLine.EditValue
            .CurrencyId = CurrencyId
            .Detail = Description
            .QuotationNumber = QuotationNumber
            .OrderType = OrderType
            .OtherOrderType = OtherOrderType
            .WayPay = WayPay
            .Warranty = Warranty
            .DeliveryPlace = DeliveryPlace
            .RequestedFunctionalUnitId = RequestedFunctionalUnitId
            '.RequestedFunctionalUnitName = INDsleRequestedFunctionalUnit.Text
            .DeadlineDays = DeadlineDays

            If _listFixedAssetPurchaseOrderEquipment IsNot Nothing AndAlso _listFixedAssetPurchaseOrderEquipment.Count > 0 Then
                For Each item In _listFixedAssetPurchaseOrderEquipment
                    .FixedAssetPurchaseOrderItem.Add(item)
                Next
            End If

            If _listFixedAssetPurchaseOrderEquipmentValidationDelete IsNot Nothing AndAlso _listFixedAssetPurchaseOrderEquipmentValidationDelete.Count > 0 Then
                For Each item In _listFixedAssetPurchaseOrderEquipmentValidationDelete
                    .FixedAssetPurchaseOrderItem.Add(item)
                Next
            End If

            If BudgetInterface Then
                If ListPurchaseOrderAvailability IsNot Nothing Then
                    _purchaseOrder.BudgetaryEntityId = BudgetaryEntityId
                    _purchaseOrder.BudgetaryEntityDescription = INDSleBudgetaryEntityId.Text
                    _purchaseOrder.BudgetaryValidityId = BudgetaryValidityId
                    _purchaseOrder.BudgetaryValidityDescription = INDSleBudgetaryValidityId.Text

                    For Each purchaseOrderAvailability In ListPurchaseOrderAvailability
                        If purchaseOrderAvailability.Value > 0 Then
                            If purchaseOrderAvailability.ChangeTracker.State = ObjectState.Added Then
                                _purchaseOrder.FixedAssetPurchaseOrderAvailability.Add(purchaseOrderAvailability)
                            ElseIf purchaseOrderAvailability.ChangeTracker.State = ObjectState.Modified Then
                                purchaseOrderAvailability.MarkAsModified()
                            End If
                        Else
                            purchaseOrderAvailability.MarkAsDeleted()
                        End If
                    Next
                End If
            End If

            If ListDeletePurchaseOrderAvailability IsNot Nothing Then
                For Each purchaseOrderAvailability In ListDeletePurchaseOrderAvailability
                    purchaseOrderAvailability.MarkAsDeleted()
                    _purchaseOrder.FixedAssetPurchaseOrderAvailability.Add(purchaseOrderAvailability)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        _fixedAssetPurchaseOrderEquipment = DirectCast(INDGvReferralEntry.GetFocusedRow(), FixedAssetPurchaseOrderItem)
        _indexEditRecord = _listFixedAssetPurchaseOrderEquipment.IndexOf(_fixedAssetPurchaseOrderEquipment)
        Using formulario As New FrmPopupPurchaseOrderEquipment(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                            _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value),
                                                            _roundType:=_roundingType)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEquipmentRemissionEventArgs, AddressOf ReturnAddRemissionEntranceDetail
            formulario.Size = New Drawing.Size(1090, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.FixedAssetPurchaseOrderEquipment = _fixedAssetPurchaseOrderEquipment
            formulario.EditMode = True
            formulario.ListCompare = _listFixedAssetPurchaseOrderEquipment
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
        Dim FixedAssetPurchaseOrderItem = CType(INDGvReferralEntry.GetFocusedRow, FixedAssetPurchaseOrderItem)
        _listFixedAssetPurchaseOrderEquipment.Remove(FixedAssetPurchaseOrderItem)
        If FixedAssetPurchaseOrderItem.Id > 0 Then
            If _listFixedAssetPurchaseOrderEquipmentValidationDelete Is Nothing Then
                _listFixedAssetPurchaseOrderEquipmentValidationDelete = New List(Of FixedAssetPurchaseOrderItem)
            End If
            FixedAssetPurchaseOrderItem.MarkAsDeleted()
            _listFixedAssetPurchaseOrderEquipmentValidationDelete.Add(FixedAssetPurchaseOrderItem)
        End If

        INDGcReferralEntry.DataSource = Nothing
        INDGcReferralEntry.DataSource = _listFixedAssetPurchaseOrderEquipment
    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Async Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        If Me.CurrencyId > 0 Then
            Using modelCurrency As New MCurrency(MyTag)
                Dim currency As Currency = Await modelCurrency.GetCurrencyById(Me.CurrencyId)
                Dim round = currency.RoundingType
                Select Case round
                    Case 1
                        _roundingType = 0.01
                    Case 2
                        _roundingType = 0.1
                    Case 3
                        _roundingType = 1
                    Case 4
                        _roundingType = 10
                    Case 5
                        _roundingType = 100
                    Case 6
                        _roundingType = 1000
                End Select
            End Using
            ''se actualiza el formato de los campos numericos en el formuario
            Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat
            _culture.NumberFormat.CurrencyDecimalDigits = Utils.MaskByCurrencyRounding(_roundingType, _culture.NumberFormat)
            Me.changeNumericFormatByCurrency(_culture.NumberFormat)
            _ctrTmp.CodeISO4217 = _currencyAbbreviation
            _ctrTmp.CurrencyNumbertFormat = _culture.NumberFormat
            _ctrTmp.PrintInfo()
        End If
    End Sub

    ''' <summary>
    ''' Funcion que se encarga de consultar el TRM
    ''' </summary>
    ''' <param name="_currencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function GetTRM(_currencyId As Integer, ToCurrencyId As Integer) As Task(Of Boolean)
        Using Model As New MInventoryContract("")
            Dim Result = Await Model.GetTRMbyCurrencyIdAsync(ToCurrencyId, _currencyId)
            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If
            Me.TRM = Result.ObjectEmbbeded
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            Return True
        End Using
    End Function

    ''' <summary>
    ''' habilita o deshabilita el control de currency dependiendo si estan cargados o no detalles
    ''' </summary>
    Private Sub EnableOrDisableCurrecy()
        Me.INDsleCurrency.ReadOnly = If(ListPurchaseOrderAvailability?.Any(), True, False)
    End Sub

    ''' <summary>
    ''' funcion Valida y establece el evento cuando la moneda cambia 
    ''' </summary>
    ''' <param name="IsLoadControl"></param>
    ''' <returns></returns>
    Private Async Function ValidateCurrencyEditValue(IsLoadControl As Boolean, _currencyId As Integer?) As Task(Of ActionResult)
        If _currencyId Is Nothing OrElse _currencyId = 0 OrElse IsLoadControl OrElse {2, 3}.Contains(BarraBotones.StatusRecord) Then
            Return New ActionResult With {.StateResult = True}
        End If

        If Me.CurrencySelected IsNot Nothing Then
            Me.SetCurrencyUI(Me.CurrencyAbbreviation)
        End If

        If _currencyId = Me.indigo.OfficialCurrencyId Then
            Me.TRM = New TRM With {.CurrencyId = _currencyId, .OfficialCurrencyId = Me.indigo.OfficialCurrencyId, .Value = 1}
            Return New ActionResult With {.StateResult = True}
        End If

        Dim _stateResult = Await GetTRM(Me.indigo.OfficialCurrencyId, _currencyId)

        If _stateResult Then
            Return New ActionResult With {.StateResult = True}
        End If

        If Not _isLoading Then
            Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        End If
        Return New ActionResult With {.StateResult = False}
    End Function


    ''' <summary>
    ''' Desconfirma la orden de compra
    ''' </summary>
    Private Async Sub UnconfirmPurchaseOrder()
        Try
            Using Model As New MFixedAssetPurchaseOrder(CStr(Me.Tag))
                AsyncLoader(True)
                Dim Result = Await Model.UnconfirmPurchaseOrder(_purchaseOrder)
                If Result.StateResult Then
                    ReadOnlyControls(False)
                    Await LoadControls()
                End If
                Mensaje(EeventViewerImages.Informacion) = Result.Message
            End Using
        Catch ex As Exception
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _paymentsSettingPaymentsXpo = Nothing
        _presenter = Nothing
        _ctrTmp = Nothing
        _record = Nothing
        _purchaseOrder = Nothing
        _fixedAssetPurchaseOrderEquipment = Nothing
        _listFixedAssetPurchaseOrderEquipment = Nothing
        _listFixedAssetPurchaseOrderEquipmentValidationDelete = Nothing
        _supplierId = Nothing
        _listOrderType = Nothing
        _discountValue = Nothing
        _ivaValue = Nothing
        _totalValue = Nothing
        _netoValue = Nothing
        _varImp = Nothing
        _indexEditRecord = Nothing
        _isLoading = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReferralEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcReferralEntry, True)
        _idOperativeUnit = BarraBotones.OperatingUnitValue

        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PFixedAssetPurchaseOrder(Me)
        _presenter.LoadDefinitionLayout()
        Me.LoadPaymentsSetting()
        _presenter.GetSequense()
        Me.CurrencyId(Me.indigo?.CurrencyISO4217) = Me.indigo?.OfficialCurrencyId
        InitializeTuple()
        AddActionsColumns()
        Deshacer()
        LoadStatus()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetPurchaseOrder_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBteCode.Focus()
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Validación acciones editar/eliminar detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvReferralEntry_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvReferralEntry.PopupMenuShowing
        Dim row = DirectCast(INDGvReferralEntry.GetFocusedRow(), FixedAssetPurchaseOrderItem)
        Dim buttonEdit = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Edit)))
        Dim buttonRemove = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Remove)))
        If BarraBotones.ValidateVisibilityButton(EbuttonsWithoutPermission.ActualizarConfirmar) OrElse BarraBotones.ValidateVisibilityButton(EbuttonsWithoutPermission.Desconfirmar) Then
            If row IsNot Nothing Then
                If row.Quantity = row.CancelledQuantity Then
                    buttonEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Else
                    buttonEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    buttonRemove.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If row.OutstandingQuantity <> row.Quantity Then
                    buttonRemove.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            End If
        Else
            buttonEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            buttonRemove.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._purchaseOrder IsNot Nothing AndAlso Me._purchaseOrder.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmReferralEntry_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar disponibilidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddAvailability_Click(sender As Object, e As EventArgs) Handles INDbtnAddAvailability.Click
        If INDsleAvailability.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una disponibilidad"
            INDsleAvailability.Focus()
            Exit Sub
        End If

        If ListPurchaseOrderAvailability Is Nothing Then
            ListPurchaseOrderAvailability = New List(Of FixedAssetPurchaseOrderAvailability)
        End If

        If (From x In ListPurchaseOrderAvailability Where x.AvailabilityDetailId = INDsleAvailability.EditValue Select x).Count() > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La disponibilidad " + INDsleAvailability.Text + " ya existe en la lista"
            INDsleAvailability.Focus()
            Exit Sub
        End If

        Dim viewXpo As ViewListAvailabilityDetailXpo = DirectCast(INDsleAvailability.GetSelectedObject(), ViewListAvailabilityDetailXpo)
        If viewXpo Is Nothing Then
            viewXpo = _presenter.GetAvailabilityDetailById(INDsleAvailability.EditValue)
        End If

        Dim purchaseOrderAvailability = New FixedAssetPurchaseOrderAvailability()
        With purchaseOrderAvailability
            .AvailabilityDetailId = viewXpo.Id
            .AvailabilityCode = viewXpo.AvailabilityCode
            .CategoryCodeName = viewXpo.CategoryCodeName
            .FinancialSourceCodeName = viewXpo.FinancialSourceCodeName
            .RevenueTypeCodeName = viewXpo.RevenueTypeCodeName
            .Balance = viewXpo.Balance
            .Value = 0
        End With

        ListPurchaseOrderAvailability.Add(purchaseOrderAvailability)
        EnableBudgetInterface()
        INDgcAvailability.RefreshDataSource()

        Mensaje(EeventViewerImages.Informacion) = "Disponibilidad agregada correctamente"
        INDsleAvailability.EditValue = Nothing
        INDsleAvailability.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al realizar click sobre el botón de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Using formulario As New FrmPopupPurchaseOrderEquipment(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                            _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value),
                                                            _roundType:=_roundingType)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEquipmentRemissionEventArgs, AddressOf ReturnAddRemissionEntranceDetail
            formulario.Size = New Drawing.Size(1090, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MyTag = MyTag
            formulario.EditMode = False
            formulario.ListCompare = _listFixedAssetPurchaseOrderEquipment
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplierDistributionLine.QueryPopUp
        If INDSleSupplierDistributionLine.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If SupplierXPO Is Nothing Then
            Using model As New MEquipmentEntry(MyTag)
                SupplierXPO = model.ListSuppliersDistributionLines()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidades presupuestales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBudgetaryEntityId.QueryPopUp
        _presenter.InitializeBudgetaryEntity()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de disponibilidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAvailability_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAvailability.QueryPopUp
        If BudgetaryEntityId IsNot Nothing Then
            _presenter.InitializeAvailabilityDetails(BudgetaryEntityId)
        End If
    End Sub

    Private Sub INDSleRequestedFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleRequestedFunctionalUnit.QueryPopUp
        If RequestedFunctionalUnitXPO Is Nothing Then
            Me._presenter.InitializeFunctionalUnitXPO()
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If Me.CurrencyDatasource Is Nothing Then
            _presenter.InitializeCurrency()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleSupplierDistributionLine.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(558, Nothing, True)
            Using model As New MEquipmentEntry(MyTag)
                SupplierXPO = model.ListSuppliersDistributionLines()
            End Using
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de presupuestales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        DeleteAvailability()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewEquipmentEntry()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de la rejilla de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim entity As FixedAssetPurchaseOrderAvailability = INDviewAvailability.GetFocusedRow()
            If CDec(e.NewValue) > CDec(entity.Balance) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor a ejecutar no puede ser mayor al saldo de la disponibilidad"
                e.Cancel = True
                Exit Sub
            End If
            entity.Value = e.NewValue
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplierDistributionLine_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplierDistributionLine.EditValueChanged
        HideButtonImportInformation()
        If SupplierDistributionLineId IsNot Nothing Then
            Dim supplier = _presenter.GetSupplierBySupplierDistributionLineId(SupplierDistributionLineId)
            DeadlineDays = supplier.IdSupplier.TimeLimitDays
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha de orden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDDteDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteDate.EditValueChanged
        If PurchaseOrderDate IsNot Nothing Then
            INDdteDeliverDate.Properties.MinValue = PurchaseOrderDate
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de orden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleOrderType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleOrderType.EditValueChanged
        HideButtonImportInformation()

        If INDSleOrderType.EditValue Is Nothing Then
            Exit Sub
        End If
        ' si se selecciona la opción otro se habilita cual tipo de orden
        If INDSleOrderType.EditValue = 3 Then
            INDLciOtherTypeOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciOtherTypeOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryEntityId.EditValueChanged
        CleanBudgetInterface(1)

        If BudgetaryEntityId IsNot Nothing Then
            _presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryValidityId.EditValueChanged
        CleanBudgetInterface(2)

        If BudgetaryValidityId IsNot Nothing Then
            _presenter.InitializeAvailabilityDetails(BudgetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        Await Me.ValidateCurrencyEditValue((Me._isLoading), Me.CurrencyId)

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
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
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
        _varImp = 1
        _purchaseOrder.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _purchaseOrder.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            LoadPaymentsSetting()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _purchaseOrder.Status = 3
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _purchaseOrder.Id, 0, _purchaseOrder.Id, _idOperativeUnit)
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        _purchaseOrder.Status = 2
        _varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        _purchaseOrder.Status = 2
        _varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta al dar clic en el boton de confirmar
    ''' </summary>
    Private Sub BarraBotones_Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        UnconfirmPurchaseOrder()
    End Sub

#End Region

#Region "ImportarInformacion"

    ''' <summary>
    ''' Evento que dispara el formulario de importar activos fijos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New PopUpFixedAssetPurchaseOrderImport
            AddHandler formulario.GetListFixedAssetPurchaseOrderItem, AddressOf ReturnGetListPurchaseOrderDetailAsync
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

            If _listFixedAssetPurchaseOrderEquipment Is Nothing Then
                _listFixedAssetPurchaseOrderEquipment = New List(Of FixedAssetPurchaseOrderItem)
            End If
            formulario.ListFixedAssetPurchaseOrderItemValidation = _listFixedAssetPurchaseOrderEquipment.ToList()
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que nos permite modificar el ultimo costo dependiendo de la moneda que se parametrice en la orden de compra
    ''' </summary>
    ''' <param name="fixedAsset"></param>
    ''' <param name="physicalAsset"></param>
    ''' <param name="currencyId"></param>
    ''' <param name="stateResult"></param>
    Private Sub SetLastCostItem(fixedAsset As FixedAssetEquipmentXpo, physicalAsset As FixedAssetPhysicalAssetXpo, currencyId As Integer, stateResult As Boolean)
        If physicalAsset.FixedAssetPhysicalAssetDetailBookXpo?.Any() Then
            fixedAsset.LastCostItem = physicalAsset.FixedAssetPhysicalAssetDetailBookXpo _
            .Where(Function(book) book.LegalBookId IsNot Nothing AndAlso
                                  book.LegalBookId.OfficialCurrencyId.Id = currencyId) _
            .Select(Function(book) book.HistoricalValue).FirstOrDefault()
        ElseIf stateResult Then
            fixedAsset.LastCostItem = Math.Round(fixedAsset.LastCostItem / TRM.Value, 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que nos permite obtener el ultimo ingreso de activo al articulo seleccionado
    ''' </summary>
    ''' <param name="fixedAsset"></param>
    ''' <returns></returns>
    Private Function GetLatestPhysicalAsset(fixedAsset As FixedAssetEquipmentXpo) As FixedAssetPhysicalAssetXpo
        Return fixedAsset.FixedAssetPhysicalAssetXpo?.LastOrDefault()
    End Function

    ''' <summary>
    ''' Método que retorna los registros importados al formulario de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnGetListPurchaseOrderDetailAsync(sender As Object, e As AddEquipmentPurchaseOrderEventArgs)

        Dim errors As New StringBuilder
        Dim dictionaryFixedAsset As Dictionary(Of Integer, FixedAssetEquipmentXpo) = New Dictionary(Of Integer, FixedAssetEquipmentXpo)()
        Dim FixedAsset As FixedAssetEquipmentXpo
        Dim ListFixedAssetItemWithLastCost As New List(Of FixedAssetEquipmentXpo)
        Dim ListFixedAssetItemWithNotLastCost As New List(Of FixedAssetEquipmentXpo)

        'Obtencion del estado de conversión de moneda
        Dim _stateResult = Await GetTRM(_fixedAssetSettingsXpo.CurrencyId.Id, CurrencyId)

        For Each Item In e.ListFixedAssetPurchaseOrderEquipment
            ''Dim PhysicalAsset As FixedAssetPhysicalAssetXpo = Nothing
            FixedAsset = New FixedAssetEquipmentXpo(New Infrastructure.CrossCutting.Xpo.Base.IndigoXPOSession(Of FixedAssetEquipmentXpo))
            If Not dictionaryFixedAsset.ContainsKey(Item.ItemId) Then
                Dim FixedAssetTmp = _presenter.GetItemById(Item.ItemId)
                dictionaryFixedAsset.Add(Item.ItemId, FixedAssetTmp)
            End If

            FixedAsset = dictionaryFixedAsset(Item.ItemId)
            Dim physicalAsset = GetLatestPhysicalAsset(FixedAsset)

            'Buscamos si maneja activo fijo se dara la opcion de editar el ultimo costo, y si no lo añadiremos a la lista que se cargara a la regilla
            If PhysicalAsset Is Nothing Then
                ListFixedAssetItemWithNotLastCost.Add(FixedAsset)
            Else
                SetLastCostItem(FixedAsset, physicalAsset, CurrencyId, _stateResult)
                ListFixedAssetItemWithLastCost.Add(FixedAsset)
            End If

            Item.OutstandingQuantity = Item.Quantity
            Item.CancelledQuantity = 0
            Item.UnitValue = FixedAsset.LastCostItem
            Item.IvaPercentage = 0
            If FixedAsset.IVAId IsNot Nothing AndAlso FixedAsset.IVAId.Id > 0 Then
                Item.IVAId = FixedAsset.IVAId.Id
                Item.IvaPercentage = FixedAsset.IVAId.Percentage
            End If
            Item.SubTotalValue = Utils.RoundValue(Item.UnitValue * Item.Quantity, 1)
            Item.DiscountValue = Utils.RoundValue(Item.SubTotalValue * Item.DiscountPercentage / 100, 1)
            Item.IvaValue = Utils.RoundValue(CDec((Item.SubTotalValue - Item.DiscountValue) * (Item.IvaPercentage / 100)), 1)
            Item.TotalValue = Item.SubTotalValue + Item.IvaValue - Item.DiscountValue
        Next

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
        End If

        'agregamos los articulos que no manejen lote a la rejilla directamente
        If ListFixedAssetItemWithLastCost IsNot Nothing AndAlso ListFixedAssetItemWithLastCost.Any() Then
            Dim itemsToAdd = e.ListFixedAssetPurchaseOrderEquipment.Where(Function(order) _
                     ListFixedAssetItemWithLastCost.Any(Function(item) item.Id = order.ItemId)
                     ).ToList()
            _listFixedAssetPurchaseOrderEquipment.AddRange(itemsToAdd)
            RefreshValues(False)
            INDGcReferralEntry.DataSource = Nothing
            INDGcReferralEntry.DataSource = _listFixedAssetPurchaseOrderEquipment
        End If

        'abrimos el PopUp de agregar articulo para que se pueda editar el valor unitario
        If ListFixedAssetItemWithNotLastCost IsNot Nothing AndAlso ListFixedAssetItemWithNotLastCost.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Hay artículos con valor unitario en cero."
            Dim itemsToAdd = e.ListFixedAssetPurchaseOrderEquipment.Where(Function(order) _
                     ListFixedAssetItemWithNotLastCost.Any(Function(item) item.Id = order.ItemId)
                     ).ToList()
            Using formulario As New FrmPopupPurchaseOrderEquipment(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                            _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value),
                                                            _roundType:=_roundingType)
                Me.Cursor = ChangeCursorIndigo()
                AddHandler formulario.AddEquipmentRemissionEventArgs, AddressOf ReturnAddRemissionEntranceDetail
                formulario.Size = New Drawing.Size(1090, 750)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.ListFixedAssetPurchaseOrderEquipment = itemsToAdd
                formulario.ImportDataMode = True
                formulario.ListCompare = _listFixedAssetPurchaseOrderEquipment
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub


#End Region

End Class
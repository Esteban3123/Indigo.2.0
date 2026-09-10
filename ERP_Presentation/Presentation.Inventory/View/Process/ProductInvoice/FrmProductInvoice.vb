'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 09-09-2015
'
' Last Modified By : Diego Andrés Roldán Lozano
' Last Modified On : 2017-11-01
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports DevExpress.Data.Linq
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Microsoft.Win32
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Billing
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmProductInvoice
    Implements IProductInvoice, ICustomizableForm

#Region "BUILDER"
    ''' <summary>
    ''' Se inicializa una isntancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ctrTmp = New CtrTotalInvoiceEntranceVoucher()
        InitializeComponent()
        ctrTmp.SetInfoFunction(AddressOf getInfoProductInvoice)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        IndigoGridControl1.SetHideNoRecords(INDGcProducts, True)
        INDEsbDetails.AddRangeColumns("Código Producto", "Código Lote", "Fecha", "Cantidad", "Precio Unitario Venta", "% Descuento")
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoProductInvoice() As Tuple(Of String, String, String, String)
        If listDocumentInvoiceProductSalesDetail IsNot Nothing AndAlso listDocumentInvoiceProductSalesDetail.Count > 0 Then
            For Each item In listDocumentInvoiceProductSalesDetail
                _subtotal = listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.SubTotalValue)
                _discountValue = listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.DiscountValue)
                _ivaValue = listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.IvaValue)
                _total = DocumentInvoiceProductSales.TotalValue ' listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.TotalValue) + 
                Return New Tuple(Of String, String, String, String)(_subtotal, _discountValue, _ivaValue, _total)
            Next
        Else
            Return New Tuple(Of String, String, String, String)(0, 0, 0, 0)
        End If
        Return Nothing
    End Function
#End Region

#Region "GLOBALS"
    Dim _subtotal As Decimal

    Dim _discountValue As Decimal

    Dim _ivaValue As Decimal

    Dim _icaPercentage As Decimal

    Dim _total As Decimal

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrTotalInvoiceEntranceVoucher

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As BillingSequence

    ''' <summary>
    ''' presenter de ordenes de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private presenter As PProductInvoice

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
    ''' Tercero seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Private ThirdPartySelected As ThirdParty

    ''' <summary>
    ''' representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private DocumentInvoiceProductSales As DocumentInvoiceProductSales

    ''' <summary>
    ''' entidad del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private DocumentInvoiceProductSalesDetail As DocumentInvoiceProductSalesDetail

    ''' <summary>
    ''' listado del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private listDocumentInvoiceProductSalesDetail As List(Of DocumentInvoiceProductSalesDetail)

    ''' <summary>
    ''' listado del detalle para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private listDocumentInvoiceProductSalesDetailDelete As List(Of DocumentInvoiceProductSalesDetail)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Private indexEditRecord As Integer

    ''' <summary>
    ''' bandera para limpiar los controles solo cuando se consulta ubn registro
    ''' </summary>
    ''' <remarks></remarks>
    Private cleanControlsLoadControls As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private varImp As Integer

    ''' <summary>
    ''' Permite saber si se ejecuta las acciones dentro del editValueChanged
    ''' </summary>
    Private IsLoad As Boolean = False

    ''' <summary>
    ''' bandera para identificar si el proceso se ejecuta como ya se estaba haciendo o trae las reglas del contra de centro de atencion externo del cliente
    ''' </summary>
    Private ExternalCenterMode As Boolean = False

    ''' <summary>
    ''' Parametros de inventario
    ''' </summary>
    Private parameterInventory As SettingInventory

    ''' <summary>
    ''' Parametros de facturacion
    ''' </summary>
    Private parameterBilling As SettingsBilling

    ''' <summary>
    ''' Variable que almacena la base de la retencion de iva
    ''' </summary>
    ''' <remarks></remarks>
    Private WithholdingIvaBase As Decimal

    ''' <summary>
    ''' Variable que almacena el porcentaje de la retencion de iva
    ''' </summary>
    ''' <remarks></remarks>
    Private WithholdingIvaPercentage As Decimal

    Private _currentCompany As ThirdParty

    ''' <summary>
    ''' Indica si en los parametros de la compañia se maneja la actividad economica
    ''' </summary>
    Private TransactionEconomicActivity As Boolean = False

#End Region

#Region "PROPERTIES"

    Public Property FunctionalUnitId As Integer Implements IProductInvoice.FunctionalUnitId
        Get
            Return INDsleFunctionalUnitFirst.EditValue
        End Get
        Set(value As Integer)
            INDsleFunctionalUnitFirst.EditValue = value
        End Set
    End Property

    Public Property RetentionConceptBranchTask As Task(Of RetentionConcepts) Implements IProductInvoice.RetentionConceptBranchTask

    Property BillingAuthorizationXpo As List(Of BillingAuthorizationXpo)
        Get
            Return CType(INDSleBillingAuthorization.Properties.DataSource, List(Of BillingAuthorizationXpo))
        End Get
        Set(value As List(Of BillingAuthorizationXpo))
            INDSleBillingAuthorization.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProductInvoice.ActionsOnControls
        Set(value As Boolean)
            INDLcProductInvoice.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDSleBillingAuthorization.Enabled = value
            INDsleFunctionalUnitFirst.Enabled = value
            INDDteDocumentDate.Enabled = value
            INDSleCustomer.Enabled = value
            INDSleWarehouse.Enabled = value
            INDSleConditionSales.Enabled = value
            INDSleEconomicActivity.Enabled = value
            INDMeDetail.Enabled = value
            INDsleBranchOffice.Enabled = value

            If ExternalCenterMode AndAlso ContractExternalClientsId IsNot Nothing Then
                INDGleModalidadVenta.Enabled = Not value
            Else
                INDGleModalidadVenta.Enabled = value
            End If

            INDEsbDetails.Enabled = value

            If value Then
                If DocumentInvoiceProductSales IsNot Nothing AndAlso DocumentInvoiceProductSales.Status > 1 _
                    OrElse (listDocumentInvoiceProductSalesDetail IsNot Nothing AndAlso listDocumentInvoiceProductSalesDetail.Any(Function(x) x.ImportSource IsNot Nothing AndAlso x.ImportSource = 1)) Then
                    INDBtnAddProducts.Enabled = False
                Else
                    If INDSleWarehouse.EditValue IsNot Nothing Then
                        INDBtnAddProducts.Enabled = value
                    End If
                End If
            Else
                INDBtnAddProducts.Enabled = False
            End If

            ' Información Monetaria
            INDTxtValue.Enabled = value
            INDTxtDiscountValue.Enabled = value
            INDTxtValueTax.Enabled = value
            INDTxtWithholdingTax.Enabled = value
            INDTxtWithholdingICA.Enabled = value
            INDTxtRetentionSource.Enabled = value
            INDTxtRetentionOther.Enabled = value
            INDTxtDeductionOther.Enabled = value
            INDTxtTotal.Enabled = value

            INDGcProducts.Enabled = value
            INDLcProductInvoice.EndUpdate()
            If value Then
                INDSleBillingAuthorization.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Codigo de la orden de servicio
    ''' </summary>
    Public Property Code As String Implements IProductInvoice.Code
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
    ''' fecha del documneto
    ''' </summary>
    Public Property DocumentDate As Date Implements IProductInvoice.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IProductInvoice.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IProductInvoice.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' observaciones
    ''' </summary>
    Public Property Observations As String Implements IProductInvoice.Observations
        Get
            Return INDMeDetail.EditValue
        End Get
        Set(value As String)
            INDMeDetail.EditValue = value
        End Set
    End Property

    Public Property ConditionSalesId As Integer? Implements IProductInvoice.ConditionSalesId
        Get
            Return INDSleConditionSales.EditValue
        End Get
        Set(value As Integer?)
            INDSleConditionSales.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivityId As Integer?
        Get
            Return INDSleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDSleEconomicActivity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Contrato de centro de atención externa
    ''' </summary>
    Public Property ContractExternalClientsId As Integer? Implements IProductInvoice.ContractExternalClientsId
        Get
            Return INDSleContractExternalClient.EditValue
        End Get
        Set(value As Integer?)
            INDSleContractExternalClient.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequence As BillingSequence Implements IProductInvoice.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' id del tercero
    ''' </summary>
    Public Property ThirdPartyId As Integer Implements IProductInvoice.ThirdPartyId
        Get
            Return INDSleCustomer.EditValue
        End Get
        Set(value As Integer)
            INDSleCustomer.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    Public Property WareHouseId As Integer Implements IProductInvoice.WareHouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CustomerXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleCustomer.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCustomer.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' datasource de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property WarehouseXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    Property FunctionalUnitXpoFirst As XPInstantFeedbackSource
        Get
            Return CType(INDsleFunctionalUnitFirst.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFunctionalUnitFirst.Properties.DataSource = value
        End Set
    End Property

    Property BillingAuthorizationId As Integer Implements IProductInvoice.BillingAuthorizationId
        Get
            Return INDSleBillingAuthorization.EditValue
        End Get
        Set(value As Integer)
            INDSleBillingAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la sucursal
    ''' </summary>
    ''' <returns></returns>
    Public Property BranchOfficeId As Integer? Implements IProductInvoice.BranchOfficeId
        Get
            Return INDsleBranchOffice.EditValue
        End Get
        Set(value As Integer?)
            INDsleBranchOffice.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la sucursal
    ''' </summary>
    ''' <returns></returns>
    Public Property BranchOfficeXpo As LinqInstantFeedbackSource Implements IProductInvoice.BranchOfficeXpo
        Get
            Return INDsleBranchOffice.Properties.DataSource
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleBranchOffice.Properties.DataSource = value
        End Set
    End Property

    Public ReadOnly Property RequiresConditionsSale As Boolean
        Get
            Dim _requiresConditionsSale As Boolean

            If parameterBilling IsNot Nothing Then
                _requiresConditionsSale = parameterBilling.requiresConditionsSale
            End If

            Return _requiresConditionsSale
        End Get
    End Property

    Public Property Value As Decimal
        Get
            Return INDTxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValue.EditValue = value
        End Set
    End Property

    Public Property ValueTax As Decimal
        Get
            Return INDTxtValueTax.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValueTax.EditValue = value
        End Set
    End Property

    Public Property ValueDiscount As Decimal
        Get
            Return INDTxtDiscountValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtDiscountValue.EditValue = value

        End Set
    End Property

    Public Property RetentionSource As Decimal
        Get
            Return INDTxtRetentionSource.EditValue
        End Get
        Set(value As Decimal)
            INDTxtRetentionSource.EditValue = value
        End Set
    End Property

    Public Property WithholdingICA As Decimal
        Get
            Return INDTxtWithholdingICA.EditValue
        End Get
        Set(value As Decimal)
            INDTxtWithholdingICA.EditValue = value
        End Set
    End Property

    Public Property WithholdingTax As Decimal
        Get
            Return INDTxtWithholdingTax.EditValue
        End Get
        Set(value As Decimal)
            INDTxtWithholdingTax.EditValue = value
        End Set
    End Property

    Public Property DistrictTax As Decimal

    Public Property RetentionOther As Decimal
        Get
            Return INDTxtRetentionOther.EditValue
        End Get
        Set(value As Decimal)
            INDTxtRetentionOther.EditValue = value
        End Set
    End Property

    Public Property DeductionOther As Decimal
        Get
            Return INDTxtDeductionOther.EditValue
        End Get
        Set(value As Decimal)
            INDTxtDeductionOther.EditValue = value
        End Set
    End Property

    Public Property NetoValue As Decimal

    Public Property InvoiceValue As Decimal

    Public Property TotalValue As Decimal
        Get
            Return INDTxtTotal.EditValue
        End Get
        Set(value As Decimal)
            INDTxtTotal.EditValue = value
        End Set
    End Property

    Private Property _freightIVAPercentage As Decimal
    Public Property FreightIVAPercentage As Decimal
        Get
            Return _freightIVAPercentage
        End Get
        Set(value As Decimal)
            _freightIVAPercentage = value
        End Set
    End Property

    Public Property RetentionConceptThirdTask As Task(Of RetentionConcepts) Implements IProductInvoice.RetentionConceptThirdTask


#End Region

#Region "CRUD"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If DocumentInvoiceProductSales IsNot Nothing AndAlso DocumentInvoiceProductSales.Status <> 2 Then
                If DocumentInvoiceProductSales IsNot Nothing AndAlso DocumentInvoiceProductSales.Status < 3 Then
                    If ValidateControls() = True Then
                        If INDGvProducts.RowCount = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar un detalle"
                            Exit Sub
                        End If

                        If INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            If ThirdPartySelected.ElectronicBiller And EconomicActivityId Is Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Por favor asocie una Actividad Económica al Cliente"
                                Exit Sub
                            End If
                        End If
                    Else
                        Exit Sub
                    End If

                    AssigningValues()
                End If
            End If

            Using model As New MProductInvoice(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveDocumentInvoiceProductSales(DocumentInvoiceProductSales, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    DocumentInvoiceProductSales = result.ObjectEmbbeded
                    If DocumentInvoiceProductSales.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If DocumentInvoiceProductSales.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    'Mensaje de anulado con descripcion de comprobante contable y documentos generados
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, DocumentInvoiceProductSales.Id, 0, DocumentInvoiceProductSales.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, DocumentInvoiceProductSales.Id, 0, DocumentInvoiceProductSales.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, DocumentInvoiceProductSales.Id, 0, DocumentInvoiceProductSales.Id)
                    End Select
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    If result.StateResult = False And result.StateResultAux = False Then
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult.First().ToString() <> "" Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = result.Message
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que se ejecuta en la accion de reversar
    ''' </summary>
    Public Async Sub Reversar()
        Try
            'Abrimos el modal que pide la razon de anulacion
            Dim reversalReasonId As Integer = 0
            Dim reversalDescription As String = String.Empty
            AsyncLoader(True)
            Using PopUpAnnulmentReason As New PopUpAnnulmentReason()
                Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
                If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                    AsyncLoader(False)
                    Exit Sub
                Else
                    reversalReasonId = PopUpAnnulmentReason.ReversalReasonId
                    reversalDescription = PopUpAnnulmentReason.ReversalDescription
                End If
            End Using
            If listDocumentInvoiceProductSalesDetail IsNot Nothing Then
                For Each item In listDocumentInvoiceProductSalesDetail
                    DocumentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Add(item)
                Next
            End If
            Using model As New MProductInvoice(MyTag)
                Dim result As ActionResult = Await model.ReverseDocumentInvoiceProductSales(DocumentInvoiceProductSales, reversalReasonId, reversalDescription)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Me.BarraBotones.PrintReport(PrintReportAction.Cancel, DocumentInvoiceProductSales.Id, 0, DocumentInvoiceProductSales.Id)

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewProductInvoice()
        End If
    End Sub

    Private Async Sub SaveOrUpdateAndConfirm(actions As Integer)
        Try
            If ValidateControls() Then
                If INDGvProducts.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddRemissionDetail", MODULE_NAME)
                    Exit Sub
                End If

                If INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If ThirdPartySelected.ElectronicBiller And EconomicActivityId Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = "Por favor asocie una Actividad Económica al Cliente"
                        Exit Sub
                    End If
                End If
            Else
                Exit Sub
            End If

            AssigningValues()

            AsyncLoader(True)
            Dim cashReceipts As CashReceipts = Nothing
            Dim requiredCashReceip As Boolean = True
            'Verificamos si la modalidad de venta es contado o crédito y además que no sea facturación básica para saber si se debe pedir el recibo de caja
            If CByte(INDGleModalidadVenta.EditValue) = 2 AndAlso parameterBilling IsNot Nothing AndAlso parameterBilling.Id > 0 AndAlso Not parameterBilling.ApplyBasicBilling Then
                If MessageIndigo.Show("¿Desea registrar Anticipo?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    requiredCashReceip = False
                End If
            ElseIf CByte(INDGleModalidadVenta.EditValue) = 2 Then
                requiredCashReceip = False
            End If

            If requiredCashReceip Then
                'abro el popup de recibos de caja para que se cree 
                Using frm As New FrmCashReceivableLiquidation
                    frm.IdThirdParty = ThirdPartyId
                    frm.SourceDocument = eSourceDocument.ProductSales
                    frm.CashDefaultValue = Utils.RoundValue(CDec(DocumentInvoiceProductSales.TotalValue), Utils.RoundLevel.Unit)
                    frm.ValueProductInvoice = Utils.RoundValue(CDec(DocumentInvoiceProductSales.TotalValue), Utils.RoundLevel.Unit)
                    frm.CurrencyInvoiceId = indigo.OfficialCurrencyId
                    Dim transparent As New FrmTransparent(frm, False)
                    If transparent.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                        DocumentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Clear()
                        Mensaje(EeventViewerImages.Advertencia) = "Se debe crear un recibo de caja para continuar con el proceso"
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    cashReceipts = frm.CashReceiptProductInvoice
                End Using
            End If

            Using model As New MProductInvoice(MyTag)
                Dim result = Await model.SaveAndConfirmDocumentInvoiceProductSales(DocumentInvoiceProductSales, cashReceipts, _idCurrentSequence, actions, Me._sequence)

                If result.StateResult = True And result.StateResultAux = True Then
                    DocumentInvoiceProductSales = result.ObjectEmbbeded
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        If DicSequense.Count > 0 Then
                            Me.DicSequense.Remove(_idCurrentSequence)
                        End If
                    End If
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, DocumentInvoiceProductSales.Id, 0, DocumentInvoiceProductSales.Id)
                    AsyncLoader(False)
                    Me.Deshacer()

                ElseIf result.StateResult = True And result.StateResultAux = False Then

                    'Se descarta la secuencia numerica usada
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        If DicSequense.Count > 0 Then
                            Me.DicSequense.Remove(_idCurrentSequence)
                        End If
                    End If

                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, DocumentInvoiceProductSales.Id, 0, DocumentInvoiceProductSales.Id)
                    AsyncLoader(False)
                    Me.Deshacer()

                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    AsyncLoader(False)
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult.First().ToString() <> "" Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If DocumentInvoiceProductSales.Id > 0 Then
                        DocumentInvoiceProductSales = Await model.GetDocumentInvoiceProductSalesByCode(Code)
                        listDocumentInvoiceProductSalesDetail = model.GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(DocumentInvoiceProductSales.Id)
                        listDocumentInvoiceProductSalesDetailDelete = New List(Of DocumentInvoiceProductSalesDetail)
                    Else
                        DocumentInvoiceProductSales = New DocumentInvoiceProductSales
                        listDocumentInvoiceProductSalesDetail = New List(Of DocumentInvoiceProductSalesDetail)
                        listDocumentInvoiceProductSalesDetailDelete = New List(Of DocumentInvoiceProductSalesDetail)
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub
#End Region

#Region "METHODS"

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Factura", .FieldName = "InvoiceId.InvoiceNumber", .ColumnWidth = 100, .ColumnAligment = DevExpress.Utils.HorzAlignment.Center},
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "ThirdPartyId.NitName", .ColumnWidth = 300},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDocumentInvoiceProductSales
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
    ''' limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDLcProductInvoice.BeginUpdate()
        ReadOnlyControls(False)
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Code = String.Empty
        DocumentDate = GetDateServer()

        If cleanControlsLoadControls Then
            INDSleBillingAuthorization.EditValue = Nothing
            INDSleBillingAuthorization.Properties.NullText = String.Empty
            INDsleFunctionalUnitFirst.EditValue = Nothing
            INDsleFunctionalUnitFirst.Properties.NullText = String.Empty
            INDSleWarehouse.EditValue = Nothing
            INDSleWarehouse.Properties.NullText = String.Empty
            INDSleWarehouse.Properties.ReadOnly = False
        End If

        INDlyItemBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemBranchOffice.AllowHide = True
        BranchOfficeId = Nothing
        INDsleBranchOffice.Properties.NullText = String.Empty
        BranchOfficeXpo = Nothing

        INDLciConditionSalesVisibility(RequiresConditionsSale)
        ConditionSalesId = Nothing
        INDSleConditionSales.Properties.NullText = String.Empty

        EconomicActivityId = Nothing
        INDSleEconomicActivity.Properties.NullText = String.Empty

        INDSleCustomer.EditValue = Nothing
        INDSleCustomer.Properties.NullText = String.Empty
        INDSleCustomer.Properties.ReadOnly = False
        INDGleModalidadVenta.EditValue = Nothing
        INDTxtValue.EditValue = 0
        INDTxtDiscountValue.EditValue = 0
        INDTxtValueTax.EditValue = 0
        INDTxtWithholdingTax.EditValue = 0
        INDTxtWithholdingICA.EditValue = 0
        INDTxtRetentionSource.EditValue = 0
        INDTxtRetentionOther.EditValue = 0
        INDTxtDeductionOther.EditValue = 0
        INDTxtTotal.EditValue = 0
        RetentionConceptBranchTask = Nothing
        INDBtnImportFile.Enabled = False

        ReadOnlyMonetaryInfo()

        _icaPercentage = 0
        ThirdPartySelected = Nothing

        Observations = String.Empty
        cleanControlsLoadControls = False
        listDocumentInvoiceProductSalesDetail = Nothing
        listDocumentInvoiceProductSalesDetailDelete = Nothing
        INDGcProducts.DataSource = Nothing
        INDSleContractExternalClient.EditValue = Nothing
        INDSleContractExternalClient.Properties.DataSource = Nothing
        INDLycContractExternalClient.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ExternalCenterMode = False
        IndigoGridControl1.RefreshGrid(INDGcProducts)
        ctrTmp.PrintInfo()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        ShowOrHideColumnRemissionOutput(False)
        ActionsOnControls = False
        INDLcProductInvoice.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' metodo para ocultar o mostrar columna en la gridview 
    ''' </summary>
    Private Sub ShowOrHideColumnRemissionOutput(ImportSource As Boolean)
        If ImportSource Then
            INDColRemissionOutput.VisibleIndex = 0
        Else
            INDColRemissionOutput.Visible = ImportSource
        End If
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            Try
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If
                Using model As New MProductInvoice(MyTag)
                    AsyncLoader(True)
                    INDLcProductInvoice.BeginUpdate()

                    DocumentInvoiceProductSales = Await model.GetDocumentInvoiceProductSalesByCode(Code)
                    If DocumentInvoiceProductSales IsNot Nothing AndAlso DocumentInvoiceProductSales.Id > 0 Then
                        listDocumentInvoiceProductSalesDetail = model.GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(DocumentInvoiceProductSales.Id)
                        INDGcProducts.DataSource = listDocumentInvoiceProductSalesDetail

                        If listDocumentInvoiceProductSalesDetail.Count > 0 AndAlso listDocumentInvoiceProductSalesDetail.Any(Function(x) x.ImportSource IsNot Nothing AndAlso x.ImportSource = 1) Then
                            INDBtnAddProducts.Enabled = False
                            ShowHidePermissionImportButton()
                            ShowOrHideColumnRemissionOutput(True)
                        Else
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                        End If

                        cleanControlsLoadControls = True
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(DocumentInvoiceProductSales.Id))

                            With DocumentInvoiceProductSales
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.StatusRecordVisible = True
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                presenter.GetIVARetentionConceptByThirdPartyId(.ThirdPartyId)
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case 2
                                        If parameterBilling IsNot Nothing AndAlso parameterBilling.Id > 0 AndAlso parameterBilling.ApplyBasicBilling Then
                                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAnnular)
                                        Else
                                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        End If
                                        ReadOnlyControls(True)
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                End Select

                                Code = .Code
                                BillingAuthorizationId = .BillingAuthorizationId
                                INDSleBillingAuthorization.Properties.NullText = .NameBillingAuthorization
                                FunctionalUnitId = .FunctionalUnitId
                                INDsleFunctionalUnitFirst.Properties.NullText = .CodeNameFunctionalUnit
                                DocumentDate = .DocumentDate

                                If .ContractExternalClientsId IsNot Nothing Then
                                    HideOrShowContractExternalField(True)
                                    ContractExternalClientsId = .ContractExternalClientsId
                                    INDSleContractExternalClient.Properties.NullText = .ContractExternalClients.ContractNumber
                                    INDSleContractExternalClient.ReadOnly = True
                                End If

                                If .ConditionSalesId IsNot Nothing Then
                                    ConditionSalesId = .ConditionSalesId
                                    INDSleConditionSales.Properties.NullText = .CodeNameConditionSales

                                    If .Status <> 1 Then
                                        INDLciConditionSalesVisibility(True)
                                    End If
                                End If

                                If .EconomicActivityId IsNot Nothing Then
                                    EconomicActivityId = .EconomicActivityId
                                    INDSleEconomicActivity.Properties.NullText = .CodeNameEconomicActivity

                                    If .Status <> 1 Then
                                        INDLciConditionSalesVisibility(True)
                                    End If
                                End If

                                IsLoad = True

                                ThirdPartyId = .ThirdPartyId
                                INDSleCustomer.Properties.NullText = .NitNameThirdParty
                                BranchOfficeId = .BranchOfficeId
                                INDsleBranchOffice.Properties.NullText = .CodeNameBranchOffice

                                If .BranchOfficeId IsNot Nothing Then
                                    INDlyItemBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                    INDlyItemBranchOffice.AllowHide = False
                                    presenter.InitializeBranchOfficeByThirdPartyId(ThirdPartyId)
                                End If

                                IsLoad = False

                                WareHouseId = .WarehouseId
                                INDSleWarehouse.Properties.NullText = .CodeNameWareHouse
                                INDSleWarehouse.Properties.ReadOnly = True
                                Observations = .Description
                                BarraBotones.StatusRecord = .Status.ToString()
                                INDGleModalidadVenta.EditValue = .SaleModality
                                Value = .Value
                                ValueDiscount = .ValueDiscount
                                ValueTax = .ValueTax
                                WithholdingTax = .WithholdingTax
                                WithholdingICA = .WithholdingICA
                                _icaPercentage = .IcaPercentage
                                RetentionSource = .RetentionSource
                                DistrictTax = .DistrictTax
                                TotalValue = .TotalValue
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.DocumentInvoiceProductSales.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                        New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                            .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = DocumentInvoiceProductSales.Id})
                                        ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(DocumentInvoiceProductSales.Id, Me.Tag.ToString(), Nothing, GetType(DocumentInvoiceProductSales).Name)

                            AsyncLoader(False)
                            ctrTmp.PrintInfo()
                            ActionsOnControls = True
                            If DocumentInvoiceProductSales.Status = 1 AndAlso Not listDocumentInvoiceProductSalesDetail.Any(Function(x) x.ImportSource IsNot Nothing AndAlso x.ImportSource = 1) Then
                                INDBtnAddProducts.Enabled = True
                            End If
                            INDSleBillingAuthorization.Focus()
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, DocumentInvoiceProductSales.Id, 0, DocumentInvoiceProductSales.Id)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewProductInvoice()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcProductInvoice.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Async Function NewProductInvoice() As Task

        If _currentCompany Is Nothing Then 'Si la empresa actual no existe como tercero, se debe crear
            Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el tercero de la empresa (" & Me.indigo.IndigoCompanyNit & ") actualmente seleccionada. Verifique que haya sido creada e intente de nuevo."
            Exit Function
        End If

        INDLciConditionSalesVisibility(RequiresConditionsSale)
        Me.DocumentInvoiceProductSales = New DocumentInvoiceProductSales
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.BillingSequenceDetail Is Nothing OrElse Me._sequence.BillingSequenceDetail.Count = 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el detalle de la Secuencia Numerica."
                Exit Function
            End If

            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.BillingSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
    End Function

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

    Private Sub ReturnAddDocumentInvoiceProductSalesDetail(sender As Object, e As AddDocumentInvoiceProductSalesDetailEventArgs)
        If listDocumentInvoiceProductSalesDetail Is Nothing OrElse listDocumentInvoiceProductSalesDetail.Count = 0 Then
            INDSleWarehouse.Properties.ReadOnly = True
            listDocumentInvoiceProductSalesDetail = New List(Of DocumentInvoiceProductSalesDetail)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
            ShowOrHideColumnRemissionOutput(False)
        End If
        If e.EditMode = True Then
            listDocumentInvoiceProductSalesDetail.Remove(DocumentInvoiceProductSalesDetail)
            listDocumentInvoiceProductSalesDetail.Insert(indexEditRecord, e.DocumentInvoiceProductSalesDetail)
        Else
            Dim productAdded = listDocumentInvoiceProductSalesDetail.Find(Function(x) x.ProductId = e.DocumentInvoiceProductSalesDetail.ProductId)
            If productAdded IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = $"El producto { e.DocumentInvoiceProductSalesDetail.CodeNameProduct} ya esta agregado"
                Exit Sub
            End If
            listDocumentInvoiceProductSalesDetail.Add(e.DocumentInvoiceProductSalesDetail)
        End If
        INDGcProducts.DataSource = Nothing
        INDGcProducts.DataSource = listDocumentInvoiceProductSalesDetail
        GetRetentionDeclarant()
        RefreshTotals()
    End Sub

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing
    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrosImportFile As List(Of String)
    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        Dim Dresult = openFileDialog1.ShowDialog()
        If Dresult OrElse Dresult = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Dim sddf = New SpreadsheetControl()
                    sddf.AllowDrop = False
                    sddf.LoadDocument(myStream)
                    Dim workBook As IWorkbook = sddf.Document
                    rows = workBook.Worksheets(0).Rows
                    If rows.LastUsedIndex = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    ImportExcelFile()
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(6)})
                                              End SyncLock
                                          End Sub)
    End Sub

    Private Async Sub ImportExcelFile()
        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
        Using model As New MProductInvoice(Me.MyTag)
            Dim result As ActionResult(Of List(Of DocumentInvoiceProductSalesDetail)) = Nothing
            SetRow(1, rows.LastUsedIndex + 1)
            result = Await model.SetPurchaseOrderImportFile(listRows.ToList(), WareHouseId, _idOperativeUnit)
            'si ocurrio un error
            If result.StatusCode = eStatusResult.EXCEPTION Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                AsyncLoader(False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
            For Each item In result.ObjectEmbbeded
                If listDocumentInvoiceProductSalesDetail Is Nothing Then
                    listDocumentInvoiceProductSalesDetail = New List(Of DocumentInvoiceProductSalesDetail)()
                End If
                Dim documentInvoice = listDocumentInvoiceProductSalesDetail.Where(Function(x) x.ProductId = item.ProductId).FirstOrDefault()
                If documentInvoice Is Nothing OrElse documentInvoice.ProductId = 0 Then
                    item.StopTracking()
                    For Each detail In item.DocumentInvoiceProductSalesDetailBatchSerial
                        detail.StopTracking()
                    Next
                    listDocumentInvoiceProductSalesDetail.Add(item)
                Else
                    result.MessageResult.Add($"El producto {documentInvoice.CodeNameProduct} ya esta agregado")
                End If
            Next

            INDGcProducts.DataSource = listDocumentInvoiceProductSalesDetail
            INDGcProducts.RefreshDataSource()

            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End Using
        GetRetentionDeclarant()
        RefreshTotals()
        AsyncLoader(False)
    End Sub

    Private Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If DocumentInvoiceProductSales.Status = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento esta confirmado"
            Exit Sub
        End If
        If INDSleCustomer.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Tercero"
            Exit Sub
        End If
        If INDSleWarehouse.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Almacén"
            Exit Sub
        End If
        Try
            AsyncLoader(True)
            Dim errors As New List(Of String)()
            Dim sddf = New SpreadsheetControl()
            sddf.AllowDrop = False
            'sddf.LoadDocument(myStream)
            Dim workBook As IWorkbook = sddf.Document
            rows = workBook.Worksheets(0).Rows
            rows.Insert(0) 'Inserto una fila en blanco que emularía los encabezados
            Dim objLock As New Object()
            Parallel.For(0, e.Rows.Count, Sub(i)
                                              If e.Rows(i).Count <> 6 Then
                                                  errors.Add($"El registro {i + 1} no tiene la estructura requerida")
                                                  Exit Sub
                                              End If
                                              SyncLock objLock
                                                  i += 1
                                                  rows.Insert(i)
                                                  rows(i).Item(0).SetValue(e.Rows(i - 1)(0))
                                                  rows(i).Item(1).SetValue(e.Rows(i - 1)(1))
                                                  rows(i).Item(2).SetValue(e.Rows(i - 1)(2))
                                                  rows(i).Item(3).SetValue(e.Rows(i - 1)(3))
                                                  rows(i).Item(4).SetValue(e.Rows(i - 1)(4))
                                                  rows(i).Item(5).SetValue(e.Rows(i - 1)(5))
                                              End SyncLock
                                          End Sub)
            If errors.Count > 0 Then
                Using formulario As New FrmListErrors(errors)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
                'Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                AsyncLoader(False)
                Exit Sub
            End If
            ImportExcelFile()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Function LoadThirdPartySelected(thirdPartyId As Integer?, Optional force As Boolean = False) As Task
        If thirdPartyId IsNot Nothing AndAlso (force OrElse ThirdPartySelected Is Nothing) Then
            Using modelTp As New Common.MVP.MThirdParty(Me.Tag)
                'Cargamos el Tercero seleccionado en el registro
                ThirdPartySelected = (Await modelTp.GetThirdPartyById(thirdPartyId))
            End Using
        End If
    End Function

    Private Async Sub GetRetentionDeclarant()
        If listDocumentInvoiceProductSalesDetail IsNot Nothing Then
            Await LoadThirdPartySelected(ThirdPartyId)

            Dim itemBaseValue As Decimal = Me.listDocumentInvoiceProductSalesDetail.Where(Function(x) x.IvaValue > 0).Sum(Function(x) x.SubTotalValue - x.DiscountValue)

            For Each item In listDocumentInvoiceProductSalesDetail
                Using Model As New MInventoryProduct(MyTag)
                    Dim product As InventoryProduct = Model.GetInventoryProductByIdSimpleToGroup(item.ProductId)
                    With item
                        .RTFPercentage = 0
                        .RTFValue = 0
                        .WithholdingICA = 0
                        .WithholdingTax = 0

                        If ThirdPartySelected IsNot Nothing AndAlso ThirdPartySelected.RetentionType = 2 Then
                            If (parameterBilling.ApplyBasicBilling) Then
                                If product.ProductGroup.ReteFuenteConceptId IsNot Nothing Then
                                    Using modelRetentionConcept As New Accounting.MVP.MRetentionConcept(Me.Tag)
                                        Dim retentionConcept = modelRetentionConcept.GetRetentionByIdSimple(product.ProductGroup.ReteFuenteConceptId)
                                        If listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.SubTotalValue - x.DiscountValue) > retentionConcept.MinBase Then
                                            .RTFPercentage = retentionConcept.Rate
                                            .RTFValue = CDec(Utils.RoundValue((.SubTotalValue - .DiscountValue) * (retentionConcept.Rate / 100), 6))
                                        End If
                                    End Using
                                End If
                            Else
                                Using modelAccountPayableConcept As New Payments.MVP.MConceptsAccountsPayable(Me.Tag)
                                    Dim concept = modelAccountPayableConcept.GetPaymentConceptById(product.ProductGroup.NotDeclarantRetentionAccountPayableConceptId).ObjectEmbbeded
                                    Using modelRetentionConcept As New Accounting.MVP.MRetentionConcept(Me.Tag)
                                        If concept.RetentionConceptId IsNot Nothing Then
                                            Dim retentionConcept = modelRetentionConcept.GetRetentionByIdSimple(concept.RetentionConceptId)
                                            If listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.SubTotalValue - x.DiscountValue) > retentionConcept.MinBase Then
                                                .RTFPercentage = retentionConcept.Rate
                                                .RTFValue = CDec(Utils.RoundValue((.SubTotalValue - .DiscountValue) * (retentionConcept.Rate / 100), 6))
                                            End If
                                        End If
                                    End Using
                                End Using
                            End If

                            If _icaPercentage > 0 Then
                                .WithholdingICA = CDec(Utils.RoundValue((.SubTotalValue - .DiscountValue) * (_icaPercentage / 100), 6))
                            End If

                            If _currentCompany.ContributionType > 0 AndAlso ThirdPartySelected.ContributionType > _currentCompany.ContributionType Then
                                If (itemBaseValue) >= Me.WithholdingIvaBase Then
                                    .WithholdingTax = CDec(Utils.RoundValue(.IvaValue * (Me.WithholdingIvaPercentage / 100), 6))
                                End If
                            End If
                        End If

                        .TotalValue = .SubTotalValue + .IvaValue - .DiscountValue
                    End With
                End Using
            Next
        End If
    End Sub

    ''' <summary>
    ''' funcion para mostrar u ocultar el campo de contratos de centro de atencion externo; False para ocultar, True para mostrar
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub HideOrShowContractExternalField(Optional Value As Boolean = False)
        ExternalCenterMode = Value
        INDLycContractExternalClient.Visibility = IIf(Value, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        If Value = False Then
            ContractExternalClientsId = Nothing
            INDSleContractExternalClient.Properties.NullText = String.Empty
        End If
    End Sub

#Region "Liquidacion"

    Private Async Function LoadCurrentCompany() As Task(Of Boolean)
        Try
            AsyncLoader(True)
            Using model As New MEntranceVoucher(MyTag)
                Me._currentCompany = Await model.GetThirdPartyAsync(Me.indigo.IndigoCompanyNit)
                If Me._currentCompany Is Nothing OrElse Me._currentCompany.Id = 0 Then
                    Return False
                End If
            End Using

            Return True

        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
            Return False
        Finally
            AsyncLoader(False)
        End Try
    End Function

    Private Sub ReadOnlyMonetaryInfo()
        INDTxtValue.Properties.ReadOnly = True
        INDTxtDiscountValue.Properties.ReadOnly = True
        INDTxtValueTax.Properties.ReadOnly = True
        INDTxtWithholdingTax.Properties.ReadOnly = True
        INDTxtWithholdingICA.Properties.ReadOnly = True
        INDTxtRetentionSource.Properties.ReadOnly = True
        INDTxtRetentionOther.Properties.ReadOnly = True
        INDTxtDeductionOther.Properties.ReadOnly = True
        INDTxtTotal.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' funcion para importar las remisiones de salida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ImportListDocumentInvoiceSalesDetails(sender As Object, e As ImportDocumentInvoiceProductSalesDetailEventArgs)
        Dim errors As New StringBuilder
        If listDocumentInvoiceProductSalesDetail Is Nothing OrElse listDocumentInvoiceProductSalesDetail.Count = 0 Then
            INDSleWarehouse.Properties.ReadOnly = True
            INDSleContractExternalClient.Properties.ReadOnly = True
            listDocumentInvoiceProductSalesDetail = New List(Of DocumentInvoiceProductSalesDetail)
            ShowOrHideColumnRemissionOutput(True)
            INDBtnAddProducts.Enabled = False
        End If

        Dim lockMe As Object = New Object()
        Parallel.ForEach(e.ListDocumentInvoiceProductSalesDetail, Sub(x)
                                                                      Dim _validate = listDocumentInvoiceProductSalesDetail.FindAll(Function(l) l.ImportSource = 1 AndAlso l.SourceCode = x.SourceCode _
                                                                                                                   AndAlso l.ProductId = x.ProductId).FirstOrDefault()
                                                                      SyncLock lockMe
                                                                          If _validate IsNot Nothing Then
                                                                              Dim LisPhisicalId = (From d In _validate.DocumentInvoiceProductSalesDetailBatchSerial
                                                                                                   Select d.PhysicalInventoryId).ToList()
                                                                              Dim IfExistProduct = (From k In x.DocumentInvoiceProductSalesDetailBatchSerial
                                                                                                    Where LisPhisicalId.Contains(k.PhysicalInventoryId) Select k).ToList()
                                                                              If IfExistProduct IsNot Nothing Then
                                                                                  Mensaje(EeventViewerImages.Advertencia) = $"El producto {x.CodeNameProduct} ya esta agregado. si pertenece a una remision ya importada, seleccionelas al mismo tiempo."
                                                                                  Exit Sub
                                                                              End If
                                                                          End If
                                                                          listDocumentInvoiceProductSalesDetail.Add(x)
                                                                      End SyncLock
                                                                  End Sub)
        INDGcProducts.DataSource = Nothing
        INDGcProducts.DataSource = listDocumentInvoiceProductSalesDetail
        GetRetentionDeclarant()
        RefreshTotals()

    End Sub

#End Region

    Private Async Sub CalculateTotals(CurrentCompanyContributionType As Byte, SupplierContributionType As Byte, AllowIca As Boolean, AllowIcaTop As Boolean, IcaTopValue As Decimal)
        If listDocumentInvoiceProductSalesDetail IsNot Nothing AndAlso listDocumentInvoiceProductSalesDetail.Count > 0 Then
            DocumentInvoiceProductSales.Value = listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.SubTotalValue)
            DocumentInvoiceProductSales.ValueTax = listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.IvaValue)
            DocumentInvoiceProductSales.ValueDiscount = listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.DiscountValue)
            DocumentInvoiceProductSales.RetentionSource = listDocumentInvoiceProductSalesDetail.Sum(Function(x) x.RTFValue)
            DocumentInvoiceProductSales.WithholdingTax = listDocumentInvoiceProductSalesDetail.Sum(Function(o) o.WithholdingTax)
            DocumentInvoiceProductSales.WithholdingICA = listDocumentInvoiceProductSalesDetail.Sum(Function(o) o.WithholdingICA)
        Else
            DocumentInvoiceProductSales.Value = 0
            DocumentInvoiceProductSales.ValueTax = 0
            DocumentInvoiceProductSales.ValueDiscount = 0
            DocumentInvoiceProductSales.RetentionSource = 0
        End If

        If AllowIca Then
            If Not (AllowIcaTop AndAlso DocumentInvoiceProductSales.Value <= IcaTopValue) Then
                If RetentionConceptBranchTask IsNot Nothing Then
                    Dim retention As RetentionConcepts = Await RetentionConceptBranchTask
                    If retention IsNot Nothing Then
                        DocumentInvoiceProductSales.IcaPercentage = retention.Rate
                    End If
                End If
            End If
        End If

        DocumentInvoiceProductSales.TotalValue = DocumentInvoiceProductSales.Value + DocumentInvoiceProductSales.ValueTax -
        DocumentInvoiceProductSales.ValueDiscount - DocumentInvoiceProductSales.WithholdingTax - DocumentInvoiceProductSales.WithholdingICA -
        DocumentInvoiceProductSales.RetentionSource - DocumentInvoiceProductSales.DistrictTax + DocumentInvoiceProductSales.FreightIVAValue + DocumentInvoiceProductSales.FreightValue
    End Sub

    Private Async Sub RefreshTotals()
        If DocumentInvoiceProductSales IsNot Nothing Then
            DocumentInvoiceProductSales.WithholdingIvaPercentage = WithholdingIvaPercentage
            Await LoadThirdPartySelected(ThirdPartyId)
            CalculateTotals(_currentCompany.ContributionType,
                    ThirdPartySelected.ContributionType,
                    ThirdPartySelected.Ica,
                    ThirdPartySelected.IcaTop,
                    ThirdPartySelected.IcaTopValue)

            With DocumentInvoiceProductSales
                Value = .Value
                ValueTax = .ValueTax
                ValueDiscount = .ValueDiscount
                RetentionSource = .RetentionSource
                WithholdingICA = .WithholdingICA
                WithholdingTax = .WithholdingTax
                DistrictTax = .DistrictTax
                NetoValue = .Value - .ValueDiscount
                InvoiceValue = NetoValue + .ValueTax
                TotalValue = .TotalValue
            End With
            ctrTmp.PrintInfo()
        End If
    End Sub

    Private Sub AssigningValues()
        With DocumentInvoiceProductSales
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .BillingAuthorizationId = BillingAuthorizationId
            .FunctionalUnitId = FunctionalUnitId
            .DocumentDate = DocumentDate
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .ThirdPartyId = ThirdPartyId
            .WarehouseId = WareHouseId
            .Description = Observations
            .Value = _subtotal
            .ValueDiscount = _discountValue
            .ValueTax = _ivaValue
            .TotalValue = _total
            .SaleModality = CByte(INDGleModalidadVenta.EditValue)

            If INDLciConditionSales.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ConditionSalesId = ConditionSalesId
            Else
                .ConditionSalesId = Nothing
            End If

            If INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .EconomicActivityId = EconomicActivityId
            Else
                .EconomicActivityId = Nothing
            End If

            .Status = 1
            .WithholdingTax = WithholdingTax
            .WithholdingICA = WithholdingICA
            .IcaPercentage = _icaPercentage
            .RetentionSource = RetentionSource
            .DistrictTax = DistrictTax
            .WithholdingIvaPercentage = WithholdingIvaPercentage
            .ContractExternalClientsId = ContractExternalClientsId

            If INDlyItemBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .BranchOfficeId = BranchOfficeId
            Else
                .BranchOfficeId = Nothing
            End If

            For Each item In listDocumentInvoiceProductSalesDetail
                .DocumentInvoiceProductSalesDetail.Add(item)
            Next

            If listDocumentInvoiceProductSalesDetailDelete IsNot Nothing Then
                For Each item In listDocumentInvoiceProductSalesDetailDelete
                    .DocumentInvoiceProductSalesDetail.Add(item)
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' funcion que se encarga de cargar la parametrizacion
    ''' </summary>
    Private Async Function LoadParameters() As Task
        Try
            Dim _tempOpretingUnitId As Integer
            If BarraBotones.OperatingUnit Is Nothing Then
                _tempOpretingUnitId = indigo.IndigoOperatingUnitId
            Else
                _tempOpretingUnitId = BarraBotones.OperatingUnit.Id
            End If

            Using Model As New MEntranceVoucher(Me.MyTag)
                parameterInventory = Await Model.GetSettingInventory(_tempOpretingUnitId)

                If parameterInventory Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", "Inventory")
                    Deshacer()
                    Exit Function
                End If
            End Using

            Using model As New Presentation.Billing.MVP.MBillingSetting(MyTag)
                parameterBilling = Await model.GetSettingsBillingByIdUnitOperative(_tempOpretingUnitId, False)

                If parameterBilling Is Nothing OrElse parameterBilling.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Facturación para la unidad operativa seleccionada"
                    Deshacer()
                    Exit Function
                End If
            End Using

            Using model As New Accounting.MVP.MCompanySettings(Me.Tag)
                Dim setting = Await model.GetCompanySettings
                If setting IsNot Nothing Then
                    TransactionEconomicActivity = setting.TransactionEconomicActivity
                End If
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.DocumentInvoiceProductSales.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New ObjectChangeTracker
                state.State = ObjectState.Added
                record = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.DocumentInvoiceProductSales.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
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
        Dim content = String.Format(ResourceManager.GetString("FrmProductInvoice_IndexContent", MODULE_NAME), DocumentInvoiceProductSales.Code, If(INDSleCustomer.Text = String.Empty, INDSleCustomer.Properties.NullText, INDSleCustomer.Text), DocumentDate, If(INDSleWarehouse.Text = String.Empty, INDSleWarehouse.Properties.NullText, INDSleWarehouse.Text))
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.DocumentInvoiceProductSales.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.DocumentInvoiceProductSales.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.DocumentInvoiceProductSales.Code)
            Return Me._doc
        End If
    End Function

#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _subtotal = Nothing
        _discountValue = Nothing
        _ivaValue = Nothing
        _icaPercentage = Nothing
        _total = Nothing
        ctrTmp = Nothing
        _sequence = Nothing
        presenter = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        ThirdPartySelected = Nothing
        DocumentInvoiceProductSales = Nothing
        DocumentInvoiceProductSalesDetail = Nothing
        listDocumentInvoiceProductSalesDetail = Nothing
        listDocumentInvoiceProductSalesDetailDelete = Nothing
        indexEditRecord = Nothing
        cleanControlsLoadControls = Nothing
        varImp = Nothing
    End Sub

    Private Async Sub FrmProductInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcProductInvoice, True)
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        presenter = New PProductInvoice(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequence()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        AddHandler INDGvProducts.PopupMenuShowing, AddressOf INDGvProducts_PopupMenuShowing
        AddActionsColumns()
        Deshacer()
        LoadStatus()

        Await LoadCurrentCompany()
        Await LoadParameters()
        Await SetDefaultValuesAndDatasource()
        InitTuples()

        INDBtnImportFile.Enabled = False
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de inicializar las tuplas
    ''' </summary>
    Private Sub InitTuples()
        Dim modalidades As New List(Of Tuple(Of Byte, String))()
        If parameterBilling IsNot Nothing AndAlso parameterBilling.Id > 0 AndAlso parameterBilling.ApplyBasicBilling Then
            modalidades.Add(New Tuple(Of Byte, String)(2, "Credito"))
        Else
            modalidades.Add(New Tuple(Of Byte, String)(1, "Contado"))
            modalidades.Add(New Tuple(Of Byte, String)(2, "Credito"))
        End If
        INDGleModalidadVenta.Properties.DataSource = modalidades
    End Sub

    ''' <summary>
    ''' Funcion que se encarga de setear informacion por defecto cargando previamente los Datasources
    ''' </summary>
    Private Async Function SetDefaultValuesAndDatasource() As Task
        Try

            FreightIVAPercentage = parameterInventory.IvaFreigthPercentage
            WithholdingIvaBase = parameterInventory.WithholdingIvaBase
            WithholdingIvaPercentage = parameterInventory.WithholdingIvaPercentage

            Using model As New MProductInvoice(MyTag)
                BillingAuthorizationXpo = Await model.ListAllBillingAuthorization()

                If BillingAuthorizationXpo.Count = 1 Then
                    INDSleBillingAuthorization.EditValue = BillingAuthorizationXpo.FirstOrDefault.Id
                End If
            End Using

            If RequiresConditionsSale Then
                INDLciConditionSalesVisibility(RequiresConditionsSale)
                Using model As New Billing.MVP.MLiquidation()
                    INDSleConditionSales.Properties.DataSource = Await model.ListConditionSales

                    Dim ConditionSales = TryCast(INDSleConditionSales.Properties.DataSource, List(Of ConditionSalesXpo))
                    If ConditionSales.Count = 1 Then
                        ConditionSalesId = ConditionSales.FirstOrDefault.Id
                    End If
                End Using
            End If

            INDLciEconomicActivity.Visibility = If(TransactionEconomicActivity, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Funcion que se encarga de setear la visibilidad de campos dependiendo de la parametrizacion
    ''' </summary>
    Private Sub INDLciConditionSalesVisibility(Optional IsNotVisible As Boolean = True)
        INDLciConditionSales.HideControl(Not IsNotVisible)
        INDLciConditionSales.ShowInCustomizationForm = Not IsNotVisible
    End Sub

#End Region

#Region "Shown"
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmProductInvoice_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCustomer.QueryPopUp
        If INDSleCustomer.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CustomerXPO Is Nothing Then
            Using model As New MProductInvoice(Me.Tag)
                CustomerXPO = model.ListCustomer()
            End Using
        End If
    End Sub

    Private Async Sub INDSleEconomicActivity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEconomicActivity.QueryPopUp
        If INDSleEconomicActivity.Properties.DataSource Is Nothing Then
            Using model As New Billing.MVP.MLiquidation()
                INDSleEconomicActivity.Properties.DataSource = Await model.ListEconomicActivities(ThirdPartyId)
            End Using
        End If
    End Sub

    Private Sub INDSleWareHouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If WarehouseXPO Is Nothing Then
            Using model As New MProductInvoice(MyTag)
                WarehouseXPO = model.ListWarehouse()
            End Using
        End If
    End Sub

    Private Sub INDsleFunctionalUnitFirst_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleFunctionalUnitFirst.QueryPopUp
        If FunctionalUnitXpoFirst Is Nothing Then
            Using model As New MProductInvoice(MyTag)
                FunctionalUnitXpoFirst = model.ListFunctionalUnit()
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleFunctionalUnitFirst_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFunctionalUnitFirst.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("523", Nothing, True)
        End If
    End Sub

    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCustomer.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("503", Nothing, True)
        End If
    End Sub

    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("302", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de sucursal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBranchOffice_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBranchOffice.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1970, Nothing, True)
        End If
    End Sub

#End Region

#Region "KeyDown"
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
                    Await Me.NewProductInvoice()
                Else
                    Await Me.LoadControls()
                End If
            End If

        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
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
        Me.ViewModeEditHold = True
        If Me.DocumentInvoiceProductSales IsNot Nothing AndAlso Me.DocumentInvoiceProductSales.Id > 0 Then
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

#Region "Click"
    Private Sub INDBtnAddProducts_Click(sender As Object, e As EventArgs) Handles INDBtnAddProducts.Click
        Using formulario As New FrmPopupProductInvoice
            Me.Cursor = ChangeCursorIndigo()
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.WareHouseId = WareHouseId
            formulario.operatingUnitId = BarraBotones.OperatingUnitValue
            formulario.ExternalCenterMode = ExternalCenterMode
            formulario.ContractExternalClientId = ContractExternalClientsId
            formulario.FunctionalUnitId = FunctionalUnitId
            formulario.RateProductDate = DocumentDate
            AddHandler formulario.AddDocumentInvoiceProductSalesDetail, AddressOf ReturnAddDocumentInvoiceProductSalesDetail
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        INDBtnImportFile.Enabled = INDSleWarehouse.EditValue IsNot Nothing AndAlso INDSleCustomer.EditValue IsNot Nothing
        INDBtnAddProducts.Enabled = INDSleWarehouse.EditValue IsNot Nothing AndAlso INDSleCustomer.EditValue IsNot Nothing

        If INDSleWarehouse.EditValue IsNot Nothing AndAlso ThirdPartyId <> 0 AndAlso ContractExternalClientsId IsNot Nothing AndAlso ContractExternalClientsId > 0 Then
            ShowHidePermissionImportButton()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCustomer.EditValueChanged
        If IsLoad = False Then
            _icaPercentage = 0
            EconomicActivityId = Nothing
            INDSleEconomicActivity.Properties.DataSource = Nothing
            INDSleEconomicActivity.Properties.NullText = String.Empty
            ThirdPartySelected = Nothing
            If ThirdPartyId <> Nothing AndAlso ThirdPartyId <> 0 Then
                presenter.GetIVARetentionConceptByThirdPartyId(ThirdPartyId)
                'Se obtiene el tercero por id
                Await LoadThirdPartySelected(ThirdPartyId)
                'Se valida si el tercero maneja ICA, si es así se actualiza el porcentaje de retención asociado al tercero
                If ThirdPartySelected.Ica Then
                    _icaPercentage = ThirdPartySelected.IcaPercentage
                End If
                'Se valida que el tercero maneje sucursal, si maneja se muestra el campo de sucursal
                If ThirdPartySelected.HandlesBranchOffice Then
                    INDlyItemBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemBranchOffice.AllowHide = False
                    'Se pone null el datasource y el campo de la sucursal
                    BranchOfficeId = Nothing
                    BranchOfficeXpo = Nothing
                    'Se llena el datasource con el nuevo id de tercero
                    presenter.InitializeBranchOfficeByThirdPartyId(ThirdPartyId)
                Else 'Si no maneja no muestra el campo de sucursal
                    INDlyItemBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemBranchOffice.AllowHide = True
                End If

                If TransactionEconomicActivity Then
                    Await LoadDatasourceEconomicActivities(ThirdPartySelected)
                End If

                'Valida si el cliente tiene contratos de centros de atencion externos
                Dim DataSource = (From h In ThirdPartySelected.Customer Select h.ContractExternalClients).FirstOrDefault.ToList()
                DataSource = DataSource.Where(Function(m) m.Status = True).ToList()
                If DataSource IsNot Nothing AndAlso DataSource.Count > 0 Then
                    If MessageIndigo.Show("Desea Asociar un Contrato de Centro de Atención Externo del Cliente seleccionado", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        HideOrShowContractExternalField(True)
                        If DataSource.Count = 1 Then
                            INDSleContractExternalClient.Properties.DataSource = DataSource.ToList()
                            ContractExternalClientsId = DataSource.FirstOrDefault.Id
                            INDSleContractExternalClient.Properties.NullText = DataSource.FirstOrDefault.ContractNumber
                            If INDSleWarehouse.EditValue IsNot Nothing AndAlso ContractExternalClientsId IsNot Nothing AndAlso ContractExternalClientsId > 0 Then
                                ShowHidePermissionImportButton()
                            End If
                        ElseIf DataSource.Count > 1 Then
                            INDSleContractExternalClient.Properties.DataSource = DataSource.ToList()
                        End If
                    Else
                        HideOrShowContractExternalField()
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                    End If
                Else
                    HideOrShowContractExternalField()
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                End If
            End If
        End If

        INDBtnImportFile.Enabled = INDSleWarehouse.EditValue IsNot Nothing AndAlso INDSleCustomer.EditValue IsNot Nothing
        INDBtnAddProducts.Enabled = INDSleWarehouse.EditValue IsNot Nothing AndAlso INDSleCustomer.EditValue IsNot Nothing
    End Sub

    ''' <summary>
    ''' Carga el datasource de las actividades economicas del tercero relacionado al cliente
    ''' </summary>
    ''' <param name="thirdParty"></param>
    Private Async Function LoadDatasourceEconomicActivities(thirdParty As ThirdParty) As Task
        If thirdParty Is Nothing Then
            INDSleEconomicActivity.Properties.DataSource = Nothing
            Return
        End If

        Try
            Using model As New Billing.MVP.MLiquidation()

                Dim activities As List(Of CommonRepository.CommonEconomicActivity) =
                Await model.ListEconomicActivities(thirdParty.Id)

                Dim hasEA As Boolean = activities IsNot Nothing AndAlso activities.Count > 0

                If thirdParty.ElectronicBiller AndAlso Not hasEA Then
                    INDSleEconomicActivity.Properties.DataSource = Nothing
                    ShowMessage(EeventViewerImages.Advertencia) = "Por favor asocie una Actividad Económica al Cliente"
                    Return
                End If

                INDSleEconomicActivity.Properties.DataSource = activities

                'Selección por defecto (si no es carga inicial)
                If Not IsLoad AndAlso hasEA Then
                    Dim defaultEAId As Integer? =
                    activities.
                        SelectMany(Function(ea) ea.CommonThirdPartyEconomicActivitiesXpo).
                        Where(Function(ct) ct.Defect).
                        OrderBy(Function(ct) ct.Id). ' determinístico
                        Select(Function(ct) CType(ct.EconomicActivityId?.Id, Integer?)).
                        FirstOrDefault()

                    EconomicActivityId = defaultEAId
                End If
            End Using
        Catch ex As Exception
            INDSleEconomicActivity.Properties.DataSource = Nothing
            ShowMessage(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Function

    Private Async Sub INDsleBranchOffice_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBranchOffice.EditValueChanged
        _icaPercentage = 0
        Await LoadThirdPartySelected(ThirdPartyId)
        If ThirdPartySelected IsNot Nothing Then
            _icaPercentage = IIf(ThirdPartySelected.Ica, ThirdPartySelected.IcaPercentage, 0)
            If BranchOfficeId IsNot Nothing Then
                presenter.GetRetentionConceptByBranchOfficeId(BranchOfficeId)
                If ThirdPartySelected.Ica Then
                    If RetentionConceptBranchTask IsNot Nothing Then
                        Dim retention As RetentionConcepts = Await RetentionConceptBranchTask
                        If retention IsNot Nothing Then
                            _icaPercentage = retention.Rate
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub INDSleContractExternalClient_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleContractExternalClient.EditValueChanged
        If INDSleContractExternalClient.EditValue IsNot Nothing AndAlso INDSleContractExternalClient.Properties.DataSource IsNot Nothing Then
            Dim x = CType(INDSleContractExternalClient.Properties.DataSource, List(Of ContractExternalClients)).FindAll(Function(g) g.Id = INDSleContractExternalClient.EditValue).FirstOrDefault
            INDGleModalidadVenta.EditValue = IIf(x.SaleMode = 1, 2, 1)
        End If
        If ExternalCenterMode Then
            INDGleModalidadVenta.Enabled = False
        Else
            INDGleModalidadVenta.Enabled = True
        End If
    End Sub

#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If DocumentInvoiceProductSales.Status > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("No se puede modificar el registro porque la factura esta {0}", IIf(DocumentInvoiceProductSales.Status = 2, "Confirmada", "Anulada"))
            Exit Sub
        End If
        DocumentInvoiceProductSalesDetail = DirectCast(INDGvProducts.GetFocusedRow(), DocumentInvoiceProductSalesDetail)
        Select Case (sender.Tag)
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                If listDocumentInvoiceProductSalesDetail.Any(Function(x) x.ImportSource IsNot Nothing AndAlso x.ImportSource = 1) Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque se importó desde una remisión de salida"
                    Exit Sub
                End If
                indexEditRecord = listDocumentInvoiceProductSalesDetail.IndexOf(DocumentInvoiceProductSalesDetail)
                Using formulario As New FrmPopupProductInvoice
                    Me.Cursor = ChangeCursorIndigo()
                    AddHandler formulario.AddDocumentInvoiceProductSalesDetail, AddressOf ReturnAddDocumentInvoiceProductSalesDetail
                    formulario.Size = New System.Drawing.Size(800, 730)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    formulario.DocumentInvoiceProductSalesDetailEdit = DocumentInvoiceProductSalesDetail
                    formulario.EditMode = True
                    formulario.WareHouseId = WareHouseId
                    formulario.operatingUnitId = BarraBotones.OperatingUnitValue
                    Dim transparent = New Base.FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If DocumentInvoiceProductSalesDetail IsNot Nothing AndAlso DocumentInvoiceProductSalesDetail.Id > 0 Then
                        If listDocumentInvoiceProductSalesDetailDelete Is Nothing Then
                            listDocumentInvoiceProductSalesDetailDelete = New List(Of DocumentInvoiceProductSalesDetail)
                        End If
                        While DocumentInvoiceProductSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial.Count > 0
                            If DocumentInvoiceProductSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial(0).Id > 0 Then
                                DocumentInvoiceProductSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial(0).MarkAsDeleted()
                            Else
                                DocumentInvoiceProductSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial.Remove(DocumentInvoiceProductSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial(0))
                            End If
                        End While
                        DocumentInvoiceProductSalesDetail.MarkAsDeleted()
                        listDocumentInvoiceProductSalesDetailDelete.Add(DocumentInvoiceProductSalesDetail)
                    End If
                    listDocumentInvoiceProductSalesDetail.Remove(DocumentInvoiceProductSalesDetail)
                    If listDocumentInvoiceProductSalesDetail.Count = 0 AndAlso DocumentInvoiceProductSales.Id = 0 Then
                        INDSleWarehouse.Properties.ReadOnly = False
                    End If
                    INDGcProducts.DataSource = Nothing
                    INDGcProducts.DataSource = listDocumentInvoiceProductSalesDetail
                    RefreshTotals()
                End If
        End Select
    End Sub
#End Region

#Region "PopupMenuShowing"
    ''' <summary>
    ''' Oculta el menú contextual de acciones (Editar/Eliminar) cuando el registro está en estado Confirmado.
    ''' </summary>
    Private Sub INDGvProducts_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
        IndigoGridView1.RaiseMenuPopUp = DocumentInvoiceProductSales Is Nothing OrElse DocumentInvoiceProductSales.Status <> 2
    End Sub
#End Region

#Region "ShowingEditor"
    Private Sub INDGvProducts_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvProducts.ShowingEditor
        Dim detail = DirectCast(INDGvProducts.GetFocusedRow, DocumentInvoiceProductSalesDetail)
        If detail.HandlesBatch = False Then
            INDRpPceBatch.ReadOnly = True
        Else
            INDRpPceBatch.ReadOnly = False
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDRpPceBatch_Popup(sender As Object, e As EventArgs) Handles INDRpPceBatch.Popup
        Dim detail = DirectCast(INDGvProducts.GetFocusedRow, DocumentInvoiceProductSalesDetail)
        INDGcBatch.DataSource = Nothing
        INDGcBatch.DataSource = detail.DocumentInvoiceProductSalesDetailBatchSerial
    End Sub
#End Region

    Private Sub INDGcProducts_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcProducts.DataSourceChanged
        INDsleBranchOffice.ReadOnly = INDGcProducts.DataSource IsNot Nothing AndAlso CType(INDGcProducts.DataSource, IList).Count > 0
        INDSleCustomer.ReadOnly = INDGcProducts.DataSource IsNot Nothing AndAlso CType(INDGcProducts.DataSource, IList).Count > 0
    End Sub

#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Permite mostrar el botón de importar cotización
    ''' </summary>
    Private Sub ShowHidePermissionImportButton()
        If (From x In BarraBotones.PermissionsForm Where x.Key = 95 Select x).Count > 0 AndAlso DocumentInvoiceProductSales IsNot Nothing AndAlso {0, 1}.Contains(DocumentInvoiceProductSales.Status) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

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
        BarraBotones.Focus()
        varImp = 1
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
        BarraBotones.Focus()
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BillingSequenceDetail IsNot Nothing Then
                If Not Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If DocumentInvoiceProductSales IsNot Nothing AndAlso DocumentInvoiceProductSales.Status = 2 Then
            'Confirmacion para reversar
            If MessageIndigo.Show("¿Está seguro que desea reversar el documento?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                varImp = 4
                Reversar()
            End If
        Else
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DocumentInvoiceProductSales.Status = 3
                varImp = 3
                Guardar()
            End If
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            SaveOrUpdateAndConfirm(1)
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            SaveOrUpdateAndConfirm(2)
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, DocumentInvoiceProductSales.Id, 0, DocumentInvoiceProductSales.Id)
    End Sub
#End Region

#Region "IMPORT"
    ''' <summary>
    ''' Evento que dispara el formulario de importar productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmImportRemissionOut
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ThirdPartyId = ThirdPartyId
            formulario.codeUser = indigo.UserIndigo
            formulario.WarehouseId = INDSleWarehouse.EditValue
            formulario.Presenter = presenter
            If listDocumentInvoiceProductSalesDetail Is Nothing Then
                listDocumentInvoiceProductSalesDetail = New List(Of DocumentInvoiceProductSalesDetail)
            End If
            formulario.ListDocumentInvoiceProductSalesDetail = listDocumentInvoiceProductSalesDetail.ToList()
            formulario.ContractExternalClientId = INDSleContractExternalClient.EditValue
            formulario.FunctionalUnitId = FunctionalUnitId
            AddHandler formulario.GetDocumentInvoiceProductSalesDetail, AddressOf ImportListDocumentInvoiceSalesDetails
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

End Class
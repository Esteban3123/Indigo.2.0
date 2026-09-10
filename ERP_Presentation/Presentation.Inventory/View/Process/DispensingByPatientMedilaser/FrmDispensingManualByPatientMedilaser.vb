'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/09/2018
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports DevExpress.XtraGrid.Columns
Imports System.Drawing
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Text
Imports Domain.Base.Entities
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Billing.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.XtraSplashScreen
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.CommonRepository
Imports DevExpress.Data.Async.Helpers
Imports Presentation.Accounting.MVP

#End Region

Public Class FrmDispensingManualByPatientMedilaser
    Implements IDispensingManualPatientMedilaser

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ctrTmp = New CtrReprintDispensing()
        ctrTmp.PopupContainerControlTotalValue = INDpopupRealizedDispensing
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)

        AddHandler bwGetRealizedDispensing.DoWork, AddressOf bwGetRealizedDispensing_DoWork
        AddHandler bwGetRealizedDispensing.RunWorkerCompleted, AddressOf bwGetRealizedDispensing_RunWorkerCompleted
    End Sub

#End Region

#Region "Properties"

    Public Property CareCenterCode As String Implements IDispensingManualPatientMedilaser.CareCenterCode
        Get
            Return INDsleCareCenter.EditValue
        End Get
        Set(value As String)
            INDsleCareCenter.EditValue = value
        End Set
    End Property

    Public Property CareCenterXpo As XPInstantFeedbackSource Implements IDispensingManualPatientMedilaser.CareCenterXpo
        Get
            Return INDsleCareCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCareCenter.Properties.DataSource = value
        End Set
    End Property

    Public Property WarehouseId As Integer Implements IDispensingManualPatientMedilaser.WarehouseId
        Get
            Return INDsleWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDsleWarehouse.EditValue = value
        End Set
    End Property

    Public Property WarehouseXpo As XPInstantFeedbackSource Implements IDispensingManualPatientMedilaser.WarehouseXpo
        Get
            Return INDsleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleWarehouse.Properties.DataSource = value
        End Set
    End Property

    Public Property CareGroupId As Integer Implements IDispensingManualPatientMedilaser.CareGroupId
        Get
            Return INDsleCareGroup.EditValue
        End Get
        Set(value As Integer)
            INDsleCareGroup.EditValue = value
        End Set
    End Property

    Public Property CareGroupXpo As XPInstantFeedbackSource Implements IDispensingManualPatientMedilaser.CareGroupXpo
        Get
            Return INDsleCareGroup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCareGroup.Properties.DataSource = value
        End Set
    End Property

    Public Property BillingAuthorizationId As Integer Implements IDispensingManualPatientMedilaser.BillingAuthorizationId
        Get
            Return INDsleBillingAuthorization.EditValue
        End Get
        Set(value As Integer)
            INDsleBillingAuthorization.EditValue = value
        End Set
    End Property

    Public Property BillingAuthorizationXpo As XPInstantFeedbackSource Implements IDispensingManualPatientMedilaser.BillingAuthorizationXpo
        Get
            Return INDsleBillingAuthorization.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBillingAuthorization.Properties.DataSource = value
        End Set
    End Property

    Public Property DateItem As DateTime? Implements IDispensingManualPatientMedilaser.DateItem
        Get
            Return INDdteDateItem.EditValue
        End Get
        Set(value As DateTime?)
            INDdteDateItem.EditValue = value
        End Set
    End Property

    Public Property Number As String Implements IDispensingManualPatientMedilaser.Number
        Get
            Return INDtxtNumber.EditValue
        End Get
        Set(value As String)
            INDtxtNumber.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements IDispensingManualPatientMedilaser.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDispensingManualPatientMedilaser.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    Public Property PatientIdentification As String Implements IDispensingManualPatientMedilaser.PatientIdentification
        Get
            Return INDbtnPatient.EditValue
        End Get
        Set(value As String)
            INDbtnPatient.EditValue = value
        End Set
    End Property

    Public Property IPSCode As String Implements IDispensingManualPatientMedilaser.IPSCode
        Get
            Return INDsleIPS.EditValue
        End Get
        Set(value As String)
            INDsleIPS.EditValue = value
        End Set
    End Property

    Public Property IPSXpo As XPInstantFeedbackSource Implements IDispensingManualPatientMedilaser.IPSXpo
        Get
            Return INDsleIPS.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIPS.Properties.DataSource = value
        End Set
    End Property

    Public Property HealthProfessionalXpo As XPInstantFeedbackSource Implements IDispensingManualPatientMedilaser.HealthProfessionalXpo
        Get
            Return INDsleProfessionalHealth.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProfessionalHealth.Properties.DataSource = value
        End Set
    End Property

    Public Property PerformsHealthProfessionalThirdPartyId As Integer? Implements IDispensingManualPatientMedilaser.PerformsHealthProfessionalThirdPartyId
        Get
            Return INDsleProfessionalHealth.EditValue
        End Get
        Set(value As Integer?)
            INDsleProfessionalHealth.EditValue = value
        End Set
    End Property

    Public ReadOnly Property SelectedPerformsHealthProfessional As ViewHealthProfessionalXpo
        Get
            Return TryCast(TryCast(INDGdvHealthProfessional.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, ViewHealthProfessionalXpo)
        End Get
    End Property

    Public Property FormulationDate As DateTime? Implements IDispensingManualPatientMedilaser.FormulationDate
        Get
            Return INDdteDateFormulation.EditValue
        End Get
        Set(value As DateTime?)
            INDdteDateFormulation.EditValue = value
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDtxtNumber.Enabled = Not value
            INDbtnPatient.Enabled = Not value
            INDsleIPS.Enabled = value
            INDsleCareCenter.Enabled = value
            INDsleWarehouse.Enabled = value
            INDsleCareGroup.Enabled = value
            INDsleBillingAuthorization.Enabled = value
            INDdteDateItem.Enabled = value
            INDdteDateFormulation.Enabled = value
            INDsleProfessionalHealth.Enabled = value
            INDbtnAddProduct.Enabled = value
            INDgcProducts.Enabled = value

            If value Then
                INDsleIPS.Focus()
            Else
                INDtxtNumber.Focus()
            End If

            INDlyRoot.EndUpdate()
        End Set
    End Property

#End Region

#Region "BackgroundWorker"

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwGetRealizedDispensing_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        ListViewRealizedDispensingByManualDispensing = Presenter.ListViewRealizedDispensingByManualDispensing(MedicalFormulaXpo.Id)
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwGetRealizedDispensing_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        INDviewRealizedDispensing.HideLoadingPanel()
        INDgcRealizedDispensing.DataSource = Nothing
        If ListViewRealizedDispensingByManualDispensing IsNot Nothing AndAlso ListViewRealizedDispensingByManualDispensing.Count > 0 Then
            INDgcRealizedDispensing.DataSource = ListViewRealizedDispensingByManualDispensing
        End If
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado de dispensaciones que se han realizado con la formual medica
    ''' </summary>
    Dim ListViewRealizedDispensingByManualDispensing As List(Of ViewRealizedDispensingByManualDispensingXpo)

    ''' <summary>
    ''' Asyncrono para obtener las dispensaciones que se han realizado con la formula medica
    ''' </summary>
    ''' <remarks></remarks>
    Private bwGetRealizedDispensing As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrReprintDispensing

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDispensingManualByPatientMedilaser

    ''' <summary>
    ''' Formula medica que se consulta por paciente y no. de formula
    ''' </summary>
    Dim MedicalFormulaXpo As MedicalFormulaXpo

    ''' <summary>
    ''' Listado de los productos
    ''' </summary>
    Dim ListViewDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)

    ''' <summary>
    ''' Entidad xpo para el paciente
    ''' </summary>
    Dim PatientXpo As PatientXpo

    ''' <summary>
    ''' Listado que viene desde dispensación por paciente Medilaser
    ''' </summary>
    Public ListProductsByCODCONCEC As List(Of SP_ListHCPRESCRDByCODCONCEC_Result)

    ''' <summary>
    ''' Splash de espera para sacar la tirilla
    ''' </summary>
    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)


    ''' <summary>
    ''' Obtiene o establece la configuracion de la compañia
    ''' </summary>
    Private _companySettings As CompanySettings


    ''' <summary>
    ''' Tipo de redondeo de la moneda parametrizada
    ''' </summary>
    Private _roundingType As Byte

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Abre el form modal para la busqueda
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        DeleteBlockRecord()
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Form modal que consulta los pacientes
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "IPCODPACI", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "IPNOMCOMP", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)}}.ToList
            .ValorSolicitado = "IPCODPACI"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmDispensingManualByPatientMedilaser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Presenter = New PDispensingManualByPatientMedilaser(Me)
        IndigoGridControl1.RefreshGrid(INDgcProducts)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewGridProducts, ListActions)
        IndigoGridView1.MoreInfoColunmns(INDviewGridProducts)

        Dim ListActions2 As New List(Of eAcciones)
        ListActions2.Add(eAcciones.Print)
        IndigoGridView2.SetListAcction(INDviewRealizedDispensing, ListActions2)
        LoadCompanySettings()
        Deshacer()
    End Sub


    Private Async Function LoadCompanySettings() As Task
        Using model As New MCompanySettings(Me.Tag)
            _companySettings = Await model.GetCompanySettings()
        End Using
    End Function



#End Region

#Region "QueryPopup"

    Private Sub INDrepPceDeferred_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceDeferred.QueryPopUp
        Dim rowDetail = DirectCast(INDviewGridProducts.GetFocusedRow(), Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
        INDgcDeferred.DataSource = Nothing
        INDgcDeferred.DataSource = rowDetail.ListDeferred
    End Sub

    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If CareCenterXpo Is Nothing Then
            Presenter.LoadCareCenter()
        End If
    End Sub

    Private Sub INDsleWarehouse_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleWarehouse.QueryPopUp
        If WarehouseXpo Is Nothing Then
            Presenter.LoadWareHouse()
        End If
    End Sub

    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If CareGroupXpo Is Nothing Then
            Presenter.LoadCareGroup()
        End If
    End Sub

    Private Sub INDsleBillingAuthorization_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBillingAuthorization.QueryPopUp
        If BillingAuthorizationXpo Is Nothing Then
            Presenter.LoadBillingAuthorization()
        End If
    End Sub

    Private Sub INDsleIPS_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIPS.QueryPopUp
        If IPSXpo Is Nothing Then
            Presenter.LoadIPS()
        End If
    End Sub

    Private Sub INDsleProfessionalHealth_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProfessionalHealth.QueryPopUp
        If Me.HealthProfessionalXpo Is Nothing Then
            Presenter.LoadHealthProfessional()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(302, Nothing, True)
            Presenter.LoadWareHouse()
        End If
    End Sub

    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(985, Nothing, True)
            Presenter.LoadCareGroup()
        End If
    End Sub

    Private Sub INDsleBillingAuthorization_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBillingAuthorization.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1517, Nothing, True)
            Presenter.LoadBillingAuthorization()
        End If
    End Sub


    Private Sub INDtxtNumber_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtNumber.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            OpenSearchMedicalFormula()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDbtnAddProduct.Click
        OpenAddProduct(False)
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmDispensingManualByPatientMedilaser_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDtxtNumber.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDbtnPatient_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnPatient.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                OpenAddProduct(True)
            Case "Remove"
                DeleteProduct()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                OpenAddProduct(True)
            Case "Remove"
                DeleteProduct()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Dim info As ViewRealizedDispensingByManualDispensingXpo = INDviewRealizedDispensing.GetFocusedRow()
        OpenSelectedReport(info.InvoiceId, info.AdmissionNumber, info.InvoiceNumber)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim info As ViewRealizedDispensingByManualDispensingXpo = INDviewRealizedDispensing.GetFocusedRow()
        OpenSelectedReport(info.InvoiceId, info.AdmissionNumber, info.InvoiceNumber)
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmDispensingManualByPatientMedilaser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockRecord()
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDsleWarehouse_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleWarehouse.EditValueChanging
        If ListViewDashboardPharmacyDetail IsNot Nothing AndAlso ListViewDashboardPharmacyDetail.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede cambiar de almacen debido a que ya hay detalles agregados."
            e.Cancel = True
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Procesa la dispensación
    ''' </summary>
    Private Async Sub ProccessDispensing()
        'Se validan los campos del formulario
        Dim errors = ValidateFields()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        AsyncLoader(True)

        Dim listPharmaceuticalDispensing As New List(Of PharmaceuticalDispensing)
        Dim TempListDeferred As New List(Of ViewDashboardPharmacyDetailDeferred)
        Dim PharmaceuticalDispensing As New PharmaceuticalDispensing
        Dim errorsPharmaceutical As New StringBuilder

        Dim listPhysical = (From e In ListViewDashboardPharmacyDetail Where e.ListPhysicalInventory IsNot Nothing Select e.ListPhysicalInventory).ToList
        Dim listProductsId As New List(Of Integer)
        If listPhysical IsNot Nothing AndAlso listPhysical.Count > 0 Then
            For Each item In listPhysical
                listProductsId.AddRange((From e In item Select e.ProductId).ToList())
            Next
        End If

        Dim listProductRateDetail As List(Of ProductRateDetail)
        Using model As New MProductRate(Me.Tag)
            listProductRateDetail = Await model.GetListProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, listProductsId, GetDateServer())
        End Using

        If _companySettings IsNot Nothing Then
            _roundingType = _companySettings.Currency?.RoundingType
        End If

        With PharmaceuticalDispensing
            .CodeNameWarehouse = Me.INDsleWarehouse.Text.Split(" - ")(0).Trim()
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .AdmissionNumber = ""
            .DocumentDate = Me.DateItem()
            .Status = 1
            .PantientName = PatientXpo.IPNOMCOMP.Trim()
            .PantientFirstName = PatientXpo.IPPRINOMB.Trim()
            .PantientMiddleName = PatientXpo.IPSEGNOMB.Trim()
            .PantientLastName = PatientXpo.IPPRIAPEL.Trim()
            .PantientSecondLastName = PatientXpo.IPSEGAPEL.Trim()
            .CodePatient = PatientXpo.IPCODPACI.Trim()
            .FunctionUnitName = ""
            .CareCenterCode = CareCenterCode
            .FunctionUnitCode = ""
            .ConsecutivePescription = 0
            .ConsecutiveInputs = 0
            .ConsecutivePharmacy = 0
            .ConsecutiveCrystal = 0
            .HistoryType = ""
            .AffectInventory = True
            .PerformsHealthProfessionalThirdPartyId = Me.PerformsHealthProfessionalThirdPartyId
            .DateFormulation = Me.FormulationDate
        End With
        For Each item In (From x In ListViewDashboardPharmacyDetail Where x.CantidadEntregada > 0)
            For Each itemPhysical In item.ListPhysicalInventory
                Dim PharmaceuticalDispensingDetailBatchSerial = New PharmaceuticalDispensingDetailBatchSerial
                With PharmaceuticalDispensingDetailBatchSerial
                    .PhysicalInventoryId = itemPhysical.Id
                    .OutstandingQuantity = itemPhysical.QuantityDeliver
                    .Quantity = itemPhysical.QuantityDeliver
                    .IdWarehouse = itemPhysical.WarehouseId
                    .ProductId = itemPhysical.ProductId
                    .BatchSerialId = itemPhysical.BatchSerialId
                    .UserIndigo = Me.indigo.UserIndigo
                End With
                Dim despensigDetailAdded = PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Where(Function(x) x.WarehouseId = itemPhysical.WarehouseId And x.ProductId = itemPhysical.ProductId).FirstOrDefault()
                If despensigDetailAdded IsNot Nothing Then
                    despensigDetailAdded.Quantity += itemPhysical.QuantityDeliver
                    despensigDetailAdded.GrandTotalSalesPrice = despensigDetailAdded.SalePrice * despensigDetailAdded.Quantity
                    despensigDetailAdded.PharmaceuticalDispensingDetailBatchSerial.Add(PharmaceuticalDispensingDetailBatchSerial)
                Else
                    Dim PharmaceuticalDispensingDetail As New PharmaceuticalDispensingDetail
                    With PharmaceuticalDispensingDetail
                        .CareGroupId = CareGroupId
                        .ProductId = itemPhysical.ProductId
                        .WarehouseId = itemPhysical.WarehouseId
                        .Quantity = itemPhysical.QuantityDeliver
                        .ServiceDate = PharmaceuticalDispensing.DocumentDate
                        .FunctionalUnitId = 0
                        .OrderedHealthProfessionalCode = SelectedPerformsHealthProfessional.Code
                        .OrderedProfessionalSpecialty = SelectedPerformsHealthProfessional.CodeProfessionalSpecialty
                        .OrderedHealthProfessionalThirdPartyId = Me.PerformsHealthProfessionalThirdPartyId
                        .CodeProduct = item.Producto.Split(" - ").ElementAt(0).Trim()
                        .NameProduct = item.Producto.Substring(item.Producto.IndexOf(" - ") + 3)
                        .CantidadPendiente = item.CantidadPendiente
                        .CantidadSolicitada = item.CantidadSolicitada
                        .UnidadMedida = item.UnidadMedida
                        .ProductType = item.Tipo
                        .HealthAdministratorId = 0
                        .ThirdPartyId = 0
                        .idProductoHeon = 0
                        .recetarioOMedica = ""
                        .LiquidationType = 1
                        .MedicamentCode = item.MedicamentCode
                        .TypeProduct = item.TypeProduct
                        .AuthorizationNumber = item.AuthorizationNumber
                        .DiagnosticCode = item.DiagnosticCode
                        .IDMipres = item.IDMipres
                        .TreatmentDays = item.TreatmentDays

                        Dim _productRateDetail As ProductRateDetail = listProductRateDetail.Find(Function(x) x.ProductId = .ProductId)
                        If _productRateDetail IsNot Nothing Then
                            .SalePrice = RoundValue(_productRateDetail.SalesValue, _roundingType)
                            .TotalSalesPrice = .SalePrice
                            .GrandTotalSalesPrice = RoundValue(.TotalSalesPrice * .Quantity, _roundingType)
                            If _productRateDetail?.InventoryProduct?.GeneralLedgerIVA?.Id > 0 Then
                                .GrossValue = RoundValue((.SalePrice / ((_productRateDetail.InventoryProduct.GeneralLedgerIVA.Percentage / 100.0) + 1)), _roundingType)
                                .TaxValue = RoundValue(If(_productRateDetail.InventoryProduct.GeneralLedgerIVA.Percentage = 0, 0, (.SalePrice - .GrossValue)), _roundingType)
                            Else
                                .GrossValue = .SalePrice
                                .TaxValue = 0
                            End If
                        Else
                            errorsPharmaceutical.AppendLine("No se encontro tarifa para el producto " + itemPhysical.CodeNameProduct)
                            Continue For
                        End If
                    End With
                    PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Add(PharmaceuticalDispensingDetailBatchSerial)
                    PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(PharmaceuticalDispensingDetail)
                End If
            Next
        Next

        'se la cantidad entregada es 0 
        Dim itemDeliver = (From x In ListViewDashboardPharmacyDetail Where x.CantidadEntregada = 0).ToList()
        For Each item In itemDeliver
            Dim DetailPharmaceutical = (From x In PharmaceuticalDispensing.PharmaceuticalDispensingDetail Where x.MedicamentCode = item.MedicamentCode Select x).SingleOrDefault()
            If DetailPharmaceutical IsNot Nothing Then
                DetailPharmaceutical.CantidadSolicitada += item.CantidadSolicitada
                DetailPharmaceutical.CantidadPendiente += item.CantidadPendiente
            End If
        Next

        'Se obtienen todos los diferidos
        For Each item In (From x In ListViewDashboardPharmacyDetail Where x.ListDeferred IsNot Nothing AndAlso x.ListDeferred.Count > 0)
            TempListDeferred.AddRange(item.ListDeferred)
        Next

        If errorsPharmaceutical.Length > 0 Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = errorsPharmaceutical.ToString()
            Exit Sub
        End If

        If listPhysical IsNot Nothing AndAlso listPhysical.Count > 0 Then
            listPharmaceuticalDispensing.Add(PharmaceuticalDispensing)
        End If

        Using model As New MDashBoardPharmacy(MyTag)

            GenerateListCrystal()

            Dim result = Await model.SaveDispensingByPatientMedilaser(ListProductsByCODCONCEC, listPharmaceuticalDispensing, Nothing, CareCenterCode,
                                                                                      WarehouseId, CareGroupId, BillingAuthorizationId,
                                                                                      DateItem, Number, TempListDeferred, True)

            If result.StatusCode = eStatusResult.SUCCESS Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Informacion) = result.Message

                If listPhysical IsNot Nothing AndAlso listPhysical.Count > 0 Then
                    OpenSelectedReport(result.ObjectEmbbeded.InvoiceId, result.ObjectEmbbeded.AdmissionNumber, result.ObjectEmbbeded.InvoiceNumber)
                End If

                Deshacer()
            Else
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que abre el form modal para seleccionar el tipo de reporte
    ''' </summary>
    Private Sub OpenSelectedReport(InvoiceId As Integer, AdmissionNumber As String, InvoiceNumber As String)
        Using Formulario As New FrmSelectedReport
            AddHandler Formulario.SetTypeReport, AddressOf ReturnSelectedReport
            Formulario.ToolBar.Visible = False
            Formulario.StartPosition = FormStartPosition.CenterParent
            Formulario.InvoiceId = InvoiceId
            Formulario.AdmissionNumber = AdmissionNumber
            Formulario.InvoiceNumber = InvoiceNumber
            Dim size As System.Drawing.Size
            size.Width = 455
            size.Height = 245
            Formulario.Size = size
            Dim transParent As New FrmTransparent(Formulario, False)
            transParent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Retorna el porcentaje que se le aplica al item
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReturnSelectedReport(Senders As Object, e As SelectReportEventArgs)
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        'Si el tipo de reporte es tirilla 1
        If e.TypeReport = 1 Then
            Dim reportDef As New Reporter.rptSaleInvoiceReducedByDispensing
            AddHandler reportDef.AfterPrint, Sub()
                                                 If waitForm.IsSplashFormVisible Then
                                                     waitForm.CloseWaitForm()
                                                 End If
                                             End Sub
            ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, e.InvoiceId, e.AdmissionNumber)
        Else 'Si el tipo de reporte es media carta 2
            Dim reportDef As New Reporter.rptSubSaleInvoiceAll
            Dim parametrosReporte = New Object() {Nothing,                 'INDDateStart.EditValue,
                                                  Nothing,                 'INDDateEnd.EditValue,
                                                  0,                       'INDGleTypeInvoice.EditValue,
                                                  3,                       'INDGleStatus.EditValue,
                                                  e.InvoiceNumber,         'INDSleInitialInvoice.EditValue,
                                                  e.InvoiceNumber,         'INDSleFinalInvoice.EditValue,
                                                  Nothing,                 'INDSleHealthAdministrator.EditValue,
                                                  Nothing,                 'INDSlePatient.EditValue,
                                                  Nothing,                 'INDSleGroup.EditValue,
                                                  Nothing,                 'INDSleCategories.EditValue,
                                                  Nothing,                 'INDSleThirdParty.EditValue,
                                                  Nothing,                 'INDSleBranchOffice.EditValue,
                                                  Nothing,                 'INDsleAdmissionNumber.EditValue,
                                                  Nothing,                 'INDSleRadicated.EditValue,
                                                  Nothing,                 'INDSleUser.EditValue,
                                                  Nothing}                 'INDGleOrder.EditValue

            AddHandler reportDef.AfterPrint, Sub()
                                                 If waitForm.IsSplashFormVisible Then
                                                     waitForm.CloseWaitForm()
                                                 End If
                                             End Sub
            ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, parametrosReporte)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que genera el listado de crystal para enviar al sp
    ''' </summary>
    Private Sub GenerateListCrystal()
        ListProductsByCODCONCEC = New List(Of SP_ListHCPRESCRDByCODCONCEC_Result)
        For Each item In ListViewDashboardPharmacyDetail
            Dim entity As New SP_ListHCPRESCRDByCODCONCEC_Result
            With entity
                .NUMINGRES = ""
                .IPCODPACI = PatientXpo.IPCODPACI.Trim()
                .IPPRINOMB = PatientXpo.IPPRINOMB.Trim()
                .IPSEGNOMB = PatientXpo.IPSEGNOMB.Trim()
                .IPPRIAPEL = PatientXpo.IPPRIAPEL.Trim()
                .IPSEGAPEL = PatientXpo.IPSEGAPEL.Trim()
                .IPNOMCOMP = PatientXpo.IPNOMCOMP.Trim()
                .CODCENATE = ""
                .UFUCODIGO = ""
                .UFUDESCRI = ""
                .GENCONENTITY = 0
                .CODCONTRA = ""
                .CODPANATE = ""
                .CODPRODUC = item.MedicamentCode
                .DESPRODUC = item.MedicamentName
                .CANPEDPRO = item.CantidadSolicitada
                .TIPPRODUC = item.TypeProduct
                .NOPOSPROD = False
                .CODUNIMED = ""
                .CODPROSAL = ""
                .NOMMEDICO = ""
                .CODIGONITPROFSAL = ""
                .IDETIPHIS = ""
                .NUMEFOLIO = ""
                .CODESPEC1 = ""
                .DESESPECI = ""
                .IsDeferred = item.IsDeferred
                .IPSCode = INDsleIPS.EditValue
                .DateFormulation = Me.FormulationDate
                .PerformsHealthProfessionalThirdPartyId = Me.PerformsHealthProfessionalThirdPartyId
                .TreatmentDays = item.TreatmentDays
                .DiagnosticCode = item.DiagnosticCode
                .AuthorizationNumber = item.AuthorizationNumber
                .IDMipres = item.IDMipres
            End With
            ListProductsByCODCONCEC.Add(entity)
        Next
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As String
        Dim errors As New StringBuilder

        If String.IsNullOrEmpty(IPSCode) Then
            errors.AppendLine("Debe seleccionar una IPS")
        End If

        If String.IsNullOrEmpty(CareCenterCode) Then
            errors.AppendLine("Debe seleccionar un centro de atención")
        End If

        If WarehouseId = Nothing Then
            errors.AppendLine("Debe seleccionar un almacén")
        End If

        If CareGroupId = Nothing Then
            errors.AppendLine("Debe seleccionar un grupo de atención")
        End If

        If BillingAuthorizationId = Nothing Then
            errors.AppendLine("Debe seleccionar una autorización de facturación")
        End If

        If DateItem Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha")
        End If

        If ListViewDashboardPharmacyDetail Is Nothing OrElse ListViewDashboardPharmacyDetail.Count = 0 Then
            errors.AppendLine("No hay productos agregados a la rejilla")
        End If

        'If ListViewDashboardPharmacyDetail IsNot Nothing AndAlso ListViewDashboardPharmacyDetail.Count > 0 Then
        '    If ListViewDashboardPharmacyDetail.Count = (From x In ListViewDashboardPharmacyDetail Where x.CantidadEntregada = 0).Count Then
        '        errors.AppendLine("No hay productos en la rejilla con cantidad a entregar")
        '    End If
        'End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Abre el form que agrega los productos
    ''' </summary>
    ''' <param name="_editMode"></param>
    Private Sub OpenAddProduct(_editMode As Boolean)
        If INDsleWarehouse.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un almacén"
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Form As New FrmPopupProductsDispensing(_editMode, PatientXpo, Convert.ToDateTime(INDdteDateItem.EditValue).ToString("dd/MM/yyyy"))
            AddHandler Form.GetCUM, AddressOf ReturnGetCUM
            Form.EditMode = _editMode
            Form.WareHouseId = WarehouseId
            Form.PatientCode = PatientIdentification
            Form.ProductType = 2

            If _editMode Then
                Dim rowDetail = DirectCast(INDviewGridProducts.GetFocusedRow(), Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
                Form.ViewDashboardPharmacyDetail = rowDetail
            End If

            Form.Size = New System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width * 0.57, Screen.PrimaryScreen.WorkingArea.Height * 0.7)
            Form.StartPosition = FormStartPosition.CenterParent
            Dim trasparent As New FrmTransparent(Form, False)
            Me.Cursor = Cursors.Default
            trasparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Retorno del popup que agrega el producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnGetCUM(sender As Object, e As GetCUMEventArgs)
        If e IsNot Nothing Then
            If e.EditMode = False Then 'Si esta en modo agregar
                If ListViewDashboardPharmacyDetail Is Nothing Then
                    ListViewDashboardPharmacyDetail = New List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
                End If

                'Se valida que no exista el producto en el listado
                If (From x In ListViewDashboardPharmacyDetail Where x.Producto.Trim = e.ViewDashboardPharmacyDetail.Producto.Trim Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El producto " + e.ViewDashboardPharmacyDetail.Producto.Trim + " ya existe en el listado"
                    Exit Sub
                End If

                ListViewDashboardPharmacyDetail.Add(e.ViewDashboardPharmacyDetail)
                INDgcProducts.DataSource = Nothing
                INDgcProducts.DataSource = ListViewDashboardPharmacyDetail
            Else 'Si esta en modo edicion
                Dim rowDetail = DirectCast(INDviewGridProducts.GetFocusedRow(), Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
                rowDetail.ListPhysicalInventory = Nothing
                rowDetail.ListPhysicalInventory = e.ListPhysicalInventory
                rowDetail.CantidadSolicitada = e.RequestQuantity
                rowDetail.CantidadEntregada = e.DeliveryQuantity
                rowDetail.CantidadPendiente = e.PendingQuantity
                rowDetail.ListDeferred = e.ListDeferred
                rowDetail.TreatmentDays = e.ViewDashboardPharmacyDetail.TreatmentDays
                rowDetail.DiagnosticCode = e.ViewDashboardPharmacyDetail.DiagnosticCode
            End If

            INDgcProducts.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        Number = Nothing
        PatientIdentification = Nothing
        ListViewDashboardPharmacyDetail = Nothing
        INDgcProducts.DataSource = Nothing
        ActionsOnControls = False
        ListViewRealizedDispensingByManualDispensing = Nothing
        INDgcRealizedDispensing.DataSource = Nothing
        BarraBotones.StatusRecordVisible = False
        ReadOnlyControls(False)
        INDbtnAddProduct.Enabled = False

        INDsleIPS.EditValue = Nothing
        INDsleIPS.Properties.NullText = String.Empty

        INDsleCareCenter.EditValue = Nothing
        INDsleCareCenter.Properties.NullText = String.Empty

        INDsleWarehouse.EditValue = Nothing
        INDsleWarehouse.Properties.NullText = String.Empty

        INDsleCareGroup.EditValue = Nothing
        INDsleCareGroup.Properties.NullText = String.Empty

        INDsleBillingAuthorization.EditValue = Nothing
        INDsleBillingAuthorization.Properties.NullText = String.Empty

        DateItem = Me.GetDateServer()
        INDdteDateItem.Properties.MinValue = Me.DateItem.Value.AddDays(-15)
        INDdteDateItem.Properties.NullText = String.Empty

        Me.PerformsHealthProfessionalThirdPartyId = Nothing
        Me.INDsleProfessionalHealth.Properties.NullText = Nothing
        Me.FormulationDate = Me.GetDateServer()


        INDlyRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        PatientIdentification = ReturnValue
        If Not String.IsNullOrEmpty(Number) Then
            LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Redondea el valor por el tipo de redondeo
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="RoundingType"></param>
    ''' <returns></returns>
    Private Function RoundValue(value As Decimal, RoundingType As Integer) As Decimal
        Return Utils.RoundValueByTypeCurrency(value, RoundingType)
    End Function

    ''' <summary>
    ''' Abre el form de busqueda de las fórmulas médicas
    ''' </summary>
    Private Sub OpenSearchMedicalFormula()
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValueMedicalFormula
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "No. Fórmula", .FieldName = "Number", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Código Paciente", .FieldName = "PatientCode", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre Paciente", .FieldName = "PatientName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "CreationDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Number"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllMedicalFormula
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorno del form de busqueda de formulas medicas
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    Private Sub ReturnValueMedicalFormula(ReturnValue As String, ReturnObject As Object)
        Number = ReturnValue
        PatientIdentification = CType(ReturnObject, MedicalFormulaXpo).PatientCode
        LoadControls()
    End Sub

    ''' <summary>
    ''' Consulta el paciente, si existe deja pasar
    ''' </summary>
    Private Async Sub LoadControls()
        AsyncLoader(True)

        'Se valida que se haya ingresado el paciente y la formula medica para poder consultar
        Dim errors As New StringBuilder
        If String.IsNullOrEmpty(Number) Then
            errors.AppendLine("Debe ingresar una fórmula médica")
        End If
        If String.IsNullOrEmpty(PatientIdentification) Then
            errors.AppendLine("Debe ingresar un paciente")
        End If
        If errors.ToString().Length > 0 Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        'Se valida si la formula médica esta siendo utilizada por otra persona
        Dim query As New StringBuilder()
        Dim flag As Boolean = False
        Using model As New MLiquidation()

            If SessionValues.Instance.ArchitectureType = 2 Then
                query.AppendLine($"SELECT ICD.DocumentNumber, ICD.DocumentUser, P.FirstName +' '+ P.SecondName +' '+ P.FirstLastName +' '+ P.SecondLastName AS UserName FROM Inventory.InventoryControlDocument ICD WITH (NOLOCK) LEFT JOIN Security.[User] U ON ICD.DocumentUser = U.UserCode LEFT JOIN Security.Person P ON U.IdPerson = P.Id WHERE ICD.DocumentNumber = '" & Number & "' AND ICD.DocumentType = 50")
            Else
                query.AppendLine($"SELECT ICD.DocumentNumber, ICD.DocumentUser, P.FirstName +' '+ P.SecondName +' '+ P.FirstLastName +' '+ P.SecondLastName AS UserName FROM Inventory.InventoryControlDocument ICD WITH (NOLOCK) LEFT JOIN " & SessionValues.Instance.SecurityContainer & ".Security.[User] U WITH (NOLOCK) ON ICD.DocumentUser = U.UserCode LEFT JOIN " & SessionValues.Instance.SecurityContainer & ".Security.Person P WITH (NOLOCK) ON U.IdPerson = P.Id WHERE ICD.DocumentNumber = '" & Number & "' AND ICD.DocumentType = 50")
            End If

            Dim dtResult As DataTable = Await model.ExecuteCommandDt(query.ToString(), SessionValues.Instance.TransactionalContainer)
            Dim user = {indigo.UserIndigo}
            Dim _userInFormula = String.Empty
            If dtResult.Rows.Count > 0 Then
                _userInFormula = dtResult(0)("DocumentUser").ToString()
            End If
            If dtResult IsNot Nothing AndAlso dtResult.Rows.Count > 0 Then
                If user(0).Equals(_userInFormula) Then
                    flag = True
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La fórmula médica " + Number + " ya está siendo utilizada por el usuario " + dtResult(0)("DocumentUser") + " - " + dtResult(0)("UserName")
                    AsyncLoader(False)
                    Exit Sub
                End If
            End If
        End Using

        'Se consulta el paciente por código
        PatientXpo = Presenter.GetPatientByCode(PatientIdentification)

        'Se valida que el paciente exista
        If PatientXpo Is Nothing Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No existe el paciente con la identificación ingresada"
            INDbtnPatient.Focus()
            Exit Sub
        End If

        'Permite saber si se bloquean los controles
        Dim IsReadOnlyControls As Boolean = False

        'Se consulta con el paciente y la formula medica si ya se ha realizado alguna dispensación
        MedicalFormulaXpo = Presenter.GetMedicalFormulaByPatientCodeAndNumber(PatientIdentification, Number)
        If MedicalFormulaXpo IsNot Nothing Then

            'Se valida si no hay productos pendientes por entregar
            If MedicalFormulaXpo.MedicalFormulaDetailXpo.Sum(Function(x) x.PendingQuantity) = 0 Then
                Mensaje(EeventViewerImages.Informacion) = "La fórmula médica ya fue dispensada en su totalidad"
                IsReadOnlyControls = True
            End If

            'Si tiene código de ips
            If MedicalFormulaXpo.IPSCode IsNot Nothing AndAlso MedicalFormulaXpo.IPSCode <> String.Empty Then
                INDsleIPS.Properties.NullText = MedicalFormulaXpo.IPSCode
                INDsleIPS.EditValue = MedicalFormulaXpo.IPSCode
                Dim tempCodeIps = MedicalFormulaXpo.IPSCode.Split(" - ")(0).Trim()
                IPSCode = tempCodeIps.ToString().PadRight(100, " ")
                Dim IPSCodeName = Presenter.GetIPSByCode(MedicalFormulaXpo.IPSCode)
                INDsleIPS.Properties.NullText = IPSCodeName.CodeName
            End If

            'si tiene el centro de atención 
            If MedicalFormulaXpo.CareCenterCode IsNot Nothing AndAlso MedicalFormulaXpo.CareCenterCode <> String.Empty Then
                INDsleCareCenter.Properties.NullText = MedicalFormulaXpo.CareCenterCode
                INDsleCareCenter.EditValue = MedicalFormulaXpo.CareCenterCode
                Dim tempCareCenterCode = MedicalFormulaXpo.CareCenterCode.Split(" - ")(0).Trim()
                CareCenterCode = tempCareCenterCode.ToString().PadRight(100, " ")
                Dim CareCenterCodeName = Presenter.GetCenterAttentionByCode(MedicalFormulaXpo.CareCenterCode)
                INDsleCareCenter.Properties.NullText = CareCenterCodeName.CodeName
            End If

            'Se asigna el almacen
            If MedicalFormulaXpo.WarehouseId IsNot Nothing Then
                INDsleWarehouse.EditValue = MedicalFormulaXpo.WarehouseId.Id
                INDsleWarehouse.Properties.NullText = MedicalFormulaXpo.WarehouseId.CodeName
            End If

            'Se asigna el grupo de atención
            If MedicalFormulaXpo.CareGroupId IsNot Nothing Then
                INDsleCareGroup.EditValue = MedicalFormulaXpo.CareGroupId.Id
                INDsleCareGroup.Properties.NullText = MedicalFormulaXpo.CareGroupId.CodeName
            End If

            'Se asigna la autorizacion de facturacion 
            If MedicalFormulaXpo.BillingAuthorizationId IsNot Nothing Then
                INDsleBillingAuthorization.EditValue = MedicalFormulaXpo.BillingAuthorizationId.Id
                INDsleBillingAuthorization.Properties.NullText = MedicalFormulaXpo.BillingAuthorizationId.CodeName
            End If

            'Se asigna el tercero profesional de la salud
            If MedicalFormulaXpo.PerformsHealthProfessionalThirdParty IsNot Nothing Then
                Me.PerformsHealthProfessionalThirdPartyId = MedicalFormulaXpo.PerformsHealthProfessionalThirdPartyId
                Me.INDsleProfessionalHealth.Properties.NullText = MedicalFormulaXpo.PerformsHealthProfessionalThirdParty.CodeName
            End If

            'se asigna la fecha
            Me.FormulationDate = MedicalFormulaXpo.DateFormulation
            Me.DateItem = MedicalFormulaXpo.Date

            For Each item In MedicalFormulaXpo.MedicalFormulaDetailXpo
                Dim detail As New Domain.Crystal.Entities.ViewDashboardPharmacyDetail
                With detail
                    .Ingreso = item.AdmissionNumber
                    .Entidad = item.EntityId
                    .CodigoEntidad = .Entidad
                    .CodigoContrato = item.ContractCode
                    .CodigoPlan = item.PlanCode

                    .Producto = item.ProductCode + " - " + item.ProductName
                    .MedicamentCode = item.ProductCode
                    .MedicamentName = item.ProductName
                    'End If

                    .Tipo = "1"
                    .CantidadSolicitada = item.RequestQuantity
                    .CantidadEntregada = 0
                    .CantidadPendiente = item.PendingQuantity
                    .QuantityAsignedByHEON = item.DeliveryQuantity
                    .Unico = False
                    .NOPOS = item.NoPos
                    .UnidadMedida = item.MeasurementUnitCode
                    .Medico = item.HealthProfessionalCode + " - " + item.HealthProfessionalName
                    .NitMedico = item.HealthProfessionalNit
                    .FilaSeleccionada = 0
                    .IDETIPHIS = item.IdeTipHis
                    .NUMEFOLIO = item.NumFolio
                    .Opcion = "0"
                    .OpcionAnulado = "0"
                    .Procedimiento = ""
                    .MarcarOpcion = False
                    .CodigoPaciente = item.PatientCode
                    .Consecutivo = 0
                    .Estado = 1
                    .Especialidad = item.SpecialtyCode + " - " + item.SpecialtyName
                    .MedicalFormulaDetailId = item.Id
                    .IsDeferred = item.IsDeferred
                    .TreatmentDays = item.TreatmentDays
                    .DiagnosticCode = item.DiagnosticCode
                    .IDMipres = item.IDMipres
                    .AuthorizationNumber = item.AuthorizationNumber

                    'Si el producto que se va recorriendo de la formula tiene diferidos
                    If item.MedicalFormulaDetailDeferredXpo IsNot Nothing AndAlso item.MedicalFormulaDetailDeferredXpo.Count > 0 Then
                        'Se realiza el listado de diferidos
                        Dim listDefrred As New List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred)
                        For Each itemDeferred In item.MedicalFormulaDetailDeferredXpo
                            Dim deferred As New Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred
                            deferred.Id = itemDeferred.Id
                            deferred.FirstDeliveryDate = itemDeferred.FirstDeliveryDate
                            deferred.DeliveryQuantityDeferred = itemDeferred.DeliveryQuantityDeferred
                            deferred.Periodicity = itemDeferred.Periodicity
                            deferred.ProductCode = item.ProductCode
                            deferred.Number = itemDeferred.Number
                            deferred.DeliveryDate = itemDeferred.DeliveryDate
                            deferred.DeliveryQuantity = itemDeferred.DeliveryQuantity
                            deferred.PendingQuantity = itemDeferred.PendingQuantity
                            listDefrred.Add(deferred)
                        Next
                        .ListDeferred = listDefrred

                        'Dim listTemp = (From x In listDefrred Where x.DeliveryDate <= GetDateServer() Select x).ToList()
                        '.CantidadSolicitada = listTemp.Sum(Function(x) x.DeliveryQuantity)
                        '.CantidadPendiente = listTemp.Sum(Function(x) x.PendingQuantity)
                    End If

                End With
                If ListViewDashboardPharmacyDetail Is Nothing Then
                    ListViewDashboardPharmacyDetail = New List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
                End If
                ListViewDashboardPharmacyDetail.Add(detail)
            Next

            INDgcProducts.DataSource = Nothing
            INDgcProducts.DataSource = ListViewDashboardPharmacyDetail

            'Se realiza la consulta para mostrar en la rejilla las dispensaciones que se le han realizado a la formula médica
            BarraBotones.StatusRecordVisible = True
            BarraBotones.ControlHideStatus = False
            INDviewRealizedDispensing.ShowLoadingPanel()
            bwGetRealizedDispensing.RunWorkerAsync()
        End If

        'Se agrega la formula medica a la tabla de control
        query.Clear()
        query.AppendLine($"insert into Inventory.InventoryControlDocument(DocumentNumber, DocumentType, DocumentUser, DocumentDate) values('{Number}', 50, '{indigo.UserIndigo}', '{Date.Now().Year}-{Date.Now().Day}-{Date.Now().Month}')")
        Using model As New MLiquidation()
            Dim resultControl = Await model.ExecuteQuery(query.ToString(), SessionValues.Instance.TransactionalContainer)
            If Not resultControl Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un error guardando la fórmula médica en la tabla de control"
                Deshacer()
                Exit Sub
            End If
        End Using

        AsyncLoader(False)

        'Se muestra el botón de procesar
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False

        'Se habilita los controles
        ActionsOnControls = True

        'Si ya hay registros con la formula medica
        If MedicalFormulaXpo IsNot Nothing Then
            INDbtnAddProduct.Enabled = False
        End If

        'Si se debe bloquear los controles
        If IsReadOnlyControls Then
            ReadOnlyControls(True)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True
            INDpopupRealizedDispensing.Enabled = True
            INDlyRealizedDispensing.Enabled = True
            LayoutControlGroup2.Enabled = True
            INDgcRealizedDispensing.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro de la tabla de control
    ''' </summary>
    Private Async Sub DeleteBlockRecord()
        Dim query As New StringBuilder()
        query.Clear()
        query.AppendLine($"delete from Inventory.InventoryControlDocument where DocumentNumber = '{Number}' and DocumentType = 50")
        Using model As New MLiquidation()
            Dim resultControl = Await model.ExecuteQuery(query.ToString(), SessionValues.Instance.TransactionalContainer)
            If Not resultControl Then
                Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un error eliminando la fórmula médica en la tabla de control"
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Elimina un producto de la rejilla
    ''' </summary>
    Private Sub DeleteProduct()
        Dim rowDetail = DirectCast(INDviewGridProducts.GetFocusedRow(), Domain.Crystal.Entities.ViewDashboardPharmacyDetail)

        'Se valida que el producto no haya tenido alguna formula medica
        If rowDetail.MedicalFormulaDetailId > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El producto no se puede eliminar porque ya fue agregado a una fórmula médica"
            Exit Sub
        End If

        ListViewDashboardPharmacyDetail.Remove(rowDetail)
        INDgcProducts.DataSource = Nothing
        INDgcProducts.DataSource = ListViewDashboardPharmacyDetail
    End Sub

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Procesar solicitud
    ''' </summary>
    Private Sub BarraBotones_ClickProcesar() Handles BarraBotones.ClickProcesar
        ProccessDispensing()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnPatient.ButtonClick
        OpenSearch()
    End Sub

#End Region

End Class
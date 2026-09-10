'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-01-2015
'
' Last Modified By : Carlos Mario Arias Rubiano
' Last Modified On : 06/07/2015
' Description      : Se sugieren por defecto los campos de tipo de liquidación,
'                    aplica recurso y grupo de atención, todos en el form detalle.
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Accounting
Imports System.Text
Imports Presentation.Inventory.MVP
Imports Presentation.Payroll
Imports Presentation.Common
Imports DevExpress.Xpo
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraBars
Imports DevExpress.XtraLayout.Utils
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Linq
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data
Imports Presentation.Inventory
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class FrmPharmaceuticalDispensing
    Implements IPharmaceuticalDispensing, ICustomizableForm

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmPharmaceuticalDispensing"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Properties and Variables"

#Region "Properties Entity"
    ''' <summary>
    ''' Obtiene o establece el codigo de la dispensación farmacéutica
    ''' </summary>
    Public Property Code As String Implements IPharmaceuticalDispensing.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el número de ingreso al paciente de la dispensación farmacéutica
    ''' </summary>
    Public Property AdmissionNumber As String Implements IPharmaceuticalDispensing.AdmissionNumber

    ''' <summary>
    ''' Tarea que se ejecuta para traer los batchserial de la dispensacíón
    ''' </summary>
    Private taskBatchSerial As Task(Of List(Of PharmaceuticalDispensingDetailBatchSerial))

    Dim dispensingTmp As PharmaceuticalDispensing

    Private listBatchSerials As New List(Of PharmaceuticalDispensingDetailBatchSerial)

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    Public Property DocumentDate As DateTime? Implements IPharmaceuticalDispensing.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As DateTime?)
            If value IsNot Nothing Then
                If value < INDdeDocumentDate.Properties.MinValue Then
                    value = INDdeDocumentDate.Properties.MinValue
                ElseIf value > INDdeDocumentDate.Properties.MaxValue Then
                    value = INDdeDocumentDate.Properties.MaxValue
                End If
            End If

            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad operativa de la dispensación farmacéutica
    ''' </summary>
    Public Property OperationgUnitId As Integer Implements IPharmaceuticalDispensing.OperationgUnitId
        Get
            Return _idOperativeUnit
        End Get
        Set(value As Integer)
            _idOperativeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Public Property Sequence As InventorySequence Implements IPharmaceuticalDispensing.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            _sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Public Property Status As String Implements IPharmaceuticalDispensing.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de productos ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteOutpatientProducts As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteOutpatientProducts)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de productos intrahospitalarios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteIntrahospitalProducts As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteIntrahospitalProducts)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de productos intrahospitalarios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteProducts As Boolean
        Get
            Dim result As Boolean
            If Me.admission IsNot Nothing Then
                If Me.admission.AdmissionType = 1 Then
                    result = Me.RequestQuoteOutpatientProducts
                Else
                    result = Me.RequestQuoteIntrahospitalProducts
                End If
            End If
            Return result
        End Get
    End Property

#End Region

#Region "Datasource Entity"
    ''' <summary>
    ''' Datasource de Ingreso del Paciente
    ''' </summary>
    Public Property AdmissionNumberDatasource As XPInstantFeedbackSource Implements IPharmaceuticalDispensing.AdmissionNumberDatasource
        Get
            Return CType(INDsleAdmissionNumber.Datasource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAdmissionNumber.Datasource = value
        End Set
    End Property
#End Region

#Region "Others"

    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object

    Private taskWait As Task

    ''' <summary>
    ''' Obtiene el layout principal
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPharmaceuticalDispensing.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IPharmaceuticalDispensing.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' tercero del paciente en la admision
    ''' </summary>
    Private _thirdPartyPatientId As Integer

    ''' <summary>
    ''' genero del tercero
    ''' </summary>
    Private _genderThirdParty As Byte

    ''' <summary>
    ''' fecha de nacimiento del paciente en la admision
    ''' </summary>
    Private _patientDate As DateTime

    ''' <summary>
    ''' Tipo de redondeo de la moneda parametrizada
    ''' </summary>
    Public _roundingType As Integer

    ''' <summary>
    ''' Id de la Unidad Operativa Seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory

    ''' <summary>
    ''' Variable que representa la entidad de parámetros de contratos
    ''' </summary>
    Dim _settingsContractXpo As Infrastructure.Data.Xpo.ContractRepository.SettingsContractXpo

    ''' <summary>
    ''' listado de tipos de liquidacion
    ''' </summary>
    Private ListLiquidationType As List(Of Tuple(Of Integer, String))

    Private ListSurchargeApply As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PPharmaceuticalDispensing

    ''' <summary>
    ''' entidad de dispensación farmacéutica
    ''' </summary>
    Private _pharmaceuticalDispensing As PharmaceuticalDispensing

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInventory

    ''' <summary>
    ''' Listado del detalle de dispensación farmacéutica
    ''' </summary>
    Public Property ListPharmaceuticalDispensingDetail As List(Of PharmaceuticalDispensingDetail) Implements IPharmaceuticalDispensing.ListPharmaceuticalDispensingDetail
        Get
            Return CType(INDgcProductoAddedEdited.DataSource, List(Of PharmaceuticalDispensingDetail))
        End Get
        Set(value As List(Of PharmaceuticalDispensingDetail))
            INDgcProductoAddedEdited.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Representa al id del grupo de atención que viene asociado a la admision
    ''' y se envia como parametro al form FrmPharmaceuticalDispensingDetail
    ''' </summary>
    ''' <remarks></remarks>
    Private CareGroupId As Integer

    ''' <summary>
    ''' Representa al codigo y nombre del grupo de atención que viene asociado a la admision
    ''' y se envia como parametro al form FrmPharmaceuticalDispensingDetail
    ''' </summary>
    ''' <remarks></remarks>
    Private CareGroupCodeName As String

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
    ''' <summary>
    ''' 
    ''' </summary>
    Dim _patientCode As String

#End Region

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        admission = Nothing
        taskWait = Nothing
        _thirdPartyPatientId = Nothing
        _patientDate = Nothing
        _genderThirdParty = Nothing
        _idOperativeUnit = Nothing
        _settingsInventory = Nothing
        ListLiquidationType = Nothing
        ListSurchargeApply = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _presenter = Nothing
        _pharmaceuticalDispensing = Nothing
        _record = Nothing
        CareGroupId = Nothing
        CareGroupCodeName = Nothing
        varImp = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmSupplyPatients control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmSupplyPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        _presenter = New PPharmaceuticalDispensing(Me)

        Try
            AsyncLoader(True)
            Deshacer()
            Await Me.LoadParameters()
            _presenter.LoadDefinitionLayout()
            _presenter.GetSequence()
            _presenter.LoadAdmissionNumber()

            Dim _listActions As New List(Of eAcciones)()
            _listActions.Add(eAcciones.Remove)
            _listActions.Add(eAcciones.Edit)
            IndigoGridView1.SetListAcction(INDgvProducts1, _listActions)
            For Each col As GridColumn In INDgvProducts1.Columns
                If col.Name = "colActions" Then
                    col.OptionsColumn.FixedWidth = True
                End If
            Next

            Dim _listActions1 As New List(Of eAcciones)()
            _listActions1.Add(eAcciones.Remove)
            _listActions1.Add(eAcciones.Edit)
            IndigoGridView2.SetListAcction(INDgvProductoAddedEdited, _listActions1)
            For Each col As GridColumn In INDgvProductoAddedEdited.Columns
                If col.Name = "colActions" Then
                    col.OptionsColumn.FixedWidth = True
                End If
            Next

            AddHandler Me.INDsleAdmissionNumber.Search.KeyDown, AddressOf INDSleAdmissionNumber_KeyDown
            AddHandler Me.INDsleAdmissionNumber.Search.QueryPopUp, AddressOf INDSleAdmissionNumber_QueryPopUp

            IndigoGridView2.MoreInfoColunmns(INDgvProductoAddedEdited)
            INDsleAdmissionNumber.PopupContainerControl = INDPccMoreInfoAdmission
            INDsleAdmissionNumber.Search.Properties.ValueMember = "AdmissionCode"
            INDsleAdmissionNumber.Search.Properties.DisplayMember = "FullNameAdmission"

            CreateLiquidationType()
            CreateListSurchargeApply()
            LoadStatus()
            GetOfficialCurrencyFromCompanySettings()
            AsyncLoader(False)

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmDistributionDirectExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmSupplyPatients_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDbteCode.Text.Trim()) Then
                    Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbteCode.Text.Trim()) Then
                    Me.BarraBotones.StatusRecordVisible = True
                    Me.NewPharmaceuticalDispensing()
                    If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
                        ActionsOnControls = True
                        INDbteCode.Enabled = False
                    End If
                Else
                    Me.LoadControls()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDSleAdmissionNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAdmissionNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDdeDocumentDate.Focus()
        End If
    End Sub
#End Region

#Region "NewSelectedValue"
    Private Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDsleAdmissionNumber.NewSelectedValue

        If e.AdmissionObject IsNot Nothing AndAlso e.AdmissionObject.GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
            If Not ValidatePatientThirdParty(e.AdmissionObject.PatientCode) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = String.Format("El paciente {0} no está creado como tercero en Indigo VIE", String.Concat(e.AdmissionObject.PatientCode.ToString().Trim(), " - ", e.AdmissionObject.PatientName.ToString().Trim()))
                INDsleAdmissionNumber.Search.EditValue = Nothing
                INDsleAdmissionNumber.SetNullText(String.Empty)
                AdmissionNumber = Nothing
                INDsbAddProduct.Enabled = False
                Exit Sub
            End If

            'valido que el ingreso no este facturado
            Using model As New MAdmissions(MyTag)
                Dim admissionTmp = model.GetAdmissionsByCodeSimple(e.AdmissionObject.AdmissionCode.ToString().Trim()).ObjectEmbbeded
                If admissionTmp.IESTADOIN = "F" Then
                    Mensaje(EeventViewerImages.Advertencia) = "El ingreso esta facturado"
                    INDsleAdmissionNumber.Search.EditValue = Nothing
                    INDsleAdmissionNumber.SetNullText(String.Empty)
                    AdmissionNumber = Nothing
                    INDsbAddProduct.Enabled = False
                    Exit Sub
                End If
            End Using
        End If

        INDsbAddProduct.Enabled = True
        SetAdmission(e.AdmissionObject)
        'ctrTmp.PrintInfo()
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAddProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddProduct_Click(sender As Object, e As EventArgs) Handles INDsbAddProduct.Click
        OpenFormPharmaceuticalDetail(False)
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag)
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                OpenFormPharmaceuticalDetail(True, INDgvProducts1)
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeletePharmaCeuticalDetail(True)
        End Select
    End Sub
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag)
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                OpenFormPharmaceuticalDetail(True, INDgvProductoAddedEdited)
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeletePharmaCeuticalDetail()
        End Select
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDSleAdmissionNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAdmissionNumber_QueryPopUp(sender As Object, e As CancelEventArgs)
        INDsleAdmissionNumber.SetEditValue = AdmissionNumber
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDrpPceLote control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDrpPceLote_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrpPceLMoreInfo.QueryPopUp
        Dim aux = INDgvProducts1.GetFocusedRow()
        If TypeOf aux Is NotLoadedObject Then
            e.Cancel = True
            System.Windows.Forms.Application.DoEvents()
        End If
        Dim pharmaceuticalDetail = CType(aux, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow

        If listBatchSerials Is Nothing AndAlso taskBatchSerial IsNot Nothing Then
            listBatchSerials = Await taskBatchSerial
        End If

        Dim batchSerials As List(Of PharmaceuticalDispensingDetailBatchSerial) = listBatchSerials.Where(Function(db) db.PharmaceuticalDispensingDetailId = pharmaceuticalDetail.Id).ToList()

        If batchSerials.Count > 0 Then
            Dim listBatchSerialDatasource As New List(Of BatchSerial)()

            For Each item As PharmaceuticalDispensingDetailBatchSerial In batchSerials.Where(Function(bs) bs.PhysicalInventory IsNot Nothing AndAlso bs.PhysicalInventory.Id > 0)
                Dim batch = item.PhysicalInventory.BatchSerial
                If batch IsNot Nothing Then
                    batch.Quantity = item.Quantity
                    listBatchSerialDatasource.Add(batch)
                End If
            Next

            For Each item As PharmaceuticalDispensingDetailBatchSerial In batchSerials.Where(Function(bs) bs.PhysicalInventoryCustody IsNot Nothing AndAlso bs.PhysicalInventoryCustody.Id > 0)
                Dim batch = item.PhysicalInventoryCustody.BatchSerial
                If batch IsNot Nothing Then
                    batch.Quantity = item.Quantity
                    listBatchSerialDatasource.Add(batch)
                End If
            Next
            INDgcBatchSerial.DataSource = Nothing
            INDgcBatchSerial.DataSource = listBatchSerialDatasource
            If listBatchSerialDatasource.Count > 0 Then
                INDlcgBatchSerial.Visibility = LayoutVisibility.Always
            Else
                INDlcgBatchSerial.Visibility = LayoutVisibility.Never
            End If
        Else
            INDlcgBatchSerial.Visibility = LayoutVisibility.Never
        End If

        With pharmaceuticalDetail
            INDtxtCareGroup.Text = .CodeNameCareGroup
            INDtxtWareHouse.Text = .CodeNameWareHouse
            INDtxtDispensationDate.Text = .ServiceDate
            INDtxtHealthProfessional.Text = .CodeNameHealthProfessional
            'INDtxtHealthProfessionalSpecialty.Text = .CodeNameHealthProfessionalSpeciality
            INDtxtAuthorizatNumber.Text = .AuthorizationNumber
            INDtxtFunctionalUnit.Text = .FullNameFunctionalUnit
            INDtxtLiquidation.EditValue = .LiquidationType

            INDliCups.Visibility = LayoutVisibility.Never
            'If String.IsNullOrEmpty(.CodeNameCups) Then
            '    INDliCups.Visibility = LayoutVisibility.Never
            'Else
            '    INDliCups.Visibility = LayoutVisibility.Always
            '    INDtxtCups.Text = .CodeNameCups
            'End If
            INDgleApplyRecharge.EditValue = .SurchargeApply
            INDtxtSalePrice.EditValue = .SalePrice
            INDtxtPromCost.EditValue = .AverageCost
            INDtxtSubTotal.EditValue = .TotalSalesPrice
            INDtxtDiscountPercent.EditValue = .DiscountPercentage
            INDtxtDiscountValue.EditValue = .DiscountValue
            INDtxtQuotation.EditValue = .QuotationCode
        End With
    End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPharmaceuticalDispensing_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbteCode.Focus()
    End Sub

    Private _dataLoaded As Boolean = False

#End Region

#Region "ButtonClick"

    Private Sub INDFpAdmission_ButtonClick(sender As Object, e As DevExpress.Utils.FlyoutPanelButtonClickEventArgs) Handles INDFpAdmission.ButtonClick
        INDFpAdmission.HideBeakForm()
    End Sub

#End Region

#Region "MouseEnterAdmission"

    Private Sub INDsleAdmissionNumber_MouseEnterAdmission(sender As Object, e As EventArgs) Handles INDsleAdmissionNumber.MouseEnterAdmission
        If admission IsNot Nothing Then
            INDFpAdmission.ShowBeakForm()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Permite mostrar el botón de importar cotización
    ''' </summary>
    Private Sub ShowHidePermissionImportButton()
        If (From x In BarraBotones.PermissionsForm Where x.Key = 95 Select x).Count > 0 Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.ImportarInformacion, "Importar Cotización")
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    ''' <summary>
    ''' Método que abre el form de importar la información
    ''' </summary>
    Private Sub OpenFormImport()
        If admission Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un ingreso"
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmImportQuotationPharmaceutical()
            AddHandler Formulario.ImportEvent, AddressOf ImportInfo
            Formulario.PatientCode = _patientCode
            Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1054
            Formulario.Height = 500
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ImportInfo(e As AddImportQuotationPharmaceuticalDispensingDetail)
        If e IsNot Nothing AndAlso e.ListQuotationPharmaceuticalDispensingDetailXpo IsNot Nothing AndAlso e.ListQuotationPharmaceuticalDispensingDetailXpo.Count > 0 Then
            INDgvProductoAddedEdited.ShowLoadingPanel()
            Task.Factory.StartNew(Sub() ListDetailsImport(e.ListQuotationPharmaceuticalDispensingDetailXpo))
        End If
    End Sub

    Private Sub ListDetailsImport(listXpo As List(Of QuotationPharmaceuticalDispensingDetailXpo))
        CheckForIllegalCrossThreadCalls = False

        If ListPharmaceuticalDispensingDetail Is Nothing Then
            ListPharmaceuticalDispensingDetail = New List(Of PharmaceuticalDispensingDetail)
        End If

        Dim itemsRepeats As New StringBuilder

        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each entityXpo In listXpo

                If (From x In ListPharmaceuticalDispensingDetail Where x.QuotationPharmaceuticalDispensingDetailId = entityXpo.Id Select x).Count > 0 Then
                    itemsRepeats.AppendLine("El producto " + entityXpo.ProductId.CodeName + " no se puede agregar porque ya existe en la lista")
                    Continue For
                End If

                Dim pdd As New PharmaceuticalDispensingDetail
                With pdd
                    .QuotationPharmaceuticalDispensingDetailId = entityXpo.Id
                    .CareGroupId = entityXpo.CareGroupId.Id
                    .CodeNameCareGroup = entityXpo.CareGroupId.CodeName
                    .ProductId = entityXpo.ProductId.Id
                    .CodeProduct = entityXpo.ProductId.Code
                    .NameProduct = entityXpo.ProductId.CodeName
                    .WarehouseId = entityXpo.WarehouseId.Id
                    .CodeNameWareHouse = entityXpo.WarehouseId.CodeName
                    If entityXpo.HealthAdministratorId IsNot Nothing Then
                        .HealthAdministratorId = entityXpo.HealthAdministratorId.Id
                    End If
                    If entityXpo.ThirdPartyId IsNot Nothing Then
                        .ThirdPartyId = entityXpo.ThirdPartyId.Id
                    End If
                    .Quantity = entityXpo.Quantity
                    .ReturnedQuantity = 0
                    .ServiceDate = entityXpo.ServiceDate
                    .FunctionalUnitId = entityXpo.FunctionalUnitId.Id
                    .FullNameFunctionalUnit = entityXpo.FunctionalUnitId.Code + " - " + entityXpo.FunctionalUnitId.Name
                    .OrderedHealthProfessionalCode = entityXpo.OrderedHealthProfessionalCode
                    .OrderedProfessionalSpecialty = entityXpo.OrderedProfessionalSpecialty
                    If entityXpo.OrderedHealthProfessionalThirdPartyId IsNot Nothing Then
                        .OrderedHealthProfessionalThirdPartyId = entityXpo.OrderedHealthProfessionalThirdPartyId.Id
                        .CodeNameHealthProfessional = entityXpo.OrderedHealthProfessionalCode + " - " + entityXpo.OrderedHealthProfessionalThirdPartyId.Name
                    End If
                    .AuthorizationNumber = entityXpo.AuthorizationNumber
                    .LiquidationType = entityXpo.LiquidationType
                    If entityXpo.CUPSEntityId IsNot Nothing Then
                        .CupsEntityId = entityXpo.CUPSEntityId.Id
                        .CodeNameCups = entityXpo.CUPSEntityId.CodeDescription
                    End If
                    .SurchargeApply = entityXpo.SurchargeApply
                    .SalePrice = entityXpo.SalePrice
                    .AverageCost = entityXpo.AverageCost
                    .TotalSalesPrice = entityXpo.TotalSalesPrice
                    .GrandTotalSalesPrice = entityXpo.GrandTotalSalesPrice
                    .DiscountPercentage = entityXpo.DiscountPercentage
                    .DiscountValue = entityXpo.DiscountValue
                    .QuotationCode = entityXpo.QuotationId.Code

                    If entityXpo.QuotationPharmaceuticalDispensingDetailBatchSerialXpo IsNot Nothing AndAlso entityXpo.QuotationPharmaceuticalDispensingDetailBatchSerialXpo.Count > 0 Then
                        For Each item In entityXpo.QuotationPharmaceuticalDispensingDetailBatchSerialXpo
                            Dim batchSerial As New PharmaceuticalDispensingDetailBatchSerial
                            batchSerial.PhysicalInventoryId = item.PhysicalInventoryId
                            batchSerial.Quantity = item.Quantity
                            batchSerial.OutstandingQuantity = item.OutstandingQuantity
                            batchSerial.PhysicalInventoryCustodyId = item.PhysicalInventoryCustodyId
                            .PharmaceuticalDispensingDetailBatchSerial.Add(batchSerial)
                        Next
                    End If
                End With

                _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(pdd)
                ListPharmaceuticalDispensingDetail.Add(pdd)
            Next
        End If

        'Se asigna que no afecta inventario ya que todas cotizaciones se realizan sobre almacenes virtuales
        INDGleAffectInventory.EditValue = False
        INDGleAffectInventory.Properties.ReadOnly = True

        INDgvProductoAddedEdited.HideLoadingPanel()
        INDgcProductoAddedEdited.RefreshDataSource()

        INDliProductoAddedEdited.Visibility = LayoutVisibility.Always
        FormatEmptyGrid(False, If(INDliProducto1.Visibility = LayoutVisibility.Never, True, False))

        If Not String.IsNullOrEmpty(itemsRepeats.ToString()) Then
            Mensaje(EeventViewerImages.Advertencia) = itemsRepeats.ToString()
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            'parámetos de contratos
            _settingsContractXpo = _presenter.GetSettingsContractByOperatingUnitId(_idOperativeUnit)

            'parámetros de inventario
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
                Deshacer()
                Exit Function
            End If

            Me.ValidateDate()
        End Using
    End Function

    ''' <summary>
    ''' Consulta la fecha de los parametros y establece la fecha minima y maxima de la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = New DateTime(_settingsInventory.Year, _settingsInventory.Month, 1)
        INDdeDocumentDate.Properties.MinValue = dateMin
        INDdeDocumentDate.Properties.MaxValue = dateMin.AddMonths(1).AddDays(-1)
    End Sub

    ''' <summary>
    ''' Da formato a la rejilla de registros sin guardar cuando no tiene registros
    ''' </summary>
    ''' <param name="isEmpty">Valor que indica si la rejilla esta vacia</param>
    ''' <param name="isNew">Valor que indica si la dispensación es nueva</param>
    Private Sub FormatEmptyGrid(Optional ByVal isEmpty As Boolean = True, Optional ByVal isNew As Boolean = False)
        Dim caption As String = "Registros Sin Guardar"
        Dim gridMaxSize As New Size(828, 150)
        Dim gridMinSize As New Size(828, 0)
        If isEmpty Then
            gridMaxSize = New Size(828, 30)
            gridMinSize = New Size(828, 30)
            caption = "No hay Registros"
        ElseIf isNew Then
            gridMaxSize = New Size(828, 0)
            gridMinSize = New Size(828, 150)
        End If
        INDliProductoAddedEdited.MinSize = gridMinSize
        INDliProductoAddedEdited.MaxSize = gridMaxSize
        INDliProductoAddedEdited.Text = caption
    End Sub

    ''' <summary>
    ''' Validates the patient third party.
    ''' </summary>
    ''' <param name="patientCode">The patient code.</param>
    ''' <returns></returns>
    Private Function ValidatePatientThirdParty(patientCode As String) As Boolean
        If Not String.IsNullOrEmpty(patientCode.Trim()) Then
            Using model As New MThirdParty(Me.Tag)
                Dim third As Domain.Entities.ThirdParty = model.GetThirdParty(patientCode.Trim())
                If third IsNot Nothing AndAlso third.Id > 0 Then
                    _thirdPartyPatientId = third.Id
                    If third.Person.Gender Is Nothing OrElse third.Person.BirthDate Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = "Existen campos de la información del paciente que deben ser diligenciados"
                        INDsleAdmissionNumber.Search.EditValue = Nothing
                        INDsleAdmissionNumber.SetNullText(String.Empty)
                        AdmissionNumber = Nothing
                        INDsbAddProduct.Enabled = False
                        Return True
                    End If
                    _genderThirdParty = third.Person.Gender
                    _patientDate = third.Person.BirthDate
                    Else
                        Return False
                End If
            End Using
        Else
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' News the distribution fixed asset.
    ''' </summary>
    Private Async Function NewPharmaceuticalDispensing() As Task

        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
            Exit Function
        End If

        _pharmaceuticalDispensing = New PharmaceuticalDispensing()
        Me.DocumentDate = GetDateServer()
        INDliProducto1.Visibility = LayoutVisibility.Never
        INDliProductoAddedEdited.Visibility = LayoutVisibility.Always

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            ShowHidePermissionImportButton()
            Me.SaveAndConfirmObligatory()
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                    Exit Function
                End If
            End If

            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                ShowHidePermissionImportButton()
                Me.SaveAndConfirmObligatory()
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        ShowHidePermissionImportButton()
                        Me.SaveAndConfirmObligatory()
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
                            ShowHidePermissionImportButton()
                            Me.SaveAndConfirmObligatory()
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
                    ShowHidePermissionImportButton()
                    Me.SaveAndConfirmObligatory()
                End If
            End If

        End If
    End Function

    Private Sub SaveAndConfirmObligatory()
        If Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar)) Then
            Exit Sub
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar))
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(Me.Tag)
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Creates the type of the liquidation.
    ''' </summary>
    Private Sub CreateLiquidationType()
        ListLiquidationType = New List(Of Tuple(Of Integer, String))()
        ListLiquidationType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("LiquidationTypeRateManual", MODULE_NAME)))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("LiquidationTypeIPS", MODULE_NAME)))
        INDtxtLiquidation.Properties.DataSource = ListLiquidationType
    End Sub

    ''' <summary>
    ''' Creates the list surcharge apply.
    ''' </summary>
    Private Sub CreateListSurchargeApply()
        ListSurchargeApply = New List(Of Tuple(Of Boolean, String))()
        ListSurchargeApply.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListSurchargeApply.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDgleApplyRecharge.Properties.DataSource = ListSurchargeApply
    End Sub

    ''' <summary>
    ''' Deletes the pharma ceutical detail.
    ''' </summary>
    Private Async Sub DeletePharmaCeuticalDetail(Optional ByVal isXpo As Boolean = False)
        If isXpo Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim pharmaceuticalDetail As PharmaceuticalDispensingDetail = Await GetPharmaceuticalDispensingDetailFromGrid(INDgvProducts1.FocusedRowHandle, False)

                Dim idx As Integer = GetPharmaDetailIndexOf(_pharmaceuticalDispensing.PharmaceuticalDispensingDetail.ToList(), pharmaceuticalDetail.Id)

                If idx > -1 Then
                    _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.RemoveAt(idx)
                End If

                Dim auxList As List(Of PharmaceuticalDispensingDetailBatchSerial) = Await ListBathserialsFromDetail(pharmaceuticalDetail.Id)

                pharmaceuticalDetail.MarkAsDeleted()

                For i = 0 To auxList.Count - 1
                    auxList(i).MarkAsDeleted()
                    pharmaceuticalDetail.PharmaceuticalDispensingDetailBatchSerial.Add(auxList(i))
                Next

                _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(pharmaceuticalDetail)

                _pharmaceuticalDispensing.MarkAsModified()

                ListPharmaceuticalDispensingDetail = _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.ToList()
                INDgcProductoAddedEdited.RefreshDataSource()
                If ListPharmaceuticalDispensingDetail.Count = 0 Then
                    INDGleAffectInventory.ReadOnly = False
                End If
                INDliProductoAddedEdited.Visibility = LayoutVisibility.Always
                FormatEmptyGrid(False)
            End If
        Else
            _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.RemoveAt(INDgvProductoAddedEdited.DataController.GetListSourceRowIndex(INDgvProductoAddedEdited.FocusedRowHandle))
            ListPharmaceuticalDispensingDetail = _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.ToList()
            INDgcProductoAddedEdited.RefreshDataSource()
            If _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Count = 0 Then
                FormatEmptyGrid()
                If INDliProducto1.Visibility = LayoutVisibility.Always Then
                    INDliProductoAddedEdited.Visibility = LayoutVisibility.Never
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Selecteds the value.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="SearchAdmissionClosingEventArgs"/> instance containing the event data.</param>
    Private Sub SelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs)
        INDsbAddProduct.Enabled = True
        SetAdmission(e.AdmissionObject)
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._pharmaceuticalDispensing.Code, Me._pharmaceuticalDispensing.AdmissionNumber),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._pharmaceuticalDispensing.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._pharmaceuticalDispensing.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._pharmaceuticalDispensing.Code, Me._pharmaceuticalDispensing.AdmissionNumber)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._pharmaceuticalDispensing.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Opens the form.
    ''' </summary>
    ''' <param name="form">The form.</param>
    Private Sub OpenForms(ByVal form As FormBase)
        form.ViewModeEditHold = True
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.MinimizeBox = True
        form.MaximizeBox = True
        form.Size = New Size(1100, 800)
        form.SizeGripStyle = SizeGripStyle.Show
        Dim transparent As New FrmTransparent(form, False)
        transparent.ShowDialog(Me.MdiParent)
        If form.DialogResult <> System.Windows.Forms.DialogResult.OK Then
            PharmaceuticalDispensingDetailEdit = Nothing
        End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPharmaceuticalDispensing.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDsleAdmissionNumber.Enabled = value
            INDsleAdmissionNumber.Search.Enabled = value
            INDdeDocumentDate.Enabled = value
            INDGleAffectInventory.Enabled = value
            INDgcProducts1.Enabled = value
            INDgcProductoAddedEdited.Enabled = value
            INDlcRoot.EndUpdate()
            If value Then
                INDsleAdmissionNumber.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IPharmaceuticalDispensing.CleanControls
        _dataLoaded = False
        INDlcRoot.BeginUpdate()
        ReadOnlyControls(False)
        Code = String.Empty
        AdmissionNumber = Nothing
        INDsleAdmissionNumber.IsReadOnly = False
        DocumentDate = Nothing
        INDGleAffectInventory.EditValue = True
        INDGleAffectInventory.ReadOnly = False
        INDlcRoot.EndUpdate()

        listBatchSerials = Nothing
        taskBatchSerial = Nothing
        INDTxtAdmissionPopup.Text = String.Empty
        INDsbAddProduct.Enabled = False
        ListPharmaceuticalDispensingDetail = Nothing
        INDgcProducts1.DataSource = Nothing
        FormatEmptyGrid()
        INDliProductoAddedEdited.Visibility = LayoutVisibility.Always
        INDliProducto1.Visibility = LayoutVisibility.Never
        _pharmaceuticalDispensing = Nothing
        INDsleAdmissionNumber.SetNullText(String.Empty)
        PharmaceuticalDispensingDetailEdit = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.ValidateDate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        admission = Nothing
        CleanControlsAdminssion()
        ActionsOnControls = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
    End Sub

    ''' <summary>
    ''' metodo para limpioar los controles del popup de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdminssion()
        Me.TxtAdmissionCode.Text = String.Empty
        Me.TxtFunctionalUnitAdmission.Text = String.Empty

        Me.TxtAdmissionDate.Text = String.Empty
        Me.TxtBedStay.Text = String.Empty

        Me.TxtAdmissionType.Text = String.Empty
        Me.TxtPlaceEntry.Text = String.Empty

        Me.TxtLiquidationType.Text = String.Empty
        Me.TxtAuthorization.Text = String.Empty

        Me.TxtAtentionCenter.Text = String.Empty
        Me.TxtEntityNameAdmission.Text = String.Empty

        Me.TxtResponsiblePhone.Text = String.Empty
        Me.TxtResponsibleName.Text = String.Empty

        Me.TxtPatientCode.Text = String.Empty
        Me.TxtPatientName.Text = String.Empty
        Me.TxtPatientBirth.Text = String.Empty
        Me.TxtPatientAge.Text = String.Empty
        Me.TxtCareGroupPatient.Text = String.Empty
        Me.TxtPatientEntityName.Text = String.Empty
        Me.TxtPatientEstrato.Text = String.Empty

        Me.TxtPatientType.EditValue = Nothing
        Me.TxtAfiliationType.EditValue = Nothing
        Me.TxtCareGroupPatient.Text = String.Empty
        Me.TxtCareGroupAdmission.Text = String.Empty
        Me.TxtRiskType.Text = String.Empty
        Me.TxtContact.Text = String.Empty

        admission = Nothing
    End Sub

    ''' <summary>
    ''' Sets the admission.
    ''' </summary>
    Private Async Sub SetAdmission(record As Object)
        If record Is Nothing Then
            Exit Sub
        End If
        admission = record
        With record
            Me.INDsleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), record.AdmissionCode.ToString().Trim(), record.PatientCode.ToString().Trim(), record.PatientName.ToString().Trim()))

            AdmissionNumber = .AdmissionCode.ToString() '.Trim()
            _patientCode = record.PatientCode.ToString().Trim()

            Select Case .TRATAESPECIA
                Case 2
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Renal"
                Case 3
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Oncológico"
                Case Else
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Ninguno"
            End Select

            Mensaje(EeventViewerImages.Informacion) = INDTxtAdmissionPopup.Text

            If .CareGroupId > 0 Then
                Using model As New Presentation.Contract.MVP.MCareGroup(MyTag)
                    Dim careGroup = model.GetCareGroupByIdSimple(.CareGroupId).ObjectEmbbeded
                    CareGroupId = careGroup.Id
                    CareGroupCodeName = careGroup.Code + " - " + careGroup.Name
                End Using
            End If
        End With

        'Consultamos de uns sp los ingresos
        Dim _auxAdmissionToReload As Infrastructure.Data.Xpo.CrystalRepository.ViewLiquidationGetAdmissionAll = Nothing
        Await Task.Factory.StartNew(Sub()
                                        _auxAdmissionToReload = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                                                .CrystalService.Liquidation_GetAdmission(admission.AdmissionCode.ToString().Trim())
                                    End Sub)

        Dim documentType As String = String.Empty
        Select Case _auxAdmissionToReload.PatientDocumentType.ToString().Trim()
            Case "1"
                documentType = "CC"
            Case "2"
                documentType = "CE"
            Case "3"
                documentType = "TI"
            Case "4"
                documentType = "RC"
            Case "5"
                documentType = "PA"
            Case "6"
                documentType = "AS"
            Case "7"
                documentType = "MS"
            Case "8"
                documentType = "NU"
        End Select
        Me.TxtPatientCode.Text = If(_auxAdmissionToReload.PatientCode Is Nothing, String.Empty, String.Concat(documentType, " - ", _auxAdmissionToReload.PatientCode.ToString().Trim()))
        Me.TxtPatientName.Text = If(_auxAdmissionToReload.PatientName Is Nothing, String.Empty, _auxAdmissionToReload.PatientName.ToString().Trim())
        Me.TxtPatientBirth.Text = Convert.ToDateTime(_auxAdmissionToReload.PatientBirth).ToString(SessionValues.Instance.Culture)
        Me.TxtPatientAge.Text = Utils.AgeToString(Convert.ToDateTime(_auxAdmissionToReload.PatientBirth))
        Me.TxtPatientType.EditValue = Convert.ToInt32(_auxAdmissionToReload.PatientType)
        Me.TxtAfiliationType.EditValue = Convert.ToInt32(_auxAdmissionToReload.PatientAfiliation)
        Me.TxtPatientEstrato.Text = (_auxAdmissionToReload.NivelCode & " - " & _auxAdmissionToReload.NivelName)
        'Me.TxtContacto

        'DATOS DEL INGRESO
        Me.TxtAdmissionCode.Text = _auxAdmissionToReload.AdmissionCode.ToString().Trim()
        Me.TxtAdmissionDate.Text = Convert.ToDateTime(_auxAdmissionToReload.AdmissionDate).ToString(SessionValues.Instance.Culture)
        Me.TxtEntityNameAdmission.Text = _auxAdmissionToReload.EntityName
        Select Case _auxAdmissionToReload.AdmissionRiskType
            Case "1"
                Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
            Case "2"
                Me.TxtRiskType.Text = "Accidente de Tránsito"
            Case "3"
                Me.TxtRiskType.Text = "Catástrofe"
            Case "4"
                Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
            Case "5"
                Me.TxtRiskType.Text = "Accidente de Trabajo"
            Case "6"
                Me.TxtRiskType.Text = "Enfermedad Profesional"
            Case "7"
                Me.TxtRiskType.Text = "Atención Inicial de Urgencias"
            Case "8"
                Me.TxtRiskType.Text = "Otro Tipo de Accidente"
            Case "9"
                Me.TxtRiskType.Text = "Lesión Por Agresión"
            Case "10"
                Me.TxtRiskType.Text = "Lesión AutoInfligida"
            Case "11"
                Me.TxtRiskType.Text = "Maltrato Físico"
            Case "12"
                Me.TxtRiskType.Text = "Promoción y Prevención"
            Case "13"
                Me.TxtRiskType.Text = "Otro"
            Case "14"
                Me.TxtRiskType.Text = "Accidente Rabico"
            Case "15"
                Me.TxtRiskType.Text = "Accidente Ofídico"
            Case "16"
                Me.TxtRiskType.Text = "Sopecha de Abuso Sexual"
            Case "17"
                Me.TxtRiskType.Text = "Sopecha de Violencia Sexual"
            Case "18"
                Me.TxtRiskType.Text = "Sopecha de Maltrato Emocional"
        End Select
        Me.TxtPlaceEntry.Text = ResourceManager.GetString("PlaceEntry_" & _auxAdmissionToReload.PlaceEntry.ToString(), "IndigoCrystalHis")
        Me.TxtAdmissionType.EditValue = Convert.ToInt32(_auxAdmissionToReload.AdmissionType)
        Me.TxtBedStay.Text = If(_auxAdmissionToReload.BedStay Is Nothing, String.Empty, _auxAdmissionToReload.BedStay.ToString().Trim())
        Me.TxtLiquidationType.EditValue = Convert.ToInt32(_auxAdmissionToReload.LiquidationType)
        Me.TxtAuthorization.Text = _auxAdmissionToReload.AuthorizationNumber.ToString().Trim()
        Me.TxtAtentionCenter.Text = _auxAdmissionToReload.AdmissionCentAtencCodeName
        Me.TxtFunctionalUnitAdmission.Text = _auxAdmissionToReload.AdmissionUniFuncCodeName.ToString().Trim()
        Me.TxtResponsibleName.Text = If(_auxAdmissionToReload.ResponsibleName Is Nothing, String.Empty, _auxAdmissionToReload.ResponsibleName.ToString().Trim())
        Me.TxtResponsiblePhone.Text = _auxAdmissionToReload.ResponsiblePhone

        If _auxAdmissionToReload IsNot Nothing Then
            If CInt(_auxAdmissionToReload.CareGroupTypePatient) <> -1 Then
                Select Case CByte(_auxAdmissionToReload.CareGroupTypePatient)
                    Case 1, 2, 4 'EAPB con contrato
                        Me.TxtPatientEntityName.Text = String.Concat(_auxAdmissionToReload.PatientEntityCode, " - ", _auxAdmissionToReload.PatientEntity)
                    Case 3 'Particulares
                        LiEntity.Text = ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", "CtrFolio")
                        'tercero
                        Me.TxtPatientEntityName.Text = _auxAdmissionToReload.PatientEntity
                End Select
                Me.TxtCareGroupPatient.Text = _auxAdmissionToReload.CareGroupCodeName
            End If
            Me.TxtCareGroupAdmission.Text = _auxAdmissionToReload.AdmissionCareGroupCodeName
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements IPharmaceuticalDispensing.AssigningValues
        With _pharmaceuticalDispensing
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Me.Code
            .OperatingUnitId = OperationgUnitId
            .AdmissionNumber = AdmissionNumber
            .DocumentDate = DocumentDate
            .AffectInventory = INDGleAffectInventory.EditValue
            .Status = If(Status Is Nothing, 1, CByte(Status))
            .CodePatient = _patientCode
        End With
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    Dim PharmaceuticalDispensingDetailEdit As PharmaceuticalDispensingDetail = Nothing
    Private Sub OpenFormPharmaceuticalDetail(edit As Boolean)
        OpenFormPharmaceuticalDetail(edit, Nothing)
    End Sub

    ''' <summary>
    ''' Obtiene  las propiedades de la moneda oficial
    ''' </summary>
    Public Sub GetOfficialCurrencyFromCompanySettings()
        Dim companySettings = _presenter.GetOfficialCurrencyFromCompanySettings()
        If companySettings IsNot Nothing Then
            Dim round = companySettings?.OfficialCurrency?.RoundingType
            If round IsNot Nothing Then
                _roundingType = CInt(round)
            End If
        End If
    End Sub



    ''' <summary>
    ''' Abre el formulario detalle para agregar o editar
    ''' </summary>
    ''' <param name="edit">if set to <c>true</c> [edit].</param>
    ''' <param name="source">The pharmaceutical detail.</param>
    Private Async Sub OpenFormPharmaceuticalDetail(edit As Boolean, ByVal source As GridView)
        If DocumentDate Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una fecha correcta"
            Exit Sub
        End If
        Using Formulario As New FrmPharmaceuticalDispensingDetail
            Formulario.FlagTaxInclude = Me._settingsInventory?.FlagTaxInclude
            Formulario.RequestQuoteProducts = Me.RequestQuoteProducts
            Formulario.AffectedInventory = INDGleAffectInventory.EditValue
            Formulario.AdmissionNumberHeader = AdmissionNumber
            Formulario.AdmissionDate = admission.AdmissionDate
            Formulario.ServiceDate = DocumentDate
            Formulario.ThirdPartyPatientId = _thirdPartyPatientId
            Formulario.GenderThirdParty = _genderThirdParty
            Formulario.PatientDate = _patientDate
            Formulario.FullNameThirdPartyPatient = TxtPatientCode.Text + " - " + TxtPatientName.Text
            Formulario.PatientCode = _patientCode
            Formulario._roundingType = _roundingType

            If edit Then

                If source.Name.Equals("INDgvProducts1") Then
                    PharmaceuticalDispensingDetailEdit = Await GetPharmaceuticalDispensingDetailFromGrid(source.FocusedRowHandle)
                Else
                    PharmaceuticalDispensingDetailEdit = CType(source.GetFocusedRow(), PharmaceuticalDispensingDetail)
                End If

                Formulario.PharmaceuticalDispensingDetail = PharmaceuticalDispensingDetailEdit
                Formulario.EditMode = True
                Formulario.LoadControls()
            Else
                Formulario.CleanControls(True)
                Formulario.PharmaceuticalDispensingDetail = New PharmaceuticalDispensingDetail()
                Formulario.HealthAdministratorCrystal = admission.HealthAdministratorId
                Formulario.CareGroupId = CareGroupId
                Formulario.CareGroupCodeName = CareGroupCodeName
                Formulario.AutorizationNumber = TxtAuthorization.Text
            End If

            Formulario.StatusInventory = INDGleAffectInventory.EditValue
            AddHandler Formulario.AddPharmaceuticalDispensingDetail, AddressOf AddPharmaceuticalDispensingDetail
            OpenForms(Formulario)
        End Using
    End Sub

    ''' <summary>
    ''' Valida que el producto con el almacen no existan en la rejilla de linqinstantfeedbacksource
    ''' </summary>
    ''' <param name="_productId"></param>
    ''' <param name="_warehouseId"></param>
    ''' <returns></returns>
    Private Function ValidateProductInLinqInstantFeedbackSource(_productId As Integer, _warehouseId As Integer) As Boolean
        For i = 0 To INDgvProducts1.RowCount - 1 Step 1
            Dim productId As Integer
            Dim warehouseId As Integer

            If INDgvProducts1.IsRowLoaded(i) Then
                productId = INDgvProducts1.GetRowCellValue(i, "ProductId").Id
                warehouseId = INDgvProducts1.GetRowCellValue(i, "WarehouseId")
            Else
                INDgvProducts1.EnsureRowLoaded(i, Sub(aux)
                                                      Dim pharmaceuticalDetail = CType(aux, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
                                                      productId = pharmaceuticalDetail.ProductId.Id
                                                      warehouseId = pharmaceuticalDetail.WarehouseId
                                                  End Sub)
            End If

            If productId = _productId AndAlso warehouseId = _warehouseId Then
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Adds the pharmaceutical dispensing detail.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="AddPharmaceuticalDispensingDetailEventArgs"/> instance containing the event data.</param>
    Private Sub AddPharmaceuticalDispensingDetail(sender As Object, e As AddPharmaceuticalDispensingDetailEventArgs)
        If PharmaceuticalDispensingDetailEdit Is Nothing Then
            If ListPharmaceuticalDispensingDetail IsNot Nothing AndAlso ListPharmaceuticalDispensingDetail.Count > 0 Then
                Dim productAdded = ListPharmaceuticalDispensingDetail.Find(Function(x) x.ProductId = e.PharmaceuticalDispensingDetail.ProductId And x.WarehouseId = e.PharmaceuticalDispensingDetail.WarehouseId And x.FunctionalUnitId = e.PharmaceuticalDispensingDetail.FunctionalUnitId)
                If productAdded IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El producto " + e.PharmaceuticalDispensingDetail.NameProduct + " ya esta agregado con el almacen " + e.PharmaceuticalDispensingDetail.CodeNameWareHouse + " y la unidad funcional " + e.PharmaceuticalDispensingDetail.FullNameFunctionalUnit
                    Exit Sub
                End If
            End If

            If INDgvProducts1.RowCount > 0 Then
                If ValidateProductInLinqInstantFeedbackSource(e.PharmaceuticalDispensingDetail.ProductId, e.PharmaceuticalDispensingDetail.WarehouseId) = False Then
                    Mensaje(EeventViewerImages.Advertencia) = "El producto " + e.PharmaceuticalDispensingDetail.NameProduct + " ya esta agregado con el almacen " + e.PharmaceuticalDispensingDetail.CodeNameWareHouse
                    Exit Sub
                End If
            End If
        Else
            If _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Count > 0 Then
                If PharmaceuticalDispensingDetailEdit.Id > 0 Then
                    Dim idx As Int32 = GetPharmaDetailIndexOf(_pharmaceuticalDispensing.PharmaceuticalDispensingDetail.ToList(), PharmaceuticalDispensingDetailEdit.Id)
                    If idx > -1 Then
                        _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.RemoveAt(idx)
                    End If
                Else
                    _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Remove(PharmaceuticalDispensingDetailEdit)
                End If
            End If
            PharmaceuticalDispensingDetailEdit = Nothing
        End If
        _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(e.PharmaceuticalDispensingDetail)
        ListPharmaceuticalDispensingDetail = _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.ToList()
        INDgcProductoAddedEdited.RefreshDataSource()

        'Si el almacen que se esta agregando es virtual se cambia el parámetro de Afecta Inventario en NO
        If e.VirtualStore Then
            INDGleAffectInventory.EditValue = Not e.VirtualStore
        End If

        If e.Custody Then
            INDGleAffectInventory.EditValue = e.Custody
        End If

        INDGleAffectInventory.ReadOnly = True
        If _pharmaceuticalDispensing.ChangeTracker.State <> ObjectState.Added Then
            _pharmaceuticalDispensing.MarkAsModified()
        End If
        INDliProductoAddedEdited.Visibility = LayoutVisibility.Always
        FormatEmptyGrid(False, If(INDliProducto1.Visibility = LayoutVisibility.Never, True, False))
    End Sub

    ''' <summary>
    ''' Obtiene el indice en la coleccion del detalle con id
    ''' </summary>
    ''' <param name="list">Colección a recorrer</param>
    ''' <param name="id">Id a buscar</param>
    ''' <returns>Indice en la colección</returns>
    Private Function GetPharmaDetailIndexOf(ByVal list As List(Of PharmaceuticalDispensingDetail), ByVal id As Int32) As Int32
        Dim i As Int32 = -1
        For j = 0 To list.Count - 1
            If list(j).Id = id Then
                i = j
                Exit For
            End If
        Next
        Return i
    End Function

    ''' <summary>
    ''' Genera el objeto entity del detalle de dispensación
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    Private Async Function GetPharmaceuticalDispensingDetailFromGrid(ByVal row As Int32, Optional ByVal withBatchserial As Boolean = True) As Task(Of PharmaceuticalDispensingDetail)
        Dim pdd As New Domain.Entities.PharmaceuticalDispensingDetail()

        pdd.Id = INDgvProducts1.GetRowCellValue(row, "Id")
        pdd.PharmaceuticalDispensingId = INDgvProducts1.GetRowCellValue(row, "Iddd")
        pdd.CareGroupId = INDgvProducts1.GetRowCellValue(row, "CareGroupId")
        pdd.CodeNameCareGroup = INDgvProducts1.GetRowCellValue(row, "CodeNameCareGroup")
        pdd.CodeNameWareHouse = INDgvProducts1.GetRowCellValue(row, "CodeNameWareHouse")
        pdd.CodeNameHealthProfessional = INDgvProducts1.GetRowCellValue(row, "CodeNameHealthProfessional")
        pdd.FullNameFunctionalUnit = INDgvProducts1.GetRowCellValue(row, "FullNameFunctionalUnit")
        pdd.NameProduct = INDgvProducts1.GetRowCellValue(row, "Name")

        pdd.HealthAdministratorId = INDgvProducts1.GetRowCellValue(row, "HealthAdministratorId")
        pdd.ThirdPartyId = INDgvProducts1.GetRowCellValue(row, "ThirdPartyId")
        pdd.ProductId = INDgvProducts1.GetRowCellValue(row, "ProductId").Id
        pdd.WarehouseId = INDgvProducts1.GetRowCellValue(row, "WarehouseId")
        pdd.Quantity = INDgvProducts1.GetRowCellValue(row, "Quantity")
        pdd.ReturnedQuantity = INDgvProducts1.GetRowCellValue(row, "ReturnedQuantity")
        pdd.ServiceDate = INDgvProducts1.GetRowCellValue(row, "ServiceDate")
        pdd.FunctionalUnitId = INDgvProducts1.GetRowCellValue(row, "FuFunctionalUnitId")
        pdd.OrderedHealthProfessionalCode = INDgvProducts1.GetRowCellValue(row, "OrderedHealthProfessionalCode")
        pdd.OrderedProfessionalSpecialty = INDgvProducts1.GetRowCellValue(row, "OrderedProfessionalSpecialty")
        pdd.OrderedHealthProfessionalThirdPartyId = INDgvProducts1.GetRowCellValue(row, "OrderedHealthProfessionalThirdPartyId")
        pdd.AuthorizationNumber = INDgvProducts1.GetRowCellValue(row, "AuthorizationNumber")
        pdd.LiquidationType = INDgvProducts1.GetRowCellValue(row, "LiquidationType")
        pdd.CupsEntityId = INDgvProducts1.GetRowCellValue(row, "CupsEntityId")
        pdd.SurchargeApply = INDgvProducts1.GetRowCellValue(row, "SurchargeApply")
        pdd.SalePrice = INDgvProducts1.GetRowCellValue(row, "SalePrice")
        pdd.AverageCost = INDgvProducts1.GetRowCellValue(row, "AverageCost")
        pdd.DiscountPercentage = INDgvProducts1.GetRowCellValue(row, "DiscountPercentage")
        pdd.DiscountValue = INDgvProducts1.GetRowCellValue(row, "DiscountValue")
        pdd.TotalSalesPrice = INDgvProducts1.GetRowCellValue(row, "TotalSalesPrice")
        pdd.GrandTotalSalesPrice = INDgvProducts1.GetRowCellValue(row, "GrandTotalSalesPrice")
        pdd.Custody = INDgvProducts1.GetRowCellValue(row, "Custody")
        pdd.QuotationPharmaceuticalDispensingDetailId = INDgvProducts1.GetRowCellValue(row, "QuotationPharmaceuticalDispensingDetailId")
        pdd.GrossValue = INDgvProducts1.GetRowCellValue(row, "GrossValue")
        pdd.TaxValue = INDgvProducts1.GetRowCellValue(row, "TaxValue")

        If withBatchserial Then
            If listBatchSerials Is Nothing AndAlso taskBatchSerial IsNot Nothing Then
                listBatchSerials = Await taskBatchSerial
            End If

            Dim auxList As List(Of PharmaceuticalDispensingDetailBatchSerial) = listBatchSerials.Where(Function(b) b.PharmaceuticalDispensingDetailId = pdd.Id).ToList()

            For i = 0 To auxList.Count - 1
                Dim nBatchSerial As New PharmaceuticalDispensingDetailBatchSerial()
                With nBatchSerial
                    .Id = auxList(i).Id
                    .PharmaceuticalDispensingDetailId = auxList(i).PharmaceuticalDispensingDetailId
                    .PhysicalInventoryId = auxList(i).PhysicalInventoryId
                    .PhysicalInventoryCustodyId = auxList(i).PhysicalInventoryCustodyId
                    .Quantity = auxList(i).Quantity
                    .OutstandingQuantity = auxList(i).OutstandingQuantity
                    .MarkAsUnchanged()
                End With
                pdd.PharmaceuticalDispensingDetailBatchSerial.Add(nBatchSerial)
            Next
        End If
        pdd.ChangeTracker.ObjectsAddedToCollectionProperties.Clear()
        pdd.MarkAsUnchanged()
        If _pharmaceuticalDispensing IsNot Nothing AndAlso _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Count > 0 Then
            Dim PSD = (From k In _pharmaceuticalDispensing.PharmaceuticalDispensingDetail Where k.Id = pdd.Id Select k.ProductServiceDetail).FirstOrDefault()
            PSD.ToList().ForEach(Sub(x)
                                     pdd.ProductServiceDetail.Add(x)
                                 End Sub)
        End If

        Return pdd
    End Function

    Private Async Function ListBathserialsFromDetail(ByVal idDetail As Int32) As Task(Of List(Of PharmaceuticalDispensingDetailBatchSerial))
        If listBatchSerials Is Nothing AndAlso taskBatchSerial IsNot Nothing Then
            listBatchSerials = Await taskBatchSerial
        End If

        Dim auxList As List(Of PharmaceuticalDispensingDetailBatchSerial) = listBatchSerials.Where(Function(b) b.PharmaceuticalDispensingDetailId = idDetail).ToList()
        Dim result As New List(Of PharmaceuticalDispensingDetailBatchSerial)()

        For i = 0 To auxList.Count - 1
            Dim nBatchSerial As New PharmaceuticalDispensingDetailBatchSerial()
            With nBatchSerial
                .Id = auxList(i).Id
                .PharmaceuticalDispensingDetailId = auxList(i).PharmaceuticalDispensingDetailId
                .PhysicalInventoryId = auxList(i).PhysicalInventoryId
                .PhysicalInventoryCustodyId = auxList(i).PhysicalInventoryCustodyId
                .Quantity = auxList(i).Quantity
                .OutstandingQuantity = auxList(i).OutstandingQuantity
                .MarkAsUnchanged()
            End With
            result.Add(nBatchSerial)
        Next

        Return result
    End Function

    ''' <summary>
    ''' Genera el objeto Entity de la cabecera de la dispensación
    ''' </summary>
    ''' <returns></returns>
    Private Function GetPharmaceuticalDispensingFromGrid() As PharmaceuticalDispensing
        Dim pdObject As New PharmaceuticalDispensing()

        pdObject.Id = INDgvProducts1.GetFocusedRowCellValue("Iddd")
        pdObject.Code = INDgvProducts1.GetFocusedRowCellValue("Code")
        pdObject.OperatingUnitId = If(_pharmaceuticalDispensing IsNot Nothing AndAlso _pharmaceuticalDispensing.Id > 0, _pharmaceuticalDispensing.OperatingUnitId, Me.BarraBotones.OperatingUnitValue)
        pdObject.AdmissionNumber = INDgvProducts1.GetFocusedRowCellValue("AdmissionNumber")
        pdObject.DocumentDate = INDgvProducts1.GetFocusedRowCellValue("DocumentDate")
        pdObject.AffectInventory = INDgvProducts1.GetFocusedRowCellValue("AffectInventory")
        pdObject.Status = INDgvProducts1.GetFocusedRowCellValue("Status")
        pdObject.CreationUser = INDgvProducts1.GetFocusedRowCellValue("CreationUser")
        pdObject.CreationDate = INDgvProducts1.GetFocusedRowCellValue("CreationDate")
        pdObject.ModificationUser = INDgvProducts1.GetFocusedRowCellValue("ModificationUser")
        pdObject.ModificationDate = INDgvProducts1.GetFocusedRowCellValue("ModificationDate")
        pdObject.ConfirmationUser = INDgvProducts1.GetFocusedRowCellValue("ConfirmationUser")
        pdObject.ConfirmationDate = INDgvProducts1.GetFocusedRowCellValue("ConfirmationDate")
        pdObject.AnnulmentUser = INDgvProducts1.GetFocusedRowCellValue("AnnulmentUser")
        pdObject.AnnulmentDate = INDgvProducts1.GetFocusedRowCellValue("AnnulmentDate")

        pdObject.MarkAsUnchanged()

        Return pdObject
    End Function

    Public Async Sub LoadControls() Implements IPharmaceuticalDispensing.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If

            INDliProducto1.Visibility = LayoutVisibility.Always
            INDliProductoAddedEdited.Visibility = LayoutVisibility.Never

            INDgcProducts1.Focus()
            INDgvProducts1.Focus()
            INDgvProducts1.FocusedRowHandle = 1

            Using model As New MPharmaceuticalDispensing(Me.Tag.ToString())
                AsyncLoader(True)
                Dim res = Await model.GetPharmaceuticalDispensing(Code.Trim())
                If res.StateResult AndAlso res.ObjectEmbbeded IsNot Nothing AndAlso res.ObjectEmbbeded.Id > 0 Then
                    _pharmaceuticalDispensing = res.ObjectEmbbeded
                    Await LoadRecord()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró un registro para el Código digitado"
                    AsyncLoader(False)
                End If
            End Using
        End If
    End Sub


    Private Async Sub QueryBlockrecordAndIndexedDocument()
        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
            Dim result = Await ModelRecord.GetBlockRecord(Me.Tag, _pharmaceuticalDispensing.Id)
            With _pharmaceuticalDispensing
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

            End With

            Me.GetDocumentIndexed(Me.Tag & "_" & Me._pharmaceuticalDispensing.Code)
            If result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _record = New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _pharmaceuticalDispensing.Id}
                Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                _record = operation.ObjectEmbbeded
            Else
                _record = result
                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
            Me.BarraBotones.SetDocuments(_pharmaceuticalDispensing.Id, MyTag, Nothing, GetType(PharmaceuticalDispensing).Name)
        End Using
    End Sub

    Private _popUpAdmissionLoaded As Boolean = False

    ''' <summary>
    ''' Consulta y carga la admisión
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RunSetAdmission() As Task
        Return Task.Factory.StartNew(Sub()
                                         Using modelServiceOrder As New Billing.MVP.MServiceOrder(Me.MyTag)
                                             Dim admissionTmp = modelServiceOrder.GetAdmissionByServiceOrder(_pharmaceuticalDispensing.AdmissionNumber.Trim())
                                             If Me.INDsleAdmissionNumber.InvokeRequired Then
                                                 Me.INDsleAdmissionNumber.BeginInvoke(Sub()
                                                                                          SetAdmission(admissionTmp)
                                                                                      End Sub)
                                             Else
                                                 SetAdmission(admissionTmp)
                                             End If
                                             _popUpAdmissionLoaded = True
                                             If admissionTmp IsNot Nothing Then
                                                 ValidatePatientThirdParty(admissionTmp.PatientCode)
                                             End If
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' Carga el registro con la cabecera
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadRecord() As Task
        Try
            AsyncLoader(True)
            INDlcRoot.BeginUpdate()
            If _pharmaceuticalDispensing IsNot Nothing AndAlso _pharmaceuticalDispensing.Id > 0 Then
                Me.BarraBotones.StatusRecordVisible = True
                Using model As New MPharmaceuticalDispensing(Me.Tag)
                    taskBatchSerial = model.ListPharmaceuticalDispensingDetailBatchSerialByDispensingId(_pharmaceuticalDispensing.Id)
                End Using
                _pharmaceuticalDispensing.MarkAsUnchanged()
                QueryBlockrecordAndIndexedDocument()
                With _pharmaceuticalDispensing
                    If .Status <> 1 Then
                        INDdeDocumentDate.Properties.MinValue = .DocumentDate
                        INDdeDocumentDate.Properties.MaxValue = .DocumentDate
                    End If

                    Code = .Code
                    AdmissionNumber = .AdmissionNumber
                    OperationgUnitId = .OperatingUnitId
                    DocumentDate = .DocumentDate
                    INDGleAffectInventory.EditValue = .AffectInventory
                    INDGleAffectInventory.ReadOnly = True
                    Status = .Status.ToString()
                End With
                INDsleAdmissionNumber.SetNullText(_pharmaceuticalDispensing.AdmissionNumber)
                Await RunSetAdmission()
                Me.INDgcProducts1.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetPharmaceuticalDispensing(Me.Code.Trim())
                AsyncLoader(False)
                ActionsOnControls = True
                INDbteCode.Enabled = False
                If _pharmaceuticalDispensing.Status = 2 OrElse _pharmaceuticalDispensing.Status = 3 Then 'estado confirmado
                    ReadOnlyControls(True)
                    ReadOnlyControls(False, LayoutControl2)
                    INDsleAdmissionNumber.IsReadOnly = True
                    INDcolMoreInfo.OptionsColumn.AllowEdit = True
                    INDcolMoreInfo.OptionsColumn.AllowFocus = True
                    INDlcgMoreInfo.Enabled = True
                    INDlcgBatchSerial.Enabled = True
                    INDsbAddProduct.Enabled = False
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                Else
                    INDsbAddProduct.Enabled = True
                    INDsleAdmissionNumber.IsReadOnly = False
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                    ShowHidePermissionImportButton()
                End If
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Await Task.Factory.StartNew(Sub()
                                                If Me.BarraBotones.InvokeRequired Then
                                                    Me.BarraBotones.BeginInvoke(Sub()
                                                                                    Me.BarraBotones.PrintReport(PrintReportAction.None, _pharmaceuticalDispensing.Id, 0, _pharmaceuticalDispensing.Id)
                                                                                End Sub)
                                                Else
                                                    Me.BarraBotones.PrintReport(PrintReportAction.None, _pharmaceuticalDispensing.Id, 0, _pharmaceuticalDispensing.Id)
                                                End If
                                            End Sub)
                _dataLoaded = True
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Await Me.NewPharmaceuticalDispensing()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Code = String.Empty
                    INDbteCode.Focus()
                End If
            End If
            INDlcRoot.EndUpdate()
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Function

    Private Sub INDgvProducts1_RowLoaded(sender As Object, e As Views.Base.RowEventArgs) Handles INDgvProducts1.RowLoaded
        'If e.RowHandle > -1 AndAlso Not _dataLoaded Then
        '    Await LoadRecord()
        'End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._pharmaceuticalDispensing IsNot Nothing AndAlso Me._pharmaceuticalDispensing.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Eliminars this instance.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me._pharmaceuticalDispensing IsNot Nothing AndAlso Me._pharmaceuticalDispensing.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MPharmaceuticalDispensing(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePharmaceuticalDispensing(Me._pharmaceuticalDispensing)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                            If result.Message IsNot Nothing Then
                                generateListError(result.Message)
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Guardars the specified confirm.
    ''' </summary>
    ''' <param name="confirm">if set to <c>true</c> [confirm].</param>
    Public Async Sub Guardar(confirm As Boolean)
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        If INDgvProducts1.RowCount = 0 AndAlso INDgvProductoAddedEdited.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar, modificar o eliminar minimo un producto"
            Exit Sub
        End If

        'Se valida que no quede la entidad sin detalles porque al volver a consultar la dispensación arroja error
        If Me.ListPharmaceuticalDispensingDetail IsNot Nothing AndAlso (From x In Me.ListPharmaceuticalDispensingDetail Where x.ChangeTrackerState = 8 Select x).Count > 0 Then
            If INDgvProducts1.RowCount = (From x In Me.ListPharmaceuticalDispensingDetail Where x.ChangeTrackerState = 8 Select x).Count AndAlso (From x In Me.ListPharmaceuticalDispensingDetail Where x.ChangeTrackerState = 1 OrElse x.ChangeTrackerState = 2 OrElse x.ChangeTrackerState = 3 Select x).Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La dispensación no se puede quedar sin detalles en la rejilla"
                Exit Sub
            End If
        End If

        If OperationgUnitId = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione unidad operativa"
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MPharmaceuticalDispensing(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SavePharmaceuticalDispensing(Me._pharmaceuticalDispensing, Me._idCurrentSequence, confirm, INDGleAffectInventory.EditValue)
                If Result.StatusCode = eStatusResult.SUCCESS Then
                    If confirm Then
                        If _pharmaceuticalDispensing.Id = 0 Then
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                            End If

                            If Result.StatusCode = eStatusResult.SUCCESS Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SavedNoConfirmed", MODULE_NAME), Result.ObjectEmbbeded.Code, Result.Message)
                            End If

                        ElseIf _pharmaceuticalDispensing.Id > 0 Then

                            If Result.StatusCode = eStatusResult.SUCCESS Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("UpdatedNoConfirmed", MODULE_NAME), Result.Message)
                            End If

                        End If
                    Else
                        If _pharmaceuticalDispensing.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            If Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                            End If
                            If Me._sequence.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf _pharmaceuticalDispensing.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me._pharmaceuticalDispensing = Result.ObjectEmbbeded
                    dispensingTmp = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    taskWait = Task.Factory.StartNew(Sub()
                                                         If Me.BarraBotones.InvokeRequired Then
                                                             Me.BarraBotones.BeginInvoke(Sub()
                                                                                             Select Case varImp
                                                                                                 Case 1
                                                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Create, dispensingTmp.Id, 0, dispensingTmp.Id)
                                                                                                 Case 2
                                                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Update, dispensingTmp.Id, 0, dispensingTmp.Id)
                                                                                                 Case 3
                                                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Confirm, dispensingTmp.Id, 0, dispensingTmp.Id)
                                                                                             End Select
                                                                                         End Sub)
                                                         Else
                                                             Select Case varImp
                                                                 Case 1
                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Create, dispensingTmp.Id, 0, dispensingTmp.Id)
                                                                 Case 2
                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Update, dispensingTmp.Id, 0, dispensingTmp.Id)
                                                                 Case 3
                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Confirm, dispensingTmp.Id, 0, dispensingTmp.Id)
                                                             End Select
                                                         End If
                                                     End Sub)

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    If Result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Else
                        If Result.Message IsNot Nothing Then
                            generateListError(Result.Message)
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Anulars this instance.
    ''' </summary>
    Private Async Sub Anular()
        If Me._pharmaceuticalDispensing IsNot Nothing AndAlso Me._pharmaceuticalDispensing.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                _pharmaceuticalDispensing.Status = 3
                Try
                    Using Model As New MPharmaceuticalDispensing(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.SavePharmaceuticalDispensing(Me._pharmaceuticalDispensing, Me._idCurrentSequence, False)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Me._pharmaceuticalDispensing = result.ObjectEmbbeded
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            taskWait = Task.Factory.StartNew(Sub()
                                                                 If Me.BarraBotones.InvokeRequired Then
                                                                     Me.BarraBotones.BeginInvoke(Sub()
                                                                                                     'Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _pharmaceuticalDispensing.Id, 0, _pharmaceuticalDispensing.Id)
                                                                                                 End Sub)
                                                                 Else
                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _pharmaceuticalDispensing.Id, 0, _pharmaceuticalDispensing.Id)
                                                                 End If
                                                             End Sub)
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                            If result.Message IsNot Nothing Then
                                generateListError(result.Message)
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Guardars this instance.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.MensajeError) = errors
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPharmaceuticalDispensing()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)(ResourceManager.GetString("StateUnconfirmed"), 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)(ResourceManager.GetString("StateConfirmed"), 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)(ResourceManager.GetString("StatusCanceled"), 3))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)(ResourceManager.GetString("StatusReverse"), 4))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15},
                              New ColumnInfo() With {.Caption = "Número Admisión", .FieldName = "AdmissionNumber", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Paciente", .FieldName = "CodeNamePatient", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Almacenes", .FieldName = "Warehouse", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Usuario de la dispensacion", .FieldName = "CreationUser", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus, .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPharmaceuticalDispensing
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub
#End Region

#Region "BarButton Events"
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            varImp = 3
            Guardar(True)
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        varImp = 3
        Guardar(True)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anular()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        taskWait = Task.Factory.StartNew(Sub()
                                             If Me.BarraBotones.InvokeRequired Then
                                                 Me.BarraBotones.BeginInvoke(Sub()
                                                                                 Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _pharmaceuticalDispensing.Id, 0, _pharmaceuticalDispensing.Id)
                                                                             End Sub)
                                             Else
                                                 Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _pharmaceuticalDispensing.Id, 0, _pharmaceuticalDispensing.Id)
                                             End If
                                         End Sub)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        If Me.BarraBotones.PermissionsForm.ContainsKey(74) Then
            INDLciAffectInventory.Visibility = LayoutVisibility.Always
        Else
            INDLciAffectInventory.Visibility = LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Barra Botones: ImportarInformación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        OpenFormImport()
    End Sub

#End Region

End Class
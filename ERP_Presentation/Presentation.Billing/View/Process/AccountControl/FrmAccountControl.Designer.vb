Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountControl
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode5 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccountControl))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.INDgvKardex = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcMedicineSupplier = New DevExpress.XtraGrid.GridControl()
        Me.INDGvManualMovements = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSubFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSubPatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSubQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColMovementType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvMedicineSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvNursingProceduresDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn80 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn81 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn82 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcNurseProcedure = New DevExpress.XtraGrid.GridControl()
        Me.INDgvNursingProcedures = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn79 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSelNursingProcedures = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkNursingProcedures = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn76 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn198 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn77 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn83 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn96 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn154 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn155 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn156 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn183 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn184 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrptPceNursingProcedures = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptNursingGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDgvPharmaDoseMSDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSelected = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRIselector = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn124 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn127 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn129 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn126 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn125 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcMixingStation = New DevExpress.XtraGrid.GridControl()
        Me.INDGvMixingStation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDeliveryQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColApplyQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCurrentQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptStatus = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDRptMixingStationGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDGvSurgeriesPerformed = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColSelQx = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESelected = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColMainqx = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColRouteIn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn98 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn171 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn187 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn188 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationQx = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDgcProceduresQx = New DevExpress.XtraGrid.GridControl()
        Me.INDgvProceduresQx = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptPceDetailProceduresQx = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptImgGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDrptPceHemo = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPccHemo = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcHemo = New DevExpress.XtraLayout.LayoutControl()
        Me.INDteComponente = New DevExpress.XtraEditors.TextEdit()
        Me.INDteNumeroUnidad = New DevExpress.XtraEditors.TextEdit()
        Me.INDteSelloCalidad = New DevExpress.XtraEditors.TextEdit()
        Me.INDrgPruebaCru = New DevExpress.XtraEditors.RadioGroup()
        Me.INDrgRastreoAnti = New DevExpress.XtraEditors.RadioGroup()
        Me.INDteBacEntrega = New DevExpress.XtraEditors.TextEdit()
        Me.INDteMedReaSolicitudReserva = New DevExpress.XtraEditors.TextEdit()
        Me.INDteMedReaSolicitudTransfusion = New DevExpress.XtraEditors.TextEdit()
        Me.INDteMedRealizoRegTransfusión = New DevExpress.XtraEditors.TextEdit()
        Me.INDteEnfRealizoRegistroAplicacion = New DevExpress.XtraEditors.TextEdit()
        Me.INDteFechaEntrega = New DevExpress.XtraEditors.TextEdit()
        Me.INDteFechaExpiracion = New DevExpress.XtraEditors.TextEdit()
        Me.INDteFechaAplicacionMed = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeDateEndTrans = New DevExpress.XtraEditors.TextEdit()
        Me.INDteFechaAplicacionEnf = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeDateIniTrans = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem35 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem36 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem37 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem38 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem40 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem41 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem42 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem43 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem44 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem45 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem46 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem47 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem48 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem49 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem50 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem51 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDrptChkHemo = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbJustification = New DevExpress.XtraEditors.SimpleButton()
        Me.INDpccServicesProcedures = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtQxTime = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtTipoAnestesia = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtMuestraPatologicas = New DevExpress.XtraEditors.TextEdit()
        Me.INDGcQxEquipe = New DevExpress.XtraGrid.GridControl()
        Me.INDGvQxEquipe = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcProceduresRealizados = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProceduresRealizados = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCodigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcDetailServiciosNoRealizados = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetailServiciosNoRealizados = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFecha = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFolio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColMedicoNoRelazados = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcDetailServiciosRealizados = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetailServiciosRealizados = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolSeleccioneLab = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkSeleccioneServiceProcedure = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColMedico = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColObservacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColUnidadFuncional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColInterpretacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptMeInterpretation = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.ColCantidad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicoRealizo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSearchMedicoRealizo = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn99 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn100 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn101 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox2 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.colFechaRealizacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.INDrpticeGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDTxtObservaciones = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxtProcedimientosRealizados = New DevExpress.XtraEditors.MemoEdit()
        Me.INDMeHallazgosOperatorios = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDtcgServicesProcedures = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDtpServiciosRealizados = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDtpServiciosNoRealizados = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TabServices = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TabProfessional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TabOtherData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem33 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem30 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem28 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem32 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcHemoDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvHemoDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn106 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSelHemo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn107 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn108 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn109 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn110 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn122 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn114 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn115 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn177 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn178 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationH = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.MBtnOpenAdmission = New DevExpress.XtraBars.BarButtonItem()
        Me.MBtnCloseAdmission = New DevExpress.XtraBars.BarButtonItem()
        Me.INDRptHemoGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDSleAdmissionNumber2 = New Presentation.Controls.SearchLookUpEditExAdmission()
        Me.PccAdmission = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LycPccAdmission = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdeOut = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtFunctionalUnitOut = New DevExpress.XtraEditors.TextEdit()
        Me.TxtRiskType = New DevExpress.XtraEditors.TextEdit()
        Me.TxtCareGroupAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtContact = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientEstrato = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientEntityName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtCareGroupPatient = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientAge = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientBirth = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientCode = New DevExpress.XtraEditors.TextEdit()
        Me.TxtBedStay = New DevExpress.XtraEditors.TextEdit()
        Me.TxtResponsiblePhone = New DevExpress.XtraEditors.TextEdit()
        Me.TxtResponsibleName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtEntityNameAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAtentionCenter = New DevExpress.XtraEditors.TextEdit()
        Me.TxtBenefitPlan = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPlaceEntry = New DevExpress.XtraEditors.TextEdit()
        Me.TxtFunctionalUnitAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAdmissionDate = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAdmissionCode = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtAfiliationType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtAdmissionType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtLiquidationType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LycgPccAdmission = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LycgAdmissionGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem27 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem39 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem25 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LiCareGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem29 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem26 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LiEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TxtContacto = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem31 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.viewSearchAdmission = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LblState = New DevExpress.XtraEditors.LabelControl()
        Me.SleOperatingUnit = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DdbMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDsbClear = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsbGererateServiceOrder = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcValoraciones = New DevExpress.XtraGrid.GridControl()
        Me.INDgvValorations = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColSeleccioneValoraciones = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptChkValoration = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDgcRegisterDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcFolioDoneVal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcMDDone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn86 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn105 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrpticeGeneratedValoraciones = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn84 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn185 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn186 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationV = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDRptPceDetailValoration = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptImgGenerado = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcOxygenConsumption = New DevExpress.XtraGrid.GridControl()
        Me.INDgvOxygen = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn72 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSelOxygen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkOrygenConsumpsion = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn95 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn60 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn66 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn67 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn70 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn181 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn182 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationJ = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrptImeOxygen = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcTerapy = New DevExpress.XtraGrid.GridControl()
        Me.INDgvTherapy = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColSelTerapy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkTherapy = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn62 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn63 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn64 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn65 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn94 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn157 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn158 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn159 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn160 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn161 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn179 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn180 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn199 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptPceDetailTherapy = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptTherapyGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcConsultation = New DevExpress.XtraGrid.GridControl()
        Me.INDgvConsultation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSelInterconsulta = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkInterSearch = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcFolioRequest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn93 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn137 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn138 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcFolioDoneIC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn139 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn140 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn141 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn142 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn121 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn175 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn176 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationC = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrptPceDetailConsultation = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptIntercosultasGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcProceduresNoQX = New DevExpress.XtraGrid.GridControl()
        Me.INDgvProceduresNoQx = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColSelNoQx = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkProceduresNoQx = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn92 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn97 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColContractDescriptionNoQx = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptSleContractDescriptionProceduresNoQx = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn120 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn123 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn102 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn162 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn163 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn164 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn173 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn174 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationNQX = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrptPceDetailProceduresNoQx = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptNoQxGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcImagesDx = New DevExpress.XtraGrid.GridControl()
        Me.INDgvImagesDX = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSelImagesDx = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkImagesDx = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn91 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn130 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn131 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn132 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn133 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn134 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn135 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn136 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn118 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn169 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn170 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationDx = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrptDetailImagesDX = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptImagesGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcPathology = New DevExpress.XtraGrid.GridControl()
        Me.INDgvPathologies = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSelPathologies = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkPathologies = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn90 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn117 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn148 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn149 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn151 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn150 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn152 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn153 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn167 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn168 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidationP = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrptPceDetailPathologies = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptPathologiesGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcLaboratories = New DevExpress.XtraGrid.GridControl()
        Me.INDgvLaboratories = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSeleccioneLaboratorio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptChkLaboratories = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn59 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn87 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn143 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn144 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn116 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn145 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn146 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn147 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn165 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn166 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESkipLiquidation = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrptPceDetailLaboratories = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptLaboratoriesGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDGcStays = New DevExpress.XtraGrid.GridControl()
        Me.INDGvStays = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn197 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptChkSelectStay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn119 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFunctionalUnitStay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn189 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn190 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn191 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn192 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn194 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn172 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn193 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn196 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn195 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptImgGeneratedStay = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDtcgSources = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlcgConsultation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliInterSearch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgMedicinesSupplies = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliMedicamentosInsumos = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLycgMixingStation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlycMixingStation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgLaboratories = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliLaboratories = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgPathology = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliIPathology = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgImagesDX = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliImagesDx = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgProceduresQX = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliProceduresQX = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgProceduresNoQX = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliProceduresNoQX = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgHemo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciHemoDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgTerapy = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliTerapy = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgOxigenConsumer = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliOxigenConsumer = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgStays = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem52 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgNurseProcedure = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliNurseProcedure = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgValoraciones = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliValoraciones = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDliGenerateServiceOrder = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliClear = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliOperativeUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem34 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LciStateAdmission = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciJustification = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        Me.IndigoCheckEdit2 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDbbiOpenRequest = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu2 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView3 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView4 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView5 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView6 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView7 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView8 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridViewStay = New Presentation.Controls.IndigoGridView(Me.components)
        Me.SvgImageCollection1 = New DevExpress.Utils.SvgImageCollection(Me.components)
        Me.INDRptInterconsultasValidated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvKardex, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcMedicineSupplier, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvManualMovements, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvMedicineSupplier, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvNursingProceduresDetail, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcNurseProcedure, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvNursingProcedures, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkNursingProcedures, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationE, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptPceNursingProcedures, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptNursingGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvPharmaDoseMSDetail, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRIselector, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGcMixingStation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvMixingStation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptStatus, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptMixingStationGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvSurgeriesPerformed, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESelected, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationQx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcProceduresQx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvProceduresQx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptPceDetailProceduresQx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptImgGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptPceHemo, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDPccHemo, System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPccHemo.SuspendLayout
        CType(Me.INDLcHemo, System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDLcHemo.SuspendLayout
        CType(Me.INDteComponente.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteNumeroUnidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteSelloCalidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrgPruebaCru.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrgRastreoAnti.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteBacEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteMedReaSolicitudReserva.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteMedReaSolicitudTransfusion.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteMedRealizoRegTransfusión.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteEnfRealizoRegistroAplicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteFechaEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteFechaExpiracion.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteFechaAplicacionMed.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTeDateEndTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDteFechaAplicacionEnf.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTeDateIniTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem35, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem36, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem37, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem38, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem40, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem41, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem42, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem43, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem44, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem45, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem46, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem47, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem48, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem49, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem50, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem51, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkHemo, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlcRoot.SuspendLayout
        CType(Me.INDpccServicesProcedures, System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDpccServicesProcedures.SuspendLayout
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.INDTxtQxTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTxtTipoAnestesia.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTxtMuestraPatologicas.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGcQxEquipe, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvQxEquipe, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGcProceduresRealizados, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvProceduresRealizados, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcDetailServiciosNoRealizados, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvDetailServiciosNoRealizados, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcDetailServiciosRealizados, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvDetailServiciosRealizados, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkSeleccioneServiceProcedure, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptMeInterpretation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptSearchMedicoRealizo, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemImageComboBox2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrpticeGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTxtObservaciones.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTxtProcedimientosRealizados.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDMeHallazgosOperatorios.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDtcgServicesProcedures, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDtpServiciosRealizados, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDtpServiciosNoRealizados, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TabServices, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TabProfessional, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TabOtherData, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem33, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem30, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem28, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LciObservations, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem32, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGcHemoDetail, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvHemoDetail, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationH, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptHemoGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PccAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PccAdmission.SuspendLayout
        CType(Me.LycPccAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LycPccAdmission.SuspendLayout
        CType(Me.INDdeOut.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDtxtFunctionalUnitOut.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtRiskType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtCareGroupAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtContact.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientEstrato.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientEntityName.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtCareGroupPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientAge.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientBirth.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientName.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtBedStay.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtEntityNameAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAtentionCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtBenefitPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPlaceEntry.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtFunctionalUnitAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAfiliationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgPccAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgAdmissionGroup, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LiCareGroup, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LiEntity, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtContacto, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.viewSearchAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SleOperatingUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcValoraciones, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvValorations, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptChkValoration, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrpticeGeneratedValoraciones, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationV, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptPceDetailValoration, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptImgGenerado, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcOxygenConsumption, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvOxygen, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkOrygenConsumpsion, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationJ, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptImeOxygen, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcTerapy, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvTherapy, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkTherapy, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationT, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptPceDetailTherapy, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptTherapyGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcConsultation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvConsultation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkInterSearch, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationC, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptPceDetailConsultation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptIntercosultasGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcProceduresNoQX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvProceduresNoQx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkProceduresNoQx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptSleContractDescriptionProceduresNoQx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationNQX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptPceDetailProceduresNoQx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptNoQxGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcImagesDx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvImagesDX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkImagesDx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationDx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptDetailImagesDX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptImagesGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcPathology, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvPathologies, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkPathologies, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidationP, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptPceDetailPathologies, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptPathologiesGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgcLaboratories, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvLaboratories, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptChkLaboratories, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRICESkipLiquidation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDrptPceDetailLaboratories, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptLaboratoriesGenerated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGcStays, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvStays, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptChkSelectStay, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptImgGeneratedStay, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDtcgSources, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgConsultation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliInterSearch, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgMedicinesSupplies, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliMedicamentosInsumos, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLycgMixingStation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlycMixingStation, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgLaboratories, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliLaboratories, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgPathology, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliIPathology, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgImagesDX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliImagesDx, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgProceduresQX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliProceduresQX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgProceduresNoQX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliProceduresNoQX, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLcgHemo, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciHemoDetail, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgTerapy, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliTerapy, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgOxigenConsumer, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliOxigenConsumer, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLcgStays, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem52, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgNurseProcedure, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliNurseProcedure, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgValoraciones, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliValoraciones, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliGenerateServiceOrder, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliClear, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDliOperativeUnit, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem34, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LciStateAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciJustification, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PopupMenu2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView7, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView8, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridViewStay, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SvgImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptInterconsultasValidated, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1685, 586)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1685, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(6)
        Me.BarraBotones.Size = New System.Drawing.Size(1685, 130)
        '
        'INDgvKardex
        '
        Me.INDgvKardex.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvKardex.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvKardex.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvKardex.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvKardex.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvKardex.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvKardex.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvKardex.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvKardex.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvKardex.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvKardex.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvKardex.Appearance.Row.Options.UseFont = True
        Me.INDgvKardex.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvKardex.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvKardex.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn88, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.INDgvKardex.GridControl = Me.INDgcMedicineSupplier
        Me.INDgvKardex.Name = "INDgvKardex"
        Me.INDgvKardex.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvKardex.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvKardex.OptionsView.RowAutoHeight = True
        Me.INDgvKardex.OptionsView.ShowAutoFilterRow = True
        Me.INDgvKardex.OptionsView.ShowDetailButtons = False
        Me.INDgvKardex.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvKardex, False)
        Me.INDgvKardex.ViewCaption = "Kardex"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Unidad Funcional"
        Me.GridColumn1.FieldName = "UFUDESCRI"
        Me.GridColumn1.MinWidth = 21
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 261
        '
        'GridColumn88
        '
        Me.GridColumn88.Caption = "Paciente"
        Me.GridColumn88.FieldName = "PersonName"
        Me.GridColumn88.MinWidth = 21
        Me.GridColumn88.Name = "GridColumn88"
        Me.GridColumn88.OptionsColumn.AllowEdit = False
        Me.GridColumn88.OptionsColumn.AllowFocus = False
        Me.GridColumn88.Visible = True
        Me.GridColumn88.VisibleIndex = 1
        Me.GridColumn88.Width = 411
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Fecha Movimiento"
        Me.GridColumn6.FieldName = "Fecha"
        Me.GridColumn6.MinWidth = 21
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 273
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Movimiento"
        Me.GridColumn7.FieldName = "Movimiento"
        Me.GridColumn7.MinWidth = 21
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 261
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Cantidad"
        Me.GridColumn8.FieldName = "Cantidad"
        Me.GridColumn8.MinWidth = 21
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 4
        Me.GridColumn8.Width = 93
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Tipo"
        Me.GridColumn9.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn9.FieldName = "Tipo"
        Me.GridColumn9.MinWidth = 21
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 5
        Me.GridColumn9.Width = 91
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Entrada", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Salida", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Solicitud", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anulacion", CType(4, Byte), -1)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'INDgcMedicineSupplier
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcMedicineSupplier, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcMedicineSupplier, Nothing)
        Me.INDgcMedicineSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcMedicineSupplier.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcMedicineSupplier, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcMedicineSupplier, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcMedicineSupplier, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcMedicineSupplier, False)
        GridLevelNode4.LevelTemplate = Me.INDgvKardex
        GridLevelNode4.RelationName = "KardexMedicineSupplier"
        GridLevelNode5.LevelTemplate = Me.INDGvManualMovements
        GridLevelNode5.RelationName = "ManualMovements"
        Me.INDgcMedicineSupplier.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode4, GridLevelNode5})
        Me.INDgcMedicineSupplier.Location = New System.Drawing.Point(36, 146)
        Me.INDgcMedicineSupplier.MainView = Me.INDgvMedicineSupplier
        Me.INDgcMedicineSupplier.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcMedicineSupplier.Name = "INDgcMedicineSupplier"
        Me.INDgcMedicineSupplier.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDgcMedicineSupplier.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcMedicineSupplier, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcMedicineSupplier.TabIndex = 15
        Me.INDgcMedicineSupplier.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvManualMovements, Me.INDgvMedicineSupplier, Me.INDgvKardex})
        '
        'INDGvManualMovements
        '
        Me.INDGvManualMovements.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvManualMovements.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvManualMovements.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvManualMovements.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvManualMovements.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvManualMovements.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvManualMovements.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvManualMovements.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvManualMovements.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvManualMovements.Appearance.Row.Options.UseFont = True
        Me.INDGvManualMovements.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSubFunctionalUnit, Me.INDColSubPatient, Me.INDColDocumentDate, Me.INDColSubQuantity, Me.INDColMovementType, Me.INDColCreationUser})
        Me.INDGvManualMovements.GridControl = Me.INDgcMedicineSupplier
        Me.INDGvManualMovements.Name = "INDGvManualMovements"
        Me.INDGvManualMovements.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvManualMovements.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvManualMovements.OptionsView.ShowAutoFilterRow = True
        Me.INDGvManualMovements.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvManualMovements, False)
        Me.INDGvManualMovements.ViewCaption = "Movimientos Manuales"
        '
        'INDColSubFunctionalUnit
        '
        Me.INDColSubFunctionalUnit.Caption = "Unidad funcional"
        Me.INDColSubFunctionalUnit.FieldName = "FunctionalUnit"
        Me.INDColSubFunctionalUnit.MinWidth = 21
        Me.INDColSubFunctionalUnit.Name = "INDColSubFunctionalUnit"
        Me.INDColSubFunctionalUnit.OptionsColumn.AllowEdit = False
        Me.INDColSubFunctionalUnit.OptionsColumn.AllowFocus = False
        Me.INDColSubFunctionalUnit.Visible = True
        Me.INDColSubFunctionalUnit.VisibleIndex = 0
        '
        'INDColSubPatient
        '
        Me.INDColSubPatient.Caption = "Paciente"
        Me.INDColSubPatient.FieldName = "Patient"
        Me.INDColSubPatient.MinWidth = 21
        Me.INDColSubPatient.Name = "INDColSubPatient"
        Me.INDColSubPatient.OptionsColumn.AllowEdit = False
        Me.INDColSubPatient.OptionsColumn.AllowFocus = False
        Me.INDColSubPatient.Visible = True
        Me.INDColSubPatient.VisibleIndex = 1
        '
        'INDColDocumentDate
        '
        Me.INDColDocumentDate.Caption = "Fecha Movimiento"
        Me.INDColDocumentDate.FieldName = "DocumentDate"
        Me.INDColDocumentDate.MinWidth = 21
        Me.INDColDocumentDate.Name = "INDColDocumentDate"
        Me.INDColDocumentDate.OptionsColumn.AllowEdit = False
        Me.INDColDocumentDate.OptionsColumn.AllowFocus = False
        Me.INDColDocumentDate.Visible = True
        Me.INDColDocumentDate.VisibleIndex = 2
        '
        'INDColSubQuantity
        '
        Me.INDColSubQuantity.Caption = "Cantidades"
        Me.INDColSubQuantity.DisplayFormat.FormatString = "n0"
        Me.INDColSubQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSubQuantity.FieldName = "Quantity"
        Me.INDColSubQuantity.MinWidth = 21
        Me.INDColSubQuantity.Name = "INDColSubQuantity"
        Me.INDColSubQuantity.OptionsColumn.AllowEdit = False
        Me.INDColSubQuantity.OptionsColumn.AllowFocus = False
        Me.INDColSubQuantity.Visible = True
        Me.INDColSubQuantity.VisibleIndex = 3
        '
        'INDColMovementType
        '
        Me.INDColMovementType.Caption = "Tipo"
        Me.INDColMovementType.FieldName = "MovementType"
        Me.INDColMovementType.MinWidth = 21
        Me.INDColMovementType.Name = "INDColMovementType"
        Me.INDColMovementType.OptionsColumn.AllowEdit = False
        Me.INDColMovementType.OptionsColumn.AllowFocus = False
        Me.INDColMovementType.Visible = True
        Me.INDColMovementType.VisibleIndex = 4
        '
        'INDColCreationUser
        '
        Me.INDColCreationUser.Caption = "Codigo Usuario"
        Me.INDColCreationUser.FieldName = "CreationUser"
        Me.INDColCreationUser.MinWidth = 21
        Me.INDColCreationUser.Name = "INDColCreationUser"
        Me.INDColCreationUser.OptionsColumn.AllowEdit = False
        Me.INDColCreationUser.OptionsColumn.AllowFocus = False
        Me.INDColCreationUser.Visible = True
        Me.INDColCreationUser.VisibleIndex = 5
        '
        'INDgvMedicineSupplier
        '
        Me.INDgvMedicineSupplier.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvMedicineSupplier.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvMedicineSupplier.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvMedicineSupplier.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvMedicineSupplier.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvMedicineSupplier.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvMedicineSupplier.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMedicineSupplier.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvMedicineSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMedicineSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvMedicineSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvMedicineSupplier.Appearance.Row.Options.UseFont = True
        Me.INDgvMedicineSupplier.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvMedicineSupplier.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvMedicineSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.INDgvMedicineSupplier.GridControl = Me.INDgcMedicineSupplier
        Me.INDgvMedicineSupplier.Name = "INDgvMedicineSupplier"
        Me.INDgvMedicineSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvMedicineSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvMedicineSupplier.OptionsView.ShowAutoFilterRow = True
        Me.INDgvMedicineSupplier.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvMedicineSupplier, False)
        Me.INDgvMedicineSupplier.ViewCaption = "Kardex"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Producto"
        Me.GridColumn2.FieldName = "Producto"
        Me.GridColumn2.MinWidth = 21
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 415
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cant. Entregada"
        Me.GridColumn3.FieldName = "Entregada"
        Me.GridColumn3.MinWidth = 21
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 184
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cant. Aplicada"
        Me.GridColumn4.FieldName = "Aplicada"
        Me.GridColumn4.MinWidth = 21
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 184
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Cant. Actual"
        Me.GridColumn5.FieldName = "Fisico"
        Me.GridColumn5.MinWidth = 21
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        Me.GridColumn5.Width = 189
        '
        'INDgvNursingProceduresDetail
        '
        Me.INDgvNursingProceduresDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvNursingProceduresDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvNursingProceduresDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvNursingProceduresDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvNursingProceduresDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvNursingProceduresDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvNursingProceduresDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvNursingProceduresDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvNursingProceduresDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvNursingProceduresDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvNursingProceduresDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvNursingProceduresDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn80, Me.GridColumn81, Me.GridColumn82})
        Me.INDgvNursingProceduresDetail.GridControl = Me.INDgcNurseProcedure
        Me.INDgvNursingProceduresDetail.Name = "INDgvNursingProceduresDetail"
        Me.INDgvNursingProceduresDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvNursingProceduresDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvNursingProceduresDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvNursingProceduresDetail.OptionsView.ShowDetailButtons = False
        Me.INDgvNursingProceduresDetail.OptionsView.ShowFooter = True
        Me.INDgvNursingProceduresDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvNursingProceduresDetail, False)
        Me.INDgvNursingProceduresDetail.ViewCaption = "Historico"
        '
        'GridColumn80
        '
        Me.GridColumn80.Caption = "Código producto"
        Me.GridColumn80.FieldName = "CodigoProducto"
        Me.GridColumn80.MinWidth = 21
        Me.GridColumn80.Name = "GridColumn80"
        Me.GridColumn80.OptionsColumn.AllowEdit = False
        Me.GridColumn80.OptionsColumn.AllowFocus = False
        Me.GridColumn80.Visible = True
        Me.GridColumn80.VisibleIndex = 0
        Me.GridColumn80.Width = 435
        '
        'GridColumn81
        '
        Me.GridColumn81.Caption = "Producto"
        Me.GridColumn81.FieldName = "Producto"
        Me.GridColumn81.MinWidth = 21
        Me.GridColumn81.Name = "GridColumn81"
        Me.GridColumn81.OptionsColumn.AllowEdit = False
        Me.GridColumn81.OptionsColumn.AllowFocus = False
        Me.GridColumn81.Visible = True
        Me.GridColumn81.VisibleIndex = 1
        Me.GridColumn81.Width = 748
        '
        'GridColumn82
        '
        Me.GridColumn82.Caption = "Cantidad"
        Me.GridColumn82.FieldName = "CantidadProducto"
        Me.GridColumn82.MinWidth = 21
        Me.GridColumn82.Name = "GridColumn82"
        Me.GridColumn82.OptionsColumn.AllowEdit = False
        Me.GridColumn82.OptionsColumn.AllowFocus = False
        Me.GridColumn82.Visible = True
        Me.GridColumn82.VisibleIndex = 2
        Me.GridColumn82.Width = 207
        '
        'INDgcNurseProcedure
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcNurseProcedure, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcNurseProcedure, Nothing)
        Me.INDgcNurseProcedure.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcNurseProcedure.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcNurseProcedure, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcNurseProcedure, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcNurseProcedure, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcNurseProcedure, False)
        GridLevelNode2.LevelTemplate = Me.INDgvNursingProceduresDetail
        GridLevelNode2.RelationName = "ListNursingProcedureDetail"
        Me.INDgcNurseProcedure.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.INDgcNurseProcedure.Location = New System.Drawing.Point(36, 146)
        Me.INDgcNurseProcedure.MainView = Me.INDgvNursingProcedures
        Me.INDgcNurseProcedure.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcNurseProcedure.Name = "INDgcNurseProcedure"
        Me.INDgcNurseProcedure.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptChkNursingProcedures, Me.INDrptPceNursingProcedures, Me.INDRptNursingGenerated, Me.INDRICESkipLiquidationE})
        Me.INDgcNurseProcedure.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcNurseProcedure, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcNurseProcedure.TabIndex = 25
        Me.INDgcNurseProcedure.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvNursingProcedures, Me.INDgvNursingProceduresDetail})
        '
        'INDgvNursingProcedures
        '
        Me.INDgvNursingProcedures.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvNursingProcedures.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvNursingProcedures.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvNursingProcedures.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvNursingProcedures.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvNursingProcedures.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvNursingProcedures.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvNursingProcedures.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvNursingProcedures.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvNursingProcedures.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvNursingProcedures.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvNursingProcedures.Appearance.Row.Options.UseFont = True
        Me.INDgvNursingProcedures.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvNursingProcedures.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvNursingProcedures.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn79, Me.ColSelNursingProcedures, Me.GridColumn75, Me.GridColumn76, Me.GridColumn198, Me.GridColumn77, Me.GridColumn83, Me.GridColumn96, Me.GridColumn154, Me.GridColumn155, Me.GridColumn156, Me.GridColumn183, Me.GridColumn184})
        Me.INDgvNursingProcedures.GridControl = Me.INDgcNurseProcedure
        Me.INDgvNursingProcedures.GroupCount = 1
        Me.INDgvNursingProcedures.Name = "INDgvNursingProcedures"
        Me.INDgvNursingProcedures.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvNursingProcedures.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvNursingProcedures.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvNursingProcedures.OptionsView.ShowAutoFilterRow = True
        Me.INDgvNursingProcedures.OptionsView.ShowDetailButtons = False
        Me.INDgvNursingProcedures.OptionsView.ShowFooter = True
        Me.INDgvNursingProcedures.OptionsView.ShowGroupPanel = False
        Me.INDgvNursingProcedures.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn79, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvNursingProcedures, False)
        Me.INDgvNursingProcedures.ViewCaption = "Historico"
        '
        'GridColumn79
        '
        Me.GridColumn79.Caption = "Tipo"
        Me.GridColumn79.FieldName = "Tipo"
        Me.GridColumn79.MinWidth = 21
        Me.GridColumn79.Name = "GridColumn79"
        Me.GridColumn79.Visible = True
        Me.GridColumn79.VisibleIndex = 0
        '
        'ColSelNursingProcedures
        '
        Me.ColSelNursingProcedures.Caption = "Sel."
        Me.ColSelNursingProcedures.ColumnEdit = Me.INDrptChkNursingProcedures
        Me.ColSelNursingProcedures.FieldName = "Seleccione"
        Me.ColSelNursingProcedures.MinWidth = 21
        Me.ColSelNursingProcedures.Name = "ColSelNursingProcedures"
        Me.ColSelNursingProcedures.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelNursingProcedures.OptionsColumn.AllowIncrementalSearch = False
        Me.ColSelNursingProcedures.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelNursingProcedures.OptionsColumn.AllowMove = False
        Me.ColSelNursingProcedures.OptionsColumn.AllowShowHide = False
        Me.ColSelNursingProcedures.OptionsColumn.AllowSize = False
        Me.ColSelNursingProcedures.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelNursingProcedures.OptionsColumn.FixedWidth = True
        Me.ColSelNursingProcedures.OptionsFilter.AllowAutoFilter = False
        Me.ColSelNursingProcedures.OptionsFilter.AllowFilter = False
        Me.ColSelNursingProcedures.Visible = True
        Me.ColSelNursingProcedures.VisibleIndex = 0
        Me.ColSelNursingProcedures.Width = 39
        '
        'INDrptChkNursingProcedures
        '
        Me.INDrptChkNursingProcedures.AutoHeight = False
        Me.INDrptChkNursingProcedures.Name = "INDrptChkNursingProcedures"
        '
        'GridColumn75
        '
        Me.GridColumn75.Caption = "Código"
        Me.GridColumn75.FieldName = "CODSERIPS"
        Me.GridColumn75.MinWidth = 21
        Me.GridColumn75.Name = "GridColumn75"
        Me.GridColumn75.OptionsColumn.AllowEdit = False
        Me.GridColumn75.OptionsColumn.AllowFocus = False
        Me.GridColumn75.Visible = True
        Me.GridColumn75.VisibleIndex = 1
        Me.GridColumn75.Width = 350
        '
        'GridColumn76
        '
        Me.GridColumn76.Caption = "Procedimientos de enfermería"
        Me.GridColumn76.FieldName = "Actividad"
        Me.GridColumn76.MinWidth = 21
        Me.GridColumn76.Name = "GridColumn76"
        Me.GridColumn76.OptionsColumn.AllowEdit = False
        Me.GridColumn76.OptionsColumn.AllowFocus = False
        Me.GridColumn76.Visible = True
        Me.GridColumn76.VisibleIndex = 2
        Me.GridColumn76.Width = 392
        '
        'GridColumn198
        '
        Me.GridColumn198.Caption = "Descripción Relacionada"
        Me.GridColumn198.CustomizationCaption = "Descripción Relacionada"
        Me.GridColumn198.FieldName = "CodeNameContractDescriptions"
        Me.GridColumn198.MinWidth = 19
        Me.GridColumn198.Name = "GridColumn198"
        Me.GridColumn198.OptionsColumn.AllowEdit = False
        Me.GridColumn198.OptionsColumn.AllowFocus = False
        Me.GridColumn198.Visible = True
        Me.GridColumn198.VisibleIndex = 3
        Me.GridColumn198.Width = 278
        '
        'GridColumn77
        '
        Me.GridColumn77.Caption = "Cantidad"
        Me.GridColumn77.FieldName = "CANSERIPS"
        Me.GridColumn77.MinWidth = 21
        Me.GridColumn77.Name = "GridColumn77"
        Me.GridColumn77.OptionsColumn.AllowEdit = False
        Me.GridColumn77.OptionsColumn.AllowFocus = False
        Me.GridColumn77.Visible = True
        Me.GridColumn77.VisibleIndex = 4
        Me.GridColumn77.Width = 237
        '
        'GridColumn83
        '
        Me.GridColumn83.Caption = "Fecha Solicitud"
        Me.GridColumn83.FieldName = "Fecha"
        Me.GridColumn83.MinWidth = 21
        Me.GridColumn83.Name = "GridColumn83"
        Me.GridColumn83.OptionsColumn.AllowEdit = False
        Me.GridColumn83.OptionsColumn.AllowFocus = False
        '
        'GridColumn96
        '
        Me.GridColumn96.Caption = "Fecha Realizado"
        Me.GridColumn96.FieldName = "Fecha"
        Me.GridColumn96.MinWidth = 21
        Me.GridColumn96.Name = "GridColumn96"
        Me.GridColumn96.OptionsColumn.AllowEdit = False
        Me.GridColumn96.OptionsColumn.AllowFocus = False
        Me.GridColumn96.Visible = True
        Me.GridColumn96.VisibleIndex = 5
        Me.GridColumn96.Width = 157
        '
        'GridColumn154
        '
        Me.GridColumn154.Caption = "Medico"
        Me.GridColumn154.FieldName = "Medico"
        Me.GridColumn154.MinWidth = 21
        Me.GridColumn154.Name = "GridColumn154"
        Me.GridColumn154.OptionsColumn.AllowEdit = False
        Me.GridColumn154.OptionsColumn.AllowFocus = False
        '
        'GridColumn155
        '
        Me.GridColumn155.Caption = "Unidad Funcional "
        Me.GridColumn155.FieldName = "UnidadFuncional"
        Me.GridColumn155.MinWidth = 21
        Me.GridColumn155.Name = "GridColumn155"
        Me.GridColumn155.OptionsColumn.AllowEdit = False
        Me.GridColumn155.OptionsColumn.AllowFocus = False
        '
        'GridColumn156
        '
        Me.GridColumn156.Caption = "Observación"
        Me.GridColumn156.FieldName = "Observacion"
        Me.GridColumn156.MinWidth = 21
        Me.GridColumn156.Name = "GridColumn156"
        Me.GridColumn156.OptionsColumn.AllowEdit = False
        Me.GridColumn156.OptionsColumn.AllowFocus = False
        '
        'GridColumn183
        '
        Me.GridColumn183.Caption = "Justificación"
        Me.GridColumn183.FieldName = "JustificationCodeName"
        Me.GridColumn183.MinWidth = 21
        Me.GridColumn183.Name = "GridColumn183"
        Me.GridColumn183.OptionsColumn.AllowEdit = False
        Me.GridColumn183.OptionsColumn.AllowFocus = False
        Me.GridColumn183.Visible = True
        Me.GridColumn183.VisibleIndex = 6
        Me.GridColumn183.Width = 71
        '
        'GridColumn184
        '
        Me.GridColumn184.Caption = "Omitir Liquidacion"
        Me.GridColumn184.ColumnEdit = Me.INDRICESkipLiquidationE
        Me.GridColumn184.FieldName = "SkipLiquidation"
        Me.GridColumn184.MinWidth = 21
        Me.GridColumn184.Name = "GridColumn184"
        Me.GridColumn184.OptionsColumn.AllowEdit = False
        Me.GridColumn184.OptionsColumn.AllowFocus = False
        Me.GridColumn184.Visible = True
        Me.GridColumn184.VisibleIndex = 7
        Me.GridColumn184.Width = 78
        '
        'INDRICESkipLiquidationE
        '
        Me.INDRICESkipLiquidationE.AutoHeight = False
        Me.INDRICESkipLiquidationE.Name = "INDRICESkipLiquidationE"
        Me.INDRICESkipLiquidationE.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDrptPceNursingProcedures
        '
        Me.INDrptPceNursingProcedures.AutoHeight = False
        Me.INDrptPceNursingProcedures.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptPceNursingProcedures.Name = "INDrptPceNursingProcedures"
        Me.INDrptPceNursingProcedures.PopupSizeable = False
        Me.INDrptPceNursingProcedures.ShowPopupCloseButton = False
        Me.INDrptPceNursingProcedures.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptNursingGenerated
        '
        Me.INDRptNursingGenerated.AutoHeight = False
        Me.INDRptNursingGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptNursingGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptNursingGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptNursingGenerated.Name = "INDRptNursingGenerated"
        Me.INDRptNursingGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptNursingGenerated.SmallImages = Me.ImageCollection1
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "Icono correcto e incorrecto 16x16-02.png")
        Me.ImageCollection1.Images.SetKeyName(1, "Icono correcto e incorrecto 16x16-01.png")
        '
        'INDgvPharmaDoseMSDetail
        '
        Me.INDgvPharmaDoseMSDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvPharmaDoseMSDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvPharmaDoseMSDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvPharmaDoseMSDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvPharmaDoseMSDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvPharmaDoseMSDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvPharmaDoseMSDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvPharmaDoseMSDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvPharmaDoseMSDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvPharmaDoseMSDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvPharmaDoseMSDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvPharmaDoseMSDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvPharmaDoseMSDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvPharmaDoseMSDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvPharmaDoseMSDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSelected, Me.GridColumn124, Me.GridColumn127, Me.GridColumn129, Me.GridColumn126, Me.GridColumn128, Me.GridColumn125})
        Me.INDgvPharmaDoseMSDetail.GridControl = Me.INDGcMixingStation
        Me.INDgvPharmaDoseMSDetail.Name = "INDgvPharmaDoseMSDetail"
        Me.INDgvPharmaDoseMSDetail.OptionsSelection.MultiSelect = True
        Me.INDgvPharmaDoseMSDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvPharmaDoseMSDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvPharmaDoseMSDetail.OptionsView.RowAutoHeight = True
        Me.INDgvPharmaDoseMSDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvPharmaDoseMSDetail.OptionsView.ShowDetailButtons = False
        Me.INDgvPharmaDoseMSDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvPharmaDoseMSDetail, False)
        Me.INDgvPharmaDoseMSDetail.ViewCaption = "Dosis"
        '
        'INDColSelected
        '
        Me.INDColSelected.Caption = "Sel."
        Me.INDColSelected.ColumnEdit = Me.INDRIselector
        Me.INDColSelected.FieldName = "Selected"
        Me.INDColSelected.MinWidth = 21
        Me.INDColSelected.Name = "INDColSelected"
        Me.INDColSelected.Visible = True
        Me.INDColSelected.VisibleIndex = 0
        Me.INDColSelected.Width = 33
        '
        'INDRIselector
        '
        Me.INDRIselector.AutoHeight = False
        Me.INDRIselector.Caption = ""
        Me.INDRIselector.Name = "INDRIselector"
        '
        'GridColumn124
        '
        Me.GridColumn124.Caption = "Producto"
        Me.GridColumn124.FieldName = "ProductCodName"
        Me.GridColumn124.MinWidth = 21
        Me.GridColumn124.Name = "GridColumn124"
        Me.GridColumn124.OptionsColumn.AllowEdit = False
        Me.GridColumn124.Visible = True
        Me.GridColumn124.VisibleIndex = 1
        Me.GridColumn124.Width = 328
        '
        'GridColumn127
        '
        Me.GridColumn127.Caption = "Lote"
        Me.GridColumn127.FieldName = "BatchCode"
        Me.GridColumn127.MinWidth = 21
        Me.GridColumn127.Name = "GridColumn127"
        Me.GridColumn127.OptionsColumn.AllowEdit = False
        Me.GridColumn127.Visible = True
        Me.GridColumn127.VisibleIndex = 2
        Me.GridColumn127.Width = 328
        '
        'GridColumn129
        '
        Me.GridColumn129.Caption = "Aplicado"
        Me.GridColumn129.FieldName = "GridColumn129"
        Me.GridColumn129.MinWidth = 21
        Me.GridColumn129.Name = "GridColumn129"
        Me.GridColumn129.OptionsColumn.AllowEdit = False
        Me.GridColumn129.UnboundExpression = "Iif([AppliedDose] = 1, True, False)"
        Me.GridColumn129.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.GridColumn129.Visible = True
        Me.GridColumn129.VisibleIndex = 3
        Me.GridColumn129.Width = 328
        '
        'GridColumn126
        '
        Me.GridColumn126.Caption = "Estado Entrega"
        Me.GridColumn126.FieldName = "DeliveryStatusName"
        Me.GridColumn126.MinWidth = 21
        Me.GridColumn126.Name = "GridColumn126"
        Me.GridColumn126.OptionsColumn.AllowEdit = False
        Me.GridColumn126.Visible = True
        Me.GridColumn126.VisibleIndex = 4
        Me.GridColumn126.Width = 328
        '
        'GridColumn128
        '
        Me.GridColumn128.Caption = "Cant. A cobrar "
        Me.GridColumn128.FieldName = "QuantityReceivable"
        Me.GridColumn128.MinWidth = 21
        Me.GridColumn128.Name = "GridColumn128"
        Me.GridColumn128.OptionsColumn.AllowEdit = False
        Me.GridColumn128.Visible = True
        Me.GridColumn128.VisibleIndex = 5
        Me.GridColumn128.Width = 333
        '
        'GridColumn125
        '
        Me.GridColumn125.Caption = "Cant. Aplicada"
        Me.GridColumn125.FieldName = "AppliedDose"
        Me.GridColumn125.MinWidth = 21
        Me.GridColumn125.Name = "GridColumn125"
        Me.GridColumn125.OptionsColumn.AllowEdit = False
        Me.GridColumn125.OptionsColumn.ReadOnly = True
        Me.GridColumn125.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn125.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        '
        'INDGcMixingStation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcMixingStation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcMixingStation, Nothing)
        Me.INDGcMixingStation.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDGcMixingStation.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcMixingStation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcMixingStation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcMixingStation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcMixingStation, False)
        GridLevelNode1.LevelTemplate = Me.INDgvPharmaDoseMSDetail
        GridLevelNode1.RelationName = "PharmaDoseMSDetail"
        Me.INDGcMixingStation.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.INDGcMixingStation.Location = New System.Drawing.Point(36, 146)
        Me.INDGcMixingStation.MainView = Me.INDGvMixingStation
        Me.INDGcMixingStation.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcMixingStation.Name = "INDGcMixingStation"
        Me.INDGcMixingStation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptStatus, Me.INDRIselector, Me.INDRptMixingStationGenerated})
        Me.INDGcMixingStation.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcMixingStation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcMixingStation.TabIndex = 36
        Me.INDGcMixingStation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvMixingStation, Me.INDgvPharmaDoseMSDetail})
        '
        'INDGvMixingStation
        '
        Me.INDGvMixingStation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvMixingStation.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvMixingStation.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvMixingStation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvMixingStation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvMixingStation.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvMixingStation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMixingStation.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvMixingStation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMixingStation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvMixingStation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvMixingStation.Appearance.Row.Options.UseFont = True
        Me.INDGvMixingStation.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvMixingStation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvMixingStation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProduct, Me.INDColDeliveryQuantity, Me.INDColApplyQuantity, Me.INDColCurrentQuantity})
        Me.INDGvMixingStation.GridControl = Me.INDGcMixingStation
        Me.INDGvMixingStation.Name = "INDGvMixingStation"
        Me.INDGvMixingStation.OptionsSelection.MultiSelect = True
        Me.INDGvMixingStation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvMixingStation.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvMixingStation.OptionsView.ShowAutoFilterRow = True
        Me.INDGvMixingStation.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvMixingStation, False)
        Me.INDGvMixingStation.ViewCaption = "Kardex"
        '
        'INDColProduct
        '
        Me.INDColProduct.Caption = "Producto"
        Me.INDColProduct.FieldName = "ProductCodName"
        Me.INDColProduct.MinWidth = 21
        Me.INDColProduct.Name = "INDColProduct"
        Me.INDColProduct.OptionsColumn.AllowEdit = False
        Me.INDColProduct.OptionsColumn.AllowFocus = False
        Me.INDColProduct.Visible = True
        Me.INDColProduct.VisibleIndex = 0
        Me.INDColProduct.Width = 415
        '
        'INDColDeliveryQuantity
        '
        Me.INDColDeliveryQuantity.Caption = "Cant. Dispensada"
        Me.INDColDeliveryQuantity.FieldName = "QuantityDelivered"
        Me.INDColDeliveryQuantity.MinWidth = 21
        Me.INDColDeliveryQuantity.Name = "INDColDeliveryQuantity"
        Me.INDColDeliveryQuantity.OptionsColumn.AllowEdit = False
        Me.INDColDeliveryQuantity.OptionsColumn.AllowFocus = False
        Me.INDColDeliveryQuantity.Visible = True
        Me.INDColDeliveryQuantity.VisibleIndex = 1
        Me.INDColDeliveryQuantity.Width = 184
        '
        'INDColApplyQuantity
        '
        Me.INDColApplyQuantity.Caption = "Cant. Dosis Aplicada"
        Me.INDColApplyQuantity.FieldName = "AppliedDose"
        Me.INDColApplyQuantity.MinWidth = 21
        Me.INDColApplyQuantity.Name = "INDColApplyQuantity"
        Me.INDColApplyQuantity.OptionsColumn.AllowEdit = False
        Me.INDColApplyQuantity.OptionsColumn.AllowFocus = False
        Me.INDColApplyQuantity.Visible = True
        Me.INDColApplyQuantity.VisibleIndex = 2
        Me.INDColApplyQuantity.Width = 184
        '
        'INDColCurrentQuantity
        '
        Me.INDColCurrentQuantity.Caption = "Cant. Dosis a cobrar"
        Me.INDColCurrentQuantity.FieldName = "QuantityReceivable"
        Me.INDColCurrentQuantity.MinWidth = 21
        Me.INDColCurrentQuantity.Name = "INDColCurrentQuantity"
        Me.INDColCurrentQuantity.OptionsColumn.AllowEdit = False
        Me.INDColCurrentQuantity.OptionsColumn.AllowFocus = False
        Me.INDColCurrentQuantity.Visible = True
        Me.INDColCurrentQuantity.VisibleIndex = 3
        Me.INDColCurrentQuantity.Width = 189
        '
        'INDRptStatus
        '
        Me.INDRptStatus.AutoHeight = False
        Me.INDRptStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Entrada", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Salida", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Solicitud", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anulacion", CType(4, Byte), -1)})
        Me.INDRptStatus.Name = "INDRptStatus"
        '
        'INDRptMixingStationGenerated
        '
        Me.INDRptMixingStationGenerated.AutoHeight = False
        Me.INDRptMixingStationGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptMixingStationGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptMixingStationGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptMixingStationGenerated.Name = "INDRptMixingStationGenerated"
        Me.INDRptMixingStationGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptMixingStationGenerated.SmallImages = Me.ImageCollection1
        '
        'INDGvSurgeriesPerformed
        '
        Me.INDGvSurgeriesPerformed.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSurgeriesPerformed.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSurgeriesPerformed.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSurgeriesPerformed.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSurgeriesPerformed.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSurgeriesPerformed.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSurgeriesPerformed.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSurgeriesPerformed.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSurgeriesPerformed.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSurgeriesPerformed.Appearance.Row.Options.UseFont = True
        Me.INDGvSurgeriesPerformed.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColSelQx, Me.INDColCode, Me.INDColDescription, Me.INDColMainqx, Me.INDColRouteIn, Me.GridColumn98, Me.GridColumn171, Me.GridColumn187, Me.GridColumn188})
        Me.INDGvSurgeriesPerformed.GridControl = Me.INDgcProceduresQx
        Me.INDGvSurgeriesPerformed.Name = "INDGvSurgeriesPerformed"
        Me.INDGvSurgeriesPerformed.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSurgeriesPerformed.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSurgeriesPerformed.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSurgeriesPerformed.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvSurgeriesPerformed, False)
        Me.INDGvSurgeriesPerformed.ViewCaption = "Qx"
        '
        'ColSelQx
        '
        Me.ColSelQx.Caption = "Sel."
        Me.ColSelQx.ColumnEdit = Me.INDRICESelected
        Me.ColSelQx.FieldName = "Seleccione"
        Me.ColSelQx.MinWidth = 21
        Me.ColSelQx.Name = "ColSelQx"
        Me.ColSelQx.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelQx.OptionsColumn.AllowIncrementalSearch = False
        Me.ColSelQx.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelQx.OptionsColumn.AllowMove = False
        Me.ColSelQx.OptionsColumn.AllowShowHide = False
        Me.ColSelQx.OptionsColumn.AllowSize = False
        Me.ColSelQx.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelQx.OptionsColumn.FixedWidth = True
        Me.ColSelQx.Visible = True
        Me.ColSelQx.VisibleIndex = 0
        Me.ColSelQx.Width = 30
        '
        'INDRICESelected
        '
        Me.INDRICESelected.AutoHeight = False
        Me.INDRICESelected.Name = "INDRICESelected"
        Me.INDRICESelected.ValueChecked = 1
        Me.INDRICESelected.ValueUnchecked = 0
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Código"
        Me.INDColCode.FieldName = "CODSERIPS"
        Me.INDColCode.MinWidth = 21
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 1
        Me.INDColCode.Width = 223
        '
        'INDColDescription
        '
        Me.INDColDescription.Caption = "Descripción"
        Me.INDColDescription.FieldName = "DESSERIPS"
        Me.INDColDescription.MinWidth = 21
        Me.INDColDescription.Name = "INDColDescription"
        Me.INDColDescription.OptionsColumn.AllowEdit = False
        Me.INDColDescription.OptionsColumn.AllowFocus = False
        Me.INDColDescription.Visible = True
        Me.INDColDescription.VisibleIndex = 2
        Me.INDColDescription.Width = 223
        '
        'INDColMainqx
        '
        Me.INDColMainqx.Caption = "Principal"
        Me.INDColMainqx.FieldName = "QXPRINCIP"
        Me.INDColMainqx.MinWidth = 21
        Me.INDColMainqx.Name = "INDColMainqx"
        Me.INDColMainqx.OptionsColumn.AllowEdit = False
        Me.INDColMainqx.OptionsColumn.AllowFocus = False
        Me.INDColMainqx.Visible = True
        Me.INDColMainqx.VisibleIndex = 3
        Me.INDColMainqx.Width = 223
        '
        'INDColRouteIn
        '
        Me.INDColRouteIn.Caption = "Vía Abordaje"
        Me.INDColRouteIn.FieldName = "CODVIAABO"
        Me.INDColRouteIn.MinWidth = 21
        Me.INDColRouteIn.Name = "INDColRouteIn"
        Me.INDColRouteIn.OptionsColumn.AllowEdit = False
        Me.INDColRouteIn.OptionsColumn.AllowFocus = False
        Me.INDColRouteIn.Visible = True
        Me.INDColRouteIn.VisibleIndex = 4
        Me.INDColRouteIn.Width = 223
        '
        'GridColumn98
        '
        Me.GridColumn98.Caption = "Fecha Realización"
        Me.GridColumn98.FieldName = "FechaRealizacion"
        Me.GridColumn98.MinWidth = 21
        Me.GridColumn98.Name = "GridColumn98"
        Me.GridColumn98.OptionsColumn.AllowEdit = False
        Me.GridColumn98.OptionsColumn.AllowFocus = False
        Me.GridColumn98.Visible = True
        Me.GridColumn98.VisibleIndex = 5
        Me.GridColumn98.Width = 223
        '
        'GridColumn171
        '
        Me.GridColumn171.Caption = "Descripción Relacionada"
        Me.GridColumn171.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn171.MinWidth = 21
        Me.GridColumn171.Name = "GridColumn171"
        Me.GridColumn171.OptionsColumn.AllowEdit = False
        Me.GridColumn171.OptionsColumn.AllowFocus = False
        Me.GridColumn171.Visible = True
        Me.GridColumn171.VisibleIndex = 6
        '
        'GridColumn187
        '
        Me.GridColumn187.Caption = "Justificación"
        Me.GridColumn187.FieldName = "JustificationCodeName"
        Me.GridColumn187.MinWidth = 21
        Me.GridColumn187.Name = "GridColumn187"
        Me.GridColumn187.OptionsColumn.AllowEdit = False
        Me.GridColumn187.OptionsColumn.AllowFocus = False
        Me.GridColumn187.Visible = True
        Me.GridColumn187.VisibleIndex = 7
        Me.GridColumn187.Width = 223
        '
        'GridColumn188
        '
        Me.GridColumn188.Caption = "Liquida"
        Me.GridColumn188.ColumnEdit = Me.INDRICESkipLiquidationQx
        Me.GridColumn188.FieldName = "SkipLiquidation"
        Me.GridColumn188.MinWidth = 21
        Me.GridColumn188.Name = "GridColumn188"
        Me.GridColumn188.OptionsColumn.AllowEdit = False
        Me.GridColumn188.OptionsColumn.AllowFocus = False
        Me.GridColumn188.Visible = True
        Me.GridColumn188.VisibleIndex = 8
        Me.GridColumn188.Width = 232
        '
        'INDRICESkipLiquidationQx
        '
        Me.INDRICESkipLiquidationQx.AutoHeight = False
        Me.INDRICESkipLiquidationQx.Name = "INDRICESkipLiquidationQx"
        Me.INDRICESkipLiquidationQx.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDgcProceduresQx
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcProceduresQx, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcProceduresQx, Nothing)
        Me.INDgcProceduresQx.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcProceduresQx.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcProceduresQx, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcProceduresQx, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcProceduresQx, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcProceduresQx, False)
        GridLevelNode3.LevelTemplate = Me.INDGvSurgeriesPerformed
        GridLevelNode3.RelationName = "Detail"
        Me.INDgcProceduresQx.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.INDgcProceduresQx.Location = New System.Drawing.Point(36, 146)
        Me.INDgcProceduresQx.MainView = Me.INDgvProceduresQx
        Me.INDgcProceduresQx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcProceduresQx.Name = "INDgcProceduresQx"
        Me.INDgcProceduresQx.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptPceDetailProceduresQx, Me.INDRICESkipLiquidationQx, Me.INDRICESelected, Me.INDRptImgGenerated})
        Me.INDgcProceduresQx.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcProceduresQx, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcProceduresQx.TabIndex = 20
        Me.INDgcProceduresQx.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvProceduresQx, Me.INDGvSurgeriesPerformed})
        '
        'INDgvProceduresQx
        '
        Me.INDgvProceduresQx.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvProceduresQx.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvProceduresQx.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvProceduresQx.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvProceduresQx.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvProceduresQx.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvProceduresQx.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvProceduresQx.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvProceduresQx.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvProceduresQx.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvProceduresQx.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvProceduresQx.Appearance.Row.Options.UseFont = True
        Me.INDgvProceduresQx.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvProceduresQx.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvProceduresQx.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn46, Me.GridColumn41, Me.GridColumn43, Me.GridColumn44, Me.GridColumn10, Me.GridColumn45})
        Me.INDgvProceduresQx.CustomizationFormBounds = New System.Drawing.Rectangle(1260, 623, 210, 227)
        Me.INDgvProceduresQx.GridControl = Me.INDgcProceduresQx
        Me.INDgvProceduresQx.GroupCount = 1
        Me.INDgvProceduresQx.Name = "INDgvProceduresQx"
        Me.INDgvProceduresQx.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvProceduresQx.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvProceduresQx.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvProceduresQx.OptionsView.ShowAutoFilterRow = True
        Me.INDgvProceduresQx.OptionsView.ShowGroupPanel = False
        Me.INDgvProceduresQx.OptionsView.ShowViewCaption = True
        Me.INDgvProceduresQx.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn46, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvProceduresQx, False)
        Me.INDgvProceduresQx.ViewCaption = "Procedimientos Qx"
        '
        'GridColumn46
        '
        Me.GridColumn46.Caption = "Tipo"
        Me.GridColumn46.FieldName = "Tipo"
        Me.GridColumn46.MinWidth = 21
        Me.GridColumn46.Name = "GridColumn46"
        Me.GridColumn46.Visible = True
        Me.GridColumn46.VisibleIndex = 0
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "Codigo"
        Me.GridColumn41.FieldName = "CODSERIPS"
        Me.GridColumn41.MinWidth = 21
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.OptionsColumn.AllowEdit = False
        Me.GridColumn41.OptionsColumn.AllowFocus = False
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 0
        Me.GridColumn41.Width = 138
        '
        'GridColumn43
        '
        Me.GridColumn43.Caption = "Procedimientos QX"
        Me.GridColumn43.FieldName = "DESSERIPS"
        Me.GridColumn43.MinWidth = 21
        Me.GridColumn43.Name = "GridColumn43"
        Me.GridColumn43.OptionsColumn.AllowEdit = False
        Me.GridColumn43.OptionsColumn.AllowFocus = False
        Me.GridColumn43.Visible = True
        Me.GridColumn43.VisibleIndex = 1
        Me.GridColumn43.Width = 388
        '
        'GridColumn44
        '
        Me.GridColumn44.Caption = "Cantidad"
        Me.GridColumn44.FieldName = "TOTAL"
        Me.GridColumn44.MinWidth = 21
        Me.GridColumn44.Name = "GridColumn44"
        Me.GridColumn44.OptionsColumn.AllowEdit = False
        Me.GridColumn44.OptionsColumn.AllowFocus = False
        Me.GridColumn44.Visible = True
        Me.GridColumn44.VisibleIndex = 3
        Me.GridColumn44.Width = 144
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Es Multiple"
        Me.GridColumn10.FieldName = "IsMultiple"
        Me.GridColumn10.MinWidth = 21
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 2
        Me.GridColumn10.Width = 120
        '
        'GridColumn45
        '
        Me.GridColumn45.Caption = "Ver Detalle"
        Me.GridColumn45.ColumnEdit = Me.INDrptPceDetailProceduresQx
        Me.GridColumn45.MinWidth = 21
        Me.GridColumn45.Name = "GridColumn45"
        Me.GridColumn45.Visible = True
        Me.GridColumn45.VisibleIndex = 4
        Me.GridColumn45.Width = 213
        '
        'INDrptPceDetailProceduresQx
        '
        Me.INDrptPceDetailProceduresQx.AutoHeight = False
        Me.INDrptPceDetailProceduresQx.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptPceDetailProceduresQx.Name = "INDrptPceDetailProceduresQx"
        Me.INDrptPceDetailProceduresQx.PopupSizeable = False
        Me.INDrptPceDetailProceduresQx.ShowPopupCloseButton = False
        Me.INDrptPceDetailProceduresQx.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptImgGenerated
        '
        Me.INDRptImgGenerated.Appearance.Options.UseTextOptions = True
        Me.INDRptImgGenerated.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDRptImgGenerated.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDRptImgGenerated.AppearanceItemSelected.Options.UseTextOptions = True
        Me.INDRptImgGenerated.AppearanceItemSelected.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDRptImgGenerated.AppearanceItemSelected.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDRptImgGenerated.AppearanceReadOnly.Options.UseTextOptions = True
        Me.INDRptImgGenerated.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDRptImgGenerated.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDRptImgGenerated.AutoHeight = False
        Me.INDRptImgGenerated.ContextButtonOptions.ShowToolTips = False
        Me.INDRptImgGenerated.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDRptImgGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptImgGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", 0, 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", 1, 1)})
        Me.INDRptImgGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptImgGenerated.Name = "INDRptImgGenerated"
        Me.INDRptImgGenerated.ReadOnly = True
        Me.INDRptImgGenerated.ShowPopupShadow = False
        Me.INDRptImgGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptImgGenerated.SmallImages = Me.ImageCollection1
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDrptPceHemo
        '
        Me.INDrptPceHemo.AutoHeight = False
        Me.INDrptPceHemo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptPceHemo.Name = "INDrptPceHemo"
        Me.INDrptPceHemo.PopupControl = Me.INDPccHemo
        Me.INDrptPceHemo.PopupSizeable = False
        Me.INDrptPceHemo.ShowPopupCloseButton = False
        Me.INDrptPceHemo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPccHemo
        '
        Me.INDPccHemo.Controls.Add(Me.INDLcHemo)
        Me.INDPccHemo.Location = New System.Drawing.Point(125, 429)
        Me.INDPccHemo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPccHemo.Name = "INDPccHemo"
        Me.INDPccHemo.Size = New System.Drawing.Size(837, 282)
        Me.INDPccHemo.TabIndex = 35
        '
        'INDLcHemo
        '
        Me.INDLcHemo.Controls.Add(Me.INDteComponente)
        Me.INDLcHemo.Controls.Add(Me.INDteNumeroUnidad)
        Me.INDLcHemo.Controls.Add(Me.INDteSelloCalidad)
        Me.INDLcHemo.Controls.Add(Me.INDrgPruebaCru)
        Me.INDLcHemo.Controls.Add(Me.INDrgRastreoAnti)
        Me.INDLcHemo.Controls.Add(Me.INDteBacEntrega)
        Me.INDLcHemo.Controls.Add(Me.INDteMedReaSolicitudReserva)
        Me.INDLcHemo.Controls.Add(Me.INDteMedReaSolicitudTransfusion)
        Me.INDLcHemo.Controls.Add(Me.INDteMedRealizoRegTransfusión)
        Me.INDLcHemo.Controls.Add(Me.INDteEnfRealizoRegistroAplicacion)
        Me.INDLcHemo.Controls.Add(Me.INDteFechaEntrega)
        Me.INDLcHemo.Controls.Add(Me.INDteFechaExpiracion)
        Me.INDLcHemo.Controls.Add(Me.INDteFechaAplicacionMed)
        Me.INDLcHemo.Controls.Add(Me.INDTeDateEndTrans)
        Me.INDLcHemo.Controls.Add(Me.INDteFechaAplicacionEnf)
        Me.INDLcHemo.Controls.Add(Me.INDTeDateIniTrans)
        Me.INDLcHemo.Location = New System.Drawing.Point(0, 0)
        Me.INDLcHemo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDLcHemo.Name = "INDLcHemo"
        Me.INDLcHemo.Root = Me.LayoutControlGroup4
        Me.INDLcHemo.Size = New System.Drawing.Size(837, 282)
        Me.INDLcHemo.TabIndex = 0
        Me.INDLcHemo.Text = "LayoutControl2"
        '
        'INDteComponente
        '
        Me.INDteComponente.Location = New System.Drawing.Point(186, 58)
        Me.INDteComponente.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteComponente.Name = "INDteComponente"
        Me.INDteComponente.Properties.ReadOnly = True
        Me.INDteComponente.Size = New System.Drawing.Size(342, 20)
        Me.INDteComponente.StyleController = Me.INDLcHemo
        Me.INDteComponente.TabIndex = 5
        '
        'INDteNumeroUnidad
        '
        Me.INDteNumeroUnidad.Location = New System.Drawing.Point(186, 106)
        Me.INDteNumeroUnidad.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteNumeroUnidad.Name = "INDteNumeroUnidad"
        Me.INDteNumeroUnidad.Properties.ReadOnly = True
        Me.INDteNumeroUnidad.Size = New System.Drawing.Size(342, 20)
        Me.INDteNumeroUnidad.StyleController = Me.INDLcHemo
        Me.INDteNumeroUnidad.TabIndex = 6
        '
        'INDteSelloCalidad
        '
        Me.INDteSelloCalidad.Location = New System.Drawing.Point(186, 130)
        Me.INDteSelloCalidad.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteSelloCalidad.Name = "INDteSelloCalidad"
        Me.INDteSelloCalidad.Properties.ReadOnly = True
        Me.INDteSelloCalidad.Size = New System.Drawing.Size(342, 20)
        Me.INDteSelloCalidad.StyleController = Me.INDLcHemo
        Me.INDteSelloCalidad.TabIndex = 7
        '
        'INDrgPruebaCru
        '
        Me.INDrgPruebaCru.Location = New System.Drawing.Point(186, 178)
        Me.INDrgPruebaCru.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDrgPruebaCru.Name = "INDrgPruebaCru"
        Me.INDrgPruebaCru.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDrgPruebaCru.Properties.ReadOnly = True
        Me.INDrgPruebaCru.Size = New System.Drawing.Size(342, 21)
        Me.INDrgPruebaCru.StyleController = Me.INDLcHemo
        Me.INDrgPruebaCru.TabIndex = 9
        '
        'INDrgRastreoAnti
        '
        Me.INDrgRastreoAnti.Location = New System.Drawing.Point(186, 203)
        Me.INDrgRastreoAnti.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDrgRastreoAnti.Name = "INDrgRastreoAnti"
        Me.INDrgRastreoAnti.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDrgRastreoAnti.Properties.ReadOnly = True
        Me.INDrgRastreoAnti.Size = New System.Drawing.Size(342, 21)
        Me.INDrgRastreoAnti.StyleController = Me.INDLcHemo
        Me.INDrgRastreoAnti.TabIndex = 10
        '
        'INDteBacEntrega
        '
        Me.INDteBacEntrega.Location = New System.Drawing.Point(694, 58)
        Me.INDteBacEntrega.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteBacEntrega.Name = "INDteBacEntrega"
        Me.INDteBacEntrega.Properties.ReadOnly = True
        Me.INDteBacEntrega.Size = New System.Drawing.Size(119, 20)
        Me.INDteBacEntrega.StyleController = Me.INDLcHemo
        Me.INDteBacEntrega.TabIndex = 11
        '
        'INDteMedReaSolicitudReserva
        '
        Me.INDteMedReaSolicitudReserva.Location = New System.Drawing.Point(694, 82)
        Me.INDteMedReaSolicitudReserva.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteMedReaSolicitudReserva.Name = "INDteMedReaSolicitudReserva"
        Me.INDteMedReaSolicitudReserva.Properties.ReadOnly = True
        Me.INDteMedReaSolicitudReserva.Size = New System.Drawing.Size(119, 20)
        Me.INDteMedReaSolicitudReserva.StyleController = Me.INDLcHemo
        Me.INDteMedReaSolicitudReserva.TabIndex = 12
        '
        'INDteMedReaSolicitudTransfusion
        '
        Me.INDteMedReaSolicitudTransfusion.Location = New System.Drawing.Point(694, 106)
        Me.INDteMedReaSolicitudTransfusion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteMedReaSolicitudTransfusion.Name = "INDteMedReaSolicitudTransfusion"
        Me.INDteMedReaSolicitudTransfusion.Properties.ReadOnly = True
        Me.INDteMedReaSolicitudTransfusion.Size = New System.Drawing.Size(119, 20)
        Me.INDteMedReaSolicitudTransfusion.StyleController = Me.INDLcHemo
        Me.INDteMedReaSolicitudTransfusion.TabIndex = 13
        '
        'INDteMedRealizoRegTransfusión
        '
        Me.INDteMedRealizoRegTransfusión.Location = New System.Drawing.Point(694, 130)
        Me.INDteMedRealizoRegTransfusión.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteMedRealizoRegTransfusión.Name = "INDteMedRealizoRegTransfusión"
        Me.INDteMedRealizoRegTransfusión.Properties.ReadOnly = True
        Me.INDteMedRealizoRegTransfusión.Size = New System.Drawing.Size(119, 20)
        Me.INDteMedRealizoRegTransfusión.StyleController = Me.INDLcHemo
        Me.INDteMedRealizoRegTransfusión.TabIndex = 14
        '
        'INDteEnfRealizoRegistroAplicacion
        '
        Me.INDteEnfRealizoRegistroAplicacion.Location = New System.Drawing.Point(694, 154)
        Me.INDteEnfRealizoRegistroAplicacion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteEnfRealizoRegistroAplicacion.Name = "INDteEnfRealizoRegistroAplicacion"
        Me.INDteEnfRealizoRegistroAplicacion.Properties.ReadOnly = True
        Me.INDteEnfRealizoRegistroAplicacion.Size = New System.Drawing.Size(119, 20)
        Me.INDteEnfRealizoRegistroAplicacion.StyleController = Me.INDLcHemo
        Me.INDteEnfRealizoRegistroAplicacion.TabIndex = 15
        '
        'INDteFechaEntrega
        '
        Me.INDteFechaEntrega.Location = New System.Drawing.Point(186, 82)
        Me.INDteFechaEntrega.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteFechaEntrega.Name = "INDteFechaEntrega"
        Me.INDteFechaEntrega.Properties.ReadOnly = True
        Me.INDteFechaEntrega.Size = New System.Drawing.Size(342, 20)
        Me.INDteFechaEntrega.StyleController = Me.INDLcHemo
        Me.INDteFechaEntrega.TabIndex = 16
        '
        'INDteFechaExpiracion
        '
        Me.INDteFechaExpiracion.Location = New System.Drawing.Point(186, 154)
        Me.INDteFechaExpiracion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteFechaExpiracion.Name = "INDteFechaExpiracion"
        Me.INDteFechaExpiracion.Properties.ReadOnly = True
        Me.INDteFechaExpiracion.Size = New System.Drawing.Size(342, 20)
        Me.INDteFechaExpiracion.StyleController = Me.INDLcHemo
        Me.INDteFechaExpiracion.TabIndex = 17
        '
        'INDteFechaAplicacionMed
        '
        Me.INDteFechaAplicacionMed.Location = New System.Drawing.Point(694, 178)
        Me.INDteFechaAplicacionMed.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteFechaAplicacionMed.Name = "INDteFechaAplicacionMed"
        Me.INDteFechaAplicacionMed.Properties.ReadOnly = True
        Me.INDteFechaAplicacionMed.Size = New System.Drawing.Size(119, 20)
        Me.INDteFechaAplicacionMed.StyleController = Me.INDLcHemo
        Me.INDteFechaAplicacionMed.TabIndex = 18
        '
        'INDTeDateEndTrans
        '
        Me.INDTeDateEndTrans.Location = New System.Drawing.Point(694, 227)
        Me.INDTeDateEndTrans.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDTeDateEndTrans.Name = "INDTeDateEndTrans"
        Me.INDTeDateEndTrans.Properties.ReadOnly = True
        Me.INDTeDateEndTrans.Size = New System.Drawing.Size(119, 20)
        Me.INDTeDateEndTrans.StyleController = Me.INDLcHemo
        Me.INDTeDateEndTrans.TabIndex = 20
        '
        'INDteFechaAplicacionEnf
        '
        Me.INDteFechaAplicacionEnf.Location = New System.Drawing.Point(694, 203)
        Me.INDteFechaAplicacionEnf.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDteFechaAplicacionEnf.Name = "INDteFechaAplicacionEnf"
        Me.INDteFechaAplicacionEnf.Properties.ReadOnly = True
        Me.INDteFechaAplicacionEnf.Size = New System.Drawing.Size(119, 20)
        Me.INDteFechaAplicacionEnf.StyleController = Me.INDLcHemo
        Me.INDteFechaAplicacionEnf.TabIndex = 21
        '
        'INDTeDateIniTrans
        '
        Me.INDTeDateIniTrans.Location = New System.Drawing.Point(186, 228)
        Me.INDTeDateIniTrans.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDTeDateIniTrans.Name = "INDTeDateIniTrans"
        Me.INDTeDateIniTrans.Properties.ReadOnly = True
        Me.INDTeDateIniTrans.Size = New System.Drawing.Size(342, 20)
        Me.INDTeDateIniTrans.StyleController = Me.INDLcHemo
        Me.INDTeDateIniTrans.TabIndex = 19
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup7})
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(837, 282)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlGroup7
        '
        Me.LayoutControlGroup7.CaptionImageOptions.Image = CType(resources.GetObject("LayoutControlGroup7.CaptionImageOptions.Image"), System.Drawing.Image)
        Me.LayoutControlGroup7.CustomizationFormText = "Información Componente"
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem35, Me.LayoutControlItem36, Me.LayoutControlItem37, Me.LayoutControlItem38, Me.LayoutControlItem40, Me.LayoutControlItem41, Me.LayoutControlItem42, Me.LayoutControlItem43, Me.LayoutControlItem44, Me.LayoutControlItem45, Me.LayoutControlItem46, Me.LayoutControlItem47, Me.LayoutControlItem48, Me.LayoutControlItem49, Me.LayoutControlItem50, Me.LayoutControlItem51})
        Me.LayoutControlGroup7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup7.Name = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(817, 262)
        Me.LayoutControlGroup7.Text = "Información Componente"
        '
        'LayoutControlItem35
        '
        Me.LayoutControlItem35.Control = Me.INDteComponente
        Me.LayoutControlItem35.CustomizationFormText = "Componente"
        Me.LayoutControlItem35.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem35.Name = "LayoutControlItem35"
        Me.LayoutControlItem35.Size = New System.Drawing.Size(508, 24)
        Me.LayoutControlItem35.Text = "Componente"
        Me.LayoutControlItem35.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem36
        '
        Me.LayoutControlItem36.Control = Me.INDteNumeroUnidad
        Me.LayoutControlItem36.CustomizationFormText = "# Unidad"
        Me.LayoutControlItem36.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem36.Name = "LayoutControlItem36"
        Me.LayoutControlItem36.Size = New System.Drawing.Size(508, 24)
        Me.LayoutControlItem36.Text = "# Unidad"
        Me.LayoutControlItem36.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem37
        '
        Me.LayoutControlItem37.Control = Me.INDteSelloCalidad
        Me.LayoutControlItem37.CustomizationFormText = "Sello de Calidad"
        Me.LayoutControlItem37.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem37.Name = "LayoutControlItem37"
        Me.LayoutControlItem37.Size = New System.Drawing.Size(508, 24)
        Me.LayoutControlItem37.Text = "Sello de Calidad"
        Me.LayoutControlItem37.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem38
        '
        Me.LayoutControlItem38.Control = Me.INDrgPruebaCru
        Me.LayoutControlItem38.CustomizationFormText = "Realizó Prueba Cruzada"
        Me.LayoutControlItem38.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem38.MaxSize = New System.Drawing.Size(0, 25)
        Me.LayoutControlItem38.MinSize = New System.Drawing.Size(390, 25)
        Me.LayoutControlItem38.Name = "LayoutControlItem38"
        Me.LayoutControlItem38.Size = New System.Drawing.Size(508, 25)
        Me.LayoutControlItem38.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem38.Text = "Realizó Prueba Cruzada"
        Me.LayoutControlItem38.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem40
        '
        Me.LayoutControlItem40.Control = Me.INDrgRastreoAnti
        Me.LayoutControlItem40.CustomizationFormText = "Realizó Rastreo de Anticuerpos"
        Me.LayoutControlItem40.Location = New System.Drawing.Point(0, 145)
        Me.LayoutControlItem40.MaxSize = New System.Drawing.Size(0, 25)
        Me.LayoutControlItem40.MinSize = New System.Drawing.Size(390, 25)
        Me.LayoutControlItem40.Name = "LayoutControlItem40"
        Me.LayoutControlItem40.Size = New System.Drawing.Size(508, 25)
        Me.LayoutControlItem40.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem40.Text = "Realizó Rastreo de Anticuerpos"
        Me.LayoutControlItem40.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem41
        '
        Me.LayoutControlItem41.Control = Me.INDteBacEntrega
        Me.LayoutControlItem41.CustomizationFormText = "Bacteriologo(a) que entrega"
        Me.LayoutControlItem41.Location = New System.Drawing.Point(508, 0)
        Me.LayoutControlItem41.Name = "LayoutControlItem41"
        Me.LayoutControlItem41.Size = New System.Drawing.Size(285, 24)
        Me.LayoutControlItem41.Text = "Bacteriologo(a) que entrega"
        Me.LayoutControlItem41.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem42
        '
        Me.LayoutControlItem42.Control = Me.INDteMedReaSolicitudReserva
        Me.LayoutControlItem42.CustomizationFormText = "Med Realizó Solicitud Reserva"
        Me.LayoutControlItem42.Location = New System.Drawing.Point(508, 24)
        Me.LayoutControlItem42.Name = "LayoutControlItem42"
        Me.LayoutControlItem42.Size = New System.Drawing.Size(285, 24)
        Me.LayoutControlItem42.Text = "Med Realizó Solicitud Reserva"
        Me.LayoutControlItem42.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem43
        '
        Me.LayoutControlItem43.Control = Me.INDteMedReaSolicitudTransfusion
        Me.LayoutControlItem43.CustomizationFormText = "Med Realizó Solicitud Transfusión"
        Me.LayoutControlItem43.Location = New System.Drawing.Point(508, 48)
        Me.LayoutControlItem43.Name = "LayoutControlItem43"
        Me.LayoutControlItem43.Size = New System.Drawing.Size(285, 24)
        Me.LayoutControlItem43.Text = "Med Realizó Solicitud Transfusión"
        Me.LayoutControlItem43.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem44
        '
        Me.LayoutControlItem44.Control = Me.INDteMedRealizoRegTransfusión
        Me.LayoutControlItem44.CustomizationFormText = "Med Realizó Registro Transfusión"
        Me.LayoutControlItem44.Location = New System.Drawing.Point(508, 72)
        Me.LayoutControlItem44.Name = "LayoutControlItem44"
        Me.LayoutControlItem44.Size = New System.Drawing.Size(285, 24)
        Me.LayoutControlItem44.Text = "Med Realizó Registro Transfusión"
        Me.LayoutControlItem44.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem45
        '
        Me.LayoutControlItem45.Control = Me.INDteEnfRealizoRegistroAplicacion
        Me.LayoutControlItem45.CustomizationFormText = "Enf Realizó Registro Aplicación"
        Me.LayoutControlItem45.Location = New System.Drawing.Point(508, 96)
        Me.LayoutControlItem45.Name = "LayoutControlItem45"
        Me.LayoutControlItem45.Size = New System.Drawing.Size(285, 24)
        Me.LayoutControlItem45.Text = "Enf Realizó Registro Aplicación"
        Me.LayoutControlItem45.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem46
        '
        Me.LayoutControlItem46.Control = Me.INDteFechaEntrega
        Me.LayoutControlItem46.CustomizationFormText = "Fecha Entrega"
        Me.LayoutControlItem46.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem46.Name = "LayoutControlItem46"
        Me.LayoutControlItem46.Size = New System.Drawing.Size(508, 24)
        Me.LayoutControlItem46.Text = "Fecha Entrega"
        Me.LayoutControlItem46.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem47
        '
        Me.LayoutControlItem47.Control = Me.INDteFechaExpiracion
        Me.LayoutControlItem47.CustomizationFormText = "Fecha Expiración"
        Me.LayoutControlItem47.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem47.Name = "LayoutControlItem47"
        Me.LayoutControlItem47.Size = New System.Drawing.Size(508, 24)
        Me.LayoutControlItem47.Text = "Fecha Expiración"
        Me.LayoutControlItem47.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem48
        '
        Me.LayoutControlItem48.Control = Me.INDteFechaAplicacionMed
        Me.LayoutControlItem48.CustomizationFormText = "Fecha Aplicación Médico"
        Me.LayoutControlItem48.Location = New System.Drawing.Point(508, 120)
        Me.LayoutControlItem48.Name = "LayoutControlItem48"
        Me.LayoutControlItem48.Size = New System.Drawing.Size(285, 25)
        Me.LayoutControlItem48.Text = "Fecha Aplicación Médico"
        Me.LayoutControlItem48.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem49
        '
        Me.LayoutControlItem49.Control = Me.INDTeDateEndTrans
        Me.LayoutControlItem49.CustomizationFormText = "Fecha Hora Final Transfusión"
        Me.LayoutControlItem49.Location = New System.Drawing.Point(508, 169)
        Me.LayoutControlItem49.Name = "LayoutControlItem49"
        Me.LayoutControlItem49.Size = New System.Drawing.Size(285, 35)
        Me.LayoutControlItem49.Text = "Fecha Hora Final Transfusión"
        Me.LayoutControlItem49.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem50
        '
        Me.LayoutControlItem50.Control = Me.INDteFechaAplicacionEnf
        Me.LayoutControlItem50.CustomizationFormText = "Fecha Aplicación Entermería"
        Me.LayoutControlItem50.Location = New System.Drawing.Point(508, 145)
        Me.LayoutControlItem50.Name = "LayoutControlItem50"
        Me.LayoutControlItem50.Size = New System.Drawing.Size(285, 24)
        Me.LayoutControlItem50.Text = "Fecha Aplicación Entermería"
        Me.LayoutControlItem50.TextSize = New System.Drawing.Size(159, 13)
        '
        'LayoutControlItem51
        '
        Me.LayoutControlItem51.Control = Me.INDTeDateIniTrans
        Me.LayoutControlItem51.CustomizationFormText = "Fecha Hora Inicial Transfusión"
        Me.LayoutControlItem51.Location = New System.Drawing.Point(0, 170)
        Me.LayoutControlItem51.Name = "LayoutControlItem51"
        Me.LayoutControlItem51.Size = New System.Drawing.Size(508, 34)
        Me.LayoutControlItem51.Text = "Fecha Hora Inicial Transfusión"
        Me.LayoutControlItem51.TextSize = New System.Drawing.Size(159, 13)
        '
        'INDrptChkHemo
        '
        Me.INDrptChkHemo.AutoHeight = False
        Me.INDrptChkHemo.Name = "INDrptChkHemo"
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDSbJustification)
        Me.INDlcRoot.Controls.Add(Me.INDpccServicesProcedures)
        Me.INDlcRoot.Controls.Add(Me.INDGcMixingStation)
        Me.INDlcRoot.Controls.Add(Me.INDPccHemo)
        Me.INDlcRoot.Controls.Add(Me.INDGcHemoDetail)
        Me.INDlcRoot.Controls.Add(Me.INDSleAdmissionNumber2)
        Me.INDlcRoot.Controls.Add(Me.LblState)
        Me.INDlcRoot.Controls.Add(Me.SleOperatingUnit)
        Me.INDlcRoot.Controls.Add(Me.DdbMenu)
        Me.INDlcRoot.Controls.Add(Me.PccAdmission)
        Me.INDlcRoot.Controls.Add(Me.INDsbClear)
        Me.INDlcRoot.Controls.Add(Me.INDsbGererateServiceOrder)
        Me.INDlcRoot.Controls.Add(Me.INDgcValoraciones)
        Me.INDlcRoot.Controls.Add(Me.INDgcNurseProcedure)
        Me.INDlcRoot.Controls.Add(Me.INDgcOxygenConsumption)
        Me.INDlcRoot.Controls.Add(Me.INDgcTerapy)
        Me.INDlcRoot.Controls.Add(Me.INDgcConsultation)
        Me.INDlcRoot.Controls.Add(Me.INDgcProceduresNoQX)
        Me.INDlcRoot.Controls.Add(Me.INDgcProceduresQx)
        Me.INDlcRoot.Controls.Add(Me.INDgcImagesDx)
        Me.INDlcRoot.Controls.Add(Me.INDgcPathology)
        Me.INDlcRoot.Controls.Add(Me.INDgcLaboratories)
        Me.INDlcRoot.Controls.Add(Me.INDgcMedicineSupplier)
        Me.INDlcRoot.Controls.Add(Me.INDGcStays)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(2, 8)
        Me.INDlcRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1020, 236, 574, 569)
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(1681, 576)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDSbJustification
        '
        Me.INDSbJustification.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbJustification.Appearance.Options.UseFont = True
        Me.INDSbJustification.Location = New System.Drawing.Point(1055, 53)
        Me.INDSbJustification.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbJustification, False)
        Me.INDSbJustification.Name = "INDSbJustification"
        Me.INDSbJustification.Size = New System.Drawing.Size(120, 28)
        Me.INDSbJustification.StyleController = Me.INDlcRoot
        Me.INDSbJustification.TabIndex = 37
        Me.INDSbJustification.Text = "Generar Justificación"
        '
        'INDpccServicesProcedures
        '
        Me.INDpccServicesProcedures.Controls.Add(Me.LayoutControl1)
        Me.INDpccServicesProcedures.Location = New System.Drawing.Point(69, 538)
        Me.INDpccServicesProcedures.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDpccServicesProcedures.Name = "INDpccServicesProcedures"
        Me.INDpccServicesProcedures.Size = New System.Drawing.Size(921, 583)
        Me.INDpccServicesProcedures.TabIndex = 31
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDTxtQxTime)
        Me.LayoutControl1.Controls.Add(Me.INDTxtTipoAnestesia)
        Me.LayoutControl1.Controls.Add(Me.INDTxtMuestraPatologicas)
        Me.LayoutControl1.Controls.Add(Me.INDGcQxEquipe)
        Me.LayoutControl1.Controls.Add(Me.INDGcProceduresRealizados)
        Me.LayoutControl1.Controls.Add(Me.INDgcDetailServiciosNoRealizados)
        Me.LayoutControl1.Controls.Add(Me.INDgcDetailServiciosRealizados)
        Me.LayoutControl1.Controls.Add(Me.INDTxtObservaciones)
        Me.LayoutControl1.Controls.Add(Me.INDTxtProcedimientosRealizados)
        Me.LayoutControl1.Controls.Add(Me.INDMeHallazgosOperatorios)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup2
        Me.LayoutControl1.Size = New System.Drawing.Size(921, 583)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDTxtQxTime
        '
        Me.INDTxtQxTime.Location = New System.Drawing.Point(260, 96)
        Me.INDTxtQxTime.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDTxtQxTime.MaximumSize = New System.Drawing.Size(120, 26)
        Me.INDTxtQxTime.MinimumSize = New System.Drawing.Size(219, 26)
        Me.INDTxtQxTime.Name = "INDTxtQxTime"
        Me.INDTxtQxTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTxtQxTime.Properties.Appearance.Options.UseFont = True
        Me.INDTxtQxTime.Properties.AutoHeight = False
        Me.INDTxtQxTime.Properties.ReadOnly = True
        Me.INDTxtQxTime.Size = New System.Drawing.Size(219, 26)
        Me.INDTxtQxTime.StyleController = Me.LayoutControl1
        Me.INDTxtQxTime.TabIndex = 11
        '
        'INDTxtTipoAnestesia
        '
        Me.INDTxtTipoAnestesia.Location = New System.Drawing.Point(260, 164)
        Me.INDTxtTipoAnestesia.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDTxtTipoAnestesia.MaximumSize = New System.Drawing.Size(120, 26)
        Me.INDTxtTipoAnestesia.MinimumSize = New System.Drawing.Size(219, 26)
        Me.INDTxtTipoAnestesia.Name = "INDTxtTipoAnestesia"
        Me.INDTxtTipoAnestesia.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTxtTipoAnestesia.Properties.Appearance.Options.UseFont = True
        Me.INDTxtTipoAnestesia.Properties.AutoHeight = False
        Me.INDTxtTipoAnestesia.Properties.ReadOnly = True
        Me.INDTxtTipoAnestesia.Size = New System.Drawing.Size(219, 26)
        Me.INDTxtTipoAnestesia.StyleController = Me.LayoutControl1
        Me.INDTxtTipoAnestesia.TabIndex = 10
        '
        'INDTxtMuestraPatologicas
        '
        Me.INDTxtMuestraPatologicas.Location = New System.Drawing.Point(260, 130)
        Me.INDTxtMuestraPatologicas.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDTxtMuestraPatologicas.MaximumSize = New System.Drawing.Size(180, 26)
        Me.INDTxtMuestraPatologicas.MinimumSize = New System.Drawing.Size(219, 26)
        Me.INDTxtMuestraPatologicas.Name = "INDTxtMuestraPatologicas"
        Me.INDTxtMuestraPatologicas.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTxtMuestraPatologicas.Properties.Appearance.Options.UseFont = True
        Me.INDTxtMuestraPatologicas.Properties.AutoHeight = False
        Me.INDTxtMuestraPatologicas.Properties.ReadOnly = True
        Me.INDTxtMuestraPatologicas.Size = New System.Drawing.Size(219, 26)
        Me.INDTxtMuestraPatologicas.StyleController = Me.LayoutControl1
        Me.INDTxtMuestraPatologicas.TabIndex = 9
        '
        'INDGcQxEquipe
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcQxEquipe, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcQxEquipe, Nothing)
        Me.INDGcQxEquipe.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcQxEquipe, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcQxEquipe, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcQxEquipe, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcQxEquipe, False)
        Me.INDGcQxEquipe.Location = New System.Drawing.Point(36, 96)
        Me.INDGcQxEquipe.MainView = Me.INDGvQxEquipe
        Me.INDGcQxEquipe.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcQxEquipe.Name = "INDGcQxEquipe"
        Me.INDGcQxEquipe.Size = New System.Drawing.Size(849, 451)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcQxEquipe, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcQxEquipe.TabIndex = 6
        Me.INDGcQxEquipe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvQxEquipe})
        '
        'INDGvQxEquipe
        '
        Me.INDGvQxEquipe.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvQxEquipe.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvQxEquipe.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvQxEquipe.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvQxEquipe.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvQxEquipe.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvQxEquipe.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvQxEquipe.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvQxEquipe.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvQxEquipe.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvQxEquipe.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvQxEquipe.Appearance.Row.Options.UseFont = True
        Me.INDGvQxEquipe.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvQxEquipe.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvQxEquipe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn55, Me.GridColumn61, Me.GridColumn74})
        Me.INDGvQxEquipe.GridControl = Me.INDGcQxEquipe
        Me.INDGvQxEquipe.Name = "INDGvQxEquipe"
        Me.INDGvQxEquipe.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvQxEquipe.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvQxEquipe.OptionsView.ShowAutoFilterRow = True
        Me.INDGvQxEquipe.OptionsView.ShowDetailButtons = False
        Me.INDGvQxEquipe.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDGvQxEquipe.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvQxEquipe, False)
        '
        'GridColumn55
        '
        Me.GridColumn55.Caption = "Código"
        Me.GridColumn55.FieldName = "CODPROSAL"
        Me.GridColumn55.MinWidth = 21
        Me.GridColumn55.Name = "GridColumn55"
        Me.GridColumn55.OptionsColumn.AllowEdit = False
        Me.GridColumn55.OptionsColumn.AllowFocus = False
        Me.GridColumn55.Visible = True
        Me.GridColumn55.VisibleIndex = 0
        Me.GridColumn55.Width = 292
        '
        'GridColumn61
        '
        Me.GridColumn61.Caption = "Nombre"
        Me.GridColumn61.FieldName = "NOMMEDICO"
        Me.GridColumn61.MinWidth = 21
        Me.GridColumn61.Name = "GridColumn61"
        Me.GridColumn61.OptionsColumn.AllowEdit = False
        Me.GridColumn61.OptionsColumn.AllowFocus = False
        Me.GridColumn61.Visible = True
        Me.GridColumn61.VisibleIndex = 1
        Me.GridColumn61.Width = 621
        '
        'GridColumn74
        '
        Me.GridColumn74.Caption = "Tipo"
        Me.GridColumn74.FieldName = "TIPOEQUIPName"
        Me.GridColumn74.MinWidth = 21
        Me.GridColumn74.Name = "GridColumn74"
        Me.GridColumn74.OptionsColumn.AllowEdit = False
        Me.GridColumn74.OptionsColumn.AllowFocus = False
        Me.GridColumn74.Visible = True
        Me.GridColumn74.VisibleIndex = 2
        Me.GridColumn74.Width = 478
        '
        'INDGcProceduresRealizados
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcProceduresRealizados, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcProceduresRealizados, Nothing)
        Me.INDGcProceduresRealizados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcProceduresRealizados, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcProceduresRealizados, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcProceduresRealizados, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcProceduresRealizados, False)
        Me.INDGcProceduresRealizados.Location = New System.Drawing.Point(36, 96)
        Me.INDGcProceduresRealizados.MainView = Me.INDGvProceduresRealizados
        Me.INDGcProceduresRealizados.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcProceduresRealizados.Name = "INDGcProceduresRealizados"
        Me.INDGcProceduresRealizados.Size = New System.Drawing.Size(849, 451)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcProceduresRealizados, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcProceduresRealizados.TabIndex = 5
        Me.INDGcProceduresRealizados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProceduresRealizados})
        '
        'INDGvProceduresRealizados
        '
        Me.INDGvProceduresRealizados.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProceduresRealizados.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProceduresRealizados.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvProceduresRealizados.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProceduresRealizados.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProceduresRealizados.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvProceduresRealizados.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProceduresRealizados.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProceduresRealizados.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProceduresRealizados.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProceduresRealizados.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProceduresRealizados.Appearance.Row.Options.UseFont = True
        Me.INDGvProceduresRealizados.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvProceduresRealizados.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvProceduresRealizados.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCodigo, Me.GridColumn30, Me.GridColumn36, Me.GridColumn49})
        Me.INDGvProceduresRealizados.GridControl = Me.INDGcProceduresRealizados
        Me.INDGvProceduresRealizados.Name = "INDGvProceduresRealizados"
        Me.INDGvProceduresRealizados.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProceduresRealizados.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProceduresRealizados.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProceduresRealizados.OptionsView.ShowDetailButtons = False
        Me.INDGvProceduresRealizados.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDGvProceduresRealizados.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvProceduresRealizados, False)
        '
        'ColCodigo
        '
        Me.ColCodigo.Caption = "Código"
        Me.ColCodigo.FieldName = "CODSERIPS"
        Me.ColCodigo.MinWidth = 21
        Me.ColCodigo.Name = "ColCodigo"
        Me.ColCodigo.OptionsColumn.AllowEdit = False
        Me.ColCodigo.OptionsColumn.AllowFocus = False
        Me.ColCodigo.Visible = True
        Me.ColCodigo.VisibleIndex = 0
        Me.ColCodigo.Width = 273
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Descripción"
        Me.GridColumn30.FieldName = "DESSERIPS"
        Me.GridColumn30.MinWidth = 21
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.OptionsColumn.AllowEdit = False
        Me.GridColumn30.OptionsColumn.AllowFocus = False
        Me.GridColumn30.Visible = True
        Me.GridColumn30.VisibleIndex = 1
        Me.GridColumn30.Width = 450
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "Principal"
        Me.GridColumn36.FieldName = "QXPRINCIP"
        Me.GridColumn36.MinWidth = 21
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.OptionsColumn.AllowEdit = False
        Me.GridColumn36.OptionsColumn.AllowFocus = False
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 2
        Me.GridColumn36.Width = 148
        '
        'GridColumn49
        '
        Me.GridColumn49.Caption = "Vía Abordaje"
        Me.GridColumn49.FieldName = "CODVIAABO"
        Me.GridColumn49.MinWidth = 21
        Me.GridColumn49.Name = "GridColumn49"
        Me.GridColumn49.OptionsColumn.AllowEdit = False
        Me.GridColumn49.OptionsColumn.AllowFocus = False
        Me.GridColumn49.Visible = True
        Me.GridColumn49.VisibleIndex = 3
        Me.GridColumn49.Width = 519
        '
        'INDgcDetailServiciosNoRealizados
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetailServiciosNoRealizados, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetailServiciosNoRealizados, Nothing)
        Me.INDgcDetailServiciosNoRealizados.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcDetailServiciosNoRealizados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetailServiciosNoRealizados, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetailServiciosNoRealizados, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetailServiciosNoRealizados, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetailServiciosNoRealizados, False)
        Me.INDgcDetailServiciosNoRealizados.Location = New System.Drawing.Point(36, 96)
        Me.INDgcDetailServiciosNoRealizados.MainView = Me.INDgvDetailServiciosNoRealizados
        Me.INDgcDetailServiciosNoRealizados.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcDetailServiciosNoRealizados.Name = "INDgcDetailServiciosNoRealizados"
        Me.INDgcDetailServiciosNoRealizados.Size = New System.Drawing.Size(849, 451)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetailServiciosNoRealizados, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDetailServiciosNoRealizados.TabIndex = 4
        Me.INDgcDetailServiciosNoRealizados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetailServiciosNoRealizados})
        '
        'INDgvDetailServiciosNoRealizados
        '
        Me.INDgvDetailServiciosNoRealizados.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetailServiciosNoRealizados.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDetailServiciosNoRealizados.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDetailServiciosNoRealizados.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDetailServiciosNoRealizados.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDetailServiciosNoRealizados.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvDetailServiciosNoRealizados.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetailServiciosNoRealizados.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDetailServiciosNoRealizados.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetailServiciosNoRealizados.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDetailServiciosNoRealizados.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDetailServiciosNoRealizados.Appearance.Row.Options.UseFont = True
        Me.INDgvDetailServiciosNoRealizados.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDetailServiciosNoRealizados.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDetailServiciosNoRealizados.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFecha, Me.ColFolio, Me.ColMedicoNoRelazados, Me.GridColumn28, Me.GridColumn29})
        Me.INDgvDetailServiciosNoRealizados.GridControl = Me.INDgcDetailServiciosNoRealizados
        Me.INDgvDetailServiciosNoRealizados.Name = "INDgvDetailServiciosNoRealizados"
        Me.INDgvDetailServiciosNoRealizados.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDetailServiciosNoRealizados.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDetailServiciosNoRealizados.OptionsView.ShowAutoFilterRow = True
        Me.INDgvDetailServiciosNoRealizados.OptionsView.ShowDetailButtons = False
        Me.INDgvDetailServiciosNoRealizados.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDgvDetailServiciosNoRealizados.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvDetailServiciosNoRealizados, False)
        '
        'ColFecha
        '
        Me.ColFecha.Caption = "Fecha"
        Me.ColFecha.FieldName = "Fecha"
        Me.ColFecha.MinWidth = 21
        Me.ColFecha.Name = "ColFecha"
        Me.ColFecha.OptionsColumn.AllowEdit = False
        Me.ColFecha.OptionsColumn.AllowFocus = False
        Me.ColFecha.Visible = True
        Me.ColFecha.VisibleIndex = 0
        Me.ColFecha.Width = 117
        '
        'ColFolio
        '
        Me.ColFolio.Caption = "Folio"
        Me.ColFolio.FieldName = "Folio"
        Me.ColFolio.MinWidth = 21
        Me.ColFolio.Name = "ColFolio"
        Me.ColFolio.OptionsColumn.AllowEdit = False
        Me.ColFolio.OptionsColumn.AllowFocus = False
        Me.ColFolio.Visible = True
        Me.ColFolio.VisibleIndex = 1
        Me.ColFolio.Width = 69
        '
        'ColMedicoNoRelazados
        '
        Me.ColMedicoNoRelazados.Caption = "Medico"
        Me.ColMedicoNoRelazados.FieldName = "Medico"
        Me.ColMedicoNoRelazados.MinWidth = 21
        Me.ColMedicoNoRelazados.Name = "ColMedicoNoRelazados"
        Me.ColMedicoNoRelazados.OptionsColumn.AllowEdit = False
        Me.ColMedicoNoRelazados.OptionsColumn.AllowFocus = False
        Me.ColMedicoNoRelazados.Visible = True
        Me.ColMedicoNoRelazados.VisibleIndex = 2
        Me.ColMedicoNoRelazados.Width = 168
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Unidad Funcional"
        Me.GridColumn28.FieldName = "UnidadFuncional"
        Me.GridColumn28.MinWidth = 21
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.OptionsColumn.AllowEdit = False
        Me.GridColumn28.OptionsColumn.AllowFocus = False
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 3
        Me.GridColumn28.Width = 168
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Observación"
        Me.GridColumn29.FieldName = "Observacion"
        Me.GridColumn29.MinWidth = 21
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.OptionsColumn.AllowEdit = False
        Me.GridColumn29.OptionsColumn.AllowFocus = False
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 4
        Me.GridColumn29.Width = 172
        '
        'INDgcDetailServiciosRealizados
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetailServiciosRealizados, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetailServiciosRealizados, Nothing)
        Me.INDgcDetailServiciosRealizados.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcDetailServiciosRealizados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetailServiciosRealizados, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetailServiciosRealizados, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetailServiciosRealizados, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetailServiciosRealizados, False)
        Me.INDgcDetailServiciosRealizados.Location = New System.Drawing.Point(36, 96)
        Me.INDgcDetailServiciosRealizados.MainView = Me.INDgvDetailServiciosRealizados
        Me.INDgcDetailServiciosRealizados.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcDetailServiciosRealizados.Name = "INDgcDetailServiciosRealizados"
        Me.INDgcDetailServiciosRealizados.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptChkSeleccioneServiceProcedure, Me.INDrptMeInterpretation, Me.INDrpticeGenerated, Me.INDRptSearchMedicoRealizo, Me.RepositoryItemDateEdit1})
        Me.INDgcDetailServiciosRealizados.Size = New System.Drawing.Size(849, 451)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetailServiciosRealizados, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDetailServiciosRealizados.TabIndex = 1
        Me.INDgcDetailServiciosRealizados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetailServiciosRealizados})
        '
        'INDgvDetailServiciosRealizados
        '
        Me.INDgvDetailServiciosRealizados.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetailServiciosRealizados.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDetailServiciosRealizados.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDetailServiciosRealizados.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDetailServiciosRealizados.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDetailServiciosRealizados.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvDetailServiciosRealizados.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetailServiciosRealizados.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDetailServiciosRealizados.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetailServiciosRealizados.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDetailServiciosRealizados.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDetailServiciosRealizados.Appearance.Row.Options.UseFont = True
        Me.INDgvDetailServiciosRealizados.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDetailServiciosRealizados.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDetailServiciosRealizados.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolSeleccioneLab, Me.GridColumn17, Me.GridColumn18, Me.ColMedico, Me.ColObservacion, Me.ColUnidadFuncional, Me.ColInterpretacion, Me.ColCantidad, Me.colMedicoRealizo, Me.colFechaRealizacion})
        Me.INDgvDetailServiciosRealizados.CustomizationFormBounds = New System.Drawing.Rectangle(1428, 452, 252, 305)
        Me.INDgvDetailServiciosRealizados.GridControl = Me.INDgcDetailServiciosRealizados
        Me.INDgvDetailServiciosRealizados.Name = "INDgvDetailServiciosRealizados"
        Me.INDgvDetailServiciosRealizados.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDetailServiciosRealizados.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDetailServiciosRealizados.OptionsView.ShowAutoFilterRow = True
        Me.INDgvDetailServiciosRealizados.OptionsView.ShowDetailButtons = False
        Me.INDgvDetailServiciosRealizados.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDgvDetailServiciosRealizados.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvDetailServiciosRealizados, False)
        '
        'INDcolSeleccioneLab
        '
        Me.INDcolSeleccioneLab.Caption = "Seleccionar"
        Me.INDcolSeleccioneLab.ColumnEdit = Me.INDrptChkSeleccioneServiceProcedure
        Me.INDcolSeleccioneLab.FieldName = "Seleccione"
        Me.INDcolSeleccioneLab.MinWidth = 21
        Me.INDcolSeleccioneLab.Name = "INDcolSeleccioneLab"
        Me.INDcolSeleccioneLab.Visible = True
        Me.INDcolSeleccioneLab.VisibleIndex = 0
        Me.INDcolSeleccioneLab.Width = 45
        '
        'INDrptChkSeleccioneServiceProcedure
        '
        Me.INDrptChkSeleccioneServiceProcedure.AutoHeight = False
        Me.INDrptChkSeleccioneServiceProcedure.Name = "INDrptChkSeleccioneServiceProcedure"
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Folio"
        Me.GridColumn17.FieldName = "Folio"
        Me.GridColumn17.MinWidth = 21
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 1
        Me.GridColumn17.Width = 51
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Fecha"
        Me.GridColumn18.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn18.FieldName = "Fecha"
        Me.GridColumn18.MinWidth = 21
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 2
        Me.GridColumn18.Width = 78
        '
        'ColMedico
        '
        Me.ColMedico.Caption = "Médico"
        Me.ColMedico.FieldName = "Medico"
        Me.ColMedico.MinWidth = 21
        Me.ColMedico.Name = "ColMedico"
        Me.ColMedico.OptionsColumn.AllowEdit = False
        Me.ColMedico.OptionsColumn.AllowFocus = False
        Me.ColMedico.Visible = True
        Me.ColMedico.VisibleIndex = 3
        Me.ColMedico.Width = 111
        '
        'ColObservacion
        '
        Me.ColObservacion.Caption = "Observación"
        Me.ColObservacion.FieldName = "Observacion"
        Me.ColObservacion.MinWidth = 21
        Me.ColObservacion.Name = "ColObservacion"
        Me.ColObservacion.OptionsColumn.AllowEdit = False
        Me.ColObservacion.OptionsColumn.AllowFocus = False
        Me.ColObservacion.Visible = True
        Me.ColObservacion.VisibleIndex = 6
        Me.ColObservacion.Width = 78
        '
        'ColUnidadFuncional
        '
        Me.ColUnidadFuncional.Caption = "Unidad Funcional"
        Me.ColUnidadFuncional.FieldName = "UnidadFuncional"
        Me.ColUnidadFuncional.MinWidth = 21
        Me.ColUnidadFuncional.Name = "ColUnidadFuncional"
        Me.ColUnidadFuncional.OptionsColumn.AllowEdit = False
        Me.ColUnidadFuncional.OptionsColumn.AllowFocus = False
        Me.ColUnidadFuncional.Visible = True
        Me.ColUnidadFuncional.VisibleIndex = 7
        Me.ColUnidadFuncional.Width = 123
        '
        'ColInterpretacion
        '
        Me.ColInterpretacion.Caption = "Interpretación"
        Me.ColInterpretacion.ColumnEdit = Me.INDrptMeInterpretation
        Me.ColInterpretacion.FieldName = "Interpretacion"
        Me.ColInterpretacion.MinWidth = 21
        Me.ColInterpretacion.Name = "ColInterpretacion"
        Me.ColInterpretacion.OptionsColumn.AllowEdit = False
        Me.ColInterpretacion.OptionsColumn.AllowFocus = False
        Me.ColInterpretacion.Visible = True
        Me.ColInterpretacion.VisibleIndex = 8
        Me.ColInterpretacion.Width = 78
        '
        'INDrptMeInterpretation
        '
        Me.INDrptMeInterpretation.Name = "INDrptMeInterpretation"
        '
        'ColCantidad
        '
        Me.ColCantidad.Caption = "Cantidad"
        Me.ColCantidad.FieldName = "CANSERIPS"
        Me.ColCantidad.MinWidth = 21
        Me.ColCantidad.Name = "ColCantidad"
        Me.ColCantidad.OptionsColumn.AllowEdit = False
        Me.ColCantidad.OptionsColumn.AllowFocus = False
        Me.ColCantidad.Visible = True
        Me.ColCantidad.VisibleIndex = 9
        Me.ColCantidad.Width = 58
        '
        'colMedicoRealizo
        '
        Me.colMedicoRealizo.Caption = "Medico Realizo"
        Me.colMedicoRealizo.ColumnEdit = Me.INDRptSearchMedicoRealizo
        Me.colMedicoRealizo.FieldName = "MedicoRealizo"
        Me.colMedicoRealizo.MinWidth = 21
        Me.colMedicoRealizo.Name = "colMedicoRealizo"
        Me.colMedicoRealizo.Visible = True
        Me.colMedicoRealizo.VisibleIndex = 4
        Me.colMedicoRealizo.Width = 105
        '
        'INDRptSearchMedicoRealizo
        '
        Me.INDRptSearchMedicoRealizo.AllowFocused = False
        Me.INDRptSearchMedicoRealizo.AutoHeight = False
        Me.INDRptSearchMedicoRealizo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptSearchMedicoRealizo.DisplayMember = "CodeName"
        Me.INDRptSearchMedicoRealizo.Name = "INDRptSearchMedicoRealizo"
        Me.INDRptSearchMedicoRealizo.NullText = ""
        Me.INDRptSearchMedicoRealizo.PopupSizeable = False
        Me.INDRptSearchMedicoRealizo.PopupView = Me.RepositoryItemSearchLookUpEdit1View
        Me.INDRptSearchMedicoRealizo.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox2})
        Me.INDRptSearchMedicoRealizo.ValueMember = "CODMEDICO"
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn99, Me.GridColumn100, Me.GridColumn101})
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        '
        'GridColumn99
        '
        Me.GridColumn99.Caption = "Codigo"
        Me.GridColumn99.FieldName = "CODPROSAL"
        Me.GridColumn99.MinWidth = 21
        Me.GridColumn99.Name = "GridColumn99"
        Me.GridColumn99.Visible = True
        Me.GridColumn99.VisibleIndex = 0
        Me.GridColumn99.Width = 252
        '
        'GridColumn100
        '
        Me.GridColumn100.Caption = "Medico"
        Me.GridColumn100.FieldName = "NOMMEDICO"
        Me.GridColumn100.MinWidth = 21
        Me.GridColumn100.Name = "GridColumn100"
        Me.GridColumn100.Visible = True
        Me.GridColumn100.VisibleIndex = 1
        Me.GridColumn100.Width = 690
        '
        'GridColumn101
        '
        Me.GridColumn101.Caption = "Profesion"
        Me.GridColumn101.ColumnEdit = Me.RepositoryItemImageComboBox2
        Me.GridColumn101.FieldName = "TIPPROFES"
        Me.GridColumn101.MinWidth = 21
        Me.GridColumn101.Name = "GridColumn101"
        Me.GridColumn101.Visible = True
        Me.GridColumn101.VisibleIndex = 2
        Me.GridColumn101.Width = 690
        '
        'RepositoryItemImageComboBox2
        '
        Me.RepositoryItemImageComboBox2.AutoHeight = False
        Me.RepositoryItemImageComboBox2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox2.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Medico General", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Medico Especialista", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Enfermera", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Auxiliar Enfermeria", 4, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Odontologo General", 5, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Odontologo Especialista", 6, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Nutricionista", 7, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Higienista", 8, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Psicologo", 9, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Trabajadora Social", 10, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Promotor de Saneamiento", 11, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ingeniero Sanitario", 12, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Medico Veterinario", 13, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ingeniero Alimento", 14, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Auxiliar Bacteriologo", 15, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Terapeuta ", 16, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Optometra ", 17, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Quimico Farmaceutico", 18, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Radiologo", 19, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tecnologo Radiologo", 20, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Instrumentador Qx", 21, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Auxiliar Patologia", 22, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Otros", 23, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Medico Interno", 24, -1)})
        Me.RepositoryItemImageComboBox2.Name = "RepositoryItemImageComboBox2"
        '
        'colFechaRealizacion
        '
        Me.colFechaRealizacion.Caption = "Fecha Realizacion"
        Me.colFechaRealizacion.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.colFechaRealizacion.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
        Me.colFechaRealizacion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colFechaRealizacion.FieldName = "FechaRealizacion"
        Me.colFechaRealizacion.MinWidth = 21
        Me.colFechaRealizacion.Name = "colFechaRealizacion"
        Me.colFechaRealizacion.Visible = True
        Me.colFechaRealizacion.VisibleIndex = 5
        Me.colFechaRealizacion.Width = 105
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'INDrpticeGenerated
        '
        Me.INDrpticeGenerated.AutoHeight = False
        Me.INDrpticeGenerated.HtmlImages = Me.ImageCollection1
        Me.INDrpticeGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por Generar", False, 0)})
        Me.INDrpticeGenerated.LargeImages = Me.ImageCollection1
        Me.INDrpticeGenerated.Name = "INDrpticeGenerated"
        Me.INDrpticeGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDrpticeGenerated.SmallImages = Me.ImageCollection1
        '
        'INDTxtObservaciones
        '
        Me.INDTxtObservaciones.Location = New System.Drawing.Point(486, 122)
        Me.INDTxtObservaciones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDTxtObservaciones.Name = "INDTxtObservaciones"
        Me.INDTxtObservaciones.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTxtObservaciones.Properties.Appearance.Options.UseFont = True
        Me.INDTxtObservaciones.Properties.ReadOnly = True
        Me.INDTxtObservaciones.Size = New System.Drawing.Size(399, 72)
        Me.INDTxtObservaciones.StyleController = Me.LayoutControl1
        Me.INDTxtObservaciones.TabIndex = 14
        '
        'INDTxtProcedimientosRealizados
        '
        Me.INDTxtProcedimientosRealizados.Location = New System.Drawing.Point(486, 224)
        Me.INDTxtProcedimientosRealizados.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDTxtProcedimientosRealizados.Name = "INDTxtProcedimientosRealizados"
        Me.INDTxtProcedimientosRealizados.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTxtProcedimientosRealizados.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProcedimientosRealizados.Properties.ReadOnly = True
        Me.INDTxtProcedimientosRealizados.Size = New System.Drawing.Size(399, 70)
        Me.INDTxtProcedimientosRealizados.StyleController = Me.LayoutControl1
        Me.INDTxtProcedimientosRealizados.TabIndex = 12
        '
        'INDMeHallazgosOperatorios
        '
        Me.INDMeHallazgosOperatorios.Location = New System.Drawing.Point(36, 224)
        Me.INDMeHallazgosOperatorios.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDMeHallazgosOperatorios.Name = "INDMeHallazgosOperatorios"
        Me.INDMeHallazgosOperatorios.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDMeHallazgosOperatorios.Properties.Appearance.Options.UseFont = True
        Me.INDMeHallazgosOperatorios.Properties.ReadOnly = True
        Me.INDMeHallazgosOperatorios.Size = New System.Drawing.Size(446, 70)
        Me.INDMeHallazgosOperatorios.StyleController = Me.LayoutControl1
        Me.INDMeHallazgosOperatorios.TabIndex = 13
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup5})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(921, 583)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup5.CustomizationFormText = "Procedimientos y Servicios"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDtcgServicesProcedures})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(901, 563)
        Me.LayoutControlGroup5.Text = "Procedimientos y Servicios"
        '
        'INDtcgServicesProcedures
        '
        Me.INDtcgServicesProcedures.CustomizationFormText = "TabbedControlGroup2"
        Me.INDtcgServicesProcedures.Location = New System.Drawing.Point(0, 0)
        Me.INDtcgServicesProcedures.Name = "INDtcgServicesProcedures"
        Me.INDtcgServicesProcedures.SelectedTabPage = Me.INDtpServiciosRealizados
        Me.INDtcgServicesProcedures.Size = New System.Drawing.Size(877, 510)
        Me.INDtcgServicesProcedures.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDtpServiciosRealizados, Me.INDtpServiciosNoRealizados, Me.TabServices, Me.TabProfessional, Me.TabOtherData})
        '
        'INDtpServiciosRealizados
        '
        Me.INDtpServiciosRealizados.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtpServiciosRealizados.AppearanceGroup.Options.UseFont = True
        Me.INDtpServiciosRealizados.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtpServiciosRealizados.AppearanceItemCaption.Options.UseFont = True
        Me.INDtpServiciosRealizados.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtpServiciosRealizados.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDtpServiciosRealizados.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDtpServiciosRealizados.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDtpServiciosRealizados.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtpServiciosRealizados.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDtpServiciosRealizados.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtpServiciosRealizados.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDtpServiciosRealizados.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtpServiciosRealizados.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDtpServiciosRealizados.CustomizationFormText = "Procedimientos y Servicios"
        Me.INDtpServiciosRealizados.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDtpServiciosRealizados.Location = New System.Drawing.Point(0, 0)
        Me.INDtpServiciosRealizados.Name = "INDtpServiciosRealizados"
        Me.INDtpServiciosRealizados.Size = New System.Drawing.Size(853, 455)
        Me.INDtpServiciosRealizados.Text = "Realizados"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcDetailServiciosRealizados
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(853, 455)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDtpServiciosNoRealizados
        '
        Me.INDtpServiciosNoRealizados.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtpServiciosNoRealizados.AppearanceGroup.Options.UseFont = True
        Me.INDtpServiciosNoRealizados.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtpServiciosNoRealizados.AppearanceItemCaption.Options.UseFont = True
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtpServiciosNoRealizados.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDtpServiciosNoRealizados.CustomizationFormText = "No Realizados"
        Me.INDtpServiciosNoRealizados.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.INDtpServiciosNoRealizados.Location = New System.Drawing.Point(0, 0)
        Me.INDtpServiciosNoRealizados.Name = "INDtpServiciosNoRealizados"
        Me.INDtpServiciosNoRealizados.Size = New System.Drawing.Size(853, 455)
        Me.INDtpServiciosNoRealizados.Text = "No Realizados"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDgcDetailServiciosNoRealizados
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(853, 455)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'TabServices
        '
        Me.TabServices.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabServices.AppearanceGroup.Options.UseFont = True
        Me.TabServices.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabServices.AppearanceItemCaption.Options.UseFont = True
        Me.TabServices.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabServices.AppearanceTabPage.Header.Options.UseFont = True
        Me.TabServices.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TabServices.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.TabServices.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabServices.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.TabServices.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabServices.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.TabServices.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabServices.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.TabServices.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6})
        Me.TabServices.Location = New System.Drawing.Point(0, 0)
        Me.TabServices.Name = "TabServices"
        Me.TabServices.Size = New System.Drawing.Size(853, 455)
        Me.TabServices.Text = "Servicios Relacionados"
        Me.TabServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDGcProceduresRealizados
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(853, 455)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'TabProfessional
        '
        Me.TabProfessional.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabProfessional.AppearanceGroup.Options.UseFont = True
        Me.TabProfessional.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabProfessional.AppearanceItemCaption.Options.UseFont = True
        Me.TabProfessional.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabProfessional.AppearanceTabPage.Header.Options.UseFont = True
        Me.TabProfessional.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TabProfessional.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.TabProfessional.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabProfessional.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.TabProfessional.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabProfessional.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.TabProfessional.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabProfessional.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.TabProfessional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem21})
        Me.TabProfessional.Location = New System.Drawing.Point(0, 0)
        Me.TabProfessional.Name = "TabProfessional"
        Me.TabProfessional.Size = New System.Drawing.Size(853, 455)
        Me.TabProfessional.Text = "Profesionales"
        Me.TabProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.INDGcQxEquipe
        Me.LayoutControlItem21.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(853, 455)
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem21.TextVisible = False
        '
        'TabOtherData
        '
        Me.TabOtherData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabOtherData.AppearanceGroup.Options.UseFont = True
        Me.TabOtherData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabOtherData.AppearanceItemCaption.Options.UseFont = True
        Me.TabOtherData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabOtherData.AppearanceTabPage.Header.Options.UseFont = True
        Me.TabOtherData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TabOtherData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.TabOtherData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabOtherData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.TabOtherData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabOtherData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.TabOtherData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TabOtherData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.TabOtherData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem22, Me.LayoutControlItem33, Me.LayoutControlItem30, Me.LayoutControlItem28, Me.LciObservations, Me.LayoutControlItem32})
        Me.TabOtherData.Location = New System.Drawing.Point(0, 0)
        Me.TabOtherData.Name = "TabOtherData"
        Me.TabOtherData.Size = New System.Drawing.Size(853, 455)
        Me.TabOtherData.Text = "Otros Datos"
        Me.TabOtherData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.Control = Me.INDTxtMuestraPatologicas
        Me.LayoutControlItem22.Location = New System.Drawing.Point(0, 34)
        Me.LayoutControlItem22.MaxSize = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem22.MinSize = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Size = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem22.Text = "Muestras Patologicas Solicitadas"
        Me.LayoutControlItem22.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(219, 19)
        Me.LayoutControlItem22.TextToControlDistance = 5
        '
        'LayoutControlItem33
        '
        Me.LayoutControlItem33.Control = Me.INDMeHallazgosOperatorios
        Me.LayoutControlItem33.Location = New System.Drawing.Point(0, 102)
        Me.LayoutControlItem33.MaxSize = New System.Drawing.Size(450, 100)
        Me.LayoutControlItem33.MinSize = New System.Drawing.Size(450, 100)
        Me.LayoutControlItem33.Name = "LayoutControlItem33"
        Me.LayoutControlItem33.Size = New System.Drawing.Size(450, 353)
        Me.LayoutControlItem33.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem33.Text = "Hallazgos Operatorios"
        Me.LayoutControlItem33.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem33.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem33.TextSize = New System.Drawing.Size(219, 21)
        Me.LayoutControlItem33.TextToControlDistance = 5
        '
        'LayoutControlItem30
        '
        Me.LayoutControlItem30.Control = Me.INDTxtQxTime
        Me.LayoutControlItem30.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem30.MaxSize = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem30.MinSize = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem30.Name = "LayoutControlItem30"
        Me.LayoutControlItem30.Size = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem30.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem30.Text = "Tiempo Qx (hh:mm:ss)"
        Me.LayoutControlItem30.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem30.TextSize = New System.Drawing.Size(219, 19)
        Me.LayoutControlItem30.TextToControlDistance = 5
        '
        'LayoutControlItem28
        '
        Me.LayoutControlItem28.Control = Me.INDTxtTipoAnestesia
        Me.LayoutControlItem28.Location = New System.Drawing.Point(0, 68)
        Me.LayoutControlItem28.MaxSize = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem28.MinSize = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem28.Name = "LayoutControlItem28"
        Me.LayoutControlItem28.Size = New System.Drawing.Size(450, 34)
        Me.LayoutControlItem28.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem28.Text = "Tipo Anestesia"
        Me.LayoutControlItem28.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem28.TextSize = New System.Drawing.Size(219, 19)
        Me.LayoutControlItem28.TextToControlDistance = 5
        '
        'LciObservations
        '
        Me.LciObservations.Control = Me.INDTxtObservaciones
        Me.LciObservations.Location = New System.Drawing.Point(450, 0)
        Me.LciObservations.MaxSize = New System.Drawing.Size(390, 102)
        Me.LciObservations.MinSize = New System.Drawing.Size(390, 102)
        Me.LciObservations.Name = "LciObservations"
        Me.LciObservations.Size = New System.Drawing.Size(403, 102)
        Me.LciObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciObservations.Text = "Observaciones"
        Me.LciObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.LciObservations.TextSize = New System.Drawing.Size(219, 21)
        Me.LciObservations.TextToControlDistance = 5
        '
        'LayoutControlItem32
        '
        Me.LayoutControlItem32.Control = Me.INDTxtProcedimientosRealizados
        Me.LayoutControlItem32.Location = New System.Drawing.Point(450, 102)
        Me.LayoutControlItem32.MaxSize = New System.Drawing.Size(390, 100)
        Me.LayoutControlItem32.MinSize = New System.Drawing.Size(390, 100)
        Me.LayoutControlItem32.Name = "LayoutControlItem32"
        Me.LayoutControlItem32.Size = New System.Drawing.Size(403, 353)
        Me.LayoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem32.Text = "Descripcion Procedimiento(s) Realizado(s)"
        Me.LayoutControlItem32.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem32.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem32.TextSize = New System.Drawing.Size(219, 21)
        Me.LayoutControlItem32.TextToControlDistance = 5
        '
        'INDGcHemoDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcHemoDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcHemoDetail, Nothing)
        Me.INDGcHemoDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcHemoDetail, False)
        Me.INDGcHemoDetail.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcHemoDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcHemoDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcHemoDetail, False)
        Me.INDGcHemoDetail.Location = New System.Drawing.Point(36, 146)
        Me.INDGcHemoDetail.MainView = Me.INDGvHemoDetail
        Me.INDGcHemoDetail.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcHemoDetail.MenuManager = Me.BarManager1
        Me.INDGcHemoDetail.Name = "INDGcHemoDetail"
        Me.INDGcHemoDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptChkHemo, Me.INDrptPceHemo, Me.INDRptHemoGenerated, Me.INDRICESkipLiquidationH})
        Me.INDGcHemoDetail.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcHemoDetail, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcHemoDetail.TabIndex = 15
        Me.INDGcHemoDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvHemoDetail})
        '
        'INDGvHemoDetail
        '
        Me.INDGvHemoDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvHemoDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvHemoDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvHemoDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvHemoDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvHemoDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvHemoDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvHemoDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvHemoDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvHemoDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvHemoDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvHemoDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvHemoDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn106, Me.INDColSelHemo, Me.GridColumn107, Me.GridColumn108, Me.GridColumn109, Me.GridColumn110, Me.GridColumn122, Me.GridColumn111, Me.GridColumn112, Me.GridColumn113, Me.GridColumn114, Me.GridColumn115, Me.GridColumn177, Me.GridColumn178})
        Me.INDGvHemoDetail.GridControl = Me.INDGcHemoDetail
        Me.INDGvHemoDetail.GroupCount = 1
        Me.INDGvHemoDetail.Name = "INDGvHemoDetail"
        Me.INDGvHemoDetail.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvHemoDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvHemoDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvHemoDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvHemoDetail.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn106, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvHemoDetail, False)
        '
        'GridColumn106
        '
        Me.GridColumn106.Caption = "Tipo Servicio"
        Me.GridColumn106.FieldName = "TIPOSERVICIO"
        Me.GridColumn106.MinWidth = 21
        Me.GridColumn106.Name = "GridColumn106"
        Me.GridColumn106.Visible = True
        Me.GridColumn106.VisibleIndex = 6
        '
        'INDColSelHemo
        '
        Me.INDColSelHemo.Caption = "Sel."
        Me.INDColSelHemo.ColumnEdit = Me.INDrptChkHemo
        Me.INDColSelHemo.FieldName = "Seleccione"
        Me.INDColSelHemo.MinWidth = 21
        Me.INDColSelHemo.Name = "INDColSelHemo"
        Me.INDColSelHemo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelHemo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelHemo.OptionsColumn.AllowMove = False
        Me.INDColSelHemo.OptionsColumn.AllowShowHide = False
        Me.INDColSelHemo.OptionsColumn.AllowSize = False
        Me.INDColSelHemo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelHemo.OptionsColumn.FixedWidth = True
        Me.INDColSelHemo.OptionsColumn.ShowInCustomizationForm = False
        Me.INDColSelHemo.ToolTip = "Seleccione el hemocomponente a liquidar"
        Me.INDColSelHemo.Visible = True
        Me.INDColSelHemo.VisibleIndex = 0
        Me.INDColSelHemo.Width = 40
        '
        'GridColumn107
        '
        Me.GridColumn107.Caption = "Código"
        Me.GridColumn107.FieldName = "CODSERIPS"
        Me.GridColumn107.MinWidth = 21
        Me.GridColumn107.Name = "GridColumn107"
        Me.GridColumn107.OptionsColumn.AllowEdit = False
        Me.GridColumn107.OptionsColumn.AllowFocus = False
        Me.GridColumn107.Visible = True
        Me.GridColumn107.VisibleIndex = 1
        Me.GridColumn107.Width = 142
        '
        'GridColumn108
        '
        Me.GridColumn108.Caption = "Paciente"
        Me.GridColumn108.FieldName = "IPNOMCOMP"
        Me.GridColumn108.MinWidth = 21
        Me.GridColumn108.Name = "GridColumn108"
        Me.GridColumn108.OptionsColumn.AllowEdit = False
        Me.GridColumn108.OptionsColumn.AllowFocus = False
        Me.GridColumn108.Visible = True
        Me.GridColumn108.VisibleIndex = 2
        Me.GridColumn108.Width = 306
        '
        'GridColumn109
        '
        Me.GridColumn109.Caption = "Servicio"
        Me.GridColumn109.FieldName = "DESSERIPS"
        Me.GridColumn109.MinWidth = 21
        Me.GridColumn109.Name = "GridColumn109"
        Me.GridColumn109.OptionsColumn.AllowEdit = False
        Me.GridColumn109.OptionsColumn.AllowFocus = False
        Me.GridColumn109.Visible = True
        Me.GridColumn109.VisibleIndex = 3
        Me.GridColumn109.Width = 375
        '
        'GridColumn110
        '
        Me.GridColumn110.Caption = "Estado Servicio"
        Me.GridColumn110.FieldName = "EstadoServicio"
        Me.GridColumn110.MinWidth = 21
        Me.GridColumn110.Name = "GridColumn110"
        Me.GridColumn110.OptionsColumn.AllowEdit = False
        Me.GridColumn110.OptionsColumn.AllowFocus = False
        Me.GridColumn110.Visible = True
        Me.GridColumn110.VisibleIndex = 4
        Me.GridColumn110.Width = 123
        '
        'GridColumn122
        '
        Me.GridColumn122.Caption = "Descripción Relacionada"
        Me.GridColumn122.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn122.MinWidth = 21
        Me.GridColumn122.Name = "GridColumn122"
        Me.GridColumn122.OptionsColumn.AllowEdit = False
        Me.GridColumn122.OptionsColumn.AllowFocus = False
        Me.GridColumn122.Visible = True
        Me.GridColumn122.VisibleIndex = 5
        Me.GridColumn122.Width = 336
        '
        'GridColumn111
        '
        Me.GridColumn111.Caption = "+ info"
        Me.GridColumn111.ColumnEdit = Me.INDrptPceHemo
        Me.GridColumn111.MinWidth = 21
        Me.GridColumn111.Name = "GridColumn111"
        Me.GridColumn111.Visible = True
        Me.GridColumn111.VisibleIndex = 6
        Me.GridColumn111.Width = 159
        '
        'GridColumn112
        '
        Me.GridColumn112.Caption = "REARASANT"
        Me.GridColumn112.FieldName = "REARASANT"
        Me.GridColumn112.MinWidth = 21
        Me.GridColumn112.Name = "GridColumn112"
        '
        'GridColumn113
        '
        Me.GridColumn113.Caption = "TIPOSERVICIO"
        Me.GridColumn113.FieldName = "TIPOSERVICIO"
        Me.GridColumn113.MinWidth = 21
        Me.GridColumn113.Name = "GridColumn113"
        '
        'GridColumn114
        '
        Me.GridColumn114.Caption = "COMPONENTE"
        Me.GridColumn114.FieldName = "COMPONENTE"
        Me.GridColumn114.MinWidth = 21
        Me.GridColumn114.Name = "GridColumn114"
        '
        'GridColumn115
        '
        Me.GridColumn115.Caption = "BOLSAID"
        Me.GridColumn115.FieldName = "BOLSAID"
        Me.GridColumn115.MinWidth = 21
        Me.GridColumn115.Name = "GridColumn115"
        '
        'GridColumn177
        '
        Me.GridColumn177.Caption = "Justificación"
        Me.GridColumn177.FieldName = "JustificationCodeName"
        Me.GridColumn177.MinWidth = 21
        Me.GridColumn177.Name = "GridColumn177"
        Me.GridColumn177.OptionsColumn.AllowEdit = False
        Me.GridColumn177.OptionsColumn.AllowFocus = False
        Me.GridColumn177.Visible = True
        Me.GridColumn177.VisibleIndex = 7
        '
        'GridColumn178
        '
        Me.GridColumn178.Caption = "Omitir Liquidacion"
        Me.GridColumn178.ColumnEdit = Me.INDRICESkipLiquidationH
        Me.GridColumn178.FieldName = "SkipLiquidation"
        Me.GridColumn178.MinWidth = 21
        Me.GridColumn178.Name = "GridColumn178"
        Me.GridColumn178.OptionsColumn.AllowEdit = False
        Me.GridColumn178.OptionsColumn.AllowFocus = False
        Me.GridColumn178.Visible = True
        Me.GridColumn178.VisibleIndex = 8
        '
        'INDRICESkipLiquidationH
        '
        Me.INDRICESkipLiquidationH.AutoHeight = False
        Me.INDRICESkipLiquidationH.Name = "INDRICESkipLiquidationH"
        Me.INDRICESkipLiquidationH.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MBtnOpenAdmission, Me.MBtnCloseAdmission})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlTop.Size = New System.Drawing.Size(1685, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 722)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1685, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 716)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1685, 6)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 716)
        '
        'MBtnOpenAdmission
        '
        Me.MBtnOpenAdmission.Caption = "Ingresos Abiertos"
        Me.MBtnOpenAdmission.Id = 0
        Me.MBtnOpenAdmission.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.ingresos_abierto
        Me.MBtnOpenAdmission.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnOpenAdmission.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnOpenAdmission.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnOpenAdmission.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnOpenAdmission.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnOpenAdmission.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnOpenAdmission.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnOpenAdmission.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnOpenAdmission.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnOpenAdmission.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnOpenAdmission.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnOpenAdmission.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnOpenAdmission.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnOpenAdmission.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnOpenAdmission.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnOpenAdmission.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnOpenAdmission.Name = "MBtnOpenAdmission"
        '
        'MBtnCloseAdmission
        '
        Me.MBtnCloseAdmission.Caption = "Ingresos Facturados"
        Me.MBtnCloseAdmission.Id = 1
        Me.MBtnCloseAdmission.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.Ingresos_facturados_1_
        Me.MBtnCloseAdmission.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCloseAdmission.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnCloseAdmission.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCloseAdmission.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnCloseAdmission.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCloseAdmission.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnCloseAdmission.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCloseAdmission.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnCloseAdmission.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCloseAdmission.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnCloseAdmission.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCloseAdmission.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnCloseAdmission.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCloseAdmission.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnCloseAdmission.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCloseAdmission.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnCloseAdmission.Name = "MBtnCloseAdmission"
        '
        'INDRptHemoGenerated
        '
        Me.INDRptHemoGenerated.AutoHeight = False
        Me.INDRptHemoGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptHemoGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptHemoGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptHemoGenerated.Name = "INDRptHemoGenerated"
        Me.INDRptHemoGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptHemoGenerated.SmallImages = Me.ImageCollection1
        '
        'INDSleAdmissionNumber2
        '
        Me.INDSleAdmissionNumber2.AllowQueryOne = True
        Me.INDSleAdmissionNumber2.Datasource = Nothing
        Me.INDSleAdmissionNumber2.DisplayMember = "{FullNameAdmission}"
        Me.INDSleAdmissionNumber2.DisplayNullText = ""
        Me.INDSleAdmissionNumber2.EditValue = Nothing
        Me.INDSleAdmissionNumber2.EnterMoveNextControl = True
        Me.INDSleAdmissionNumber2.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleAdmissionNumber2.IdOpenForm = 0
        Me.INDSleAdmissionNumber2.IsReadOnly = False
        Me.INDSleAdmissionNumber2.Location = New System.Drawing.Point(170, 53)
        Me.INDSleAdmissionNumber2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleAdmissionNumber2.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleAdmissionNumber2.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleAdmissionNumber2.Name = "INDSleAdmissionNumber2"
        Me.INDSleAdmissionNumber2.PopupContainerControl = Me.PccAdmission
        Me.INDSleAdmissionNumber2.PopUpFormSize = New System.Drawing.Size(1000, 300)
        Me.INDSleAdmissionNumber2.Size = New System.Drawing.Size(712, 28)
        Me.INDSleAdmissionNumber2.TabIndex = 34
        Me.INDSleAdmissionNumber2.ValueMember = "AdmissionCode"
        Me.INDSleAdmissionNumber2.View = Me.viewSearchAdmission
        '
        'PccAdmission
        '
        Me.PccAdmission.Controls.Add(Me.LycPccAdmission)
        Me.PccAdmission.Location = New System.Drawing.Point(1550, 297)
        Me.PccAdmission.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PccAdmission.Name = "PccAdmission"
        Me.PccAdmission.Size = New System.Drawing.Size(741, 310)
        Me.PccAdmission.TabIndex = 10
        '
        'LycPccAdmission
        '
        Me.LycPccAdmission.AllowCustomization = False
        Me.LycPccAdmission.Controls.Add(Me.INDdeOut)
        Me.LycPccAdmission.Controls.Add(Me.INDtxtFunctionalUnitOut)
        Me.LycPccAdmission.Controls.Add(Me.TxtRiskType)
        Me.LycPccAdmission.Controls.Add(Me.TxtCareGroupAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtContact)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientEstrato)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientEntityName)
        Me.LycPccAdmission.Controls.Add(Me.TxtCareGroupPatient)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientAge)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientBirth)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientName)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientCode)
        Me.LycPccAdmission.Controls.Add(Me.TxtBedStay)
        Me.LycPccAdmission.Controls.Add(Me.TxtResponsiblePhone)
        Me.LycPccAdmission.Controls.Add(Me.TxtResponsibleName)
        Me.LycPccAdmission.Controls.Add(Me.TxtEntityNameAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtAtentionCenter)
        Me.LycPccAdmission.Controls.Add(Me.TxtBenefitPlan)
        Me.LycPccAdmission.Controls.Add(Me.TxtPlaceEntry)
        Me.LycPccAdmission.Controls.Add(Me.TxtFunctionalUnitAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionDate)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionCode)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientType)
        Me.LycPccAdmission.Controls.Add(Me.TxtAfiliationType)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionType)
        Me.LycPccAdmission.Controls.Add(Me.TxtLiquidationType)
        Me.LycPccAdmission.Controls.Add(Me.LabelControl3)
        Me.LycPccAdmission.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LycPccAdmission, False)
        Me.LycPccAdmission.Location = New System.Drawing.Point(0, 0)
        Me.LycPccAdmission.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LycPccAdmission.Name = "LycPccAdmission"
        Me.LycPccAdmission.Root = Me.LycgPccAdmission
        Me.LycPccAdmission.Size = New System.Drawing.Size(741, 310)
        Me.LycPccAdmission.TabIndex = 0
        Me.LycPccAdmission.Text = "LayoutControl1"
        '
        'INDdeOut
        '
        Me.INDdeOut.Location = New System.Drawing.Point(151, 125)
        Me.INDdeOut.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDdeOut.Name = "INDdeOut"
        Me.INDdeOut.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDdeOut.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDdeOut.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeOut.Properties.Appearance.Options.UseFont = True
        Me.INDdeOut.Size = New System.Drawing.Size(549, 24)
        Me.INDdeOut.StyleController = Me.LycPccAdmission
        Me.INDdeOut.TabIndex = 35
        '
        'INDtxtFunctionalUnitOut
        '
        Me.INDtxtFunctionalUnitOut.Location = New System.Drawing.Point(151, 95)
        Me.INDtxtFunctionalUnitOut.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDtxtFunctionalUnitOut.Name = "INDtxtFunctionalUnitOut"
        Me.INDtxtFunctionalUnitOut.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtFunctionalUnitOut.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDtxtFunctionalUnitOut.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtFunctionalUnitOut.Properties.Appearance.Options.UseFont = True
        Me.INDtxtFunctionalUnitOut.Size = New System.Drawing.Size(549, 24)
        Me.INDtxtFunctionalUnitOut.StyleController = Me.LycPccAdmission
        Me.INDtxtFunctionalUnitOut.TabIndex = 34
        '
        'TxtRiskType
        '
        Me.TxtRiskType.Location = New System.Drawing.Point(151, 155)
        Me.TxtRiskType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtRiskType.Name = "TxtRiskType"
        Me.TxtRiskType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtRiskType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtRiskType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtRiskType.Properties.Appearance.Options.UseFont = True
        Me.TxtRiskType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtRiskType.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtRiskType.Properties.ReadOnly = True
        Me.TxtRiskType.Size = New System.Drawing.Size(169, 24)
        Me.TxtRiskType.StyleController = Me.LycPccAdmission
        Me.TxtRiskType.TabIndex = 33
        '
        'TxtCareGroupAdmission
        '
        Me.TxtCareGroupAdmission.Location = New System.Drawing.Point(151, 125)
        Me.TxtCareGroupAdmission.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtCareGroupAdmission.Name = "TxtCareGroupAdmission"
        Me.TxtCareGroupAdmission.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtCareGroupAdmission.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.TxtCareGroupAdmission.Properties.Appearance.Options.UseFont = True
        Me.TxtCareGroupAdmission.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtCareGroupAdmission.Properties.ReadOnly = True
        Me.TxtCareGroupAdmission.Size = New System.Drawing.Size(169, 24)
        Me.TxtCareGroupAdmission.StyleController = Me.LycPccAdmission
        Me.TxtCareGroupAdmission.TabIndex = 32
        '
        'TxtContact
        '
        Me.TxtContact.Location = New System.Drawing.Point(458, 215)
        Me.TxtContact.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtContact.Name = "TxtContact"
        Me.TxtContact.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtContact.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtContact.Properties.Appearance.Options.UseBackColor = True
        Me.TxtContact.Properties.Appearance.Options.UseFont = True
        Me.TxtContact.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtContact.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtContact.Properties.ReadOnly = True
        Me.TxtContact.Size = New System.Drawing.Size(241, 24)
        Me.TxtContact.StyleController = Me.LycPccAdmission
        Me.TxtContact.TabIndex = 31
        '
        'TxtPatientEstrato
        '
        Me.TxtPatientEstrato.Location = New System.Drawing.Point(151, 215)
        Me.TxtPatientEstrato.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtPatientEstrato.Name = "TxtPatientEstrato"
        Me.TxtPatientEstrato.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientEstrato.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientEstrato.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientEstrato.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientEstrato.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientEstrato.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientEstrato.Properties.ReadOnly = True
        Me.TxtPatientEstrato.Size = New System.Drawing.Size(169, 24)
        Me.TxtPatientEstrato.StyleController = Me.LycPccAdmission
        Me.TxtPatientEstrato.TabIndex = 25
        '
        'TxtPatientEntityName
        '
        Me.TxtPatientEntityName.Location = New System.Drawing.Point(458, 185)
        Me.TxtPatientEntityName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtPatientEntityName.Name = "TxtPatientEntityName"
        Me.TxtPatientEntityName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientEntityName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientEntityName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientEntityName.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientEntityName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientEntityName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientEntityName.Properties.ReadOnly = True
        Me.TxtPatientEntityName.Size = New System.Drawing.Size(241, 24)
        Me.TxtPatientEntityName.StyleController = Me.LycPccAdmission
        Me.TxtPatientEntityName.TabIndex = 24
        '
        'TxtCareGroupPatient
        '
        Me.TxtCareGroupPatient.Location = New System.Drawing.Point(151, 185)
        Me.TxtCareGroupPatient.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtCareGroupPatient.Name = "TxtCareGroupPatient"
        Me.TxtCareGroupPatient.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtCareGroupPatient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCareGroupPatient.Properties.Appearance.Options.UseBackColor = True
        Me.TxtCareGroupPatient.Properties.Appearance.Options.UseFont = True
        Me.TxtCareGroupPatient.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupPatient.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtCareGroupPatient.Properties.ReadOnly = True
        Me.TxtCareGroupPatient.Size = New System.Drawing.Size(169, 24)
        Me.TxtCareGroupPatient.StyleController = Me.LycPccAdmission
        Me.TxtCareGroupPatient.TabIndex = 23
        '
        'TxtPatientAge
        '
        Me.TxtPatientAge.Location = New System.Drawing.Point(458, 125)
        Me.TxtPatientAge.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtPatientAge.Name = "TxtPatientAge"
        Me.TxtPatientAge.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientAge.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientAge.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientAge.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientAge.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientAge.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientAge.Properties.ReadOnly = True
        Me.TxtPatientAge.Size = New System.Drawing.Size(241, 24)
        Me.TxtPatientAge.StyleController = Me.LycPccAdmission
        Me.TxtPatientAge.TabIndex = 22
        '
        'TxtPatientBirth
        '
        Me.TxtPatientBirth.Location = New System.Drawing.Point(151, 125)
        Me.TxtPatientBirth.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtPatientBirth.Name = "TxtPatientBirth"
        Me.TxtPatientBirth.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientBirth.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientBirth.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientBirth.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientBirth.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientBirth.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientBirth.Properties.ReadOnly = True
        Me.TxtPatientBirth.Size = New System.Drawing.Size(169, 24)
        Me.TxtPatientBirth.StyleController = Me.LycPccAdmission
        Me.TxtPatientBirth.TabIndex = 21
        '
        'TxtPatientName
        '
        Me.TxtPatientName.Location = New System.Drawing.Point(458, 95)
        Me.TxtPatientName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtPatientName.Name = "TxtPatientName"
        Me.TxtPatientName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientName.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientName.Properties.ReadOnly = True
        Me.TxtPatientName.Size = New System.Drawing.Size(241, 24)
        Me.TxtPatientName.StyleController = Me.LycPccAdmission
        Me.TxtPatientName.TabIndex = 20
        '
        'TxtPatientCode
        '
        Me.TxtPatientCode.Location = New System.Drawing.Point(151, 95)
        Me.TxtPatientCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtPatientCode.Name = "TxtPatientCode"
        Me.TxtPatientCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientCode.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientCode.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientCode.Properties.ReadOnly = True
        Me.TxtPatientCode.Size = New System.Drawing.Size(169, 24)
        Me.TxtPatientCode.StyleController = Me.LycPccAdmission
        Me.TxtPatientCode.TabIndex = 19
        '
        'TxtBedStay
        '
        Me.TxtBedStay.Location = New System.Drawing.Point(458, 185)
        Me.TxtBedStay.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtBedStay.Name = "TxtBedStay"
        Me.TxtBedStay.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtBedStay.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBedStay.Properties.Appearance.Options.UseBackColor = True
        Me.TxtBedStay.Properties.Appearance.Options.UseFont = True
        Me.TxtBedStay.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtBedStay.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtBedStay.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtBedStay.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtBedStay.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtBedStay.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtBedStay.Properties.ReadOnly = True
        Me.TxtBedStay.Size = New System.Drawing.Size(200, 24)
        Me.TxtBedStay.StyleController = Me.LycPccAdmission
        Me.TxtBedStay.TabIndex = 18
        '
        'TxtResponsiblePhone
        '
        Me.TxtResponsiblePhone.Location = New System.Drawing.Point(458, 275)
        Me.TxtResponsiblePhone.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtResponsiblePhone.Name = "TxtResponsiblePhone"
        Me.TxtResponsiblePhone.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtResponsiblePhone.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtResponsiblePhone.Properties.Appearance.Options.UseBackColor = True
        Me.TxtResponsiblePhone.Properties.Appearance.Options.UseFont = True
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtResponsiblePhone.Properties.ReadOnly = True
        Me.TxtResponsiblePhone.Size = New System.Drawing.Size(200, 24)
        Me.TxtResponsiblePhone.StyleController = Me.LycPccAdmission
        Me.TxtResponsiblePhone.TabIndex = 17
        '
        'TxtResponsibleName
        '
        Me.TxtResponsibleName.Location = New System.Drawing.Point(151, 275)
        Me.TxtResponsibleName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtResponsibleName.Name = "TxtResponsibleName"
        Me.TxtResponsibleName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtResponsibleName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtResponsibleName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtResponsibleName.Properties.Appearance.Options.UseFont = True
        Me.TxtResponsibleName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtResponsibleName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtResponsibleName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtResponsibleName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtResponsibleName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtResponsibleName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtResponsibleName.Properties.ReadOnly = True
        Me.TxtResponsibleName.Size = New System.Drawing.Size(169, 24)
        Me.TxtResponsibleName.StyleController = Me.LycPccAdmission
        Me.TxtResponsibleName.TabIndex = 16
        '
        'TxtEntityNameAdmission
        '
        Me.TxtEntityNameAdmission.Location = New System.Drawing.Point(458, 125)
        Me.TxtEntityNameAdmission.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtEntityNameAdmission.Name = "TxtEntityNameAdmission"
        Me.TxtEntityNameAdmission.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtEntityNameAdmission.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntityNameAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.TxtEntityNameAdmission.Properties.Appearance.Options.UseFont = True
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtEntityNameAdmission.Properties.ReadOnly = True
        Me.TxtEntityNameAdmission.Size = New System.Drawing.Size(200, 24)
        Me.TxtEntityNameAdmission.StyleController = Me.LycPccAdmission
        Me.TxtEntityNameAdmission.TabIndex = 13
        '
        'TxtAtentionCenter
        '
        Me.TxtAtentionCenter.Location = New System.Drawing.Point(151, 245)
        Me.TxtAtentionCenter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtAtentionCenter.Name = "TxtAtentionCenter"
        Me.TxtAtentionCenter.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAtentionCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAtentionCenter.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAtentionCenter.Properties.Appearance.Options.UseFont = True
        Me.TxtAtentionCenter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAtentionCenter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAtentionCenter.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAtentionCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAtentionCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAtentionCenter.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAtentionCenter.Properties.ReadOnly = True
        Me.TxtAtentionCenter.Size = New System.Drawing.Size(169, 24)
        Me.TxtAtentionCenter.StyleController = Me.LycPccAdmission
        Me.TxtAtentionCenter.TabIndex = 12
        '
        'TxtBenefitPlan
        '
        Me.TxtBenefitPlan.Location = New System.Drawing.Point(458, 215)
        Me.TxtBenefitPlan.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtBenefitPlan.Name = "TxtBenefitPlan"
        Me.TxtBenefitPlan.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtBenefitPlan.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBenefitPlan.Properties.Appearance.Options.UseBackColor = True
        Me.TxtBenefitPlan.Properties.Appearance.Options.UseFont = True
        Me.TxtBenefitPlan.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtBenefitPlan.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtBenefitPlan.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtBenefitPlan.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtBenefitPlan.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtBenefitPlan.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtBenefitPlan.Properties.ReadOnly = True
        Me.TxtBenefitPlan.Size = New System.Drawing.Size(200, 24)
        Me.TxtBenefitPlan.StyleController = Me.LycPccAdmission
        Me.TxtBenefitPlan.TabIndex = 11
        '
        'TxtPlaceEntry
        '
        Me.TxtPlaceEntry.Location = New System.Drawing.Point(458, 155)
        Me.TxtPlaceEntry.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtPlaceEntry.Name = "TxtPlaceEntry"
        Me.TxtPlaceEntry.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPlaceEntry.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlaceEntry.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPlaceEntry.Properties.Appearance.Options.UseFont = True
        Me.TxtPlaceEntry.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPlaceEntry.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtPlaceEntry.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPlaceEntry.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtPlaceEntry.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtPlaceEntry.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPlaceEntry.Properties.ReadOnly = True
        Me.TxtPlaceEntry.Size = New System.Drawing.Size(200, 24)
        Me.TxtPlaceEntry.StyleController = Me.LycPccAdmission
        Me.TxtPlaceEntry.TabIndex = 9
        '
        'TxtFunctionalUnitAdmission
        '
        Me.TxtFunctionalUnitAdmission.Location = New System.Drawing.Point(458, 245)
        Me.TxtFunctionalUnitAdmission.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtFunctionalUnitAdmission.Name = "TxtFunctionalUnitAdmission"
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.Options.UseFont = True
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtFunctionalUnitAdmission.Properties.ReadOnly = True
        Me.TxtFunctionalUnitAdmission.Size = New System.Drawing.Size(200, 24)
        Me.TxtFunctionalUnitAdmission.StyleController = Me.LycPccAdmission
        Me.TxtFunctionalUnitAdmission.TabIndex = 7
        '
        'TxtAdmissionDate
        '
        Me.TxtAdmissionDate.Location = New System.Drawing.Point(458, 95)
        Me.TxtAdmissionDate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtAdmissionDate.Name = "TxtAdmissionDate"
        Me.TxtAdmissionDate.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAdmissionDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdmissionDate.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionDate.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAdmissionDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAdmissionDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAdmissionDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAdmissionDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionDate.Properties.ReadOnly = True
        Me.TxtAdmissionDate.Size = New System.Drawing.Size(200, 24)
        Me.TxtAdmissionDate.StyleController = Me.LycPccAdmission
        Me.TxtAdmissionDate.TabIndex = 6
        '
        'TxtAdmissionCode
        '
        Me.TxtAdmissionCode.Location = New System.Drawing.Point(151, 95)
        Me.TxtAdmissionCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtAdmissionCode.Name = "TxtAdmissionCode"
        Me.TxtAdmissionCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAdmissionCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdmissionCode.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionCode.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAdmissionCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionCode.Properties.ReadOnly = True
        Me.TxtAdmissionCode.Size = New System.Drawing.Size(169, 24)
        Me.TxtAdmissionCode.StyleController = Me.LycPccAdmission
        Me.TxtAdmissionCode.TabIndex = 4
        '
        'TxtPatientType
        '
        Me.TxtPatientType.Location = New System.Drawing.Point(151, 155)
        Me.TxtPatientType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtPatientType.Name = "TxtPatientType"
        Me.TxtPatientType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientType.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Contributivo", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Subsidiado", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Vinculado", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Particular", 4, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado Reg. Contributivo", 6, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado Reg. Subsidiado", 7, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado no Asegurado", 8, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Otro", 5, -1)})
        Me.TxtPatientType.Properties.ReadOnly = True
        Me.TxtPatientType.Size = New System.Drawing.Size(169, 24)
        Me.TxtPatientType.StyleController = Me.LycPccAdmission
        Me.TxtPatientType.TabIndex = 27
        '
        'TxtAfiliationType
        '
        Me.TxtAfiliationType.Location = New System.Drawing.Point(458, 155)
        Me.TxtAfiliationType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtAfiliationType.Name = "TxtAfiliationType"
        Me.TxtAfiliationType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAfiliationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAfiliationType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAfiliationType.Properties.Appearance.Options.UseFont = True
        Me.TxtAfiliationType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No Aplica", 0, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cotizante", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Beneficiario", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Adicional", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Jub/Retirado", 4, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pensionado", 5, -1)})
        Me.TxtAfiliationType.Properties.ReadOnly = True
        Me.TxtAfiliationType.Size = New System.Drawing.Size(241, 24)
        Me.TxtAfiliationType.StyleController = Me.LycPccAdmission
        Me.TxtAfiliationType.TabIndex = 28
        '
        'TxtAdmissionType
        '
        Me.TxtAdmissionType.Location = New System.Drawing.Point(151, 185)
        Me.TxtAdmissionType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtAdmissionType.Name = "TxtAdmissionType"
        Me.TxtAdmissionType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAdmissionType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdmissionType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionType.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAdmissionType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAdmissionType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAdmissionType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAdmissionType.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ambulatorio", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hospitalario", 2, -1)})
        Me.TxtAdmissionType.Properties.ReadOnly = True
        Me.TxtAdmissionType.Size = New System.Drawing.Size(169, 24)
        Me.TxtAdmissionType.StyleController = Me.LycPccAdmission
        Me.TxtAdmissionType.TabIndex = 8
        '
        'TxtLiquidationType
        '
        Me.TxtLiquidationType.Location = New System.Drawing.Point(151, 215)
        Me.TxtLiquidationType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtLiquidationType.Name = "TxtLiquidationType"
        Me.TxtLiquidationType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtLiquidationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLiquidationType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtLiquidationType.Properties.Appearance.Options.UseFont = True
        Me.TxtLiquidationType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtLiquidationType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtLiquidationType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtLiquidationType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtLiquidationType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtLiquidationType.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtLiquidationType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pago por servicios", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Capacitacion", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Factura Global", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Capitacion Global", 4, -1)})
        Me.TxtLiquidationType.Properties.ReadOnly = True
        Me.TxtLiquidationType.Size = New System.Drawing.Size(169, 24)
        Me.TxtLiquidationType.StyleController = Me.LycPccAdmission
        Me.TxtLiquidationType.TabIndex = 10
        '
        'LabelControl3
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl3, True)
        Me.LabelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl3.Appearance.Options.UseBackColor = True
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl3, False)
        Me.LabelControl3.Location = New System.Drawing.Point(10, 10)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(704, 40)
        Me.LabelControl3.StyleController = Me.LycPccAdmission
        Me.LabelControl3.TabIndex = 26
        Me.LabelControl3.Text = "Más Información"
        '
        'LycgPccAdmission
        '
        Me.LycgPccAdmission.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgPccAdmission.AppearanceGroup.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgPccAdmission.AppearanceItemCaption.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgPccAdmission.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LycgPccAdmission.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgPccAdmission.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgPccAdmission.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgPccAdmission.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LycgPccAdmission.CustomizationFormText = "LayoutControlGroup1"
        Me.LycgPccAdmission.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LycgPccAdmission.GroupBordersVisible = False
        Me.LycgPccAdmission.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup1, Me.LayoutControlItem31})
        Me.LycgPccAdmission.Name = "LycgPccAdmission"
        Me.LycgPccAdmission.Size = New System.Drawing.Size(724, 325)
        Me.LycgPccAdmission.TextVisible = False
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.CustomizationFormText = "TabbedControlGroup1"
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 40)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LycgAdmissionGroup
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(704, 265)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LycgAdmissionGroup, Me.LayoutControlGroup3})
        '
        'LycgAdmissionGroup
        '
        Me.LycgAdmissionGroup.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgAdmissionGroup.AppearanceGroup.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgAdmissionGroup.AppearanceItemCaption.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgAdmissionGroup.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgAdmissionGroup.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LycgAdmissionGroup.CustomizationFormText = "Datos del Ingreso"
        Me.LycgAdmissionGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem11, Me.LayoutControlItem13, Me.LayoutControlItem14, Me.LayoutControlItem15, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem27, Me.LayoutControlItem16, Me.LayoutControlItem39, Me.LayoutControlItem12, Me.LayoutControlItem10, Me.LayoutControlItem19, Me.LayoutControlItem20})
        Me.LycgAdmissionGroup.Location = New System.Drawing.Point(0, 0)
        Me.LycgAdmissionGroup.Name = "LycgAdmissionGroup"
        Me.LycgAdmissionGroup.Size = New System.Drawing.Size(680, 210)
        Me.LycgAdmissionGroup.Text = "Datos del Ingreso"
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.TxtAdmissionCode
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "No. Ingreso"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem7.TextToControlDistance = 12
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem11.Control = Me.TxtAdmissionType
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.Text = "Tipo Ingreso"
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem11.TextToControlDistance = 12
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem13.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem13.Control = Me.TxtLiquidationType
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.Text = "Tipo Liquidación"
        Me.LayoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem13.TextToControlDistance = 12
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem14.Control = Me.TxtBenefitPlan
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(300, 120)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem14.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.Text = "Plan Beneficio"
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem14.TextToControlDistance = 12
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem15.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem15.Control = Me.TxtAtentionCenter
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 150)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.Text = "Centro Atención"
        Me.LayoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem15.TextToControlDistance = 12
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.TxtBedStay
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(300, 90)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem8.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Estancia (Cama)"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem8.TextToControlDistance = 12
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem9.Control = Me.TxtAdmissionDate
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(300, 0)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem9.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "Fecha Ingreso"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem9.TextToControlDistance = 12
        '
        'LayoutControlItem27
        '
        Me.LayoutControlItem27.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem27.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem27.Control = Me.TxtCareGroupAdmission
        Me.LayoutControlItem27.CustomizationFormText = "Grupo Atención"
        Me.LayoutControlItem27.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem27.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem27.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem27.Name = "LayoutControlItem27"
        Me.LayoutControlItem27.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem27.Text = "Grupo Atención"
        Me.LayoutControlItem27.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem27.TextSize = New System.Drawing.Size(115, 19)
        Me.LayoutControlItem27.TextToControlDistance = 12
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem16.Control = Me.TxtEntityNameAdmission
        Me.LayoutControlItem16.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(300, 30)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem16.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.Text = "Nombre Entidad"
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem16.TextToControlDistance = 12
        '
        'LayoutControlItem39
        '
        Me.LayoutControlItem39.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem39.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem39.Control = Me.TxtRiskType
        Me.LayoutControlItem39.CustomizationFormText = "Tipo Riesgo"
        Me.LayoutControlItem39.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem39.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem39.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem39.Name = "LayoutControlItem39"
        Me.LayoutControlItem39.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem39.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem39.Text = "Tipo Riesgo"
        Me.LayoutControlItem39.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem39.TextSize = New System.Drawing.Size(115, 19)
        Me.LayoutControlItem39.TextToControlDistance = 12
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem12.Control = Me.TxtPlaceEntry
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(300, 60)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem12.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.Text = "Ingreso Por"
        Me.LayoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem12.TextToControlDistance = 12
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem10.Control = Me.TxtFunctionalUnitAdmission
        Me.LayoutControlItem10.CustomizationFormText = "LayoutControlItem10"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(300, 150)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem10.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.Text = "Unidad Funcional"
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem10.TextToControlDistance = 12
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem19.Control = Me.TxtResponsibleName
        Me.LayoutControlItem19.CustomizationFormText = "LayoutControlItem19"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.Text = "Acudiente"
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem19.TextToControlDistance = 12
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem20.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem20.Control = Me.TxtResponsiblePhone
        Me.LayoutControlItem20.CustomizationFormText = "LayoutControlItem20"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(300, 180)
        Me.LayoutControlItem20.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem20.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem20.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem20.Text = "Teléfono Acudiente"
        Me.LayoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem20.TextToControlDistance = 12
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup1.CustomizationFormText = "Datos del Paciente"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem23, Me.LayoutControlItem25, Me.LiCareGroup, Me.LayoutControlItem29, Me.LayoutControlItem26, Me.LiEntity, Me.LayoutControlItem24, Me.LayoutControlItem18, Me.LayoutControlItem17, Me.TxtContacto})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(680, 210)
        Me.LayoutControlGroup1.Text = "Datos del Paciente"
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem23.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem23.Control = Me.TxtPatientCode
        Me.LayoutControlItem23.CustomizationFormText = "Identificación"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem23.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem23.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem23.Text = "Identificación"
        Me.LayoutControlItem23.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem23.TextToControlDistance = 12
        '
        'LayoutControlItem25
        '
        Me.LayoutControlItem25.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem25.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem25.Control = Me.TxtPatientBirth
        Me.LayoutControlItem25.CustomizationFormText = "Fecha Nacimiento"
        Me.LayoutControlItem25.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem25.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem25.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem25.Name = "LayoutControlItem25"
        Me.LayoutControlItem25.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem25.Text = "Fecha Nacimiento"
        Me.LayoutControlItem25.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem25.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem25.TextToControlDistance = 12
        '
        'LiCareGroup
        '
        Me.LiCareGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LiCareGroup.AppearanceItemCaption.Options.UseFont = True
        Me.LiCareGroup.Control = Me.TxtCareGroupPatient
        Me.LiCareGroup.CustomizationFormText = "Código Entidad"
        Me.LiCareGroup.Location = New System.Drawing.Point(0, 90)
        Me.LiCareGroup.MaxSize = New System.Drawing.Size(300, 30)
        Me.LiCareGroup.MinSize = New System.Drawing.Size(300, 30)
        Me.LiCareGroup.Name = "LiCareGroup"
        Me.LiCareGroup.Size = New System.Drawing.Size(300, 30)
        Me.LiCareGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiCareGroup.Text = "Grupo Atención"
        Me.LiCareGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiCareGroup.TextSize = New System.Drawing.Size(115, 21)
        Me.LiCareGroup.TextToControlDistance = 12
        '
        'LayoutControlItem29
        '
        Me.LayoutControlItem29.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem29.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem29.Control = Me.TxtPatientEstrato
        Me.LayoutControlItem29.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LayoutControlItem29.CustomizationFormText = "Estrato"
        Me.LayoutControlItem29.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem29.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem29.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem29.Name = "LayoutControlItem29"
        Me.LayoutControlItem29.Size = New System.Drawing.Size(300, 90)
        Me.LayoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem29.Text = "Estrato o Nivel"
        Me.LayoutControlItem29.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem29.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem29.TextToControlDistance = 12
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem26.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem26.Control = Me.TxtPatientAge
        Me.LayoutControlItem26.CustomizationFormText = "Edad"
        Me.LayoutControlItem26.Location = New System.Drawing.Point(300, 30)
        Me.LayoutControlItem26.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem26.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem26.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem26.Text = "Edad"
        Me.LayoutControlItem26.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem26.TextToControlDistance = 12
        '
        'LiEntity
        '
        Me.LiEntity.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LiEntity.AppearanceItemCaption.Options.UseFont = True
        Me.LiEntity.Control = Me.TxtPatientEntityName
        Me.LiEntity.CustomizationFormText = "Entidad"
        Me.LiEntity.Location = New System.Drawing.Point(300, 90)
        Me.LiEntity.MaxSize = New System.Drawing.Size(339, 30)
        Me.LiEntity.MinSize = New System.Drawing.Size(339, 30)
        Me.LiEntity.Name = "LiEntity"
        Me.LiEntity.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LiEntity.Size = New System.Drawing.Size(380, 30)
        Me.LiEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiEntity.Text = "Entidad"
        Me.LiEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiEntity.TextSize = New System.Drawing.Size(115, 21)
        Me.LiEntity.TextToControlDistance = 12
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem24.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem24.Control = Me.TxtPatientName
        Me.LayoutControlItem24.CustomizationFormText = "Nombre"
        Me.LayoutControlItem24.Location = New System.Drawing.Point(300, 0)
        Me.LayoutControlItem24.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem24.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem24.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem24.Text = "Nombre"
        Me.LayoutControlItem24.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem24.TextToControlDistance = 12
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem18.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem18.Control = Me.TxtAfiliationType
        Me.LayoutControlItem18.CustomizationFormText = "Tipo Afiliación"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(300, 60)
        Me.LayoutControlItem18.MaxSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem18.MinSize = New System.Drawing.Size(339, 30)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.LayoutControlItem18.Size = New System.Drawing.Size(380, 30)
        Me.LayoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem18.Text = "Tipo Afiliación"
        Me.LayoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem18.TextToControlDistance = 12
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem17.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem17.Control = Me.TxtPatientType
        Me.LayoutControlItem17.CustomizationFormText = "Tipo"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem17.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem17.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem17.Text = "Tipo Paciente"
        Me.LayoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem17.TextToControlDistance = 12
        '
        'TxtContacto
        '
        Me.TxtContacto.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtContacto.AppearanceItemCaption.Options.UseFont = True
        Me.TxtContacto.Control = Me.TxtContact
        Me.TxtContacto.CustomizationFormText = "Contacto"
        Me.TxtContacto.Location = New System.Drawing.Point(300, 120)
        Me.TxtContacto.MaxSize = New System.Drawing.Size(339, 30)
        Me.TxtContacto.MinSize = New System.Drawing.Size(339, 30)
        Me.TxtContacto.Name = "TxtContacto"
        Me.TxtContacto.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 3, 2, 2)
        Me.TxtContacto.Size = New System.Drawing.Size(380, 90)
        Me.TxtContacto.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.TxtContacto.Text = "Contacto"
        Me.TxtContacto.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.TxtContacto.TextSize = New System.Drawing.Size(115, 19)
        Me.TxtContacto.TextToControlDistance = 12
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup3.CustomizationFormText = "Datos del Egreso"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(680, 210)
        Me.LayoutControlGroup3.Text = "Datos del Egreso"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.INDtxtFunctionalUnitOut
        Me.LayoutControlItem3.CustomizationFormText = "Unidad Funcional"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 30)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 30)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(680, 30)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Unidad Funcional"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem3.TextToControlDistance = 12
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.Control = Me.INDdeOut
        Me.LayoutControlItem4.CustomizationFormText = "Fecha"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 30)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 30)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(680, 180)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Fecha"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem4.TextToControlDistance = 12
        '
        'LayoutControlItem31
        '
        Me.LayoutControlItem31.Control = Me.LabelControl3
        Me.LayoutControlItem31.CustomizationFormText = "LayoutControlItem31"
        Me.LayoutControlItem31.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem31.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem31.MinSize = New System.Drawing.Size(1, 40)
        Me.LayoutControlItem31.Name = "LayoutControlItem31"
        Me.LayoutControlItem31.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem31.Size = New System.Drawing.Size(704, 40)
        Me.LayoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem31.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem31.TextVisible = False
        '
        'viewSearchAdmission
        '
        Me.viewSearchAdmission.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSearchAdmission.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewSearchAdmission.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSearchAdmission.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSearchAdmission.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSearchAdmission.Appearance.GroupRow.Options.UseFont = True
        Me.viewSearchAdmission.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSearchAdmission.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSearchAdmission.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewSearchAdmission.Appearance.Row.Options.UseFont = True
        Me.viewSearchAdmission.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.viewSearchAdmission.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn19, Me.GridColumn27, Me.GridColumn20, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23})
        Me.viewSearchAdmission.Name = "viewSearchAdmission"
        Me.viewSearchAdmission.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSearchAdmission.OptionsView.EnableAppearanceOddRow = True
        Me.viewSearchAdmission.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "No. Ingreso"
        Me.GridColumn24.FieldName = "AdmissionCode"
        Me.GridColumn24.MinWidth = 21
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsColumn.AllowFocus = False
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 0
        Me.GridColumn24.Width = 178
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Identificación"
        Me.GridColumn25.FieldName = "PatientCode"
        Me.GridColumn25.MinWidth = 21
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.OptionsColumn.AllowFocus = False
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 1
        Me.GridColumn25.Width = 153
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Paciente"
        Me.GridColumn26.FieldName = "PatientName"
        Me.GridColumn26.MinWidth = 21
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.OptionsColumn.AllowEdit = False
        Me.GridColumn26.OptionsColumn.AllowFocus = False
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 2
        Me.GridColumn26.Width = 309
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "T. Ingreso"
        Me.GridColumn19.FieldName = "AdmissionTypeName"
        Me.GridColumn19.MinWidth = 21
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 4
        Me.GridColumn19.Width = 123
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Fecha Ingreso"
        Me.GridColumn27.DisplayFormat.FormatString = "MM/dd/yyyy HH:mm:ss"
        Me.GridColumn27.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn27.FieldName = "AdmissionDate"
        Me.GridColumn27.MinWidth = 21
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.OptionsColumn.AllowEdit = False
        Me.GridColumn27.OptionsColumn.AllowFocus = False
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 3
        Me.GridColumn27.Width = 141
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Estancia (Cama)"
        Me.GridColumn20.FieldName = "BedStay"
        Me.GridColumn20.MinWidth = 21
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.OptionsColumn.AllowEdit = False
        Me.GridColumn20.OptionsColumn.AllowFocus = False
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 5
        Me.GridColumn20.Width = 183
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "T. Liquidación"
        Me.GridColumn21.FieldName = "LiquidationType"
        Me.GridColumn21.MinWidth = 21
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 6
        Me.GridColumn21.Width = 94
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Unidad Funcional"
        Me.GridColumn22.FieldName = "AdmissionUniFuncCodeName"
        Me.GridColumn22.MinWidth = 21
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.OptionsColumn.AllowFocus = False
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 7
        Me.GridColumn22.Width = 129
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Estado"
        Me.GridColumn23.FieldName = "StatusName"
        Me.GridColumn23.MinWidth = 21
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.OptionsColumn.AllowFocus = False
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 8
        Me.GridColumn23.Width = 81
        '
        'LblState
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LblState, True)
        Me.LblState.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LblState.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblState.Appearance.Options.UseFont = True
        Me.LblState.Appearance.Options.UseForeColor = True
        Me.LblState.Appearance.Options.UseTextOptions = True
        Me.LblState.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblState.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LblState.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LblState, False)
        Me.LblState.Location = New System.Drawing.Point(1313, 53)
        Me.LblState.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LblState.Name = "LblState"
        Me.LblState.Size = New System.Drawing.Size(176, 22)
        Me.LblState.StyleController = Me.INDlcRoot
        Me.LblState.TabIndex = 33
        '
        'SleOperatingUnit
        '
        Me.SleOperatingUnit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SleOperatingUnit.Location = New System.Drawing.Point(1493, 53)
        Me.SleOperatingUnit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SleOperatingUnit.Name = "SleOperatingUnit"
        Me.SleOperatingUnit.Properties.AllowFocused = False
        Me.SleOperatingUnit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.SleOperatingUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.SleOperatingUnit.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.SleOperatingUnit.Properties.Appearance.Options.UseBackColor = True
        Me.SleOperatingUnit.Properties.Appearance.Options.UseFont = True
        Me.SleOperatingUnit.Properties.Appearance.Options.UseForeColor = True
        Me.SleOperatingUnit.Properties.Appearance.Options.UseTextOptions = True
        Me.SleOperatingUnit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleOperatingUnit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SleOperatingUnit.Properties.DisplayMember = "UnitName"
        Me.SleOperatingUnit.Properties.ImmediatePopup = True
        Me.SleOperatingUnit.Properties.NullText = ""
        Me.SleOperatingUnit.Properties.PopupFormMinSize = New System.Drawing.Size(231, 119)
        Me.SleOperatingUnit.Properties.PopupFormSize = New System.Drawing.Size(231, 119)
        Me.SleOperatingUnit.Properties.PopupView = Me.GridLookUpEdit1View
        Me.SleOperatingUnit.Properties.ShowFooter = False
        Me.SleOperatingUnit.Properties.ValueMember = "Id"
        Me.SleOperatingUnit.Size = New System.Drawing.Size(176, 22)
        Me.SleOperatingUnit.StyleController = Me.INDlcRoot
        Me.SleOperatingUnit.TabIndex = 32
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16, Me.GridColumn47})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Código"
        Me.GridColumn16.FieldName = "UnitCode"
        Me.GridColumn16.MinWidth = 21
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        Me.GridColumn16.Width = 505
        '
        'GridColumn47
        '
        Me.GridColumn47.Caption = "Nombre"
        Me.GridColumn47.FieldName = "UnitName"
        Me.GridColumn47.MinWidth = 21
        Me.GridColumn47.Name = "GridColumn47"
        Me.GridColumn47.Visible = True
        Me.GridColumn47.VisibleIndex = 1
        Me.GridColumn47.Width = 886
        '
        'DdbMenu
        '
        Me.DdbMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.DdbMenu.Appearance.Options.UseBackColor = True
        Me.DdbMenu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DdbMenu.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.DdbMenu.DropDownControl = Me.PopupMenu1
        Me.DdbMenu.ImageOptions.Image = CType(resources.GetObject("DdbMenu.ImageOptions.Image"), System.Drawing.Image)
        Me.DdbMenu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.DdbMenu.Location = New System.Drawing.Point(886, 53)
        Me.DdbMenu.Margin = New System.Windows.Forms.Padding(0)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.DdbMenu, False)
        Me.DdbMenu.Name = "DdbMenu"
        Me.DdbMenu.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[False]
        Me.DdbMenu.Size = New System.Drawing.Size(41, 28)
        Me.DdbMenu.StyleController = Me.INDlcRoot
        Me.DdbMenu.TabIndex = 10
        Me.DdbMenu.ToolTip = "Click para desplegar el menú de acciones"
        Me.DdbMenu.ToolTipTitle = "Menú de Acciones"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnOpenAdmission), New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnCloseAdmission)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'INDsbClear
        '
        Me.INDsbClear.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbClear.Appearance.Options.UseFont = True
        Me.INDsbClear.Location = New System.Drawing.Point(1179, 53)
        Me.INDsbClear.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbClear, False)
        Me.INDsbClear.Name = "INDsbClear"
        Me.INDsbClear.Size = New System.Drawing.Size(120, 28)
        Me.INDsbClear.StyleController = Me.INDlcRoot
        Me.INDsbClear.TabIndex = 2
        Me.INDsbClear.Text = "Limpiar"
        '
        'INDsbGererateServiceOrder
        '
        Me.INDsbGererateServiceOrder.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbGererateServiceOrder.Appearance.Options.UseFont = True
        Me.INDsbGererateServiceOrder.Location = New System.Drawing.Point(931, 53)
        Me.INDsbGererateServiceOrder.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbGererateServiceOrder, False)
        Me.INDsbGererateServiceOrder.Name = "INDsbGererateServiceOrder"
        Me.INDsbGererateServiceOrder.Size = New System.Drawing.Size(120, 28)
        Me.INDsbGererateServiceOrder.StyleController = Me.INDlcRoot
        Me.INDsbGererateServiceOrder.TabIndex = 1
        Me.INDsbGererateServiceOrder.Text = "Generar Orden"
        '
        'INDgcValoraciones
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcValoraciones, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcValoraciones, Nothing)
        Me.INDgcValoraciones.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcValoraciones.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcValoraciones, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcValoraciones, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcValoraciones, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcValoraciones, False)
        Me.INDgcValoraciones.Location = New System.Drawing.Point(36, 146)
        Me.INDgcValoraciones.MainView = Me.INDgvValorations
        Me.INDgcValoraciones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcValoraciones.Name = "INDgcValoraciones"
        Me.INDgcValoraciones.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptChkValoration, Me.INDRptPceDetailValoration, Me.INDrpticeGeneratedValoraciones, Me.INDRptImgGenerado, Me.INDRICESkipLiquidationV})
        Me.INDgcValoraciones.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcValoraciones, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcValoraciones.TabIndex = 26
        Me.INDgcValoraciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvValorations})
        '
        'INDgvValorations
        '
        Me.INDgvValorations.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvValorations.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvValorations.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvValorations.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvValorations.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvValorations.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvValorations.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValorations.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvValorations.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValorations.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvValorations.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvValorations.Appearance.Row.Options.UseFont = True
        Me.INDgvValorations.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvValorations.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvValorations.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColSeleccioneValoraciones, Me.INDgcRegisterDate, Me.GridColumn85, Me.INDgcFolioDoneVal, Me.INDgcMDDone, Me.GridColumn86, Me.GridColumn105, Me.GridColumn84, Me.GridColumn185, Me.GridColumn186})
        Me.INDgvValorations.GridControl = Me.INDgcValoraciones
        Me.INDgvValorations.Name = "INDgvValorations"
        Me.INDgvValorations.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvValorations.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvValorations.OptionsView.ShowAutoFilterRow = True
        Me.INDgvValorations.OptionsView.ShowDetailButtons = False
        Me.INDgvValorations.OptionsView.ShowFooter = True
        Me.INDgvValorations.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvValorations, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvValorations, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvValorations, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvValorations, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvValorations, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvValorations, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvValorations, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvValorations, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvValorations, False)
        '
        'ColSeleccioneValoraciones
        '
        Me.ColSeleccioneValoraciones.Caption = "Sel."
        Me.ColSeleccioneValoraciones.ColumnEdit = Me.INDRptChkValoration
        Me.ColSeleccioneValoraciones.FieldName = "Seleccione"
        Me.ColSeleccioneValoraciones.MinWidth = 21
        Me.ColSeleccioneValoraciones.Name = "ColSeleccioneValoraciones"
        Me.ColSeleccioneValoraciones.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSeleccioneValoraciones.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSeleccioneValoraciones.OptionsColumn.AllowMove = False
        Me.ColSeleccioneValoraciones.OptionsColumn.AllowShowHide = False
        Me.ColSeleccioneValoraciones.OptionsColumn.AllowSize = False
        Me.ColSeleccioneValoraciones.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSeleccioneValoraciones.OptionsColumn.FixedWidth = True
        Me.ColSeleccioneValoraciones.Visible = True
        Me.ColSeleccioneValoraciones.VisibleIndex = 0
        Me.ColSeleccioneValoraciones.Width = 28
        '
        'INDRptChkValoration
        '
        Me.INDRptChkValoration.AutoHeight = False
        Me.INDRptChkValoration.Name = "INDRptChkValoration"
        '
        'INDgcRegisterDate
        '
        Me.INDgcRegisterDate.Caption = "Fecha Registro"
        Me.INDgcRegisterDate.FieldName = "FECHISPAC"
        Me.INDgcRegisterDate.MinWidth = 21
        Me.INDgcRegisterDate.Name = "INDgcRegisterDate"
        Me.INDgcRegisterDate.OptionsColumn.AllowEdit = False
        Me.INDgcRegisterDate.OptionsColumn.AllowFocus = False
        Me.INDgcRegisterDate.Visible = True
        Me.INDgcRegisterDate.VisibleIndex = 1
        Me.INDgcRegisterDate.Width = 217
        '
        'GridColumn85
        '
        Me.GridColumn85.Caption = "Unidad Funcional"
        Me.GridColumn85.FieldName = "UFUDESCRI"
        Me.GridColumn85.MinWidth = 21
        Me.GridColumn85.Name = "GridColumn85"
        Me.GridColumn85.OptionsColumn.AllowEdit = False
        Me.GridColumn85.OptionsColumn.AllowFocus = False
        Me.GridColumn85.Visible = True
        Me.GridColumn85.VisibleIndex = 3
        Me.GridColumn85.Width = 282
        '
        'INDgcFolioDoneVal
        '
        Me.INDgcFolioDoneVal.Caption = "Folio Realizado"
        Me.INDgcFolioDoneVal.FieldName = "Folio"
        Me.INDgcFolioDoneVal.MinWidth = 21
        Me.INDgcFolioDoneVal.Name = "INDgcFolioDoneVal"
        Me.INDgcFolioDoneVal.OptionsColumn.AllowEdit = False
        Me.INDgcFolioDoneVal.OptionsColumn.AllowFocus = False
        Me.INDgcFolioDoneVal.Visible = True
        Me.INDgcFolioDoneVal.VisibleIndex = 2
        Me.INDgcFolioDoneVal.Width = 124
        '
        'INDgcMDDone
        '
        Me.INDgcMDDone.Caption = "Médico Realizado"
        Me.INDgcMDDone.FieldName = "Medico"
        Me.INDgcMDDone.MinWidth = 21
        Me.INDgcMDDone.Name = "INDgcMDDone"
        Me.INDgcMDDone.OptionsColumn.AllowEdit = False
        Me.INDgcMDDone.OptionsColumn.AllowFocus = False
        Me.INDgcMDDone.Visible = True
        Me.INDgcMDDone.VisibleIndex = 4
        Me.INDgcMDDone.Width = 436
        '
        'GridColumn86
        '
        Me.GridColumn86.Caption = "Especialidad"
        Me.GridColumn86.FieldName = "DESESPECI"
        Me.GridColumn86.MinWidth = 21
        Me.GridColumn86.Name = "GridColumn86"
        Me.GridColumn86.OptionsColumn.AllowEdit = False
        Me.GridColumn86.OptionsColumn.AllowFocus = False
        Me.GridColumn86.Visible = True
        Me.GridColumn86.VisibleIndex = 5
        Me.GridColumn86.Width = 249
        '
        'GridColumn105
        '
        Me.GridColumn105.Caption = "Tratante"
        Me.GridColumn105.ColumnEdit = Me.INDrpticeGeneratedValoraciones
        Me.GridColumn105.FieldName = "Tratante"
        Me.GridColumn105.MinWidth = 21
        Me.GridColumn105.Name = "GridColumn105"
        Me.GridColumn105.OptionsColumn.AllowEdit = False
        Me.GridColumn105.OptionsColumn.AllowFocus = False
        Me.GridColumn105.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn105.OptionsColumn.AllowMove = False
        Me.GridColumn105.OptionsColumn.AllowSize = False
        Me.GridColumn105.OptionsColumn.FixedWidth = True
        Me.GridColumn105.Visible = True
        Me.GridColumn105.VisibleIndex = 6
        Me.GridColumn105.Width = 130
        '
        'INDrpticeGeneratedValoraciones
        '
        Me.INDrpticeGeneratedValoraciones.AutoHeight = False
        Me.INDrpticeGeneratedValoraciones.Name = "INDrpticeGeneratedValoraciones"
        '
        'GridColumn84
        '
        Me.GridColumn84.Caption = "Centro Atención"
        Me.GridColumn84.FieldName = "NOMCENATE"
        Me.GridColumn84.MinWidth = 21
        Me.GridColumn84.Name = "GridColumn84"
        Me.GridColumn84.OptionsColumn.AllowEdit = False
        Me.GridColumn84.OptionsColumn.AllowFocus = False
        Me.GridColumn84.Width = 309
        '
        'GridColumn185
        '
        Me.GridColumn185.Caption = "Justificación"
        Me.GridColumn185.FieldName = "JustificationCodeName"
        Me.GridColumn185.MinWidth = 21
        Me.GridColumn185.Name = "GridColumn185"
        Me.GridColumn185.OptionsColumn.AllowEdit = False
        Me.GridColumn185.OptionsColumn.AllowFocus = False
        Me.GridColumn185.Visible = True
        Me.GridColumn185.VisibleIndex = 7
        Me.GridColumn185.Width = 63
        '
        'GridColumn186
        '
        Me.GridColumn186.Caption = "Omitir Liquidacion"
        Me.GridColumn186.ColumnEdit = Me.INDRICESkipLiquidationV
        Me.GridColumn186.FieldName = "SkipLiquidation"
        Me.GridColumn186.MinWidth = 21
        Me.GridColumn186.Name = "GridColumn186"
        Me.GridColumn186.OptionsColumn.AllowEdit = False
        Me.GridColumn186.OptionsColumn.AllowFocus = False
        Me.GridColumn186.Visible = True
        Me.GridColumn186.VisibleIndex = 8
        Me.GridColumn186.Width = 69
        '
        'INDRICESkipLiquidationV
        '
        Me.INDRICESkipLiquidationV.AutoHeight = False
        Me.INDRICESkipLiquidationV.Name = "INDRICESkipLiquidationV"
        Me.INDRICESkipLiquidationV.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDRptPceDetailValoration
        '
        Me.INDRptPceDetailValoration.AutoHeight = False
        Me.INDRptPceDetailValoration.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptPceDetailValoration.CloseOnLostFocus = False
        Me.INDRptPceDetailValoration.CloseOnOuterMouseClick = False
        Me.INDRptPceDetailValoration.Name = "INDRptPceDetailValoration"
        Me.INDRptPceDetailValoration.PopupSizeable = False
        Me.INDRptPceDetailValoration.ShowPopupCloseButton = False
        Me.INDRptPceDetailValoration.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptImgGenerado
        '
        Me.INDRptImgGenerado.AutoHeight = False
        Me.INDRptImgGenerado.HtmlImages = Me.ImageCollection1
        Me.INDRptImgGenerado.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por Generar", False, 0)})
        Me.INDRptImgGenerado.LargeImages = Me.ImageCollection1
        Me.INDRptImgGenerado.Name = "INDRptImgGenerado"
        Me.INDRptImgGenerado.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptImgGenerado.SmallImages = Me.ImageCollection1
        '
        'INDgcOxygenConsumption
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcOxygenConsumption, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcOxygenConsumption, Nothing)
        Me.INDgcOxygenConsumption.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcOxygenConsumption.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcOxygenConsumption, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcOxygenConsumption, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcOxygenConsumption, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcOxygenConsumption, False)
        Me.INDgcOxygenConsumption.Location = New System.Drawing.Point(36, 146)
        Me.INDgcOxygenConsumption.MainView = Me.INDgvOxygen
        Me.INDgcOxygenConsumption.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcOxygenConsumption.Name = "INDgcOxygenConsumption"
        Me.INDgcOxygenConsumption.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptChkOrygenConsumpsion, Me.INDrptImeOxygen, Me.INDRICESkipLiquidationJ})
        Me.INDgcOxygenConsumption.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcOxygenConsumption, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcOxygenConsumption.TabIndex = 24
        Me.INDgcOxygenConsumption.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvOxygen})
        '
        'INDgvOxygen
        '
        Me.INDgvOxygen.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvOxygen.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvOxygen.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvOxygen.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvOxygen.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvOxygen.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvOxygen.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvOxygen.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvOxygen.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvOxygen.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvOxygen.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvOxygen.Appearance.Row.Options.UseFont = True
        Me.INDgvOxygen.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvOxygen.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvOxygen.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn72, Me.GridColumn73, Me.ColSelOxygen, Me.GridColumn89, Me.GridColumn95, Me.GridColumn60, Me.GridColumn66, Me.GridColumn67, Me.GridColumn68, Me.GridColumn69, Me.GridColumn70, Me.GridColumn71, Me.GridColumn181, Me.GridColumn182})
        Me.INDgvOxygen.GridControl = Me.INDgcOxygenConsumption
        Me.INDgvOxygen.GroupCount = 2
        Me.INDgvOxygen.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTLITADM", Me.GridColumn70, "Total: {0:C0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTHORAS", Me.GridColumn69, "Total: {0:n1}")})
        Me.INDgvOxygen.Name = "INDgvOxygen"
        Me.INDgvOxygen.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvOxygen.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvOxygen.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvOxygen.OptionsView.ShowAutoFilterRow = True
        Me.INDgvOxygen.OptionsView.ShowDetailButtons = False
        Me.INDgvOxygen.OptionsView.ShowFooter = True
        Me.INDgvOxygen.OptionsView.ShowGroupPanel = False
        Me.INDgvOxygen.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn72, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn73, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvOxygen, False)
        '
        'GridColumn72
        '
        Me.GridColumn72.Caption = "Unidad Funcional"
        Me.GridColumn72.FieldName = "UFUDESCRI"
        Me.GridColumn72.MinWidth = 21
        Me.GridColumn72.Name = "GridColumn72"
        Me.GridColumn72.OptionsColumn.AllowEdit = False
        Me.GridColumn72.OptionsColumn.AllowFocus = False
        Me.GridColumn72.Visible = True
        Me.GridColumn72.VisibleIndex = 0
        '
        'GridColumn73
        '
        Me.GridColumn73.Caption = "Vía Administración"
        Me.GridColumn73.FieldName = "DESVIAADM"
        Me.GridColumn73.MinWidth = 21
        Me.GridColumn73.Name = "GridColumn73"
        Me.GridColumn73.OptionsColumn.AllowEdit = False
        Me.GridColumn73.OptionsColumn.AllowFocus = False
        Me.GridColumn73.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTLITADM", "Total: {0:C1}")})
        Me.GridColumn73.Visible = True
        Me.GridColumn73.VisibleIndex = 2
        '
        'ColSelOxygen
        '
        Me.ColSelOxygen.Caption = "Sel."
        Me.ColSelOxygen.ColumnEdit = Me.INDrptChkOrygenConsumpsion
        Me.ColSelOxygen.FieldName = "Seleccione"
        Me.ColSelOxygen.MinWidth = 21
        Me.ColSelOxygen.Name = "ColSelOxygen"
        Me.ColSelOxygen.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelOxygen.OptionsColumn.AllowIncrementalSearch = False
        Me.ColSelOxygen.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelOxygen.OptionsColumn.AllowMove = False
        Me.ColSelOxygen.OptionsColumn.AllowShowHide = False
        Me.ColSelOxygen.OptionsColumn.AllowSize = False
        Me.ColSelOxygen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelOxygen.OptionsColumn.FixedWidth = True
        Me.ColSelOxygen.OptionsFilter.AllowAutoFilter = False
        Me.ColSelOxygen.OptionsFilter.AllowFilter = False
        Me.ColSelOxygen.Visible = True
        Me.ColSelOxygen.VisibleIndex = 0
        Me.ColSelOxygen.Width = 40
        '
        'INDrptChkOrygenConsumpsion
        '
        Me.INDrptChkOrygenConsumpsion.AutoHeight = False
        Me.INDrptChkOrygenConsumpsion.Name = "INDrptChkOrygenConsumpsion"
        '
        'GridColumn89
        '
        Me.GridColumn89.Caption = "Código"
        Me.GridColumn89.FieldName = "CODSERIPS"
        Me.GridColumn89.MinWidth = 21
        Me.GridColumn89.Name = "GridColumn89"
        Me.GridColumn89.OptionsColumn.AllowEdit = False
        Me.GridColumn89.OptionsColumn.AllowFocus = False
        Me.GridColumn89.Visible = True
        Me.GridColumn89.VisibleIndex = 1
        Me.GridColumn89.Width = 202
        '
        'GridColumn95
        '
        Me.GridColumn95.Caption = "Paciente"
        Me.GridColumn95.FieldName = "PersonName"
        Me.GridColumn95.MinWidth = 21
        Me.GridColumn95.Name = "GridColumn95"
        Me.GridColumn95.OptionsColumn.AllowEdit = False
        Me.GridColumn95.OptionsColumn.AllowFocus = False
        Me.GridColumn95.Visible = True
        Me.GridColumn95.VisibleIndex = 2
        Me.GridColumn95.Width = 285
        '
        'GridColumn60
        '
        Me.GridColumn60.Caption = "Fecha"
        Me.GridColumn60.DisplayFormat.FormatString = "D"
        Me.GridColumn60.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn60.FieldName = "DIAS"
        Me.GridColumn60.MinWidth = 21
        Me.GridColumn60.Name = "GridColumn60"
        Me.GridColumn60.OptionsColumn.AllowEdit = False
        Me.GridColumn60.OptionsColumn.AllowFocus = False
        Me.GridColumn60.Visible = True
        Me.GridColumn60.VisibleIndex = 3
        Me.GridColumn60.Width = 153
        '
        'GridColumn66
        '
        Me.GridColumn66.Caption = "Lit X Min"
        Me.GridColumn66.FieldName = "LITRXMINUT"
        Me.GridColumn66.MinWidth = 21
        Me.GridColumn66.Name = "GridColumn66"
        Me.GridColumn66.OptionsColumn.AllowEdit = False
        Me.GridColumn66.OptionsColumn.AllowFocus = False
        Me.GridColumn66.Visible = True
        Me.GridColumn66.VisibleIndex = 4
        Me.GridColumn66.Width = 133
        '
        'GridColumn67
        '
        Me.GridColumn67.Caption = "Hora Inicial"
        Me.GridColumn67.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
        Me.GridColumn67.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn67.FieldName = "HORAINICIA"
        Me.GridColumn67.MinWidth = 21
        Me.GridColumn67.Name = "GridColumn67"
        Me.GridColumn67.OptionsColumn.AllowEdit = False
        Me.GridColumn67.OptionsColumn.AllowFocus = False
        Me.GridColumn67.Visible = True
        Me.GridColumn67.VisibleIndex = 5
        Me.GridColumn67.Width = 123
        '
        'GridColumn68
        '
        Me.GridColumn68.Caption = "Hora Final"
        Me.GridColumn68.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
        Me.GridColumn68.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn68.FieldName = "HORAFINAL"
        Me.GridColumn68.MinWidth = 21
        Me.GridColumn68.Name = "GridColumn68"
        Me.GridColumn68.OptionsColumn.AllowEdit = False
        Me.GridColumn68.OptionsColumn.AllowFocus = False
        Me.GridColumn68.Visible = True
        Me.GridColumn68.VisibleIndex = 6
        Me.GridColumn68.Width = 115
        '
        'GridColumn69
        '
        Me.GridColumn69.Caption = "Total Horas"
        Me.GridColumn69.FieldName = "TOTHORAS"
        Me.GridColumn69.MinWidth = 21
        Me.GridColumn69.Name = "GridColumn69"
        Me.GridColumn69.OptionsColumn.AllowEdit = False
        Me.GridColumn69.OptionsColumn.AllowFocus = False
        Me.GridColumn69.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTHORAS", "Total: {0:n1}")})
        Me.GridColumn69.Visible = True
        Me.GridColumn69.VisibleIndex = 7
        Me.GridColumn69.Width = 174
        '
        'GridColumn70
        '
        Me.GridColumn70.Caption = "Total LitAdmin"
        Me.GridColumn70.FieldName = "TOTLITADM"
        Me.GridColumn70.MinWidth = 21
        Me.GridColumn70.Name = "GridColumn70"
        Me.GridColumn70.OptionsColumn.AllowEdit = False
        Me.GridColumn70.OptionsColumn.AllowFocus = False
        Me.GridColumn70.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTLITADM", "Total: {0:C1}")})
        Me.GridColumn70.Visible = True
        Me.GridColumn70.VisibleIndex = 8
        Me.GridColumn70.Width = 165
        '
        'GridColumn71
        '
        Me.GridColumn71.Caption = "Profesional Responsable"
        Me.GridColumn71.FieldName = "NOMMEDICO"
        Me.GridColumn71.MinWidth = 21
        Me.GridColumn71.Name = "GridColumn71"
        Me.GridColumn71.OptionsColumn.AllowEdit = False
        Me.GridColumn71.OptionsColumn.AllowFocus = False
        Me.GridColumn71.Visible = True
        Me.GridColumn71.VisibleIndex = 9
        Me.GridColumn71.Width = 286
        '
        'GridColumn181
        '
        Me.GridColumn181.Caption = "Justificación"
        Me.GridColumn181.FieldName = "JustificationCodeName"
        Me.GridColumn181.MinWidth = 21
        Me.GridColumn181.Name = "GridColumn181"
        Me.GridColumn181.OptionsColumn.AllowEdit = False
        Me.GridColumn181.OptionsColumn.AllowFocus = False
        Me.GridColumn181.Visible = True
        Me.GridColumn181.VisibleIndex = 10
        '
        'GridColumn182
        '
        Me.GridColumn182.Caption = "Omitir Liquidacion"
        Me.GridColumn182.ColumnEdit = Me.INDRICESkipLiquidationJ
        Me.GridColumn182.FieldName = "SkipLiquidation"
        Me.GridColumn182.MinWidth = 21
        Me.GridColumn182.Name = "GridColumn182"
        Me.GridColumn182.OptionsColumn.AllowEdit = False
        Me.GridColumn182.OptionsColumn.AllowFocus = False
        Me.GridColumn182.Visible = True
        Me.GridColumn182.VisibleIndex = 11
        '
        'INDRICESkipLiquidationJ
        '
        Me.INDRICESkipLiquidationJ.AutoHeight = False
        Me.INDRICESkipLiquidationJ.Name = "INDRICESkipLiquidationJ"
        Me.INDRICESkipLiquidationJ.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDrptImeOxygen
        '
        Me.INDrptImeOxygen.AutoHeight = False
        Me.INDrptImeOxygen.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por Generar", False, 0)})
        Me.INDrptImeOxygen.LargeImages = Me.ImageCollection1
        Me.INDrptImeOxygen.Name = "INDrptImeOxygen"
        Me.INDrptImeOxygen.SmallImages = Me.ImageCollection1
        '
        'INDgcTerapy
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcTerapy, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcTerapy, Nothing)
        Me.INDgcTerapy.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcTerapy.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcTerapy, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcTerapy, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcTerapy, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcTerapy, False)
        Me.INDgcTerapy.Location = New System.Drawing.Point(36, 146)
        Me.INDgcTerapy.MainView = Me.INDgvTherapy
        Me.INDgcTerapy.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcTerapy.Name = "INDgcTerapy"
        Me.INDgcTerapy.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptPceDetailTherapy, Me.INDrptChkTherapy, Me.INDRptTherapyGenerated, Me.INDRICESkipLiquidationT})
        Me.INDgcTerapy.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcTerapy, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcTerapy.TabIndex = 23
        Me.INDgcTerapy.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvTherapy})
        '
        'INDgvTherapy
        '
        Me.INDgvTherapy.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvTherapy.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvTherapy.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvTherapy.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvTherapy.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvTherapy.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvTherapy.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvTherapy.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvTherapy.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvTherapy.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvTherapy.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvTherapy.Appearance.Row.Options.UseFont = True
        Me.INDgvTherapy.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvTherapy.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvTherapy.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColSelTerapy, Me.GridColumn62, Me.GridColumn63, Me.GridColumn64, Me.GridColumn65, Me.GridColumn94, Me.GridColumn157, Me.GridColumn158, Me.GridColumn159, Me.GridColumn160, Me.GridColumn161, Me.GridColumn179, Me.GridColumn180, Me.GridColumn199})
        Me.INDgvTherapy.GridControl = Me.INDgcTerapy
        Me.INDgvTherapy.GroupCount = 1
        Me.INDgvTherapy.Name = "INDgvTherapy"
        Me.INDgvTherapy.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvTherapy.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvTherapy.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvTherapy.OptionsView.ShowAutoFilterRow = True
        Me.INDgvTherapy.OptionsView.ShowDetailButtons = False
        Me.INDgvTherapy.OptionsView.ShowFooter = True
        Me.INDgvTherapy.OptionsView.ShowGroupPanel = False
        Me.INDgvTherapy.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn62, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvTherapy, False)
        '
        'ColSelTerapy
        '
        Me.ColSelTerapy.Caption = "Sel."
        Me.ColSelTerapy.ColumnEdit = Me.INDrptChkTherapy
        Me.ColSelTerapy.FieldName = "Seleccione"
        Me.ColSelTerapy.MinWidth = 21
        Me.ColSelTerapy.Name = "ColSelTerapy"
        Me.ColSelTerapy.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelTerapy.OptionsColumn.AllowIncrementalSearch = False
        Me.ColSelTerapy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelTerapy.OptionsColumn.AllowMove = False
        Me.ColSelTerapy.OptionsColumn.AllowShowHide = False
        Me.ColSelTerapy.OptionsColumn.AllowSize = False
        Me.ColSelTerapy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelTerapy.OptionsColumn.FixedWidth = True
        Me.ColSelTerapy.OptionsFilter.AllowAutoFilter = False
        Me.ColSelTerapy.OptionsFilter.AllowFilter = False
        Me.ColSelTerapy.Visible = True
        Me.ColSelTerapy.VisibleIndex = 0
        Me.ColSelTerapy.Width = 39
        '
        'INDrptChkTherapy
        '
        Me.INDrptChkTherapy.AutoHeight = False
        Me.INDrptChkTherapy.Name = "INDrptChkTherapy"
        '
        'GridColumn62
        '
        Me.GridColumn62.Caption = "Código"
        Me.GridColumn62.FieldName = "CODSERIPS"
        Me.GridColumn62.MinWidth = 21
        Me.GridColumn62.Name = "GridColumn62"
        Me.GridColumn62.OptionsColumn.AllowEdit = False
        Me.GridColumn62.OptionsColumn.AllowFocus = False
        Me.GridColumn62.Visible = True
        Me.GridColumn62.VisibleIndex = 1
        Me.GridColumn62.Width = 268
        '
        'GridColumn63
        '
        Me.GridColumn63.Caption = "Terapia"
        Me.GridColumn63.FieldName = "DESSERIPS"
        Me.GridColumn63.MinWidth = 21
        Me.GridColumn63.Name = "GridColumn63"
        Me.GridColumn63.OptionsColumn.AllowEdit = False
        Me.GridColumn63.OptionsColumn.AllowFocus = False
        Me.GridColumn63.Visible = True
        Me.GridColumn63.VisibleIndex = 1
        Me.GridColumn63.Width = 565
        '
        'GridColumn64
        '
        Me.GridColumn64.Caption = "Cantidad"
        Me.GridColumn64.FieldName = "CODSERIPS"
        Me.GridColumn64.MinWidth = 21
        Me.GridColumn64.Name = "GridColumn64"
        Me.GridColumn64.OptionsColumn.AllowEdit = False
        Me.GridColumn64.OptionsColumn.AllowFocus = False
        Me.GridColumn64.Visible = True
        Me.GridColumn64.VisibleIndex = 3
        Me.GridColumn64.Width = 238
        '
        'GridColumn65
        '
        Me.GridColumn65.Caption = "Folio"
        Me.GridColumn65.FieldName = "Folio"
        Me.GridColumn65.MinWidth = 21
        Me.GridColumn65.Name = "GridColumn65"
        Me.GridColumn65.OptionsColumn.AllowEdit = False
        Me.GridColumn65.OptionsColumn.AllowFocus = False
        Me.GridColumn65.Visible = True
        Me.GridColumn65.VisibleIndex = 4
        Me.GridColumn65.Width = 92
        '
        'GridColumn94
        '
        Me.GridColumn94.Caption = " Fecha Solicitud"
        Me.GridColumn94.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn94.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn94.FieldName = "Fecha"
        Me.GridColumn94.MinWidth = 21
        Me.GridColumn94.Name = "GridColumn94"
        Me.GridColumn94.OptionsColumn.AllowEdit = False
        Me.GridColumn94.OptionsColumn.AllowFocus = False
        Me.GridColumn94.Visible = True
        Me.GridColumn94.VisibleIndex = 5
        Me.GridColumn94.Width = 92
        '
        'GridColumn157
        '
        Me.GridColumn157.Caption = "Fecha Realizado"
        Me.GridColumn157.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn157.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn157.FieldName = "FechaRealizacion"
        Me.GridColumn157.MinWidth = 21
        Me.GridColumn157.Name = "GridColumn157"
        Me.GridColumn157.OptionsColumn.AllowEdit = False
        Me.GridColumn157.OptionsColumn.AllowFocus = False
        Me.GridColumn157.Visible = True
        Me.GridColumn157.VisibleIndex = 6
        Me.GridColumn157.Width = 98
        '
        'GridColumn158
        '
        Me.GridColumn158.Caption = "Medico"
        Me.GridColumn158.FieldName = "Medico"
        Me.GridColumn158.MinWidth = 21
        Me.GridColumn158.Name = "GridColumn158"
        Me.GridColumn158.OptionsColumn.AllowEdit = False
        Me.GridColumn158.OptionsColumn.AllowFocus = False
        '
        'GridColumn159
        '
        Me.GridColumn159.Caption = "Observación"
        Me.GridColumn159.FieldName = "Observacion"
        Me.GridColumn159.MinWidth = 21
        Me.GridColumn159.Name = "GridColumn159"
        Me.GridColumn159.OptionsColumn.AllowEdit = False
        Me.GridColumn159.OptionsColumn.AllowFocus = False
        '
        'GridColumn160
        '
        Me.GridColumn160.Caption = "Medico Realizó"
        Me.GridColumn160.FieldName = "MedicoRealizo"
        Me.GridColumn160.MinWidth = 21
        Me.GridColumn160.Name = "GridColumn160"
        Me.GridColumn160.OptionsColumn.AllowEdit = False
        Me.GridColumn160.OptionsColumn.AllowFocus = False
        '
        'GridColumn161
        '
        Me.GridColumn161.Caption = "Unidad Funcional"
        Me.GridColumn161.FieldName = "UnidadFuncional"
        Me.GridColumn161.MinWidth = 21
        Me.GridColumn161.Name = "GridColumn161"
        Me.GridColumn161.OptionsColumn.AllowEdit = False
        Me.GridColumn161.OptionsColumn.AllowFocus = False
        '
        'GridColumn179
        '
        Me.GridColumn179.Caption = "Justificación"
        Me.GridColumn179.FieldName = "JustificationCodeName"
        Me.GridColumn179.MinWidth = 21
        Me.GridColumn179.Name = "GridColumn179"
        Me.GridColumn179.OptionsColumn.AllowEdit = False
        Me.GridColumn179.OptionsColumn.AllowFocus = False
        Me.GridColumn179.Visible = True
        Me.GridColumn179.VisibleIndex = 7
        Me.GridColumn179.Width = 51
        '
        'GridColumn180
        '
        Me.GridColumn180.Caption = "Omitir Liquidacion"
        Me.GridColumn180.ColumnEdit = Me.INDRICESkipLiquidationT
        Me.GridColumn180.FieldName = "SkipLiquidation"
        Me.GridColumn180.MinWidth = 21
        Me.GridColumn180.Name = "GridColumn180"
        Me.GridColumn180.OptionsColumn.AllowEdit = False
        Me.GridColumn180.OptionsColumn.AllowFocus = False
        Me.GridColumn180.Visible = True
        Me.GridColumn180.VisibleIndex = 8
        Me.GridColumn180.Width = 62
        '
        'INDRICESkipLiquidationT
        '
        Me.INDRICESkipLiquidationT.AutoHeight = False
        Me.INDRICESkipLiquidationT.Name = "INDRICESkipLiquidationT"
        Me.INDRICESkipLiquidationT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GridColumn199
        '
        Me.GridColumn199.Caption = "Descripción Relacionada"
        Me.GridColumn199.CustomizationCaption = "Descripción Relacionada"
        Me.GridColumn199.FieldName = "CodeNameContractDescriptions"
        Me.GridColumn199.MinWidth = 21
        Me.GridColumn199.Name = "GridColumn199"
        Me.GridColumn199.Visible = True
        Me.GridColumn199.VisibleIndex = 2
        Me.GridColumn199.Width = 363
        '
        'INDrptPceDetailTherapy
        '
        Me.INDrptPceDetailTherapy.AutoHeight = False
        Me.INDrptPceDetailTherapy.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptPceDetailTherapy.Name = "INDrptPceDetailTherapy"
        Me.INDrptPceDetailTherapy.PopupSizeable = False
        Me.INDrptPceDetailTherapy.ShowPopupCloseButton = False
        Me.INDrptPceDetailTherapy.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptTherapyGenerated
        '
        Me.INDRptTherapyGenerated.AutoHeight = False
        Me.INDRptTherapyGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptTherapyGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptTherapyGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptTherapyGenerated.Name = "INDRptTherapyGenerated"
        Me.INDRptTherapyGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptTherapyGenerated.SmallImages = Me.ImageCollection1
        '
        'INDgcConsultation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcConsultation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcConsultation, Nothing)
        Me.INDgcConsultation.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcConsultation.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcConsultation, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcConsultation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcConsultation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcConsultation, False)
        Me.INDgcConsultation.Location = New System.Drawing.Point(36, 146)
        Me.INDgcConsultation.MainView = Me.INDgvConsultation
        Me.INDgcConsultation.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcConsultation.Name = "INDgcConsultation"
        Me.INDgcConsultation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptPceDetailConsultation, Me.INDrptChkInterSearch, Me.INDRptIntercosultasGenerated, Me.INDRICESkipLiquidationC, Me.INDRptInterconsultasValidated})
        Me.INDgcConsultation.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcConsultation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcConsultation.TabIndex = 22
        Me.INDgcConsultation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvConsultation})
        '
        'INDgvConsultation
        '
        Me.INDgvConsultation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvConsultation.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvConsultation.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvConsultation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvConsultation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvConsultation.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvConsultation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvConsultation.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvConsultation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvConsultation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvConsultation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvConsultation.Appearance.Row.Options.UseFont = True
        Me.INDgvConsultation.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvConsultation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvConsultation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn54, Me.ColSelInterconsulta, Me.GridColumn56, Me.GridColumn57, Me.GridColumn58, Me.INDgcFolioRequest, Me.GridColumn93, Me.GridColumn137, Me.GridColumn138, Me.INDgcFolioDoneIC, Me.GridColumn139, Me.GridColumn140, Me.GridColumn141, Me.GridColumn142, Me.GridColumn121, Me.GridColumn175, Me.GridColumn176})
        Me.INDgvConsultation.GridControl = Me.INDgcConsultation
        Me.INDgvConsultation.GroupCount = 1
        Me.INDgvConsultation.Name = "INDgvConsultation"
        Me.INDgvConsultation.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvConsultation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvConsultation.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvConsultation.OptionsView.ShowAutoFilterRow = True
        Me.INDgvConsultation.OptionsView.ShowDetailButtons = False
        Me.INDgvConsultation.OptionsView.ShowFooter = True
        Me.INDgvConsultation.OptionsView.ShowGroupPanel = False
        Me.INDgvConsultation.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn54, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvConsultation, False)
        '
        'GridColumn54
        '
        Me.GridColumn54.Caption = "Tipo"
        Me.GridColumn54.FieldName = "Tipo"
        Me.GridColumn54.MinWidth = 21
        Me.GridColumn54.Name = "GridColumn54"
        Me.GridColumn54.Visible = True
        Me.GridColumn54.VisibleIndex = 0
        Me.GridColumn54.Width = 231
        '
        'ColSelInterconsulta
        '
        Me.ColSelInterconsulta.Caption = "Sel."
        Me.ColSelInterconsulta.ColumnEdit = Me.INDrptChkInterSearch
        Me.ColSelInterconsulta.FieldName = "Seleccione"
        Me.ColSelInterconsulta.MinWidth = 21
        Me.ColSelInterconsulta.Name = "ColSelInterconsulta"
        Me.ColSelInterconsulta.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelInterconsulta.OptionsColumn.AllowIncrementalSearch = False
        Me.ColSelInterconsulta.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelInterconsulta.OptionsColumn.AllowMove = False
        Me.ColSelInterconsulta.OptionsColumn.AllowShowHide = False
        Me.ColSelInterconsulta.OptionsColumn.AllowSize = False
        Me.ColSelInterconsulta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelInterconsulta.OptionsColumn.FixedWidth = True
        Me.ColSelInterconsulta.OptionsFilter.AllowAutoFilter = False
        Me.ColSelInterconsulta.OptionsFilter.AllowFilter = False
        Me.ColSelInterconsulta.Visible = True
        Me.ColSelInterconsulta.VisibleIndex = 0
        Me.ColSelInterconsulta.Width = 40
        '
        'INDrptChkInterSearch
        '
        Me.INDrptChkInterSearch.AutoHeight = False
        Me.INDrptChkInterSearch.Name = "INDrptChkInterSearch"
        '
        'GridColumn56
        '
        Me.GridColumn56.Caption = "Código"
        Me.GridColumn56.FieldName = "CODSERIPS"
        Me.GridColumn56.MinWidth = 21
        Me.GridColumn56.Name = "GridColumn56"
        Me.GridColumn56.OptionsColumn.AllowEdit = False
        Me.GridColumn56.OptionsColumn.AllowFocus = False
        Me.GridColumn56.Visible = True
        Me.GridColumn56.VisibleIndex = 1
        Me.GridColumn56.Width = 160
        '
        'GridColumn57
        '
        Me.GridColumn57.Caption = "Interconsultas"
        Me.GridColumn57.FieldName = "DESSERIPS"
        Me.GridColumn57.MinWidth = 21
        Me.GridColumn57.Name = "GridColumn57"
        Me.GridColumn57.OptionsColumn.AllowEdit = False
        Me.GridColumn57.OptionsColumn.AllowFocus = False
        Me.GridColumn57.Visible = True
        Me.GridColumn57.VisibleIndex = 2
        Me.GridColumn57.Width = 363
        '
        'GridColumn58
        '
        Me.GridColumn58.Caption = "Cantidad"
        Me.GridColumn58.FieldName = "CANSERIPS"
        Me.GridColumn58.MinWidth = 21
        Me.GridColumn58.Name = "GridColumn58"
        Me.GridColumn58.OptionsColumn.AllowEdit = False
        Me.GridColumn58.OptionsColumn.AllowFocus = False
        Me.GridColumn58.Visible = True
        Me.GridColumn58.VisibleIndex = 3
        Me.GridColumn58.Width = 105
        '
        'INDgcFolioRequest
        '
        Me.INDgcFolioRequest.Caption = "Folio Solicitud"
        Me.INDgcFolioRequest.FieldName = "FolioSolicitud"
        Me.INDgcFolioRequest.MinWidth = 21
        Me.INDgcFolioRequest.Name = "INDgcFolioRequest"
        Me.INDgcFolioRequest.OptionsColumn.AllowEdit = False
        Me.INDgcFolioRequest.OptionsColumn.AllowFocus = False
        Me.INDgcFolioRequest.Visible = True
        Me.INDgcFolioRequest.VisibleIndex = 4
        '
        'GridColumn93
        '
        Me.GridColumn93.Caption = "Fecha Solicitud"
        Me.GridColumn93.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn93.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn93.FieldName = "FechaSolicitud"
        Me.GridColumn93.MinWidth = 21
        Me.GridColumn93.Name = "GridColumn93"
        Me.GridColumn93.OptionsColumn.AllowEdit = False
        Me.GridColumn93.OptionsColumn.AllowFocus = False
        Me.GridColumn93.Visible = True
        Me.GridColumn93.VisibleIndex = 5
        '
        'GridColumn137
        '
        Me.GridColumn137.Caption = "Fecha realizado"
        Me.GridColumn137.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn137.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn137.FieldName = "FechaRealizacion"
        Me.GridColumn137.MinWidth = 21
        Me.GridColumn137.Name = "GridColumn137"
        Me.GridColumn137.OptionsColumn.AllowEdit = False
        Me.GridColumn137.OptionsColumn.AllowFocus = False
        Me.GridColumn137.Visible = True
        Me.GridColumn137.VisibleIndex = 6
        '
        'GridColumn138
        '
        Me.GridColumn138.Caption = "Interpretación"
        Me.GridColumn138.FieldName = "Interpretacion"
        Me.GridColumn138.MinWidth = 21
        Me.GridColumn138.Name = "GridColumn138"
        Me.GridColumn138.OptionsColumn.AllowEdit = False
        Me.GridColumn138.OptionsColumn.AllowFocus = False
        Me.GridColumn138.Visible = True
        Me.GridColumn138.VisibleIndex = 7
        '
        'INDgcFolioDoneIC
        '
        Me.INDgcFolioDoneIC.Caption = "Folio Realizado"
        Me.INDgcFolioDoneIC.Name = "INDgcFolioDoneIC"
        Me.INDgcFolioDoneIC.FieldName = "FolioRealizado"
        Me.INDgcFolioDoneIC.Visible = True
        Me.INDgcFolioDoneIC.VisibleIndex = 8
        '
        'GridColumn139
        '
        Me.GridColumn139.Caption = "Medico"
        Me.GridColumn139.FieldName = "Medico"
        Me.GridColumn139.MinWidth = 21
        Me.GridColumn139.Name = "GridColumn139"
        Me.GridColumn139.OptionsColumn.AllowEdit = False
        Me.GridColumn139.OptionsColumn.AllowFocus = False
        '
        'GridColumn140
        '
        Me.GridColumn140.Caption = "Observación"
        Me.GridColumn140.FieldName = "Observacion"
        Me.GridColumn140.MinWidth = 21
        Me.GridColumn140.Name = "GridColumn140"
        Me.GridColumn140.OptionsColumn.AllowEdit = False
        Me.GridColumn140.OptionsColumn.AllowFocus = False
        '
        'GridColumn141
        '
        Me.GridColumn141.Caption = "Medico Realizo"
        Me.GridColumn141.FieldName = "MedicoRealizado"
        Me.GridColumn141.MinWidth = 21
        Me.GridColumn141.Name = "GridColumn141"
        Me.GridColumn141.OptionsColumn.AllowEdit = False
        Me.GridColumn141.OptionsColumn.AllowFocus = False
        '
        'GridColumn142
        '
        Me.GridColumn142.Caption = "Unidad Funcional"
        Me.GridColumn142.FieldName = "UnidadFuncional"
        Me.GridColumn142.MinWidth = 21
        Me.GridColumn142.Name = "GridColumn142"
        Me.GridColumn142.OptionsColumn.AllowEdit = False
        Me.GridColumn142.OptionsColumn.AllowFocus = False
        '
        'GridColumn121
        '
        Me.GridColumn121.Caption = "Descripción Relacionada"
        Me.GridColumn121.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn121.MinWidth = 21
        Me.GridColumn121.Name = "GridColumn121"
        Me.GridColumn121.OptionsColumn.AllowEdit = False
        Me.GridColumn121.OptionsColumn.AllowFocus = False
        Me.GridColumn121.Width = 261
        '
        'GridColumn175
        '
        Me.GridColumn175.Caption = "Justificación"
        Me.GridColumn175.FieldName = "JustificationCodeName"
        Me.GridColumn175.MinWidth = 21
        Me.GridColumn175.Name = "GridColumn175"
        Me.GridColumn175.OptionsColumn.AllowEdit = False
        Me.GridColumn175.OptionsColumn.AllowFocus = False
        Me.GridColumn175.Visible = True
        Me.GridColumn175.VisibleIndex = 9
        '
        'GridColumn176
        '
        Me.GridColumn176.Caption = "Omitir Liquidacion"
        Me.GridColumn176.ColumnEdit = Me.INDRICESkipLiquidationC
        Me.GridColumn176.FieldName = "SkipLiquidation"
        Me.GridColumn176.MinWidth = 21
        Me.GridColumn176.Name = "GridColumn176"
        Me.GridColumn176.OptionsColumn.AllowEdit = False
        Me.GridColumn176.OptionsColumn.AllowFocus = False
        Me.GridColumn176.Visible = True
        Me.GridColumn176.VisibleIndex = 10
        '
        'INDRICESkipLiquidationC
        '
        Me.INDRICESkipLiquidationC.AutoHeight = False
        Me.INDRICESkipLiquidationC.Name = "INDRICESkipLiquidationC"
        Me.INDRICESkipLiquidationC.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDrptPceDetailConsultation
        '
        Me.INDrptPceDetailConsultation.AutoHeight = False
        Me.INDrptPceDetailConsultation.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptPceDetailConsultation.Name = "INDrptPceDetailConsultation"
        Me.INDrptPceDetailConsultation.PopupSizeable = False
        Me.INDrptPceDetailConsultation.ShowPopupCloseButton = False
        Me.INDrptPceDetailConsultation.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptIntercosultasGenerated
        '
        Me.INDRptIntercosultasGenerated.AutoHeight = False
        Me.INDRptIntercosultasGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptIntercosultasGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptIntercosultasGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptIntercosultasGenerated.Name = "INDRptIntercosultasGenerated"
        Me.INDRptIntercosultasGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptIntercosultasGenerated.SmallImages = Me.ImageCollection1
        '
        'INDgcProceduresNoQX
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcProceduresNoQX, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcProceduresNoQX, Nothing)
        Me.INDgcProceduresNoQX.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcProceduresNoQX.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcProceduresNoQX, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcProceduresNoQX, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcProceduresNoQX, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcProceduresNoQX, False)
        Me.INDgcProceduresNoQX.Location = New System.Drawing.Point(36, 146)
        Me.INDgcProceduresNoQX.MainView = Me.INDgvProceduresNoQx
        Me.INDgcProceduresNoQX.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcProceduresNoQX.Name = "INDgcProceduresNoQX"
        Me.INDgcProceduresNoQX.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptPceDetailProceduresNoQx, Me.INDrptChkProceduresNoQx, Me.INDrptSleContractDescriptionProceduresNoQx, Me.INDRptNoQxGenerated, Me.INDRICESkipLiquidationNQX})
        Me.INDgcProceduresNoQX.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcProceduresNoQX, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcProceduresNoQX.TabIndex = 21
        Me.INDgcProceduresNoQX.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvProceduresNoQx})
        '
        'INDgvProceduresNoQx
        '
        Me.INDgvProceduresNoQx.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvProceduresNoQx.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvProceduresNoQx.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvProceduresNoQx.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvProceduresNoQx.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvProceduresNoQx.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvProceduresNoQx.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvProceduresNoQx.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvProceduresNoQx.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvProceduresNoQx.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvProceduresNoQx.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvProceduresNoQx.Appearance.Row.Options.UseFont = True
        Me.INDgvProceduresNoQx.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvProceduresNoQx.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvProceduresNoQx.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColSelNoQx, Me.GridColumn48, Me.GridColumn50, Me.GridColumn51, Me.GridColumn52, Me.GridColumn92, Me.GridColumn53, Me.GridColumn97, Me.ColContractDescriptionNoQx, Me.GridColumn102, Me.GridColumn162, Me.GridColumn163, Me.GridColumn164, Me.GridColumn173, Me.GridColumn174})
        Me.INDgvProceduresNoQx.GridControl = Me.INDgcProceduresNoQX
        Me.INDgvProceduresNoQx.GroupCount = 1
        Me.INDgvProceduresNoQx.Name = "INDgvProceduresNoQx"
        Me.INDgvProceduresNoQx.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvProceduresNoQx.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvProceduresNoQx.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvProceduresNoQx.OptionsView.ShowAutoFilterRow = True
        Me.INDgvProceduresNoQx.OptionsView.ShowDetailButtons = False
        Me.INDgvProceduresNoQx.OptionsView.ShowFooter = True
        Me.INDgvProceduresNoQx.OptionsView.ShowGroupPanel = False
        Me.INDgvProceduresNoQx.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn48, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvProceduresNoQx, False)
        '
        'ColSelNoQx
        '
        Me.ColSelNoQx.Caption = "Sel."
        Me.ColSelNoQx.ColumnEdit = Me.INDrptChkProceduresNoQx
        Me.ColSelNoQx.FieldName = "Seleccione"
        Me.ColSelNoQx.MinWidth = 21
        Me.ColSelNoQx.Name = "ColSelNoQx"
        Me.ColSelNoQx.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelNoQx.OptionsColumn.AllowIncrementalSearch = False
        Me.ColSelNoQx.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelNoQx.OptionsColumn.AllowMove = False
        Me.ColSelNoQx.OptionsColumn.AllowShowHide = False
        Me.ColSelNoQx.OptionsColumn.AllowSize = False
        Me.ColSelNoQx.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelNoQx.OptionsColumn.FixedWidth = True
        Me.ColSelNoQx.OptionsFilter.AllowAutoFilter = False
        Me.ColSelNoQx.OptionsFilter.AllowFilter = False
        Me.ColSelNoQx.Visible = True
        Me.ColSelNoQx.VisibleIndex = 0
        Me.ColSelNoQx.Width = 39
        '
        'INDrptChkProceduresNoQx
        '
        Me.INDrptChkProceduresNoQx.AutoHeight = False
        Me.INDrptChkProceduresNoQx.Name = "INDrptChkProceduresNoQx"
        '
        'GridColumn48
        '
        Me.GridColumn48.Caption = "Tipo"
        Me.GridColumn48.FieldName = "Tipo"
        Me.GridColumn48.MinWidth = 21
        Me.GridColumn48.Name = "GridColumn48"
        Me.GridColumn48.OptionsColumn.AllowEdit = False
        Me.GridColumn48.OptionsColumn.AllowFocus = False
        Me.GridColumn48.Visible = True
        Me.GridColumn48.VisibleIndex = 0
        '
        'GridColumn50
        '
        Me.GridColumn50.Caption = "Código"
        Me.GridColumn50.FieldName = "CODSERIPS"
        Me.GridColumn50.MinWidth = 21
        Me.GridColumn50.Name = "GridColumn50"
        Me.GridColumn50.OptionsColumn.AllowEdit = False
        Me.GridColumn50.OptionsColumn.AllowFocus = False
        Me.GridColumn50.Visible = True
        Me.GridColumn50.VisibleIndex = 1
        Me.GridColumn50.Width = 327
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "Procedimientos No Qx"
        Me.GridColumn51.FieldName = "DESSERIPS"
        Me.GridColumn51.MinWidth = 21
        Me.GridColumn51.Name = "GridColumn51"
        Me.GridColumn51.OptionsColumn.AllowEdit = False
        Me.GridColumn51.OptionsColumn.AllowFocus = False
        Me.GridColumn51.Visible = True
        Me.GridColumn51.VisibleIndex = 2
        Me.GridColumn51.Width = 651
        '
        'GridColumn52
        '
        Me.GridColumn52.Caption = "Cantidad"
        Me.GridColumn52.FieldName = "CANSERIPS"
        Me.GridColumn52.MinWidth = 21
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.OptionsColumn.AllowEdit = False
        Me.GridColumn52.OptionsColumn.AllowFocus = False
        Me.GridColumn52.Visible = True
        Me.GridColumn52.VisibleIndex = 3
        Me.GridColumn52.Width = 231
        '
        'GridColumn92
        '
        Me.GridColumn92.Caption = "Folio"
        Me.GridColumn92.FieldName = "Folio"
        Me.GridColumn92.MinWidth = 21
        Me.GridColumn92.Name = "GridColumn92"
        Me.GridColumn92.OptionsColumn.AllowEdit = False
        Me.GridColumn92.OptionsColumn.AllowFocus = False
        Me.GridColumn92.Visible = True
        Me.GridColumn92.VisibleIndex = 5
        Me.GridColumn92.Width = 139
        '
        'GridColumn53
        '
        Me.GridColumn53.Caption = " Fecha Solicitud"
        Me.GridColumn53.DisplayFormat.FormatString = "d"
        Me.GridColumn53.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn53.FieldName = "Fecha"
        Me.GridColumn53.MinWidth = 21
        Me.GridColumn53.Name = "GridColumn53"
        Me.GridColumn53.Visible = True
        Me.GridColumn53.VisibleIndex = 4
        Me.GridColumn53.Width = 139
        '
        'GridColumn97
        '
        Me.GridColumn97.Caption = "Fecha Realizado"
        Me.GridColumn97.DisplayFormat.FormatString = "d"
        Me.GridColumn97.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn97.FieldName = "FechaRealizacion"
        Me.GridColumn97.MinWidth = 21
        Me.GridColumn97.Name = "GridColumn97"
        Me.GridColumn97.OptionsColumn.AllowEdit = False
        Me.GridColumn97.OptionsColumn.AllowFocus = False
        Me.GridColumn97.Visible = True
        Me.GridColumn97.VisibleIndex = 6
        Me.GridColumn97.Width = 153
        '
        'ColContractDescriptionNoQx
        '
        Me.ColContractDescriptionNoQx.Caption = "Descripción Relacionada"
        Me.ColContractDescriptionNoQx.ColumnEdit = Me.INDrptSleContractDescriptionProceduresNoQx
        Me.ColContractDescriptionNoQx.FieldName = "CUPSEntityContractDescriptionId"
        Me.ColContractDescriptionNoQx.MinWidth = 21
        Me.ColContractDescriptionNoQx.Name = "ColContractDescriptionNoQx"
        Me.ColContractDescriptionNoQx.Width = 201
        '
        'INDrptSleContractDescriptionProceduresNoQx
        '
        Me.INDrptSleContractDescriptionProceduresNoQx.AllowFocused = False
        Me.INDrptSleContractDescriptionProceduresNoQx.AutoHeight = False
        Me.INDrptSleContractDescriptionProceduresNoQx.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptSleContractDescriptionProceduresNoQx.DisplayMember = "ContractDescriptionId.CodeName"
        Me.INDrptSleContractDescriptionProceduresNoQx.Name = "INDrptSleContractDescriptionProceduresNoQx"
        Me.INDrptSleContractDescriptionProceduresNoQx.NullText = ""
        Me.INDrptSleContractDescriptionProceduresNoQx.PopupSizeable = False
        Me.INDrptSleContractDescriptionProceduresNoQx.PopupView = Me.GridView1
        Me.INDrptSleContractDescriptionProceduresNoQx.ValueMember = "Id"
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn120, Me.GridColumn123})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn120
        '
        Me.GridColumn120.Caption = "Código"
        Me.GridColumn120.FieldName = "ContractDescriptionId.Code"
        Me.GridColumn120.MinWidth = 21
        Me.GridColumn120.Name = "GridColumn120"
        Me.GridColumn120.Visible = True
        Me.GridColumn120.VisibleIndex = 0
        '
        'GridColumn123
        '
        Me.GridColumn123.Caption = "Nombre"
        Me.GridColumn123.FieldName = "ContractDescriptionId.Name"
        Me.GridColumn123.MinWidth = 21
        Me.GridColumn123.Name = "GridColumn123"
        Me.GridColumn123.Visible = True
        Me.GridColumn123.VisibleIndex = 1
        '
        'GridColumn102
        '
        Me.GridColumn102.Caption = "Medico"
        Me.GridColumn102.FieldName = "Medico"
        Me.GridColumn102.MinWidth = 21
        Me.GridColumn102.Name = "GridColumn102"
        Me.GridColumn102.OptionsColumn.AllowEdit = False
        Me.GridColumn102.OptionsColumn.AllowFocus = False
        '
        'GridColumn162
        '
        Me.GridColumn162.Caption = "Observación"
        Me.GridColumn162.FieldName = "Observacion"
        Me.GridColumn162.MinWidth = 21
        Me.GridColumn162.Name = "GridColumn162"
        Me.GridColumn162.OptionsColumn.AllowEdit = False
        Me.GridColumn162.OptionsColumn.AllowFocus = False
        '
        'GridColumn163
        '
        Me.GridColumn163.Caption = "Medico Realizó"
        Me.GridColumn163.FieldName = "MedicoRealizo"
        Me.GridColumn163.MinWidth = 21
        Me.GridColumn163.Name = "GridColumn163"
        Me.GridColumn163.OptionsColumn.AllowEdit = False
        Me.GridColumn163.OptionsColumn.AllowFocus = False
        '
        'GridColumn164
        '
        Me.GridColumn164.Caption = "Unidad Funcional"
        Me.GridColumn164.FieldName = "UnidadFuncional"
        Me.GridColumn164.MinWidth = 21
        Me.GridColumn164.Name = "GridColumn164"
        Me.GridColumn164.OptionsColumn.AllowEdit = False
        Me.GridColumn164.OptionsColumn.AllowFocus = False
        '
        'GridColumn173
        '
        Me.GridColumn173.Caption = "Justificación"
        Me.GridColumn173.FieldName = "JustificationCodeName"
        Me.GridColumn173.MinWidth = 21
        Me.GridColumn173.Name = "GridColumn173"
        Me.GridColumn173.OptionsColumn.AllowEdit = False
        Me.GridColumn173.OptionsColumn.AllowFocus = False
        Me.GridColumn173.Visible = True
        Me.GridColumn173.VisibleIndex = 7
        '
        'GridColumn174
        '
        Me.GridColumn174.Caption = "Omitir Liquidacion"
        Me.GridColumn174.ColumnEdit = Me.INDRICESkipLiquidationNQX
        Me.GridColumn174.FieldName = "SkipLiquidation"
        Me.GridColumn174.MinWidth = 21
        Me.GridColumn174.Name = "GridColumn174"
        Me.GridColumn174.OptionsColumn.AllowEdit = False
        Me.GridColumn174.OptionsColumn.AllowFocus = False
        Me.GridColumn174.Visible = True
        Me.GridColumn174.VisibleIndex = 8
        '
        'INDRICESkipLiquidationNQX
        '
        Me.INDRICESkipLiquidationNQX.AutoHeight = False
        Me.INDRICESkipLiquidationNQX.Name = "INDRICESkipLiquidationNQX"
        Me.INDRICESkipLiquidationNQX.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDrptPceDetailProceduresNoQx
        '
        Me.INDrptPceDetailProceduresNoQx.AutoHeight = False
        Me.INDrptPceDetailProceduresNoQx.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptPceDetailProceduresNoQx.Name = "INDrptPceDetailProceduresNoQx"
        Me.INDrptPceDetailProceduresNoQx.PopupSizeable = False
        Me.INDrptPceDetailProceduresNoQx.ShowPopupCloseButton = False
        Me.INDrptPceDetailProceduresNoQx.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptNoQxGenerated
        '
        Me.INDRptNoQxGenerated.AutoHeight = False
        Me.INDRptNoQxGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptNoQxGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptNoQxGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptNoQxGenerated.Name = "INDRptNoQxGenerated"
        Me.INDRptNoQxGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptNoQxGenerated.SmallImages = Me.ImageCollection1
        '
        'INDgcImagesDx
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcImagesDx, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcImagesDx, Nothing)
        Me.INDgcImagesDx.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcImagesDx.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcImagesDx, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcImagesDx, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcImagesDx, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcImagesDx, False)
        Me.INDgcImagesDx.Location = New System.Drawing.Point(36, 146)
        Me.INDgcImagesDx.MainView = Me.INDgvImagesDX
        Me.INDgcImagesDx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcImagesDx.Name = "INDgcImagesDx"
        Me.INDgcImagesDx.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptDetailImagesDX, Me.INDrptChkImagesDx, Me.INDRptImagesGenerated, Me.INDRICESkipLiquidationDx})
        Me.INDgcImagesDx.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcImagesDx, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcImagesDx.TabIndex = 19
        Me.INDgcImagesDx.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvImagesDX})
        '
        'INDgvImagesDX
        '
        Me.INDgvImagesDX.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvImagesDX.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvImagesDX.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvImagesDX.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvImagesDX.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvImagesDX.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvImagesDX.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvImagesDX.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvImagesDX.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvImagesDX.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvImagesDX.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvImagesDX.Appearance.Row.Options.UseFont = True
        Me.INDgvImagesDX.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvImagesDX.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvImagesDX.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn42, Me.ColSelImagesDx, Me.GridColumn37, Me.GridColumn38, Me.GridColumn39, Me.GridColumn91, Me.GridColumn130, Me.GridColumn131, Me.GridColumn132, Me.GridColumn133, Me.GridColumn134, Me.GridColumn135, Me.GridColumn136, Me.GridColumn118, Me.GridColumn169, Me.GridColumn170})
        Me.INDgvImagesDX.GridControl = Me.INDgcImagesDx
        Me.INDgvImagesDX.GroupCount = 1
        Me.INDgvImagesDX.Name = "INDgvImagesDX"
        Me.INDgvImagesDX.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvImagesDX.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvImagesDX.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvImagesDX.OptionsView.ShowAutoFilterRow = True
        Me.INDgvImagesDX.OptionsView.ShowDetailButtons = False
        Me.INDgvImagesDX.OptionsView.ShowFooter = True
        Me.INDgvImagesDX.OptionsView.ShowGroupPanel = False
        Me.INDgvImagesDX.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn42, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvImagesDX, False)
        '
        'GridColumn42
        '
        Me.GridColumn42.Caption = "Tipo"
        Me.GridColumn42.FieldName = "Tipo"
        Me.GridColumn42.MinWidth = 21
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.Visible = True
        Me.GridColumn42.VisibleIndex = 0
        '
        'ColSelImagesDx
        '
        Me.ColSelImagesDx.Caption = "Sel."
        Me.ColSelImagesDx.ColumnEdit = Me.INDrptChkImagesDx
        Me.ColSelImagesDx.FieldName = "Seleccione"
        Me.ColSelImagesDx.MinWidth = 21
        Me.ColSelImagesDx.Name = "ColSelImagesDx"
        Me.ColSelImagesDx.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.ColSelImagesDx.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelImagesDx.OptionsColumn.AllowMove = False
        Me.ColSelImagesDx.OptionsColumn.AllowShowHide = False
        Me.ColSelImagesDx.OptionsColumn.AllowSize = False
        Me.ColSelImagesDx.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelImagesDx.OptionsColumn.FixedWidth = True
        Me.ColSelImagesDx.Visible = True
        Me.ColSelImagesDx.VisibleIndex = 0
        Me.ColSelImagesDx.Width = 39
        '
        'INDrptChkImagesDx
        '
        Me.INDrptChkImagesDx.AutoHeight = False
        Me.INDrptChkImagesDx.Name = "INDrptChkImagesDx"
        '
        'GridColumn37
        '
        Me.GridColumn37.Caption = "Código"
        Me.GridColumn37.FieldName = "CODSERIPS"
        Me.GridColumn37.MinWidth = 21
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.OptionsColumn.AllowEdit = False
        Me.GridColumn37.OptionsColumn.AllowFocus = False
        Me.GridColumn37.Visible = True
        Me.GridColumn37.VisibleIndex = 1
        Me.GridColumn37.Width = 297
        '
        'GridColumn38
        '
        Me.GridColumn38.Caption = "Imágenes"
        Me.GridColumn38.FieldName = "DESSERIPS"
        Me.GridColumn38.MinWidth = 21
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.OptionsColumn.AllowEdit = False
        Me.GridColumn38.OptionsColumn.AllowFocus = False
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 2
        Me.GridColumn38.Width = 618
        '
        'GridColumn39
        '
        Me.GridColumn39.Caption = "Cantidad"
        Me.GridColumn39.FieldName = "CANSERIPS"
        Me.GridColumn39.MinWidth = 21
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.OptionsColumn.AllowEdit = False
        Me.GridColumn39.OptionsColumn.AllowFocus = False
        Me.GridColumn39.Visible = True
        Me.GridColumn39.VisibleIndex = 3
        Me.GridColumn39.Width = 178
        '
        'GridColumn91
        '
        Me.GridColumn91.Caption = "Folio"
        Me.GridColumn91.FieldName = "Folio"
        Me.GridColumn91.MinWidth = 21
        Me.GridColumn91.Name = "GridColumn91"
        Me.GridColumn91.OptionsColumn.AllowEdit = False
        Me.GridColumn91.OptionsColumn.AllowFocus = False
        Me.GridColumn91.Visible = True
        Me.GridColumn91.VisibleIndex = 4
        Me.GridColumn91.Width = 135
        '
        'GridColumn130
        '
        Me.GridColumn130.Caption = "Fecha Solicitud"
        Me.GridColumn130.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn130.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn130.FieldName = "Fecha"
        Me.GridColumn130.MinWidth = 21
        Me.GridColumn130.Name = "GridColumn130"
        Me.GridColumn130.OptionsColumn.AllowEdit = False
        Me.GridColumn130.OptionsColumn.AllowFocus = False
        Me.GridColumn130.Visible = True
        Me.GridColumn130.VisibleIndex = 5
        Me.GridColumn130.Width = 135
        '
        'GridColumn131
        '
        Me.GridColumn131.Caption = "Fecha Realizado"
        Me.GridColumn131.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn131.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn131.FieldName = "FechaRealizacion"
        Me.GridColumn131.MinWidth = 21
        Me.GridColumn131.Name = "GridColumn131"
        Me.GridColumn131.OptionsColumn.AllowEdit = False
        Me.GridColumn131.OptionsColumn.AllowFocus = False
        Me.GridColumn131.Visible = True
        Me.GridColumn131.VisibleIndex = 6
        Me.GridColumn131.Width = 135
        '
        'GridColumn132
        '
        Me.GridColumn132.Caption = "Interpretación"
        Me.GridColumn132.FieldName = "Interpretacion"
        Me.GridColumn132.MinWidth = 21
        Me.GridColumn132.Name = "GridColumn132"
        Me.GridColumn132.OptionsColumn.AllowEdit = False
        Me.GridColumn132.OptionsColumn.AllowFocus = False
        Me.GridColumn132.Visible = True
        Me.GridColumn132.VisibleIndex = 7
        Me.GridColumn132.Width = 141
        '
        'GridColumn133
        '
        Me.GridColumn133.Caption = "Medico"
        Me.GridColumn133.FieldName = "Medico"
        Me.GridColumn133.MinWidth = 21
        Me.GridColumn133.Name = "GridColumn133"
        Me.GridColumn133.OptionsColumn.AllowEdit = False
        Me.GridColumn133.OptionsColumn.AllowFocus = False
        '
        'GridColumn134
        '
        Me.GridColumn134.Caption = "Observación"
        Me.GridColumn134.FieldName = "Observacion"
        Me.GridColumn134.MinWidth = 21
        Me.GridColumn134.Name = "GridColumn134"
        '
        'GridColumn135
        '
        Me.GridColumn135.Caption = "Medico Realizo"
        Me.GridColumn135.FieldName = "MedicoRealizo"
        Me.GridColumn135.MinWidth = 21
        Me.GridColumn135.Name = "GridColumn135"
        Me.GridColumn135.OptionsColumn.AllowEdit = False
        Me.GridColumn135.OptionsColumn.AllowFocus = False
        '
        'GridColumn136
        '
        Me.GridColumn136.Caption = "Unidad Funcional"
        Me.GridColumn136.FieldName = "UnidadFuncional"
        Me.GridColumn136.MinWidth = 21
        Me.GridColumn136.Name = "GridColumn136"
        Me.GridColumn136.OptionsColumn.AllowEdit = False
        Me.GridColumn136.OptionsColumn.AllowFocus = False
        '
        'GridColumn118
        '
        Me.GridColumn118.Caption = "Descripción Relacionada"
        Me.GridColumn118.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn118.MinWidth = 21
        Me.GridColumn118.Name = "GridColumn118"
        Me.GridColumn118.OptionsColumn.AllowEdit = False
        Me.GridColumn118.OptionsColumn.AllowFocus = False
        Me.GridColumn118.Width = 207
        '
        'GridColumn169
        '
        Me.GridColumn169.Caption = "Justificación"
        Me.GridColumn169.FieldName = "JustificationCodeName"
        Me.GridColumn169.MinWidth = 21
        Me.GridColumn169.Name = "GridColumn169"
        Me.GridColumn169.OptionsColumn.AllowEdit = False
        Me.GridColumn169.OptionsColumn.AllowFocus = False
        Me.GridColumn169.Visible = True
        Me.GridColumn169.VisibleIndex = 8
        '
        'GridColumn170
        '
        Me.GridColumn170.Caption = "Omitir Liquidacion"
        Me.GridColumn170.ColumnEdit = Me.INDRICESkipLiquidationDx
        Me.GridColumn170.FieldName = "SkipLiquidation"
        Me.GridColumn170.MinWidth = 21
        Me.GridColumn170.Name = "GridColumn170"
        Me.GridColumn170.OptionsColumn.AllowEdit = False
        Me.GridColumn170.OptionsColumn.AllowFocus = False
        Me.GridColumn170.Visible = True
        Me.GridColumn170.VisibleIndex = 9
        '
        'INDRICESkipLiquidationDx
        '
        Me.INDRICESkipLiquidationDx.AutoHeight = False
        Me.INDRICESkipLiquidationDx.Name = "INDRICESkipLiquidationDx"
        Me.INDRICESkipLiquidationDx.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDrptDetailImagesDX
        '
        Me.INDrptDetailImagesDX.AutoHeight = False
        Me.INDrptDetailImagesDX.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptDetailImagesDX.Name = "INDrptDetailImagesDX"
        Me.INDrptDetailImagesDX.PopupSizeable = False
        Me.INDrptDetailImagesDX.ShowPopupCloseButton = False
        Me.INDrptDetailImagesDX.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptImagesGenerated
        '
        Me.INDRptImagesGenerated.AutoHeight = False
        Me.INDRptImagesGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptImagesGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptImagesGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptImagesGenerated.Name = "INDRptImagesGenerated"
        Me.INDRptImagesGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptImagesGenerated.SmallImages = Me.ImageCollection1
        '
        'INDgcPathology
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcPathology, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcPathology, Nothing)
        Me.INDgcPathology.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcPathology.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcPathology, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcPathology, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcPathology, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcPathology, False)
        Me.INDgcPathology.Location = New System.Drawing.Point(36, 146)
        Me.INDgcPathology.MainView = Me.INDgvPathologies
        Me.INDgcPathology.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcPathology.Name = "INDgcPathology"
        Me.INDgcPathology.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptPceDetailPathologies, Me.INDrptChkPathologies, Me.INDRptPathologiesGenerated, Me.INDRICESkipLiquidationP})
        Me.INDgcPathology.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcPathology, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcPathology.TabIndex = 18
        Me.INDgcPathology.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvPathologies})
        '
        'INDgvPathologies
        '
        Me.INDgvPathologies.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvPathologies.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvPathologies.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvPathologies.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvPathologies.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvPathologies.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvPathologies.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvPathologies.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvPathologies.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvPathologies.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvPathologies.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvPathologies.Appearance.Row.Options.UseFont = True
        Me.INDgvPathologies.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvPathologies.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvPathologies.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn35, Me.ColSelPathologies, Me.GridColumn31, Me.GridColumn32, Me.GridColumn33, Me.GridColumn90, Me.GridColumn117, Me.GridColumn148, Me.GridColumn149, Me.GridColumn34, Me.GridColumn151, Me.GridColumn150, Me.GridColumn152, Me.GridColumn153, Me.GridColumn167, Me.GridColumn168})
        Me.INDgvPathologies.GridControl = Me.INDgcPathology
        Me.INDgvPathologies.GroupCount = 1
        Me.INDgvPathologies.Name = "INDgvPathologies"
        Me.INDgvPathologies.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvPathologies.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvPathologies.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvPathologies.OptionsView.ShowAutoFilterRow = True
        Me.INDgvPathologies.OptionsView.ShowDetailButtons = False
        Me.INDgvPathologies.OptionsView.ShowFooter = True
        Me.INDgvPathologies.OptionsView.ShowGroupPanel = False
        Me.INDgvPathologies.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn35, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvPathologies, False)
        '
        'GridColumn35
        '
        Me.GridColumn35.Caption = "Tipo"
        Me.GridColumn35.FieldName = "Tipo"
        Me.GridColumn35.MinWidth = 21
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Visible = True
        Me.GridColumn35.VisibleIndex = 0
        '
        'ColSelPathologies
        '
        Me.ColSelPathologies.Caption = "Sel."
        Me.ColSelPathologies.ColumnEdit = Me.INDrptChkPathologies
        Me.ColSelPathologies.FieldName = "Seleccione"
        Me.ColSelPathologies.MinWidth = 21
        Me.ColSelPathologies.Name = "ColSelPathologies"
        Me.ColSelPathologies.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelPathologies.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelPathologies.OptionsColumn.AllowMove = False
        Me.ColSelPathologies.OptionsColumn.AllowShowHide = False
        Me.ColSelPathologies.OptionsColumn.AllowSize = False
        Me.ColSelPathologies.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSelPathologies.OptionsColumn.FixedWidth = True
        Me.ColSelPathologies.OptionsFilter.AllowAutoFilter = False
        Me.ColSelPathologies.OptionsFilter.AllowFilter = False
        Me.ColSelPathologies.Visible = True
        Me.ColSelPathologies.VisibleIndex = 0
        Me.ColSelPathologies.Width = 40
        '
        'INDrptChkPathologies
        '
        Me.INDrptChkPathologies.AutoHeight = False
        Me.INDrptChkPathologies.Name = "INDrptChkPathologies"
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Código"
        Me.GridColumn31.FieldName = "CODSERIPS"
        Me.GridColumn31.MinWidth = 21
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.OptionsColumn.AllowFocus = False
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 1
        Me.GridColumn31.Width = 253
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "Patologías"
        Me.GridColumn32.FieldName = "DESSERIPS"
        Me.GridColumn32.MinWidth = 21
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.OptionsColumn.AllowEdit = False
        Me.GridColumn32.OptionsColumn.AllowFocus = False
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 2
        Me.GridColumn32.Width = 361
        '
        'GridColumn33
        '
        Me.GridColumn33.Caption = "Cantidad"
        Me.GridColumn33.FieldName = "CANSERIPS"
        Me.GridColumn33.MinWidth = 21
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.OptionsColumn.AllowEdit = False
        Me.GridColumn33.OptionsColumn.AllowFocus = False
        Me.GridColumn33.Visible = True
        Me.GridColumn33.VisibleIndex = 3
        Me.GridColumn33.Width = 216
        '
        'GridColumn90
        '
        Me.GridColumn90.Caption = "Folio"
        Me.GridColumn90.FieldName = "Folio"
        Me.GridColumn90.MinWidth = 21
        Me.GridColumn90.Name = "GridColumn90"
        Me.GridColumn90.OptionsColumn.AllowEdit = False
        Me.GridColumn90.OptionsColumn.AllowFocus = False
        Me.GridColumn90.Visible = True
        Me.GridColumn90.VisibleIndex = 4
        '
        'GridColumn117
        '
        Me.GridColumn117.Caption = "Fecha solicitud"
        Me.GridColumn117.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn117.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn117.FieldName = "Fecha"
        Me.GridColumn117.MinWidth = 21
        Me.GridColumn117.Name = "GridColumn117"
        Me.GridColumn117.OptionsColumn.AllowEdit = False
        Me.GridColumn117.OptionsColumn.AllowFocus = False
        Me.GridColumn117.Visible = True
        Me.GridColumn117.VisibleIndex = 5
        '
        'GridColumn148
        '
        Me.GridColumn148.Caption = "Fecha realizado"
        Me.GridColumn148.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn148.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn148.FieldName = "FechaRealizacion"
        Me.GridColumn148.MinWidth = 21
        Me.GridColumn148.Name = "GridColumn148"
        Me.GridColumn148.OptionsColumn.AllowEdit = False
        Me.GridColumn148.OptionsColumn.AllowFocus = False
        Me.GridColumn148.Visible = True
        Me.GridColumn148.VisibleIndex = 6
        '
        'GridColumn149
        '
        Me.GridColumn149.Caption = "Interpretación"
        Me.GridColumn149.FieldName = "Interpretacion"
        Me.GridColumn149.MinWidth = 21
        Me.GridColumn149.Name = "GridColumn149"
        Me.GridColumn149.OptionsColumn.AllowEdit = False
        Me.GridColumn149.OptionsColumn.AllowFocus = False
        Me.GridColumn149.Visible = True
        Me.GridColumn149.VisibleIndex = 7
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "Descripción Relacionada"
        Me.GridColumn34.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn34.MinWidth = 21
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.OptionsColumn.AllowEdit = False
        Me.GridColumn34.OptionsColumn.AllowFocus = False
        '
        'GridColumn151
        '
        Me.GridColumn151.Caption = "Medico"
        Me.GridColumn151.FieldName = "Medico"
        Me.GridColumn151.MinWidth = 21
        Me.GridColumn151.Name = "GridColumn151"
        Me.GridColumn151.OptionsColumn.AllowEdit = False
        Me.GridColumn151.OptionsColumn.AllowFocus = False
        '
        'GridColumn150
        '
        Me.GridColumn150.Caption = "Observación"
        Me.GridColumn150.FieldName = "Observacion"
        Me.GridColumn150.MinWidth = 21
        Me.GridColumn150.Name = "GridColumn150"
        Me.GridColumn150.OptionsColumn.AllowEdit = False
        Me.GridColumn150.OptionsColumn.AllowFocus = False
        '
        'GridColumn152
        '
        Me.GridColumn152.Caption = "Medico Realizó"
        Me.GridColumn152.FieldName = "MedicoRealizo"
        Me.GridColumn152.MinWidth = 21
        Me.GridColumn152.Name = "GridColumn152"
        Me.GridColumn152.OptionsColumn.AllowEdit = False
        Me.GridColumn152.OptionsColumn.AllowFocus = False
        '
        'GridColumn153
        '
        Me.GridColumn153.Caption = "Unidad Funcional"
        Me.GridColumn153.FieldName = "UnidadFuncional"
        Me.GridColumn153.MinWidth = 21
        Me.GridColumn153.Name = "GridColumn153"
        Me.GridColumn153.OptionsColumn.AllowEdit = False
        Me.GridColumn153.OptionsColumn.AllowFocus = False
        '
        'GridColumn167
        '
        Me.GridColumn167.Caption = "Justificación"
        Me.GridColumn167.FieldName = "JustificationCodeName"
        Me.GridColumn167.MinWidth = 21
        Me.GridColumn167.Name = "GridColumn167"
        Me.GridColumn167.OptionsColumn.AllowEdit = False
        Me.GridColumn167.OptionsColumn.AllowFocus = False
        Me.GridColumn167.Visible = True
        Me.GridColumn167.VisibleIndex = 8
        '
        'GridColumn168
        '
        Me.GridColumn168.Caption = "Omitir Liquidacion"
        Me.GridColumn168.ColumnEdit = Me.INDRICESkipLiquidationP
        Me.GridColumn168.FieldName = "SkipLiquidation"
        Me.GridColumn168.MinWidth = 21
        Me.GridColumn168.Name = "GridColumn168"
        Me.GridColumn168.OptionsColumn.AllowEdit = False
        Me.GridColumn168.OptionsColumn.AllowFocus = False
        Me.GridColumn168.Visible = True
        Me.GridColumn168.VisibleIndex = 9
        '
        'INDRICESkipLiquidationP
        '
        Me.INDRICESkipLiquidationP.AutoHeight = False
        Me.INDRICESkipLiquidationP.Name = "INDRICESkipLiquidationP"
        Me.INDRICESkipLiquidationP.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDrptPceDetailPathologies
        '
        Me.INDrptPceDetailPathologies.AutoHeight = False
        Me.INDrptPceDetailPathologies.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptPceDetailPathologies.Name = "INDrptPceDetailPathologies"
        Me.INDrptPceDetailPathologies.PopupControl = Me.INDpccServicesProcedures
        Me.INDrptPceDetailPathologies.PopupSizeable = False
        Me.INDrptPceDetailPathologies.ShowPopupCloseButton = False
        Me.INDrptPceDetailPathologies.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptPathologiesGenerated
        '
        Me.INDRptPathologiesGenerated.AutoHeight = False
        Me.INDRptPathologiesGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptPathologiesGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptPathologiesGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptPathologiesGenerated.Name = "INDRptPathologiesGenerated"
        Me.INDRptPathologiesGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptPathologiesGenerated.SmallImages = Me.ImageCollection1
        '
        'INDgcLaboratories
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLaboratories, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLaboratories, Nothing)
        Me.INDgcLaboratories.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcLaboratories.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLaboratories, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLaboratories, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLaboratories, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLaboratories, False)
        Me.INDgcLaboratories.Location = New System.Drawing.Point(36, 146)
        Me.INDgcLaboratories.MainView = Me.INDgvLaboratories
        Me.INDgcLaboratories.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcLaboratories.Name = "INDgcLaboratories"
        Me.INDgcLaboratories.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptPceDetailLaboratories, Me.INDrptChkLaboratories, Me.INDRptLaboratoriesGenerated, Me.INDRICESkipLiquidation})
        Me.INDgcLaboratories.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLaboratories, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcLaboratories.TabIndex = 17
        Me.INDgcLaboratories.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvLaboratories})
        '
        'INDgvLaboratories
        '
        Me.INDgvLaboratories.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvLaboratories.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvLaboratories.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvLaboratories.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvLaboratories.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvLaboratories.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvLaboratories.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvLaboratories.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvLaboratories.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvLaboratories.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvLaboratories.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvLaboratories.Appearance.Row.Options.UseFont = True
        Me.INDgvLaboratories.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvLaboratories.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvLaboratories.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn15, Me.ColSeleccioneLaboratorio, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn59, Me.GridColumn87, Me.GridColumn143, Me.GridColumn144, Me.GridColumn116, Me.GridColumn14, Me.GridColumn145, Me.GridColumn146, Me.GridColumn147, Me.GridColumn165, Me.GridColumn166})
        Me.INDgvLaboratories.GridControl = Me.INDgcLaboratories
        Me.INDgvLaboratories.GroupCount = 1
        Me.INDgvLaboratories.Name = "INDgvLaboratories"
        Me.INDgvLaboratories.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvLaboratories.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvLaboratories.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvLaboratories.OptionsView.ShowAutoFilterRow = True
        Me.INDgvLaboratories.OptionsView.ShowDetailButtons = False
        Me.INDgvLaboratories.OptionsView.ShowFooter = True
        Me.INDgvLaboratories.OptionsView.ShowGroupPanel = False
        Me.INDgvLaboratories.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn15, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDgvLaboratories, False)
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Tipo"
        Me.GridColumn15.FieldName = "Tipo"
        Me.GridColumn15.MinWidth = 21
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 0
        '
        'ColSeleccioneLaboratorio
        '
        Me.ColSeleccioneLaboratorio.Caption = "Sel."
        Me.ColSeleccioneLaboratorio.ColumnEdit = Me.INDrptChkLaboratories
        Me.ColSeleccioneLaboratorio.FieldName = "Seleccione"
        Me.ColSeleccioneLaboratorio.MinWidth = 21
        Me.ColSeleccioneLaboratorio.Name = "ColSeleccioneLaboratorio"
        Me.ColSeleccioneLaboratorio.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSeleccioneLaboratorio.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSeleccioneLaboratorio.OptionsColumn.AllowMove = False
        Me.ColSeleccioneLaboratorio.OptionsColumn.AllowShowHide = False
        Me.ColSeleccioneLaboratorio.OptionsColumn.AllowSize = False
        Me.ColSeleccioneLaboratorio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSeleccioneLaboratorio.OptionsColumn.FixedWidth = True
        Me.ColSeleccioneLaboratorio.OptionsFilter.AllowAutoFilter = False
        Me.ColSeleccioneLaboratorio.OptionsFilter.AllowFilter = False
        Me.ColSeleccioneLaboratorio.Visible = True
        Me.ColSeleccioneLaboratorio.VisibleIndex = 0
        Me.ColSeleccioneLaboratorio.Width = 33
        '
        'INDrptChkLaboratories
        '
        Me.INDrptChkLaboratories.AutoHeight = False
        Me.INDrptChkLaboratories.Name = "INDrptChkLaboratories"
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Codigo"
        Me.GridColumn11.FieldName = "CODSERIPS"
        Me.GridColumn11.MinWidth = 21
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        Me.GridColumn11.Width = 201
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Laboratorio"
        Me.GridColumn12.FieldName = "DESSERIPS"
        Me.GridColumn12.MinWidth = 21
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 2
        Me.GridColumn12.Width = 636
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Cantidad"
        Me.GridColumn13.FieldName = "CANSERIPS"
        Me.GridColumn13.MinWidth = 21
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 3
        Me.GridColumn13.Width = 178
        '
        'GridColumn59
        '
        Me.GridColumn59.Caption = "Folio"
        Me.GridColumn59.FieldName = "Folio"
        Me.GridColumn59.MinWidth = 21
        Me.GridColumn59.Name = "GridColumn59"
        Me.GridColumn59.OptionsColumn.AllowEdit = False
        Me.GridColumn59.OptionsColumn.AllowFocus = False
        Me.GridColumn59.Visible = True
        Me.GridColumn59.VisibleIndex = 4
        Me.GridColumn59.Width = 127
        '
        'GridColumn87
        '
        Me.GridColumn87.Caption = "Fecha Solicitud"
        Me.GridColumn87.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn87.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn87.FieldName = "Fecha"
        Me.GridColumn87.MinWidth = 21
        Me.GridColumn87.Name = "GridColumn87"
        Me.GridColumn87.OptionsColumn.AllowEdit = False
        Me.GridColumn87.OptionsColumn.AllowFocus = False
        Me.GridColumn87.Visible = True
        Me.GridColumn87.VisibleIndex = 5
        Me.GridColumn87.Width = 127
        '
        'GridColumn143
        '
        Me.GridColumn143.Caption = "Fecha Realizado"
        Me.GridColumn143.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss tt"
        Me.GridColumn143.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn143.FieldName = "FechaRealizacion"
        Me.GridColumn143.MinWidth = 21
        Me.GridColumn143.Name = "GridColumn143"
        Me.GridColumn143.OptionsColumn.AllowEdit = False
        Me.GridColumn143.OptionsColumn.AllowFocus = False
        Me.GridColumn143.Visible = True
        Me.GridColumn143.VisibleIndex = 6
        Me.GridColumn143.Width = 127
        '
        'GridColumn144
        '
        Me.GridColumn144.Caption = "Interpretación"
        Me.GridColumn144.FieldName = "Interpretacion"
        Me.GridColumn144.MinWidth = 21
        Me.GridColumn144.Name = "GridColumn144"
        Me.GridColumn144.OptionsColumn.AllowEdit = False
        Me.GridColumn144.OptionsColumn.AllowFocus = False
        Me.GridColumn144.Visible = True
        Me.GridColumn144.VisibleIndex = 7
        Me.GridColumn144.Width = 153
        '
        'GridColumn116
        '
        Me.GridColumn116.Caption = "Descripción Relacionada"
        Me.GridColumn116.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn116.MinWidth = 21
        Me.GridColumn116.Name = "GridColumn116"
        Me.GridColumn116.OptionsColumn.AllowEdit = False
        Me.GridColumn116.OptionsColumn.AllowFocus = False
        Me.GridColumn116.Width = 295
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Medico"
        Me.GridColumn14.FieldName = "Medico"
        Me.GridColumn14.MinWidth = 21
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        '
        'GridColumn145
        '
        Me.GridColumn145.Caption = "Observación"
        Me.GridColumn145.FieldName = "Observacion"
        Me.GridColumn145.MinWidth = 21
        Me.GridColumn145.Name = "GridColumn145"
        Me.GridColumn145.OptionsColumn.AllowEdit = False
        Me.GridColumn145.OptionsColumn.AllowFocus = False
        '
        'GridColumn146
        '
        Me.GridColumn146.Caption = "Medico Realizo"
        Me.GridColumn146.FieldName = "MedicoRealizo"
        Me.GridColumn146.MinWidth = 21
        Me.GridColumn146.Name = "GridColumn146"
        Me.GridColumn146.OptionsColumn.AllowEdit = False
        Me.GridColumn146.OptionsColumn.AllowFocus = False
        '
        'GridColumn147
        '
        Me.GridColumn147.Caption = "Unidad Funcional"
        Me.GridColumn147.FieldName = "UnidadFuncional"
        Me.GridColumn147.MinWidth = 21
        Me.GridColumn147.Name = "GridColumn147"
        Me.GridColumn147.OptionsColumn.AllowEdit = False
        Me.GridColumn147.OptionsColumn.AllowFocus = False
        '
        'GridColumn165
        '
        Me.GridColumn165.Caption = "Justificación"
        Me.GridColumn165.FieldName = "JustificationCodeName"
        Me.GridColumn165.MinWidth = 21
        Me.GridColumn165.Name = "GridColumn165"
        Me.GridColumn165.OptionsColumn.AllowEdit = False
        Me.GridColumn165.OptionsColumn.AllowFocus = False
        Me.GridColumn165.Visible = True
        Me.GridColumn165.VisibleIndex = 8
        '
        'GridColumn166
        '
        Me.GridColumn166.Caption = "Omitir Liquidacion"
        Me.GridColumn166.ColumnEdit = Me.INDRICESkipLiquidation
        Me.GridColumn166.FieldName = "SkipLiquidation"
        Me.GridColumn166.MinWidth = 21
        Me.GridColumn166.Name = "GridColumn166"
        Me.GridColumn166.OptionsColumn.AllowEdit = False
        Me.GridColumn166.OptionsColumn.AllowFocus = False
        Me.GridColumn166.Visible = True
        Me.GridColumn166.VisibleIndex = 9
        '
        'INDRICESkipLiquidation
        '
        Me.INDRICESkipLiquidation.AutoHeight = False
        Me.INDRICESkipLiquidation.Name = "INDRICESkipLiquidation"
        Me.INDRICESkipLiquidation.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDrptPceDetailLaboratories
        '
        Me.INDrptPceDetailLaboratories.AutoHeight = False
        Me.INDrptPceDetailLaboratories.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrptPceDetailLaboratories.CloseOnLostFocus = False
        Me.INDrptPceDetailLaboratories.CloseOnOuterMouseClick = False
        Me.INDrptPceDetailLaboratories.Name = "INDrptPceDetailLaboratories"
        Me.INDrptPceDetailLaboratories.PopupSizeable = False
        Me.INDrptPceDetailLaboratories.ShowPopupCloseButton = False
        Me.INDrptPceDetailLaboratories.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptLaboratoriesGenerated
        '
        Me.INDRptLaboratoriesGenerated.AutoHeight = False
        Me.INDRptLaboratoriesGenerated.HtmlImages = Me.ImageCollection1
        Me.INDRptLaboratoriesGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Por generar", False, 0)})
        Me.INDRptLaboratoriesGenerated.LargeImages = Me.ImageCollection1
        Me.INDRptLaboratoriesGenerated.Name = "INDRptLaboratoriesGenerated"
        Me.INDRptLaboratoriesGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptLaboratoriesGenerated.SmallImages = Me.ImageCollection1
        '
        'INDGcStays
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcStays, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcStays, Nothing)
        Me.INDGcStays.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcStays, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcStays, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcStays, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcStays, False)
        Me.INDGcStays.Location = New System.Drawing.Point(36, 146)
        Me.INDGcStays.MainView = Me.INDGvStays
        Me.INDGcStays.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcStays.MenuManager = Me.BarManager1
        Me.INDGcStays.Name = "INDGcStays"
        Me.INDGcStays.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptChkSelectStay, Me.INDRptImgGeneratedStay})
        Me.INDGcStays.Size = New System.Drawing.Size(1621, 377)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcStays, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcStays.TabIndex = 38
        Me.INDGcStays.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvStays})
        '
        'INDGvStays
        '
        Me.INDGvStays.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvStays.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvStays.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvStays.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvStays.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvStays.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvStays.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvStays.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvStays.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvStays.Appearance.Row.Options.UseFont = True
        Me.INDGvStays.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvStays.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvStays.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn197, Me.GridColumn119, Me.INDColFunctionalUnitStay, Me.GridColumn189, Me.GridColumn190, Me.GridColumn191, Me.GridColumn192, Me.GridColumn194, Me.GridColumn172, Me.GridColumn193, Me.GridColumn196, Me.GridColumn195})
        Me.INDGvStays.GridControl = Me.INDGcStays
        Me.INDGvStays.Name = "INDGvStays"
        Me.INDGvStays.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvStays.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvStays.OptionsView.ShowAutoFilterRow = True
        Me.INDGvStays.OptionsView.ShowDetailButtons = False
        Me.INDGvStays.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvStays, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvStays, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvStays, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvStays, False)
        Me.IndigoGridView6.SetTemaIndigoMetro(Me.INDGvStays, False)
        Me.IndigoGridView5.SetTemaIndigoMetro(Me.INDGvStays, False)
        Me.IndigoGridView8.SetTemaIndigoMetro(Me.INDGvStays, False)
        Me.IndigoGridViewStay.SetTemaIndigoMetro(Me.INDGvStays, False)
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvStays, False)
        '
        'GridColumn197
        '
        Me.GridColumn197.Caption = "Sel."
        Me.GridColumn197.ColumnEdit = Me.INDRptChkSelectStay
        Me.GridColumn197.FieldName = "Selected"
        Me.GridColumn197.MinWidth = 21
        Me.GridColumn197.Name = "GridColumn197"
        Me.GridColumn197.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn197.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn197.OptionsColumn.AllowMove = False
        Me.GridColumn197.OptionsColumn.AllowShowHide = False
        Me.GridColumn197.OptionsColumn.AllowSize = False
        Me.GridColumn197.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn197.OptionsColumn.FixedWidth = True
        Me.GridColumn197.Visible = True
        Me.GridColumn197.VisibleIndex = 0
        Me.GridColumn197.Width = 93
        '
        'INDRptChkSelectStay
        '
        Me.INDRptChkSelectStay.AutoHeight = False
        Me.INDRptChkSelectStay.Name = "INDRptChkSelectStay"
        '
        'GridColumn119
        '
        Me.GridColumn119.Caption = "Cama"
        Me.GridColumn119.FieldName = "Bed"
        Me.GridColumn119.MinWidth = 21
        Me.GridColumn119.Name = "GridColumn119"
        Me.GridColumn119.OptionsColumn.AllowEdit = False
        Me.GridColumn119.OptionsColumn.AllowFocus = False
        Me.GridColumn119.Visible = True
        Me.GridColumn119.VisibleIndex = 1
        Me.GridColumn119.Width = 63
        '
        'INDColFunctionalUnitStay
        '
        Me.INDColFunctionalUnitStay.Caption = "Unidad Funcional"
        Me.INDColFunctionalUnitStay.FieldName = "INDColFunctionalUnitStay"
        Me.INDColFunctionalUnitStay.MinWidth = 21
        Me.INDColFunctionalUnitStay.Name = "INDColFunctionalUnitStay"
        Me.INDColFunctionalUnitStay.OptionsColumn.AllowEdit = False
        Me.INDColFunctionalUnitStay.OptionsColumn.AllowFocus = False
        Me.INDColFunctionalUnitStay.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.INDColFunctionalUnitStay.Visible = True
        Me.INDColFunctionalUnitStay.VisibleIndex = 2
        Me.INDColFunctionalUnitStay.Width = 129
        '
        'GridColumn189
        '
        Me.GridColumn189.Caption = "F. Inicial"
        Me.GridColumn189.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
        Me.GridColumn189.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn189.FieldName = "InitialDate"
        Me.GridColumn189.MinWidth = 21
        Me.GridColumn189.Name = "GridColumn189"
        Me.GridColumn189.OptionsColumn.AllowEdit = False
        Me.GridColumn189.OptionsColumn.AllowFocus = False
        Me.GridColumn189.Visible = True
        Me.GridColumn189.VisibleIndex = 3
        Me.GridColumn189.Width = 69
        '
        'GridColumn190
        '
        Me.GridColumn190.Caption = "F. Final"
        Me.GridColumn190.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
        Me.GridColumn190.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn190.FieldName = "EndDate"
        Me.GridColumn190.MinWidth = 21
        Me.GridColumn190.Name = "GridColumn190"
        Me.GridColumn190.OptionsColumn.AllowEdit = False
        Me.GridColumn190.OptionsColumn.AllowFocus = False
        Me.GridColumn190.Visible = True
        Me.GridColumn190.VisibleIndex = 4
        Me.GridColumn190.Width = 69
        '
        'GridColumn191
        '
        Me.GridColumn191.Caption = "Tipo Estancia"
        Me.GridColumn191.FieldName = "StayTypeName"
        Me.GridColumn191.MinWidth = 21
        Me.GridColumn191.Name = "GridColumn191"
        Me.GridColumn191.OptionsColumn.AllowEdit = False
        Me.GridColumn191.OptionsColumn.AllowFocus = False
        Me.GridColumn191.Visible = True
        Me.GridColumn191.VisibleIndex = 5
        Me.GridColumn191.Width = 87
        '
        'GridColumn192
        '
        Me.GridColumn192.Caption = "Tiempo de Estancia"
        Me.GridColumn192.FieldName = "StayInBed"
        Me.GridColumn192.MinWidth = 21
        Me.GridColumn192.Name = "GridColumn192"
        Me.GridColumn192.OptionsColumn.AllowEdit = False
        Me.GridColumn192.OptionsColumn.AllowFocus = False
        Me.GridColumn192.Visible = True
        Me.GridColumn192.VisibleIndex = 6
        Me.GridColumn192.Width = 69
        '
        'GridColumn194
        '
        Me.GridColumn194.Caption = "Fecha de Liquidación"
        Me.GridColumn194.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
        Me.GridColumn194.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn194.FieldName = "LiquidationDate"
        Me.GridColumn194.MinWidth = 21
        Me.GridColumn194.Name = "GridColumn194"
        Me.GridColumn194.OptionsColumn.AllowEdit = False
        Me.GridColumn194.OptionsColumn.AllowFocus = False
        Me.GridColumn194.Visible = True
        Me.GridColumn194.VisibleIndex = 7
        Me.GridColumn194.Width = 99
        '
        'GridColumn172
        '
        Me.GridColumn172.Caption = "Justificación"
        Me.GridColumn172.FieldName = "JustificationCodeName"
        Me.GridColumn172.MinWidth = 21
        Me.GridColumn172.Name = "GridColumn172"
        Me.GridColumn172.OptionsColumn.AllowEdit = False
        Me.GridColumn172.OptionsColumn.AllowFocus = False
        Me.GridColumn172.Visible = True
        Me.GridColumn172.VisibleIndex = 8
        Me.GridColumn172.Width = 40
        '
        'GridColumn193
        '
        Me.GridColumn193.Caption = "Omitir Liquidacion"
        Me.GridColumn193.ColumnEdit = Me.INDRICESkipLiquidationV
        Me.GridColumn193.FieldName = "SkipLiquidation"
        Me.GridColumn193.MinWidth = 21
        Me.GridColumn193.Name = "GridColumn193"
        Me.GridColumn193.OptionsColumn.AllowEdit = False
        Me.GridColumn193.OptionsColumn.AllowFocus = False
        Me.GridColumn193.Visible = True
        Me.GridColumn193.VisibleIndex = 9
        Me.GridColumn193.Width = 40
        '
        'GridColumn196
        '
        Me.GridColumn196.Caption = "CUPS Liquidados"
        Me.GridColumn196.FieldName = "CUPsCodeName"
        Me.GridColumn196.MinWidth = 21
        Me.GridColumn196.Name = "GridColumn196"
        Me.GridColumn196.OptionsColumn.AllowEdit = False
        Me.GridColumn196.OptionsColumn.AllowFocus = False
        Me.GridColumn196.Visible = True
        Me.GridColumn196.VisibleIndex = 10
        Me.GridColumn196.Width = 93
        '
        'GridColumn195
        '
        Me.GridColumn195.Caption = "Descripción Relacionada"
        Me.GridColumn195.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn195.MinWidth = 21
        Me.GridColumn195.Name = "GridColumn195"
        Me.GridColumn195.OptionsColumn.AllowEdit = False
        Me.GridColumn195.OptionsColumn.AllowFocus = False
        Me.GridColumn195.Visible = True
        Me.GridColumn195.VisibleIndex = 11
        Me.GridColumn195.Width = 90
        '
        'INDRptImgGeneratedStay
        '
        Me.INDRptImgGeneratedStay.AutoHeight = False
        Me.INDRptImgGeneratedStay.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Generado", True, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No generado", False, 0)})
        Me.INDRptImgGeneratedStay.LargeImages = Me.ImageCollection1
        Me.INDRptImgGeneratedStay.Name = "INDRptImgGeneratedStay"
        Me.INDRptImgGeneratedStay.SmallImages = Me.ImageCollection1
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgRoot.CustomizationFormText = "INDlcgRoot"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData})
        Me.INDlcgRoot.Name = "Root"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1693, 559)
        Me.INDlcgRoot.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgMainData.CustomizationFormText = "Datos Básicos"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDtcgSources, Me.EmptySpaceItem1, Me.INDliGenerateServiceOrder, Me.INDliClear, Me.LayoutControlItem5, Me.INDliOperativeUnit, Me.EmptySpaceItem2, Me.LayoutControlItem34, Me.LciStateAdmission, Me.INDLciJustification})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(1673, 539)
        Me.INDlcgMainData.Text = "Datos Básicos"
        '
        'INDtcgSources
        '
        Me.INDtcgSources.CustomizationFormText = "TabbedControlGroup3"
        Me.INDtcgSources.Location = New System.Drawing.Point(0, 54)
        Me.INDtcgSources.Name = "INDtcgSources"
        Me.INDtcgSources.SelectedTabPage = Me.INDlcgConsultation
        Me.INDtcgSources.Size = New System.Drawing.Size(1649, 432)
        Me.INDtcgSources.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMedicinesSupplies, Me.INDLycgMixingStation, Me.INDlcgLaboratories, Me.INDlcgPathology, Me.INDlcgImagesDX, Me.INDlcgProceduresQX, Me.INDlcgProceduresNoQX, Me.INDlcgConsultation, Me.INDLcgHemo, Me.INDlcgTerapy, Me.INDlcgOxigenConsumer, Me.INDLcgStays, Me.INDlcgNurseProcedure, Me.INDlcgValoraciones})
        '
        'INDlcgConsultation
        '
        Me.INDlcgConsultation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgConsultation.AppearanceGroup.Options.UseFont = True
        Me.INDlcgConsultation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgConsultation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgConsultation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgConsultation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgConsultation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgConsultation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgConsultation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgConsultation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgConsultation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgConsultation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgConsultation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgConsultation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgConsultation.CustomizationFormText = "Interconsultas"
        Me.INDlcgConsultation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliInterSearch})
        Me.INDlcgConsultation.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgConsultation.Name = "INDlcgInterSearch"
        Me.INDlcgConsultation.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgConsultation.Text = "Interconsultas"
        '
        'INDliInterSearch
        '
        Me.INDliInterSearch.Control = Me.INDgcConsultation
        Me.INDliInterSearch.CustomizationFormText = "INDliInterSearch"
        Me.INDliInterSearch.Location = New System.Drawing.Point(0, 0)
        Me.INDliInterSearch.Name = "INDliInterSearch"
        Me.INDliInterSearch.Size = New System.Drawing.Size(1625, 381)
        Me.INDliInterSearch.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliInterSearch.TextVisible = False
        '
        'INDlcgMedicinesSupplies
        '
        Me.INDlcgMedicinesSupplies.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.INDlcgMedicinesSupplies.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMedicinesSupplies.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMedicinesSupplies.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgMedicinesSupplies.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgMedicinesSupplies.CustomizationFormText = "Medicamentos e Insumos"
        Me.INDlcgMedicinesSupplies.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliMedicamentosInsumos})
        Me.INDlcgMedicinesSupplies.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMedicinesSupplies.Name = "INDlcgMedicinesSupplies"
        Me.INDlcgMedicinesSupplies.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgMedicinesSupplies.Text = "Medicamentos e Insumos"
        '
        'INDliMedicamentosInsumos
        '
        Me.INDliMedicamentosInsumos.Control = Me.INDgcMedicineSupplier
        Me.INDliMedicamentosInsumos.CustomizationFormText = "INDliMedicamentosInsumos"
        Me.INDliMedicamentosInsumos.Location = New System.Drawing.Point(0, 0)
        Me.INDliMedicamentosInsumos.Name = "INDliMedicamentosInsumos"
        Me.INDliMedicamentosInsumos.Size = New System.Drawing.Size(1625, 381)
        Me.INDliMedicamentosInsumos.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliMedicamentosInsumos.TextVisible = False
        '
        'INDLycgMixingStation
        '
        Me.INDLycgMixingStation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.INDLycgMixingStation.AppearanceGroup.Options.UseFont = True
        Me.INDLycgMixingStation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLycgMixingStation.AppearanceItemCaption.Options.UseFont = True
        Me.INDLycgMixingStation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLycgMixingStation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLycgMixingStation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLycgMixingStation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLycgMixingStation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLycgMixingStation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLycgMixingStation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLycgMixingStation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLycgMixingStation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLycgMixingStation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLycgMixingStation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlycMixingStation})
        Me.INDLycgMixingStation.Location = New System.Drawing.Point(0, 0)
        Me.INDLycgMixingStation.Name = "INDLycgMixingStation"
        Me.INDLycgMixingStation.Size = New System.Drawing.Size(1625, 381)
        Me.INDLycgMixingStation.Text = "Central de Mezclas"
        '
        'INDlycMixingStation
        '
        Me.INDlycMixingStation.Control = Me.INDGcMixingStation
        Me.INDlycMixingStation.Location = New System.Drawing.Point(0, 0)
        Me.INDlycMixingStation.Name = "INDlycMixingStation"
        Me.INDlycMixingStation.Size = New System.Drawing.Size(1625, 381)
        Me.INDlycMixingStation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlycMixingStation.TextVisible = False
        '
        'INDlcgLaboratories
        '
        Me.INDlcgLaboratories.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgLaboratories.AppearanceGroup.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgLaboratories.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgLaboratories.CustomizationFormText = "Laboratorios"
        Me.INDlcgLaboratories.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliLaboratories})
        Me.INDlcgLaboratories.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgLaboratories.Name = "INDlcgLaboratories"
        Me.INDlcgLaboratories.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgLaboratories.Text = "Laboratorios"
        '
        'INDliLaboratories
        '
        Me.INDliLaboratories.Control = Me.INDgcLaboratories
        Me.INDliLaboratories.CustomizationFormText = "INDliLaboratories"
        Me.INDliLaboratories.Location = New System.Drawing.Point(0, 0)
        Me.INDliLaboratories.Name = "INDliLaboratories"
        Me.INDliLaboratories.Size = New System.Drawing.Size(1625, 381)
        Me.INDliLaboratories.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliLaboratories.TextVisible = False
        '
        'INDlcgPathology
        '
        Me.INDlcgPathology.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPathology.AppearanceGroup.Options.UseFont = True
        Me.INDlcgPathology.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPathology.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgPathology.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgPathology.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgPathology.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgPathology.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgPathology.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgPathology.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgPathology.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgPathology.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgPathology.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgPathology.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgPathology.CustomizationFormText = "Patología"
        Me.INDlcgPathology.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliIPathology})
        Me.INDlcgPathology.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgPathology.Name = "INDlcgPathology"
        Me.INDlcgPathology.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgPathology.Text = "Patología"
        '
        'INDliIPathology
        '
        Me.INDliIPathology.Control = Me.INDgcPathology
        Me.INDliIPathology.CustomizationFormText = "INDliIPathology"
        Me.INDliIPathology.Location = New System.Drawing.Point(0, 0)
        Me.INDliIPathology.Name = "INDliIPathology"
        Me.INDliIPathology.Size = New System.Drawing.Size(1625, 381)
        Me.INDliIPathology.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliIPathology.TextVisible = False
        '
        'INDlcgImagesDX
        '
        Me.INDlcgImagesDX.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgImagesDX.AppearanceGroup.Options.UseFont = True
        Me.INDlcgImagesDX.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgImagesDX.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgImagesDX.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgImagesDX.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgImagesDX.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgImagesDX.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgImagesDX.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgImagesDX.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgImagesDX.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgImagesDX.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgImagesDX.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgImagesDX.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgImagesDX.CustomizationFormText = "Imágenes DX"
        Me.INDlcgImagesDX.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliImagesDx})
        Me.INDlcgImagesDX.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgImagesDX.Name = "INDlcgImagesDX"
        Me.INDlcgImagesDX.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgImagesDX.Text = "Imágenes DX"
        '
        'INDliImagesDx
        '
        Me.INDliImagesDx.Control = Me.INDgcImagesDx
        Me.INDliImagesDx.CustomizationFormText = "INDliImagesDx"
        Me.INDliImagesDx.Location = New System.Drawing.Point(0, 0)
        Me.INDliImagesDx.Name = "INDliImagesDx"
        Me.INDliImagesDx.Size = New System.Drawing.Size(1625, 381)
        Me.INDliImagesDx.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliImagesDx.TextVisible = False
        '
        'INDlcgProceduresQX
        '
        Me.INDlcgProceduresQX.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgProceduresQX.AppearanceGroup.Options.UseFont = True
        Me.INDlcgProceduresQX.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgProceduresQX.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgProceduresQX.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgProceduresQX.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgProceduresQX.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgProceduresQX.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgProceduresQX.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgProceduresQX.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgProceduresQX.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgProceduresQX.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgProceduresQX.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgProceduresQX.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgProceduresQX.CustomizationFormText = "Procedimientos QX"
        Me.INDlcgProceduresQX.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliProceduresQX})
        Me.INDlcgProceduresQX.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgProceduresQX.Name = "INDlcgProceduresQX"
        Me.INDlcgProceduresQX.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgProceduresQX.Text = "Procedimientos QX"
        '
        'INDliProceduresQX
        '
        Me.INDliProceduresQX.Control = Me.INDgcProceduresQx
        Me.INDliProceduresQX.CustomizationFormText = "INDliProceduresQX"
        Me.INDliProceduresQX.Location = New System.Drawing.Point(0, 0)
        Me.INDliProceduresQX.Name = "INDliProceduresQX"
        Me.INDliProceduresQX.Size = New System.Drawing.Size(1625, 381)
        Me.INDliProceduresQX.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliProceduresQX.TextVisible = False
        '
        'INDlcgProceduresNoQX
        '
        Me.INDlcgProceduresNoQX.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgProceduresNoQX.AppearanceGroup.Options.UseFont = True
        Me.INDlcgProceduresNoQX.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgProceduresNoQX.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgProceduresNoQX.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgProceduresNoQX.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgProceduresNoQX.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgProceduresNoQX.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgProceduresNoQX.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgProceduresNoQX.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgProceduresNoQX.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgProceduresNoQX.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgProceduresNoQX.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgProceduresNoQX.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgProceduresNoQX.CustomizationFormText = "Procedimientos noQX"
        Me.INDlcgProceduresNoQX.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliProceduresNoQX})
        Me.INDlcgProceduresNoQX.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgProceduresNoQX.Name = "INDlcgProceduresNoQX"
        Me.INDlcgProceduresNoQX.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgProceduresNoQX.Text = "Procedimientos noQX"
        '
        'INDliProceduresNoQX
        '
        Me.INDliProceduresNoQX.Control = Me.INDgcProceduresNoQX
        Me.INDliProceduresNoQX.CustomizationFormText = "INDliProceduresNoQX"
        Me.INDliProceduresNoQX.Location = New System.Drawing.Point(0, 0)
        Me.INDliProceduresNoQX.Name = "INDliProceduresNoQX"
        Me.INDliProceduresNoQX.Size = New System.Drawing.Size(1625, 381)
        Me.INDliProceduresNoQX.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliProceduresNoQX.TextVisible = False
        '
        'INDLcgHemo
        '
        Me.INDLcgHemo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.INDLcgHemo.AppearanceGroup.Options.UseFont = True
        Me.INDLcgHemo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgHemo.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgHemo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgHemo.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgHemo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgHemo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgHemo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgHemo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgHemo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgHemo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgHemo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgHemo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgHemo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciHemoDetail})
        Me.INDLcgHemo.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgHemo.Name = "INDLcgHemo"
        Me.INDLcgHemo.Size = New System.Drawing.Size(1625, 381)
        Me.INDLcgHemo.Text = "Hemocomponentes"
        '
        'INDLciHemoDetail
        '
        Me.INDLciHemoDetail.Control = Me.INDGcHemoDetail
        Me.INDLciHemoDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDLciHemoDetail.Name = "INDLciHemoDetail"
        Me.INDLciHemoDetail.Size = New System.Drawing.Size(1625, 381)
        Me.INDLciHemoDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciHemoDetail.TextVisible = False
        '
        'INDlcgTerapy
        '
        Me.INDlcgTerapy.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgTerapy.AppearanceGroup.Options.UseFont = True
        Me.INDlcgTerapy.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgTerapy.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgTerapy.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgTerapy.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgTerapy.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgTerapy.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgTerapy.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgTerapy.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgTerapy.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgTerapy.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgTerapy.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgTerapy.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgTerapy.CustomizationFormText = "Terapias"
        Me.INDlcgTerapy.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliTerapy})
        Me.INDlcgTerapy.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgTerapy.Name = "INDlcgTerapy"
        Me.INDlcgTerapy.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgTerapy.Text = "Terapias"
        '
        'INDliTerapy
        '
        Me.INDliTerapy.Control = Me.INDgcTerapy
        Me.INDliTerapy.CustomizationFormText = "INDliTerapy"
        Me.INDliTerapy.Location = New System.Drawing.Point(0, 0)
        Me.INDliTerapy.Name = "INDliTerapy"
        Me.INDliTerapy.Size = New System.Drawing.Size(1625, 381)
        Me.INDliTerapy.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliTerapy.TextVisible = False
        '
        'INDlcgOxigenConsumer
        '
        Me.INDlcgOxigenConsumer.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOxigenConsumer.AppearanceGroup.Options.UseFont = True
        Me.INDlcgOxigenConsumer.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOxigenConsumer.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgOxigenConsumer.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgOxigenConsumer.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgOxigenConsumer.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgOxigenConsumer.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgOxigenConsumer.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgOxigenConsumer.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgOxigenConsumer.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgOxigenConsumer.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgOxigenConsumer.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgOxigenConsumer.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgOxigenConsumer.CustomizationFormText = "Gases Medicinales"
        Me.INDlcgOxigenConsumer.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliOxigenConsumer})
        Me.INDlcgOxigenConsumer.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgOxigenConsumer.Name = "INDlcgOxigenConsumer"
        Me.INDlcgOxigenConsumer.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgOxigenConsumer.Text = "Gases Medicinales"
        '
        'INDliOxigenConsumer
        '
        Me.INDliOxigenConsumer.Control = Me.INDgcOxygenConsumption
        Me.INDliOxigenConsumer.CustomizationFormText = "INDliOxigenConsumer"
        Me.INDliOxigenConsumer.Location = New System.Drawing.Point(0, 0)
        Me.INDliOxigenConsumer.Name = "INDliOxigenConsumer"
        Me.INDliOxigenConsumer.Size = New System.Drawing.Size(1625, 381)
        Me.INDliOxigenConsumer.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliOxigenConsumer.TextVisible = False
        '
        'INDLcgStays
        '
        Me.INDLcgStays.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgStays.AppearanceGroup.Options.UseFont = True
        Me.INDLcgStays.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgStays.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgStays.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgStays.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgStays.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgStays.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgStays.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgStays.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgStays.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgStays.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgStays.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLcgStays.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgStays.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem52})
        Me.INDLcgStays.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgStays.Name = "INDLcgStays"
        Me.INDLcgStays.Size = New System.Drawing.Size(1625, 381)
        Me.INDLcgStays.Text = "Estancia"
        '
        'LayoutControlItem52
        '
        Me.LayoutControlItem52.Control = Me.INDGcStays
        Me.LayoutControlItem52.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem52.Name = "LayoutControlItem52"
        Me.LayoutControlItem52.Size = New System.Drawing.Size(1625, 381)
        Me.LayoutControlItem52.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem52.TextVisible = False
        '
        'INDlcgNurseProcedure
        '
        Me.INDlcgNurseProcedure.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgNurseProcedure.AppearanceGroup.Options.UseFont = True
        Me.INDlcgNurseProcedure.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgNurseProcedure.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgNurseProcedure.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgNurseProcedure.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgNurseProcedure.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgNurseProcedure.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgNurseProcedure.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgNurseProcedure.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgNurseProcedure.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgNurseProcedure.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgNurseProcedure.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgNurseProcedure.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgNurseProcedure.CustomizationFormText = "Procedimientos de Enfermería"
        Me.INDlcgNurseProcedure.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliNurseProcedure})
        Me.INDlcgNurseProcedure.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgNurseProcedure.Name = "INDlcgNurseProcedure"
        Me.INDlcgNurseProcedure.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgNurseProcedure.Text = "Procedimientos de Enfermería"
        '
        'INDliNurseProcedure
        '
        Me.INDliNurseProcedure.Control = Me.INDgcNurseProcedure
        Me.INDliNurseProcedure.CustomizationFormText = "INDliNurseProcedure"
        Me.INDliNurseProcedure.Location = New System.Drawing.Point(0, 0)
        Me.INDliNurseProcedure.Name = "INDliNurseProcedure"
        Me.INDliNurseProcedure.Size = New System.Drawing.Size(1625, 381)
        Me.INDliNurseProcedure.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliNurseProcedure.TextVisible = False
        '
        'INDlcgValoraciones
        '
        Me.INDlcgValoraciones.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgValoraciones.AppearanceGroup.Options.UseFont = True
        Me.INDlcgValoraciones.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgValoraciones.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgValoraciones.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgValoraciones.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgValoraciones.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlcgValoraciones.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgValoraciones.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgValoraciones.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgValoraciones.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgValoraciones.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgValoraciones.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlcgValoraciones.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlcgValoraciones.CustomizationFormText = "Valoraciones"
        Me.INDlcgValoraciones.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliValoraciones})
        Me.INDlcgValoraciones.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgValoraciones.Name = "INDlcgValoraciones"
        Me.INDlcgValoraciones.Size = New System.Drawing.Size(1625, 381)
        Me.INDlcgValoraciones.Text = "Valoraciones"
        '
        'INDliValoraciones
        '
        Me.INDliValoraciones.Control = Me.INDgcValoraciones
        Me.INDliValoraciones.CustomizationFormText = "INDliValoraciones"
        Me.INDliValoraciones.Location = New System.Drawing.Point(0, 0)
        Me.INDliValoraciones.Name = "INDliValoraciones"
        Me.INDliValoraciones.Size = New System.Drawing.Size(1625, 381)
        Me.INDliValoraciones.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliValoraciones.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 40)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(141, 14)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(141, 14)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(1649, 14)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDliGenerateServiceOrder
        '
        Me.INDliGenerateServiceOrder.Control = Me.INDsbGererateServiceOrder
        Me.INDliGenerateServiceOrder.CustomizationFormText = "Cambiar Contrato/Plan"
        Me.INDliGenerateServiceOrder.Location = New System.Drawing.Point(907, 0)
        Me.INDliGenerateServiceOrder.MaxSize = New System.Drawing.Size(124, 32)
        Me.INDliGenerateServiceOrder.MinSize = New System.Drawing.Size(124, 32)
        Me.INDliGenerateServiceOrder.Name = "INDliGenerateServiceOrder"
        Me.INDliGenerateServiceOrder.Size = New System.Drawing.Size(124, 40)
        Me.INDliGenerateServiceOrder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliGenerateServiceOrder.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliGenerateServiceOrder.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliGenerateServiceOrder.TextToControlDistance = 0
        Me.INDliGenerateServiceOrder.TextVisible = False
        '
        'INDliClear
        '
        Me.INDliClear.Control = Me.INDsbClear
        Me.INDliClear.CustomizationFormText = "Limpiar"
        Me.INDliClear.Location = New System.Drawing.Point(1155, 0)
        Me.INDliClear.MaxSize = New System.Drawing.Size(124, 32)
        Me.INDliClear.MinSize = New System.Drawing.Size(124, 32)
        Me.INDliClear.Name = "INDliClear"
        Me.INDliClear.Size = New System.Drawing.Size(124, 40)
        Me.INDliClear.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliClear.Text = "Limpiar"
        Me.INDliClear.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliClear.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliClear.TextToControlDistance = 0
        Me.INDliClear.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.DdbMenu
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(862, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(45, 32)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(45, 32)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(45, 40)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'INDliOperativeUnit
        '
        Me.INDliOperativeUnit.Control = Me.SleOperatingUnit
        Me.INDliOperativeUnit.CustomizationFormText = "INDliOperativeUnit"
        Me.INDliOperativeUnit.Location = New System.Drawing.Point(1469, 0)
        Me.INDliOperativeUnit.MaxSize = New System.Drawing.Size(180, 28)
        Me.INDliOperativeUnit.MinSize = New System.Drawing.Size(180, 28)
        Me.INDliOperativeUnit.Name = "INDliOperativeUnit"
        Me.INDliOperativeUnit.Size = New System.Drawing.Size(180, 40)
        Me.INDliOperativeUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliOperativeUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliOperativeUnit.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliOperativeUnit.TextToControlDistance = 0
        Me.INDliOperativeUnit.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(1279, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(10, 40)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem34
        '
        Me.LayoutControlItem34.Control = Me.INDSleAdmissionNumber2
        Me.LayoutControlItem34.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem34.MaxSize = New System.Drawing.Size(862, 40)
        Me.LayoutControlItem34.MinSize = New System.Drawing.Size(862, 40)
        Me.LayoutControlItem34.Name = "LayoutControlItem34"
        Me.LayoutControlItem34.Size = New System.Drawing.Size(862, 40)
        Me.LayoutControlItem34.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem34.Text = "Ingreso"
        Me.LayoutControlItem34.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem34.TextSize = New System.Drawing.Size(141, 21)
        Me.LayoutControlItem34.TextToControlDistance = 5
        '
        'LciStateAdmission
        '
        Me.LciStateAdmission.Control = Me.LblState
        Me.LciStateAdmission.Location = New System.Drawing.Point(1289, 0)
        Me.LciStateAdmission.MaxSize = New System.Drawing.Size(180, 26)
        Me.LciStateAdmission.MinSize = New System.Drawing.Size(180, 26)
        Me.LciStateAdmission.Name = "LciStateAdmission"
        Me.LciStateAdmission.Size = New System.Drawing.Size(180, 40)
        Me.LciStateAdmission.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciStateAdmission.Text = "Estado"
        Me.LciStateAdmission.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LciStateAdmission.TextSize = New System.Drawing.Size(0, 0)
        Me.LciStateAdmission.TextToControlDistance = 0
        Me.LciStateAdmission.TextVisible = False
        '
        'INDLciJustification
        '
        Me.INDLciJustification.Control = Me.INDSbJustification
        Me.INDLciJustification.CustomizationFormText = "Justificación"
        Me.INDLciJustification.Location = New System.Drawing.Point(1031, 0)
        Me.INDLciJustification.MaxSize = New System.Drawing.Size(124, 32)
        Me.INDLciJustification.MinSize = New System.Drawing.Size(124, 32)
        Me.INDLciJustification.Name = "INDLciJustification"
        Me.INDLciJustification.Size = New System.Drawing.Size(124, 40)
        Me.INDLciJustification.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciJustification.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciJustification.TextVisible = False
        Me.INDLciJustification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'BarManager2
        '
        Me.BarManager2.DockControls.Add(Me.BarDockControl1)
        Me.BarManager2.DockControls.Add(Me.BarDockControl2)
        Me.BarManager2.DockControls.Add(Me.BarDockControl3)
        Me.BarManager2.DockControls.Add(Me.BarDockControl4)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDbbiOpenRequest})
        Me.BarManager2.MaxItemId = 2
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 6)
        Me.BarDockControl1.Manager = Me.BarManager2
        Me.BarDockControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarDockControl1.Size = New System.Drawing.Size(1685, 0)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 722)
        Me.BarDockControl2.Manager = Me.BarManager2
        Me.BarDockControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarDockControl2.Size = New System.Drawing.Size(1685, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 6)
        Me.BarDockControl3.Manager = Me.BarManager2
        Me.BarDockControl3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 716)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1685, 6)
        Me.BarDockControl4.Manager = Me.BarManager2
        Me.BarDockControl4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 716)
        '
        'INDbbiOpenRequest
        '
        Me.INDbbiOpenRequest.Caption = "Control Autorización"
        Me.INDbbiOpenRequest.Id = 0
        Me.INDbbiOpenRequest.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.MoreInfo_16x16_blue
        Me.INDbbiOpenRequest.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDbbiOpenRequest.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDbbiOpenRequest.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDbbiOpenRequest.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDbbiOpenRequest.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDbbiOpenRequest.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDbbiOpenRequest.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDbbiOpenRequest.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDbbiOpenRequest.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDbbiOpenRequest.Name = "INDbbiOpenRequest"
        '
        'PopupMenu2
        '
        Me.PopupMenu2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbiOpenRequest)})
        Me.PopupMenu2.Manager = Me.BarManager2
        Me.PopupMenu2.Name = "PopupMenu2"
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        Me.IndigoGridView2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView3
        '
        Me.IndigoGridView3.RaiseMenuPopUp = True
        Me.IndigoGridView3.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView4
        '
        Me.IndigoGridView4.RaiseMenuPopUp = True
        Me.IndigoGridView4.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView5
        '
        Me.IndigoGridView5.RaiseMenuPopUp = True
        Me.IndigoGridView5.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView6
        '
        Me.IndigoGridView6.RaiseMenuPopUp = True
        Me.IndigoGridView6.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView7
        '
        Me.IndigoGridView7.RaiseMenuPopUp = True
        Me.IndigoGridView7.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView8
        '
        Me.IndigoGridView8.RaiseMenuPopUp = True
        Me.IndigoGridView8.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridViewStay
        '
        Me.IndigoGridViewStay.RaiseMenuPopUp = True
        Me.IndigoGridViewStay.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'SvgImageCollection1
        '
        Me.SvgImageCollection1.Add("Chulo Azul", CType(resources.GetObject("SvgImageCollection1.Chulo Azul"), DevExpress.Utils.Svg.SvgImage))
        '
        'INDRptInterconsultasValidated
        '
        Me.INDRptInterconsultasValidated.AutoHeight = False
        Me.INDRptInterconsultasValidated.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptInterconsultasValidated.HtmlImages = Me.SvgImageCollection1
        Me.INDRptInterconsultasValidated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Validada", True, 0)})
        Me.INDRptInterconsultasValidated.LargeImages = Me.SvgImageCollection1
        Me.INDRptInterconsultasValidated.Name = "INDRptInterconsultasValidated"
        Me.INDRptInterconsultasValidated.SmallImages = Me.SvgImageCollection1
        '
        'FrmAccountControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1685, 722)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmAccountControl"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "776"
        Me.Text = "Control de Cuentas Hospitalario"
        Me.ViewModeEditHold = True
        Me.Controls.SetChildIndex(Me.BarDockControl1, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl2, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl4, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl3, 0)
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvKardex, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcMedicineSupplier, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvManualMovements, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvMedicineSupplier, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvNursingProceduresDetail, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcNurseProcedure, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvNursingProcedures, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkNursingProcedures, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationE, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptPceNursingProcedures, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptNursingGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvPharmaDoseMSDetail, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRIselector, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGcMixingStation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvMixingStation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptStatus, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptMixingStationGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvSurgeriesPerformed, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESelected, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationQx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcProceduresQx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvProceduresQx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptPceDetailProceduresQx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptImgGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptPceHemo, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDPccHemo, System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPccHemo.ResumeLayout(False)
        CType(Me.INDLcHemo, System.ComponentModel.ISupportInitialize).EndInit
        Me.INDLcHemo.ResumeLayout(False)
        CType(Me.INDteComponente.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteNumeroUnidad.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteSelloCalidad.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrgPruebaCru.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrgRastreoAnti.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteBacEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteMedReaSolicitudReserva.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteMedReaSolicitudTransfusion.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteMedRealizoRegTransfusión.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteEnfRealizoRegistroAplicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteFechaEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteFechaExpiracion.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteFechaAplicacionMed.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTeDateEndTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDteFechaAplicacionEnf.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTeDateIniTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem35, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem36, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem37, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem38, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem40, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem41, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem42, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem43, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem44, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem45, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem46, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem47, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem48, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem49, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem50, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem51, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkHemo, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDpccServicesProcedures, System.ComponentModel.ISupportInitialize).EndInit
        Me.INDpccServicesProcedures.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDTxtQxTime.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTxtTipoAnestesia.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTxtMuestraPatologicas.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGcQxEquipe, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvQxEquipe, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGcProceduresRealizados, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvProceduresRealizados, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcDetailServiciosNoRealizados, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvDetailServiciosNoRealizados, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcDetailServiciosRealizados, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvDetailServiciosRealizados, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkSeleccioneServiceProcedure, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptMeInterpretation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptSearchMedicoRealizo, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemImageComboBox2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrpticeGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTxtObservaciones.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTxtProcedimientosRealizados.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDMeHallazgosOperatorios.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDtcgServicesProcedures, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDtpServiciosRealizados, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDtpServiciosNoRealizados, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TabServices, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TabProfessional, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TabOtherData, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem33, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem30, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem28, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LciObservations, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem32, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGcHemoDetail, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvHemoDetail, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationH, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptHemoGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PccAdmission, System.ComponentModel.ISupportInitialize).EndInit
        Me.PccAdmission.ResumeLayout(False)
        CType(Me.LycPccAdmission, System.ComponentModel.ISupportInitialize).EndInit
        Me.LycPccAdmission.ResumeLayout(False)
        CType(Me.INDdeOut.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDtxtFunctionalUnitOut.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtRiskType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtCareGroupAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtContact.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientEstrato.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientEntityName.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtCareGroupPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientAge.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientBirth.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientName.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientCode.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtBedStay.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtEntityNameAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAtentionCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtBenefitPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPlaceEntry.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtFunctionalUnitAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAfiliationType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgPccAdmission, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgAdmissionGroup, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LiCareGroup, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LiEntity, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtContacto, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.viewSearchAdmission, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SleOperatingUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcValoraciones, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvValorations, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptChkValoration, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrpticeGeneratedValoraciones, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationV, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptPceDetailValoration, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptImgGenerado, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcOxygenConsumption, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvOxygen, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkOrygenConsumpsion, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationJ, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptImeOxygen, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcTerapy, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvTherapy, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkTherapy, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationT, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptPceDetailTherapy, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptTherapyGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcConsultation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvConsultation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkInterSearch, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationC, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptPceDetailConsultation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptIntercosultasGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcProceduresNoQX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvProceduresNoQx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkProceduresNoQx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptSleContractDescriptionProceduresNoQx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationNQX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptPceDetailProceduresNoQx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptNoQxGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcImagesDx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvImagesDX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkImagesDx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationDx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptDetailImagesDX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptImagesGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcPathology, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvPathologies, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkPathologies, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidationP, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptPceDetailPathologies, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptPathologiesGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgcLaboratories, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvLaboratories, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptChkLaboratories, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRICESkipLiquidation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDrptPceDetailLaboratories, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptLaboratoriesGenerated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGcStays, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvStays, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptChkSelectStay, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptImgGeneratedStay, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDtcgSources, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgConsultation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliInterSearch, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgMedicinesSupplies, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliMedicamentosInsumos, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLycgMixingStation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlycMixingStation, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgLaboratories, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliLaboratories, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgPathology, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliIPathology, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgImagesDX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliImagesDx, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgProceduresQX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliProceduresQX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgProceduresNoQX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliProceduresNoQX, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcgHemo, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciHemoDetail, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgTerapy, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliTerapy, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgOxigenConsumer, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliOxigenConsumer, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcgStays, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem52, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgNurseProcedure, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliNurseProcedure, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgValoraciones, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliValoraciones, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliGenerateServiceOrder, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliClear, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDliOperativeUnit, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem34, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LciStateAdmission, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciJustification, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PopupMenu2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView7, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView8, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridViewStay, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SvgImageCollection1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptInterconsultasValidated, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(False)
        Me.PerformLayout

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents PccAdmission As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LycPccAdmission As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents TxtRiskType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtCareGroupAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtContact As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtPatientEstrato As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientEntityName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtCareGroupPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientAge As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientBirth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtBedStay As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtResponsiblePhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtResponsibleName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtEntityNameAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAtentionCenter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtBenefitPlan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPlaceEntry As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtFunctionalUnitAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAdmissionDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAdmissionCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtAfiliationType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtAdmissionType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtLiquidationType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents LycgPccAdmission As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem25 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LiCareGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem29 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem26 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LiEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TxtContacto As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LycgAdmissionGroup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem27 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem39 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcProceduresNoQX As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvProceduresNoQx As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcProceduresQx As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvProceduresQx As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcImagesDx As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvImagesDX As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcPathology As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvPathologies As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcLaboratories As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvLaboratories As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcMedicineSupplier As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvMedicineSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtcgSources As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlcgMedicinesSupplies As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliMedicamentosInsumos As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgLaboratories As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliLaboratories As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgPathology As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliIPathology As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgImagesDX As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliImagesDx As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgProceduresQX As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliProceduresQX As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgProceduresNoQX As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliProceduresNoQX As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcValoraciones As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvValorations As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcNurseProcedure As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvNursingProcedures As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcOxygenConsumption As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvOxygen As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcTerapy As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvTherapy As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcConsultation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvConsultation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgConsultation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliInterSearch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgTerapy As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliTerapy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgOxigenConsumer As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliOxigenConsumer As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgNurseProcedure As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliNurseProcedure As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgValoraciones As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliValoraciones As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbGererateServiceOrder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDliGenerateServiceOrder As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDliClear As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvKardex As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ColSeleccioneLaboratorio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptPceDetailLaboratories As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpccServicesProcedures As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcDetailServiciosRealizados As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDetailServiciosRealizados As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDcolSeleccioneLab As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColMedico As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColObservacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColUnidadFuncional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColInterpretacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCantidad As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcDetailServiciosNoRealizados As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDetailServiciosNoRealizados As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtcgServicesProcedures As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDtpServiciosRealizados As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtpServiciosNoRealizados As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFecha As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFolio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColMedicoNoRelazados As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSelPathologies As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptPceDetailPathologies As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSelImagesDx As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptDetailImagesDX As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptPceDetailProceduresQx As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDrptChkLaboratories As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDrptChkSeleccioneServiceProcedure As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDdeOut As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtFunctionalUnitOut As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DdbMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SleOperatingUnit As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliOperativeUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptChkImagesDx As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrptChkPathologies As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSelNoQx As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptChkProceduresNoQx As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptPceDetailProceduresNoQx As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSelInterconsulta As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptChkInterSearch As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrptPceDetailConsultation As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDrptMeInterpretation As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents ColSelTerapy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn62 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn63 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn64 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptChkTherapy As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrptPceDetailTherapy As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn60 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn66 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn67 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn68 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn69 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn70 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn72 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn73 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSelNursingProcedures As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn77 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn79 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptChkNursingProcedures As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDgvNursingProceduresDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn80 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn81 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn82 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptPceNursingProcedures As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDgcRegisterDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn84 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn85 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn86 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrpticeGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents ColSelOxygen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptChkOrygenConsumpsion As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn89 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrptImeOxygen As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoCheckEdit1 As Presentation.Controls.IndigoCheckEdit
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcProceduresRealizados As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvProceduresRealizados As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TabServices As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TabProfessional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TabOtherData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcQxEquipe As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvQxEquipe As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCodigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTxtMuestraPatologicas As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtQxTime As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtTipoAnestesia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem28 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem30 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem32 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem33 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtObservaciones As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDTxtProcedimientosRealizados As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDMeHallazgosOperatorios As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LblState As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LciStateAdmission As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents MBtnOpenAdmission As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MBtnCloseAdmission As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents GridColumn88 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn95 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleAdmissionNumber2 As SearchLookUpEditExAdmission
    Friend WithEvents viewSearchAdmission As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem34 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colMedicoRealizo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptSearchMedicoRealizo As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFechaRealizacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox2 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn99 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn100 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn101 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents ColSeleccioneValoraciones As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptChkValoration As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDRptPceDetailValoration As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDgcFolioDoneVal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcMDDone As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn105 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrpticeGeneratedValoraciones As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDRptImgGenerado As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDPccHemo As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcHemo As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDteComponente As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteNumeroUnidad As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteSelloCalidad As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDrgPruebaCru As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDrgRastreoAnti As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDteBacEntrega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteMedReaSolicitudReserva As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteMedReaSolicitudTransfusion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteMedRealizoRegTransfusión As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteEnfRealizoRegistroAplicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteFechaEntrega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteFechaExpiracion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteFechaAplicacionMed As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeDateEndTrans As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteFechaAplicacionEnf As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeDateIniTrans As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem35 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem36 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem37 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem38 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem40 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem41 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem42 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem43 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem44 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem45 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem46 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem47 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem48 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem49 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem50 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem51 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcHemoDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvHemoDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDrptChkHemo As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrptPceHemo As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn106 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSelHemo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn107 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn108 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn109 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn110 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgHemo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciHemoDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckEdit2 As IndigoCheckEdit
    Friend WithEvents GridColumn112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn114 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn115 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn116 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn118 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColContractDescriptionNoQx As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn121 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn122 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDbbiOpenRequest As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu2 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDrptSleContractDescriptionProceduresNoQx As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn120 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn123 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLycgMixingStation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGcMixingStation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvPharmaDoseMSDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDRptStatus As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDGvMixingStation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDeliveryQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColApplyQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCurrentQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlycMixingStation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn124 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn125 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn126 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn127 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn128 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn129 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSelected As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRIselector As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn91 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn130 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn131 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn132 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn133 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn134 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn135 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn136 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcFolioRequest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn93 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn137 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn138 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn139 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn140 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn141 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn142 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView2 As IndigoGridView
    Friend WithEvents GridColumn59 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn87 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn143 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn144 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn145 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn146 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn147 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView3 As IndigoGridView
    Friend WithEvents IndigoGridView4 As IndigoGridView
    Friend WithEvents GridColumn90 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn117 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn148 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn149 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn151 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn150 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn152 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn153 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView5 As IndigoGridView
    Friend WithEvents GridColumn83 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn96 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn154 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn155 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn156 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn65 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn94 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn157 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn158 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn159 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn160 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn161 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView6 As IndigoGridView
    Friend WithEvents LayoutControlItem31 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView7 As IndigoGridView
    Friend WithEvents GridColumn92 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn97 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn102 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn162 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn163 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn164 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView8 As IndigoGridView
    Friend WithEvents INDRptLaboratoriesGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptPathologiesGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptHemoGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptImagesGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptNoQxGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptIntercosultasGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptTherapyGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptNursingGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptMixingStationGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn165 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn166 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidation As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDSbJustification As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciJustification As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn177 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn178 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationH As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn175 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn176 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationC As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn173 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn174 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationNQX As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDRICESkipLiquidationQx As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn169 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn170 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationDx As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn167 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn168 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationP As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn181 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn182 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationJ As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn179 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn180 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn185 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn186 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationV As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn183 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn184 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESkipLiquidationE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDGvSurgeriesPerformed As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColSelQx As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColMainqx As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColRouteIn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESelected As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDRptImgGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn187 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn188 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn98 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn171 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgStays As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcStays As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvStays As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridViewStay As IndigoGridView
    Friend WithEvents LayoutControlItem52 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn119 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFunctionalUnitStay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn189 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn190 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn191 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn192 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn194 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn196 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn197 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptChkSelectStay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDRptImgGeneratedStay As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn172 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn193 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn195 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvManualMovements As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColSubFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSubQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCreationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSubPatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColMovementType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn198 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn199 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcFolioDoneIC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SvgImageCollection1 As DevExpress.Utils.SvgImageCollection
    Friend WithEvents INDRptInterconsultasValidated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
End Class

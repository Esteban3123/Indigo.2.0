Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountControlAmbulatoryDetail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccountControlAmbulatoryDetail))
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDicceType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcAdministrativeNotes = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAdministrativeNotes = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectionAdministrativeNotes = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.MBtnCurrentAdmission = New DevExpress.XtraBars.BarButtonItem()
        Me.MBtnNewAdmission = New DevExpress.XtraBars.BarButtonItem()
        Me.INDrepIcbeGeneratedAdministrativeNotes = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDgcInterconsults = New DevExpress.XtraGrid.GridControl()
        Me.INDviewInterconsults = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectionInterconsults = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIcbeGeneratedInterconsults = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDpanelAdmission = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LycPccAdmission = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcAppointments = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAppointment = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtContact = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientEstrato = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientEntityName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtCareGroupPatient = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientAge = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientBirth = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtAfiliationType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtPatientCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.TxtRiskType = New DevExpress.XtraEditors.TextEdit()
        Me.TxtCareGroupAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtBedStay = New DevExpress.XtraEditors.TextEdit()
        Me.TxtResponsiblePhone = New DevExpress.XtraEditors.TextEdit()
        Me.TxtResponsibleName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtEntityNameAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAtentionCenter = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAuthorization = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPlaceEntry = New DevExpress.XtraEditors.TextEdit()
        Me.TxtFunctionalUnitAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAdmissionDate = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAdmissionType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtLiquidationType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtAdmissionCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LycgPccAdmission = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDtcgAppointments = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlcgAppointment = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAppointments = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
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
        Me.LayoutControlItem31 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcImages = New DevExpress.XtraGrid.GridControl()
        Me.INDviewImages = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectionImages = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIcbeGeneratedImages = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcPathologies = New DevExpress.XtraGrid.GridControl()
        Me.INDviewPathologies = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectionPathologies = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIcbeGeneratedPathologies = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcLaboratories = New DevExpress.XtraGrid.GridControl()
        Me.INDviewLaboratories = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectionLaboratories = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIcbeGeneratedLaboratories = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.DdbMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDgcOdontologyProcedures = New DevExpress.XtraGrid.GridControl()
        Me.INDviewOdontologyProcedure = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelection = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIcbeGenerated = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDtxtDate = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtFor = New DevExpress.XtraEditors.TextEdit()
        Me.INDlblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.INDSleAdmissionNumber2 = New Presentation.Controls.SearchLookUpEditExAdmission()
        Me.SearchLookUpEditExAdmissionView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAdmission = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemStatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFor = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDtcgInfo = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlcgOdontologyProcedures = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgLaboratories = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemLaboratories = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgPathologies = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPathologies = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgImages = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemImages = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgInterconsults = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemInterconsults = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgAdministrativeNotes = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAdministrativeNotes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridControl2 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDicceType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDgcAdministrativeNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAdministrativeNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectionAdministrativeNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeGeneratedAdministrativeNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcInterconsults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewInterconsults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectionInterconsults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeGeneratedInterconsults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelAdmission.SuspendLayout()
        CType(Me.LycPccAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LycPccAdmission.SuspendLayout()
        CType(Me.INDgcAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAppointment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtContact.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientEstrato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientEntityName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCareGroupPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientAge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientBirth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAfiliationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRiskType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCareGroupAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBedStay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEntityNameAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAtentionCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAuthorization.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPlaceEntry.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFunctionalUnitAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgPccAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtcgAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgAppointment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LiCareGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LiEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtContacto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgAdmissionGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcImages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewImages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectionImages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeGeneratedImages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcPathologies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewPathologies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectionPathologies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeGeneratedPathologies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcLaboratories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewLaboratories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectionLaboratories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeGeneratedLaboratories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcOdontologyProcedures, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewOdontologyProcedure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeGenerated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtFor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExAdmissionView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtcgInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgOdontologyProcedures, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgLaboratories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLaboratories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgPathologies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPathologies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgImages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemImages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgInterconsults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInterconsults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgAdministrativeNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAdministrativeNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1208, 558)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(1208, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1208, 98)
        '
        'INDicceType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDicceType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDicceType, False)
        Me.INDicceType.EditValue = ""
        Me.INDicceType.Location = New System.Drawing.Point(202, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDicceType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDicceType.Name = "INDicceType"
        Me.INDicceType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDicceType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDicceType.Properties.Appearance.Options.UseBackColor = True
        Me.INDicceType.Properties.Appearance.Options.UseFont = True
        Me.INDicceType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDicceType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDicceType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDicceType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDicceType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDicceType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDicceType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ambulatorio", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hospitalario", 2, -1)})
        Me.INDicceType.Properties.ReadOnly = True
        Me.INDicceType.Size = New System.Drawing.Size(136, 28)
        Me.INDicceType.StyleController = Me.INDlyRoot
        Me.INDicceType.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDicceType, 0)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDgcAdministrativeNotes)
        Me.INDlyRoot.Controls.Add(Me.INDgcInterconsults)
        Me.INDlyRoot.Controls.Add(Me.INDpanelAdmission)
        Me.INDlyRoot.Controls.Add(Me.INDgcImages)
        Me.INDlyRoot.Controls.Add(Me.INDgcPathologies)
        Me.INDlyRoot.Controls.Add(Me.INDgcLaboratories)
        Me.INDlyRoot.Controls.Add(Me.DdbMenu)
        Me.INDlyRoot.Controls.Add(Me.INDgcOdontologyProcedures)
        Me.INDlyRoot.Controls.Add(Me.INDtxtDate)
        Me.INDlyRoot.Controls.Add(Me.INDtxtFor)
        Me.INDlyRoot.Controls.Add(Me.INDicceType)
        Me.INDlyRoot.Controls.Add(Me.INDlblStatus)
        Me.INDlyRoot.Controls.Add(Me.INDSleAdmissionNumber2)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(1204, 549)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDgcAdministrativeNotes
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAdministrativeNotes, Nothing)
        Me.IndigoGridControl2.SetAddActions(Me.INDgcAdministrativeNotes, Nothing)
        Me.IndigoGridControl2.SetControlNextFocus(Me.INDgcAdministrativeNotes, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAdministrativeNotes, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAdministrativeNotes, False)
        Me.IndigoGridControl2.SetExportButton(Me.INDgcAdministrativeNotes, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAdministrativeNotes, True)
        Me.IndigoGridControl2.SetGuardarXml(Me.INDgcAdministrativeNotes, True)
        Me.IndigoGridControl2.SetHoldSize(Me.INDgcAdministrativeNotes, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAdministrativeNotes, False)
        Me.IndigoGridControl2.SetHotTrack(Me.INDgcAdministrativeNotes, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAdministrativeNotes, False)
        Me.INDgcAdministrativeNotes.Location = New System.Drawing.Point(24, 156)
        Me.INDgcAdministrativeNotes.MainView = Me.INDviewAdministrativeNotes
        Me.INDgcAdministrativeNotes.MenuManager = Me.BarManager1
        Me.INDgcAdministrativeNotes.Name = "INDgcAdministrativeNotes"
        Me.INDgcAdministrativeNotes.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectionAdministrativeNotes, Me.INDrepIcbeGeneratedAdministrativeNotes})
        Me.INDgcAdministrativeNotes.Size = New System.Drawing.Size(1156, 369)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAdministrativeNotes, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl2.SetSizeConstraintsType(Me.INDgcAdministrativeNotes, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcAdministrativeNotes.TabIndex = 42
        Me.INDgcAdministrativeNotes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewAdministrativeNotes})
        '
        'INDviewAdministrativeNotes
        '
        Me.INDviewAdministrativeNotes.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewAdministrativeNotes.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewAdministrativeNotes.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewAdministrativeNotes.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewAdministrativeNotes.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAdministrativeNotes.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewAdministrativeNotes.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAdministrativeNotes.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewAdministrativeNotes.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewAdministrativeNotes.Appearance.Row.Options.UseFont = True
        Me.INDviewAdministrativeNotes.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAdministrativeNotes.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewAdministrativeNotes.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn33, Me.GridColumn34, Me.GridColumn37, Me.GridColumn35, Me.GridColumn36, Me.GridColumn43})
        Me.INDviewAdministrativeNotes.GridControl = Me.INDgcAdministrativeNotes
        Me.INDviewAdministrativeNotes.Name = "INDviewAdministrativeNotes"
        Me.INDviewAdministrativeNotes.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAdministrativeNotes.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAdministrativeNotes.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAdministrativeNotes.OptionsView.ShowDetailButtons = False
        Me.INDviewAdministrativeNotes.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAdministrativeNotes, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewAdministrativeNotes, False)
        '
        'GridColumn33
        '
        Me.GridColumn33.Caption = "Sel."
        Me.GridColumn33.ColumnEdit = Me.INDrepCheckSelectionAdministrativeNotes
        Me.GridColumn33.FieldName = "Selection"
        Me.GridColumn33.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn33.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn33.OptionsColumn.AllowMove = False
        Me.GridColumn33.OptionsColumn.AllowShowHide = False
        Me.GridColumn33.OptionsColumn.AllowSize = False
        Me.GridColumn33.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn33.OptionsColumn.FixedWidth = True
        Me.GridColumn33.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn33.OptionsFilter.AllowFilter = False
        Me.GridColumn33.Visible = True
        Me.GridColumn33.VisibleIndex = 0
        Me.GridColumn33.Width = 49
        '
        'INDrepCheckSelectionAdministrativeNotes
        '
        Me.INDrepCheckSelectionAdministrativeNotes.AutoHeight = False
        Me.INDrepCheckSelectionAdministrativeNotes.Name = "INDrepCheckSelectionAdministrativeNotes"
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "CUPS"
        Me.GridColumn34.FieldName = "CupsDescription"
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.OptionsColumn.AllowEdit = False
        Me.GridColumn34.OptionsColumn.AllowFocus = False
        Me.GridColumn34.Visible = True
        Me.GridColumn34.VisibleIndex = 1
        Me.GridColumn34.Width = 501
        '
        'GridColumn37
        '
        Me.GridColumn37.Caption = "Variable"
        Me.GridColumn37.FieldName = "Variable"
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.OptionsColumn.AllowEdit = False
        Me.GridColumn37.OptionsColumn.AllowFocus = False
        Me.GridColumn37.Visible = True
        Me.GridColumn37.VisibleIndex = 2
        Me.GridColumn37.Width = 172
        '
        'GridColumn35
        '
        Me.GridColumn35.Caption = "Cantidad"
        Me.GridColumn35.FieldName = "Quantity"
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.OptionsColumn.AllowEdit = False
        Me.GridColumn35.OptionsColumn.AllowFocus = False
        Me.GridColumn35.Visible = True
        Me.GridColumn35.VisibleIndex = 3
        Me.GridColumn35.Width = 84
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "Usuario"
        Me.GridColumn36.FieldName = "ProfessionalDescription"
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.OptionsColumn.AllowEdit = False
        Me.GridColumn36.OptionsColumn.AllowFocus = False
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 4
        Me.GridColumn36.Width = 334
        '
        'GridColumn43
        '
        Me.GridColumn43.Caption = "RIAS"
        Me.GridColumn43.FieldName = "RiasDescription"
        Me.GridColumn43.Name = "GridColumn43"
        Me.GridColumn43.OptionsColumn.AllowEdit = False
        Me.GridColumn43.OptionsColumn.AllowFocus = False
        Me.GridColumn43.Visible = True
        Me.GridColumn43.VisibleIndex = 5
        Me.GridColumn43.Width = 252
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MBtnCurrentAdmission, Me.MBtnNewAdmission})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1208, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 680)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1208, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 675)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1208, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 675)
        '
        'MBtnCurrentAdmission
        '
        Me.MBtnCurrentAdmission.Caption = "Liquidar en Ingreso Actual"
        Me.MBtnCurrentAdmission.Id = 0
        Me.MBtnCurrentAdmission.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.ingresos_abierto
        Me.MBtnCurrentAdmission.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCurrentAdmission.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnCurrentAdmission.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCurrentAdmission.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnCurrentAdmission.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCurrentAdmission.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnCurrentAdmission.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCurrentAdmission.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnCurrentAdmission.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCurrentAdmission.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnCurrentAdmission.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCurrentAdmission.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnCurrentAdmission.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCurrentAdmission.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnCurrentAdmission.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnCurrentAdmission.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnCurrentAdmission.Name = "MBtnCurrentAdmission"
        '
        'MBtnNewAdmission
        '
        Me.MBtnNewAdmission.Caption = "Liquidar en Ingreso Nuevo"
        Me.MBtnNewAdmission.Id = 1
        Me.MBtnNewAdmission.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.Ingresos_facturados_1_
        Me.MBtnNewAdmission.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnNewAdmission.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnNewAdmission.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnNewAdmission.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnNewAdmission.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnNewAdmission.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnNewAdmission.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnNewAdmission.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnNewAdmission.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnNewAdmission.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnNewAdmission.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnNewAdmission.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnNewAdmission.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnNewAdmission.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnNewAdmission.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.MBtnNewAdmission.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnNewAdmission.Name = "MBtnNewAdmission"
        '
        'INDrepIcbeGeneratedAdministrativeNotes
        '
        Me.INDrepIcbeGeneratedAdministrativeNotes.AutoHeight = False
        Me.INDrepIcbeGeneratedAdministrativeNotes.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedAdministrativeNotes.HtmlImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedAdministrativeNotes.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", True, 1)})
        Me.INDrepIcbeGeneratedAdministrativeNotes.LargeImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedAdministrativeNotes.Name = "INDrepIcbeGeneratedAdministrativeNotes"
        Me.INDrepIcbeGeneratedAdministrativeNotes.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDrepIcbeGeneratedAdministrativeNotes.SmallImages = Me.ImageCollection1
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "Icono correcto e incorrecto 16x16-02.png")
        Me.ImageCollection1.Images.SetKeyName(1, "Icono correcto e incorrecto 16x16-01.png")
        '
        'INDgcInterconsults
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInterconsults, Nothing)
        Me.IndigoGridControl2.SetAddActions(Me.INDgcInterconsults, Nothing)
        Me.IndigoGridControl2.SetControlNextFocus(Me.INDgcInterconsults, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInterconsults, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInterconsults, False)
        Me.IndigoGridControl2.SetExportButton(Me.INDgcInterconsults, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInterconsults, True)
        Me.IndigoGridControl2.SetGuardarXml(Me.INDgcInterconsults, True)
        Me.IndigoGridControl2.SetHoldSize(Me.INDgcInterconsults, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInterconsults, False)
        Me.IndigoGridControl2.SetHotTrack(Me.INDgcInterconsults, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInterconsults, False)
        Me.INDgcInterconsults.Location = New System.Drawing.Point(24, 156)
        Me.INDgcInterconsults.MainView = Me.INDviewInterconsults
        Me.INDgcInterconsults.MenuManager = Me.BarManager1
        Me.INDgcInterconsults.Name = "INDgcInterconsults"
        Me.INDgcInterconsults.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectionInterconsults, Me.INDrepIcbeGeneratedInterconsults})
        Me.INDgcInterconsults.Size = New System.Drawing.Size(1156, 369)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInterconsults, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl2.SetSizeConstraintsType(Me.INDgcInterconsults, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcInterconsults.TabIndex = 41
        Me.INDgcInterconsults.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewInterconsults})
        '
        'INDviewInterconsults
        '
        Me.INDviewInterconsults.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewInterconsults.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewInterconsults.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewInterconsults.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewInterconsults.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewInterconsults.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewInterconsults.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewInterconsults.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewInterconsults.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewInterconsults.Appearance.Row.Options.UseFont = True
        Me.INDviewInterconsults.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewInterconsults.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewInterconsults.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn27, Me.GridColumn28, Me.GridColumn29, Me.GridColumn30, Me.GridColumn31, Me.GridColumn32, Me.GridColumn42, Me.GridColumn54})
        Me.INDviewInterconsults.GridControl = Me.INDgcInterconsults
        Me.INDviewInterconsults.Name = "INDviewInterconsults"
        Me.INDviewInterconsults.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewInterconsults.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewInterconsults.OptionsView.ShowAutoFilterRow = True
        Me.INDviewInterconsults.OptionsView.ShowDetailButtons = False
        Me.INDviewInterconsults.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewInterconsults, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewInterconsults, False)
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Sel."
        Me.GridColumn27.ColumnEdit = Me.INDrepCheckSelectionInterconsults
        Me.GridColumn27.FieldName = "Selection"
        Me.GridColumn27.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn27.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn27.OptionsColumn.AllowMove = False
        Me.GridColumn27.OptionsColumn.AllowShowHide = False
        Me.GridColumn27.OptionsColumn.AllowSize = False
        Me.GridColumn27.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn27.OptionsColumn.FixedWidth = True
        Me.GridColumn27.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn27.OptionsFilter.AllowFilter = False
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 0
        Me.GridColumn27.Width = 30
        '
        'INDrepCheckSelectionInterconsults
        '
        Me.INDrepCheckSelectionInterconsults.AutoHeight = False
        Me.INDrepCheckSelectionInterconsults.Name = "INDrepCheckSelectionInterconsults"
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Fecha"
        Me.GridColumn28.FieldName = "MedicalOrderDate"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.OptionsColumn.AllowEdit = False
        Me.GridColumn28.OptionsColumn.AllowFocus = False
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 1
        Me.GridColumn28.Width = 115
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "CUPS"
        Me.GridColumn29.FieldName = "CupsDescription"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.OptionsColumn.AllowEdit = False
        Me.GridColumn29.OptionsColumn.AllowFocus = False
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 2
        Me.GridColumn29.Width = 466
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Folio"
        Me.GridColumn30.FieldName = "FolioNumber"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.OptionsColumn.AllowEdit = False
        Me.GridColumn30.OptionsColumn.AllowFocus = False
        Me.GridColumn30.Visible = True
        Me.GridColumn30.VisibleIndex = 3
        Me.GridColumn30.Width = 55
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Profesional"
        Me.GridColumn31.FieldName = "ProfessionalDescription"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.OptionsColumn.AllowFocus = False
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 4
        Me.GridColumn31.Width = 404
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "Cantidad"
        Me.GridColumn32.FieldName = "Quantity"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.OptionsColumn.AllowEdit = False
        Me.GridColumn32.OptionsColumn.AllowFocus = False
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 5
        Me.GridColumn32.Width = 86
        '
        'GridColumn42
        '
        Me.GridColumn42.Caption = "RIAS"
        Me.GridColumn42.FieldName = "RiasDescription"
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.OptionsColumn.AllowEdit = False
        Me.GridColumn42.OptionsColumn.AllowFocus = False
        Me.GridColumn42.Visible = True
        Me.GridColumn42.VisibleIndex = 6
        Me.GridColumn42.Width = 219
        '
        'INDrepIcbeGeneratedInterconsults
        '
        Me.INDrepIcbeGeneratedInterconsults.Appearance.Options.UseTextOptions = True
        Me.INDrepIcbeGeneratedInterconsults.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedInterconsults.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDrepIcbeGeneratedInterconsults.AutoHeight = False
        Me.INDrepIcbeGeneratedInterconsults.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedInterconsults.HtmlImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedInterconsults.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", True, 1)})
        Me.INDrepIcbeGeneratedInterconsults.LargeImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedInterconsults.Name = "INDrepIcbeGeneratedInterconsults"
        Me.INDrepIcbeGeneratedInterconsults.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDrepIcbeGeneratedInterconsults.SmallImages = Me.ImageCollection1
        '
        'INDpanelAdmission
        '
        Me.INDpanelAdmission.Controls.Add(Me.LycPccAdmission)
        Me.INDpanelAdmission.Location = New System.Drawing.Point(90, 446)
        Me.INDpanelAdmission.Name = "INDpanelAdmission"
        Me.INDpanelAdmission.Size = New System.Drawing.Size(799, 305)
        Me.INDpanelAdmission.TabIndex = 24
        '
        'LycPccAdmission
        '
        Me.LycPccAdmission.AllowCustomization = False
        Me.LycPccAdmission.Controls.Add(Me.INDgcAppointments)
        Me.LycPccAdmission.Controls.Add(Me.LabelControl3)
        Me.LycPccAdmission.Controls.Add(Me.TxtContact)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientEstrato)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientEntityName)
        Me.LycPccAdmission.Controls.Add(Me.TxtCareGroupPatient)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientAge)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientBirth)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientName)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientType)
        Me.LycPccAdmission.Controls.Add(Me.TxtAfiliationType)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientCode)
        Me.LycPccAdmission.Controls.Add(Me.TxtRiskType)
        Me.LycPccAdmission.Controls.Add(Me.TxtCareGroupAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtBedStay)
        Me.LycPccAdmission.Controls.Add(Me.TxtResponsiblePhone)
        Me.LycPccAdmission.Controls.Add(Me.TxtResponsibleName)
        Me.LycPccAdmission.Controls.Add(Me.TxtEntityNameAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtAtentionCenter)
        Me.LycPccAdmission.Controls.Add(Me.TxtAuthorization)
        Me.LycPccAdmission.Controls.Add(Me.TxtPlaceEntry)
        Me.LycPccAdmission.Controls.Add(Me.TxtFunctionalUnitAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionDate)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionType)
        Me.LycPccAdmission.Controls.Add(Me.TxtLiquidationType)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionCode)
        Me.LycPccAdmission.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LycPccAdmission, False)
        Me.LycPccAdmission.Location = New System.Drawing.Point(0, 0)
        Me.LycPccAdmission.Name = "LycPccAdmission"
        Me.LycPccAdmission.Root = Me.LycgPccAdmission
        Me.LycPccAdmission.Size = New System.Drawing.Size(799, 305)
        Me.LycPccAdmission.TabIndex = 0
        Me.LycPccAdmission.Text = "LayoutControl1"
        '
        'INDgcAppointments
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAppointments, Nothing)
        Me.IndigoGridControl2.SetAddActions(Me.INDgcAppointments, Nothing)
        Me.IndigoGridControl2.SetControlNextFocus(Me.INDgcAppointments, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAppointments, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAppointments, False)
        Me.IndigoGridControl2.SetExportButton(Me.INDgcAppointments, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAppointments, True)
        Me.IndigoGridControl2.SetGuardarXml(Me.INDgcAppointments, True)
        Me.IndigoGridControl2.SetHoldSize(Me.INDgcAppointments, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAppointments, False)
        Me.IndigoGridControl2.SetHotTrack(Me.INDgcAppointments, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAppointments, False)
        Me.INDgcAppointments.Location = New System.Drawing.Point(14, 84)
        Me.INDgcAppointments.MainView = Me.INDviewAppointment
        Me.INDgcAppointments.MenuManager = Me.BarManager1
        Me.INDgcAppointments.Name = "INDgcAppointments"
        Me.INDgcAppointments.Size = New System.Drawing.Size(771, 207)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAppointments, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl2.SetSizeConstraintsType(Me.INDgcAppointments, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcAppointments.TabIndex = 34
        Me.INDgcAppointments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewAppointment})
        '
        'INDviewAppointment
        '
        Me.INDviewAppointment.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewAppointment.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewAppointment.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewAppointment.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewAppointment.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAppointment.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewAppointment.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAppointment.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewAppointment.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewAppointment.Appearance.Row.Options.UseFont = True
        Me.INDviewAppointment.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAppointment.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewAppointment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn44, Me.GridColumn45, Me.GridColumn46, Me.GridColumn47, Me.GridColumn48, Me.GridColumn49, Me.GridColumn50})
        Me.INDviewAppointment.GridControl = Me.INDgcAppointments
        Me.INDviewAppointment.Name = "INDviewAppointment"
        Me.INDviewAppointment.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAppointment.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAppointment.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAppointment.OptionsView.ShowDetailButtons = False
        Me.INDviewAppointment.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAppointment, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewAppointment, False)
        '
        'GridColumn44
        '
        Me.GridColumn44.Caption = "Centro Atención"
        Me.GridColumn44.FieldName = "CareCenterDescription"
        Me.GridColumn44.Name = "GridColumn44"
        Me.GridColumn44.OptionsColumn.AllowEdit = False
        Me.GridColumn44.OptionsColumn.AllowFocus = False
        Me.GridColumn44.Visible = True
        Me.GridColumn44.VisibleIndex = 0
        '
        'GridColumn45
        '
        Me.GridColumn45.Caption = "Fecha Inicio"
        Me.GridColumn45.FieldName = "InitialDate"
        Me.GridColumn45.Name = "GridColumn45"
        Me.GridColumn45.OptionsColumn.AllowEdit = False
        Me.GridColumn45.OptionsColumn.AllowFocus = False
        Me.GridColumn45.Visible = True
        Me.GridColumn45.VisibleIndex = 1
        '
        'GridColumn46
        '
        Me.GridColumn46.Caption = "Fecha Fin"
        Me.GridColumn46.FieldName = "EndDate"
        Me.GridColumn46.Name = "GridColumn46"
        Me.GridColumn46.OptionsColumn.AllowEdit = False
        Me.GridColumn46.OptionsColumn.AllowFocus = False
        Me.GridColumn46.Visible = True
        Me.GridColumn46.VisibleIndex = 2
        '
        'GridColumn47
        '
        Me.GridColumn47.Caption = "Especialidad"
        Me.GridColumn47.FieldName = "SpecialtyDescription"
        Me.GridColumn47.Name = "GridColumn47"
        Me.GridColumn47.OptionsColumn.AllowEdit = False
        Me.GridColumn47.OptionsColumn.AllowFocus = False
        Me.GridColumn47.Visible = True
        Me.GridColumn47.VisibleIndex = 3
        '
        'GridColumn48
        '
        Me.GridColumn48.Caption = "Actividad"
        Me.GridColumn48.FieldName = "ActivityDescription"
        Me.GridColumn48.Name = "GridColumn48"
        Me.GridColumn48.OptionsColumn.AllowEdit = False
        Me.GridColumn48.OptionsColumn.AllowFocus = False
        Me.GridColumn48.Visible = True
        Me.GridColumn48.VisibleIndex = 4
        '
        'GridColumn49
        '
        Me.GridColumn49.Caption = "Profesional"
        Me.GridColumn49.FieldName = "ProfessionalDescription"
        Me.GridColumn49.Name = "GridColumn49"
        Me.GridColumn49.OptionsColumn.AllowEdit = False
        Me.GridColumn49.OptionsColumn.AllowFocus = False
        Me.GridColumn49.Visible = True
        Me.GridColumn49.VisibleIndex = 5
        '
        'GridColumn50
        '
        Me.GridColumn50.Caption = "Estado"
        Me.GridColumn50.FieldName = "StatusDescription"
        Me.GridColumn50.Name = "GridColumn50"
        Me.GridColumn50.OptionsColumn.AllowEdit = False
        Me.GridColumn50.OptionsColumn.AllowFocus = False
        Me.GridColumn50.Visible = True
        Me.GridColumn50.VisibleIndex = 6
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
        Me.LabelControl3.Location = New System.Drawing.Point(0, 0)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(14, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(799, 40)
        Me.LabelControl3.StyleController = Me.LycPccAdmission
        Me.LabelControl3.TabIndex = 26
        Me.LabelControl3.Text = "Más Información"
        '
        'TxtContact
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtContact, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtContact, False)
        Me.TxtContact.Location = New System.Drawing.Point(449, 204)
        Me.IndigoTextEdit1.SetMascara(Me.TxtContact, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtContact.Name = "TxtContact"
        Me.TxtContact.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtContact.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtContact.Properties.Appearance.Options.UseBackColor = True
        Me.TxtContact.Properties.Appearance.Options.UseFont = True
        Me.TxtContact.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtContact.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtContact.Properties.ReadOnly = True
        Me.TxtContact.Size = New System.Drawing.Size(336, 24)
        Me.TxtContact.StyleController = Me.LycPccAdmission
        Me.TxtContact.TabIndex = 31
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtContact, 0)
        '
        'TxtPatientEstrato
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientEstrato, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientEstrato, False)
        Me.TxtPatientEstrato.Location = New System.Drawing.Point(141, 204)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientEstrato, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientEstrato.Name = "TxtPatientEstrato"
        Me.TxtPatientEstrato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtPatientEstrato.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientEstrato.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientEstrato.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientEstrato.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientEstrato.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientEstrato.Properties.ReadOnly = True
        Me.TxtPatientEstrato.Size = New System.Drawing.Size(169, 24)
        Me.TxtPatientEstrato.StyleController = Me.LycPccAdmission
        Me.TxtPatientEstrato.TabIndex = 25
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientEstrato, 0)
        '
        'TxtPatientEntityName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientEntityName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientEntityName, False)
        Me.TxtPatientEntityName.Location = New System.Drawing.Point(449, 174)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientEntityName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientEntityName.Name = "TxtPatientEntityName"
        Me.TxtPatientEntityName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtPatientEntityName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientEntityName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientEntityName.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientEntityName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientEntityName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientEntityName.Properties.ReadOnly = True
        Me.TxtPatientEntityName.Size = New System.Drawing.Size(336, 24)
        Me.TxtPatientEntityName.StyleController = Me.LycPccAdmission
        Me.TxtPatientEntityName.TabIndex = 24
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientEntityName, 0)
        '
        'TxtCareGroupPatient
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtCareGroupPatient, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtCareGroupPatient, False)
        Me.TxtCareGroupPatient.Location = New System.Drawing.Point(141, 174)
        Me.IndigoTextEdit1.SetMascara(Me.TxtCareGroupPatient, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtCareGroupPatient.Name = "TxtCareGroupPatient"
        Me.TxtCareGroupPatient.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtCareGroupPatient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCareGroupPatient.Properties.Appearance.Options.UseBackColor = True
        Me.TxtCareGroupPatient.Properties.Appearance.Options.UseFont = True
        Me.TxtCareGroupPatient.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupPatient.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtCareGroupPatient.Properties.ReadOnly = True
        Me.TxtCareGroupPatient.Size = New System.Drawing.Size(169, 24)
        Me.TxtCareGroupPatient.StyleController = Me.LycPccAdmission
        Me.TxtCareGroupPatient.TabIndex = 23
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtCareGroupPatient, 0)
        '
        'TxtPatientAge
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientAge, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientAge, False)
        Me.TxtPatientAge.Location = New System.Drawing.Point(449, 114)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientAge, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientAge.Name = "TxtPatientAge"
        Me.TxtPatientAge.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtPatientAge.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientAge.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientAge.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientAge.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientAge.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientAge.Properties.ReadOnly = True
        Me.TxtPatientAge.Size = New System.Drawing.Size(336, 24)
        Me.TxtPatientAge.StyleController = Me.LycPccAdmission
        Me.TxtPatientAge.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientAge, 0)
        '
        'TxtPatientBirth
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientBirth, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientBirth, False)
        Me.TxtPatientBirth.Location = New System.Drawing.Point(141, 114)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientBirth, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientBirth.Name = "TxtPatientBirth"
        Me.TxtPatientBirth.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtPatientBirth.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientBirth.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientBirth.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientBirth.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientBirth.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientBirth.Properties.ReadOnly = True
        Me.TxtPatientBirth.Size = New System.Drawing.Size(169, 24)
        Me.TxtPatientBirth.StyleController = Me.LycPccAdmission
        Me.TxtPatientBirth.TabIndex = 21
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientBirth, 0)
        '
        'TxtPatientName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientName, False)
        Me.TxtPatientName.Location = New System.Drawing.Point(449, 84)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientName.Name = "TxtPatientName"
        Me.TxtPatientName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtPatientName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientName.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientName.Properties.ReadOnly = True
        Me.TxtPatientName.Size = New System.Drawing.Size(336, 24)
        Me.TxtPatientName.StyleController = Me.LycPccAdmission
        Me.TxtPatientName.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientName, 0)
        '
        'TxtPatientType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientType, False)
        Me.TxtPatientType.Location = New System.Drawing.Point(141, 144)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientType.Name = "TxtPatientType"
        Me.TxtPatientType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtPatientType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientType.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Contributivo", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Subsidiado", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Vinculado", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Particular", 4, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado Reg. Contributivo", 6, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado Reg. Subsidiado", 7, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado no Asegurado", 8, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Otro", 5, -1)})
        Me.TxtPatientType.Properties.ReadOnly = True
        Me.TxtPatientType.Size = New System.Drawing.Size(169, 24)
        Me.TxtPatientType.StyleController = Me.LycPccAdmission
        Me.TxtPatientType.TabIndex = 27
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientType, 0)
        '
        'TxtAfiliationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAfiliationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAfiliationType, False)
        Me.TxtAfiliationType.Location = New System.Drawing.Point(449, 144)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAfiliationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAfiliationType.Name = "TxtAfiliationType"
        Me.TxtAfiliationType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtAfiliationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAfiliationType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAfiliationType.Properties.Appearance.Options.UseFont = True
        Me.TxtAfiliationType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No Aplica", 0, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cotizante", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Beneficiario", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Adicional", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Jub/Retirado", 4, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pensionado", 5, -1)})
        Me.TxtAfiliationType.Properties.ReadOnly = True
        Me.TxtAfiliationType.Size = New System.Drawing.Size(336, 24)
        Me.TxtAfiliationType.StyleController = Me.LycPccAdmission
        Me.TxtAfiliationType.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAfiliationType, 0)
        '
        'TxtPatientCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientCode, False)
        Me.TxtPatientCode.Location = New System.Drawing.Point(141, 84)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientCode.Name = "TxtPatientCode"
        Me.TxtPatientCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtPatientCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientCode.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientCode.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
        Me.TxtPatientCode.Properties.ReadOnly = True
        Me.TxtPatientCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.TxtPatientCode.Size = New System.Drawing.Size(169, 24)
        Me.TxtPatientCode.StyleController = Me.LycPccAdmission
        Me.TxtPatientCode.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientCode, 0)
        '
        'TxtRiskType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtRiskType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtRiskType, False)
        Me.TxtRiskType.Location = New System.Drawing.Point(141, 144)
        Me.IndigoTextEdit1.SetMascara(Me.TxtRiskType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtRiskType.Name = "TxtRiskType"
        Me.TxtRiskType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtRiskType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtRiskType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtRiskType.Properties.Appearance.Options.UseFont = True
        Me.TxtRiskType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtRiskType.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtRiskType.Properties.ReadOnly = True
        Me.TxtRiskType.Size = New System.Drawing.Size(169, 24)
        Me.TxtRiskType.StyleController = Me.LycPccAdmission
        Me.TxtRiskType.TabIndex = 33
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtRiskType, 0)
        '
        'TxtCareGroupAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtCareGroupAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtCareGroupAdmission, False)
        Me.TxtCareGroupAdmission.Location = New System.Drawing.Point(141, 114)
        Me.IndigoTextEdit1.SetMascara(Me.TxtCareGroupAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtCareGroupAdmission.Name = "TxtCareGroupAdmission"
        Me.TxtCareGroupAdmission.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtCareGroupAdmission.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.TxtCareGroupAdmission.Properties.Appearance.Options.UseFont = True
        Me.TxtCareGroupAdmission.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtCareGroupAdmission.Properties.ReadOnly = True
        Me.TxtCareGroupAdmission.Size = New System.Drawing.Size(169, 24)
        Me.TxtCareGroupAdmission.StyleController = Me.LycPccAdmission
        Me.TxtCareGroupAdmission.TabIndex = 32
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtCareGroupAdmission, 0)
        '
        'TxtBedStay
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtBedStay, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtBedStay, False)
        Me.TxtBedStay.Location = New System.Drawing.Point(449, 174)
        Me.IndigoTextEdit1.SetMascara(Me.TxtBedStay, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtBedStay.Name = "TxtBedStay"
        Me.TxtBedStay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.TxtBedStay.Size = New System.Drawing.Size(336, 24)
        Me.TxtBedStay.StyleController = Me.LycPccAdmission
        Me.TxtBedStay.TabIndex = 18
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtBedStay, 0)
        '
        'TxtResponsiblePhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtResponsiblePhone, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtResponsiblePhone, False)
        Me.TxtResponsiblePhone.Location = New System.Drawing.Point(449, 264)
        Me.IndigoTextEdit1.SetMascara(Me.TxtResponsiblePhone, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtResponsiblePhone.Name = "TxtResponsiblePhone"
        Me.TxtResponsiblePhone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.TxtResponsiblePhone.Size = New System.Drawing.Size(336, 24)
        Me.TxtResponsiblePhone.StyleController = Me.LycPccAdmission
        Me.TxtResponsiblePhone.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtResponsiblePhone, 0)
        '
        'TxtResponsibleName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtResponsibleName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtResponsibleName, False)
        Me.TxtResponsibleName.Location = New System.Drawing.Point(141, 264)
        Me.IndigoTextEdit1.SetMascara(Me.TxtResponsibleName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtResponsibleName.Name = "TxtResponsibleName"
        Me.TxtResponsibleName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtResponsibleName, 0)
        '
        'TxtEntityNameAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtEntityNameAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtEntityNameAdmission, False)
        Me.TxtEntityNameAdmission.Location = New System.Drawing.Point(449, 114)
        Me.IndigoTextEdit1.SetMascara(Me.TxtEntityNameAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtEntityNameAdmission.Name = "TxtEntityNameAdmission"
        Me.TxtEntityNameAdmission.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.TxtEntityNameAdmission.Size = New System.Drawing.Size(336, 24)
        Me.TxtEntityNameAdmission.StyleController = Me.LycPccAdmission
        Me.TxtEntityNameAdmission.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtEntityNameAdmission, 0)
        '
        'TxtAtentionCenter
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAtentionCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAtentionCenter, False)
        Me.TxtAtentionCenter.Location = New System.Drawing.Point(141, 234)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAtentionCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAtentionCenter.Name = "TxtAtentionCenter"
        Me.TxtAtentionCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAtentionCenter, 0)
        '
        'TxtAuthorization
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAuthorization, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAuthorization, False)
        Me.TxtAuthorization.Location = New System.Drawing.Point(449, 204)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAuthorization, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAuthorization.Name = "TxtAuthorization"
        Me.TxtAuthorization.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtAuthorization.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAuthorization.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAuthorization.Properties.Appearance.Options.UseFont = True
        Me.TxtAuthorization.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAuthorization.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAuthorization.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAuthorization.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAuthorization.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAuthorization.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAuthorization.Properties.ReadOnly = True
        Me.TxtAuthorization.Size = New System.Drawing.Size(336, 24)
        Me.TxtAuthorization.StyleController = Me.LycPccAdmission
        Me.TxtAuthorization.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAuthorization, 0)
        '
        'TxtPlaceEntry
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPlaceEntry, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPlaceEntry, False)
        Me.TxtPlaceEntry.Location = New System.Drawing.Point(449, 144)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPlaceEntry, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPlaceEntry.Name = "TxtPlaceEntry"
        Me.TxtPlaceEntry.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.TxtPlaceEntry.Size = New System.Drawing.Size(336, 24)
        Me.TxtPlaceEntry.StyleController = Me.LycPccAdmission
        Me.TxtPlaceEntry.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPlaceEntry, 0)
        '
        'TxtFunctionalUnitAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtFunctionalUnitAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtFunctionalUnitAdmission, False)
        Me.TxtFunctionalUnitAdmission.Location = New System.Drawing.Point(449, 234)
        Me.IndigoTextEdit1.SetMascara(Me.TxtFunctionalUnitAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtFunctionalUnitAdmission.Name = "TxtFunctionalUnitAdmission"
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.TxtFunctionalUnitAdmission.Size = New System.Drawing.Size(336, 24)
        Me.TxtFunctionalUnitAdmission.StyleController = Me.LycPccAdmission
        Me.TxtFunctionalUnitAdmission.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtFunctionalUnitAdmission, 0)
        '
        'TxtAdmissionDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionDate, False)
        Me.TxtAdmissionDate.Location = New System.Drawing.Point(449, 84)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAdmissionDate.Name = "TxtAdmissionDate"
        Me.TxtAdmissionDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.TxtAdmissionDate.Size = New System.Drawing.Size(336, 24)
        Me.TxtAdmissionDate.StyleController = Me.LycPccAdmission
        Me.TxtAdmissionDate.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionDate, 0)
        '
        'TxtAdmissionType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionType, False)
        Me.TxtAdmissionType.Location = New System.Drawing.Point(141, 174)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAdmissionType.Name = "TxtAdmissionType"
        Me.TxtAdmissionType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionType, 0)
        '
        'TxtLiquidationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtLiquidationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtLiquidationType, False)
        Me.TxtLiquidationType.Location = New System.Drawing.Point(141, 204)
        Me.IndigoTextEdit1.SetMascara(Me.TxtLiquidationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtLiquidationType.Name = "TxtLiquidationType"
        Me.TxtLiquidationType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtLiquidationType, 0)
        '
        'TxtAdmissionCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionCode, False)
        Me.TxtAdmissionCode.Location = New System.Drawing.Point(141, 84)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAdmissionCode.Name = "TxtAdmissionCode"
        Me.TxtAdmissionCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtAdmissionCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdmissionCode.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionCode.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAdmissionCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
        Me.TxtAdmissionCode.Properties.ReadOnly = True
        Me.TxtAdmissionCode.Size = New System.Drawing.Size(169, 24)
        Me.TxtAdmissionCode.StyleController = Me.LycPccAdmission
        Me.TxtAdmissionCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionCode, 0)
        '
        'LycgPccAdmission
        '
        Me.LycgPccAdmission.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgPccAdmission.AppearanceGroup.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LycgPccAdmission, False)
        Me.LycgPccAdmission.CustomizationFormText = "LayoutControlGroup1"
        Me.LycgPccAdmission.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LycgPccAdmission.GroupBordersVisible = False
        Me.LycgPccAdmission.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDtcgAppointments, Me.LayoutControlItem31})
        Me.LycgPccAdmission.Name = "LycgPccAdmission"
        Me.LycgPccAdmission.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LycgPccAdmission.Size = New System.Drawing.Size(799, 305)
        Me.LycgPccAdmission.TextVisible = False
        '
        'INDtcgAppointments
        '
        Me.INDtcgAppointments.CustomizationFormText = "TabbedControlGroup1"
        Me.INDtcgAppointments.Location = New System.Drawing.Point(0, 40)
        Me.INDtcgAppointments.Name = "INDtcgAppointments"
        Me.INDtcgAppointments.SelectedTabPage = Me.INDlcgAppointment
        Me.INDtcgAppointments.Size = New System.Drawing.Size(799, 265)
        Me.INDtcgAppointments.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LycgAdmissionGroup, Me.INDlcgAppointment})
        '
        'INDlcgAppointment
        '
        Me.INDlcgAppointment.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgAppointment.AppearanceGroup.Options.UseFont = True
        Me.INDlcgAppointment.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgAppointment.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgAppointment.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAppointment.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgAppointment.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgAppointment.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgAppointment.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAppointment.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgAppointment.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAppointment.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgAppointment.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAppointment.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgAppointment, False)
        Me.INDlcgAppointment.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAppointments})
        Me.INDlcgAppointment.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgAppointment.Name = "INDlcgAppointment"
        Me.INDlcgAppointment.Size = New System.Drawing.Size(775, 211)
        Me.INDlcgAppointment.Text = "Citas de Agendamiento"
        '
        'INDlyItemAppointments
        '
        Me.INDlyItemAppointments.Control = Me.INDgcAppointments
        Me.INDlyItemAppointments.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAppointments.Name = "INDlyItemAppointments"
        Me.INDlyItemAppointments.Size = New System.Drawing.Size(775, 211)
        Me.INDlyItemAppointments.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAppointments.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "Datos del Paciente"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem23, Me.LayoutControlItem25, Me.LiCareGroup, Me.LayoutControlItem29, Me.LayoutControlItem26, Me.LiEntity, Me.LayoutControlItem24, Me.LayoutControlItem18, Me.LayoutControlItem17, Me.TxtContacto})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(775, 211)
        Me.LayoutControlGroup2.Text = "Datos del Paciente"
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem23.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem23.Control = Me.TxtPatientCode
        Me.LayoutControlItem23.CustomizationFormText = "Identificación"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem23.MinSize = New System.Drawing.Size(50, 25)
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
        Me.LayoutControlItem29.Size = New System.Drawing.Size(300, 91)
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
        Me.LayoutControlItem26.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem26.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem26.Size = New System.Drawing.Size(475, 30)
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
        Me.LiEntity.MaxSize = New System.Drawing.Size(340, 30)
        Me.LiEntity.MinSize = New System.Drawing.Size(340, 30)
        Me.LiEntity.Name = "LiEntity"
        Me.LiEntity.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LiEntity.Size = New System.Drawing.Size(475, 30)
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
        Me.LayoutControlItem24.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem24.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem24.Size = New System.Drawing.Size(475, 30)
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
        Me.LayoutControlItem18.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem18.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem18.Size = New System.Drawing.Size(475, 30)
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
        Me.TxtContacto.MaxSize = New System.Drawing.Size(340, 30)
        Me.TxtContacto.MinSize = New System.Drawing.Size(340, 30)
        Me.TxtContacto.Name = "TxtContacto"
        Me.TxtContacto.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.TxtContacto.Size = New System.Drawing.Size(475, 91)
        Me.TxtContacto.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.TxtContacto.Text = "Contacto"
        Me.TxtContacto.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.TxtContacto.TextSize = New System.Drawing.Size(115, 20)
        Me.TxtContacto.TextToControlDistance = 12
        '
        'LycgAdmissionGroup
        '
        Me.LycgAdmissionGroup.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgAdmissionGroup.AppearanceGroup.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LycgAdmissionGroup, False)
        Me.LycgAdmissionGroup.CustomizationFormText = "Datos del Ingreso"
        Me.LycgAdmissionGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem11, Me.LayoutControlItem13, Me.LayoutControlItem14, Me.LayoutControlItem15, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem27, Me.LayoutControlItem16, Me.LayoutControlItem39, Me.LayoutControlItem12, Me.LayoutControlItem10, Me.LayoutControlItem19, Me.LayoutControlItem20})
        Me.LycgAdmissionGroup.Location = New System.Drawing.Point(0, 0)
        Me.LycgAdmissionGroup.Name = "LycgAdmissionGroup"
        Me.LycgAdmissionGroup.Size = New System.Drawing.Size(775, 211)
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
        Me.LayoutControlItem14.Control = Me.TxtAuthorization
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(300, 120)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem14.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.Text = "# Autorización"
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
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem8.Size = New System.Drawing.Size(475, 30)
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
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem9.Size = New System.Drawing.Size(475, 30)
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
        Me.LayoutControlItem27.TextSize = New System.Drawing.Size(115, 20)
        Me.LayoutControlItem27.TextToControlDistance = 12
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem16.Control = Me.TxtEntityNameAdmission
        Me.LayoutControlItem16.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(300, 30)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem16.Size = New System.Drawing.Size(475, 30)
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
        Me.LayoutControlItem39.TextSize = New System.Drawing.Size(115, 20)
        Me.LayoutControlItem39.TextToControlDistance = 12
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem12.Control = Me.TxtPlaceEntry
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(300, 60)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem12.Size = New System.Drawing.Size(475, 30)
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
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem10.Size = New System.Drawing.Size(475, 30)
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
        Me.LayoutControlItem19.Size = New System.Drawing.Size(300, 31)
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
        Me.LayoutControlItem20.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem20.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem20.Size = New System.Drawing.Size(475, 31)
        Me.LayoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem20.Text = "Teléfono Acudiente"
        Me.LayoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem20.TextToControlDistance = 12
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
        Me.LayoutControlItem31.Size = New System.Drawing.Size(799, 40)
        Me.LayoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem31.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem31.TextVisible = False
        '
        'INDgcImages
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcImages, Nothing)
        Me.IndigoGridControl2.SetAddActions(Me.INDgcImages, Nothing)
        Me.IndigoGridControl2.SetControlNextFocus(Me.INDgcImages, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcImages, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcImages, False)
        Me.IndigoGridControl2.SetExportButton(Me.INDgcImages, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcImages, True)
        Me.IndigoGridControl2.SetGuardarXml(Me.INDgcImages, True)
        Me.IndigoGridControl2.SetHoldSize(Me.INDgcImages, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcImages, False)
        Me.IndigoGridControl2.SetHotTrack(Me.INDgcImages, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcImages, False)
        Me.INDgcImages.Location = New System.Drawing.Point(24, 156)
        Me.INDgcImages.MainView = Me.INDviewImages
        Me.INDgcImages.MenuManager = Me.BarManager1
        Me.INDgcImages.Name = "INDgcImages"
        Me.INDgcImages.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectionImages, Me.INDrepIcbeGeneratedImages})
        Me.INDgcImages.Size = New System.Drawing.Size(1156, 369)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcImages, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl2.SetSizeConstraintsType(Me.INDgcImages, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcImages.TabIndex = 40
        Me.INDgcImages.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewImages})
        '
        'INDviewImages
        '
        Me.INDviewImages.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewImages.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewImages.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewImages.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewImages.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewImages.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewImages.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewImages.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewImages.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewImages.Appearance.Row.Options.UseFont = True
        Me.INDviewImages.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewImages.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewImages.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn21, Me.GridColumn22, Me.GridColumn23, Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn41, Me.GridColumn53})
        Me.INDviewImages.GridControl = Me.INDgcImages
        Me.INDviewImages.Name = "INDviewImages"
        Me.INDviewImages.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewImages.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewImages.OptionsView.ShowAutoFilterRow = True
        Me.INDviewImages.OptionsView.ShowDetailButtons = False
        Me.INDviewImages.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewImages, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewImages, False)
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Sel."
        Me.GridColumn21.ColumnEdit = Me.INDrepCheckSelectionImages
        Me.GridColumn21.FieldName = "Selection"
        Me.GridColumn21.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn21.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn21.OptionsColumn.AllowMove = False
        Me.GridColumn21.OptionsColumn.AllowShowHide = False
        Me.GridColumn21.OptionsColumn.AllowSize = False
        Me.GridColumn21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn21.OptionsColumn.FixedWidth = True
        Me.GridColumn21.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn21.OptionsFilter.AllowFilter = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 0
        Me.GridColumn21.Width = 28
        '
        'INDrepCheckSelectionImages
        '
        Me.INDrepCheckSelectionImages.AutoHeight = False
        Me.INDrepCheckSelectionImages.Name = "INDrepCheckSelectionImages"
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Fecha"
        Me.GridColumn22.FieldName = "MedicalOrderDate"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.OptionsColumn.AllowFocus = False
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 1
        Me.GridColumn22.Width = 115
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "CUPS"
        Me.GridColumn23.FieldName = "CupsDescription"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.OptionsColumn.AllowFocus = False
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 2
        Me.GridColumn23.Width = 466
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Folio"
        Me.GridColumn24.FieldName = "FolioNumber"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsColumn.AllowFocus = False
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 3
        Me.GridColumn24.Width = 55
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Profesional"
        Me.GridColumn25.FieldName = "ProfessionalDescription"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.OptionsColumn.AllowFocus = False
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 4
        Me.GridColumn25.Width = 398
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Cantidad"
        Me.GridColumn26.FieldName = "Quantity"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.OptionsColumn.AllowEdit = False
        Me.GridColumn26.OptionsColumn.AllowFocus = False
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 5
        Me.GridColumn26.Width = 99
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "RIAS"
        Me.GridColumn41.FieldName = "RiasDescription"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.OptionsColumn.AllowEdit = False
        Me.GridColumn41.OptionsColumn.AllowFocus = False
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 6
        Me.GridColumn41.Width = 212
        '
        'INDrepIcbeGeneratedImages
        '
        Me.INDrepIcbeGeneratedImages.Appearance.Options.UseTextOptions = True
        Me.INDrepIcbeGeneratedImages.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedImages.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDrepIcbeGeneratedImages.AutoHeight = False
        Me.INDrepIcbeGeneratedImages.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedImages.HtmlImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedImages.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", True, 1)})
        Me.INDrepIcbeGeneratedImages.LargeImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedImages.Name = "INDrepIcbeGeneratedImages"
        Me.INDrepIcbeGeneratedImages.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDrepIcbeGeneratedImages.SmallImages = Me.ImageCollection1
        '
        'INDgcPathologies
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcPathologies, Nothing)
        Me.IndigoGridControl2.SetAddActions(Me.INDgcPathologies, Nothing)
        Me.IndigoGridControl2.SetControlNextFocus(Me.INDgcPathologies, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcPathologies, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcPathologies, False)
        Me.IndigoGridControl2.SetExportButton(Me.INDgcPathologies, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcPathologies, True)
        Me.IndigoGridControl2.SetGuardarXml(Me.INDgcPathologies, True)
        Me.IndigoGridControl2.SetHoldSize(Me.INDgcPathologies, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcPathologies, False)
        Me.IndigoGridControl2.SetHotTrack(Me.INDgcPathologies, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcPathologies, False)
        Me.INDgcPathologies.Location = New System.Drawing.Point(24, 156)
        Me.INDgcPathologies.MainView = Me.INDviewPathologies
        Me.INDgcPathologies.MenuManager = Me.BarManager1
        Me.INDgcPathologies.Name = "INDgcPathologies"
        Me.INDgcPathologies.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectionPathologies, Me.INDrepIcbeGeneratedPathologies})
        Me.INDgcPathologies.Size = New System.Drawing.Size(1156, 369)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcPathologies, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl2.SetSizeConstraintsType(Me.INDgcPathologies, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcPathologies.TabIndex = 39
        Me.INDgcPathologies.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewPathologies})
        '
        'INDviewPathologies
        '
        Me.INDviewPathologies.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewPathologies.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewPathologies.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewPathologies.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewPathologies.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewPathologies.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewPathologies.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewPathologies.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewPathologies.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewPathologies.Appearance.Row.Options.UseFont = True
        Me.INDviewPathologies.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewPathologies.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewPathologies.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18, Me.GridColumn19, Me.GridColumn20, Me.GridColumn40, Me.GridColumn52})
        Me.INDviewPathologies.GridControl = Me.INDgcPathologies
        Me.INDviewPathologies.Name = "INDviewPathologies"
        Me.INDviewPathologies.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewPathologies.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewPathologies.OptionsView.ShowAutoFilterRow = True
        Me.INDviewPathologies.OptionsView.ShowDetailButtons = False
        Me.INDviewPathologies.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewPathologies, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewPathologies, False)
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Sel."
        Me.GridColumn15.ColumnEdit = Me.INDrepCheckSelectionPathologies
        Me.GridColumn15.FieldName = "Selection"
        Me.GridColumn15.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn15.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn15.OptionsColumn.AllowMove = False
        Me.GridColumn15.OptionsColumn.AllowShowHide = False
        Me.GridColumn15.OptionsColumn.AllowSize = False
        Me.GridColumn15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn15.OptionsColumn.FixedWidth = True
        Me.GridColumn15.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn15.OptionsFilter.AllowFilter = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 0
        Me.GridColumn15.Width = 30
        '
        'INDrepCheckSelectionPathologies
        '
        Me.INDrepCheckSelectionPathologies.AutoHeight = False
        Me.INDrepCheckSelectionPathologies.Name = "INDrepCheckSelectionPathologies"
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Fecha"
        Me.GridColumn16.FieldName = "MedicalOrderDate"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 1
        Me.GridColumn16.Width = 70
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "CUPS"
        Me.GridColumn17.FieldName = "CupsDescription"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 2
        Me.GridColumn17.Width = 295
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Folio"
        Me.GridColumn18.FieldName = "FolioNumber"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 3
        Me.GridColumn18.Width = 40
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Profesional"
        Me.GridColumn19.FieldName = "ProfessionalDescription"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 4
        Me.GridColumn19.Width = 276
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Cantidad"
        Me.GridColumn20.FieldName = "Quantity"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.OptionsColumn.AllowEdit = False
        Me.GridColumn20.OptionsColumn.AllowFocus = False
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 5
        Me.GridColumn20.Width = 67
        '
        'GridColumn40
        '
        Me.GridColumn40.Caption = "RIAS"
        Me.GridColumn40.FieldName = "RiasDescription"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.OptionsColumn.AllowEdit = False
        Me.GridColumn40.OptionsColumn.AllowFocus = False
        Me.GridColumn40.Visible = True
        Me.GridColumn40.VisibleIndex = 6
        Me.GridColumn40.Width = 178
        '
        'INDrepIcbeGeneratedPathologies
        '
        Me.INDrepIcbeGeneratedPathologies.Appearance.Options.UseTextOptions = True
        Me.INDrepIcbeGeneratedPathologies.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedPathologies.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDrepIcbeGeneratedPathologies.AutoHeight = False
        Me.INDrepIcbeGeneratedPathologies.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedPathologies.HtmlImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedPathologies.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", True, 1)})
        Me.INDrepIcbeGeneratedPathologies.LargeImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedPathologies.Name = "INDrepIcbeGeneratedPathologies"
        Me.INDrepIcbeGeneratedPathologies.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDrepIcbeGeneratedPathologies.SmallImages = Me.ImageCollection1
        '
        'INDgcLaboratories
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLaboratories, Nothing)
        Me.IndigoGridControl2.SetAddActions(Me.INDgcLaboratories, Nothing)
        Me.IndigoGridControl2.SetControlNextFocus(Me.INDgcLaboratories, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLaboratories, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLaboratories, False)
        Me.IndigoGridControl2.SetExportButton(Me.INDgcLaboratories, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLaboratories, True)
        Me.IndigoGridControl2.SetGuardarXml(Me.INDgcLaboratories, True)
        Me.IndigoGridControl2.SetHoldSize(Me.INDgcLaboratories, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLaboratories, False)
        Me.IndigoGridControl2.SetHotTrack(Me.INDgcLaboratories, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLaboratories, False)
        Me.INDgcLaboratories.Location = New System.Drawing.Point(24, 156)
        Me.INDgcLaboratories.MainView = Me.INDviewLaboratories
        Me.INDgcLaboratories.MenuManager = Me.BarManager1
        Me.INDgcLaboratories.Name = "INDgcLaboratories"
        Me.INDgcLaboratories.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectionLaboratories, Me.INDrepIcbeGeneratedLaboratories})
        Me.INDgcLaboratories.Size = New System.Drawing.Size(1156, 369)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLaboratories, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl2.SetSizeConstraintsType(Me.INDgcLaboratories, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcLaboratories.TabIndex = 38
        Me.INDgcLaboratories.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewLaboratories})
        '
        'INDviewLaboratories
        '
        Me.INDviewLaboratories.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewLaboratories.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewLaboratories.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewLaboratories.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewLaboratories.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewLaboratories.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewLaboratories.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewLaboratories.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewLaboratories.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewLaboratories.Appearance.Row.Options.UseFont = True
        Me.INDviewLaboratories.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewLaboratories.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewLaboratories.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn14, Me.GridColumn39, Me.GridColumn51})
        Me.INDviewLaboratories.GridControl = Me.INDgcLaboratories
        Me.INDviewLaboratories.Name = "INDviewLaboratories"
        Me.INDviewLaboratories.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewLaboratories.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewLaboratories.OptionsView.ShowAutoFilterRow = True
        Me.INDviewLaboratories.OptionsView.ShowDetailButtons = False
        Me.INDviewLaboratories.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewLaboratories, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewLaboratories, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Sel."
        Me.GridColumn9.ColumnEdit = Me.INDrepCheckSelectionLaboratories
        Me.GridColumn9.FieldName = "Selection"
        Me.GridColumn9.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn9.OptionsColumn.AllowMove = False
        Me.GridColumn9.OptionsColumn.AllowShowHide = False
        Me.GridColumn9.OptionsColumn.AllowSize = False
        Me.GridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn9.OptionsColumn.FixedWidth = True
        Me.GridColumn9.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn9.OptionsFilter.AllowFilter = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        Me.GridColumn9.Width = 28
        '
        'INDrepCheckSelectionLaboratories
        '
        Me.INDrepCheckSelectionLaboratories.AutoHeight = False
        Me.INDrepCheckSelectionLaboratories.Name = "INDrepCheckSelectionLaboratories"
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Fec. Sol."
        Me.GridColumn10.FieldName = "MedicalOrderDate"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Servicio"
        Me.GridColumn11.FieldName = "CupsDescription"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 2
        Me.GridColumn11.Width = 475
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Médico"
        Me.GridColumn12.FieldName = "ProfessionalDescription"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 3
        Me.GridColumn12.Width = 324
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Cantidad"
        Me.GridColumn13.FieldName = "Quantity"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 4
        Me.GridColumn13.Width = 89
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Folio"
        Me.GridColumn14.FieldName = "FolioNumber"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 5
        Me.GridColumn14.Width = 103
        '
        'GridColumn39
        '
        Me.GridColumn39.Caption = "RIAS"
        Me.GridColumn39.FieldName = "RiasDescription"
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.OptionsColumn.AllowEdit = False
        Me.GridColumn39.OptionsColumn.AllowFocus = False
        Me.GridColumn39.Visible = True
        Me.GridColumn39.VisibleIndex = 6
        Me.GridColumn39.Width = 232
        '
        'INDrepIcbeGeneratedLaboratories
        '
        Me.INDrepIcbeGeneratedLaboratories.Appearance.Options.UseTextOptions = True
        Me.INDrepIcbeGeneratedLaboratories.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedLaboratories.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDrepIcbeGeneratedLaboratories.AutoHeight = False
        Me.INDrepIcbeGeneratedLaboratories.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGeneratedLaboratories.HtmlImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedLaboratories.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", True, 1)})
        Me.INDrepIcbeGeneratedLaboratories.LargeImages = Me.ImageCollection1
        Me.INDrepIcbeGeneratedLaboratories.Name = "INDrepIcbeGeneratedLaboratories"
        Me.INDrepIcbeGeneratedLaboratories.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDrepIcbeGeneratedLaboratories.SmallImages = Me.ImageCollection1
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
        Me.DdbMenu.Location = New System.Drawing.Point(703, 12)
        Me.DdbMenu.Margin = New System.Windows.Forms.Padding(0)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.DdbMenu, False)
        Me.DdbMenu.Name = "DdbMenu"
        Me.DdbMenu.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[False]
        Me.DdbMenu.Size = New System.Drawing.Size(68, 64)
        Me.DdbMenu.StyleController = Me.INDlyRoot
        Me.DdbMenu.TabIndex = 37
        Me.DdbMenu.ToolTip = "Click para desplegar el menú de acciones"
        Me.DdbMenu.ToolTipTitle = "Menú de Acciones"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnCurrentAdmission), New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnNewAdmission)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'INDgcOdontologyProcedures
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcOdontologyProcedures, Nothing)
        Me.IndigoGridControl2.SetAddActions(Me.INDgcOdontologyProcedures, Nothing)
        Me.IndigoGridControl2.SetControlNextFocus(Me.INDgcOdontologyProcedures, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcOdontologyProcedures, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcOdontologyProcedures, False)
        Me.IndigoGridControl2.SetExportButton(Me.INDgcOdontologyProcedures, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcOdontologyProcedures, True)
        Me.IndigoGridControl2.SetGuardarXml(Me.INDgcOdontologyProcedures, True)
        Me.IndigoGridControl2.SetHoldSize(Me.INDgcOdontologyProcedures, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcOdontologyProcedures, False)
        Me.IndigoGridControl2.SetHotTrack(Me.INDgcOdontologyProcedures, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcOdontologyProcedures, False)
        Me.INDgcOdontologyProcedures.Location = New System.Drawing.Point(24, 156)
        Me.INDgcOdontologyProcedures.MainView = Me.INDviewOdontologyProcedure
        Me.INDgcOdontologyProcedures.Name = "INDgcOdontologyProcedures"
        Me.INDgcOdontologyProcedures.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelection, Me.INDrepIcbeGenerated})
        Me.INDgcOdontologyProcedures.Size = New System.Drawing.Size(1156, 369)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcOdontologyProcedures, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl2.SetSizeConstraintsType(Me.INDgcOdontologyProcedures, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcOdontologyProcedures.TabIndex = 23
        Me.INDgcOdontologyProcedures.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewOdontologyProcedure})
        '
        'INDviewOdontologyProcedure
        '
        Me.INDviewOdontologyProcedure.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewOdontologyProcedure.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewOdontologyProcedure.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewOdontologyProcedure.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewOdontologyProcedure.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewOdontologyProcedure.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewOdontologyProcedure.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewOdontologyProcedure.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewOdontologyProcedure.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewOdontologyProcedure.Appearance.Row.Options.UseFont = True
        Me.INDviewOdontologyProcedure.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewOdontologyProcedure.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewOdontologyProcedure.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn38})
        Me.INDviewOdontologyProcedure.GridControl = Me.INDgcOdontologyProcedures
        Me.INDviewOdontologyProcedure.Name = "INDviewOdontologyProcedure"
        Me.INDviewOdontologyProcedure.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewOdontologyProcedure.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewOdontologyProcedure.OptionsView.ShowAutoFilterRow = True
        Me.INDviewOdontologyProcedure.OptionsView.ShowDetailButtons = False
        Me.INDviewOdontologyProcedure.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewOdontologyProcedure, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewOdontologyProcedure, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Sel."
        Me.GridColumn1.ColumnEdit = Me.INDrepCheckSelection
        Me.GridColumn1.FieldName = "Selection"
        Me.GridColumn1.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.AllowMove = False
        Me.GridColumn1.OptionsColumn.AllowShowHide = False
        Me.GridColumn1.OptionsColumn.AllowSize = False
        Me.GridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.FixedWidth = True
        Me.GridColumn1.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn1.OptionsFilter.AllowFilter = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 37
        '
        'INDrepCheckSelection
        '
        Me.INDrepCheckSelection.AutoHeight = False
        Me.INDrepCheckSelection.Name = "INDrepCheckSelection"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Procedimiento"
        Me.GridColumn2.FieldName = "ProcedureName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 228
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "CUPS"
        Me.GridColumn3.FieldName = "CupsDescription"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 251
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cantidad"
        Me.GridColumn4.FieldName = "Quantity"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Folio"
        Me.GridColumn5.FieldName = "FolioNumber"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 57
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Fecha"
        Me.GridColumn6.FieldName = "RegisterDate"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 67
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Profesional"
        Me.GridColumn7.FieldName = "ProfessionalDescription"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 254
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Unidad Funcional"
        Me.GridColumn8.FieldName = "FunctionalUnitDescription"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        Me.GridColumn8.Width = 243
        '
        'GridColumn38
        '
        Me.GridColumn38.Caption = "RIAS"
        Me.GridColumn38.FieldName = "RiasDescription"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.OptionsColumn.AllowEdit = False
        Me.GridColumn38.OptionsColumn.AllowFocus = False
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 8
        Me.GridColumn38.Width = 180
        '
        'INDrepIcbeGenerated
        '
        Me.INDrepIcbeGenerated.Appearance.Options.UseTextOptions = True
        Me.INDrepIcbeGenerated.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGenerated.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDrepIcbeGenerated.AutoHeight = False
        Me.INDrepIcbeGenerated.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepIcbeGenerated.HtmlImages = Me.ImageCollection1
        Me.INDrepIcbeGenerated.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", True, 1)})
        Me.INDrepIcbeGenerated.LargeImages = Me.ImageCollection1
        Me.INDrepIcbeGenerated.Name = "INDrepIcbeGenerated"
        Me.INDrepIcbeGenerated.ShowToolTipForTrimmedText = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDrepIcbeGenerated.SmallImages = Me.ImageCollection1
        '
        'INDtxtDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDate, False)
        Me.INDtxtDate.Location = New System.Drawing.Point(572, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDate.Name = "INDtxtDate"
        Me.INDtxtDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDate.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDate.Properties.ReadOnly = True
        Me.INDtxtDate.Size = New System.Drawing.Size(126, 28)
        Me.INDtxtDate.StyleController = Me.INDlyRoot
        Me.INDtxtDate.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDate, 0)
        '
        'INDtxtFor
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtFor, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtFor, False)
        Me.INDtxtFor.Location = New System.Drawing.Point(382, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtFor, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtFor.Name = "INDtxtFor"
        Me.INDtxtFor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtFor.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFor.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtFor.Properties.Appearance.Options.UseFont = True
        Me.INDtxtFor.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFor.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtFor.Properties.ReadOnly = True
        Me.INDtxtFor.Size = New System.Drawing.Size(136, 28)
        Me.INDtxtFor.StyleController = Me.INDlyRoot
        Me.INDtxtFor.TabIndex = 21
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtFor, 0)
        '
        'INDlblStatus
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblStatus, True)
        Me.INDlblStatus.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblStatus.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblStatus.Appearance.Options.UseFont = True
        Me.INDlblStatus.Appearance.Options.UseForeColor = True
        Me.INDlblStatus.Appearance.Options.UseTextOptions = True
        Me.INDlblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblStatus, False)
        Me.INDlblStatus.Location = New System.Drawing.Point(65, 48)
        Me.INDlblStatus.Name = "INDlblStatus"
        Me.INDlblStatus.Size = New System.Drawing.Size(93, 32)
        Me.INDlblStatus.StyleController = Me.INDlyRoot
        Me.INDlblStatus.TabIndex = 19
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
        Me.INDSleAdmissionNumber2.IsReadOnly = True
        Me.INDSleAdmissionNumber2.Location = New System.Drawing.Point(74, 12)
        Me.INDSleAdmissionNumber2.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleAdmissionNumber2.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleAdmissionNumber2.Name = "INDSleAdmissionNumber2"
        Me.INDSleAdmissionNumber2.PopupContainerControl = Me.INDpanelAdmission
        Me.INDSleAdmissionNumber2.PopUpFormSize = New System.Drawing.Size(1000, 300)
        Me.INDSleAdmissionNumber2.Size = New System.Drawing.Size(625, 28)
        Me.INDSleAdmissionNumber2.TabIndex = 18
        Me.INDSleAdmissionNumber2.ValueMember = "AdmissionCode"
        Me.INDSleAdmissionNumber2.View = Me.SearchLookUpEditExAdmissionView1
        '
        'SearchLookUpEditExAdmissionView1
        '
        Me.SearchLookUpEditExAdmissionView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExAdmissionView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExAdmissionView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExAdmissionView1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExAdmissionView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExAdmissionView1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExAdmissionView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExAdmissionView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExAdmissionView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExAdmissionView1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExAdmissionView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExAdmissionView1.Name = "SearchLookUpEditExAdmissionView1"
        Me.SearchLookUpEditExAdmissionView1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExAdmissionView1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExAdmissionView1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExAdmissionView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExAdmissionView1, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEditExAdmissionView1, False)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAdmission, Me.INDlyItemStatus, Me.INDlyItemType, Me.INDlyItemFor, Me.INDlyItemDate, Me.INDtcgInfo, Me.EmptySpaceItem1, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1204, 549)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemAdmission
        '
        Me.INDlyItemAdmission.Control = Me.INDSleAdmissionNumber2
        Me.INDlyItemAdmission.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAdmission.MaxSize = New System.Drawing.Size(691, 36)
        Me.INDlyItemAdmission.MinSize = New System.Drawing.Size(691, 36)
        Me.INDlyItemAdmission.Name = "INDlyItemAdmission"
        Me.INDlyItemAdmission.Size = New System.Drawing.Size(691, 36)
        Me.INDlyItemAdmission.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAdmission.Text = "Ingreso"
        Me.INDlyItemAdmission.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAdmission.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemAdmission.TextToControlDistance = 12
        '
        'INDlyItemStatus
        '
        Me.INDlyItemStatus.Control = Me.INDlblStatus
        Me.INDlyItemStatus.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemStatus.MaxSize = New System.Drawing.Size(150, 36)
        Me.INDlyItemStatus.MinSize = New System.Drawing.Size(150, 36)
        Me.INDlyItemStatus.Name = "INDlyItemStatus"
        Me.INDlyItemStatus.Size = New System.Drawing.Size(150, 36)
        Me.INDlyItemStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemStatus.Text = "Estado"
        Me.INDlyItemStatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemStatus.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemStatus.TextToControlDistance = 3
        '
        'INDlyItemType
        '
        Me.INDlyItemType.Control = Me.INDicceType
        Me.INDlyItemType.Location = New System.Drawing.Point(150, 36)
        Me.INDlyItemType.MaxSize = New System.Drawing.Size(180, 36)
        Me.INDlyItemType.MinSize = New System.Drawing.Size(180, 36)
        Me.INDlyItemType.Name = "INDlyItemType"
        Me.INDlyItemType.Size = New System.Drawing.Size(180, 36)
        Me.INDlyItemType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemType.Text = "Tipo"
        Me.INDlyItemType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemType.TextSize = New System.Drawing.Size(35, 21)
        Me.INDlyItemType.TextToControlDistance = 5
        '
        'INDlyItemFor
        '
        Me.INDlyItemFor.Control = Me.INDtxtFor
        Me.INDlyItemFor.Location = New System.Drawing.Point(330, 36)
        Me.INDlyItemFor.MaxSize = New System.Drawing.Size(180, 36)
        Me.INDlyItemFor.MinSize = New System.Drawing.Size(180, 36)
        Me.INDlyItemFor.Name = "INDlyItemFor"
        Me.INDlyItemFor.Size = New System.Drawing.Size(180, 36)
        Me.INDlyItemFor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFor.Text = "Por"
        Me.INDlyItemFor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFor.TextSize = New System.Drawing.Size(35, 21)
        Me.INDlyItemFor.TextToControlDistance = 5
        '
        'INDlyItemDate
        '
        Me.INDlyItemDate.Control = Me.INDtxtDate
        Me.INDlyItemDate.Location = New System.Drawing.Point(510, 36)
        Me.INDlyItemDate.MaxSize = New System.Drawing.Size(180, 36)
        Me.INDlyItemDate.MinSize = New System.Drawing.Size(150, 36)
        Me.INDlyItemDate.Name = "INDlyItemDate"
        Me.INDlyItemDate.Size = New System.Drawing.Size(181, 36)
        Me.INDlyItemDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDate.Text = "Fecha"
        Me.INDlyItemDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDate.TextSize = New System.Drawing.Size(45, 21)
        Me.INDlyItemDate.TextToControlDistance = 5
        '
        'INDtcgInfo
        '
        Me.INDtcgInfo.Location = New System.Drawing.Point(0, 102)
        Me.INDtcgInfo.Name = "INDtcgInfo"
        Me.INDtcgInfo.SelectedTabPage = Me.INDlcgPathologies
        Me.INDtcgInfo.Size = New System.Drawing.Size(1184, 427)
        Me.INDtcgInfo.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgOdontologyProcedures, Me.INDlcgLaboratories, Me.INDlcgPathologies, Me.INDlcgImages, Me.INDlcgInterconsults, Me.INDlcgAdministrativeNotes})
        '
        'INDlcgOdontologyProcedures
        '
        Me.INDlcgOdontologyProcedures.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOdontologyProcedures.AppearanceGroup.Options.UseFont = True
        Me.INDlcgOdontologyProcedures.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOdontologyProcedures.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOdontologyProcedures.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgOdontologyProcedures, False)
        Me.INDlcgOdontologyProcedures.CustomizationFormText = "Procedimientos Odontológicos"
        Me.INDlcgOdontologyProcedures.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItem})
        Me.INDlcgOdontologyProcedures.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgOdontologyProcedures.Name = "INDlcgOdontologyProcedures"
        Me.INDlcgOdontologyProcedures.Size = New System.Drawing.Size(1160, 373)
        Me.INDlcgOdontologyProcedures.Text = "Procedimientos Odontológicos"
        '
        'INDlyItem
        '
        Me.INDlyItem.Control = Me.INDgcOdontologyProcedures
        Me.INDlyItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItem.Name = "INDlyItem"
        Me.INDlyItem.Size = New System.Drawing.Size(1160, 373)
        Me.INDlyItem.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItem.TextVisible = False
        '
        'INDlcgLaboratories
        '
        Me.INDlcgLaboratories.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgLaboratories.AppearanceGroup.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgLaboratories.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgLaboratories.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgLaboratories.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgLaboratories, False)
        Me.INDlcgLaboratories.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemLaboratories})
        Me.INDlcgLaboratories.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgLaboratories.Name = "INDlcgLaboratories"
        Me.INDlcgLaboratories.Size = New System.Drawing.Size(1160, 373)
        Me.INDlcgLaboratories.Text = "Laboratorios Ordenados"
        '
        'INDlyItemLaboratories
        '
        Me.INDlyItemLaboratories.Control = Me.INDgcLaboratories
        Me.INDlyItemLaboratories.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemLaboratories.Name = "INDlyItemLaboratories"
        Me.INDlyItemLaboratories.Size = New System.Drawing.Size(1160, 373)
        Me.INDlyItemLaboratories.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemLaboratories.TextVisible = False
        '
        'INDlcgPathologies
        '
        Me.INDlcgPathologies.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPathologies.AppearanceGroup.Options.UseFont = True
        Me.INDlcgPathologies.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPathologies.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgPathologies.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPathologies.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgPathologies.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgPathologies.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgPathologies.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPathologies.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgPathologies.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPathologies.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgPathologies.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPathologies.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgPathologies, False)
        Me.INDlcgPathologies.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPathologies})
        Me.INDlcgPathologies.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgPathologies.Name = "INDlcgPathologies"
        Me.INDlcgPathologies.Size = New System.Drawing.Size(1160, 373)
        Me.INDlcgPathologies.Text = "Patologías Ordenadas"
        '
        'INDlyItemPathologies
        '
        Me.INDlyItemPathologies.Control = Me.INDgcPathologies
        Me.INDlyItemPathologies.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPathologies.Name = "INDlyItemPathologies"
        Me.INDlyItemPathologies.Size = New System.Drawing.Size(1160, 373)
        Me.INDlyItemPathologies.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemPathologies.TextVisible = False
        '
        'INDlcgImages
        '
        Me.INDlcgImages.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgImages.AppearanceGroup.Options.UseFont = True
        Me.INDlcgImages.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgImages.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgImages.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgImages.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgImages.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgImages.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgImages.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgImages.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgImages.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgImages.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgImages.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgImages.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgImages, False)
        Me.INDlcgImages.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemImages})
        Me.INDlcgImages.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgImages.Name = "INDlcgImages"
        Me.INDlcgImages.Size = New System.Drawing.Size(1160, 373)
        Me.INDlcgImages.Text = "Imágenes Dx Ordenadas"
        '
        'INDlyItemImages
        '
        Me.INDlyItemImages.Control = Me.INDgcImages
        Me.INDlyItemImages.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemImages.Name = "INDlyItemImages"
        Me.INDlyItemImages.Size = New System.Drawing.Size(1160, 373)
        Me.INDlyItemImages.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemImages.TextVisible = False
        '
        'INDlcgInterconsults
        '
        Me.INDlcgInterconsults.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgInterconsults.AppearanceGroup.Options.UseFont = True
        Me.INDlcgInterconsults.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgInterconsults.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgInterconsults.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInterconsults.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgInterconsults.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgInterconsults.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgInterconsults.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInterconsults.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgInterconsults.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInterconsults.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgInterconsults.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInterconsults.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgInterconsults, False)
        Me.INDlcgInterconsults.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemInterconsults})
        Me.INDlcgInterconsults.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgInterconsults.Name = "INDlcgInterconsults"
        Me.INDlcgInterconsults.Size = New System.Drawing.Size(1160, 373)
        Me.INDlcgInterconsults.Text = "Interconsultas"
        '
        'INDlyItemInterconsults
        '
        Me.INDlyItemInterconsults.Control = Me.INDgcInterconsults
        Me.INDlyItemInterconsults.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemInterconsults.Name = "INDlyItemInterconsults"
        Me.INDlyItemInterconsults.Size = New System.Drawing.Size(1160, 373)
        Me.INDlyItemInterconsults.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemInterconsults.TextVisible = False
        '
        'INDlcgAdministrativeNotes
        '
        Me.INDlcgAdministrativeNotes.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgAdministrativeNotes.AppearanceGroup.Options.UseFont = True
        Me.INDlcgAdministrativeNotes.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgAdministrativeNotes.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAdministrativeNotes.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgAdministrativeNotes, False)
        Me.INDlcgAdministrativeNotes.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAdministrativeNotes})
        Me.INDlcgAdministrativeNotes.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgAdministrativeNotes.Name = "INDlcgAdministrativeNotes"
        Me.INDlcgAdministrativeNotes.Size = New System.Drawing.Size(1160, 373)
        Me.INDlcgAdministrativeNotes.Text = "Notas Administrativas"
        '
        'INDlyItemAdministrativeNotes
        '
        Me.INDlyItemAdministrativeNotes.Control = Me.INDgcAdministrativeNotes
        Me.INDlyItemAdministrativeNotes.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAdministrativeNotes.Name = "INDlyItemAdministrativeNotes"
        Me.INDlyItemAdministrativeNotes.Size = New System.Drawing.Size(1160, 373)
        Me.INDlyItemAdministrativeNotes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAdministrativeNotes.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 72)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(0, 30)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(104, 30)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(691, 30)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.DdbMenu
        Me.LayoutControlItem1.Location = New System.Drawing.Point(691, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(72, 68)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(72, 68)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(493, 102)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "Descripción Relacionada"
        Me.GridColumn51.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn51.Name = "GridColumn51"
        Me.GridColumn51.OptionsColumn.AllowEdit = False
        Me.GridColumn51.OptionsColumn.AllowFocus = False
        Me.GridColumn51.Visible = True
        Me.GridColumn51.VisibleIndex = 7
        '
        'GridColumn52
        '
        Me.GridColumn52.Caption = "Descripción Relacionada"
        Me.GridColumn52.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.OptionsColumn.AllowEdit = False
        Me.GridColumn52.OptionsColumn.AllowFocus = False
        Me.GridColumn52.Visible = True
        Me.GridColumn52.VisibleIndex = 7
        Me.GridColumn52.Width = 182
        '
        'GridColumn53
        '
        Me.GridColumn53.Caption = "Descripción Relacionada"
        Me.GridColumn53.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn53.Name = "GridColumn53"
        Me.GridColumn53.OptionsColumn.AllowEdit = False
        Me.GridColumn53.OptionsColumn.AllowFocus = False
        Me.GridColumn53.Visible = True
        Me.GridColumn53.VisibleIndex = 7
        Me.GridColumn53.Width = 172
        '
        'GridColumn54
        '
        Me.GridColumn54.Caption = "Descripción Relacionada"
        Me.GridColumn54.FieldName = "ContractDescriptionCodeName"
        Me.GridColumn54.Name = "GridColumn54"
        Me.GridColumn54.OptionsColumn.AllowEdit = False
        Me.GridColumn54.OptionsColumn.AllowFocus = False
        Me.GridColumn54.Visible = True
        Me.GridColumn54.VisibleIndex = 7
        Me.GridColumn54.Width = 165
        '
        'FrmAccountControlAmbulatoryDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 680)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAccountControlAmbulatoryDetail"
        Me.Opacity = 1.0R
        Me.Text = "Detalles del Ingreso"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDicceType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDgcAdministrativeNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAdministrativeNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectionAdministrativeNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeGeneratedAdministrativeNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcInterconsults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewInterconsults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectionInterconsults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeGeneratedInterconsults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelAdmission.ResumeLayout(False)
        CType(Me.LycPccAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LycPccAdmission.ResumeLayout(False)
        CType(Me.INDgcAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAppointment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtContact.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientEstrato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientEntityName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCareGroupPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientAge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientBirth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAfiliationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRiskType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCareGroupAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBedStay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEntityNameAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAtentionCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAuthorization.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPlaceEntry.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFunctionalUnitAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgPccAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtcgAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgAppointment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LiCareGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LiEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtContacto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgAdmissionGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcImages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewImages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectionImages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeGeneratedImages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcPathologies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewPathologies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectionPathologies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeGeneratedPathologies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcLaboratories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewLaboratories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectionLaboratories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeGeneratedLaboratories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcOdontologyProcedures, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewOdontologyProcedure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeGenerated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtFor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExAdmissionView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtcgInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgOdontologyProcedures, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgLaboratories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLaboratories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgPathologies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPathologies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgImages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemImages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgInterconsults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInterconsults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgAdministrativeNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAdministrativeNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleAdmissionNumber2 As SearchLookUpEditExAdmission
    Friend WithEvents SearchLookUpEditExAdmissionView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemAdmission As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlyItemStatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDicceType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents INDlyItemType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtFor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemFor As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView2 As IndigoGridView
    Friend WithEvents INDtcgInfo As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlcgOdontologyProcedures As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridControl2 As IndigoGridControl
    Friend WithEvents INDgcOdontologyProcedures As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewOdontologyProcedure As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpanelAdmission As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LycPccAdmission As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtContact As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientEstrato As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientEntityName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtCareGroupPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientAge As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientBirth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtAfiliationType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtPatientCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents TxtRiskType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtCareGroupAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtBedStay As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtResponsiblePhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtResponsibleName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtEntityNameAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAtentionCenter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAuthorization As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPlaceEntry As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtFunctionalUnitAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAdmissionDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAdmissionType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtLiquidationType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtAdmissionCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LycgPccAdmission As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtcgAppointments As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
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
    Friend WithEvents LayoutControlItem31 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DdbMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents MBtnCurrentAdmission As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MBtnNewAdmission As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents INDrepCheckSelection As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrepIcbeGenerated As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDgcLaboratories As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewLaboratories As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgLaboratories As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemLaboratories As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectionLaboratories As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrepIcbeGeneratedLaboratories As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcPathologies As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewPathologies As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlcgPathologies As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemPathologies As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrepCheckSelectionPathologies As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrepIcbeGeneratedPathologies As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDlcgImages As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcImages As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewImages As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemImages As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectionImages As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrepIcbeGeneratedImages As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDlcgInterconsults As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcInterconsults As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewInterconsults As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectionInterconsults As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepIcbeGeneratedInterconsults As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDlyItemInterconsults As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgAdministrativeNotes As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcAdministrativeNotes As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAdministrativeNotes As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemAdministrativeNotes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectionAdministrativeNotes As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrepIcbeGeneratedAdministrativeNotes As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcAppointments As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAppointment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgAppointment As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemAppointments As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
End Class

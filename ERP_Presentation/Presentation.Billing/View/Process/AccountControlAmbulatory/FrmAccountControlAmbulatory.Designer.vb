Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountControlAmbulatory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccountControlAmbulatory))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcAdmissions = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAdmissions = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSleAdmissionNumber2 = New Presentation.Controls.SearchLookUpEditExAdmission()
        Me.PccAdmission = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LycPccAdmission = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdeOut = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtFunctionalUnitOut = New DevExpress.XtraEditors.TextEdit()
        Me.TxtRiskType = New DevExpress.XtraEditors.TextEdit()
        Me.TxtCareGroupAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtContact = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
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
        Me.LycgPccAdmission = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
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
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem31 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.viewSearchAdmission = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DdbMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.MBtnOpenAdmission = New DevExpress.XtraBars.BarButtonItem()
        Me.MBtnCloseAdmission = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnProcess = New DevExpress.XtraEditors.SimpleButton()
        Me.INDlyItemProcess = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRefresh = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDsleCareCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewSearchCareCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.INDlblNameTitle = New DevExpress.XtraEditors.LabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDpanelSearchCareCenter = New DevExpress.XtraEditors.PanelControl()
        Me.INDpanelPrincipalForm = New DevExpress.XtraEditors.PanelControl()
        Me.INDpcDocumentDetail = New DevExpress.XtraEditors.PanelControl()
        Me.INDpcDocuments = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.CtrNavigation1 = New Presentation.Controls.CtrNavigation()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcAdmissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAdmissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PccAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PccAdmission.SuspendLayout()
        CType(Me.LycPccAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LycPccAdmission.SuspendLayout()
        CType(Me.INDdeOut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtFunctionalUnitOut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRiskType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCareGroupAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtContact.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientEstrato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientEntityName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCareGroupPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientAge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientBirth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBedStay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEntityNameAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAtentionCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBenefitPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPlaceEntry.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFunctionalUnitAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatientType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAfiliationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgPccAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSearchAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDlyItemProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCareCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewSearchCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelSearchCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelSearchCareCenter.SuspendLayout()
        CType(Me.INDpanelPrincipalForm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelPrincipalForm.SuspendLayout()
        CType(Me.INDpcDocumentDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcDocumentDetail.SuspendLayout()
        CType(Me.INDpcDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelPrincipalForm)
        Me.INDPanelControlBase.Controls.Add(Me.INDpcDocumentDetail)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1709, 737)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Size = New System.Drawing.Size(1709, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1709, 130)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.INDlyItemProcess, Me.INDlyItemRefresh})
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(1701, 637)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcAdmissions
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 49)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1677, 564)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDgcAdmissions
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAdmissions, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAdmissions, Nothing)
        Me.INDgcAdmissions.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAdmissions, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAdmissions, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAdmissions, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAdmissions, False)
        Me.INDgcAdmissions.Location = New System.Drawing.Point(14, 63)
        Me.INDgcAdmissions.MainView = Me.INDviewAdmissions
        Me.INDgcAdmissions.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgcAdmissions.Name = "INDgcAdmissions"
        Me.INDgcAdmissions.Size = New System.Drawing.Size(1673, 560)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAdmissions, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcAdmissions.TabIndex = 7
        Me.INDgcAdmissions.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewAdmissions})
        '
        'INDviewAdmissions
        '
        Me.INDviewAdmissions.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewAdmissions.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewAdmissions.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewAdmissions.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewAdmissions.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAdmissions.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewAdmissions.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAdmissions.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewAdmissions.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewAdmissions.Appearance.Row.Options.UseFont = True
        Me.INDviewAdmissions.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAdmissions.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewAdmissions.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDviewAdmissions.DetailHeight = 431
        Me.INDviewAdmissions.GridControl = Me.INDgcAdmissions
        Me.INDviewAdmissions.Name = "INDviewAdmissions"
        Me.INDviewAdmissions.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAdmissions.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAdmissions.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAdmissions.OptionsView.ShowDetailButtons = False
        Me.INDviewAdmissions.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAdmissions, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Ingreso"
        Me.GridColumn3.FieldName = "AdmissionNumber"
        Me.GridColumn3.MinWidth = 23
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 201
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Fecha Ingreso"
        Me.GridColumn4.FieldName = "AdmissionDate"
        Me.GridColumn4.MinWidth = 23
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 196
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Paciente"
        Me.GridColumn5.FieldName = "PatientDescription"
        Me.GridColumn5.MinWidth = 23
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 719
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Unidad Funcional"
        Me.GridColumn6.FieldName = "FuntionalUnitDescription"
        Me.GridColumn6.MinWidth = 23
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        Me.GridColumn6.Width = 509
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSleAdmissionNumber2
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(1007, 49)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(1007, 49)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1007, 49)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Ingreso"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(163, 26)
        Me.LayoutControlItem2.TextToControlDistance = 5
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
        Me.INDSleAdmissionNumber2.Location = New System.Drawing.Point(182, 14)
        Me.INDSleAdmissionNumber2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDSleAdmissionNumber2.MaximumSize = New System.Drawing.Size(5000, 34)
        Me.INDSleAdmissionNumber2.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleAdmissionNumber2.Name = "INDSleAdmissionNumber2"
        Me.INDSleAdmissionNumber2.PopupContainerControl = Me.PccAdmission
        Me.INDSleAdmissionNumber2.PopUpFormSize = New System.Drawing.Size(1000, 300)
        Me.INDSleAdmissionNumber2.Size = New System.Drawing.Size(835, 34)
        Me.INDSleAdmissionNumber2.TabIndex = 35
        Me.INDSleAdmissionNumber2.ValueMember = "AdmissionCode"
        Me.INDSleAdmissionNumber2.View = Me.viewSearchAdmission
        '
        'PccAdmission
        '
        Me.PccAdmission.Controls.Add(Me.LycPccAdmission)
        Me.PccAdmission.Location = New System.Drawing.Point(852, 462)
        Me.PccAdmission.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PccAdmission.Name = "PccAdmission"
        Me.PccAdmission.Size = New System.Drawing.Size(866, 380)
        Me.PccAdmission.TabIndex = 15
        '
        'LycPccAdmission
        '
        Me.LycPccAdmission.AllowCustomization = False
        Me.LycPccAdmission.Controls.Add(Me.INDdeOut)
        Me.LycPccAdmission.Controls.Add(Me.INDtxtFunctionalUnitOut)
        Me.LycPccAdmission.Controls.Add(Me.TxtRiskType)
        Me.LycPccAdmission.Controls.Add(Me.TxtCareGroupAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtContact)
        Me.LycPccAdmission.Controls.Add(Me.LabelControl3)
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
        Me.LycPccAdmission.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LycPccAdmission, False)
        Me.LycPccAdmission.Location = New System.Drawing.Point(0, 0)
        Me.LycPccAdmission.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LycPccAdmission.Name = "LycPccAdmission"
        Me.LycPccAdmission.Root = Me.LycgPccAdmission
        Me.LycPccAdmission.Size = New System.Drawing.Size(866, 380)
        Me.LycPccAdmission.TabIndex = 0
        Me.LycPccAdmission.Text = "LayoutControl1"
        '
        'INDdeOut
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeOut, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeOut, False)
        Me.INDdeOut.Location = New System.Drawing.Point(173, 154)
        Me.INDdeOut.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeOut, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdeOut.Name = "INDdeOut"
        Me.INDdeOut.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDdeOut.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDdeOut.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeOut.Properties.Appearance.Options.UseFont = True
        Me.INDdeOut.Size = New System.Drawing.Size(645, 28)
        Me.INDdeOut.StyleController = Me.LycPccAdmission
        Me.INDdeOut.TabIndex = 35
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeOut, 0)
        '
        'INDtxtFunctionalUnitOut
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtFunctionalUnitOut, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtFunctionalUnitOut, False)
        Me.INDtxtFunctionalUnitOut.Location = New System.Drawing.Point(173, 117)
        Me.INDtxtFunctionalUnitOut.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtFunctionalUnitOut, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtFunctionalUnitOut.Name = "INDtxtFunctionalUnitOut"
        Me.INDtxtFunctionalUnitOut.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtFunctionalUnitOut.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDtxtFunctionalUnitOut.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtFunctionalUnitOut.Properties.Appearance.Options.UseFont = True
        Me.INDtxtFunctionalUnitOut.Size = New System.Drawing.Size(645, 28)
        Me.INDtxtFunctionalUnitOut.StyleController = Me.LycPccAdmission
        Me.INDtxtFunctionalUnitOut.TabIndex = 34
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtFunctionalUnitOut, 0)
        '
        'TxtRiskType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtRiskType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtRiskType, False)
        Me.TxtRiskType.Location = New System.Drawing.Point(173, 191)
        Me.TxtRiskType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtRiskType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtRiskType.Name = "TxtRiskType"
        Me.TxtRiskType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtRiskType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtRiskType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtRiskType.Properties.Appearance.Options.UseFont = True
        Me.TxtRiskType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtRiskType.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtRiskType.Properties.ReadOnly = True
        Me.TxtRiskType.Size = New System.Drawing.Size(200, 28)
        Me.TxtRiskType.StyleController = Me.LycPccAdmission
        Me.TxtRiskType.TabIndex = 33
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtRiskType, 0)
        '
        'TxtCareGroupAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtCareGroupAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtCareGroupAdmission, False)
        Me.TxtCareGroupAdmission.Location = New System.Drawing.Point(173, 154)
        Me.TxtCareGroupAdmission.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtCareGroupAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtCareGroupAdmission.Name = "TxtCareGroupAdmission"
        Me.TxtCareGroupAdmission.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtCareGroupAdmission.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.TxtCareGroupAdmission.Properties.Appearance.Options.UseFont = True
        Me.TxtCareGroupAdmission.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtCareGroupAdmission.Properties.ReadOnly = True
        Me.TxtCareGroupAdmission.Size = New System.Drawing.Size(200, 28)
        Me.TxtCareGroupAdmission.StyleController = Me.LycPccAdmission
        Me.TxtCareGroupAdmission.TabIndex = 32
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtCareGroupAdmission, 0)
        '
        'TxtContact
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtContact, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtContact, False)
        Me.TxtContact.Location = New System.Drawing.Point(533, 265)
        Me.TxtContact.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtContact, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtContact.Name = "TxtContact"
        Me.TxtContact.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtContact.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtContact.Properties.Appearance.Options.UseBackColor = True
        Me.TxtContact.Properties.Appearance.Options.UseFont = True
        Me.TxtContact.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtContact.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtContact.Properties.ReadOnly = True
        Me.TxtContact.Size = New System.Drawing.Size(237, 28)
        Me.TxtContact.StyleController = Me.LycPccAdmission
        Me.TxtContact.TabIndex = 31
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtContact, 0)
        '
        'LabelControl3
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl3, True)
        Me.LabelControl3.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LabelControl3.Appearance.Options.UseBackColor = True
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl3, False)
        Me.LabelControl3.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(821, 49)
        Me.LabelControl3.StyleController = Me.LycPccAdmission
        Me.LabelControl3.TabIndex = 26
        Me.LabelControl3.Text = "Más Información"
        '
        'TxtPatientEstrato
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientEstrato, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientEstrato, False)
        Me.TxtPatientEstrato.Location = New System.Drawing.Point(173, 265)
        Me.TxtPatientEstrato.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientEstrato, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientEstrato.Name = "TxtPatientEstrato"
        Me.TxtPatientEstrato.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientEstrato.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientEstrato.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientEstrato.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientEstrato.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientEstrato.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientEstrato.Properties.ReadOnly = True
        Me.TxtPatientEstrato.Size = New System.Drawing.Size(200, 28)
        Me.TxtPatientEstrato.StyleController = Me.LycPccAdmission
        Me.TxtPatientEstrato.TabIndex = 25
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientEstrato, 0)
        '
        'TxtPatientEntityName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientEntityName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientEntityName, False)
        Me.TxtPatientEntityName.Location = New System.Drawing.Point(533, 228)
        Me.TxtPatientEntityName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientEntityName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientEntityName.Name = "TxtPatientEntityName"
        Me.TxtPatientEntityName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientEntityName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientEntityName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientEntityName.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientEntityName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientEntityName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientEntityName.Properties.ReadOnly = True
        Me.TxtPatientEntityName.Size = New System.Drawing.Size(237, 28)
        Me.TxtPatientEntityName.StyleController = Me.LycPccAdmission
        Me.TxtPatientEntityName.TabIndex = 24
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientEntityName, 0)
        '
        'TxtCareGroupPatient
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtCareGroupPatient, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtCareGroupPatient, False)
        Me.TxtCareGroupPatient.Location = New System.Drawing.Point(173, 228)
        Me.TxtCareGroupPatient.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtCareGroupPatient, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtCareGroupPatient.Name = "TxtCareGroupPatient"
        Me.TxtCareGroupPatient.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtCareGroupPatient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCareGroupPatient.Properties.Appearance.Options.UseBackColor = True
        Me.TxtCareGroupPatient.Properties.Appearance.Options.UseFont = True
        Me.TxtCareGroupPatient.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtCareGroupPatient.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtCareGroupPatient.Properties.ReadOnly = True
        Me.TxtCareGroupPatient.Size = New System.Drawing.Size(200, 28)
        Me.TxtCareGroupPatient.StyleController = Me.LycPccAdmission
        Me.TxtCareGroupPatient.TabIndex = 23
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtCareGroupPatient, 0)
        '
        'TxtPatientAge
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientAge, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientAge, False)
        Me.TxtPatientAge.Location = New System.Drawing.Point(533, 154)
        Me.TxtPatientAge.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientAge, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientAge.Name = "TxtPatientAge"
        Me.TxtPatientAge.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientAge.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientAge.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientAge.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientAge.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientAge.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientAge.Properties.ReadOnly = True
        Me.TxtPatientAge.Size = New System.Drawing.Size(237, 28)
        Me.TxtPatientAge.StyleController = Me.LycPccAdmission
        Me.TxtPatientAge.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientAge, 0)
        '
        'TxtPatientBirth
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientBirth, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientBirth, False)
        Me.TxtPatientBirth.Location = New System.Drawing.Point(173, 154)
        Me.TxtPatientBirth.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientBirth, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientBirth.Name = "TxtPatientBirth"
        Me.TxtPatientBirth.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientBirth.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientBirth.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientBirth.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientBirth.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientBirth.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientBirth.Properties.ReadOnly = True
        Me.TxtPatientBirth.Size = New System.Drawing.Size(200, 28)
        Me.TxtPatientBirth.StyleController = Me.LycPccAdmission
        Me.TxtPatientBirth.TabIndex = 21
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientBirth, 0)
        '
        'TxtPatientName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientName, False)
        Me.TxtPatientName.Location = New System.Drawing.Point(533, 117)
        Me.TxtPatientName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientName.Name = "TxtPatientName"
        Me.TxtPatientName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientName.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientName.Properties.ReadOnly = True
        Me.TxtPatientName.Size = New System.Drawing.Size(237, 28)
        Me.TxtPatientName.StyleController = Me.LycPccAdmission
        Me.TxtPatientName.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientName, 0)
        '
        'TxtPatientCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientCode, False)
        Me.TxtPatientCode.Location = New System.Drawing.Point(173, 117)
        Me.TxtPatientCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientCode.Name = "TxtPatientCode"
        Me.TxtPatientCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPatientCode.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientCode.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientCode.Properties.ReadOnly = True
        Me.TxtPatientCode.Size = New System.Drawing.Size(200, 28)
        Me.TxtPatientCode.StyleController = Me.LycPccAdmission
        Me.TxtPatientCode.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientCode, 0)
        '
        'TxtBedStay
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtBedStay, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtBedStay, False)
        Me.TxtBedStay.Location = New System.Drawing.Point(533, 228)
        Me.TxtBedStay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtBedStay, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtBedStay.Size = New System.Drawing.Size(285, 28)
        Me.TxtBedStay.StyleController = Me.LycPccAdmission
        Me.TxtBedStay.TabIndex = 18
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtBedStay, 0)
        '
        'TxtResponsiblePhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtResponsiblePhone, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtResponsiblePhone, False)
        Me.TxtResponsiblePhone.Location = New System.Drawing.Point(533, 339)
        Me.TxtResponsiblePhone.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtResponsiblePhone, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtResponsiblePhone.Size = New System.Drawing.Size(285, 28)
        Me.TxtResponsiblePhone.StyleController = Me.LycPccAdmission
        Me.TxtResponsiblePhone.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtResponsiblePhone, 0)
        '
        'TxtResponsibleName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtResponsibleName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtResponsibleName, False)
        Me.TxtResponsibleName.Location = New System.Drawing.Point(173, 339)
        Me.TxtResponsibleName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtResponsibleName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtResponsibleName.Size = New System.Drawing.Size(200, 28)
        Me.TxtResponsibleName.StyleController = Me.LycPccAdmission
        Me.TxtResponsibleName.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtResponsibleName, 0)
        '
        'TxtEntityNameAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtEntityNameAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtEntityNameAdmission, False)
        Me.TxtEntityNameAdmission.Location = New System.Drawing.Point(533, 154)
        Me.TxtEntityNameAdmission.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtEntityNameAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtEntityNameAdmission.Size = New System.Drawing.Size(285, 28)
        Me.TxtEntityNameAdmission.StyleController = Me.LycPccAdmission
        Me.TxtEntityNameAdmission.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtEntityNameAdmission, 0)
        '
        'TxtAtentionCenter
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAtentionCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAtentionCenter, False)
        Me.TxtAtentionCenter.Location = New System.Drawing.Point(173, 302)
        Me.TxtAtentionCenter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAtentionCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtAtentionCenter.Size = New System.Drawing.Size(200, 28)
        Me.TxtAtentionCenter.StyleController = Me.LycPccAdmission
        Me.TxtAtentionCenter.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAtentionCenter, 0)
        '
        'TxtBenefitPlan
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtBenefitPlan, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtBenefitPlan, False)
        Me.TxtBenefitPlan.Location = New System.Drawing.Point(533, 265)
        Me.TxtBenefitPlan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtBenefitPlan, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtBenefitPlan.Size = New System.Drawing.Size(285, 28)
        Me.TxtBenefitPlan.StyleController = Me.LycPccAdmission
        Me.TxtBenefitPlan.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtBenefitPlan, 0)
        '
        'TxtPlaceEntry
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPlaceEntry, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPlaceEntry, False)
        Me.TxtPlaceEntry.Location = New System.Drawing.Point(533, 191)
        Me.TxtPlaceEntry.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPlaceEntry, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtPlaceEntry.Size = New System.Drawing.Size(285, 28)
        Me.TxtPlaceEntry.StyleController = Me.LycPccAdmission
        Me.TxtPlaceEntry.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPlaceEntry, 0)
        '
        'TxtFunctionalUnitAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtFunctionalUnitAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtFunctionalUnitAdmission, False)
        Me.TxtFunctionalUnitAdmission.Location = New System.Drawing.Point(533, 302)
        Me.TxtFunctionalUnitAdmission.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtFunctionalUnitAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtFunctionalUnitAdmission.Size = New System.Drawing.Size(285, 28)
        Me.TxtFunctionalUnitAdmission.StyleController = Me.LycPccAdmission
        Me.TxtFunctionalUnitAdmission.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtFunctionalUnitAdmission, 0)
        '
        'TxtAdmissionDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionDate, False)
        Me.TxtAdmissionDate.Location = New System.Drawing.Point(533, 117)
        Me.TxtAdmissionDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtAdmissionDate.Size = New System.Drawing.Size(285, 28)
        Me.TxtAdmissionDate.StyleController = Me.LycPccAdmission
        Me.TxtAdmissionDate.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionDate, 0)
        '
        'TxtAdmissionCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionCode, False)
        Me.TxtAdmissionCode.Location = New System.Drawing.Point(173, 117)
        Me.TxtAdmissionCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtAdmissionCode.Size = New System.Drawing.Size(200, 28)
        Me.TxtAdmissionCode.StyleController = Me.LycPccAdmission
        Me.TxtAdmissionCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionCode, 0)
        '
        'TxtPatientType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientType, False)
        Me.TxtPatientType.Location = New System.Drawing.Point(173, 191)
        Me.TxtPatientType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientType.Name = "TxtPatientType"
        Me.TxtPatientType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtPatientType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientType.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Contributivo", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Subsidiado", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Vinculado", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Particular", 4, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado Reg. Contributivo", 6, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado Reg. Subsidiado", 7, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Desplazado no Asegurado", 8, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Otro", 5, -1)})
        Me.TxtPatientType.Properties.ReadOnly = True
        Me.TxtPatientType.Size = New System.Drawing.Size(200, 28)
        Me.TxtPatientType.StyleController = Me.LycPccAdmission
        Me.TxtPatientType.TabIndex = 27
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientType, 0)
        '
        'TxtAfiliationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAfiliationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAfiliationType, False)
        Me.TxtAfiliationType.Location = New System.Drawing.Point(533, 191)
        Me.TxtAfiliationType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAfiliationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAfiliationType.Name = "TxtAfiliationType"
        Me.TxtAfiliationType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAfiliationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtAfiliationType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAfiliationType.Properties.Appearance.Options.UseFont = True
        Me.TxtAfiliationType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No Aplica", 0, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cotizante", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Beneficiario", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Adicional", 3, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Jub/Retirado", 4, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pensionado", 5, -1)})
        Me.TxtAfiliationType.Properties.ReadOnly = True
        Me.TxtAfiliationType.Size = New System.Drawing.Size(237, 28)
        Me.TxtAfiliationType.StyleController = Me.LycPccAdmission
        Me.TxtAfiliationType.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAfiliationType, 0)
        '
        'TxtAdmissionType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionType, False)
        Me.TxtAdmissionType.Location = New System.Drawing.Point(173, 228)
        Me.TxtAdmissionType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtAdmissionType.Size = New System.Drawing.Size(200, 28)
        Me.TxtAdmissionType.StyleController = Me.LycPccAdmission
        Me.TxtAdmissionType.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionType, 0)
        '
        'TxtLiquidationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtLiquidationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtLiquidationType, False)
        Me.TxtLiquidationType.Location = New System.Drawing.Point(173, 265)
        Me.TxtLiquidationType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.TxtLiquidationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.TxtLiquidationType.Size = New System.Drawing.Size(200, 28)
        Me.TxtLiquidationType.StyleController = Me.LycPccAdmission
        Me.TxtLiquidationType.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtLiquidationType, 0)
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
        Me.LycgPccAdmission.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup1, Me.LayoutControlItem31})
        Me.LycgPccAdmission.Name = "LycgPccAdmission"
        Me.LycgPccAdmission.Size = New System.Drawing.Size(845, 400)
        Me.LycgPccAdmission.TextVisible = False
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.CustomizationFormText = "TabbedControlGroup1"
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 49)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LayoutControlGroup1
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(821, 327)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LycgAdmissionGroup, Me.LayoutControlGroup2})
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
        Me.LayoutControlGroup1.CustomizationFormText = "Datos del Paciente"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem23, Me.LayoutControlItem25, Me.LiCareGroup, Me.LayoutControlItem29, Me.LayoutControlItem26, Me.LiEntity, Me.LayoutControlItem24, Me.LayoutControlItem18, Me.LayoutControlItem17, Me.TxtContacto})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(795, 259)
        Me.LayoutControlGroup1.Text = "Datos del Paciente"
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem23.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem23.Control = Me.TxtPatientCode
        Me.LayoutControlItem23.CustomizationFormText = "Identificación"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem23.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem23.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem23.Text = "Identificación"
        Me.LayoutControlItem23.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem23.TextToControlDistance = 12
        '
        'LayoutControlItem25
        '
        Me.LayoutControlItem25.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem25.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem25.Control = Me.TxtPatientBirth
        Me.LayoutControlItem25.CustomizationFormText = "Fecha Nacimiento"
        Me.LayoutControlItem25.Location = New System.Drawing.Point(0, 37)
        Me.LayoutControlItem25.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem25.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem25.Name = "LayoutControlItem25"
        Me.LayoutControlItem25.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem25.Text = "Fecha Nacimiento"
        Me.LayoutControlItem25.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem25.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem25.TextToControlDistance = 12
        '
        'LiCareGroup
        '
        Me.LiCareGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LiCareGroup.AppearanceItemCaption.Options.UseFont = True
        Me.LiCareGroup.Control = Me.TxtCareGroupPatient
        Me.LiCareGroup.CustomizationFormText = "Código Entidad"
        Me.LiCareGroup.Location = New System.Drawing.Point(0, 111)
        Me.LiCareGroup.MaxSize = New System.Drawing.Size(350, 37)
        Me.LiCareGroup.MinSize = New System.Drawing.Size(350, 37)
        Me.LiCareGroup.Name = "LiCareGroup"
        Me.LiCareGroup.Size = New System.Drawing.Size(350, 37)
        Me.LiCareGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiCareGroup.Text = "Grupo Atención"
        Me.LiCareGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiCareGroup.TextSize = New System.Drawing.Size(134, 26)
        Me.LiCareGroup.TextToControlDistance = 12
        '
        'LayoutControlItem29
        '
        Me.LayoutControlItem29.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem29.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem29.Control = Me.TxtPatientEstrato
        Me.LayoutControlItem29.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LayoutControlItem29.CustomizationFormText = "Estrato"
        Me.LayoutControlItem29.Location = New System.Drawing.Point(0, 148)
        Me.LayoutControlItem29.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem29.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem29.Name = "LayoutControlItem29"
        Me.LayoutControlItem29.Size = New System.Drawing.Size(350, 111)
        Me.LayoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem29.Text = "Estrato o Nivel"
        Me.LayoutControlItem29.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem29.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem29.TextToControlDistance = 12
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem26.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem26.Control = Me.TxtPatientAge
        Me.LayoutControlItem26.CustomizationFormText = "Edad"
        Me.LayoutControlItem26.Location = New System.Drawing.Point(350, 37)
        Me.LayoutControlItem26.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem26.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem26.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem26.Text = "Edad"
        Me.LayoutControlItem26.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem26.TextToControlDistance = 12
        '
        'LiEntity
        '
        Me.LiEntity.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LiEntity.AppearanceItemCaption.Options.UseFont = True
        Me.LiEntity.Control = Me.TxtPatientEntityName
        Me.LiEntity.CustomizationFormText = "Entidad"
        Me.LiEntity.Location = New System.Drawing.Point(350, 111)
        Me.LiEntity.MaxSize = New System.Drawing.Size(397, 37)
        Me.LiEntity.MinSize = New System.Drawing.Size(397, 37)
        Me.LiEntity.Name = "LiEntity"
        Me.LiEntity.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LiEntity.Size = New System.Drawing.Size(445, 37)
        Me.LiEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiEntity.Text = "Entidad"
        Me.LiEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiEntity.TextSize = New System.Drawing.Size(134, 26)
        Me.LiEntity.TextToControlDistance = 12
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem24.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem24.Control = Me.TxtPatientName
        Me.LayoutControlItem24.CustomizationFormText = "Nombre"
        Me.LayoutControlItem24.Location = New System.Drawing.Point(350, 0)
        Me.LayoutControlItem24.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem24.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem24.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem24.Text = "Nombre"
        Me.LayoutControlItem24.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem24.TextToControlDistance = 12
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem18.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem18.Control = Me.TxtAfiliationType
        Me.LayoutControlItem18.CustomizationFormText = "Tipo Afiliación"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(350, 74)
        Me.LayoutControlItem18.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem18.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem18.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem18.Text = "Tipo Afiliación"
        Me.LayoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem18.TextToControlDistance = 12
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem17.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem17.Control = Me.TxtPatientType
        Me.LayoutControlItem17.CustomizationFormText = "Tipo"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 74)
        Me.LayoutControlItem17.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem17.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem17.Text = "Tipo Paciente"
        Me.LayoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem17.TextToControlDistance = 12
        '
        'TxtContacto
        '
        Me.TxtContacto.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TxtContacto.AppearanceItemCaption.Options.UseFont = True
        Me.TxtContacto.Control = Me.TxtContact
        Me.TxtContacto.CustomizationFormText = "Contacto"
        Me.TxtContacto.Location = New System.Drawing.Point(350, 148)
        Me.TxtContacto.MaxSize = New System.Drawing.Size(397, 37)
        Me.TxtContacto.MinSize = New System.Drawing.Size(397, 37)
        Me.TxtContacto.Name = "TxtContacto"
        Me.TxtContacto.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.TxtContacto.Size = New System.Drawing.Size(445, 111)
        Me.TxtContacto.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.TxtContacto.Text = "Contacto"
        Me.TxtContacto.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.TxtContacto.TextSize = New System.Drawing.Size(134, 25)
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
        Me.LycgAdmissionGroup.Size = New System.Drawing.Size(795, 259)
        Me.LycgAdmissionGroup.Text = "Datos del Ingreso"
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.TxtAdmissionCode
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "No. Ingreso"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem7.TextToControlDistance = 12
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem11.Control = Me.TxtAdmissionType
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 111)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.Text = "Tipo Ingreso"
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem11.TextToControlDistance = 12
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem13.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem13.Control = Me.TxtLiquidationType
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 148)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.Text = "Tipo Liquidación"
        Me.LayoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem13.TextToControlDistance = 12
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem14.Control = Me.TxtBenefitPlan
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(350, 148)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem14.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.Text = "Plan Beneficio"
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem14.TextToControlDistance = 12
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem15.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem15.Control = Me.TxtAtentionCenter
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 185)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.Text = "Centro Atención"
        Me.LayoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem15.TextToControlDistance = 12
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.TxtBedStay
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(350, 111)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem8.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Estancia (Cama)"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem8.TextToControlDistance = 12
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem9.Control = Me.TxtAdmissionDate
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(350, 0)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem9.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "Fecha Ingreso"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem9.TextToControlDistance = 12
        '
        'LayoutControlItem27
        '
        Me.LayoutControlItem27.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem27.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem27.Control = Me.TxtCareGroupAdmission
        Me.LayoutControlItem27.CustomizationFormText = "Grupo Atención"
        Me.LayoutControlItem27.Location = New System.Drawing.Point(0, 37)
        Me.LayoutControlItem27.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem27.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem27.Name = "LayoutControlItem27"
        Me.LayoutControlItem27.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem27.Text = "Grupo Atención"
        Me.LayoutControlItem27.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem27.TextSize = New System.Drawing.Size(134, 25)
        Me.LayoutControlItem27.TextToControlDistance = 12
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem16.Control = Me.TxtEntityNameAdmission
        Me.LayoutControlItem16.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(350, 37)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem16.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.Text = "Nombre Entidad"
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem16.TextToControlDistance = 12
        '
        'LayoutControlItem39
        '
        Me.LayoutControlItem39.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem39.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem39.Control = Me.TxtRiskType
        Me.LayoutControlItem39.CustomizationFormText = "Tipo Riesgo"
        Me.LayoutControlItem39.Location = New System.Drawing.Point(0, 74)
        Me.LayoutControlItem39.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem39.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem39.Name = "LayoutControlItem39"
        Me.LayoutControlItem39.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem39.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem39.Text = "Tipo Riesgo"
        Me.LayoutControlItem39.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem39.TextSize = New System.Drawing.Size(134, 25)
        Me.LayoutControlItem39.TextToControlDistance = 12
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem12.Control = Me.TxtPlaceEntry
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(350, 74)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem12.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.Text = "Ingreso Por"
        Me.LayoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem12.TextToControlDistance = 12
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem10.Control = Me.TxtFunctionalUnitAdmission
        Me.LayoutControlItem10.CustomizationFormText = "LayoutControlItem10"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(350, 185)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem10.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.Text = "Unidad Funcional"
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem10.TextToControlDistance = 12
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem19.Control = Me.TxtResponsibleName
        Me.LayoutControlItem19.CustomizationFormText = "LayoutControlItem19"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 222)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(350, 37)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.Text = "Acudiente"
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem19.TextToControlDistance = 12
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem20.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem20.Control = Me.TxtResponsiblePhone
        Me.LayoutControlItem20.CustomizationFormText = "LayoutControlItem20"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(350, 222)
        Me.LayoutControlItem20.MaxSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem20.MinSize = New System.Drawing.Size(397, 37)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 2, 2, 2)
        Me.LayoutControlItem20.Size = New System.Drawing.Size(445, 37)
        Me.LayoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem20.Text = "Teléfono Acudiente"
        Me.LayoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem20.TextToControlDistance = 12
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
        Me.LayoutControlGroup2.CustomizationFormText = "Datos del Egreso"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem5})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(795, 259)
        Me.LayoutControlGroup2.Text = "Datos del Egreso"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.Control = Me.INDtxtFunctionalUnitOut
        Me.LayoutControlItem4.CustomizationFormText = "Unidad Funcional"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(455, 37)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(455, 37)
        Me.LayoutControlItem4.Name = "LayoutControlItem3"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(795, 37)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Unidad Funcional"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem4.TextToControlDistance = 12
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.Control = Me.INDdeOut
        Me.LayoutControlItem5.CustomizationFormText = "Fecha"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 37)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(455, 37)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(455, 37)
        Me.LayoutControlItem5.Name = "LayoutControlItem4"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(795, 222)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Fecha"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(134, 26)
        Me.LayoutControlItem5.TextToControlDistance = 12
        '
        'LayoutControlItem31
        '
        Me.LayoutControlItem31.Control = Me.LabelControl3
        Me.LayoutControlItem31.CustomizationFormText = "LayoutControlItem31"
        Me.LayoutControlItem31.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem31.MaxSize = New System.Drawing.Size(0, 49)
        Me.LayoutControlItem31.MinSize = New System.Drawing.Size(1, 49)
        Me.LayoutControlItem31.Name = "LayoutControlItem31"
        Me.LayoutControlItem31.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem31.Size = New System.Drawing.Size(821, 49)
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
        Me.viewSearchAdmission.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn19, Me.GridColumn27, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23})
        Me.viewSearchAdmission.DetailHeight = 431
        Me.viewSearchAdmission.Name = "viewSearchAdmission"
        Me.viewSearchAdmission.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSearchAdmission.OptionsView.EnableAppearanceOddRow = True
        Me.viewSearchAdmission.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSearchAdmission, False)
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "No. Ingreso"
        Me.GridColumn24.FieldName = "AdmissionCode"
        Me.GridColumn24.MinWidth = 23
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsColumn.AllowFocus = False
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 0
        Me.GridColumn24.Width = 209
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Identificación"
        Me.GridColumn25.FieldName = "PatientCode"
        Me.GridColumn25.MinWidth = 23
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.OptionsColumn.AllowFocus = False
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 1
        Me.GridColumn25.Width = 177
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Paciente"
        Me.GridColumn26.FieldName = "PatientName"
        Me.GridColumn26.MinWidth = 23
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.OptionsColumn.AllowEdit = False
        Me.GridColumn26.OptionsColumn.AllowFocus = False
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 2
        Me.GridColumn26.Width = 362
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "T. Ingreso"
        Me.GridColumn19.FieldName = "AdmissionTypeNameInGrid"
        Me.GridColumn19.MinWidth = 23
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 4
        Me.GridColumn19.Width = 145
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Fecha Ingreso"
        Me.GridColumn27.FieldName = "AdmissionDate"
        Me.GridColumn27.MinWidth = 23
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.OptionsColumn.AllowEdit = False
        Me.GridColumn27.OptionsColumn.AllowFocus = False
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 3
        Me.GridColumn27.Width = 163
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "T. Liquidación"
        Me.GridColumn21.FieldName = "LiquidationTypeNameInGrid"
        Me.GridColumn21.MinWidth = 23
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 5
        Me.GridColumn21.Width = 111
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Acudiente"
        Me.GridColumn22.FieldName = "ResponsibleName"
        Me.GridColumn22.MinWidth = 23
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.OptionsColumn.AllowFocus = False
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 6
        Me.GridColumn22.Width = 150
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Estado"
        Me.GridColumn23.FieldName = "StatusName"
        Me.GridColumn23.MinWidth = 23
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.OptionsColumn.AllowFocus = False
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 7
        Me.GridColumn23.Width = 96
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.DdbMenu
        Me.LayoutControlItem3.Location = New System.Drawing.Point(1007, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(54, 39)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(54, 39)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(54, 49)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
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
        Me.DdbMenu.Location = New System.Drawing.Point(1021, 14)
        Me.DdbMenu.Margin = New System.Windows.Forms.Padding(0)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.DdbMenu, False)
        Me.DdbMenu.Name = "DdbMenu"
        Me.DdbMenu.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[False]
        Me.DdbMenu.Size = New System.Drawing.Size(50, 35)
        Me.DdbMenu.StyleController = Me.INDlyRoot
        Me.DdbMenu.TabIndex = 36
        Me.DdbMenu.ToolTip = "Click para desplegar el menú de acciones"
        Me.DdbMenu.ToolTipTitle = "Menú de Acciones"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnOpenAdmission), New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnCloseAdmission)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
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
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 7)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlTop.Size = New System.Drawing.Size(1709, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 874)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1709, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 7)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 867)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1709, 7)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 867)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.PccAdmission)
        Me.INDlyRoot.Controls.Add(Me.INDbtnRefresh)
        Me.INDlyRoot.Controls.Add(Me.INDbtnProcess)
        Me.INDlyRoot.Controls.Add(Me.DdbMenu)
        Me.INDlyRoot.Controls.Add(Me.INDSleAdmissionNumber2)
        Me.INDlyRoot.Controls.Add(Me.INDgcAdmissions)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 87)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup3
        Me.INDlyRoot.Size = New System.Drawing.Size(1701, 637)
        Me.INDlyRoot.TabIndex = 12
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDbtnRefresh
        '
        Me.INDbtnRefresh.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnRefresh.Appearance.Options.UseFont = True
        Me.INDbtnRefresh.Location = New System.Drawing.Point(1192, 14)
        Me.INDbtnRefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnRefresh, True)
        Me.INDbtnRefresh.Name = "INDbtnRefresh"
        Me.INDbtnRefresh.Size = New System.Drawing.Size(113, 35)
        Me.INDbtnRefresh.StyleController = Me.INDlyRoot
        Me.INDbtnRefresh.TabIndex = 38
        Me.INDbtnRefresh.Text = "Refrescar"
        '
        'INDbtnProcess
        '
        Me.INDbtnProcess.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnProcess.Appearance.Options.UseFont = True
        Me.INDbtnProcess.Location = New System.Drawing.Point(1075, 14)
        Me.INDbtnProcess.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnProcess, True)
        Me.INDbtnProcess.Name = "INDbtnProcess"
        Me.INDbtnProcess.Size = New System.Drawing.Size(113, 35)
        Me.INDbtnProcess.StyleController = Me.INDlyRoot
        Me.INDbtnProcess.TabIndex = 37
        Me.INDbtnProcess.Text = "Procesar"
        '
        'INDlyItemProcess
        '
        Me.INDlyItemProcess.Control = Me.INDbtnProcess
        Me.INDlyItemProcess.Location = New System.Drawing.Point(1061, 0)
        Me.INDlyItemProcess.MaxSize = New System.Drawing.Size(117, 39)
        Me.INDlyItemProcess.MinSize = New System.Drawing.Size(117, 39)
        Me.INDlyItemProcess.Name = "INDlyItemProcess"
        Me.INDlyItemProcess.Size = New System.Drawing.Size(117, 49)
        Me.INDlyItemProcess.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemProcess.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemProcess.TextVisible = False
        '
        'INDlyItemRefresh
        '
        Me.INDlyItemRefresh.Control = Me.INDbtnRefresh
        Me.INDlyItemRefresh.Location = New System.Drawing.Point(1178, 0)
        Me.INDlyItemRefresh.MaxSize = New System.Drawing.Size(117, 39)
        Me.INDlyItemRefresh.MinSize = New System.Drawing.Size(117, 39)
        Me.INDlyItemRefresh.Name = "INDlyItemRefresh"
        Me.INDlyItemRefresh.Size = New System.Drawing.Size(499, 49)
        Me.INDlyItemRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRefresh.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemRefresh.TextVisible = False
        '
        'INDsleCareCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCareCenter, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCareCenter, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCareCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCareCenter, False)
        Me.INDsleCareCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCareCenter, False)
        Me.INDsleCareCenter.Location = New System.Drawing.Point(2, 2)
        Me.INDsleCareCenter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCareCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCareCenter.MaximumSize = New System.Drawing.Size(0, 74)
        Me.INDsleCareCenter.MinimumSize = New System.Drawing.Size(0, 74)
        Me.INDsleCareCenter.Name = "INDsleCareCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCareCenter, False)
        Me.INDsleCareCenter.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.INDsleCareCenter.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDsleCareCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCareCenter.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCareCenter.Properties.Appearance.Options.UseBorderColor = True
        Me.INDsleCareCenter.Properties.Appearance.Options.UseFont = True
        Me.INDsleCareCenter.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCareCenter.Properties.Appearance.Options.UseTextOptions = True
        Me.INDsleCareCenter.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDsleCareCenter.Properties.DisplayMember = "CodigoDescripcion"
        Me.INDsleCareCenter.Properties.NullText = "Seleccione un Centro de Atención"
        Me.INDsleCareCenter.Properties.PopupSizeable = False
        Me.INDsleCareCenter.Properties.PopupView = Me.INDviewSearchCareCenter
        Me.INDsleCareCenter.Properties.ShowClearButton = False
        Me.INDsleCareCenter.Properties.ShowFooter = False
        Me.INDsleCareCenter.Properties.ValueMember = "Codigo"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCareCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCareCenter, True)
        Me.INDsleCareCenter.Size = New System.Drawing.Size(1697, 74)
        Me.INDsleCareCenter.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCareCenter, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCareCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCareCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCareCenter, False)
        '
        'INDviewSearchCareCenter
        '
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewSearchCareCenter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchCareCenter.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewSearchCareCenter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchCareCenter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewSearchCareCenter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewSearchCareCenter.Appearance.Row.Options.UseFont = True
        Me.INDviewSearchCareCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDviewSearchCareCenter.DetailHeight = 431
        Me.INDviewSearchCareCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewSearchCareCenter.Name = "INDviewSearchCareCenter"
        Me.INDviewSearchCareCenter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewSearchCareCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewSearchCareCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewSearchCareCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDviewSearchCareCenter.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewSearchCareCenter, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Codigo"
        Me.GridColumn1.MinWidth = 23
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 257
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "CentroAtencion"
        Me.GridColumn2.MinWidth = 23
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 1367
        '
        'INDlblNameTitle
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblNameTitle, True)
        Me.INDlblNameTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblNameTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlblNameTitle.Appearance.Options.UseFont = True
        Me.INDlblNameTitle.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblNameTitle, False)
        Me.INDlblNameTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlblNameTitle.Location = New System.Drawing.Point(72, 0)
        Me.INDlblNameTitle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblNameTitle.Name = "INDlblNameTitle"
        Me.INDlblNameTitle.Padding = New System.Windows.Forms.Padding(0, 17, 0, 0)
        Me.INDlblNameTitle.Size = New System.Drawing.Size(163, 45)
        Me.INDlblNameTitle.TabIndex = 2
        Me.INDlblNameTitle.Text = "Detalles del Ingreso"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'INDpanelSearchCareCenter
        '
        Me.INDpanelSearchCareCenter.Controls.Add(Me.INDsleCareCenter)
        Me.INDpanelSearchCareCenter.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpanelSearchCareCenter.Location = New System.Drawing.Point(2, 2)
        Me.INDpanelSearchCareCenter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDpanelSearchCareCenter.Name = "INDpanelSearchCareCenter"
        Me.INDpanelSearchCareCenter.Size = New System.Drawing.Size(1701, 85)
        Me.INDpanelSearchCareCenter.TabIndex = 13
        '
        'INDpanelPrincipalForm
        '
        Me.INDpanelPrincipalForm.Controls.Add(Me.INDlyRoot)
        Me.INDpanelPrincipalForm.Controls.Add(Me.INDpanelSearchCareCenter)
        Me.INDpanelPrincipalForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpanelPrincipalForm.Location = New System.Drawing.Point(2, 9)
        Me.INDpanelPrincipalForm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDpanelPrincipalForm.Name = "INDpanelPrincipalForm"
        Me.INDpanelPrincipalForm.Size = New System.Drawing.Size(1705, 726)
        Me.INDpanelPrincipalForm.TabIndex = 14
        '
        'INDpcDocumentDetail
        '
        Me.INDpcDocumentDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcDocumentDetail.Controls.Add(Me.INDpcDocuments)
        Me.INDpcDocumentDetail.Controls.Add(Me.PanelControl1)
        Me.INDpcDocumentDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpcDocumentDetail.Location = New System.Drawing.Point(2, 9)
        Me.INDpcDocumentDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpcDocumentDetail.Name = "INDpcDocumentDetail"
        Me.INDpcDocumentDetail.Size = New System.Drawing.Size(1705, 726)
        Me.INDpcDocumentDetail.TabIndex = 15
        '
        'INDpcDocuments
        '
        Me.INDpcDocuments.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcDocuments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpcDocuments.Location = New System.Drawing.Point(0, 74)
        Me.INDpcDocuments.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpcDocuments.Name = "INDpcDocuments"
        Me.INDpcDocuments.Size = New System.Drawing.Size(1705, 652)
        Me.INDpcDocuments.TabIndex = 1
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.INDlblNameTitle)
        Me.PanelControl1.Controls.Add(Me.CtrNavigation1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1705, 74)
        Me.PanelControl1.TabIndex = 3
        '
        'CtrNavigation1
        '
        Me.CtrNavigation1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigation1.HideGroupContent = False
        Me.CtrNavigation1.Location = New System.Drawing.Point(0, 0)
        Me.CtrNavigation1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigation1.Name = "CtrNavigation1"
        Me.CtrNavigation1.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.CtrNavigation1.Size = New System.Drawing.Size(72, 74)
        Me.CtrNavigation1.TabIndex = 0
        '
        'FrmAccountControlAmbulatory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1709, 874)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAccountControlAmbulatory"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "2056"
        Me.Text = "Control de Cuentas Ambulatorio"
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
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcAdmissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAdmissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PccAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PccAdmission.ResumeLayout(False)
        CType(Me.LycPccAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LycPccAdmission.ResumeLayout(False)
        CType(Me.INDdeOut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtFunctionalUnitOut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRiskType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCareGroupAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtContact.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientEstrato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientEntityName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCareGroupPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientAge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientBirth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBedStay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEntityNameAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAtentionCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBenefitPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPlaceEntry.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFunctionalUnitAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatientType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAfiliationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgPccAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSearchAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDlyItemProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCareCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewSearchCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelSearchCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelSearchCareCenter.ResumeLayout(False)
        CType(Me.INDpanelPrincipalForm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelPrincipalForm.ResumeLayout(False)
        CType(Me.INDpcDocumentDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcDocumentDetail.ResumeLayout(False)
        CType(Me.INDpcDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDsleCareCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewSearchCareCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcAdmissions As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAdmissions As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpanelSearchCareCenter As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDSleAdmissionNumber2 As SearchLookUpEditExAdmission
    Friend WithEvents viewSearchAdmission As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DdbMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents MBtnOpenAdmission As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MBtnCloseAdmission As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PccAdmission As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LycPccAdmission As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDdeOut As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtFunctionalUnitOut As DevExpress.XtraEditors.TextEdit
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
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem31 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpanelPrincipalForm As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpcDocumentDetail As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpcDocuments As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblNameTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CtrNavigation1 As CtrNavigation
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDbtnProcess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemProcess As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemRefresh As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
End Class

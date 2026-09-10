Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPharmaceuticalDispensingTransfer
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPharmaceuticalDispensingTransfer))
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcPharmaceuticalDispensingTransfer = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccMoreInfoAdmission = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtStay = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtResponsiblePhone = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtResponsibleName = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAuthorizationNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtEntity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtLiquidationType = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionPlace = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionType = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtBenefitsPlan = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionDate = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtPatient = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStay = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDGcProducts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProducts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvProducts_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvProducts_Product = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvProducts_Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvWarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMeDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSleAdmissionNumber = New Presentation.Controls.CtrSearchLookUpEditWithPopUp()
        Me.INDDteDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDSleAdmissionNumberDestination = New Presentation.Controls.CtrSearchLookUpEditWithPopUp()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMainInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdmissionNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdmissionNumberDestination = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgProducts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.ImcDetailStatus = New DevExpress.Utils.ImageCollection(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcPharmaceuticalDispensingTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcPharmaceuticalDispensingTransfer.SuspendLayout()
        CType(Me.INDPccMoreInfoAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccMoreInfoAdmission.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDTxtStay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionPlace.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtBenefitsPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMainInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdmissionNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdmissionNumberDestination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImcDetailStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcPharmaceuticalDispensingTransfer)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1490, 579)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1490, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1490, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLcPharmaceuticalDispensingTransfer
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLcPharmaceuticalDispensingTransfer
        '
        Me.INDLcPharmaceuticalDispensingTransfer.AllowCustomization = False
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDPccMoreInfoAdmission)
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDGcProducts)
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDBtnAdd)
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDSleWarehouse)
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDMeDetail)
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDSleAdmissionNumber)
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDDteDate)
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDBteCode)
        Me.INDLcPharmaceuticalDispensingTransfer.Controls.Add(Me.INDSleAdmissionNumberDestination)
        Me.INDLcPharmaceuticalDispensingTransfer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLcPharmaceuticalDispensingTransfer, False)
        Me.INDLcPharmaceuticalDispensingTransfer.Location = New System.Drawing.Point(202, 7)
        Me.INDLcPharmaceuticalDispensingTransfer.Name = "INDLcPharmaceuticalDispensingTransfer"
        Me.INDLcPharmaceuticalDispensingTransfer.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2071, 297, 574, 569)
        Me.INDLcPharmaceuticalDispensingTransfer.Root = Me.LayoutControlGroup1
        Me.INDLcPharmaceuticalDispensingTransfer.Size = New System.Drawing.Size(1286, 570)
        Me.INDLcPharmaceuticalDispensingTransfer.TabIndex = 1
        Me.INDLcPharmaceuticalDispensingTransfer.Text = "LayoutControl1"
        '
        'INDPccMoreInfoAdmission
        '
        Me.INDPccMoreInfoAdmission.Controls.Add(Me.LayoutControl2)
        Me.INDPccMoreInfoAdmission.Location = New System.Drawing.Point(463, 228)
        Me.INDPccMoreInfoAdmission.Name = "INDPccMoreInfoAdmission"
        Me.INDPccMoreInfoAdmission.Size = New System.Drawing.Size(714, 305)
        Me.INDPccMoreInfoAdmission.TabIndex = 23
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDTxtStay)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAdmissionCode)
        Me.LayoutControl2.Controls.Add(Me.INDTxtResponsiblePhone)
        Me.LayoutControl2.Controls.Add(Me.INDTxtResponsibleName)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAuthorizationNumber)
        Me.LayoutControl2.Controls.Add(Me.INDTxtEntity)
        Me.LayoutControl2.Controls.Add(Me.INDTxtLiquidationType)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAdmissionPlace)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAdmissionType)
        Me.LayoutControl2.Controls.Add(Me.INDTxtBenefitsPlan)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAdmissionDate)
        Me.LayoutControl2.Controls.Add(Me.INDTxtPatient)
        Me.LayoutControl2.Controls.Add(Me.LabelControl1)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup5
        Me.LayoutControl2.Size = New System.Drawing.Size(714, 305)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDTxtStay
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtStay, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtStay, False)
        Me.INDTxtStay.Location = New System.Drawing.Point(141, 114)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtStay, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtStay.Name = "INDTxtStay"
        Me.INDTxtStay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtStay.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtStay.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtStay.Properties.Appearance.Options.UseFont = True
        Me.INDTxtStay.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtStay.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtStay.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtStay.Properties.ReadOnly = True
        Me.INDTxtStay.Size = New System.Drawing.Size(210, 24)
        Me.INDTxtStay.StyleController = Me.LayoutControl2
        Me.INDTxtStay.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtStay, 0)
        '
        'INDTxtAdmissionCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionCode, False)
        Me.INDTxtAdmissionCode.Location = New System.Drawing.Point(482, 114)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionCode.Name = "INDTxtAdmissionCode"
        Me.INDTxtAdmissionCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionCode.Properties.ReadOnly = True
        Me.INDTxtAdmissionCode.Size = New System.Drawing.Size(218, 24)
        Me.INDTxtAdmissionCode.StyleController = Me.LayoutControl2
        Me.INDTxtAdmissionCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionCode, 0)
        '
        'INDTxtResponsiblePhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtResponsiblePhone, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtResponsiblePhone, False)
        Me.INDTxtResponsiblePhone.Location = New System.Drawing.Point(141, 264)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtResponsiblePhone, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtResponsiblePhone.Name = "INDTxtResponsiblePhone"
        Me.INDTxtResponsiblePhone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtResponsiblePhone.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsiblePhone.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtResponsiblePhone.Properties.Appearance.Options.UseFont = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtResponsiblePhone.Properties.ReadOnly = True
        Me.INDTxtResponsiblePhone.Size = New System.Drawing.Size(213, 24)
        Me.INDTxtResponsiblePhone.StyleController = Me.LayoutControl2
        Me.INDTxtResponsiblePhone.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtResponsiblePhone, 0)
        '
        'INDTxtResponsibleName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtResponsibleName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtResponsibleName, False)
        Me.INDTxtResponsibleName.Location = New System.Drawing.Point(485, 234)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtResponsibleName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtResponsibleName.Name = "INDTxtResponsibleName"
        Me.INDTxtResponsibleName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtResponsibleName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsibleName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtResponsibleName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtResponsibleName.Properties.ReadOnly = True
        Me.INDTxtResponsibleName.Size = New System.Drawing.Size(215, 24)
        Me.INDTxtResponsibleName.StyleController = Me.LayoutControl2
        Me.INDTxtResponsibleName.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtResponsibleName, 0)
        '
        'INDTxtAuthorizationNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAuthorizationNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAuthorizationNumber, False)
        Me.INDTxtAuthorizationNumber.Location = New System.Drawing.Point(141, 234)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAuthorizationNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAuthorizationNumber.Name = "INDTxtAuthorizationNumber"
        Me.INDTxtAuthorizationNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAuthorizationNumber.Properties.ReadOnly = True
        Me.INDTxtAuthorizationNumber.Size = New System.Drawing.Size(213, 24)
        Me.INDTxtAuthorizationNumber.StyleController = Me.LayoutControl2
        Me.INDTxtAuthorizationNumber.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAuthorizationNumber, 0)
        '
        'INDTxtEntity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtEntity, False)
        Me.INDTxtEntity.Location = New System.Drawing.Point(141, 204)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtEntity.Name = "INDTxtEntity"
        Me.INDTxtEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtEntity.Properties.Appearance.Options.UseFont = True
        Me.INDTxtEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtEntity.Properties.ReadOnly = True
        Me.INDTxtEntity.Size = New System.Drawing.Size(213, 24)
        Me.INDTxtEntity.StyleController = Me.LayoutControl2
        Me.INDTxtEntity.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtEntity, 0)
        '
        'INDTxtLiquidationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtLiquidationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtLiquidationType, False)
        Me.INDTxtLiquidationType.Location = New System.Drawing.Point(482, 174)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtLiquidationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtLiquidationType.Name = "INDTxtLiquidationType"
        Me.INDTxtLiquidationType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtLiquidationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtLiquidationType.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtLiquidationType.Properties.Appearance.Options.UseFont = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtLiquidationType.Properties.ReadOnly = True
        Me.INDTxtLiquidationType.Size = New System.Drawing.Size(218, 24)
        Me.INDTxtLiquidationType.StyleController = Me.LayoutControl2
        Me.INDTxtLiquidationType.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtLiquidationType, 0)
        '
        'INDTxtAdmissionPlace
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionPlace, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionPlace, False)
        Me.INDTxtAdmissionPlace.Location = New System.Drawing.Point(141, 174)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionPlace, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionPlace.Name = "INDTxtAdmissionPlace"
        Me.INDTxtAdmissionPlace.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionPlace.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionPlace.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionPlace.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionPlace.Properties.ReadOnly = True
        Me.INDTxtAdmissionPlace.Size = New System.Drawing.Size(210, 24)
        Me.INDTxtAdmissionPlace.StyleController = Me.LayoutControl2
        Me.INDTxtAdmissionPlace.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionPlace, 0)
        '
        'INDTxtAdmissionType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionType, False)
        Me.INDTxtAdmissionType.Location = New System.Drawing.Point(482, 144)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionType.Name = "INDTxtAdmissionType"
        Me.INDTxtAdmissionType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionType.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionType.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionType.Properties.ReadOnly = True
        Me.INDTxtAdmissionType.Size = New System.Drawing.Size(218, 24)
        Me.INDTxtAdmissionType.StyleController = Me.LayoutControl2
        Me.INDTxtAdmissionType.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionType, 0)
        '
        'INDTxtBenefitsPlan
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtBenefitsPlan, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtBenefitsPlan, False)
        Me.INDTxtBenefitsPlan.Location = New System.Drawing.Point(485, 204)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtBenefitsPlan, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtBenefitsPlan.Name = "INDTxtBenefitsPlan"
        Me.INDTxtBenefitsPlan.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtBenefitsPlan.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtBenefitsPlan.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtBenefitsPlan.Properties.Appearance.Options.UseFont = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtBenefitsPlan.Properties.ReadOnly = True
        Me.INDTxtBenefitsPlan.Size = New System.Drawing.Size(215, 24)
        Me.INDTxtBenefitsPlan.StyleController = Me.LayoutControl2
        Me.INDTxtBenefitsPlan.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtBenefitsPlan, 0)
        '
        'INDTxtAdmissionDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionDate, False)
        Me.INDTxtAdmissionDate.Location = New System.Drawing.Point(141, 144)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionDate.Name = "INDTxtAdmissionDate"
        Me.INDTxtAdmissionDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionDate.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionDate.Properties.ReadOnly = True
        Me.INDTxtAdmissionDate.Size = New System.Drawing.Size(210, 24)
        Me.INDTxtAdmissionDate.StyleController = Me.LayoutControl2
        Me.INDTxtAdmissionDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionDate, 0)
        '
        'INDTxtPatient
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPatient, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPatient, False)
        Me.INDTxtPatient.Location = New System.Drawing.Point(141, 84)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPatient, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtPatient.Name = "INDTxtPatient"
        Me.INDTxtPatient.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtPatient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtPatient.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPatient.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPatient.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtPatient.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtPatient.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPatient.Properties.ReadOnly = True
        Me.INDTxtPatient.Size = New System.Drawing.Size(559, 24)
        Me.INDTxtPatient.StyleController = Me.LayoutControl2
        Me.INDTxtPatient.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPatient, 0)
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl1.Appearance.Options.UseBackColor = True
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Location = New System.Drawing.Point(0, 0)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(14, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(714, 40)
        Me.LabelControl1.StyleController = Me.LayoutControl2
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Más Información"
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup5, False)
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup5.GroupBordersVisible = False
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6, Me.TabbedControlGroup1})
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(714, 305)
        Me.LayoutControlGroup5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.LabelControl1
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(1, 40)
        Me.LayoutControlItem6.Name = "LayoutControlItem1"
        Me.LayoutControlItem6.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem6.Size = New System.Drawing.Size(714, 40)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.CustomizationFormText = "TabbedControlGroup1"
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 40)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LayoutControlGroup6
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(714, 265)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6})
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup6, False)
        Me.LayoutControlGroup6.CustomizationFormText = "Datos del Ingreso"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem14, Me.LayoutControlItem11, Me.LayoutControlItem13, Me.LayoutControlItem9, Me.LayoutControlItem16, Me.LayoutControlItem7, Me.LayoutControlItem10, Me.INDLciStay, Me.LayoutControlItem8, Me.LayoutControlItem19, Me.LayoutControlItem12, Me.LayoutControlItem15, Me.EmptySpaceItem1})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(690, 211)
        Me.LayoutControlGroup6.Text = "Datos del Ingreso"
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem14.Control = Me.INDTxtAuthorizationNumber
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 150)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(344, 30)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.Text = "Nº Autorización"
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem14.TextToControlDistance = 12
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem11.Control = Me.INDTxtLiquidationType
        Me.LayoutControlItem11.CustomizationFormText = "Tipo de Liquidación"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(341, 90)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.Text = "Tipo de Liquidación"
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem11.TextToControlDistance = 12
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem13.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem13.Control = Me.INDTxtEntity
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(344, 30)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.Text = "Entidad"
        Me.LayoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem13.TextToControlDistance = 12
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem9.Control = Me.INDTxtAdmissionType
        Me.LayoutControlItem9.CustomizationFormText = "Tipo de Ingreso"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(341, 60)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "Tipo de Ingreso"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem9.TextToControlDistance = 12
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem16.Control = Me.INDTxtResponsiblePhone
        Me.LayoutControlItem16.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(344, 31)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.Text = "Telefono Acudiente"
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem16.TextToControlDistance = 12
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.INDTxtAdmissionDate
        Me.LayoutControlItem7.CustomizationFormText = "Fecha Ingreso"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem7.Name = "LayoutControlItem5"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(341, 30)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "Fecha Ingreso"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem7.TextToControlDistance = 12
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem10.Control = Me.INDTxtAdmissionPlace
        Me.LayoutControlItem10.CustomizationFormText = "Lugar Ingreso"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(341, 30)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.Text = "Lugar Ingreso"
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem10.TextToControlDistance = 12
        '
        'INDLciStay
        '
        Me.INDLciStay.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLciStay.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciStay.Control = Me.INDTxtStay
        Me.INDLciStay.CustomizationFormText = "Estancia (Cama)"
        Me.INDLciStay.Location = New System.Drawing.Point(0, 30)
        Me.INDLciStay.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDLciStay.MinSize = New System.Drawing.Size(187, 30)
        Me.INDLciStay.Name = "INDLciStay"
        Me.INDLciStay.Size = New System.Drawing.Size(341, 30)
        Me.INDLciStay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStay.Text = "Estancia (Cama)"
        Me.INDLciStay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciStay.TextSize = New System.Drawing.Size(115, 21)
        Me.INDLciStay.TextToControlDistance = 12
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.INDTxtPatient
        Me.LayoutControlItem8.CustomizationFormText = "Paciente"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem8.Name = "LayoutControlItem2"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(690, 30)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Paciente"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem8.TextToControlDistance = 12
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem19.Control = Me.INDTxtAdmissionCode
        Me.LayoutControlItem19.CustomizationFormText = "Nº Ingreso"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(341, 30)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.Text = "Nº Ingreso"
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem19.TextToControlDistance = 12
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem12.Control = Me.INDTxtBenefitsPlan
        Me.LayoutControlItem12.CustomizationFormText = "Plan de Beneficios"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(344, 120)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem12.Name = "LayoutControlItem8"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(346, 30)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.Text = "Plan de Beneficios"
        Me.LayoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem12.TextToControlDistance = 12
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem15.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem15.Control = Me.INDTxtResponsibleName
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(344, 150)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(346, 30)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.Text = "Nombre Acudiente"
        Me.LayoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem15.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(344, 180)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(346, 31)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDGcProducts
        '
        Me.INDGcProducts.Location = New System.Drawing.Point(438, 95)
        Me.INDGcProducts.MainView = Me.INDGvProducts
        Me.INDGcProducts.Name = "INDGcProducts"
        Me.INDGcProducts.Size = New System.Drawing.Size(824, 451)
        Me.INDGcProducts.TabIndex = 29
        Me.INDGcProducts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProducts})
        '
        'INDGvProducts
        '
        Me.INDGvProducts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProducts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProducts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProducts.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProducts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProducts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProducts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProducts.Appearance.Row.Options.UseFont = True
        Me.INDGvProducts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvProducts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvProducts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvProducts_Code, Me.INDGvProducts_Product, Me.INDGvProducts_Quantity})
        Me.INDGvProducts.GridControl = Me.INDGcProducts
        Me.INDGvProducts.GroupCount = 1
        Me.INDGvProducts.Name = "INDGvProducts"
        Me.INDGvProducts.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvProducts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProducts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProducts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProducts.OptionsView.ShowDetailButtons = False
        Me.INDGvProducts.OptionsView.ShowGroupPanel = False
        Me.INDGvProducts.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvProducts_Code, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProducts, False)
        '
        'INDGvProducts_Code
        '
        Me.INDGvProducts_Code.Caption = "Dispensación"
        Me.INDGvProducts_Code.FieldName = "PharmaceuticalDispensingCode"
        Me.INDGvProducts_Code.Name = "INDGvProducts_Code"
        Me.INDGvProducts_Code.OptionsColumn.AllowEdit = False
        Me.INDGvProducts_Code.OptionsColumn.AllowFocus = False
        Me.INDGvProducts_Code.Visible = True
        Me.INDGvProducts_Code.VisibleIndex = 0
        '
        'INDGvProducts_Product
        '
        Me.INDGvProducts_Product.Caption = "Producto"
        Me.INDGvProducts_Product.FieldName = "ProductCodeName"
        Me.INDGvProducts_Product.Name = "INDGvProducts_Product"
        Me.INDGvProducts_Product.OptionsColumn.AllowEdit = False
        Me.INDGvProducts_Product.OptionsColumn.AllowFocus = False
        Me.INDGvProducts_Product.Visible = True
        Me.INDGvProducts_Product.VisibleIndex = 0
        Me.INDGvProducts_Product.Width = 634
        '
        'INDGvProducts_Quantity
        '
        Me.INDGvProducts_Quantity.Caption = "Cantidad Devolución"
        Me.INDGvProducts_Quantity.FieldName = "Quantity"
        Me.INDGvProducts_Quantity.Name = "INDGvProducts_Quantity"
        Me.INDGvProducts_Quantity.OptionsColumn.AllowEdit = False
        Me.INDGvProducts_Quantity.OptionsColumn.AllowFocus = False
        Me.INDGvProducts_Quantity.OptionsColumn.FixedWidth = True
        Me.INDGvProducts_Quantity.Visible = True
        Me.INDGvProducts_Quantity.VisibleIndex = 1
        Me.INDGvProducts_Quantity.Width = 194
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Location = New System.Drawing.Point(438, 59)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(824, 32)
        Me.INDBtnAdd.StyleController = Me.INDLcPharmaceuticalDispensingTransfer
        Me.INDBtnAdd.TabIndex = 28
        Me.INDBtnAdd.Text = "Agregar Productos"
        '
        'INDSleWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWarehouse, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWarehouse, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleWarehouse, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleWarehouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseFont = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleWarehouse.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleWarehouse.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleWarehouse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleWarehouse.Properties.DisplayMember = "CodeName"
        Me.INDSleWarehouse.Properties.NullText = ""
        Me.INDSleWarehouse.Properties.PopupSizeable = False
        Me.INDSleWarehouse.Properties.PopupView = Me.INDGdvWarehouse
        Me.INDSleWarehouse.Properties.ShowClearButton = False
        Me.INDSleWarehouse.Properties.ShowFooter = False
        Me.INDSleWarehouse.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.Size = New System.Drawing.Size(386, 28)
        Me.INDSleWarehouse.StyleController = Me.INDLcPharmaceuticalDispensingTransfer
        Me.INDSleWarehouse.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleWarehouse, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWarehouse, "{0} - {1}")
        Me.INDSleWarehouse.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWarehouse, False)
        '
        'INDGdvWarehouse
        '
        Me.INDGdvWarehouse.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGdvWarehouse.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGdvWarehouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGdvWarehouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGdvWarehouse.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvWarehouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvWarehouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGdvWarehouse.Appearance.Row.Options.UseFont = True
        Me.INDGdvWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16, Me.GridColumn17})
        Me.INDGdvWarehouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGdvWarehouse.Name = "INDGdvWarehouse"
        Me.INDGdvWarehouse.OptionsFind.FindFilterColumns = "Code"
        Me.INDGdvWarehouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGdvWarehouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGdvWarehouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGdvWarehouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGdvWarehouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGdvWarehouse, False)
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Código"
        Me.GridColumn16.FieldName = "Code"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        Me.GridColumn16.Width = 405
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Nombre"
        Me.GridColumn17.FieldName = "Name"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 1
        Me.GridColumn17.Width = 987
        '
        'INDMeDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDetail, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDetail, True)
        Me.INDMeDetail.Location = New System.Drawing.Point(24, 384)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDetail.Name = "INDMeDetail"
        Me.INDMeDetail.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMeDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeDetail.Properties.Appearance.Options.UseFont = True
        Me.INDMeDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMeDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeDetail.Size = New System.Drawing.Size(386, 71)
        Me.INDMeDetail.StyleController = Me.INDLcPharmaceuticalDispensingTransfer
        Me.INDMeDetail.TabIndex = 24
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDetail, 0)
        Me.INDMeDetail.ToolTip = "Este Campo es Necesario"
        '
        'INDSleAdmissionNumber
        '
        Me.INDSleAdmissionNumber._flagLoadEditValue = False
        Me.INDSleAdmissionNumber.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleAdmissionNumber.Appearance.Options.UseBackColor = True
        Me.INDSleAdmissionNumber.Datasource = Nothing
        Me.INDSleAdmissionNumber.IsReadOnly = False
        Me.INDSleAdmissionNumber.Location = New System.Drawing.Point(24, 265)
        Me.INDSleAdmissionNumber.Margin = New System.Windows.Forms.Padding(0)
        Me.INDSleAdmissionNumber.MaximumSize = New System.Drawing.Size(0, 28)
        Me.INDSleAdmissionNumber.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleAdmissionNumber.Name = "INDSleAdmissionNumber"
        Me.INDSleAdmissionNumber.OpenFormAction = Nothing
        Me.INDSleAdmissionNumber.PopupContainerControl = Nothing
        Me.INDSleAdmissionNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDSleAdmissionNumber.TabIndex = 22
        Me.INDSleAdmissionNumber.TagForm = ""
        '
        'INDDteDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDate, True)
        Me.INDDteDate.EditValue = Nothing
        Me.INDDteDate.EnterMoveNextControl = True
        Me.INDDteDate.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteDate.Name = "INDDteDate"
        Me.INDDteDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDteDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDteDate.StyleController = Me.INDLcPharmaceuticalDispensingTransfer
        Me.INDDteDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDate, 0)
        Me.INDDteDate.ToolTip = "Este Campo es Necesario"
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        Me.INDBteCode.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBteCode.StyleController = Me.INDLcPharmaceuticalDispensingTransfer
        Me.INDBteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        Me.INDBteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDSleAdmissionNumberDestination
        '
        Me.INDSleAdmissionNumberDestination._flagLoadEditValue = False
        Me.INDSleAdmissionNumberDestination.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleAdmissionNumberDestination.Appearance.Options.UseBackColor = True
        Me.INDSleAdmissionNumberDestination.Datasource = Nothing
        Me.INDSleAdmissionNumberDestination.IsReadOnly = False
        Me.INDSleAdmissionNumberDestination.Location = New System.Drawing.Point(24, 325)
        Me.INDSleAdmissionNumberDestination.Margin = New System.Windows.Forms.Padding(0)
        Me.INDSleAdmissionNumberDestination.MaximumSize = New System.Drawing.Size(0, 28)
        Me.INDSleAdmissionNumberDestination.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleAdmissionNumberDestination.Name = "INDSleAdmissionNumberDestination"
        Me.INDSleAdmissionNumberDestination.OpenFormAction = Nothing
        Me.INDSleAdmissionNumberDestination.PopupContainerControl = Nothing
        Me.INDSleAdmissionNumberDestination.Size = New System.Drawing.Size(386, 28)
        Me.INDSleAdmissionNumberDestination.TabIndex = 22
        Me.INDSleAdmissionNumberDestination.TagForm = ""
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
        Me.LayoutControlGroup1.CustomizationFormText = "Devolución de Dispensación"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMainInformation, Me.INDLcgProducts})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1286, 570)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgMainInformation
        '
        Me.INDLcgMainInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainInformation.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMainInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMainInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMainInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMainInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMainInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMainInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMainInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMainInformation, False)
        Me.INDLcgMainInformation.CustomizationFormText = "Información Principal"
        Me.INDLcgMainInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDate, Me.INDLciCode, Me.INDLciAdmissionNumber, Me.INDLciDetail, Me.INDLciWarehouse, Me.INDLciAdmissionNumberDestination})
        Me.INDLcgMainInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMainInformation.Name = "INDLcgMainInformation"
        Me.INDLcgMainInformation.Size = New System.Drawing.Size(414, 550)
        Me.INDLcgMainInformation.Text = "Información Principal"
        '
        'INDLciDate
        '
        Me.INDLciDate.Control = Me.INDDteDate
        Me.INDLciDate.CustomizationFormText = "Fecha"
        Me.INDLciDate.Location = New System.Drawing.Point(0, 60)
        Me.INDLciDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.Name = "INDLciDate"
        Me.INDLciDate.ShowInCustomizationForm = False
        Me.INDLciDate.Size = New System.Drawing.Size(390, 60)
        Me.INDLciDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDate.Text = "Fecha"
        Me.INDLciDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDate.TextToControlDistance = 5
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBteCode
        Me.INDLciCode.CustomizationFormText = "Código"
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(390, 60)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciAdmissionNumber
        '
        Me.INDLciAdmissionNumber.Control = Me.INDSleAdmissionNumber
        Me.INDLciAdmissionNumber.CustomizationFormText = "Ingreso Origen"
        Me.INDLciAdmissionNumber.Location = New System.Drawing.Point(0, 180)
        Me.INDLciAdmissionNumber.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciAdmissionNumber.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciAdmissionNumber.Name = "INDLciAdmissionNumber"
        Me.INDLciAdmissionNumber.ShowInCustomizationForm = False
        Me.INDLciAdmissionNumber.Size = New System.Drawing.Size(390, 60)
        Me.INDLciAdmissionNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdmissionNumber.Text = "Ingreso Origen"
        Me.INDLciAdmissionNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdmissionNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAdmissionNumber.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAdmissionNumber.TextToControlDistance = 5
        '
        'INDLciDetail
        '
        Me.INDLciDetail.AllowHide = False
        Me.INDLciDetail.Control = Me.INDMeDetail
        Me.INDLciDetail.CustomizationFormText = "Detalle"
        Me.INDLciDetail.Location = New System.Drawing.Point(0, 300)
        Me.INDLciDetail.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDLciDetail.MinSize = New System.Drawing.Size(390, 100)
        Me.INDLciDetail.Name = "INDLciDetail"
        Me.INDLciDetail.ShowInCustomizationForm = False
        Me.INDLciDetail.Size = New System.Drawing.Size(390, 191)
        Me.INDLciDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDetail.Text = "Detalle"
        Me.INDLciDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDetail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDetail.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciDetail.TextToControlDistance = 5
        '
        'INDLciWarehouse
        '
        Me.INDLciWarehouse.Control = Me.INDSleWarehouse
        Me.INDLciWarehouse.CustomizationFormText = "Almacén"
        Me.INDLciWarehouse.Location = New System.Drawing.Point(0, 120)
        Me.INDLciWarehouse.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciWarehouse.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciWarehouse.Name = "INDLciWarehouse"
        Me.INDLciWarehouse.ShowInCustomizationForm = False
        Me.INDLciWarehouse.Size = New System.Drawing.Size(390, 60)
        Me.INDLciWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciWarehouse.Text = "Almacén"
        Me.INDLciWarehouse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciWarehouse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciWarehouse.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciWarehouse.TextToControlDistance = 5
        '
        'INDLciAdmissionNumberDestination
        '
        Me.INDLciAdmissionNumberDestination.Control = Me.INDSleAdmissionNumberDestination
        Me.INDLciAdmissionNumberDestination.CustomizationFormText = "Ingreso Destino"
        Me.INDLciAdmissionNumberDestination.Location = New System.Drawing.Point(0, 240)
        Me.INDLciAdmissionNumberDestination.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciAdmissionNumberDestination.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciAdmissionNumberDestination.Name = "INDLciAdmissionNumberDestination"
        Me.INDLciAdmissionNumberDestination.ShowInCustomizationForm = False
        Me.INDLciAdmissionNumberDestination.Size = New System.Drawing.Size(390, 60)
        Me.INDLciAdmissionNumberDestination.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdmissionNumberDestination.Text = "Ingreso Destino"
        Me.INDLciAdmissionNumberDestination.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdmissionNumberDestination.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAdmissionNumberDestination.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAdmissionNumberDestination.TextToControlDistance = 5
        '
        'INDLcgProducts
        '
        Me.INDLcgProducts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProducts.AppearanceGroup.Options.UseFont = True
        Me.INDLcgProducts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProducts.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgProducts, False)
        Me.INDLcgProducts.CustomizationFormText = "Listado de Productos"
        Me.INDLcgProducts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAdd, Me.INDLciProducts})
        Me.INDLcgProducts.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgProducts.Name = "INDLcgProducts"
        Me.INDLcgProducts.Size = New System.Drawing.Size(852, 550)
        Me.INDLcgProducts.Text = "Listado de Productos"
        '
        'INDLciAdd
        '
        Me.INDLciAdd.Control = Me.INDBtnAdd
        Me.INDLciAdd.CustomizationFormText = "Agregar Productos"
        Me.INDLciAdd.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAdd.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.MinSize = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.Name = "INDLciAdd"
        Me.INDLciAdd.Size = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdd.TextVisible = False
        '
        'INDLciProducts
        '
        Me.INDLciProducts.AppearanceItemCaption.Options.UseTextOptions = True
        Me.INDLciProducts.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLciProducts.Control = Me.INDGcProducts
        Me.INDLciProducts.CustomizationFormText = "Registros"
        Me.INDLciProducts.Location = New System.Drawing.Point(0, 36)
        Me.INDLciProducts.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDLciProducts.MinSize = New System.Drawing.Size(828, 250)
        Me.INDLciProducts.Name = "INDLciProducts"
        Me.INDLciProducts.Size = New System.Drawing.Size(828, 455)
        Me.INDLciProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProducts.Text = "Registros"
        Me.INDLciProducts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProducts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProducts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciProducts.TextToControlDistance = 0
        Me.INDLciProducts.TextVisible = False
        '
        'ImcDetailStatus
        '
        Me.ImcDetailStatus.ImageStream = CType(resources.GetObject("ImcDetailStatus.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImcDetailStatus.Images.SetKeyName(0, "eliminado(1).png")
        Me.ImcDetailStatus.Images.SetKeyName(1, "eliminado(2).png")
        Me.ImcDetailStatus.Images.SetKeyName(2, "eliminado.png")
        Me.ImcDetailStatus.Images.SetKeyName(3, "modificado(1).png")
        Me.ImcDetailStatus.Images.SetKeyName(4, "modificado(2).png")
        Me.ImcDetailStatus.Images.SetKeyName(5, "modificado.png")
        Me.ImcDetailStatus.Images.SetKeyName(6, "nuevo(1).png")
        Me.ImcDetailStatus.Images.SetKeyName(7, "nuevo(2).png")
        Me.ImcDetailStatus.Images.SetKeyName(8, "nuevo.png")
        Me.ImcDetailStatus.Images.SetKeyName(9, "sin modificar(1).png")
        Me.ImcDetailStatus.Images.SetKeyName(10, "sin modificar(2).png")
        Me.ImcDetailStatus.Images.SetKeyName(11, "sin modificar.png")
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmPharmaceuticalDispensingTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1490, 701)
        Me.Name = "FrmPharmaceuticalDispensingTransfer"
        Me.Opacity = 1.0R
        Me.Tag = "2187"
        Me.Text = "Traslado de Dispensación por Ingreso"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcPharmaceuticalDispensingTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcPharmaceuticalDispensingTransfer.ResumeLayout(False)
        CType(Me.INDPccMoreInfoAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccMoreInfoAdmission.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDTxtStay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionPlace.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtBenefitsPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMainInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdmissionNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdmissionNumberDestination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImcDetailStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcPharmaceuticalDispensingTransfer As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDDteDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDLcgMainInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleAdmissionNumber As Presentation.Controls.CtrSearchLookUpEditWithPopUp
    Friend WithEvents INDLciAdmissionNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPccMoreInfoAdmission As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtStay As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtResponsiblePhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtResponsibleName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAuthorizationNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtEntity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtLiquidationType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionPlace As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtBenefitsPlan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciStay As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDMeDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgProducts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGdvWarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcProducts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvProducts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvProducts_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvProducts_Product As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvProducts_Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ImcDetailStatus As DevExpress.Utils.ImageCollection
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleAdmissionNumberDestination As CtrSearchLookUpEditWithPopUp
    Friend WithEvents INDLciAdmissionNumberDestination As DevExpress.XtraLayout.LayoutControlItem
End Class

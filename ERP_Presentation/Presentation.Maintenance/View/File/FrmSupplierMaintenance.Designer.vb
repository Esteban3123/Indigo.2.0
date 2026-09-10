Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSupplierMaintenance
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSupplierMaintenance))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlySupplier = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtLastName = New DevExpress.XtraEditors.TextEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDChkResponsibleWarranty = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkSeller = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkManufacturer = New DevExpress.XtraEditors.CheckEdit()
        Me.INDckRetentionIVA = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.INDseTimeLimitDays = New DevExpress.XtraEditors.SpinEdit()
        Me.INDpccContactsControl = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrContacts = New Presentation.Controls.CtrContactosMaintenance()
        Me.INDpceContactData = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDtxtCodeCMMS = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtWebSite = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleCity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrSupplier = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCodeCMMS = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTimeLimitDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLastName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrContacts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemContacts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemWebSite = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrSupplierType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn169 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn170 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn171 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn166 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn167 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn168 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn163 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn164 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn165 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn160 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn161 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn162 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn153 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn154 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn155 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn150 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn151 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn152 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn145 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn146 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn147 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn142 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn143 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn144 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn139 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn140 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn141 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn136 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn137 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn138 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn133 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn134 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn135 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn130 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn131 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn132 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn127 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn129 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn124 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn125 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn126 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn121 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn122 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn123 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn118 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn119 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn120 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn115 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn116 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn117 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn114 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn109 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn110 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn106 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn107 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn108 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn103 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn104 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn105 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn100 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn101 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn102 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn97 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn98 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn99 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn94 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn95 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn96 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn91 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn92 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn93 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn90 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn86 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn87 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn82 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn83 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn84 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn79 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn80 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn81 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn76 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn77 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn78 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn70 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn72 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn67 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoCheckedListBoxControl1 = New Presentation.Controls.IndigoCheckedListBoxControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoPopUpContainerEdit2 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlySupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlySupplier.SuspendLayout()
        CType(Me.INDtxtLastName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkResponsibleWarranty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkSeller.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkManufacturer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDckRetentionIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseTimeLimitDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccContactsControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccContactsControl.SuspendLayout()
        CType(Me.INDpceContactData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtCodeCMMS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtWebSite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCodeCMMS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTimeLimitDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLastName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemWebSite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrSupplierType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckedListBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlySupplier)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDbteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.Appearance.Font = CType(resources.GetObject("INDbteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        SerializableAppearanceObject2.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, resources.GetString("INDbteCode.Properties.Buttons7"), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons10"), Boolean))})
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.StyleController = Me.INDlySupplier
        Me.INDbteCode.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDlySupplier
        '
        Me.INDlySupplier.AllowCustomization = False
        Me.INDlySupplier.Controls.Add(Me.INDtxtLastName)
        Me.INDlySupplier.Controls.Add(Me.INDChkResponsibleWarranty)
        Me.INDlySupplier.Controls.Add(Me.INDChkSeller)
        Me.INDlySupplier.Controls.Add(Me.INDChkManufacturer)
        Me.INDlySupplier.Controls.Add(Me.INDckRetentionIVA)
        Me.INDlySupplier.Controls.Add(Me.INDseTimeLimitDays)
        Me.INDlySupplier.Controls.Add(Me.INDpccContactsControl)
        Me.INDlySupplier.Controls.Add(Me.INDpceContactData)
        Me.INDlySupplier.Controls.Add(Me.INDtxtCodeCMMS)
        Me.INDlySupplier.Controls.Add(Me.INDtxtWebSite)
        Me.INDlySupplier.Controls.Add(Me.INDtxtName)
        Me.INDlySupplier.Controls.Add(Me.INDbteCode)
        Me.INDlySupplier.Controls.Add(Me.INDsleCity)
        resources.ApplyResources(Me.INDlySupplier, "INDlySupplier")
        Me.LayoutControls.SetIsCustomizable(Me.INDlySupplier, False)
        Me.INDlySupplier.Name = "INDlySupplier"
        Me.INDlySupplier.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(741, 239, 250, 350)
        Me.INDlySupplier.Root = Me.LayoutControlGroup1
        '
        'INDtxtLastName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtLastName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtLastName, False)
        resources.ApplyResources(Me.INDtxtLastName, "INDtxtLastName")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtLastName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtLastName.MenuManager = Me.BarManager1
        Me.INDtxtLastName.Name = "INDtxtLastName"
        Me.INDtxtLastName.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtLastName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtLastName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtLastName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtLastName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtLastName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtLastName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtLastName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtLastName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtLastName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtLastName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtLastName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtLastName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtLastName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtLastName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtLastName.StyleController = Me.INDlySupplier
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtLastName, 0)
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.MaxItemId = 0
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        '
        'INDChkResponsibleWarranty
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkResponsibleWarranty, False)
        resources.ApplyResources(Me.INDChkResponsibleWarranty, "INDChkResponsibleWarranty")
        Me.INDChkResponsibleWarranty.MenuManager = Me.BarManager1
        Me.INDChkResponsibleWarranty.Name = "INDChkResponsibleWarranty"
        Me.INDChkResponsibleWarranty.Properties.Appearance.Font = CType(resources.GetObject("INDChkResponsibleWarranty.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDChkResponsibleWarranty.Properties.Appearance.Options.UseFont = True
        Me.INDChkResponsibleWarranty.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDChkResponsibleWarranty.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDChkResponsibleWarranty.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDChkResponsibleWarranty.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDChkResponsibleWarranty.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDChkResponsibleWarranty.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDChkResponsibleWarranty.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDChkResponsibleWarranty.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDChkResponsibleWarranty.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkResponsibleWarranty.Properties.Caption = resources.GetString("INDChkResponsibleWarranty.Properties.Caption")
        Me.INDChkResponsibleWarranty.StyleController = Me.INDlySupplier
        '
        'INDChkSeller
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkSeller, False)
        resources.ApplyResources(Me.INDChkSeller, "INDChkSeller")
        Me.INDChkSeller.MenuManager = Me.BarManager1
        Me.INDChkSeller.Name = "INDChkSeller"
        Me.INDChkSeller.Properties.Appearance.Font = CType(resources.GetObject("INDChkSeller.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDChkSeller.Properties.Appearance.Options.UseFont = True
        Me.INDChkSeller.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDChkSeller.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDChkSeller.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDChkSeller.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDChkSeller.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDChkSeller.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDChkSeller.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDChkSeller.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDChkSeller.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkSeller.Properties.Caption = resources.GetString("INDChkSeller.Properties.Caption")
        Me.INDChkSeller.StyleController = Me.INDlySupplier
        '
        'INDChkManufacturer
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkManufacturer, False)
        resources.ApplyResources(Me.INDChkManufacturer, "INDChkManufacturer")
        Me.INDChkManufacturer.MenuManager = Me.BarManager1
        Me.INDChkManufacturer.Name = "INDChkManufacturer"
        Me.INDChkManufacturer.Properties.Appearance.Font = CType(resources.GetObject("INDChkManufacturer.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDChkManufacturer.Properties.Appearance.Options.UseFont = True
        Me.INDChkManufacturer.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDChkManufacturer.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDChkManufacturer.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDChkManufacturer.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDChkManufacturer.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDChkManufacturer.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDChkManufacturer.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDChkManufacturer.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDChkManufacturer.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkManufacturer.Properties.Caption = resources.GetString("INDChkManufacturer.Properties.Caption")
        Me.INDChkManufacturer.StyleController = Me.INDlySupplier
        '
        'INDckRetentionIVA
        '
        Me.INDckRetentionIVA.Appearance.Font = CType(resources.GetObject("INDckRetentionIVA.Appearance.Font"), System.Drawing.Font)
        Me.INDckRetentionIVA.Appearance.Options.UseFont = True
        Me.IndigoCheckedListBoxControl1.SetCampoObligatorio(Me.INDckRetentionIVA, False)
        Me.INDckRetentionIVA.CheckOnClick = True
        Me.INDckRetentionIVA.Items.AddRange(New DevExpress.XtraEditors.Controls.CheckedListBoxItem() {New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(resources.GetObject("INDckRetentionIVA.Items"), Object), resources.GetString("INDckRetentionIVA.Items1")), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(resources.GetObject("INDckRetentionIVA.Items2"), Object), resources.GetString("INDckRetentionIVA.Items3"))})
        resources.ApplyResources(Me.INDckRetentionIVA, "INDckRetentionIVA")
        Me.INDckRetentionIVA.MultiColumn = True
        Me.INDckRetentionIVA.Name = "INDckRetentionIVA"
        Me.INDckRetentionIVA.StyleController = Me.INDlySupplier
        '
        'INDseTimeLimitDays
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseTimeLimitDays, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseTimeLimitDays, True)
        resources.ApplyResources(Me.INDseTimeLimitDays, "INDseTimeLimitDays")
        Me.INDseTimeLimitDays.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDseTimeLimitDays, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseTimeLimitDays.Name = "INDseTimeLimitDays"
        Me.INDseTimeLimitDays.Properties.Appearance.BackColor = CType(resources.GetObject("INDseTimeLimitDays.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDseTimeLimitDays.Properties.Appearance.Font = CType(resources.GetObject("INDseTimeLimitDays.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDseTimeLimitDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDseTimeLimitDays.Properties.Appearance.Options.UseFont = True
        Me.INDseTimeLimitDays.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDseTimeLimitDays.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDseTimeLimitDays.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDseTimeLimitDays.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDseTimeLimitDays.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDseTimeLimitDays.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDseTimeLimitDays.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseTimeLimitDays.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseTimeLimitDays.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseTimeLimitDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDseTimeLimitDays.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDseTimeLimitDays.Properties.Mask.EditMask = resources.GetString("INDseTimeLimitDays.Properties.Mask.EditMask")
        Me.INDseTimeLimitDays.Properties.Mask.MaskType = CType(resources.GetObject("INDseTimeLimitDays.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDseTimeLimitDays.Properties.MaxLength = 6
        Me.INDseTimeLimitDays.Properties.MaxValue = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.INDseTimeLimitDays.Properties.NullText = resources.GetString("INDseTimeLimitDays.Properties.NullText")
        Me.INDseTimeLimitDays.StyleController = Me.INDlySupplier
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseTimeLimitDays, 0)
        '
        'INDpccContactsControl
        '
        Me.INDpccContactsControl.Controls.Add(Me.CtrContacts)
        resources.ApplyResources(Me.INDpccContactsControl, "INDpccContactsControl")
        Me.INDpccContactsControl.Name = "INDpccContactsControl"
        '
        'CtrContacts
        '
        resources.ApplyResources(Me.CtrContacts, "CtrContacts")
        Me.CtrContacts.Name = "CtrContacts"
        '
        'INDpceContactData
        '
        Me.IndigoPopUpContainerEdit2.SetButtonMoreOptions(Me.INDpceContactData, False)
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceContactData, True)
        Me.INDpceContactData.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceContactData, Nothing)
        Me.IndigoPopUpContainerEdit2.SetHostControl(Me.INDpceContactData, Nothing)
        resources.ApplyResources(Me.INDpceContactData, "INDpceContactData")
        Me.INDpceContactData.MenuManager = Me.BarManager1
        Me.INDpceContactData.Name = "INDpceContactData"
        Me.IndigoPopUpContainerEdit2.SetOpenForm(Me.INDpceContactData, False)
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceContactData, False)
        Me.IndigoPopUpContainerEdit2.SetPopUpAnimation(Me.INDpceContactData, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceContactData, False)
        Me.INDpceContactData.Properties.Appearance.BackColor = CType(resources.GetObject("INDpceContactData.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDpceContactData.Properties.Appearance.Font = CType(resources.GetObject("INDpceContactData.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDpceContactData.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceContactData.Properties.Appearance.Options.UseFont = True
        Me.INDpceContactData.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDpceContactData.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDpceContactData.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDpceContactData.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDpceContactData.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDpceContactData.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDpceContactData.Properties.AutoHeight = CType(resources.GetObject("INDpceContactData.Properties.AutoHeight"), Boolean)
        Me.INDpceContactData.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceContactData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDpceContactData.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDpceContactData.Properties.Buttons1"), CType(resources.GetObject("INDpceContactData.Properties.Buttons2"), Integer), CType(resources.GetObject("INDpceContactData.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDpceContactData.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDpceContactData.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDpceContactData.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), CType(resources.GetObject("INDpceContactData.Properties.Buttons7"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDpceContactData.Properties.Buttons8"), CType(resources.GetObject("INDpceContactData.Properties.Buttons9"), Object), CType(resources.GetObject("INDpceContactData.Properties.Buttons10"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDpceContactData.Properties.Buttons11"), Boolean))})
        Me.INDpceContactData.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceContactData.Properties.PopupControl = Me.INDpccContactsControl
        Me.INDpceContactData.Properties.PopupSizeable = False
        Me.INDpceContactData.Properties.ShowPopupCloseButton = False
        Me.INDpceContactData.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceContactData.StyleController = Me.INDlySupplier
        Me.INDpceContactData.Tag = 414
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceContactData, Nothing)
        Me.IndigoPopUpContainerEdit2.SetTagForm(Me.INDpceContactData, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceContactData, Nothing)
        Me.IndigoPopUpContainerEdit2.SetWpfControl(Me.INDpceContactData, Nothing)
        '
        'INDtxtCodeCMMS
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCodeCMMS, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCodeCMMS, False)
        Me.INDtxtCodeCMMS.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtCodeCMMS, "INDtxtCodeCMMS")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCodeCMMS, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCodeCMMS.Name = "INDtxtCodeCMMS"
        Me.INDtxtCodeCMMS.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtCodeCMMS.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtCodeCMMS.Properties.Appearance.Font = CType(resources.GetObject("INDtxtCodeCMMS.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtCodeCMMS.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCodeCMMS.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCodeCMMS.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtCodeCMMS.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtCodeCMMS.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtCodeCMMS.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtCodeCMMS.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtCodeCMMS.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtCodeCMMS.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtCodeCMMS.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtCodeCMMS.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtCodeCMMS.Properties.MaxLength = 20
        Me.INDtxtCodeCMMS.StyleController = Me.INDlySupplier
        Me.INDtxtCodeCMMS.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCodeCMMS, 0)
        '
        'INDtxtWebSite
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtWebSite, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtWebSite, False)
        Me.INDtxtWebSite.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtWebSite, "INDtxtWebSite")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtWebSite, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtWebSite.Name = "INDtxtWebSite"
        Me.INDtxtWebSite.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtWebSite.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtWebSite.Properties.Appearance.Font = CType(resources.GetObject("INDtxtWebSite.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtWebSite.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtWebSite.Properties.Appearance.Options.UseFont = True
        Me.INDtxtWebSite.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtWebSite.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtWebSite.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtWebSite.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtWebSite.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtWebSite.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtWebSite.Properties.Mask.EditMask = resources.GetString("INDtxtWebSite.Properties.Mask.EditMask")
        Me.INDtxtWebSite.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtWebSite.Properties.MaxLength = 80
        Me.INDtxtWebSite.StyleController = Me.INDlySupplier
        Me.INDtxtWebSite.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtWebSite, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.StyleController = Me.INDlySupplier
        Me.INDtxtName.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDsleCity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCity, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCity, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCity, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCity, False)
        Me.INDsleCity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCity, False)
        resources.ApplyResources(Me.INDsleCity, "INDsleCity")
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCity.Name = "INDsleCity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCity, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCity, False)
        Me.INDsleCity.Properties.Appearance.BackColor = CType(resources.GetObject("INDsleCity.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDsleCity.Properties.Appearance.Font = CType(resources.GetObject("INDsleCity.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDsleCity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCity.Properties.Appearance.Options.UseFont = True
        Me.INDsleCity.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDsleCity.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDsleCity.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDsleCity.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDsleCity.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDsleCity.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDsleCity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDsleCity.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDsleCity.Properties.DisplayMember = "CodeName"
        Me.INDsleCity.Properties.NullText = resources.GetString("INDsleCity.Properties.NullText")
        Me.INDsleCity.Properties.PopupSizeable = False
        Me.INDsleCity.Properties.ShowFooter = False
        Me.INDsleCity.Properties.ValueMember = "Id"
        Me.INDsleCity.Properties.View = Me.GridView2
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCity, True)
        Me.INDsleCity.StyleController = Me.INDlySupplier
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCity, "513")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCity, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.GroupRow.Font = CType(resources.GetObject("GridView2.Appearance.GroupRow.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("GridView2.Appearance.GroupRow.ForeColor"), System.Drawing.Color)
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView2.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView2.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("GridView2.Appearance.HeaderPanel.ForeColor"), System.Drawing.Color)
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView2.Appearance.Row.Font = CType(resources.GetObject("GridView2.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn4})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "CodeName"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrSupplier, Me.INDlyGrContacts, Me.INDlyGrSupplierType})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1112, 597)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrSupplier
        '
        resources.ApplyResources(Me.INDlyGrSupplier, "INDlyGrSupplier")
        Me.INDlyGrSupplier.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemCodeCMMS, Me.INDlyItemTimeLimitDays, Me.INDlyItemLastName})
        Me.INDlyGrSupplier.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrSupplier.Name = "INDlyGrSupplier"
        Me.INDlyGrSupplier.Size = New System.Drawing.Size(444, 597)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 3, 2, 2)
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemCodeCMMS
        '
        Me.INDlyItemCodeCMMS.Control = Me.INDtxtCodeCMMS
        resources.ApplyResources(Me.INDlyItemCodeCMMS, "INDlyItemCodeCMMS")
        Me.INDlyItemCodeCMMS.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemCodeCMMS.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodeCMMS.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodeCMMS.Name = "INDlyItemCodeCMMS"
        Me.INDlyItemCodeCMMS.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 3, 2, 2)
        Me.INDlyItemCodeCMMS.Size = New System.Drawing.Size(420, 394)
        Me.INDlyItemCodeCMMS.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCodeCMMS.Tag = "CodeCMMS"
        Me.INDlyItemCodeCMMS.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCodeCMMS.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCodeCMMS.TextToControlDistance = 12
        '
        'INDlyItemTimeLimitDays
        '
        Me.INDlyItemTimeLimitDays.Control = Me.INDseTimeLimitDays
        resources.ApplyResources(Me.INDlyItemTimeLimitDays, "INDlyItemTimeLimitDays")
        Me.INDlyItemTimeLimitDays.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemTimeLimitDays.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemTimeLimitDays.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemTimeLimitDays.Name = "INDlyItemTimeLimitDays"
        Me.INDlyItemTimeLimitDays.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemTimeLimitDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTimeLimitDays.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTimeLimitDays.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemTimeLimitDays.TextToControlDistance = 12
        '
        'INDlyItemLastName
        '
        Me.INDlyItemLastName.Control = Me.INDtxtLastName
        resources.ApplyResources(Me.INDlyItemLastName, "INDlyItemLastName")
        Me.INDlyItemLastName.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemLastName.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemLastName.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemLastName.Name = "INDlyItemLastName"
        Me.INDlyItemLastName.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemLastName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLastName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLastName.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemLastName.TextToControlDistance = 12
        '
        'INDlyGrContacts
        '
        resources.ApplyResources(Me.INDlyGrContacts, "INDlyGrContacts")
        Me.INDlyGrContacts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemContacts, Me.INDlyItemWebSite, Me.INDlyItemCity, Me.LayoutControlItem2})
        Me.INDlyGrContacts.Location = New System.Drawing.Point(444, 0)
        Me.INDlyGrContacts.Name = "INDlyGrContacts"
        Me.INDlyGrContacts.Size = New System.Drawing.Size(444, 597)
        '
        'INDlyItemContacts
        '
        Me.INDlyItemContacts.Control = Me.INDpceContactData
        resources.ApplyResources(Me.INDlyItemContacts, "INDlyItemContacts")
        Me.INDlyItemContacts.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemContacts.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemContacts.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemContacts.Name = "INDlyItemContacts"
        Me.INDlyItemContacts.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemContacts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemContacts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemContacts.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemContacts.TextToControlDistance = 12
        '
        'INDlyItemWebSite
        '
        Me.INDlyItemWebSite.Control = Me.INDtxtWebSite
        resources.ApplyResources(Me.INDlyItemWebSite, "INDlyItemWebSite")
        Me.INDlyItemWebSite.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemWebSite.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemWebSite.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemWebSite.Name = "INDlyItemWebSite"
        Me.INDlyItemWebSite.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 3, 2, 2)
        Me.INDlyItemWebSite.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemWebSite.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemWebSite.Tag = "WebSite"
        Me.INDlyItemWebSite.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemWebSite.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemWebSite.TextToControlDistance = 12
        '
        'INDlyItemCity
        '
        Me.INDlyItemCity.Control = Me.INDsleCity
        resources.ApplyResources(Me.INDlyItemCity, "INDlyItemCity")
        Me.INDlyItemCity.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemCity.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCity.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCity.Name = "INDlyItemCity"
        Me.INDlyItemCity.ShowInCustomizationForm = False
        Me.INDlyItemCity.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemCity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCity.Tag = "City"
        Me.INDlyItemCity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCity.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCity.TextToControlDistance = 12
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDckRetentionIVA
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 108)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(420, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(420, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(420, 430)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDlyGrSupplierType
        '
        resources.ApplyResources(Me.INDlyGrSupplierType, "INDlyGrSupplierType")
        Me.INDlyGrSupplierType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.INDlyGrSupplierType.Location = New System.Drawing.Point(888, 0)
        Me.INDlyGrSupplierType.Name = "INDlyGrSupplierType"
        Me.INDlyGrSupplierType.Size = New System.Drawing.Size(224, 597)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDChkManufacturer
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(300, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(200, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(200, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDChkSeller
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(93, 29)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(200, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDChkResponsibleWarranty
        resources.ApplyResources(Me.LayoutControlItem4, "LayoutControlItem4")
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(200, 466)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'GridColumn169
        '
        resources.ApplyResources(Me.GridColumn169, "GridColumn169")
        Me.GridColumn169.FieldName = "Id"
        Me.GridColumn169.Name = "GridColumn169"
        '
        'GridColumn170
        '
        Me.GridColumn170.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn170, "GridColumn170")
        Me.GridColumn170.FieldName = "Number"
        Me.GridColumn170.Name = "GridColumn170"
        '
        'GridColumn171
        '
        Me.GridColumn171.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn171, "GridColumn171")
        Me.GridColumn171.FieldName = "Name"
        Me.GridColumn171.Name = "GridColumn171"
        '
        'GridColumn166
        '
        resources.ApplyResources(Me.GridColumn166, "GridColumn166")
        Me.GridColumn166.FieldName = "Id"
        Me.GridColumn166.Name = "GridColumn166"
        '
        'GridColumn167
        '
        Me.GridColumn167.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn167, "GridColumn167")
        Me.GridColumn167.FieldName = "Number"
        Me.GridColumn167.Name = "GridColumn167"
        '
        'GridColumn168
        '
        Me.GridColumn168.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn168, "GridColumn168")
        Me.GridColumn168.FieldName = "Name"
        Me.GridColumn168.Name = "GridColumn168"
        '
        'GridColumn163
        '
        resources.ApplyResources(Me.GridColumn163, "GridColumn163")
        Me.GridColumn163.FieldName = "Id"
        Me.GridColumn163.Name = "GridColumn163"
        '
        'GridColumn164
        '
        Me.GridColumn164.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn164, "GridColumn164")
        Me.GridColumn164.FieldName = "Number"
        Me.GridColumn164.Name = "GridColumn164"
        '
        'GridColumn165
        '
        Me.GridColumn165.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn165, "GridColumn165")
        Me.GridColumn165.FieldName = "Name"
        Me.GridColumn165.Name = "GridColumn165"
        '
        'GridColumn160
        '
        resources.ApplyResources(Me.GridColumn160, "GridColumn160")
        Me.GridColumn160.FieldName = "Id"
        Me.GridColumn160.Name = "GridColumn160"
        '
        'GridColumn161
        '
        Me.GridColumn161.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn161, "GridColumn161")
        Me.GridColumn161.FieldName = "Number"
        Me.GridColumn161.Name = "GridColumn161"
        '
        'GridColumn162
        '
        Me.GridColumn162.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn162, "GridColumn162")
        Me.GridColumn162.FieldName = "Name"
        Me.GridColumn162.Name = "GridColumn162"
        '
        'GridColumn153
        '
        resources.ApplyResources(Me.GridColumn153, "GridColumn153")
        Me.GridColumn153.FieldName = "Id"
        Me.GridColumn153.Name = "GridColumn153"
        '
        'GridColumn154
        '
        Me.GridColumn154.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn154, "GridColumn154")
        Me.GridColumn154.FieldName = "Number"
        Me.GridColumn154.Name = "GridColumn154"
        '
        'GridColumn155
        '
        Me.GridColumn155.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn155, "GridColumn155")
        Me.GridColumn155.FieldName = "Name"
        Me.GridColumn155.Name = "GridColumn155"
        '
        'GridColumn150
        '
        resources.ApplyResources(Me.GridColumn150, "GridColumn150")
        Me.GridColumn150.FieldName = "Id"
        Me.GridColumn150.Name = "GridColumn150"
        '
        'GridColumn151
        '
        Me.GridColumn151.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn151, "GridColumn151")
        Me.GridColumn151.FieldName = "Number"
        Me.GridColumn151.Name = "GridColumn151"
        '
        'GridColumn152
        '
        Me.GridColumn152.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn152, "GridColumn152")
        Me.GridColumn152.FieldName = "Name"
        Me.GridColumn152.Name = "GridColumn152"
        '
        'GridColumn145
        '
        resources.ApplyResources(Me.GridColumn145, "GridColumn145")
        Me.GridColumn145.FieldName = "Id"
        Me.GridColumn145.Name = "GridColumn145"
        '
        'GridColumn146
        '
        Me.GridColumn146.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn146, "GridColumn146")
        Me.GridColumn146.FieldName = "Number"
        Me.GridColumn146.Name = "GridColumn146"
        '
        'GridColumn147
        '
        Me.GridColumn147.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn147, "GridColumn147")
        Me.GridColumn147.FieldName = "Name"
        Me.GridColumn147.Name = "GridColumn147"
        '
        'GridColumn142
        '
        resources.ApplyResources(Me.GridColumn142, "GridColumn142")
        Me.GridColumn142.FieldName = "Id"
        Me.GridColumn142.Name = "GridColumn142"
        '
        'GridColumn143
        '
        Me.GridColumn143.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn143, "GridColumn143")
        Me.GridColumn143.FieldName = "Number"
        Me.GridColumn143.Name = "GridColumn143"
        '
        'GridColumn144
        '
        Me.GridColumn144.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn144, "GridColumn144")
        Me.GridColumn144.FieldName = "Name"
        Me.GridColumn144.Name = "GridColumn144"
        '
        'GridColumn139
        '
        resources.ApplyResources(Me.GridColumn139, "GridColumn139")
        Me.GridColumn139.FieldName = "Id"
        Me.GridColumn139.Name = "GridColumn139"
        '
        'GridColumn140
        '
        Me.GridColumn140.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn140, "GridColumn140")
        Me.GridColumn140.FieldName = "Number"
        Me.GridColumn140.Name = "GridColumn140"
        '
        'GridColumn141
        '
        Me.GridColumn141.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn141, "GridColumn141")
        Me.GridColumn141.FieldName = "Name"
        Me.GridColumn141.Name = "GridColumn141"
        '
        'GridColumn136
        '
        Me.GridColumn136.FieldName = "Id"
        Me.GridColumn136.Name = "GridColumn136"
        '
        'GridColumn137
        '
        Me.GridColumn137.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn137.FieldName = "Number"
        Me.GridColumn137.Name = "GridColumn137"
        '
        'GridColumn138
        '
        Me.GridColumn138.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn138.FieldName = "Name"
        Me.GridColumn138.Name = "GridColumn138"
        '
        'GridColumn133
        '
        resources.ApplyResources(Me.GridColumn133, "GridColumn133")
        Me.GridColumn133.FieldName = "Id"
        Me.GridColumn133.Name = "GridColumn133"
        '
        'GridColumn134
        '
        Me.GridColumn134.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn134, "GridColumn134")
        Me.GridColumn134.FieldName = "Number"
        Me.GridColumn134.Name = "GridColumn134"
        '
        'GridColumn135
        '
        Me.GridColumn135.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn135, "GridColumn135")
        Me.GridColumn135.FieldName = "Name"
        Me.GridColumn135.Name = "GridColumn135"
        '
        'GridColumn130
        '
        resources.ApplyResources(Me.GridColumn130, "GridColumn130")
        Me.GridColumn130.FieldName = "Id"
        Me.GridColumn130.Name = "GridColumn130"
        '
        'GridColumn131
        '
        Me.GridColumn131.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn131, "GridColumn131")
        Me.GridColumn131.FieldName = "Number"
        Me.GridColumn131.Name = "GridColumn131"
        '
        'GridColumn132
        '
        Me.GridColumn132.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn132, "GridColumn132")
        Me.GridColumn132.FieldName = "Name"
        Me.GridColumn132.Name = "GridColumn132"
        '
        'GridColumn127
        '
        resources.ApplyResources(Me.GridColumn127, "GridColumn127")
        Me.GridColumn127.FieldName = "Id"
        Me.GridColumn127.Name = "GridColumn127"
        '
        'GridColumn128
        '
        Me.GridColumn128.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn128, "GridColumn128")
        Me.GridColumn128.FieldName = "Number"
        Me.GridColumn128.Name = "GridColumn128"
        '
        'GridColumn129
        '
        Me.GridColumn129.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn129, "GridColumn129")
        Me.GridColumn129.FieldName = "Name"
        Me.GridColumn129.Name = "GridColumn129"
        '
        'GridColumn124
        '
        resources.ApplyResources(Me.GridColumn124, "GridColumn124")
        Me.GridColumn124.FieldName = "Id"
        Me.GridColumn124.Name = "GridColumn124"
        '
        'GridColumn125
        '
        Me.GridColumn125.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn125, "GridColumn125")
        Me.GridColumn125.FieldName = "Number"
        Me.GridColumn125.Name = "GridColumn125"
        '
        'GridColumn126
        '
        Me.GridColumn126.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn126, "GridColumn126")
        Me.GridColumn126.FieldName = "Name"
        Me.GridColumn126.Name = "GridColumn126"
        '
        'GridColumn121
        '
        resources.ApplyResources(Me.GridColumn121, "GridColumn121")
        Me.GridColumn121.FieldName = "Id"
        Me.GridColumn121.Name = "GridColumn121"
        '
        'GridColumn122
        '
        Me.GridColumn122.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn122, "GridColumn122")
        Me.GridColumn122.FieldName = "Number"
        Me.GridColumn122.Name = "GridColumn122"
        '
        'GridColumn123
        '
        Me.GridColumn123.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn123, "GridColumn123")
        Me.GridColumn123.FieldName = "Name"
        Me.GridColumn123.Name = "GridColumn123"
        '
        'GridColumn118
        '
        resources.ApplyResources(Me.GridColumn118, "GridColumn118")
        Me.GridColumn118.FieldName = "Id"
        Me.GridColumn118.Name = "GridColumn118"
        '
        'GridColumn119
        '
        Me.GridColumn119.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn119, "GridColumn119")
        Me.GridColumn119.FieldName = "Number"
        Me.GridColumn119.Name = "GridColumn119"
        '
        'GridColumn120
        '
        Me.GridColumn120.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn120, "GridColumn120")
        Me.GridColumn120.FieldName = "Name"
        Me.GridColumn120.Name = "GridColumn120"
        '
        'GridColumn115
        '
        resources.ApplyResources(Me.GridColumn115, "GridColumn115")
        Me.GridColumn115.FieldName = "Id"
        Me.GridColumn115.Name = "GridColumn115"
        '
        'GridColumn116
        '
        Me.GridColumn116.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn116, "GridColumn116")
        Me.GridColumn116.FieldName = "Number"
        Me.GridColumn116.Name = "GridColumn116"
        '
        'GridColumn117
        '
        Me.GridColumn117.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn117, "GridColumn117")
        Me.GridColumn117.FieldName = "Name"
        Me.GridColumn117.Name = "GridColumn117"
        '
        'GridColumn112
        '
        Me.GridColumn112.FieldName = "Id"
        Me.GridColumn112.Name = "GridColumn112"
        '
        'GridColumn113
        '
        Me.GridColumn113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn113.FieldName = "AccountCode"
        Me.GridColumn113.Name = "GridColumn113"
        '
        'GridColumn114
        '
        Me.GridColumn114.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn114.FieldName = "AccountName"
        Me.GridColumn114.Name = "GridColumn114"
        '
        'GridColumn109
        '
        resources.ApplyResources(Me.GridColumn109, "GridColumn109")
        Me.GridColumn109.FieldName = "Id"
        Me.GridColumn109.Name = "GridColumn109"
        '
        'GridColumn110
        '
        Me.GridColumn110.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn110, "GridColumn110")
        Me.GridColumn110.FieldName = "AccountCode"
        Me.GridColumn110.Name = "GridColumn110"
        '
        'GridColumn111
        '
        Me.GridColumn111.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn111, "GridColumn111")
        Me.GridColumn111.FieldName = "AccountName"
        Me.GridColumn111.Name = "GridColumn111"
        '
        'GridColumn106
        '
        resources.ApplyResources(Me.GridColumn106, "GridColumn106")
        Me.GridColumn106.FieldName = "Id"
        Me.GridColumn106.Name = "GridColumn106"
        '
        'GridColumn107
        '
        Me.GridColumn107.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn107, "GridColumn107")
        Me.GridColumn107.FieldName = "AccountCode"
        Me.GridColumn107.Name = "GridColumn107"
        '
        'GridColumn108
        '
        Me.GridColumn108.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn108, "GridColumn108")
        Me.GridColumn108.FieldName = "AccountName"
        Me.GridColumn108.Name = "GridColumn108"
        '
        'GridColumn103
        '
        resources.ApplyResources(Me.GridColumn103, "GridColumn103")
        Me.GridColumn103.FieldName = "Id"
        Me.GridColumn103.Name = "GridColumn103"
        '
        'GridColumn104
        '
        Me.GridColumn104.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn104, "GridColumn104")
        Me.GridColumn104.FieldName = "AccountCode"
        Me.GridColumn104.Name = "GridColumn104"
        '
        'GridColumn105
        '
        Me.GridColumn105.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn105, "GridColumn105")
        Me.GridColumn105.FieldName = "AccountName"
        Me.GridColumn105.Name = "GridColumn105"
        '
        'GridColumn100
        '
        resources.ApplyResources(Me.GridColumn100, "GridColumn100")
        Me.GridColumn100.FieldName = "Id"
        Me.GridColumn100.Name = "GridColumn100"
        '
        'GridColumn101
        '
        Me.GridColumn101.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn101, "GridColumn101")
        Me.GridColumn101.FieldName = "AccountCode"
        Me.GridColumn101.Name = "GridColumn101"
        '
        'GridColumn102
        '
        Me.GridColumn102.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn102, "GridColumn102")
        Me.GridColumn102.FieldName = "AccountName"
        Me.GridColumn102.Name = "GridColumn102"
        '
        'GridColumn97
        '
        resources.ApplyResources(Me.GridColumn97, "GridColumn97")
        Me.GridColumn97.FieldName = "Id"
        Me.GridColumn97.Name = "GridColumn97"
        '
        'GridColumn98
        '
        Me.GridColumn98.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn98, "GridColumn98")
        Me.GridColumn98.FieldName = "AccountCode"
        Me.GridColumn98.Name = "GridColumn98"
        '
        'GridColumn99
        '
        Me.GridColumn99.AppearanceHeader.Options.UseTextOptions = True
        resources.ApplyResources(Me.GridColumn99, "GridColumn99")
        Me.GridColumn99.FieldName = "AccountName"
        Me.GridColumn99.Name = "GridColumn99"
        '
        'GridColumn94
        '
        Me.GridColumn94.FieldName = "Id"
        Me.GridColumn94.Name = "GridColumn94"
        '
        'GridColumn95
        '
        Me.GridColumn95.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn95.FieldName = "AccountCode"
        Me.GridColumn95.Name = "GridColumn95"
        '
        'GridColumn96
        '
        Me.GridColumn96.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn96.FieldName = "AccountName"
        Me.GridColumn96.Name = "GridColumn96"
        '
        'GridColumn91
        '
        Me.GridColumn91.FieldName = "Id"
        Me.GridColumn91.Name = "GridColumn91"
        '
        'GridColumn92
        '
        Me.GridColumn92.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn92.FieldName = "AccountCode"
        Me.GridColumn92.Name = "GridColumn92"
        '
        'GridColumn93
        '
        Me.GridColumn93.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn93.FieldName = "AccountName"
        Me.GridColumn93.Name = "GridColumn93"
        '
        'GridColumn88
        '
        Me.GridColumn88.FieldName = "Id"
        Me.GridColumn88.Name = "GridColumn88"
        '
        'GridColumn89
        '
        Me.GridColumn89.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn89.FieldName = "AccountCode"
        Me.GridColumn89.Name = "GridColumn89"
        '
        'GridColumn90
        '
        Me.GridColumn90.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn90.FieldName = "AccountName"
        Me.GridColumn90.Name = "GridColumn90"
        '
        'GridColumn85
        '
        Me.GridColumn85.FieldName = "Id"
        Me.GridColumn85.Name = "GridColumn85"
        '
        'GridColumn86
        '
        Me.GridColumn86.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn86.FieldName = "AccountCode"
        Me.GridColumn86.Name = "GridColumn86"
        '
        'GridColumn87
        '
        Me.GridColumn87.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn87.FieldName = "AccountName"
        Me.GridColumn87.Name = "GridColumn87"
        '
        'GridColumn82
        '
        Me.GridColumn82.FieldName = "Id"
        Me.GridColumn82.Name = "GridColumn82"
        '
        'GridColumn83
        '
        Me.GridColumn83.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn83.FieldName = "AccountCode"
        Me.GridColumn83.Name = "GridColumn83"
        '
        'GridColumn84
        '
        Me.GridColumn84.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn84.FieldName = "AccountName"
        Me.GridColumn84.Name = "GridColumn84"
        '
        'GridColumn79
        '
        Me.GridColumn79.FieldName = "Id"
        Me.GridColumn79.Name = "GridColumn79"
        '
        'GridColumn80
        '
        Me.GridColumn80.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn80.FieldName = "AccountCode"
        Me.GridColumn80.Name = "GridColumn80"
        '
        'GridColumn81
        '
        Me.GridColumn81.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn81.FieldName = "AccountName"
        Me.GridColumn81.Name = "GridColumn81"
        '
        'GridColumn76
        '
        Me.GridColumn76.FieldName = "Id"
        Me.GridColumn76.Name = "GridColumn76"
        '
        'GridColumn77
        '
        Me.GridColumn77.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn77.FieldName = "AccountCode"
        Me.GridColumn77.Name = "GridColumn77"
        '
        'GridColumn78
        '
        Me.GridColumn78.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn78.FieldName = "AccountName"
        Me.GridColumn78.Name = "GridColumn78"
        '
        'GridColumn73
        '
        Me.GridColumn73.FieldName = "Id"
        Me.GridColumn73.Name = "GridColumn73"
        '
        'GridColumn74
        '
        Me.GridColumn74.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn74.FieldName = "AccountCode"
        Me.GridColumn74.Name = "GridColumn74"
        '
        'GridColumn75
        '
        Me.GridColumn75.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn75.FieldName = "AccountName"
        Me.GridColumn75.Name = "GridColumn75"
        '
        'GridColumn70
        '
        Me.GridColumn70.FieldName = "Id"
        Me.GridColumn70.Name = "GridColumn70"
        '
        'GridColumn71
        '
        Me.GridColumn71.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn71.FieldName = "AccountCode"
        Me.GridColumn71.Name = "GridColumn71"
        '
        'GridColumn72
        '
        Me.GridColumn72.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn72.FieldName = "AccountName"
        Me.GridColumn72.Name = "GridColumn72"
        '
        'GridColumn67
        '
        Me.GridColumn67.FieldName = "Id"
        Me.GridColumn67.Name = "GridColumn67"
        '
        'GridColumn68
        '
        Me.GridColumn68.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn68.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn68.FieldName = "AccountCode"
        Me.GridColumn68.Name = "GridColumn68"
        '
        'GridColumn69
        '
        Me.GridColumn69.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn69.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn69.FieldName = "AccountName"
        Me.GridColumn69.Name = "GridColumn69"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDlySupplier
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmSupplierMaintenance
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSupplierMaintenance"
        Me.Opacity = 1.0R
        Me.Tag = "623"
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
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlySupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlySupplier.ResumeLayout(False)
        CType(Me.INDtxtLastName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkResponsibleWarranty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkSeller.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkManufacturer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDckRetentionIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseTimeLimitDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccContactsControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccContactsControl.ResumeLayout(False)
        CType(Me.INDpceContactData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtCodeCMMS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtWebSite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCodeCMMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTimeLimitDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLastName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrContacts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemContacts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemWebSite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrSupplierType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckedListBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoCheckEdit1 As Presentation.Controls.IndigoCheckEdit
    Friend WithEvents CtrPUC1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlySupplier As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDseTimeLimitDays As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDpccContactsControl As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrContacts As Presentation.Controls.CtrContactosMaintenance
    Friend WithEvents INDpceContactData As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDtxtCodeCMMS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtWebSite As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDsleCity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGrSupplier As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCodeCMMS As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTimeLimitDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGrContacts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemContacts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemWebSite As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn59 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn60 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn62 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn63 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn64 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn65 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn66 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn67 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn68 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn69 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn70 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn72 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn73 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn77 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn78 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn79 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn80 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn81 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn82 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn83 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn84 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn85 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn86 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn87 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn88 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn89 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn90 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn91 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn92 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn93 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn94 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn95 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn96 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDckRetentionIVA As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents GridColumn97 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn98 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn99 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckedListBoxControl1 As Presentation.Controls.IndigoCheckedListBoxControl
    Friend WithEvents GridColumn100 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn101 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn102 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn103 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn104 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn105 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn106 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn107 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn108 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents GridColumn109 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn110 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn114 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn115 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn116 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn117 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn118 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn119 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn120 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn121 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn122 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn123 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn124 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn125 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn126 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn127 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn128 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn129 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn130 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn131 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn132 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn133 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn134 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn135 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn136 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn137 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn138 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn139 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn140 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn141 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn142 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn143 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn144 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn145 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn146 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn147 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridColumn150 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn151 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn152 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents GridColumn153 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn154 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn155 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn160 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn161 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn162 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn163 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn164 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn165 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn166 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn167 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn168 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn169 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn170 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn171 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoPopUpContainerEdit2 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDChkResponsibleWarranty As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkSeller As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkManufacturer As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDlyGrSupplierType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtLastName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemLastName As DevExpress.XtraLayout.LayoutControlItem
End Class

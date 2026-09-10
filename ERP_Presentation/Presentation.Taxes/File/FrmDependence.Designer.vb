Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDependence
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTextEResponsible = New DevExpress.XtraEditors.TextEdit()
        Me.indTextEResponsibleIdentityCard = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleCityMunicipality = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSleCostCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDTextEName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBeCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciSourceFinancing = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCostCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCityMunicipality = New DevExpress.XtraLayout.LayoutControlItem()
        Me.indlCIResponsibleIdentityCard = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciResponsible = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDTextEResponsible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indTextEResponsibleIdentityCard.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCityMunicipality.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTextEName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSourceFinancing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCityMunicipality, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indlCIResponsibleIdentityCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcBase
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDTextEResponsible)
        Me.INDLcBase.Controls.Add(Me.indTextEResponsibleIdentityCard)
        Me.INDLcBase.Controls.Add(Me.INDSleCityMunicipality)
        Me.INDLcBase.Controls.Add(Me.INDSleCostCenter)
        Me.INDLcBase.Controls.Add(Me.INDTextEName)
        Me.INDLcBase.Controls.Add(Me.INDBeCode)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(1261, 574)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDTextEResponsible
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTextEResponsible, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTextEResponsible, False)
        Me.INDTextEResponsible.Location = New System.Drawing.Point(24, 403)
        Me.IndigoTextEdit1.SetMascara(Me.INDTextEResponsible, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTextEResponsible.Name = "INDTextEResponsible"
        Me.INDTextEResponsible.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTextEResponsible.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextEResponsible.Properties.Appearance.Options.UseBackColor = True
        Me.INDTextEResponsible.Properties.Appearance.Options.UseFont = True
        Me.INDTextEResponsible.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextEResponsible.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTextEResponsible.Size = New System.Drawing.Size(386, 28)
        Me.INDTextEResponsible.StyleController = Me.INDLcBase
        Me.INDTextEResponsible.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTextEResponsible, 0)
        '
        'indTextEResponsibleIdentityCard
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.indTextEResponsibleIdentityCard, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.indTextEResponsibleIdentityCard, False)
        Me.indTextEResponsibleIdentityCard.Location = New System.Drawing.Point(24, 339)
        Me.IndigoTextEdit1.SetMascara(Me.indTextEResponsibleIdentityCard, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.indTextEResponsibleIdentityCard.Name = "indTextEResponsibleIdentityCard"
        Me.indTextEResponsibleIdentityCard.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.indTextEResponsibleIdentityCard.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indTextEResponsibleIdentityCard.Properties.Appearance.Options.UseBackColor = True
        Me.indTextEResponsibleIdentityCard.Properties.Appearance.Options.UseFont = True
        Me.indTextEResponsibleIdentityCard.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indTextEResponsibleIdentityCard.Properties.AppearanceFocused.Options.UseFont = True
        Me.indTextEResponsibleIdentityCard.Size = New System.Drawing.Size(386, 28)
        Me.indTextEResponsibleIdentityCard.StyleController = Me.INDLcBase
        Me.indTextEResponsibleIdentityCard.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.indTextEResponsibleIdentityCard, 0)
        '
        'INDSleCityMunicipality
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCityMunicipality, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCityMunicipality, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCityMunicipality, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.INDSleCityMunicipality.Location = New System.Drawing.Point(24, 275)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCityMunicipality, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCityMunicipality.Name = "INDSleCityMunicipality"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.INDSleCityMunicipality.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleCityMunicipality.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCityMunicipality.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCityMunicipality.Properties.Appearance.Options.UseFont = True
        Me.INDSleCityMunicipality.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSleCityMunicipality.Properties.NullText = ""
        Me.INDSleCityMunicipality.Properties.PopupSizeable = False
        Me.INDSleCityMunicipality.Properties.ShowFooter = False
        Me.INDSleCityMunicipality.Properties.View = Me.GridView1
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCityMunicipality, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCityMunicipality, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCityMunicipality, True)
        Me.INDSleCityMunicipality.Size = New System.Drawing.Size(386, 28)
        Me.INDSleCityMunicipality.StyleController = Me.INDLcBase
        Me.INDSleCityMunicipality.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCityMunicipality, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCityMunicipality, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCityMunicipality, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCityMunicipality, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCityMunicipality, False)
        '
        'GridView1
        '
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'INDSleCostCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCostCenter, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCostCenter, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCostCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCostCenter, False)
        Me.INDSleCostCenter.Location = New System.Drawing.Point(24, 211)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCostCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCostCenter.Name = "INDSleCostCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCostCenter, False)
        Me.INDSleCostCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleCostCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCostCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCostCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSleCostCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSleCostCenter.Properties.NullText = ""
        Me.INDSleCostCenter.Properties.PopupSizeable = False
        Me.INDSleCostCenter.Properties.ShowFooter = False
        Me.INDSleCostCenter.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCostCenter, True)
        Me.INDSleCostCenter.Size = New System.Drawing.Size(386, 28)
        Me.INDSleCostCenter.StyleController = Me.INDLcBase
        Me.INDSleCostCenter.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCostCenter, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCostCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCostCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCostCenter, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'INDTextEName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTextEName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTextEName, False)
        Me.INDTextEName.EnterMoveNextControl = True
        Me.INDTextEName.Location = New System.Drawing.Point(24, 147)
        Me.IndigoTextEdit1.SetMascara(Me.INDTextEName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTextEName.Name = "INDTextEName"
        Me.INDTextEName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTextEName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextEName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTextEName.Properties.Appearance.Options.UseFont = True
        Me.INDTextEName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextEName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTextEName.Size = New System.Drawing.Size(386, 28)
        Me.INDTextEName.StyleController = Me.INDLcBase
        Me.INDTextEName.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTextEName, 0)
        '
        'INDBeCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBeCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBeCode, False)
        Me.INDBeCode.EnterMoveNextControl = True
        Me.INDBeCode.Location = New System.Drawing.Point(24, 83)
        Me.IndigoTextEdit1.SetMascara(Me.INDBeCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBeCode.Name = "INDBeCode"
        Me.INDBeCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBeCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBeCode.Properties.Appearance.Options.UseFont = True
        Me.INDBeCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBeCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Taxes.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.INDBeCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBeCode.StyleController = Me.INDLcBase
        Me.INDBeCode.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBeCode, 0)
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciSourceFinancing})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(1261, 574)
        Me.INDLcgBase.TextVisible = False
        '
        'INDLciSourceFinancing
        '
        Me.INDLciSourceFinancing.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLciSourceFinancing.AppearanceGroup.Options.UseFont = True
        Me.INDLciSourceFinancing.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLciSourceFinancing.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciSourceFinancing.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLciSourceFinancing.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLciSourceFinancing.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLciSourceFinancing.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLciSourceFinancing.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLciSourceFinancing.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLciSourceFinancing.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLciSourceFinancing.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLciSourceFinancing.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLciSourceFinancing.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLciSourceFinancing, False)
        Me.INDLciSourceFinancing.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciName, Me.INDLciCostCenter, Me.INDLciCityMunicipality, Me.indlCIResponsibleIdentityCard, Me.INDLciResponsible})
        Me.INDLciSourceFinancing.Location = New System.Drawing.Point(0, 0)
        Me.INDLciSourceFinancing.Name = "INDLciSourceFinancing"
        Me.INDLciSourceFinancing.Size = New System.Drawing.Size(1241, 554)
        Me.INDLciSourceFinancing.Text = "Datos generales"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBeCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        '
        'INDLciName
        '
        Me.INDLciName.Control = Me.INDTextEName
        Me.INDLciName.Location = New System.Drawing.Point(0, 64)
        Me.INDLciName.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciName.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciName.Name = "INDLciName"
        Me.INDLciName.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciName.Text = "Nombre"
        Me.INDLciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciName.TextSize = New System.Drawing.Size(135, 21)
        '
        'INDLciCostCenter
        '
        Me.INDLciCostCenter.Control = Me.INDSleCostCenter
        Me.INDLciCostCenter.Location = New System.Drawing.Point(0, 128)
        Me.INDLciCostCenter.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCostCenter.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCostCenter.Name = "INDLciCostCenter"
        Me.INDLciCostCenter.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciCostCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCostCenter.Text = "Centro de Costo"
        Me.INDLciCostCenter.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCostCenter.TextSize = New System.Drawing.Size(135, 21)
        '
        'INDLciCityMunicipality
        '
        Me.INDLciCityMunicipality.Control = Me.INDSleCityMunicipality
        Me.INDLciCityMunicipality.Location = New System.Drawing.Point(0, 192)
        Me.INDLciCityMunicipality.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCityMunicipality.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCityMunicipality.Name = "INDLciCityMunicipality"
        Me.INDLciCityMunicipality.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciCityMunicipality.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCityMunicipality.Text = "Ciudad / Municipio"
        Me.INDLciCityMunicipality.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCityMunicipality.TextSize = New System.Drawing.Size(135, 21)
        '
        'indlCIResponsibleIdentityCard
        '
        Me.indlCIResponsibleIdentityCard.Control = Me.indTextEResponsibleIdentityCard
        Me.indlCIResponsibleIdentityCard.Location = New System.Drawing.Point(0, 256)
        Me.indlCIResponsibleIdentityCard.MaxSize = New System.Drawing.Size(390, 64)
        Me.indlCIResponsibleIdentityCard.MinSize = New System.Drawing.Size(390, 64)
        Me.indlCIResponsibleIdentityCard.Name = "indlCIResponsibleIdentityCard"
        Me.indlCIResponsibleIdentityCard.Size = New System.Drawing.Size(1217, 64)
        Me.indlCIResponsibleIdentityCard.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.indlCIResponsibleIdentityCard.Text = "Cedula Responsable"
        Me.indlCIResponsibleIdentityCard.TextLocation = DevExpress.Utils.Locations.Top
        Me.indlCIResponsibleIdentityCard.TextSize = New System.Drawing.Size(135, 21)
        '
        'INDLciResponsible
        '
        Me.INDLciResponsible.Control = Me.INDTextEResponsible
        Me.INDLciResponsible.Location = New System.Drawing.Point(0, 320)
        Me.INDLciResponsible.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciResponsible.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciResponsible.Name = "INDLciResponsible"
        Me.INDLciResponsible.Size = New System.Drawing.Size(1217, 175)
        Me.INDLciResponsible.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciResponsible.Text = "Responsable"
        Me.INDLciResponsible.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciResponsible.TextSize = New System.Drawing.Size(135, 21)
        '
        'FrmDependence
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmDependence"
        Me.Opacity = 1.0R
        Me.Tag = "1847"
        Me.Text = "Dependencia"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDTextEResponsible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indTextEResponsibleIdentityCard.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCityMunicipality.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTextEName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSourceFinancing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCityMunicipality, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indlCIResponsibleIdentityCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBeCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLciSourceFinancing As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTextEName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents indTextEResponsibleIdentityCard As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSleCityMunicipality As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleCostCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciCostCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCityMunicipality As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents indlCIResponsibleIdentityCard As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTextEResponsible As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciResponsible As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
End Class

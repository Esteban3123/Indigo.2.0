Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFileContainer
    Inherits Presentation.Controls.FormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFileContainer))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.INDlycgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgFile = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciUseInformation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDUseInformationRdg = New DevExpress.XtraEditors.RadioGroup()
        Me.INDlyFileContainer = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgleTypeField = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtNameMetadata = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtLengthField = New DevExpress.XtraEditors.TextEdit()
        Me.INDgcFields = New DevExpress.XtraGrid.GridControl()
        Me.INDgcvFields = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDclName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclFieldType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclLong = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclAction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDpceAction = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDpccActionMetadata = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDbtnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.INDFormsGc = New DevExpress.XtraGrid.GridControl()
        Me.INDFormsGv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbteDeleteForm = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDpccDeleteForm = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDbtnEliminarForm = New DevExpress.XtraEditors.SimpleButton()
        Me.INDAddFormSb = New DevExpress.XtraEditors.SimpleButton()
        Me.INDFormsGle = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDCodeTxt = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgFormulario = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiGridForm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciFormulario = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAddForm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgMetadata = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciNameMetadata = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciTypeField = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciLengthField = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAddMetadata = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciGridMetadata = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup()
        Me.IndigoRichEditControl1 = New Presentation.Controls.IndigoRichEditControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciUseInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDUseInformationRdg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyFileContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyFileContainer.SuspendLayout()
        CType(Me.INDgleTypeField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtNameMetadata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtLengthField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcvFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccActionMetadata, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccActionMetadata.SuspendLayout()
        CType(Me.INDFormsGc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDFormsGv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteDeleteForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccDeleteForm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccDeleteForm.SuspendLayout()
        CType(Me.INDFormsGle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCodeTxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgFormulario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiGridForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciFormulario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAddForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMetadata, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciNameMetadata, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciTypeField, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciLengthField, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAddMetadata, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciGridMetadata, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRichEditControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyFileContainer)
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
        'INDlycgRoot
        '
        Me.INDlycgRoot.AppearanceGroup.Font = CType(resources.GetObject("INDlycgRoot.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlycgRoot.AppearanceItemCaption.Font = CType(resources.GetObject("INDlycgRoot.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycgRoot, False)
        resources.ApplyResources(Me.INDlycgRoot, "INDlycgRoot")
        Me.INDlycgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycgRoot.GroupBordersVisible = False
        Me.INDlycgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgFile, Me.INDlcgFormulario, Me.INDlcgMetadata})
        Me.INDlycgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgRoot.Name = "INDlycgRoot"
        Me.INDlycgRoot.Size = New System.Drawing.Size(1464, 597)
        Me.INDlycgRoot.TextVisible = False
        '
        'INDlcgFile
        '
        Me.INDlcgFile.AppearanceGroup.Font = CType(resources.GetObject("INDlcgFile.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlcgFile.AppearanceGroup.Options.UseFont = True
        Me.INDlcgFile.AppearanceItemCaption.Font = CType(resources.GetObject("INDlcgFile.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlcgFile.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgFile.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlcgFile.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlcgFile.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgFile.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlcgFile.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlcgFile.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgFile.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlcgFile.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlcgFile.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgFile.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlcgFile.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlcgFile.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgFile.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlcgFile.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlcgFile.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgFile, False)
        resources.ApplyResources(Me.INDlcgFile, "INDlcgFile")
        Me.INDlcgFile.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciUseInformation, Me.INDlciCode, Me.INDlciName})
        Me.INDlcgFile.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgFile.Name = "INDlcgFile"
        Me.INDlcgFile.Size = New System.Drawing.Size(414, 577)
        '
        'INDlciUseInformation
        '
        Me.INDlciUseInformation.AppearanceItemCaption.Options.UseTextOptions = True
        Me.INDlciUseInformation.AppearanceItemCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlciUseInformation.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDlciUseInformation.Control = Me.INDUseInformationRdg
        resources.ApplyResources(Me.INDlciUseInformation, "INDlciUseInformation")
        Me.INDlciUseInformation.Location = New System.Drawing.Point(0, 72)
        Me.INDlciUseInformation.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciUseInformation.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciUseInformation.Name = "INDlciUseInformation"
        Me.INDlciUseInformation.Size = New System.Drawing.Size(390, 446)
        Me.INDlciUseInformation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciUseInformation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciUseInformation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciUseInformation.TextToControlDistance = 12
        '
        'INDUseInformationRdg
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDUseInformationRdg, False)
        Me.INDUseInformationRdg.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDUseInformationRdg, "INDUseInformationRdg")
        Me.INDUseInformationRdg.Name = "INDUseInformationRdg"
        Me.INDUseInformationRdg.Properties.Appearance.BackColor = CType(resources.GetObject("INDUseInformationRdg.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDUseInformationRdg.Properties.Appearance.Font = CType(resources.GetObject("INDUseInformationRdg.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDUseInformationRdg.Properties.Appearance.Options.UseBackColor = True
        Me.INDUseInformationRdg.Properties.Appearance.Options.UseFont = True
        Me.INDUseInformationRdg.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDUseInformationRdg.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDUseInformationRdg.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDUseInformationRdg.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDUseInformationRdg.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDUseInformationRdg.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDUseInformationRdg.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDUseInformationRdg.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDUseInformationRdg.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDUseInformationRdg.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDUseInformationRdg.Properties.Items"), Object), resources.GetString("INDUseInformationRdg.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDUseInformationRdg.Properties.Items2"), Object), resources.GetString("INDUseInformationRdg.Properties.Items3"))})
        Me.INDUseInformationRdg.StyleController = Me.INDlyFileContainer
        '
        'INDlyFileContainer
        '
        Me.INDlyFileContainer.AllowCustomization = False
        Me.INDlyFileContainer.Controls.Add(Me.INDgleTypeField)
        Me.INDlyFileContainer.Controls.Add(Me.INDBtnAdd)
        Me.INDlyFileContainer.Controls.Add(Me.INDtxtNameMetadata)
        Me.INDlyFileContainer.Controls.Add(Me.INDtxtLengthField)
        Me.INDlyFileContainer.Controls.Add(Me.INDgcFields)
        Me.INDlyFileContainer.Controls.Add(Me.INDFormsGc)
        Me.INDlyFileContainer.Controls.Add(Me.INDAddFormSb)
        Me.INDlyFileContainer.Controls.Add(Me.INDFormsGle)
        Me.INDlyFileContainer.Controls.Add(Me.INDUseInformationRdg)
        Me.INDlyFileContainer.Controls.Add(Me.INDTxtName)
        Me.INDlyFileContainer.Controls.Add(Me.INDCodeTxt)
        resources.ApplyResources(Me.INDlyFileContainer, "INDlyFileContainer")
        Me.LayoutControls.SetIsCustomizable(Me.INDlyFileContainer, False)
        Me.INDlyFileContainer.Name = "INDlyFileContainer"
        Me.INDlyFileContainer.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(394, 240, 428, 350)
        Me.INDlyFileContainer.Root = Me.INDlycgRoot
        '
        'INDgleTypeField
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleTypeField, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleTypeField, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleTypeField, False)
        Me.INDgleTypeField.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleTypeField, False)
        resources.ApplyResources(Me.INDgleTypeField, "INDgleTypeField")
        Me.IndigoTextEdit1.SetMascara(Me.INDgleTypeField, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleTypeField.Name = "INDgleTypeField"
        Me.INDgleTypeField.Properties.Appearance.BackColor = CType(resources.GetObject("INDgleTypeField.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDgleTypeField.Properties.Appearance.Font = CType(resources.GetObject("INDgleTypeField.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDgleTypeField.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleTypeField.Properties.Appearance.Options.UseFont = True
        Me.INDgleTypeField.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDgleTypeField.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDgleTypeField.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDgleTypeField.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDgleTypeField.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDgleTypeField.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDgleTypeField.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleTypeField.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleTypeField.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleTypeField.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDgleTypeField.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDgleTypeField.Properties.DisplayMember = "Item2"
        Me.INDgleTypeField.Properties.ImmediatePopup = True
        Me.INDgleTypeField.Properties.NullText = resources.GetString("INDgleTypeField.Properties.NullText")
        Me.INDgleTypeField.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.INDgleTypeField.Properties.ValueMember = "Item1"
        Me.INDgleTypeField.Properties.View = Me.GridView1
        Me.INDgleTypeField.StyleController = Me.INDlyFileContainer
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleTypeField, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleTypeField, 0)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("GridView1.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.GridView1.Appearance.FocusedRow.Font = CType(resources.GetObject("GridView1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn6
        '
        resources.ApplyResources(Me.GridColumn6, "GridColumn6")
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'INDBtnAdd
        '
        resources.ApplyResources(Me.INDBtnAdd, "INDBtnAdd")
        Me.INDBtnAdd.Image = Global.Presentation.DocumentalSystem.My.Resources.Resources.Agregar16
        Me.INDBtnAdd.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.StyleController = Me.INDlyFileContainer
        '
        'INDtxtNameMetadata
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtNameMetadata, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtNameMetadata, False)
        Me.INDtxtNameMetadata.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtNameMetadata, "INDtxtNameMetadata")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtNameMetadata, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtNameMetadata.Name = "INDtxtNameMetadata"
        Me.INDtxtNameMetadata.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtNameMetadata.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtNameMetadata.Properties.Appearance.Font = CType(resources.GetObject("INDtxtNameMetadata.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtNameMetadata.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtNameMetadata.Properties.Appearance.Options.UseFont = True
        Me.INDtxtNameMetadata.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtNameMetadata.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtNameMetadata.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtNameMetadata.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtNameMetadata.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtNameMetadata.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtNameMetadata.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtNameMetadata.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtNameMetadata.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtNameMetadata.Properties.MaxLength = 50
        Me.INDtxtNameMetadata.StyleController = Me.INDlyFileContainer
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtNameMetadata, 0)
        '
        'INDtxtLengthField
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtLengthField, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtLengthField, False)
        Me.INDtxtLengthField.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtLengthField, "INDtxtLengthField")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtLengthField, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtLengthField.Name = "INDtxtLengthField"
        Me.INDtxtLengthField.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtLengthField.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtLengthField.Properties.Appearance.Font = CType(resources.GetObject("INDtxtLengthField.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtLengthField.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtLengthField.Properties.Appearance.Options.UseFont = True
        Me.INDtxtLengthField.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtLengthField.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtLengthField.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtLengthField.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtLengthField.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtLengthField.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtLengthField.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtLengthField.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtLengthField.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtLengthField.Properties.Mask.EditMask = resources.GetString("INDtxtLengthField.Properties.Mask.EditMask")
        Me.INDtxtLengthField.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtLengthField.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtLengthField.Properties.MaxLength = 15
        Me.INDtxtLengthField.StyleController = Me.INDlyFileContainer
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtLengthField, 0)
        '
        'INDgcFields
        '
        resources.ApplyResources(Me.INDgcFields, "INDgcFields")
        Me.INDgcFields.MainView = Me.INDgcvFields
        Me.INDgcFields.Name = "INDgcFields"
        Me.INDgcFields.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDpceAction})
        Me.INDgcFields.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcvFields})
        '
        'INDgcvFields
        '
        Me.INDgcvFields.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("INDgcvFields.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.INDgcvFields.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgcvFields.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgcvFields.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgcvFields.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgcvFields.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgcvFields.Appearance.GroupRow.Font = CType(resources.GetObject("INDgcvFields.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgcvFields.Appearance.GroupRow.Options.UseFont = True
        Me.INDgcvFields.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgcvFields.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgcvFields.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcvFields.Appearance.Row.Font = CType(resources.GetObject("INDgcvFields.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgcvFields.Appearance.Row.Options.UseFont = True
        Me.INDgcvFields.Appearance.ViewCaption.Font = CType(resources.GetObject("INDgcvFields.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDgcvFields.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgcvFields.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDclName, Me.INDclFieldType, Me.INDclLong, Me.INDclAction})
        Me.INDgcvFields.GridControl = Me.INDgcFields
        Me.INDgcvFields.Name = "INDgcvFields"
        Me.INDgcvFields.OptionsCustomization.AllowFilter = False
        Me.INDgcvFields.OptionsCustomization.AllowGroup = False
        Me.INDgcvFields.OptionsView.AutoCalcPreviewLineCount = True
        Me.INDgcvFields.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcvFields.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcvFields.OptionsView.ShowAutoFilterRow = True
        Me.INDgcvFields.OptionsView.ShowGroupPanel = False
        Me.INDgcvFields.Tag = 346
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgcvFields, False)
        '
        'INDclName
        '
        resources.ApplyResources(Me.INDclName, "INDclName")
        Me.INDclName.FieldName = "Name"
        Me.INDclName.Name = "INDclName"
        '
        'INDclFieldType
        '
        resources.ApplyResources(Me.INDclFieldType, "INDclFieldType")
        Me.INDclFieldType.FieldName = "DataType"
        Me.INDclFieldType.Name = "INDclFieldType"
        '
        'INDclLong
        '
        resources.ApplyResources(Me.INDclLong, "INDclLong")
        Me.INDclLong.FieldName = "Long"
        Me.INDclLong.Name = "INDclLong"
        '
        'INDclAction
        '
        resources.ApplyResources(Me.INDclAction, "INDclAction")
        Me.INDclAction.ColumnEdit = Me.INDpceAction
        Me.INDclAction.Name = "INDclAction"
        '
        'INDpceAction
        '
        resources.ApplyResources(Me.INDpceAction, "INDpceAction")
        Me.INDpceAction.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDpceAction.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDpceAction.Name = "INDpceAction"
        Me.INDpceAction.PopupControl = Me.INDpccActionMetadata
        Me.INDpceAction.PopupSizeable = False
        Me.INDpceAction.ShowPopupCloseButton = False
        Me.INDpceAction.ShowPopupShadow = False
        Me.INDpceAction.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDpccActionMetadata
        '
        Me.INDpccActionMetadata.Controls.Add(Me.INDbtnDelete)
        Me.INDpccActionMetadata.Controls.Add(Me.INDbtnEdit)
        resources.ApplyResources(Me.INDpccActionMetadata, "INDpccActionMetadata")
        Me.INDpccActionMetadata.Name = "INDpccActionMetadata"
        '
        'INDbtnDelete
        '
        Me.INDbtnDelete.Appearance.Font = CType(resources.GetObject("INDbtnDelete.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnDelete.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDbtnDelete, "INDbtnDelete")
        Me.INDbtnDelete.Image = Global.Presentation.DocumentalSystem.My.Resources.Resources.eliminarLineaAzul
        Me.INDbtnDelete.Name = "INDbtnDelete"
        '
        'INDbtnEdit
        '
        Me.INDbtnEdit.Appearance.Font = CType(resources.GetObject("INDbtnEdit.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnEdit.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDbtnEdit, "INDbtnEdit")
        Me.INDbtnEdit.Image = Global.Presentation.DocumentalSystem.My.Resources.Resources.modificarLineaAzul
        Me.INDbtnEdit.Name = "INDbtnEdit"
        '
        'INDFormsGc
        '
        resources.ApplyResources(Me.INDFormsGc, "INDFormsGc")
        Me.INDFormsGc.MainView = Me.INDFormsGv
        Me.INDFormsGc.Name = "INDFormsGc"
        Me.INDFormsGc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit1, Me.INDbteDeleteForm})
        Me.INDFormsGc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDFormsGv})
        '
        'INDFormsGv
        '
        Me.INDFormsGv.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("INDFormsGv.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.INDFormsGv.Appearance.FocusedRow.Font = CType(resources.GetObject("INDFormsGv.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDFormsGv.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDFormsGv.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDFormsGv.Appearance.FocusedRow.Options.UseFont = True
        Me.INDFormsGv.Appearance.GroupRow.Font = CType(resources.GetObject("INDFormsGv.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDFormsGv.Appearance.GroupRow.Options.UseFont = True
        Me.INDFormsGv.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDFormsGv.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDFormsGv.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDFormsGv.Appearance.Row.Font = CType(resources.GetObject("INDFormsGv.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDFormsGv.Appearance.Row.Options.UseFont = True
        Me.INDFormsGv.Appearance.ViewCaption.Font = CType(resources.GetObject("INDFormsGv.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDFormsGv.Appearance.ViewCaption.Options.UseFont = True
        Me.INDFormsGv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn1, Me.GridColumn2, Me.GridColumn5})
        Me.INDFormsGv.GridControl = Me.INDFormsGc
        Me.INDFormsGv.Name = "INDFormsGv"
        Me.INDFormsGv.OptionsView.EnableAppearanceEvenRow = True
        Me.INDFormsGv.OptionsView.EnableAppearanceOddRow = True
        Me.INDFormsGv.OptionsView.ShowAutoFilterRow = True
        Me.INDFormsGv.OptionsView.ShowGroupPanel = False
        Me.INDFormsGv.Tag = 454
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDFormsGv, False)
        '
        'GridColumn7
        '
        resources.ApplyResources(Me.GridColumn7, "GridColumn7")
        Me.GridColumn7.FieldName = "NameModule"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "IdForm"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "FormName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        '
        'GridColumn5
        '
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.ColumnEdit = Me.INDbteDeleteForm
        Me.GridColumn5.Name = "GridColumn5"
        '
        'INDbteDeleteForm
        '
        resources.ApplyResources(Me.INDbteDeleteForm, "INDbteDeleteForm")
        resources.ApplyResources(SerializableAppearanceObject1, "SerializableAppearanceObject1")
        SerializableAppearanceObject1.Options.UseFont = True
        Me.INDbteDeleteForm.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteDeleteForm.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteDeleteForm.Buttons1"), CType(resources.GetObject("INDbteDeleteForm.Buttons2"), Integer), CType(resources.GetObject("INDbteDeleteForm.Buttons3"), Boolean), CType(resources.GetObject("INDbteDeleteForm.Buttons4"), Boolean), CType(resources.GetObject("INDbteDeleteForm.Buttons5"), Boolean), CType(resources.GetObject("INDbteDeleteForm.Buttons6"), DevExpress.XtraEditors.ImageLocation), CType(resources.GetObject("INDbteDeleteForm.Buttons7"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteDeleteForm.Buttons8"), CType(resources.GetObject("INDbteDeleteForm.Buttons9"), Object), CType(resources.GetObject("INDbteDeleteForm.Buttons10"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteDeleteForm.Buttons11"), Boolean))})
        Me.INDbteDeleteForm.Name = "INDbteDeleteForm"
        Me.INDbteDeleteForm.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'RepositoryItemPopupContainerEdit1
        '
        resources.ApplyResources(Me.RepositoryItemPopupContainerEdit1, "RepositoryItemPopupContainerEdit1")
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        Me.RepositoryItemPopupContainerEdit1.PopupControl = Me.INDpccDeleteForm
        Me.RepositoryItemPopupContainerEdit1.PopupSizeable = False
        Me.RepositoryItemPopupContainerEdit1.ShowPopupCloseButton = False
        Me.RepositoryItemPopupContainerEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDpccDeleteForm
        '
        Me.INDpccDeleteForm.Controls.Add(Me.INDbtnEliminarForm)
        resources.ApplyResources(Me.INDpccDeleteForm, "INDpccDeleteForm")
        Me.INDpccDeleteForm.Name = "INDpccDeleteForm"
        '
        'INDbtnEliminarForm
        '
        Me.INDbtnEliminarForm.Appearance.Font = CType(resources.GetObject("INDbtnEliminarForm.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnEliminarForm.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDbtnEliminarForm, "INDbtnEliminarForm")
        Me.INDbtnEliminarForm.Image = Global.Presentation.DocumentalSystem.My.Resources.Resources.eliminarLineaAzul
        Me.INDbtnEliminarForm.Name = "INDbtnEliminarForm"
        '
        'INDAddFormSb
        '
        Me.INDAddFormSb.Appearance.Font = CType(resources.GetObject("INDAddFormSb.Appearance.Font"), System.Drawing.Font)
        Me.INDAddFormSb.Appearance.ForeColor = CType(resources.GetObject("INDAddFormSb.Appearance.ForeColor"), System.Drawing.Color)
        Me.INDAddFormSb.Appearance.Options.UseFont = True
        Me.INDAddFormSb.Appearance.Options.UseForeColor = True
        Me.INDAddFormSb.Image = CType(resources.GetObject("INDAddFormSb.Image"), System.Drawing.Image)
        Me.INDAddFormSb.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.INDAddFormSb, "INDAddFormSb")
        Me.INDAddFormSb.Name = "INDAddFormSb"
        Me.INDAddFormSb.StyleController = Me.INDlyFileContainer
        '
        'INDFormsGle
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDFormsGle, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDFormsGle, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDFormsGle, False)
        Me.INDFormsGle.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDFormsGle, False)
        resources.ApplyResources(Me.INDFormsGle, "INDFormsGle")
        Me.IndigoTextEdit1.SetMascara(Me.INDFormsGle, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDFormsGle.Name = "INDFormsGle"
        Me.INDFormsGle.Properties.Appearance.BackColor = CType(resources.GetObject("INDFormsGle.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDFormsGle.Properties.Appearance.Font = CType(resources.GetObject("INDFormsGle.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDFormsGle.Properties.Appearance.Options.UseBackColor = True
        Me.INDFormsGle.Properties.Appearance.Options.UseFont = True
        Me.INDFormsGle.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDFormsGle.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDFormsGle.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDFormsGle.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDFormsGle.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDFormsGle.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDFormsGle.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDFormsGle.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDFormsGle.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDFormsGle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDFormsGle.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDFormsGle.Properties.DisplayMember = "Name"
        Me.INDFormsGle.Properties.ImmediatePopup = True
        Me.INDFormsGle.Properties.NullText = resources.GetString("INDFormsGle.Properties.NullText")
        Me.INDFormsGle.Properties.ValueMember = "Id"
        Me.INDFormsGle.Properties.View = Me.GridLookUpEdit1View
        Me.INDFormsGle.StyleController = Me.INDlyFileContainer
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDFormsGle, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDFormsGle, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8, Me.GridColumn3, Me.GridColumn4})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn8
        '
        resources.ApplyResources(Me.GridColumn8, "GridColumn8")
        Me.GridColumn8.FieldName = "Module.Name"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "Id"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.StyleController = Me.INDlyFileContainer
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        '
        'INDCodeTxt
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCodeTxt, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCodeTxt, True)
        resources.ApplyResources(Me.INDCodeTxt, "INDCodeTxt")
        Me.IndigoTextEdit1.SetMascara(Me.INDCodeTxt, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDCodeTxt.Name = "INDCodeTxt"
        Me.INDCodeTxt.Properties.Appearance.BackColor = CType(resources.GetObject("INDCodeTxt.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDCodeTxt.Properties.Appearance.Font = CType(resources.GetObject("INDCodeTxt.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDCodeTxt.Properties.Appearance.Options.UseBackColor = True
        Me.INDCodeTxt.Properties.Appearance.Options.UseFont = True
        Me.INDCodeTxt.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDCodeTxt.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDCodeTxt.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDCodeTxt.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDCodeTxt.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDCodeTxt.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDCodeTxt.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDCodeTxt.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDCodeTxt.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCodeTxt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDCodeTxt.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDCodeTxt.Properties.Buttons1"), CType(resources.GetObject("INDCodeTxt.Properties.Buttons2"), Integer), CType(resources.GetObject("INDCodeTxt.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDCodeTxt.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDCodeTxt.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDCodeTxt.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.DocumentalSystem.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, resources.GetString("INDCodeTxt.Properties.Buttons7"), CType(resources.GetObject("INDCodeTxt.Properties.Buttons8"), Object), CType(resources.GetObject("INDCodeTxt.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDCodeTxt.Properties.Buttons10"), Boolean))})
        Me.INDCodeTxt.Properties.Mask.EditMask = resources.GetString("INDCodeTxt.Properties.Mask.EditMask")
        Me.INDCodeTxt.Properties.Mask.MaskType = CType(resources.GetObject("INDCodeTxt.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDCodeTxt.StyleController = Me.INDlyFileContainer
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCodeTxt, 0)
        '
        'INDlciCode
        '
        Me.INDlciCode.Control = Me.INDCodeTxt
        resources.ApplyResources(Me.INDlciCode, "INDlciCode")
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.ShowInCustomizationForm = False
        Me.INDlciCode.Size = New System.Drawing.Size(390, 36)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciCode.TextToControlDistance = 12
        '
        'INDlciName
        '
        Me.INDlciName.Control = Me.INDTxtName
        resources.ApplyResources(Me.INDlciName, "INDlciName")
        Me.INDlciName.Location = New System.Drawing.Point(0, 36)
        Me.INDlciName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciName.Name = "INDlciName"
        Me.INDlciName.ShowInCustomizationForm = False
        Me.INDlciName.Size = New System.Drawing.Size(390, 36)
        Me.INDlciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciName.TextToControlDistance = 12
        '
        'INDlcgFormulario
        '
        Me.INDlcgFormulario.AppearanceGroup.Font = CType(resources.GetObject("INDlcgFormulario.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlcgFormulario.AppearanceGroup.Options.UseFont = True
        Me.INDlcgFormulario.AppearanceItemCaption.Font = CType(resources.GetObject("INDlcgFormulario.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlcgFormulario.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgFormulario.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlcgFormulario.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlcgFormulario.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgFormulario.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlcgFormulario.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlcgFormulario.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgFormulario.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlcgFormulario.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlcgFormulario.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgFormulario.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlcgFormulario.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlcgFormulario.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgFormulario.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlcgFormulario.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlcgFormulario.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgFormulario, False)
        resources.ApplyResources(Me.INDlcgFormulario, "INDlcgFormulario")
        Me.INDlcgFormulario.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiGridForm, Me.INDlciFormulario, Me.INDlciAddForm})
        Me.INDlcgFormulario.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgFormulario.Name = "INDlcgFormulario"
        Me.INDlcgFormulario.Size = New System.Drawing.Size(464, 577)
        '
        'INDlyiGridForm
        '
        Me.INDlyiGridForm.Control = Me.INDFormsGc
        resources.ApplyResources(Me.INDlyiGridForm, "INDlyiGridForm")
        Me.INDlyiGridForm.Location = New System.Drawing.Point(0, 36)
        Me.INDlyiGridForm.MinSize = New System.Drawing.Size(104, 24)
        Me.INDlyiGridForm.Name = "INDlyiGridForm"
        Me.INDlyiGridForm.Size = New System.Drawing.Size(440, 482)
        Me.INDlyiGridForm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiGridForm.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyiGridForm.TextVisible = False
        '
        'INDlciFormulario
        '
        Me.INDlciFormulario.Control = Me.INDFormsGle
        resources.ApplyResources(Me.INDlciFormulario, "INDlciFormulario")
        Me.INDlciFormulario.Location = New System.Drawing.Point(0, 0)
        Me.INDlciFormulario.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciFormulario.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciFormulario.Name = "INDlciFormulario"
        Me.INDlciFormulario.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 20, 2, 2)
        Me.INDlciFormulario.Size = New System.Drawing.Size(390, 36)
        Me.INDlciFormulario.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciFormulario.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciFormulario.TextVisible = False
        '
        'INDlciAddForm
        '
        Me.INDlciAddForm.Control = Me.INDAddFormSb
        resources.ApplyResources(Me.INDlciAddForm, "INDlciAddForm")
        Me.INDlciAddForm.Location = New System.Drawing.Point(390, 0)
        Me.INDlciAddForm.MaxSize = New System.Drawing.Size(50, 36)
        Me.INDlciAddForm.MinSize = New System.Drawing.Size(50, 36)
        Me.INDlciAddForm.Name = "INDlciAddForm"
        Me.INDlciAddForm.Size = New System.Drawing.Size(50, 36)
        Me.INDlciAddForm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAddForm.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAddForm.TextVisible = False
        '
        'INDlcgMetadata
        '
        Me.INDlcgMetadata.AppearanceGroup.Font = CType(resources.GetObject("INDlcgMetadata.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlcgMetadata.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMetadata.AppearanceItemCaption.Font = CType(resources.GetObject("INDlcgMetadata.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlcgMetadata.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMetadata.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlcgMetadata.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlcgMetadata.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMetadata.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlcgMetadata.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlcgMetadata.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMetadata.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlcgMetadata.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlcgMetadata.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMetadata.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlcgMetadata.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlcgMetadata.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMetadata.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlcgMetadata.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlcgMetadata.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMetadata, False)
        resources.ApplyResources(Me.INDlcgMetadata, "INDlcgMetadata")
        Me.INDlcgMetadata.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciNameMetadata, Me.INDlciTypeField, Me.INDlciLengthField, Me.INDlciAddMetadata, Me.INDlciGridMetadata, Me.EmptySpaceItem1, Me.EmptySpaceItem2})
        Me.INDlcgMetadata.Location = New System.Drawing.Point(878, 0)
        Me.INDlcgMetadata.Name = "INDlcgMetadata"
        Me.INDlcgMetadata.Size = New System.Drawing.Size(566, 577)
        Me.INDlcgMetadata.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciNameMetadata
        '
        Me.INDlciNameMetadata.Control = Me.INDtxtNameMetadata
        resources.ApplyResources(Me.INDlciNameMetadata, "INDlciNameMetadata")
        Me.INDlciNameMetadata.Location = New System.Drawing.Point(0, 0)
        Me.INDlciNameMetadata.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciNameMetadata.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciNameMetadata.Name = "INDlciNameMetadata"
        Me.INDlciNameMetadata.Size = New System.Drawing.Size(542, 36)
        Me.INDlciNameMetadata.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciNameMetadata.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciNameMetadata.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciNameMetadata.TextToControlDistance = 12
        '
        'INDlciTypeField
        '
        Me.INDlciTypeField.Control = Me.INDgleTypeField
        resources.ApplyResources(Me.INDlciTypeField, "INDlciTypeField")
        Me.INDlciTypeField.Location = New System.Drawing.Point(0, 36)
        Me.INDlciTypeField.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciTypeField.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciTypeField.Name = "INDlciTypeField"
        Me.INDlciTypeField.Size = New System.Drawing.Size(542, 36)
        Me.INDlciTypeField.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciTypeField.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciTypeField.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciTypeField.TextToControlDistance = 12
        '
        'INDlciLengthField
        '
        Me.INDlciLengthField.Control = Me.INDtxtLengthField
        resources.ApplyResources(Me.INDlciLengthField, "INDlciLengthField")
        Me.INDlciLengthField.Location = New System.Drawing.Point(0, 72)
        Me.INDlciLengthField.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciLengthField.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciLengthField.Name = "INDlciLengthField"
        Me.INDlciLengthField.Size = New System.Drawing.Size(542, 36)
        Me.INDlciLengthField.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciLengthField.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciLengthField.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciLengthField.TextToControlDistance = 12
        '
        'INDlciAddMetadata
        '
        Me.INDlciAddMetadata.Control = Me.INDBtnAdd
        Me.INDlciAddMetadata.ControlAlignment = System.Drawing.ContentAlignment.BottomRight
        resources.ApplyResources(Me.INDlciAddMetadata, "INDlciAddMetadata")
        Me.INDlciAddMetadata.Location = New System.Drawing.Point(341, 108)
        Me.INDlciAddMetadata.MaxSize = New System.Drawing.Size(50, 36)
        Me.INDlciAddMetadata.MinSize = New System.Drawing.Size(50, 36)
        Me.INDlciAddMetadata.Name = "INDlciAddMetadata"
        Me.INDlciAddMetadata.Size = New System.Drawing.Size(50, 36)
        Me.INDlciAddMetadata.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAddMetadata.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAddMetadata.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAddMetadata.TextToControlDistance = 0
        Me.INDlciAddMetadata.TextVisible = False
        '
        'INDlciGridMetadata
        '
        Me.INDlciGridMetadata.Control = Me.INDgcFields
        resources.ApplyResources(Me.INDlciGridMetadata, "INDlciGridMetadata")
        Me.INDlciGridMetadata.Location = New System.Drawing.Point(0, 144)
        Me.INDlciGridMetadata.MaxSize = New System.Drawing.Size(403, 346)
        Me.INDlciGridMetadata.MinSize = New System.Drawing.Size(403, 346)
        Me.INDlciGridMetadata.Name = "INDlciGridMetadata"
        Me.INDlciGridMetadata.Size = New System.Drawing.Size(542, 374)
        Me.INDlciGridMetadata.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciGridMetadata.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciGridMetadata.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciGridMetadata.TextToControlDistance = 0
        Me.INDlciGridMetadata.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(391, 108)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(12, 36)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(12, 36)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(151, 36)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem2, "EmptySpaceItem2")
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 108)
        Me.EmptySpaceItem2.MaxSize = New System.Drawing.Size(341, 36)
        Me.EmptySpaceItem2.MinSize = New System.Drawing.Size(341, 36)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(341, 36)
        Me.EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyFileContainer
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmFileContainer
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpccDeleteForm)
        Me.Controls.Add(Me.INDpccActionMetadata)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmFileContainer"
        Me.Opacity = 1.0R
        Me.Tag = "581"
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDpccActionMetadata, 0)
        Me.Controls.SetChildIndex(Me.INDpccDeleteForm, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciUseInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDUseInformationRdg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyFileContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyFileContainer.ResumeLayout(False)
        CType(Me.INDgleTypeField.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtNameMetadata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtLengthField.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcvFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccActionMetadata, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccActionMetadata.ResumeLayout(False)
        CType(Me.INDFormsGc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDFormsGv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteDeleteForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccDeleteForm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccDeleteForm.ResumeLayout(False)
        CType(Me.INDFormsGle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCodeTxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgFormulario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiGridForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciFormulario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAddForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMetadata, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciNameMetadata, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciTypeField, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciLengthField, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAddMetadata, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciGridMetadata, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRichEditControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoRichEditControl1 As Presentation.Controls.IndigoRichEditControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyFileContainer As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgleTypeField As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDtxtNameMetadata As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtLengthField As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDgcFields As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcvFields As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDclName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclFieldType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclLong As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclAction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceAction As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDFormsGc As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDFormsGv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDAddFormSb As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDFormsGle As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDUseInformationRdg As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDCodeTxt As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlycgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlcgFile As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciUseInformation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgFormulario As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyiGridForm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciFormulario As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciAddForm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciGridMetadata As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciLengthField As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciNameMetadata As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciTypeField As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciAddMetadata As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgMetadata As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDpccActionMetadata As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDbtnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDpccDeleteForm As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDbtnEliminarForm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbteDeleteForm As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
End Class

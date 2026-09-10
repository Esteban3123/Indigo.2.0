Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRegisterObjection
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
        Dim LytCServicio As DevExpress.XtraLayout.LayoutControlItem
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRegisterObjection))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LblDescripcionServicio = New DevExpress.XtraEditors.LabelControl()
        Me.INDlycRegisterObjection = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpceAddItemGloss = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPopupContAddGlosar = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDmemComment = New DevExpress.XtraEditors.MemoEdit()
        Me.INDgleResponsible = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDglvResponsible = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgleGeneralConcept = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyRegisterObjection = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiGeneralConcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiResponsible = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiComment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDAddorUpdateBtn = New DevExpress.XtraEditors.SimpleButton()
        Me.LblValorSeleccion = New DevExpress.XtraEditors.LabelControl()
        Me.INDgdcObjections = New DevExpress.XtraGrid.GridControl()
        Me.INDgdvObjections = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ClPrincipal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepMain = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ClConcepto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClConceptoName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLResponsable = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClValor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClValueAcceptFirstInstance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClValuePendingConciliation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClComentaryGlosa = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmemoRationale = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.ClValueReiteration = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClResponsibleReiteration = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClComentaryReiteration = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDActionColumn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDactionpce = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDactionpcc = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDbntUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.INDDeleteBtn = New DevExpress.XtraEditors.SimpleButton()
        Me.ClSpecificConceptId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNormative = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlycgObjections = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LytCValor = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl2 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.INDtxtValue = New DevExpress.XtraEditors.SpinEdit()
        LytCServicio = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LytCServicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycRegisterObjection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycRegisterObjection.SuspendLayout()
        CType(Me.INDpceAddItemGloss.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupContAddGlosar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPopupContAddGlosar.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDmemComment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleResponsible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglvResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleGeneralConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRegisterObjection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiGeneralConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgdcObjections, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgdvObjections, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoRationale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDactionpce, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDactionpcc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDactionpcc.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgObjections, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LytCValor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycRegisterObjection)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'LytCServicio
        '
        LytCServicio.AppearanceItemCaption.Font = CType(resources.GetObject("LytCServicio.AppearanceItemCaption.Font"), System.Drawing.Font)
        LytCServicio.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        LytCServicio.AppearanceItemCaption.Options.UseFont = True
        LytCServicio.AppearanceItemCaption.Options.UseForeColor = True
        LytCServicio.Control = Me.LblDescripcionServicio
        resources.ApplyResources(LytCServicio, "LytCServicio")
        LytCServicio.Location = New System.Drawing.Point(0, 0)
        LytCServicio.MaxSize = New System.Drawing.Size(0, 40)
        LytCServicio.MinSize = New System.Drawing.Size(109, 40)
        LytCServicio.Name = "LytCServicio"
        LytCServicio.Size = New System.Drawing.Size(1234, 40)
        LytCServicio.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        LytCServicio.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        LytCServicio.TextSize = New System.Drawing.Size(135, 21)
        LytCServicio.TextToControlDistance = 12
        '
        'LblDescripcionServicio
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LblDescripcionServicio, True)
        Me.LblDescripcionServicio.Appearance.Font = CType(resources.GetObject("LblDescripcionServicio.Appearance.Font"), System.Drawing.Font)
        Me.LblDescripcionServicio.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LblDescripcionServicio.Appearance.Options.UseFont = True
        Me.LblDescripcionServicio.Appearance.Options.UseForeColor = True
        Me.LblDescripcionServicio.Appearance.Options.UseTextOptions = True
        Me.LblDescripcionServicio.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LblDescripcionServicio, False)
        resources.ApplyResources(Me.LblDescripcionServicio, "LblDescripcionServicio")
        Me.LblDescripcionServicio.Name = "LblDescripcionServicio"
        Me.LblDescripcionServicio.StyleController = Me.INDlycRegisterObjection
        '
        'INDlycRegisterObjection
        '
        Me.INDlycRegisterObjection.Controls.Add(Me.INDpceAddItemGloss)
        Me.INDlycRegisterObjection.Controls.Add(Me.INDPopupContAddGlosar)
        Me.INDlycRegisterObjection.Controls.Add(Me.LblValorSeleccion)
        Me.INDlycRegisterObjection.Controls.Add(Me.LblDescripcionServicio)
        Me.INDlycRegisterObjection.Controls.Add(Me.INDgdcObjections)
        resources.ApplyResources(Me.INDlycRegisterObjection, "INDlycRegisterObjection")
        Me.INDlycRegisterObjection.Name = "INDlycRegisterObjection"
        Me.INDlycRegisterObjection.Root = Me.LayoutControlGroup1
        '
        'INDpceAddItemGloss
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceAddItemGloss, False)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceAddItemGloss, Nothing)
        resources.ApplyResources(Me.INDpceAddItemGloss, "INDpceAddItemGloss")
        Me.INDpceAddItemGloss.Name = "INDpceAddItemGloss"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceAddItemGloss, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceAddItemGloss, False)
        Me.INDpceAddItemGloss.Properties.Appearance.Font = CType(resources.GetObject("INDpceAddItemGloss.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDpceAddItemGloss.Properties.Appearance.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Glosas.My.Resources.Resources.Agregar16
        Me.INDpceAddItemGloss.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDpceAddItemGloss.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDpceAddItemGloss.Properties.Buttons1"), CType(resources.GetObject("INDpceAddItemGloss.Properties.Buttons2"), Integer), CType(resources.GetObject("INDpceAddItemGloss.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDpceAddItemGloss.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDpceAddItemGloss.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDpceAddItemGloss.Properties.Buttons6"), CType(resources.GetObject("INDpceAddItemGloss.Properties.Buttons7"), Object), CType(resources.GetObject("INDpceAddItemGloss.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDpceAddItemGloss.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDpceAddItemGloss.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceAddItemGloss.Properties.PopupControl = Me.INDPopupContAddGlosar
        Me.INDpceAddItemGloss.Properties.PopupFormSize = New System.Drawing.Size(444, 316)
        Me.INDpceAddItemGloss.Properties.PopupSizeable = False
        Me.INDpceAddItemGloss.Properties.ShowPopupCloseButton = False
        Me.INDpceAddItemGloss.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceAddItemGloss.StyleController = Me.INDlycRegisterObjection
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceAddItemGloss, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceAddItemGloss, Nothing)
        '
        'INDPopupContAddGlosar
        '
        Me.INDPopupContAddGlosar.Controls.Add(Me.LayoutControl1)
        Me.INDPopupContAddGlosar.Controls.Add(Me.INDAddorUpdateBtn)
        resources.ApplyResources(Me.INDPopupContAddGlosar, "INDPopupContAddGlosar")
        Me.INDPopupContAddGlosar.Name = "INDPopupContAddGlosar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomization = False
        Me.LayoutControl1.Controls.Add(Me.INDmemComment)
        Me.LayoutControl1.Controls.Add(Me.INDgleResponsible)
        Me.LayoutControl1.Controls.Add(Me.INDgleGeneralConcept)
        Me.LayoutControl1.Controls.Add(Me.INDtxtValue)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, False)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup2
        '
        'INDmemComment
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemComment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemComment, False)
        Me.INDmemComment.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDmemComment, "INDmemComment")
        Me.IndigoTextEdit1.SetMascara(Me.INDmemComment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemComment.Name = "INDmemComment"
        Me.INDmemComment.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemComment.Properties.Appearance.Font = CType(resources.GetObject("INDmemComment.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDmemComment.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemComment.Properties.Appearance.Options.UseFont = True
        Me.INDmemComment.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmemComment.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmemComment.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDmemComment.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDmemComment.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmemComment.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmemComment.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemComment.Properties.MaxLength = 4000
        Me.INDmemComment.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemComment, 0)
        '
        'INDgleResponsible
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleResponsible, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleResponsible, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleResponsible, False)
        Me.INDgleResponsible.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleResponsible, False)
        resources.ApplyResources(Me.INDgleResponsible, "INDgleResponsible")
        Me.IndigoTextEdit1.SetMascara(Me.INDgleResponsible, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleResponsible.Name = "INDgleResponsible"
        Me.INDgleResponsible.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDgleResponsible.Properties.Appearance.Font = CType(resources.GetObject("INDgleResponsible.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDgleResponsible.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleResponsible.Properties.Appearance.Options.UseFont = True
        Me.INDgleResponsible.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDgleResponsible.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDgleResponsible.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDgleResponsible.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDgleResponsible.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleResponsible.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleResponsible.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleResponsible.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDgleResponsible.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDgleResponsible.Properties.DisplayMember = "responsibleCodeName"
        Me.INDgleResponsible.Properties.ImmediatePopup = True
        Me.INDgleResponsible.Properties.MaxLength = 2
        Me.INDgleResponsible.Properties.NullText = resources.GetString("INDgleResponsible.Properties.NullText")
        Me.INDgleResponsible.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.INDgleResponsible.Properties.PopupView = Me.INDglvResponsible
        Me.INDgleResponsible.Properties.ValueMember = "Id"
        Me.INDgleResponsible.StyleController = Me.LayoutControl1
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleResponsible, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleResponsible, 0)
        '
        'INDglvResponsible
        '
        Me.INDglvResponsible.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDglvResponsible.Appearance.FocusedRow.Font = CType(resources.GetObject("INDglvResponsible.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDglvResponsible.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDglvResponsible.Appearance.FocusedRow.Options.UseFont = True
        Me.INDglvResponsible.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDglvResponsible.Appearance.GroupRow.Font = CType(resources.GetObject("INDglvResponsible.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDglvResponsible.Appearance.GroupRow.Options.UseFont = True
        Me.INDglvResponsible.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDglvResponsible.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDglvResponsible.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDglvResponsible.Appearance.Row.Font = CType(resources.GetObject("INDglvResponsible.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDglvResponsible.Appearance.Row.Options.UseFont = True
        Me.INDglvResponsible.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10})
        Me.INDglvResponsible.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDglvResponsible.Name = "INDglvResponsible"
        Me.INDglvResponsible.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDglvResponsible.OptionsView.EnableAppearanceEvenRow = True
        Me.INDglvResponsible.OptionsView.EnableAppearanceOddRow = True
        Me.INDglvResponsible.OptionsView.ShowAutoFilterRow = True
        Me.INDglvResponsible.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDglvResponsible, False)
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.GridColumn10, "GridColumn10")
        Me.GridColumn10.FieldName = "responsibleCodeName"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        '
        'INDgleGeneralConcept
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleGeneralConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleGeneralConcept, False)
        resources.ApplyResources(Me.INDgleGeneralConcept, "INDgleGeneralConcept")
        Me.IndigoTextEdit1.SetMascara(Me.INDgleGeneralConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleGeneralConcept.Name = "INDgleGeneralConcept"
        Me.INDgleGeneralConcept.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDgleGeneralConcept.Properties.Appearance.Font = CType(resources.GetObject("INDgleGeneralConcept.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDgleGeneralConcept.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleGeneralConcept.Properties.Appearance.Options.UseFont = True
        Me.INDgleGeneralConcept.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDgleGeneralConcept.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDgleGeneralConcept.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDgleGeneralConcept.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDgleGeneralConcept.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleGeneralConcept.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleGeneralConcept.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleGeneralConcept.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDgleGeneralConcept.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDgleGeneralConcept.Properties.DisplayMember = "NameCode"
        Me.INDgleGeneralConcept.Properties.NullText = resources.GetString("INDgleGeneralConcept.Properties.NullText")
        Me.INDgleGeneralConcept.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.INDgleGeneralConcept.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDgleGeneralConcept.Properties.ValueMember = "Id"
        Me.INDgleGeneralConcept.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleGeneralConcept, 0)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "NameCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyRegisterObjection})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(444, 248)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyRegisterObjection
        '
        Me.INDlyRegisterObjection.AppearanceGroup.Font = CType(resources.GetObject("INDlyRegisterObjection.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlyRegisterObjection.AppearanceGroup.Options.UseFont = True
        Me.INDlyRegisterObjection.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyRegisterObjection.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyRegisterObjection.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyRegisterObjection.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlyRegisterObjection.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlyRegisterObjection.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyRegisterObjection.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlyRegisterObjection.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlyRegisterObjection.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyRegisterObjection.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlyRegisterObjection.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlyRegisterObjection.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyRegisterObjection.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlyRegisterObjection.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlyRegisterObjection.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyRegisterObjection.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlyRegisterObjection.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlyRegisterObjection.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyRegisterObjection, False)
        resources.ApplyResources(Me.INDlyRegisterObjection, "INDlyRegisterObjection")
        Me.INDlyRegisterObjection.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiGeneralConcept, Me.INDlyiResponsible, Me.INDlyiValue, Me.INDlyiComment})
        Me.INDlyRegisterObjection.Location = New System.Drawing.Point(0, 0)
        Me.INDlyRegisterObjection.Name = "INDlyRegisterObjection"
        Me.INDlyRegisterObjection.Size = New System.Drawing.Size(424, 228)
        '
        'INDlyiGeneralConcept
        '
        Me.INDlyiGeneralConcept.Control = Me.INDgleGeneralConcept
        resources.ApplyResources(Me.INDlyiGeneralConcept, "INDlyiGeneralConcept")
        Me.INDlyiGeneralConcept.Location = New System.Drawing.Point(0, 0)
        Me.INDlyiGeneralConcept.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiGeneralConcept.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiGeneralConcept.Name = "INDlyiGeneralConcept"
        Me.INDlyiGeneralConcept.Size = New System.Drawing.Size(400, 36)
        Me.INDlyiGeneralConcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiGeneralConcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiGeneralConcept.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiGeneralConcept.TextToControlDistance = 12
        '
        'INDlyiResponsible
        '
        Me.INDlyiResponsible.Control = Me.INDgleResponsible
        resources.ApplyResources(Me.INDlyiResponsible, "INDlyiResponsible")
        Me.INDlyiResponsible.Location = New System.Drawing.Point(0, 36)
        Me.INDlyiResponsible.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiResponsible.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiResponsible.Name = "INDlyiResponsible"
        Me.INDlyiResponsible.Size = New System.Drawing.Size(400, 36)
        Me.INDlyiResponsible.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiResponsible.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiResponsible.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiResponsible.TextToControlDistance = 12
        '
        'INDlyiValue
        '
        Me.INDlyiValue.Control = Me.INDtxtValue
        resources.ApplyResources(Me.INDlyiValue, "INDlyiValue")
        Me.INDlyiValue.Location = New System.Drawing.Point(0, 72)
        Me.INDlyiValue.MinSize = New System.Drawing.Size(50, 25)
        Me.INDlyiValue.Name = "INDlyiValue"
        Me.INDlyiValue.Size = New System.Drawing.Size(400, 38)
        Me.INDlyiValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiValue.TextToControlDistance = 12
        '
        'INDlyiComment
        '
        Me.INDlyiComment.Control = Me.INDmemComment
        resources.ApplyResources(Me.INDlyiComment, "INDlyiComment")
        Me.INDlyiComment.Location = New System.Drawing.Point(0, 110)
        Me.INDlyiComment.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyiComment.MinSize = New System.Drawing.Size(390, 50)
        Me.INDlyiComment.Name = "INDlyiComment"
        Me.INDlyiComment.Size = New System.Drawing.Size(400, 50)
        Me.INDlyiComment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiComment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiComment.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiComment.TextToControlDistance = 12
        '
        'INDAddorUpdateBtn
        '
        Me.INDAddorUpdateBtn.Appearance.Font = CType(resources.GetObject("INDAddorUpdateBtn.Appearance.Font"), System.Drawing.Font)
        Me.INDAddorUpdateBtn.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDAddorUpdateBtn, "INDAddorUpdateBtn")
        Me.INDAddorUpdateBtn.Name = "INDAddorUpdateBtn"
        '
        'LblValorSeleccion
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LblValorSeleccion, True)
        Me.LblValorSeleccion.Appearance.Font = CType(resources.GetObject("LblValorSeleccion.Appearance.Font"), System.Drawing.Font)
        Me.LblValorSeleccion.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LblValorSeleccion.Appearance.Options.UseFont = True
        Me.LblValorSeleccion.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LblValorSeleccion, False)
        resources.ApplyResources(Me.LblValorSeleccion, "LblValorSeleccion")
        Me.LblValorSeleccion.Name = "LblValorSeleccion"
        Me.LblValorSeleccion.StyleController = Me.INDlycRegisterObjection
        '
        'INDgdcObjections
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgdcObjections, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgdcObjections, Nothing)
        Me.INDgdcObjections.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgdcObjections, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgdcObjections, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgdcObjections, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgdcObjections, False)
        resources.ApplyResources(Me.INDgdcObjections, "INDgdcObjections")
        Me.INDgdcObjections.MainView = Me.INDgdvObjections
        Me.INDgdcObjections.Name = "INDgdcObjections"
        Me.INDgdcObjections.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepMain, Me.INDactionpce, Me.INDmemoRationale})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgdcObjections, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgdcObjections.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgdvObjections})
        '
        'INDgdvObjections
        '
        Me.INDgdvObjections.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgdvObjections.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgdvObjections.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgdvObjections.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgdvObjections.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgdvObjections.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgdvObjections.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgdvObjections.Appearance.GroupRow.Font = CType(resources.GetObject("INDgdvObjections.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgdvObjections.Appearance.GroupRow.Options.UseFont = True
        Me.INDgdvObjections.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgdvObjections.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgdvObjections.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgdvObjections.Appearance.Row.Font = CType(resources.GetObject("INDgdvObjections.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgdvObjections.Appearance.Row.Options.UseFont = True
        Me.INDgdvObjections.Appearance.ViewCaption.Font = CType(resources.GetObject("INDgdvObjections.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDgdvObjections.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgdvObjections.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ClPrincipal, Me.ClConcepto, Me.ClConceptoName, Me.CLResponsable, Me.ClValor, Me.ClValueAcceptFirstInstance, Me.ClValuePendingConciliation, Me.ClComentaryGlosa, Me.ClValueReiteration, Me.ClResponsibleReiteration, Me.ClComentaryReiteration, Me.INDActionColumn, Me.ClSpecificConceptId, Me.INDColNormative})
        Me.INDgdvObjections.GridControl = Me.INDgdcObjections
        Me.INDgdvObjections.GroupCount = 1
        Me.INDgdvObjections.Name = "INDgdvObjections"
        Me.INDgdvObjections.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgdvObjections.OptionsCustomization.AllowFilter = False
        Me.INDgdvObjections.OptionsCustomization.AllowGroup = False
        Me.INDgdvObjections.OptionsCustomization.AllowQuickHideColumns = False
        Me.INDgdvObjections.OptionsCustomization.AllowRowSizing = True
        Me.INDgdvObjections.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgdvObjections.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgdvObjections.OptionsView.EnableAppearanceOddRow = True
        Me.INDgdvObjections.OptionsView.ShowAutoFilterRow = True
        Me.INDgdvObjections.OptionsView.ShowDetailButtons = False
        Me.INDgdvObjections.OptionsView.ShowGroupPanel = False
        Me.INDgdvObjections.OptionsView.ShowIndicator = False
        Me.INDgdvObjections.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColNormative, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgdvObjections, False)
        '
        'ClPrincipal
        '
        Me.ClPrincipal.AppearanceHeader.Options.UseTextOptions = True
        Me.ClPrincipal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClPrincipal.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ClPrincipal, "ClPrincipal")
        Me.ClPrincipal.ColumnEdit = Me.INDrepMain
        Me.ClPrincipal.FieldName = "MainGlosa"
        Me.ClPrincipal.Name = "ClPrincipal"
        Me.ClPrincipal.OptionsColumn.AllowEdit = False
        Me.ClPrincipal.OptionsColumn.AllowFocus = False
        Me.ClPrincipal.OptionsColumn.FixedWidth = True
        Me.ClPrincipal.OptionsColumn.ReadOnly = True
        '
        'INDrepMain
        '
        resources.ApplyResources(Me.INDrepMain, "INDrepMain")
        Me.INDrepMain.Name = "INDrepMain"
        '
        'ClConcepto
        '
        Me.ClConcepto.AppearanceCell.Options.UseTextOptions = True
        Me.ClConcepto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClConcepto.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClConcepto.AppearanceHeader.Options.UseTextOptions = True
        Me.ClConcepto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClConcepto.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ClConcepto, "ClConcepto")
        Me.ClConcepto.FieldName = "CodeGlosa"
        Me.ClConcepto.Name = "ClConcepto"
        Me.ClConcepto.OptionsColumn.AllowEdit = False
        Me.ClConcepto.OptionsColumn.AllowFocus = False
        Me.ClConcepto.OptionsColumn.FixedWidth = True
        '
        'ClConceptoName
        '
        resources.ApplyResources(Me.ClConceptoName, "ClConceptoName")
        Me.ClConceptoName.FieldName = "ConceptGlosasCodeName"
        Me.ClConceptoName.Name = "ClConceptoName"
        Me.ClConceptoName.OptionsColumn.AllowEdit = False
        Me.ClConceptoName.OptionsColumn.AllowFocus = False
        '
        'CLResponsable
        '
        Me.CLResponsable.AppearanceHeader.Options.UseTextOptions = True
        Me.CLResponsable.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CLResponsable.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.CLResponsable, "CLResponsable")
        Me.CLResponsable.FieldName = "ResponsibleName"
        Me.CLResponsable.Name = "CLResponsable"
        Me.CLResponsable.OptionsColumn.AllowEdit = False
        Me.CLResponsable.OptionsColumn.AllowFocus = False
        '
        'ClValor
        '
        Me.ClValor.AppearanceCell.Options.UseTextOptions = True
        Me.ClValor.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ClValor.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClValor.AppearanceHeader.Options.UseTextOptions = True
        Me.ClValor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClValor.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ClValor, "ClValor")
        Me.ClValor.DisplayFormat.FormatString = "C2"
        Me.ClValor.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ClValor.FieldName = "ValueGlosado"
        Me.ClValor.Name = "ClValor"
        Me.ClValor.OptionsColumn.AllowEdit = False
        Me.ClValor.OptionsColumn.FixedWidth = True
        '
        'ClValueAcceptFirstInstance
        '
        resources.ApplyResources(Me.ClValueAcceptFirstInstance, "ClValueAcceptFirstInstance")
        Me.ClValueAcceptFirstInstance.DisplayFormat.FormatString = "C"
        Me.ClValueAcceptFirstInstance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ClValueAcceptFirstInstance.FieldName = "ValueAcceptedFirstInstance"
        Me.ClValueAcceptFirstInstance.Name = "ClValueAcceptFirstInstance"
        Me.ClValueAcceptFirstInstance.OptionsColumn.AllowEdit = False
        '
        'ClValuePendingConciliation
        '
        resources.ApplyResources(Me.ClValuePendingConciliation, "ClValuePendingConciliation")
        Me.ClValuePendingConciliation.DisplayFormat.FormatString = "C2"
        Me.ClValuePendingConciliation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ClValuePendingConciliation.FieldName = "ValuePendingConciliation"
        Me.ClValuePendingConciliation.Name = "ClValuePendingConciliation"
        Me.ClValuePendingConciliation.OptionsColumn.AllowEdit = False
        Me.ClValuePendingConciliation.OptionsColumn.AllowFocus = False
        Me.ClValuePendingConciliation.OptionsColumn.ReadOnly = True
        '
        'ClComentaryGlosa
        '
        resources.ApplyResources(Me.ClComentaryGlosa, "ClComentaryGlosa")
        Me.ClComentaryGlosa.ColumnEdit = Me.INDmemoRationale
        Me.ClComentaryGlosa.FieldName = "RationaleGlosa"
        Me.ClComentaryGlosa.Name = "ClComentaryGlosa"
        '
        'INDmemoRationale
        '
        resources.ApplyResources(Me.INDmemoRationale, "INDmemoRationale")
        EditorButtonImageOptions2.Image = Global.Presentation.Glosas.My.Resources.Resources.comentario
        Me.INDmemoRationale.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDmemoRationale.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDmemoRationale.Buttons1"), CType(resources.GetObject("INDmemoRationale.Buttons2"), Integer), CType(resources.GetObject("INDmemoRationale.Buttons3"), Boolean), CType(resources.GetObject("INDmemoRationale.Buttons4"), Boolean), CType(resources.GetObject("INDmemoRationale.Buttons5"), Boolean), EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, resources.GetString("INDmemoRationale.Buttons6"), CType(resources.GetObject("INDmemoRationale.Buttons7"), Object), CType(resources.GetObject("INDmemoRationale.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDmemoRationale.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDmemoRationale.Name = "INDmemoRationale"
        Me.INDmemoRationale.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'ClValueReiteration
        '
        Me.ClValueReiteration.AppearanceHeader.BackColor = System.Drawing.Color.Gray
        Me.ClValueReiteration.AppearanceHeader.Options.UseBackColor = True
        resources.ApplyResources(Me.ClValueReiteration, "ClValueReiteration")
        Me.ClValueReiteration.DisplayFormat.FormatString = "C"
        Me.ClValueReiteration.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ClValueReiteration.FieldName = "ValueReiterated"
        Me.ClValueReiteration.Name = "ClValueReiteration"
        Me.ClValueReiteration.OptionsColumn.AllowEdit = False
        Me.ClValueReiteration.OptionsColumn.AllowFocus = False
        '
        'ClResponsibleReiteration
        '
        Me.ClResponsibleReiteration.AppearanceHeader.BackColor = System.Drawing.Color.Gray
        Me.ClResponsibleReiteration.AppearanceHeader.Options.UseBackColor = True
        resources.ApplyResources(Me.ClResponsibleReiteration, "ClResponsibleReiteration")
        Me.ClResponsibleReiteration.FieldName = "ResponsibleReiterationName"
        Me.ClResponsibleReiteration.Name = "ClResponsibleReiteration"
        Me.ClResponsibleReiteration.OptionsColumn.AllowEdit = False
        Me.ClResponsibleReiteration.OptionsColumn.AllowFocus = False
        '
        'ClComentaryReiteration
        '
        Me.ClComentaryReiteration.AppearanceHeader.BackColor = System.Drawing.Color.Gray
        Me.ClComentaryReiteration.AppearanceHeader.Options.UseBackColor = True
        resources.ApplyResources(Me.ClComentaryReiteration, "ClComentaryReiteration")
        Me.ClComentaryReiteration.ColumnEdit = Me.INDmemoRationale
        Me.ClComentaryReiteration.FieldName = "RationaleReiteration"
        Me.ClComentaryReiteration.Name = "ClComentaryReiteration"
        '
        'INDActionColumn
        '
        resources.ApplyResources(Me.INDActionColumn, "INDActionColumn")
        Me.INDActionColumn.ColumnEdit = Me.INDactionpce
        Me.INDActionColumn.MaxWidth = 75
        Me.INDActionColumn.MinWidth = 75
        Me.INDActionColumn.Name = "INDActionColumn"
        '
        'INDactionpce
        '
        resources.ApplyResources(Me.INDactionpce, "INDactionpce")
        Me.INDactionpce.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDactionpce.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDactionpce.Name = "INDactionpce"
        Me.INDactionpce.PopupControl = Me.INDactionpcc
        Me.INDactionpce.PopupSizeable = False
        Me.INDactionpce.ShowPopupCloseButton = False
        Me.INDactionpce.ShowPopupShadow = False
        Me.INDactionpce.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDactionpcc
        '
        Me.INDactionpcc.Controls.Add(Me.INDbntUpdate)
        Me.INDactionpcc.Controls.Add(Me.INDDeleteBtn)
        resources.ApplyResources(Me.INDactionpcc, "INDactionpcc")
        Me.INDactionpcc.Name = "INDactionpcc"
        '
        'INDbntUpdate
        '
        Me.INDbntUpdate.Appearance.Font = CType(resources.GetObject("INDbntUpdate.Appearance.Font"), System.Drawing.Font)
        Me.INDbntUpdate.Appearance.Options.UseFont = True
        Me.INDbntUpdate.ImageOptions.Image = Global.Presentation.Glosas.My.Resources.Resources.EditarAzul32
        resources.ApplyResources(Me.INDbntUpdate, "INDbntUpdate")
        Me.INDbntUpdate.Name = "INDbntUpdate"
        '
        'INDDeleteBtn
        '
        Me.INDDeleteBtn.Appearance.Font = CType(resources.GetObject("INDDeleteBtn.Appearance.Font"), System.Drawing.Font)
        Me.INDDeleteBtn.Appearance.Options.UseFont = True
        Me.INDDeleteBtn.ImageOptions.Image = Global.Presentation.Glosas.My.Resources.Resources.EliminarAzul32
        resources.ApplyResources(Me.INDDeleteBtn, "INDDeleteBtn")
        Me.INDDeleteBtn.Name = "INDDeleteBtn"
        '
        'ClSpecificConceptId
        '
        Me.ClSpecificConceptId.FieldName = "ConceptGlosas.Id"
        Me.ClSpecificConceptId.Name = "ClSpecificConceptId"
        Me.ClSpecificConceptId.OptionsColumn.AllowEdit = False
        Me.ClSpecificConceptId.OptionsColumn.AllowFocus = False
        '
        'INDColNormative
        '
        resources.ApplyResources(Me.INDColNormative, "INDColNormative")
        Me.INDColNormative.FieldName = "IsNormative"
        Me.INDColNormative.Name = "INDColNormative"
        Me.INDColNormative.OptionsColumn.AllowEdit = False
        Me.INDColNormative.OptionsColumn.AllowFocus = False
        Me.INDColNormative.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AllowHide = False
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlycgObjections})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1278, 919)
        '
        'INDlycgObjections
        '
        Me.INDlycgObjections.AllowHide = False
        Me.INDlycgObjections.AppearanceGroup.Font = CType(resources.GetObject("INDlycgObjections.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlycgObjections.AppearanceGroup.Options.UseFont = True
        Me.INDlycgObjections.AppearanceItemCaption.Font = CType(resources.GetObject("INDlycgObjections.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlycgObjections.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgObjections.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlycgObjections.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlycgObjections.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgObjections.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlycgObjections.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlycgObjections.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycgObjections.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlycgObjections.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlycgObjections.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgObjections.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlycgObjections.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlycgObjections.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycgObjections.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlycgObjections.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlycgObjections.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycgObjections, False)
        resources.ApplyResources(Me.INDlycgObjections, "INDlycgObjections")
        Me.INDlycgObjections.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, LytCServicio, Me.LytCValor, Me.LayoutControlItem1})
        Me.INDlycgObjections.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgObjections.Name = "INDlycgObjections"
        Me.INDlycgObjections.Size = New System.Drawing.Size(1258, 899)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AllowHide = False
        Me.LayoutControlItem7.Control = Me.INDgdcObjections
        resources.ApplyResources(Me.LayoutControlItem7, "LayoutControlItem7")
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 112)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(807, 0)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(807, 24)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(1234, 719)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'LytCValor
        '
        Me.LytCValor.AppearanceItemCaption.Font = CType(resources.GetObject("LytCValor.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LytCValor.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LytCValor.AppearanceItemCaption.Options.UseFont = True
        Me.LytCValor.AppearanceItemCaption.Options.UseForeColor = True
        Me.LytCValor.Control = Me.LblValorSeleccion
        resources.ApplyResources(Me.LytCValor, "LytCValor")
        Me.LytCValor.Location = New System.Drawing.Point(0, 40)
        Me.LytCValor.MaxSize = New System.Drawing.Size(736, 36)
        Me.LytCValor.MinSize = New System.Drawing.Size(736, 36)
        Me.LytCValor.Name = "LytCValor"
        Me.LytCValor.Size = New System.Drawing.Size(1234, 36)
        Me.LytCValor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LytCValor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LytCValor.TextSize = New System.Drawing.Size(135, 21)
        Me.LytCValor.TextToControlDistance = 12
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDpceAddItemGloss
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 76)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(807, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(807, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1234, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit2.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'GridColumn7
        '
        Me.GridColumn7.FieldName = "responsibleCode"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        Me.GridColumn8.FieldName = "responsibleName"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn11.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn11.FieldName = "specificconceptCode"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.FixedWidth = True
        '
        'GridColumn12
        '
        Me.GridColumn12.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn12.FieldName = "specificconceptName"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn13.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn13.FieldName = "detailedconceptCode"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.FixedWidth = True
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn14.FieldName = "detailedconceptName"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'INDtxtValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtValue, False)
        resources.ApplyResources(Me.INDtxtValue, "INDtxtValue")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtValue.Name = "INDtxtValue"
        Me.INDtxtValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtValue.Properties.Appearance.Font = CType(resources.GetObject("INDtxtValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtValue.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtValue.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDtxtValue.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDtxtValue.Properties.DisplayFormat.FormatString = "c2"
        Me.INDtxtValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtValue.Properties.EditFormat.FormatString = "c2"
        Me.INDtxtValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtValue.Properties.Mask.BeepOnError = CType(resources.GetObject("INDtxtValue.Properties.Mask.BeepOnError"), Boolean)
        Me.INDtxtValue.Properties.Mask.EditMask = resources.GetString("INDtxtValue.Properties.Mask.EditMask")
        Me.INDtxtValue.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDtxtValue.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDtxtValue.Properties.NullText = resources.GetString("INDtxtValue.Properties.NullText")
        Me.INDtxtValue.Properties.NullValuePrompt = resources.GetString("INDtxtValue.Properties.NullValuePrompt")
        Me.INDtxtValue.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtValue, 0)
        '
        'FrmRegisterObjection
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmRegisterObjection"
        Me.Opacity = 1.0R
        Me.Tag = "528"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LytCServicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycRegisterObjection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycRegisterObjection.ResumeLayout(False)
        CType(Me.INDpceAddItemGloss.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupContAddGlosar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPopupContAddGlosar.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDmemComment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleResponsible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglvResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleGeneralConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRegisterObjection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiGeneralConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgdcObjections, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgdvObjections, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoRationale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDactionpce, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDactionpcc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDactionpcc.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgObjections, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LytCValor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlycRegisterObjection As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDmemComment As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDgdcObjections As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgdvObjections As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlycgObjections As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents ClConcepto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLResponsable As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClValor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClPrincipal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepMain As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDgleResponsible As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDglvResponsible As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDAddorUpdateBtn As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDActionColumn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDactionpcc As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDDeleteBtn As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDactionpce As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents ClValueReiteration As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClResponsibleReiteration As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClComentaryReiteration As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDmemoRationale As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyRegisterObjection As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyiGeneralConcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiResponsible As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiComment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupContAddGlosar As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl2 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents ClComentaryGlosa As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClSpecificConceptId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LblValorSeleccion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblDescripcionServicio As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LytCValor As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ClConceptoName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClValueAcceptFirstInstance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbntUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ClValuePendingConciliation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceAddItemGloss As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColNormative As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgleGeneralConcept As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtValue As DevExpress.XtraEditors.SpinEdit
End Class

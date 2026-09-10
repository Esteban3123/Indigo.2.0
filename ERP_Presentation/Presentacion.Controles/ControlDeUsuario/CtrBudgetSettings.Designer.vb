<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrBudgetSettings
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrRubroSelect1 = New Presentation.Controls.CtrRubroSelect()
        Me.INDtxtEntryType = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtResource = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtRubro = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtValidity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtBudgetEntity = New DevExpress.XtraEditors.TextEdit()
        Me.INDpopRubroSelect = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliRubroSelect = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliRubro = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliResource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliEntryType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControl1.SuspendLayout()
        CType(Me.INDtxtEntryType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtResource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtRubro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopRubroSelect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliRubroSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliRubro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliResource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliEntryType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.PopupContainerControl1)
        Me.LayoutControl1.Controls.Add(Me.INDtxtEntryType)
        Me.LayoutControl1.Controls.Add(Me.INDtxtResource)
        Me.LayoutControl1.Controls.Add(Me.INDtxtRubro)
        Me.LayoutControl1.Controls.Add(Me.INDtxtValidity)
        Me.LayoutControl1.Controls.Add(Me.INDTxtBudgetEntity)
        Me.LayoutControl1.Controls.Add(Me.INDpopRubroSelect)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(391, 216)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.CtrRubroSelect1)
        Me.PopupContainerControl1.Location = New System.Drawing.Point(10, 205)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(621, 341)
        Me.PopupContainerControl1.TabIndex = 10
        '
        'CtrRubroSelect1
        '
        Me.CtrRubroSelect1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrRubroSelect1.Location = New System.Drawing.Point(0, 0)
        Me.CtrRubroSelect1.Name = "CtrRubroSelect1"
        Me.CtrRubroSelect1.Size = New System.Drawing.Size(621, 341)
        Me.CtrRubroSelect1.TabIndex = 0
        '
        'INDtxtEntryType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtEntryType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtEntryType, False)
        Me.INDtxtEntryType.Enabled = False
        Me.INDtxtEntryType.Location = New System.Drawing.Point(159, 182)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtEntryType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtEntryType.Name = "INDtxtEntryType"
        Me.INDtxtEntryType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtEntryType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEntryType.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtEntryType.Properties.Appearance.Options.UseFont = True
        Me.INDtxtEntryType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtEntryType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtEntryType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEntryType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtEntryType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtEntryType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtEntryType.Size = New System.Drawing.Size(229, 28)
        Me.INDtxtEntryType.StyleController = Me.LayoutControl1
        Me.INDtxtEntryType.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtEntryType, 0)
        '
        'INDtxtResource
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtResource, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtResource, False)
        Me.INDtxtResource.Enabled = False
        Me.INDtxtResource.Location = New System.Drawing.Point(159, 146)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtResource, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtResource.Name = "INDtxtResource"
        Me.INDtxtResource.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtResource.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtResource.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtResource.Properties.Appearance.Options.UseFont = True
        Me.INDtxtResource.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtResource.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtResource.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtResource.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtResource.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtResource.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtResource.Size = New System.Drawing.Size(229, 28)
        Me.INDtxtResource.StyleController = Me.LayoutControl1
        Me.INDtxtResource.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtResource, 0)
        '
        'INDtxtRubro
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtRubro, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtRubro, False)
        Me.INDtxtRubro.Enabled = False
        Me.INDtxtRubro.Location = New System.Drawing.Point(159, 110)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtRubro, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtRubro.Name = "INDtxtRubro"
        Me.INDtxtRubro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtRubro.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRubro.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtRubro.Properties.Appearance.Options.UseFont = True
        Me.INDtxtRubro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtRubro.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtRubro.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRubro.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtRubro.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtRubro.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtRubro.Size = New System.Drawing.Size(229, 28)
        Me.INDtxtRubro.StyleController = Me.LayoutControl1
        Me.INDtxtRubro.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtRubro, 0)
        '
        'INDtxtValidity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtValidity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtValidity, False)
        Me.INDtxtValidity.Enabled = False
        Me.INDtxtValidity.Location = New System.Drawing.Point(159, 74)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtValidity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtValidity.Name = "INDtxtValidity"
        Me.INDtxtValidity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtValidity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtValidity.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtValidity.Properties.Appearance.Options.UseFont = True
        Me.INDtxtValidity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtValidity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtValidity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtValidity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtValidity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtValidity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtValidity.Size = New System.Drawing.Size(229, 28)
        Me.INDtxtValidity.StyleController = Me.LayoutControl1
        Me.INDtxtValidity.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtValidity, 0)
        '
        'INDTxtBudgetEntity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtBudgetEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtBudgetEntity, False)
        Me.INDTxtBudgetEntity.Enabled = False
        Me.INDTxtBudgetEntity.Location = New System.Drawing.Point(159, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtBudgetEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtBudgetEntity.Name = "INDTxtBudgetEntity"
        Me.INDTxtBudgetEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtBudgetEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtBudgetEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtBudgetEntity.Properties.Appearance.Options.UseFont = True
        Me.INDTxtBudgetEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtBudgetEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtBudgetEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtBudgetEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtBudgetEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtBudgetEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtBudgetEntity.Size = New System.Drawing.Size(229, 28)
        Me.INDTxtBudgetEntity.StyleController = Me.LayoutControl1
        Me.INDTxtBudgetEntity.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtBudgetEntity, 0)
        '
        'INDpopRubroSelect
        '
        Me.INDpopRubroSelect.EditValue = "Seleccione Rubro"
        Me.INDpopRubroSelect.Location = New System.Drawing.Point(2, 2)
        Me.INDpopRubroSelect.Name = "INDpopRubroSelect"
        Me.INDpopRubroSelect.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpopRubroSelect.Properties.Appearance.Options.UseFont = True
        Me.INDpopRubroSelect.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpopRubroSelect.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDpopRubroSelect.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpopRubroSelect.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDpopRubroSelect.Properties.PopupControl = Me.PopupContainerControl1
        Me.INDpopRubroSelect.Size = New System.Drawing.Size(386, 28)
        Me.INDpopRubroSelect.StyleController = Me.LayoutControl1
        Me.INDpopRubroSelect.TabIndex = 4
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliRubroSelect, Me.INDliEntity, Me.INDliValidity, Me.INDliRubro, Me.INDliResource, Me.INDliEntryType})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(391, 216)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliRubroSelect
        '
        Me.INDliRubroSelect.Control = Me.INDpopRubroSelect
        Me.INDliRubroSelect.CustomizationFormText = "LayoutControlItem1"
        Me.INDliRubroSelect.Location = New System.Drawing.Point(0, 0)
        Me.INDliRubroSelect.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliRubroSelect.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliRubroSelect.Name = "INDliRubroSelect"
        Me.INDliRubroSelect.Size = New System.Drawing.Size(391, 36)
        Me.INDliRubroSelect.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliRubroSelect.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliRubroSelect.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliRubroSelect.TextToControlDistance = 0
        Me.INDliRubroSelect.TextVisible = False
        '
        'INDliEntity
        '
        Me.INDliEntity.Control = Me.INDTxtBudgetEntity
        Me.INDliEntity.CustomizationFormText = "LayoutControlItem2"
        Me.INDliEntity.Location = New System.Drawing.Point(0, 36)
        Me.INDliEntity.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliEntity.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliEntity.Name = "INDliEntity"
        Me.INDliEntity.Size = New System.Drawing.Size(391, 36)
        Me.INDliEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliEntity.Text = "Entidad"
        Me.INDliEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliEntity.TextSize = New System.Drawing.Size(145, 13)
        Me.INDliEntity.TextToControlDistance = 12
        '
        'INDliValidity
        '
        Me.INDliValidity.Control = Me.INDtxtValidity
        Me.INDliValidity.CustomizationFormText = "LayoutControlItem3"
        Me.INDliValidity.Location = New System.Drawing.Point(0, 72)
        Me.INDliValidity.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliValidity.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliValidity.Name = "INDliValidity"
        Me.INDliValidity.Size = New System.Drawing.Size(391, 36)
        Me.INDliValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliValidity.Text = "Vigencia"
        Me.INDliValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliValidity.TextSize = New System.Drawing.Size(145, 13)
        Me.INDliValidity.TextToControlDistance = 12
        '
        'INDliRubro
        '
        Me.INDliRubro.Control = Me.INDtxtRubro
        Me.INDliRubro.CustomizationFormText = "LayoutControlItem4"
        Me.INDliRubro.Location = New System.Drawing.Point(0, 108)
        Me.INDliRubro.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliRubro.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliRubro.Name = "INDliRubro"
        Me.INDliRubro.Size = New System.Drawing.Size(391, 36)
        Me.INDliRubro.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliRubro.Text = "Rubro"
        Me.INDliRubro.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliRubro.TextSize = New System.Drawing.Size(145, 13)
        Me.INDliRubro.TextToControlDistance = 12
        '
        'INDliResource
        '
        Me.INDliResource.Control = Me.INDtxtResource
        Me.INDliResource.CustomizationFormText = "LayoutControlItem5"
        Me.INDliResource.Location = New System.Drawing.Point(0, 144)
        Me.INDliResource.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliResource.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliResource.Name = "INDliResource"
        Me.INDliResource.Size = New System.Drawing.Size(391, 36)
        Me.INDliResource.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliResource.Text = "Recurso"
        Me.INDliResource.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliResource.TextSize = New System.Drawing.Size(145, 13)
        Me.INDliResource.TextToControlDistance = 12
        '
        'INDliEntryType
        '
        Me.INDliEntryType.Control = Me.INDtxtEntryType
        Me.INDliEntryType.CustomizationFormText = "LayoutControlItem6"
        Me.INDliEntryType.Location = New System.Drawing.Point(0, 180)
        Me.INDliEntryType.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliEntryType.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliEntryType.Name = "INDliEntryType"
        Me.INDliEntryType.Size = New System.Drawing.Size(391, 36)
        Me.INDliEntryType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliEntryType.Text = "Tipo Ingreso"
        Me.INDliEntryType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliEntryType.TextSize = New System.Drawing.Size(145, 13)
        Me.INDliEntryType.TextToControlDistance = 12
        '
        'CtrBudgetSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrBudgetSettings"
        Me.Size = New System.Drawing.Size(391, 216)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.INDtxtEntryType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtResource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtRubro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopRubroSelect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliRubroSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliRubro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliResource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliEntryType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDpopRubroSelect As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDliRubroSelect As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtEntryType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDtxtResource As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtRubro As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtValidity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtBudgetEntity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDliEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliRubro As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliResource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliEntryType As DevExpress.XtraLayout.LayoutControlItem
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrRubroSelect1 As Presentation.Controls.CtrRubroSelect

End Class

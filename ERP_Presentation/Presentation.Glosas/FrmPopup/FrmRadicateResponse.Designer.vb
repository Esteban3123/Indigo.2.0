Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRadicateResponse
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbteCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtSave = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeCommentConfirm = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdteDateConfirm = New DevExpress.XtraEditors.DateEdit()
        Me.INDmeWhoReceive = New DevExpress.XtraEditors.TextEdit()
        Me.INDmeWhoSend = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiDateRadicate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiWhoSend = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyIbtAccept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyibeCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiWhoReceive = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiCommentConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDmeCommentConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDateConfirm.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDateConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeWhoReceive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeWhoSend.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiDateRadicate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiWhoSend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyIbtAccept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyibeCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiWhoReceive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiCommentConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.AutoSize = True
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(478, 342)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(478, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(478, 130)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AutoSize = True
        Me.LayoutControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly
        Me.LayoutControl1.Controls.Add(Me.INDbteCancel)
        Me.LayoutControl1.Controls.Add(Me.INDbtSave)
        Me.LayoutControl1.Controls.Add(Me.INDmeCommentConfirm)
        Me.LayoutControl1.Controls.Add(Me.INDdteDateConfirm)
        Me.LayoutControl1.Controls.Add(Me.INDmeWhoReceive)
        Me.LayoutControl1.Controls.Add(Me.INDmeWhoSend)
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(467, 324)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDbteCancel
        '
        Me.INDbteCancel.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDbteCancel.Appearance.Options.UseFont = True
        Me.INDbteCancel.Location = New System.Drawing.Point(235, 273)
        Me.INDbteCancel.MaximumSize = New System.Drawing.Size(219, 38)
        Me.INDbteCancel.MinimumSize = New System.Drawing.Size(213, 18)
        Me.INDbteCancel.Name = "INDbteCancel"
        Me.INDbteCancel.Size = New System.Drawing.Size(219, 34)
        Me.INDbteCancel.StyleController = Me.LayoutControl1
        Me.INDbteCancel.TabIndex = 7
        Me.INDbteCancel.Text = "Cancelar"
        '
        'INDbtSave
        '
        Me.INDbtSave.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDbtSave.Appearance.Options.UseFont = True
        Me.INDbtSave.Location = New System.Drawing.Point(12, 273)
        Me.INDbtSave.MaximumSize = New System.Drawing.Size(223, 38)
        Me.INDbtSave.MinimumSize = New System.Drawing.Size(213, 18)
        Me.INDbtSave.Name = "INDbtSave"
        Me.INDbtSave.Size = New System.Drawing.Size(219, 34)
        Me.INDbtSave.StyleController = Me.LayoutControl1
        Me.INDbtSave.TabIndex = 6
        Me.INDbtSave.Text = "Guardar"
        '
        'INDmeCommentConfirm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeCommentConfirm, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeCommentConfirm, False)
        Me.INDmeCommentConfirm.Location = New System.Drawing.Point(159, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeCommentConfirm, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeCommentConfirm.Name = "INDmeCommentConfirm"
        Me.INDmeCommentConfirm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeCommentConfirm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeCommentConfirm.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeCommentConfirm.Properties.Appearance.Options.UseFont = True
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeCommentConfirm.Size = New System.Drawing.Size(289, 96)
        Me.INDmeCommentConfirm.StyleController = Me.LayoutControl1
        Me.INDmeCommentConfirm.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeCommentConfirm, 0)
        '
        'INDdteDateConfirm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDateConfirm, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDateConfirm, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteDateConfirm, False)
        Me.INDdteDateConfirm.EditValue = Nothing
        Me.INDdteDateConfirm.Location = New System.Drawing.Point(159, 41)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDateConfirm, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteDateConfirm, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteDateConfirm.Name = "INDdteDateConfirm"
        Me.INDdteDateConfirm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteDateConfirm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDateConfirm.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDateConfirm.Properties.Appearance.Options.UseFont = True
        Me.INDdteDateConfirm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteDateConfirm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteDateConfirm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDateConfirm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteDateConfirm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteDateConfirm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDateConfirm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDateConfirm.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDateConfirm.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteDateConfirm.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDateConfirm.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDateConfirm.Size = New System.Drawing.Size(289, 28)
        Me.INDdteDateConfirm.StyleController = Me.LayoutControl1
        Me.INDdteDateConfirm.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDateConfirm, 0)
        '
        'INDmeWhoReceive
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeWhoReceive, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeWhoReceive, False)
        Me.INDmeWhoReceive.Location = New System.Drawing.Point(159, 77)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeWhoReceive, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeWhoReceive.Name = "INDmeWhoReceive"
        Me.INDmeWhoReceive.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeWhoReceive.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeWhoReceive.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeWhoReceive.Properties.Appearance.Options.UseFont = True
        Me.INDmeWhoReceive.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeWhoReceive.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeWhoReceive.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeWhoReceive.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeWhoReceive.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeWhoReceive.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeWhoReceive.Size = New System.Drawing.Size(289, 28)
        Me.INDmeWhoReceive.StyleController = Me.LayoutControl1
        Me.INDmeWhoReceive.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeWhoReceive, 0)
        '
        'INDmeWhoSend
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeWhoSend, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeWhoSend, False)
        Me.INDmeWhoSend.Location = New System.Drawing.Point(159, 113)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeWhoSend, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeWhoSend.Name = "INDmeWhoSend"
        Me.INDmeWhoSend.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeWhoSend.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeWhoSend.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeWhoSend.Properties.Appearance.Options.UseFont = True
        Me.INDmeWhoSend.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeWhoSend.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeWhoSend.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeWhoSend.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeWhoSend.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeWhoSend.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeWhoSend.Size = New System.Drawing.Size(289, 28)
        Me.INDmeWhoSend.StyleController = Me.LayoutControl1
        Me.INDmeWhoSend.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeWhoSend, 0)
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[False]
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiDateRadicate, Me.INDlyiCommentConfirm, Me.INDlyiWhoReceive, Me.INDlyiWhoSend, Me.INDlyibeCancel, Me.INDLyIbtAccept, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(467, 324)
        Me.LayoutControlGroup1.Text = "Radicación Respuesta"
        '
        'INDlyiDateRadicate
        '
        Me.INDlyiDateRadicate.Control = Me.INDdteDateConfirm
        Me.INDlyiDateRadicate.CustomizationFormText = "Fecha Radicación"
        Me.INDlyiDateRadicate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyiDateRadicate.MaxSize = New System.Drawing.Size(440, 36)
        Me.INDlyiDateRadicate.MinSize = New System.Drawing.Size(440, 36)
        Me.INDlyiDateRadicate.Name = "INDlyiDateRadicate"
        Me.INDlyiDateRadicate.Size = New System.Drawing.Size(447, 36)
        Me.INDlyiDateRadicate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiDateRadicate.Text = "Fecha Radicación"
        Me.INDlyiDateRadicate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiDateRadicate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiDateRadicate.TextToControlDistance = 12
        '
        'INDlyiWhoSend
        '
        Me.INDlyiWhoSend.Control = Me.INDmeWhoSend
        Me.INDlyiWhoSend.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyiWhoSend.CustomizationFormText = "Quien Envió"
        Me.INDlyiWhoSend.Location = New System.Drawing.Point(0, 72)
        Me.INDlyiWhoSend.MaxSize = New System.Drawing.Size(440, 36)
        Me.INDlyiWhoSend.MinSize = New System.Drawing.Size(440, 36)
        Me.INDlyiWhoSend.Name = "INDlyiWhoSend"
        Me.INDlyiWhoSend.OptionsTableLayoutItem.RowIndex = 1
        Me.INDlyiWhoSend.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 5)
        Me.INDlyiWhoSend.Size = New System.Drawing.Size(447, 36)
        Me.INDlyiWhoSend.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiWhoSend.Text = "Quien Envió"
        Me.INDlyiWhoSend.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiWhoSend.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiWhoSend.TextToControlDistance = 12
        '
        'INDLyIbtAccept
        '
        Me.INDLyIbtAccept.Control = Me.INDbtSave
        Me.INDLyIbtAccept.CustomizationFormText = "Aceptar"
        Me.INDLyIbtAccept.Location = New System.Drawing.Point(0, 232)
        Me.INDLyIbtAccept.MaxSize = New System.Drawing.Size(223, 38)
        Me.INDLyIbtAccept.MinSize = New System.Drawing.Size(223, 38)
        Me.INDLyIbtAccept.Name = "INDLyIbtAccept"
        Me.INDLyIbtAccept.OptionsTableLayoutItem.ColumnIndex = 1
        Me.INDLyIbtAccept.OptionsTableLayoutItem.RowIndex = 1
        Me.INDLyIbtAccept.Size = New System.Drawing.Size(223, 43)
        Me.INDLyIbtAccept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyIbtAccept.Text = "Aceptar"
        Me.INDLyIbtAccept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyIbtAccept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyIbtAccept.TextToControlDistance = 0
        Me.INDLyIbtAccept.TextVisible = False
        '
        'INDlyibeCancel
        '
        Me.INDlyibeCancel.Control = Me.INDbteCancel
        Me.INDlyibeCancel.CustomizationFormText = "Cancelar"
        Me.INDlyibeCancel.Location = New System.Drawing.Point(223, 232)
        Me.INDlyibeCancel.MaxSize = New System.Drawing.Size(243, 38)
        Me.INDlyibeCancel.MinSize = New System.Drawing.Size(223, 38)
        Me.INDlyibeCancel.Name = "INDlyibeCancel"
        Me.INDlyibeCancel.OptionsTableLayoutItem.RowIndex = 2
        Me.INDlyibeCancel.Size = New System.Drawing.Size(224, 43)
        Me.INDlyibeCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyibeCancel.Text = "Cancelar"
        Me.INDlyibeCancel.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyibeCancel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyibeCancel.TextToControlDistance = 0
        Me.INDlyibeCancel.TextVisible = False
        '
        'INDlyiWhoReceive
        '
        Me.INDlyiWhoReceive.Control = Me.INDmeWhoReceive
        Me.INDlyiWhoReceive.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyiWhoReceive.CustomizationFormText = "Quien Recibe"
        Me.INDlyiWhoReceive.Location = New System.Drawing.Point(0, 36)
        Me.INDlyiWhoReceive.MaxSize = New System.Drawing.Size(440, 36)
        Me.INDlyiWhoReceive.MinSize = New System.Drawing.Size(440, 36)
        Me.INDlyiWhoReceive.Name = "INDlyiWhoReceive"
        Me.INDlyiWhoReceive.OptionsTableLayoutItem.ColumnIndex = 1
        Me.INDlyiWhoReceive.OptionsTableLayoutItem.RowIndex = 2
        Me.INDlyiWhoReceive.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 5)
        Me.INDlyiWhoReceive.Size = New System.Drawing.Size(447, 36)
        Me.INDlyiWhoReceive.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiWhoReceive.Text = "Quien Recibe"
        Me.INDlyiWhoReceive.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiWhoReceive.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiWhoReceive.TextToControlDistance = 12
        '
        'INDlyiCommentConfirm
        '
        Me.INDlyiCommentConfirm.Control = Me.INDmeCommentConfirm
        Me.INDlyiCommentConfirm.CustomizationFormText = "Comentario"
        Me.INDlyiCommentConfirm.Location = New System.Drawing.Point(0, 108)
        Me.INDlyiCommentConfirm.MaxSize = New System.Drawing.Size(440, 100)
        Me.INDlyiCommentConfirm.MinSize = New System.Drawing.Size(440, 100)
        Me.INDlyiCommentConfirm.Name = "INDlyiCommentConfirm"
        Me.INDlyiCommentConfirm.OptionsTableLayoutItem.ColumnIndex = 1
        Me.INDlyiCommentConfirm.Size = New System.Drawing.Size(447, 100)
        Me.INDlyiCommentConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiCommentConfirm.Text = "Comentario"
        Me.INDlyiCommentConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiCommentConfirm.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDlyiCommentConfirm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiCommentConfirm.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 208)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(104, 24)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(104, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.OptionsTableLayoutItem.RowIndex = 3
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(447, 24)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmRadicateResponse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 477)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmRadicateResponse"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Radicación Respuesta"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        Me.INDPanelControlBase.PerformLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDmeCommentConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDateConfirm.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDateConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeWhoReceive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeWhoSend.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiDateRadicate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiWhoSend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyIbtAccept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyibeCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiWhoReceive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiCommentConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDmeCommentConfirm As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdteDateConfirm As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyiDateRadicate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiCommentConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDbteCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDLyIbtAccept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyibeCancel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyiWhoReceive As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeWhoReceive As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDmeWhoSend As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyiWhoSend As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class

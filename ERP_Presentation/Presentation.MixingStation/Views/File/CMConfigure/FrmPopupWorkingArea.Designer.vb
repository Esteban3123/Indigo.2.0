<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopupWorkingArea
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPopupWorkingArea))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGleStatus = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMeObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTeDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeCode = New DevExpress.XtraEditors.TextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciLineCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescripcion = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGleStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLineCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDescripcion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGleStatus)
        Me.LayoutControl1.Controls.Add(Me.INDMeObservation)
        Me.LayoutControl1.Controls.Add(Me.INDTeDescription)
        Me.LayoutControl1.Controls.Add(Me.INDTeCode)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(425, 339)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGleStatus
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleStatus, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleStatus, False)
        Me.INDGleStatus.EnterMoveNextControl = True
        Me.INDGleStatus.Location = New System.Drawing.Point(12, 274)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleStatus, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleStatus.Name = "INDGleStatus"
        Me.INDGleStatus.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleStatus.Properties.Appearance.Options.UseFont = True
        Me.INDGleStatus.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleStatus.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleStatus.Properties.DataSource = CType(resources.GetObject("INDGleStatus.Properties.DataSource"), Object)
        Me.INDGleStatus.Properties.DisplayMember = "Item2"
        Me.INDGleStatus.Properties.NullText = ""
        Me.INDGleStatus.Properties.PopupView = Me.CtrYesNo1View
        Me.INDGleStatus.Properties.ValueMember = "Item1"
        Me.INDGleStatus.Size = New System.Drawing.Size(386, 28)
        Me.INDGleStatus.StyleController = Me.LayoutControl1
        Me.INDGleStatus.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleStatus, 0)
        '
        'CtrYesNo1View
        '
        Me.CtrYesNo1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.CtrYesNo1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo1View.Name = "CtrYesNo1View"
        Me.CtrYesNo1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo1View.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.Caption = "Selección"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.Caption = "Selección"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'INDMeObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeObservation, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeObservation, False)
        Me.INDMeObservation.EnterMoveNextControl = True
        Me.INDMeObservation.Location = New System.Drawing.Point(12, 150)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeObservation, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDMeObservation.Name = "INDMeObservation"
        Me.INDMeObservation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMeObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeObservation.Properties.Appearance.Options.UseFont = True
        Me.INDMeObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeObservation.Properties.MaxLength = 500
        Me.INDMeObservation.Size = New System.Drawing.Size(386, 98)
        Me.INDMeObservation.StyleController = Me.LayoutControl1
        Me.INDMeObservation.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeObservation, 0)
        '
        'INDTeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeDescription, False)
        Me.INDTeDescription.EnterMoveNextControl = True
        Me.INDTeDescription.Location = New System.Drawing.Point(12, 90)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeDescription, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTeDescription.Name = "INDTeDescription"
        Me.INDTeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDTeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeDescription.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTeDescription.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTeDescription.Properties.MaxLength = 20
        Me.INDTeDescription.Size = New System.Drawing.Size(386, 28)
        Me.INDTeDescription.StyleController = Me.LayoutControl1
        Me.INDTeDescription.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeDescription, 0)
        '
        'INDTeCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeCode, False)
        Me.INDTeCode.EnterMoveNextControl = True
        Me.INDTeCode.Location = New System.Drawing.Point(12, 30)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTeCode.Name = "INDTeCode"
        Me.INDTeCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeCode.Properties.Appearance.Options.UseFont = True
        Me.INDTeCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeCode.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTeCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTeCode.Properties.MaxLength = 3
        Me.INDTeCode.Size = New System.Drawing.Size(386, 28)
        Me.INDTeCode.StyleController = Me.LayoutControl1
        Me.INDTeCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeCode, 0)
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciLineCode, Me.INDLciDescripcion, Me.INDLciObservation, Me.INDLciSatus})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(425, 339)
        Me.Root.TextVisible = False
        '
        'INDLciLineCode
        '
        Me.INDLciLineCode.Control = Me.INDTeCode
        Me.INDLciLineCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciLineCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciLineCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciLineCode.Name = "INDLciLineCode"
        Me.INDLciLineCode.Size = New System.Drawing.Size(405, 60)
        Me.INDLciLineCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciLineCode.Text = "Código de la Línea"
        Me.INDLciLineCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciLineCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciLineCode.TextSize = New System.Drawing.Size(96, 13)
        Me.INDLciLineCode.TextToControlDistance = 5
        '
        'INDLciDescripcion
        '
        Me.INDLciDescripcion.Control = Me.INDTeDescription
        Me.INDLciDescripcion.Location = New System.Drawing.Point(0, 60)
        Me.INDLciDescripcion.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDescripcion.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDescripcion.Name = "INDLciDescripcion"
        Me.INDLciDescripcion.Size = New System.Drawing.Size(405, 60)
        Me.INDLciDescripcion.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescripcion.Text = "Descripción de la Línea"
        Me.INDLciDescripcion.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDescripcion.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescripcion.TextSize = New System.Drawing.Size(96, 13)
        Me.INDLciDescripcion.TextToControlDistance = 5
        '
        'INDLciObservation
        '
        Me.INDLciObservation.Control = Me.INDMeObservation
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 120)
        Me.INDLciObservation.MaxSize = New System.Drawing.Size(390, 120)
        Me.INDLciObservation.MinSize = New System.Drawing.Size(390, 120)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Size = New System.Drawing.Size(405, 120)
        Me.INDLciObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservation.Text = "Observación"
        Me.INDLciObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservation.TextSize = New System.Drawing.Size(96, 13)
        Me.INDLciObservation.TextToControlDistance = 5
        '
        'INDLciSatus
        '
        Me.INDLciSatus.Control = Me.INDGleStatus
        Me.INDLciSatus.Location = New System.Drawing.Point(0, 240)
        Me.INDLciSatus.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciSatus.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciSatus.Name = "INDLciSatus"
        Me.INDLciSatus.Size = New System.Drawing.Size(405, 79)
        Me.INDLciSatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSatus.Text = "Activo"
        Me.INDLciSatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSatus.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSatus.TextSize = New System.Drawing.Size(40, 17)
        Me.INDLciSatus.TextToControlDistance = 5
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDSbAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 339)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(425, 40)
        Me.PanelControl1.TabIndex = 1
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAdd.Appearance.Options.UseFont = True
        Me.INDSbAdd.Dock = System.Windows.Forms.DockStyle.Right
        Me.INDSbAdd.Location = New System.Drawing.Point(293, 2)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(130, 36)
        Me.INDSbAdd.TabIndex = 0
        Me.INDSbAdd.Text = "Aceptar"
        '
        'FrmPopupWorkingArea
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 379)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupWorkingArea"
        Me.ShowInTaskbar = False
        Me.Text = "Áreas de Trabajo"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGleStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLineCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDescripcion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDMeObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDTeDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciLineCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDescripcion As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents INDGleStatus As Controls.CtrYesNo
    Friend WithEvents CtrYesNo1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciSatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class

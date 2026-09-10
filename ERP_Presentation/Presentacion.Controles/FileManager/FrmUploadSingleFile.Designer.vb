<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUploadSingleFile
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
        Me.LycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpgbLoading = New DevExpress.XtraEditors.ProgressBarControl()
        Me.LblNamePackage = New DevExpress.XtraEditors.MemoEdit()
        Me.BtnSelectedFile = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyciLoading = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PnlButtons = New DevExpress.XtraEditors.PanelControl()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnLoadFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDofdPackages = New System.Windows.Forms.OpenFileDialog()
        CType(Me.LycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LycRoot.SuspendLayout()
        CType(Me.INDpgbLoading.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblNamePackage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnSelectedFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyciLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'LycRoot
        '
        Me.LycRoot.AllowCustomizationMenu = False
        Me.LycRoot.AllowDrop = True
        Me.LycRoot.Controls.Add(Me.INDpgbLoading)
        Me.LycRoot.Controls.Add(Me.LblNamePackage)
        Me.LycRoot.Controls.Add(Me.BtnSelectedFile)
        Me.LycRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LycRoot.Location = New System.Drawing.Point(0, 0)
        Me.LycRoot.Name = "LycRoot"
        Me.LycRoot.Root = Me.LayoutControlGroup1
        Me.LycRoot.Size = New System.Drawing.Size(337, 189)
        Me.LycRoot.TabIndex = 0
        Me.LycRoot.Text = "LayoutControl1"
        '
        'INDpgbLoading
        '
        Me.INDpgbLoading.Location = New System.Drawing.Point(12, 159)
        Me.INDpgbLoading.Name = "INDpgbLoading"
        Me.INDpgbLoading.Size = New System.Drawing.Size(313, 18)
        Me.INDpgbLoading.StyleController = Me.LycRoot
        Me.INDpgbLoading.TabIndex = 6
        '
        'LblNamePackage
        '
        Me.LblNamePackage.AllowDrop = True
        Me.LblNamePackage.Location = New System.Drawing.Point(12, 44)
        Me.LblNamePackage.Name = "LblNamePackage"
        Me.LblNamePackage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LblNamePackage.Properties.Appearance.Options.UseFont = True
        Me.LblNamePackage.Properties.ReadOnly = True
        Me.LblNamePackage.Size = New System.Drawing.Size(313, 111)
        Me.LblNamePackage.StyleController = Me.LycRoot
        Me.LblNamePackage.TabIndex = 5
        '
        'BtnSelectedFile
        '
        Me.BtnSelectedFile.EditValue = "[Seleccione el Archivo a Cargar]"
        Me.BtnSelectedFile.Location = New System.Drawing.Point(12, 12)
        Me.BtnSelectedFile.Name = "BtnSelectedFile"
        Me.BtnSelectedFile.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BtnSelectedFile.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.BtnSelectedFile.Properties.Appearance.Options.UseFont = True
        Me.BtnSelectedFile.Properties.Appearance.Options.UseForeColor = True
        Me.BtnSelectedFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.BtnSelectedFile.Properties.ReadOnly = True
        Me.BtnSelectedFile.Size = New System.Drawing.Size(313, 28)
        Me.BtnSelectedFile.StyleController = Me.LycRoot
        Me.BtnSelectedFile.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyciLoading, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(337, 189)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLyciLoading
        '
        Me.INDLyciLoading.Control = Me.INDpgbLoading
        Me.INDLyciLoading.CustomizationFormText = "LyciLoading"
        Me.INDLyciLoading.Location = New System.Drawing.Point(0, 147)
        Me.INDLyciLoading.Name = "INDLyciLoading"
        Me.INDLyciLoading.Size = New System.Drawing.Size(317, 22)
        Me.INDLyciLoading.Text = "INDLyciLoading"
        Me.INDLyciLoading.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyciLoading.TextToControlDistance = 0
        Me.INDLyciLoading.TextVisible = False
        Me.INDLyciLoading.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.LblNamePackage
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(317, 115)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.BtnSelectedFile
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(317, 32)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'PnlButtons
        '
        Me.PnlButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlButtons.Controls.Add(Me.BtnCancel)
        Me.PnlButtons.Controls.Add(Me.BtnLoadFile)
        Me.PnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PnlButtons.Location = New System.Drawing.Point(0, 189)
        Me.PnlButtons.Name = "PnlButtons"
        Me.PnlButtons.Size = New System.Drawing.Size(337, 57)
        Me.PnlButtons.TabIndex = 1
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(174, 12)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(151, 32)
        Me.BtnCancel.TabIndex = 3
        Me.BtnCancel.Text = "Cancelar"
        '
        'BtnLoadFile
        '
        Me.BtnLoadFile.Enabled = False
        Me.BtnLoadFile.Location = New System.Drawing.Point(12, 12)
        Me.BtnLoadFile.Name = "BtnLoadFile"
        Me.BtnLoadFile.Size = New System.Drawing.Size(151, 32)
        Me.BtnLoadFile.TabIndex = 2
        Me.BtnLoadFile.Text = "Cargar Archivo"
        '
        'INDofdPackages
        '
        Me.INDofdPackages.Title = "Seleccione el archivo a cargar"
        '
        'FrmUploadSingleFile
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 246)
        Me.ControlBox = False
        Me.Controls.Add(Me.LycRoot)
        Me.Controls.Add(Me.PnlButtons)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmUploadSingleFile"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Carga de Archivo"
        CType(Me.LycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LycRoot.ResumeLayout(False)
        CType(Me.INDpgbLoading.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblNamePackage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnSelectedFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyciLoading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PnlButtons As DevExpress.XtraEditors.PanelControl
    Private WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Private WithEvents BtnLoadFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDpgbLoading As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents LblNamePackage As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents BtnSelectedFile As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLyciLoading As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDofdPackages As System.Windows.Forms.OpenFileDialog
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GridLookUpEditExtendido
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
    'It can be modified using the Windows Form Designer.  ca
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GridLookUpEditExtendido))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDglCustomizable = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDglCustomizableView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ImagenCarga = New DevExpress.XtraEditors.PictureEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciUploader = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.INDListaImagenes = New System.Windows.Forms.ImageList()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDglCustomizable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglCustomizableView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImagenCarga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciUploader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AutoScroll = False
        Me.LayoutControl1.Controls.Add(Me.INDglCustomizable)
        Me.LayoutControl1.Controls.Add(Me.ImagenCarga)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(399, 302, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(397, 24)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDglCustomizable
        '
        Me.INDglCustomizable.EnterMoveNextControl = True
        Me.INDglCustomizable.Location = New System.Drawing.Point(2, 2)
        Me.INDglCustomizable.Name = "INDglCustomizable"
        Me.INDglCustomizable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("INDglCustomizable.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDglCustomizable.Properties.ImmediatePopup = True
        Me.INDglCustomizable.Properties.NullText = ""
        Me.INDglCustomizable.Properties.PopupFormSize = New System.Drawing.Size(400, 280)
        Me.INDglCustomizable.Properties.View = Me.INDglCustomizableView
        Me.INDglCustomizable.Size = New System.Drawing.Size(369, 20)
        Me.INDglCustomizable.StyleController = Me.LayoutControl1
        Me.INDglCustomizable.TabIndex = 5
        '
        'INDglCustomizableView
        '
        Me.INDglCustomizableView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDglCustomizableView.Name = "INDglCustomizableView"
        Me.INDglCustomizableView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDglCustomizableView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDglCustomizableView.OptionsView.EnableAppearanceOddRow = True
        Me.INDglCustomizableView.OptionsView.ShowGroupPanel = False
        '
        'ImagenCarga
        '
        Me.ImagenCarga.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ImagenCarga.EditValue = Global.Presentation.Controls.My.Resources.Resources.carga
        Me.ImagenCarga.Location = New System.Drawing.Point(375, 2)
        Me.ImagenCarga.Name = "ImagenCarga"
        Me.ImagenCarga.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ImagenCarga.Properties.Appearance.Options.UseBackColor = True
        Me.ImagenCarga.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.ImagenCarga.Properties.PictureAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.ImagenCarga.Size = New System.Drawing.Size(20, 20)
        Me.ImagenCarga.StyleController = Me.LayoutControl1
        Me.ImagenCarga.TabIndex = 4
        Me.ImagenCarga.Visible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciUploader, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(397, 24)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlciUploader
        '
        Me.INDlciUploader.Control = Me.ImagenCarga
        Me.INDlciUploader.CustomizationFormText = "INDlciUploader"
        Me.INDlciUploader.Location = New System.Drawing.Point(373, 0)
        Me.INDlciUploader.Name = "INDlciUploader"
        Me.INDlciUploader.Size = New System.Drawing.Size(24, 24)
        Me.INDlciUploader.Text = "INDlciUploader"
        Me.INDlciUploader.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciUploader.TextToControlDistance = 0
        Me.INDlciUploader.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDglCustomizable
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 24)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(54, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(373, 24)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDBackgroundWorker
        '
        Me.INDBackgroundWorker.WorkerReportsProgress = True
        '
        'INDListaImagenes
        '
        Me.INDListaImagenes.ImageStream = CType(resources.GetObject("INDListaImagenes.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.INDListaImagenes.TransparentColor = System.Drawing.Color.Transparent
        Me.INDListaImagenes.Images.SetKeyName(0, "on.png")
        Me.INDListaImagenes.Images.SetKeyName(1, "off.png")
        '
        'GridLookUpEditExtendido
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.LayoutControl1)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "GridLookUpEditExtendido"
        Me.Size = New System.Drawing.Size(397, 24)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDglCustomizable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglCustomizableView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImagenCarga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciUploader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents INDBackgroundWorker As System.ComponentModel.BackgroundWorker
    Private WithEvents INDListaImagenes As System.Windows.Forms.ImageList
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents ImagenCarga As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDlciUploader As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents INDglCustomizable As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDglCustomizableView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem

End Class

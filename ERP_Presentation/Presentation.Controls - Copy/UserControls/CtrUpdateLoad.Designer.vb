<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrUpdateLoad
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrUpdateLoad))
        Me.INDbtnUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.INDpeAsyncLoad = New DevExpress.XtraEditors.PictureEdit()
        Me.INDimcIconos = New DevExpress.Utils.ImageCollection()
        CType(Me.INDpeAsyncLoad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDimcIconos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDbtnUpdate
        '
        Me.INDbtnUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnUpdate.ImageIndex = 0
        Me.INDbtnUpdate.ImageList = Me.INDimcIconos
        Me.INDbtnUpdate.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDbtnUpdate.Location = New System.Drawing.Point(0, 0)
        Me.INDbtnUpdate.Margin = New System.Windows.Forms.Padding(6)
        Me.INDbtnUpdate.Name = "INDbtnUpdate"
        Me.INDbtnUpdate.Size = New System.Drawing.Size(36, 36)
        Me.INDbtnUpdate.TabIndex = 1
        '
        'INDpeAsyncLoad
        '
        Me.INDpeAsyncLoad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpeAsyncLoad.EditValue = Global.Presentation.Controls.My.Resources.Resources.Cargando36x36
        Me.INDpeAsyncLoad.Location = New System.Drawing.Point(0, 0)
        Me.INDpeAsyncLoad.Name = "INDpeAsyncLoad"
        Me.INDpeAsyncLoad.Properties.AllowFocused = False
        Me.INDpeAsyncLoad.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeAsyncLoad.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDpeAsyncLoad.Size = New System.Drawing.Size(36, 36)
        Me.INDpeAsyncLoad.TabIndex = 0
        Me.INDpeAsyncLoad.Visible = False
        '
        'INDimcIconos
        '
        Me.INDimcIconos.ImageSize = New System.Drawing.Size(25, 25)
        Me.INDimcIconos.ImageStream = CType(resources.GetObject("INDimcIconos.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDimcIconos.Images.SetKeyName(0, "ActualizarBlanco.png")
        '
        'CtrUpdateLoad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDbtnUpdate)
        Me.Controls.Add(Me.INDpeAsyncLoad)
        Me.Name = "CtrUpdateLoad"
        Me.Size = New System.Drawing.Size(36, 36)
        CType(Me.INDpeAsyncLoad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDimcIconos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents INDpeAsyncLoad As DevExpress.XtraEditors.PictureEdit
    Private WithEvents INDbtnUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDimcIconos As DevExpress.Utils.ImageCollection

End Class

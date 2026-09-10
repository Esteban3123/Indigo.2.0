<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrFirmaDigital
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrFirmaDigital))
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtnCancelar = New DevExpress.XtraEditors.SimpleButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolTipController1
        '
        Me.ToolTipController1.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SimpleButton1.Location = New System.Drawing.Point(18, 95)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(68, 23)
        Me.SimpleButton1.TabIndex = 0
        Me.SimpleButton1.Text = "Iniciar"
        Me.SimpleButton1.ToolTipController = Me.ToolTipController1
        Me.SimpleButton1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.SimpleButton1.ToolTipTitle = "Iniciar Proceso"
        '
        'INDBtnCancelar
        '
        Me.INDBtnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDBtnCancelar.Location = New System.Drawing.Point(108, 95)
        Me.INDBtnCancelar.Name = "INDBtnCancelar"
        Me.INDBtnCancelar.Size = New System.Drawing.Size(68, 23)
        Me.INDBtnCancelar.TabIndex = 5
        Me.INDBtnCancelar.Text = "Cancelar"
        Me.INDBtnCancelar.ToolTip = "Cancelar"
        Me.INDBtnCancelar.ToolTipController = Me.ToolTipController1
        Me.INDBtnCancelar.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "firma digital02ssssss.png")
        '
        'PictureEdit1
        '
        Me.PictureEdit1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureEdit1.Location = New System.Drawing.Point(0, 0)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.ReadOnly = True
        Me.PictureEdit1.Properties.ShowMenu = False
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.PictureEdit1.Size = New System.Drawing.Size(187, 89)
        Me.PictureEdit1.TabIndex = 6
        '
        'CtrFirmaDigital
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureEdit1)
        Me.Controls.Add(Me.INDBtnCancelar)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Name = "CtrFirmaDigital"
        Me.Size = New System.Drawing.Size(187, 124)
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
    Friend WithEvents INDBtnCancelar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit

End Class

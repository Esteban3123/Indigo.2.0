<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrPhoto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrPhoto))
        Me.INDpanelControles = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnCargar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDicIconos = New DevExpress.Utils.ImageCollection()
        Me.INDBtnIniciar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtnCancelar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtnCapturar = New DevExpress.XtraEditors.SimpleButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.INDfotoUsuario = New DevExpress.XtraEditors.PictureEdit()
        Me.INDlblTitle = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpccImageLarge = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDpeImageLarge = New DevExpress.XtraEditors.PictureEdit()
        Me.INDpceImageLarge = New DevExpress.XtraEditors.PopupContainerEdit()
        CType(Me.INDpanelControles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelControles.SuspendLayout()
        CType(Me.INDicIconos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDfotoUsuario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccImageLarge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccImageLarge.SuspendLayout()
        CType(Me.INDpeImageLarge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceImageLarge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDpanelControles
        '
        Me.INDpanelControles.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpanelControles.Controls.Add(Me.INDBtnCapturar)
        Me.INDpanelControles.Controls.Add(Me.INDBtnIniciar)
        Me.INDpanelControles.Controls.Add(Me.INDBtnCancelar)
        Me.INDpanelControles.Controls.Add(Me.INDBtnCargar)
        resources.ApplyResources(Me.INDpanelControles, "INDpanelControles")
        Me.INDpanelControles.Name = "INDpanelControles"
        '
        'INDBtnCargar
        '
        resources.ApplyResources(Me.INDBtnCargar, "INDBtnCargar")
        Me.INDBtnCargar.ImageIndex = 0
        Me.INDBtnCargar.ImageList = Me.INDicIconos
        Me.INDBtnCargar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnCargar.Name = "INDBtnCargar"
        '
        'INDicIconos
        '
        Me.INDicIconos.ImageStream = CType(resources.GetObject("INDicIconos.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicIconos.Images.SetKeyName(0, "Abrir.png")
        Me.INDicIconos.Images.SetKeyName(1, "Captura.png")
        Me.INDicIconos.Images.SetKeyName(2, "Play.png")
        Me.INDicIconos.Images.SetKeyName(3, "Stop.png")
        '
        'INDBtnIniciar
        '
        resources.ApplyResources(Me.INDBtnIniciar, "INDBtnIniciar")
        Me.INDBtnIniciar.ImageIndex = 2
        Me.INDBtnIniciar.ImageList = Me.INDicIconos
        Me.INDBtnIniciar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnIniciar.Name = "INDBtnIniciar"
        '
        'INDBtnCancelar
        '
        resources.ApplyResources(Me.INDBtnCancelar, "INDBtnCancelar")
        Me.INDBtnCancelar.ImageIndex = 3
        Me.INDBtnCancelar.ImageList = Me.INDicIconos
        Me.INDBtnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnCancelar.Name = "INDBtnCancelar"
        '
        'INDBtnCapturar
        '
        resources.ApplyResources(Me.INDBtnCapturar, "INDBtnCapturar")
        Me.INDBtnCapturar.ImageIndex = 1
        Me.INDBtnCapturar.ImageList = Me.INDicIconos
        Me.INDBtnCapturar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnCapturar.Name = "INDBtnCapturar"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'INDfotoUsuario
        '
        Me.INDfotoUsuario.EditValue = Global.Presentation.Controls.My.Resources.Resources.Usuario
        resources.ApplyResources(Me.INDfotoUsuario, "INDfotoUsuario")
        Me.INDfotoUsuario.Name = "INDfotoUsuario"
        Me.INDfotoUsuario.Properties.AllowFocused = False
        Me.INDfotoUsuario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDfotoUsuario.Properties.ReadOnly = True
        Me.INDfotoUsuario.Properties.ShowMenu = False
        Me.INDfotoUsuario.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDfotoUsuario.StyleController = Me.LayoutControl1
        '
        'INDlblTitle
        '
        Me.INDlblTitle.Appearance.Font = CType(resources.GetObject("INDlblTitle.Appearance.Font"), System.Drawing.Font)
        Me.INDlblTitle.Appearance.ForeColor = CType(resources.GetObject("INDlblTitle.Appearance.ForeColor"), System.Drawing.Color)
        resources.ApplyResources(Me.INDlblTitle, "INDlblTitle")
        Me.INDlblTitle.Name = "INDlblTitle"
        Me.INDlblTitle.StyleController = Me.LayoutControl1
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDpccImageLarge)
        Me.LayoutControl1.Controls.Add(Me.INDlblTitle)
        Me.LayoutControl1.Controls.Add(Me.INDfotoUsuario)
        Me.LayoutControl1.Controls.Add(Me.INDpanelControles)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(654, 190, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'LayoutControlGroup1
        '
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(295, 68)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDfotoUsuario
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(68, 68)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(68, 68)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(68, 68)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDpanelControles
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Location = New System.Drawing.Point(68, 25)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(227, 43)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(227, 43)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(227, 43)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDlblTitle
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Location = New System.Drawing.Point(68, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(227, 25)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(227, 25)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(227, 25)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDpccImageLarge
        '
        Me.INDpccImageLarge.Controls.Add(Me.INDpeImageLarge)
        resources.ApplyResources(Me.INDpccImageLarge, "INDpccImageLarge")
        Me.INDpccImageLarge.Name = "INDpccImageLarge"
        '
        'INDpeImageLarge
        '
        resources.ApplyResources(Me.INDpeImageLarge, "INDpeImageLarge")
        Me.INDpeImageLarge.EditValue = Global.Presentation.Controls.My.Resources.Resources.Usuario
        Me.INDpeImageLarge.Name = "INDpeImageLarge"
        Me.INDpeImageLarge.Properties.AllowFocused = False
        Me.INDpeImageLarge.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeImageLarge.Properties.ReadOnly = True
        Me.INDpeImageLarge.Properties.ShowMenu = False
        Me.INDpeImageLarge.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDpeImageLarge.StyleController = Me.LayoutControl1
        '
        'INDpceImageLarge
        '
        resources.ApplyResources(Me.INDpceImageLarge, "INDpceImageLarge")
        Me.INDpceImageLarge.Name = "INDpceImageLarge"
        Me.INDpceImageLarge.Properties.AllowFocused = False
        Me.INDpceImageLarge.Properties.AutoHeight = CType(resources.GetObject("PopupContainerEdit1.Properties.AutoHeight"), Boolean)
        Me.INDpceImageLarge.Properties.PopupControl = Me.INDpccImageLarge
        Me.INDpceImageLarge.Properties.PopupSizeable = False
        Me.INDpceImageLarge.Properties.ShowPopupCloseButton = False
        Me.INDpceImageLarge.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'CtrPhoto
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.INDpceImageLarge)
        Me.Name = "CtrPhoto"
        CType(Me.INDpanelControles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelControles.ResumeLayout(False)
        CType(Me.INDicIconos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDfotoUsuario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccImageLarge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccImageLarge.ResumeLayout(False)
        CType(Me.INDpeImageLarge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceImageLarge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDfotoUsuario As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDBtnIniciar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDBtnCapturar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDBtnCargar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents INDBtnCancelar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDpanelControles As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDicIconos As DevExpress.Utils.ImageCollection
    Friend WithEvents INDlblTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpccImageLarge As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDpeImageLarge As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDpceImageLarge As DevExpress.XtraEditors.PopupContainerEdit

End Class

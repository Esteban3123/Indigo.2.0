<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDigitalization
    Inherits FormBase

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDigitalization))
        Me.INDImgButtons = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDImagenes = New DevExpress.Utils.ImageCollection(Me.components)
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.DynamicDotNetTwainThumb = New Dynamsoft.DotNet.TWAIN.DynamicDotNetTwain()
        Me.DynamicDotNetTwainView = New Dynamsoft.DotNet.TWAIN.DynamicDotNetTwain()
        Me.INDlblImagenActual = New DevExpress.XtraEditors.LabelControl()
        Me.INDMarqueeProgressBarControl = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.INDbtnUltimo = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnEliminar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnPrimero = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnGuardar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnRotarDerecha = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnEscanear = New DevExpress.XtraEditors.DropDownButton()
        Me.INDbtnRotarIzquierda = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnDerecha = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnIzquierda = New DevExpress.XtraEditors.SimpleButton()
        Me.INDchkDuplex = New DevExpress.XtraEditors.CheckEdit()
        Me.INDchkADF = New DevExpress.XtraEditors.CheckEdit()
        Me.INDcbDispositivos = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLycProcessPrgM = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.AdditionalControlPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDImgButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDImagenes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDMarqueeProgressBarControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDchkDuplex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDchkADF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDcbDispositivos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycProcessPrgM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(884, 418)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.Size = New System.Drawing.Size(584, 100)
        Me.BarraBotones.Visible = False
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(884, 108)
        Me.ToolBars.Visible = False
        '
        'AdditionalControlPanel
        '
        Me.AdditionalControlPanel.Location = New System.Drawing.Point(584, 0)
        '
        'INDImgButtons
        '
        Me.INDImgButtons.ImageSize = New System.Drawing.Size(24, 24)
        Me.INDImgButtons.ImageStream = CType(resources.GetObject("INDImgButtons.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDImgButtons.Images.SetKeyName(0, "32x32.png")
        Me.INDImgButtons.Images.SetKeyName(1, "adela.png")
        Me.INDImgButtons.Images.SetKeyName(2, "adelantar.png")
        Me.INDImgButtons.Images.SetKeyName(3, "derecho.png")
        Me.INDImgButtons.Images.SetKeyName(4, "izquierdo.png")
        Me.INDImgButtons.Images.SetKeyName(5, "retro.png")
        Me.INDImgButtons.Images.SetKeyName(6, "retroceder.png")
        '
        'INDImagenes
        '
        Me.INDImagenes.ImageSize = New System.Drawing.Size(24, 24)
        Me.INDImagenes.ImageStream = CType(resources.GetObject("INDImagenes.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDImagenes.Images.SetKeyName(0, "Back.png")
        Me.INDImagenes.Images.SetKeyName(1, "Deshacer.png")
        Me.INDImagenes.Images.SetKeyName(2, "Eliminar.png")
        Me.INDImagenes.Images.SetKeyName(3, "First.png")
        Me.INDImagenes.Images.SetKeyName(4, "Last.png")
        Me.INDImagenes.Images.SetKeyName(5, "Next.png")
        Me.INDImagenes.Images.SetKeyName(6, "Rehacer.png")
        Me.INDImagenes.Images.SetKeyName(7, "Guardar.png")
        Me.INDImagenes.Images.SetKeyName(8, "Buscar.png")
        Me.INDImagenes.Images.SetKeyName(9, "digitalizar-blanco.png")
        Me.INDImagenes.Images.SetKeyName(10, "adjuntar-blanco.png")
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.LayoutControl2)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(880, 409)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.DynamicDotNetTwainThumb)
        Me.LayoutControl2.Controls.Add(Me.DynamicDotNetTwainView)
        Me.LayoutControl2.Controls.Add(Me.INDlblImagenActual)
        Me.LayoutControl2.Controls.Add(Me.INDMarqueeProgressBarControl)
        Me.LayoutControl2.Controls.Add(Me.INDbtnUltimo)
        Me.LayoutControl2.Controls.Add(Me.INDbtnEliminar)
        Me.LayoutControl2.Controls.Add(Me.INDbtnPrimero)
        Me.LayoutControl2.Controls.Add(Me.INDbtnGuardar)
        Me.LayoutControl2.Controls.Add(Me.INDbtnRotarDerecha)
        Me.LayoutControl2.Controls.Add(Me.INDbtnEscanear)
        Me.LayoutControl2.Controls.Add(Me.INDbtnRotarIzquierda)
        Me.LayoutControl2.Controls.Add(Me.INDbtnDerecha)
        Me.LayoutControl2.Controls.Add(Me.INDbtnIzquierda)
        Me.LayoutControl2.Controls.Add(Me.INDchkDuplex)
        Me.LayoutControl2.Controls.Add(Me.INDchkADF)
        Me.LayoutControl2.Controls.Add(Me.INDcbDispositivos)
        Me.LayoutControl2.Location = New System.Drawing.Point(12, 12)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(856, 385)
        Me.LayoutControl2.TabIndex = 4
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'DynamicDotNetTwainThumb
        '
        Me.DynamicDotNetTwainThumb.AnnotationFillColor = System.Drawing.Color.White
        Me.DynamicDotNetTwainThumb.AnnotationPen = Nothing
        Me.DynamicDotNetTwainThumb.AnnotationTextColor = System.Drawing.Color.Black
        Me.DynamicDotNetTwainThumb.AnnotationTextFont = Nothing
        Me.DynamicDotNetTwainThumb.IfShowPrintUI = False
        Me.DynamicDotNetTwainThumb.Location = New System.Drawing.Point(28, 98)
        Me.DynamicDotNetTwainThumb.LogLevel = CType(0, Short)
        Me.DynamicDotNetTwainThumb.Name = "DynamicDotNetTwainThumb"
        Me.DynamicDotNetTwainThumb.PDFMarginBottom = CType(0UI, UInteger)
        Me.DynamicDotNetTwainThumb.PDFMarginLeft = CType(0UI, UInteger)
        Me.DynamicDotNetTwainThumb.PDFMarginRight = CType(0UI, UInteger)
        Me.DynamicDotNetTwainThumb.PDFMarginTop = CType(0UI, UInteger)
        Me.DynamicDotNetTwainThumb.Size = New System.Drawing.Size(154, 259)
        Me.DynamicDotNetTwainThumb.TabIndex = 34
        '
        'DynamicDotNetTwainView
        '
        Me.DynamicDotNetTwainView.AnnotationFillColor = System.Drawing.Color.White
        Me.DynamicDotNetTwainView.AnnotationPen = Nothing
        Me.DynamicDotNetTwainView.AnnotationTextColor = System.Drawing.Color.Black
        Me.DynamicDotNetTwainView.AnnotationTextFont = Nothing
        Me.DynamicDotNetTwainView.IfShowPrintUI = False
        Me.DynamicDotNetTwainView.JPEGQuality = CType(100, Short)
        Me.DynamicDotNetTwainView.Location = New System.Drawing.Point(210, 98)
        Me.DynamicDotNetTwainView.LogLevel = CType(0, Short)
        Me.DynamicDotNetTwainView.Name = "DynamicDotNetTwainView"
        Me.DynamicDotNetTwainView.PDFMarginBottom = CType(0UI, UInteger)
        Me.DynamicDotNetTwainView.PDFMarginLeft = CType(0UI, UInteger)
        Me.DynamicDotNetTwainView.PDFMarginRight = CType(0UI, UInteger)
        Me.DynamicDotNetTwainView.PDFMarginTop = CType(0UI, UInteger)
        Me.DynamicDotNetTwainView.Size = New System.Drawing.Size(618, 146)
        Me.DynamicDotNetTwainView.TabIndex = 33
        '
        'INDlblImagenActual
        '
        Me.INDlblImagenActual.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblImagenActual.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblImagenActual.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblImagenActual.Location = New System.Drawing.Point(329, 307)
        Me.INDlblImagenActual.Name = "INDlblImagenActual"
        Me.INDlblImagenActual.Size = New System.Drawing.Size(342, 21)
        Me.INDlblImagenActual.StyleController = Me.LayoutControl2
        Me.INDlblImagenActual.TabIndex = 31
        Me.INDlblImagenActual.Text = "Pagina 0 de "
        '
        'INDMarqueeProgressBarControl
        '
        Me.INDMarqueeProgressBarControl.EditValue = "Procesando . . . "
        Me.INDMarqueeProgressBarControl.Location = New System.Drawing.Point(210, 248)
        Me.INDMarqueeProgressBarControl.Name = "INDMarqueeProgressBarControl"
        Me.INDMarqueeProgressBarControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDMarqueeProgressBarControl.Properties.ReadOnly = True
        Me.INDMarqueeProgressBarControl.Properties.ShowTitle = True
        Me.INDMarqueeProgressBarControl.Size = New System.Drawing.Size(618, 26)
        Me.INDMarqueeProgressBarControl.StyleController = Me.LayoutControl2
        Me.INDMarqueeProgressBarControl.TabIndex = 4
        Me.INDMarqueeProgressBarControl.Visible = False
        '
        'INDbtnUltimo
        '
        Me.INDbtnUltimo.Enabled = False
        Me.INDbtnUltimo.ImageIndex = 1
        Me.INDbtnUltimo.ImageList = Me.INDImgButtons
        Me.INDbtnUltimo.Location = New System.Drawing.Point(756, 302)
        Me.INDbtnUltimo.Name = "INDbtnUltimo"
        Me.INDbtnUltimo.Size = New System.Drawing.Size(34, 30)
        Me.INDbtnUltimo.StyleController = Me.LayoutControl2
        Me.INDbtnUltimo.TabIndex = 27
        Me.INDbtnUltimo.ToolTip = "Click aqui para ir al ultimo"
        Me.INDbtnUltimo.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'INDbtnEliminar
        '
        Me.INDbtnEliminar.Enabled = False
        Me.INDbtnEliminar.ImageIndex = 0
        Me.INDbtnEliminar.ImageList = Me.INDImgButtons
        Me.INDbtnEliminar.Location = New System.Drawing.Point(794, 302)
        Me.INDbtnEliminar.Name = "INDbtnEliminar"
        Me.INDbtnEliminar.Size = New System.Drawing.Size(34, 30)
        Me.INDbtnEliminar.StyleController = Me.LayoutControl2
        Me.INDbtnEliminar.TabIndex = 30
        Me.INDbtnEliminar.ToolTip = "Click aqui para eliminar la imagen actual"
        Me.INDbtnEliminar.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'INDbtnPrimero
        '
        Me.INDbtnPrimero.Enabled = False
        Me.INDbtnPrimero.ImageIndex = 5
        Me.INDbtnPrimero.ImageList = Me.INDImgButtons
        Me.INDbtnPrimero.Location = New System.Drawing.Point(210, 302)
        Me.INDbtnPrimero.Name = "INDbtnPrimero"
        Me.INDbtnPrimero.Size = New System.Drawing.Size(34, 30)
        Me.INDbtnPrimero.StyleController = Me.LayoutControl2
        Me.INDbtnPrimero.TabIndex = 26
        Me.INDbtnPrimero.ToolTip = "Click aqui para ir al primero "
        Me.INDbtnPrimero.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'INDbtnGuardar
        '
        Me.INDbtnGuardar.Enabled = False
        Me.INDbtnGuardar.ImageIndex = 10
        Me.INDbtnGuardar.ImageList = Me.INDImagenes
        Me.INDbtnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDbtnGuardar.Location = New System.Drawing.Point(718, 16)
        Me.INDbtnGuardar.Name = "INDbtnGuardar"
        Me.INDbtnGuardar.Size = New System.Drawing.Size(122, 30)
        Me.INDbtnGuardar.StyleController = Me.LayoutControl2
        Me.INDbtnGuardar.TabIndex = 9
        '
        'INDbtnRotarDerecha
        '
        Me.INDbtnRotarDerecha.Enabled = False
        Me.INDbtnRotarDerecha.ImageIndex = 3
        Me.INDbtnRotarDerecha.ImageList = Me.INDImgButtons
        Me.INDbtnRotarDerecha.Location = New System.Drawing.Point(718, 302)
        Me.INDbtnRotarDerecha.Name = "INDbtnRotarDerecha"
        Me.INDbtnRotarDerecha.Size = New System.Drawing.Size(34, 30)
        Me.INDbtnRotarDerecha.StyleController = Me.LayoutControl2
        Me.INDbtnRotarDerecha.TabIndex = 29
        Me.INDbtnRotarDerecha.ToolTip = "Click aqui para rotar"
        Me.INDbtnRotarDerecha.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'INDbtnEscanear
        '
        Me.INDbtnEscanear.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDbtnEscanear.Enabled = False
        Me.INDbtnEscanear.ImageIndex = 9
        Me.INDbtnEscanear.ImageList = Me.INDImagenes
        Me.INDbtnEscanear.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDbtnEscanear.Location = New System.Drawing.Point(606, 16)
        Me.INDbtnEscanear.Name = "INDbtnEscanear"
        Me.INDbtnEscanear.Size = New System.Drawing.Size(108, 30)
        Me.INDbtnEscanear.StyleController = Me.LayoutControl2
        Me.INDbtnEscanear.TabIndex = 8
        '
        'INDbtnRotarIzquierda
        '
        Me.INDbtnRotarIzquierda.Enabled = False
        Me.INDbtnRotarIzquierda.ImageIndex = 4
        Me.INDbtnRotarIzquierda.ImageList = Me.INDImgButtons
        Me.INDbtnRotarIzquierda.Location = New System.Drawing.Point(248, 302)
        Me.INDbtnRotarIzquierda.Name = "INDbtnRotarIzquierda"
        Me.INDbtnRotarIzquierda.Size = New System.Drawing.Size(34, 30)
        Me.INDbtnRotarIzquierda.StyleController = Me.LayoutControl2
        Me.INDbtnRotarIzquierda.TabIndex = 28
        Me.INDbtnRotarIzquierda.ToolTip = "Click aqui para rotar"
        Me.INDbtnRotarIzquierda.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'INDbtnDerecha
        '
        Me.INDbtnDerecha.Enabled = False
        Me.INDbtnDerecha.ImageIndex = 2
        Me.INDbtnDerecha.ImageList = Me.INDImgButtons
        Me.INDbtnDerecha.Location = New System.Drawing.Point(680, 302)
        Me.INDbtnDerecha.Name = "INDbtnDerecha"
        Me.INDbtnDerecha.Size = New System.Drawing.Size(34, 30)
        Me.INDbtnDerecha.StyleController = Me.LayoutControl2
        Me.INDbtnDerecha.TabIndex = 25
        Me.INDbtnDerecha.ToolTip = "Click aqui para ir adelante"
        Me.INDbtnDerecha.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'INDbtnIzquierda
        '
        Me.INDbtnIzquierda.Enabled = False
        Me.INDbtnIzquierda.ImageIndex = 6
        Me.INDbtnIzquierda.ImageList = Me.INDImgButtons
        Me.INDbtnIzquierda.Location = New System.Drawing.Point(286, 302)
        Me.INDbtnIzquierda.Name = "INDbtnIzquierda"
        Me.INDbtnIzquierda.Size = New System.Drawing.Size(34, 30)
        Me.INDbtnIzquierda.StyleController = Me.LayoutControl2
        Me.INDbtnIzquierda.TabIndex = 24
        Me.INDbtnIzquierda.ToolTip = "Click aqui para ir atras"
        Me.INDbtnIzquierda.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'INDchkDuplex
        '
        Me.INDchkDuplex.Location = New System.Drawing.Point(495, 16)
        Me.INDchkDuplex.Name = "INDchkDuplex"
        Me.INDchkDuplex.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDchkDuplex.Properties.Appearance.Options.UseFont = True
        Me.INDchkDuplex.Properties.Caption = "DUPLEX"
        Me.INDchkDuplex.Size = New System.Drawing.Size(107, 25)
        Me.INDchkDuplex.StyleController = Me.LayoutControl2
        Me.INDchkDuplex.TabIndex = 6
        '
        'INDchkADF
        '
        Me.INDchkADF.Location = New System.Drawing.Point(423, 16)
        Me.INDchkADF.Name = "INDchkADF"
        Me.INDchkADF.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDchkADF.Properties.Appearance.Options.UseFont = True
        Me.INDchkADF.Properties.Caption = "ADF"
        Me.INDchkADF.Size = New System.Drawing.Size(68, 25)
        Me.INDchkADF.StyleController = Me.LayoutControl2
        Me.INDchkADF.TabIndex = 5
        '
        'INDcbDispositivos
        '
        Me.INDcbDispositivos.Location = New System.Drawing.Point(92, 16)
        Me.INDcbDispositivos.Name = "INDcbDispositivos"
        Me.INDcbDispositivos.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDcbDispositivos.Properties.Appearance.Options.UseFont = True
        Me.INDcbDispositivos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDcbDispositivos.Size = New System.Drawing.Size(327, 28)
        Me.INDcbDispositivos.StyleController = Me.LayoutControl2
        Me.INDcbDispositivos.TabIndex = 3
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(856, 385)
        Me.LayoutControlGroup2.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlGroup4, Me.LayoutControlGroup5, Me.LayoutControlGroup6})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup3.Padding = New DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1)
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(836, 365)
        Me.LayoutControlGroup3.Text = "LayoutControlGroup2"
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.Control = Me.INDcbDispositivos
        Me.LayoutControlItem2.CustomizationFormText = "Dispositivo"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(407, 34)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(407, 34)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(407, 34)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Dispositivo"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(75, 21)
        Me.LayoutControlItem2.TextToControlDistance = 1
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDchkADF
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(407, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(72, 34)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(72, 34)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(72, 34)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDchkDuplex
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(479, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(111, 34)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(111, 34)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(111, 34)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDbtnEscanear
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(590, 0)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(52, 34)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(112, 34)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "LayoutControlItem8"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextToControlDistance = 0
        Me.LayoutControlItem8.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDbtnGuardar
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(702, 0)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(126, 34)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "LayoutControlItem9"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextToControlDistance = 0
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup4.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLycProcessPrgM, Me.LayoutControlItem1})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(182, 34)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(646, 240)
        Me.LayoutControlGroup4.Text = "Vista previa"
        '
        'INDLycProcessPrgM
        '
        Me.INDLycProcessPrgM.Control = Me.INDMarqueeProgressBarControl
        Me.INDLycProcessPrgM.CustomizationFormText = "LayoutControlItem10"
        Me.INDLycProcessPrgM.Location = New System.Drawing.Point(0, 150)
        Me.INDLycProcessPrgM.MinSize = New System.Drawing.Size(54, 26)
        Me.INDLycProcessPrgM.Name = "INDLycProcessPrgM"
        Me.INDLycProcessPrgM.Size = New System.Drawing.Size(622, 30)
        Me.INDLycProcessPrgM.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLycProcessPrgM.Text = "INDLycProcessPrgM"
        Me.INDLycProcessPrgM.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLycProcessPrgM.TextToControlDistance = 0
        Me.INDLycProcessPrgM.TextVisible = False
        Me.INDLycProcessPrgM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.DynamicDotNetTwainView
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(622, 150)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup5.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup4"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 34)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(182, 323)
        Me.LayoutControlGroup5.Text = "Lista"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.DynamicDotNetTwainThumb
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(158, 263)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12, Me.LayoutControlItem11, Me.LayoutControlItem15, Me.LayoutControlItem13, Me.LayoutControlItem14, Me.LayoutControlItem16, Me.LayoutControlItem17, Me.LayoutControlGroup7})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(182, 274)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(646, 83)
        Me.LayoutControlGroup6.Text = "LayoutControlGroup5"
        Me.LayoutControlGroup6.TextVisible = False
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.INDbtnIzquierda
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(76, 0)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(38, 59)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.Text = "LayoutControlItem12"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextToControlDistance = 0
        Me.LayoutControlItem12.TextVisible = False
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.INDbtnRotarIzquierda
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(38, 0)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(38, 59)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.Text = "LayoutControlItem11"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextToControlDistance = 0
        Me.LayoutControlItem11.TextVisible = False
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.INDbtnPrimero
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(38, 59)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.Text = "LayoutControlItem15"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextToControlDistance = 0
        Me.LayoutControlItem15.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.INDbtnDerecha
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(470, 0)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(38, 59)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.Text = "LayoutControlItem13"
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextToControlDistance = 0
        Me.LayoutControlItem13.TextVisible = False
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.INDbtnRotarDerecha
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(508, 0)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(38, 59)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.Text = "LayoutControlItem14"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem14.TextToControlDistance = 0
        Me.LayoutControlItem14.TextVisible = False
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.INDbtnEliminar
        Me.LayoutControlItem16.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(584, 0)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(38, 59)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.Text = "LayoutControlItem16"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem16.TextToControlDistance = 0
        Me.LayoutControlItem16.TextVisible = False
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.INDbtnUltimo
        Me.LayoutControlItem17.CustomizationFormText = "LayoutControlItem17"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(546, 0)
        Me.LayoutControlItem17.MaxSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem17.MinSize = New System.Drawing.Size(38, 34)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(38, 59)
        Me.LayoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem17.Text = "LayoutControlItem17"
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem17.TextToControlDistance = 0
        Me.LayoutControlItem17.TextVisible = False
        '
        'LayoutControlGroup7
        '
        Me.LayoutControlGroup7.CustomizationFormText = "LayoutControlGroup6"
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem18, Me.EmptySpaceItem2})
        Me.LayoutControlGroup7.Location = New System.Drawing.Point(114, 0)
        Me.LayoutControlGroup7.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup7.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2)
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(356, 59)
        Me.LayoutControlGroup7.Text = "LayoutControlGroup6"
        Me.LayoutControlGroup7.TextVisible = False
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem18.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlItem18.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem18.Control = Me.INDlblImagenActual
        Me.LayoutControlItem18.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.LayoutControlItem18.CustomizationFormText = "LayoutControlItem18"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem18.MinSize = New System.Drawing.Size(92, 25)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(346, 25)
        Me.LayoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem18.Text = "LayoutControlItem18"
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem18.TextToControlDistance = 0
        Me.LayoutControlItem18.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 25)
        Me.EmptySpaceItem2.MinSize = New System.Drawing.Size(104, 24)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(346, 24)
        Me.EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(880, 409)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.LayoutControl2
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(860, 389)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'FrmDigitalization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 549)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmDigitalization"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Digitalización"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.AdditionalControlPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDImgButtons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDImagenes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDMarqueeProgressBarControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDchkDuplex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDchkADF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDcbDispositivos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycProcessPrgM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDImagenes As DevExpress.Utils.ImageCollection
    Friend WithEvents INDImgButtons As DevExpress.Utils.ImageCollection
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents DynamicDotNetTwainThumb As Dynamsoft.DotNet.TWAIN.DynamicDotNetTwain
    Friend WithEvents DynamicDotNetTwainView As Dynamsoft.DotNet.TWAIN.DynamicDotNetTwain
    Friend WithEvents INDlblImagenActual As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDMarqueeProgressBarControl As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents INDbtnUltimo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnEliminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnPrimero As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnGuardar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnRotarDerecha As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnEscanear As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDbtnRotarIzquierda As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnDerecha As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnIzquierda As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDchkDuplex As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDchkADF As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDcbDispositivos As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLycProcessPrgM As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
End Class

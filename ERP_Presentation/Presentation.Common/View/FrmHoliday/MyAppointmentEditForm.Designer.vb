' Developer Express Code Central Example:
' How to customize the Edit Appointment form to show custom fields
' 
' This example illustrates the use of a custom form to enable the end-user to edit
' custom fields. The custom form is invoked instead of the default one by handling
' the SchedulerControl.EditAppointmentFormShowing
' (ms-help://DevExpress.NETv8.2/DevExpress.XtraScheduler/DevExpressXtraSchedulerSchedulerControl_EditAppointmentFormShowingtopic.htm)
' event.
' 
' See also:
' For a simple application that enables you to handle custom
' fields, see the http://www.devexpress.com/scid=E2782 article.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E152

Imports Microsoft.VisualBasic
Imports System

Namespace CustomAppointmentEditForm


    Partial Public Class MyAppointmentEditForm

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MyAppointmentEditForm))
            Me.dtStart = New DevExpress.XtraEditors.DateEdit()
            Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
            Me.txSubject = New DevExpress.XtraEditors.TextEdit()
            Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
            Me.INDLyGrAppoimentData = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.INDLyItemDate = New DevExpress.XtraLayout.LayoutControlItem()
            Me.INDLyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
            Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
            Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
            Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
            Me.IndigoDate1 = New Presentation.Controls.IndigoDate()
            CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ToolBars.SuspendLayout()
            CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dtStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dtStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.LayoutControl1.SuspendLayout()
            CType(Me.txSubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.INDLyGrAppoimentData, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.INDLyItemDate, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.INDLyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'INDPanelControlBase
            '
            resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
            '
            'ToolBars
            '
            resources.ApplyResources(Me.ToolBars, "ToolBars")
            Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
            Me.ToolBars.Appearance.FontSizeDelta = CType(resources.GetObject("ToolBars.Appearance.FontSizeDelta"), Integer)
            Me.ToolBars.Appearance.FontStyleDelta = CType(resources.GetObject("ToolBars.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
            Me.ToolBars.Appearance.GradientMode = CType(resources.GetObject("ToolBars.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.ToolBars.Appearance.Image = CType(resources.GetObject("ToolBars.Appearance.Image"), System.Drawing.Image)
            Me.ToolBars.Appearance.Options.UseBackColor = True
            '
            'BarraBotones
            '
            resources.ApplyResources(Me.BarraBotones, "BarraBotones")
            Me.BarraBotones.LookAndFeel.SkinName = "Blue"
            Me.BarraBotones.OperatingUnitVisible = True
            '
            'dtStart
            '
            resources.ApplyResources(Me.dtStart, "dtStart")
            Me.IndigoTextEdit1.SetApplyStyle(Me.dtStart, False)
            Me.IndigoDate1.SetCampoObligatorio(Me.dtStart, False)
            Me.IndigoTextEdit1.SetCampoObligatorio(Me.dtStart, False)
            Me.dtStart.EnterMoveNextControl = True
            Me.IndigoTextEdit1.SetMascara(Me.dtStart, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
            Me.IndigoDate1.SetMascaraDate(Me.dtStart, Presentation.Controls.IndigoDate.EMask.Fecha)
            Me.dtStart.Name = "dtStart"
            Me.dtStart.Properties.AccessibleDescription = resources.GetString("dtStart.Properties.AccessibleDescription")
            Me.dtStart.Properties.AccessibleName = resources.GetString("dtStart.Properties.AccessibleName")
            Me.dtStart.Properties.Appearance.BackColor = CType(resources.GetObject("dtStart.Properties.Appearance.BackColor"), System.Drawing.Color)
            Me.dtStart.Properties.Appearance.Font = CType(resources.GetObject("dtStart.Properties.Appearance.Font"), System.Drawing.Font)
            Me.dtStart.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("dtStart.Properties.Appearance.FontSizeDelta"), Integer)
            Me.dtStart.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("dtStart.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
            Me.dtStart.Properties.Appearance.GradientMode = CType(resources.GetObject("dtStart.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.dtStart.Properties.Appearance.Image = CType(resources.GetObject("dtStart.Properties.Appearance.Image"), System.Drawing.Image)
            Me.dtStart.Properties.Appearance.Options.UseBackColor = True
            Me.dtStart.Properties.Appearance.Options.UseFont = True
            Me.dtStart.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("dtStart.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
            Me.dtStart.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("dtStart.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
            Me.dtStart.Properties.AppearanceFocused.Font = CType(resources.GetObject("dtStart.Properties.AppearanceFocused.Font"), System.Drawing.Font)
            Me.dtStart.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("dtStart.Properties.AppearanceFocused.FontSizeDelta"), Integer)
            Me.dtStart.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("dtStart.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
            Me.dtStart.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("dtStart.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.dtStart.Properties.AppearanceFocused.Image = CType(resources.GetObject("dtStart.Properties.AppearanceFocused.Image"), System.Drawing.Image)
            Me.dtStart.Properties.AppearanceFocused.Options.UseBackColor = True
            Me.dtStart.Properties.AppearanceFocused.Options.UseBorderColor = True
            Me.dtStart.Properties.AppearanceFocused.Options.UseFont = True
            Me.dtStart.Properties.AutoHeight = CType(resources.GetObject("dtStart.Properties.AutoHeight"), Boolean)
            Me.dtStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtStart.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
            Me.dtStart.Properties.CalendarTimeProperties.AccessibleDescription = resources.GetString("dtStart.Properties.CalendarTimeProperties.AccessibleDescription")
            Me.dtStart.Properties.CalendarTimeProperties.AccessibleName = resources.GetString("dtStart.Properties.CalendarTimeProperties.AccessibleName")
            Me.dtStart.Properties.CalendarTimeProperties.AutoHeight = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.AutoHeight"), Boolean)
            Me.dtStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
            Me.dtStart.Properties.CalendarTimeProperties.Mask.AutoComplete = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
            Me.dtStart.Properties.CalendarTimeProperties.Mask.BeepOnError = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.Mask.BeepOnError"), Boolean)
            Me.dtStart.Properties.CalendarTimeProperties.Mask.EditMask = resources.GetString("dtStart.Properties.CalendarTimeProperties.Mask.EditMask")
            Me.dtStart.Properties.CalendarTimeProperties.Mask.IgnoreMaskBlank = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.Mask.IgnoreMaskBlank"), Boolean)
            Me.dtStart.Properties.CalendarTimeProperties.Mask.MaskType = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
            Me.dtStart.Properties.CalendarTimeProperties.Mask.PlaceHolder = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.Mask.PlaceHolder"), Char)
            Me.dtStart.Properties.CalendarTimeProperties.Mask.SaveLiteral = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.Mask.SaveLiteral"), Boolean)
            Me.dtStart.Properties.CalendarTimeProperties.Mask.ShowPlaceHolders = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.Mask.ShowPlaceHolders"), Boolean)
            Me.dtStart.Properties.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat"), Boolean)
            Me.dtStart.Properties.CalendarTimeProperties.NullValuePrompt = resources.GetString("dtStart.Properties.CalendarTimeProperties.NullValuePrompt")
            Me.dtStart.Properties.CalendarTimeProperties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("dtStart.Properties.CalendarTimeProperties.NullValuePromptShowForEmptyValue"), Boolean)
            Me.dtStart.Properties.Mask.AutoComplete = CType(resources.GetObject("dtStart.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
            Me.dtStart.Properties.Mask.BeepOnError = CType(resources.GetObject("dtStart.Properties.Mask.BeepOnError"), Boolean)
            Me.dtStart.Properties.Mask.EditMask = resources.GetString("dtStart.Properties.Mask.EditMask")
            Me.dtStart.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("dtStart.Properties.Mask.IgnoreMaskBlank"), Boolean)
            Me.dtStart.Properties.Mask.MaskType = CType(resources.GetObject("dtStart.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
            Me.dtStart.Properties.Mask.PlaceHolder = CType(resources.GetObject("dtStart.Properties.Mask.PlaceHolder"), Char)
            Me.dtStart.Properties.Mask.SaveLiteral = CType(resources.GetObject("dtStart.Properties.Mask.SaveLiteral"), Boolean)
            Me.dtStart.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("dtStart.Properties.Mask.ShowPlaceHolders"), Boolean)
            Me.dtStart.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("dtStart.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
            Me.dtStart.Properties.NullValuePrompt = resources.GetString("dtStart.Properties.NullValuePrompt")
            Me.dtStart.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("dtStart.Properties.NullValuePromptShowForEmptyValue"), Boolean)
            Me.dtStart.StyleController = Me.LayoutControl1
            Me.IndigoTextEdit1.SetTamañoMinimoString(Me.dtStart, 0)
            '
            'LayoutControl1
            '
            resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
            Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
            Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
            Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
            Me.LayoutControl1.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
            Me.LayoutControl1.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
            Me.LayoutControl1.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControl1.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
            Me.LayoutControl1.Controls.Add(Me.dtStart)
            Me.LayoutControl1.Controls.Add(Me.txSubject)
            Me.LayoutControl1.Name = "LayoutControl1"
            Me.LayoutControl1.Root = Me.LayoutControlGroup1
            '
            'txSubject
            '
            resources.ApplyResources(Me.txSubject, "txSubject")
            Me.IndigoTextEdit1.SetApplyStyle(Me.txSubject, False)
            Me.IndigoTextEdit1.SetCampoObligatorio(Me.txSubject, False)
            Me.txSubject.EnterMoveNextControl = True
            Me.IndigoTextEdit1.SetMascara(Me.txSubject, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
            Me.txSubject.Name = "txSubject"
            Me.txSubject.Properties.AccessibleDescription = resources.GetString("txSubject.Properties.AccessibleDescription")
            Me.txSubject.Properties.AccessibleName = resources.GetString("txSubject.Properties.AccessibleName")
            Me.txSubject.Properties.Appearance.BackColor = CType(resources.GetObject("txSubject.Properties.Appearance.BackColor"), System.Drawing.Color)
            Me.txSubject.Properties.Appearance.Font = CType(resources.GetObject("txSubject.Properties.Appearance.Font"), System.Drawing.Font)
            Me.txSubject.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("txSubject.Properties.Appearance.FontSizeDelta"), Integer)
            Me.txSubject.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("txSubject.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
            Me.txSubject.Properties.Appearance.GradientMode = CType(resources.GetObject("txSubject.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.txSubject.Properties.Appearance.Image = CType(resources.GetObject("txSubject.Properties.Appearance.Image"), System.Drawing.Image)
            Me.txSubject.Properties.Appearance.Options.UseBackColor = True
            Me.txSubject.Properties.Appearance.Options.UseFont = True
            Me.txSubject.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("txSubject.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
            Me.txSubject.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("txSubject.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
            Me.txSubject.Properties.AppearanceFocused.Font = CType(resources.GetObject("txSubject.Properties.AppearanceFocused.Font"), System.Drawing.Font)
            Me.txSubject.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("txSubject.Properties.AppearanceFocused.FontSizeDelta"), Integer)
            Me.txSubject.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("txSubject.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
            Me.txSubject.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("txSubject.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.txSubject.Properties.AppearanceFocused.Image = CType(resources.GetObject("txSubject.Properties.AppearanceFocused.Image"), System.Drawing.Image)
            Me.txSubject.Properties.AppearanceFocused.Options.UseBackColor = True
            Me.txSubject.Properties.AppearanceFocused.Options.UseBorderColor = True
            Me.txSubject.Properties.AppearanceFocused.Options.UseFont = True
            Me.txSubject.Properties.AutoHeight = CType(resources.GetObject("txSubject.Properties.AutoHeight"), Boolean)
            Me.txSubject.Properties.Mask.AutoComplete = CType(resources.GetObject("txSubject.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
            Me.txSubject.Properties.Mask.BeepOnError = CType(resources.GetObject("txSubject.Properties.Mask.BeepOnError"), Boolean)
            Me.txSubject.Properties.Mask.EditMask = resources.GetString("txSubject.Properties.Mask.EditMask")
            Me.txSubject.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("txSubject.Properties.Mask.IgnoreMaskBlank"), Boolean)
            Me.txSubject.Properties.Mask.MaskType = CType(resources.GetObject("txSubject.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
            Me.txSubject.Properties.Mask.PlaceHolder = CType(resources.GetObject("txSubject.Properties.Mask.PlaceHolder"), Char)
            Me.txSubject.Properties.Mask.SaveLiteral = CType(resources.GetObject("txSubject.Properties.Mask.SaveLiteral"), Boolean)
            Me.txSubject.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("txSubject.Properties.Mask.ShowPlaceHolders"), Boolean)
            Me.txSubject.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("txSubject.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
            Me.txSubject.Properties.MaxLength = 50
            Me.txSubject.Properties.NullValuePrompt = resources.GetString("txSubject.Properties.NullValuePrompt")
            Me.txSubject.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("txSubject.Properties.NullValuePromptShowForEmptyValue"), Boolean)
            Me.txSubject.StyleController = Me.LayoutControl1
            Me.IndigoTextEdit1.SetTamañoMinimoString(Me.txSubject, 0)
            '
            'LayoutControlGroup1
            '
            Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
            Me.LayoutControlGroup1.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.FontSizeDelta"), Integer)
            Me.LayoutControlGroup1.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
            'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.ForeColor"), System.Drawing.Color)
            Me.LayoutControlGroup1.AppearanceGroup.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControlGroup1.AppearanceGroup.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Image"), System.Drawing.Image)
            Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
            'Cadena reemplazada... True
            Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
            Me.LayoutControlGroup1.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.FontSizeDelta"), Integer)
            Me.LayoutControlGroup1.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
            Me.LayoutControlGroup1.AppearanceItemCaption.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControlGroup1.AppearanceItemCaption.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Image"), System.Drawing.Image)
            Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
            Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
            Me.LayoutControlGroup1.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.FontSizeDelta"), Integer)
            Me.LayoutControlGroup1.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
            Me.LayoutControlGroup1.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControlGroup1.AppearanceTabPage.Header.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Image"), System.Drawing.Image)
            Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
            'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
            'Cadena reemplazada... True
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
            'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
            Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
            'Cadena reemplazada... True
            Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
            Me.LayoutControlGroup1.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
            Me.LayoutControlGroup1.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
            Me.LayoutControlGroup1.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
            Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
            Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
            resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
            Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
            Me.LayoutControlGroup1.GroupBordersVisible = False
            Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.INDLyGrAppoimentData})
            Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
            Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
            Me.LayoutControlGroup1.Size = New System.Drawing.Size(714, 323)
            Me.LayoutControlGroup1.TextVisible = False
            '
            'EmptySpaceItem1
            '
            Me.EmptySpaceItem1.AllowHotTrack = False
            resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
            Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 132)
            Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
            Me.EmptySpaceItem1.Size = New System.Drawing.Size(694, 171)
            Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
            '
            'INDLyGrAppoimentData
            '
            Me.INDLyGrAppoimentData.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceGroup.Font"), System.Drawing.Font)
            Me.INDLyGrAppoimentData.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceGroup.FontSizeDelta"), Integer)
            Me.INDLyGrAppoimentData.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
            'Cadena reemplazada... CType(resources.GetObject("INDLyGrAppoimentData.AppearanceGroup.ForeColor"), System.Drawing.Color)
            Me.INDLyGrAppoimentData.AppearanceGroup.GradientMode = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.INDLyGrAppoimentData.AppearanceGroup.Image = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceGroup.Image"), System.Drawing.Image)
            Me.INDLyGrAppoimentData.AppearanceGroup.Options.UseFont = True
            'Cadena reemplazada... True
            Me.INDLyGrAppoimentData.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceItemCaption.Font"), System.Drawing.Font)
            Me.INDLyGrAppoimentData.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceItemCaption.FontSizeDelta"), Integer)
            Me.INDLyGrAppoimentData.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
            Me.INDLyGrAppoimentData.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.INDLyGrAppoimentData.AppearanceItemCaption.Image = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceItemCaption.Image"), System.Drawing.Image)
            Me.INDLyGrAppoimentData.AppearanceItemCaption.Options.UseFont = True
            Me.INDLyGrAppoimentData.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.Header.Font"), System.Drawing.Font)
            Me.INDLyGrAppoimentData.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.Header.FontSizeDelta"), Integer)
            Me.INDLyGrAppoimentData.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
            Me.INDLyGrAppoimentData.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.INDLyGrAppoimentData.AppearanceTabPage.Header.Image = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.Header.Image"), System.Drawing.Image)
            Me.INDLyGrAppoimentData.AppearanceTabPage.Header.Options.UseFont = True
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
            'Cadena reemplazada... CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderActive.Options.UseFont = True
            'Cadena reemplazada... True
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
            'Cadena reemplazada... CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
            Me.INDLyGrAppoimentData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
            'Cadena reemplazada... True
            Me.INDLyGrAppoimentData.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
            Me.INDLyGrAppoimentData.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
            Me.INDLyGrAppoimentData.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
            Me.INDLyGrAppoimentData.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
            Me.INDLyGrAppoimentData.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("INDLyGrAppoimentData.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
            Me.INDLyGrAppoimentData.AppearanceTabPage.PageClient.Options.UseFont = True
            Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrAppoimentData, False)
            resources.ApplyResources(Me.INDLyGrAppoimentData, "INDLyGrAppoimentData")
            Me.INDLyGrAppoimentData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemDate, Me.INDLyItemDescription})
            Me.INDLyGrAppoimentData.Location = New System.Drawing.Point(0, 0)
            Me.INDLyGrAppoimentData.Name = "INDLyGrAppoimentData"
            Me.INDLyGrAppoimentData.Size = New System.Drawing.Size(694, 132)
            '
            'INDLyItemDate
            '
            Me.INDLyItemDate.Control = Me.dtStart
            resources.ApplyResources(Me.INDLyItemDate, "INDLyItemDate")
            Me.INDLyItemDate.Location = New System.Drawing.Point(0, 0)
            Me.INDLyItemDate.MaxSize = New System.Drawing.Size(400, 36)
            Me.INDLyItemDate.MinSize = New System.Drawing.Size(400, 36)
            Me.INDLyItemDate.Name = "INDLyItemDate"
            Me.INDLyItemDate.Size = New System.Drawing.Size(670, 36)
            Me.INDLyItemDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            Me.INDLyItemDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
            Me.INDLyItemDate.TextSize = New System.Drawing.Size(145, 21)
            Me.INDLyItemDate.TextToControlDistance = 12
            '
            'INDLyItemDescription
            '
            Me.INDLyItemDescription.Control = Me.txSubject
            resources.ApplyResources(Me.INDLyItemDescription, "INDLyItemDescription")
            Me.INDLyItemDescription.Location = New System.Drawing.Point(0, 36)
            Me.INDLyItemDescription.MaxSize = New System.Drawing.Size(700, 36)
            Me.INDLyItemDescription.MinSize = New System.Drawing.Size(600, 36)
            Me.INDLyItemDescription.Name = "INDLyItemDescription"
            Me.INDLyItemDescription.Size = New System.Drawing.Size(670, 36)
            Me.INDLyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            Me.INDLyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
            Me.INDLyItemDescription.TextSize = New System.Drawing.Size(145, 21)
            Me.INDLyItemDescription.TextToControlDistance = 12
            '
            'EmptySpaceItem3
            '
            Me.EmptySpaceItem3.AllowHotTrack = False
            resources.ApplyResources(Me.EmptySpaceItem3, "EmptySpaceItem3")
            Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 366)
            Me.EmptySpaceItem3.Name = "EmptySpaceItem2"
            Me.EmptySpaceItem3.Size = New System.Drawing.Size(99, 26)
            Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
            '
            'MyAppointmentEditForm
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.LayoutControl1)
            Me.Name = "MyAppointmentEditForm"
            Me.Opacity = 1.0R
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Tag = "553"
            Me.Controls.SetChildIndex(Me.ToolBars, 0)
            Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
            Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
            CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ToolBars.ResumeLayout(False)
            CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dtStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dtStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.LayoutControl1.ResumeLayout(False)
            CType(Me.txSubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.INDLyGrAppoimentData, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.INDLyItemDate, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.INDLyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private WithEvents dtStart As DevExpress.XtraEditors.DateEdit
        Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
        Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
        Friend WithEvents INDLyItemDescription As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents INDLyItemDate As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
        Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
        Friend WithEvents INDLyGrAppoimentData As DevExpress.XtraLayout.LayoutControlGroup
        Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
        Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents txSubject As DevExpress.XtraEditors.TextEdit
    End Class
End Namespace
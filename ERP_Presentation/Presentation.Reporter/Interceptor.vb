Imports DevExpress.XtraReports.UserDesigner
Imports DevExpress.XtraReports.UI
Imports System.IO
Imports DevExpress.XtraEditors

Public Class Interceptor

    ''' <summary>
    ''' Bandera que indica si el interceptor es usado en el
    ''' control extendido y por ello el metodo guardar no debe preguntar
    ''' nombre de reporte.
    ''' </summary>
    Private _flagUniqueName As Boolean
    Private _report As XtraReport
    Private _mdiController As XRDesignMdiController
    Private _pathReport As String
    Private _dialogResult As DialogResult
    Public Property DialogResult As DialogResult
        Get
            Return Me._dialogResult
        End Get
        Set(value As DialogResult)
            Me._dialogResult = value
        End Set
    End Property

    Sub New(Report As XtraReport, pathReport As String, Optional flagUniqueName As Boolean = False)
        Me._flagUniqueName = flagUniqueName
        Me._pathReport = pathReport
        Me._report = Report
        Me._dialogResult = DialogResult.Ignore
        Me.LoadLayout()
        If TypeOf Report Is IReport Then
            CType(Report, IReport).CargarDataSource()
            CType(Report, IReport).CargarImagenes()
        End If
    End Sub

    Private Delegate Sub InvokeHandler()

    Public Sub Show()
        Dim form As New XRDesignForm()

        Me._mdiController = form.DesignMdiController
        AddHandler Me._mdiController.DesignPanelLoaded, AddressOf mdiController_DesignPanelLoaded
        Me._mdiController.OpenReport(Me._report)
        form.Show()
    End Sub

    Public Function ShowDialog() As DialogResult
        ' Create a design form and get its MDI controller.
        Dim form As New XRDesignForm()

        Me._mdiController = form.DesignMdiController
        AddHandler Me._mdiController.DesignPanelLoaded, AddressOf mdiController_DesignPanelLoaded
        Me._mdiController.OpenReport(Me._report)
        Me._dialogResult = form.ShowDialog()
        Return Me._dialogResult
    End Function

    Private Sub mdiController_DesignPanelLoaded(ByVal sender As Object, ByVal e As DesignerLoadedEventArgs)
        Dim panel As XRDesignPanel = CType(sender, XRDesignPanel)
        Me._mdiController.SetCommandVisibility(ReportCommand.NewReport, CommandVisibility.None)
        Me._mdiController.SetCommandVisibility(ReportCommand.NewReportWizard, CommandVisibility.None)
        Me._mdiController.SetCommandVisibility(ReportCommand.OpenFile, CommandVisibility.None)
        Me._mdiController.SetCommandVisibility(ReportCommand.OpenRemoteReport, CommandVisibility.None)
        Me._mdiController.SetCommandVisibility(ReportCommand.OpenSubreport, CommandVisibility.None)
        Me._mdiController.SetCommandVisibility(ReportCommand.SaveFileAs, CommandVisibility.None)
        Me._mdiController.SetCommandVisibility(ReportCommand.SaveAll, CommandVisibility.None)
        Me._mdiController.AddCommandHandler(New SaveCommandHandler(panel, Me._pathReport, Me, Me._flagUniqueName))
    End Sub

    Private Sub LoadLayout()
        If File.Exists(Me._pathReport) Then
            Me._report.LoadLayout(Me._pathReport)
        End If
    End Sub

End Class

Public Class SaveCommandHandler
    Implements DevExpress.XtraReports.UserDesigner.ICommandHandler
    Private panel As XRDesignPanel
    Private _flagUniqueName As Boolean
    Private _pathReport As String
    Private _interceptorParent As Interceptor

    Public Sub New(ByVal panel As XRDesignPanel, ByVal pathReport As String, ByVal InterceptorParent As Interceptor, ByVal flagUniqueName As Boolean)
        Me.panel = panel
        Me._flagUniqueName = flagUniqueName
        Me._pathReport = pathReport
        Me._interceptorParent = InterceptorParent
    End Sub

    Public Sub HandleCommand(ByVal command As DevExpress.XtraReports.UserDesigner.ReportCommand, ByVal args() As Object) Implements DevExpress.XtraReports.UserDesigner.ICommandHandler.HandleCommand
        If Not CanHandleCommand(command, True) Then
            Return
        End If
        Select Case command
            Case ReportCommand.SaveFile
                Save()
            Case ReportCommand.Closing
                Closing()
        End Select
    End Sub

    Public Function CanHandleCommand(ByVal command As DevExpress.XtraReports.UserDesigner.ReportCommand, ByRef useNextHandler As Boolean) As Boolean Implements DevExpress.XtraReports.UserDesigner.ICommandHandler.CanHandleCommand
        useNextHandler = Not (command = ReportCommand.SaveFile OrElse command = ReportCommand.Closing)
        Return Not useNextHandler
    End Function

    Private Sub Closing()
        If panel.ReportState = ReportState.Changed Then
            If XtraMessageBox.Show("La definición del reporte ha sido modificada. Desea guardar los cambios realizados?", "Guardar definición!", MessageBoxButtons.YesNo) = MsgBoxResult.Yes Then
                Save()
            End If
        End If
    End Sub

    Private Sub Save()
        If Not Me._flagUniqueName AndAlso Me._pathReport.Contains("{0}") Then 'Es una nueva definición a partir de una compilada
            Dim res As String = InputBox("Escriba un nombre para la nueva definición:", "Guardando definición...")
            If Not res.Trim().Equals(String.Empty) Then
                If File.Exists(String.Format(Me._pathReport, res)) Then
                    If XtraMessageBox.Show("Ya existe una definición con el mismo nombre (" & res & "). Desea reemplazarlo?", "Definición ya existe!", MessageBoxButtons.YesNo) = MsgBoxResult.Yes Then
                        panel.Report.SaveLayout(String.Format(Me._pathReport, res))
                        panel.ReportState = ReportState.Saved
                        Me._interceptorParent.DialogResult = DialogResult.Ignore
                    End If
                Else
                    panel.Report.SaveLayout(String.Format(Me._pathReport, res))
                    panel.ReportState = ReportState.Saved
                    Me._interceptorParent.DialogResult = DialogResult.OK
                End If
            Else
                Me._interceptorParent.DialogResult = DialogResult.Ignore
                XtraMessageBox.Show("No ha escrito un nombre válido para la nueva definición", "Nombre inválido!")
            End If
        Else 'La definición ya existe y se esta editando
            Dim pathReport = _pathReport
            If Not pathReport.Contains(panel.Report.GetType().Name) Then
                Dim frmParentName = Path.GetFileName(pathReport).Split(".")(1)
                pathReport = pathReport.Replace(frmParentName, panel.Report.Name)
            End If

            panel.Report.SaveLayout(pathReport)
            panel.ReportState = ReportState.Saved
            Me._interceptorParent.DialogResult = DialogResult.Ignore
        End If
    End Sub

End Class
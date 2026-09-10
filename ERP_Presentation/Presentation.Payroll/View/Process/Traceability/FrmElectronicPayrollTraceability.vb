#Region "Imports"

Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Payroll.MVP
Imports Presentation.Reporter
Imports System.IO
Imports System.Windows.Forms

#End Region

Public Class FrmElectronicPayrollTraceability
    Implements IElectronicPayrollTraceability

#Region "Variables"

    ''' <summary>
    ''' presenter del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PElectronicPayrollTraceability

    Private _isPopupMenuShowing As Boolean

    Private waitForm As New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, DevExpress.XtraSplashScreen.ParentType.UserControl)

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object Implements IElectronicPayrollTraceability.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Datasource"

    Public Property ElectronicPayrollPaymentSupportXpo As XPInstantFeedbackSource Implements IElectronicPayrollTraceability.ElectronicPayrollPaymentSupportXpo
        Get
            Return CType(INDGcElectronicPayrollPaymentSupport.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcElectronicPayrollPaymentSupport.DataSource = value
        End Set
    End Property

    Public Property AdjustmentNoteXpo As XPInstantFeedbackSource Implements IElectronicPayrollTraceability.AdjustmentNoteXpo
        Get
            Return CType(INDGcAdjustmentNote.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcAdjustmentNote.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
    End Sub

#End Region

#Region "Methods"

    Private Sub CleanControls()
        ElectronicPayrollPaymentSupportXpo = Nothing
        AdjustmentNoteXpo = Nothing

        _selectorElectronicPayrollPaymentSupport.Clear()
        _selectorAdjustmentNote.Clear()
    End Sub

    Private Sub BeginReloadDatasource(selectedPage As String)
        Dim selector = GetSelector(selectedPage, False)
        selector.Clear()

        Select Case selectedPage
            Case INDLcgElectronicPayrollPaymentSupport.Name 'Soporte de pago de nomina electronica
                Me.ElectronicPayrollPaymentSupportXpo = Nothing
                Me._presenter.GetElectronicPayrollPaymentSupports()
            Case INDLcgAdjustmentNote.Name 'Notas de Ajuste
                Me.AdjustmentNoteXpo = Nothing
                Me._presenter.GetAdjustmentNotes()
        End Select
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al retornar el modal de eventos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnModalArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource(INDTcgElectronicPayroll.SelectedTabPageName)
    End Sub

    Private Sub OpenFormSendElectronicPayrollNotification(listElectronicPayrolls As List(Of Domain.Payroll.Entities.ElectronicPayroll))
        Using formulario As New FrmSendElectronicPayrollNotification()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ListElectronicPayrolls = listElectronicPayrolls
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

#Region "Selector"

    Private _selectorElectronicPayrollPaymentSupport As SelectorCache = New SelectorCache("Id", "Status", "Year", "Month", "EntityId", "EmployeePartyId")
    Private _selectorAdjustmentNote As SelectorCache = New SelectorCache("Id", "Status", "EntityId", "EmployeePartyId")

    Private Function GetSelector(selectedPage As String, selection As Boolean) As SelectorCache
        Dim selector As New SelectorCache("", "")
        Select Case selectedPage
            Case INDLcgElectronicPayrollPaymentSupport.Name 'Soporte de pago de nomina electronica
                If selection Then
                    _selectorElectronicPayrollPaymentSupport.SetValue(INDGvElectronicPayrollPaymentSupport.GetFocusedRow, True)
                End If
                selector = _selectorElectronicPayrollPaymentSupport
            Case INDLcgAdjustmentNote.Name 'Notas de ajuste
                If selection Then
                    _selectorAdjustmentNote.SetValue(INDGvAdjustmentNote.GetFocusedRow, True)
                End If
                selector = _selectorAdjustmentNote
        End Select
        Return selector
    End Function

    Private Function GetView(selectedPage As String) As GridView
        Dim view As New GridView
        Select Case selectedPage
            Case INDLcgElectronicPayrollPaymentSupport.Name 'Soporte de pago de nomina electronica
                view = INDGvElectronicPayrollPaymentSupport
            Case INDLcgAdjustmentNote.Name 'Notas de ajuste
                view = INDGvAdjustmentNote
        End Select
        Return view
    End Function

#End Region

#End Region

#Region "Handles"

#Region "Load And Disposed"

    Private Async Sub FrmElectronicPayrollTraceability_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()

        Me._presenter = New PElectronicPayrollTraceability(Me)
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmElectronicPayrollTraceability_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        INDTcgElectronicPayroll.SelectedTabPageIndex = 0
        ReturnModalArgs(Nothing, Nothing)
    End Sub

#End Region

#Region "SelectedPageChanged"

    Private Sub INDTcgElectronicPayroll_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgElectronicPayroll.SelectedPageChanged
        BeginReloadDatasource(e.Page.Name)
    End Sub

#End Region

#Region "Selection"

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvElectronicPayrollPaymentSupport.CustomUnboundColumnData, INDGvAdjustmentNote.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvElectronicPayrollPaymentSupport" Then
                e.Value = _selectorElectronicPayrollPaymentSupport.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvAdjustmentNote" Then
                e.Value = _selectorAdjustmentNote.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para las rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDview_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvElectronicPayrollPaymentSupport.PopupMenuShowing, INDGvAdjustmentNote.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If

        Dim view = CType(sender, GridView)

        INDBbiPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiGenerateAdjustmentNote.Visibility = BarItemVisibility.Never
        INDBbiSendNotification.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Dim selector = GetSelector(INDTcgElectronicPayroll.SelectedTabPageName, False)

        If selector.Count > 0 Then
            'INDBbiPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBbiExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            If BarraBotones.PermissionsForm.ContainsKey(81) Then 'si tiene permiso de procesar
                For Each key In selector.GetKeysToArray()
                    If selector.GetValueByKey(key, "Status") <> "3" Then
                        INDBbiProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        Exit For
                    ElseIf view.Name = INDGvElectronicPayrollPaymentSupport.Name AndAlso selector.Count = 1 Then
                        INDBbiGenerateAdjustmentNote.Visibility = BarItemVisibility.Always
                    End If
                Next
            End If

            'For Each key In selector.GetKeysToArray()
            '    If selector.GetValueByKey(key, "Status") = "3" Then
            '        INDBbiSendNotification.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            '        Exit For
            '    End If
            'Next
        End If

        Me._isPopupMenuShowing = True

        view.RefreshRow(e.HitInfo.RowHandle)
        INDPopMenuActions2.Manager = BarManager2
        INDPopMenuActions2.ShowPopup(view.GridControl.PointToScreen(e.Point))
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDBbiSelection_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiSelection.ItemClick
        Dim selector = GetSelector(INDTcgElectronicPayroll.SelectedTabPageName, False)
        Dim view = GetView(INDTcgElectronicPayroll.SelectedTabPageName)

        selector.Clear()

        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                selector.SetValue(view.GetRow(item), True)
                view.RefreshRow(item)
            End If
        Next

        view.RefreshData()
    End Sub

    Private Sub INDBbiUnSelection_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiUnSelection.ItemClick
        Dim selector = GetSelector(INDTcgElectronicPayroll.SelectedTabPageName, False)

        For Each item As Integer In INDGvElectronicPayrollPaymentSupport.GetSelectedRows()
            If item > -1 Then
                selector.UnSetValue(INDGvElectronicPayrollPaymentSupport.GetRow(item))
                INDGvElectronicPayrollPaymentSupport.RefreshRow(item)
            End If
        Next
    End Sub

    Private Sub INDBbiPrint_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiPrint.ItemClick
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Dim selector = GetSelector(INDTcgElectronicPayroll.SelectedTabPageName, False)
        'Select Case INDTcgElectronicPayroll.SelectedTabPageName
        '    Case INDLcgElectronicPayrollPaymentSupport.Name 'Facturas
        '        Dim listElectronicPayrollPaymentSupports As New List(Of Domain.Entities.ElectronicPayrollPaymentSupport)
        '        For Each key In selector.GetKeysToArray()
        '            listElectronicPayrollPaymentSupports.Add(New Domain.Entities.ElectronicPayrollPaymentSupport With {.Id = selector.GetValueByKey(key, "EntityId")})
        '        Next

        '        Dim reportDef As New Reporter.rptSubSaleElectronicPayrollPaymentSupportAll
        '        AddHandler reportDef.AfterPrint, Sub()
        '                                             If waitForm.IsSplashFormVisible Then
        '                                                 waitForm.CloseWaitForm()
        '                                             End If
        '                                         End Sub
        '        ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listElectronicPayrollPaymentSupports)
        'End Select
    End Sub

    Private Async Sub INDBbiProcess_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiProcess.ItemClick
        Dim selector = GetSelector(INDTcgElectronicPayroll.SelectedTabPageName, False)
        Dim view = GetView(INDTcgElectronicPayroll.SelectedTabPageName)

        Try
            Dim listElectronicPayrolls As New List(Of Integer)

            For Each key In selector.GetKeysToArray()
                If selector.GetValueByKey(key, "Status") <> "3" Then
                    listElectronicPayrolls.Add(key)
                End If
            Next

            If listElectronicPayrolls.Count = 0 Then
                Mensaje(EeventViewerImages.Informacion) = "Debe seleccionar al menos un registro sin validar"
                Exit Sub
            End If

            If BarraBotones.OperatingUnitValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(UsuarioUnidadOperativaDefault, Eform.Usuario)
                Exit Sub
            End If

            Using Model As New MElectronicPayrollTraceability(Me.Tag)
                AsyncLoader(True)
                Dim result = Await Model.ProcessElectronicPayroll(BarraBotones.OperatingUnitValue, listElectronicPayrolls)
                AsyncLoader(False)

                If Not String.IsNullOrEmpty(result.ObjectEmbbeded) Then
                    Mensaje(EeventViewerImages.Informacion) = result.ObjectEmbbeded
                End If

                If Not String.IsNullOrEmpty(result.Message) Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using

            selector.Clear()
            view.RefreshData()
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Async Sub INDBbiGenerateAdjustmentNote_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiGenerateAdjustmentNote.ItemClick
        Dim selector = GetSelector(INDTcgElectronicPayroll.SelectedTabPageName, False)
        Dim view = GetView(INDTcgElectronicPayroll.SelectedTabPageName)

        Try
            Dim electronicPayroll As Domain.Payroll.Entities.ElectronicPayroll = Nothing

            If selector.Count <> 1 Then
                Mensaje(EeventViewerImages.Informacion) = "Debe seleccionar solo un registro"
                Exit Sub
            End If

            For Each key In selector.GetKeysToArray()
                If selector.GetValueByKey(key, "Status") <> "3" Then
                    Mensaje(EeventViewerImages.Informacion) = "Debe seleccionar un registro validado"
                    Exit Sub
                End If

                electronicPayroll = New Domain.Payroll.Entities.ElectronicPayroll With
                {
                    .Id = key,
                    .Year = selector.GetValueByKey(key, "Year"),
                    .Month = selector.GetValueByKey(key, "Month"),
                    .EmployeePartyId = selector.GetValueByKey(key, "EmployeePartyId")
                }
            Next

            Using Model As New MElectronicPayrollTraceability(Me.Tag)
                AsyncLoader(True)
                Dim result = Await Model.GenerateAdjustmentNote(BarraBotones.OperatingUnitValue, electronicPayroll)
                AsyncLoader(False)

                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using

            selector.Clear()
            view.RefreshData()
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Sub INDBbiSendNotification_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiSendNotification.ItemClick
        Dim selector = GetSelector(INDTcgElectronicPayroll.SelectedTabPageName, False)
        Dim listElectronicPayrolls As New List(Of Domain.Payroll.Entities.ElectronicPayroll)

        For Each key In selector.GetKeysToArray()
            If selector.GetValueByKey(key, "Status") = "3" Then
                listElectronicPayrolls.Add(New Domain.Payroll.Entities.ElectronicPayroll With {.Id = key, .EmployeePartyId = selector.GetValueByKey(key, "EmployeePartyId")})
            End If
        Next

        If listElectronicPayrolls.GroupBy(Function(ed) ed.EmployeePartyId).Count > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "Los registros seleccionados no corresponden al mismo cliente"
            Exit Sub
        End If

        OpenFormSendElectronicPayrollNotification(listElectronicPayrolls)
    End Sub

    Private Sub INDBbiExport_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiExport.ItemClick
        Dim selector = GetSelector(INDTcgElectronicPayroll.SelectedTabPageName, False)

        If selector.Count = 0 Then
            Mensaje(EeventViewerImages.Informacion) = "Debe seleccionar al menos un registro"
            Exit Sub
        End If

        AsyncLoader(True)
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of Infrastructure.Data.Xpo.PayrollRepository.ElectronicPayrollInformation) = Nothing
                                  Try
                                      result = _presenter.GetElectronicPayrollInformation(selector.GetKeys)
                                      INDGcExportExcel.BeginInvoke(Sub()
                                                                       INDGcExportExcel.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                       AsyncLoader(False)

                                                                       If INDGcExportExcel.DataSource IsNot Nothing Then
                                                                           generateExcel()
                                                                       End If
                                                                   End Sub)
                                  Catch ex As Exception
                                      INDGcExportExcel.BeginInvoke(Sub()
                                                                       Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                       AsyncLoader(False)
                                                                   End Sub)
                                  End Try
                              End Sub)
    End Sub

#End Region

#Region "ShowingEditor"

    Private Sub INDGvElectronicPayrollPaymentSupport_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvElectronicPayrollPaymentSupport.ShowingEditor
        INDGvElectronicPayrollPaymentSupport_PceDetails.PopupControl = Nothing
        INDGvAdjustmentNote_PceDetails.PopupControl = Nothing
        INDGcElectronicPayrollDetails.DataSource = Nothing

        INDGvElectronicPayrollPaymentSupport_PceNotifications.PopupControl = Nothing
        INDGvAdjustmentNote_PceNotifications.PopupControl = Nothing
        INDGcElectronicPayrollNotifications.DataSource = Nothing

        Dim ElectronicPayroll = CType(INDGvElectronicPayrollPaymentSupport.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicPayrollDetails.DataSource = Me._presenter.GetDetails(ElectronicPayroll.Id)
        INDGvElectronicPayrollPaymentSupport_PceDetails.PopupControl = INDPccElectronicPayrollDetails
        INDGcElectronicPayrollNotifications.DataSource = Me._presenter.GetNotifications(ElectronicPayroll.Id)
        INDGvElectronicPayrollPaymentSupport_PceNotifications.PopupControl = INDPccElectronicPayrollNotifications
    End Sub

    Private Sub INDGvAdjustmentNote_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvAdjustmentNote.ShowingEditor
        INDGvElectronicPayrollPaymentSupport_PceDetails.PopupControl = Nothing
        INDGvAdjustmentNote_PceDetails.PopupControl = Nothing
        INDGcElectronicPayrollDetails.DataSource = Nothing

        INDGvElectronicPayrollPaymentSupport_PceNotifications.PopupControl = Nothing
        INDGvAdjustmentNote_PceNotifications.PopupControl = Nothing
        INDGcElectronicPayrollNotifications.DataSource = Nothing

        Dim ElectronicPayroll = CType(INDGvAdjustmentNote.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicPayrollDetails.DataSource = Me._presenter.GetDetails(ElectronicPayroll.Id)
        INDGvAdjustmentNote_PceDetails.PopupControl = INDPccElectronicPayrollDetails
        INDGcElectronicPayrollNotifications.DataSource = Me._presenter.GetNotifications(ElectronicPayroll.Id)
        INDGvAdjustmentNote_PceNotifications.PopupControl = INDPccElectronicPayrollNotifications
    End Sub

    ''' <summary>
    ''' Evento que controla la acción del botón de descarga
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnDownload_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnDownload.ButtonClick
        Dim view As GridView = INDGvElectronicPayrollPaymentSupport
        Dim rowHandle As Integer = view.FocusedRowHandle

        If rowHandle >= 0 Then
            Dim consecutive As String = view.GetRowCellValue(rowHandle, "DocumentNumberWithPrefix")?.ToString()
            Dim state As String = view.GetRowCellValue(rowHandle, "StatusName")

            If state <> "Valido" Then
                Mensaje(EeventViewerImages.Advertencia) = $"El documento aún no ha sido validado por la DIAN, se encuentra en estado: {state}"
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(consecutive) Then
                GeneratePDF(consecutive)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método para generar y descargar el pdf de soporte de nómina electrónica
    ''' </summary>
    ''' <param name="consecutive"></param>
    Public Async Sub GeneratePDF(consecutive As String)
        Try
            Using Model As New MElectronicPayrollTraceability(Me.Tag)
                AsyncLoader(True)
                Dim result = Await Model.GetElectronicPaymentSupportXML(consecutive)

                If result Is Nothing OrElse Not result.StateResult OrElse result.ObjectEmbbeded Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = $"No se pudo obtener el documento de Nómina electrónica: {result.Message}"
                End If
                Dim payrollDocumentObject As NominaIndividual = result.ObjectEmbbeded

                If payrollDocumentObject IsNot Nothing Then

                    Dim cityXMLGeneration = _presenter.GetCityByCode(payrollDocumentObject.LugarGeneracionXML.MunicipioCiudad)
                    Dim workerCity = _presenter.GetCityByCode(payrollDocumentObject.Trabajador.LugarTrabajoMunicipioCiudad)

                    Dim departmentXMLGeneration = _presenter.GetDepartmentByCode(payrollDocumentObject.LugarGeneracionXML.DepartamentoEstado)
                    Dim workerDepartment = _presenter.GetDepartmentByCode(payrollDocumentObject.Trabajador.LugarTrabajoDepartamentoEstado)

                    Dim countryXMLGeneration = _presenter.GetCountryByStandardCode(payrollDocumentObject.LugarGeneracionXML.Pais)
                    Dim workerCountry = _presenter.GetCountryByStandardCode(payrollDocumentObject.Trabajador.LugarTrabajoPais)

                    payrollDocumentObject.LugarGeneracionXML.MunicipioCiudad = cityXMLGeneration.Descripcion
                    payrollDocumentObject.Trabajador.LugarTrabajoMunicipioCiudad = workerCity.Descripcion

                    payrollDocumentObject.LugarGeneracionXML.DepartamentoEstado = departmentXMLGeneration.Descripcion
                    payrollDocumentObject.Trabajador.LugarTrabajoDepartamentoEstado = workerDepartment.Descripcion

                    payrollDocumentObject.LugarGeneracionXML.Pais = countryXMLGeneration.Descripcion
                    payrollDocumentObject.Trabajador.LugarTrabajoPais = workerCountry.Descripcion

                    Dim report As New rptPayrollElectronicPaymentSupport()
                    report.DataSource = New List(Of NominaIndividual) From {payrollDocumentObject}
                    report.DataMember = ""

                    report.CreateDocument()

                    Using pdfStream As New MemoryStream()
                        report.ExportToPdf(pdfStream)
                        pdfStream.Seek(0, SeekOrigin.Begin)

                        Using sfd As New SaveFileDialog()
                            sfd.Filter = "PDF Files|*.pdf"
                            sfd.FileName = $"SoporteNominaElectronica-{consecutive}.pdf"
                            If sfd.ShowDialog() = DialogResult.OK Then
                                File.WriteAllBytes(sfd.FileName, pdfStream.ToArray())
                                MessageBox.Show("PDF guardado en: " & sfd.FileName, "Éxito",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End Using
                    End Using
                End If
            End Using
        Catch ex As Exception

        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#End Region

End Class
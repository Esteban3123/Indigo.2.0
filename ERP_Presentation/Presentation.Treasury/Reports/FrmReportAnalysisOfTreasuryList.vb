#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportAnalysisOfTreasuryList

#Region "Properties"
    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Caja"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Egresos"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrados (Sin Confirmar)"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        If INDDeDateStart.EditValue Is Nothing Or INDDeDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable
        If Me.ValidateControlsReports() = True Then
            If INDGleTypeReport.EditValue = 1 Then
                Dim filtroConsulta As String = "GetDate(IdCashReceipt.DocumentDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(IdCashReceipt.DocumentDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"

                If INDGleStatus.EditValue <> 4 Then
                    filtroConsulta &= "AND IdCashReceipt.Status = " & INDGleStatus.EditValue
                End If

                Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryPaymentMethodsXpo)(Nothing, filtroConsulta)

                Dim dt As New DataTable
                dt.Columns.Add("Código")
                dt.Columns.Add("Estado")
                dt.Columns.Add("Forma De Pago")

                Dim columValue As DataColumn = New DataColumn
                columValue.DataType = System.Type.GetType("System.Decimal")
                columValue.AllowDBNull = False
                columValue.Caption = "Valor"
                columValue.ColumnName = "Valor"
                dt.Columns.Add(columValue)

                For Each itemView In IndList
                    Dim paymentMethodsText As String = String.Empty
                    Select Case itemView.PaymentMethodTypes
                        Case 1
                            paymentMethodsText = "Efectivo"
                        Case 2
                            paymentMethodsText = "Cheque"
                        Case 3
                            paymentMethodsText = "Tarjeta"
                        Case 4
                            paymentMethodsText = "Consignacion"
                    End Select
                    Dim row As DataRow = dt.NewRow()
                    row.Item("Código") = itemView.IdCashReceipt.Code
                    row.Item("Estado") = itemView.IdCashReceipt.Status
                    row.Item("Forma De Pago") = paymentMethodsText
                    row.Item("Valor") = itemView.Value

                    dt.Rows.Add(row)
                Next
                AsyncLoader(False)
                If dt IsNot Nothing Then
                    Return dt
                Else
                    Return New DataTable
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDeDateStart.Focus()
                End If
            Else
                Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"

                If INDGleStatus.EditValue <> 4 Then
                    filtroConsulta &= "AND Status = " & INDGleStatus.EditValue
                End If

                Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionXpo)(Nothing, filtroConsulta)

                Dim dt As New DataTable
                dt.Columns.Add("Código")
                dt.Columns.Add("Estado")
                dt.Columns.Add("Código Caja / Banco")
                dt.Columns.Add("Nombre Caja / Banco")
                dt.Columns.Add("Numero")
                dt.Columns.Add("Fecha Consignación")

                Dim columValue As DataColumn = New DataColumn
                columValue.DataType = System.Type.GetType("System.Decimal")
                columValue.AllowDBNull = False
                columValue.Caption = "Impuesto X Mil"
                columValue.ColumnName = "Impuesto X Mil"
                dt.Columns.Add(columValue)

                For Each itemView In IndList
                    Dim codeCashBank As String
                    Dim nameCashBank As String
                    If itemView.IdCashRegister Is Nothing Then
                        codeCashBank = itemView.IdEntityBankAccount.Code
                        nameCashBank = itemView.IdEntityBankAccount.IdBank.Name
                    Else
                        codeCashBank = itemView.IdCashRegister.Code
                        nameCashBank = itemView.IdCashRegister.Name
                    End If
                    Dim numberCheckNote As String = String.Empty
                    Select Case itemView.PaymentMethod
                        Case 1
                            numberCheckNote = itemView.CheckNumber
                        Case Else
                            numberCheckNote = itemView.NoteNumber
                    End Select
                    Dim row As DataRow = dt.NewRow()
                    row.Item("Código") = itemView.Code
                    row.Item("Estado") = itemView.Status
                    row.Item("Código Caja / Banco") = codeCashBank
                    row.Item("Nombre Caja / Banco") = nameCashBank
                    row.Item("Numero") = numberCheckNote
                    row.Item("Fecha Consignación") = itemView.TransactionDate
                    row.Item("Impuesto X Mil") = itemView.TaxByMilValue

                    dt.Rows.Add(row)
                Next
                AsyncLoader(False)
                If dt IsNot Nothing Then
                    Return dt
                Else
                    Return New DataTable
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDeDateStart.Focus()
                End If

            End If

        End If

    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportAnalysisOfTreasuryList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleStatus.Properties.DataSource = FillingStatus

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleStatus.EditValue = 4
        Me.INDGleTypeReport.EditValue = 1

    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDcnBack_ClickBack() Handles INDcnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            If INDGleTypeReport.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptReportAnalysisOfTreasuryListCashReceipts
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      INDGleStatus.EditValue}
                INDDvDocumentViewer.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    AsyncLoader(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                    AsyncLoader(False)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDeDateStart.Focus()
                End If
            Else
                Dim reporte As New rptReportAnalysisOfTreasuryListVoucherTransaction
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      INDGleStatus.EditValue}
                INDDvDocumentViewer.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDeDateStart.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            INDGcExportExcell.DataSource = chargueDatasource()
            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

#End Region

End Class
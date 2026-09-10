#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports System.Text

Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports Presentation.Payroll.MVP
Imports System.IO
#End Region

Public Class FrmReportPlaneTreasury
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Private lista As List(Of PayrollViewReportPlaneTreasuryReportXpo) '= New List(Of InventoryTransferOrderDetailReportXpo)
    Private listaRetroactive As List(Of PayrollVPlaneTreasuryRetroactiveReportXpo)
    Private listIncentivePayment As List(Of PayrollVPlaneTreasuryIncentivePaymentReportXpo)

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    'Variable que se devuelve para generar el arvhivo plano
    Dim result As New StringBuilder()

    Dim dt As New DataTable

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingRetroactive As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingRetroactive As List(Of Tuple(Of Integer, String))
        Get
            If _FillingRetroactive Is Nothing Then
                _FillingRetroactive = New List(Of Tuple(Of Integer, String))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(1, "Nómina"))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(2, "Retroactivo"))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(3, "Primas"))
            End If
            Return _FillingRetroactive
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Tupla de Datos de Periodo
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingPeriod As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingPeriod As List(Of Tuple(Of Integer, String))
        Get
            If _FillingPeriod Is Nothing Then
                _FillingPeriod = New List(Of Tuple(Of Integer, String))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(1, "1"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(2, "2"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _FillingPeriod
        End Get
    End Property
#End Region

#Region "Method"

    Private Function hargeDatasource() As StringBuilder
        If INDGleRetroactive.EditValue = 1 Then
            'Se obtiene el listado por medio de un Xpo
            Dim filtroConsulta As String = "PayrollDateLiquidated >= '" & Format(INDDateEditStart.EditValue, "yyyy-MM-dd") & "' AND PayrollDateLiquidated <= '" & Format(INDDateEditEnd.EditValue, "yyyy-MM-dd") & "'"

            If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue IsNot Nothing Then
                filtroConsulta &= " AND Code >= '" & INDSleGroupStart.EditValue & "' AND Code <= '" & INDSleGroupEnd.EditValue & "'"
            End If

            lista = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollViewReportPlaneTreasuryReportXpo)(Nothing, filtroConsulta)

            ''Variable que se devuelve para generar el arvhivo plano
            'Dim result As New StringBuilder()

            'Se arma la cabecera del archivo plano   
            lineHead &= Utils.StringPad("Identificación Del Empleado", 25, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Nombre Del Empleado", 35, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Valor Neto a Pagar ", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Cuenta a Consignar", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Banco a Consignar", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            'lineHead &= Utils.StringPad(CDate(DateEnd), 10, " ", Utils.PadType.STR_PAD_RIGHT)
            result.Append(lineHead)

            If lista.Count > 0 Then
                For Each item In lista
                    Dim Nit As String = item.Nit 'Codigo cuenta contable
                    Dim Name As String = item.Name '
                    Dim TotalPaid As String = item.TotalPaid 'e
                    Dim BankAccountNumber As String = item.BankAccountNumber '
                    Dim NameBank As String = item.NameBank '

                    'Cuenta que se imprime en el archivo plano
                    Dim lineDet As String = vbCrLf

                    lineDet &= Utils.StringPad(Nit, 25, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(Name, 35, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(TotalPaid, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(BankAccountNumber, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(NameBank, 21, " ", Utils.PadType.STR_PAD_RIGHT)

                    result.Append(lineDet)
                Next
            End If
        ElseIf INDGleRetroactive.EditValue = 2 Then
            'Se obtiene el listado por medio de un Xpo
            Dim filtroConsulta As String = "InitialDateRetroactive >= '" & Format(INDDateEditStart.EditValue, "yyyy-MM-dd") & "' AND InitialDateRetroactive <= '" & Format(INDDateEditEnd.EditValue, "yyyy-MM-dd") & "'"

            If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue IsNot Nothing Then
                filtroConsulta &= " AND CodeGroup >= '" & INDSleGroupStart.EditValue & "' AND CodeGroup <= '" & INDSleGroupEnd.EditValue & "'"
            End If

            listaRetroactive = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVPlaneTreasuryRetroactiveReportXpo)(Nothing, filtroConsulta)

            ''Variable que se devuelve para generar el arvhivo plano
            'Dim result As New StringBuilder()

            'Se arma la cabecera del archivo plano   
            lineHead &= Utils.StringPad("Identificación Del Empleado", 25, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Nombre Del Empleado", 35, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Valor Neto a Pagar ", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Cuenta a Consignar", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Banco a Consignar", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            'lineHead &= Utils.StringPad(CDate(DateEnd), 10, " ", Utils.PadType.STR_PAD_RIGHT)
            result.Append(lineHead)

            If listaRetroactive.Count > 0 Then
                For Each item In listaRetroactive
                    Dim Nit As String = item.Nit 'Codigo cuenta contable
                    Dim Name As String = item.ThirdPartyName
                    Dim TotalPaid As String = item.TotalRetroactiveValue
                    Dim BankAccountNumber As String = item.BankAccountNumber
                    Dim NameBank As String = item.BankName '

                    'Cuenta que se imprime en el archivo plano
                    Dim lineDet As String = vbCrLf

                    lineDet &= Utils.StringPad(Nit, 25, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(Name, 35, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(TotalPaid, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(BankAccountNumber, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(NameBank, 21, " ", Utils.PadType.STR_PAD_RIGHT)

                    result.Append(lineDet)
                Next
            End If
        ElseIf INDGleRetroactive.EditValue = 3 Then
            'Se obtiene el listado por medio de un Xpo
            Dim filtroConsulta As String = "PeriodInitialDate >= '" & Format(INDDateEditStart.EditValue, "yyyy-MM-dd") & "' AND PeriodEndDate <= '" & Format(INDDateEditEnd.EditValue, "yyyy-MM-dd") & "'"

            If INDGlePeriod.EditValue <> 3 Then
                filtroConsulta &= " AND Period = " & INDGlePeriod.EditValue
            End If

            If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue IsNot Nothing Then
                filtroConsulta &= " AND GroupCode >= '" & INDSleGroupStart.EditValue & "' AND GroupCode <= '" & INDSleGroupEnd.EditValue & "'"
            End If

            listIncentivePayment = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVPlaneTreasuryIncentivePaymentReportXpo)(Nothing, filtroConsulta)

            ''Variable que se devuelve para generar el arvhivo plano
            'Dim result As New StringBuilder()

            'Se arma la cabecera del archivo plano   
            lineHead &= Utils.StringPad("Identificación Del Empleado", 25, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Nombre Del Empleado", 35, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Valor Neto a Pagar ", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Cuenta a Consignar", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("Banco a Consignar", 21, " ", Utils.PadType.STR_PAD_RIGHT)
            'lineHead &= Utils.StringPad(CDate(DateEnd), 10, " ", Utils.PadType.STR_PAD_RIGHT)
            result.Append(lineHead)

            If listIncentivePayment.Count > 0 Then
                For Each item In listIncentivePayment
                    Dim Nit As String = item.Nit 'Codigo cuenta contable
                    Dim Name As String = item.ThirdName
                    Dim TotalPaid As String = item.TotalPaid
                    Dim BankAccountNumber As String = item.BankAccountNumber
                    Dim NameBank As String = item.BankName

                    'Cuenta que se imprime en el archivo plano
                    Dim lineDet As String = vbCrLf

                    lineDet &= Utils.StringPad(Nit, 25, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(Name, 35, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(TotalPaid, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(BankAccountNumber, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(NameBank, 21, " ", Utils.PadType.STR_PAD_RIGHT)

                    result.Append(lineDet)
                Next
            End If
        End If
        Return result
    End Function

    Private Function chargueDatasource() As DataTable ' EXCEL
        If INDGleRetroactive.EditValue = 1 Then

            'Se obtiene el listado por medio de un Xpo
            Dim filtroConsulta As String = "PayrollDateLiquidated >= '" & Format(INDDateEditStart.EditValue, "yyyy-MM-dd") & "' AND PayrollDateLiquidated <= '" & Format(INDDateEditEnd.EditValue, "yyyy-MM-dd") & "'"

            If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue IsNot Nothing Then
                filtroConsulta &= " AND Code >= '" & INDSleGroupStart.EditValue & "' AND Code <= '" & INDSleGroupEnd.EditValue & "'"
            End If

            lista = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollViewReportPlaneTreasuryReportXpo)(Nothing, filtroConsulta)

            'Dim dt As New DataTable
            dt.Columns.Add("Num Identificación")
            dt.Columns.Add("Nombre Del Empleado")

            Dim columValue As DataColumn = New DataColumn
            columValue.DataType = System.Type.GetType("System.Decimal")
            columValue.AllowDBNull = False
            columValue.Caption = "Neto A Pagar"
            columValue.ColumnName = "Neto A Pagar"
            dt.Columns.Add(columValue)

            dt.Columns.Add("Cuenta a Consignar")
            dt.Columns.Add("Banco a Consignar")
            dt.Columns.Add("Código grupo")
            dt.Columns.Add("Nombre Grupo")
            dt.Columns.Add("Periodo de Liquidación")
            dt.Columns.Add("Fecha de la Nómina Liquidada")



            For Each itemView In lista

                Dim row As DataRow = dt.NewRow()
                row.Item("Num Identificación") = itemView.Nit
                row.Item("Nombre Del Empleado") = itemView.Name
                row.Item("Neto A Pagar") = itemView.TotalPaid
                row.Item("Cuenta a Consignar") = itemView.BankAccountNumber
                row.Item("Banco a Consignar") = itemView.NameBank
                row.Item("Código grupo") = itemView.Code
                row.Item("Nombre Grupo") = itemView.NameGroup
                row.Item("Periodo de Liquidación") = itemView.LiquidationPeriod
                row.Item("Fecha de la Nómina Liquidada") = itemView.PayrollDateLiquidated

                dt.Rows.Add(row)
            Next
        ElseIf INDGleRetroactive.EditValue = 2 Then

            'Se obtiene el listado por medio de un Xpo
            Dim filtroConsulta As String = "InitialDateRetroactive >= '" & Format(INDDateEditStart.EditValue, "yyyy-MM-dd") & "' AND InitialDateRetroactive <= '" & Format(INDDateEditEnd.EditValue, "yyyy-MM-dd") & "'"

            If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue IsNot Nothing Then
                filtroConsulta &= " AND CodeGroup >= '" & INDSleGroupStart.EditValue & "' AND CodeGroup <= '" & INDSleGroupEnd.EditValue & "'"
            End If

            listaRetroactive = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVPlaneTreasuryRetroactiveReportXpo)(Nothing, filtroConsulta)

            'Dim dt As New DataTable
            dt.Columns.Add("Num Identificación")
            dt.Columns.Add("Nombre Del Empleado")

            Dim columValue As DataColumn = New DataColumn
            columValue.DataType = System.Type.GetType("System.Decimal")
            columValue.AllowDBNull = False
            columValue.Caption = "Neto A Pagar"
            columValue.ColumnName = "Neto A Pagar"
            dt.Columns.Add(columValue)

            dt.Columns.Add("Cuenta a Consignar")
            dt.Columns.Add("Banco a Consignar")
            dt.Columns.Add("Código grupo")
            dt.Columns.Add("Nombre Grupo")
            dt.Columns.Add("Periodo de Liquidación")



            For Each itemView In listaRetroactive

                Dim row As DataRow = dt.NewRow()
                row.Item("Num Identificación") = itemView.Nit
                row.Item("Nombre Del Empleado") = itemView.ThirdPartyName
                row.Item("Neto A Pagar") = itemView.TotalRetroactiveValue
                row.Item("Cuenta a Consignar") = itemView.BankAccountNumber
                row.Item("Banco a Consignar") = itemView.BankName
                row.Item("Código grupo") = itemView.CodeGroup
                row.Item("Nombre Grupo") = itemView.NameGroup
                row.Item("Periodo de Liquidación") = itemView.InitialDateRetroactive

                dt.Rows.Add(row)
            Next
        ElseIf INDGleRetroactive.EditValue = 3 Then

            'Se obtiene el listado por medio de un Xpo
            Dim filtroConsulta As String = "PeriodInitialDate >= '" & Format(INDDateEditStart.EditValue, "yyyy-MM-dd") & "' AND PeriodEndDate <= '" & Format(INDDateEditEnd.EditValue, "yyyy-MM-dd") & "'"

            If INDGlePeriod.EditValue <> 3 Then
                filtroConsulta &= " AND Period = " & INDGlePeriod.EditValue
            End If

            If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue IsNot Nothing Then
                filtroConsulta &= " AND GroupCode >= '" & INDSleGroupStart.EditValue & "' AND GroupCode <= '" & INDSleGroupEnd.EditValue & "'"
            End If

            listIncentivePayment = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVPlaneTreasuryIncentivePaymentReportXpo)(Nothing, filtroConsulta)

            'Dim dt As New DataTable
            dt.Columns.Add("Num Identificación")
            dt.Columns.Add("Nombre Del Empleado")

            Dim columValue As DataColumn = New DataColumn
            columValue.DataType = System.Type.GetType("System.Decimal")
            columValue.AllowDBNull = False
            columValue.Caption = "Neto A Pagar"
            columValue.ColumnName = "Neto A Pagar"
            dt.Columns.Add(columValue)

            dt.Columns.Add("Cuenta a Consignar")
            dt.Columns.Add("Banco a Consignar")
            dt.Columns.Add("Código grupo")
            dt.Columns.Add("Nombre Grupo")
            dt.Columns.Add("Periodo de Liquidación")


            If listIncentivePayment.Count > 0 Then
                For Each itemView In listIncentivePayment

                    Dim row As DataRow = dt.NewRow()
                    row.Item("Num Identificación") = itemView.Nit
                    row.Item("Nombre Del Empleado") = itemView.ThirdName
                    row.Item("Neto A Pagar") = itemView.TotalPaid
                    row.Item("Cuenta a Consignar") = itemView.BankAccountNumber
                    row.Item("Banco a Consignar") = itemView.BankName
                    row.Item("Código grupo") = itemView.GroupCode
                    row.Item("Nombre Grupo") = itemView.GroupName
                    row.Item("Periodo de Liquidación") = itemView.Period

                    dt.Rows.Add(row)
                Next
            End If
        End If
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateEditStart.Focus()
        End If
    End Function


    Private Sub INDSbGenerateExcelFile_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcelFile.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Me.INDGcExportExcel.DataSource = chargueDatasource()
            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub
    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
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

    Private Sub INDSbGenerateFlatFile_Click(sender As Object, e As EventArgs) Handles INDSbGenerateFlatFile.Click
        If Me.ValidateControlsReports = True Then
            Dim DataArchive As StringBuilder

            DataArchive = hargeDatasource()

            DialogGenerateFile(DataArchive)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(content As StringBuilder)
        Try
            Dim save As System.Windows.Forms.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            save.Filter = "Texto|*.txt"
            save.Title = "ARCHIVO PLANO TESORERÍA " & INDDateEditStart.EditValue & " Del " & Year(CDate(INDDateEnd))
            save.FileName = "ARCHIVO PLANO TESORERÍA " & INDDateEditStart.EditValue & " Del " & Year(CDate(INDDateEnd))
            If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim file = save.OpenFile()
                Dim streamWrite As New StreamWriter(file)
                streamWrite.Write(content)
                streamWrite.Flush()
                streamWrite.Close()
                If MessageIndigo.Show("Desea Abrir el Archivo", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Process.Start(save.FileName)
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un Error Creando el Archivo"
        End Try
    End Sub


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

    '''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        If INDDateEditStart.EditValue Is Nothing Or INDDateEditEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateEditStart.Focus()
            Validations = False
        ElseIf Me.INDDateEditStart.EditValue > INDDateEditEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateEditStart.Focus()
            Validations = False
        End If

        'Valida Grupo
        If INDSleGroupStart.EditValue Is Nothing And INDSleGroupEnd.EditValue IsNot Nothing Or INDSleGroupEnd.EditValue Is Nothing And INDSleGroupStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
#End Region


#Region "Event"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoGroup = Nothing
        lista = Nothing
        listaRetroactive = Nothing
        listIncentivePayment = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerateReport
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>    
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        If Me.ValidateControlsReports Then
            If Me.INDGleRetroactive.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptPlaneTreasury()

                reporte.ParametrosReporte = {INDDateEditStart.EditValue, INDDateEditEnd.EditValue,
                                             INDSleGroupStart.EditValue, INDSleGroupEnd.EditValue}

                INDDvReport.DocumentSource = reporte

                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditStart.Focus()
                End If
            ElseIf Me.INDGleRetroactive.EditValue = 2 Then
                AsyncLoader(True)
                Dim reporte As New rptPlaneTreasuryRetroactive()

                reporte.ParametrosReporte = {INDDateEditStart.EditValue, INDDateEditEnd.EditValue,
                                             INDSleGroupStart.EditValue, INDSleGroupEnd.EditValue}

                INDDvReport.DocumentSource = reporte

                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditStart.Focus()
                End If
            ElseIf Me.INDGleRetroactive.EditValue = 3 Then
                AsyncLoader(True)
                Dim reporte As New rptPlaneTreasuryIncentivePayment()

                reporte.ParametrosReporte = {INDDateEditStart.EditValue, INDDateEditEnd.EditValue, INDGlePeriod.EditValue,
                                             INDSleGroupStart.EditValue, INDSleGroupEnd.EditValue}

                INDDvReport.DocumentSource = reporte

                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditStart.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDateEditStart.Focus()
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportPlaneTreasury_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleRetroactive.Properties.DataSource = FillingRetroactive
        Me.INDGlePeriod.Properties.DataSource = FillingPeriod
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleRetroactive.EditValue = 1
        Me.INDGlePeriod.EditValue = 3
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub

    ''' <summary>
    ''' Oculta El filtro de periodo para los preoceos que no lo utilizan
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleRetroactive_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRetroactive.EditValueChanged
        If INDGleRetroactive.EditValue = 1 Then
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGlePeriod.EditValue = Nothing
        ElseIf INDGleRetroactive.EditValue = 2 Then
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGlePeriod.EditValue = Nothing
        ElseIf INDGleRetroactive.EditValue = 3 Then
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGlePeriod.EditValue = 3
            Me.INDDateEditStart.Focus()
        End If
    End Sub

    Private Sub FrmReportPayrollTotal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.ToolBar.Visible = False
        'Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetDocumentType
        'Me.INDSleVoucherTypeEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetDocumentType
        'Me.INDSleUnitStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        'Me.INDSleUnitEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
    End Sub
#End Region
End Class
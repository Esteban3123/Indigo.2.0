#Region "Imports"
Imports Presentation.Reporter
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Presentation.Payroll.MVP
Imports Infrastructure.Data.Xpo.PayrollRepository
#End Region

Public Class FrmReportIncomeTax

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    '' <summary>
    '' Referencia al modelo de PUC
    '' </summary>
    Private _PayrollModelGroup As MGroups

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoResponsible As XPInstantFeedbackSource
    Public Property ProoftCloseXpoClassification As XPInstantFeedbackSource
    Public Property ProoftCloseXpoStatusAsset As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEquipmentType As XPInstantFeedbackSource
    Public Property ProoftCloseXpoItem As XPInstantFeedbackSource
    Public Property ProoftCloseXpoPlate As XPInstantFeedbackSource
    Public Property ProoftCloseXpoLocation As XPCollection

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' Propiedad para almacenar el dia de inicio parametrizado por el usuario
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property StartDate As DateTime
        Get
            Return Convert.ToDateTime(INDDateStart.EditValue)
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para almacenar la fecha final a consultar parametrizada por el usuario.
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property EndDate As DateTime
        Get
            Return Convert.ToDateTime(INDDateEnd.EditValue)
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida si la fecha inicial es mayor a la inicial
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            If StartDate > EndDate Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                Me.INDDateEnd.Focus()
                Validations = False
            End If

        End If

        Return Validations
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = Nothing

        'filtro por fechas
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            filtroConsulta = "PayrollDateLiquidated >= #" & Format(StartDate, "yyyy-MM-dd") & "# AND PayrollDateLiquidated <= #" & Format(EndDate, "yyyy-MM-dd") & "#"
        End If

        Dim list As List(Of PayrollViewReportIncomeTaxXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollViewReportIncomeTaxXpo)(Nothing, filtroConsulta)

        Dim groupList = list.GroupBy(Function(x) x.IdentificationNumber).
                            Select(Function(g)
                                       Dim group = g.First()

                                       ' Cálculo personalizado de impuesto retenido
                                       Dim rentMinusWithholding = g.Sum(Function(x) x.ConceptRentPaid) - g.Sum(Function(x) x.ConceptWithholding)
                                       Dim totalBase = g.Sum(Function(x) x.TotalBaseRetention)

                                       Return New PayrollViewReportIncomeTaxXpo With {
                                           .Id = group.Id,
                                           .IdentificationNumber = group.IdentificationNumber,
                                           .EmployeeName = group.EmployeeName,
                                           .TotalBaseRetention = If(totalBase > 0, totalBase, Nothing),
                                           .ConceptRentPaid = If(rentMinusWithholding > 0, rentMinusWithholding, Nothing),
                                           .ConceptWithholding = g.Sum(Function(x) x.ConceptWithholding),
                                           .SupplementaryPension = g.Sum(Function(x) x.SupplementaryPension),
                                           .TaxCredits = g.Sum(Function(x) x.TaxCredits),
                                           .Code = "SL"
                                       }
                                   End Function).OrderBy(Function(x) x.IdentificationNumber).ThenBy(Function(x) x.EmployeeName).ToList()

        Dim dt As New DataTable
        'Restricciones colocadas en el requerimiento
        Dim cedulaColumn As New DataColumn("CEDULA", GetType(String))
        cedulaColumn.MaxLength = 200
        dt.Columns.Add(cedulaColumn)

        Dim nombreColumn As New DataColumn("NOMBRE", GetType(String))
        nombreColumn.MaxLength = 203
        dt.Columns.Add(nombreColumn)

        dt.Columns.Add("BASE RETENCIÓN", GetType(Decimal))
        dt.Columns.Add("IMPUESTO RETENIDO", GetType(Decimal))
        dt.Columns.Add("DEDUCCIÓN PENSIÓN COMPLEMENTARIA", GetType(Decimal))
        dt.Columns.Add("CRÉDITOS FISCALES", GetType(Decimal))
        dt.Columns.Add("CÓDIGO", GetType(String))


        For Each item In groupList
            Dim row As DataRow = dt.NewRow()
            row("CEDULA") = item.IdentificationNumber
            row("NOMBRE") = item.EmployeeName?.Trim()

            If item.TotalBaseRetention > 0 Then
                row("BASE RETENCIÓN") = item.TotalBaseRetention
            End If

            If item.ConceptRentPaid > 0 Then
                row("IMPUESTO RETENIDO") = item.ConceptRentPaid
            End If

            row("DEDUCCIÓN PENSIÓN COMPLEMENTARIA") = item.SupplementaryPension
            row("CRÉDITOS FISCALES") = item.TaxCredits
            row("CÓDIGO") = item.Code

            dt.Rows.Add(row)
        Next
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

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

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoResponsible = Nothing
        ProoftCloseXpoClassification = Nothing
        ProoftCloseXpoStatusAsset = Nothing
        ProoftCloseXpoEquipmentType = Nothing
        ProoftCloseXpoItem = Nothing
        ProoftCloseXpoPlate = Nothing
        ProoftCloseXpoLocation = Nothing
        List = Nothing
        _FillingGroupBy = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
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
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Not ValidateControlsReports() Then Exit Sub
        Try
            AsyncLoader(True)

            Dim reporte As New rptIncomeTax
            reporte.ParametrosReporte = New Object() {StartDate,
                                                      EndDate,
                                                      Me.BarraBotones.OperatingUnit.Id}

            INDDvViewReport.DocumentSource = reporte
            reporte.CargarDataSource()

            Dim dataExist As Boolean = TryCast(reporte.DataSource, ICollection)?.Count > 0

            If dataExist Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message

        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDateStart.Focus()
    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportIncomeTax_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._PayrollModelGroup = New MGroups(Me.Tag)
        Me._pucModel = New MCommon(Me.Tag)
    End Sub
#End Region

End Class
#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
Imports Presentation.Cost.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportActivityCosts

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para el cargue de los centros de producción
    ''' </summary>
    Public Property ProoftCloseXpoProductionCenter As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para obtener los datos del reporte o excel segun los parametros generales de costo
    ''' </summary>
    Dim AverageStandarCostActivity As Boolean? = Nothing

    ''' <summary>
    ''' Filtros a enviar al procedimiento con el fin de obtener los datos
    ''' </summary>
    Dim filters As New Dictionary(Of String, String)

    ''' <summary>
    ''' Propiedad para obtener el año parametrizado en el control
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property YearCost As Integer
        Get
            Return INDcdnDateStart.GetYear
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para obtener el mes parametrizado en el control
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MonthCost As Integer
        Get
            Return INDcdnDateStart.GetMonth
        End Get
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleProductionCenterStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductionCenterStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductionCenterStart.QueryPopUp
        If INDSleProductionCenterStart.Datasource Is Nothing Then
            LoadXpoProductionCenterStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleProductionCenterEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductionCenterEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductionCenterEnd.QueryPopUp
        If INDSleProductionCenterEnd.Datasource Is Nothing Then
            LoadXpoProductionCenterEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await GetAverageStandarCostActivity()
                Dim reporte = If(AverageStandarCostActivity,
                    New rptReportActivityCosts,
                    New rptReportActivityNoStandardCost
                    )
                reporte.ParametrosReporte = New Object() {filters}
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()

                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDcdnDateStart.Focus()
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await GetAverageStandarCostActivity()
                Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetListReportActivityCostsAsync(filters, Me.IndigoSessionValues)
                If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dtReportActivityCosts As DataTable = ds.Tables("ReportActivityCosts")
                    chargueDatasource(dtReportActivityCosts)

                    If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                        generateExcel()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
                Me.INDcdnDateStart.Focus()
            End Try
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoProductionCenter = Nothing
    End Sub

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

        'validaciones controles de centros producción
        If INDSleProductionCenterStart.EditValue IsNot Nothing And INDSleProductionCenterEnd.EditValue Is Nothing Or INDSleProductionCenterStart.EditValue Is Nothing And INDSleProductionCenterEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleProductionCenterStart.Focus()
            Validations = False
        ElseIf INDSleProductionCenterStart.EditValue > INDSleProductionCenterEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleProductionCenterStart.Focus()
            Validations = False
        End If

        If Validations Then
            filters = New Dictionary(Of String, String)
            filters.Add("Year", YearCost)
            filters.Add("Month", MonthCost)
            filters.Add("ProductionCenterStart", INDSleProductionCenterStart.EditValue)
            filters.Add("ProductionCenterEnd", INDSleProductionCenterEnd.EditValue)
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' Obtiene el valor si se encuentra parametrizado el campo Costo Estándar Promedio por Actividades
    ''' en el formulario parametros generales de costo segun el mes y año del reporte
    ''' </summary>
    Private Async Function GetAverageStandarCostActivity() As Task
        Using modelCost As New MCostSetting(Me.Tag)
            Dim costSetting = Await modelCost.GetCostSettingAsync()
            If costSetting IsNot Nothing Then
                If costSetting.Year = YearCost AndAlso costSetting.Month = MonthCost Then
                    AverageStandarCostActivity = costSetting.AverageStandardCostActivity
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSupplierStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductionCenterStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoProductionCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostProductionCenter)
            INDSleProductionCenterStart.Datasource = ProoftCloseXpoProductionCenter
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSupplierEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductionCenterEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoProductionCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostProductionCenter)
            INDSleProductionCenterEnd.Datasource = ProoftCloseXpoProductionCenter
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource(ByVal dtReportPaymentsByAge As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código Centro de Producción")
        dt.Columns.Add("Nombre Centro de Producción")
        dt.Columns.Add("Tipo Centro de Producción")
        dt.Columns.Add("Código Servicio")
        dt.Columns.Add("Nombre Servicio")
        dt.Columns.Add("Código Actividad")
        dt.Columns.Add("Nombre Actividad")

        dt.Columns.Add("Gastos Directos", GetType(Decimal))
        dt.Columns.Add("Gastos Variables", GetType(Decimal))
        dt.Columns.Add("Mano de Obra", GetType(Decimal))
        dt.Columns.Add("Activos Fijos", GetType(Decimal))
        dt.Columns.Add("Dispensación", GetType(Decimal))
        dt.Columns.Add("Consumo", GetType(Decimal))
        dt.Columns.Add("Total Costo", GetType(Decimal))

        dt.Columns.Add("Unidades Facturadas", GetType(Integer))
        dt.Columns.Add("Costo Unitario por Actividad - Médodo Absorvente", GetType(Decimal))
        dt.Columns.Add("Costo Unitario por Actividad", GetType(Decimal))
        dt.Columns.Add("Costo Total Actividad", GetType(Decimal))
        dt.Columns.Add("Vr Promedio de Venta Unitario", GetType(Decimal))

        If AverageStandarCostActivity Then
            dt.Columns.Add("Costo Estándar Promedio Unitario", GetType(Decimal))
            dt.Columns.Add("Vr Desviación Costo Estándar", GetType(Decimal))
            dt.Columns.Add("% Desviación", GetType(Decimal))
        End If

        dt.Columns.Add("Resultado Actividad", GetType(Decimal))
        dt.Columns.Add("% Resultado Actividad", GetType(Decimal))

        For Each item In dtReportPaymentsByAge.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código Centro de Producción") = item("CostProductionCenterCode")
            row.Item("Nombre Centro de Producción") = item("CostProductionCenterName")
            row.Item("Tipo Centro de Producción") = item("CenterTypeName")
            row.Item("Código Servicio") = item("CUPSEntityCode")
            row.Item("Nombre Servicio") = item("CUPSEntityDescription")
            row.Item("Código Actividad") = item("CostActivityCode")
            row.Item("Nombre Actividad") = item("CostActivityDescription")

            row.Item("Gastos Directos") = item("DirectCostDistribution")
            row.Item("Gastos Variables") = item("AutoCostDistribution")
            row.Item("Mano de Obra") = item("ManPowerDistribution")
            row.Item("Activos Fijos") = item("FixedAssetDistribution")
            row.Item("Dispensación") = item("DispensingDistribution")
            row.Item("Consumo") = item("TransferDistribution")
            row.Item("Total Costo") = item("Distribution")

            row.Item("Unidades Facturadas") = item("Quantity")
            row.Item("Costo Unitario por Actividad - Médodo Absorvente") = item("ActivityUnitValue")
            row.Item("Costo Unitario por Actividad") = item("CalculatedActivityUnitValue")
            row.Item("Costo Total Actividad") = item("EstimatedValue")
            row.Item("Vr Promedio de Venta Unitario") = item("AverageUnitSales")

            If AverageStandarCostActivity Then
                row.Item("Costo Estándar Promedio Unitario") = item("StandarCostValue")
                row.Item("Vr Desviación Costo Estándar") = item("StandarCostDeviation")
                row.Item("% Desviación") = item("CostDeviationPercentage")
            End If

            row.Item("Resultado Actividad") = item("ActivityResult")
            row.Item("% Resultado Actividad") = item("ActivityResultPercentage")

            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
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

End Class
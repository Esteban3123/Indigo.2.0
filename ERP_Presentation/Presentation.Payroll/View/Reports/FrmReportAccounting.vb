#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

Imports Domain.Entities
#End Region

Public Class FrmReportAccounting
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployeeType As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource


    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private List As List(Of PayrollLiquidationDetail)

    Private ListRetroactive As List(Of PayrollViewReportAccountingRetroactiveXpo)

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Estado de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Devengados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Deducidos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    ''' <summary>
    ''' Tipo de reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingProcess As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingProcess As List(Of Tuple(Of Integer, String))
        Get
            If _FillingProcess Is Nothing Then
                _FillingProcess = New List(Of Tuple(Of Integer, String))
                _FillingProcess.Add(New Tuple(Of Integer, String)(1, "Contabilidad Nomina"))
                _FillingProcess.Add(New Tuple(Of Integer, String)(2, "Retroactivo"))
            End If
            Return _FillingProcess
        End Get
    End Property
#End Region

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateStart.Focus()
            Validations = False
        End If


        Return Validations
    End Function

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
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            If INDGleProcess.EditValue = 1 Then
                Me.INDGcExportExcel.DataSource = chargueDatasource()
            Else
                Me.INDGcExportExcel.DataSource = chargueDatasourceRetroactive()
            End If

            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub


    Private Function chargueDatasource() As DataTable
        Dim filtroConsulta As String = "PayrollId.PayrollDateLiquidated >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# And PayrollId.PayrollDateLiquidated <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'filtro por estado
        If INDGleTypeConcept.EditValue IsNot Nothing Then
            filtroConsulta &= " AND ConceptId.ConceptType = " & INDGleTypeConcept.EditValue
        End If

        'filtro por empleado
        If INDSleEmployeeType.EditValue IsNot Nothing Then
            filtroConsulta &= " AND PayrollId.EmployeeId.EmployeeTypeId.Id = " & INDSleEmployeeType.EditValue
        End If


        List = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollLiquidationDetail)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Documento")
        dt.Columns.Add("Nombre Empleado")
        dt.Columns.Add("Fecha Liquidación")
        dt.Columns.Add("Codigo Unidad Funcional")
        dt.Columns.Add("Nombre Unidad Funcional")
        dt.Columns.Add("Codigo Centro Costo")
        dt.Columns.Add("Centro de Costo")
        dt.Columns.Add("Salario Básico")
        dt.Columns.Add("Código Concepto")
        dt.Columns.Add("Concepto")
        dt.Columns.Add("Fondo Salud")
        dt.Columns.Add("Fondo Pensión")
        dt.Columns.Add("Aporte Sena")
        dt.Columns.Add("Aporte Caja Compensación")
        dt.Columns.Add("Aporte ICBF")
        dt.Columns.Add("Aporte Salud Patrono")
        dt.Columns.Add("Aporte Salud Empleado")
        dt.Columns.Add("Aporte Pensión Patrono")
        dt.Columns.Add("Aporte Pensión Empleado")
        dt.Columns.Add("Provisión Cesantías")
        dt.Columns.Add("Provisión Intereses Cesantías")
        dt.Columns.Add("Provisión Primas")
        dt.Columns.Add("Provisión Vacaciones")

        For Each itemView In List
            Dim row As DataRow = dt.NewRow()
            row.Item("Documento") = itemView.PayrollId.EmployeeId.ThirdPartyId.Nit
            row.Item("Nombre Empleado") = itemView.PayrollId.EmployeeId.ThirdPartyId.Name
            row.Item("Fecha Liquidación") = itemView.PayrollId.PayrollDateLiquidated
            row.Item("Codigo Unidad Funcional") = itemView.PayrollId.ContractId.FunctionalUnitId.Code
            row.Item("Nombre Unidad Funcional") = itemView.PayrollId.ContractId.FunctionalUnitId.Name
            row.Item("Codigo Centro Costo") = itemView.PayrollId.CostCenterId.Code
            row.Item("Centro de Costo") = itemView.PayrollId.CostCenterId.Name
            row.Item("Salario Básico") = itemView.PayrollId.ContractId.BasicSalary
            row.Item("Código Concepto") = itemView.ConceptId.Code
            row.Item("Concepto") = itemView.ConceptId.Name
            row.Item("Fondo Salud") = itemView.PayrollId.HealthFundId.Code & " - " & itemView.PayrollId.HealthFundId.Name
            'row.Item("Fondo Pensión") = itemView.PayrollId.PensionFundId.Code & " -" & itemView.PayrollId.PensionFundId.Name
            'Se coloca esta validacion para que los que sean pensionados o practicantes no llegue null el fondo de pensión al general el excel 
            If itemView.PayrollId.PensionFundId IsNot Nothing Then
                row.Item("Fondo Pensión") = itemView.PayrollId.PensionFundId.Code & " -" & itemView.PayrollId.PensionFundId.Name
            End If
            row.Item("Aporte Sena") = itemView.PayrollId.SenaContributionValue
            row.Item("Aporte Caja Compensación") = itemView.PayrollId.FamilyCompensationFundContributionValue
            row.Item("Aporte ICBF") = itemView.PayrollId.ICBFContributionValue
            row.Item("Aporte Salud Patrono") = itemView.PayrollId.EmployerHealthContributionValue
            row.Item("Aporte Salud Empleado") = itemView.PayrollId.EmployeeHealthContributionValue
            row.Item("Aporte Pensión Patrono") = itemView.PayrollId.EmployerPensionContributionValue
            row.Item("Aporte Pensión Empleado") = itemView.PayrollId.PensionContributionValue
            row.Item("Provisión Cesantías") = itemView.PayrollId.UnemploymentAccumulated
            row.Item("Provisión Intereses Cesantías") = itemView.PayrollId.ProvisionInterestsUnemployment
            row.Item("Provisión Primas") = itemView.PayrollId.ProvisionIncentive
            row.Item("Provisión Vacaciones") = itemView.PayrollId.ProvisionVacation

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

    Private Function chargueDatasourceRetroactive() As DataTable
        Dim filtroConsulta As String = "InitialDateRetroactive >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# And InitialDateRetroactive <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'filtro por estado
        If INDGleTypeConcept.EditValue IsNot Nothing Then
            filtroConsulta &= " AND TipoConcepto = " & INDGleTypeConcept.EditValue
        End If

        'filtro por empleado
        If INDSleEmployeeType.EditValue IsNot Nothing Then
            filtroConsulta &= " AND IdTipoEmpleado = " & INDSleEmployeeType.EditValue
        End If

        ListRetroactive = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollViewReportAccountingRetroactiveXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Documento")
        dt.Columns.Add("Nombre Empleado")
        dt.Columns.Add("Fecha Liquidación")
        dt.Columns.Add("Nombre Unidad Funcional")
        dt.Columns.Add("Codigo Centro Costo")
        dt.Columns.Add("Centro de Costo")
        dt.Columns.Add("Salario Básico")
        dt.Columns.Add("Concepto")
        dt.Columns.Add("Fondo Salud")
        dt.Columns.Add("Fondo Pensión")
        dt.Columns.Add("Aporte Sena")
        dt.Columns.Add("Aporte Caja Compensación")
        dt.Columns.Add("Aporte ICBF")
        dt.Columns.Add("Aporte Salud Patrono")
        dt.Columns.Add("Aporte Salud Empleado")
        dt.Columns.Add("Aporte Pensión Patrono")
        dt.Columns.Add("Aporte Pensión Empleado")
        dt.Columns.Add("Provisión Cesantías")
        dt.Columns.Add("Provisión Intereses Cesantías")
        dt.Columns.Add("Provisión Primas")
        dt.Columns.Add("Provisión Vacaciones")
        dt.Columns.Add("ARL")

        'dt.Columns.Add("Documento")
        'dt.Columns.Add("Nombre Empleado")
        'dt.Columns.Add("Fecha Liquidación")
        ''dt.Columns.Add("Codigo Unidad Funcional")
        'dt.Columns.Add("Nombre Unidad Funcional")
        'dt.Columns.Add("Codigo Centro Costo")
        'dt.Columns.Add("Centro de Costo")
        'dt.Columns.Add("Salario Básico")
        ''dt.Columns.Add("Código Concepto")
        'dt.Columns.Add("Concepto")
        'dt.Columns.Add("Valor Concepto")
        'dt.Columns.Add("Fondo Salud")
        'dt.Columns.Add("Fondo Pensión")

        For Each itemView In ListRetroactive
            Dim row As DataRow = dt.NewRow()
            row.Item("Documento") = itemView.Nit
            row.Item("Nombre Empleado") = itemView.NombreEmpleado
            row.Item("Fecha Liquidación") = itemView.InitialDateRetroactive
            row.Item("Nombre Unidad Funcional") = itemView.FuncionalUnitName
            row.Item("Codigo Centro Costo") = itemView.CodeCostCenter
            row.Item("Centro de Costo") = itemView.NameCostCenter
            row.Item("Salario Básico") = itemView.BasicSalary
            row.Item("Concepto") = itemView.Concepto
            row.Item("Fondo Salud") = itemView.NitFondoSalud & " - " & itemView.NombreFondoSalud
            row.Item("Fondo Pensión") = itemView.NitFondoPension & " -" & itemView.NombreFondoPension
            row.Item("Aporte Sena") = itemView.Sena
            row.Item("Aporte Caja Compensación") = itemView.CompensationFundValue
            row.Item("Aporte ICBF") = itemView.ICBF
            row.Item("Aporte Salud Patrono") = itemView.HealthValueEmployer
            row.Item("Aporte Salud Empleado") = itemView.HealthValueEmployee
            row.Item("Aporte Pensión Patrono") = itemView.PensionValueEmployer
            row.Item("Aporte Pensión Empleado") = itemView.PensionValueEmployee
            row.Item("Provisión Cesantías") = itemView.UnemploymentProvision
            row.Item("Provisión Intereses Cesantías") = itemView.UnemploymentInterestProvision
            row.Item("Provisión Primas") = itemView.IncentivePaymentProvision
            row.Item("Provisión Vacaciones") = itemView.UnemploymentVacationProvision
            row.Item("ARL") = itemView.ARL
            'Dim row As DataRow = dt.NewRow()
            'row.Item("Documento") = itemView.Nit
            'row.Item("Nombre Empleado") = itemView.NombreEmpleado
            'row.Item("Fecha Liquidación") = itemView.InitialDateRetroactive
            ''row.Item("Codigo Unidad Funcional") = itemView.PayrollId.ContractId.FunctionalUnitId.Code
            'row.Item("Nombre Unidad Funcional") = itemView.FuncionalUnitName
            'row.Item("Codigo Centro Costo") = itemView.CodeCostCenter
            'row.Item("Centro de Costo") = itemView.NameCostCenter
            'row.Item("Salario Básico") = itemView.BasicSalary
            ''row.Item("Código Concepto") = itemView.ConceptId.Code
            'row.Item("Concepto") = itemView.Concepto
            'row.Item("Valor Concepto") = itemView.ValorConcepto
            'row.Item("Fondo Salud") = itemView.NitFondoSalud & " - " & itemView.NombreFondoSalud
            'row.Item("Fondo Pensión") = itemView.NitFondoPension & " -" & itemView.NombreFondoPension

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


    '' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployeeType()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployeeType = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.EmployeeType)
            INDSleEmployeeType.Datasource = ProoftCloseXpoEmployeeType
        End Using
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        '_pucModel.Dispose()
        '_pucModel = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerateReport
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>   
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then

            If INDGleProcess.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptPayrollAccounting()

                reporte.ParametrosReporte = {
                    INDDateStart.EditValue,
                    INDDateEnd.EditValue,
                    INDGleTypeConcept.EditValue,
                    INDSleEmployeeType.EditValue,
                    INDsleGroupStart.EditValue,
                    INDsleGroupEnd.EditValue,
                    INDsleSucursalStart.EditValue,
                    INDsleSucursalEnd.EditValue}

                INDDvViewReport.DocumentSource = reporte

                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
            Else
                AsyncLoader(True)
                Dim reporte As New rptPayrollAccountingRetroactive()

                reporte.ParametrosReporte = {
                    INDDateStart.EditValue,
                    INDDateEnd.EditValue,
                    INDGleTypeConcept.EditValue,
                    INDSleEmployeeType.EditValue,
                    INDsleGroupStart.EditValue,
                    INDsleGroupEnd.EditValue,
                    INDsleSucursalStart.EditValue,
                    INDsleSucursalEnd.EditValue}

                INDDvViewReport.DocumentSource = reporte

                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
            End If
        End If
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
    ''' Se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportControlLiquidation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeConcept.Properties.DataSource = FillingStatus
        Me.INDGleProcess.Properties.DataSource = FillingProcess

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeConcept.EditValue = 1
        Me.INDGleProcess.EditValue = 1
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployeeType.QueryPopUp
        If INDSleEmployeeType.Datasource Is Nothing Then
            LoadXpoEmployeeType()
        End If
    End Sub

    Private Sub INDsleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleGroupStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDsleGroupStart.Properties.DataSource = ProoftCloseXpoGroup
        End Using
    End Sub

    Private Sub INDsleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleGroupEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDsleGroupEnd.Properties.DataSource = ProoftCloseXpoGroup
        End Using
    End Sub

    Private Sub INDsleSucursalStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalStart.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

    Private Sub INDsleSucursalEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalEnd.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub
End Class
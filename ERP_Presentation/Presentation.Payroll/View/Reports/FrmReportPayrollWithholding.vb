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

#End Region

Public Class FrmReportPayrollWithholding


#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

    Private _FillingTypeProcedure As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeProcedure As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeProcedure Is Nothing Then
                _FillingTypeProcedure = New List(Of Tuple(Of Integer, String))
                _FillingTypeProcedure.Add(New Tuple(Of Integer, String)(1, "Procedimiento 1"))
                _FillingTypeProcedure.Add(New Tuple(Of Integer, String)(2, "Procedimiento 2"))
                _FillingTypeProcedure.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _FillingTypeProcedure
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
                _FillingProcess.Add(New Tuple(Of Integer, String)(1, "Retención de nomina"))
                _FillingProcess.Add(New Tuple(Of Integer, String)(2, "Retención de nomina retroactivo"))
                _FillingProcess.Add(New Tuple(Of Integer, String)(3, "Retención de primas"))
                _FillingProcess.Add(New Tuple(Of Integer, String)(4, "Retención de liquidación de contratos"))
            End If
            Return _FillingProcess
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
    '''<summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleEmployee.Datasource = ProoftCloseXpoEmployee
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupStart
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        '_pucModel.Dispose()
        '_pucModel = Nothing
        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoGroup = Nothing
    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            If INDGleProcess.EditValue = 1 Then

                If INDSleGroupStart.EditValue IsNot Nothing Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollWithholdingByGroup
                    reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                              INDCdnMonthYear.GetYear,
                                                              INDCdnMonthYear.GetMonth,
                                                              INDSleGroupStart.EditValue,
                                                              INDSleGroupEnd.EditValue,
                                                              INDGleTypeProcedure.EditValue,
                                                              INDsleSucursalStart.EditValue,
                                                              INDsleSucursalEnd.EditValue}
                    'INDsleGroup.EditValue, INDCtrDateNavigator.GetMonth, INDCtrDateNavigator.GetYear, INDSleEmployee.EditValue)
                    INDDvReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument()
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncReport.Visible = False
                        Me.INDPcReport.Visible = True
                        INDDvReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDCdnMonthYear.Focus()
                    End If
                Else
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollWithholding
                    reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                              INDCdnMonthYear.GetYear,
                                                              INDCdnMonthYear.GetMonth,
                                                              INDSleGroupStart.EditValue,
                                                              INDSleGroupEnd.EditValue,
                                                              INDGleTypeProcedure.EditValue,
                                                              INDsleSucursalStart.EditValue,
                                                              INDsleSucursalEnd.EditValue}
                    'INDsleGroup.EditValue, INDCtrDateNavigator.GetMonth, INDCtrDateNavigator.GetYear, INDSleEmployee.EditValue)
                    INDDvReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument()
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncReport.Visible = False
                        Me.INDPcReport.Visible = True
                        INDDvReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDCdnMonthYear.Focus()
                    End If
                End If
            ElseIf INDGleProcess.EditValue = 2 Then
                If INDSleGroupStart.EditValue IsNot Nothing Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollWithholdingByGroupRetroactive
                    reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                              INDCdnMonthYear.GetYear,
                                                              INDCdnMonthYear.GetMonth,
                                                              INDSleGroupStart.EditValue,
                                                              INDSleGroupEnd.EditValue,
                                                              INDGleTypeProcedure.EditValue,
                                                              INDsleSucursalStart.EditValue,
                                                              INDsleSucursalEnd.EditValue}
                    'INDsleGroup.EditValue, INDCtrDateNavigator.GetMonth, INDCtrDateNavigator.GetYear, INDSleEmployee.EditValue)
                    INDDvReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument()
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncReport.Visible = False
                        Me.INDPcReport.Visible = True
                        INDDvReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDCdnMonthYear.Focus()
                    End If
                Else
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollWithholdingRetroactive
                    reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                              INDCdnMonthYear.GetYear,
                                                              INDCdnMonthYear.GetMonth,
                                                              INDSleGroupStart.EditValue,
                                                              INDSleGroupEnd.EditValue,
                                                              INDGleTypeProcedure.EditValue,
                                                              INDsleSucursalStart.EditValue,
                                                              INDsleSucursalEnd.EditValue}
                    'INDsleGroup.EditValue, INDCtrDateNavigator.GetMonth, INDCtrDateNavigator.GetYear, INDSleEmployee.EditValue)
                    INDDvReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument()
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncReport.Visible = False
                        Me.INDPcReport.Visible = True
                        INDDvReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDCdnMonthYear.Focus()
                    End If
                End If
            ElseIf INDGleProcess.EditValue = 3 Then
                If INDSleGroupStart.EditValue IsNot Nothing Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollWithholdingByGroupIncentive
                    reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                              INDDateStart.EditValue,
                                                              INDDateEnd.EditValue,
                                                              INDSleGroupStart.EditValue,
                                                              INDSleGroupEnd.EditValue,
                                                              INDGleTypeProcedure.EditValue,
                                                              INDGlePeriod.EditValue,
                                                              INDsleSucursalStart.EditValue,
                                                              INDsleSucursalEnd.EditValue}
                    INDDvReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument()
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncReport.Visible = False
                        Me.INDPcReport.Visible = True
                        INDDvReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDCdnMonthYear.Focus()
                    End If
                Else
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollWithholdingIncentive
                    reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                              INDDateStart.EditValue,
                                                              INDDateEnd.EditValue,
                                                              INDSleGroupStart.EditValue,
                                                              INDSleGroupEnd.EditValue,
                                                              INDGleTypeProcedure.EditValue,
                                                              INDGlePeriod.EditValue,
                                                              INDsleSucursalStart.EditValue,
                                                              INDsleSucursalEnd.EditValue}
                    INDDvReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument()
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncReport.Visible = False
                        Me.INDPcReport.Visible = True
                        INDDvReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDCdnMonthYear.Focus()
                    End If
                End If
            ElseIf INDGleProcess.EditValue = 4 Then
                AsyncLoader(True)
                    Dim reporte As New rptContractLiquidationWithholding
                    reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                              INDCdnMonthYear.GetYear,
                                                              INDCdnMonthYear.GetMonth,
                                                              INDSleGroupStart.EditValue,
                                                              INDSleGroupEnd.EditValue,
                                                              INDGleTypeProcedure.EditValue,
                                                              INDsleSucursalStart.EditValue,
                                                              INDsleSucursalEnd.EditValue}
                    INDDvReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument()
                    End If
                    AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnMonthYear.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDCdnMonthYear.Focus()
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleEmployees
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployees_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Datasource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub
    ' <summary>
    ' se ejecuta en el evento querypopup del control INDSleFunctionalUnitStart
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    Private Sub INDSleFunctionalUnitStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub
    ' <summary>
    ' se ejecuta en el evento querypopup del control INDSleFunctionalUnitEnd
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    Private Sub INDSleFunctionalUnitEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub
#End Region

#Region "Validate"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida Grupo
        If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue Is Nothing Or INDSleGroupEnd.EditValue IsNot Nothing And INDSleGroupStart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If

        'If INDSleEmployees.EditValue Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciEmployees.Text)
        '    INDSleEmployees.Focus()
        '    ValidateControlsCuadroTurno = False
        'End If
        Return Validations
    End Function
#End Region


    Private Sub INDGleProcess_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleProcess.EditValueChanged
        If INDGleProcess.EditValue = 1 Then
            INDLciYearMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGlePeriod.EditValue = Nothing
        ElseIf INDGleProcess.EditValue = 2 Then
            INDLciYearMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGlePeriod.EditValue = Nothing
        ElseIf INDGleProcess.EditValue = 3 Then
            INDLciYearMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGlePeriod.EditValue = 3
            Me.INDDateStart.Focus()
        ElseIf INDGleProcess.EditValue = 4 Then
            ' Retención de liquidación de contratos: mismo criterio que 1 y 2 (año/mes)
            INDLciYearMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGlePeriod.EditValue = Nothing
        End If
    End Sub

    Private Sub FrmReportPayrollWithholding_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeProcedure.Properties.DataSource = FillingTypeProcedure
        Me.INDGleProcess.Properties.DataSource = FillingProcess
        Me.INDGlePeriod.Properties.DataSource = FillingPeriod

        'Dar un valor por defecto a los GridLomokEdit
        Me.INDGleTypeProcedure.EditValue = 3
        Me.INDGleProcess.EditValue = 1
        Me.INDGlePeriod.EditValue = 3
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
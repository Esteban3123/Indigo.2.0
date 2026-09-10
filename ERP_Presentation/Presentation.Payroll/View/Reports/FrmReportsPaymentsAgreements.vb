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

Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Payroll.Entities
Imports DevExpress.XtraEditors
Imports System.Drawing
#End Region

Public Class FrmReportsPaymentsAgreements

#Region "Fields"

    '' <summary>
    '' Referencia al modelo de PUC
    '' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoCompany As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource
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
    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Estado de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Sin confirmar"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Suspendido"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Terminado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(5, "Anulado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(6, "Todos"))

            End If
            Return _FillingStatus
        End Get
    End Property
    'FunctionalUnit

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueGroup
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupsPayroll()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSlueGroup
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSlueGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupsPayroll()
        End If
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupsPayrollEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSlueGroupEnd
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSlueGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupsPayrollEnd()
        End If
    End Sub

#Region "Company"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCompanyStart
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoCompanyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCompany = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CompanyPayroll)
            INDSleCompanyStart.Datasource = ProoftCloseXpoCompany
        End Using
    End Sub

    ' <summary>
    ' se ejecuta en el evento querypopup del control INDSleCompanyStart
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    Private Sub INDSleComapnyStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCompanyStart.QueryPopUp
        If INDSleCompanyStart.Datasource Is Nothing Then
            LoadXpoCompanyStart()
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCompanyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoCompanyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCompany = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CompanyPayroll)
            INDSleCompanyEnd.Datasource = ProoftCloseXpoCompany
        End Using
    End Sub

    ' <summary>
    ' se ejecuta en el evento querypopup del control INDSleCompanyEnd
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    Private Sub INDSleComapnyEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCompanyEnd.QueryPopUp
        If INDSleCompanyEnd.Datasource Is Nothing Then
            LoadXpoCompanyEnd()
        End If
    End Sub
#End Region

    '' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleEmployees.Datasource = ProoftCloseXpoEmployee
        End Using
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleEmployees
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployees_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployees.QueryPopUp
        If INDSleEmployees.Datasource Is Nothing Then
            LoadXpoEmployee()
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
        'Valida Fechas
        If INDDateEditDateStart.EditValue Is Nothing Or INDDateEditDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateEditDateStart.EditValue > INDDateEditDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        End If

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

        'Valida Company
        If INDSleCompanyStart.EditValue Is Nothing And INDSleCompanyEnd.EditValue IsNot Nothing Or INDSleCompanyEnd.EditValue Is Nothing And INDSleCompanyStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCompany.Text)
            Me.INDSleCompanyStart.Focus()
            Validations = False
        ElseIf INDSleCompanyEnd.EditValue < INDSleCompanyStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCompany.Text)
            Me.INDSleCompanyStart.Focus()
            Validations = False
        End If
        Return Validations
    End Function
#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoCompany = Nothing
        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoGroup = Nothing
    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            If INDSleGroupStart.EditValue IsNot Nothing And INDSleCompanyStart.EditValue Is Nothing Or INDSleGroupStart.EditValue IsNot Nothing And INDSleCompanyStart.EditValue IsNot Nothing Then
                AsyncLoader(True)
                Dim reporte As New rptPayrollAgreementsD
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                          INDDateEditDateEnd.EditValue,
                                                          INDSleGroupStart.EditValue,
                                                          INDSleGroupEnd.EditValue,
                                                          INDSleEmployees.EditValue,
                                                          INDGleState.EditValue,
                                                          INDGleState.Text,
                                                          INDSleCompanyStart.EditValue,
                                                          INDSleCompanyEnd.EditValue,
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
                    Me.INDCnpReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditDateStart.Focus()
                End If

            ElseIf INDSleGroupStart.EditValue Is Nothing And INDSleCompanyStart.EditValue IsNot Nothing Then
                AsyncLoader(True)
                Dim reporte As New rptPayrollAgreementsDEntid
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                          INDDateEditDateEnd.EditValue,
                                                          INDSleGroupStart.EditValue,
                                                          INDSleGroupEnd.EditValue,
                                                          INDSleEmployees.EditValue,
                                                          INDGleState.EditValue,
                                                          INDGleState.Text,
                                                          INDSleCompanyStart.EditValue,
                                                          INDSleCompanyEnd.EditValue,
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
                    Me.INDCnpReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditDateStart.Focus()
                End If


            ElseIf INDSleGroupStart.EditValue Is Nothing And INDSleCompanyStart.EditValue Is Nothing Then
                AsyncLoader(True)
                Dim reporte As New rptPayrollAgreement
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                          INDDateEditDateEnd.EditValue,
                                                          INDSleGroupStart.EditValue,
                                                          INDSleGroupEnd.EditValue,
                                                          INDSleEmployees.EditValue,
                                                          INDGleState.EditValue,
                                                          INDGleState.Text,
                                                          INDSleCompanyStart.EditValue,
                                                          INDSleCompanyEnd.EditValue,
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
                    Me.INDCnpReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditDateStart.Focus()
                End If

            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCnpReport.Visible = True
        Me.INDDateEditDateStart.Focus()
    End Sub

    Private Sub FrmReportsPaymentsAgreements_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleState.Properties.DataSource = FillingStatus
        Me.INDGleState.EditValue = 6
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
#End Region
End Class
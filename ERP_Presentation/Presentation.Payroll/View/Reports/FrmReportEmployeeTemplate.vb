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

Public Class FrmReportEmployeeTemplate
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoFunctionalUnit As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoTemplate As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

    Private reporte As Object

    Private _FillingStatus As List(Of Tuple(Of String, String))

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Plantilla por Empleado"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Plantilla por Concepto"))
            End If
            Return _FillingReportType
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
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnitStart
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoFunctionalUnitStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoFunctionalUnit = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
            INDSleFunctionalUnitStart.Datasource = ProoftCloseXpoFunctionalUnit
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnitEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoFunctionalUnitEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoFunctionalUnit = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
            INDSleFunctionalUnitEnd.Datasource = ProoftCloseXpoFunctionalUnit
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTemplate
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoTemplate()
        ProoftCloseXpoTemplate = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetScheduleTemplate
        INDSleTemplate.Datasource = ProoftCloseXpoTemplate
    End Sub
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        '_pucModel.Dispose()
        '_pucModel = Nothing
        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoFunctionalUnit = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoTemplate = Nothing
        reporte = Nothing
        _FillingReportType = Nothing
        _FillingStatus = Nothing
    End Sub
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)

            Select Case INDGleTypeReport.EditValue
                Case 1
                    reporte = New rptTemplateByEmployeex
                Case Else
                    reporte = New rptTemplateByConcept
            End Select


            reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                      INDCdnMonthYear.GetMonth,
                                                      INDCdnMonthYear.GetYear,
                                                      INDSleFunctionalUnitStart.EditValue,
                                                      INDSleFunctionalUnitEnd.EditValue,
                                                      INDSleGroupStart.EditValue,
                                                      INDSleGroupEnd.EditValue,
                                                      INDSleTemplate.EditValue,
                                                      INDsleSucursalStart.EditValue,
                                                      INDsleSucursalEnd.EditValue}

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
                Me.INDCdnMonthYear.Focus()
            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack()
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
    Private Sub INDSleFunctionalUnitStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnitStart.QueryPopUp
        If INDSleFunctionalUnitStart.Datasource Is Nothing Then
            LoadXpoFunctionalUnitStart()
        End If
    End Sub

    ' <summary>
    ' se ejecuta en el evento querypopup del control INDSleFunctionalUnitEnd
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    Private Sub INDSleFunctionalUnitEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnitEnd.QueryPopUp
        If INDSleFunctionalUnitEnd.Datasource Is Nothing Then
            LoadXpoFunctionalUnitEnd()
        End If
    End Sub
    Private Sub FrmReportEmployeeTemplate_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = FillingReportType

        'Dar un valor por defecto a los GridLookEdit        
        INDGleTypeReport.EditValue = 1
    End Sub
#End Region

#Region "Validate"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida Unidad Funcional
        If INDSleFunctionalUnitStart.EditValue IsNot Nothing And INDSleFunctionalUnitEnd.EditValue Is Nothing Or INDSleFunctionalUnitEnd.EditValue IsNot Nothing And INDSleFunctionalUnitStart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblFunctionalUnit.Text)
            Me.INDSleFunctionalUnitStart.Focus()
            Validations = False
        ElseIf INDSleFunctionalUnitEnd.EditValue < INDSleFunctionalUnitStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblFunctionalUnit.Text)
            Me.INDSleFunctionalUnitStart.Focus()
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



    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroup
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

    Private Sub INDCnReport_ClickBack1() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDSleEmployee.Focus()
    End Sub

    Private Sub INDSleTemplate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTemplate.QueryPopUp
        If INDSleTemplate.Datasource Is Nothing Then
            LoadXpoTemplate()
        End If
    End Sub

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 2 Then
            INDSleTemplate.EditValue = Nothing
            INDLciTemplate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDLciTemplate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
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
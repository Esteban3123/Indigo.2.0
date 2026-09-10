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

Public Class FrmReportIncentiveAccounting
#Region "Fields"

    '' <summary>
    '' Referencia al modelo de PUC
    '' </summary>
    Private _pucModel As MCommon

    '' <summary>
    '' Referencia al modelo de PUC
    '' </summary>
    Private _PayrollModelGroup As MGroups

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoConcept As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

    Private reporte As Object
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
    ''' Propiedad que se usa para cargar la Dupla de Datos de Tipos de Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Planilla Por Empleado"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Planilla Por Concepto"))
            End If
            Return _LoadTypeReport
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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.AllEmployees)
            INDSleEmployee.Datasource = ProoftCloseXpoEmployee
        End Using
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
        If INDSleGroupStart.EditValue Is Nothing And INDSleGroupEnd.EditValue IsNot Nothing Or INDSleGroupEnd.EditValue Is Nothing And INDSleGroupStart.EditValue IsNot Nothing Or (INDSleGroupStart.TextEditValue <> String.Empty And INDSleGroupEnd.TextEditValue = String.Empty Or INDSleGroupEnd.TextEditValue <> String.Empty And INDSleGroupStart.TextEditValue = String.Empty) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Or (INDSleGroupEnd.TextEditValue < INDSleGroupStart.TextEditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If
        Return Validations
    End Function
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        '_pucModel.Dispose()
        '_pucModel = Nothing
        _PayrollModelGroup.Dispose()
        _PayrollModelGroup = Nothing
        ProoftCloseXpoConcept = Nothing
        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoGroup = Nothing
        reporte = Nothing
    End Sub
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Me.Cursor = BaseClass.ChangeCursorIndigo

            If INDSleGroupStart.TextEditValue <> "" Then

                reporte = New rptIncentiveAccountingWithConceptsByGroup
            Else
                reporte = New rptIncentiveAccountingWithConcepts

            End If


            'If INDSleGroupStart.TextEditValue <> "" Then

            '    reporte = New rptIncentiveAccountingByGroup
            'Else
            '    reporte = New rptIncentiveAccounting

            'End If

            reporte.ParametrosReporte = {INDDateEditDateStart.EditValue,
                                         INDDateEditDateEnd.EditValue,
                                         INDSleEmployee.TextEditValue,
                                         INDSleGroupStart.TextEditValue,
                                         INDSleGroupEnd.TextEditValue,
                                         INDGlePeriod.EditValue,
                                         INDsleSucursalStart.EditValue,
                                         INDsleSucursalEnd.EditValue}

            INDDvReport.DocumentSource = reporte
            Await CType(reporte, IReportAsync).CargarDataSourceAsync
            'reporte.CargarDataSource()
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateEditDateStart.Focus()
            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDDateEditDateStart.Focus()
    End Sub

    Private Sub FrmReportCostDistributions_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Me.INDGleState.Properties.DataSource = FillingStatus
        Me.INDGlePeriod.Properties.DataSource = FillingPeriod

        'Dar un valor por defecto a los GridLookEdit
        'Me.INDGleState.EditValue = "T"
        Me.INDGlePeriod.EditValue = 3

    End Sub

    Private Sub FrmReportCostDistributions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._PayrollModelGroup = New MGroups(Me.Tag)
        ' Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._PayrollModelGroup.GetGroupAsync()
        'Me.INDSleUnitEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
    End Sub


    Private Sub INDsleSucursalStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalStart.QueryPopUp
        LoadXpoBranchOfficeStart()
    End Sub

    Private Sub LoadXpoBranchOfficeStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalStart.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

    Private Sub INDsleSucursalEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalEnd.QueryPopUp
        LoadXpoBranchOfficeEnd()
    End Sub

    Private Sub LoadXpoBranchOfficeEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalEnd.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

#End Region
End Class
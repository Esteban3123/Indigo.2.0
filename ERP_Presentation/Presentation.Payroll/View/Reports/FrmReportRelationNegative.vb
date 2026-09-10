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

Public Class FrmReportRelationNegative
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
    ''' Propiedad que se usa para cargar la Dupla de Datos de Estado de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingStatus As List(Of Tuple(Of String, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of String, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of String, String))
                _FillingStatus.Add(New Tuple(Of String, String)("C", "Confirmados"))
                _FillingStatus.Add(New Tuple(Of String, String)("", "Sin Confirmar"))
                _FillingStatus.Add(New Tuple(Of String, String)("S", "Saldo Inicial"))
                _FillingStatus.Add(New Tuple(Of String, String)("T", "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property


    ''' <summary>
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
    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Dim reporte As New rptPayrollRelationNegative()

            reporte.ParametrosReporte = {INDDateEditDateStart.EditValue,
                                         INDDateEditDateEnd.EditValue,
                                         INDSleEmployee.EditValue}

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

    Private Sub FrmReportsConventionsEmployees_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Me.INDGleState.Properties.DataSource = FillingStatus

        ''Dar un valor por defecto a los GridLookEdit
        'Me.INDGleState.EditValue = "T"

    End Sub

    Private Sub FrmReportPayrollByGroups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._PayrollModelGroup = New MGroups(Me.Tag)
        ' Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._PayrollModelGroup.GetGroupAsync()
        'Me.INDSleUnitEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
    End Sub
#End Region
End Class
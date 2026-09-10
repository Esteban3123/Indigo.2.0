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

Public Class FrmReportBoxShift
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoFunctionalUnit As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

    Private _FillingStatus As List(Of Tuple(Of String, String))
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
#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        If _pucModel IsNot Nothing Then
            _pucModel.Dispose()
            _pucModel = Nothing
        End If

        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoFunctionalUnit = Nothing
        _FillingStatus = Nothing
    End Sub
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Dim reporte As New rptSchedule
            reporte.ParametrosReporte = New Object() {INDSleEmployee.EditValue,
                                                      INDCdnMonthYear.GetMonth,
                                                      INDCdnMonthYear.GetYear,
                                                      Me.BarraBotones.OperatingUnit,
                                                      INDSleFunctionalUnitStart.EditValue,
                                                      INDSleFunctionalUnitEnd.EditValue,
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
    Private Sub FrmReportsConventionsEmployees_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Me.INDGleState.Properties.DataSource = FillingStatus
        'Me.INDGleState.EditValue = "T"
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

    Private Sub SearchLookUpEdit1_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalStart.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

    Private Sub SearchLookUpEdit2_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalEnd.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub
#End Region
End Class
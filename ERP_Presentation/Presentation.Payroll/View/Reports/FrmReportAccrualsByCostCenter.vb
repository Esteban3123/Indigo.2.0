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


Public Class FrmReportAccrualsByCostCenter


#Region "Properties"
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCostCenter As XPInstantFeedbackSource

    Private _FillingReport As List(Of Tuple(Of Integer, String))

    Private _LoadEmployeeType As List(Of Tuple(Of String, String))

#End Region


#Region "Method"

    '''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

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

        'Valida Centro de costo
        If INDSleCostCenterStart.EditValue Is Nothing And INDSleCostCenterEnd.EditValue IsNot Nothing Or INDSleCostCenterEnd.EditValue Is Nothing And INDSleCostCenterStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
            Validations = False
        ElseIf INDSleCostCenterEnd.EditValue < INDSleCostCenterStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenterStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCostCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            INDSleCostCenterStart.Datasource = ProoftCloseXpoCostCenter
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenterEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCostCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            INDSleCostCenterEnd.Datasource = ProoftCloseXpoCostCenter
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly Property FillingReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReport Is Nothing Then
                _FillingReport = New List(Of Tuple(Of Integer, String))
                _FillingReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))

            End If
            Return _FillingReport
        End Get
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly Property LoadEmployeeType As List(Of Tuple(Of String, String))
        Get
            If _LoadEmployeeType Is Nothing Then
                _LoadEmployeeType = New List(Of Tuple(Of String, String))
                _LoadEmployeeType.Add(New Tuple(Of String, String)("001", "Administrativos"))
                _LoadEmployeeType.Add(New Tuple(Of String, String)("002", "Operativos"))
                _LoadEmployeeType.Add(New Tuple(Of String, String)("T", "Todo"))

            End If
            Return _LoadEmployeeType
        End Get
    End Property
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoCostCenter = Nothing
        _FillingReport = Nothing
        _LoadEmployeeType = Nothing
    End Sub
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            If INDGleReport.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptAccrualsByCostCenter()

                reporte.ParametrosReporte = {INDDnPeriodStart.GetYear,
                                             INDDnPeriodStart.GetMonth,
                                             INDDnPeriodEnd.GetYear,
                                             INDDnPeriodEnd.GetMonth,
                                             INDSleCostCenterStart.EditValue,
                                             INDSleCostCenterEnd.EditValue,
                                             INDSleGroupStart.EditValue,
                                             INDSleGroupEnd.EditValue,
                                             INDGleEmployeeType.EditValue,
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
                    Me.INDDnPeriodStart.Focus()
                End If
            Else
                AsyncLoader(True)
                Dim reporte As New rptAccrualsByCostCenterSummary()

                reporte.ParametrosReporte = {INDDnPeriodStart.GetYear,
                                             INDDnPeriodStart.GetMonth,
                                             INDDnPeriodEnd.GetYear,
                                             INDDnPeriodEnd.GetMonth,
                                             INDSleCostCenterStart.EditValue,
                                             INDSleCostCenterEnd.EditValue,
                                             INDSleGroupStart.EditValue,
                                             INDSleGroupEnd.EditValue,
                                             INDGleEmployeeType.EditValue}

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
                    Me.INDDnPeriodStart.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDnPeriodStart.Focus()
    End Sub
    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub

    Private Sub INDSleCostCenterStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterStart.QueryPopUp
        If INDSleCostCenterStart.Datasource Is Nothing Then
            LoadXpoCostCenterStart()
        End If
    End Sub

    Private Sub INDSleCostCenterEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterEnd.QueryPopUp
        If INDSleCostCenterEnd.Datasource Is Nothing Then
            LoadXpoCostCenterEnd()
        End If
    End Sub
#End Region

    Private Sub FrmReportAccrualsByCostCenter_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleReport.Properties.DataSource = FillingReport
        Me.INDGleEmployeeType.Properties.DataSource = LoadEmployeeType
        'Valor por defecto en GridLookUpEdit
        Me.INDGleReport.EditValue = 1
        Me.INDGleEmployeeType.EditValue = "T"

    End Sub

    Private Sub INDDnPeriodEnd_OnChangeDate(sender As Object, e As EventArgs) Handles INDDnPeriodEnd.OnChangeDate
        If INDDnPeriodEnd.GetYear <> INDDnPeriodStart.GetYear Then
            INDDnPeriodEnd.SetYear = INDDnPeriodStart.GetYear
        End If
    End Sub

    Private Sub INDDnPeriodStart_OnChangeDate(sender As Object, e As EventArgs) Handles INDDnPeriodStart.OnChangeDate
        INDDnPeriodEnd.SetYear = INDDnPeriodStart.GetYear
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
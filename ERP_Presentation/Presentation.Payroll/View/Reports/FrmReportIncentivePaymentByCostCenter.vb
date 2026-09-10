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

Public Class FrmReportIncentivePaymentByCostCenter

    Public Property ProoftCloseXpoCostCenter As XPInstantFeedbackSource

    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource

    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#End Region

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de tipo de empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingTypeEmployee As List(Of Tuple(Of String, String))
    Private ReadOnly Property FillingTypeEmployee As List(Of Tuple(Of String, String))
        Get
            If _FillingTypeEmployee Is Nothing Then
                _FillingTypeEmployee = New List(Of Tuple(Of String, String))
                _FillingTypeEmployee.Add(New Tuple(Of String, String)("001", "Administrativo"))
                _FillingTypeEmployee.Add(New Tuple(Of String, String)("002", "Operativo"))
            End If
            Return _FillingTypeEmployee
        End Get
    End Property



    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de tipo de empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingTypeConcept As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeConcept As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeConcept Is Nothing Then
                _FillingTypeConcept = New List(Of Tuple(Of Integer, String))
                _FillingTypeConcept.Add(New Tuple(Of Integer, String)(1, "Deducciones"))
                _FillingTypeConcept.Add(New Tuple(Of Integer, String)(2, "Devengos"))
            End If
            Return _FillingTypeConcept
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

    Private Sub FrmReportIncentivePayment_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit        
        Me.INDGleTypeEmployee.Properties.DataSource = FillingTypeEmployee
        Me.INDTypeConcept.Properties.DataSource = FillingTypeConcept
        Me.INDGlePeriod.Properties.DataSource = FillingPeriod
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeEmployee.EditValue = "001"
        Me.INDTypeConcept.EditValue = 1
        Me.INDGlePeriod.EditValue = 3
    End Sub

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

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        '_pucModel.Dispose()
        '_pucModel = Nothing
    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            If INDGleTypeEmployee.EditValue = "001" Then

                If INDTypeConcept.EditValue = 1 Then 'ADMINISTRATIVOS DEDUCCIONES

                    AsyncLoader(True)
                    Dim reporte As New rptDeductionsIncentivePaymentBasicAdministrativeByCostCenter()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleTypeEmployee.EditValue,
                        INDSleCostCenterStart.EditValue,
                        INDSleCostCenterEnd.EditValue,
                        INDGlePeriod.EditValue,
                        INDsleGroupStart.EditValue,
                        INDsleGroupEnd.EditValue,
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
                        Me.INDDateStart.Focus()
                    End If

                ElseIf INDTypeConcept.EditValue = 2 Then  'ADMINISTRATIVOS DEVENGOS

                    AsyncLoader(True)
                    Dim reporte As New rptAccrualsDeductionsIncentivePaymentBasicAdministrativeByCostCenter()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleTypeEmployee.EditValue,
                        INDSleCostCenterStart.EditValue,
                        INDSleCostCenterEnd.EditValue,
                        INDGlePeriod.EditValue,
                        INDsleGroupStart.EditValue,
                        INDsleGroupEnd.EditValue,
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
                        Me.INDDateStart.Focus()
                    End If
                End If

            ElseIf INDGleTypeEmployee.EditValue = "002" Then

                If INDTypeConcept.EditValue = 1 Then 'OPERATIVOS DEDUCCIONES
                    AsyncLoader(True)
                    Dim reporte As New rptDeductionsIncentivePaymentBasicOperativeByCostCenter()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleTypeEmployee.EditValue,
                        INDSleCostCenterStart.EditValue,
                        INDSleCostCenterEnd.EditValue,
                        INDGlePeriod.EditValue,
                        INDsleGroupStart.EditValue,
                        INDsleGroupEnd.EditValue,
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
                        Me.INDDateStart.Focus()
                    End If

                ElseIf INDTypeConcept.EditValue = 2 Then  'OPERATIVOS DEVENGOS
                    AsyncLoader(True)
                    Dim reporte As New rptAccrualsIncentivePaymentBasicOperativeByCostCenter()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleTypeEmployee.EditValue,
                        INDSleCostCenterStart.EditValue,
                        INDSleCostCenterEnd.EditValue,
                        INDGlePeriod.EditValue,
                        INDsleGroupStart.EditValue,
                        INDsleGroupEnd.EditValue,
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
                        Me.INDDateStart.Focus()
                    End If

                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDateStart.Focus()
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
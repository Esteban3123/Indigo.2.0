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


Public Class FrmReportPayrollVacation

#Region "Fields"

    '' <summary>
    '' Referencia al modelo de PUC
    '' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Private varSelect As ImageComboBoxEdit

    Public Property ProoftCloseXpoThirdParty As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroupEnd As XPInstantFeedbackSource

    Private _LoadPaymentType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadPaymentType As List(Of Tuple(Of Integer, String))
        Get
            If _LoadPaymentType Is Nothing Then
                _LoadPaymentType = New List(Of Tuple(Of Integer, String))
                _LoadPaymentType.Add(New Tuple(Of Integer, String)(1, "Por Periodo Independiente"))
                _LoadPaymentType.Add(New Tuple(Of Integer, String)(2, "Por Nomina"))

            End If
            Return _LoadPaymentType
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
    '' <summary>
    '' metodo para Cargar el data source Del Control INDSlueIdentificationDocument
    '' </summary>
    '' <remarks></remarks>
    Private Sub LoadXpoThirdParty()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSlueIdentificationDocument.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub
    

    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSlueIdentificationDocument
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSlueDocumentoIdentidad_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlueIdentificationDocument.QueryPopUp
        If INDSlueIdentificationDocument.Datasource Is Nothing Then
            LoadXpoThirdParty()
        End If
    End Sub


    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueGroup
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupsPayroll()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSlueGroup.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSlueGroup
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSlueGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlueGroup.QueryPopUp
        If INDSlueGroup.Datasource Is Nothing Then
            LoadXpoGroupsPayroll()
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupsPayrollEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroupEnd = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSlueGroupEnd.Datasource = ProoftCloseXpoGroupEnd
        End Using
    End Sub
    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSlueGroupEnd
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSlueGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlueGroupEnd.QueryPopUp
        If INDSlueGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupsPayrollEnd()
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

        If varSelect.EditValue = 1 Then

            'INDCmbReport

            'If INDSlueIdentificationDocument.EditValue Is Nothing Then
            '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FieldEmpty", "Commons"), INDLciIdentificationDocument.Text)
            '    Me.INDSlueIdentificationDocument.Focus()
            '    Validations = False
            'End If

            'Valida Fechas
            If INDDateEditStart.EditValue Is Nothing Or INDDateEditEnd.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
                Me.INDDateEditStart.Focus()
                Validations = False
            ElseIf Me.INDDateEditStart.EditValue > INDDateEditEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
                Me.INDDateEditStart.Focus()
                Validations = False
            End If

        ElseIf varSelect.EditValue = 2 Then
            'Validar Filtros
            'Valida Grupo
            If INDSlueGroup.EditValue Is Nothing And INDSlueGroupEnd.EditValue IsNot Nothing Or INDSlueGroupEnd.EditValue Is Nothing And INDSlueGroup.EditValue IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
                Me.INDSlueGroup.Focus()
                Validations = False
            ElseIf INDSlueGroupEnd.EditValue < INDSlueGroup.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
                Me.INDSlueGroup.Focus()
                Validations = False
            End If
        End If

        Return Validations
    End Function
#End Region


#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoGroupEnd = Nothing
        ProoftCloseXpoThirdParty = Nothing
        _LoadPaymentType = Nothing
    End Sub
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            If varSelect.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptLiquidationOfSheetVacations
                reporte.ParametrosReporte = New Object() {INDDateEditStart.EditValue,
                                                          INDDateEditEnd.EditValue,
                                                          INDSlueIdentificationDocument.EditValue,
                                                          INDSlueGroup.EditValue,
                                                          INDSlueGroupEnd.EditValue,
                                                          INDGlePaymentType.EditValue}
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
                    Me.INDSlueIdentificationDocument.Focus()
                End If
            ElseIf varSelect.EditValue = 2 Then
                AsyncLoader(True)
                Dim reporte As New rptEmployeeRelationshipVacation
                reporte.ParametrosReporte = New Object() {INDDnYearMonth.GetYear,
                                                          INDDnYearMonth.GetMonth,
                                                          INDSlueGroup.EditValue,
                                                          INDSlueGroupEnd.EditValue,
                                                          INDGlePaymentType.EditValue}
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
                    Me.INDSlueIdentificationDocument.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub FrmReportPayrollVacation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDCmbReport.SelectedIndex = INDCmbReport.Properties.Items.Count - 1

        'Cargar GridLookUpEdit
        Me.INDGlePaymentType.Properties.DataSource = LoadPaymentType
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGlePaymentType.EditValue = 1
    End Sub
    Private Sub FrmReportPayrollVacation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc, son los metodos de busqueda de los searchlookupedit
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSlueIdentificationDocument.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
    End Sub

    Public Sub RelaconEmpleadosVacaciones()
        INDLciDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDDnYearMonth.HowShowControl = CtrDateNavigator.EHowShowControl.Both

        'INDLciDateNavigator.MinSize = New Size(364, 64)
        'INDLciDateNavigator.MaxSize = New Size(364, 64)
        INDLciIdentificationDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSlueGroup.EditValue = Nothing
        INDSlueGroupEnd.EditValue = Nothing

        INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDDateEditStart.EditValue = Nothing
        INDDateEditEnd.EditValue = Nothing
        INDSlueIdentificationDocument.EditValue = Nothing
    End Sub


    Private Sub ImageComboBoxEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles INDCmbReport.EditValueChanged

        varSelect = sender
        If varSelect.EditValue = 1 Then
            'INDLciDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciIdentificationDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'INDDnYearMonth.HowShowControl = CtrDateNavigator.EHowShowControl.Both
            'INDLciDateNavigator.MinSize = New Size(414, 330)
            'INDLciDateNavigator.MaxSize = New Size(414, 330)            
            INDSbGenerateReport.Enabled = False          
            INDSlueGroup.EditValue = Nothing
            INDSlueGroupEnd.EditValue = Nothing

            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDDateEditStart.EditValue = Nothing
            INDDateEditEnd.EditValue = Nothing
            INDLciDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf varSelect.EditValue = 2 Then
            RelaconEmpleadosVacaciones()
        End If

        If varSelect.EditValue = Nothing Then
            INDSbGenerateReport.Enabled = False
        Else
            INDSbGenerateReport.Enabled = True
        End If

    End Sub
    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnReport
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
    End Sub
#End Region
End Class
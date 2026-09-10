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

Public Class FrmReportExpeditionCDPAndRPb


#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoUnit As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _LoadConceptType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadConceptType As List(Of Tuple(Of Integer, String))
        Get
            If _LoadConceptType Is Nothing Then
                _LoadConceptType = New List(Of Tuple(Of Integer, String))
                _LoadConceptType.Add(New Tuple(Of Integer, String)(1, "Devengados"))
                _LoadConceptType.Add(New Tuple(Of Integer, String)(2, "Deducidos"))

            End If
            Return _LoadConceptType
        End Get
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _LoadEmployeeType As List(Of Tuple(Of String, String))
    Private ReadOnly Property LoadEmployeeType As List(Of Tuple(Of String, String))
        Get
            If _LoadEmployeeType Is Nothing Then
                _LoadEmployeeType = New List(Of Tuple(Of String, String))
                _LoadEmployeeType.Add(New Tuple(Of String, String)("001", "Administrativos"))
                _LoadEmployeeType.Add(New Tuple(Of String, String)("002", "Operativos"))

            End If
            Return _LoadEmployeeType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Process
    ''' </summary>
    ''' <remarks></remarks>
    Private _LoadTypeProcess As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeProcess As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeProcess Is Nothing Then
                _LoadTypeProcess = New List(Of Tuple(Of Integer, String))
                _LoadTypeProcess.Add(New Tuple(Of Integer, String)(1, "Nómina"))
                _LoadTypeProcess.Add(New Tuple(Of Integer, String)(2, "Retroactivo"))
                _LoadTypeProcess.Add(New Tuple(Of Integer, String)(3, "Primas"))
            End If
            Return _LoadTypeProcess
        End Get
    End Property

#End Region

    '''' <summary>
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
    ''' se ejecuta en el evento click del control INDSbGenerateReport
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>    
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            If INDGleProcess.EditValue = 1 Or INDGleProcess.EditValue = 3 Then
                AsyncLoader(True)
                Dim reporte As New rptExpeditionCDPAndRP

                reporte.ParametrosReporte = {INDDateStart.EditValue,
                                             INDDateEnd.EditValue,
                                             INDGleConceptType.EditValue,
                                             INDGleEmployeeType.EditValue,
                                             INDGleProcess.EditValue,
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
            ElseIf INDGleProcess.EditValue = 2 Then
                AsyncLoader(True)
                Dim reporte As New rptExpeditionCDPAndRPRetroactive

                reporte.ParametrosReporte = {INDDateStart.EditValue,
                                             INDDateEnd.EditValue,
                                             INDGleConceptType.EditValue,
                                             INDGleEmployeeType.EditValue}

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

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoUnit = Nothing
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportContributionFunds_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleConceptType.Properties.DataSource = LoadConceptType
        Me.INDGleEmployeeType.Properties.DataSource = LoadEmployeeType
        Me.INDGleProcess.Properties.DataSource = LoadTypeProcess

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleConceptType.EditValue = 1
        Me.INDGleProcess.EditValue = 1
        'Me.INDGleStatus.EditValue = "001"
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
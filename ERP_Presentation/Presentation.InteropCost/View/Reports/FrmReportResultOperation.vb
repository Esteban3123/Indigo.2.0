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

Public Class FrmReportResultOperation
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _FillingReportType
        End Get
    End Property


    Private _FillingDetailType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingDetailType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingDetailType Is Nothing Then
                _FillingDetailType = New List(Of Tuple(Of Integer, String))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(1, "Mano de Obra"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(2, "Ventas"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(3, "Suministros"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(4, "Consumo"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(5, "Gastos Generales"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(6, "Depreciación"))
            End If
            Return _FillingDetailType
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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnitStart
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, 1}
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductionCenterByStatusCenterTypeReport, filter)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnitEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, 1}
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductionCenterByStatusCenterTypeReport, filter)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoGroup = Nothing
        _FillingDetailType = Nothing
        _FillingReportType = Nothing
    End Sub

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            'If INDGleReportType.EditValue = 1 Then
            AsyncLoader(True)
            Dim reporte As New rptListResultOperation
            reporte.ParametrosReporte = New Object() {INDCdnMonthYearStart.GetMonth,
                                                      INDCdnMonthYearStart.GetYear,
                                                      INDCdnMonthYearEnd.GetMonth,
                                                      INDCdnMonthYearEnd.GetYear,
                                                      INDSleGroupStart.TextEditValue,
                                                      INDSleGroupEnd.TextEditValue}

            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
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
                Me.INDCdnMonthYearStart.Focus()
            End If
            'Else
            '    AsyncLoader(True)
            '    Dim reporte As New rptListResultOperationDetail

            '    Dim INDDateStart As Date = INDCdnMonthYearStart.GetYear & "-" & INDCdnMonthYearStart.GetMonth & "-01"
            '    Dim INDDateEnd As Date = INDCdnMonthYearEnd.GetYear & "-" & INDCdnMonthYearEnd.GetMonth & "-01"
            '    INDDateEnd = (INDDateEnd.AddDays(-INDDateEnd.Day + 1).AddMonths(1).AddDays(-1))

            '    reporte.ParametrosReporte = New Object() {INDDateStart,
            '                                              INDDateEnd,
            '                                              INDGleDetailType.EditValue,
            '                                              INDSleGroupStart.TextEditValue,
            '                                              INDSleGroupEnd.TextEditValue}

            '    INDDvReport.DocumentSource = reporte
            '    Await reporte.CargarDataSourceAsync()
            '    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
            '        reporte.CreateDocument()
            '    End If
            '    AsyncLoader(False)
            '    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
            '        Me.INDLcBase.Visible = False
            '        Me.INDCncReport.Visible = False
            '        Me.INDPcReport.Visible = True
            '        INDDvReport.Show()
            '    Else
            '        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            '        Me.INDCdnMonthYearStart.Focus()
            '    End If
            'End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDCdnMonthYearStart.Focus()
    End Sub


    Private Sub FrmReportResultOperation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleDetailType.Properties.DataSource = FillingDetailType
        Me.INDGleDetailType.EditValue = 1

        Me.INDGleReportType.Properties.DataSource = FillingReportType
        Me.INDGleReportType.EditValue = 1
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
        If INDSleGroupStart.TextEditValue <> String.Empty And INDSleGroupEnd.TextEditValue = String.Empty Or INDSleGroupEnd.TextEditValue <> String.Empty And INDSleGroupStart.TextEditValue = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf (INDSleGroupEnd.TextEditValue < INDSleGroupStart.TextEditValue) AndAlso (INDSleGroupEnd.TextEditValue.Length < INDSleGroupStart.TextEditValue.Length) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If

        If INDCdnMonthYearStart.GetYear IsNot Nothing And INDCdnMonthYearEnd.GetYear IsNot Nothing Then
            If INDCdnMonthYearStart.GetYear = INDCdnMonthYearEnd.GetYear Then
                If Me.INDCdnMonthYearStart.GetMonth > INDCdnMonthYearEnd.GetMonth Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                    Me.INDCdnMonthYearStart.Focus()
                    Validations = False
                End If
            End If
        End If

        If INDCdnMonthYearStart.GetYear IsNot Nothing And INDCdnMonthYearEnd.GetYear IsNot Nothing Then
            If Me.INDCdnMonthYearStart.GetYear <> INDCdnMonthYearEnd.GetYear Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar el mismo año para el Rango"
                Me.INDCdnMonthYearStart.Focus()
                Validations = False
            End If
        End If

        Return Validations
    End Function


    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.EditValue Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.EditValue Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' al cambiar el tipo de documento se muestra el filtro del documento seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReportType.EditValueChanged
        If INDGleReportType.EditValue = 1 Then
            INDLciDetailType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleDetailType.EditValue = Nothing
        Else
            INDLciDetailType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleDetailType.EditValue = 1
        End If
    End Sub
End Class
#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportActivities

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para el cargue de las Actividades
    ''' </summary>
    Public Property ProoftCloseXpoActivity As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para el cargue de los Servicios
    ''' </summary>
    Public Property ProoftCloseXpoCUPSEntity As XPInstantFeedbackSource

    ''' <summary>
    ''' Filtros a enviar al procedimiento con el fin de obtener los datos
    ''' </summary>
    Dim filters As New Dictionary(Of String, String)

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleActivityStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleActivityStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleActivityStart.QueryPopUp
        If INDSleActivityStart.Datasource Is Nothing Then
            LoadXpoActivityStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleActivityEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleActivityEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleActivityEnd.QueryPopUp
        If INDSleActivityEnd.Datasource Is Nothing Then
            LoadXpoActivityEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleCUPSEntityStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCUPSEntityStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCUPSEntityStart.QueryPopUp
        If INDSleCUPSEntityStart.Datasource Is Nothing Then
            LoadXpoCUPSEntityStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleCUPSEntityEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCUPSEntityEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCUPSEntityEnd.QueryPopUp
        If INDSleCUPSEntityEnd.Datasource Is Nothing Then
            LoadXpoCUPSEntityEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Dim reporte As New rptReportActivities
                reporte.ParametrosReporte = New Object() {filters}
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDSleActivityStart.Focus()
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoActivity = Nothing
        ProoftCloseXpoCUPSEntity = Nothing
    End Sub

#End Region

#Region "Methods"

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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'validaciones controles de actividades
        If INDSleActivityStart.EditValue IsNot Nothing And INDSleActivityEnd.EditValue Is Nothing Or INDSleActivityStart.EditValue Is Nothing And INDSleActivityEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblActivity.Text)
            Me.INDSleActivityStart.Focus()
            Validations = False
        ElseIf INDSleActivityStart.EditValue > INDSleActivityEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblActivity.Text)
            Me.INDSleActivityStart.Focus()
            Validations = False
        End If

        'validaciones controles de servicios
        If INDSleCUPSEntityStart.EditValue IsNot Nothing And INDSleCUPSEntityEnd.EditValue Is Nothing Or INDSleCUPSEntityStart.EditValue Is Nothing And INDSleCUPSEntityEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCUPSEntity.Text)
            Me.INDSleCUPSEntityStart.Focus()
            Validations = False
        ElseIf INDSleCUPSEntityStart.EditValue > INDSleCUPSEntityEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCUPSEntity.Text)
            Me.INDSleCUPSEntityStart.Focus()
            Validations = False
        End If

        If Validations Then
            filters = New Dictionary(Of String, String)
            filters.Add("ActivityStart", INDSleActivityStart.EditValue)
            filters.Add("ActivityEnd", INDSleActivityEnd.EditValue)
            filters.Add("CUPSEntityStart", INDSleCUPSEntityStart.EditValue)
            filters.Add("CUPSEntityEnd", INDSleCUPSEntityEnd.EditValue)
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleActivitytart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoActivityStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoActivity = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostActivities)
            INDSleActivityStart.Datasource = ProoftCloseXpoActivity
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleActivityEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoActivityEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoActivity = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostActivities)
            INDSleActivityEnd.Datasource = ProoftCloseXpoActivity
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCUPSEntitytart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCUPSEntityStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCUPSEntity = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsEntity)
            INDSleCUPSEntityStart.Datasource = ProoftCloseXpoCUPSEntity
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCUPSEntityEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCUPSEntityEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCUPSEntity = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsEntity)
            INDSleCUPSEntityEnd.Datasource = ProoftCloseXpoCUPSEntity
        End Using
    End Sub

#End Region

End Class
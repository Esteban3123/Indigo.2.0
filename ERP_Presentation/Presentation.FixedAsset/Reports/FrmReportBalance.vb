#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
#End Region

Public Class FrmReportBalance

#Region "Properties"
    Public Property ProoftCloseXpoPlate As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCatalog As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoLocation As XPCollection
    Public Property ProoftCloseXpoResponsible As XPInstantFeedbackSource

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Balance"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Kardex"))
            End If
            Return _FillingTypeReport
        End Get
    End Property
    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

        'Valida placas
        If INDSleInitialPlate.EditValue Is Nothing And INDSleFinalPlate.EditValue IsNot Nothing Or INDSleFinalPlate.EditValue Is Nothing And INDSleInitialPlate.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblPlates.Text)
            Me.INDSleInitialPlate.Focus()
            Validations = False
        ElseIf INDSleFinalPlate.EditValue < INDSleInitialPlate.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPlates.Text)
            Me.INDSleInitialPlate.Focus()
            Validations = False
        End If
        'Valida catalogos
        If INDSleInitialCatalog.EditValue Is Nothing And INDSleFinalCatalog.EditValue IsNot Nothing Or INDSleFinalCatalog.EditValue Is Nothing And INDSleInitialCatalog.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCatalogs.Text)
            Me.INDSleInitialCatalog.Focus()
            Validations = False
        ElseIf INDSleFinalCatalog.EditValue < INDSleInitialCatalog.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCatalogs.Text)
            Me.INDSleInitialCatalog.Focus()
            Validations = False
        End If
        'Valida grupos
        If INDSleInitialGroup.EditValue Is Nothing And INDSleFinalGroup.EditValue IsNot Nothing Or INDSleFinalGroup.EditValue Is Nothing And INDSleInitialGroup.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroups.Text)
            Me.INDSleInitialGroup.Focus()
            Validations = False
        ElseIf INDSleFinalGroup.EditValue < INDSleInitialGroup.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroups.Text)
            Me.INDSleInitialGroup.Focus()
            Validations = False
        End If
        'Valida ubicaciones
        If INDSleInitialLocation.EditValue Is Nothing And INDSleFinalLocation.EditValue IsNot Nothing Or INDSleFinalLocation.EditValue Is Nothing And INDSleInitialLocation.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblLocations.Text)
            Me.INDSleInitialLocation.Focus()
            Validations = False
        ElseIf INDSleFinalLocation.EditValue < INDSleInitialLocation.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblLocations.Text)
            Me.INDSleInitialLocation.Focus()
            Validations = False
        End If
        Return Validations
    End Function

#End Region

#Region "Eventos"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoPlate = Nothing
        ProoftCloseXpoCatalog = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoLocation = Nothing
        ProoftCloseXpoResponsible = Nothing
        _FillingTypeReport = Nothing
    End Sub

    Private Sub FrmReportBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Cargar GridLookUpEdit
        Me.INDGleType.Properties.DataSource = FillingTypeReport

        'Dar un valor por defecto a los GridLookEdit
        INDGleType.EditValue = 1

    End Sub
#End Region

#Region "Methods"



    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialPlate
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialPlate()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSleInitialPlate.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalPlate
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalPlate()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSleFinalPlate.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialCatalog
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialCatalog()
        Using msearch As New MBusqueda
            ProoftCloseXpoCatalog = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentCatalog)
            INDSleInitialCatalog.Datasource = ProoftCloseXpoCatalog
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalCatalog
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalCatalog()
        Using msearch As New MBusqueda
            ProoftCloseXpoCatalog = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentCatalog)
            INDSleFinalCatalog.Datasource = ProoftCloseXpoCatalog
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialGroup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialGroup()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetMainAccount)
            INDSleInitialGroup.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalGroup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalGroup()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetMainAccount)
            INDSleFinalGroup.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialLocation
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialLocation()
        Using msearch As New MBusqueda
            ProoftCloseXpoLocation = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetLocation)
            INDSleInitialLocation.Datasource = ProoftCloseXpoLocation
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalLocation
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalLocation()
        Using msearch As New MBusqueda
            ProoftCloseXpoLocation = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetLocation)
            INDSleFinalLocation.Datasource = ProoftCloseXpoLocation
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialResponsible
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialResponsible()
        Using msearch As New MBusqueda
            ProoftCloseXpoResponsible = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetResponsible)
            INDSleInitialResponsible.Datasource = ProoftCloseXpoResponsible
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalResponsible
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalResponsible()
        Using msearch As New MBusqueda
            ProoftCloseXpoResponsible = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetResponsible)
            INDSleFinalResponsible.Datasource = ProoftCloseXpoResponsible
        End Using
    End Sub





    Private Sub INDSleInitialCatalog_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialCatalog.QueryPopUp
        If INDSleInitialCatalog.Datasource Is Nothing Then
            LoadXpoInitialCatalog()
        End If
    End Sub



    Private Sub INDSleInitialPlate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialPlate.QueryPopUp
        If INDSleInitialPlate.Datasource Is Nothing Then
            LoadXpoInitialPlate()
        End If
    End Sub

    Private Sub INDSleFinalPlate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalPlate.QueryPopUp
        If INDSleFinalPlate.Datasource Is Nothing Then
            LoadXpoFinalPlate()
        End If
    End Sub

    Private Sub INDSleFinalCatalog_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalCatalog.QueryPopUp
        If INDSleFinalCatalog.Datasource Is Nothing Then
            LoadXpoFinalCatalog()
        End If
    End Sub

    Private Sub INDSleInitialGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialGroup.QueryPopUp
        If INDSleInitialGroup.Datasource Is Nothing Then
            LoadXpoInitialGroup()
        End If
    End Sub

    Private Sub INDSleFinalGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalGroup.QueryPopUp
        If INDSleFinalGroup.Datasource Is Nothing Then
            LoadXpoFinalGroup()
        End If
    End Sub

    Private Sub INDSleInitialLocation_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialLocation.QueryPopUp
        If INDSleInitialLocation.Datasource Is Nothing Then
            LoadXpoInitialLocation()
        End If
    End Sub

    Private Sub INDSleFinalLocation_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalLocation.QueryPopUp
        If INDSleFinalLocation.Datasource Is Nothing Then
            LoadXpoFinalLocation()
        End If
    End Sub

    Private Sub INDGleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleType.EditValueChanged
        If INDGleType.EditValue = 2 Then
            ''kardex
            INDLciLblResponsible.Visibility = False
            INDLciInitialResponsible.Visibility = False
            INDLciFinalResponsible.Visibility = False
            INDSleInitialPlate.EditValue = Nothing
            INDSleFinalPlate.EditValue = Nothing
            INDSleInitialCatalog.EditValue = Nothing
            INDSleFinalCatalog.EditValue = Nothing
            INDSleInitialGroup.EditValue = Nothing
            INDSleFinalGroup.EditValue = Nothing
            INDSleInitialLocation.EditValue = Nothing
            INDSleFinalLocation.EditValue = Nothing
            INDSleInitialResponsible.EditValue = Nothing
            INDSleFinalResponsible.EditValue = Nothing

        ElseIf INDGleType.EditValue = 1 Then
            ''balance
            INDLciLblResponsible.Visibility = True
            INDLciInitialResponsible.Visibility = True
            INDLciFinalResponsible.Visibility = True
            INDSleInitialPlate.EditValue = Nothing
            INDSleFinalPlate.EditValue = Nothing
            INDSleInitialCatalog.EditValue = Nothing
            INDSleFinalCatalog.EditValue = Nothing
            INDSleInitialGroup.EditValue = Nothing
            INDSleFinalGroup.EditValue = Nothing
            INDSleInitialLocation.EditValue = Nothing
            INDSleFinalLocation.EditValue = Nothing
            INDSleInitialResponsible.EditValue = Nothing
            INDSleFinalResponsible.EditValue = Nothing

        End If


    End Sub

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            If INDGleType.EditValue = 1 Then
                'Si el tipo de reporte es balance
                AsyncLoader(True)
                Dim reporte As New rptFixedAssetBalance
                reporte.ParametrosReporte = New Object() {INDCdnDate.GetYear,
                                                          INDCdnDate.GetMonth,
                                                          INDSleInitialPlate.EditValue,
                                                          INDSleFinalPlate.EditValue,
                                                          INDSleInitialCatalog.EditValue,
                                                          INDSleFinalCatalog.EditValue,
                                                          INDSleInitialGroup.EditValue,
                                                          INDSleFinalGroup.EditValue,
                                                          INDSleInitialLocation.EditValue,
                                                          INDSleFinalLocation.EditValue}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSource1()

                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    AsyncLoader(False)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDate.Focus()
                End If


            ElseIf INDGleType.EditValue = 2 Then
                'Si el tipo de reporte es kardex
                AsyncLoader(True)
                Dim reporte As New rptFixedAssetKardex
                reporte.ParametrosReporte = New Object() {INDCdnDate.GetYear,
                                                          INDCdnDate.GetMonth,
                                                          INDSleInitialPlate.EditValue,
                                                          INDSleFinalPlate.EditValue,
                                                          INDSleInitialCatalog.EditValue,
                                                          INDSleFinalCatalog.EditValue,
                                                          INDSleInitialGroup.EditValue,
                                                          INDSleFinalGroup.EditValue,
                                                          INDSleInitialLocation.EditValue,
                                                          INDSleFinalLocation.EditValue,
                                                          INDSleInitialResponsible.EditValue,
                                                          INDSleFinalResponsible.EditValue}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSource1()

                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    AsyncLoader(False)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDate.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDCdnDate.Focus()
    End Sub

    Private Sub INDSleInitialResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialResponsible.QueryPopUp
        If INDSleInitialResponsible.Datasource Is Nothing Then
            LoadXpoInitialResponsible()
        End If
    End Sub

    Private Sub INDSleFinalResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalResponsible.QueryPopUp
        If INDSleFinalResponsible.Datasource Is Nothing Then
            LoadXpoFinalResponsible()
        End If
    End Sub
#End Region
End Class
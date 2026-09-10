#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

#End Region

Public Class FrmReportContracts


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ListTypes = New List(Of Tuple(Of String, Byte))
        ListTypes.Add(New Tuple(Of String, Byte)("EPS Contributivo", 1))
        ListTypes.Add(New Tuple(Of String, Byte)("EPS Subsidiado", 2))
        ListTypes.Add(New Tuple(Of String, Byte)("ET Vinculados Municipios", 3))
        ListTypes.Add(New Tuple(Of String, Byte)("ET Vinculados Departamentos", 4))
        ListTypes.Add(New Tuple(Of String, Byte)("ARL Riesgos Laborales", 5))
        ListTypes.Add(New Tuple(Of String, Byte)("MP Medicina Prepagada", 6))
        ListTypes.Add(New Tuple(Of String, Byte)("IPS Privada", 7))
        ListTypes.Add(New Tuple(Of String, Byte)("IPS Publica", 8))
        ListTypes.Add(New Tuple(Of String, Byte)("Regimen Especial", 9))
        ListTypes.Add(New Tuple(Of String, Byte)("Accidentes de transito", 10))
        ListTypes.Add(New Tuple(Of String, Byte)("Fosyga", 11))
        ListTypes.Add(New Tuple(Of String, Byte)("Otros", 12))

        Dim repositoryTmp = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        repositoryTmp.AutoHeight = False
        repositoryTmp.Name = "INDIcbSource"
        INDSleHealAdministrator.Grid.RepositoryItems.Add(repositoryTmp)
        For Each item In ListTypes
            repositoryTmp.Items.Add(New DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Item1, item.Item2, -1))
        Next
        GridColumn7.ColumnEdit = repositoryTmp

    End Sub

#Region "Fields"
    Private ListTypes As List(Of Tuple(Of String, Byte))
    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoContracts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoHealAdministrator As XPInstantFeedbackSource
    Public Property ProoftCloseXpoContractEntity As XPInstantFeedbackSource
    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Vigente"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Suspendido"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Terminado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

#End Region

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
    ''' metodo para Cargar el data source Del Control INDSleContracts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoContracts()
        Using msearch As New MBusqueda
            ProoftCloseXpoContracts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListContract)
            INDSleContracts.Datasource = ProoftCloseXpoContracts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleHealAdministrator
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoHealtAdministrator()
        Using msearch As New MBusqueda
            ProoftCloseXpoHealAdministrator = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListHealthAdministratorByStatus, True)
            INDSleHealAdministrator.Datasource = ProoftCloseXpoHealAdministrator
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleContractEntity
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoContractEntity()
        Using msearch As New MBusqueda
            ProoftCloseXpoContractEntity = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListContractEntityByStatus, True)
            INDSleContractEntity.Datasource = ProoftCloseXpoContractEntity
        End Using
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleContracts
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleContracts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleContracts.QueryPopUp
        If INDSleContracts.Datasource Is Nothing Then
            LoadXpoContracts()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleHealAdministrator
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleHealAdministrator_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleHealAdministrator.QueryPopUp
        If INDSleHealAdministrator.Datasource Is Nothing Then
            LoadXpoHealtAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleContractEntity
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleContractEntity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleContractEntity.QueryPopUp
        If INDSleContractEntity.Datasource Is Nothing Then
            LoadXpoContractEntity()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCnc.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ListTypes = Nothing
    End Sub


    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        AsyncLoader(True)
        Dim reporte As New rptContract
        reporte.ParametrosReporte = New Object() {INDSleContracts.EditValue,
                                              INDSleHealAdministrator.EditValue,
                                              INDSleContractEntity.EditValue,
                                              INDTxeNumber.EditValue,
                                              INDGleStatus.EditValue}
        INDDvDocumentViewer.DocumentSource = reporte
        reporte.CargarDataSource()
        reporte.CreateDocument(True)
        AsyncLoader(False)
        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
            Me.INDLcBase.Visible = False
            Me.INDCnc.Visible = False
            Me.INDPcDocumentViewer.Visible = True
            INDDvDocumentViewer.Show()
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDSleContracts.Focus()
        End If

    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportContracts_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleStatus.EditValue = 4
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportContracts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        'Me.INDSleContracts.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInventoryProduct
        'Me.INDSleHealAdministrator.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInventoryProduct
        'Me.INDSleContractEntity.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

End Class
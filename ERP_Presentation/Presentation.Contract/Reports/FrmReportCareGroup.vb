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

Public Class FrmReportCareGroup

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoCareGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoContracts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoProcedures As XPInstantFeedbackSource
    Public Property ProoftCloseXpoProducts As XPInstantFeedbackSource
    Private _FillingTypeCareGroup As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeCareGroup As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeCareGroup Is Nothing Then
                _FillingTypeCareGroup = New List(Of Tuple(Of Integer, String))
                _FillingTypeCareGroup.Add(New Tuple(Of Integer, String)(1, "EAPB con Contrato"))
                _FillingTypeCareGroup.Add(New Tuple(Of Integer, String)(2, "EAPB con Contrato"))
                _FillingTypeCareGroup.Add(New Tuple(Of Integer, String)(3, "Particulares"))
                _FillingTypeCareGroup.Add(New Tuple(Of Integer, String)(4, "Aseguradoras"))
                _FillingTypeCareGroup.Add(New Tuple(Of Integer, String)(5, "Todos"))
            End If
            Return _FillingTypeCareGroup
        End Get
    End Property
    Private _FillingTypeLiquidation As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeLiquidation As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeLiquidation Is Nothing Then
                _FillingTypeLiquidation = New List(Of Tuple(Of Integer, String))
                _FillingTypeLiquidation.Add(New Tuple(Of Integer, String)(1, "Pago por Servicios"))
                _FillingTypeLiquidation.Add(New Tuple(Of Integer, String)(2, "Capitación"))
                _FillingTypeLiquidation.Add(New Tuple(Of Integer, String)(3, "Factura Global"))
                _FillingTypeLiquidation.Add(New Tuple(Of Integer, String)(4, "Capitación Global"))
                _FillingTypeLiquidation.Add(New Tuple(Of Integer, String)(5, "Todos"))
            End If
            Return _FillingTypeLiquidation
        End Get
    End Property
    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(0, "Inactivo"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Activo"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Todos"))
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
    ''' metodo para Cargar el data source Del Control INDSleCareGroup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCareGroups()
        Using msearch As New MBusqueda
            ProoftCloseXpoCareGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCareGroup)
            INDSleCareGroup.Datasource = ProoftCloseXpoCareGroup
        End Using
    End Sub

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
    ''' metodo para Cargar el data source Del Control INDSleProcedureTemplate
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProcedures()
        Using msearch As New MBusqueda
            ProoftCloseXpoProcedures = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProcedureTemplateByStatus, True)
            INDSleProcedureTemplate.Datasource = ProoftCloseXpoProcedures
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductsTemplate
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProducts()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductTemplateByStatus, True)
            INDSleProductsTemplate.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCareGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If INDSleCareGroup.Datasource Is Nothing Then
            LoadXpoCareGroups()
        End If
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
    ''' se ejecuta en el evento querypopup del control INDSleProcedureTemplate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProcedureTemplate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProcedureTemplate.QueryPopUp
        If INDSleProcedureTemplate.Datasource Is Nothing Then
            LoadXpoProcedures()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductsTemplate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductsTemplate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductsTemplate.QueryPopUp
        If INDSleProductsTemplate.Datasource Is Nothing Then
            LoadXpoProducts()
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
    End Sub


    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        AsyncLoader(True)
        Dim reporte As New rptCareGroup
        reporte.ParametrosReporte = New Object() {INDSleCareGroup.EditValue,
                                              INDGleTypeCareGroup.EditValue,
                                              INDSleContracts.EditValue,
                                              INDGleLiquidationType.EditValue,
                                              INDSleProcedureTemplate.EditValue,
                                              INDSleProductsTemplate.EditValue,
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
    Private Sub FrmReportCareGroup_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeCareGroup.Properties.DataSource = FillingTypeCareGroup
        Me.INDGleLiquidationType.Properties.DataSource = FillingTypeLiquidation
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeCareGroup.EditValue = 5
        Me.INDGleLiquidationType.EditValue = 5
        Me.INDGleStatus.EditValue = 2
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCareGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
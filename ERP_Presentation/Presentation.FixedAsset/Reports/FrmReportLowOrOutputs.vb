#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
#End Region

Public Class FrmReportLowOrOutputs
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpo As XPInstantFeedbackSource
    Private _FillingState As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private ReadOnly Property FillingState As List(Of Tuple(Of Integer, String))
        Get
            If _FillingState Is Nothing Then
                _FillingState = New List(Of Tuple(Of Integer, String))
                _FillingState.Add(New Tuple(Of Integer, String)(1, "Registrado"))
                _FillingState.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingState.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingState.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingState
        End Get
    End Property

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
#End Region

#Region "Methods"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        If INDDateEditDateStart.EditValue Is Nothing Or INDDateEditDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateEditDateStart.EditValue > INDDateEditDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpo()
        Using msearch As New MBusqueda
            ProoftCloseXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEntry)
            'INDSleThirdPartyStart.Datasource = ProoftCloseXpo
        End Using
    End Sub
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpo = Nothing
        _FillingState = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportLowOrOutputs_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleState.Properties.DataSource = FillingState

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleState.EditValue = 4
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptDetailIncomeFixedAsset
            reporte.ParametrosReporte = New Object() {
                INDDateEditDateStart.EditValue,
                INDDateEditDateEnd.EditValue,
                INDGleState.EditValue,
                INDSleOutletStart.EditValue,
                INDSleOutletEnd.EditValue
            }
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
                Me.INDDateEditDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportLowOrOutputs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        'Me.INDSleDocumentStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCashReceiptsByCode

    End Sub
#End Region
End Class
#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Presentation.Base
Imports System.Windows.Forms
Imports System.ComponentModel
Imports Domain.Entities
Imports Presentation.Taxes.MVP
Imports Presentation.Controls

#End Region

Public Class FrmReportTaxesLiquidation
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    ''' <summary>
    ''' Lista de predios
    ''' </summary>
    ''' <remarks></remarks>
    Private _listGetTexedProperties As List(Of TaxesProperty)

    Private _taskListGetAllTaxedProperties As Task(Of List(Of TaxesProperty))
#End Region

#Region "Properties"
    Public Property ProoftCloseXpoProperty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoAddres As XPInstantFeedbackSource
    Public Property ProoftCloseXpoXpoPropertyOwner As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida TaxesProperty
        If INDSleTaxesPropertyStart.EditValue Is Nothing And INDSleTaxesPropertyEnd.EditValue IsNot Nothing Or INDSleTaxesPropertyEnd.EditValue Is Nothing And INDSleTaxesPropertyStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblTaxesProperty.Text)
            Me.INDSleTaxesPropertyStart.Focus()
            Validations = False
        ElseIf INDSleTaxesPropertyEnd.EditValue < INDSleTaxesPropertyStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblTaxesProperty.Text)
            Me.INDSleTaxesPropertyStart.Focus()
            Validations = False
        End If


        ''Valida Grupo
        'If INDSleGroupStart.EditValue Is Nothing And INDSleGroupEnd.EditValue IsNot Nothing Or INDSleGroupEnd.EditValue Is Nothing And INDSleGroupStart.EditValue IsNot Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
        '    Me.INDSleGroupStart.Focus()
        '    Validations = False
        'ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
        '    Me.INDSleGroupStart.Focus()
        '    Validations = False
        'End If

        ''Valida Artículo
        'If INDSleItemStart.EditValue Is Nothing And INDSleItemEnd.EditValue IsNot Nothing Or INDSleItemEnd.EditValue Is Nothing And INDSleItemStart.EditValue IsNot Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblItem.Text)
        '    Me.INDSleItemStart.Focus()
        '    Validations = False
        'ElseIf INDSleItemEnd.EditValue < INDSleItemStart.EditValue Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblItem.Text)
        '    Me.INDSleItemStart.Focus()
        '    Validations = False
        'End If

        Return Validations
    End Function

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
    ''' Propiedad que se usa para cargar la Dupla del tipo de reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Detalldo"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingTypeReport
        End Get
    End Property
#End Region

#Region "Eventos"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        _listGetTexedProperties = Nothing
        _taskListGetAllTaxedProperties = Nothing
    End Sub
#Region "Map"

    ''' <summary>
    ''' Aqui se captura los errores del mapa
    ''' </summary>
    Private Sub IndigoMapControl_ErrorEventLog(sender As Object, e As Controls.ErrorEventArgs) Handles IndigoMapControl.ErrorEventLog
        Mensaje(EeventViewerImages.Advertencia) = e.Message
    End Sub
    ''' <summary>
    ''' Aqui se lanza el reporte con los predios exportados por el mapa
    ''' </summary>
    Private Async Sub IndigoMapControl_ExportResult(sender As Object, e As Controls.ExportResultEventArgs) Handles IndigoMapControl.ExportResult
        If e.Result.Count > 0 Then
            Dim listTaxes As New List(Of String)
            For Each item In e.Result()
                listTaxes.Add(CType(item, Object).Code)
            Next
            'MsgBox(CType(e.Result(0), Object).Code)
            'MessageBox.Show(listTaxes(0).ToString())
            Me.PceMap.ClosePopup()
            'Dim reportdef As New Reporter.rptSubMapListTaxes
            Dim reportdef As New rptListTaxes
            reportdef.ParametrosReporte = New Object() {listTaxes}

            Await reportdef.CargarDataSource1()
            If DirectCast(reportdef.DataSource, ICollection).Count > 0 Then
                ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm)
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSleTaxesPropertyStart.Focus()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Aqui se lanza el mapa
    ''' </summary>
    Private Async Sub BtnShowMap_Click(sender As Object, e As EventArgs) Handles BtnShowMap.Click
        If Me._listGetTexedProperties Is Nothing OrElse Me._listGetTexedProperties.Count = 0 Then
            Me.Cursor = Cursors.WaitCursor
            Me._listGetTexedProperties = Await Me._taskListGetAllTaxedProperties
            Me.IndigoMapControl.Datasource = Me._listGetTexedProperties
        End If
        Me.Cursor = Cursors.Default
        Me.PceMap.ShowPopup()
    End Sub

#End Region

    ''' <summary>
    ''' cargamos el datasource de la Subcuenta Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePropertyOwner_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlePropertyOwner.QueryPopUp
        If INDSlePropertyOwner.Datasource Is Nothing Then
            LoadXpoPropertyOwner()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de la TaxesPropertyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTaxesPropertyStart_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleTaxesPropertyStart.QueryPopUp
        If INDSleTaxesPropertyStart.Datasource Is Nothing Then
            LoadXpoTaxesPropertytStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de la TaxesPropertyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTaxesPropertyEnd_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleTaxesPropertyEnd.QueryPopUp
        If INDSleTaxesPropertyEnd.Datasource Is Nothing Then
            LoadXpoTaxesPropertytEnd()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de la TaxesAddressPropertyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTaxesAddressPropertyStart_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleTaxesAddressPropertyStart.QueryPopUp
        If INDSleTaxesAddressPropertyStart.Datasource Is Nothing Then
            LoadXpoTaxesPropertyAddresStart()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de la TaxesAddressPropertyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTaxesAddressPropertyEnd_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleTaxesAddressPropertyEnd.QueryPopUp
        If INDSleTaxesAddressPropertyEnd.Datasource Is Nothing Then
            LoadXpoTaxesPropertytAddresEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbReportGenerate_Click(sender As Object, e As EventArgs) Handles INDSbReportGenerate.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            Dim reporte As New rptListTaxes

            reporte.ParametrosReporte = New Object() {INDSleTaxesPropertyStart.EditValue, INDSleTaxesPropertyEnd.EditValue,
                                                      INDSleTaxesAddressPropertyStart.EditValue, INDSleTaxesAddressPropertyEnd.EditValue,
                                                      INDSlePropertyOwner.EditValue}

            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSource1()
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
                AsyncLoader(False)
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSleTaxesPropertyStart.Focus()
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
        Me.INDSlePropertyOwner.Focus()
    End Sub

    Private Sub GetAllTaxedProperties()
        Using model As New MTaxesProperty(Me.Tag)
            Me._taskListGetAllTaxedProperties = model.GetAllTaxedProperties()
        End Using
    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTaxesLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.PccMap.Size = New System.Drawing.Size(Screen.FromControl(Me).WorkingArea.Width, Screen.FromControl(Me).WorkingArea.Height - 200)
        Me.IndigoMapControl.InitializeMap()
        GetAllTaxedProperties()
        'Me.INDSleSubAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleSubAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        Me.INDSleTaxesPropertyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetTaxesPropertyAsync
        Me.INDSleTaxesPropertyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetTaxesPropertyAsync
        Me.INDSlePropertyOwner.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync

        'INDSleSubAccountStart.View.OptionsView.ShowGroupPanel = False
        INDSleTaxesPropertyEnd.View.OptionsView.ShowGroupPanel = False
        INDSleTaxesPropertyStart.View.OptionsView.ShowGroupPanel = False
        INDSlePropertyOwner.View.OptionsView.ShowGroupPanel = False
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePropertyOwner
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPropertyOwner()
        Using msearch As New MBusqueda
            ProoftCloseXpoXpoPropertyOwner = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSlePropertyOwner.Datasource = ProoftCloseXpoXpoPropertyOwner
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTaxesPropertyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTaxesPropertytStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoProperty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTaxesPropertyReport)
            INDSleTaxesPropertyStart.Datasource = ProoftCloseXpoProperty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTaxesPropertyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTaxesPropertytEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoProperty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTaxesPropertyReport)
            INDSleTaxesPropertyEnd.Datasource = ProoftCloseXpoProperty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTaxesAddressPropertyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTaxesPropertyAddresStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoAddres = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTaxesPropertyReport)
            INDSleTaxesAddressPropertyStart.Datasource = ProoftCloseXpoAddres
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTaxesAddressPropertyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTaxesPropertytAddresEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoAddres = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTaxesPropertyReport)
            INDSleTaxesAddressPropertyEnd.Datasource = ProoftCloseXpoAddres
        End Using
    End Sub
#End Region
End Class
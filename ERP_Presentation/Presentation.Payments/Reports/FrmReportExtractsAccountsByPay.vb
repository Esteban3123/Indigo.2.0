#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

#End Region

Public Class FrmReportExtractsAccountsByPay

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"

    Public Property LoadSearchLookUpSupplier As XPInstantFeedbackSource
    Public Property LoadSearchLookUpAccountPayable As XPInstantFeedbackSource
    Private SelectAllSupplier = True
    Private SelectAllInvoice = True
    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido Tercero"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido Documento"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(3, "Detallado"))
            End If
            Return _LoadTypeReport
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

#Region "XPO"

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSupplierStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSupplierStart()
        Using msearch As New MBusqueda
            LoadSearchLookUpSupplier = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierReport)
            INDSleSupplierStart.Datasource = LoadSearchLookUpSupplier
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSupplierEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSupplierEnd()
        Using msearch As New MBusqueda
            LoadSearchLookUpSupplier = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierReport)
            INDSleSupplierEnd.Datasource = LoadSearchLookUpSupplier
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInvoiceStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInvoiceStart()
        Dim criteria As String = Nothing
        If (INDSleSupplierStart.EditValue IsNot Nothing And INDSleSupplierEnd.EditValue IsNot Nothing) Then
            criteria = "IdSupplier.IdThirdParty.Nit >= '" & INDSleSupplierStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleSupplierEnd.EditValue & "'"
        End If
        Using msearch As New MBusqueda
            LoadSearchLookUpAccountPayable = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPaymentsAccountPayableReportByFilter, criteria)
            INDSleInvoiceStart.Datasource = LoadSearchLookUpAccountPayable
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInvoiceEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInvoiceEnd()
        Dim criteria As String = Nothing
        If (INDSleSupplierStart.EditValue IsNot Nothing And INDSleSupplierEnd.EditValue IsNot Nothing) Then
            criteria = "IdSupplier.IdThirdParty.Nit >= '" & INDSleSupplierStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleSupplierEnd.EditValue & "'"
        End If
        Using msearch As New MBusqueda
            LoadSearchLookUpAccountPayable = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPaymentsAccountPayableReportByFilter, criteria)
            INDSleInvoiceEnd.Datasource = LoadSearchLookUpAccountPayable
        End Using
    End Sub

#End Region

#Region "Events"

#Region "Init"

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportExtractsAccountsByPay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleSupplierStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
        Me.INDSleSupplierEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
        Me.INDSleInvoiceStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountsPayableByBillNumber
        Me.INDSleInvoiceEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountsPayableByBillNumber
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportExtractsAccountsByPay_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Disposed"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleSupplierStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplierStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplierStart.QueryPopUp
        If INDSleSupplierStart.Datasource Is Nothing Then
            LoadXpoSupplierStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleSupplierEnd
    ''' </summary>
    ''' <param name="sender">object</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplierEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplierEnd.QueryPopUp
        If INDSleSupplierEnd.Datasource Is Nothing Then
            LoadXpoSupplierEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleInvoiceStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleInvoiceStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInvoiceStart.QueryPopUp
        If INDSleInvoiceStart.Datasource Is Nothing Then
            LoadXpoInvoiceStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleInvoiceEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleInvoiceEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInvoiceEnd.QueryPopUp
        If INDSleInvoiceEnd.Datasource Is Nothing Then
            LoadXpoInvoiceEnd()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' vuelve a cargar el datasource de los facturas de pagos filtrado por proveedores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplierStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleSupplierStart.EditValueChanged
        LoadXpoInvoiceStart()
        LoadXpoInvoiceEnd()
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de los facturas de pagos filtrado por proveedores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplierEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleSupplierEnd.EditValueChanged
        LoadXpoInvoiceStart()
        LoadXpoInvoiceEnd()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerateReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Dim reporte As New rptExtractsAccountsByPay
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDSleSupplierStart.EditValue,
                                                          INDSleSupplierEnd.EditValue,
                                                          INDSleInvoiceStart.EditValue,
                                                          INDSleInvoiceEnd.EditValue,
                                                          INDGleTypeReport.EditValue}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBaseHome.Visible = False
                    Me.INDCtcNavigation.Visible = False
                    Me.INDPcReportView.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = ex.Message
                Me.INDDateStart.Focus()
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack_1() Handles INDCnReturn.ClickBack
        Me.INDLcBaseHome.Visible = True
        Me.INDCtcNavigation.Visible = True
        Me.INDPcReportView.Visible = False
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        'If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
        '    Me.INDDateStart.Focus()
        '    Validations = False
        'ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmFrmReportExtractsAccountsByPay_CompareDate", "Payments"))
        '    Me.INDDateStart.Focus()
        '    Validations = False
        'End If

        'Valida si las fechas, proveedores y Fatcuras son nulas 
        If INDDateStart.EditValue Is Nothing OrElse INDDateEnd.EditValue Is Nothing Then
            If (Me.INDSleSupplierStart.EditValue Is Nothing And Me.INDSleSupplierEnd.EditValue Is Nothing) AndAlso (Me.INDSleInvoiceStart.EditValue Is Nothing And Me.INDSleInvoiceStart.EditValue Is Nothing) Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
                Me.INDDateStart.Focus()
                Validations = False
            End If
        End If

        'Valida si la fecha inicial es mayor a la inicial
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            If Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                Me.INDDateEnd.Focus()
                Validations = False
            End If
            'Valida si alguna de las fechas tiene informacion  y los controles de proveedores y facturas tiene datos ..para informar a obligar a completar las dos fechas
        ElseIf ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) AndAlso (Me.INDSleSupplierStart.EditValue IsNot Nothing And Me.INDSleSupplierEnd.EditValue IsNot Nothing)) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) AndAlso (Me.INDSleInvoiceStart.EditValue IsNot Nothing And Me.INDSleInvoiceStart.EditValue IsNot Nothing)) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        'validaciones controles de proveedores
        If INDSleSupplierStart.EditValue IsNot Nothing And INDSleSupplierEnd.EditValue Is Nothing Or INDSleSupplierStart.EditValue Is Nothing And INDSleSupplierEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblSupplier.Text)
            Me.INDSleSupplierStart.Focus()
            Validations = False
        ElseIf INDSleSupplierStart.EditValue > INDSleSupplierEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblSupplier.Text)
            Me.INDSleSupplierStart.Focus()
            Validations = False
        End If

        'validaciones controles de facturas
        If INDSleInvoiceStart.EditValue IsNot Nothing And INDSleInvoiceEnd.EditValue Is Nothing Or INDSleInvoiceStart.EditValue Is Nothing And INDSleInvoiceEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblInvoice.Text)
            Me.INDSleInvoiceStart.Focus()
            Validations = False
        ElseIf INDSleInvoiceStart.EditValue > INDSleInvoiceEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblInvoice.Text)
            Me.INDSleInvoiceStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

#End Region

End Class
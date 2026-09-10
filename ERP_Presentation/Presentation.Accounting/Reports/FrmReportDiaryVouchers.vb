#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportDiaryVouchers

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para tipos de comprobantes de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoVouchersType As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para comprobantes de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoVouchers As XPInstantFeedbackSource

    ''' <summary>
    ''' Almacena el origen de datos para terceros
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource

    ''' <summary>
    ''' Almacena el origen de datos para centros de costo
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoCostCenter As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Tipos de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingTypeVoucher As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeVoucher As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeVoucher Is Nothing Then
                _FillingTypeVoucher = New List(Of Tuple(Of Integer, String))
                _FillingTypeVoucher.Add(New Tuple(Of Integer, String)(1, "Registrados"))
                _FillingTypeVoucher.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingTypeVoucher.Add(New Tuple(Of Integer, String)(3, "Anulados"))
                _FillingTypeVoucher.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingTypeVoucher
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Comprobantes
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingViews As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingViews As List(Of Tuple(Of Integer, String))
        Get
            If _FillingViews Is Nothing Then
                _FillingViews = New List(Of Tuple(Of Integer, String))
                _FillingViews.Add(New Tuple(Of Integer, String)(1, "Información De Tercero y/o Centro De Costo"))
                _FillingViews.Add(New Tuple(Of Integer, String)(2, "Nivel De Cuenta"))
            End If
            Return _FillingViews
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos del Nivel de Cuentas
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingAccountLevel As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingAccountLevel As List(Of Tuple(Of Integer, String))
        Get
            If _FillingAccountLevel Is Nothing Then
                _FillingAccountLevel = New List(Of Tuple(Of Integer, String))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(1, "Cuenta"))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(2, "SubCuenta"))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(3, "Auxiliar"))
            End If
            Return _FillingAccountLevel
        End Get
    End Property

    Private criteria As String = Nothing

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


    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleVoucherTypeStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoVouchersTypeStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoVouchersType = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTypeVoucherRepor)
            INDSleVoucherTypeStart.Datasource = ProoftCloseXpoVouchersType
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleVoucherTypeEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoVouchersTypeEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoVouchersType = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTypeVoucherRepor)
            INDSleVoucherTypeEnd.Datasource = ProoftCloseXpoVouchersType
        End Using
    End Sub

    ''' <summary>
    ''' Método para cargar el DataSource del Control INDSleVoucherStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoVouchersStart()
        criteria = Nothing

        If (INDGleTypeVoucher.EditValue IsNot Nothing And INDGleTypeVoucher.EditValue <> 4) Then
            criteria = "Status = " & INDGleTypeVoucher.EditValue
        End If

        If (INDSleVoucherTypeStart.EditValue IsNot Nothing And INDSleVoucherTypeEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "IdJournalVoucher.Code >= '" & INDSleVoucherTypeStart.EditValue & "' AND IdJournalVoucher.Code <= '" & INDSleVoucherTypeEnd.EditValue & "'"
            Else
                criteria &= " AND IdJournalVoucher.Code >= '" & INDSleVoucherTypeStart.EditValue & "' AND IdJournalVoucher.Code <= '" & INDSleVoucherTypeEnd.EditValue & "'"
            End If
        End If

        If INDsleBook.EditValue IsNot Nothing Then
            If criteria Is Nothing Then
                criteria = "LegalBookId.Id = " & INDsleBook.EditValue
            Else
                criteria &= "AND LegalBookId.Id = " & INDsleBook.EditValue
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoVouchers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVoucherReportFilter, criteria)
            INDSleVoucherStart.Datasource = ProoftCloseXpoVouchers
        End Using
    End Sub

    ''' <summary>
    ''' Método para cargar el DataSource del Control INDSleVoucherEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoVouchersEnd()
        criteria = Nothing

        If (INDGleTypeVoucher.EditValue IsNot Nothing And INDGleTypeVoucher.EditValue <> 4) Then
            criteria = "Status = " & INDGleTypeVoucher.EditValue
        End If

        If (INDSleVoucherTypeStart.EditValue IsNot Nothing And INDSleVoucherTypeEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "IdJournalVoucher.Code >= '" & INDSleVoucherTypeStart.EditValue & "' AND IdJournalVoucher.Code <= '" & INDSleVoucherTypeEnd.EditValue & "'"
            Else
                criteria &= " AND IdJournalVoucher.Code >= '" & INDSleVoucherTypeStart.EditValue & "' AND IdJournalVoucher.Code <= '" & INDSleVoucherTypeEnd.EditValue & "'"
            End If
        End If

        If INDsleBook.EditValue IsNot Nothing Then
            If criteria Is Nothing Then
                criteria = "LegalBookId.Id = " & INDsleBook.EditValue
            Else
                criteria &= "AND LegalBookId.Id = " & INDsleBook.EditValue
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoVouchers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVoucherReportFilter, criteria)
            INDSleVoucherEnd.Datasource = ProoftCloseXpoVouchers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenterStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCostCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            INDSleCostCenterStart.Datasource = ProoftCloseXpoCostCenter
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenterEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCostCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            INDSleCostCenterEnd.Datasource = ProoftCloseXpoCostCenter
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBook()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            bookXpcollection = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            INDsleBook.Properties.DataSource = bookXpcollection
        End Using
    End Sub

    ''' <summary>
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

        'Valida Tipos De Comprobante
        If INDSleVoucherTypeStart.EditValue Is Nothing And INDSleVoucherTypeEnd.EditValue IsNot Nothing Or INDSleVoucherTypeEnd.EditValue Is Nothing And INDSleVoucherTypeStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblVoucherType.Text)
            Me.INDSleVoucherTypeStart.Focus()
            Validations = False
        ElseIf INDSleVoucherTypeEnd.EditValue < INDSleVoucherTypeStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblVoucherType.Text)
            Me.INDSleVoucherTypeStart.Focus()
            Validations = False
        End If

        'Valida Comprobantes
        If INDSleVoucherStart.EditValue Is Nothing And INDSleVoucherEnd.EditValue IsNot Nothing Or INDSleVoucherEnd.EditValue Is Nothing And INDSleVoucherStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblVoucher.Text)
            Me.INDSleVoucherStart.Focus()
            Validations = False
        ElseIf INDSleVoucherEnd.EditValue < INDSleVoucherStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblVoucher.Text)
            Me.INDSleVoucherStart.Focus()
            Validations = False
        End If

        'Valida Terceros
        If INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Or INDSleThirdPartyEnd.EditValue Is Nothing And INDSleThirdPartyStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyEnd.EditValue < INDSleThirdPartyStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If

        'Valida Centros de Costo
        If INDSleCostCenterStart.EditValue Is Nothing And INDSleCostCenterEnd.EditValue IsNot Nothing Or INDSleCostCenterEnd.EditValue Is Nothing And INDSleCostCenterStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
            Validations = False
        ElseIf INDSleCostCenterEnd.EditValue < INDSleCostCenterStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' Se ejecuta al darle clic al Botón INDSbGenerateReport para generar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        If Me.ValidateControlsReports Then
            AsyncLoader(True)

            Dim reporte As New rptDiaryVouchers()

            'filtro para visualizar los datos con información de Terceros y Centros de Costo o por nivel de cuenta

            reporte.ParametrosReporte = {INDGleViews.EditValue, INDGleAccountLevel.EditValue, INDSleVoucherTypeStart.EditValue,
                                         INDSleVoucherTypeEnd.EditValue, INDSleVoucherStart.EditValue, INDSleVoucherEnd.EditValue,
                                         INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue, INDSleCostCenterStart.EditValue,
                                         INDSleCostCenterEnd.EditValue, INDDateStart.EditValue, INDDateEnd.EditValue, INDGleTypeVoucher.EditValue, INDsleBook.EditValue}

            INDDvReportPrint.DocumentSource = reporte

            reporte.CargarDataSource()
            reporte.CreateDocument(True)

            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBaseHome.Visible = False
                Me.INDCtcNavigation.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReportPrint.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportDiaryVouchers_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeVoucher.Properties.DataSource = FillingTypeVoucher
        Me.INDGleAccountLevel.Properties.DataSource = FillingAccountLevel
        Me.INDGleViews.Properties.DataSource = FillingViews
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeVoucher.EditValue = 4
        Me.INDGleAccountLevel.EditValue = 1
        Me.INDGleViews.EditValue = 1
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDGleViews
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleViews_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleViews.EditValueChanged
        If INDGleViews.EditValue = 2 Then
            Me.INDLciAccountLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDGleAccountLevel.Visible = True
        Else
            Me.INDLciAccountLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDGleAccountLevel.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBaseHome.Visible = True
        Me.INDCtcNavigation.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleVoucherTypeStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleVoucherTypeStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleVoucherTypeStart.QueryPopUp
        If INDSleVoucherTypeStart.Datasource Is Nothing Then
            LoadXpoVouchersTypeStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleVoucherTypeEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleVoucherTypeEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleVoucherTypeEnd.QueryPopUp
        If INDSleVoucherTypeEnd.Datasource Is Nothing Then
            LoadXpoVouchersTypeEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleVoucherStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleVoucherStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleVoucherStart.QueryPopUp
        If INDSleVoucherStart.Datasource Is Nothing Then
            LoadXpoVouchersStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleVoucherEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleVoucherEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleVoucherEnd.QueryPopUp
        If INDSleVoucherEnd.Datasource Is Nothing Then
            LoadXpoVouchersEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de final tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCostCenterStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCostCenterStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterStart.QueryPopUp
        If INDSleCostCenterStart.Datasource Is Nothing Then
            LoadXpoCostCenterStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCostCenterEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterEnd.QueryPopUp
        If INDSleCostCenterEnd.Datasource Is Nothing Then
            LoadXpoCostCenterEnd()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de libros oficiales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBook.QueryPopUp
        If INDsleBook.Properties.DataSource Is Nothing Then
            LoadXpoBook()
        End If
    End Sub

    ''' <summary>
    ''' manejador de eventos que se activa cuando cambia el valor seleccionado en el control "INDGleTypeVoucher"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeVoucher_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeVoucher.EditValueChanged
        LoadXpoVouchersStart()
        LoadXpoVouchersEnd()
    End Sub

    ''' <summary>
    ''' manejador de eventos que se activa cuando cambia el valor seleccionado en el control "Estado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleVoucherTypeStart_EditValueChanged(sender As Object, e As Controls.EditValueChangedEventArgs) Handles INDSleVoucherTypeStart.EditValueChanged
        LoadXpoVouchersStart()
        LoadXpoVouchersEnd()
    End Sub

    ''' <summary>
    ''' Manejador de eventos que se activa cuando cambia el valor seleccionado en el control "INDSleVoucherTypeEnd"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleVoucherTypeEnd_EditValueChanged(sender As Object, e As Controls.EditValueChangedEventArgs) Handles INDSleVoucherTypeEnd.EditValueChanged
        LoadXpoVouchersStart()
        LoadXpoVouchersEnd()
    End Sub

    ''' <summary>
    ''' metodo que carga los datasource de la cuenta inicial y final al momento de cambiar el libro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        LoadXpoVouchersStart()
        LoadXpoVouchersEnd()
    End Sub


    ''' <summary>
    ''' Metodo que se encarga de inicializar y configurar el formulario cuando se carga
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportDiaryVouchers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleVoucherTypeStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetDocumentType
        Me.INDSleVoucherTypeEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetDocumentType
        Me.INDSleVoucherStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountingDocumenteByConsecutive
        Me.INDSleVoucherEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountingDocumenteByConsecutive
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleCostCenterStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCostCenterAsync
        Me.INDSleCostCenterEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCostCenterAsync

        INDsleBook.Properties.Buttons(1).Visible = False
        INDSleVoucherTypeStart.View.OptionsView.ShowGroupPanel = False
        INDSleVoucherTypeEnd.View.OptionsView.ShowGroupPanel = False
        INDSleVoucherStart.View.OptionsView.ShowGroupPanel = False
        INDSleVoucherEnd.View.OptionsView.ShowGroupPanel = False
        INDSleThirdPartyStart.View.OptionsView.ShowGroupPanel = False
        INDSleThirdPartyEnd.View.OptionsView.ShowGroupPanel = False
        INDSleCostCenterStart.View.OptionsView.ShowGroupPanel = False
        INDSleCostCenterEnd.View.OptionsView.ShowGroupPanel = False
        LoadXpoBook()
        SetOfficialBook()
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetOfficialBook()
        If bookXpcollection IsNot Nothing AndAlso bookXpcollection.Count > 0 Then
            Dim item = (From l In bookXpcollection Where l.OfficialBook = True Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleBook.EditValue = item.Id
            End If
        End If
    End Sub


    ''' <summary>
    ''' Se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

End Class
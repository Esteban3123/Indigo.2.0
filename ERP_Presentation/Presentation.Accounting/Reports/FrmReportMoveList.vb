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

#End Region

Public Class FrmReportMoveList

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
    Public Property ProoftCloseXpoTypeVoucher As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de Datos de Tipos de comprobantes(Registrados, Confirmados, Anulados, Todos)
    ''' </summary>
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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        'fecha inicial no puede ser mayor a la fecha final
        If INDSleFilterDate.EditValue = True Then
            If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
                Me.INDDateStart.Focus()
                Validations = False
            ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateCompareDateReport", "Commons"))
                Me.INDDateStart.Focus()
                Validations = False
            End If
        End If
        'la lista de tipo de comprobante no puede ser vacia
        If INDSleTypeVoucherStart.EditValue Is Nothing And INDSleTypeVoucherEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportMoveList_TypeVoucherRange", "Accounting"))
            Me.INDSleTypeVoucherStart.Focus()
            Validations = False
        ElseIf INDSleTypeVoucherStart.EditValue > INDSleTypeVoucherEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblTypeVoucher.Text)
            Me.INDSleTypeVoucherStart.Focus()
            Validations = False
        End If
        Return Validations
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDGcTypeVoucherList
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTypeVoucherStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoTypeVoucher = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTypeVoucherRepor)
            INDSleTypeVoucherStart.Datasource = ProoftCloseXpoTypeVoucher
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTypeVoucherEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTypeVoucherEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoTypeVoucher = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTypeVoucherRepor)
            INDSleTypeVoucherEnd.Datasource = ProoftCloseXpoTypeVoucher
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
    ''' se ejecuta en el evento querypopup del control INDSleTypeVoucherStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTypeVoucherStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTypeVoucherStart.QueryPopUp
        If INDSleTypeVoucherStart.Datasource Is Nothing Then
            LoadXpoTypeVoucherStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleTypeVoucherEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTypeVoucherEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTypeVoucherEnd.QueryPopUp
        If INDSleTypeVoucherEnd.Datasource Is Nothing Then
            LoadXpoTypeVoucherEnd()
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
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptMoveList()
            reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDSleTypeVoucherStart.EditValue,
                                                          INDSleTypeVoucherEnd.EditValue,
                                                          INDGleStatus.EditValue,
                                                          INDsleBook.EditValue}
            INDDvViewReport.DocumentSource = reporte
            reporte.CargarDataSource()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcHomeBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDpcDocumentViewer.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento clickback del control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcHomeBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDpcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' Controla la visibilidad de los controles de selección de fechas en función del valor seleccionado en el control INDSleFilterDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFilterDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleFilterDate.EditValueChanged
        If INDSleFilterDate.EditValue = True Then
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportMoveList_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleStatus.Properties.DataSource = FillingTypeVoucher

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleStatus.EditValue = 2
        Me.INDSleFilterDate.EditValue = False
    End Sub

    ''' <summary>
    ''' se inicializan los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportMoveList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleTypeVoucherStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetDocumentType
        Me.INDSleTypeVoucherEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetDocumentType

        INDsleBook.Properties.Buttons(1).Visible = False
        INDSleTypeVoucherStart.View.OptionsView.ShowGroupPanel = False
        INDSleTypeVoucherEnd.View.OptionsView.ShowGroupPanel = False
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
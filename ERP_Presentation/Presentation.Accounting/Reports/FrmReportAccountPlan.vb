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
Imports Presentation.Common.MVP

#End Region

Public Class FrmReportAccountPlan

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"

    ''' <summary>
    ''' Fuente de datos para cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoAccount As XPInstantFeedbackSource

    ''' <summary>
    ''' Colección de libros oficiales
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    '''  Lista de niveles de cuenta para cargar en el control GridLookUpEdit
    ''' </summary>
    Private _FillingAccountLevel As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingAccountLevel As List(Of Tuple(Of Integer, String))
        Get
            If _FillingAccountLevel Is Nothing Then
                _FillingAccountLevel = New List(Of Tuple(Of Integer, String))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(1, "Clase"))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(2, "Grupo"))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(3, "Cuenta"))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(4, "Subcuenta"))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(5, "Auxiliar"))
                _FillingAccountLevel.Add(New Tuple(Of Integer, String)(6, "Todos"))
            End If
            Return _FillingAccountLevel
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
    ''' Método para cargar el DataSource Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountStart()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue}
            ProoftCloseXpoAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            'ProoftCloseXpoAccount.Sorting.Add(New SortProperty("Number", DB.SortingDirection.Ascending))
            INDSleAccountStart.Datasource = ProoftCloseXpoAccount
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountEnd()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue}
            ProoftCloseXpoAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            'ProoftCloseXpoAccount.Sorting.Add(New SortProperty("Number", DB.SortingDirection.Ascending))
            INDSleAccountEnd.Datasource = ProoftCloseXpoAccount
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
        If Me.INDSleAccountStart.EditValue > INDSleAccountEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareAccount", "Accounting"))
            Me.INDSleAccountEnd.Focus()
            Validations = False
        End If
        If INDSleAccountStart.EditValue Is Nothing Or INDSleAccountEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_AccountRange", "Accounting"))
            Me.INDSleAccountStart.Focus()
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
            Dim reporte As New rptAccountPlan()

            reporte.ParametrosReporte = {INDSleAccountStart.EditValue, INDSleAccountEnd.EditValue, INDGleAccountLevel.EditValue, INDsleBook.EditValue}

            INDDvReportPrint.DocumentSource = reporte
            reporte.CargarDataSource()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCtcNavigation.Visible = False
                Me.INDPcReportViewer.Visible = True
                INDDvReportPrint.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSleAccountStart.Focus()
            End If

        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCtnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCtnReturn_ClickBack() Handles INDCtnReturn.ClickBack
        Me.INDPcReportViewer.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCtcNavigation.Visible = True
    End Sub

    ''' <summary>
    ''' Se ejecuta en el Evento QueryPopUp del Control INDSleAccountStart que asocia el Método LoadXpoAccountStart mencionado anteriormente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountStart.QueryPopUp
        If INDsleBook.EditValue IsNot Nothing Then
            If INDSleAccountStart.Datasource Is Nothing Then
                LoadXpoAccountStart()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en el Evento QueryPopUp del Control INDSleAccountEnd que asocia el Método LoadXpoAccountEnd mencionado anteriormente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountEnd.QueryPopUp
        If INDsleBook.EditValue IsNot Nothing Then
            If INDSleAccountEnd.Datasource Is Nothing Then
                LoadXpoAccountEnd()
            End If
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
    ''' Este evento se ejecuta cuando se carga el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportAccountPlan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        INDsleBook.Properties.Buttons(1).Visible = False
        INDSleAccountStart.View.OptionsView.ShowGroupPanel = False
        INDSleAccountEnd.View.OptionsView.ShowGroupPanel = False
        LoadXpoBook()
        SetOfficialBook()
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportAccountPlan_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar GridLookUpEdit
        Me.INDGleAccountLevel.Properties.DataSource = FillingAccountLevel

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleAccountLevel.EditValue = 6
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
    ''' metodo que carga los datasource de la cuenta inicial y final al momento de cambiar el libro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        If INDsleBook.EditValue IsNot Nothing Then
            LoadXpoAccountStart()
            LoadXpoAccountEnd()
        End If
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
    ''' Evento que se ejecuta cuando se activa la barra de botones 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

End Class
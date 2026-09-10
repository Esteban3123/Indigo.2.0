#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo

#End Region

Public Class FrmReportLedgerAndBalance

#Region "Properties"

    ''' <summary>
    ''' Propiedad que almacena la última fecha de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property INDLastClosingDate As Date

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Lista de tuplas de niveles de cuenta
    ''' </summary>
    Private _FillingNivel As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite acceder a una lista de niveles de cuenta predefinidos (Clase, Grupo, Cuenta, Subcuenta, Auxiliar) 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingNivel As List(Of Tuple(Of Integer, String))
        Get
            If _FillingNivel Is Nothing Then
                _FillingNivel = New List(Of Tuple(Of Integer, String))
                _FillingNivel.Add(New Tuple(Of Integer, String)(1, "Clase"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(2, "Grupo"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(3, "Cuenta"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(4, "Subcuenta"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(5, "Auxiliar"))
            End If
            Return _FillingNivel
        End Get
    End Property

    ''' <summary>
    ''' Lista de tuplas tipo de reporte
    ''' </summary>
    Private _FillingTypeRepor As List(Of Tuple(Of Integer, String))

    ''' <summary>
    '''  Permite acceder a una lista de tipos de reportes predefinidos (Informe Definitivo, Numeración) 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingTypeRepor As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeRepor Is Nothing Then
                _FillingTypeRepor = New List(Of Tuple(Of Integer, String))
                _FillingTypeRepor.Add(New Tuple(Of Integer, String)(1, "Informe Definitivo"))
                _FillingTypeRepor.Add(New Tuple(Of Integer, String)(2, "Numeración"))
            End If
            Return _FillingTypeRepor
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
    ''' se ejecuta al cambiar la fecha del control INDCdnPeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnPeriod_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnPeriod.OnChangeDate
        'cuando el usuario cambie la fecha inicial si la fecha inicial es menor que la fecha del ultimo cierre hacer la fecha inicial igual que la fecha del ultimo cierre
        If INDCdnPeriod.GetYear <= Year(INDLastClosingDate) Then
            INDCdnPeriod.SetYear = Year(INDLastClosingDate)
            If INDCdnPeriod.GetMonth < Month(INDLastClosingDate) Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidatePeriodClosingReport", "Commons"), INDCdnPeriod.GetMonth, INDCdnPeriod.GetYear)
                INDCdnPeriod.SetMonth = Month(INDLastClosingDate)
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click al control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcHomeBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
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
    ''' se ejecuta al dar click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        AsyncLoader(True)
        Dim reporte As New rptLedgerAndBalance()
        Dim INDPeriod As Date = "01/" & INDCdnPeriod.GetMonth & "/" & INDCdnPeriod.GetYear
        reporte.ParametrosReporte = New Object() {INDPeriod,
                                                  INDGleAccountLevel.EditValue,
                                                  INDGleTypeReport.EditValue,
                                                  INDSleAccountsZero.EditValue,
                                                  INDsleBook.EditValue,
                                                  INDTxtPrefix.EditValue,
                                                  INDTxtInitialConsecutive.EditValue}
        INDDvDocumentViewer.DocumentSource = reporte
        Await reporte.CargarDataSource1()
        reporte.CreateDocument(True)
        AsyncLoader(False)
        If reporte.DataSource IsNot Nothing Then
            Me.INDLcHomeBase.Visible = False
            Me.INDCncNavigation.Visible = False
            Me.INDPcDocumentViewer.Visible = True
            INDDvDocumentViewer.Show()
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDCdnPeriod.Focus()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportLedgerAndBalance_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'Cargar GridLookUpEdit
        Me.INDGleAccountLevel.Properties.DataSource = Me.FillingNivel
        Me.INDGleTypeReport.Properties.DataSource = Me.FillingTypeRepor

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleAccountLevel.EditValue = 5
        Me.INDSleAccountsZero.EditValue = False
        Me.INDGleTypeReport.EditValue = 1

        Using msearch As New MBusqueda
            If msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate) = Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateExistencePeriodClosingReport", "Commons"))
                INDLastClosingDate = Date.Now
            Else
                INDLastClosingDate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate)
            End If
        End Using

        INDCdnPeriod.SetMonth = Month(INDLastClosingDate)
        INDCdnPeriod.SetYear = Year(INDLastClosingDate)

        INDsleBook.Properties.Buttons(1).Visible = False
        LoadXpoBook()
        SetOfficialBook()

    End Sub

    ''' <summary>
    ''' Al cambiar el tipo de reporte se muestran los filtros de la paginación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Then
            INDLciTxtPrefix.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciTxtInitialConsecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDTxtPrefix.EditValue = Nothing
            INDTxtInitialConsecutive.EditValue = Nothing
        Else
            INDLciTxtPrefix.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciTxtInitialConsecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
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
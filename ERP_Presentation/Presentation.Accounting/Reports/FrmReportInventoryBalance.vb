#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo

#End Region

Public Class FrmReportInventoryBalance

#Region "Properties"

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoAccount As XPInstantFeedbackSource

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
    ''' Permite acceder a una lista de tipos de reportes predefinidos (Informe Definitivo, Numeración) 
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
#Region "Events"
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
        Me.INDLcBaseHome.Visible = True
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
        If ValidateControls() = False Then
            Exit Sub
        End If
        AsyncLoader(True)
        Dim reporte As New rptInventoryAndBalance()
        'fecha Inicial
        Dim INDPeriodStart As Integer = 1
        'fecha
        Dim INDPeriodEnd As Integer = INDCdnPeriod.GetMonth
        Dim INDYear As Integer = INDCdnPeriod.GetYear
        reporte.ParametrosReporte = New Object() {INDPeriodStart,
                                                  INDPeriodEnd,
                                                  INDYear,
                                                  INDGleTypeReport.EditValue,
                                                  INDSleAccountsZero.EditValue,
                                                  INDsleDetailingThird.EditValue,
                                                  INDsleBook.EditValue,
                                                  INDGleAccountLevel.EditValue,
                                                  INDTxtPrefix.EditValue,
                                                  INDTxtInitialConsecutive.EditValue,
                                                  INDSleInitialAccount.EditValue,
                                                  INDSleFinalAccount.EditValue}
        INDDvDocumentViewer.DocumentSource = reporte
        Await reporte.CargarDataSource1()
        reporte.CreateDocument(True)
        AsyncLoader(False)
        If reporte.DataSource IsNot Nothing Then
            Me.INDLcBaseHome.Visible = False
            Me.INDCncNavigation.Visible = False
            Me.INDPcDocumentViewer.Visible = True
            INDDvDocumentViewer.Show()
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDCdnPeriod.Focus()
        End If
    End Sub

    ''' <summary>
    '''  se ejecuta en el evento EditValueChanged del control INDGleAccountLevel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleAccountLevel_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAccountLevel.EditValueChanged
        'si el nivel de la cuenta es igual a 5 
        If INDGleAccountLevel.EditValue = 5 Then
            INDLciDetailingThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDsleDetailingThird.EditValue = False
        Else
            INDLciDetailingThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDsleDetailingThird.EditValue = False
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportInventoryBalance_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'Cargar GridLookUpEdit
        Me.INDGleAccountLevel.Properties.DataSource = Me.FillingNivel
        Me.INDGleTypeReport.Properties.DataSource = Me.FillingTypeRepor


        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleAccountLevel.EditValue = 5
        Me.INDSleAccountsZero.EditValue = False
        Me.INDsleDetailingThird.EditValue = False

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
    ''' al cambiar el tipo de reporte se muestran los filtros de la paginación
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
    ''' Método para seleccionar por defecto el libro oficial
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

    ''' <summary>
    ''' Carga los datos del control Cuenta Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleInitialAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialAccount.QueryPopUp
        If INDSleInitialAccount.Datasource Is Nothing Then
            LoadXpoInitialAccount()
        End If
    End Sub

    ''' <summary>
    ''' Carga los datos del control Cuenta Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFinalAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalAccount.QueryPopUp
        If INDSleFinalAccount.Datasource Is Nothing Then
            LoadXpoFinalAccount()
        End If
    End Sub
#End Region
#Region "Methods"
    ''' <summary>
    ''' Metodo para validar los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        If INDSleInitialAccount.EditValue Is Nothing And INDSleFinalAccount.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la cuenta inicial."
            INDSleInitialAccount.Focus()
            Return False
        ElseIf INDSleInitialAccount.EditValue IsNot Nothing And INDSleFinalAccount.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la cuenta final."
            INDSleFinalAccount.Focus()
            Return False
        ElseIf CInt(INDSleInitialAccount.EditValue) > CInt(INDSleFinalAccount.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "El número de cuenta inicial no puede ser mayor al de la cuenta final."
            INDSleInitialAccount.Focus()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Método que consulta y trae los datos de cuenta inicial
    ''' </summary>
    Private Sub LoadXpoInitialAccount()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue, 1}
            ProoftCloseXpoAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookIdHandledThirdParty, filter)
            INDSleInitialAccount.Datasource = ProoftCloseXpoAccount
        End Using
    End Sub

    ''' <summary>
    ''' Método que consulta y trae los datos de cuenta final
    ''' </summary>
    Private Sub LoadXpoFinalAccount()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue, 1}
            ProoftCloseXpoAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookIdHandledThirdParty, filter)
            INDSleFinalAccount.Datasource = ProoftCloseXpoAccount
        End Using
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta y evalúa cuando cambia el valor del control "Detallar Terceros"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDetailingThird_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDetailingThird.EditValueChanged
        If INDsleDetailingThird.EditValue = True And INDGleAccountLevel.EditValue = 5 Then
            INDLcgFilterNotRequired.Visibility = System.Windows.Visibility.Visible
            INDSleInitialAccount.EditValue = Nothing
            INDSleFinalAccount.EditValue = Nothing
        ElseIf INDsleDetailingThird.EditValue = False Then
            INDLcgFilterNotRequired.Visibility = System.Windows.Visibility.Hidden
            INDSleInitialAccount.EditValue = Nothing
            INDSleFinalAccount.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportInventoryBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDLcgFilterNotRequired.Visibility = System.Windows.Visibility.Hidden

    End Sub
#End Region

End Class
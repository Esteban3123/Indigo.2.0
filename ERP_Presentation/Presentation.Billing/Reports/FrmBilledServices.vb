'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Presentation.Billing.MVP
Imports System.Text
Imports Infrastructure.Data.Xpo
Imports Domain.Entities

#End Region

Public Class FrmBilledServices

#Region "Variables"

    ''' <summary>
    ''' Representa al reporte
    ''' </summary>
    Dim report As Object

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PBillingStatistics

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Se obtienen los tipos de facturas
    ''' </summary>
    Dim invoiceTypeFilter As String

    ''' <summary>
    ''' Se obtienen los id de los terceros
    ''' </summary>
    Dim thirdPartyIdsFilter As String

    ''' <summary>
    ''' Se obtienen los id de las entidades
    ''' </summary>
    Dim healthAdministratorIdsFilter As String

    ''' <summary>
    ''' Se obtienen los id de los grupos de atención
    ''' </summary>
    Dim careGroupIdsFilter As String

    ''' <summary>
    ''' Se obtienen los códigos de los centros de atención
    ''' </summary>
    Dim careCenterCodesFilter As String

    ''' <summary>
    ''' Se obtienen los códigos de los usuarios
    ''' </summary>
    Dim userCodesFilter As String

    ''' <summary>
    ''' Se obtienen los códigos de las unidades funcionales
    ''' </summary>
    Dim functionalUnitCodesFilter As String

    ''' <summary>
    ''' Se obtienen los códigos de los profesionales
    ''' </summary>
    Dim professionalCodesFilter As String

    ''' <summary>
    ''' Se obtienen los id de los cups
    ''' </summary>
    Dim cupsEntityIdFilter As String

    ''' <summary>
    ''' Se obtienen los id de los productos
    ''' </summary>
    Dim inventoryProductIdFilter As String

    ''' <summary>
    ''' Representa al reporte
    ''' </summary>
    Dim reporte As Object
    ''' <summary>
    ''' obtiene o establece la informacion de la moneda
    ''' </summary>
    Private _currency As Currency

#End Region

#Region "Properties"

    Private WriteOnly Property DataSourceAgrupedBy As Integer
        Set(value As Integer)
            Dim loadAgrupedBy As New List(Of Tuple(Of Integer, String))
            If value = 1 Then 'Resumido
                loadAgrupedBy.Add(New Tuple(Of Integer, String)(3, "Grupo Atención"))
                loadAgrupedBy.Add(New Tuple(Of Integer, String)(6, "Servicio"))
            Else 'Detallado
                loadAgrupedBy.Add(New Tuple(Of Integer, String)(1, "Tercero"))
                loadAgrupedBy.Add(New Tuple(Of Integer, String)(2, "Entidad"))
                loadAgrupedBy.Add(New Tuple(Of Integer, String)(3, "Grupo Atención"))
                loadAgrupedBy.Add(New Tuple(Of Integer, String)(4, "Usuario"))
                loadAgrupedBy.Add(New Tuple(Of Integer, String)(5, "Profesional"))
            End If
            INDsleAgrupedBy.Properties.DataSource = loadAgrupedBy
        End Set
    End Property

    Public Property ProoftCloseXpoCurrency As XPInstantFeedbackSource

    Public Property TypeReport As Integer
        Get
            Return INDsleReportType.EditValue
        End Get
        Set(value As Integer)
            INDsleReportType.EditValue = value
        End Set
    End Property

    Public Property CurrencyId As Integer
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
        End Set
    End Property

#End Region

#Region "Method"

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
    ''' Inicializa los search con datos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim loadStatus As New List(Of Tuple(Of Integer, String))
        loadStatus.Add(New Tuple(Of Integer, String)(1, "Facturado"))
        loadStatus.Add(New Tuple(Of Integer, String)(2, "Anulado"))
        loadStatus.Add(New Tuple(Of Integer, String)(3, "Todos"))
        INDsleStatus.Properties.DataSource = loadStatus

        Dim loadGetDateOf As New List(Of Tuple(Of Integer, String))
        loadGetDateOf.Add(New Tuple(Of Integer, String)(1, "Factura"))
        'loadGetDateOf.Add(New Tuple(Of Integer, String)(2, "Servicios"))
        'loadGetDateOf.Add(New Tuple(Of Integer, String)(3, "Ingresos"))
        'loadGetDateOf.Add(New Tuple(Of Integer, String)(4, "Egresos"))
        INDsleGetDateOf.Properties.DataSource = loadGetDateOf

        Dim loadReportType As New List(Of Tuple(Of Integer, String))
        loadReportType.Add(New Tuple(Of Integer, String)(1, "Resumido"))
        loadReportType.Add(New Tuple(Of Integer, String)(2, "Detallado"))
        INDsleReportType.Properties.DataSource = loadReportType
    End Sub

    ''' <summary>
    ''' Valida los controles del reporte
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsResport() As Boolean
        Dim errors As New StringBuilder

        If INDdteInitialDate.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una fecha inicial")
        End If

        If INDdteEndDate.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una fecha final")
        End If

        If INDsleStatus.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un estado")
        End If

        If INDsleGetDateOf.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar tomar fecha de")
        End If

        If (From x As DevExpress.XtraEditors.Controls.CheckedListBoxItem In INDccbeInvoiceType.Properties.Items Where x.CheckState = System.Windows.Forms.CheckState.Checked Select x).Count = 0 Then
            errors.AppendLine("Debe seleccionar un tipo de factura")
        Else
            If INDccbeInvoiceType.Properties.Items(3).CheckState AndAlso INDccbeInvoiceType.Properties.Items(4).CheckState Then
                errors.AppendLine("No puede seleccionar los tipos factura capitada y control de capitación a la vez")
            End If
        End If

        If INDsleReportType.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un tipo de reporte")
        End If

        If INDsleAgrupedBy.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar agrupado por")
        End If

        If TypeReport = 2 AndAlso CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CurrencyVoid", "Commons"))
            Me.INDsleCurrency.Focus()
        End If

        If errors.ToString().Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' obtiene la informacion de la moneda por el id
    ''' </summary>
    ''' <param name="currencyId"></param>
    ''' <returns></returns>
    Private Async Function GetDataCurrency(currencyId As Integer) As Task
        Using Model As New MCurrency("")
            _currency = Await Model.GetCurrencyById(currencyId)
        End Using
    End Function

    ''' <summary>
    ''' Se obtienen los filtros
    ''' </summary>
    Private Async Function GetFilters() As Task
        Try
            invoiceTypeFilter = String.Join(",", (From x As DevExpress.XtraEditors.Controls.CheckedListBoxItem In INDccbeInvoiceType.Properties.Items Where x.CheckState = System.Windows.Forms.CheckState.Checked Select x.Value).ToArray())
            Dim GriedViewThirdParty = DirectCast(INDgcThirdParty.FocusedView, GridView)
            thirdPartyIdsFilter = String.Join(",", (From x In GriedViewThirdParty.GetSelectedRows() Select DirectCast(GriedViewThirdParty.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))
            Dim GriedViewHealthAdministrator = DirectCast(INDgcHealthAdministrator.FocusedView, GridView)
            healthAdministratorIdsFilter = String.Join(",", (From x In GriedViewHealthAdministrator.GetSelectedRows() Select DirectCast(GriedViewHealthAdministrator.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))
            Dim GriedViewCareGroup = DirectCast(INDgcCareGroup.FocusedView, GridView)
            careGroupIdsFilter = String.Join(",", (From x In GriedViewCareGroup.GetSelectedRows() Select DirectCast(GriedViewCareGroup.GetRow(x), Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupXpo).Id))
            Dim GriedViewCareCenter = DirectCast(INDgcCareCenter.FocusedView, GridView)
            careCenterCodesFilter = String.Join(",", (From x In GriedViewCareCenter.GetSelectedRows() Select "'" + DirectCast(GriedViewCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.CODCENATE + "'"))
            Dim GriedViewUsers = DirectCast(INDgcUsers.FocusedView, GridView)
            userCodesFilter = String.Join(",", (From x In GriedViewUsers.GetSelectedRows() Select "'" + DirectCast(GriedViewUsers.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.UserCode + "'"))
            Dim GriedViewFunctionalUnit = DirectCast(INDgcFunctionalUnit.FocusedView, GridView)
            functionalUnitCodesFilter = String.Join(",", (From x In GriedViewFunctionalUnit.GetSelectedRows() Select "'" + DirectCast(GriedViewFunctionalUnit.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Codigo + "'"))
            Dim GriedViewProfessional = DirectCast(INDgcProfessional.FocusedView, GridView)
            professionalCodesFilter = String.Join(",", (From x In GriedViewProfessional.GetSelectedRows() Select "'" + DirectCast(GriedViewProfessional.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.CODPROSAL + "'"))
            Dim GriedViewCupsEntityId = DirectCast(INDgcIPSService.FocusedView, GridView)
            cupsEntityIdFilter = String.Join(",", (From x In GriedViewCupsEntityId.GetSelectedRows() Where DirectCast(GriedViewCupsEntityId.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Identification = 0 Select DirectCast(GriedViewCupsEntityId.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))
            Dim GriedViewInventoryProduct = DirectCast(INDgcIPSService.FocusedView, GridView)
            inventoryProductIdFilter = String.Join(",", (From x In GriedViewInventoryProduct.GetSelectedRows() Where DirectCast(GriedViewInventoryProduct.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Identification = 1 Select DirectCast(GriedViewInventoryProduct.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))
        Catch ex As Exception
            Throw New Exception("Ocurrió un error en la conversión de los filtros, contacte al administrador.")
        End Try
    End Function

    ''' <summary>
    ''' Carga el datasource de la rejilla de excel
    ''' </summary>
    Private Async Sub DataSourceExcel()
        Try
            Await GetFilters()

            Dim filter As String = ""

            If INDsleGetDateOf.EditValue = 1 Then
                filter = "InvoiceDate >= #" & Format(INDdteInitialDate.EditValue, "yyyy-MM-dd HH:mm:ss") & "# and InvoiceDate <= #" & Format(INDdteEndDate.EditValue, "yyyy-MM-dd HH:mm:ss") & "#"
            End If

            filter &= " and DocumentType in (" & invoiceTypeFilter & ")"

            If INDsleStatus.EditValue <> 3 Then
                filter &= " and StatusInvoice = " & INDsleStatus.EditValue
            End If

            If String.IsNullOrEmpty(thirdPartyIdsFilter) = False Then
                filter &= " and ThirdPartyId in (" & thirdPartyIdsFilter & ")"
            End If

            If String.IsNullOrEmpty(healthAdministratorIdsFilter) = False Then
                filter &= " and HealthAdministratorId in (" & healthAdministratorIdsFilter & ")"
            End If

            If String.IsNullOrEmpty(careGroupIdsFilter) = False Then
                filter &= " and CareGroupId in (" & careGroupIdsFilter & ")"
            End If

            If String.IsNullOrEmpty(careCenterCodesFilter) = False Then
                filter &= " and CareCenterCode in (" & careCenterCodesFilter & ")"
            End If

            If String.IsNullOrEmpty(userCodesFilter) = False Then
                filter &= " and UserCode in (" & userCodesFilter & ")"
            End If

            If String.IsNullOrEmpty(functionalUnitCodesFilter) = False Then
                filter &= " and FunctionalUnitCode in (" & functionalUnitCodesFilter & ")"
            End If

            If String.IsNullOrEmpty(professionalCodesFilter) = False Then
                filter &= " and PerformsHealthProfessionalCode in (" & professionalCodesFilter & ")"
            End If

            If String.IsNullOrEmpty(cupsEntityIdFilter) = False AndAlso String.IsNullOrEmpty(inventoryProductIdFilter) = False Then
                filter &= " and (CupsEntityId in (" & cupsEntityIdFilter & ") or ProductId in (" & inventoryProductIdFilter & "))"
            End If

            If String.IsNullOrEmpty(cupsEntityIdFilter) = False AndAlso String.IsNullOrEmpty(inventoryProductIdFilter) = True Then
                filter &= " and CupsEntityId in (" & cupsEntityIdFilter & ")"
            End If

            If String.IsNullOrEmpty(cupsEntityIdFilter) = True AndAlso String.IsNullOrEmpty(inventoryProductIdFilter) = False Then
                filter &= " and ProductId in (" & inventoryProductIdFilter & ")"
            End If

            If CurrencyId > 0 Then
                filter &= " and CurrencyId = " & CurrencyId & ""
            End If

            INDgcGenerateExcel.DataSource = Await Presenter.GetDataGenerateExcel(filter)

            generateExcel()
            INDgcGenerateExcel.SafeInvoke(Sub()
                                              AsyncLoader(False)
                                          End Sub)
        Catch ex As Exception
            INDgcGenerateExcel.SafeInvoke(Sub()
                                              AsyncLoader(False)
                                              Throw ex
                                          End Sub)
        End Try
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = INDgcGenerateExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        INDgcGenerateExcel.DataSource = Nothing
        INDgcGenerateExcel.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCurrency
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCurrency()
        ProoftCloseXpoCurrency = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.GetCurrency()
        INDsleCurrency.Properties.DataSource = ProoftCloseXpoCurrency
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBilledServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PBillingStatistics()
        InitializeTuples()
    End Sub

    ''' <summary>
    ''' Rompe todos los objetos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        report = Nothing
        ProoftCloseXpoCurrency = Nothing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBilledServices_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDdteInitialDate.Focus()
        LoadXpoCurrency()
        CurrencyId = IndigoSessionValues.OfficialCurrencyId
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al dar click en el botón de generar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbReportGenerate_Click(sender As Object, e As EventArgs) Handles INDsbReportGenerate.Click
        If ValidateControlsResport() = False Then
            Exit Sub
        End If

        Await GetFilters()

        Me.ChangeMessageProgressBar = "El informe se esta construyendo, puede tardar dependiendo de la solicitud"
        AsyncLoader(True)

        If CurrencyId > 0 Then
            Await GetDataCurrency(CurrencyId)
        End If

        If INDsleAgrupedBy.EditValue = 3 Then 'Si el agrupamiento es por grupo atención

            reporte = New rptBilledServices

        Else 'Si es por otro tipo de agrupamiento

            reporte = New rptBilledServicesDetailed

        End If

        reporte.ParametrosReporte = New Object() {
                                                  INDdteInitialDate.EditValue,
                                                  INDdteEndDate.EditValue,
                                                  INDsleStatus.EditValue,
                                                  INDsleGetDateOf.EditValue,
                                                  invoiceTypeFilter,
                                                  INDsleReportType.EditValue,
                                                  INDsleAgrupedBy.EditValue,
                                                  thirdPartyIdsFilter,
                                                  healthAdministratorIdsFilter,
                                                  careGroupIdsFilter,
                                                  careCenterCodesFilter,
                                                  userCodesFilter,
                                                  functionalUnitCodesFilter,
                                                  professionalCodesFilter,
                                                  cupsEntityIdFilter,
                                                  inventoryProductIdFilter,
                                                  CurrencyId '16
        }
        reporte.Currency = _currency
        INDDvReport.DocumentSource = reporte

        Await CType(reporte, IReportAsync).CargarDataSourceAsync
        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
            reporte.CreateDocument(True)
        End If
        AsyncLoader(False)
        Me.ChangeMessageProgressBar = "Cargando"
        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
            Me.INDLcBase.Visible = False
            Me.INDNcpReport.Visible = False
            Me.INDPcReport.Visible = True
            INDDvReport.Show()
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            INDdteInitialDate.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre el boton de regresar
    ''' </summary>
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDNcpReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón del excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnGenerateExcel_Click(sender As Object, e As EventArgs) Handles INDbtnGenerateExcel.Click
        If ValidateControlsResport() = False Then
            Exit Sub
        End If
        AsyncLoader(True)
        Task.Factory.StartNew(AddressOf DataSourceExcel)
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceThirdParty.QueryPopUp
        If INDgcThirdParty.DataSource Is Nothing Then
            INDgcThirdParty.DataSource = Presenter.LoadDatasourceThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceHealthAdministrator_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceHealthAdministrator.QueryPopUp
        If INDgcHealthAdministrator.DataSource Is Nothing Then
            INDgcHealthAdministrator.DataSource = Presenter.LoadDatasourceHealthAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de grupo atención popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceCareGroup.QueryPopUp
        If INDgcCareGroup.DataSource Is Nothing Then
            INDgcCareGroup.DataSource = Presenter.LoadDatasourceCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceUsers_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceUsers.QueryPopUp
        If INDgcUsers.DataSource Is Nothing Then
            INDgcUsers.DataSource = Presenter.LoadDatasourceUsers()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centros de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceCareCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceCareCenter.QueryPopUp
        If INDgcCareCenter.DataSource Is Nothing Then
            INDgcCareCenter.DataSource = Presenter.LoadDatasourceCareCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceFunctionalUnit.QueryPopUp
        If INDgcFunctionalUnit.DataSource Is Nothing Then
            INDgcFunctionalUnit.DataSource = Presenter.LoadDatasourceFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de profesional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceProfessional_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceProfessional.QueryPopUp
        If INDgcProfessional.DataSource Is Nothing Then
            INDgcProfessional.DataSource = Presenter.LoadDatasourceProfessional()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceIPSService_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceIPSService.QueryPopUp
        If INDgcIPSService.DataSource Is Nothing Then
            INDgcIPSService.DataSource = Presenter.LoadDatasourceIPSService()
        End If
    End Sub

    ''' <summary>
    ''' popup para mostrar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp, INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            LoadXpoCurrency()
        End If
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Evento que se dispara al seleccionar un item en el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewThirdParty_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewThirdParty.SelectionChanged
        INDpceThirdParty.Text = DirectCast(INDgcThirdParty.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al seleccionar un item en el control de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewHealthAdministrator_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewHealthAdministrator.SelectionChanged
        INDpceHealthAdministrator.Text = DirectCast(INDgcHealthAdministrator.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al checkear en la rejila de grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewCareGroup_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewCareGroup.SelectionChanged
        INDpceCareGroup.Text = DirectCast(INDgcCareGroup.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al checkear en la rejilla de los usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewUsers_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewUsers.SelectionChanged
        INDpceUsers.Text = DirectCast(INDgcUsers.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al checkear en la rejilla de centros de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewCareCenter_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewCareCenter.SelectionChanged
        INDpceCareCenter.Text = DirectCast(INDgcCareCenter.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al checkear en la rejilla de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewFunctionalUnit_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewFunctionalUnit.SelectionChanged
        INDpceFunctionalUnit.Text = DirectCast(INDgcFunctionalUnit.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al checkear en la rejilla de profesionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewProfessional_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewProfessional.SelectionChanged
        INDpceProfessional.Text = DirectCast(INDgcProfessional.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al checkear en la rejilla de servicios ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewIPSService_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewIPSService.SelectionChanged
        INDpceIPSService.Text = DirectCast(INDgcIPSService.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleReportType.EditValueChanged
        INDsleAgrupedBy.EditValue = Nothing
        DataSourceAgrupedBy = INDsleReportType.EditValue
    End Sub

#End Region

#End Region

End Class
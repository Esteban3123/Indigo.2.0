'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/07/2019
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
Imports DevExpress.Data.Async.Helpers
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.CloudAgent
Imports Presentation.Billing.MVP
Imports System.Text
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Presentation.Accounting.MVP
Imports Domain.Entities

#End Region

Public Class FrmBillingStatistics

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PBillingStatistics

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Permite establecer la cantidad de días que se trae la información
    ''' </summary>
    Dim QuantityDays As Integer = 5

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
    ''' Se obtienen los códigos de los usuarios
    ''' </summary>
    Dim userCodesFilter As String

    ''' <summary>
    ''' Se obtienen los códigos de los centros de atención
    ''' </summary>
    Dim careCenterCodesFilter As String

    ''' <summary>
    ''' diccionario para obtenes los rangos
    ''' </summary>
    Private _criterias As Dictionary(Of String, String)

    ''' <summary>
    ''' diccionario para obtenes los filtros
    ''' </summary>
    Private _filters As Dictionary(Of String, String)

    ''' <summary>
    ''' obtiene la informacion de la moneda
    ''' </summary>
    Private CurrencyData As Currency

    ''' <summary>
    ''' obtiene la informacion de la configuracion de la compañia
    ''' </summary>
    Private CompanySettings As CompanySettings

    ''' <summary>
    ''' obtiene la moneda oficial
    ''' </summary>
    Dim OfficialCurrency As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
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
    ''' obtiene la moneda seleccionada en el campo moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property Currency As Integer
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene la fecha inicial
    ''' </summary>
    ''' <returns></returns>
    Public Property DateStart As DateTime
        Get
            Return INDdteInitialDate.EditValue
        End Get
        Set(value As DateTime)
            INDdteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene la fecha final
    ''' </summary>
    ''' <returns></returns>
    Public Property DateEnd As DateTime
        Get
            Return INDdteEndDate.EditValue
        End Get
        Set(value As DateTime)
            INDdteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el tipo de reporte
    ''' </summary>
    ''' <returns></returns>
    Public Property ReportType As Integer
        Get
            Return INDsleReportType.EditValue
        End Get
        Set(value As Integer)
            INDsleReportType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el agrupado por
    ''' </summary>
    ''' <returns></returns>
    Public Property GroupBy As Integer
        Get
            Return INDsleAgrupedBy.EditValue
        End Get
        Set(value As Integer)
            INDsleAgrupedBy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el tipo de factura
    ''' </summary>
    ''' <returns></returns>
    Public Property InvoiceType As String
        Get
            Return INDccbeInvoiceType.EditValue
        End Get
        Set(value As String)
            INDccbeInvoiceType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el estado
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Integer
        Get
            Return INDsleStatus.EditValue
        End Get
        Set(value As Integer)
            INDsleStatus.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el tercero
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdParty As Integer
        Get
            Return INDpceThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDpceThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene la entidad
    ''' </summary>
    ''' <returns></returns>
    Public Property Entity As Integer
        Get
            Return INDpceHealthAdministrator.EditValue
        End Get
        Set(value As Integer)
            INDpceHealthAdministrator.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el gropu de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Property CareCenter As String
        Get
            Return INDpceCareCenter.EditValue
        End Get
        Set(value As String)
            INDpceCareCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Property CareGroup As String
        Get
            Return INDpceCareGroup.EditValue
        End Get
        Set(value As String)
            INDpceCareGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el usuario
    ''' </summary>
    ''' <returns></returns>
    Public Property Users As String
        Get
            Return INDpceUsers.EditValue
        End Get
        Set(value As String)
            INDpceUsers.EditValue = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que carga el datasource de los search quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim loadReportType As New List(Of Tuple(Of Integer, String))
        loadReportType.Add(New Tuple(Of Integer, String)(1, "Resumido"))
        loadReportType.Add(New Tuple(Of Integer, String)(2, "Detallado"))
        INDsleReportType.Properties.DataSource = loadReportType

        Dim loadAgrupedBy As New List(Of Tuple(Of Integer, String))
        loadAgrupedBy.Add(New Tuple(Of Integer, String)(1, "Tercero"))
        loadAgrupedBy.Add(New Tuple(Of Integer, String)(2, "Entidad"))
        loadAgrupedBy.Add(New Tuple(Of Integer, String)(3, "Grupo Atención"))
        loadAgrupedBy.Add(New Tuple(Of Integer, String)(4, "Usuario"))
        loadAgrupedBy.Add(New Tuple(Of Integer, String)(5, "Centro Atención"))
        INDsleAgrupedBy.Properties.DataSource = loadAgrupedBy

        Dim loadStatus As New List(Of Tuple(Of Integer, String))
        loadStatus.Add(New Tuple(Of Integer, String)(1, "Facturado"))
        loadStatus.Add(New Tuple(Of Integer, String)(2, "Anulado"))
        loadStatus.Add(New Tuple(Of Integer, String)(3, "Todos"))
        INDsleStatus.Properties.DataSource = loadStatus

        INDccbeInvoiceType.Properties.Items.First(Function(x) x.Value = 1).Description = $"Factura {ResourceManager.GetString("CareGroupType1", "Contract")}"
        INDccbeInvoiceType.Properties.Items.First(Function(x) x.Value = 2).Description = $"Factura {ResourceManager.GetString("CareGroupType2", "Contract")}"

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

        If INDsleReportType.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un tipo de reporte")
        End If

        If INDsleAgrupedBy.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una agrupación")
        End If

        If (From x As DevExpress.XtraEditors.Controls.CheckedListBoxItem In INDccbeInvoiceType.Properties.Items Where x.CheckState = System.Windows.Forms.CheckState.Checked Select x).Count = 0 Then
            errors.AppendLine("Debe seleccionar un tipo de factura")
        Else
            If INDccbeInvoiceType.Properties.Items(3).CheckState AndAlso INDccbeInvoiceType.Properties.Items(4).CheckState Then
                errors.AppendLine("No puede seleccionar los tipos factura capitada y control de capitación a la vez")
            End If
        End If

        If INDsleStatus.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un estado")
        End If

        If errors.ToString().Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = INDgcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        INDgcExportExcel.DataSource = Nothing
        INDgcExportExcel.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' obtiene la informacion de la moneda por el id
    ''' </summary>
    ''' <param name="currencyId"></param>
    ''' <returns></returns>
    Private Async Function GetDataCurrency(currencyId As Integer) As Task
        Using Model As New MCurrency("")
            CurrencyData = Await Model.GetCurrencyById(currencyId)
        End Using
    End Function

    ''' <summary>
    ''' obtiene la moneda oficial de configuracion de la compañia
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetOfficialCurrency() As Task
        Using Model As New MCompanySettings("")
            CompanySettings = Await Model.GetCompanySettings()
            OfficialCurrency = CompanySettings?.OfficialCurrencyId
        End Using
    End Function

    ''' <summary>
    ''' Se obtienen los filtros
    ''' </summary>
    Private Async Function GetFilters() As Task
        invoiceTypeFilter = String.Join(",", (From x As DevExpress.XtraEditors.Controls.CheckedListBoxItem In INDccbeInvoiceType.Properties.Items Where x.CheckState = System.Windows.Forms.CheckState.Checked Select x.Value).ToArray())
        careCenterCodesFilter = String.Join(",", (From x In DirectCast(INDgcCareCenter.FocusedView, GridView).GetSelectedRows() Select DirectCast(DirectCast(INDgcCareCenter.FocusedView, GridView).GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.CODCENATE))
        thirdPartyIdsFilter = String.Join(",", (From x In DirectCast(INDgcThirdParty.FocusedView, GridView).GetSelectedRows() Select DirectCast(DirectCast(INDgcThirdParty.FocusedView, GridView).GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))
        healthAdministratorIdsFilter = String.Join(",", (From x In DirectCast(INDgcHealthAdministrator.FocusedView, GridView).GetSelectedRows() Select DirectCast(DirectCast(INDgcHealthAdministrator.FocusedView, GridView).GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))

        careGroupIdsFilter = String.Join(",", (From x In DirectCast(INDgcCareGroup.FocusedView, GridView).GetSelectedRows() Select DirectCast(DirectCast(INDgcCareGroup.FocusedView, GridView).GetRow(x), Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupXpo).Id))
        userCodesFilter = String.Join(",", (From x In DirectCast(INDgcUsers.FocusedView, GridView).GetSelectedRows() Select DirectCast(DirectCast(INDgcUsers.FocusedView, GridView).GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.UserCode))

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("DateStart", DateStart)
        _criterias.Add("DateEnd", DateEnd)
        _criterias.Add("ReportType", ReportType)
        _criterias.Add("GroupBy", GroupBy)
        _criterias.Add("InvoiceType", InvoiceType)
        If Status <> 3 Then
            _criterias.Add("Status", Status)
        Else
            _criterias.Add("Status", 0)
        End If
        _criterias.Add("CareCenter", careCenterCodesFilter)
        _criterias.Add("CurrencyReport", Currency)

        If Currency > 0 Then
            Await GetDataCurrency(Currency)
        End If

        _filters = New Dictionary(Of String, String)
        _filters.Add("ThirdParty", thirdPartyIdsFilter)
        _filters.Add("Entity", healthAdministratorIdsFilter)
        _filters.Add("CareGroup", careGroupIdsFilter)
        _filters.Add("Users", userCodesFilter)

    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBillingStatistics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PBillingStatistics()
        InitializeTuples()
    End Sub

    ''' <summary>
    ''' Dispose del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed

    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tercero popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceThirdParty.QueryPopUp
        If INDgcThirdParty.DataSource Is Nothing Then
            INDgcThirdParty.DataSource = Presenter.LoadDatasourceThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceHealthAdministrator.QueryPopUp
        If INDgcHealthAdministrator.DataSource Is Nothing Then
            INDgcHealthAdministrator.DataSource = Presenter.LoadDatasourceHealthAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de grupo atención popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceCareGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceCareGroup.QueryPopUp
        If INDgcCareGroup.DataSource Is Nothing Then
            INDgcCareGroup.DataSource = Presenter.LoadDatasourceCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceUsers.QueryPopUp
        If INDgcUsers.DataSource Is Nothing Then
            INDgcUsers.DataSource = Presenter.LoadDatasourceUsers()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centros de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceCareCenter.QueryPopUp
        If INDgcCareCenter.DataSource Is Nothing Then
            INDgcCareCenter.DataSource = Presenter.LoadDatasourceCareCenter()
        End If
    End Sub

    ''' <summary>
    ''' popup para mostrar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            INDsleCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBillingStatistics_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDdteInitialDate.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleReportType.EditValueChanged
        If INDsleReportType.EditValue = 2 Then
            INDccbeInvoiceType.Properties.Items(3).Enabled = False
            INDccbeInvoiceType.Properties.Items(3).CheckState = False
        Else
            INDccbeInvoiceType.Properties.Items(3).Enabled = True
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se ejecuta al presionar click sobre el boton de generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnGenerateReport_Click(sender As Object, e As EventArgs) Handles INDbtnGenerateReport.Click
        If ValidateControlsResport() = False Then
            Exit Sub
        End If

        Await GetFilters()

        AsyncLoader(True)
        Dim reporte As New rptReportBillingStatisticsResume

        reporte.ParametrosReporte = New Object() {_criterias, _filters}

        If Currency > 0 Then
            reporte.Currency = CurrencyData
        End If

        Dim Data = Await reporte.ValidateData()
        If Data.IsValid Then
            INDDvDocumentViewer.DocumentSource = reporte

            Await CType(reporte, IReportAsync).CargarDataSourceAsync
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
            End If
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                INDlyRoot.Visible = False
                INDCncNavigation.Visible = False
                INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                INDdteInitialDate.Focus()
            End If
        Else
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Data.Message
            INDdteInitialDate.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al presionar click sobre el boton de exportar a excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnExportExcel_Click(sender As Object, e As EventArgs) Handles INDbtnExportExcel.Click
        If ValidateControlsResport() = False Then
            Exit Sub
        End If
        AsyncLoader(True)
        Task.Factory.StartNew(AddressOf DataSourceExcel)
    End Sub

    ''' <summary>
    ''' Carga el datasource de la rejilla de excel
    ''' </summary>
    Private Async Sub DataSourceExcel()
        Try
            Await GetFilters()

            Dim XmlCriterias = Utils.DictionaryToXML(_criterias)
            Dim XmlFilters = Utils.DictionaryToXML(_filters)

            Dim resultValidation = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReportBillingStadisticsCountAsync(XmlCriterias, XmlFilters, IndigoSessionValues)
            If resultValidation.IsValid Then
                Dim IndList = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReportBillingStadisticsAsync(XmlCriterias, XmlFilters, IndigoSessionValues)
                If IndList?.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontrarón datos para generar el reporte."
                    Return
                End If

                Dim dt As New DataTable
                dt.Columns.Add("Código Centro Atención", GetType(String))
                dt.Columns.Add("Nombre Centro Atención", GetType(String))
                dt.Columns.Add("Nit", GetType(String))
                dt.Columns.Add("Nombre Tercero", GetType(String))
                dt.Columns.Add("Código Entidad", GetType(String))
                dt.Columns.Add("Nombre Entidad", GetType(String))
                dt.Columns.Add("Código Grupo Atención", GetType(String))
                dt.Columns.Add("Nombre Grupo Atención", GetType(String))
                dt.Columns.Add("Estado", GetType(String))
                dt.Columns.Add("Tipo Documento", GetType(String))
                dt.Columns.Add("No. Factura", GetType(String))
                dt.Columns.Add("Ingreso", GetType(String))
                dt.Columns.Add("Fecha Ingreso")
                dt.Columns.Add("Causa", GetType(String))
                dt.Columns.Add("Tipo Ingreso", GetType(String))
                dt.Columns.Add("Paciente", GetType(String))
                dt.Columns.Add("Tipo Identicación", GetType(String))
                dt.Columns.Add("Primer Nombre", GetType(String))
                dt.Columns.Add("Segundo Nombre", GetType(String))
                dt.Columns.Add("Primer Apellido", GetType(String))
                dt.Columns.Add("Segundo Apellido", GetType(String))
                dt.Columns.Add("Fecha Nacimiento")
                dt.Columns.Add("Edad", GetType(String))
                dt.Columns.Add("Sexo", GetType(String))
                dt.Columns.Add("Moneda", GetType(String))
                dt.Columns.Add("Total Factura", GetType(Decimal))
                dt.Columns.Add("Fecha Factura")
                dt.Columns.Add("Código Unidad Funcional", GetType(String))
                dt.Columns.Add("Nombre Unidad Funcional", GetType(String))
                dt.Columns.Add("Cuota", GetType(Decimal))
                dt.Columns.Add("Valor Entidad", GetType(Decimal))
                dt.Columns.Add("Código Usuario", GetType(String))
                dt.Columns.Add("Nombre Usuario", GetType(String))
                dt.Columns.Add("Usuario Anulación", GetType(String))
                dt.Columns.Add("Fecha Anulación")
                dt.Columns.Add("Motivo Anulación", GetType(String))
                dt.Columns.Add("Categoría Factura", GetType(String))

                For Each itemView In IndList
                    Dim row As DataRow = dt.NewRow()
                    row.Item("Código Centro Atención") = itemView.CareCenterCode
                    row.Item("Nombre Centro Atención") = itemView.CareCenterName
                    row.Item("Nit") = itemView.ThirdPartyNit
                    row.Item("Nombre Tercero") = itemView.ThirdPartyName
                    row.Item("Código Entidad") = itemView.HealthAdministratorCode
                    row.Item("Nombre Entidad") = itemView.HealthAdministratorName
                    row.Item("Código Grupo Atención") = itemView.CareGroupCode
                    row.Item("Nombre Grupo Atención") = itemView.CareGroupName
                    row.Item("Estado") = itemView.StatusDescription
                    row.Item("Tipo Documento") = itemView.DocumentTypeDescription
                    row.Item("No. Factura") = itemView.InvoiceNumber
                    row.Item("Ingreso") = itemView.AdmissionNumber
                    row.Item("Fecha Ingreso") = itemView.AdmissionDate
                    row.Item("Causa") = itemView.CauseIncomeDescription
                    row.Item("Tipo Ingreso") = itemView.AdmissionTypeDescription
                    row.Item("Paciente") = itemView.PatientCode
                    row.Item("Tipo Identicación") = itemView.IdentificationTypeDescription
                    row.Item("Primer Nombre") = itemView.FirstName
                    row.Item("Segundo Nombre") = itemView.SecondName
                    row.Item("Primer Apellido") = itemView.FirstLastName
                    row.Item("Segundo Apellido") = itemView.SecondLastName
                    row.Item("Fecha Nacimiento") = itemView.BirthDate
                    row.Item("Edad") = itemView.PatientAge
                    row.Item("Sexo") = itemView.SexDescription
                    row.Item("Moneda") = itemView.Abbreviation
                    row.Item("Total Factura") = itemView.TotalInvoice
                    row.Item("Fecha Factura") = itemView.InvoiceDate
                    row.Item("Código Unidad Funcional") = itemView.FunctionalUnitCode
                    row.Item("Nombre Unidad Funcional") = itemView.FunctionalUnitName
                    row.Item("Cuota") = itemView.ValueCopay
                    row.Item("Valor Entidad") = itemView.EntityValue
                    row.Item("Código Usuario") = itemView.UserCode
                    row.Item("Nombre Usuario") = itemView.UserName
                    row.Item("Usuario Anulación") = itemView.AnnulmentUser
                    row.Item("Fecha Anulación") = itemView.AnnulmentDate
                    row.Item("Motivo Anulación") = itemView.ReversalReasonDescription
                    row.Item("Categoría Factura") = itemView.InvoiceCategories

                    dt.Rows.Add(row)
                Next

                INDgcExportExcel.DataSource = dt
                generateExcel()
                INDgcExportExcel.SafeInvoke(Sub()
                                                AsyncLoader(False)
                                            End Sub)
            Else
                INDgcExportExcel.SafeInvoke(Sub()
                                                AsyncLoader(False)
                                            End Sub)
                Mensaje(EeventViewerImages.Advertencia) = resultValidation.Message
                Return
            End If
        Catch ex As Exception
            INDgcExportExcel.SafeInvoke(Sub()
                                            AsyncLoader(False)
                                            Throw ex
                                        End Sub)
        End Try
    End Sub


    ''' <summary>
    ''' Se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        INDlyRoot.Visible = True
        INDCncNavigation.Visible = True
        INDPcDocumentViewer.Visible = False
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Evento que se dispara al checkear en la rejilla de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewThirdParty_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewThirdParty.SelectionChanged
        INDpceThirdParty.Text = DirectCast(INDgcThirdParty.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Item Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al checkear en la rejilla de entidad
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

#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Evento load de la barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

End Class
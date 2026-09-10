#Region "Imports"

Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.XtraReports.Parameters
Imports Presentation.Billing.MVP
Imports Presentation.Reporter
Imports Domain.Entities
Imports System.Linq

#End Region

Public Class FrmReportListInvoices

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    ''' <summary>
    ''' obtiene o establece la informacion de la moneda
    ''' </summary>
    Private _currency As Currency

    ''' <summary>
    ''' Parrametros de Facturacion
    ''' </summary>
    Private SettingBilling As SettingsBilling

#End Region

#Region "Datasource"

    Public Property ProoftCloseXpoInitialInvoice As XPInstantFeedbackSource
    Public Property ProoftCloseXpoFinalInvoice As XPInstantFeedbackSource
    Public Property ProoftCloseXpoHealthAdministrator As XPInstantFeedbackSource
    Public Property ProoftCloseXpoPatient As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoInvoiceCategories As XPInstantFeedbackSource
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoAdmissionNumber As XPInstantFeedbackSource
    Public Property ProoftCloseXpoRadicated As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCreationUsers As LinqInstantFeedbackSource

    Public Property CurrencyId As Integer
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
        End Set
    End Property

    Public Property TypeReport As Integer
        Get
            Return INDGleTypeReport.EditValue
        End Get
        Set(value As Integer)
            INDGleTypeReport.EditValue = value
        End Set
    End Property

    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(3, "Radicado con CUV"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

    Private _LoadTypeInvoice As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeInvoice As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeInvoice Is Nothing Then
                _LoadTypeInvoice = New List(Of Tuple(Of Integer, String))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("InvoiceWithContract", "Billing")))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("InvoiceWithoutContract", "Billing")))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("ParticularBill", "Billing")))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("CapitatedBill", "Billing")))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("CapitationControl", "Billing")))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(6, ResourceManager.GetString("BasicBill", "Billing")))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(7, ResourceManager.GetString("ProductSalesInvoice", "Billing")))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(0, "Todos"))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(8, ResourceManager.GetString("HealthBillParentAccount", "Billing")))
            End If
            Return _LoadTypeInvoice
        End Get
    End Property

    Private _LoadStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadStatus As List(Of Tuple(Of Integer, String))
        Get
            If _LoadStatus Is Nothing Then
                _LoadStatus = New List(Of Tuple(Of Integer, String))
                _LoadStatus.Add(New Tuple(Of Integer, String)(1, "Facturado"))
                _LoadStatus.Add(New Tuple(Of Integer, String)(2, "Anulado"))
                _LoadStatus.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _LoadStatus
        End Get
    End Property

    Private _LoadOrder As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadOrder As List(Of Tuple(Of Integer, String))
        Get
            If _LoadOrder Is Nothing Then
                _LoadOrder = New List(Of Tuple(Of Integer, String))
                _LoadOrder.Add(New Tuple(Of Integer, String)(1, "Número Factura"))
                _LoadOrder.Add(New Tuple(Of Integer, String)(2, "Fecha"))
                _LoadOrder.Add(New Tuple(Of Integer, String)(3, "Cliente"))
            End If
            Return _LoadOrder
        End Get
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

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

        'Valida si las fechas son nulas y si el Radicado esta vacio y la Factura esta vacia
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            If Me.INDSleRadicated.EditValue Is Nothing And (INDSleInitialInvoice.EditValue Is Nothing Or INDSleFinalInvoice.EditValue Is Nothing) Then
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

            'valida si alguna de las fechas tiene informacion  y el radiacado tiene datos ..para informar a obligar a completar las dos fechas
        ElseIf ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleRadicated.EditValue IsNot Nothing) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleInitialInvoice.EditValue IsNot Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        'Valida facturas
        If (INDSleInitialInvoice.EditValue Is Nothing And INDSleFinalInvoice.EditValue IsNot Nothing) Or (INDSleInitialInvoice.EditValue Is Nothing And INDSleFinalInvoice.EditValue IsNot Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), "Facturas")
            Me.INDSleInitialInvoice.Focus()
            Validations = False

        ElseIf Not ValidateInvoices() Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), "Facturas")
            Me.INDSleInitialInvoice.Focus()
            Validations = False
        End If

        If (TypeReport = 2 OrElse TypeReport = 3) AndAlso CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CurrencyVoid", "Commons"))
            Me.INDsleCurrency.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    Private Function ValidateInvoices() As Boolean
        Dim InitialInvoice As String = INDSleInitialInvoice.EditValue
        Dim FinalInvoice As String = INDSleFinalInvoice.EditValue
        If InitialInvoice IsNot Nothing AndAlso FinalInvoice IsNot Nothing Then
            Dim numberString As String = Nothing

            Dim InitialTextPart As String = String.Empty
            Dim InitialNumberPart As Int64 = 0
            numberString = System.Text.RegularExpressions.Regex.Match(InitialInvoice, "\d+").Value
            If String.IsNullOrEmpty(numberString) Then
                InitialTextPart = InitialInvoice.ToString().ToLower()
                InitialNumberPart = 0
            Else
                InitialTextPart = InitialInvoice.ToString().Replace(numberString, "").ToLower()
                InitialNumberPart = Convert.ToInt64(numberString)
            End If

            Dim FinalTextPart As String = String.Empty
            Dim FinalNumberPart As Int64 = 0
            numberString = System.Text.RegularExpressions.Regex.Match(FinalInvoice, "\d+").Value
            If String.IsNullOrEmpty(numberString) Then
                FinalTextPart = FinalInvoice.ToString().ToLower()
                FinalNumberPart = 0
            Else
                FinalTextPart = FinalInvoice.ToString().Replace(numberString, "").ToLower()
                FinalNumberPart = Convert.ToInt64(numberString)
            End If

            InitialInvoice = String.Concat(InitialTextPart, InitialNumberPart.ToString().PadLeft(20, "0"))
            FinalInvoice = String.Concat(FinalTextPart, FinalNumberPart.ToString().PadLeft(20, "0"))

            If InitialInvoice > FinalInvoice Then
                Return False
            End If
        End If

        Return True
    End Function

#Region "Load Datasources"

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialInvoice
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialInvoice()
        Using msearch As New MBusqueda
            ProoftCloseXpoInitialInvoice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListViewListInvoiceAndPatient)
            INDSleInitialInvoice.Datasource = ProoftCloseXpoInitialInvoice
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalInvoice
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalInvoice()
        Using msearch As New MBusqueda
            ProoftCloseXpoFinalInvoice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListViewListInvoiceAndPatient)
            INDSleFinalInvoice.Datasource = ProoftCloseXpoFinalInvoice
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleHealthAdministrator
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoHealthAdministrator()
        Using msearch As New MBusqueda
            ProoftCloseXpoHealthAdministrator = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListHealthAdministrator)
            INDSleHealthAdministrator.Datasource = ProoftCloseXpoHealthAdministrator
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePatient
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBillingPatient()
        Using msearch As New MBusqueda
            ProoftCloseXpoPatient = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients)
            INDSlePatient.Datasource = ProoftCloseXpoPatient
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoContractGroup()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCareGroup)
            INDSleGroup.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCategories
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBillingInvoiceCategories()
        Using msearch As New MBusqueda
            ProoftCloseXpoInvoiceCategories = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceCategories)
            INDSleCategories.Datasource = ProoftCloseXpoInvoiceCategories
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdParty
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdParty()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdParty.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleBranchOffice
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBranchOffice()
        Using msearch As New MBusqueda
            Dim filter() As Object = {INDSleThirdParty.EditValue}
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.BranchOfficeByThirdPartyId, filter)
            INDSleBranchOffice.Datasource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

    Private Async Sub LoadBranchOfficeDatasource()
        If INDSleThirdParty.EditValue <> Nothing Then
            'Se obtiene el tercero por id
            Using modelTp As New Common.MVP.MThirdParty(Me.Tag)
                Dim thirdParty = Await modelTp.GetThirdPartyById(INDSleThirdParty.EditValue)
                'Se valida que el tercero maneje sucursal, si maneja se muestra el campo de sucursal
                If thirdParty IsNot Nothing AndAlso thirdParty.HandlesBranchOffice Then
                    INDLciBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAdmissionNumber
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAdmissionNumber()
        Using msearch As New MBusqueda
            ProoftCloseXpoAdmissionNumber = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdmissionsToLiquidation)
            INDsleAdmissionNumber.Datasource = ProoftCloseXpoAdmissionNumber
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleRadicated
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoRadicated()
        Using msearch As New MBusqueda
            ProoftCloseXpoRadicated = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRadicateInvoiceReport)
            INDSleRadicated.Datasource = ProoftCloseXpoRadicated
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleUser
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCreationUsers()
        Using msearch As New MBusqueda
            ProoftCloseXpoCreationUsers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDSleUser.Datasource = ProoftCloseXpoCreationUsers
        End Using
    End Sub

#End Region

#Region "ToExcel"

    Private Sub chargueDatasource()
        If (INDDateStart.EditValue Is Nothing OrElse INDDateEnd.EditValue Is Nothing) Then
            Mensaje(EeventViewerImages.Informacion) = "Debe seleccionar una fecha inicial y final"
        Else
            Dim filtroConsulta As String = "InvoiceDate >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd HH:mm:ss") & "# AND InvoiceDate <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd HH:mm:ss") & "#"

            If INDGleTypeInvoice.EditValue <> 0 Then
                filtroConsulta &= String.Format(" AND DocumentType = {0}", INDGleTypeInvoice.EditValue)
            End If

            If INDGleStatus.EditValue <> 3 Then
                filtroConsulta &= String.Format(" AND Status = {0}", INDGleStatus.EditValue)
            End If

            If INDSleInitialInvoice.EditValue IsNot Nothing AndAlso INDSleFinalInvoice.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND InvoiceNumber >= '{0}' AND InvoiceNumber <= '{1}'", INDSleInitialInvoice.EditValue, INDSleFinalInvoice.EditValue)
            End If

            If INDSleHealthAdministrator.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND HealthAdministratorId = {0}", INDSleHealthAdministrator.EditValue)
            End If

            If INDsleAdmissionNumber.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND AdmissionNumber = '{0}'", INDsleAdmissionNumber.EditValue)
            End If

            If INDSlePatient.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND PatientCode = '{0}'", INDSlePatient.EditValue)
            End If

            If INDSleGroup.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND CareGroupId = {0}", INDSleGroup.EditValue)
            End If

            If INDSleCategories.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND InvoiceCategoryId = {0}", INDSleCategories.EditValue)
            End If

            If INDSleThirdParty.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND ThirdPartyId = {0}", INDSleThirdParty.EditValue)
            End If

            If INDSleBranchOffice.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND SucursalId = {0}", INDSleBranchOffice.EditValue)
            End If

            If INDSleRadicated.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND RadicateInvoiceId = {0}", INDSleRadicated.EditValue)
            End If

            If INDSleUser.EditValue IsNot Nothing Then
                filtroConsulta &= String.Format(" AND InvoicedUser = '{0}'", INDSleUser.EditValue)
            End If

            If CurrencyId > 0 Then
                filtroConsulta &= String.Format(" AND CurrencyId = {0}", CurrencyId)
            End If

            If TypeReport = 3 Then
                filtroConsulta &= " AND CUV IS NOT NULL AND CUV <> ''"
            End If

            Dim IndList = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.GetCollection(Of BillingVReportListInvoice)(Nothing, filtroConsulta)

            If IndList IsNot Nothing AndAlso IndList.Any() Then
                IndList = IndList.GroupBy(Function(m) m.InvoiceId).Select(Function(m) m.FirstOrDefault()).ToList()
            End If

            Dim dt As New DataTable
            dt.Columns.Add("Tipo")
            dt.Columns.Add("Código")
            dt.Columns.Add("Factura")
            dt.Columns.Add("Fecha", GetType(DateTime))
            dt.Columns.Add("Observación")
            dt.Columns.Add("Nit")
            dt.Columns.Add("Tercero")
            dt.Columns.Add("Ciudad")
            dt.Columns.Add("Subtotal", GetType(Decimal))
            dt.Columns.Add("Descuento", GetType(Decimal))
            dt.Columns.Add("Valor Paciente", GetType(Decimal))
            dt.Columns.Add("IVA", GetType(Decimal))
            dt.Columns.Add("Total", GetType(Decimal))
            dt.Columns.Add("Moneda")
            dt.Columns.Add("Usuario")
            dt.Columns.Add("Estado")

            If TypeReport = 3 Then
                dt.Columns.Add("CUV")
            End If

            If IndList.Any() Then
                For Each itemView In IndList
                    Dim row As DataRow = dt.NewRow()

                    Select Case itemView.DocumentType
                        Case 1
                            row.Item("Tipo") = ResourceManager.GetString("InvoiceWithContract", "Billing")
                        Case 2
                            row.Item("Tipo") = ResourceManager.GetString("InvoiceWithoutContract", "Billing")
                        Case 3
                            row.Item("Tipo") = ResourceManager.GetString("ParticularBill", "Billing")
                        Case 4
                            row.Item("Tipo") = ResourceManager.GetString("CapitatedBill", "Billing")
                        Case 5
                            row.Item("Tipo") = ResourceManager.GetString("CapitationControl", "Billing")
                        Case 6
                            row.Item("Tipo") = ResourceManager.GetString("BasicBill", "Billing")
                        Case Else
                            row.Item("Tipo") = ResourceManager.GetString("ProductSalesInvoice", "Billing")
                    End Select

                    row.Item("Código") = itemView.Code
                    row.Item("Factura") = itemView.InvoiceNumber
                    row.Item("Fecha") = itemView.InvoiceDate
                    row.Item("Observación") = itemView.Observation
                    row.Item("Nit") = itemView.ThirdPartyNit
                    row.Item("Tercero") = itemView.ThirdPartyName
                    row.Item("Ciudad") = itemView.CityName
                    row.Item("Subtotal") = itemView.Subtotal
                    row.Item("Descuento") = itemView.DiscountValue
                    row.Item("Valor Paciente") = itemView.PatientValue
                    row.Item("IVA") = itemView.ValueTax
                    row.Item("Total") = itemView.TotalValue
                    row.Item("Moneda") = itemView.Abbreviation
                    row.Item("Usuario") = itemView.InvoicedUser
                    row.Item("Estado") = itemView.StatusName

                    If TypeReport = 3 Then
                        row.Item("CUV") = itemView.CUV
                    End If

                    dt.Rows.Add(row)
                Next

                INDGcGenerateExcel.DataSource = dt
            End If
        End If
    End Sub

    Private Sub generateExcel()
        Dim _gridView = INDGcGenerateExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcGenerateExcel.DataSource = Nothing
        Me.INDGcGenerateExcel.RefreshDataSource()
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmReportListInvoices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleInitialInvoice.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInvoiceByNumberInvoice
        Me.INDSleFinalInvoice.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInvoiceByNumberInvoice
        Me.INDSleHealthAdministrator.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetHealthAdministratorByCode
        Me.INDSlePatient.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPattientByCode
        Me.INDSleGroup.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCareGroupByCode
        Me.INDSleCategories.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCategoryByCode
        Me.INDSleThirdParty.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDsleAdmissionNumber.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAdmissionNumberByNumberListBilling
        Me.INDSleUser.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
    End Sub

    Private Sub FrmReportListInvoices_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport
        Me.INDGleTypeInvoice.Properties.DataSource = LoadTypeInvoice
        Me.INDGleStatus.Properties.DataSource = LoadStatus
        Me.INDGleOrder.Properties.DataSource = LoadOrder

        'Dar un valor por defecto a los GridLookEdit
        Me.TypeReport = 1
        Me.INDGleTypeInvoice.EditValue = 0
        Me.INDGleStatus.EditValue = 3
        Me.INDGleOrder.EditValue = 1

        INDSleHealthAdministrator.View.OptionsView.ShowGroupPanel = False
        INDSlePatient.View.OptionsView.ShowGroupPanel = False
        INDSleGroup.View.OptionsView.ShowGroupPanel = False
        INDSleCategories.View.OptionsView.ShowGroupPanel = False
        INDSleThirdParty.View.OptionsView.ShowGroupPanel = False
        INDsleAdmissionNumber.View.OptionsView.ShowGroupPanel = False
        INDSleUser.View.OptionsView.ShowGroupPanel = False
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing

        ProoftCloseXpoInitialInvoice = Nothing
        ProoftCloseXpoFinalInvoice = Nothing
        ProoftCloseXpoHealthAdministrator = Nothing
        ProoftCloseXpoPatient = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoInvoiceCategories = Nothing
        ProoftCloseXpoThirdParty = Nothing
        ProoftCloseXpoBranchOffice = Nothing
        ProoftCloseXpoAdmissionNumber = Nothing
        ProoftCloseXpoRadicated = Nothing
        ProoftCloseXpoCreationUsers = Nothing

        _LoadTypeReport = Nothing
        _LoadTypeInvoice = Nothing
        _LoadStatus = Nothing
        _LoadOrder = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleInitialInvoice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialInvoice.QueryPopUp
        If INDSleInitialInvoice.Datasource Is Nothing Then
            LoadXpoInitialInvoice()
        End If
    End Sub

    Private Sub INDSleFinalInvoice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalInvoice.QueryPopUp
        If INDSleFinalInvoice.Datasource Is Nothing Then
            LoadXpoFinalInvoice()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleHealthAdministrator
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleHealthAdministrator_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleHealthAdministrator.QueryPopUp
        If INDSleHealthAdministrator.Datasource Is Nothing Then
            LoadXpoHealthAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSlePatient
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePatient_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePatient.QueryPopUp
        If INDSlePatient.Datasource Is Nothing Then
            LoadXpoBillingPatient()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroup.QueryPopUp
        If INDSleGroup.Datasource Is Nothing Then
            LoadXpoContractGroup()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleCategories
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCategories_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCategories.QueryPopUp
        If INDSleCategories.Datasource Is Nothing Then
            LoadXpoBillingInvoiceCategories()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleThirdparty
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Datasource Is Nothing Then
            LoadXpoThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleBranchOffice
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBranchOffice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBranchOffice.QueryPopUp
        If INDSleBranchOffice.Datasource Is Nothing AndAlso INDSleThirdParty.EditValue IsNot Nothing Then
            LoadXpoBranchOffice()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDsleAdmissionNumber
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdmissionNumber_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAdmissionNumber.QueryPopUp
        If INDsleAdmissionNumber.Datasource Is Nothing Then
            LoadXpoAdmissionNumber()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleCategories
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleRadicated_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRadicated.QueryPopUp
        If INDSleRadicated.Datasource Is Nothing Then
            LoadXpoRadicated()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleUser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleUser_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUser.QueryPopUp
        If INDSleUser.Datasource Is Nothing Then
            LoadXpoCreationUsers()
        End If
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDsleCurrency
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            INDsleCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If TypeReport = 1 Then
            INDLciOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleOrder.EditValue = 1
            INDlyCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            CurrencyId = Nothing
        ElseIf TypeReport = 2 OrElse TypeReport = 3 Then
            INDLciOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleOrder.EditValue = Nothing
            INDlyCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    Private Sub INDGleTypeInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeInvoice.EditValueChanged
        If INDGleTypeInvoice.EditValue = 6 OrElse INDGleTypeInvoice.EditValue = 7 Then
            INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPatient.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCategories.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciAdmissionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRadicated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciExportExcel.ShowLayout()

            If INDGleTypeInvoice.EditValue = 7 Then
                Me.LoadBranchOfficeDatasource()
            End If
        Else
            INDLciHealthAdministrator.ShowLayout()
            INDLciPatient.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCategories.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciAdmissionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciRadicated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciExportExcel.ShowLayout()

            If INDGleTypeInvoice.EditValue = 8 Then
                INDLciExportExcel.HideLayout()
            End If
        End If
    End Sub

    Private Sub INDSleInitialInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleInitialInvoice.EditValueChanged
        If INDSleInitialInvoice.EditValue IsNot Nothing And INDSleFinalInvoice.EditValue Is Nothing Then
            INDSleFinalInvoice.EditValue = INDSleInitialInvoice.EditValue
            INDSleFinalInvoice.DisplayNullText = INDSleInitialInvoice.TextEditValue
        ElseIf INDSleInitialInvoice.EditValue Is Nothing And INDSleFinalInvoice.EditValue IsNot Nothing Then
            INDSleFinalInvoice.EditValue = Nothing
            INDSleFinalInvoice.DisplayNullText = String.Empty
        End If
    End Sub

    Private Sub INDSleFinalInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleFinalInvoice.EditValueChanged
        If INDSleInitialInvoice.EditValue IsNot Nothing And INDSleFinalInvoice.EditValue Is Nothing Then
            INDSleInitialInvoice.EditValue = Nothing
            INDSleInitialInvoice.DisplayNullText = String.Empty
        ElseIf INDSleInitialInvoice.EditValue Is Nothing And INDSleFinalInvoice.EditValue IsNot Nothing Then
            INDSleInitialInvoice.EditValue = INDSleFinalInvoice.EditValue
            INDSleInitialInvoice.DisplayNullText = INDSleFinalInvoice.TextEditValue
        End If
    End Sub

    Private Sub INDSleThirdParty_EditValueChanged(sender As Object, e As Controls.EditValueChangedEventArgs) Handles INDSleThirdParty.EditValueChanged
        'Se pone null el datasource y el campo de la sucursal
        INDSleBranchOffice.Datasource = Nothing
        INDSleBranchOffice.EditValue = Nothing
        INDSleBranchOffice.DisplayNullText = String.Empty
        INDLciBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If INDGleTypeInvoice.EditValue = 7 Then
            Me.LoadBranchOfficeDatasource()
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbReportGenerate
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Not Me.ValidateControlsReports Then Exit Sub

        If INDDateStart.EditValue Is Nothing OrElse INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = "Debe seleccionar una fecha inicial y final"
            Exit Sub
        End If

        AsyncLoader(True)

        Dim parametrosBase As Object() = {
                INDDateStart.EditValue,
                INDDateEnd.EditValue,
                INDGleTypeInvoice.EditValue,
                INDGleStatus.EditValue,
                INDSleInitialInvoice.EditValue,
                INDSleFinalInvoice.EditValue,
                INDSleHealthAdministrator.EditValue,
                INDSlePatient.EditValue,
                INDSleGroup.EditValue,
                INDSleCategories.EditValue,
                INDSleThirdParty.EditValue,
                INDSleBranchOffice.EditValue,
                If(INDsleAdmissionNumber.EditValue Is Nothing, Nothing, String.Format("'{0}'", INDsleAdmissionNumber.EditValue)),
                INDSleRadicated.EditValue,
                INDSleUser.EditValue
            }

        If TypeReport = 1 Then

            Dim reporte As New rptListInvoiceResume
            reporte.ParametrosReporte = parametrosBase.Concat({
                 INDGleOrder.EditValue,
                INDSleHealthAdministrator.TextEditValue,
                INDSlePatient.TextEditValue,
                INDSleGroup.TextEditValue,
                INDSleCategories.TextEditValue,
                INDSleThirdParty.TextEditValue,
                INDsleAdmissionNumber.TextEditValue,
                INDSleRadicated.TextEditValue,
                INDSleUser.TextEditValue
            }).ToArray()

            INDDvReport.DocumentSource = reporte
            reporte.CargarDataSource()

            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
                Me.INDLcBaseHome.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If

        ElseIf TypeReport = 2 OrElse TypeReport = 3 Then

            If SettingBilling Is Nothing Then
                Using model As New MBillingSetting(Me.Tag)
                    SettingBilling = Await model.GetSettingsBillingByIdUnitOperative(BarraBotones.OperatingUnit.Id.ToString(), False)
                End Using
            End If

            If CurrencyId > 0 Then
                Using model As New MCurrency(MyBase.Tag)
                    _currency = Await model.GetCurrencyById(CurrencyId)
                End Using
            End If

            Dim reporte As New rptSubSaleInvoiceAll
            reporte.Currency = _currency
            reporte.Parameters("LiquidateMasterAccount").Value = SettingBilling?.LiquidateMasterAccount
            reporte.ParametrosReporte = parametrosBase.Concat({
                CurrencyId,
                TypeReport
            }).ToArray()

            INDDvReport.DocumentSource = reporte
            reporte.CargarDataSource()

            If TryCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
                Me.INDLcBaseHome.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If

            AsyncLoader(False)
        End If

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBaseHome.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    Private Async Sub INDSbExportExcel_Click(sender As Object, e As EventArgs) Handles INDSbExportExcel.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await Task.Factory.StartNew(Sub()
                                                chargueDatasource()
                                            End Sub)
                If Me.INDGcGenerateExcel.DataSource IsNot Nothing Then
                    generateExcel()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

#End Region

#End Region

End Class
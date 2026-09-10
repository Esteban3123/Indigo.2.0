Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Glosas.MVP
Imports Presentation.Billing.MVP
Imports DevExpress.Xpo

Public Class FrmInvoiceTraceability
    Implements IInvoiceTraceability

#Region "Variables"
    ''' <summary>
    ''' Presentador de la trazabilidad de la factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PInvoiceTraceability
#End Region

#Region "fields"
    ''' <summary>
    ''' Parrametros de Facturacion
    ''' </summary>
    Private SettingBilling As SettingsBilling
    ''' <summary>
    ''' parametros de la moneda
    ''' </summary>
    Private _currencyData As Currency
    ''' <summary>
    ''' variable privada que guarda los datos de la moneda oficial para no reconsultar
    ''' </summary>
    Private _currencyDataOfficial As Currency
    ''' <summary>
    ''' variable que almacena la abreviacion de la moneda
    ''' </summary>
    Private _currencyASelected As String
    ''' <summary>
    ''' presenter del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PBasicBilling
    ''' <summary>
    ''' bandera para identificarcuando se consulta un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private _isLoad As Boolean
#End Region

#Region "ICRUD"

    Public Sub Buscar() Implements Base.ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements Base.ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    Public Property DataSourceBranch As List(Of Domain.Entities.GlosasParametersInterface) Implements IInvoiceTraceability.DataSourceBranch
        Get
            Return INDgluContainer.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.GlosasParametersInterface))
            INDgluContainer.Properties.DataSource = value
            If value IsNot Nothing AndAlso value.Count = 1 Then
                INDgluContainer.EditValue = value(0).ContainerName
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar e inactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInvoiceTraceability.ActionsOnControls
        Set(value As Boolean)
            If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                INDgluContainer.Enabled = Not value
                If INDgluContainer.EditValue IsNot Nothing Then
                    INDtxtInvoiceNumber.Enabled = True
                Else
                    INDtxtInvoiceNumber.Enabled = False
                End If
                INDgluContainer.Focus()
            Else
                INDbteInvoiceNumber.Focus()
            End If
            INDtxtInvoiceDate.Enabled = value
            INDtxtBillerName.Enabled = value
            INDtxtContractCode.Enabled = value
            INDtxtStateERPIntegration.Enabled = value
            INDtxtPortfolioStatus.Enabled = value
            INDtxtCurrency.Enabled = False
            INDtxtInvoiceValue.Enabled = value
            INDtxtPatientValue.Enabled = value
            INDgcRadication.Enabled = value
            INDgcDevolution.Enabled = value
            INDtxtObjectionCode.Enabled = value
            INDtxtObjectionDate.Enabled = value
            INDtxtObjectedValue.Enabled = value
            INDtxtAcceptedValue1.Enabled = value
            INDtxtReiterationCode.Enabled = value
            INDtxtReiterationDate.Enabled = value
            INDtxtRepeatedValue.Enabled = value
            INDtxtAcceptedValue2.Enabled = value
            INDgcConciliation.Enabled = value
            INDtxtBillTotal.Enabled = value
            INDtxtCurrentBalance.Enabled = value
            INDtxtCurrentRetention.Enabled = value
            INDtxtGlossedTotal.Enabled = value
            INDtxtPartialPayments.Enabled = value
            INDtxtBalanceReconcile.Enabled = value
            INDtxtTotalAcceptedIPS.Enabled = value
            INDtxtTotalAcceptedEAPB.Enabled = value
            INDtxtState.Enabled = value
            INDmeComment.Enabled = value
            INDtxtCustomer.Enabled = value
            INDPceMoreInfo.Enabled = value
            INDPceMoreInfoRetentions.Enabled = value
        End Set
    End Property

    Private _invoiceTraceability As SP_InvoiceTraceability_Result = Nothing
    Public Property InvoiceTraceability As SP_InvoiceTraceability_Result Implements IInvoiceTraceability.InvoiceTraceability
        Get
            Return _invoiceTraceability
        End Get
        Set(value As SP_InvoiceTraceability_Result)
            _invoiceTraceability = value
            If value IsNot Nothing Then
                Me._isLoad = True
                INDtxtInvoiceDate.EditValue = value.InvoiceDate
                INDtxtBillerName.EditValue = value.BillerName
                INDtxtContractCode.EditValue = value.ContractCode
                INDtxtStateERPIntegration.EditValue = value.StateERPIntegration
                INDtxtPortfolioStatus.EditValue = value.PortfolioStatusName
                INDtxtInvoiceValue.EditValue = value.InvoiceValueEntity
                INDtxtPatientValue.EditValue = value.InvoiceValuePacient
                INDtxtObjectionCode.EditValue = value.ObjectionCode
                If value.ObjectionCode > 0 Then
                    INDlyGroupGlosas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyGroupGlosas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
                INDtxtObjectionDate.EditValue = value.ObjectionDate
                INDtxtObjectedValue.EditValue = value.ObjectionValue
                INDtxtAcceptedValue1.EditValue = value.ValueAcceptedFirstInstance
                If value.ReiterationCode > 0 Then
                    INDlyItemReiterationCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemReiterationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemRepeatedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDtxtReiterationCode.EditValue = value.ReiterationCode
                    INDtxtReiterationDate.EditValue = value.ReiterationDate
                    INDtxtRepeatedValue.EditValue = value.ValueReiterated
                End If
                INDtxtAcceptedValue2.EditValue = value.ValueAcceptedSecondInstance
                INDtxtBillTotal.EditValue = value.InvoiceTotal
                INDtxtCurrentBalance.EditValue = value.PortfolioCurrentBalance
                INDtxtCurrentRetention.EditValue = value.PortfolioCurrentRetention
                INDtxtGlossedTotal.EditValue = value.GlossedTotal
                INDtxtPartialPayments.EditValue = value.ValuePayments
                INDtxtBalanceReconcile.EditValue = value.BalanceReconcile
                INDtxtTotalAcceptedIPS.EditValue = value.TotalAcceptIPS
                INDtxtTotalAcceptedEAPB.EditValue = value.TotalAcceptEAPB
                INDtxtState.EditValue = value.State
                INDtxtCustomer.EditValue = value.Customer
                CurrencyId(value.CurrencyAbbreviation) = value.CurrencyId
                ValidateCurrencyEditValue(value.CurrencyId, value.CurrencyAbbreviation, _isLoad)
                INDmeComment.EditValue = value.TimeResponse
                Me._isLoad = False
                INDlyItemDocumentLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDHleDocument.Text = "Factura" + " - " + INDbteInvoiceNumber.EditValue

                Dim InvoiceNUmber As String
                Dim Containers As String
                Dim ObjCompany As GlosasParametersInterface = INDgluContainer.GetSelectedDataRow()
                If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                    InvoiceNUmber = INDtxtInvoiceNumber.Text
                    Containers = ObjCompany.ContainerName
                Else
                    InvoiceNUmber = INDbteInvoiceNumber.EditValue
                    Containers = String.Empty
                End If
                Me.BarraBotones.PrintReport(PrintReportAction.None, 0, 0, {Containers, InvoiceNUmber, Me.BarraBotones.OperatingUnit})
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
            Else
                MessageIndigo.Show(ResourceManager.GetString("FacturaNoExiste", "Glosas"), MessageType.Warning, Me.Text)
            End If
        End Set
    End Property

    Public WriteOnly Property InvoiceTraceabilityConciliation As List(Of SP_InvoiceTraceabilityConciliation_Result) Implements IInvoiceTraceability.InvoiceTraceabilityConciliation
        Set(value As List(Of SP_InvoiceTraceabilityConciliation_Result))
            If value IsNot Nothing AndAlso value.Count > 0 Then
                INDgcConciliation.DataSource = value
                INDgcConciliation.RefreshDataSource()
                INDlyGroupConciliation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlyGroupConciliation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    Public WriteOnly Property InvoiceTraceabilityDevolution As List(Of SP_InvoiceTraceabilityDevolution_Result) Implements IInvoiceTraceability.InvoiceTraceabilityDevolution
        Set(value As List(Of SP_InvoiceTraceabilityDevolution_Result))
            If value IsNot Nothing AndAlso value.Count > 0 Then
                INDgcDevolution.DataSource = value
                INDlygDevolution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlygDevolution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    Public WriteOnly Property InvoiceTraceabilityRadication As List(Of SP_InvoiceTraceabilityRadication_Result) Implements IInvoiceTraceability.InvoiceTraceabilityRadication
        Set(value As List(Of SP_InvoiceTraceabilityRadication_Result))
            INDgcRadication.DataSource = value
            If value IsNot Nothing AndAlso value.Count > 0 Then
                INDgcRadication.DataSource = value
                INDlyGroupRadication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlyGroupRadication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el id de la moneda del documento
    ''' establece opcionalmente la abreviacion
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional currencyAbbreviation As String = Nothing) As Integer Implements IInvoiceTraceability.CurrencyId
        Get
            Return INDtxtCurrency.EditValue
        End Get
        Set(value As Integer)
            If (value <> indigo?.OfficialCurrencyId) AndAlso String.IsNullOrEmpty(currencyAbbreviation) Then
                value = indigo?.OfficialCurrencyId
                currencyAbbreviation = indigo?.CurrencyISO4217
            End If
            INDtxtCurrency.EditValue = value
            INDtxtCurrency.Properties.NullText = currencyAbbreviation
            SetFormatCurrencyUI(currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' propiedad de lectura que lee la abbreviacion de la moneda
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyASelected As String
        Get
            Return _currencyASelected
        End Get
    End Property
#End Region

#Region "XPO"
    Public Property CurrencyXpo As XPInstantFeedbackSource Implements IInvoiceTraceability.CurrencyXpo
        Get
            Return TryCast(INDtxtCurrency.EditValue, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDtxtCurrency.EditValue = value
        End Set
    End Property

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDtxtInvoiceNumber.EditValue = Nothing
        INDbteInvoiceNumber.EditValue = Nothing
        INDtxtInvoiceDate.EditValue = Nothing
        INDtxtBillerName.EditValue = Nothing
        INDtxtContractCode.EditValue = Nothing
        INDtxtStateERPIntegration.EditValue = Nothing
        INDtxtPortfolioStatus.EditValue = Nothing
        CurrencyId = Nothing
        INDtxtInvoiceValue.EditValue = Nothing
        INDtxtPatientValue.EditValue = Nothing
        INDgcRadication.DataSource = Nothing
        INDgcDevolution.DataSource = Nothing
        INDgcConciliation.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDgcRadication)
        IndigoGridControl1.RefreshGrid(INDgcDevolution)
        IndigoGridControl1.RefreshGrid(INDgcConciliation)
        INDtxtObjectionCode.EditValue = Nothing
        INDtxtObjectionDate.EditValue = Nothing
        INDtxtObjectedValue.EditValue = Nothing
        INDtxtAcceptedValue1.EditValue = Nothing
        INDtxtReiterationCode.EditValue = Nothing
        INDtxtReiterationDate.EditValue = Nothing
        INDtxtRepeatedValue.EditValue = Nothing
        INDtxtAcceptedValue2.EditValue = Nothing
        INDtxtBillTotal.EditValue = Nothing
        INDtxtCurrentBalance.EditValue = Nothing
        INDtxtCurrentRetention.EditValue = Nothing
        INDtxtGlossedTotal.EditValue = Nothing
        INDtxtPartialPayments.EditValue = Nothing
        INDtxtBalanceReconcile.EditValue = Nothing
        INDtxtTotalAcceptedIPS.EditValue = Nothing
        INDtxtTotalAcceptedEAPB.EditValue = Nothing
        INDtxtState.EditValue = Nothing
        INDmeComment.EditValue = Nothing
        INDtxtCustomer.EditValue = Nothing
        INDGcMoreInfo.DataSource = Nothing
        INDGcMoreInfoRetentions.DataSource = Nothing
        CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
        GetRoundingType()
        INDlyGroupRadication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygDevolution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyGroupConciliation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyGroupGlosas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemDocumentLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ActionsOnControls = False
    End Sub

    Private Sub CleanDataReiteration()
        INDtxtReiterationCode.EditValue = Nothing
        INDtxtReiterationDate.EditValue = Nothing
        INDtxtRepeatedValue.EditValue = Nothing
        INDtxtAcceptedValue2.EditValue = Nothing
        INDlyItemReiterationCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemReiterationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemRepeatedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Async Function GetRoundingType(Optional currencyId As Integer? = Nothing) As Task
        Using model As New Presentation.Common.MVP.MCurrency(Tag)

            If Me._currencyDataOfficial Is Nothing Then
                Me._currencyDataOfficial = Await model.GetCurrencyById(indigo.OfficialCurrencyId)
            End If

            If currencyId Is Nothing OrElse currencyId = indigo.OfficialCurrencyId Then
                Me._currencyData = Me._currencyDataOfficial.Clone()
                Return
            End If

            Me._currencyData = Await model.GetCurrencyById(currencyId)
        End Using
    End Function

    ''' <summary>
    ''' funcion Valida y establece el evento cuando la moneda cambia 
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ValidateCurrencyEditValue(_currencyId As Integer?,
                                                     currencyAbbreviation As String, Optional loadControl As Boolean = False) As Task
        If _currencyId Is Nothing OrElse String.IsNullOrEmpty(currencyAbbreviation) Then
            Return
        End If

        Await GetRoundingType(_currencyId)

        If Not loadControl Then
            Me.SetFormatCurrencyUI(currencyAbbreviation)
        End If
        Return
    End Function

    ''' <summary>
    ''' funcion que centraliza el formato de la moneda dentro del formulario
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>

    Private Sub SetFormatCurrencyUI(currencyAbbreviation As String)
        If String.IsNullOrEmpty(currencyAbbreviation) Then
            currencyAbbreviation = indigo.CurrencyISO4217
            Mensaje(EeventViewerImages.Advertencia) = "La moneda esta llegando vacia"
        End If
        Me._currencyASelected = currencyAbbreviation
        Me.changeNumericFormatByCurrency(currencyAbbreviation.GetNumberFormat)
        'Me._ctrTotal.CurrencyAbbreviation = currencyAbbreviation
        GridColumn23 = Window.Utils.FormatGrid(Me.GridColumn23, currencyAbbreviation)
        GridColumn24 = Window.Utils.FormatGrid(Me.GridColumn24, currencyAbbreviation)
        GridColumn26 = Window.Utils.FormatGrid(Me.GridColumn26, currencyAbbreviation)
        GridColumn28 = Window.Utils.FormatGrid(Me.GridColumn28, currencyAbbreviation)

    End Sub

#End Region

#Region "Eventos"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _isLoad = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cargar el frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInvoiceTraceability_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecordEnabled = False
        Presenter = New PInvoiceTraceability(Me)
        Presenter.Initializes()
        Me.indigo = SessionValues.Instance
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
            INDlyItemContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiERPStateIntegration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiNativeModeInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyItemContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiERPStateIntegration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiNativeModeInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        Deshacer()
        INDlyItemReiterationCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemReiterationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemRepeatedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyGroupGlosas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al estar cargado toda la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub INDgluContainer_EditValueChanged(sender As Object, e As EventArgs) Handles INDgluContainer.EditValueChanged
        If INDgluContainer.EditValue IsNot Nothing Then
            INDtxtInvoiceNumber.Enabled = True
        Else
            INDtxtInvoiceNumber.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' evento al cambiar la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtCurrency.EditValueChanged
        Dim currencyAbbreviation = TryCast(TryCast(INDtxtCurrency?.EditValue, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, CommonCurrencyXpo)?.Abbreviation

        If INDtxtCurrency.EditValue Is Nothing OrElse _isLoad OrElse String.IsNullOrEmpty(currencyAbbreviation) Then
            Exit Sub
        End If
        Me._presenter.InitializateCurrency()
        Await Me.ValidateCurrencyEditValue(Me.CurrencyId, currencyAbbreviation)
    End Sub

    Private Async Sub INDtxtInvoiceNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtInvoiceNumber.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDtxtInvoiceNumber.EditValue IsNot Nothing AndAlso INDtxtInvoiceNumber.Text.Trim.Length > 0 Then
                ActionsOnControls = True
                If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                    Dim ObjCompany As GlosasParametersInterface = INDgluContainer.GetSelectedDataRow()
                    Dim invoiceNumberFixed As String = Utils.FixInvoiceNumber(INDtxtInvoiceNumber.Text.Trim(), ObjCompany.AccountingMethod)
                    INDtxtInvoiceNumber.Text = invoiceNumberFixed
                    AsyncLoader(True)
                    Await Presenter.LoadInvoice(ObjCompany.ContainerName, invoiceNumberFixed)
                    IndigoGridControl1.AcceptXPO = True
                    Using model As New MInvoiceTraceability(Me.Tag)
                        INDGcMoreInfo.DataSource = model.GetAllExtractAccountReceivableByDocumentNumber(invoiceNumberFixed)
                        INDGcMoreInfoRetentions.DataSource = model.GetAllInvoiceCustomerRetention(invoiceNumberFixed)
                    End Using

                    AsyncLoader(False)
                End If
            End If
        End If
    End Sub

    Private Sub RepositoryItemPopupContainerEdit1_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles RepositoryItemPopupContainerEdit1.QueryPopUp
        Dim mainView1 As GridView = INDgvDevolution
        Dim ObjD As SP_InvoiceTraceabilityDevolution_Result = TryCast(INDgvDevolution.GetRow(INDgvDevolution.FocusedRowHandle), SP_InvoiceTraceabilityDevolution_Result)
        CtrXtraInfoInvoiceDetail.ListProperties.Clear()
        CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Estado Oficio", ObjD.StateRadicate))
        CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Estado Factura", ObjD.StateInvoice))
        CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Concepto", ObjD.NameSpecific, 50))
        CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Comentario", ObjD.Comment, 70))
        CtrXtraInfoInvoiceDetail.ListProperties.Add(New XtraInfoProperty("Respuesta", ObjD.Answer, 70))
    End Sub

    Private Sub FrmInvoiceTraceability_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDgluContainer.Focus()
    End Sub


    ''' <summary>
    ''' Abrir busqueda de consecutivos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteInvoiceNumber.ButtonClick
        Me.AbrirBusquedaConsecutive()
    End Sub

    ''' <summary>
    ''' Abrirs the busqueda consecutive.
    ''' </summary>
    Public Sub AbrirBusquedaConsecutive()
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Número de Factura", .FieldName = "InvoiceNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                               New ColumnInfo() With {.Caption = "Documento Electronico", .FieldName = "InvoiceId.ElectronicInvoiceNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                               New ColumnInfo() With {.Caption = "Cliente", .FieldName = "CustomerId.NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "ExpiredDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}
                             }.ToList
            .ValorSolicitado = "InvoiceNumber"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAccountReceivable
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteInvoiceNumber.Text = ReturnValue
        INDbteInvoiceNumber.Focus()
    End Sub


    Private Async Sub INDbteInvoiceNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteInvoiceNumber.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Try
                If INDbteInvoiceNumber.EditValue IsNot Nothing AndAlso INDbteInvoiceNumber.Text.Trim.Length > 0 Then
                    ActionsOnControls = True
                    AsyncLoader(True)
                    Await Presenter.LoadInvoice(String.Empty, INDbteInvoiceNumber.EditValue.Trim())
                    If InvoiceTraceability.ReiterationCode = 0 Then
                        CleanDataReiteration()
                    End If
                    IndigoGridControl1.AcceptXPO = True
                        Using model As New MInvoiceTraceability(Me.Tag)
                            INDGcMoreInfo.DataSource = model.GetAllExtractAccountReceivableByDocumentNumber(INDbteInvoiceNumber.EditValue.Trim())
                            INDGcMoreInfoRetentions.DataSource = model.GetAllInvoiceCustomerRetention(INDbteInvoiceNumber.EditValue.Trim())
                        End Using
                        AsyncLoader(False)
                    End If
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Sub

    Private Async Sub INDHleDocument_OpenLink(sender As Object, e As DevExpress.XtraEditors.Controls.OpenLinkEventArgs) Handles INDHleDocument.OpenLink
        Dim INDLIst As List(Of InvoicesXpo) = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.GetCollection(Of InvoicesXpo)(Nothing, "InvoiceNumber = '" & INDbteInvoiceNumber.EditValue & "'")
        If INDLIst.Count > 0 Then
            If INDLIst(0).DocumentType = 7 Then
                Dim reportDef As New Reporter.rptInvoiceProducts
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, {0, INDLIst(0).Id})
                reportDef.ParametrosReporte = New Object() {INDLIst(0).Id}
            ElseIf INDLIst(0).DocumentType = 6 Then
                Dim reportDef As New Reporter.rptBasicBilling
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, {0, INDLIst(0).Id})
                reportDef.ParametrosReporte = New Object() {INDLIst(0).Id}
            ElseIf INDLIst(0).DocumentType = 4 Then
                Dim reportDef As New Reporter.rptSaleInvoiceCapitated
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, INDLIst(0).Id)
                reportDef.ParametrosReporte = New Object() {INDLIst(0).Id}
            ElseIf {1, 2, 3, 5}.Contains(INDLIst(0).DocumentType) Then

                If SettingBilling Is Nothing Then
                    Using model As New MBillingSetting(Me.Tag)
                        SettingBilling = Await model.GetSettingsBillingByIdUnitOperative(BarraBotones.OperatingUnit.Id.ToString(), False)
                    End Using
                End If

                If SettingBilling?.LiquidateMasterAccount Then
                    Dim reportSima As New Reporter.rptSaleInvoiceMotherAccount
                    ReportHelper.ExecuteReport(reportSima, Me, Me.BarraBotones.PermissionsForm, INDLIst(0).Id)
                    reportSima.ParametrosReporte = New Object() {INDLIst(0).Id}
                Else
                    Dim reportDef As New Reporter.rptSaleInvoice
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, INDLIst(0).Id)
                    reportDef.ParametrosReporte = New Object() {INDLIst(0).Id}
                End If

            End If
        End If
    End Sub

#End Region

End Class
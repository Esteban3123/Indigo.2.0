'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports DevExpress.Xpo

#End Region

Public Class PNotesDebitCreditPortfolio


#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim View As INotesDebitCreditPortfolio

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As INotesDebitCreditPortfolio)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.Indigo = SessionValues.Instance
            Me.View = view
        End If
    End Sub


#End Region

#Region "Methods"
    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenterXPO(Ids As String)
        Me.View.CostCenterConceptXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True, Ids)
    End Sub

    ''' <summary>
    ''' inicializa los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenterDistributionXPO()
        Me.View.CostCenterConceptDistributionXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

    ''' <summary>
    ''' inicializa los terceros
    ''' </summary>
    Public Sub InitializeThirdPartyXPO()
        Using model As New MBusqueda
            Me.View.ThirdPartyXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub


    ''' <summary>
    ''' inicializa la consulta de conceptos de retencion
    ''' </summary>
    Public Sub InitializeRetentionConceptXPO()
        Using model As New MBusqueda
            Me.View.RetentionConceptXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConceptByStatus, "True")
        End Using
    End Sub
    ''' <summary>
    ''' inicializa los conceptos de notas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeNoteConceptXPO(NoteType As Integer, Ids As String)
        Dim filter = $"Status = {True}"
        If NoteType = 6 Then
            If Not String.IsNullOrEmpty(Ids) Then
                filter &= $" AND (NoteType = 1 OR IdAccount.Id IN ({Ids}))"
            End If
        End If

        Me.View.NoteConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetAllPortfolioNoteConceptByFilter(filter)
    End Sub
    ''' <summary>
    ''' inicializa los clientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCustomerXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {True}
            Me.View.CustomerXPO = model.ConsultarEntidades(eDataSource.ListCustomerByStatus, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa los clientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCustomerDistributionXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {True}
            Me.View.CustomerDistributionXPO = model.ConsultarEntidades(eDataSource.ListCustomerByStatus, filter)
        End Using
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeAccountXPO(Ids As String)
        Me.View.AccountsXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True, 0, Ids)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeAccountDistributionXPO()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountsDistributionXPO = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
    ''' <summary>
    ''' obtiene la secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequense(CStr(Me.View.MyTag))
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    Public Sub InitializeBillsXPO(thirdPartyId As Integer, noteType As Integer, nature As Integer, SettingPortfolioNote As Boolean)
        Using model As New MBusqueda
            Dim filter() As Object = {thirdPartyId, noteType, nature, SettingPortfolioNote}
            Me.View.BillsXPO = model.ConsultarEntidades(eDataSource.ListBillsPortfolioNoteByThridParty, filter)
        End Using
    End Sub

    Public Sub InitializeAdvanceXPO(customerId As Integer, nature As Integer)
        Using model As New MBusqueda
            Dim filter() As Object = {customerId, nature}
            Me.View.AdvanceXPO = model.ConsultarEntidades(eDataSource.ListPortfolioAdvancePortfolioNote, filter)
        End Using
    End Sub

    Public Sub InitializeAdvanceDistributionXPO(ThirdPartyId As Integer)
        Using model As New MBusqueda
            View.AdvanceDistributionXPO = model.ConsultarEntidades(eDataSource.ListAdvanceTransfers, ThirdPartyId.ToString())
        End Using
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableAccountingById(Id As Integer) As List(Of PortfolioAccountReceivableAccountingXpo)
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAccountReceivableAccountingXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' metodo asincrono para listar las remisiones de entrada
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetAccountReceivableAccountingByIdAsync(Id As Integer) As Task(Of List(Of PortfolioAccountReceivableAccountingXpo))
        Return Await Task.Factory.StartNew(Function() As List(Of PortfolioAccountReceivableAccountingXpo)
                                               Dim filtroConsulta As String = "Id = " & Id
                                               Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAccountReceivableAccountingXpo)(Nothing, filtroConsulta)
                                           End Function)
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableShareById(Id As Integer) As List(Of PortfolioAccountReceivableShareXpo)
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAccountReceivableShareXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' metodo asincrono para obtener las cuotas 
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetAccountReceivableShareByIdAsync(Id As Integer) As Task(Of List(Of PortfolioAccountReceivableShareXpo))
        Return Await Task.Factory.StartNew(Function() As List(Of PortfolioAccountReceivableShareXpo)
                                               Dim filtroConsulta As String = "Id = " & Id
                                               Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAccountReceivableShareXpo)(Nothing, filtroConsulta)
                                           End Function)
    End Function

    Public Function GetClientById(Id As Integer) As CommonCustomerXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of CommonCustomerXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    Public Sub InitializePorfolioTransfer()
        Using model As New MBusqueda
            View.PorfolioTransferXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListPortfolioTransfersConfirmed
        End Using
    End Sub

    ''' <summary>
    ''' Consulta los detalles de una factura por el id de la cuenta de cobro
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetInvoiceDetailsByAccountReceivable(AccountReceivableId As Integer, Nature As Integer, Optional noteId As Integer? = Nothing) As XPCollection(Of ViewInvoiceDetailsXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListInvoiceDetailsByAccountReceivable(AccountReceivableId, Nature, noteId)
    End Function

    ''' <summary>
    ''' inicializa el datasource de la moneda
    ''' </summary>
    Public Sub InitializeCurrency()
        Using model As New MBusqueda
            Me.View.CurrencyDataSourceXpo = model.ConsultarEntidades(eDataSource.Currency)
        End Using
    End Sub

    ''' <summary>
    ''' Initializa tax rate datasource
    ''' </summary>
    Public Sub InitializeTaxRate()
        Using model As New MBusqueda
            View.TaxRateDataSourceXpo = model.ConsultarEntidades(eDataSource.ListGeneralLedgerIva)
        End Using
    End Sub
#End Region

End Class

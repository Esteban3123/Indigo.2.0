'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CostRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Maintenance.MVP

#End Region

Public Class PPopupBills

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPopupBills

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IPopupBills)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()

    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeCostCenter()
        ' Using model As New MBusqueda
        Me.View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        ' End Using
    End Sub

    ''' <summary>
    ''' Obtiene el proveedor
    ''' </summary>
    ''' <param name="IdSupplier"></param>
    ''' <remarks></remarks>
    Public Sub InitializeSupplier(ByVal IdSupplier As Integer)
        Using model As New MSupplier("")
            'Me.View.Supplier = model.GetSupplierById(IdSupplier)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la cuenta contable
    ''' </summary>
    ''' <param name="IdAccount"></param>
    ''' <remarks></remarks>
    Public Sub InitializeMainAccount(ByVal IdAccount As Integer)
        Using model As New MPUC("")
            'Me.View.MainAccount = model.GetAccountByIdSimple(IdAccount)
        End Using
    End Sub

    Public Function ListCostDistributionDirectCost(AccountPayableId As Integer) As List(Of CostDistributionDirectCostXpo)
        Dim filtroConsulta As String = "AccountPayableId = " & AccountPayableId & " And Status = 2"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.GetCollection(Of CostDistributionDirectCostXpo)(Nothing, filtroConsulta).ToList
    End Function

    Public Function GetCostSetting() As CostSettingXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.GetCollection(Of CostSettingXpo)(Nothing, Nothing).FirstOrDefault
    End Function

    Public Function GetSupplierById(Id As Integer) As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier)(Nothing, filtroConsulta).FirstOrDefault
    End Function

    Public Function GetMainAccountById(Id As Integer) As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo)(Nothing, filtroConsulta).FirstOrDefault
    End Function

    ''' <summary>
    ''' Metodo que consulta la cuenta por pagar
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAccountPayableByCompareBillNumber(BillNumber As String, SupplierId As Integer, Id As Integer) As Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableXpo
        Dim filtroConsulta As String = "BillNumber = '" & BillNumber & "' And IdSupplier.Id = " & SupplierId & " And Id <> " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableXpo)(Nothing, filtroConsulta).FirstOrDefault
    End Function

    ''' <summary>
    ''' Inicializa el datasource de las resoluciones de documento soporte
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeDocumentSupport()
        If View.DocumentSupportXpo Is Nothing Then
            View.DocumentSupportXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListDocumentSupportAuthorizationByUser(Indigo.UserIndigo)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las actividades económicas
    ''' </summary>
    Public Sub GetEconomicActivity()
        Dim filters As String = "IsIncomeGenerating = 1"
        Dim economicActivityList = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListEconomicActivity()
        economicActivityList.FixedFilterString = filters
        View.EconomicActivityXpo = economicActivityList
    End Sub

    ''' <summary>
    ''' Obtiene los Iva para cada detalle de la cuenta por pagar cuando proviene de la distribución del elemento del costo.
    ''' </summary>
    Public Function GetCostDistributionDirectCostDetailIva(AccountPayableId As Integer) As List(Of PaymentsRepository.ViewCostDistributionDirectCostDetailIvaXpo)
        Dim filters As String = String.Format("AccountPayableId = {0}", AccountPayableId)
        Dim data = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsRepository.ViewCostDistributionDirectCostDetailIvaXpo)(Nothing, filters)
        If data.Any() Then
            Return data.ToList()
        Else
            Return New List(Of PaymentsRepository.ViewCostDistributionDirectCostDetailIvaXpo)
        End If
    End Function

#Region "Budget Interface"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        If View.BudgetaryEntityXpo Is Nothing Then
            View.BudgetaryEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionBudgetEntityByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        If View.BudgetaryValidityXpo Is Nothing Then
            View.BudgetaryValidityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
        End If
    End Sub

    ''' <summary>
    ''' metodo para listar los detalles del compromiso
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCommitmentDetail(validityId As Integer, thirdPartyId As Integer, documentDate As DateTime)
        If View.ListAccountPayableCommitment Is Nothing Then
            Dim collectionAccountPayableCommitment = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCommitmentDetailObligation(validityId, thirdPartyId, documentDate)
            If collectionAccountPayableCommitment IsNot Nothing AndAlso collectionAccountPayableCommitment.Count > 0 Then
                Dim ListAccountPayableCommitment = New List(Of Domain.Entities.AccountPayableCommitments)

                For Each detail In collectionAccountPayableCommitment
                    ListAccountPayableCommitment.Add(New Domain.Entities.AccountPayableCommitments With {
                        .CommitmentDetailId = detail.Id,
                        .CommitmentCode = detail.CommitmentId.Code,
                        .CommitmentDocument = detail.CommitmentId.Document,
                        .CategoryCodeName = detail.CategoryId.NameCode,
                        .FinancialSourceCodeName = detail.CategoryId.FinancialSourceId.NameCode,
                        .RevenueTypeCodeName = detail.RevenueTypeId.NameCode,
                        .Balance = detail.Balance,
                        .Value = 0
                    })
                Next

                View.ListAccountPayableCommitment = ListAccountPayableCommitment
            End If
        End If
    End Sub

#End Region

End Class

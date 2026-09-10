'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Base

Public Class CostDistributionDirectCostRepository
    Inherits GenericRepository(Of CostDistributionDirectCost)
    Implements ICostDistributionDirectCostRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Public Function GetDistributionDirectCost(code As String) As CostDistributionDirectCost Implements ICostDistributionDirectCostRepository.GetDistributionDirectCost
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From d In _context.CostDistributionDirectCost.
                         Include("CostDistributionDirectCostDetail").
                         Include("CostDistributionDirectCostValues").
                         Include("CostDistributionDirectCostIva").
                         Include("CostDistributionDirectCostLegalizedDocuments").
                         Include("CostGeneralExpense").
                         Include("Currency").
                         Include("Supplier").
                         Include("ThirdParty") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.FullNameGeneralExpense = (From ge In _context.CostGeneralExpense.AsNoTracking() Where ge.Id = query.GeneralExpenseId Select String.Concat(ge.Code, " - ", ge.Name)).FirstOrDefault()
            query.ThirdPartyDescription = (From ge In _context.ThirdParty.AsNoTracking() Where ge.Id = query.ThirdPartyId Select String.Concat(ge.Nit, " - ", ge.Name)).FirstOrDefault()
            If query.SuppliersDistributionLinesId IsNot Nothing Then
                query.DistributionLineCodeName = (From sdl In _context.SuppliersDistributionLines.AsNoTracking()
                                                  Join dl In _context.DistributionLines.AsNoTracking() On sdl.IdDistributionLine Equals dl.Id
                                                  Where sdl.Id = query.SuppliersDistributionLinesId
                                                  Select String.Concat(dl.Code, " - ", dl.Name)).FirstOrDefault()

                query.PositionCodeName = String.Empty
                If query.PositionId IsNot Nothing Then
                    query.PositionCodeName = (From p In _context.Position.AsNoTracking()
                                              Where p.Id = query.PositionId
                                              Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                End If
            End If
            If query.MainAccountId IsNot Nothing Then
                query.MainAccountCodeName = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = query.MainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If
            If query.CostCenterId IsNot Nothing Then
                query.CostCenterCodeName = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = query.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
            End If
            If query.FilingUnitId IsNot Nothing Then
                query.FilingUnitCodeName = (From fu In _context.FilingUnit.AsNoTracking() Where fu.Id = query.FilingUnitId Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()
            End If
            If query.SupplierTypeId IsNot Nothing Then
                query.SupplierTypeCodeName = (From st In _context.SupplierType.AsNoTracking() Where st.Id = query.SupplierTypeId Select String.Concat(st.Code, " - ", st.Name)).FirstOrDefault()
            End If

            query.CurrencyAbbreviation = query?.Currency?.Abbreviation

            For Each item In query.CostDistributionDirectCostIva
                item.IvaName = (From i In _context.GeneralLedgerIVA.AsNoTracking() Where i.Id = item.GeneralLedgerIvaId Select i).FirstOrDefault.Name
                item.Percentage = (From i In _context.GeneralLedgerIVA.AsNoTracking() Where i.Id = item.GeneralLedgerIvaId Select i).FirstOrDefault.Percentage
            Next

            If query.CostDistributionDirectCostDetail IsNot Nothing AndAlso query.CostDistributionDirectCostDetail.Count > 0 Then
                For Each item In query.CostDistributionDirectCostDetail
                    item.ProductionCenterCodeName = (From ge In _context.CostProductionCenter.AsNoTracking() Where ge.Id = item.ProductionCenterId Select String.Concat(ge.Code, " - ", ge.Name)).FirstOrDefault()
                    item.MainAccountCodeName = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.MainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    If item.CostCenterId IsNot Nothing Then
                        item.CostCenterCodeName = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = item.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
                    End If
                    If item.MeasurementUnitId IsNot Nothing Then
                        item.MeasurementUnitCodeName = (From ge In _context.InventoryMeasurementUnit.AsNoTracking() Where ge.Id = item.MeasurementUnitId Select String.Concat(ge.Code, " - ", ge.Name)).FirstOrDefault()
                    End If
                    If item.Nature IsNot Nothing Then
                        If item.Nature = 1 Then
                            item.NatureText = "DEBITO"
                        Else
                            item.NatureText = "CREDITO"
                        End If
                    End If
                Next
            End If

            If query.CostDistributionDirectCostLegalizedDocuments.Any() Then
                For Each item In query.CostDistributionDirectCostLegalizedDocuments
                    Dim provisionDocument = (From cd In _context.CostDistributionDirectCost.AsNoTracking() Where cd.Id = item.ProvisionDocumentId Select cd).FirstOrDefault()
                    item.Code = provisionDocument.Code
                    item.Value = provisionDocument.Value
                    item.ConfirmDate = If(IsDBNull(provisionDocument.ServicePeriodDate), provisionDocument.ConfirmDate, provisionDocument.ServicePeriodDate)

                    Select Case provisionDocument.Status
                        Case 5
                            item.StatusName = "Confirmado Pendiente por legalizar"
                        Case 6
                            item.StatusName = "Confirmado Legalizado"
                    End Select
                Next
            End If

            query.OriginalValue = (From d In _context.CostDistributionDirectCost.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionDirectCost()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Public Function GetDistributionDirectCostById(id As Integer) As CostDistributionDirectCost Implements ICostDistributionDirectCostRepository.GetDistributionDirectCostById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From d In _context.CostDistributionDirectCost.
                         Include("CostDistributionDirectCostDetail").
                         Include("CostDistributionDirectCostDetail.CostDistributionDirectCostDetailIva")
                     Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionDirectCost.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionDirectCost()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de elementos del costo por medio de la cuenta por pagar
    ''' </summary>
    Public Function GetDistributionDirectCostByAccountPayableId(AccountPayableId As Integer) As CostDistributionDirectCost Implements ICostDistributionDirectCostRepository.GetDistributionDirectCostByAccountPayableId
        If AccountPayableId = 0 Then
            Throw New ArgumentNullException("AccountPayableId")
        End If

        Dim query = (From d In _context.CostDistributionDirectCost.Include("CostDistributionDirectCostLegalizedDocuments")
                     Where d.AccountPayableId = AccountPayableId Select d).FirstOrDefault()

        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New CostDistributionDirectCost()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el valor contable por contenedor, número de cuenta contable, año y mes
    ''' </summary>
    Public Function GetMainAccountValueByContainerNumberAccountYearAndMotn(mainAccountId As Integer, year As String, month As Integer) As Decimal Implements ICostDistributionDirectCostRepository.GetMainAccountValueByNumberAccountYearAndMotn
        'Dim query = (From m In _context.GeneralLedgerBalance.AsNoTracking() Where m.IdMainAccount = mainAccountId And m.Year = year And m.Month = month Group By m.IdMainAccount Into Sum(m.DebitValue - m.CreditValue)).FirstOrDefault()
        Dim query = (From m In _context.GeneralLedgerBalance.AsNoTracking() Where m.IdMainAccount = mainAccountId And m.Year = year And m.Month = month Select m).ToList()
        If query IsNot Nothing AndAlso query.Any() Then
            Dim mainAccount As MainAccounts = (From m In _context.MainAccounts.AsNoTracking() Where m.Id = mainAccountId Select m).FirstOrDefault()
            If mainAccount IsNot Nothing AndAlso mainAccount.Id > 0 Then
                If mainAccount.Nature = 1 Then
                    Return query.Sum(Function(x) x.DebitValue - x.CreditValue)
                Else
                    Return query.Sum(Function(x) x.CreditValue - x.DebitValue)
                End If
            End If
            Return 0D
        Else
            Return 0D
        End If
    End Function

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    Public Function ListDistributionDirectCostByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionDirectCost) Implements ICostDistributionDirectCostRepository.ListDistributionDirectCostByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Return (From d In _context.CostDistributionDirectCost.Include("CostDistributionDirectCostDetail") Where d.Year = year AndAlso d.Month = month Select d).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el elemento del costo por id
    ''' </summary>
    ''' <param name="CostGeneralExpenseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostGeneralExpenseById(CostGeneralExpenseId As Integer) As CostGeneralExpense Implements ICostDistributionDirectCostRepository.GetCostGeneralExpenseById
        Return (From e In _context.CostGeneralExpense.AsNoTracking.Include("CostDistributionBase").AsNoTracking.Include("CostDistributionBase.CostDistributionBaseDetail").AsNoTracking.Include("CostDistributionBase.CostDistributionBaseDetail.CostProductionCenter").AsNoTracking.Include("CostDistributionBase.CostDistributionBaseDetail.MainAccounts").AsNoTracking Where e.Id = CostGeneralExpenseId Select e).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Calcular distribución
    ''' </summary>
    ''' <param name="CostGeneralExpenseId"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Function CalculateCostDistribution(CostGeneralExpenseId As Integer, Year As Integer, Month As Integer, Value As Decimal) As List(Of SP_CalculateCostDistribution_Result) Implements ICostDistributionDirectCostRepository.CalculateCostDistribution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CalculateCostDistribution(CostGeneralExpenseId, Year, Month, Value).ToList()
    End Function

    ''' <summary>
    ''' Calcular distribución
    ''' </summary>
    ''' <param name="IdProvisionDocument"></param>
    ''' <param name="User"></param>
    ''' <returns></returns>
    Public Function SP_ConfirmProvisionDocumentDistributionDirectCost(IdProvisionDocument As Integer, User As String) As List(Of SP_ConfirmProvisionDocumentDistributionDirectCost_Result) Implements ICostDistributionDirectCostRepository.SP_ConfirmProvisionDocumentDistributionDirectCost
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmProvisionDocumentDistributionDirectCost(IdProvisionDocument, User).ToList()
    End Function

    ''' <summary>
    ''' Calcular distribución
    ''' </summary>
    ''' <param name="IdProvisionDocument"></param>
    ''' <param name="User"></param>
    ''' <returns></returns>
    Public Function SP_ReverseProvisionDocumentDistributionDirectCost(IdProvisionDocument As Integer, User As String, Type As Integer) As List(Of SP_ReverseProvisionDocumentDistributionDirectCost_Result) Implements ICostDistributionDirectCostRepository.SP_ReverseProvisionDocumentDistributionDirectCost
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReverseProvisionDocumentDistributionDirectCost(IdProvisionDocument, User, Type).ToList()
    End Function

    ''' <summary>
    ''' Copy And Paste Detalles de la distribucion de elementos del costo
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Public Function SP_CopyAndPasteCostDistributionDirectCostDetail(XmlObject As String) As List(Of SP_CopyAndPasteCostDistributionDirectCostDetail_Result) Implements ICostDistributionDirectCostRepository.SP_CopyAndPasteCostDistributionDirectCostDetail
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteCostDistributionDirectCostDetail(XmlObject).ToList()
    End Function

End Class

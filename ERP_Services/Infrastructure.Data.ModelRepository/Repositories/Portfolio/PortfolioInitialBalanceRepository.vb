'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data
Imports System.Data.Entity
Imports System.Data.SqlClient

Public Class PortfolioInitialBalanceRepository
    Inherits GenericRepository(Of PortfolioInitialBalance)
    Implements IPortfolioInitialBalanceRepository

#Region "Context"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' metodo para obtener las facturas del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAccountReceivable) Implements IPortfolioInitialBalanceRepository.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance
        ' Esta operación es consumida por la presentación para alimentar la grilla.
        Dim res = (From ibar In _context.PortfolioInitialBalanceAccountReceivable.AsNoTracking()
                   Where ibar.PortfolioInitialBalanceId = idPortfolioInitialBalance
                   Select ibar).ToList()

        If res.Count = 0 Then Return res

        ' La grilla solo necesita CodeNameCustomer. Resolver los clientes en una
        ' sola consulta evita una consulta por cada factura.
        Dim customerIds = res.Where(Function(item) item.CustomerId.HasValue).
            Select(Function(item) item.CustomerId.Value).
            Distinct().
            ToList()

        Dim customerNames As New Dictionary(Of Integer, String)
        For offset As Integer = 0 To customerIds.Count - 1 Step 1000
            Dim customerIdsBatch = customerIds.Skip(offset).Take(1000).ToList()
            Dim customers = (From customer In _context.Customer.AsNoTracking()
                             Where customerIdsBatch.Contains(customer.Id)
                             Select New With {
                                 .Id = customer.Id,
                                 .CodeName = String.Concat(customer.Nit, " - ", customer.Name)
                             }).ToList()
            For Each customer In customers
                customerNames(customer.Id) = customer.CodeName
            Next
        Next

        For Each item In res
            Dim codeName As String = Nothing
            If item.CustomerId.HasValue AndAlso customerNames.TryGetValue(item.CustomerId.Value, codeName) Then
                item.CodeNameCustomer = codeName
            End If
        Next

        Return res
    End Function

    Public Function GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAdvance) Implements IPortfolioInitialBalanceRepository.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance
        Dim res = (From iba In _context.PortfolioInitialBalanceAdvance.AsNoTracking() Where iba.PortfolioInitialBalanceId = idPortfolioInitialBalance Select iba).ToList()
        If res.Count = 0 Then Return res

        Dim customerIds = res.Where(Function(item) item.CustomerId.HasValue).
            Select(Function(item) item.CustomerId.Value).Distinct().ToList()
        Dim thirdPartyIds = res.Select(Function(item) item.ThirdPartyId).Distinct().ToList()
        Dim mainAccountIds = res.Select(Function(item) item.MainAccountId).Distinct().ToList()
        Dim costCenterIds = res.Where(Function(item) item.CostCenterId.HasValue).
            Select(Function(item) item.CostCenterId.Value).Distinct().ToList()

        Dim customerNames As New Dictionary(Of Integer, String)
        For offset As Integer = 0 To customerIds.Count - 1 Step 1000
            Dim idsBatch = customerIds.Skip(offset).Take(1000).ToList()
            Dim names = (From customer In _context.Customer.AsNoTracking()
                         Where idsBatch.Contains(customer.Id)
                         Select New With {
                             .Id = customer.Id,
                             .CodeName = String.Concat(customer.Nit, " - ", customer.Name)
                         }).ToList()
            For Each name In names
                customerNames(name.Id) = name.CodeName
            Next
        Next

        Dim thirdPartyNames As New Dictionary(Of Integer, String)
        For offset As Integer = 0 To thirdPartyIds.Count - 1 Step 1000
            Dim idsBatch = thirdPartyIds.Skip(offset).Take(1000).ToList()
            Dim names = (From thirdParty In _context.ThirdParty.AsNoTracking()
                         Where idsBatch.Contains(thirdParty.Id)
                         Select New With {
                             .Id = thirdParty.Id,
                             .CodeName = String.Concat(thirdParty.Nit, " - ", thirdParty.Name)
                         }).ToList()
            For Each name In names
                thirdPartyNames(name.Id) = name.CodeName
            Next
        Next

        Dim mainAccountNames As New Dictionary(Of Integer, String)
        For offset As Integer = 0 To mainAccountIds.Count - 1 Step 1000
            Dim idsBatch = mainAccountIds.Skip(offset).Take(1000).ToList()
            Dim names = (From mainAccount In _context.MainAccounts.AsNoTracking()
                         Where idsBatch.Contains(mainAccount.Id)
                         Select New With {
                             .Id = mainAccount.Id,
                             .CodeName = String.Concat(mainAccount.Number, " - ", mainAccount.Name)
                         }).ToList()
            For Each name In names
                mainAccountNames(name.Id) = name.CodeName
            Next
        Next

        Dim costCenterNames As New Dictionary(Of Integer, String)
        For offset As Integer = 0 To costCenterIds.Count - 1 Step 1000
            Dim idsBatch = costCenterIds.Skip(offset).Take(1000).ToList()
            Dim names = (From costCenter In _context.CostCenter.AsNoTracking()
                         Where idsBatch.Contains(costCenter.Id)
                         Select New With {
                             .Id = costCenter.Id,
                             .CodeName = String.Concat(costCenter.Code, " - ", costCenter.Name)
                         }).ToList()
            For Each name In names
                costCenterNames(name.Id) = name.CodeName
            Next
        Next

        For Each item In res
            Dim codeName As String = Nothing
            If item.CustomerId.HasValue AndAlso customerNames.TryGetValue(item.CustomerId.Value, codeName) Then
                item.CodeNameCustomer = codeName
            End If
            If thirdPartyNames.TryGetValue(item.ThirdPartyId, codeName) Then
                item.NitNameThirdParty = codeName
            End If
            If mainAccountNames.TryGetValue(item.MainAccountId, codeName) Then
                item.CodeNameMainAccount = codeName
            End If
            If item.CostCenterId.HasValue AndAlso costCenterNames.TryGetValue(item.CostCenterId.Value, codeName) Then
                item.CodeNameCostCenter = codeName
            End If
        Next

        Return res
    End Function

    ''' <summary>
    ''' Ejecuta directamente la confirmación set-based y devuelve su resumen.
    ''' </summary>
    Public Function ConfirmPortfolioInitialBalanceSetBased(idPortfolioInitialBalance As Integer, auditUser As String, electronicDocumentContainer As String) As PortfolioInitialBalanceConfirmationSummary Implements IPortfolioInitialBalanceRepository.ConfirmPortfolioInitialBalanceSetBased
        Const confirmationSql As String =
            "EXEC Portfolio.SP_ConfirmPortfolioInitialBalanceSetBased " &
            "@PortfolioInitialBalanceId = @PortfolioInitialBalanceId, " &
            "@AuditUser = @AuditUser, " &
            "@ElectronicDocumentContainer = @ElectronicDocumentContainer"

        Dim idParameter As New SqlParameter("@PortfolioInitialBalanceId", SqlDbType.Int) With {
            .Value = idPortfolioInitialBalance
        }
        Dim auditUserParameter As New SqlParameter("@AuditUser", SqlDbType.VarChar, 20) With {
            .Value = auditUser
        }
        Dim electronicDocumentContainerParameter As New SqlParameter("@ElectronicDocumentContainer", SqlDbType.VarChar, 50) With {
            .Value = If(electronicDocumentContainer, String.Empty)
        }
        ' El valor predeterminado de EF (~30 s) no es suficiente para una
        ' confirmación set-based de cientos de miles de facturas. El alcance es
        ' local a esta llamada y se restaura incluso si SQL cancela la ejecución.
        Dim dbContext = CType(_context, DbContext)
        Dim previousCommandTimeout = dbContext.Database.CommandTimeout
        dbContext.Database.CommandTimeout = 3600
        Try
            Dim summaries = _context.ExecuteQuery(Of PortfolioInitialBalanceConfirmationSummary)(confirmationSql, idParameter, auditUserParameter, electronicDocumentContainerParameter).ToList()
            If summaries.Count <> 1 Then
                Throw New InvalidOperationException("El procedimiento de confirmación no devolvió un único resumen.")
            End If
            Return summaries(0)
        Finally
            dbContext.Database.CommandTimeout = previousCommandTimeout
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceByCode(code As String) As PortfolioInitialBalance Implements IPortfolioInitialBalanceRepository.GetPortfolioInitialBalanceByCode
        Dim res = (From ib In _context.PortfolioInitialBalance Where ib.Code = code Select ib).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From ib In _context.PortfolioInitialBalance.AsNoTracking() Where ib.Code = code Select ib).FirstOrDefault()
            Return res
        Else
            Return New PortfolioInitialBalance
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceById(id As Integer) As PortfolioInitialBalance Implements IPortfolioInitialBalanceRepository.GetPortfolioInitialBalanceById
        Dim res = (From ib In _context.PortfolioInitialBalance Where ib.Id = id Select ib).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New PortfolioInitialBalance
        End If
    End Function

    ''' <summary>
    ''' Registra exclusivamente el grafo recibido por el import de facturas.
    ''' Evita ApplyChanges, cuyo índice genérico vuelve a recorrer todas las
    ''' entidades por cada elemento del lote.
    ''' </summary>
    Public Sub RegisterAccountReceivableImportBatch(portfolioInitialBalance As PortfolioInitialBalance) Implements IPortfolioInitialBalanceRepository.RegisterAccountReceivableImportBatch
        If portfolioInitialBalance Is Nothing Then
            Throw New ArgumentNullException("portfolioInitialBalance")
        End If

        If portfolioInitialBalance.Id = 0 Then
            ' EF registra el encabezado nuevo y todo su grafo relacionado.
            _context.PortfolioInitialBalance.Add(portfolioInitialBalance)
            Return
        End If

        Dim importedRows = portfolioInitialBalance.PortfolioInitialBalanceAccountReceivable.ToList()

        ' Separar las filas del encabezado deserializado evita que AddRange
        ' intente insertar nuevamente un saldo que ya existe.
        For Each importedRow In importedRows
            importedRow.PortfolioInitialBalance = Nothing
            importedRow.PortfolioInitialBalanceId = portfolioInitialBalance.Id
        Next

        Dim persistedHeader = _context.PortfolioInitialBalance.SingleOrDefault(
            Function(item) item.Id = portfolioInitialBalance.Id)
        If persistedHeader Is Nothing Then
            Throw New InvalidOperationException("No existe el saldo inicial asociado al lote importado.")
        End If

        Dim dbContext = CType(_context, DbContext)
        dbContext.Entry(persistedHeader).CurrentValues.SetValues(portfolioInitialBalance)

        If importedRows.Count > 0 Then
            _context.PortfolioInitialBalanceAccountReceivable.AddRange(importedRows)
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta persistencia masiva únicamente sobre el contexto cuyo grafo de
    ''' staging fue registrado por RegisterAccountReceivableImportBatch.
    ''' </summary>
    Public Sub CommitAccountReceivableImportBatch() Implements IPortfolioInitialBalanceRepository.CommitAccountReceivableImportBatch
        BulkUnitWorkPersistence.Commit(CType(_context, DbContext))
    End Sub
End Class

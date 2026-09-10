'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions

#End Region

Public Class PortfolioAdvanceAdminService
    Implements IPortfolioAdvanceAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de anticipos
    ''' </summary>
    Private _portfolioAdvanceRepository As IPortfolioAdvanceRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository

    ''' <summary>
    ''' Company Setting Repository
    ''' </summary>
    Private _companySettingsRepository As ICompanySettingsRepository

    ''' <summary>
    ''' Currency repository
    ''' </summary>
    Private _currencyRepository As ICurrencyRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal portfolioAdvanceRepository As IPortfolioAdvanceRepository,
                   ByVal sequensePortfolioDRepository As ISequensePortfolioDRepository,
                   ByVal companySettingsRepository As ICompanySettingsRepository,
                   ByVal currencyRepository As ICurrencyRepository)
        If portfolioAdvanceRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioAdvanceRepository")
        End If
        If sequensePortfolioDRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePortfolioDRepository")
        End If
        _portfolioAdvanceRepository = portfolioAdvanceRepository
        _sequensePortfolioDRepository = sequensePortfolioDRepository
        _companySettingsRepository = companySettingsRepository
        _currencyRepository = currencyRepository
    End Sub

    ''' <summary>
    ''' Elimina un anticipo
    ''' </summary>
    Public Function DeletePortfolioAdvance(portfolioAdvance As PortfolioAdvance, audit As AuditMessage) As ActionResult Implements IPortfolioAdvanceAdminService.DeletePortfolioAdvance
        If portfolioAdvance Is Nothing Then
            Throw New ArgumentNullException("portfolioAdvance")
        End If
        Dim unitOfWork As IUnitWork = Me._portfolioAdvanceRepository.UnitWork
        Try
            portfolioAdvance.ModificationDate = DateTime.Now
            portfolioAdvance.ModificationUser = audit.CodeUser
            Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioAdvance)(portfolioAdvance, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._portfolioAdvanceRepository.DeleteEntity(portfolioAdvance)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por codigo
    ''' </summary>
    Public Function GetPortfolioAdvance(code As String, audit As AuditMessage) As ActionResult(Of PortfolioAdvance) Implements IPortfolioAdvanceAdminService.GetPortfolioAdvance
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim portfolioAdvance As PortfolioAdvance = Me._portfolioAdvanceRepository.GetPortfolioAdvance(code.Trim())
            If portfolioAdvance IsNot Nothing AndAlso portfolioAdvance.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioAdvance)(portfolioAdvance, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of PortfolioAdvance) With {.StateResult = True, .ObjectEmbbeded = portfolioAdvance}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceById(Id As Integer) As ActionResult(Of PortfolioAdvance) Implements IPortfolioAdvanceAdminService.GetPortfolioAdvanceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim portfolio As PortfolioAdvance = _portfolioAdvanceRepository.GetPortfolioAdvanceById(Id)
            Return New ActionResult(Of PortfolioAdvance) With {.StateResult = True, .ObjectEmbbeded = portfolio}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Public Function ListPorfolioAdvanceByThirdId(ThirdId As Integer) As ActionResult(Of List(Of PortfolioAdvance)) Implements IPortfolioAdvanceAdminService.ListPorfolioAdvanceByThirdId
        If ThirdId = 0 Then
            Throw New ArgumentNullException("ThirdId")
        End If
        Try
            Dim portfolio As List(Of PortfolioAdvance) = _portfolioAdvanceRepository.ListPorfolioAdvanceByThirdId(ThirdId)
            Return New ActionResult(Of List(Of PortfolioAdvance)) With {.StateResult = True, .ObjectEmbbeded = portfolio}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of PortfolioAdvance)) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Lists the porfolio advance by third identifier with balance.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ThirdId</exception>
    Public Function ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ThirdId As Integer, admission As String) As ActionResult(Of List(Of PortfolioAdvance)) Implements IPortfolioAdvanceAdminService.ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance
        If ThirdId = 0 Then
            Throw New ArgumentNullException("ThirdId")
        End If
        Try
            Dim portfolio As List(Of PortfolioAdvance) = _portfolioAdvanceRepository.ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ThirdId, admission)
            Return New ActionResult(Of List(Of PortfolioAdvance)) With {.StateResult = True, .ObjectEmbbeded = portfolio}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of PortfolioAdvance)) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un anticipo
    ''' </summary>
    ''' <param name="portfolioAdvance"></param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">portfolioAdvance</exception>
    Public Function SavePortfolioAdvance(portfolioAdvance As PortfolioAdvance, audit As AuditMessage, Optional idSequence As Long = 0, Optional ByVal withCommit As Boolean = True) As ActionResult(Of PortfolioAdvance) Implements IPortfolioAdvanceAdminService.SavePortfolioAdvance
        If portfolioAdvance Is Nothing Then
            Throw New ArgumentNullException("portfolioAdvance")
        End If

        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Dim unitOfWork As IUnitWork = Me._portfolioAdvanceRepository.UnitWork
            Dim unitWorkSequence As IUnitWork = Me._sequensePortfolioDRepository.UnitWork

            Try
                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioAdvance)
                Dim status As Integer
                Dim auxPortfolioAdvance As PortfolioAdvance = Nothing

                If String.IsNullOrEmpty(portfolioAdvance.Code) Then
                    Dim seq As PortfolioSequenceDetail = _sequensePortfolioDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            portfolioAdvance.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList(), .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If
                If portfolioAdvance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    portfolioAdvance.CreationUser = audit.CodeUser
                    portfolioAdvance.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    portfolioAdvance.ModificationDate = DateTime.Now
                    portfolioAdvance.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxPortfolioAdvance = portfolioAdvance.OriginalValue
                End If

                Me._portfolioAdvanceRepository.SaveEntity(portfolioAdvance)
                If withCommit Then
                    unitOfWork.Commit()
                    unitWorkSequence.Commit()
                End If
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioAdvance)(portfolioAdvance, audit, status, auxPortfolioAdvance)
                auditProcess.Execute()

                scope.Complete()
                Return New ActionResult(Of PortfolioAdvance) With {.StateResult = True, .ObjectEmbbeded = portfolioAdvance}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Get The advance balance in diferente currency
    ''' </summary>
    ''' <param name="portfolioAdvanceId"></param>
    ''' <param name="toCurrencyId"></param>
    ''' <param name="dateTRM"></param>
    ''' <param name="moduleTRM"></param>
    ''' <returns></returns>
    Public Function GetBalanceAdvanceByCurrency(portfolioAdvanceId As Integer,
                                                Optional toCurrencyId As Integer? = Nothing,
                                                Optional dateTRM As Date? = Nothing,
                                                Optional moduleTRM As EModuleTRM = EModuleTRM.CommonTRM) As ActionResult(Of PortfolioAdvance) Implements IPortfolioAdvanceAdminService.GetBalanceAdvanceByCurrency
        Try

            If portfolioAdvanceId = 0 Then
                Throw New ArgumentNullException(NameOf(portfolioAdvanceId), "parámetro Id del anticipo obligatorio")
            End If

            Dim portfolioAdvance = _portfolioAdvanceRepository.FirstOrDefault(Function(x) x.Id = portfolioAdvanceId, False, {"Currency"})

            If portfolioAdvance Is Nothing Then
                Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .Message = "No se encontró el anticipo"}
            End If

            Dim advanceCurrencyId = portfolioAdvance.CurrencyId

            'If the currency advance doesnt have Currency, put the Official Currency
            If advanceCurrencyId Is Nothing Then
                advanceCurrencyId = _companySettingsRepository.GetCompanySettings().OfficialCurrencyId
            End If

            'if To currency is nothing or is equals to 0, return the portfolio advance entity
            If toCurrencyId Is Nothing OrElse toCurrencyId = 0 Then
                Return New ActionResult(Of PortfolioAdvance) With {.StateResult = True, .ObjectEmbbeded = portfolioAdvance}
            End If

            'get the TRM to make the division with portfolio advance balance
            Dim queryTRM = _currencyRepository.GetJustTRMToMakeDivision(advanceCurrencyId.Value,
                                                                        toCurrencyId,
                                                                        If(EModuleTRM.InvoiceTRM = moduleTRM,
                                                                        NameOf(Invoice), String.Empty),
                                                                        dateTRM)
            ' if TRM is 0 return a fail validation
            If queryTRM.TRMValue = 0 Then
                Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .Message = $"No existe un TRM para la fecha {dateTRM}"}
            End If

            queryTRM.Value = Math.Round(portfolioAdvance.Balance / queryTRM.TRMValue, 2, MidpointRounding.AwayFromZero)
            portfolioAdvance.BalanceInCurrencyConverted = queryTRM
            portfolioAdvance.CurrencyAbbreviation = portfolioAdvance.Currency?.Abbreviation

            Return New ActionResult(Of PortfolioAdvance) With {.StateResult = True, .ObjectEmbbeded = portfolioAdvance}
        Catch ex As Exception
            Return New ActionResult(Of PortfolioAdvance) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _portfolioAdvanceRepository = Nothing
            _sequensePortfolioDRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
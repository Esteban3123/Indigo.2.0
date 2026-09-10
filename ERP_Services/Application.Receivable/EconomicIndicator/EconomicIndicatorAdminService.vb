'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
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

Public Class EconomicIndicatorAdminService
    Implements IEconomicIndicatorAdminService
    Private Const FORM_NAME As String = "FrmEconomicIndicators"


#Region "Fields"
    Private _economicIndicatorRepository As IEconomicIndicatorRepository

    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository
#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="economicIndicatorRepository">Repositorio de la entidad indicador economico</param>
    Public Sub New(ByVal economicIndicatorRepository As IEconomicIndicatorRepository, ByVal sequenseRepository As ISequensePortfolioDRepository)
        If (economicIndicatorRepository Is Nothing) Then
            Throw New ArgumentNullException("economicIndicatorRepository Vacio")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _economicIndicatorRepository = economicIndicatorRepository
        _sequensePortfolioDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para eliminar un indicador economico
    ''' </summary>
    ''' <param name="economicIndicator">The economic indicator.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">EconomicIndicator Vacio</exception>
    Public Function DeleteEconomicIndicator(economicIndicator As Domain.Entities.EconomicIndicator, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult Implements IEconomicIndicatorAdminService.DeleteEconomicIndicator
        'If economicIndicator Is Nothing Then
        '    Throw New ArgumentNullException("EconomicIndicator Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _economicIndicatorRepository.UnitWork

        'Try
        '    economicIndicator.ModificationDate = DateTime.Now
        '    economicIndicator.ModificationUser = audit.CodeUser
        '    Dim auditProcess = New IndigoAuditSimpleEntity(Of EconomicIndicator)(economicIndicator, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    _economicIndicatorRepository.DeleteEntity(economicIndicator)
        '    UnitOfWork.Commit()
        '    auditProcess.Execute()
        '    Return New ActionResult With {.StateResult = True}

        'Catch ex As OptimisticConcurrencyException
        '    UnitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As DbUpdateException
        '    UnitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False}
        'End Try


        If economicIndicator Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._economicIndicatorRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                economicIndicator.ModificationUser = audit.CodeUser
                economicIndicator.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of EconomicIndicator)(economicIndicator, audit, status)

                'While economicIndicator.InvoiceCategoriesUser.Count > 0
                '    economicIndicator.InvoiceCategoriesUser(economicIndicator.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                economicIndicator.MarkAsDeleted()
                Me._economicIndicatorRepository.SaveEntity(economicIndicator)
                UnitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Metodo para obtener un indicador economico por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetEconomicIndicatorByCode(code As String, audit As AuditMessage) As EconomicIndicator Implements IEconomicIndicatorAdminService.GetEconomicIndicatorByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code Vacio")
        End If
        Try
            Dim EconomicIndicator As EconomicIndicator = _economicIndicatorRepository.GetEconomicIndicatorByCode(code)
            If EconomicIndicator IsNot Nothing AndAlso EconomicIndicator.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of EconomicIndicator)(EconomicIndicator, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return EconomicIndicator
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Metodo para listar todos los indicadores economicos
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetAllEconomicIndicator(audit As AuditMessage) As List(Of EconomicIndicator) Implements IEconomicIndicatorAdminService.GetAllEconomicIndicator
        Try
            Dim EconomicIndicators = _economicIndicatorRepository.GetAllEconomicIndicator()
            For Each item As EconomicIndicator In EconomicIndicators
                Dim auditProcess As New IndigoAuditSimpleEntity(Of EconomicIndicator)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            Next
            Return EconomicIndicators
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Metodo para obtener un indicador economico por añoy mes
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">year o month Vacio</exception>
    Public Function GetEconomicIndicator(year As String, month As String, audit As AuditMessage) As EconomicIndicator Implements IEconomicIndicatorAdminService.GetEconomicIndicator
        If String.IsNullOrEmpty(year) Or String.IsNullOrEmpty(month) Then
            Throw New ArgumentNullException("year o month Vacio")
        End If
        Try
            Return _economicIndicatorRepository.GetEconomicIndicator(year, month)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' metodo para guardar un indicador economico
    ''' </summary>
    ''' <param name="economicIndicator">The economic indicator.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">economicIndicator Vacio</exception>
    Public Function SaveEconomicIndicator(economicIndicator As EconomicIndicator, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of EconomicIndicator) Implements IEconomicIndicatorAdminService.SaveEconomicIndicator
        'If economicIndicator Is Nothing Then
        '    Throw New ArgumentNullException("economicIndicator Vacio")
        'End If
        'Dim economicIndicatorUnitOfWork As IUnitWork = _economicIndicatorRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        'Try
        '    Dim seq As PortfolioSequenceDetail = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of EconomicIndicator)
        '    Dim status As Integer
        '    Dim auxEconomicIndicator As EconomicIndicator = Nothing

        '    If economicIndicator.Code Is Nothing OrElse economicIndicator.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequensePortfolioDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                economicIndicator.Code = res
        '                seq.Next += 1
        '                Me._sequensePortfolioDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of EconomicIndicator) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of EconomicIndicator) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    If economicIndicator.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        economicIndicator.CreationUser = audit.CodeUser
        '        economicIndicator.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        economicIndicator.ModificationUser = audit.CodeUser
        '        economicIndicator.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxEconomicIndicator = _economicIndicatorRepository.GetEconomicIndicatorByCode(economicIndicator.Code, False)
        '    End If

        '    Me._economicIndicatorRepository.SaveEntity(economicIndicator)
        '    economicIndicatorUnitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of EconomicIndicator)(economicIndicator, audit, status, auxEconomicIndicator)
        '    auditProcess.Execute()

        '    Return New ActionResult(Of EconomicIndicator) With {.StateResult = True, .ObjectEmbbeded = economicIndicator}
        'Catch ex As OptimisticConcurrencyException
        '    economicIndicatorUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    Return New ActionResult(Of EconomicIndicator) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    economicIndicatorUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of EconomicIndicator) With {.StateResult = False}
        'End Try




        If economicIndicator Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._economicIndicatorRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(economicIndicator.Code) Then
                    Dim seq As PortfolioSequenceDetail = Me._sequensePortfolioDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            economicIndicator.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of EconomicIndicator) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), economicIndicator.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of EconomicIndicator) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As EconomicIndicator = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of EconomicIndicator)
                Dim status As Integer

                If economicIndicator.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    economicIndicator.CreationUser = audit.CodeUser
                    economicIndicator.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = economicIndicator.OriginalValue
                    economicIndicator.ModificationUser = audit.CodeUser
                    economicIndicator.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._economicIndicatorRepository.SaveEntity(economicIndicator)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of EconomicIndicator)(economicIndicator, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                economicIndicator.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of EconomicIndicator) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = economicIndicator, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of EconomicIndicator) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EconomicIndicator) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of EconomicIndicator) Implements IEconomicIndicatorAdminService.ChangeState
        'Dim economicIndicator As EconomicIndicator = GetEconomicIndicatorByCode(code, audit)
        'economicIndicator.Status = state
        'economicIndicator.MarkAsModified()
        'Return SaveEconomicIndicator(economicIndicator, audit)




        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim economicIndicator As EconomicIndicator = Me._economicIndicatorRepository.GetEconomicIndicatorByCode(code.Trim())
            If economicIndicator IsNot Nothing AndAlso economicIndicator.Id > 0 Then
                economicIndicator.Status = state
            End If
            Dim result = Me.SaveEconomicIndicator(economicIndicator, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EconomicIndicator) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _economicIndicatorRepository = Nothing
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

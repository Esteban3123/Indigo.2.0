'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
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

Public Class PortfolioNoteConceptAdminService
    Implements IPortfolioNoteConceptAdminService



#Region "Fields"
    Private Const FORM_NAME As String = "FrmPortfolioNoteConcepts"
    Private _portfolioNoteConceptRepository As IPortfolioNoteConceptRepository
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository
#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="portfolioNoteConceptRepository">Repositorio de la entidad concepto de nota</param>
    Public Sub New(ByVal portfolioNoteConceptRepository As IPortfolioNoteConceptRepository, ByVal sequenseRepository As ISequensePortfolioDRepository)
        If (portfolioNoteConceptRepository Is Nothing) Then
            Throw New ArgumentNullException("portfolioNoteConceptRepository Vacio")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _portfolioNoteConceptRepository = portfolioNoteConceptRepository
        _sequensePortfolioDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' eliminar un conepto de nota.
    ''' </summary>
    ''' <param name="portfolioNoteConcept">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">EconomicIndicator Vacio</exception>
    Public Function DeletePortfolioNoteConcept(portfolioNoteConcept As PortfolioNoteConcept, audit As AuditMessage) As ActionResult Implements IPortfolioNoteConceptAdminService.DeletePortfolioNoteConcept
        'If portfolioNoteConcept Is Nothing Then
        '    Throw New ArgumentNullException("portfolioNoteConcept Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _portfolioNoteConceptRepository.UnitWork
        'Try
        '    portfolioNoteConcept.ModificationDate = DateTime.Now
        '    portfolioNoteConcept.ModificationUser = audit.CodeUser
        '    Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioNoteConcept)(portfolioNoteConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    _portfolioNoteConceptRepository.DeleteEntity(portfolioNoteConcept)
        '    UnitOfWork.Commit()
        '    auditProcess.Execute()

        '    Return New ActionResult With {.StateResult = True}
        'Catch ex As OptimisticConcurrencyException
        '    UnitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As UpdateException
        '    UnitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False}
        'End Try




        If portfolioNoteConcept Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._portfolioNoteConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                portfolioNoteConcept.ModificationUser = audit.CodeUser
                portfolioNoteConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioNoteConcept)(portfolioNoteConcept, audit, status)

                'While portfolioNoteConcept.InvoiceCategoriesUser.Count > 0
                '    portfolioNoteConcept.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                portfolioNoteConcept.MarkAsDeleted()
                Me._portfolioNoteConceptRepository.SaveEntity(portfolioNoteConcept)
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
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code Vacio</exception>
    Public Function GetPortfolioNoteConcept(code As String, audit As AuditMessage) As PortfolioNoteConcept Implements IPortfolioNoteConceptAdminService.GetPortfolioNoteConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Dim portfolioNoteConcept = _portfolioNoteConceptRepository.GetPortfolioNoteConcept(code)
            If portfolioNoteConcept IsNot Nothing AndAlso portfolioNoteConcept.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioNoteConcept)(portfolioNoteConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return portfolioNoteConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioNoteConcept()
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de nota por id
    ''' </summary>
    ''' <param name="id">id</param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteConceptById(id As Integer) As PortfolioNoteConcept Implements IPortfolioNoteConceptAdminService.GetPortfolioNoteConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return _portfolioNoteConceptRepository.GetPortfolioNoteConceptById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' guardar un concepto de nota
    ''' </summary>
    ''' <param name="portfolioNoteConcept">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">portfolioNoteConcept Vacio</exception>
    Public Function SavePortfolioNoteConcept(portfolioNoteConcept As PortfolioNoteConcept, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PortfolioNoteConcept) Implements IPortfolioNoteConceptAdminService.SavePortfolioNoteConcept
        'If portfolioNoteConcept Is Nothing Then
        '    Throw New ArgumentNullException("portfolioNoteConcept Vacio")
        'End If
        'Dim portfolioNoteConceptUnitOfWork As IUnitWork = _portfolioNoteConceptRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        'Try
        '    Dim seq As PortfolioSequenceDetail = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioNoteConcept)
        '    Dim status As Integer
        '    Dim auxPortfolioNoteConcept As PortfolioNoteConcept = Nothing

        '    If portfolioNoteConcept.Code Is Nothing OrElse portfolioNoteConcept.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequensePortfolioDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                portfolioNoteConcept.Code = res
        '                seq.Next += 1
        '                Me._sequensePortfolioDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    If portfolioNoteConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        portfolioNoteConcept.CreationDate = DateTime.Now
        '        portfolioNoteConcept.CreationUser = audit.CodeUser
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        portfolioNoteConcept.ModificationDate = DateTime.Now
        '        portfolioNoteConcept.ModificationUser = audit.CodeUser
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxPortfolioNoteConcept = _portfolioNoteConceptRepository.GetPortfolioNoteConcept(portfolioNoteConcept.Code, False)
        '    End If

        '    _portfolioNoteConceptRepository.SaveEntity(portfolioNoteConcept)
        '    portfolioNoteConceptUnitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of PortfolioNoteConcept)(portfolioNoteConcept, audit, status, auxPortfolioNoteConcept)
        '    auditProcess.Execute()
        '    Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = True, .ObjectEmbbeded = portfolioNoteConcept}
        'Catch ex As OptimisticConcurrencyException
        '    portfolioNoteConceptUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    portfolioNoteConceptUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = False}
        'End Try

        If portfolioNoteConcept Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._portfolioNoteConceptRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(portfolioNoteConcept.Code) Then
                    Dim seq As PortfolioSequenceDetail = Me._sequensePortfolioDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            portfolioNoteConcept.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of PortfolioNoteConcept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), portfolioNoteConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of PortfolioNoteConcept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As PortfolioNoteConcept = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioNoteConcept)
                Dim status As Integer

                If portfolioNoteConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    portfolioNoteConcept.CreationUser = audit.CodeUser
                    portfolioNoteConcept.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = portfolioNoteConcept.OriginalValue
                    portfolioNoteConcept.ModificationUser = audit.CodeUser
                    portfolioNoteConcept.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._portfolioNoteConceptRepository.SaveEntity(portfolioNoteConcept)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioNoteConcept)(portfolioNoteConcept, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                portfolioNoteConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = portfolioNoteConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePortfolioNoteConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PortfolioNoteConcept) Implements IPortfolioNoteConceptAdminService.ChangeStatePortfolioNoteConcept
        'Dim portfolioNoteConcept As PortfolioNoteConcept = GetPortfolioNoteConcept(code, audit)
        'portfolioNoteConcept.Status = state
        'portfolioNoteConcept.MarkAsModified()
        'Return SavePortfolioNoteConcept(portfolioNoteConcept, audit)


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
            Dim portfolioNoteConcept As PortfolioNoteConcept = Me._portfolioNoteConceptRepository.GetPortfolioNoteConcept(code.Trim())
            If portfolioNoteConcept IsNot Nothing AndAlso portfolioNoteConcept.Id > 0 Then
                portfolioNoteConcept.Status = state
            End If
            Dim result = Me.SavePortfolioNoteConcept(portfolioNoteConcept, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioNoteConcept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _portfolioNoteConceptRepository = Nothing
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

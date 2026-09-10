'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 10-04-2014
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

Public Class ProvisionRangesAdminService
    Implements IProvisionRangesAdminService


#Region "Fields"
    Private Const FORM_NAME As String = "FrmProvisionRanges"
    Private _provisionRangesRepository As IProvisionRangesRepository
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository
#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="provisionRangesRepository">Repositorio de la entidad concepto de nota</param>
    Public Sub New(ByVal provisionRangesRepository As IProvisionRangesRepository, ByVal sequenseRepository As ISequensePortfolioDRepository)
        If (provisionRangesRepository Is Nothing) Then
            Throw New ArgumentNullException("provisionRangesRepository Vacio")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _provisionRangesRepository = provisionRangesRepository
        _sequensePortfolioDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' eliminar un Rango de provision
    ''' </summary>
    ''' <param name="provisionRanges">rango de provision</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">provisionRanges Vacio</exception>
    Public Function DeleteProvisionRanges(provisionRanges As ProvisionRanges, audit As AuditMessage) As ActionResult Implements IProvisionRangesAdminService.DeleteProvisionRanges
        'If provisionRanges Is Nothing Then
        '    Throw New ArgumentNullException("provisionRanges Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _provisionRangesRepository.UnitWork
        'Try
        '    provisionRanges.ModificationDate = DateTime.Now
        '    provisionRanges.ModificationUser = audit.CodeUser
        '    Dim auditProcess As New IndigoAuditSimpleEntity(Of ProvisionRanges)(provisionRanges, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    _provisionRangesRepository.DeleteEntity(provisionRanges)
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



        If provisionRanges Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._provisionRangesRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                provisionRanges.ModificationUser = audit.CodeUser
                provisionRanges.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ProvisionRanges)(provisionRanges, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                provisionRanges.MarkAsDeleted()
                Me._provisionRangesRepository.SaveEntity(provisionRanges)
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
    ''' metodo para obtener un Rango de provision
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code Vacio</exception>
    Public Function GetProvisionRangesByCode(code As String, audit As AuditMessage) As ProvisionRanges Implements IProvisionRangesAdminService.GetProvisionRangesByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Dim provisionRanges = _provisionRangesRepository.GetProvisionRangesByCode(code)
            If provisionRanges IsNot Nothing AndAlso provisionRanges.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ProvisionRanges)(provisionRanges, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return provisionRanges
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ProvisionRanges()
        End Try
    End Function

    ''' <summary>
    ''' guardar un Rango de provision
    ''' </summary>
    ''' <param name="provisionRanges">rango de provision</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">provisionRanges Vacio</exception>
    Public Function SaveProvisionRanges(provisionRanges As ProvisionRanges, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ProvisionRanges) Implements IProvisionRangesAdminService.SaveProvisionRanges
        'If provisionRanges Is Nothing Then
        '    Throw New ArgumentNullException("provisionRanges Vacio")
        'End If
        'Dim provisionRangesUnitOfWork As IUnitWork = _provisionRangesRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        'Try
        '    Dim seq As PortfolioSequenceDetail = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of ProvisionRanges)
        '    Dim status As Integer
        '    Dim auxprovisionRanges As ProvisionRanges = Nothing

        '    If provisionRanges.Code Is Nothing OrElse provisionRanges.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequensePortfolioDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                provisionRanges.Code = res
        '                seq.Next += 1
        '                Me._sequensePortfolioDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of ProvisionRanges) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of ProvisionRanges) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    If provisionRanges.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        provisionRanges.CreationUser = audit.CodeUser
        '        provisionRanges.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        provisionRanges.ModificationDate = DateTime.Now
        '        provisionRanges.ModificationUser = audit.CodeUser
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxprovisionRanges = _provisionRangesRepository.GetProvisionRangesByCode(provisionRanges.Code, False)
        '    End If

        '    _provisionRangesRepository.SaveEntity(provisionRanges)
        '    provisionRangesUnitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of ProvisionRanges)(provisionRanges, audit, status, auxprovisionRanges)
        '    auditProcess.Execute()

        '    Return New ActionResult(Of ProvisionRanges) With {.StateResult = True, .ObjectEmbbeded = provisionRanges}
        'Catch ex As OptimisticConcurrencyException
        '    provisionRangesUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    Return New ActionResult(Of ProvisionRanges) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    provisionRangesUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of ProvisionRanges) With {.StateResult = False}
        'End Try



        If provisionRanges Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._provisionRangesRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(provisionRanges.Code) Then
                    Dim seq As PortfolioSequenceDetail = Me._sequensePortfolioDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            provisionRanges.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ProvisionRanges) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), provisionRanges.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ProvisionRanges) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ProvisionRanges = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ProvisionRanges)
                Dim status As Integer

                If provisionRanges.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    provisionRanges.CreationUser = audit.CodeUser
                    provisionRanges.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = provisionRanges.OriginalValue
                    provisionRanges.ModificationUser = audit.CodeUser
                    provisionRanges.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._provisionRangesRepository.SaveEntity(provisionRanges)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ProvisionRanges)(provisionRanges, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                provisionRanges.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ProvisionRanges) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = provisionRanges, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ProvisionRanges) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProvisionRanges) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateProvisionRanges(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProvisionRanges) Implements IProvisionRangesAdminService.ChangeStateProvisionRanges
        'Dim ProvisionRanges As ProvisionRanges = GetProvisionRangesByCode(code, audit)
        'ProvisionRanges.Status = state
        'ProvisionRanges.MarkAsModified()
        'Return SaveProvisionRanges(ProvisionRanges, audit)



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
            Dim ProvisionRanges As ProvisionRanges = Me._provisionRangesRepository.GetProvisionRangesByCode(code.Trim())
            If ProvisionRanges IsNot Nothing AndAlso ProvisionRanges.Id > 0 Then
                ProvisionRanges.Status = state
            End If
            Dim result = Me.SaveProvisionRanges(ProvisionRanges, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProvisionRanges) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _sequensePortfolioDRepository = Nothing
            _provisionRangesRepository = Nothing
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

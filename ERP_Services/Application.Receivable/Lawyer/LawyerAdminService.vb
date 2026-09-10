'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/06/2017
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
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions

#End Region

Public Class LawyerAdminService
    Implements ILawyerAdminService

#Region "Variables"
    Private Const FORM_NAME As String = "FrmLawyers"

    Private _lawyerRepository As ILawyerRepository

    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository

#End Region

#Region "Builder"
    Public Sub New(ByVal lawyerRepository As ILawyerRepository, ByVal sequenseRepository As ISequensePortfolioDRepository)
        If (lawyerRepository Is Nothing) Then
            Throw New ArgumentNullException("lawyerRepository Vacio")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _lawyerRepository = lawyerRepository
        _sequensePortfolioDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para eliminar un abogado
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">PortfolioConcept Vacio</exception>
    Public Function DeleteLawyer(Lawyer As Lawyer, audit As AuditMessage) As ActionResult Implements ILawyerAdminService.DeleteLawyer
        'If Lawyer Is Nothing Then
        '    Throw New ArgumentNullException("Lawyer Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _lawyerRepository.UnitWork
        'Try
        '    Lawyer.ModificationDate = DateTime.Now
        '    Lawyer.ModificationUser = audit.CodeUser
        '    Dim auditProcess As New IndigoAuditSimpleEntity(Of Lawyer)(Lawyer, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    _lawyerRepository.DeleteEntity(Lawyer)
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



        If Lawyer Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._lawyerRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Lawyer.ModificationUser = audit.CodeUser
                Lawyer.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Lawyer)(Lawyer, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                Lawyer.MarkAsDeleted()
                Me._lawyerRepository.SaveEntity(Lawyer)
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
    ''' metodo para obtener un abogado
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code Vacio</exception>
    Public Function GetLawyerByCode(code As String, audit As AuditMessage) As ActionResult(Of Lawyer) Implements ILawyerAdminService.GetLawyerByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Lawyer As Lawyer = _lawyerRepository.GetLawyerByCode(code.Trim())
            If Lawyer IsNot Nothing AndAlso Lawyer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Lawyer)(Lawyer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Lawyer) With {.StateResult = True, .ObjectEmbbeded = Lawyer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Lawyer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' metodo para guardar un abogado
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">portfolioConcept Vacio</exception>
    Public Function SaveLawyer(Lawyer As Lawyer, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Lawyer) Implements ILawyerAdminService.SaveLawyer
        'If Lawyer Is Nothing Then
        '    Throw New ArgumentNullException("Lawyer Vacio")
        'End If
        'Dim lawyerUnitOfWork As IUnitWork = _lawyerRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        'Try

        '    'Se valida que no haya sido registrado el tercero anteriormente
        '    Dim lawyerTemp = _lawyerRepository.GetLawyerByThirdPartyId(Lawyer.ThirdPartyId, Lawyer.Id)
        '    If lawyerTemp IsNot Nothing Then
        '        Return New ActionResult(Of Lawyer) With {.StateResult = False, .MessageResult = {"El tercero seleccionado ya existe como abogado"}.ToList()}
        '    End If

        '    Dim seq As PortfolioSequenceDetail = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of Lawyer)
        '    Dim status As Integer
        '    Dim auxLawyer As Lawyer = Nothing

        '    If Lawyer.Code Is Nothing OrElse Lawyer.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequensePortfolioDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                Lawyer.Code = res
        '                seq.Next += 1
        '                Me._sequensePortfolioDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of Lawyer) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of Lawyer) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If


        '    If Lawyer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        Lawyer.CreationUser = audit.CodeUser
        '        Lawyer.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        Lawyer.ModificationDate = DateTime.Now
        '        Lawyer.ModificationUser = audit.CodeUser
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxLawyer = Lawyer.OriginalValue
        '    End If

        '    _lawyerRepository.SaveEntity(Lawyer)
        '    lawyerUnitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of Lawyer)(Lawyer, audit, status, auxLawyer)
        '    auditProcess.Execute()
        '    Return New ActionResult(Of Lawyer) With {.StateResult = True, .ObjectEmbbeded = Lawyer}

        'Catch ex As OptimisticConcurrencyException
        '    lawyerUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    Return New ActionResult(Of Lawyer) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    lawyerUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of Lawyer) With {.StateResult = False}
        'End Try



        If Lawyer Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._lawyerRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Lawyer.Code) Then
                    Dim seq As PortfolioSequenceDetail = Me._sequensePortfolioDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Lawyer.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Lawyer) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Lawyer.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Lawyer) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Lawyer = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Lawyer)
                Dim status As Integer

                If Lawyer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Lawyer.CreationUser = audit.CodeUser
                    Lawyer.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Lawyer.OriginalValue
                    Lawyer.ModificationUser = audit.CodeUser
                    Lawyer.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._lawyerRepository.SaveEntity(Lawyer)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Lawyer)(Lawyer, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Lawyer.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Lawyer) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Lawyer, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Lawyer) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Lawyer) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>    
    ''' <returns></returns>
    Public Function ChangeStateLawyer(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Lawyer) Implements ILawyerAdminService.ChangeStateLawyer
        'Dim Lawyer As Lawyer = _lawyerRepository.GetLawyerByCode(code)
        'Lawyer.Status = state
        'Lawyer.MarkAsModified()
        'Return SaveLawyer(Lawyer, audit)


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
            Dim Lawyer As Lawyer = Me._lawyerRepository.GetLawyerByCode(code.Trim())
            If Lawyer IsNot Nothing AndAlso Lawyer.Id > 0 Then
                Lawyer.Status = state
            End If
            Dim result = Me.SaveLawyer(Lawyer, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Lawyer) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un abogado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLawyerById(id As Integer) As ActionResult(Of Lawyer) Implements ILawyerAdminService.GetLawyerById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim Lawyer As Lawyer = Me._lawyerRepository.GetLawyerById(id)
            If Lawyer IsNot Nothing AndAlso Lawyer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Lawyer)(Lawyer, Nothing, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Lawyer) With {.StateResult = True, .ObjectEmbbeded = Lawyer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Lawyer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _lawyerRepository = Nothing
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

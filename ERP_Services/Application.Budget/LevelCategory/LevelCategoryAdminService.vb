'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
'Imports System.Data.Entity.Core
'Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
#End Region

Public Class LevelCategoryAdminService
    Implements ILevelCategoryAdminService


#Region "Fields"

    ''' <summary>
    ''' Repositorio de las fuentes de financiacion
    ''' </summary>
    Private _levelCategoryRepository As ILevelCategoryRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="levelCategoryRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal levelCategoryRepository As ILevelCategoryRepository)
        If levelCategoryRepository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _levelCategoryRepository = levelCategoryRepository
    End Sub
#End Region


    Public Function DeleteLevelCategory(LevelCategory As LevelCategory, audit As AuditMessage) As ActionResult Implements ILevelCategoryAdminService.DeleteLevelCategory
        If LevelCategory Is Nothing Then
            Throw New ArgumentNullException("LevelCategory vacio")
        End If
        Dim unitOfWork As IUnitWork = Me._levelCategoryRepository.UnitWork
        Try

            Me._levelCategoryRepository.DeleteEntity(LevelCategory)
            unitOfWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(LevelCategory).Name, audit.Functional, LevelCategory.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of LevelCategory)(LevelCategory, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}

        Catch ex As System.Data.Entity.Infrastructure.DbUpdateConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})} 'error de concurrencia
        Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"c-0000"})} ' error de dependencia
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})} ' error generico
        End Try
    End Function

    Public Function GetLevelCategory(level As String, audit As AuditMessage) As LevelCategory Implements ILevelCategoryAdminService.GetLevelCategory
        If String.IsNullOrEmpty(level) Then
            Throw New ArgumentNullException("level")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim levelcategory As LevelCategory = Me._levelCategoryRepository.GetLevelCategory(level.Trim())
            If levelcategory IsNot Nothing AndAlso levelcategory.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of LevelCategory)(levelcategory, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return levelcategory
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListLevelsCategory(audit As AuditMessage) As List(Of LevelCategory) Implements ILevelCategoryAdminService.ListLevelsCategory
        Try
            Dim levelscategory As List(Of LevelCategory) = Me._levelCategoryRepository.ListLevelsCategory()
            Return levelscategory
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveLevelCategory(LevelsCategory As List(Of LevelCategory), audit As AuditMessage) As ActionResult(Of List(Of LevelCategory)) Implements ILevelCategoryAdminService.SaveLevelCategory
        If LevelsCategory Is Nothing Then
            Throw New ArgumentNullException("LevelCategory")
        End If
        Dim LevelCategoryunitOfWork As IUnitWork = Me._levelCategoryRepository.UnitWork
        Try
            For Each LevelCategory In LevelsCategory
                With LevelCategory
                    If .ChangeTracker.State = ObjectState.Added Then
                        .CreationUser = audit.IdUser
                        .CreationDate = DateTime.Now
                    ElseIf .ChangeTracker.State = ObjectState.Modified Then
                        .ModificationUser = audit.IdUser
                        .ModificationDate = DateTime.Now
                    End If

                End With
                If LevelCategory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse LevelCategory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified OrElse LevelCategory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    Me._levelCategoryRepository.SaveEntity(LevelCategory)
                End If
            Next

            LevelCategoryunitOfWork.Commit()

            For Each LevelCategory In LevelsCategory
                Dim auxLevelCategory As LevelCategory = Me._levelCategoryRepository.GetLevelCategory(LevelCategory.Level, False)
                If LevelCategory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(GetType(LevelCategory).Name, audit.Functional, LevelCategory.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of LevelCategory)(LevelCategory, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf LevelCategory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(GetType(LevelCategory).Name, audit.Functional, LevelCategory.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of LevelCategory)(LevelCategory, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxLevelCategory)
                    auditObject.Execute()
                End If
            Next
            Return New ActionResult(Of List(Of LevelCategory)) With {.StateResult = True, .ObjectEmbbeded = LevelsCategory}
        Catch ex As OptimisticConcurrencyException
            LevelCategoryunitOfWork.RollbackChanges()
            Return New ActionResult(Of List(Of LevelCategory)) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            LevelCategoryunitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of LevelCategory)) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _levelCategoryRepository = Nothing
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

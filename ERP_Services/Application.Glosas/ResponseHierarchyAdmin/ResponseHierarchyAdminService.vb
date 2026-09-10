'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 10-06-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Transactions

Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Application.Base
Imports Domain.Common.Entities
Imports Domain.Security
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure

#End Region
Public Class ResponseHierarchyAdminService
    Implements IResponseHierarchyAdminService



    Private _ResponseHierarchyRepository As IResponseHierarchyRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="ResponsibleAdminService" />.
    ''' </summary>
    ''' <param name="ResponseHierarchyRepository">el repositorio para el manejo de los Jerarquías.</param>
    Public Sub New(ByVal ResponseHierarchyRepository As IResponseHierarchyRepository)
        If ResponseHierarchyRepository Is Nothing Then
            Throw New ArgumentNullException("ResponseHierarchyRepository Vacío")
        End If
        _ResponseHierarchyRepository = ResponseHierarchyRepository
    End Sub

    ''' <summary>
    ''' Elimina una jerarquía
    ''' </summary>
    ''' <param name="GlosasResponseHierarchy"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteGlosasResponseHierarchy(GlosasResponseHierarchy As GlosasResponseHierarchy, audit As AuditMessage) As ActionResult Implements IResponseHierarchyAdminService.DeleteGlosasResponseHierarchy
        If GlosasResponseHierarchy Is Nothing Then
            Throw New ArgumentNullException("GlosasResponseHierarchy Vacio")
        End If
        Dim unitOfWork As IUnitWork = _ResponseHierarchyRepository.UnitWork
        Try
            'Elimino la jerraquía
            Dim auditProcess As IndigoAuditSimpleEntity(Of GlosasResponseHierarchy)
            GlosasResponseHierarchy.ModificationUser = audit.CodeUser
            GlosasResponseHierarchy.ModificationDate = Date.Now()
            _ResponseHierarchyRepository.SaveEntity(GlosasResponseHierarchy)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of GlosasResponseHierarchy)(GlosasResponseHierarchy, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function
    ''' <summary>
    ''' obtiene una jerarquía
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResponseHierarchy(Code As String, audit As AuditMessage) As GlosasResponseHierarchy Implements IResponseHierarchyAdminService.GetResponseHierarchy
        If String.IsNullOrEmpty(Code) = True Then
            Throw New ArgumentNullException("codeResponsible Vacio")
        End If
        Try
            Dim Responsible = _ResponseHierarchyRepository.GetResponseHierarchy(Code)
            If Responsible.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of GlosasResponseHierarchy)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Responsible
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Obtiene jerarquía por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResponseHierarchyById(id As Integer, audit As AuditMessage) As GlosasResponseHierarchy Implements IResponseHierarchyAdminService.GetResponseHierarchyById
        If id = 0 Then
            Throw New ArgumentNullException("id Vacio")
        End If
        Try
            Dim Responsible = _ResponseHierarchyRepository.GetResponseHierarchyById(id)
            If Responsible.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of GlosasResponseHierarchy)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Responsible
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Lista todas la Jerarquías
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListResponseHierarchy(audit As AuditMessage) As List(Of GlosasResponseHierarchy) Implements IResponseHierarchyAdminService.ListResponseHierarchy
        Try
            Dim Responsible = _ResponseHierarchyRepository.ListResponseHierarchy
            Return Responsible
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Guarda Una Jerarquía
    ''' </summary>
    ''' <param name="GlosasResponseHierarchy"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveGlosasResponseHierarchy(GlosasResponseHierarchy As GlosasResponseHierarchy, audit As AuditMessage) As ActionResult(Of GlosasResponseHierarchy) Implements IResponseHierarchyAdminService.SaveGlosasResponseHierarchy
        If GlosasResponseHierarchy Is Nothing Then
            Throw New ArgumentNullException("GlosasResponseHierarchy Vacio")
        End If
        Dim unitOfWork As IUnitWork = _ResponseHierarchyRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of GlosasResponseHierarchy)
            Dim AuxResponsible As GlosasResponseHierarchy = Nothing
            Dim status As Integer
            If GlosasResponseHierarchy.ChangeTracker.State = ObjectState.Modified Then
                AuxResponsible = GlosasResponseHierarchy.OriginalValue
                GlosasResponseHierarchy.ModificationUser = audit.CodeUser
                GlosasResponseHierarchy.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            ElseIf GlosasResponseHierarchy.ChangeTracker.State = ObjectState.Added Then
                GlosasResponseHierarchy.CreationUser = audit.CodeUser
                GlosasResponseHierarchy.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If
            _ResponseHierarchyRepository.SaveEntity(GlosasResponseHierarchy)
            'Confirmo la unidad de trabajo
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of GlosasResponseHierarchy)(GlosasResponseHierarchy, audit, status, AuxResponsible)
            auditProcess.Execute()
            Return New ActionResult(Of GlosasResponseHierarchy) With {.StateResult = True, .ObjectEmbbeded = GlosasResponseHierarchy}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GlosasResponseHierarchy) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosasResponseHierarchy) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ResponseHierarchyRepository = Nothing
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

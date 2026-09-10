'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 27-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Audit
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class KinshipAdminService
    Implements IKinshipAdminService

    ' Repositorio de parentesco
    Private _kinshipRepository As IKinshipRepository

    Public Sub New(ByVal kinshipRepositoty As IKinshipRepository)
        If kinshipRepositoty Is Nothing Then
            Throw New ArgumentNullException("KinshipRepository Vacio")
        End If
        _kinshipRepository = kinshipRepositoty
    End Sub

    ''' <summary>
    ''' Obtiene un parentesco en especifico
    ''' </summary>
    ''' <param name="code">Codigo del parentesco</param>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    Public Function GetKinship(code As String) As Kinship Implements IKinshipAdminService.GetKinship
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code vacio")
        End If
        Try
            Return _kinshipRepository.GetKinship(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los parentescos
    ''' </summary>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    Public Function ListAllKinship() As List(Of Kinship) Implements IKinshipAdminService.ListAllKinship
        Try
            Return _kinshipRepository.ListAllKinship()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza un parentesco
    ''' </summary>
    ''' <param name="kinship">Parentesco</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveKinship(kinship As Kinship, audit As AuditMessage) As Boolean Implements IKinshipAdminService.SaveKinship
        If kinship Is Nothing Then
            Throw New ArgumentNullException("Kinship Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _kinshipRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of Kinship)
            Dim auxKinship As Kinship = Nothing
            Dim status As Integer

            If kinship.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                kinship.ModificationUser = audit.CodeUser
                kinship.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxKinship = _kinshipRepository.GetKinship(kinship.Code, False)
            Else
                kinship.CreationUser = audit.CodeUser
                kinship.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _kinshipRepository.SaveEntity(kinship)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Kinship)(kinship, audit, status, auxKinship)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' elimina un objeto parentesco
    ''' </summary>
    ''' <param name="kinship">Parentesco</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteKinship(kinship As Kinship, audit As AuditMessage) As Domain.Base.Entities.ActionMessageResult(Of Kinship) Implements IKinshipAdminService.DeleteKinship
        Dim result As New ActionMessageResult(Of Kinship)
        result.StateResult = True
        If kinship Is Nothing Then
            Throw New ArgumentNullException("Kinship vacio")
        End If
        Dim UnitOfWork As IUnitWork = _kinshipRepository.UnitWork
        Try
            _kinshipRepository.DeleteEntity(kinship)
            UnitOfWork.Commit()
            'Auditoria
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(kinship.GetType.Name, audit.Functional, kinship.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Kinship)(kinship, audit, Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", kinship.Code))
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _kinshipRepository = Nothing
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

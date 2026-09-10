'***********************************************************************
' Assembly         : Application.Common
' Author           : Andres Alarcon
' Created          : 26/08/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class TaxExemptionsAdminService
    Implements ITaxExemptionsAdminService

#Region "Builder"

    ''' <summary>
    ''' Variable tipo repositorio para exoneraciones tributarias
    ''' </summary>
    ''' <remarks></remarks>
    Private _taxExemptionsRepository As ITaxExemptionsRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal taxExemptionsRepository As ITaxExemptionsRepository)
        If taxExemptionsRepository Is Nothing Then
            Throw New ArgumentNullException("taxExemptionsRepository Vacio")
        End If
        _taxExemptionsRepository = taxExemptionsRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateTaxExemptions(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of TaxExemptions) Implements ITaxExemptionsAdminService.ChangeStateTaxExemptions
        Dim TaxExemptions As TaxExemptions = _taxExemptionsRepository.GetTaxExemptions(code)
        TaxExemptions.Status = state
        Return SaveTaxExemptions(TaxExemptions, audit)
    End Function

    ''' <summary>
    ''' Obtiene una actividad economica por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaxExemptions(code As String, audit As AuditMessage) As ActionResult(Of TaxExemptions) Implements ITaxExemptionsAdminService.GetTaxExemptions
        If code = String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim TaxExemptions As TaxExemptions = Me._taxExemptionsRepository.GetTaxExemptions(code)
            If TaxExemptions IsNot Nothing AndAlso TaxExemptions.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of TaxExemptions)(TaxExemptions, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of TaxExemptions) With {.StateResult = True, .ObjectEmbbeded = TaxExemptions}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TaxExemptions) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la actividad economica por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaxExemptionsyById(id As Integer, audit As AuditMessage) As ActionResult(Of TaxExemptions) Implements ITaxExemptionsAdminService.GetTaxExemptionsyById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim TaxExemptions As TaxExemptions = Me._taxExemptionsRepository.GetTaxExemptionsById(id)
            If TaxExemptions IsNot Nothing AndAlso TaxExemptions.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of TaxExemptions)(TaxExemptions, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of TaxExemptions) With {.StateResult = True, .ObjectEmbbeded = TaxExemptions}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TaxExemptions) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una exoneracion tributaria
    ''' </summary>
    ''' <param name="TaxExemptions"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveTaxExemptions(TaxExemptions As TaxExemptions, audit As AuditMessage) As ActionResult(Of TaxExemptions) Implements ITaxExemptionsAdminService.SaveTaxExemptions
        If TaxExemptions Is Nothing Then
            Throw New ArgumentNullException("TaxExemptions")
        End If
        Dim unitOfWork As IUnitWork = Me._taxExemptionsRepository.UnitWork
        Try

            Dim auxTaxExemptions As TaxExemptions = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of TaxExemptions)
            Dim status As Integer

            If TaxExemptions.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                TaxExemptions.CreationUser = audit.CodeUser
                TaxExemptions.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxTaxExemptions = TaxExemptions.OriginalValue
                TaxExemptions.ModificationUser = audit.CodeUser
                TaxExemptions.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._taxExemptionsRepository.SaveEntity(TaxExemptions)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of TaxExemptions)(TaxExemptions, audit, status, auxTaxExemptions)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            TaxExemptions.MarkAsUnchanged()

            Return New ActionResult(Of TaxExemptions) With {.StateResult = True, .ObjectEmbbeded = TaxExemptions}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of TaxExemptions) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TaxExemptions) With {.StateResult = False}
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
            _taxExemptionsRepository = Nothing
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

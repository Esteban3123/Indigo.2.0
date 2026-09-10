'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Angi Camila Duran Vargas
' Created          : 15/03/2023
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

Public Class DiscountTypesAdminService
    Implements IDiscountTypesAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractDescriptionsRepository As IDiscountTypesRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal contractDescriptionsRepository As IDiscountTypesRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If contractDescriptionsRepository Is Nothing Then
            Throw New ArgumentNullException("contractDescriptionsRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _contractDescriptionsRepository = contractDescriptionsRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"


    ''' <summary>
    ''' Elimina un tipo de descuento
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDiscountTypes(DiscountTypes As DiscountTypes, audit As AuditMessage) As ActionResult Implements IDiscountTypesAdminService.DeleteDiscountTypes
        If DiscountTypes Is Nothing Then
            Throw New ArgumentNullException("DiscountTypes")
        End If
        Dim unitOfWork As IUnitWork = Me._contractDescriptionsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of DiscountTypes)
            auditProcess = New IndigoAuditSimpleEntity(Of DiscountTypes)(DiscountTypes, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._contractDescriptionsRepository.DeleteEntity(DiscountTypes)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de descuento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDiscountTypes(code As String, audit As AuditMessage) As ActionResult(Of DiscountTypes) Implements IDiscountTypesAdminService.GetDiscountTypes
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DiscountTypes As DiscountTypes = Me._contractDescriptionsRepository.GetDiscountTypes(code.Trim())
            If DiscountTypes IsNot Nothing AndAlso DiscountTypes.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DiscountTypes)(DiscountTypes, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DiscountTypes) With {.StateResult = True, .ObjectEmbbeded = DiscountTypes}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DiscountTypes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de descuento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDiscountTypesById(id As Integer, audit As AuditMessage) As ActionResult(Of DiscountTypes) Implements IDiscountTypesAdminService.GetDiscountTypesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DiscountTypes As DiscountTypes = Me._contractDescriptionsRepository.GetDiscountTypesById(id)
            If DiscountTypes IsNot Nothing AndAlso DiscountTypes.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DiscountTypes)(DiscountTypes, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DiscountTypes) With {.StateResult = True, .ObjectEmbbeded = DiscountTypes}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DiscountTypes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un tipo de descuento
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDiscountTypes(DiscountTypes As DiscountTypes, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DiscountTypes) Implements IDiscountTypesAdminService.SaveDiscountTypes
        If DiscountTypes Is Nothing Then
            Throw New ArgumentNullException("DiscountTypes")
        End If
        Dim unitOfWork As IUnitWork = Me._contractDescriptionsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If DiscountTypes.Code Is Nothing OrElse DiscountTypes.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        DiscountTypes.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of DiscountTypes) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of DiscountTypes) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxDiscountTypes As DiscountTypes = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of DiscountTypes)
            Dim status As Integer

            If DiscountTypes.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                DiscountTypes.CreationUser = audit.CodeUser
                DiscountTypes.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxDiscountTypes = DiscountTypes.OriginalValue
                DiscountTypes.ModificationUser = audit.CodeUser
                DiscountTypes.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._contractDescriptionsRepository.SaveEntity(DiscountTypes)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of DiscountTypes)(DiscountTypes, audit, status, auxDiscountTypes)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            DiscountTypes.MarkAsUnchanged()

            Return New ActionResult(Of DiscountTypes) With {.StateResult = True, .ObjectEmbbeded = DiscountTypes}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DiscountTypes) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DiscountTypes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _contractDescriptionsRepository = Nothing
            _secuenseDRepository = Nothing
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

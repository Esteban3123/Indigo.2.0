'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources

Public Class SupplierTypeAdminService
    Implements ISupplierTypeAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierTypeRepository As ISupplierTypeRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal supplierTypeRepository As ISupplierTypeRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository)
        If supplierTypeRepository Is Nothing Then
            Throw New ArgumentNullException("supplierTypeRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _supplierTypeRepository = supplierTypeRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of SupplierType) Implements ISupplierTypeAdminService.ChangeState
        Dim supplierType As SupplierType = _supplierTypeRepository.GetSupplierType(code)
        supplierType.Status = state
        Return SaveSupplierType(supplierType, audit)
    End Function

    ''' <summary>
    ''' Elimina un tipo de proveedor
    ''' </summary>
    ''' <param name="SupplierType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSupplierType(SupplierType As SupplierType, audit As AuditMessage) As ActionResult Implements ISupplierTypeAdminService.DeleteSupplierType
        If SupplierType Is Nothing Then
            Throw New ArgumentNullException("SupplierType")
        End If
        Dim unitOfWork As IUnitWork = Me._supplierTypeRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of SupplierType)
            auditProcess = New IndigoAuditSimpleEntity(Of SupplierType)(SupplierType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._supplierTypeRepository.DeleteEntity(SupplierType)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de proveedor
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierType(code As String, audit As AuditMessage) As ActionResult(Of SupplierType) Implements ISupplierTypeAdminService.GetSupplierType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim supplierType As SupplierType = Me._supplierTypeRepository.GetSupplierType(code.Trim())
            If supplierType IsNot Nothing AndAlso supplierType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SupplierType)(supplierType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SupplierType) With {.StateResult = True, .ObjectEmbbeded = supplierType}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SupplierType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de proveedor por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierTypeById(id As String, audit As AuditMessage) As ActionResult(Of SupplierType) Implements ISupplierTypeAdminService.GetSupplierTypeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim supplierType As SupplierType = Me._supplierTypeRepository.GetSupplierTypeById(id)
            If supplierType IsNot Nothing AndAlso supplierType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SupplierType)(supplierType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SupplierType) With {.StateResult = True, .ObjectEmbbeded = supplierType}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SupplierType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el tipo de proveedor
    ''' </summary>
    ''' <param name="SupplierType"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSupplierType(SupplierType As SupplierType, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of SupplierType) Implements ISupplierTypeAdminService.SaveSupplierType
        If SupplierType Is Nothing Then
            Throw New ArgumentNullException("SupplierType")
        End If
        Dim unitOfWork As IUnitWork = Me._supplierTypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PaymentsSecuenceDetail = Nothing
            If SupplierType.Code Is Nothing OrElse SupplierType.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        SupplierType.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of SupplierType) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of SupplierType) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxSupplierType As SupplierType = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SupplierType)
            Dim status As Integer

            If SupplierType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                SupplierType.CreationUser = audit.CodeUser
                SupplierType.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSupplierType = SupplierType.OriginalValue
                SupplierType.ModificationUser = audit.CodeUser
                SupplierType.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._supplierTypeRepository.SaveEntity(SupplierType)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SupplierType)(SupplierType, audit, status, auxSupplierType)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            SupplierType.MarkAsUnchanged()

            Return New ActionResult(Of SupplierType) With {.StateResult = True, .ObjectEmbbeded = SupplierType}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SupplierType) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SupplierType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los tipos de proveedor hijos que tiene asociado el proveedor,
    ''' y obtiene sus respectivos padres
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierTypeBySupplierId(supplierId As Integer) As ActionResult(Of List(Of SupplierType)) Implements ISupplierTypeAdminService.GetSupplierTypeBySupplierId
        If supplierId = 0 Then
            Throw New ArgumentNullException("supplierId")
        End If
        Try
            Dim listSupplierType As List(Of SupplierType) = Me._supplierTypeRepository.GetSupplierTypeBySupplierId(supplierId)

            Return New ActionResult(Of List(Of SupplierType)) With {.StateResult = True, .ObjectEmbbeded = listSupplierType}
        Catch ex As InvalidOperationException
            Return New ActionResult(Of List(Of SupplierType)) With {.StateResult = False, .MessageResult = {"El proveedor ya tenía parametrizado un tipo de proveedor padre, debe eliminarlo y agregar solo los tipos de proveedor hijos."}.ToList}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SupplierType)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _supplierTypeRepository = Nothing
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

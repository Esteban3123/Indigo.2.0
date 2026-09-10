'***********************************************************************
' Assembly         : Application.Billing
' Author           : Andres Alarcon
' Created          : 14-06-2024
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
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Security
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region

Public Class ConditionSalesAdminService
    Implements IConditionSalesAdminService

    Private Const FORM_NAME As String = "FrmConditionsSale"

    Private _secuenseDRepository As IBillingSequenceDetailRepository
    Private _conditionSalesRepository As IConditionSalesRepository
    Private _IUserAdminService As IUserAdminService

    Public Sub New(secuenceDRepository As IBillingSequenceDetailRepository, ByVal ConditionSales As IConditionSalesRepository, IUserAdminService As IUserAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If ConditionSales Is Nothing Then
            Throw New ArgumentNullException("ConditionSales vacío")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If

        Me._secuenseDRepository = secuenceDRepository
        Me._conditionSalesRepository = ConditionSales
        Me._IUserAdminService = IUserAdminService
    End Sub

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListConditionSalesByUserCode(userCode As String) As List(Of ConditionSales) Implements IConditionSalesAdminService.ListConditionSalesByUserCode
        If String.IsNullOrEmpty(userCode) Then
            Throw New ArgumentNullException("userCode Vacio")
        End If

        Try
            Return _conditionSalesRepository.ListConditionSalesByUserCode(userCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una autorizacion de factura
    ''' </summary>
    ''' <param name="ConditionSales"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteConditionSales(ConditionSales As ConditionSales, audit As AuditMessage) As ActionResult Implements IConditionSalesAdminService.DeleteConditionSales
        If ConditionSales Is Nothing Then
            Throw New ArgumentNullException("ConditionSales")
        End If

        Dim unitOfWork As IUnitWork = Me._conditionSalesRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                ConditionSales.ModificationUser = audit.CodeUser
                ConditionSales.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ConditionSales)(ConditionSales, audit, status)

                While ConditionSales.ConditionSalesUser.Count > 0
                    ConditionSales.ConditionSalesUser(ConditionSales.ConditionSalesUser.Count - 1).MarkAsDeleted()
                End While

                ConditionSales.MarkAsDeleted()
                Me._conditionSalesRepository.SaveEntity(ConditionSales)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorización
    ''' </summary>
    ''' <param name="ConditionSales">The billing authorization.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="ArgumentNullException">distributionManpower</exception>
    Public Function SaveConditionSales(ConditionSales As ConditionSales, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of ConditionSales) Implements IConditionSalesAdminService.SaveConditionSales
        If ConditionSales Is Nothing Then
            Throw New ArgumentNullException("ConditionSales")
        End If

        Dim unitOfWork As IUnitWork = Me._conditionSalesRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(ConditionSales.Code) Then
                    Dim seq As BillingSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSecuence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ConditionSales.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ConditionSales) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), ConditionSales.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ConditionSales) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ConditionSales = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ConditionSales)
                Dim status As Integer

                If ConditionSales.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ConditionSales.CreationUser = audit.CodeUser
                    ConditionSales.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = ConditionSales.OriginalValue
                    ConditionSales.ModificationUser = audit.CodeUser
                    ConditionSales.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._conditionSalesRepository.SaveEntity(ConditionSales)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ConditionSales)(ConditionSales, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ConditionSales.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ConditionSales) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ConditionSales, .Message = MessageResult}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ConditionSales) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConditionSales) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una autorización de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditionSalesById(id As Integer, audit As AuditMessage) As ActionResult(Of ConditionSales) Implements IConditionSalesAdminService.GetConditionSalesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim ConditionSales As ConditionSales = _conditionSalesRepository.GetConditionSalesById(id)
            Return New ActionResult(Of ConditionSales) With {.StateResult = True, .ObjectEmbbeded = ConditionSales}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConditionSales) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditionSalesByCode(code As String, audit As AuditMessage) As ActionResult(Of ConditionSales) Implements IConditionSalesAdminService.GetConditionSalesByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim ConditionSales As ConditionSales = _conditionSalesRepository.GetConditionSalesByCode(code)

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If ConditionSales IsNot Nothing AndAlso ConditionSales.Id > 0 AndAlso ConditionSales.ConditionSalesUser IsNot Nothing AndAlso ConditionSales.ConditionSalesUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each bu In ConditionSales.ConditionSalesUser
                    ListUserIds.Add(bu.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In ConditionSales.ConditionSalesUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of ConditionSales) With {.StateResult = True, .ObjectEmbbeded = ConditionSales}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConditionSales) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateConditionSales(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ConditionSales) Implements IConditionSalesAdminService.ChangeStateConditionSales

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
            Dim ConditionSales As ConditionSales = Me._conditionSalesRepository.GetConditionSalesByCode(code.Trim())
            If ConditionSales IsNot Nothing AndAlso ConditionSales.Id > 0 Then
                ConditionSales.Status = state
            End If

            Dim result = Me.SaveConditionSales(ConditionSales, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If

            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConditionSales) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _IUserAdminService.Dispose()
            End If
            _secuenseDRepository = Nothing
            _conditionSalesRepository = Nothing
            _IUserAdminService = Nothing
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

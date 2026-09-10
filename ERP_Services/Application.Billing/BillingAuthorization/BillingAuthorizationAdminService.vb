'***********************************************************************
' Assembly         : Application.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-27
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

Public Class BillingAuthorizationAdminService
    Implements IBillingAuthorizationAdminService

    Private Const FORM_NAME As String = "FrmBillingAuthorization"


    ''' <summary>
    ''' Repositorio
    ''' </summary>
    Private _secuenseDRepository As IBillingSequenceDetailRepository
    Private _billingAuthorizationRepository As IBillingAuthorizationRepository

    ''' <summary>
    ''' Aplicación
    ''' </summary>
    ''' <remarks></remarks>
    Private _IUserAdminService As IUserAdminService
    Private _IElectronicDocumentsAdminService As ElectronicDocuments.IElectronicDocumentsAdminService

    Public Sub New(secuenceDRepository As IBillingSequenceDetailRepository, ByVal billingAuthorization As IBillingAuthorizationRepository, IUserAdminService As IUserAdminService, 
                   IElectronicDocumentsAdminService As ElectronicDocuments.IElectronicDocumentsAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If billingAuthorization Is Nothing Then
            Throw New ArgumentNullException("billingAuthorization vacío")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If

        Me._secuenseDRepository = secuenceDRepository
        Me._billingAuthorizationRepository = billingAuthorization
        Me._IUserAdminService = IUserAdminService
        Me._IElectronicDocumentsAdminService = IElectronicDocumentsAdminService
    End Sub

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListBillingAuthorizationByUserCode(userCode As String) As List(Of BillingAuthorization) Implements IBillingAuthorizationAdminService.ListBillingAuthorizationByUserCode
        If String.IsNullOrEmpty(userCode) Then
            Throw New ArgumentNullException("userCode Vacio")
        End If
        Try
            Return _billingAuthorizationRepository.ListBillingAuthorizationByUserCode(userCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una autorizacion de factura
    ''' </summary>
    ''' <param name="billingAuthorization"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBillingAuthorization(billingAuthorization As BillingAuthorization, audit As AuditMessage) As ActionResult Implements IBillingAuthorizationAdminService.DeleteBillingAuthorization
        If billingAuthorization Is Nothing Then
            Throw New ArgumentNullException("billingAuthorization")
        End If
        Dim unitOfWork As IUnitWork = Me._billingAuthorizationRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                billingAuthorization.ModificationUser = audit.CodeUser
                billingAuthorization.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingAuthorization)(billingAuthorization, audit, status)

                While billingAuthorization.BillingAuthorizationUser.Count > 0
                    billingAuthorization.BillingAuthorizationUser(billingAuthorization.BillingAuthorizationUser.Count - 1).MarkAsDeleted()
                End While
                billingAuthorization.MarkAsDeleted()
                Me._billingAuthorizationRepository.SaveEntity(billingAuthorization)
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
    ''' <param name="billingAuthorization">The billing authorization.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">distributionManpower</exception>
    Public Function SaveBillingAuthorization(billingAuthorization As BillingAuthorization, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of BillingAuthorization) Implements IBillingAuthorizationAdminService.SaveBillingAuthorization
        If billingAuthorization Is Nothing Then
            Throw New ArgumentNullException("distributionManpower")
        End If
        Dim unitOfWork As IUnitWork = Me._billingAuthorizationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(billingAuthorization.Code) Then
                    Dim seq As BillingSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSecuence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            billingAuthorization.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of BillingAuthorization) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), billingAuthorization.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of BillingAuthorization) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As BillingAuthorization = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of BillingAuthorization)
                Dim status As Integer

                If billingAuthorization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    billingAuthorization.CreationUser = audit.CodeUser
                    billingAuthorization.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    'Permitir actualizar el consecutivo, pero solo numeros mayores
                    Dim billingAuthorizationAux As Domain.Entities.BillingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationById(billingAuthorization.Id, False)
                    If billingAuthorizationAux IsNot Nothing Then
                        'Actualizar el TimeStamp con el valor actual de la base de datos para evitar error de concurrencia
                        billingAuthorization.TimeStamp = billingAuthorizationAux.TimeStamp
                        'Limpiar el registro del valor original del TimeStamp para evitar conflictos
                        If billingAuthorization.ChangeTracker.OriginalValues.ContainsKey("TimeStamp") Then
                            billingAuthorization.ChangeTracker.OriginalValues.Remove("TimeStamp")
                        End If

                        If billingAuthorizationAux.Consecutive > billingAuthorization.Consecutive Then
                            billingAuthorization.Consecutive = billingAuthorizationAux.Consecutive
                        End If
                    End If

                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = billingAuthorization.OriginalValue
                    billingAuthorization.ModificationUser = audit.CodeUser
                    billingAuthorization.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._billingAuthorizationRepository.SaveEntity(billingAuthorization)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of BillingAuthorization)(billingAuthorization, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                billingAuthorization.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of BillingAuthorization) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = billingAuthorization, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una autorización de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingAuthorizationById(id As Integer, audit As AuditMessage) As ActionResult(Of BillingAuthorization) Implements IBillingAuthorizationAdminService.GetBillingAuthorizationById

        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim billingAuthorization As Domain.Entities.BillingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationById(id)
            Return New ActionResult(Of Domain.Entities.BillingAuthorization) With {.StateResult = True, .ObjectEmbbeded = billingAuthorization}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingAuthorizationByCode(code As String, audit As AuditMessage) As ActionResult(Of BillingAuthorization) Implements IBillingAuthorizationAdminService.GetBillingAuthorizationByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim billingAuthorization As BillingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationByCode(code)

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If billingAuthorization IsNot Nothing AndAlso billingAuthorization.Id > 0 AndAlso billingAuthorization.BillingAuthorizationUser IsNot Nothing AndAlso billingAuthorization.BillingAuthorizationUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each bu In billingAuthorization.BillingAuthorizationUser
                    ListUserIds.Add(bu.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In billingAuthorization.BillingAuthorizationUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of BillingAuthorization) With {.StateResult = True, .ObjectEmbbeded = billingAuthorization}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .Message = {ex.Message}.ToString}
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
    Public Function ChangeStateBillingAuthorization(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BillingAuthorization) Implements IBillingAuthorizationAdminService.ChangeStateBillingAuthorization

        'Dim billingAuthorization As BillingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationByCode(code)
        'billingAuthorization.Status = state
        'Return SaveBillingAuthorization(billingAuthorization, audit)

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
            Dim billingAuthorization As BillingAuthorization = Me._billingAuthorizationRepository.GetBillingAuthorizationByCode(code.Trim())
            If billingAuthorization IsNot Nothing AndAlso billingAuthorization.Id > 0 Then
                billingAuthorization.Status = state
            End If
            Dim result = Me.SaveBillingAuthorization(billingAuthorization, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingAuthorizationResolution(operatingUnitId As Integer, billingAuthorization As BillingAuthorization, audit As AuditMessage) As ActionResult(Of BillingAuthorization) Implements IBillingAuthorizationAdminService.GetBillingAuthorizationResolution
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Return Me._IElectronicDocumentsAdminService.GetBillingAuthorizationResolution(operatingUnitId, billingAuthorization)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .Message = {ex.Message}.ToString}
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
            _billingAuthorizationRepository = Nothing
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

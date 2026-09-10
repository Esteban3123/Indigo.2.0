'***********************************************************************
' Assembly         : Application.Billing
' Author           : Andres Alarcon
' Created          : 2022-08-02
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

Public Class NumberingAuthorizationAdminService
    Implements INumberingAuthorizationAdminService

    Private Const FORM_NAME As String = "FrmNumberingAuthorization"

#Region "Properties"

    Private _sequenceDetailRepository As IBillingSequenceDetailRepository

    Private _NumberingAuthorizationRepository As INumberingAuthorizationRepository

    Private _userAdminService As IUserAdminService

#End Region

#Region "Methods"

    Public Sub New(secuenceDRepository As IBillingSequenceDetailRepository, ByVal NumberingAuthorization As INumberingAuthorizationRepository, IUserAdminService As IUserAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If NumberingAuthorization Is Nothing Then
            Throw New ArgumentNullException("NumberingAuthorization vacío")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If

        Me._sequenceDetailRepository = secuenceDRepository
        Me._NumberingAuthorizationRepository = NumberingAuthorization
        Me._userAdminService = IUserAdminService
    End Sub

    ''' <summary>
    ''' Lista todas las resoluciones de los documentos autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListNumberingAuthorizationByUserCode(userCode As String) As List(Of NumberingAuthorization) Implements INumberingAuthorizationAdminService.ListNumberingAuthorizationByUserCode
        If String.IsNullOrEmpty(userCode) Then
            Throw New ArgumentNullException("userCode Vacio")
        End If
        Try
            Return _NumberingAuthorizationRepository.ListNumberingAuthorizationByUserCode(userCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una autorizacion del Documento
    ''' </summary>
    ''' <param name="NumberingAuthorization"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteNumberingAuthorization(NumberingAuthorization As NumberingAuthorization, audit As AuditMessage) As ActionResult Implements INumberingAuthorizationAdminService.DeleteNumberingAuthorization
        If NumberingAuthorization Is Nothing Then
            Throw New ArgumentNullException("NumberingAuthorization")
        End If
        Dim unitOfWork As IUnitWork = Me._NumberingAuthorizationRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                NumberingAuthorization.ModificationUser = audit.CodeUser
                NumberingAuthorization.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of NumberingAuthorization)(NumberingAuthorization, audit, status)

                While NumberingAuthorization.NumberingAuthorizationUser.Count > 0
                    NumberingAuthorization.NumberingAuthorizationUser(NumberingAuthorization.NumberingAuthorizationUser.Count - 1).MarkAsDeleted()
                End While
                NumberingAuthorization.MarkAsDeleted()
                Me._NumberingAuthorizationRepository.SaveEntity(NumberingAuthorization)
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
    ''' <param name="NumberingAuthorization">The billing authorization.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">distributionManpower</exception>
    Public Function SaveNumberingAuthorization(NumberingAuthorization As NumberingAuthorization, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of NumberingAuthorization) Implements INumberingAuthorizationAdminService.SaveNumberingAuthorization
        If NumberingAuthorization Is Nothing Then
            Throw New ArgumentNullException("distributionManpower")
        End If
        Dim unitOfWork As IUnitWork = Me._NumberingAuthorizationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(NumberingAuthorization.Code) Then
                    Dim seq As BillingSequenceDetail = Me._sequenceDetailRepository.GetSequenseDById(idSecuence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            NumberingAuthorization.Code = res
                            seq.Next += 1
                            Me._sequenceDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of NumberingAuthorization) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), NumberingAuthorization.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of NumberingAuthorization) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As NumberingAuthorization = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of NumberingAuthorization)
                Dim status As Integer

                If NumberingAuthorization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    NumberingAuthorization.CreationUser = audit.CodeUser
                    NumberingAuthorization.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    'Permitir actualizar el consecutivo, pero solo numeros mayores
                    Dim NumberingAuthorizationAux As Domain.Entities.NumberingAuthorization = _NumberingAuthorizationRepository.GetNumberingAuthorizationById(NumberingAuthorization.Id, False)
                    If NumberingAuthorizationAux IsNot Nothing AndAlso NumberingAuthorizationAux.Consecutive > NumberingAuthorization.Consecutive Then
                        NumberingAuthorization.Consecutive = NumberingAuthorizationAux.Consecutive
                    End If

                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = NumberingAuthorization.OriginalValue
                    NumberingAuthorization.ModificationUser = audit.CodeUser
                    NumberingAuthorization.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._NumberingAuthorizationRepository.SaveEntity(NumberingAuthorization)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of NumberingAuthorization)(NumberingAuthorization, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                NumberingAuthorization.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of NumberingAuthorization) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = NumberingAuthorization, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of NumberingAuthorization) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of NumberingAuthorization) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una autorización del documento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNumberingAuthorizationById(id As Integer, audit As AuditMessage) As ActionResult(Of NumberingAuthorization) Implements INumberingAuthorizationAdminService.GetNumberingAuthorizationById

        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim NumberingAuthorization As Domain.Entities.NumberingAuthorization = _NumberingAuthorizationRepository.GetNumberingAuthorizationById(id)
            Return New ActionResult(Of Domain.Entities.NumberingAuthorization) With {.StateResult = True, .ObjectEmbbeded = NumberingAuthorization}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of NumberingAuthorization) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene una autorización del documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNumberingAuthorizationByCode(code As String, audit As AuditMessage) As ActionResult(Of NumberingAuthorization) Implements INumberingAuthorizationAdminService.GetNumberingAuthorizationByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim NumberingAuthorization As NumberingAuthorization = _NumberingAuthorizationRepository.GetNumberingAuthorizationByCode(code)

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If NumberingAuthorization IsNot Nothing AndAlso NumberingAuthorization.Id > 0 AndAlso NumberingAuthorization.NumberingAuthorizationUser IsNot Nothing AndAlso NumberingAuthorization.NumberingAuthorizationUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each bu In NumberingAuthorization.NumberingAuthorizationUser
                    ListUserIds.Add(bu.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _userAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In NumberingAuthorization.NumberingAuthorizationUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of NumberingAuthorization) With {.StateResult = True, .ObjectEmbbeded = NumberingAuthorization}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of NumberingAuthorization) With {.StateResult = False, .Message = {ex.Message}.ToString}
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
    Public Function ChangeStateNumberingAuthorization(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of NumberingAuthorization) Implements INumberingAuthorizationAdminService.ChangeStateNumberingAuthorization

        'Dim NumberingAuthorization As NumberingAuthorization = _NumberingAuthorizationRepository.GetNumberingAuthorizationByCode(code)
        'NumberingAuthorization.Status = state
        'Return SaveNumberingAuthorization(NumberingAuthorization, audit)

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
            Dim NumberingAuthorization As NumberingAuthorization = Me._NumberingAuthorizationRepository.GetNumberingAuthorizationByCode(code.Trim())
            If NumberingAuthorization IsNot Nothing AndAlso NumberingAuthorization.Id > 0 Then
                NumberingAuthorization.Status = state
            End If
            Dim result = Me.SaveNumberingAuthorization(NumberingAuthorization, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of NumberingAuthorization) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la resolución 
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNumberingAuthorizationResolution(operatingUnitId As Integer, NumberingAuthorization As NumberingAuthorization, audit As AuditMessage) As ActionResult(Of NumberingAuthorization) Implements INumberingAuthorizationAdminService.GetNumberingAuthorizationResolution
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Return New ActionResult(Of NumberingAuthorization) With {.StateResult = False, .Message = "Sin normatividad"}
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _userAdminService.Dispose()
            End If
            _sequenceDetailRepository = Nothing
            _NumberingAuthorizationRepository = Nothing
            _userAdminService = Nothing
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

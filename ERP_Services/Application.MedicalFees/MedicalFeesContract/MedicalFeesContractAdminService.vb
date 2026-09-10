'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports Application.Base
Imports Application.Events.Serializers
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Queue

Public Class MedicalFeesContractAdminService
    Implements IMedicalFeesContractAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesContactRepository As IMedicalFeesContractRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMedicalFeesSecuenceDetailRepository

    ''' <summary>
    ''' Fabrica de Indigo Queue para eventos
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal medicalFeesContactRepository As IMedicalFeesContractRepository,
                   ByVal secuenseDRepository As IMedicalFeesSecuenceDetailRepository,
                   ByVal factoryQueue As IFactoryQueue)
        If medicalFeesContactRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesContactRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _medicalFeesContactRepository = medicalFeesContactRepository
        _secuenseDRepository = secuenseDRepository
        _factoryQueue = factoryQueue
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
    Public Async Function ChangeStateMedicalFeesContractAsync(code As String, state As Integer, audit As AuditMessage) As Task(Of ActionResult(Of MedicalFeesContract)) Implements IMedicalFeesContractAdminService.ChangeStateMedicalFeesContractAsync
        Dim MedicalFeesContract As MedicalFeesContract = _medicalFeesContactRepository.GetMedicalFeesContract(code)
        MedicalFeesContract.Status = state
        Return Await SaveMedicalFeesContractAsync(MedicalFeesContract, audit)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="MedicalFeesContract"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMedicalFeesContract(MedicalFeesContract As MedicalFeesContract, audit As AuditMessage) As ActionResult Implements IMedicalFeesContractAdminService.DeleteMedicalFeesContract
        If MedicalFeesContract Is Nothing Then
            Throw New ArgumentNullException("MedicalFeesContract")
        End If
        Dim unitOfWork As IUnitWork = Me._medicalFeesContactRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of MedicalFeesContract)
            auditProcess = New IndigoAuditSimpleEntity(Of MedicalFeesContract)(MedicalFeesContract, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._medicalFeesContactRepository.DeleteEntity(MedicalFeesContract)
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
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesContract(code As String, audit As AuditMessage) As ActionResult(Of MedicalFeesContract) Implements IMedicalFeesContractAdminService.GetMedicalFeesContract
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MedicalFeesContract As MedicalFeesContract = Me._medicalFeesContactRepository.GetMedicalFeesContract(code.Trim())
            If MedicalFeesContract IsNot Nothing AndAlso MedicalFeesContract.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MedicalFeesContract)(MedicalFeesContract, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MedicalFeesContract) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesContract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicalFeesContract) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesContractById(id As Integer, audit As AuditMessage) As ActionResult(Of MedicalFeesContract) Implements IMedicalFeesContractAdminService.GetMedicalFeesContractById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MedicalFeesContract As MedicalFeesContract = Me._medicalFeesContactRepository.GetMedicalFeesContractById(id)
            If MedicalFeesContract IsNot Nothing AndAlso MedicalFeesContract.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MedicalFeesContract)(MedicalFeesContract, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MedicalFeesContract) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesContract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicalFeesContract) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="MedicalFeesContract"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveMedicalFeesContractAsync(MedicalFeesContract As MedicalFeesContract, audit As AuditMessage, Optional idSequense As Long = 0) As Task(Of ActionResult(Of MedicalFeesContract)) Implements IMedicalFeesContractAdminService.SaveMedicalFeesContractAsync
        If MedicalFeesContract Is Nothing Then
            Throw New ArgumentNullException("MedicalFeesContract")
        End If

        Dim unitOfWork As IUnitWork = Me._medicalFeesContactRepository.UnitWork
        Try
            Dim seq As MedicalFeesSecuenceDetail = Nothing
            If MedicalFeesContract.Code Is Nothing OrElse MedicalFeesContract.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MedicalFeesSecuence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        MedicalFeesContract.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of MedicalFeesContract) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of MedicalFeesContract) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxMedicalFeesContract As MedicalFeesContract = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of MedicalFeesContract)
            Dim status As Integer

            If MedicalFeesContract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                MedicalFeesContract.CreationUser = audit.CodeUser
                MedicalFeesContract.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxMedicalFeesContract = MedicalFeesContract.OriginalValue
                MedicalFeesContract.ModificationUser = audit.CodeUser
                MedicalFeesContract.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._medicalFeesContactRepository.SaveEntity(MedicalFeesContract)
            Await unitOfWork.CommitAsync()
            auditProcess = New IndigoAuditSimpleEntity(Of MedicalFeesContract)(MedicalFeesContract, audit, status, auxMedicalFeesContract)
            auditProcess.Execute()

            ' Disparar evento para procesamiento asíncrono de causaciones pendientes solo si se agrega un nuevo servicio IPS a los detalles
            If MedicalFeesContract.MedicalFeesContractException.Any(Function(x) x.ExceptionType = 1 And x.ChangeTracker.State = ObjectState.Added) Then
                Try
                    TriggerEvent(MedicalFeesContract, audit)
                Catch ex As Exception
                    ' Si falla el evento, solo registrar el error pero no fallar el guardado principal
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                End Try
            End If

            'Se marca la entidad como sin cambios
            MedicalFeesContract.MarkAsUnchanged()

            Return New ActionResult(Of MedicalFeesContract) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesContract}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MedicalFeesContract) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicalFeesContract) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Dispara evento de actualización de contrato médico para procesamiento asíncrono de causaciones
    ''' </summary>
    ''' <param name="medicalFeesContract">Contrato de honorarios médicos actualizado</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <remarks>
    ''' Este método envía un mensaje al Service Bus con la información del contrato actualizado
    ''' para que la Azure Function procese las causaciones pendientes de manera asíncrona
    ''' </remarks>
    Public Sub TriggerEvent(medicalFeesContract As MedicalFeesContract, audit As AuditMessage)
        If _factoryQueue Is Nothing Then
            ' Si no se inyectó la dependencia, no procesar evento
            Return
        End If

        Try
            Dim wrapperEvent As New Wrapper
            Dim changeTracker As String = medicalFeesContract.ChangeTracker.State.ToString().ToLower()

            ' Mapear estados del ChangeTracker a acciones de evento
            If {ObjectState.Unchanged, ObjectState.Modified}.Contains(medicalFeesContract.ChangeTracker.State) Then
                changeTracker = "contract_updated"
            ElseIf medicalFeesContract.ChangeTracker.State = ObjectState.Added Then
                changeTracker = "contract_created"
            ElseIf medicalFeesContract.ChangeTracker.State = ObjectState.Deleted Then
                changeTracker = "contract_deleted"
            End If

            ' Solo procesar eventos de actualización que puedan afectar causaciones
            If changeTracker = "contract_updated" OrElse changeTracker = "contract_created" Then
                Dim eventData As EventData = wrapperEvent.GenerateWrapperEventData(
                    medicalFeesContract,
                    audit.CodeUser,
                    changeTracker,
                    DittoSourceType.CausationPending)

                Dim queue As IIndigoQueue = _factoryQueue.CreateQueue()
                queue.Publish(eventData)
            End If
        Catch ex As Exception
            ' Si falla el evento, solo registrar pero no afectar el proceso principal
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        End Try
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _medicalFeesContactRepository = Nothing
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

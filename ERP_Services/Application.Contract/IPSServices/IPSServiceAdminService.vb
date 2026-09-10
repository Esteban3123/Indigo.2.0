'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Queue

Public Class IPSServiceAdminService
    Implements IIPSServiceAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _ipsServiceRepository As IIPSServicesRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

    Private _surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository

    Private _cupsHomologationRepository As ICupsHomologationRepository
    ''' <summary>
    ''' Variable de tipo repositorio para dependencia del repositorio de definicion de tarifas.
    ''' </summary>
    Private _definitionRateRepository As IDefinitionRateRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal ipsServiceRepository As IIPSServicesRepository, ByVal secuenseDRepository As ISequenseContractDRepository, surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository,
                   cupsHomologationRepository As ICupsHomologationRepository, definitionRateRepository As IDefinitionRateRepository, FactoryQueue As IFactoryQueue)
        If ipsServiceRepository Is Nothing Then
            Throw New ArgumentNullException("ipsServiceRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _ipsServiceRepository = ipsServiceRepository
        _secuenseDRepository = secuenseDRepository
        _surgicalProcedureServiceRepository = surgicalProcedureServiceRepository
        _cupsHomologationRepository = cupsHomologationRepository
        _definitionRateRepository = definitionRateRepository
        _factoryQueue = FactoryQueue
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
    Public Function ChangeStateIPSService(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of IPSService) Implements IIPSServiceAdminService.ChangeStateIPSService
        Dim IPSService As IPSService = _ipsServiceRepository.GetIPSService(code)
        IPSService.Status = state
        IPSService.MarkAsModified()
        Return SaveIPSService(IPSService, audit)
    End Function

    ''' <summary>
    ''' Elimina l aentidad
    ''' </summary>
    ''' <param name="IPSService"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteIPSService(IPSService As IPSService, audit As AuditMessage) As ActionResult Implements IIPSServiceAdminService.DeleteIPSService
        If IPSService Is Nothing Then
            Throw New ArgumentNullException("IPSService")
        End If
        Dim unitOfWork As IUnitWork = Me._ipsServiceRepository.UnitWork
        Dim unitOfWorkSurgicalProcedureService As IUnitWork = _surgicalProcedureServiceRepository.UnitWork
        Dim unitOfWorkCupsHomologation As IUnitWork = _cupsHomologationRepository.UnitWork
        Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try

                For Each item In IPSService.SurgicalProcedureService
                    _surgicalProcedureServiceRepository.DeleteEntity(item)
                    unitOfWorkSurgicalProcedureService.Commit()
                Next

                For Each item In IPSService.CupsHomologation
                    _cupsHomologationRepository.DeleteEntity(item)
                    unitOfWorkCupsHomologation.Commit()
                Next

                Dim auditProcess As IndigoAuditSimpleEntity(Of IPSService)
                auditProcess = New IndigoAuditSimpleEntity(Of IPSService)(IPSService, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                Me._ipsServiceRepository.DeleteEntity(IPSService)
                unitOfWork.Commit()
                auditProcess.Execute()

                IPSService.MarkAsDeleted()
                TriggerEvent(IPSService, audit)
                transaction.Complete()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                unitOfWorkSurgicalProcedureService.RollbackChanges()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As UpdateException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                unitOfWorkSurgicalProcedureService.RollbackChanges()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As DbUpdateException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                unitOfWorkSurgicalProcedureService.RollbackChanges()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                unitOfWorkSurgicalProcedureService.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIPSService(code As String, audit As AuditMessage) As ActionResult(Of IPSService) Implements IIPSServiceAdminService.GetIPSService
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim IPSService As IPSService = Me._ipsServiceRepository.GetIPSService(code.Trim())
            If IPSService IsNot Nothing AndAlso IPSService.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of IPSService)(IPSService, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of IPSService) With {.StateResult = True, .ObjectEmbbeded = IPSService}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of IPSService) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIPSServiceById(id As Integer, audit As AuditMessage) As ActionResult(Of IPSService) Implements IIPSServiceAdminService.GetIPSServiceById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim IPSService As IPSService = Me._ipsServiceRepository.GetIPSServiceById(id)
            If IPSService IsNot Nothing AndAlso IPSService.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of IPSService)(IPSService, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of IPSService) With {.StateResult = True, .ObjectEmbbeded = IPSService}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of IPSService) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="IPSService"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveIPSService(IPSService As IPSService, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of IPSService) Implements IIPSServiceAdminService.SaveIPSService
        If IPSService Is Nothing Then
            Throw New ArgumentNullException("IPSService")
        End If
        Dim unitOfWork As IUnitWork = Me._ipsServiceRepository.UnitWork
        Try
            Dim auxIPSService As IPSService = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of IPSService)
            Dim status As Integer

            If IPSService.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                IPSService.CreationUser = audit.CodeUser
                IPSService.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxIPSService = IPSService.OriginalValue
                IPSService.ModificationUser = audit.CodeUser
                IPSService.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If
            Dim listHomologation As New List(Of CupsHomologation)
            If (IPSService.CupsHomologation IsNot Nothing OrElse IPSService.CupsHomologation.Count > 0) Then
                listHomologation = (From e In IPSService.CupsHomologation Select e).ToList()
            End If
            Dim listSurgicalProcedureServiceId As New List(Of Integer)
            If IPSService.listSurgicalProcedureServiceDelete IsNot Nothing Then
                For Each item In IPSService.listSurgicalProcedureServiceDelete
                    listSurgicalProcedureServiceId.Add(item.Id)
                Next
            End If
            Dim detailsRepited = _definitionRateRepository.GetListDefinitionRateCode(listSurgicalProcedureServiceId)
            If (detailsRepited?.Any()) Then
                Dim listdetail = String.Join(", ", detailsRepited)
                Return New ActionResult(Of IPSService) With {.StateResult = False, .Message = "No se pueden eliminar algunos procedimientos quirurgicos ya que pertenecen al formulario Definicion de Tarifas con los siguientes codigos: " + listdetail}
            End If
            Dim listSurgicalProcedureService As New List(Of SurgicalProcedureService)
            If (IPSService.SurgicalProcedureService IsNot Nothing OrElse IPSService.SurgicalProcedureService.Count > 0) Then
                listSurgicalProcedureService = (From e In IPSService.SurgicalProcedureService Select e).ToList()
            End If
            Me._ipsServiceRepository.SaveEntity(IPSService)
            unitOfWork.Commit()
            'listCups
            If listHomologation.Count > 0 Then
                For Each item In listHomologation
                    IPSService.CupsHomologation.Add(item)
                Next
            End If
            'listSurgicalProcedureService
            If listSurgicalProcedureService.Count > 0 Then
                For Each item In listSurgicalProcedureService
                    IPSService.SurgicalProcedureService.Add(item)
                Next
            End If
            TriggerEvent(IPSService, audit)
            auditProcess = New IndigoAuditSimpleEntity(Of IPSService)(IPSService, audit, status, auxIPSService)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            IPSService.MarkAsUnchanged()

            Return New ActionResult(Of IPSService) With {.StateResult = True, .ObjectEmbbeded = IPSService}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of IPSService) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of IPSService) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' obtiene las homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    Public Function GetCupsHomologationByIPSServiceId(idIPSService As Integer) As List(Of CupsHomologation) Implements IIPSServiceAdminService.GetCupsHomologationByIPSServiceId
        Try
            Return _ipsServiceRepository.GetCupsHomologationByIPSServiceId(idIPSService)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of CupsHomologation)
        End Try
    End Function

    ''' <summary>
    ''' Copiar y pegar de la rejilla de servicios ips
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="ServiceManual"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyAndPasteIPSService(data As List(Of List(Of String)), ServiceManual As Integer) As ActionResult(Of List(Of SurgicalProcedureService), List(Of Tuple(Of String, Integer))) Implements IIPSServiceAdminService.CopyAndPasteIPSService
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListSurgicalProcedureService As New List(Of SurgicalProcedureService)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _ipsServiceRepository.SP_CopyAndPasteIPSService(xmlObject, ServiceManual)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListSurgicalProcedureService IsNot Nothing AndAlso ListSurgicalProcedureService.Count > 0 Then
                            Dim itemAddeed = ListSurgicalProcedureService.Find(Function(x) x.IPSServiceId = itemXml.IPSServiceId)
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim SurgicalProcedureService As New SurgicalProcedureService
                        With SurgicalProcedureService
                            .IPSServiceId = itemXml.IPSServiceId
                            .ServiceAmount = itemXml.ServiceAmount
                            .CodeNameService = itemXml.IPSServiceDescription
                            .ClassService = GetNameClass(itemXml.Class)
                            .DefaultService = itemXml.DefaultService
                        End With
                        ListSurgicalProcedureService.Add(SurgicalProcedureService)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of SurgicalProcedureService), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListSurgicalProcedureService, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of SurgicalProcedureService), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of SurgicalProcedureService), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of SurgicalProcedureService), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Convierte el listado de datos a xml
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            If item(0) IsNot Nothing Then
                builder.Append("<IPSServiceCode>" & item(0) & "</IPSServiceCode>")
            Else
                builder.Append("<IPSServiceCode>" & 0 & "</IPSServiceCode>")
            End If
            builder.Append("<IPSServiceDescription>" & "---" & "</IPSServiceDescription>")
            builder.Append("<IPSServiceId>" & 0 & "</IPSServiceId>")
            builder.Append("<Class>" & 0 & "</Class>")
            builder.Append("<DefaultService>" & 0 & "</DefaultService>")
            If item.Count > 1 Then
                builder.Append("<ServiceAmount>" & item(1).ToString & "</ServiceAmount>")
            Else
                builder.Append("<ServiceAmount>" & 0 & "</ServiceAmount>")
            End If

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la clase del servicio ips
    ''' </summary>
    ''' <param name="ClassIPS"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNameClass(ClassIPS As Integer) As String
        Select Case ClassIPS
            Case 1
                Return "Ninguno"
            Case 2
                Return "Cirujano"
            Case 3
                Return "Anestesiólogo"
            Case 4
                Return "Ayudante"
            Case 5
                Return "Derecho Sala"
            Case 6
                Return "Materiales Sutura"
            Case 7
                Return "Instrumentación Quirúrgica"
            Case Else
                Return String.Empty
        End Select
    End Function

    ''' <summary>
    ''' obtiene los servicvios ips qx por id ips padre, opcionalmente se puede filtra por una clase en especifico
    ''' </summary>
    ''' <param name="parentId"></param>
    ''' <param name="classService"></param>
    ''' <returns></returns>
    Public Function GetSurgicalProcedureServiceByParentIPSId(parentId As Integer, Optional classService As EClassService = 0) As ActionResult(Of List(Of IPSService)) Implements IIPSServiceAdminService.GetSurgicalProcedureServiceByParentIPSId
        If parentId = 0 Then
            Throw New ArgumentNullException("parentId")
        End If
        Try

            Dim query = _surgicalProcedureServiceRepository.GetByFilter(Function(x) x.IPSServiceParentId = parentId _
                                                                                    AndAlso (classService = 0 OrElse x.IPSService1.ServiceClass = classService), False, {"IPSService1"})
            If Not query?.Any() Then
                Return New ActionResult(Of List(Of IPSService)) With {.StateResult = False}
            End If

            Dim lisIpsService = New List(Of IPSService)

            query.ToList().ForEach(Sub(d)
                                       lisIpsService.Add(d.IPSService1)
                                   End Sub)

            Return New ActionResult(Of List(Of IPSService)) With {.StateResult = True, .ObjectEmbbeded = lisIpsService}

        Catch ex As Exception
            Return New ActionResult(Of List(Of IPSService)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "Events"

    Public Sub TriggerEvent(IPSService As IPSService, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = IPSService.ChangeTracker.State.ToString().ToLower()

        Dim eventData As EventData = wrapperEvent.GenerateWrapperEventData(IPSService, audit.CodeUser, ChangeTracker, DittoSourceType.iPSService)
        Dim Queue As IIndigoQueue = _factoryQueue.CreateQueue()
        Queue.Publish(eventData)
    End Sub

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ipsServiceRepository = Nothing
            _secuenseDRepository = Nothing
            _surgicalProcedureServiceRepository = Nothing
            _cupsHomologationRepository = Nothing
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

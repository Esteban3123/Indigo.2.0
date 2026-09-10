'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Configuration
Imports System.Data.Entity.Core
Imports System.Threading.Tasks
Imports System.Transactions
Imports Application.Base
Imports Application.EventHandlers.Security
Imports Application.EventHandlers.Security.Entities
Imports Application.Events.Models.InvoiceEntityCapitated
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Queue
Imports Newtonsoft.Json

Public Class InvoiceEntityCapitatedAdminService
    Implements IInvoiceEntityCapitatedAdminService

#Region "Fields"

    ''' <summary>
    ''' repositorios
    ''' </summary>
    Private _invoiceEntityCapitatedRepository As IInvoiceEntityCapitedRepository
    Private _outBoxRepository As IOutBoxRepository
    Private _factoryQueue As IFactoryQueue


#End Region

#Region "Builders"

    Public Sub New(ByVal invoiceEntityCapitatedRepository As IInvoiceEntityCapitedRepository,
                    ByVal outBoxRepository As IOutBoxRepository,
                    ByVal factoryQueue As IFactoryQueue)
        Me._invoiceEntityCapitatedRepository = invoiceEntityCapitatedRepository
        Me._outBoxRepository = outBoxRepository
        Me._factoryQueue = factoryQueue
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetInvoiceEntityCapitatedById(Id As Integer) As InvoiceEntityCapitated Implements IInvoiceEntityCapitatedAdminService.GetInvoiceEntityCapitatedById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._invoiceEntityCapitatedRepository.GetInvoiceEntityCapitatedById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una factura de entidad capitada por codigo
    ''' </summary>
    Public Function GetInvoiceEntityCapitated(code As String, audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitated) Implements IInvoiceEntityCapitatedAdminService.GetInvoiceEntityCapitated
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim invoiceEntityCapitated As InvoiceEntityCapitated = Me._invoiceEntityCapitatedRepository.GetInvoiceEntityCapitated(code.Trim())
            If invoiceEntityCapitated IsNot Nothing AndAlso invoiceEntityCapitated.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of InvoiceEntityCapitated)(invoiceEntityCapitated, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of InvoiceEntityCapitated) With {.StateResult = True, .ObjectEmbbeded = invoiceEntityCapitated}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InvoiceEntityCapitated) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Saves the invoice entity capitated.
    ''' </summary>
    ''' <param name="invoiceEntityCapitated">The invoice entity capitated.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">invoiceEntityCapitated</exception>
    Public Async Function SaveInvoiceEntityCapitatedAsync(invoiceEntityCapitated As InvoiceEntityCapitated, session As SessionValues) As Task(Of ActionResult(Of InvoiceEntityCapitated)) Implements IInvoiceEntityCapitatedAdminService.SaveInvoiceEntityCapitatedAsync
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Dim actionResult = New ActionResult(Of InvoiceEntityCapitated)()
        Dim eventData As EventData
        Dim unitOfWork As IUnitWork = Me._outBoxRepository.UnitWork

        Dim taskFlagEvent = Await Task.Run(Function()
                                               Return ValidateEventConfiguration(DittoSourceType.InvoiceCapitated.ToString(), session.AuditMessageWcf.Company)
                                           End Function)

        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled)
            Try
                Dim invoiceEntityCapitatedXml As String = invoiceEntityCapitated.ToXML(session)
                Dim auditStatus = CType(Utils.GetAuditStatus(invoiceEntityCapitated.Id, invoiceEntityCapitated.Status), Infrastructure.CrossCutting.Audit.Actions)
                Dim changeTracker As ObjectState = invoiceEntityCapitated.ChangeTracker.State

                Dim result = Me._invoiceEntityCapitatedRepository.SP_SaveInvoiceEntityCapitated(invoiceEntityCapitatedXml, session.AuditMessageWcf.CodeUser)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of InvoiceEntityCapitated) With {.StateResult = False, .Message = result.MessageResult}
                End If

                invoiceEntityCapitated.Id = result.Id
                invoiceEntityCapitated.Code = result.Code

                Dim auditProcess As New IndigoAuditSimpleEntity(Of Domain.Entities.InvoiceEntityCapitated)(invoiceEntityCapitated, session.AuditMessageWcf, auditStatus, invoiceEntityCapitated.OriginalValue)
                auditProcess.Execute()
                invoiceEntityCapitated.MarkAsUnchanged()

                actionResult = New ActionResult(Of InvoiceEntityCapitated) With {.StateResult = True, .ObjectEmbbeded = invoiceEntityCapitated, .Message = result.MessageResult}

                If invoiceEntityCapitated.Status <> 5 OrElse Not taskFlagEvent Then
                    scope.Complete()
                    Return actionResult
                End If

                If changeTracker = ObjectState.Added Then
                    invoiceEntityCapitated.CreationDate = DateTime.Now()
                    invoiceEntityCapitated.CreationUser = session.AuditMessageWcf.CodeUser
                Else
                    invoiceEntityCapitated.ModificationDate = DateTime.Now()
                    invoiceEntityCapitated.ModificationUser = session.AuditMessageWcf.CodeUser
                End If

                Dim wrapperEvent As New Events.Serializers.Wrapper
                eventData = wrapperEvent.GenerateWrapperEventData(invoiceEntityCapitated, session.AuditMessageWcf.CodeUser, "confirmed", DittoSourceType.InvoiceCapitated)
                Dim outBox = GetOutBoxEntity(eventData)
                _outBoxRepository.SaveEntity(outBox)
                unitOfWork.Commit()
                eventData.OutboxId = outBox.Id
                scope.Complete()

            Catch ex As OptimisticConcurrencyException
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of InvoiceEntityCapitated) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of InvoiceEntityCapitated) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of InvoiceEntityCapitated) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
        TriggerEvent(eventData)
        Return actionResult
    End Function

    ''' <summary>
    ''' Obtiene los valores totales por concepto de recaudo para un periodo de tiempo y grupo de atención establecido.
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="careGroupId">Grupo de atención</param>
    ''' <returns></returns>
    Public Async Function GetCollectionValuesAsync(initialDate As DateTime, finalDate As DateTime, careGroupId As Integer) As Task(Of CollectionValues) Implements IInvoiceEntityCapitatedAdminService.GetCollectionValuesAsync
        Dim collectionValues = New CollectionValues() With {
            .CopaymentAmount = 0.0D,
            .ModeratingFeeAmount = 0.0D,
            .SharedPaymentAmount = 0.0D
        }

        Dim query = Await _invoiceEntityCapitatedRepository.GetCapitationControlRegistry(initialDate, finalDate, careGroupId)

        If query IsNot Nothing AndAlso query.Any() Then
            Dim filteredQuery = From id In query
                                Where {2, 3, 4}.Contains(id.RecoveryFeeType) AndAlso id.SubTotalPatientSalesPrice >= 0

            collectionValues.CopaymentAmount = filteredQuery.Where(Function(x) x.RecoveryFeeType = 3).Sum(Function(x) x.SubTotalPatientSalesPrice)
            collectionValues.ModeratingFeeAmount = filteredQuery.Where(Function(x) x.RecoveryFeeType = 2).Sum(Function(x) x.SubTotalPatientSalesPrice)
            collectionValues.SharedPaymentAmount = filteredQuery.Where(Function(x) x.RecoveryFeeType = 4).Sum(Function(x) x.SubTotalPatientSalesPrice)
        End If

        Return collectionValues
    End Function

    ''' <summary>
    ''' Valida si existe el evento para Factura Capitada
    ''' </summary>
    ''' <param name="eventName"></param>
    ''' <param name="company"></param>
    ''' <returns></returns>
    Private Function ValidateEventConfiguration(eventName As String, company As String) As Boolean

        Dim securityContainer = ConfigurationManager.AppSettings.Get("containerSecurity")
        Dim moduleDctionary As New ModuleDictionary()
        Dim code As String = moduleDctionary.GetModule(eventName)
        Dim objectEvent As EventConfiguration = Nothing

        ''se realiza la consulta a la tabla eventconfiguration
        Using context As New SecurityContext(
                Utils.GetEntityConnectionString(ConfigurationFile.CONX_GENESIS, String.Empty, securityContainer, True)
            )
            objectEvent = (From ec In context.EventsConfiguration.AsNoTracking()
                           Where ec.Container.Code = company And ec.Code = code).SingleOrDefault()
        End Using

        Return If(objectEvent Is Nothing, False, True)
    End Function

    ''' <summary>
    ''' Metodo para Crear la entidad OutBox
    ''' </summary>
    ''' <param name="eventData"></param>
    ''' <returns></returns>
    Private Function GetOutBoxEntity(eventData As EventData) As Outbox

        Dim InvoiceCapitatedEvent As MInvoiceCapitated = TryCast(eventData.data, MInvoiceCapitated)

        If InvoiceCapitatedEvent Is Nothing Then
            Throw New ArgumentNullException(NameOf(InvoiceCapitatedEvent))
        End If

        Dim outBox = New Outbox()
        With outBox
            .OccurredOn = DateTime.Now()
            .EventType = eventData.source
            .AggregateKey = InvoiceCapitatedEvent.Code
            .AggregateType = NameOf(InvoiceEntityCapitated)
            .Payload = JsonConvert.SerializeObject(InvoiceCapitatedEvent, Formatting.None)
            .IsPublished = False
            .Action = eventData.action
        End With
        outBox.StartTracking
        outBox.MarkAsAdded()
        Return outBox
    End Function

    ''' <summary>
    ''' Trigger que dispara el evento
    ''' </summary>
    ''' <param name="eventData"></param>
    Public Sub TriggerEvent(eventData As EventData)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim queue = _factoryQueue.CreateQueue()
        queue.Publish(eventData)
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            _invoiceEntityCapitatedRepository = Nothing

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
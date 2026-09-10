Imports System.Transactions
Imports Application.Security
Imports Domain.AccountManagement.Model
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Queue

Public Class DashboardAccountAssignmentAdminService
    Implements IDashboardAccountAssignmentAdminService

    Private _dashboardAccountAssignmentRepository As IDashboardAccountAssignmentRepository
    Private _factoryQueue As IFactoryQueue

#Region "Builer"
    Public Sub New(dashboardAccountAssignmentRepository As IDashboardAccountAssignmentRepository, factoryQueue As IFactoryQueue)
        If dashboardAccountAssignmentRepository Is Nothing Then
            Throw New ArgumentNullException("dashboardAccountAssignmentRepository vacío")
        End If
        If factoryQueue Is Nothing Then
            Throw New ArgumentNullException("factoryQueue vacío")
        End If
        _dashboardAccountAssignmentRepository = dashboardAccountAssignmentRepository
        _factoryQueue = factoryQueue
    End Sub
#End Region

    ''' <summary>
    ''' Función que obtiene los ingresos no asignados a un facturador
    ''' </summary>
    ''' <param name="careCenter"></param>
    ''' <param name="entryType"></param>
    ''' <returns></returns>
    Public Function GetPendingAssignmentByCareCenterAndEntryType(careCenter As String, entryType As String) As ActionResult(Of List(Of VPendingAssignment)) Implements IDashboardAccountAssignmentAdminService.GetPendingAssignmentByCareCenterAndEntryType
        Try
            Dim res = _dashboardAccountAssignmentRepository.GetPendingAssignmentByCareCenterAndEntryType(careCenter, entryType, 1000)

            Return New ActionResult(Of List(Of VPendingAssignment)) With {.ObjectEmbbeded = res, .StateResult = True, .StatusCode = eStatusResult.SUCCESS}
        Catch ex As Exception
            Return New ActionResult(Of List(Of VPendingAssignment)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Guarda la asignación de los ingresos a un facturador
    ''' </summary>
    ''' <param name="admissionToAssign"></param>
    ''' <returns></returns>
    Public Async Function GenerateManualAssignmentAsync(admissionToAssign As List(Of AutomaticEntryDistribution)) As Task(Of ActionResult(Of List(Of AutomaticEntryDistribution))) Implements IDashboardAccountAssignmentAdminService.GenerateManualAssignmentAsync
        If admissionToAssign Is Nothing Then Throw New ArgumentNullException("AutomaticEntryDistribution")

        Dim unitOfWork As IUnitWork = _dashboardAccountAssignmentRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TimeSpan.FromSeconds(30), .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)
                Dim messageResult As String = String.Empty

                Await _dashboardAccountAssignmentRepository.SaveEntityMassiveAsync(admissionToAssign)
                messageResult = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AssignmentMessage")

                Await unitOfWork.CommitAsync()
                scope.Complete()
                Return New ActionResult(Of List(Of AutomaticEntryDistribution)) With {.ObjectEmbbeded = admissionToAssign, .StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = messageResult}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of List(Of AutomaticEntryDistribution)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Genera la asignación automática de un ingreso a un facturador
    ''' </summary>
    ''' <param name="admissionToAssign"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GenerateAutomaticAssignment(admissionToAssign As List(Of AutomaticDistributionMessage), session As SessionValues) As ActionResult Implements IDashboardAccountAssignmentAdminService.GenerateAutomaticAssignment
        If admissionToAssign Is Nothing Then Throw New ArgumentNullException("AutomaticEntryDistribution")

        Try
            TriggerEvent(admissionToAssign, session)
            Dim messageResult As String = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EntryDistributionMessage")

            Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = messageResult}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Genera la información del evento
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function GenerateWrapperData(data As AutomaticDistributionMessage, session As SessionValues) As EventData
        Return New EventData(data, "AccountDistributionEventFromUI", "added", session.HisContainer, session.AuditMessageWcf.CodeUser, DateTime.Now.GetTimestamp)
    End Function

    ''' <summary>
    ''' Dispara el evento de distribución de ingreso
    ''' </summary>
    ''' <param name="admissionToAssign"></param>
    ''' <param name="session"></param>
    Private Sub TriggerEvent(admissionToAssign As List(Of AutomaticDistributionMessage), session As SessionValues)
        Dim evenData As EventData
        For Each admission In admissionToAssign
            Dim queue As IIndigoQueue = _factoryQueue.CreateQueue()
            evenData = GenerateWrapperData(admission, session)
            queue.Publish(evenData)
        Next
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            _dashboardAccountAssignmentRepository = Nothing
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

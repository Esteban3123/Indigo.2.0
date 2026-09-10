#Region "Imports"
Imports System.ServiceModel
Imports Domain.AccountManagement.Model
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

<ServiceContract()>
Public Interface IAccountManagementDashboardAccountAssignment
    ''' <summary>
    ''' Retorna los ingresos pendientes por asignar a un facturador
    ''' </summary>
    ''' <param name="careCenter"></param>
    ''' <param name="entryType"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPendingAssignmentByCareCenterAndEntryType(ByVal careCenter As String, ByVal entryType As String) As ActionResult(Of List(Of VPendingAssignment))
    ''' <summary>
    ''' Genera la asignación manual de los pacientes
    ''' </summary>
    ''' <param name="patientsToAssign"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GenerateManualAssignmentAsync(patientsToAssign As List(Of Domain.Entities.AutomaticEntryDistribution)) As Task(Of ActionResult(Of List(Of Domain.Entities.AutomaticEntryDistribution)))
    ''' <summary>
    ''' Genera la asignación automática de los pacientes
    ''' </summary>
    ''' <param name="patientsToAssign"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GenerateAutomaticAssignment(patientsToAssign As List(Of AutomaticDistributionMessage), session As SessionValues) As ActionResult
End Interface

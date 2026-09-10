'************************************************************
' Assembly         : Domain.Entities
' Author           : Andrés Steven Rojas
' Created          : 05-05-2025
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports Domain.AccountManagement.Model
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
Public Interface IDashboardAccountAssignmentAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Obtiene los ingresos pendientes por asignar a un facturador
    ''' </summary>
    ''' <param name="careCenter"></param>
    ''' <param name="entryType"></param>
    ''' <returns></returns>
    Function GetPendingAssignmentByCareCenterAndEntryType(careCenter As String, entryType As String) As ActionResult(Of List(Of VPendingAssignment))
    ''' <summary>
    ''' Función que genera la asignación manual de los pacientes
    ''' </summary>
    ''' <param name="admissionToAssign"></param>
    ''' <returns></returns>
    Function GenerateManualAssignmentAsync(admissionToAssign As List(Of AutomaticEntryDistribution)) As Task(Of ActionResult(Of List(Of AutomaticEntryDistribution)))
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Function GenerateAutomaticAssignment(admissionToAssign As List(Of AutomaticDistributionMessage), session As SessionValues) As ActionResult
End Interface

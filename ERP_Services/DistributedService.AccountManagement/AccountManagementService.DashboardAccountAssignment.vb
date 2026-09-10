'************************************************************
' Assembly         : Distribuited Services
' Author           : Andrés Steven Rojas
' Created          : 05-05-2025
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports Domain.AccountManagement.Model
Imports Application.AccountManagement
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Infrastructure.CrossCutting.Base
#End Region

Partial Class AccountManagementService
    Implements IAccountManagementDashboardAccountAssignment
    ''' <summary>
    ''' Obtiene los ingresos que no han sido asignados a ningún facturador
    ''' </summary>
    ''' <param name="careCenter"></param>
    ''' <param name="entryType"></param>
    ''' <returns></returns>
    Public Function GetPendingAssignmentByCareCenterAndEntryType(careCenter As String, entryType As String) As ActionResult(Of List(Of VPendingAssignment)) Implements IAccountManagementDashboardAccountAssignment.GetPendingAssignmentByCareCenterAndEntryType
        Using service As IDashboardAccountAssignmentAdminService = Container.Current.Resolve(Of IDashboardAccountAssignmentAdminService)()
            Return service.GetPendingAssignmentByCareCenterAndEntryType(careCenter, entryType)
        End Using
    End Function
    ''' <summary>
    ''' Función que genera o guarda la asignación de los pacientes a un facturador
    ''' </summary>
    ''' <param name="admissionToAssign"></param>
    ''' <returns></returns>
    Public Async Function GenerateManualAssignmentAsync(admissionToAssign As List(Of Domain.Entities.AutomaticEntryDistribution)) As Task(Of ActionResult(Of List(Of Domain.Entities.AutomaticEntryDistribution))) Implements IAccountManagementDashboardAccountAssignment.GenerateManualAssignmentAsync
        Using service As IDashboardAccountAssignmentAdminService = Container.Current.Resolve(Of IDashboardAccountAssignmentAdminService)()
            Return Await service.GenerateManualAssignmentAsync(admissionToAssign)
        End Using
    End Function
    ''' <summary>
    ''' Función que genera la asignación automática de los pacientes a un facturador
    ''' </summary>
    ''' <param name="admissionToAssign"></param>
    ''' <returns></returns>
    Public Function GenerateAutomaticAssignment(admissionToAssign As List(Of AutomaticDistributionMessage), session As SessionValues) As ActionResult Implements IAccountManagementDashboardAccountAssignment.GenerateAutomaticAssignment
        ServerSessionValues.Current.CurrentContainer = session.HisContainer
        Using service As IDashboardAccountAssignmentAdminService = Container.Current.Resolve(Of IDashboardAccountAssignmentAdminService)()
            Return service.GenerateAutomaticAssignment(admissionToAssign, session)
        End Using
    End Function
End Class

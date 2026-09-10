#Region "Imports"
Imports Domain.AccountManagement.Model
Imports Domain.Base
#End Region
Public Interface IDashboardAccountAssignmentRepository
    Inherits IRepository(Of AutomaticEntryDistribution)
    ''' <summary>
    ''' Función que obtiene los pacientes pendientes por asignar
    ''' </summary>
    ''' <returns></returns>
    Function GetPendingAssignmentByCareCenterAndEntryType(ByVal careCenter As String, ByVal entryType As String, maxResults As Integer?) As List(Of VPendingAssignment)


End Interface

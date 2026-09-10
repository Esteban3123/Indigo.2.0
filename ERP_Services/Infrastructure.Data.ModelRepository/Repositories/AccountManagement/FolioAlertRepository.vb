Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class FolioAlertRepository
    Inherits GenericRepository(Of FolioAlert)
    Implements IFolioAlertRepository, Inject

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una alerta por su Id
    ''' </summary>
    ''' <param name="alertId"></param>
    ''' <returns></returns>
    Public Function GetExistingAlert(alertId As Integer) As FolioAlert Implements IFolioAlertRepository.GetExistingAlert
        Return _context.FolioAlert.FirstOrDefault(Function(a) a.Id = alertId)
    End Function

    ''' <summary>
    ''' Obtiene todas las alertas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAlerts() As List(Of FolioAlert) Implements IFolioAlertRepository.GetAllAlerts
        Dim res = _context.FolioAlert.Include("ManagementAreas").ToList()
        Return res
    End Function
End Class

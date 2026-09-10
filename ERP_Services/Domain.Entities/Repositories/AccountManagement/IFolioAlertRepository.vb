Imports Domain.Base

Public Interface IFolioAlertRepository
    Inherits IRepository(Of FolioAlert)

    ''' <summary>
    ''' Obtiene una alerta por su Id
    ''' </summary>
    ''' <param name="alertId"></param>
    ''' <returns></returns>
    Function GetExistingAlert(alertId As Integer) As FolioAlert

    ''' <summary>
    ''' Obtiene todas las alertas
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAlerts() As List(Of FolioAlert)
End Interface

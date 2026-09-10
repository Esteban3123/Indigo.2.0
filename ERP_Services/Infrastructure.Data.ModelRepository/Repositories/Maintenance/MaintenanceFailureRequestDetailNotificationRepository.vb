#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class MaintenanceFailureRequestDetailNotificationRepository
    Inherits GenericRepository(Of MaintenanceFailureRequestDetailNotification)
    Implements IMaintenanceFailureRequestDetailNotificationRepository

#Region "Builder"

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region

End Class

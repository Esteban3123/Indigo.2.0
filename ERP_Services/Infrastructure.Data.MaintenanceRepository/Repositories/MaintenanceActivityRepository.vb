'************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 04-09-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
#End Region

Public Class MaintenanceActivityRepository
    Inherits GenericRepository(Of MaintenanceActivity)
    Implements IMaintenanceActivityRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IMaintenanceModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
End Class

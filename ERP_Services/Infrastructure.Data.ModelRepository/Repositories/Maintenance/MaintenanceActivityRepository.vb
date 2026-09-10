'************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 04-09-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class MaintenanceActivityRepository
    Inherits GenericRepository(Of MaintenanceActivity)
    Implements IMaintenanceActivityRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
End Class

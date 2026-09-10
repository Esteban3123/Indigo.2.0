'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la historia del equipo
''' </summary>
''' <remarks></remarks>

Public Class EquipmentHistoryRepository
    Inherits GenericRepository(Of EquipmentHistory)
    Implements IEquipmentHistoryRepository

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

   
    Public Function ListAllEquipmentHistory() As List(Of EquipmentHistory) Implements IEquipmentHistoryRepository.ListAllEquipmentHistory
        Dim Busqueda = From e In _context.EquipmentHistory
                       Where e.State = True
                Select e

        Return Busqueda.ToList
    End Function
End Class

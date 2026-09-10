'************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 27-08-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
#End Region

Public Class EquipmentTypePartsAccesoriesConsumiblesRepository

    Inherits GenericRepository(Of Domain.Maintenance.Entities.EquipmentTypePartsAccesoriesConsumibles)
    Implements IEquipmentTypePartsAccesoriesConsumablesRepository

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

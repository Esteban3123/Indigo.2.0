'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 25-03-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance

#End Region


Public Class EquipmentTypeTechnicalLogRepository
    Inherits GenericRepository(Of Domain.Maintenance.Entities.EquipmentTypeTechnicalLog)
    Implements IEquipmentTypeTechnicalLogRepository

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

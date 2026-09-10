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
''' clase para hacer todas las operaciones de persistencia para la entidad tipos de ubicacion
''' </summary>
''' <remarks></remarks>

Public Class LocationTypeRepository
    Inherits GenericRepository(Of LocationType)
    Implements ILocationTypeRepository




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

    
    Public Function ListAllLocationType() As List(Of LocationType) Implements ILocationTypeRepository.ListAllLocationType
        Dim Busqueda = From e In _context.LocationType
                   Where e.State = True
                    Select e

        Return Busqueda.ToList
    End Function
End Class

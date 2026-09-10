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
''' clase para hacer todas las operaciones de persistencia para la ubicacion
''' </summary>
''' <remarks></remarks>

Public Class LocationRepository
    Inherits GenericRepository(Of Location)
    Implements ILocationRespository





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


    Public Function GetLocation(codeLocation As String) As Location Implements ILocationRespository.GetLocation
        Dim Busqueda = From e In _context.Location
                 Where e.Code = codeLocation
                 Select e
        If Busqueda.Count = 0 Then
            Return New Location
        Else
            Return Busqueda.Single
        End If
    End Function

    Public Function ListAllLocation() As List(Of Location) Implements ILocationRespository.ListAllLocation
        Dim Busqueda = From e In _context.Location
                Select e

        Return Busqueda.ToList
    End Function

    Public Function ListLocation() As List(Of Location) Implements ILocationRespository.ListLocation
        Dim Busqueda = From e In _context.Location
                       Select e


        Return Busqueda.ToList
    End Function

    Public Function SaveLocation(Location As Location) As Boolean Implements ILocationRespository.SaveLocation
        _context.Location.ApplyChanges(Location)
        Return True
    End Function
End Class

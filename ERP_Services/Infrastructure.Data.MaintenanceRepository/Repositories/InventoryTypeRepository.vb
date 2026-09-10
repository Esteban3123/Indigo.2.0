'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 08-08-2013
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
''' clase para hacer todas las operaciones de persistencia para la entidad aseguradora
''' </summary>
''' <remarks></remarks>
Public Class InventoryTypeRepository
    Inherits GenericRepository(Of InventoryType)
    Implements IInvetoryTypeRepository



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

  
    Public Function GetInventoryType(codeInventarioType As String, Optional Tracking As Boolean = True) As InventoryType Implements IInvetoryTypeRepository.GetInventoryType
        If Tracking Then
            Dim Busqueda = From e In _context.InventoryType
                       Where e.Code = codeInventarioType
                       Select e


            If Busqueda.Count = 0 Then
                Return New InventoryType
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.InventoryType.AsNoTracking
                       Where e.Code = codeInventarioType
                       Select e


            If Busqueda.Count = 0 Then
                Return New InventoryType
            Else
                Return Busqueda.Single
            End If
        End If
        
    End Function

    Public Function ListAllInventoryType() As List(Of InventoryType) Implements IInvetoryTypeRepository.ListAllInventoryType
        Dim Busqueda = From e In _context.InventoryType
                       Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveInventoryType(InventoryType As InventoryType) As Boolean Implements IInvetoryTypeRepository.SaveInventoryType
        _context.InventoryType.ApplyChanges(InventoryType)
        Return True
    End Function
End Class

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
''' clase para hacer todas las operaciones de persistencia para la entidad consumibles
''' </summary>
''' <remarks></remarks>

Public Class ConsumableRepository
    Inherits GenericRepository(Of Consumable)
    Implements IConsumableRepository






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

  

    Public Function GetConsumable(codeConsumable As String, Optional Tracking As Boolean = False) As Consumable Implements IConsumableRepository.GetConsumable
        If Tracking = False Then
            Dim Busqueda = From e In _context.Consumable.Include("ConsumableDetail.EquipmentType")
                    Where e.Code = codeConsumable
                    Select e

            If Busqueda.Count = 0 Then
                Return New Consumable
            Else
                Busqueda.Single.StartTracking()
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.Consumable.Include("ConsumableDetail.EquipmentType").AsNoTracking
                    Where e.Code = codeConsumable
                    Select e

            If Busqueda.Count = 0 Then
                Return New Consumable
            Else
                Busqueda.Single.StartTracking()
                Return Busqueda.Single
            End If
        End If
        

    End Function

    Public Function ListAllConsumable() As List(Of Consumable) Implements IConsumableRepository.ListAllConsumable

        Dim Busqueda = From e In _context.Consumable
                      Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveConsumable(Consumable As Consumable) As Boolean Implements IConsumableRepository.SaveConsumable
        _context.Consumable.ApplyChanges(Consumable)
        Return True
    End Function
End Class

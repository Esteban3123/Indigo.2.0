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
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>

Public Class AccesoryRepository
    Inherits GenericRepository(Of Accessory)
    Implements IAccesoryRepository

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


    Public Function GetAccessory(codeAccessory As String, Optional Tracking As Boolean = False) As Accessory Implements IAccesoryRepository.GetAccessory
        If Tracking = False Then
            Dim Busqueda = From e In _context.Accessory.Include("AccesoryDetail.EquipmentType")
                   Where e.Code = codeAccessory
                   Select e
            If Busqueda.Count = 0 Then
                Return New Accessory
            Else
                Busqueda.Single.StartTracking()
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.Accessory.Include("AccesoryDetail.EquipmentType").AsNoTracking
                   Where e.Code = codeAccessory
                   Select e
            If Busqueda.Count = 0 Then
                Return New Accessory
            Else
                Busqueda.Single.StartTracking()
                Return Busqueda.Single
            End If
        End If
        
    End Function

    Public Function ListAllAccessory() As List(Of Accessory) Implements IAccesoryRepository.ListAllAccessory
        Dim Busqueda = From e In _context.Accessory
                   Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveAccessory(Accessory As Accessory) As Boolean Implements IAccesoryRepository.SaveAccessory
        _context.Accessory.ApplyChanges(Accessory)
        Return True
    End Function

    Public Function ListEquipmentType() As List(Of EquipmentType) Implements IAccesoryRepository.ListEquipmentType
        Dim Busqueda = From e In _context.EquipmentType
                        Where e.State = True
                        Select e


        Return Busqueda.ToList
    End Function
End Class

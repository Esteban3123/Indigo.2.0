
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

Public Class EquipmentRegistrationRepository
    Inherits GenericRepository(Of EquipmentRegistration)
    Implements IEquipmentRegistration

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


    ''' <summary>
    ''' funcion para listar el registro de equipor po codigo de la placa
    ''' </summary>
    ''' <param name="codeEquipmentRegistration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEquipmentRegistration(codeEquipmentRegistration As String) As EquipmentRegistration Implements IEquipmentRegistration.GetEquipmentRegistration
        Dim Busqueda = From e In _context.EquipmentRegistration.
                           Include("EquipmentReceptionAccesory.AccesoryDetail.Accessory").
                           Include("EquipmentReceptionConsumable.ConsumableDetail.Consumable").
                           Include("EquipmentReceptionParte.PartDetail.Part").
                           Include("TechnicalLogDetail.TechnicalLog").
                           Include("DrawingsDetail").
                           Include("TechnicalEquipmentSheet").
                           Include("ManualDetail").
                           Include("Location").
                           Include("EquipmentReceptionPartsAccesoriesConsumables").
                           Include("EquipmentReceptionPartsAccesoriesConsumables.PartsAccesoriesConsumables").
                           Include("EquipmentType.EquipmentTypeTechnicalLog").
                           Include("EquipmentType.EquipmentTypeTechnicalLog.TechnicalLog")
                       Where e.Consecutive = codeEquipmentRegistration
                       Select e

        If Busqueda.Count = 0 Then
            Return New EquipmentRegistration
        Else
            Return Busqueda.Single
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllEquipmentRegistration() As List(Of EquipmentRegistration) Implements IEquipmentRegistration.ListAllEquipmentRegistration
        Dim Busqueda = From e In _context.EquipmentRegistration
               Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="EquipmentRegistration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEquipmentRegistration(EquipmentRegistration As EquipmentRegistration) As Boolean Implements IEquipmentRegistration.SaveEquipmentRegistration
        _context.EquipmentRegistration.ApplyChanges(EquipmentRegistration)
        Return True
    End Function



    Public Function ListPartEquipmentType(Type As Integer) As List(Of PartDetail) Implements IEquipmentRegistration.ListPartEquipmentType
        Dim Busqueda = From e In _context.PartDetail.Include("Part")
                       Where e.Part.State = True And e.IdEquipmentType = Type
                 Select e

        Return Busqueda.ToList
    End Function



    Public Function ListAccesoryEquipmentType(Type As Integer) As List(Of AccesoryDetail) Implements IEquipmentRegistration.ListAccesoryEquipmentType
        Dim Busqueda = From e In _context.AccesoryDetail.Include("Accessory")
                      Where e.Accessory.State = True And e.IdEquipmentType = Type
                Select e
        Return Busqueda.ToList
    End Function



    Public Function ListConsumableEquipmentType(Type As Integer) As List(Of ConsumableDetail) Implements IEquipmentRegistration.ListConsumableEquipmentType
        Dim Busqueda = From e In _context.ConsumableDetail.Include("Consumable")
                   Where e.Consumable.State = True And e.IdEquipmentType = Type
             Select e
        Return Busqueda.ToList
    End Function


End Class

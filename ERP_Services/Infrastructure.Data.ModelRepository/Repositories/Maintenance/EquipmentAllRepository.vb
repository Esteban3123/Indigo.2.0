'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
#End Region

''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Class EquipmentAllRepository
    Inherits GenericRepository(Of Equipment)
    Implements IEquipmentAllRepository


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

    Public Function GetEquipment(codeEquipment As String, Optional Tracking As Boolean = False) As Equipment Implements IEquipmentAllRepository.GetEquipment
        If Tracking = False Then
            Dim Busqueda = From e In _context.Equipment.Include("TechnicalEquipmentSheet")
                          Where e.Code = codeEquipment
                          Select e
            If Busqueda.Count = 0 Then
                Return New Equipment
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.Equipment.AsNoTracking
              Where e.Code = codeEquipment
              Select e
            If Busqueda.Count = 0 Then
                Return New Equipment
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllEquipment() As List(Of Equipment) Implements IEquipmentAllRepository.ListAllEquipment
        Dim Busqueda = From e In _context.Equipment
               Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveEquipment(Equipment As Equipment) As Boolean Implements IEquipmentAllRepository.SaveEquipment
        _context.Equipment.ApplyChanges(Equipment)
        Return True
    End Function
End Class

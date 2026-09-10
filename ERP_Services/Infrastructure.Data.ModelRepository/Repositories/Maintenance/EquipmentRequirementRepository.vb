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

Public Class EquipmentRequirementRepository
    Inherits GenericRepository(Of EquipmentRequirement)
    Implements IEquipmentRequirementRepository

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

    Public Function ListAllEquipmentRequirement() As List(Of EquipmentRequirement) Implements IEquipmentRequirementRepository.ListAllEquipmentRequirement
        Dim Busqueda = From e In _context.EquipmentRequirement
                       Where e.State = True
                       Select e

        Return Busqueda.ToList
    End Function

End Class

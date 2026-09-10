'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region


Public Interface IEquipmentTypeRepository
    Inherits IRepository(Of EquipmentType)


    ''' <summary>
    ''' funcion que lista todas los tipos de equipo
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllEquipmentType() As List(Of EquipmentType)
    ''' <summary>
    ''' consulta para retornar el tipo de equipo teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeEquipmentType">el codigo del tipo de equipo</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetEquipmentType(ByVal codeEquipmentType As String) As EquipmentType

    ''' <summary>
    ''' funcion para almacenar el tipo de equipo
    ''' </summary>
    ''' <param name="EquipmentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveEquipmentType(EquipmentType As EquipmentType) As Boolean



    ''' <summary>
    ''' listar los tipos de equipos dependiendo del tipo de inventario seleccionado
    ''' </summary>
    ''' <param name="IdInventoryType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListEquipmentTypeInventoryType(IdInventoryType As Integer) As List(Of EquipmentType)

    Function GetEquipmentTypeById(IdEquipmentType As Integer) As EquipmentType
End Interface

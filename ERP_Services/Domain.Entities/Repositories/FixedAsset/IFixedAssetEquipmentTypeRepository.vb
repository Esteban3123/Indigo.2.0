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
Imports Domain.Entities
#End Region

Public Interface IFixedAssetItemTypeRepository
    Inherits IRepository(Of FixedAssetItemType)


    ''' <summary>
    ''' funcion que lista todas los tipos de equipo
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllEquipmentType() As List(Of FixedAssetItemType)
    ''' <summary>
    ''' consulta para retornar el tipo de equipo teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeEquipmentType">el codigo del tipo de equipo</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetEquipmentType(ByVal codeEquipmentType As String) As FixedAssetItemType

    ''' <summary>
    ''' funcion para almacenar el tipo de equipo
    ''' </summary>
    ''' <param name="EquipmentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveEquipmentType(EquipmentType As FixedAssetItemType) As Boolean



    ''' <summary>
    ''' listar los tipos de equipos dependiendo del tipo de inventario seleccionado
    ''' </summary>
    ''' <param name="IdInventoryType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListEquipmentTypeInventoryType(IdInventoryType As Integer) As List(Of FixedAssetItemType)

    Function GetEquipmentTypeById(IdEquipmentType As Integer) As FixedAssetItemType
End Interface

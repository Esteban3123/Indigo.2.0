
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
Imports Domain.Entities
#End Region



''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>
Public Interface IEquipmentRegistration
    Inherits IRepository(Of EquipmentRegistration)

    ''' <summary>
    ''' funcion que lista todas las registroes de equipo
    ''' </summary>
    ''' <returns>Lista de areas</returns>
    Function ListAllEquipmentRegistration() As List(Of EquipmentRegistration)
    ''' <summary>
    ''' consulta para retornar registro de equipo
    ''' </summary>
    ''' <param name="codeEquipmentRegistration">el codigo del registro de equipo</param>
    ''' <returns>Objeto area</returns>
    Function GetEquipmentRegistration(plate As String) As EquipmentRegistration



    ''' <summary>
    ''' funcion para almacenar un registro de equipo
    ''' </summary>
    ''' <param name="EquipmentRegistration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveEquipmentRegistration(EquipmentRegistration As EquipmentRegistration) As Boolean

    ''' <summary>
    ''' funcion paranlistar los accesorios de un equipo 
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAccesoryEquipmentType(Type As Integer) As List(Of AccesoryDetail)


    ''' <summary>
    ''' funcion paranlistar los consumibles de un equipo 
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsumableEquipmentType(Type As Integer) As List(Of ConsumableDetail)
    Function ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration(fixedAssetPhysicalAssetId As Integer, equipmentRegistration As Integer) As List(Of TechnicalLogDetail)
End Interface

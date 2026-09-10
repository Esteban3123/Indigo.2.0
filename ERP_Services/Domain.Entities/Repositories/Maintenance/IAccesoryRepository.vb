
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



''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>
Public Interface IAccesoryRepository
    Inherits IRepository(Of Accessory)

    ''' <summary>
    ''' funcion que lista todas los accesorios
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllAccessory() As List(Of Accessory)
    ''' <summary>
    ''' consulta para retornar un accesorio teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeAccessory">el codigo de la sucursal</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetAccessory(ByVal codeAccessory As String, Optional Tracking As Boolean = False) As Accessory

    ''' <summary>
    ''' funcion para almacenar un accesorio
    ''' </summary>
    ''' <param name="Accessory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAccessory(Accessory As Accessory) As Boolean

    ''' <summary>
    ''' funcion para almacenar todos los tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListEquipmentType() As List(Of Domain.Entities.FixedAssetEquipmentType)
End Interface

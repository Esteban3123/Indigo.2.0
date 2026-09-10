
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

''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>

Public Interface IFixedAssetItemAllRepository
    Inherits IRepository(Of FixedAssetItem)

    ''' <summary>
    ''' funcion que lista todas los equipos
    ''' </summary>
    ''' <returns>Lista de areas</returns>  
    Function ListAllFixedAssetItem() As List(Of FixedAssetItem)
    ''' <summary>
    ''' consulta para retornar un equipo
    ''' </summary>
    ''' <param name="codeEquipment">el codigo del equipo</param>
    ''' <returns>Objeto area</returns>
    Function GetFixedAssetItem(ByVal codeEquipment As String, Optional Tracking As Boolean = False) As FixedAssetItem

    ''' <summary>
    ''' funcion para almacenar un equipo
    ''' </summary>
    ''' <param name="Equipment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetItem(Equipment As FixedAssetItem) As Boolean

    ''' <summary>
    ''' Metodo que consulta cuantos libros oficiales hay
    ''' para poder validar con la cantidad de libros que
    ''' se agregan en el form de articulos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CountQuantityLegalBook() As Integer


End Interface

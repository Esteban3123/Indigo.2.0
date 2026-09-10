'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Jeisson Herrera Peña
' Created          : 24/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetLocationRepository
    Inherits IRepository(Of FixedAssetLocation)
    ''' <summary>
    ''' funcion que lista todas los ubicaciones
    ''' </summary>
    ''' <returns>Lista de ubicaciones</returns>
    Function ListAllLocation() As List(Of FixedAssetLocation)
    ''' <summary>
    ''' consulta para retornar una ubicacion teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeLocation">el codigo de la ubicacion</param>
    ''' <returns>Objeto ubicacion</returns>
    Function GetLocation(ByVal codeLocation As String) As FixedAssetLocation

    ''' <summary>
    ''' funcion para almacenar una ubicacion
    ''' </summary>
    ''' <param name="Location"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveLocation(Location As FixedAssetLocation) As Boolean

    ''' <summary>
    ''' funcion para almacenar todos los tipos de ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListLocation() As List(Of FixedAssetLocation)

End Interface

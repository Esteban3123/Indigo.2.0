'************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Daniel Eduardo Arévalo
' Created          : 09-06-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
#End Region

Public Interface IFixedAssetStatusRepository

    Inherits IRepository(Of FixedAssetStatusAsset)

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllFixedAssetStatus() As List(Of FixedAssetStatusAsset)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetFixedAssetStatusByCode(Code As String) As FixedAssetStatusAsset

End Interface

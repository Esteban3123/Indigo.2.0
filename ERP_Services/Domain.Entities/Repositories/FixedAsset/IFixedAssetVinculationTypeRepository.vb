'************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Daniel Eduardo Arévalo
' Created          : 04-02-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
#End Region

Public Interface IFixedAssetVinculationTypeRepository

    Inherits IRepository(Of FixedAssetVinculationType)

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllVinculationType() As List(Of FixedAssetVinculationType)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de Tipo de Vinculación</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetVinculationTypeByCode(Code As String) As FixedAssetVinculationType
End Interface

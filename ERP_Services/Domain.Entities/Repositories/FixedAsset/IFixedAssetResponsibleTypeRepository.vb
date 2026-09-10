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

Public Interface IFixedAssetResponsibleTypeRepository

    Inherits IRepository(Of ResponsibleType)

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllResponsibleType() As List(Of ResponsibleType)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetResponsibleTypeByCode(Code As String) As ResponsibleType
End Interface

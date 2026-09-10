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

Public Interface IFixedAssetResponsibleRepository

    Inherits IRepository(Of FixedAssetResponsible)

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllResponsible() As List(Of FixedAssetResponsible)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetResponsibleByCode(Code As String) As FixedAssetResponsible

    Function GetFuncionalUnitByCode(Code As String) As FunctionalUnit

    Function GetFuncionalUnitByIdUser(IdResponsible As Integer) As List(Of ResponsibleFunctionalUnit)
End Interface

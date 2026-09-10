'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/05/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetRetirementTypesRepository
    Inherits IRepository(Of FixedAssetRetirementTypes)

    ''' <summary>
    ''' Obtiene todos los Tipos de baja de activos fijos
    ''' </summary>
    ''' <returns>Lista de los Tipos de baja de activos fijos</returns>
    ''' <remarks></remarks>
    Function GetAllFixedAssetRetirementTypes() As List(Of FixedAssetRetirementTypes)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetRetirementTypesById(Id As Integer, Optional tracking As Boolean = True) As FixedAssetRetirementTypes

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetRetirementTypesByCode(code As String, Optional tracking As Boolean = True) As FixedAssetRetirementTypes

End Interface

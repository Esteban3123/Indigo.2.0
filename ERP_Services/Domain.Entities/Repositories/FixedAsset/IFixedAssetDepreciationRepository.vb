'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/06/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetDepreciationRepository
    Inherits IRepository(Of FixedAssetDepreciation)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetDepreciation(code As String) As FixedAssetDepreciation

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetDepreciationById(Id As Integer) As FixedAssetDepreciation

    ''' <summary>
    ''' Genera y guarda la depreciación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveDepreciation(depreciateMonth As Integer, depreciateYear As Integer, codeUser As String, operatingUnitId As Integer, ModeConfirm As Boolean) As SP_SaveDepreciation_Result

End Interface

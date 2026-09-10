'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/06/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetChangePlateRepository
    Inherits IRepository(Of FixedAssetChangePlate)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetChangePlate(code As String) As FixedAssetChangePlate

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetChangePlateById(Id As Integer) As FixedAssetChangePlate

    ''' <summary>
    ''' Valida si ya existe una placa
    ''' </summary>
    ''' <param name="Plate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalByPlate(Plate As String) As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Obtiene el activo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalById(Id As Integer) As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Sp para cambiar la placa en los procesos
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_FixedAssetChangePlate(Xml As String, CodeUser As String) As List(Of SP_FixedAssetChangePlate_Result)

End Interface

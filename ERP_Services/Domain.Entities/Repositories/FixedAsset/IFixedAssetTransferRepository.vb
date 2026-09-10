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

Public Interface IFixedAssetTransferRepository
    Inherits IRepository(Of FixedAssetTransfer)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetTransfer(code As String) As FixedAssetTransfer

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetTransferById(Id As Integer) As FixedAssetTransfer

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListFixedAssetPhysicalAsset(ListInteger As List(Of Integer), Optional tracking As Boolean = True) As List(Of FixedAssetPhysicalAsset)

    ''' <summary>
    ''' Obtiene el id del tercero por el id del responsable
    ''' </summary>
    ''' <param name="ResponsibleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyIdByResponsibleId(ResponsibleId As Integer) As Integer

    ''' <summary>
    ''' Obtiene la clase de la localización
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetClassOfLocation(Id As Integer) As Integer

End Interface

'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/05/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetPhysicalAssetRepository
    Inherits IRepository(Of FixedAssetPhysicalAsset)

    ''' <summary>
    ''' Obtiene un activo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Obtiene un activo por placa
    ''' </summary>
    ''' <param name="Plate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetPhysicalAssetByPlate(Plate As String) As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Obtenemos el ultimo activo relacionado a un articulo
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <returns></returns>
    Function GetLastFixedAssetPhysicalAssetByItem(ItemId As Integer) As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Confirma la finalización de contratos de leasing
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConfirmLeasingContractsFinalization(Xml As String, OperatingUnitId As Integer, Year As Integer, Month As Integer, CompanyNit As String, CodeUser As String) As SP_ConfirmLeasingContractsFinalization_Result

End Interface

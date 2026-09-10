'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/05/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetActiveOutputRepository
    Inherits IRepository(Of FixedAssetActiveOutput)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetActiveOutput(code As String) As FixedAssetActiveOutput

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetActiveOutputById(Id As Integer) As FixedAssetActiveOutput

    ''' <summary>
    ''' Obtiene el activo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetPhysicalAssetById(Id As Integer, Optional IsTracking As Boolean = False) As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Confirma una salida de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveFixedAssetActiveOutput(FixedAssetActiveOutputXml As String, codeUser As String) As SP_ConfirmFixedAssetActiveOutput_Result

    ''' <summary>
    ''' Obtiene los parametros de contabilidad general por id de la unidad operativa
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    Function GetSettingGeneralLedgerByOperatingUnitId(OperatingUnitId As Integer) As GeneralLedgerSettings

    ''' <summary>
    ''' Obtiene los ids de los libros de los cuales tengan permiso para realizar las salidas
    ''' </summary>
    ''' <returns></returns>
    Function GetListLegalBookIds() As List(Of Integer)

    ''' <summary>
    ''' Metodo que valida que los articulos que se van dar de salida no esten en otra salida confirmada
    ''' </summary>
    ''' <param name="ListDetail"></param>
    ''' <returns></returns>
    Function ValidateItemInOutput(ListDetail As List(Of FixedAssetActiveOutputDetail)) As String

End Interface

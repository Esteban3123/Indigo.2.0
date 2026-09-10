'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/08/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetEntryDevolutionRepository
    Inherits IRepository(Of FixedAssetEntryDevolution)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntryDevolution(code As String) As FixedAssetEntryDevolution

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntryDevolutionById(Id As Integer) As FixedAssetEntryDevolution

    ''' <summary>
    ''' Guarda la devolución
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveFixedAssetEntryDevolution(XmlObject As String, codeUser As String) As SP_SaveFixedAssetDevolution_Result

    ''' <summary>
    ''' Obtiene un ingreso de activo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntryById(Id As Integer) As FixedAssetEntry

    ''' <summary>
    ''' Obtiene la cuenta por pagar asociada al ingreso de activos
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableById(Id As Integer) As List(Of AccountPayable)

    ''' <summary>
    ''' Obtiene el articulo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntryItemDetailById(Id As Integer) As FixedAssetEntryItemDetail


    ''' <summary>
    '''  Obtiene una lista de elementos de entrada de activo fijo (FixedAssetEntryItem) asociados a una devolución específica.
    '''  se obtiene solo elementos únicos (sin duplicados).
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetFixedAssetEntryItemsForDevolution(Id As Integer) As List(Of FixedAssetEntryItem)


End Interface

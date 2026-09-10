'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Angi Camila Duran Vargas
' Created          : 27-07-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IProductInTransitRepository
    Inherits IRepository(Of ProductInTransit)

    Function ListProductInTransitMassiveConfirm(listDocuments As List(Of String)) As List(Of ProductInTransit)

    ''' <summary>
    ''' obtiene  un producto en transito codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductInTransitByCode(code As String) As ProductInTransit
    ''' <summary>
    ''' obtiene un producto en transito  por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductInTransitById(id As Integer) As ProductInTransit
    ''' <summary>
    ''' obtiene parametros pet
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPETDefaultSettings() As PETDefaultSettings

    '''' <summary>
    '''' Genera el comprobante contable para remision de entrada
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Function SP_GenerateJournalVoucherByProductInTransit(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByProductInTransit_Result

End Interface

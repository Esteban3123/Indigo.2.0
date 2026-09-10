'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IRemissionEntranceRepository
    Inherits IRepository(Of RemissionEntrance)
    Inherits IRepositoryRollbackStrategy

    Function ListRemissionEntranceMassiveConfirm(listDocuments As List(Of String)) As List(Of RemissionEntrance)

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionEntranceByCode(code As String) As RemissionEntrance
    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionEntranceById(id As Integer) As RemissionEntrance

    ''' <summary>
    ''' Genera el comprobante contable para remision de entrada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_GenerateJournalVoucherByRemissionEntrance(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByRemissionEntrance_Result

End Interface

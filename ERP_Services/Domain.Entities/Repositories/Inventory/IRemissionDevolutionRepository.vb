'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IRemissionDevolutionRepository
    Inherits IRepository(Of RemissionDevolution)
    Inherits IRepositoryRollbackStrategy

    Function ListRemissionDevolutionMassiveConfirm(listDocuments As List(Of String)) As List(Of RemissionDevolution)

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionDevolutionByCode(code As String) As RemissionDevolution

    ''' <summary>
    ''' Genera el comprobante contable para la devolución de la remision
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_GenerateJournalVoucherByRemissionDevolution(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByRemissionDevolution_Result
End Interface

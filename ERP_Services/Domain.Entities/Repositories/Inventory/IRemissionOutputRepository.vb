'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IRemissionOutputRepository
    Inherits IRepository(Of RemissionOutput)
    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionOutputByCode(code As String) As RemissionOutput
    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionOutputById(id As Integer) As RemissionOutput

    ''' <summary>
    ''' Genera el comprobante contable para remision de salida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_GenerateJournalVoucherByRemissionOutput(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByRemissionOutput_Result

End Interface

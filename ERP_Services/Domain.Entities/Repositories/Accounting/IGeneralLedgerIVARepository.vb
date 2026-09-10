'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepositiry
' Author           : Carlos Ernesto Cordoba
' Created          : 06-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Contrato de repositorio para la entidad GeneralLedgerIVA
''' </summary>
Public Interface IGeneralLedgerIVARepository
    Inherits IRepository(Of GeneralLedgerIVA)

#Region "Methods"

    ''' <summary>
    ''' obtiene el iva por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGeneralLedgerIVAByCode(code As String) As GeneralLedgerIVA
    ''' <summary>
    ''' obtiene el iva por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGeneralLedgerIVAById(id As Integer) As GeneralLedgerIVA

#End Region

End Interface

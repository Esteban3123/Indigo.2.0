'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Security.Entities
Imports Domain.Base.Entities

Public Interface ICrossingAccountRepository
    Inherits IRepository(Of CrossingAccount)

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por ir
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetCrossingAccountById(ByVal Id As Integer, Optional tracking As Boolean = False) As CrossingAccount

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCrossingAccount(ByVal code As String, Optional tracking As Boolean = False) As CrossingAccount

    ''' <summary>
    ''' Gets the crossing account by account payable list identifier.
    ''' </summary>
    ''' <param name="listAccountPayableId">The list account payable identifier.</param>
    ''' <returns></returns>
    Function GetCrossingAccountByAccountPayableListId(listAccountPayableId As List(Of Integer)) As List(Of String)

    Function SP_SaveMasiveCxCCrossingAccount(XmlObject As String, CrossingType As Integer, ThirdPartyId As Integer) As List(Of SP_SaveMasiveCxCCrossingAccount_Result)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="TreasuryNoteId"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function SP_ReverseCrossingAccount(TreasuryNoteId As Integer, UserCode As String) As List(Of SP_ReverseCrossingAccount_Result)

End Interface
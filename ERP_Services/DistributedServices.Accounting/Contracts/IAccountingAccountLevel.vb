#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAccountingAccountLevel

#Region "Methods"

    ''' <summary>
    ''' Saves the account level.
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAccountLevel(ByVal doc As MainAccountLevels) As Boolean

    ''' <summary>
    ''' Deletes the account level.
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAccountLevel(ByVal doc As MainAccountLevels) As ActionMessageResult(Of MainAccountLevels)

    ''' <summary>
    ''' Gets the account Level by code.
    ''' </summary>
    ''' <param name="Code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountLevelByCode(ByVal Code As String, Optional tracking As Boolean = True) As MainAccountLevels

    ''' <summary>
    ''' Gets the account Level by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountLevelById(ByVal id As Integer, Optional tracking As Boolean = True) As MainAccountLevels

    ''' <summary>
    ''' Gets all account level.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllAccountLevel() As List(Of MainAccountLevels)
#End Region

End Interface

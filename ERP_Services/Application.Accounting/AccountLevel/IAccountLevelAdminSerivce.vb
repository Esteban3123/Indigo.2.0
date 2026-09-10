'***********************************************************************
' Assembly         : Domain.Seedwork
' Author           : Juan F. Tamayo
' Created          : 2014-01-15
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' define los servicios disponibles para todas las operaciones
''' con la entidad clase contable
''' </summary>
Public Interface IAccountLevelAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Saves the account level.
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAccountLevel(ByVal doc As MainAccountLevels, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Deletes the account level.
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAccountLevel(ByVal doc As MainAccountLevels, ByVal audit As AuditMessage) As ActionMessageResult(Of MainAccountLevels)

    ''' <summary>
    ''' Gets the account Level by code.
    ''' </summary>
    ''' <param name="Code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountLevelByCode(ByVal Code As String, Optional tracking As Boolean = True) As MainAccountLevels

    ''' <summary>
    ''' Gets the account Level by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountLevelById(ByVal id As Integer, Optional tracking As Boolean = True) As MainAccountLevels

    ''' <summary>
    ''' Gets all account level.
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAccountLevel() As List(Of MainAccountLevels)

#End Region

End Interface

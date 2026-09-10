'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Sergio abraham Fernandez Cruz
' Created          : 2014-4-12
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad nivel de cuenta
''' </summary>
Public Interface IAccountLevelRepository
    Inherits IRepository(Of MainAccountLevels)

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
    ''' Gets all acount level.
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAcountLevel() As List(Of MainAccountLevels)
End Interface
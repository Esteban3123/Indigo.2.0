'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad clase contable
''' </summary>
Public Interface IAccountClassRepository
    Inherits IRepository(Of MainAccountClasses)

    ''' <summary>
    ''' Gets the account class by code.
    ''' </summary>
    ''' <param name="Code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountClassByCode(ByVal Code As String, Optional tracking As Boolean = True) As MainAccountClasses

    ''' <summary>
    ''' Gets the account class by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountClassById(ByVal id As Integer, Optional tracking As Boolean = True) As MainAccountClasses

    ''' <summary>
    ''' Gets all account class.
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAccountClass() As List(Of MainAccountClasses)

End Interface
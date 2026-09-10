'***********************************************************************
' Assembly         : Domain.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Base
#End Region

''' <summary>
''' 	
''' </summary>
Public Interface ILoginRepository
    Inherits IRepository(Of Roll)

    ''' <summary>
    ''' Consultar el perfil del usuario
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    Function GetRolUser(codeUser As String) As String

    ''' <summary>
    ''' Lista huella digital del usuario logeado en genesis
    ''' </summary>
    ''' <returns></returns>
    Function GetFingerPrint() As List(Of Domain.Security.Entities.Person)

End Interface

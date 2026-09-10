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
Public Interface IGroupRepository
    Inherits IRepository(Of Group)


    ''' <summary>
    ''' Lists the groups all.
    ''' </summary>
    ''' <returns></returns>
    Function ListGroupsAll() As List(Of GroupAll)

    ''' <summary>
    ''' Consulta un grupo especifico
    ''' </summary>
    ''' <param name="id">el id del grupo.</param>
    ''' <returns></returns>
    Function GetGroupById(ByVal id As Integer) As Group

    ''' <summary>
    ''' Consulta un grupo especifico
    ''' </summary>
    ''' <param name="codeGroup">el codigo del grupo.</param>
    ''' <returns></returns>
    Function GetGroup(ByVal codeGroup As String) As Group

End Interface

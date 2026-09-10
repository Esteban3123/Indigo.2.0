'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface ISurgicalGroupRepository
    Inherits IRepository(Of SurgicalGroup)

    ''' <summary>
    ''' Obtiene un grupo quirurgico por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSurgicalGroup(code As String) As SurgicalGroup

    ''' <summary>
    ''' Obtiene un grupo quirurgico por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSurgicalGroupById(id As Integer) As SurgicalGroup

End Interface

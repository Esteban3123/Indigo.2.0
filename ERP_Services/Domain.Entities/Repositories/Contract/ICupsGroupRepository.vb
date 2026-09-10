'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface ICupsGroupRepository
    Inherits IRepository(Of CupsGroup)

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsGroup(code As String) As CupsGroup

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsGroupById(id As Integer) As CupsGroup

End Interface

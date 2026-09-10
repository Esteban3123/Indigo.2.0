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


Public Interface ICupsSubGroupRepository
    Inherits IRepository(Of CupsSubgroup)

    ''' <summary>
    ''' Obtiene un CupsSubgroup por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsSubgroup(code As String) As CupsSubgroup

    ''' <summary>
    ''' Obtiene un CupsSubgroup por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsSubgroupById(id As Integer) As CupsSubgroup

End Interface

'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IUVRRangeRepository
    Inherits IRepository(Of UVRRange)

    ''' <summary>
    ''' Obtiene un rango de uvr por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUVRRange(code As String) As UVRRange

    ''' <summary>
    ''' Obtiene un rango de uvr por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUVRRangeById(id As Integer) As UVRRange

End Interface

'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 19-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface ISuspensionCancellationRepository
    Inherits IRepository(Of SuspensionCancellation)

    ''' <summary>
    ''' obtiene un levantamiento de supension de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuspensionCancellationByCode(code As String) As SuspensionCancellation
    ''' <summary>
    ''' obtiene un levantamiento de supension de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuspensionCancellationById(id As Integer) As SuspensionCancellation

End Interface

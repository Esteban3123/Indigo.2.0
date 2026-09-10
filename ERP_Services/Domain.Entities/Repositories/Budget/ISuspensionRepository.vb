'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 18-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface ISuspensionRepository
    Inherits IRepository(Of Suspension)

    ''' <summary>
    ''' obtiene una supension de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuspensionByCode(code As String) As Suspension
    ''' <summary>
    ''' obtiene una supension de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuspensionById(id As Integer) As Suspension

End Interface

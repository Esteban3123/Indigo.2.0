'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IRemissionOutputDetailRepository
    Inherits IRepository(Of RemissionOutputDetail)
    ''' <summary>
    ''' lista el detalla de la remision
    ''' </summary>
    ''' <param name="RemissionOutputId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListRemissionOutputDetailByIdRemissionOutput(RemissionOutputId As Integer) As List(Of RemissionOutputDetail)
    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionOutputDetailById(Id As Integer) As RemissionOutputDetail
End Interface

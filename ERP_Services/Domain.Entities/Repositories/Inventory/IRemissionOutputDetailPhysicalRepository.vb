'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IRemissionOutputDetailPhysicalRepository
    Inherits IRepository(Of RemissionOutputDetailPhysical)

    ''' <summary>
    ''' lista los detalles del detalle de la remision de salida
    ''' </summary>
    ''' <param name="RemissionOutputId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListRemissionOutputDetailPhysicalByRemissionOutputId(RemissionOutputId As Integer) As List(Of RemissionOutputDetailPhysical)
    ''' <summary>
    ''' obtiene un detalle del detalle de la remision de salida por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionOutputDetailPhysicalById(Id As Integer) As RemissionOutputDetailPhysical
End Interface

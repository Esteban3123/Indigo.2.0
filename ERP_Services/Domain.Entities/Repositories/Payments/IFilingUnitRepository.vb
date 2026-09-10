'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IFilingUnitRepository
    Inherits IRepository(Of FilingUnit)

    ''' <summary>
    ''' Obtiene una unidad de radicacion
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilingUnit(code As String, Optional tracking As Boolean = True) As FilingUnit

    ''' <summary>
    ''' Obtiene una unidad de radicacion
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilingUnitById(id As String, Optional tracking As Boolean = True) As FilingUnit

    ''' <summary>
    ''' Obtiene las unidades operativas a las cuales tiene permiso un usuario, tambien carga las unidades funcionales padres
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilingUnitByUser(userCode As String) As List(Of FilingUnit)
    ''' <summary>
    ''' Obtiene las unidades operativas a las cuales tiene permiso un usuario
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilingUnitByUserPermission(userCode As String) As List(Of FilingUnitUser)

End Interface

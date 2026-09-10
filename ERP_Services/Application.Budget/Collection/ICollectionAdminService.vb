'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ICollectionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene una modificacion del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Collection

    ''' <summary>
    ''' obtiene una modificacion del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionById(id As Integer) As Collection

    ''' <summary>
    ''' metodo para guardar una modificacion del pac
    ''' </summary>
    ''' <param name="collection"></param>
    ''' <param name="listDetailsForDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveCollection(collection As Collection, listDetailsForDelete As List(Of Integer), ByVal audit As AuditMessage) As ActionResult(Of Collection)

End Interface

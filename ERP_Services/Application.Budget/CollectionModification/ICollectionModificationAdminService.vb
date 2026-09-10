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

Public Interface ICollectionModificationAdminService
    Inherits IDisposable
    ''' <summary>
    ''' obtiene una modificacion del recaudo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As CollectionModification
    ''' <summary>
    ''' obtiene una modificacion del recaudo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionModificationById(id As Integer) As CollectionModification
    ''' <summary>
    ''' metodo para guardar una modificacion del recaudo
    ''' </summary>
    ''' <param name="CollectionModification"></param>
    ''' <param name="listModificationDetailDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveCollectionModification(CollectionModification As CollectionModification, listModificationDetailDelete As List(Of Integer), ByVal audit As AuditMessage) As ActionResult(Of CollectionModification)
End Interface

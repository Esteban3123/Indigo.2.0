'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface ICollectionDetailRepository
    Inherits IRepository(Of CollectionDetail)
    ''' <summary>
    ''' obtiene le detalle del recaudo por id de la cabecera
    ''' </summary>
    ''' <param name="CollectionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionDetailByCollectionId(CollectionId As Integer) As List(Of CollectionDetail)
    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionDetailById(id As Integer) As CollectionDetail

End Interface

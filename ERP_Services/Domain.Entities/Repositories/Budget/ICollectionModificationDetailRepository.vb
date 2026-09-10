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

Public Interface ICollectionModificationDetailRepository
    Inherits IRepository(Of CollectionModificationDetail)
    ''' <summary>
    ''' obtiene le detalle de la modificacion del recaudo por id de la cabecera
    ''' </summary>
    ''' <param name="CollectionModificationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionModificationDetailByCollectionId(CollectionModificationId As Integer) As List(Of CollectionModificationDetail)
End Interface

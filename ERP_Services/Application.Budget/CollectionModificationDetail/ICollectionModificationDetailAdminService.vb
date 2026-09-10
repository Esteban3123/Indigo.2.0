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

Public Interface ICollectionModificationDetailAdminService
    Inherits IDisposable
    ''' <summary>
    ''' obtiene le detalle de la modificacion por id de la cabecera
    ''' </summary>
    ''' <param name="CollectionId "></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionModificationDetailByCollectionId(CollectionModificationId As Integer) As List(Of CollectionModificationDetail)
End Interface

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


Public Interface ICollectionModificationRepository
    Inherits IRepository(Of CollectionModification)

    ''' <summary>
    ''' obtiene una modificacion del recaudo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionModificationByCode(code As String, BudgetaryValidityId As Integer) As CollectionModification

    ''' <summary>
    ''' obtiene una modificacion del recaudo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionModificationById(id As Integer) As CollectionModification

    ''' <summary>
    ''' Guarda la modificacion
    ''' </summary>
    ''' <param name="CollectionModificationXml"></param>
    ''' <param name="CollectionModificationDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveCollectionModification(CollectionModificationXml As String, CollectionModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveCollectionModification_Result

End Interface

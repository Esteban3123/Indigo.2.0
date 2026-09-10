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

Public Interface ICollectionRepository
    Inherits IRepository(Of Collection)

    ''' <summary>
    ''' obtiene un recaudo del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionByCode(code As String, BudgetaryValidityId As Integer) As Collection

    ''' <summary>
    ''' obtiene un recaudo del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCollectionById(id As Integer) As Collection

    ''' <summary>
    ''' ejecuta el store procedure para guardar un recaudo
    ''' </summary>
    ''' <param name="collectionlXml"></param>
    ''' <param name="collectionDetailForDeleteXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveCollection(collectionlXml As String, collectionDetailForDeleteXml As String, codeUser As String) As SP_SaveCollection_Result

End Interface

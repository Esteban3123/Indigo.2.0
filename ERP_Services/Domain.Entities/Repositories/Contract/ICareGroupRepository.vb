'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/11/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface ICareGroupRepository
    Inherits IRepository(Of CareGroup)

    Function ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId As Integer) As List(Of CareGroupInvoiceCategories)
    Function ListCareGroupIdsRecognition(operatingUnitId As Integer) As List(Of Integer)

    ''' <summary>
    ''' Obtiene un grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCareGroup(code As String) As CareGroup

    ''' <summary>
    ''' Obtiene un grupo de atencion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCareGroupById(id As Integer) As CareGroup


    Function GetCareGroupByIdSimple(id As Integer) As CareGroup

    Function GetCareGroupByIdWithAssociations(Id As Integer) As CareGroup

    ''' <summary>
    ''' Obtiene un grupo de atencion por id para eliminar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCareGroupByIdForDelete(id As Integer) As CareGroup
    ''' <summary>
    ''' obtinene un grupo de atencion con los campos requeridos
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCareGroupPOCOById(id As Integer) As Dynamic.ExpandoObject

    ''' <summary>
    ''' Guarda el listado en la BD
    ''' </summary>
    ''' <param name="ListCareGroupRate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveList(ListCareGroupRate As List(Of CareGroupRate)) As List(Of CareGroupRate)

    Function GetGroupersCareGroup(CareGroupId As Integer, grouperId As Integer) As GroupersCareGroup

End Interface

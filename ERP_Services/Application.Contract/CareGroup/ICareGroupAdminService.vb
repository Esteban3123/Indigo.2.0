'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICareGroupAdminService
    Inherits IDisposable

    Function ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId As Integer) As List(Of CareGroupInvoiceCategories)
    ''' <summary>
    ''' Guarda o Actualiza un grupo de atencion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCareGroup(ByVal CareGroup As CareGroup, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of CareGroup)

    ''' <summary>
    ''' Elimina un grupo de atencion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCareGroup(ByVal id As Integer, Company As String, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCareGroup(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CareGroup)

    ''' <summary>
    ''' Obtiene un grupo de atencion por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCareGroupById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of CareGroup)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateCareGroup(ByVal id As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CareGroup)

    Function GetGroupersCareGroup(CareGroupId As Integer, grouperId As Integer, ByVal audit As AuditMessage) As GroupersCareGroup


End Interface

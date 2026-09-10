'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceCollection

    ''' <summary>
    ''' obtiene un recuado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCollectionByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.Collection

    ''' <summary>
    ''' obtiene un recuado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCollectionById(id As Integer) As Collection

    ''' <summary>
    ''' metodo para guardar un recaudo
    ''' </summary>
    ''' <param name="collection"></param>
    ''' <param name="listDetailsForDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveCollection(collection As Domain.Entities.Collection, listDetailsForDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Collection)

End Interface

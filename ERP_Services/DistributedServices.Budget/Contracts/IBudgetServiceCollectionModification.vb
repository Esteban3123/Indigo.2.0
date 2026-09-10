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
Public Interface IBudgetServiceCollectionModification
    ''' <summary>
    ''' obtiene un recuado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCollectionModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.CollectionModification
    ''' <summary>
    ''' obtiene un recuado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCollectionModificationById(id As Integer) As CollectionModification

    ''' <summary>
    ''' metodo para guardar un recaudo
    ''' </summary>
    ''' <param name="CollectionModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveCollectionModification(CollectionModification As Domain.Entities.CollectionModification, listModificationDetailDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CollectionModification)
End Interface

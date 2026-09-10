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
Public Interface IBudgetServiceCollectionModificationDetail
    ''' <summary>
    ''' obtiene el detalle de la modificacion recaudo
    ''' </summary>
    ''' <param name="CollectionModificationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCollectionModificationDetailByCollectionId(CollectionModificationId As Integer) As List(Of CollectionModificationDetail)
End Interface

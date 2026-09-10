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
Public Interface IBudgetServiceAnnualizedCashFlowModificationDetail
    ''' <summary>
    ''' obtiene le detalle de la modificacion por id de la cabecera
    ''' </summary>
    ''' <param name="pacModificationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId As Integer) As List(Of AnnualizedCashFlowModificationDetail)
End Interface

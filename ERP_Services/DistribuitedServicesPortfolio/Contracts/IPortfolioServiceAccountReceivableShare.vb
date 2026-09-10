'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()> _
Public Interface IPortfolioServiceAccountReceivableShare

    ''' <summary>
    ''' obtine una cuota de la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivableShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountReceivableShareById(idAccountReceivableShare) As AccountReceivableShare

    <OperationContract()>
    Function SaveAccountReceivableShare(AccountReceivableShare As Domain.Entities.AccountReceivableShare, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountReceivableShare)

End Interface

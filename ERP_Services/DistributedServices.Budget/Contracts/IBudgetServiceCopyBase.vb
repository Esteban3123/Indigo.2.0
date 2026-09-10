'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/09/2015
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
Public Interface IBudgetServiceCopyBase

    ''' <summary>
    ''' Guarda o Actualiza una modificacion de obligacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCopyBase(CopyBase As Domain.Entities.CopyBase, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CopyBase)

End Interface


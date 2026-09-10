'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
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
Public Interface IBudgetServiceObligationModificationDetail

    ''' <summary>
    ''' Obtiene un detalle de una modificacion de obligacion por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetObligationModificationDetailById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModificationDetail)

    ''' <summary>
    ''' Guarda o Actualiza un detalle de una modificacion de obligacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveObligationModificationDetail(ObligationModificationDetail As Domain.Entities.ObligationModificationDetail, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModificationDetail)

    ''' <summary>
    ''' Elimina un detalle de una modifciacion de obligacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteObligationModificationDetail(ObligationModificationDetail As Domain.Entities.ObligationModificationDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface


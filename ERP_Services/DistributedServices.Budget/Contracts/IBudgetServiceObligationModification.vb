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
Public Interface IBudgetServiceObligationModification

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por codigo
    ''' </summary>
    '''<param name="Code">Código</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetObligationModification(Code As String, validityId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModification)

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetObligationModificationById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModification)

    ''' <summary>
    ''' Guarda o Actualiza una modificacion de obligacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveObligationModification(ObligationModification As ObligationModification, listObligationModificationDetailDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModification)

    ''' <summary>
    ''' Elimina una modifciacion de obligacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteObligationModification(ObligationModification As Domain.Entities.ObligationModification, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface


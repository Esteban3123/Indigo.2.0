'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Andrés Steven Rojas
' Created          : 31/10/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCardCollections

    ''' <summary>
    ''' Guarda un registro de recaudo de tarjetas
    ''' </summary>
    ''' <param name="cardCollections">The card collections.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCardCollections(cardCollections As CardCollections, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CardCollections)

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCardCollections(code As String, audit As AuditMessage) As ActionResult(Of CardCollections)

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCardCollectionsById(id As Integer) As CardCollections

End Interface



'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Andrés Steven Rojas
' Created          : 31/10/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    ''' <summary>
    ''' Guarda un registro de recaudo de tarjetas
    ''' </summary>
    ''' <param name="cardCollections">The card collections.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveCardCollections(cardCollections As CardCollections, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CardCollections) Implements ITreasuryServiceCardCollections.SaveCardCollections
        Using service As ICardCollectionsAdminService = Container.Current.Resolve(Of ICardCollectionsAdminService)()
            Return service.SaveCardCollections(cardCollections, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function GetCardCollections(code As String, audit As AuditMessage) As ActionResult(Of CardCollections) Implements ITreasuryServiceCardCollections.GetCardCollections
        Using service As ICardCollectionsAdminService = Container.Current.Resolve(Of ICardCollectionsAdminService)()
            Return service.GetCardCollections(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCardCollectionsById(id As Integer) As CardCollections Implements ITreasuryServiceCardCollections.GetCardCollectionsById
        Using service As ICardCollectionsAdminService = Container.Current.Resolve(Of ICardCollectionsAdminService)()
            Return service.GetCardCollectionsById(id)
        End Using
    End Function

End Class



'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 02-04-2014
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
    ''' Deletes the card.
    ''' </summary>
    ''' <param name="card">The card.</param>
    ''' <returns></returns>
    Public Function DeleteCard(card As Domain.Entities.Cards, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ITreasuryServiceCard.DeleteCard
        Using service As ICardAdminService = Container.Current.Resolve(Of ICardAdminService)()
            Return service.DeleteCard(card, audit)
        End Using
        'Return Me._cardAdminService.DeleteCard(card, audit)
    End Function

    ''' <summary>
    ''' Gets the card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetCard(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.Cards) Implements ITreasuryServiceCard.GetCard
        Using service As ICardAdminService = Container.Current.Resolve(Of ICardAdminService)()
            Return service.GetCard(code, audit)
        End Using
        'Return Me._cardAdminService.GetCard(code, audit)
    End Function

    ''' <summary>
    ''' Saves the card.
    ''' </summary>
    ''' <param name="card">The card.</param>
    ''' <returns></returns>
    Public Function SaveCard(card As Domain.Entities.Cards, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Cards) Implements ITreasuryServiceCard.SaveCard
        Using service As ICardAdminService = Container.Current.Resolve(Of ICardAdminService)()
            Return service.SaveCard(card, audit, idSequence)
        End Using
        'Return Me._cardAdminService.SaveCard(card, audit, idSequence)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateCard(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Cards) Implements ITreasuryServiceCard.UpdateStateCard
        Using service As ICardAdminService = Container.Current.Resolve(Of ICardAdminService)()
            Return service.UpdateStateCard(code, state, audit)
        End Using
        'Return Me._cardAdminService.UpdateStateCard(code, state, audit)
    End Function

    Public Function GetCardById(id As Integer) As Domain.Entities.Cards Implements ITreasuryServiceCard.GetCardById
        Using service As ICardAdminService = Container.Current.Resolve(Of ICardAdminService)()
            Return service.GetCardById(id)
        End Using
        'Return Me._cardAdminService.GetCardById(id)
    End Function

End Class

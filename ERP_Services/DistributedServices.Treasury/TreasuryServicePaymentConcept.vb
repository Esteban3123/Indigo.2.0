'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 13-05-2014
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
    ''' Deletes the payment concept.
    ''' </summary>
    ''' <param name="paymentConcept"></param>
    ''' <returns></returns>
    Public Function DeletePaymentConcept(paymentConcept As Domain.Entities.TreasuryPaymentConcepts, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ITreasuryServicePaymentConcept.DeletePaymentConcept
        Using service As IPaymentConceptAdminService = Container.Current.Resolve(Of IPaymentConceptAdminService)()
            Return service.DeletePaymentConcept(paymentConcept, audit)
        End Using
        'Return Me._paymentConceptAdminService.DeletePaymentConcept(paymentConcept, audit)
    End Function

    ''' <summary>
    ''' Gets the payment concept.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetPaymentConcept(code As String, audit As AuditMessage) As ActionResult(Of TreasuryPaymentConcepts) Implements ITreasuryServicePaymentConcept.GetPaymentConcept
        Using service As IPaymentConceptAdminService = Container.Current.Resolve(Of IPaymentConceptAdminService)()
            Return service.GetPaymentConcept(code, audit)
        End Using
        'Return Me._paymentConceptAdminService.GetPaymentConcept(code, audit)
    End Function

    ''' <summary>
    ''' Saves the payment concept.
    ''' </summary>
    ''' <param name="paymentConcept"></param>
    ''' <returns></returns>
    Public Function SavePaymentConcept(paymentConcept As Domain.Entities.TreasuryPaymentConcepts, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TreasuryPaymentConcepts) Implements ITreasuryServicePaymentConcept.SavePaymentConcept
        Using service As IPaymentConceptAdminService = Container.Current.Resolve(Of IPaymentConceptAdminService)()
            Return service.SavePaymentConcept(paymentConcept, audit, idSequence)
        End Using
        'Return Me._paymentConceptAdminService.SavePaymentConcept(paymentConcept, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state payment concept.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStatePaymentConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TreasuryPaymentConcepts) Implements ITreasuryServicePaymentConcept.UpdateStatePaymentConcept
        Using service As IPaymentConceptAdminService = Container.Current.Resolve(Of IPaymentConceptAdminService)()
            Return service.UpdateStatePaymentConcept(code, state, audit)
        End Using
        'Return Me._paymentConceptAdminService.UpdateStatePaymentConcept(code, state, audit)
    End Function

End Class

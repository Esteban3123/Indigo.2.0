'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 03-04-2014
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
    ''' Elimina un concepto de recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <returns></returns>
    Public Function DeleteCashReceiptConcept(cashReceiptConcept As CashReceiptConcepts, audit As AuditMessage) As ActionResult Implements ITreasuryServiceCashReceiptConcept.DeleteCashReceiptConcept
        Using service As ICashReceiptConceptAdminService = Container.Current.Resolve(Of ICashReceiptConceptAdminService)()
            Return service.DeleteCashReceiptConcept(cashReceiptConcept, audit)
        End Using
        'Return Me._cashReceiptConceptAdminService.DeleteCashReceiptConcept(cashReceiptConcept, audit)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateCashReceiptConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CashReceiptConcepts) Implements ITreasuryServiceCashReceiptConcept.UpdateStateCashReceiptConcept
        Using service As ICashReceiptConceptAdminService = Container.Current.Resolve(Of ICashReceiptConceptAdminService)()
            Return service.UpdateStateCashReceiptConcept(code, state, audit)
        End Using
        'Return Me._cashReceiptConceptAdminService.UpdateStateCashReceiptConcept(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de recibo de caja
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetCashReceiptConcept(code As String, audit As AuditMessage) As CashReceiptConcepts Implements ITreasuryServiceCashReceiptConcept.GetCashReceiptConcept
        Using service As ICashReceiptConceptAdminService = Container.Current.Resolve(Of ICashReceiptConceptAdminService)()
            Return service.GetCashReceiptConcept(code, audit)
        End Using
        'Return Me._cashReceiptConceptAdminService.GetCashReceiptConcept(code, audit)
    End Function

    ''' <summary>
    ''' Guarda un concepto de recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <returns></returns>
    Public Function SaveCashReceiptConcept(cashReceiptConcept As CashReceiptConcepts, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CashReceiptConcepts) Implements ITreasuryServiceCashReceiptConcept.SaveCashReceiptConcept
        Using service As ICashReceiptConceptAdminService = Container.Current.Resolve(Of ICashReceiptConceptAdminService)()
            Return service.SaveCashReceiptConcept(cashReceiptConcept, audit, idSequence)
        End Using
        'Return Me._cashReceiptConceptAdminService.SaveCashReceiptConcept(cashReceiptConcept, audit, idSequence)
    End Function

    Public Function GetCashReceiptConceptById(id As Integer) As Domain.Entities.CashReceiptConcepts Implements ITreasuryServiceCashReceiptConcept.GetCashReceiptConceptById
        Using service As ICashReceiptConceptAdminService = Container.Current.Resolve(Of ICashReceiptConceptAdminService)()
            Return service.GetCashReceiptConceptById(id)
        End Using
        'Return Me._cashReceiptConceptAdminService.GetCashReceiptConceptById(id)
    End Function
End Class
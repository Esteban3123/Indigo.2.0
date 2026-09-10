'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Hector Rodriguez Rubiano
' Created          : 05/11/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Treasury
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
#End Region

Partial Class TreasuryService

    Public Function GetCashFlowConceptByCode(code As String, audit As AuditMessage) As ActionResult(Of CashFlowConcept) Implements ITreasuryServiceCashFlowConcept.GetCashFlowConceptByCode
        Using service As ICashFlowConceptAdminService = Container.Current.Resolve(Of ICashFlowConceptAdminService)()
            Return service.GetCashFlowConceptByCode(code, audit)
        End Using
    End Function

    Public Function GetCashFlowConceptById(id As Integer) As ActionResult(Of CashFlowConcept) Implements ITreasuryServiceCashFlowConcept.GetCashFlowConceptById
        Using service As ICashFlowConceptAdminService = Container.Current.Resolve(Of ICashFlowConceptAdminService)()
            Return service.GetCashFlowConceptById(id)
        End Using

    End Function

    Public Function SaveCashFlowConcept(CashFlowConcept As CashFlowConcept, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CashFlowConcept) Implements ITreasuryServiceCashFlowConcept.SaveCashFlowConcept
        Using service As ICashFlowConceptAdminService = Container.Current.Resolve(Of ICashFlowConceptAdminService)()
            Return service.SaveCashFlowConcept(CashFlowConcept, audit, idSequence)
        End Using

    End Function

    Public Function DeleteCashFlowConcept(CashFlowConcept As CashFlowConcept, audit As AuditMessage) As ActionResult Implements ITreasuryServiceCashFlowConcept.DeleteCashFlowConcept
        Using service As ICashFlowConceptAdminService = Container.Current.Resolve(Of ICashFlowConceptAdminService)()
            Return service.DeleteCashFlowConcept(CashFlowConcept, audit)
        End Using

    End Function

    Public Function ChangeStateCashFlowConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CashFlowConcept) Implements ITreasuryServiceCashFlowConcept.ChangeStateCashFlowConcept
        Using service As ICashFlowConceptAdminService = Container.Current.Resolve(Of ICashFlowConceptAdminService)()
            Return service.ChangeStateCashFlowConcept(code, state, audit)
        End Using

    End Function

    Public Function ListCashFlowStatus(parameters As String, session As Infrastructure.CrossCutting.Base.SessionValues) As System.Data.DataSet Implements ITreasuryServiceCashFlowConcept.ListCashFlowStatus

        Using service = Container.Current.Resolve(Of ICashFlowConceptAdminService)()

            Return service.ListCashFlowStatus(parameters, session)
        End Using
    End Function

End Class

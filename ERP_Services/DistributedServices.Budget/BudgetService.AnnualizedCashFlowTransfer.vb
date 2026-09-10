'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 27-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' obtiene un traslado del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowTransferByCode(code As String, type As Integer, audit As AuditMessage) As Domain.Entities.AnnualizedCashFlowTransfer Implements IBudgetServiceAnnualizedCashFlowTransfer.GetAnnualizedCashFlowTransferByCode
        Using service As IAnnualizedCashFlowTransferAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowTransferAdminService)()
            Return service.GetAnnualizedCashFlowTransferByCode(code, type, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowTransferById(id As Integer) As Domain.Entities.AnnualizedCashFlowTransfer Implements IBudgetServiceAnnualizedCashFlowTransfer.GetAnnualizedCashFlowTransferById
        Using service As IAnnualizedCashFlowTransferAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowTransferAdminService)()
            Return service.GetAnnualizedCashFlowTransferById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una modificacion de presupuesto
    ''' </summary>
    ''' <param name="annualizedCashFlowTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAnnualizedCashFlowTransfer(annualizedCashFlowTransfer As AnnualizedCashFlowTransfer, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AnnualizedCashFlowTransfer) Implements IBudgetServiceAnnualizedCashFlowTransfer.SaveAnnualizedCashFlowTransfer
        Using service As IAnnualizedCashFlowTransferAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowTransferAdminService)()
            Return service.SaveAnnualizedCashFlowTransfer(annualizedCashFlowTransfer, audit, idSequense)
        End Using
    End Function

End Class
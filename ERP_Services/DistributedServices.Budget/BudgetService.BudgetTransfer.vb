'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

    ''' <summary>
    ''' Elimina el traslado
    ''' </summary>
    ''' <param name="budgetTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBudgetTransfer(budgetTransfer As Domain.Entities.BudgetTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBudgetServiceBudgetTransfer.DeleteBudgetTransfer
        Using service As IBudgetTransferAdminService = Container.Current.Resolve(Of IBudgetTransferAdminService)()
            Return service.DeleteBudgetTransfer(budgetTransfer, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el traslado por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="ItemType"></param>
    ''' <param name="yearValidity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetTransfer(Code As String, ItemType As Byte, yearValidity As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransfer) Implements IBudgetServiceBudgetTransfer.GetBudgetTransfer
        Using service As IBudgetTransferAdminService = Container.Current.Resolve(Of IBudgetTransferAdminService)()
            Return service.GetBudgetTransfer(Code.Trim(), ItemType, yearValidity, audit)
        End Using
    End Function

    Public Function GetBudgetTransferById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransfer) Implements IBudgetServiceBudgetTransfer.GetBudgetTransferById
        Using service As IBudgetTransferAdminService = Container.Current.Resolve(Of IBudgetTransferAdminService)()
            Return service.GetBudgetTransferById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza el traslado
    ''' </summary>
    ''' <param name="budgetTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBudgetTransfer(budgetTransfer As Domain.Entities.BudgetTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransfer) Implements IBudgetServiceBudgetTransfer.SaveBudgetTransfer
        Using service As IBudgetTransferAdminService = Container.Current.Resolve(Of IBudgetTransferAdminService)()
            Return service.SaveBudgetTransfer(budgetTransfer, audit, idSequense)
        End Using
    End Function

End Class
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
    ''' Guarda la copia de presupuesto
    ''' </summary>
    ''' <param name="CopyBase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCopyBase(CopyBase As Domain.Entities.CopyBase, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CopyBase) Implements IBudgetServiceCopyBase.SaveCopyBase
        Using service As ICopyBaseAdminService = Container.Current.Resolve(Of ICopyBaseAdminService)()
            Return service.SaveCopyBase(CopyBase, audit)
        End Using
    End Function

End Class
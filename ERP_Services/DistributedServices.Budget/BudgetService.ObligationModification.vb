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
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="ObligationModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteObligationModification(ObligationModification As Domain.Entities.ObligationModification, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBudgetServiceObligationModification.DeleteObligationModification
        Using service As IObligationModificationAdminService = Container.Current.Resolve(Of IObligationModificationAdminService)()
            Return service.DeleteObligationModification(ObligationModification, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModification(Code As String, validityId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModification) Implements IBudgetServiceObligationModification.GetObligationModification
        Using service As IObligationModificationAdminService = Container.Current.Resolve(Of IObligationModificationAdminService)()
            Return service.GetObligationModification(Code.Trim(), validityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModificationById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModification) Implements IBudgetServiceObligationModification.GetObligationModificationById
        Using service As IObligationModificationAdminService = Container.Current.Resolve(Of IObligationModificationAdminService)()
            Return service.GetObligationModificationById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="ObligationModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveObligationModification(ObligationModification As Domain.Entities.ObligationModification, listObligationModificationDetailDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModification) Implements IBudgetServiceObligationModification.SaveObligationModification
        Using service As IObligationModificationAdminService = Container.Current.Resolve(Of IObligationModificationAdminService)()
            Return service.SaveObligationModification(ObligationModification, listObligationModificationDetailDelete, audit)
        End Using
    End Function

End Class
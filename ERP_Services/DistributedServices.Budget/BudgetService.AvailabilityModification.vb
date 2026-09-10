'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 28-08-2015
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
    Public Function GetAvailabilityModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.AvailabilityModification Implements IBudgetServiceAvailabilityModification.GetAvailabilityModificationByCode
        Using service As IAvailabilityModificationAdminService = Container.Current.Resolve(Of IAvailabilityModificationAdminService)()
            Return service.GetAvailabilityModificationByCode(code, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAvailabilityModificationById(id As Integer) As Domain.Entities.AvailabilityModification Implements IBudgetServiceAvailabilityModification.GetAvailabilityModificationById
        Using service As IAvailabilityModificationAdminService = Container.Current.Resolve(Of IAvailabilityModificationAdminService)()
            Return service.GetAvailabilityModificationById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una modificacion de presupuesto
    ''' </summary>
    ''' <param name="availabilityModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAvailabilityModification(AvailabilityModification As AvailabilityModification, listAvailabilityModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of AvailabilityModification) Implements IBudgetServiceAvailabilityModification.SaveAvailabilityModification
        Using service As IAvailabilityModificationAdminService = Container.Current.Resolve(Of IAvailabilityModificationAdminService)()
            Return service.SaveAvailabilityModification(AvailabilityModification, listAvailabilityModificationDetailDelete, audit)
        End Using
    End Function

End Class
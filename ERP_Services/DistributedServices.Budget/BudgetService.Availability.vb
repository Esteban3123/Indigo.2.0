'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

#Region "Methods"

    ''' <summary>
    ''' Elimina un disponibilidad
    ''' </summary>
    ''' <param name="availability"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAvailability(availability As Domain.Entities.Availability, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBudgetServiceAvailability.DeleteAvailability
        Using service As IAvailabilityAdminService = Container.Current.Resolve(Of IAvailabilityAdminService)()
            Return service.DeleteAvailability(availability, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la disponibilidad por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailability(Code As String, ItemType As Byte, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Availability) Implements IBudgetServiceAvailability.GetAvailability
        Using service As IAvailabilityAdminService = Container.Current.Resolve(Of IAvailabilityAdminService)()
            Return service.GetAvailability(Code.Trim(), ItemType, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la disponibilidad por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityById(Id As Integer) As Domain.Entities.Availability Implements IBudgetServiceAvailability.GetAvailabilityById
        Using service As IAvailabilityAdminService = Container.Current.Resolve(Of IAvailabilityAdminService)()
            Return service.GetAvailabilityById(Id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza la disponibilidad
    ''' </summary>
    ''' <param name="availability"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAvailability(availability As Domain.Entities.Availability, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Availability) Implements IBudgetServiceAvailability.SaveAvailability
        Using service As IAvailabilityAdminService = Container.Current.Resolve(Of IAvailabilityAdminService)()
            Return service.SaveAvailability(availability, audit, idSequense)
        End Using
    End Function

#End Region

End Class
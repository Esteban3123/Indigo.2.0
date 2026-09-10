'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 23-09-2015
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
    ''' obtiene una prorroga de disponibilidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAvailabilityExtensionById(id As Integer) As Domain.Entities.AvailabilityExtension Implements IBudgetServiceAvailabilityExtension.GetAvailabilityExtensionById
        Using service As IAvailabilityExtensionAdminService = Container.Current.Resolve(Of IAvailabilityExtensionAdminService)()
            Return service.GetAvailabilityExtensionById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una prorroga de disponibilidad
    ''' </summary>
    ''' <param name="availabilityExtension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAvailabilityExtension(availabilityExtension As AvailabilityExtension, audit As AuditMessage) As ActionResult(Of AvailabilityExtension) Implements IBudgetServiceAvailabilityExtension.SaveAvailabilityExtension
        Using service As IAvailabilityExtensionAdminService = Container.Current.Resolve(Of IAvailabilityExtensionAdminService)()
            Return service.SaveAvailabilityExtension(availabilityExtension, audit)
        End Using
    End Function

End Class
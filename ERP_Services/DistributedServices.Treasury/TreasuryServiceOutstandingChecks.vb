'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 02-04-2014
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
    ''' Deletes the outstanding checks.
    ''' </summary>
    ''' <param name="outstandingChecks"></param>
    ''' <returns></returns>
    Public Function DeleteOutstandingChecks(outstandingChecks As OutstandingChecks, audit As AuditMessage) As ActionResult Implements ITreasuryServiceOutstandingChecks.DeleteOutstandingChecks
        Using service As IOutstandingChecksAdminService = Container.Current.Resolve(Of IOutstandingChecksAdminService)()
            Return service.DeleteOutstandingChecks(outstandingChecks, audit)
        End Using
        'Return Me._outstandingChecks.DeleteOutstandingChecks(outstandingChecks, audit)
    End Function

    ''' <summary>
    ''' Obtiene el primer cheque que esta en espera
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFirstOutstandingChecks(IdCheckBook As Integer) As OutstandingChecks Implements ITreasuryServiceOutstandingChecks.GetFirstOutstandingChecks
        Using service As IOutstandingChecksAdminService = Container.Current.Resolve(Of IOutstandingChecksAdminService)()
            Return service.GetFirstOutstandingChecks(IdCheckBook)
        End Using
        'Return Me._outstandingChecks.GetFirstOutstandingChecks(IdCheckBook)
    End Function

    ''' <summary>
    ''' Obtiene un cheque pendiente por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetOutstandingChecksById(Id As Integer) As OutstandingChecks Implements ITreasuryServiceOutstandingChecks.GetOutstandingChecksById
        Using service As IOutstandingChecksAdminService = Container.Current.Resolve(Of IOutstandingChecksAdminService)()
            Return service.GetOutstandingChecksById(Id)
        End Using
        'Return Me._outstandingChecks.GetOutstandingChecksById(Id)
    End Function

    ''' <summary>
    ''' Lista todos los cheques pendientes por id de la chequera
    ''' </summary>
    ''' <param name="IdCheckBook">The identifier check book.</param>
    ''' <returns></returns>
    Public Function ListOutstandingChecksByIdCheckBook(IdCheckBook As Integer) As List(Of OutstandingChecks) Implements ITreasuryServiceOutstandingChecks.ListOutstandingChecksByIdCheckBook
        Using service As IOutstandingChecksAdminService = Container.Current.Resolve(Of IOutstandingChecksAdminService)()
            Return service.ListOutstandingChecksByIdCheckBook(IdCheckBook)
        End Using
        'Return Me._outstandingChecks.ListOutstandingChecksByIdCheckBook(IdCheckBook)
    End Function

    ''' <summary>
    ''' Saves the outstanding checks.
    ''' </summary>
    ''' <param name="outstandingChecks"></param>
    ''' <returns></returns>
    Public Function SaveOutstandingChecks(outstandingChecks As OutstandingChecks, audit As AuditMessage) As ActionResult(Of OutstandingChecks) Implements ITreasuryServiceOutstandingChecks.SaveOutstandingChecks
        Using service As IOutstandingChecksAdminService = Container.Current.Resolve(Of IOutstandingChecksAdminService)()
            Return service.SaveOutstandingChecks(outstandingChecks, audit)
        End Using
        'Return Me._outstandingChecks.SaveOutstandingChecks(outstandingChecks, audit)
    End Function
End Class
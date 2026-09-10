'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 24-05-2014
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
    Implements ITreasuryServiceCancellationCheck

    ''' <summary>
    ''' Obtener un registro de cheque cancelado
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCancellationCheckByEntityAccountAndCheckNumber(IdEntityAccount As Integer, CheckNumber As String, audit As AuditMessage) As ActionResult(Of CancellationChecks) Implements ITreasuryServiceCancellationCheck.GetCancellationCheckByEntityAccountAndCheckNumber
        Using service As ICancellationCheckAdminService = Container.Current.Resolve(Of ICancellationCheckAdminService)()
            Return service.GetCancellationCheckByEntityAccountAndCheckNumber(IdEntityAccount, CheckNumber, audit)
        End Using
        'Return Me._cancellationCheckAdminService.GetCancellationCheckByEntityAccountAndCheckNumber(IdEntityAccount, CheckNumber, audit)
    End Function

    ''' <summary>
    ''' Saves the cancellation check.
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveCancellationCheck(cancellationCheck As CancellationChecks, audit As AuditMessage) As ActionResult(Of CancellationChecks) Implements ITreasuryServiceCancellationCheck.SaveCancellationCheck
        Using service As ICancellationCheckAdminService = Container.Current.Resolve(Of ICancellationCheckAdminService)()
            Return service.SaveCancellationCheck(cancellationCheck, audit)
        End Using
        'Return Me._cancellationCheckAdminService.SaveCancellationCheck(cancellationCheck, audit)
    End Function

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id de chequera y numero de cheque
    ''' </summary>
    ''' <param name="checkBookId"></param>
    ''' <param name="CheckNumber"></param>
    ''' <returns></returns>
    Public Function GetCancellationCheckByCheckBookIdAndCheckNumber(checkBookId As Integer, CheckNumber As Long) As CancellationChecks Implements ITreasuryServiceCancellationCheck.GetCancellationCheckByCheckBookIdAndCheckNumber
        Using service As ICancellationCheckAdminService = Container.Current.Resolve(Of ICancellationCheckAdminService)()
            Return service.GetCancellationCheckByCheckBookIdAndCheckNumber(checkBookId, CheckNumber)
        End Using
        'Return Me._cancellationCheckAdminService.GetCancellationCheckByCheckBookIdAndCheckNumber(checkBookId, CheckNumber)
    End Function

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCancellationCheckByCheckBookIdAndCheckNumber(ByVal id As Integer) As CancellationChecks Implements ITreasuryServiceCancellationCheck.GetCancellationCheckById
        Using service As ICancellationCheckAdminService = Container.Current.Resolve(Of ICancellationCheckAdminService)()
            Return service.GetCancellationCheckById(id)
        End Using
        'Return Me._cancellationCheckAdminService.GetCancellationCheckById(id)
    End Function

End Class


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
    Implements ITreasuryServiceCashing

    ''' <summary>
    ''' Elimina un registro de cambio de cheque
    ''' </summary>
    Public Function DeleteCashing(checkCashing As CheckCashing, audit As AuditMessage) As ActionResult Implements ITreasuryServiceCashing.DeleteCashing
        Using service As ICashingAdminService = Container.Current.Resolve(Of ICashingAdminService)()
            Return service.DeleteCashing(checkCashing, audit)
        End Using
        'Return Me._cashingAdminService.DeleteCashing(checkCashing, audit)
    End Function

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por codigo
    ''' </summary>
    Public Function GetCashing(code As String, audit As AuditMessage) As ActionResult(Of CheckCashing) Implements ITreasuryServiceCashing.GetCashing
        Using service As ICashingAdminService = Container.Current.Resolve(Of ICashingAdminService)()
            Return service.GetCashing(code, audit)
        End Using
        'Return Me._cashingAdminService.GetCashing(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por id
    ''' </summary>
    Public Function GetCashingById(id As Integer, tracking As Boolean) As CheckCashing Implements ITreasuryServiceCashing.GetCashingById
        Using service As ICashingAdminService = Container.Current.Resolve(Of ICashingAdminService)()
            Return service.GetCashingById(id, tracking)
        End Using
        'Return Me._cashingAdminService.GetCashingById(id, tracking)
    End Function

    ''' <summary>
    ''' Guarda un registro de cambio de cheque
    ''' </summary>
    Public Function SaveCashing(checkCashing As CheckCashing, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CheckCashing) Implements ITreasuryServiceCashing.SaveCashing
        Using service As ICashingAdminService = Container.Current.Resolve(Of ICashingAdminService)()
            Return service.SaveCashing(checkCashing, audit, idSequence)
        End Using
        'Return Me._cashingAdminService.SaveCashing(checkCashing, audit, idSequence)
    End Function

End Class
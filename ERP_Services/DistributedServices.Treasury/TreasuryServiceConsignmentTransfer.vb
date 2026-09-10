'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 25-09-2014
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
    Implements ITreasuryServiceConsignmentTransfer

    ''' <summary>
    ''' confirma una consignacion / traslado
    ''' </summary>
    ''' <param name="consignmentTransferId"></param>
    ''' <returns></returns>
    Public Function ConfirmConsignmentTransfer(consignmentTransferId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String) Implements ITreasuryServiceConsignmentTransfer.ConfirmConsignmentTransfer
        Using service As IConsignmentTransferAdminService = Container.Current.Resolve(Of IConsignmentTransferAdminService)()
            Return service.ConfirmConsignmentTransfer(consignmentTransferId, audit)
        End Using
        'Return Me._consignmentTrabsferAdminService.ConfirmConsignmentTransfer(consignmentTransferId, audit)
    End Function

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por codigo
    ''' </summary>
    Public Function GetConsignmentTransfer(code As String, audit As AuditMessage, Optional tracking As Boolean = False) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Consignment) Implements ITreasuryServiceConsignmentTransfer.GetConsignmentTransfer
        Using service As IConsignmentTransferAdminService = Container.Current.Resolve(Of IConsignmentTransferAdminService)()
            Return service.GetConsignmentTransfer(code, audit, tracking)
        End Using
        'Return Me._consignmentTrabsferAdminService.GetConsignmentTransfer(code, audit, tracking)
    End Function

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por id
    ''' </summary>
    Public Function GetConsignmentTransferById(Id As Integer, Optional tracking As Boolean = False) As Domain.Entities.Consignment Implements ITreasuryServiceConsignmentTransfer.GetConsignmentTransferById
        Using service As IConsignmentTransferAdminService = Container.Current.Resolve(Of IConsignmentTransferAdminService)()
            Return service.GetConsignmentTransferById(Id, tracking)
        End Using
        'Return Me._consignmentTrabsferAdminService.GetConsignmentTransferById(Id, tracking)
    End Function

    ''' <summary>
    ''' Guarda una consignacion / traslado
    ''' </summary>
    Public Function SaveConsignmentTransfer(consignmentTransfer As Domain.Entities.Consignment, withConfirm As Boolean, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Consignment) Implements ITreasuryServiceConsignmentTransfer.SaveConsignmentTransfer
        Using service As IConsignmentTransferAdminService = Container.Current.Resolve(Of IConsignmentTransferAdminService)()
            Return service.SaveConsignmentTransfer(consignmentTransfer, audit, withConfirm, idSequence)
        End Using
        'Return Me._consignmentTrabsferAdminService.SaveConsignmentTransfer(consignmentTransfer, audit, withConfirm, idSequence)
    End Function

End Class
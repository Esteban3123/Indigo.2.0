'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 01-04-2014
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
    ''' Elimina un cheque bloqueado
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <returns></returns>
    Public Function DeleteCheckBlock(checkBlock As CheckBlock, audit As AuditMessage) As ActionResult Implements ITreasuryServiceCheckBlock.DeleteCheckBlock
        Using service As ICheckBlockAdminService = Container.Current.Resolve(Of ICheckBlockAdminService)()
            Return service.DeleteCheckBlock(checkBlock, audit)
        End Using
        'Return Me._checkBlockAdminService.DeleteCheckBlock(checkBlock, audit)
    End Function

    ''' <summary>
    ''' Obtiene un cheque bloqueado por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCheckBlockById(Id As Integer, audit As AuditMessage) As CheckBlock Implements ITreasuryServiceCheckBlock.GetCheckBlockById
        Using service As ICheckBlockAdminService = Container.Current.Resolve(Of ICheckBlockAdminService)()
            Return service.GetCheckBlockById(Id, audit)
        End Using
        'Return Me._checkBlockAdminService.GetCheckBlockById(Id, audit)
    End Function

    ''' <summary>
    ''' Obtiene un cheque bloqueado por el id de la chequera y numero del cheque
    ''' </summary>
    ''' <param name="IdCheckBook"></param>
    ''' <param name="checkNumber"></param>
    ''' <returns></returns>
    Public Function GetCheckBlockByIdCheckBookAndNumber(IdCheckBook As Integer, checkNumber As Long, audit As AuditMessage) As ActionResult(Of CheckBlock) Implements ITreasuryServiceCheckBlock.GetCheckBlockByIdCheckBookAndNumber
        Using service As ICheckBlockAdminService = Container.Current.Resolve(Of ICheckBlockAdminService)()
            Return service.GetCheckBlockByIdCheckBookAndNumber(IdCheckBook, checkNumber, audit)
        End Using
        'Return Me._checkBlockAdminService.GetCheckBlockByIdCheckBookAndNumber(IdCheckBook, checkNumber, audit)
    End Function

    ''' <summary>
    ''' Bloquea un cheque
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <returns></returns>
    Public Function SaveCheckBlock(checkBlock As CheckBlock, audit As AuditMessage) As ActionResult(Of CheckBlock) Implements ITreasuryServiceCheckBlock.SaveCheckBlock
        Using service As ICheckBlockAdminService = Container.Current.Resolve(Of ICheckBlockAdminService)()
            Return service.SaveCheckBlock(checkBlock, audit)
        End Using
        'Return Me._checkBlockAdminService.SaveCheckBlock(checkBlock, audit)
    End Function

End Class

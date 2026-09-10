'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 03-06-2014
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
    ''' Elimina una chequera
    ''' </summary>
    ''' <param name="checks">The checks.</param>
    ''' <returns></returns>
    Public Function DeleteCheck(checks As Checkbooks, audit As AuditMessage) As ActionResult Implements ITreasuryServiceCheck.DeleteCheck
        Using service As ICheckAdminService = Container.Current.Resolve(Of ICheckAdminService)()
            Return service.DeleteCheck(checks, audit)
        End Using
        'Return Me._checkAdminService.DeleteCheck(checks, audit)
    End Function

    ''' <summary>
    ''' Obtiene una chequera por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <returns></returns>
    Public Function GetCheckByIdEntityBankAccountAndStatus(IdEntity As Integer, status As Short, audit As AuditMessage) As ActionResult(Of Checkbooks) Implements ITreasuryServiceCheck.GetCheckByIdEntityBankAccountAndStatus
        Using service As ICheckAdminService = Container.Current.Resolve(Of ICheckAdminService)()
            Return service.GetCheckByIdEntityBankAccountAndStatus(IdEntity, status, audit)
        End Using
        'Return Me._checkAdminService.GetCheckByIdEntityBankAccountAndStatus(IdEntity, status, audit)
    End Function

    ''' <summary>
    ''' guarda una chequera
    ''' </summary>
    ''' <param name="check">The check.</param>
    ''' <returns></returns>
    Public Function SaveCheck(check As Checkbooks, audit As AuditMessage) As ActionResult(Of Checkbooks) Implements ITreasuryServiceCheck.SaveCheck
        Using service As ICheckAdminService = Container.Current.Resolve(Of ICheckAdminService)()
            Return service.SaveCheck(check, audit)
        End Using
        'Return Me._checkAdminService.SaveCheck(check, audit)
    End Function

End Class

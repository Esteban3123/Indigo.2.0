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
    Implements ITreasuryServiceCrossingAccount

    ''' <summary>
    ''' Elimina un cruce de cuentas
    ''' </summary>
    ''' <param name="crossingAccount"></param>
    ''' <returns></returns>
    Public Function DeleteCrossingAccount(crossingAccount As CrossingAccount, audit As AuditMessage) As ActionResult Implements ITreasuryServiceCrossingAccount.DeleteCrossingAccount
        Using service As ICrossingAccountAdminService = Container.Current.Resolve(Of ICrossingAccountAdminService)()
            Return service.DeleteCrossingAccount(crossingAccount, audit)
        End Using
        'Return Me._crossingAccountAdminService.DeleteCrossingAccount(crossingAccount, audit)
    End Function

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por codigo
    ''' </summary>
    Public Function GetCrossingAccount(code As String, audit As AuditMessage, Optional tracking As Boolean = False) As ActionResult(Of CrossingAccount) Implements ITreasuryServiceCrossingAccount.GetCrossingAccount
        Using service As ICrossingAccountAdminService = Container.Current.Resolve(Of ICrossingAccountAdminService)()
            Return service.GetCrossingAccount(code, audit, tracking)
        End Using
        'Return Me._crossingAccountAdminService.GetCrossingAccount(code, audit, tracking)
    End Function

    ''' <summary>
    ''' Guardar Cruce de Cuentas
    ''' </summary>
    Public Function SaveCrossingAccount(crossingAccount As CrossingAccount, withConfirm As Boolean, audit As AuditMessage, idSequence As Int64) As ActionResult(Of CrossingAccount) Implements ITreasuryServiceCrossingAccount.SaveCrossingAccount
        Using service As ICrossingAccountAdminService = Container.Current.Resolve(Of ICrossingAccountAdminService)()
            Return service.SaveCrossingAccount(crossingAccount, audit, withConfirm, idSequence)
        End Using
        'Return Me._crossingAccountAdminService.SaveCrossingAccount(crossingAccount, audit, withConfirm, idSequence)
    End Function

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por ir
    ''' </summary>
    Public Function GetCrossingAccountById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccount Implements ITreasuryServiceCrossingAccount.GetCrossingAccountById
        Using service As ICrossingAccountAdminService = Container.Current.Resolve(Of ICrossingAccountAdminService)()
            Return service.GetCrossingAccountById(Id, tracking)
        End Using
        'Return Me._crossingAccountAdminService.GetCrossingAccountById(Id, tracking)
    End Function

    ''' <summary>
    ''' Comfirma el cruce de cuentas
    ''' </summary>
    Public Function ConfirmCrossingAccount(crossingAccountId As Integer, audit As AuditMessage) As ActionResult(Of String) Implements ITreasuryServiceCrossingAccount.ConfirmCrossingAccount
        Using service As ICrossingAccountAdminService = Container.Current.Resolve(Of ICrossingAccountAdminService)()
            Return service.ConfirmCrossingAccount(crossingAccountId, audit)
        End Using
        'Return Me._crossingAccountAdminService.ConfirmCrossingAccount(crossingAccountId, audit)
    End Function

    ''' <summary>
    ''' establece las cuentas por cobrar del copiar y pegar
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC</param>
    ''' <returns></returns>
    Public Function SetDocumentsCrossingCopyPaste(data As List(Of List(Of String)), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) Implements ITreasuryServiceCrossingAccount.SetDocumentsCrossingCopyPaste
        Using service As ICrossingAccountAdminService = Container.Current.Resolve(Of ICrossingAccountAdminService)()
            Return service.SetDocumentsCrossingCopyPaste(data, idThirdPaty, crossingType, processType)
        End Using
        'Return Me._crossingAccountAdminService.SetDocumentsCrossingCopyPaste(data, idThirdPaty, crossingType, processType)
    End Function

    Public Function SetDocumentsCrossingImportFile(data As List(Of ImportFileRow), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) Implements ITreasuryServiceCrossingAccount.SetDocumentsCrossingImportFile
        Using service As ICrossingAccountAdminService = Container.Current.Resolve(Of ICrossingAccountAdminService)()
            Return service.SetDocumentsCrossingImportFile(data, idThirdPaty, crossingType, processType)
        End Using
        'Return Me._crossingAccountAdminService.SetDocumentsCrossingImportFile(data, idThirdPaty, crossingType, processType)
    End Function
End Class

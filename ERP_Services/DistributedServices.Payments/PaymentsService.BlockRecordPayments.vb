'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class PaymentsService

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordPayments(blockRecordPayments As BlockRecordPayments) As ActionResult Implements IPaymentsBlockRecordPayments.DeleteBlockRecordPayments
        Using service As IBlockRecordPaymentsAdminService = Container.Current.Resolve(Of IBlockRecordPaymentsAdminService)()
            Return service.DeleteBlockRecordPayments(blockRecordPayments)
        End Using
        'Return Me._blockRecordPaymentsAdminService.DeleteBlockRecordPayments(blockRecordPayments)
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">Consecutive.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordPaymentsByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordPayments Implements IPaymentsBlockRecordPayments.GetBlockRecordPaymentsByIdformAndIdRecord
        Using service As IBlockRecordPaymentsAdminService = Container.Current.Resolve(Of IBlockRecordPaymentsAdminService)()
            Return service.GetBlockRecordPaymentsByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordPaymentsAdminService.GetBlockRecordPaymentsByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and consecutive.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="Consecutive">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordPaymentsByIdformAndConsecutive(IdForm As String, Consecutive As String) As BlockRecordPayments Implements IPaymentsBlockRecordPayments.GetBlockRecordPaymentsByIdformAndConsecutive
        Using service As IBlockRecordPaymentsAdminService = Container.Current.Resolve(Of IBlockRecordPaymentsAdminService)()
            Return service.GetBlockRecordPaymentsByIdformAndConsecutive(IdForm, Consecutive)
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordPayments(blockRecordPayments As BlockRecordPayments) As ActionResult(Of BlockRecordPayments) Implements IPaymentsBlockRecordPayments.SaveBlockRecordPayments
        Using service As IBlockRecordPaymentsAdminService = Container.Current.Resolve(Of IBlockRecordPaymentsAdminService)()
            Return service.SaveBlockRecordPayments(blockRecordPayments)
        End Using
        'Return Me._blockRecordPaymentsAdminService.SaveBlockRecordPayments(blockRecordPayments)
    End Function

End Class

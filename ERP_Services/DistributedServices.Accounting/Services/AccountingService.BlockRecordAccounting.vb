'***********************************************************************
' Assembly         : DistributedServices.Accounting
' Author           : Diego Andrés Roldán lozano
' Created          : 11-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class AccountingService

    ''' <summary>
    ''' Deletes the block record accounting.
    ''' </summary>
    ''' <param name="blockRecordAccounting">The block record accounting.</param>
    ''' <returns></returns>
    Public Function DeleteBlockRecordAccounting(blockRecordAccounting As BlockRecordGeneralLedger) As ActionResult Implements IAccountingBlockRecordAccounting.DeleteBlockRecordAccounting
        Using service As IBlockRecordAccountingAdminService = Container.Current.Resolve(Of IBlockRecordAccountingAdminService)()
            Return service.DeleteBlockRecordAccounting(blockRecordAccounting)
        End Using
        'Return Me._blockRecordAccountingAdminService.DeleteBlockRecordAccounting(blockRecordAccounting)
    End Function

    ''' <summary>
    ''' Gets the block record accounting by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordAccountingByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordGeneralLedger Implements IAccountingBlockRecordAccounting.GetBlockRecordAccountingByIdformAndIdRecord
        Using service As IBlockRecordAccountingAdminService = Container.Current.Resolve(Of IBlockRecordAccountingAdminService)()
            Return service.GetBlockRecordAccountingByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordAccountingAdminService.GetBlockRecordAccountingByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    ''' <summary>
    ''' Saves the block record accounting.
    ''' </summary>
    ''' <param name="blockRecordAccounting">The block record accounting.</param>
    ''' <returns></returns>
    Public Function SaveBlockRecordAccounting(blockRecordAccounting As BlockRecordGeneralLedger) As ActionResult(Of BlockRecordGeneralLedger) Implements IAccountingBlockRecordAccounting.SaveBlockRecordAccounting
        Using service As IBlockRecordAccountingAdminService = Container.Current.Resolve(Of IBlockRecordAccountingAdminService)()
            Return service.SaveBlockRecordAccounting(blockRecordAccounting)
        End Using
        'Return Me._blockRecordAccountingAdminService.SaveBlockRecordAccounting(blockRecordAccounting)
    End Function

End Class
'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 03-04-2014
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
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordTreasury"></param>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordTreasury(blockRecordTreasury As BlockRecordTreasury) As ActionResult Implements ITreasuryServiceBlockRecordTreasury.DeleteBlockRecordTreasury
        Using service As IBlockRecordTreasuryAdminService = Container.Current.Resolve(Of IBlockRecordTreasuryAdminService)()
            Return service.DeleteBlockRecordTreasury(blockRecordTreasury)
        End Using
        'Return Me._blockRecordTreasuryAdminService.DeleteBlockRecordTreasury(blockRecordTreasury)
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordTreasuryByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordTreasury Implements ITreasuryServiceBlockRecordTreasury.GetBlockRecordTreasuryByIdformAndIdRecord
        Using service As IBlockRecordTreasuryAdminService = Container.Current.Resolve(Of IBlockRecordTreasuryAdminService)()
            Return service.GetBlockRecordTreasuryByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordTreasuryAdminService.GetBlockRecordTreasuryByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordTreasury"></param>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordTreasury(blockRecordTreasury As BlockRecordTreasury) As ActionResult(Of BlockRecordTreasury) Implements ITreasuryServiceBlockRecordTreasury.SaveBlockRecordTreasury
        Using service As IBlockRecordTreasuryAdminService = Container.Current.Resolve(Of IBlockRecordTreasuryAdminService)()
            Return service.SaveBlockRecordTreasury(blockRecordTreasury)
        End Using
        'Return Me._blockRecordTreasuryAdminService.SaveBlockRecordTreasury(blockRecordTreasury)
    End Function

End Class
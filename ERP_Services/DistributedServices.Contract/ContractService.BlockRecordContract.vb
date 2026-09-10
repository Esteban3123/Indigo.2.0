'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordContract(BlockRecordContract As Domain.Entities.BlockRecordContract) As Domain.Base.Entities.ActionResult Implements IContractBlockRecordContract.DeleteBlockRecordContract
        Using service As IBlockRecordContractAdminService = Container.Current.Resolve(Of IBlockRecordContractAdminService)()
            Return service.DeleteBlockRecordContract(BlockRecordContract)
        End Using
        'Return Me._blockRecordContractAdminService.DeleteBlockRecordContract(BlockRecordContract)
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordContractByIdformAndIdRecord(IdForm As String, IdRecord As String) As Domain.Entities.BlockRecordContract Implements IContractBlockRecordContract.GetBlockRecordContractByIdformAndIdRecord
        Using service As IBlockRecordContractAdminService = Container.Current.Resolve(Of IBlockRecordContractAdminService)()
            Return service.GetBlockRecordContractByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordContractAdminService.GetBlockRecordContractByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordContract(BlockRecordContract As Domain.Entities.BlockRecordContract) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecordContract) Implements IContractBlockRecordContract.SaveBlockRecordContract
        Using service As IBlockRecordContractAdminService = Container.Current.Resolve(Of IBlockRecordContractAdminService)()
            Return service.SaveBlockRecordContract(BlockRecordContract)
        End Using
        'Return Me._blockRecordContractAdminService.SaveBlockRecordContract(BlockRecordContract)
    End Function

End Class

'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.MedicalFees
Imports Microsoft.Practices.Unity

Partial Class MedicalFeesService

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordMedicalFees(BlockRecordMedicalFees As Domain.Entities.BlockRecordMedicalFees) As Domain.Base.Entities.ActionResult Implements IMedicalFeesBlockRecordMedicalFees.DeleteBlockRecordMedicalFees
        Using service As IBlockRecordMedicalFeesAdminService = Container.Current.Resolve(Of IBlockRecordMedicalFeesAdminService)()
            Return service.DeleteBlockRecordMedicalFees(BlockRecordMedicalFees)
        End Using
        'Return Me._blockRecordMedicalFeesAdminService.DeleteBlockRecordMedicalFees(BlockRecordMedicalFees)
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordMedicalFeesByIdformAndIdRecord(IdForm As String, IdRecord As String) As Domain.Entities.BlockRecordMedicalFees Implements IMedicalFeesBlockRecordMedicalFees.GetBlockRecordMedicalFeesByIdformAndIdRecord
        Using service As IBlockRecordMedicalFeesAdminService = Container.Current.Resolve(Of IBlockRecordMedicalFeesAdminService)()
            Return service.GetBlockRecordMedicalFeesByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordMedicalFeesAdminService.GetBlockRecordMedicalFeesByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordMedicalFees(BlockRecordMedicalFees As Domain.Entities.BlockRecordMedicalFees) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecordMedicalFees) Implements IMedicalFeesBlockRecordMedicalFees.SaveBlockRecordMedicalFees
        Using service As IBlockRecordMedicalFeesAdminService = Container.Current.Resolve(Of IBlockRecordMedicalFeesAdminService)()
            Return service.SaveBlockRecordMedicalFees(BlockRecordMedicalFees)
        End Using
        'Return Me._blockRecordMedicalFeesAdminService.SaveBlockRecordMedicalFees(BlockRecordMedicalFees)
    End Function

End Class

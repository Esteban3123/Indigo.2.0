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
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateMedicalFeesContract(code As String, state As Integer, audit As AuditMessage) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesContract)) Implements IMedicalFeesMedicalFeesContract.ChangeStateMedicalFeesContract
        Using service As IMedicalFeesContractAdminService = Container.Current.Resolve(Of IMedicalFeesContractAdminService)()
            Return Await service.ChangeStateMedicalFeesContractAsync(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="MedicalFeesContract"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMedicalFeesContract(MedicalFeesContract As Domain.Entities.MedicalFeesContract, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IMedicalFeesMedicalFeesContract.DeleteMedicalFeesContract
        Using service As IMedicalFeesContractAdminService = Container.Current.Resolve(Of IMedicalFeesContractAdminService)()
            Return service.DeleteMedicalFeesContract(MedicalFeesContract, audit)
        End Using
        'Return Me._medicalFeesContractAdminService.DeleteMedicalFeesContract(MedicalFeesContract, audit)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesContract(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesContract) Implements IMedicalFeesMedicalFeesContract.GetMedicalFeesContract
        Using service As IMedicalFeesContractAdminService = Container.Current.Resolve(Of IMedicalFeesContractAdminService)()
            Return service.GetMedicalFeesContract(code, audit)
        End Using
        'Return Me._medicalFeesContractAdminService.GetMedicalFeesContract(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesContractById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesContract) Implements IMedicalFeesMedicalFeesContract.GetMedicalFeesContractById
        Using service As IMedicalFeesContractAdminService = Container.Current.Resolve(Of IMedicalFeesContractAdminService)()
            Return service.GetMedicalFeesContractById(id, audit)
        End Using
        'Return Me._medicalFeesContractAdminService.GetMedicalFeesContractById(id, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="MedicalFeesContract"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveMedicalFeesContract(MedicalFeesContract As Domain.Entities.MedicalFeesContract, idSequense As Int64, audit As AuditMessage) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesContract)) Implements IMedicalFeesMedicalFeesContract.SaveMedicalFeesContract
        Using service As IMedicalFeesContractAdminService = Container.Current.Resolve(Of IMedicalFeesContractAdminService)()
            Return Await service.SaveMedicalFeesContractAsync(MedicalFeesContract, audit, idSequense)
        End Using
    End Function

End Class

'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
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
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IMedicalFeesSequense.GetNumericSequenseGroupById
        Using service As IMedicalFeesSequenseAdminService = Container.Current.Resolve(Of IMedicalFeesSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _medicalFeesSequenseAdminService.GetNumericSequenseGroupById(id)
    End Function

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.MedicalFeesSecuence Implements IMedicalFeesSequense.GetSequenseByIdForm
        Using service As IMedicalFeesSequenseAdminService = Container.Current.Resolve(Of IMedicalFeesSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return Me._medicalFeesSequenseAdminService.GetSequenseByIdForm(idForm)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.MedicalFeesSecuence) As Domain.Base.Entities.ActionResult Implements IMedicalFeesService.SaveSequence
        Using service As IMedicalFeesSequenseAdminService = Container.Current.Resolve(Of IMedicalFeesSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return Me._medicalFeesSequenseAdminService.SaveSequence(seq)
    End Function

End Class

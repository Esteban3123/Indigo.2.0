'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Diego A. Roldán
' Created          : 17-07-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Crystal
Imports Domain.Crystal.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class CrystalService
    Implements ICrystalServiceExternalConsultation

    Public Function ListarCitasMedicas(patientCode As String, atentionCenterCode As String) As List(Of SP_AD_ListarCitasMedicasNativo_Result) Implements ICrystalServiceExternalConsultation.ListarCitasMedicas
        Using service As IExternalConsultationAdminService = Container.Current.Resolve(Of IExternalConsultationAdminService)()
            Return service.ListarCitasMedicas(patientCode, atentionCenterCode)
        End Using
        'Return _externalConsultationAdminService.ListarCitasMedicas(patientCode, atentionCenterCode)
    End Function

End Class
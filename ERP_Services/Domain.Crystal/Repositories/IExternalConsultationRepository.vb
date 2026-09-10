'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 17-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IExternalConsultationRepository
    Inherits IRepository(Of HCHOGASIN)

    Function ListarCitasMedicas(patientCode As String, atentionCenterCode As String) As List(Of SP_AD_ListarCitasMedicasNativo_Result)

End Interface

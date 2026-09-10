'***********************************************************************
' Assembly         : Application.Crystal
' Author           : J. Kevin Garay
' Created          : 26-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Dynamic
Imports Domain.Crystal.Entities

Public Interface IExternalConsultationAdminService
    Inherits IDisposable

    Function ListarCitasMedicas(patientCode As String, atentionCenterCode As String) As List(Of SP_AD_ListarCitasMedicasNativo_Result)

End Interface

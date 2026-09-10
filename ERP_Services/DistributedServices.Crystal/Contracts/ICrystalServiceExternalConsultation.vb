'***********************************************************************
' Assembly         : DistributedService.Crystal
' Author           : Diego A. Roldán
' Created          : 17-07-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region
<ServiceContract()> _
Public Interface ICrystalServiceExternalConsultation

    ''' <summary>
    ''' Listars the citas medicas.
    ''' </summary>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="atentionCenterCode">The atention center code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListarCitasMedicas(patientCode As String, atentionCenterCode As String) As List(Of SP_AD_ListarCitasMedicasNativo_Result)

End Interface

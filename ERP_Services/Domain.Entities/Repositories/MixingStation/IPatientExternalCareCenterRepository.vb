'************************************************************
' Assembly         : Domain.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/07/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IPatientExternalCareCenterRepository
    Inherits IRepository(Of PatientExternalCareCenter)
    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPatientExternalCareCenterByCode(code As String) As PatientExternalCareCenter
    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPatientExternalCareCenterById(id As Integer) As PatientExternalCareCenter

    ''' <summary>
    ''' Guarda un convenio
    ''' </summary>
    ''' <param name="EntityXml"></param>    
    ''' <returns></returns>
    Function SP_SavePatientExternalCareCenter(EntityXml As String, userCode As String) As SP_SavePatientExternalCareCenter_Result

End Interface

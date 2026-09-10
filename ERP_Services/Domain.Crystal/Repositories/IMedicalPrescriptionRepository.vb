'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IMedicalPrescriptionRepository
    Inherits IRepository(Of HCPRESCRA)
    ''' <summary>
    ''' obtiene una prescripcion medica por los parametros requeridos
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalPrescriptionByPatientCodeAdmissionNumberAndProductCode(patientCode As String, admissionNumber As String, productCode As String) As HCPRESCRA
End Interface

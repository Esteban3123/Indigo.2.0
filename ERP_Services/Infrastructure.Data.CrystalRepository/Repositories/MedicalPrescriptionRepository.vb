'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 2015-03-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class MedicalPrescriptionRepository
    Inherits GenericRepository(Of HCPRESCRA)
    Implements IMedicalPrescriptionRepository


    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

    ''' <summary>
    ''' obtiene una prescripcion medica por los parametros requeridos
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    Public Function GetMedicalPrescriptionByPatientCodeAdmissionNumberAndProductCode(patientCode As String, admissionNumber As String, productCode As String) As HCPRESCRA Implements IMedicalPrescriptionRepository.GetMedicalPrescriptionByPatientCodeAdmissionNumberAndProductCode
        Dim list As New List(Of Integer) From {1, 6, 7}
        Return (From mp In _crystalContext.HCPRESCRA Where mp.IPCODPACI = patientCode And mp.ADINGRESO.NUMINGRES = admissionNumber And mp.CODPRODUC = productCode And list.Contains(mp.PREESTADO) Select mp).FirstOrDefault()
    End Function
End Class

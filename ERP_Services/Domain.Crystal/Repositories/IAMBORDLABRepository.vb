'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IAMBORDLABRepository
    Inherits IRepository(Of AMBORDLAB)

    Function GetAMBORDLABByAuto(auto As Integer) As AMBORDLAB

    Function GetAMBORDLABByIngresoPacienteCodigoServicio(admissionNumber As String, codigopaciente As String, codigoservicio As String) As AMBORDLAB

    Function GetByAdmissionNumberAndCupsCode(AdmissionNumber As String, CupsCode As String, ServiceOrderId As Integer) As AMBORDLAB


End Interface
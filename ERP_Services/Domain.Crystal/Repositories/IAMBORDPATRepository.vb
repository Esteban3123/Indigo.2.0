'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IAMBORDPATRepository
    Inherits IRepository(Of AMBORDPAT)

    Function GetAMBORDPATByAuto(auto As Integer) As AMBORDPAT

    Function GetAMBORDPATByIngresoPacienteCodigoServicio(admissionNumber As String, codigopaciente As String, codigoservicio As String) As AMBORDPAT

End Interface
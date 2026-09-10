'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 24-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class AMBORDPATRepository
    Inherits GenericRepository(Of AMBORDPAT)
    Implements IAMBORDPATRepository

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

    Public Function GetAMBORDPATByAuto(auto As Integer) As AMBORDPAT Implements IAMBORDPATRepository.GetAMBORDPATByAuto
        Dim res = (From b In _crystalContext.AMBORDPAT Where b.AUTO = auto Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.AUTO > 0 Then
            Return res
        Else
            Return New AMBORDPAT()
        End If
    End Function

    Public Function GetAMBORDPATByIngresoPacienteCodigoServicio(admissionNumber As String, codigopaciente As String, codigoservicio As String) As AMBORDPAT Implements IAMBORDPATRepository.GetAMBORDPATByIngresoPacienteCodigoServicio
        Return (From a In _crystalContext.AMBORDPAT.AsNoTracking() Where a.NUMINGRES = admissionNumber AndAlso a.IPCODPACI = codigopaciente AndAlso a.CODSERIPS = codigoservicio Select a).FirstOrDefault()
    End Function

End Class
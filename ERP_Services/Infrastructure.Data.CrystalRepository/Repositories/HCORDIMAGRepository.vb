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

Public Class HCORDIMAGRepository
    Inherits GenericRepository(Of HCORDIMAG)
    Implements IHCORDIMAGRepository

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

    Public Function GetHCORDIMAGByAuto(auto As Integer) As HCORDIMAG Implements IHCORDIMAGRepository.GetHCORDIMAGByAuto
        Dim res = (From b In _crystalContext.HCORDIMAG Where b.AUTO = auto Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.AUTO > 0 Then
            Return res
        Else
            Return New HCORDIMAG()
        End If
    End Function

    ''' <summary>
    ''' obtiene datos de la tabla filtrado por numero de ingreso, cups y service Order
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="CupsCode"></param>
    ''' <param name="ServiceOrderId"></param>
    ''' <returns></returns>
    Public Function GetHCORDIMAGByNumberandCupsCode(AdmissionNumber As String, CupsCode As String, ServiceOrderId As Integer) As List(Of DiagnosticImagingUnion) Implements IHCORDIMAGRepository.GetHCORDIMAGByNumberandCupsCode
        If String.IsNullOrEmpty(AdmissionNumber) OrElse String.IsNullOrEmpty(CupsCode) Then
            Return New List(Of DiagnosticImagingUnion)()
        End If
        Dim Query = (From x In _crystalContext.HCORDIMAG
                     Join i In _crystalContext.INPROFSAL On x.MEDREALEC Equals (i.CODPROSAL)
                     Where x.MEDREALEC IsNot Nothing And x.NUMINGRES = AdmissionNumber And x.CODSERIPS = CupsCode And x.GENSERVICEORDER = ServiceOrderId
                     Select New DiagnosticImagingUnion With {.IPCODPACI = x.IPCODPACI, .NUMINGRES = x.NUMINGRES, .MEDREALEC = x.MEDREALEC, .CODSERIPS = x.CODSERIPS,
                         .CODIGONIT = i.CODIGONIT}).ToList()
        Return Query

    End Function

End Class

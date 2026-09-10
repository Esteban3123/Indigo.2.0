'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IAMBORDIMARepository
    Inherits IRepository(Of AMBORDIMA)

    Function GetAMBORDIMAByAuto(auto As Integer) As AMBORDIMA

    ''' <summary>
    ''' funcion que retorna un nuevo objeto para realizar la union entre HCORDIMAG,AMBORDIMA
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="CupsCode"></param>
    ''' <returns></returns>
    Function GetAMBORDIMAByNumberandCupsCode(AdmissionNumber As String, CupsCode As String, ServiceOrderId As Integer) As List(Of DiagnosticImagingUnion)


    Function FindAmbordimaByAdmissionNumberAndCupsCode(AdmissionNumber As String, CupsCode As String, ServiceOrderId As Integer) As AMBORDIMA

End Interface
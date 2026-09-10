'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCORDIMAGRepository
    Inherits IRepository(Of HCORDIMAG)

    Function GetHCORDIMAGByAuto(auto As Integer) As HCORDIMAG

    ''' <summary>
    ''' funcion que retorna un nuevo objeto para realizar la union entre HCORDIMAG,AMBORDIMA
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="CupsCode"></param>
    ''' <returns></returns>
    Function GetHCORDIMAGByNumberandCupsCode(AdmissionNumber As String, CupsCode As String, ServiceOrderId As Integer) As List(Of DiagnosticImagingUnion)

End Interface
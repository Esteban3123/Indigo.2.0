'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IADCENATENRepository
    Inherits IRepository(Of ADCENATEN)

    Function GetADCENATENByCode(code As String, tracking As Boolean) As ADCENATEN

    Function GetFirstADCENATEN() As ActionResult(Of ADCENATEN)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <returns></returns>
    Function GetCentroAtencionAutorizado(usuario As String, grupo As String) As ActionResult(Of List(Of SP_SEG_CentroAtencion_Autorizado_Result))

End Interface
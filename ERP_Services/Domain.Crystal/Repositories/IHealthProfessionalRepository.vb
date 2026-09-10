'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHealthProfessionalRepository
    Inherits IRepository(Of INPROFSAL)

    ''' <summary>
    ''' Obtiene un profesional de la salud por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHealthProfessionalByCode(code As String) As INPROFSAL


End Interface

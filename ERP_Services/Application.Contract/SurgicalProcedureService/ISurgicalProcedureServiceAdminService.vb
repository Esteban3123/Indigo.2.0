'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISurgicalProcedureServiceAdminService
    Inherits IDisposable
    ''' <summary>
    ''' obtiene procedimiento quirurgicos del servcio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSurgicalProcedureServiceByIPSServiceId(idIPSService As Integer) As List(Of SurgicalProcedureService)

End Interface

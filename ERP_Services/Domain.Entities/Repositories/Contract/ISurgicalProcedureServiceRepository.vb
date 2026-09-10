'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface ISurgicalProcedureServiceRepository
    Inherits IRepository(Of SurgicalProcedureService)

    ''' <summary>
    ''' obtiene procedimiento quirurgicos del servcio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSurgicalProcedureServiceByIPSServiceId(idIPSService As Integer) As List(Of SurgicalProcedureService)

    ''' <summary>
    ''' Obtiene el listado de detalles de procedimientos quirurgicos filtrando por IPSServiceParent
    ''' y que el agregado del ips sea de la misma clase
    ''' </summary>
    ''' <param name="IPSServiceParentdId"></param>
    ''' <param name="ServiceClass"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListSurgicalProcedureService(IPSServiceParentdId As Integer, ServiceClass As Byte) As List(Of SurgicalProcedureService)

    ''' <summary>
    ''' Función que valida si item qx esta parametrizado en la tabla DefinitionRateDetailSurgicalProcedures
    ''' </summary>
    ''' <returns></returns>
    Function ValidateIPSServiceInProcedures(DefinitionRateId As Integer, IPSServiceId As Integer, IPSServiceSurgicalId As Integer, CupsEntityId As Integer) As Integer

End Interface

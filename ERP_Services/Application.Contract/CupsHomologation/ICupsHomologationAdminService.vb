'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 19/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface ICupsHomologationAdminService
    Inherits IDisposable
    Function GetHomologationCupsByListCUPS(ParamArray parameters As Object()) As ActionResult(Of List(Of CupsHomologation))
    ''' <summary>
    ''' metodo para obterner las homologaciones del cups
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="CupsId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHomologationCups(CareGroupId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, Optional IPSServiceId As Integer = 0, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of CupsHomologation))
    ''' <summary>
    ''' lista los homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="IpsServiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCupsHomologationByIpsServiceId(IpsServiceId As Integer) As List(Of CupsHomologation)

    ''' <summary>
    ''' Obtiene el listado de homologaciones
    ''' que tiene asociado la entidad CUPS
    ''' </summary>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListCupsHomologationByCupsEntityId(cupsEntityId As Integer) As ActionResult(Of List(Of CupsHomologation))

End Interface

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

Public Interface ICupsHomologationRepository
    Inherits IRepository(Of CupsHomologation)

    ''' <summary>
    ''' lista las homologaciones del CUPS
    ''' </summary>
    ''' <param name="CupsId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCupsHomologationByCupsId(CupsId As Integer, serviceType As Integer) As List(Of CupsHomologation)
    ''' <summary>
    ''' lista los homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="IpsServiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCupsHomologationByIpsServiceId(IpsServiceId As Integer) As List(Of CupsHomologation)

    ''' <summary>
    ''' Gets the cups homologation ips service by cups entity identifier and service manual.
    ''' </summary>
    ''' <param name="cupsEntityId">The cups entity identifier.</param>
    ''' <param name="serviceManua">The service manua.</param>
    ''' <returns></returns>
    Function GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManual(cupsEntityId As Integer, serviceManua As Byte) As List(Of CupsHomologation)

    ''' <summary>
    ''' Obtiene el listado de homologaciones por cupsEntityId y serviceManual
    ''' Este metodo es utilizado en medicalFeesCausation al momento de activar el check
    ''' del valor causado
    ''' </summary>
    ''' <param name="cupsEntityId">Id del CUPS</param>
    ''' <param name="serviceManual">Tipo de Manual</param>
    ''' <param name="IsQx">Variable para saber si estamos en un procedimiento Qx</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManualMedicalFeesCausation(cupsEntityId As Integer, serviceManual As Byte, IsQx As Boolean) As List(Of CupsHomologation)

    ''' <summary>
    ''' Obtiene el listado de homologaciones
    ''' que tiene asociado la entidad CUPS
    ''' </summary>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListCupsHomologationByCupsEntityId(cupsEntityId As Integer) As List(Of CupsHomologation)

End Interface

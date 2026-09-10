'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities.Service
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Class CupsHomologationAdminService
    Implements ICupsHomologationAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsHomologationRepository As ICupsHomologationRepository
    Private _rateManualRepository As IRateManualRepository
    Private _caregroupRepository As ICareGroupRepository
    Private _cupsRepository As ICupsEntityRepository
    Private _contractServices As IContractServices

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(cupsHomologationRepository As ICupsHomologationRepository, rateManualRepository As IRateManualRepository, caregroupRepository As ICareGroupRepository,
                   cupsRepository As ICupsEntityRepository, contractServices As IContractServices)
        If cupsHomologationRepository Is Nothing Then
            Throw New ArgumentNullException("cupsHomologationRepository Vacio")
        End If
        If rateManualRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualRepository vacio")
        End If
        If caregroupRepository Is Nothing Then
            Throw New ArgumentNullException("caregroupRepository vacio")
        End If
        If cupsRepository Is Nothing Then
            Throw New ArgumentNullException("cupsRepository vacio")
        End If
        _cupsHomologationRepository = cupsHomologationRepository
        _rateManualRepository = rateManualRepository
        _caregroupRepository = caregroupRepository
        _cupsRepository = cupsRepository
        _contractServices = contractServices
    End Sub

#End Region

    ''' <summary>
    ''' lista los homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="IpsServiceId"></param>
    ''' <returns></returns>
    Public Function ListCupsHomologationByIpsServiceId(IpsServiceId As Integer) As List(Of CupsHomologation) Implements ICupsHomologationAdminService.ListCupsHomologationByIpsServiceId
        Try
            Return _cupsHomologationRepository.ListCupsHomologationByIpsServiceId(IpsServiceId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of CupsHomologation)
        End Try
    End Function

    ''' <summary>
    ''' metodo para obterner las homologaciones del cups
    ''' </summary>
    Public Function GetHomologationCups(CareGroupId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, Optional IPSServiceId As Integer = 0, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of CupsHomologation)) Implements ICupsHomologationAdminService.GetHomologationCups
        Try
            Return _contractServices.GetHomologationCups(CareGroupId, CupsId, FunctionalUnitId, Specialty, ServiceDate, IPSServiceId, ManualType, RiasId, ContractDescriptionId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CupsHomologation))
        End Try
    End Function

    Public Function GetHomologationCupsByListCUPS(ParamArray parameters() As Object) As ActionResult(Of List(Of CupsHomologation)) Implements ICupsHomologationAdminService.GetHomologationCupsByListCUPS
        Try
            Return _contractServices.GetHomologationCupsByListCUPS(parameters)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CupsHomologation))
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de homologaciones
    ''' que tiene asociado la entidad CUPS
    ''' </summary>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListCupsHomologationByCupsEntityId(cupsEntityId As Integer) As ActionResult(Of List(Of CupsHomologation)) Implements ICupsHomologationAdminService.GetListCupsHomologationByCupsEntityId
        Try
            Dim list = _cupsHomologationRepository.GetListCupsHomologationByCupsEntityId(cupsEntityId)
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _contractServices.Dispose()
            End If
            _cupsHomologationRepository = Nothing
            _rateManualRepository = Nothing
            _caregroupRepository = Nothing
            _cupsRepository = Nothing
            _contractServices = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

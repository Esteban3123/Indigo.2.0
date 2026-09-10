'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos E. Cordoba
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Crystal
Imports Infrastructure.CrossCutting.Base
Imports Application.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Base.Entities
Imports System.Text
Imports Domain.Base

#End Region

Public Class DashboardPharmacyDetailAdminService
    Implements IDashboardPharmacyDetailAdminService

    Private _dashboardPharmacyDetailRepository As IDashboardPharmacyDetailRepository
    Private _dashboardPharmacyDetailSurgicalPackageRepository As IDashboardPharmacyDetailSurgicalPackageRepository
    Private _hCFARMEPDRepository As IHCFARMEPDRepository
    Private _routingLogRepository As IRoutingLogRepository

    Public Sub New(dashboardPharmacyDetailRepository As IDashboardPharmacyDetailRepository, dashboardPharmacyDetailSurgicalPackageRepository As IDashboardPharmacyDetailSurgicalPackageRepository,
                   HCFARMEPDRepository As IHCFARMEPDRepository, RoutingLogRepository As IRoutingLogRepository)
        If dashboardPharmacyDetailRepository Is Nothing Then
            Throw New ArgumentNullException("dashboardPharmacyDetailRepository")
        End If
        If dashboardPharmacyDetailSurgicalPackageRepository Is Nothing Then
            Throw New ArgumentNullException("dashboardPharmacyDetailSurgicalPackageRepository")
        End If
        _dashboardPharmacyDetailRepository = dashboardPharmacyDetailRepository
        _dashboardPharmacyDetailSurgicalPackageRepository = dashboardPharmacyDetailSurgicalPackageRepository
        _hCFARMEPDRepository = HCFARMEPDRepository
        _routingLogRepository = RoutingLogRepository
    End Sub

    ''' <summary>
    ''' lista los detalle de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    Public Function ListDashboardPharmacyDetail(consecutive As Decimal, patientCode As String, admission As String) As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail) Implements IDashboardPharmacyDetailAdminService.ListDashboardPharmacyDetail
        Try
            Return _dashboardPharmacyDetailRepository.ListDashboardPharmacyDetail(consecutive, patientCode, admission)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el nombre del tipo de estancia
    ''' </summary>
    ''' <param name="codePatient"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function GetStayType(codePatient As String, admissionNumber As String) As String Implements IDashboardPharmacyDetailAdminService.GetStayType
        Try
            Return _dashboardPharmacyDetailRepository.GetStayType(codePatient, admissionNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' listar los paquetes QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <returns></returns>
    Public Function ListDashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String) As List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils) Implements IDashboardPharmacyDetailAdminService.ListDashboardPharmacyDetailSurgicalPackage
        Try
            Return _dashboardPharmacyDetailSurgicalPackageRepository.ListDashboardPharmacyDetailSurgicalPackage(consecutive, patientCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Domain.Crystal.Entities.ViewDashBoardPharmacy_SurgicalPackageDeatils)
        End Try
    End Function

    ''' <summary>
    ''' Metodo que valida si se puede agregar la rias
    ''' </summary>
    ''' <returns></returns>
    Public Function SP_RIAS_ValidacionCUPSRIAS(ListParameters As List(Of Tuple(Of String, Integer, String, Integer, DateTime))) As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) Implements IDashboardPharmacyDetailAdminService.SP_RIAS_ValidacionCUPSRIAS
        Try
            'Mensajes que se concatena con lo que devuelva el sp que valida las rias
            Dim messages As New StringBuilder

            'Mensaje que encabeza el mensaje final
            Dim messageTemplate As String = "No se puede agregar a la orden de servicio porque: "

            'Listado que se retorna
            Dim ListReturn As New List(Of SP_RIAS_ValidacionCUPSRIAS_Result)

            'Se valida si el listado viene vacio
            If ListParameters Is Nothing OrElse ListParameters.Count = 0 Then
                Return New ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) With {.StateResult = False, .Message = "No se ha enviado cups para validar"}
            End If

            'Se recorre el listado de cups para poder validar si se puede agregar a la rejilla
            For Each item In ListParameters

                'Se consume el sp para validar el cups
                Dim result = _dashboardPharmacyDetailRepository.SP_RIAS_ValidacionCUPSRIAS(item.Item1, item.Item2, item.Item3, item.Item4, item.Item5)

                'Si no se obtuvo ningun resultado
                If result Is Nothing Then
                    messages.AppendLine("No se obtuvo ningún resultado de validación con el cups " + item.Item3)
                    Continue For
                End If

                'Se validan los resultados del sp
                If result.CodigoMensaje IsNot Nothing Then
                    Select Case result.CodigoMensaje
                        Case "1"
                            messages.AppendLine(messageTemplate)
                            messages.AppendLine("El cups " + item.Item3 + " no aplica a RIAS")
                        Case "2"
                            messages.AppendLine(messageTemplate)
                            messages.AppendLine("El paciente no tiene la edad adecuada para realizar esta actividad según la parametrización del cups " + item.Item3)
                        Case "3"
                            messages.AppendLine(messageTemplate)
                            If result.cantidadRealizadas IsNot Nothing AndAlso result.cantidadRealizadas = 0 Then 'Si la cantidad realizada es cero es porque el cups no se ha realizado ninguna vez y la cantidad solicitada excede a la cantidad de la frecuencia
                                messages.AppendLine("El cups " + item.Item3 + " tiene parametrizado " + result.Frecuencia.ToString() + " vez(veces) entre " + result.EdadMinima.ToString() + " y " + result.EdadMaxima.ToString() + " " + result.UnidadRangoEdad + ", esta es la cantidad máxima definida según la edad del paciente(" + result.Edad + "), revisar parametrización del cups")
                            Else
                                messages.AppendLine("El cups " + item.Item3 + " se ha realizado " + result.cantidadRealizadas.ToString() + " vez(veces) entre " + result.EdadMinima.ToString() + " y " + result.EdadMaxima.ToString() + " " + result.UnidadRangoEdad + ", esta es la cantidad máxima definida según la edad del paciente(" + result.Edad + "), revisar parametrización del cups")
                            End If
                        Case "4"
                            messages.AppendLine(messageTemplate)
                            messages.AppendLine("Desde la última vez que se realizó esta actividad(" + result.FechaUltimoServicioRealizado.ToString() + ") no ha pasado " + result.CantidadPeriodo.ToString() + " " + result.Periodo + ", revisar parametrización del cups " + item.Item3)
                        Case "5"
                            messages.AppendLine(messageTemplate)
                            messages.AppendLine("Esta actividad se ha realizado " + result.cantidadRealizadas.ToString() + " veces en el " + result.Periodo + "(Cantidad límite: " + result.Frecuencia.ToString() + "), la última vez fue " + result.FechaUltimoServicioRealizado.ToString() + ", revisar parametrización del cups " + item.Item3)
                        Case "999"
                            messages.AppendLine(messageTemplate)
                            messages.AppendLine("Ocurrió un error ejecutando el store procedure [dbo].[SP_RIAS_ValidacionCUPSRIAS]")
                    End Select
                End If

                result.RiasCupsId = item.Item2
                result.CupsCode = item.Item3
                ListReturn.Add(result)
            Next

            'Si hay mensajes de validación se retorna false
            If messages.ToString().Length > 0 Then
                Return New ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) With {.StateResult = False, .Message = messages.ToString()}
            End If

            'Se retorna el ok
            Return New ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) With {.ObjectEmbbeded = ListReturn, .StateResult = True, .Message = "Se ha validado correctamente las RIAS"}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para cambiar el enrutamiento de un medicamento a farmacia
    ''' </summary>
    ''' <param name="ListViewDashboardPharmacyDetail"></param>
    ''' <param name="GeneralRoutingLog"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Public Function RouteToPharmacy(ListViewDashboardPharmacyDetail As List(Of ViewDashboardPharmacyDetail), GeneralRoutingLog As RoutingLog, Audit As AuditMessage) As ActionResult Implements IDashboardPharmacyDetailAdminService.RouteToPharmacy
        Dim unitOfWork As IUnitWork = Me._hCFARMEPDRepository.UnitWork

        Try
            If ListViewDashboardPharmacyDetail Is Nothing OrElse ListViewDashboardPharmacyDetail.Count = 0 Then
                Throw New Exception("No existen datos para enrutar a Farmacia")
            End If

            Dim ListIDHCFARMEPD = ListViewDashboardPharmacyDetail.Select(Function(d) d.EntityId).ToList()
            Dim ListHCFARMEPD = Me._hCFARMEPDRepository.GetByFilter(Function(x) ListIDHCFARMEPD.Contains(x.ID)).ToList()
            If ListHCFARMEPD.Any(Function(h) h.VIEPROCESSED = 1) Then
                Throw New Exception("No se puede enviar a farmacia debido a que una o más dosis ya han sido procesadas por central de mezclas.")
            End If

            Dim ListRoutingLog = New List(Of RoutingLog)
            For Each item In ListHCFARMEPD
                Dim RoutingLog = New RoutingLog
                With RoutingLog
                    .IDHCFARMEPD = item.ID
                    .CreationDate = DateTime.Now()
                    .RoutingFrom = item.SENDTO
                    .RoutingTo = 1
                    .UserCode = Audit.CodeUser
                    .Description = GeneralRoutingLog.Description
                    .IDCODMOTANU = GeneralRoutingLog.IDCODMOTANU
                    ListRoutingLog.Add(RoutingLog)
                End With
            Next
            ListRoutingLog.ForEach(Sub(x)
                                       _routingLogRepository.SaveEntity(x)
                                   End Sub)
            ListHCFARMEPD.ForEach(Sub(h)
                                      h.SENDTO = 1
                                      h.CodeSusceptibleMixingStation = Nothing
                                      _hCFARMEPDRepository.SaveEntity(h)
                                  End Sub)
            unitOfWork.Commit()
            Return New ActionResult() With {.StateResult = True, .Message = "Se enruto exitosamente el medicamento a farmacia"}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Envía el medicamento a atención farmacéutica para enrutamiento estableciendo SENDTO = 0
    ''' </summary>
    ''' <param name="ListViewDashboardPharmacyDetail"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Public Function PharmaceuticalCareRouting(ListViewDashboardPharmacyDetail As List(Of ViewDashboardPharmacyDetail), Audit As AuditMessage) As ActionResult Implements IDashboardPharmacyDetailAdminService.PharmaceuticalCareRouting
        Dim unitOfWork As IUnitWork = Me._hCFARMEPDRepository.UnitWork
        Try
            If ListViewDashboardPharmacyDetail Is Nothing OrElse ListViewDashboardPharmacyDetail.Count = 0 Then
                Throw New Exception("No existen datos para enviar a atención farmacéutica")
            End If

            Dim ListIDHCFARMEPD = ListViewDashboardPharmacyDetail.Select(Function(d) d.EntityId).ToList()
            Dim ListHCFARMEPD = Me._hCFARMEPDRepository.GetByFilter(Function(x) ListIDHCFARMEPD.Contains(x.ID)).ToList()

            ListHCFARMEPD.ForEach(Sub(h)
                                      h.SENDTO = 0
                                      _hCFARMEPDRepository.SaveEntity(h)
                                  End Sub)
            unitOfWork.Commit()
            Return New ActionResult() With {.StateResult = True, .Message = "Se envió exitosamente el medicamento a atención farmacéutica para enrutamiento"}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Lista un detalle de farmacia
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function DashboardPharmacyDetail(entityId As Integer) As ViewDashboardPharmacyDetail Implements IDashboardPharmacyDetailAdminService.DashboardPharmacyDetail
        Try
            Return _dashboardPharmacyDetailRepository.DashboardPharmacyDetail(entityId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ViewDashboardPharmacyDetail
        End Try
    End Function

    ''' <summary>
    ''' Devuelve un detalle de la solicitud del paquete QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    Public Function DashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String, productCode As String) As ViewDashBoardPharmacy_SurgicalPackageDeatils Implements IDashboardPharmacyDetailAdminService.DashboardPharmacyDetailSurgicalPackage
        Try
            Return _dashboardPharmacyDetailSurgicalPackageRepository.DashboardPharmacyDetailSurgicalPackage(consecutive, patientCode, productCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Domain.Crystal.Entities.ViewDashBoardPharmacy_SurgicalPackageDeatils
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _dashboardPharmacyDetailRepository = Nothing
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

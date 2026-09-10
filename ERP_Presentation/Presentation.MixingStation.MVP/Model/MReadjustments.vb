'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Giovanny Plazas Lozano
' Created          : 29/08/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository

#End Region

Public Class MReadjustments
    Implements IDisposable

#Region "Fields"


    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' consulta la vista de la rejilla principal
    ''' </summary>
    ''' <param name="CMConfigurationId"></param>
    ''' <returns></returns>
    Public Function GetReadjustmentsByCMConfigurationId(CMConfigurationId As Integer) As List(Of ViewReadjustmentsXpo)
        Dim filter As String = $"CMConfigurationId ={CMConfigurationId} and SendTo =0"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewReadjustmentsXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' se actualiza o guarda la entidad
    ''' </summary>
    ''' <param name="ListReadjustments"></param>
    ''' <returns></returns>
    Public Async Function SaveReadjustmentsRepository(ListReadjustments As List(Of Readjustments), operativeUnitId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveReadjustmentsRepositoryAsync(ListReadjustments, operativeUnitId, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene las readecuaciones que ha tenido un paquete
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusId"></param>
    ''' <returns></returns>
    Public Function GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId As Integer, Optional tracking As Boolean = False) As List(Of Readjustments)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId, Me._sessionValues.AuditMessageWcf, tracking)
    End Function

    ''' <summary>
    ''' Guarda una readecuación
    ''' </summary>
    ''' <param name="readjustment"></param>
    ''' <returns></returns>
    Public Async Function SaveReadjustmentsByTechnicalConcept(readjustment As Readjustments) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveReadjustmentsByTechnicalConceptAsync(readjustment, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Genera un ajuste de inventario por adecuación no aceptada
    ''' </summary>
    ''' <param name="ListReadjustmentsIds"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    Public Async Function GenerateInventoryAjustmenByReadjusments(ListReadjustmentsIds As List(Of Integer), operatingUnitId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GenerateInventoryAjustmenByReadjusmentsAsync(ListReadjustmentsIds, operatingUnitId, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' funcion para consultar las readecuaciones que hacen match con la solicitud a enlazar
    ''' </summary>
    ''' <param name="StandartPackageId"></param>
    ''' <param name="Concentration"></param>
    ''' <param name="ConcentrationMeasurementUnitId"></param>
    ''' <param name="VolumeTotalOrder"></param>
    ''' <param name="VolumeTotalOrderMeasurementUnitId"></param>
    ''' <param name="AtcMainMedicine"></param>
    ''' <param name="AtcVehicle"></param>
    ''' <returns></returns>
    Public Function ViewListToAssignReadjustment(StandartPackageId As Integer, Concentration As Integer,
                                                    ConcentrationMeasurementUnitId As Integer, VolumeTotalOrder As Decimal,
                                                    VolumeTotalOrderMeasurementUnitId As Integer, AtcMainMedicine As Integer,
                                                    AtcVehicle As Integer, DateNow As DateTime) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ViewListToAssignReadjustment(StandartPackageId, Concentration, ConcentrationMeasurementUnitId,
                                                                                                                              VolumeTotalOrder, VolumeTotalOrderMeasurementUnitId, AtcMainMedicine, AtcVehicle, DateNow)
    End Function

    ''' <summary>
    ''' funcion para consultar las readecuaciones que hacen match con la solicitud a enlazar (Antibióticos)
    ''' </summary>
    Public Function ViewReadjustmentsToBeAssigned(StandardPackageId As Integer, DateTimeNow As DateTime,
                                                                AtcMainMedicine? As Integer, QuantityMainMedicine? As Decimal, MeasurementUnitIdMainMedicine? As Integer,
                                                                AtcVehicle? As Integer, QuantityVehicle? As Decimal, MeasurementUnitIdVehicle? As Integer,
                                                                AtcThinner? As Integer, QuantityThinner? As Decimal, MeasurementUnitIdThinner? As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ViewReadjustmentsToBeAssigned(StandardPackageId, DateTimeNow,
                                                                                                                                             AtcMainMedicine, QuantityMainMedicine, MeasurementUnitIdMainMedicine,
                                                                                                                                             AtcVehicle, QuantityVehicle, MeasurementUnitIdVehicle,
                                                                                                                                             AtcThinner, QuantityThinner, MeasurementUnitIdThinner)
    End Function

    ''' <summary>
    ''' consulta un detalle de la solicitud validando que tenga paquete asociado
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetRequestMSDToReadjustmentById(Id As Integer) As Task(Of ActionResult(Of RequestMixingStationDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetRequestMSDToReadjustmentByIdasync(Id)
    End Function

    ''' <summary>
    ''' Vincula el detalle de una solicitud con un producto readecuado
    ''' </summary>
    ''' <param name="RequestMSDetailId"></param>
    ''' <param name="Readjustment"></param>
    ''' <returns></returns>
    Public Async Function AssignReadjustmentToRequestMSDetail(RequestMSDetailId As Integer, Readjustment As Readjustments) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.AssignReadjustmentToRequestMSDetailAsync(RequestMSDetailId, Readjustment)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

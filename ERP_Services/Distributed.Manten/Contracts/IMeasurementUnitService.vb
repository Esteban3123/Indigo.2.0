#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IMeasurementUnitService

    <OperationContract()> _
    Function ListAllMeasurementUnit(Empresa As String) As List(Of MeasurementUnit)


    <OperationContract()> _
    Function DeleteMeasurementUnit(Empresa As String, ByVal MeasurementUnit As MeasurementUnit, ByVal audit As AuditMessage) As ActionResult


    <OperationContract()> _
    Function SaveMeasurementUnit(Empresa As String, ByVal MeasurementUnit As MeasurementUnit, ByVal audit As AuditMessage) As ActionResult(Of MeasurementUnit)

    <OperationContract()> _
    Function GetMeasurementUnit(Empresa As String, ByVal codeMeasurementUnit As String, ByVal audit As AuditMessage) As MeasurementUnit

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStateMeasurementUnit(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of MeasurementUnit)

End Interface

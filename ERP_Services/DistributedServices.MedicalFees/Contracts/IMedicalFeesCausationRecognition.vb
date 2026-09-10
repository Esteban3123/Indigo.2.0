Imports System.ServiceModel
Imports Domain.Base.Entities

''' <summary>
''' Contrato del servicio WCF para reconocimiento de causaciones de honorarios médicos
''' </summary>
<ServiceContract()>
Public Interface IMedicalFeesCausationRecognition

    ''' <summary>
    ''' Genera el reconocimiento contable de causaciones pendientes por proveedor y unidad operativa
    ''' </summary>
    ''' <param name="supplierId">ID del proveedor (médico o agremiación)</param>
    ''' <param name="operatingUnitId">ID de la unidad operativa</param>
    ''' <param name="recognitionDate">Fecha del reconocimiento</param>
    ''' <param name="userCode">Código del usuario que ejecuta el reconocimiento</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    <OperationContract()>
    Function GenerateCausationRecognition(supplierId As Integer, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As ActionResult

    ''' <summary>
    ''' Reversa un reconocimiento de causación
    ''' </summary>
    ''' <param name="causationRecognitionId">ID del reconocimiento a reversar</param>
    ''' <param name="userCode">Código del usuario que ejecuta la reversión</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    <OperationContract()>
    Function ReverseCausationRecognition(causationRecognitionId As Integer, userCode As String) As ActionResult

End Interface


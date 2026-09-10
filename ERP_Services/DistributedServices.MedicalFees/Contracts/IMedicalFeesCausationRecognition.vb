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

    ''' <summary>
    ''' Procesa causaciones automáticas para órdenes de servicio CUPS no reconocidas.
    ''' Ejecuta el flujo de auto-causación desde el botón "No Reconocidos":
    ''' - Solo candidatos en estado Registrado (sin factura)
    ''' - Solo CUPS, sin causación activa previa
    ''' - Excluye liquidados
    ''' - Registra fallos en CausationPending con Retry=True
    ''' Procesa todas las unidades operativas.
    ''' </summary>
    ''' <param name="userCode">Código del usuario que ejecuta el proceso</param>
    ''' <param name="batchSize">Tamaño del batch por iteración (default 500 para ejecución manual)</param>
    ''' <returns>ActionResult con MessageResult: [TotalCandidates, SuccessCount, FailedCount, ExcludedByLiquidation]</returns>
    <OperationContract()>
    Function ProcessUnrecognizedCausations(userCode As String, Optional batchSize As Integer = 500) As ActionResult

End Interface


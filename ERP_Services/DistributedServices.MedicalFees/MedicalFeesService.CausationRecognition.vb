Imports Application.MedicalFees
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

''' <summary>
''' Implementación del servicio de reconocimiento de causaciones (Partial Class)
''' </summary>
Partial Public Class MedicalFeesService
    Implements IMedicalFeesCausationRecognition

    ''' <summary>
    ''' Genera el reconocimiento contable de causaciones pendientes por proveedor y unidad operativa
    ''' </summary>
    ''' <param name="supplierId">ID del proveedor (médico o agremiación)</param>
    ''' <param name="operatingUnitId">ID de la unidad operativa</param>
    ''' <param name="recognitionDate">Fecha del reconocimiento</param>
    ''' <param name="userCode">Código del usuario que ejecuta el reconocimiento</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    Public Function GenerateCausationRecognition(supplierId As Integer, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As ActionResult Implements IMedicalFeesCausationRecognition.GenerateCausationRecognition
        Using service As ICausationRecognitionAdminService = Container.Current.Resolve(Of ICausationRecognitionAdminService)()
            Return service.GenerateCausationRecognition(supplierId, operatingUnitId, recognitionDate, userCode)
        End Using
    End Function

    ''' <summary>
    ''' Reversa un reconocimiento de causación
    ''' </summary>
    ''' <param name="causationRecognitionId">ID del reconocimiento a reversar</param>
    ''' <param name="userCode">Código del usuario que ejecuta la reversión</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    Public Function ReverseCausationRecognition(causationRecognitionId As Integer, userCode As String) As ActionResult Implements IMedicalFeesCausationRecognition.ReverseCausationRecognition
        Using service As ICausationRecognitionAdminService = Container.Current.Resolve(Of ICausationRecognitionAdminService)()
            Return service.ReverseCausationRecognition(causationRecognitionId, userCode)
        End Using
    End Function

    ''' <summary>
    ''' Procesa causaciones automáticas para órdenes de servicio CUPS no reconocidas.
    ''' Solo Registrado (sin factura), CUPS, sin causación activa, excluye liquidados.
    ''' Procesa todas las unidades operativas.
    ''' </summary>
    ''' <param name="userCode">Código del usuario que ejecuta el proceso</param>
    ''' <param name="batchSize">Tamaño del batch por iteración (default 500 para ejecución manual)</param>
    ''' <returns>ActionResult con MessageResult: [TotalCandidates, SuccessCount, FailedCount, ExcludedByLiquidation]</returns>
    Public Function ProcessUnrecognizedCausations(userCode As String, Optional batchSize As Integer = 500) As ActionResult Implements IMedicalFeesCausationRecognition.ProcessUnrecognizedCausations
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Dim result = service.ProcessUnrecognizedCausations(New AuditMessage() With {.CodeUser = userCode}, batchSize)
            Return New ActionResult With {
                .StateResult = result.StateResult,
                .Message = result.Message,
                .MessageResult = If(result.ObjectEmbbeded IsNot Nothing,
                    New List(Of String)({
                        result.ObjectEmbbeded.TotalCandidates.ToString(),
                        result.ObjectEmbbeded.SuccessCount.ToString(),
                        result.ObjectEmbbeded.FailedCount.ToString(),
                        result.ObjectEmbbeded.ExcludedByLiquidation.ToString()
                    }), Nothing)
            }
        End Using
    End Function

End Class


Imports Application.MedicalFees
Imports Domain.Base.Entities
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

End Class


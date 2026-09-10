Imports System.Transactions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

''' <summary>
''' Servicio de aplicación para el reconocimiento contable de causaciones de honorarios médicos
''' </summary>
Public Class CausationRecognitionAdminService
    Implements ICausationRecognitionAdminService

    Private _causationRepository As IMedicalFeesCausationRepository

    ''' <summary>
    ''' Constructor del servicio
    ''' </summary>
    ''' <param name="causationRepository">Repositorio de causaciones</param>
    Public Sub New(causationRepository As IMedicalFeesCausationRepository)
        _causationRepository = causationRepository
    End Sub

    ''' <summary>
    ''' Genera el reconocimiento contable de causaciones pendientes por proveedor y unidad operativa
    ''' </summary>
    ''' <param name="supplierId">ID del proveedor (médico o agremiación)</param>
    ''' <param name="operatingUnitId">ID de la unidad operativa</param>
    ''' <param name="recognitionDate">Fecha del reconocimiento</param>
    ''' <param name="userCode">Código del usuario que ejecuta el reconocimiento</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    Public Function GenerateCausationRecognition(supplierId As Integer, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As ActionResult Implements ICausationRecognitionAdminService.GenerateCausationRecognition
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
                .Timeout = TransactionManager.MaximumTimeout,
                .IsolationLevel = IsolationLevel.ReadCommitted
            })
                ' Llamar al Stored Procedure que generará el reconocimiento de causaciones
                ' El SP consultará internamente la vista ViewListCausationwithoutRecognition
                ' filtrada por SupplierId y OperatingUnitId, y generará los asientos contables correspondientes
                Dim result As SP_GenerateCausationRecognition_Result = _causationRepository.SP_GenerateCausationRecognition(supplierId, operatingUnitId, recognitionDate, userCode)

                If Not result.StatusResult Then
                    scope.Dispose()
                    Return New ActionResult With {
                        .StateResult = False,
                        .Message = result.MessageResult
                    }
                End If

                scope.Complete()
                Return New ActionResult With {
                    .StateResult = True,
                    .Message = result.MessageResult,
                    .MessageResult = {result.CausationRecognitionId.ToString}.ToList()
                }
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {
                .StateResult = False,
                .StatusCode = eStatusResult.EXCEPTION,
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

    ''' <summary>
    ''' Reversa un reconocimiento de causación
    ''' </summary>
    ''' <param name="causationRecognitionId">ID del reconocimiento a reversar</param>
    ''' <param name="userCode">Código del usuario que ejecuta la reversión</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    Public Function ReverseCausationRecognition(causationRecognitionId As Integer, userCode As String) As ActionResult Implements ICausationRecognitionAdminService.ReverseCausationRecognition
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
                .Timeout = TransactionManager.MaximumTimeout,
                .IsolationLevel = IsolationLevel.ReadCommitted
            })
                ' Llamar al Stored Procedure que reversará el reconocimiento
                Dim result As SP_ReverseCausationRecognition_Result = _causationRepository.SP_ReverseCausationRecognition(causationRecognitionId, userCode)

                If Not result.StatusResult Then
                    scope.Dispose()
                    Return New ActionResult With {
                        .StateResult = False,
                        .Message = result.MessageResult
                    }
                End If

                scope.Complete()
                Return New ActionResult With {
                    .StateResult = True,
                    .Message = result.MessageResult,
                    .MessageResult = {result.CausationRecognitionId.ToString}.ToList()
                }
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {
                .StateResult = False,
                .StatusCode = eStatusResult.EXCEPTION,
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _causationRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(ByVal disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


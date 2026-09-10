Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MedicalFeesRepository
Imports Presentation.CloudAgent

''' <summary>
''' Model para el formulario de Causación de Proveedores de Salud
''' </summary>
Public Class MCausation
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Genera el reconocimiento contable de las causaciones no reconocidas por proveedor
    ''' IMPORTANTE: El reconocimiento se agrupa por proveedor (SupplierId)
    ''' Cada proveedor genera:
    ''' - UN registro en CausationRecognition (con su ID único)
    ''' - UN comprobante contable específico
    ''' - Actualiza todas las causaciones de ese proveedor con el CausationRecognitionId
    ''' El SP consultará internamente la vista filtrada por proveedor y generará los asientos contables
    ''' </summary>
    ''' <param name="supplierId">ID del proveedor (médico o agremiación)</param>
    ''' <param name="operatingUnitId">ID de la unidad operativa</param>
    ''' <param name="recognitionDate">Fecha del reconocimiento</param>
    ''' <param name="userCode">Código del usuario</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    Friend Async Function GenerateRecognitionCausations(supplierId As Integer, operatingUnitId As Integer, recognitionDate As Date, userCode As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.GenerateCausationRecognitionAsync(supplierId, operatingUnitId, recognitionDate, userCode)
    End Function

    ''' <summary>
    ''' Reversa un reconocimiento de causación
    ''' Recibe solo el CausationRecognitionId porque cada reconocimiento ya está asociado a un único proveedor
    ''' El SP generará un comprobante de reversión y liberará las causaciones para que puedan ser reconocidas nuevamente
    ''' </summary>
    ''' <param name="causationRecognitionId">ID del reconocimiento a reversar (único por proveedor)</param>
    ''' <param name="userCode">Código del usuario</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    Friend Async Function ReverseCausationRecognition(causationRecognitionId As Integer, userCode As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.ReverseCausationRecognitionAsync(causationRecognitionId, userCode)
    End Function

    ''' <summary>
    ''' Obtiene los datos de causaciones pendientes por reconocer (sin reconocimiento de costo)
    ''' </summary>
    ''' <param name="operativeUnitId">ID de la unidad operativa</param>
    ''' <returns>XPInstantFeedbackSource con las causaciones no reconocidas</returns>
    Public Function GetCausationEntranceData(operativeUnitId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.GetCausationWithoutRecognition(operativeUnitId)
    End Function

    ''' <summary>
    ''' Obtiene las causaciones reconocidas para reversar
    ''' </summary>
    Friend Function GetReverseCausation(operativeUnitId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.GetCausationWithRecognition(operativeUnitId)
    End Function

    ''' <summary>
    ''' Obtiene las causaciones pendientes por causar (con errores)
    ''' Esta vista muestra causaciones que tienen errores de configuración o validación
    ''' Incluye una columna "ErrorMessage" con la descripción del error
    ''' </summary>
    Friend Function GetPendingCausation(operativeUnitId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.GetCausationPending(operativeUnitId)
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

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MedicalFeesRepository

''' <summary>
''' Presenter para el formulario de Causación de Proveedores de Salud
''' </summary>
Public Class PCausation

    Public view As ICausation

    Public Sub New(view As ICausation)
        Me.view = view
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Limpia todos los controles
    ''' </summary>
    Public Async Sub CleanControls()
        Dim dateNow As DateTime = Await New Controls.MVP.MformBase().GetDateServerAsync()
        Me.view.DateInit = dateNow
    End Sub

    ''' <summary>
    ''' Consulta toda la informacion de los servicios por causar
    ''' </summary>
    Public Sub ProcesarCausar(operativeUnitId As Integer)
        Task.Factory.StartNew(Sub()
                                  Using model As New MCausation(Me.view.MyTag)
                                      Dim dtsource = model.GetCausationEntranceData(operativeUnitId)
                                      Me.view.CausationDatasource = dtsource
                                  End Using
                              End Sub)
    End Sub

    ''' <summary>
    ''' Genera el reconocimiento contable de las causaciones no reconocidas por proveedor
    ''' El SP consultará internamente la vista filtrada por proveedor y generará los asientos contables
    ''' </summary>
    ''' <param name="supplierId">ID del proveedor (médico o agremiación)</param>
    ''' <param name="operatingUnitId">ID de la unidad operativa</param>
    ''' <param name="recognitionDate">Fecha del reconocimiento</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    Public Async Function GenerateRecognitionCausationsBySupplier(supplierId As Integer, operatingUnitId As Integer, recognitionDate As Date) As Task(Of ActionResult)
        Using model As New MCausation(Me.view.MyTag)
            Return Await model.GenerateRecognitionCausations(supplierId, operatingUnitId, recognitionDate, SessionValues.Instance.UserIndigo)
        End Using
    End Function

    ''' <summary>
    ''' Consulta las causaciones para reversar
    ''' </summary>
    Public Sub ProcesarReversar(operativeUnitId As Integer)
        Task.Factory.StartNew(Sub()
                                  Using model As New MCausation(Me.view.MyTag)
                                      Dim dtsource = model.GetReverseCausation(operativeUnitId)
                                      Me.view.CausationDatasource = dtsource
                                  End Using
                              End Sub)
    End Sub

    ''' <summary>
    ''' Consulta las causaciones pendientes por causar (con errores)
    ''' </summary>
    Public Sub ProcesarPendientePorCausar(operativeUnitId As Integer)
        Task.Factory.StartNew(Sub()
                                  Using model As New MCausation(Me.view.MyTag)
                                      Dim dtsource = model.GetPendingCausation(operativeUnitId)
                                      Me.view.CausationDatasource = dtsource
                                  End Using
                              End Sub)
    End Sub

    ''' <summary>
    ''' Reversa un reconocimiento de causación
    ''' </summary>
    ''' <param name="causationRecognitionId">ID del reconocimiento a reversar</param>
    ''' <returns>ActionResult con el resultado de la operación</returns>
    Public Async Function ReversarCausacionRecognition(causationRecognitionId As Integer) As Task(Of ActionResult)
        Using model As New MCausation(Me.view.MyTag)
            Return Await model.ReverseCausationRecognition(causationRecognitionId, SessionValues.Instance.UserIndigo)
        End Using
    End Function
#End Region

End Class


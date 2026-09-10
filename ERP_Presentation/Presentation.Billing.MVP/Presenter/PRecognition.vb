Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Class PRecognition

    Public view As IRecognition

    Public Sub New(view As IRecognition)
        Me.view = view
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Limpia todos los controles
    ''' </summary>
    Public Async Sub CleanControls()
        Dim dateNow As DateTime = Await New Controls.MVP.MformBase().GetDateServerAsync()
        'dateNow = dateNow.AddMonths(-1)

        Me.view.DateInit = dateNow 'New Date(dateNow.Year, dateNow.Month, 1)
        'Me.view.DateEnd = New Date(dateNow.Year, dateNow.Month + 1, 1).AddDays(-1)
    End Sub
    ''' <summary>
    ''' Consulta toda la informacion de los ingresos
    ''' </summary>
    Public Sub ProcesarConfirmar(operativeUnitId As Integer)

        Task.Factory.StartNew(Sub()
                                  Using model As New MRecognition(Me.view.MyTag)
                                      Dim dtsource = model.GetRecognitionEntranceData(operativeUnitId) '.Source
                                      Me.view.RecognitionDatasource = dtsource
                                  End Using
                              End Sub)

        'Using model As New MRecognition(Me.view.MyTag)
        '    Me.view.RecognitionDatasource = model.GetRecognitionEntranceData(values.Item1, values.Item2, operativeUnitId)
        'End Using
    End Sub

    ''' <summary>
    ''' Liquidars this instance.
    ''' </summary>
    Public Async Function Liquidar(careGroupXml As String, operatingUnitId As Integer, recognitionDate As DateTime) As Task(Of ActionResult)
        Using model As New MRecognition(Me.view.MyTag)
            Return Await model.GenerateRecognition(careGroupXml, operatingUnitId, recognitionDate, SessionValues.Instance.UserIndigo)
        End Using
    End Function

    Public Async Function LiquidarByCareGroup(careGroupId As Integer, careGroupTotal As Decimal, operatingUnitId As Integer, recognitionDate As Date) As Task(Of ActionResult)
        Using model As New MRecognition(Me.view.MyTag)
            Return Await model.GenerateRecognitionByCareGroup(careGroupId, careGroupTotal, operatingUnitId, recognitionDate, SessionValues.Instance.UserIndigo)
        End Using
    End Function

    Public Sub ProcesarReversar(operativeUnitId As Integer)
        Task.Factory.StartNew(Sub()
                                  Using model As New MRecognition(Me.view.MyTag)
                                      Dim dtsource = model.GetReverseRecognition(operativeUnitId) '.Source
                                      Me.view.RecognitionDatasource = dtsource
                                  End Using
                              End Sub)
    End Sub

    Public Async Function LiquidarReversar(recognitionId As Integer) As Task(Of ActionResult)
        Using model As New MRecognition(Me.view.MyTag)
            Return Await model.ReverseRecognition(recognitionId, SessionValues.Instance.UserIndigo)
        End Using
    End Function
#End Region

End Class

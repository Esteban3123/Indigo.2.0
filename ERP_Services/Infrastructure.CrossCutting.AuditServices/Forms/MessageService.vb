''' <summary>
''' clase para leer los mensajes que envia el servicio y mostrarlos en el frontal
''' </summary>
Public NotInheritable Class MessageService

    ''' <summary>
    ''' Ocurre cuando se modifica la propiedad del mensaje [nuevo mensaje].
    ''' </summary>
    Public Shared Event NewMessage()

    Private Shared _logService As String
    ''' <summary>
    ''' el Log del servicio
    ''' </summary>
    ''' <value>el Log del servicio.</value>
    Public Shared ReadOnly Property LogService As String
        Get
            Return _logService
        End Get
    End Property

    ''' <summary>
    ''' estable el valor de la propiedad del mensaje
    ''' </summary>
    Public Shared Sub SetMessage(ByVal message As String)
        If String.IsNullOrEmpty(message) Then
            Throw New ArgumentNullException("Mensaje vacio")
        End If
        _logService = String.Concat(_logService, Environment.NewLine, message)
        RaiseEvent NewMessage()
    End Sub

    Private Shared _dateLastExecution As DateTime?
    ''' <summary>
    ''' obtiene la fecha  de la ultima ejecucion tarea.
    ''' </summary>
    ''' <value>The fecha ultima ejecucion tarea.</value>
    Public Shared ReadOnly Property DateLastExecution As DateTime?
        Get
            Return _dateLastExecution
        End Get
    End Property

    ''' <summary>
    ''' estable el valor de la propiedad del mensaje
    ''' </summary>
    Public Shared Sub SetDateLastExecution(ByVal dateLastExecution As DateTime)
        _dateLastExecution = dateLastExecution
    End Sub

    Private Shared _timeTask As Integer
    ''' <summary>
    ''' Valor del tiempo de la proxima ejecucion de la tarea
    ''' </summary>
    ''' <value>la cantidad de tiempo.</value>
    Public Shared ReadOnly Property TimeTask As Integer
        Get
            Return _timeTask
        End Get
    End Property

    ''' <summary>
    ''' estable el valor de la propiedad del TiempoTarea
    ''' </summary>
    Public Shared Sub SetTimeTask(ByVal timeTask As Integer)
        If timeTask = 0 Then
            Throw New ArgumentNullException("timeTask vacio")
        End If
        _timeTask = timeTask
    End Sub

    Private Shared _unitTimeTask As String
    ''' <summary>
    ''' la Unidad del tiempo de la proxima ejecucion de la tarea
    ''' </summary>
    ''' <value>la unidad de tiempo (1. minutos 2. horas 3. dias ).</value>
    Public Shared ReadOnly Property UnitTimeTask As String
        Get
            Return _unitTimeTask
        End Get
    End Property

    ''' <summary>
    ''' estable el valor de la propiedad del TiempoTareaUnidad
    ''' </summary>
    Public Shared Sub SetUnitTimeTask(ByVal unitTimeTask As String)
        If String.IsNullOrEmpty(unitTimeTask) Then
            Throw New ArgumentNullException("unitTimeTask vacio")
        End If
        _unitTimeTask = unitTimeTask
    End Sub

End Class

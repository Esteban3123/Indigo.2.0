''' <summary>
''' Clase para controlar el detalle de horas agrupadas por concepto
''' </summary>
''' <remarks></remarks>
Public Class DetailHoursByConcept

    Private _conceptId As Integer
    Public Property ConceptId As Integer
        Get
            Return _conceptId
        End Get
        Set(value As Integer)
            _conceptId = value
        End Set
    End Property

    Private _conceptName As String
    Public Property ConceptName As String
        Get
            Return _conceptName
        End Get
        Set(value As String)
            _conceptName = value
        End Set
    End Property

    Private _hoursNumber As Decimal
    Public Property HoursNumber As Decimal
        Get
            Return _hoursNumber
        End Get
        Set(value As Decimal)
            _hoursNumber = value
        End Set
    End Property

End Class

''' <summary>
''' Clase que contiene los turnos en otras unidades funcionales, para mostrar en rejilla
''' </summary>
''' <remarks></remarks>
Public Class ScheduleDetailInOtherFU

    Private _detMoreFU As String
    Public Property DetMoreFU As String
        Get
            Return _detMoreFU
        End Get
        Set(value As String)
            _detMoreFU = value
        End Set
    End Property

    Private _detMoreT As String
    Public Property DetMoreT As String
        Get
            Return _detMoreT
        End Get
        Set(value As String)
            _detMoreT = value
        End Set
    End Property
End Class

''' <summary>
''' Clase que contiene el mensaje de informacion de los registros que no guardo
''' </summary>
''' <remarks></remarks>
Public Class DaysNotsave

    Private _employeeError As String
    Public Property EmployeeError As String
        Get
            Return _employeeError
        End Get
        Set(value As String)
            _employeeError = value
        End Set
    End Property

    Private _messegeError As String
    Public Property MessegeError As String
        Get
            Return _messegeError
        End Get
        Set(value As String)
            _messegeError = value
        End Set
    End Property

    Private _dayError As String
    Public Property DayError As String
        Get
            Return _dayError
        End Get
        Set(value As String)
            _dayError = value
        End Set
    End Property

    Private _iconError As System.Drawing.Bitmap
    Public Property IconError As System.Drawing.Bitmap
        Get
            Return _iconError
        End Get
        Set(value As System.Drawing.Bitmap)
            _iconError = value
        End Set
    End Property

    Private _templateError As String
    Public Property TemplateError As String
        Get
            Return _templateError
        End Get
        Set(value As String)
            _templateError = value
        End Set
    End Property
End Class

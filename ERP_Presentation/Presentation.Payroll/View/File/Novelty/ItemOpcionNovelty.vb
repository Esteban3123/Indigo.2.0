Imports System.Globalization
Imports DevExpress.XtraEditors.Controls

Public Class ItemOpcionNovelty

    ''' <summary>
    ''' Contructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(
                  ImageComboItem As ImageComboBoxItem,
                  Optional menorTres As Boolean = False,
                  Optional mayor2Menor91 As Boolean = False,
                  Optional mayor90 As Boolean = False,
                  Optional riesgoProfesional As Boolean = False,
                  Optional maternidad As Boolean = False,
                  Optional mayor180 As Boolean = False,
                  Optional losTresPrimerosDias As Boolean = False,
                  Optional todosLosDias As Boolean = False,
                  Optional noLiquida As Boolean = False,
                  Optional culture As CultureInfo = Nothing)

        Me.MenorTres = menorTres
        Me.Mayor2Menor91 = mayor2Menor91
        Me.Mayor90 = mayor90
        Me.Mayor180 = mayor180
        Me.RiesgosProfesionales = riesgoProfesional
        Me.LicenciaMaternidad = maternidad
        Me.ImageComboItem = ImageComboItem
        '--------------------------------------

        If culture Is Nothing Then
            culture = New CultureInfo("es-CO")
        End If

        Me.Culture = culture
        Me.LosTresPrimerosDias = losTresPrimerosDias
        Me.TodosLosDias = todosLosDias
        Me.NoLiquida = noLiquida
    End Sub

    ''' <summary>
    ''' Propiedad usada para identificar si el objeto se muestra
    ''' cuando la cantidad de dias sea menor 4
    ''' </summary>
    ''' <remarks></remarks>
    Private _MenorTres As Boolean = False
    Public Property MenorTres() As Boolean
        Get
            Return _MenorTres
        End Get
        Set(value As Boolean)
            _MenorTres = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad usada para identificar si el objeto se muestra
    ''' cuando la cantidad de dias sea mayor a 3 y  menor a 91
    ''' </summary>
    ''' <remarks></remarks>
    Private _Mayor2Menor91 As Boolean = False
    Public Property Mayor2Menor91 As Boolean
        Get
            Return _Mayor2Menor91
        End Get
        Set(value As Boolean)
            _Mayor2Menor91 = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad usada para identificar si el objeto se muestra
    ''' cuando la cantidad de dias sea mayor a 91
    ''' </summary>
    ''' <remarks></remarks>
    Private _Mayor90 As Boolean = False
    Public Property Mayor90 As Boolean
        Get
            Return _Mayor90
        End Get
        Set(value As Boolean)
            _Mayor90 = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para identificar que opciones se muestran cuando se selecciona la opcion "Riesgos Profesionales"
    ''' </summary>
    ''' <remarks></remarks>
    Private _riesgosProfesionales As Boolean = False
    Public Property RiesgosProfesionales As Boolean
        Get
            Return _riesgosProfesionales
        End Get
        Set(value As Boolean)
            _riesgosProfesionales = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para identificar que opciones se muestran cuando se selecciona la opcion "Licencia de Maternidad"
    ''' </summary>
    ''' <remarks></remarks>
    Private _maternidad As Boolean = False
    Public Property LicenciaMaternidad() As Boolean
        Get
            Return _maternidad
        End Get
        Set(value As Boolean)
            _maternidad = value
        End Set
    End Property

    ''' <summary>
    ''' Item que se va a mostrar en los combos
    ''' </summary>
    ''' <remarks></remarks>
    Private _ImageComboItem As ImageComboBoxItem
    Public Property ImageComboItem As ImageComboBoxItem
        Get
            Return _ImageComboItem
        End Get
        Set(value As ImageComboBoxItem)
            _ImageComboItem = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad usada para identificar si el objeto se muestra
    ''' cuando la cantidad de dias sea mayor a 180
    ''' </summary>
    ''' <remarks></remarks>
    Private _Mayor180 As Boolean = False
    Public Property Mayor180 As Boolean
        Get
            Return _Mayor180
        End Get
        Set(value As Boolean)
            _Mayor180 = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad usada para identificar si el objeto se muestra
    ''' cuando la cantidad son los 3 primero dias da un 50%
    ''' </summary>
    ''' <remarks></remarks>
    Private _LosTresPrimerosDias As Boolean = False
    Public Property LosTresPrimerosDias As Boolean
        Get
            Return _LosTresPrimerosDias
        End Get
        Set(value As Boolean)
            _LosTresPrimerosDias = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad usada para identificar si el objeto se muestra
    ''' cuando la cantidad son todos los dias al 50%
    ''' </summary>
    ''' <remarks></remarks>
    Private _TodosLosDias As Boolean = False
    Public Property TodosLosDias As Boolean
        Get
            Return _TodosLosDias
        End Get
        Set(value As Boolean)
            _TodosLosDias = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad usada para identificar si el objeto se muestra
    ''' cuando No Liquida
    ''' </summary>
    ''' <remarks></remarks>
    Private _NoLiquida As Boolean = False
    Public Property NoLiquida As Boolean
        Get
            Return _NoLiquida
        End Get
        Set(value As Boolean)
            _NoLiquida = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad usada para identificar la cultura 
    ''' </summary>
    ''' <remarks></remarks>
    Private _culture As CultureInfo
    Public Property Culture As CultureInfo
        Get
            Return _culture
        End Get
        Set(value As CultureInfo)
            _culture = value
        End Set
    End Property

End Class

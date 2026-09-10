'***********************************************************************
' Assembly         : Infraestructura.CrossCutting.Base
' Author           : Juan F. Tamayo
' Created          : 2014-03-11
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2014-03-11
' Description      : Conjunto de clases usadas para la generación
'                    de secuencias numericas.
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Provee metodos para generar secuencias numericas
''' </summary>
Public NotInheritable Class Sequense

#Region "Const"

    ''' <summary>
    ''' Constante que indica que el numero pedido al
    ''' al patron de la secuencia es superior
    ''' </summary>
    Public Const ERROR_MAXVALUE As String = "__ERROR_MAXVALUE__"

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una secuencia numerica para un patron y numero dado
    ''' </summary>
    ''' <param name="prefix">Prefijo usado en la construcción de secuencias para bancos y cajas</param>
    ''' <param name="pattern">Patron usado para generar la secuencia</param>
    ''' <param name="num">Numero a representar en la secuencia</param>
    ''' <returns>Secuencia numerica</returns>
    Public Shared Function GetSequense(ByVal prefix As String, ByVal pattern As String, ByVal num As Int64) As String
        If pattern Is Nothing OrElse pattern.Trim().Equals(String.Empty) OrElse num < 0 Then
            Return String.Empty
        End If

        'Definimos la expresion regular de segmentos
        Dim regex As String = "[\#\&]+"
        'Obtenemos la tabla de variables
        Dim vars As Hashtable = CreateVars()
        '1. Reemplazamos todas las variables
        For Each kv As DictionaryEntry In vars
            pattern = pattern.Replace(kv.Key.ToString(), kv.Value.ToString())
        Next
        '2. Obtenemos todos los segmentos incrementables
        Dim regx As New System.Text.RegularExpressions.Regex(regex)
        Dim matches As System.Text.RegularExpressions.MatchCollection = regx.Matches(pattern)
        Dim p As String = String.Empty
        For i As Int32 = 0 To matches.Count - 1
            p &= matches(i).Value
        Next
        If p.Equals(String.Empty) Then
            Return pattern
        End If
        '3. Creamos el segmento base
        Dim s As Segment = Segment.CreateOptimized(p)
        '4. Evaluamos si el numero es valido para representar con el patron de la secuencia
        If Segment.IsValid(p, num) Then
            Dim res As String = s.CalculeOptimized(num)
            '5. Reemplazamos el resultado de la secuencia calculada en el patron
            For i As Int32 = 0 To matches.Count - 1
                pattern = pattern.Substring(0, matches(i).Index) & res.Substring(0, matches(i).Length) & pattern.Substring(matches(i).Index + matches(i).Length)
                res = res.Substring(matches(i).Length)
            Next
            'Aqui se reemplaza el prefijo enviado
            If prefix IsNot Nothing Then
                pattern = System.Text.RegularExpressions.Regex.Replace(pattern, "%pfx", prefix.Trim())
            End If
            Return pattern
        Else
            Return ERROR_MAXVALUE
        End If
    End Function

    ''' <summary>
    ''' Obtiene una secuencia numerica para un patron y numero dado
    ''' </summary>
    ''' <param name="pattern">Patron usado para generar la secuencia</param>
    ''' <param name="num">Numero a representar en la secuencia</param>
    ''' <returns>Secuencia numerica</returns>
    Public Shared Function GetSequense(ByVal pattern As String, ByVal num As Int64) As String
        Return GetSequense(Nothing, pattern, num)
    End Function

    ''' <summary>
    ''' Genera una tabla de variables para reemplazar
    ''' </summary>
    ''' <returns>Tabla de variables</returns>
    Private Shared Function CreateVars() As Hashtable
        Dim vars As New Hashtable()
        vars.Add("%d", DateTime.Now.Day.ToString("00"))
        vars.Add("%M", DateTime.Now.Month.ToString("00"))
        vars.Add("%y", DateTime.Now.Year.ToString())
        vars.Add("%h", DateTime.Now.Hour.ToString("00"))
        vars.Add("%m", DateTime.Now.Minute.ToString("00"))
        vars.Add("%s", DateTime.Now.Second.ToString("00"))

        Return vars
    End Function

#End Region

End Class

''' <summary>
''' Representa un segmento en una secuencia numerica
''' </summary>
Public MustInherit Class Segment

#Region "Fields"

    ''' <summary>
    ''' Segmento padre
    ''' </summary>
    Private _parent As Segment

    ''' <summary>
    ''' Segmento hijo
    ''' </summary>
    Private _child As Segment


    Private _chars As String

    ''' <summary>
    ''' Valor del segmento
    ''' </summary>
    Private _value As String

    ''' <summary>
    ''' Candado de bloqueo
    ''' </summary>
    Private _lockObj As New Object()

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el valor del segmento
    ''' </summary>
    ''' <returns>Valor del segmento</returns>
    Public Property Value As String
        Get
            Return Me._value
        End Get
        Protected Friend Set(value As String)
            Me._value = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el conjunto de caracteres al que pertenece el segmento
    ''' </summary>
    ''' <returns>Conjunto de caracteres al que pertenece el segmento</returns>
    Public Property Chars As String
        Get
            Return Me._chars
        End Get
        Protected Set(value As String)
            Me._chars = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el padre si existe
    ''' </summary>
    ''' <value>Padre del segmento</value>
    ''' <returns>El padre del segmento</returns>
    Protected Property Parent As Segment
        Get
            Return Me._parent
        End Get
        Set(value As Segment)
            Me._parent = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el hijo si existe
    ''' </summary>
    ''' <value>Hijo del segmento</value>
    ''' <returns>El hijo del segmento</returns>
    Protected Property Child As Segment
        Get
            Return Me._child
        End Get
        Set(value As Segment)
            Me._child = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el padre raiz
    ''' </summary>
    ''' <returns>Padre raiz</returns>
    Public ReadOnly Property RootParent As Segment
        Get
            If Me._parent IsNot Nothing Then
                Return Me._parent.RootParent
            Else
                Return Me
            End If
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el hijo raiz
    ''' </summary>
    ''' <returns>Hujo raiz</returns>
    Public ReadOnly Property RootChild As Segment
        Get
            If Me._child IsNot Nothing Then
                Return Me._child.RootChild
            Else
                Return Me
            End If
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el caracter que representa el segmento en la secuencia
    ''' </summary>
    ''' <returns>Caracter usado en la secuencia</returns>
    MustOverride ReadOnly Property Pat As String

    ''' <summary>
    ''' Obtiene el numero que representa la secuencia
    ''' </summary>
    ''' <returns>Numero que representa la secuencia</returns>
    Public Property Number As Long
        Get
            Return Me.Chars.IndexOf(Me.Value)
        End Get
        Protected Friend Set(value As Long)
            Me.Value = Me.Chars(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el producto del total de los hijos
    ''' </summary>
    ''' <param name="result">Referencia al resultado</param>
    Public Sub GetMaxValueChilds(ByRef result As Int64)
        result = (result * Me.Chars.Length)
        If Me.Child IsNot Nothing Then
            Me.Child.GetMaxValueChilds(result)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia numerica usada por el conjunto de segmentos
    ''' </summary>
    ''' <returns>Secuencia numerica usada por el conjunto de segmentos</returns>
    Public ReadOnly Property Sequense As String
        Get
            Dim res As String = String.Empty
            Me.GetSequense(res)
            Return res
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el patron usado en la secuencia numerica
    ''' </summary>
    ''' <returns>Patro usado en la secuencia numerica</returns>
    Public ReadOnly Property Pattern As String
        Get
            Dim res As String = String.Empty
            Me.GetPattern(res)
            Return res
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una instancia de la clase
    ''' </summary>
    ''' <param name="value">Valor del segmento</param>
    ''' <param name="parent">Segmento padre si existe</param>
    ''' <param name="child">Segmento hijo si existe</param>
    Public Sub New(ByVal chars As String, ByVal value As String, Optional ByVal parent As Segment = Nothing, Optional ByVal child As Segment = Nothing)
        Me._parent = parent
        Me._child = child
        Me._value = value
        Me._chars = chars
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Genera el patron de la secuencia numerica
    ''' </summary>
    ''' <param name="pattern">Variable resultado del patron</param>
    Protected Sub GetPattern(ByRef pattern As String)
        If Me._parent IsNot Nothing Then
            Me._parent.GetPattern(pattern)
        End If
        pattern &= Me.Pat
    End Sub

    ''' <summary>
    ''' Genera la secuencia numerica usada por el conjunto de segmentos
    ''' </summary>
    ''' <param name="seq">Variable resultado de la secuencia</param>
    Protected Sub GetSequense(ByRef seq As String)
        If Me._parent IsNot Nothing Then
            Me._parent.GetSequense(seq)
        End If
        seq &= Me._value
    End Sub

    ''' <summary>
    ''' Incrementa su valor
    ''' </summary>
    MustOverride Sub Increment()

    ''' <summary>
    ''' Incrementa su valor
    ''' </summary>
    ''' <param name="toNumber">Incrementa los segmentos hasta el numero</param>
    Public Sub Up(ByVal originalNumber As Int64, ByVal toNumber As Long)
        Dim maxVal = (Me.GetMaxValue())
        Dim resInt = toNumber \ maxVal
        Dim resMod = (toNumber Mod maxVal)

        If Me.Child IsNot Nothing Then
            If resInt = 0 Then
                Me.Child.Up(originalNumber, toNumber)
            Else 'Si existe parte entera
                If Me.Parent IsNot Nothing Then
                    Me.Parent.Number = resInt
                    Me.Child.Up(originalNumber, resMod)
                ElseIf resMod = 0 Then
                    'El numero es igual
                Else
                    'El numero es mas grande de lo que puedo representar
                End If
            End If
        Else 'Si es el ultimo
            If Me.Parent IsNot Nothing Then
                Me.Parent.Number = resInt
            End If
            Dim currentNumber = Me.RootParent.GetMaxCurrentValue()
            Me.Number = (originalNumber - currentNumber)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el numero maximo que se puede representar
    ''' hasta el valor que se tiene
    ''' </summary>
    ''' <returns>Numero maximo que se puede representar</returns>
    Public Function GetMaxCurrentValue() As Int64
        If Me.Child IsNot Nothing Then
            Dim res As Int64 = Me.Number
            Me.Child.GetMaxValueChilds(res)
            res += Me.Child.GetMaxCurrentValue()
            Return res
        Else
            Return (Me.Number)
        End If
    End Function

    ''' <summary>
    ''' Obtiene el numero maximo que se puede representar
    ''' </summary>
    ''' <returns>Numero maximo que se puede representar</returns>
    Public Function GetMaxValue() As Int64
        If Me.Child IsNot Nothing Then
            Return (Me.Chars.Length * Me.Child.GetMaxValue())
        Else
            Return (Me.Chars.Length)
        End If
    End Function

    ''' <summary>
    ''' Calcula la secuencia de numerica para un numero dado
    ''' </summary>
    ''' <param name="number">Número a calcular</param>
    ''' <returns>Secuencia numerica</returns>
    Public Function Calcule(ByVal number As Int64) As String
        Dim res As String = String.Empty
        If Segment.IsValid(Me.Pattern, number) Then
            For i As Int64 = 1 To number
                Me.Increment()
            Next
            res = Me.Sequense
        End If
        Return res
    End Function

    ''' <summary>
    ''' Calcula la secuencia de numerica para un numero dado
    ''' </summary>
    ''' <param name="number">Número a calcular</param>
    ''' <returns>Secuencia numerica</returns>
    Public Function CalculeOptimized(ByVal number As Int64) As String
        Dim res As String = String.Empty
        If Segment.IsValid(Me.RootChild.Pattern, number) Then
            Me.Up(number, number)
            res = Me.RootChild.Sequense
        End If
        Return res
    End Function

#End Region

#Region "Shared"

    ''' <summary>
    ''' Crea un segmento a partir de un patron de secuencia
    ''' </summary>
    ''' <param name="pattern">Patron de secuencia</param>
    ''' <returns>Segmento creado</returns>
    Public Shared Function Create(ByVal pattern As String) As Segment
        Dim lst As New List(Of Segment)()
        For i As Int32 = 0 To pattern.Length - 1
            If i = 0 Then
                If pattern(i).ToString().Equals("&") Then
                    lst.Add(New LetterSegment())
                ElseIf pattern(i).ToString().Equals("#") Then
                    lst.Add(New NumberSegment())
                End If
            Else
                If pattern(i).ToString().Equals("&") Then
                    Dim n As New LetterSegment()
                    Dim s As Segment = lst(lst.Count - 1)
                    s.Child = n
                    n.Parent = s
                    lst.Add(n)
                ElseIf pattern(i).ToString().Equals("#") Then
                    Dim n As New NumberSegment()
                    Dim s As Segment = lst(lst.Count - 1)
                    s.Child = n
                    n.Parent = s
                    lst.Add(n)
                End If
            End If
        Next
        Return lst(lst.Count - 1)
    End Function

    ''' <summary>
    ''' Crea un segmento a partir de un patron de secuencia
    ''' </summary>
    ''' <param name="pattern">Patron de secuencia</param>
    ''' <returns>Segmento creado</returns>
    Public Shared Function CreateOptimized(ByVal pattern As String) As Segment
        Dim lst As New List(Of Segment)()
        For i As Int32 = 0 To pattern.Length - 1
            If i = 0 Then
                If pattern(i).ToString().Equals("&") Then
                    lst.Add(New LetterSegment())
                ElseIf pattern(i).ToString().Equals("#") Then
                    lst.Add(New NumberSegment())
                End If
            Else
                If pattern(i).ToString().Equals("&") Then
                    Dim n As New LetterSegment()
                    Dim s As Segment = lst(lst.Count - 1)
                    s.Child = n
                    n.Parent = s
                    lst.Add(n)
                ElseIf pattern(i).ToString().Equals("#") Then
                    Dim n As New NumberSegment()
                    Dim s As Segment = lst(lst.Count - 1)
                    s.Child = n
                    n.Parent = s
                    lst.Add(n)
                End If
            End If
        Next
        Return lst(0)
    End Function

    ''' <summary>
    ''' Obtiene el numero maximo que puede ser representado por un patron
    ''' </summary>
    ''' <param name="pattern">Patron a evaluar</param>
    ''' <returns>Numero maximo que puede ser representado</returns>
    Public Shared Function GetMax(ByVal pattern As String) As Int64
        If pattern Is Nothing OrElse pattern.Trim().Equals(String.Empty) Then
            Return 0
        End If
        Dim max As Int64 = 1
        For Each c As Char In pattern.Trim()
            If c.ToString().Equals("&") Then
                max *= 26
            ElseIf c.ToString().Equals("#") Then
                max *= 10
            End If
        Next
        Return (max - 1)
    End Function

    ''' <summary>
    ''' Calcula el maximo numero aceptado por el patron de la secuencia
    ''' y valida que el numero dado no sea mayor
    ''' </summary>
    ''' <param name="pattern">Patro de secuencia</param>
    ''' <param name="number">Numero a validar</param>
    ''' <returns>Valor que indica si el numero se encuentra en el rango de la secuencia</returns>
    Public Shared Function IsValid(ByVal pattern As String, number As Int64) As Boolean
        If pattern Is Nothing OrElse pattern.Trim().Equals(String.Empty) OrElse number < 0 Then
            Return False
        End If
        Return Segment.GetMax(pattern.Trim()) >= number
    End Function

#End Region

End Class

''' <summary>
''' Representa un segmento de tipo letra en una secuencia numerica
''' </summary>
Public Class LetterSegment
    Inherits Segment

#Region "Consts"

    ''' <summary>
    ''' Caracter que representa el segmento en la secuencia
    ''' </summary>
    Private Const C As String = "&"

#End Region

#Region "Properties"

    ''' <summary>
    ''' <see cref="Segment.Pat" />
    ''' </summary>
    ''' <returns><see cref="Segment.Pat" /></returns>
    Public Overrides ReadOnly Property Pat As String
        Get
            Return C
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="parent">Segmento padre si existe</param>
    ''' <param name="child">Segmento hijo si existe</param>
    Public Sub New(Optional ByVal parent As Segment = Nothing, Optional ByVal child As Segment = Nothing)
        MyBase.New("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "A", parent, child)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' <see cref="Segment.Increment" />
    ''' </summary>
    Public Overrides Sub Increment()
        If Me.Value.Equals("Z") Then
            If Me.Parent IsNot Nothing Then
                Me.Parent.Increment()
            End If
            Me.Value = "A"
        Else
            Dim index As Int32 = (Me.Chars.IndexOf(Me.Value) + 1)
            Me.Value = Me.Chars(index).ToString()
        End If
    End Sub

#End Region

End Class

''' <summary>
''' Representa un segmento de tipo numero en una secuencia numerica
''' </summary>
Public Class NumberSegment
    Inherits Segment

#Region "Consts"

    ''' <summary>
    ''' Caracter que representa el segmento en la secuencia
    ''' </summary>
    Private Const C As String = "#"

#End Region

#Region "Properties"

    ''' <summary>
    ''' <see cref="Segment.Pat" />
    ''' </summary>
    ''' <returns><see cref="Segment.Pat" /></returns>
    Public Overrides ReadOnly Property Pat As String
        Get
            Return C
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="parent">Segmento padre si existe</param>
    ''' <param name="child">Segmento hijo si existe</param>
    Public Sub New(Optional ByVal parent As Segment = Nothing, Optional ByVal child As Segment = Nothing)
        MyBase.New("0123456789", "0", parent, child)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' <see cref="Segment.Increment" />
    ''' </summary>
    Public Overrides Sub Increment()
        If Me.Value.Equals("9") Then
            If Me.Parent IsNot Nothing Then
                Me.Parent.Increment()
            End If
            Me.Value = "0"
        Else
            Dim index As Int32 = (Me.Chars.IndexOf(Me.Value) + 1)
            Me.Value = Me.Chars(index).ToString()
        End If
    End Sub

#End Region

End Class
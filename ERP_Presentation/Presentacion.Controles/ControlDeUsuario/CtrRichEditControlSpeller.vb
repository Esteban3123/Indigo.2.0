Imports System.Text.RegularExpressions
Imports DevExpress.Office.Utils
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraSpellChecker
Imports System.Globalization
Imports System.Threading.Tasks
Imports Presentation.Base
Imports System.ComponentModel

''' <summary>
''' Control de usuario RichEditControl con diccionario
''' </summary>
Public Class CtrRichEditControlSpeller

#Region "Fields"

    Private hash As New Dictionary(Of String, String)
    Private wordControl As String = String.Empty
    Private wordList As New List(Of String)(New String() {" ", ",", ";", "+", "-", "_", "enter", "*", ".", " "})
    Private restringWordList As New List(Of String)(New String() {"[", "]", "(", ")"})
    Private controlEnter As Integer = 0
    Private posic As DocumentPosition
    Private secondRichEdit As DevExpress.XtraRichEdit.RichEditControl = New DevExpress.XtraRichEdit.RichEditControl
    Dim posPat As Integer
    Dim register As String = String.Empty
    Dim index As DocumentRange

#End Region

#Region "Properties"

    Private _languageDictionary As LanguageDictionaries
    ''' <summary>
    ''' Propiedad que obtiene o establece el idioma que se va a aplicar como diccionario
    ''' </summary>
    <DefaultValue(LanguageDictionaries.Spanish)> _
    Public Property Diccionario As LanguageDictionaries
        Get
            Return _languageDictionary
        End Get
        Set(value As LanguageDictionaries)
            _languageDictionary = value
        End Set
    End Property


    Private _barControl As DevExpress.XtraLayout.Utils.LayoutVisibility
    ''' <summary>
    ''' Propiedad que obtiene o establece el estado visible de la barra de controles
    ''' </summary>
    <DefaultValue(DevExpress.XtraLayout.Utils.LayoutVisibility.Always)> _
    Public Property BarraControlesVisible As DevExpress.XtraLayout.Utils.LayoutVisibility
        Get
            Return _barControl
        End Get
        Set(value As DevExpress.XtraLayout.Utils.LayoutVisibility)
            _barControl = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo publico para cargar las siglas a validar
    ''' </summary>
    ''' <param name="listWords"></param>
    Public Sub LoadWords(listWords As Dictionary(Of String, String))
        hash = listWords
    End Sub

    ''' <summary>
    ''' Evento para cargar los diccionarios según idioma
    ''' </summary>
    Private Sub LoadDictionaries()

        If Diccionario = LanguageDictionaries.Spanish Then

            Dim currentDirectory As String = System.IO.Directory.GetCurrentDirectory()
            Dim spanishDic As SpellCheckerOpenOfficeDictionary = New SpellCheckerOpenOfficeDictionary()
            spanishDic.AlphabetPath = currentDirectory & "\Dictionaries\Alphabet.txt"
            spanishDic.DictionaryPath = currentDirectory & "\Dictionaries\es_MX.dic"
            spanishDic.GrammarPath = currentDirectory & "\Dictionaries\es_MX.aff"
            spanishDic.Encoding = New System.Text.UTF8Encoding
            spanishDic.Culture = New CultureInfo("es-MX")
            SpellChecker1.Dictionaries.Add(spanishDic)

        ElseIf Diccionario = LanguageDictionaries.English Then

            Dim currentDirectory As String = System.IO.Directory.GetCurrentDirectory()
            Dim englishDic As SpellCheckerOpenOfficeDictionary = New SpellCheckerOpenOfficeDictionary()
            englishDic.AlphabetPath = currentDirectory & "\Dictionaries\EnglishAlphabet.txt"
            englishDic.DictionaryPath = currentDirectory & "\Dictionaries\american.xlg"
            englishDic.GrammarPath = currentDirectory & "\Dictionaries\english.aff"
            englishDic.Encoding = New System.Text.UTF8Encoding
            englishDic.Culture = New CultureInfo("en-US")
            SpellChecker1.Dictionaries.Add(englishDic)

        End If

        SpellChecker1.Culture = CultureInfo.InvariantCulture
        SpellChecker1.SpellCheckMode = SpellCheckMode.AsYouType

    End Sub

    ''' <summary>
    ''' Metodo pegar
    ''' </summary>
    Private Sub PasteMethod()
        wordControl = " "
        RichEditControl1.Paste()
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento del spellChecker antes de verificar una palabra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SpellChecker1_BeforeCheckWord(sender As Object, e As DevExpress.XtraSpellChecker.BeforeCheckWordEventArgs) Handles SpellChecker1.BeforeCheckWord
        Dim word As String = e.Word.ToUpper

        If wordList.Contains(wordControl) Then
            word = e.Word.ToUpper & " "
        ElseIf sender Is Nothing Then
            word = e.Word.ToUpper & " "
        End If

        If hash.ContainsKey(word) Then

            Dim value As String = hash(word)
            Dim start1 As New List(Of Integer)

            If RichEditControl1.InvokeRequired Then

                If RichEditControl1.BeginInvoke(Sub()

                                                    Dim newText As String
                                                    Dim length As String = word.Length

                                                    Dim regular As New Regex("(&nbsp;|)\b(" & e.Word.ToUpper & "|" & e.Word.ToLower & "|" & e.Word & ")\b([\*;,._\t\r\n-]|(&nbsp;)|)")

                                                    If wordControl = "enter" Then

                                                        newText = regular.Replace(RichEditControl1.HtmlText, " 012__012")
                                                        If Not newText.Contains("012__012") Then
                                                            Exit Sub
                                                        End If
                                                    Else
                                                        newText = regular.Replace(RichEditControl1.HtmlText, " 99__")
                                                        If Not newText.Contains("99__") Then
                                                            Exit Sub
                                                        End If
                                                    End If

                                                    If wordControl = "enter" Then
                                                        RichEditControl1.HtmlText = newText
                                                        Dim singleRegular As New Regex("(012__012)")
                                                        posPat = RichEditControl1.Text.LastIndexOf("012__012")
                                                        Dim findWord As DocumentRange() = RichEditControl1.Document.FindAll(singleRegular)
                                                        Dim newRange As DocumentRange = Nothing
                                                        For Each itemRange In findWord
                                                            newRange = itemRange
                                                        Next

                                                        Dim tam = value.Length - 8

                                                        Dim otherText As String = singleRegular.Replace(RichEditControl1.HtmlText, value)
                                                        posPat = posPat + value.Length
                                                        RichEditControl1.HtmlText = otherText
                                                        RichEditControl1.BeginUpdate()
                                                        Dim myStart As DocumentPosition = RichEditControl1.Document.CreatePosition(newRange.End.ToInt + tam)
                                                        Dim myRange2 As DocumentRange = RichEditControl1.Document.CreateRange(myStart, 0)
                                                        RichEditControl1.Document.Selection = myRange2
                                                        RichEditControl1.EndUpdate()
                                                    Else
                                                        RichEditControl1.HtmlText = newText
                                                        Dim singleRegular As New Regex("(99__)")
                                                        posPat = RichEditControl1.Text.LastIndexOf("99__")

                                                        Dim findWord As DocumentRange() = RichEditControl1.Document.FindAll(singleRegular)
                                                        Dim newRange As DocumentRange = Nothing
                                                        For Each itemRange In findWord
                                                            newRange = itemRange
                                                        Next

                                                        Dim tam = (value & wordControl).Length - 4

                                                        Dim otherText As String = singleRegular.Replace(RichEditControl1.HtmlText, value & wordControl)
                                                        RichEditControl1.HtmlText = otherText
                                                        posPat = posPat + (value & wordControl).Length
                                                        RichEditControl1.BeginUpdate()
                                                        Dim myStart2 As DocumentPosition = RichEditControl1.Document.CreatePosition(newRange.End.ToInt + tam)
                                                        Dim myRange2 As DocumentRange = RichEditControl1.Document.CreateRange(myStart2, 0)
                                                        RichEditControl1.Document.Selection = myRange2
                                                        RichEditControl1.EndUpdate()
                                                    End If
                                                    wordControl = String.Empty
                                                    register = String.Empty
                                                End Sub).IsCompleted() Then
                    wordControl = String.Empty
                    register = String.Empty

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Evento al cargar el control de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CtrRichEditControlSpeller_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.INDLycBarControl.Visibility = BarraControlesVisible
        LoadDictionaries()
    End Sub

    ''' <summary>
    ''' Evento al oprimir una tecla
    ''' </summary>
    Private Sub RichEditControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichEditControl1.KeyDown
        If (e.Control AndAlso e.KeyCode = Keys.V) Or e.KeyCode = Keys.Space Then
            wordControl = " "
        ElseIf e.KeyCode = Keys.Left Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Right Or
                    e.KeyCode = Keys.Down Then
            If hash.ContainsKey(register.ToUpper & " ") Then

                wordControl = " "
                Dim rangeFind = index
                Dim control = rangeFind.Contains(RichEditControl1.Document.CaretPosition)

                For a As Integer = rangeFind.Start.ToInt To rangeFind.End.ToInt
                    If a = RichEditControl1.Document.CaretPosition.ToInt Then
                        Exit Sub
                    End If
                Next
                Dim eventArgs As New DevExpress.XtraSpellChecker.BeforeCheckWordEventArgs(Nothing, register, True)
                Task.Factory.StartNew(Sub(x)
                                          SpellChecker1_BeforeCheckWord(Nothing, eventArgs)
                                      End Sub, Nothing)
            End If
        ElseIf e.KeyCode = Keys.Enter Then
            wordControl = "enter"
        ElseIf e.Shift = False And e.KeyCode = Keys.Oemcomma Then
            wordControl = ","
        ElseIf e.Shift = False And e.KeyCode = Keys.OemPeriod Then
            wordControl = "."
        ElseIf e.Shift = False And e.KeyCode = Keys.Oemplus Then
            wordControl = "+"
        ElseIf e.Shift = False And e.KeyCode = Keys.OemMinus Then
            wordControl = "-"
        ElseIf e.Shift AndAlso e.KeyCode = Keys.Oemplus Then
            wordControl = "*"
        ElseIf e.Shift AndAlso e.KeyCode = Keys.OemMinus Then
            wordControl = "_"
        ElseIf e.Shift AndAlso e.KeyCode = Keys.Oemcomma Then
            wordControl = ";"
        ElseIf e.KeyCode = Keys.Tab Then
            wordControl = " "
        Else
            wordControl = e.KeyValue.ToString()
        End If
    End Sub

    ''' <summary>
    ''' Evento al mostrar el menu del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RichEditControl1_PopupMenuShowing(sender As Object, e As DevExpress.XtraRichEdit.PopupMenuShowingEventArgs) Handles RichEditControl1.PopupMenuShowing
        Dim menuDx = New DevExpress.Utils.Menu.DXMenuItem("Pegar", AddressOf pasteMethod, ImageCollection1.Images.Item(0))
        Dim menuDxRemove As DevExpress.Utils.Menu.DXMenuItem = Nothing
        Dim position As Integer = 0
        For Each item As DevExpress.Utils.Menu.DXMenuItem In e.Menu.Items
            If item.Caption = "Pegar" Then
                menuDxRemove = item
                Exit For
            End If
            position = position + 1
        Next
        If menuDxRemove IsNot Nothing Then
            e.Menu.Items.Remove(menuDxRemove)
            e.Menu.Items.Insert(position, menuDx)
        End If

    End Sub

    ''' <summary>
    ''' Evento al capturar el click del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RichEditControl1_MouseCaptureChanged(sender As Object, e As EventArgs) Handles RichEditControl1.MouseCaptureChanged
        If hash.ContainsKey(register.ToUpper & " ") Then
            wordControl = " "

            Dim rangeFind = index
            Dim control = rangeFind.Contains(RichEditControl1.Document.CaretPosition)

            If control Then
                Exit Sub
            End If

            Dim eventArgs As New DevExpress.XtraSpellChecker.BeforeCheckWordEventArgs(Nothing, register, True)
            Task.Factory.StartNew(Sub(x)
                                      SpellChecker1_BeforeCheckWord(Nothing, eventArgs)
                                  End Sub, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Evento al perder el foco el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RichEditControl1_Leave(sender As Object, e As EventArgs) Handles RichEditControl1.Leave
        If hash.ContainsKey(register.ToUpper & " ") Then
            wordControl = " "
            Dim eventArgs As New DevExpress.XtraSpellChecker.BeforeCheckWordEventArgs(Nothing, register, True)
            Task.Factory.StartNew(Sub(x)
                                      SpellChecker1_BeforeCheckWord(Nothing, eventArgs)
                                  End Sub, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Evento que revisa la ultima frase introducida en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SpellChecker1_BeforeCheck(sender As Object, e As BeforeCheckEventArgs) Handles SpellChecker1.BeforeCheck

        Dim regular As New Regex("\s")
        Dim splitData As String() = regular.Split(e.Text)
        For Each item As String In splitData

            If hash.ContainsKey(item.ToUpper & " ") Then

                Dim regularExp As New Regex("\b(" & item.ToUpper & "|" & item.ToLower & "|" & item & ")\b")
                Dim listRanges As DocumentRange() = RichEditControl1.Document.FindAll(regularExp)

                For Each item1 As DocumentRange In listRanges
                    index = item1
                Next
                register = item.ToUpper
            End If
        Next
    End Sub
#End Region

End Class

''' <summary>
''' Clase para cargar las siglas a validar
''' </summary>
Public Class DataRichEditWords
    Property key As String
    Property value As String
End Class

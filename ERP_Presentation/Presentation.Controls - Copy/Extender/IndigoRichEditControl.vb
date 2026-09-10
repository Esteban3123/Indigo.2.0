Imports System.ComponentModel
Imports DevExpress.XtraRichEdit
Imports DevExpress.CodeParser
Imports DevExpress.LookAndFeel
Imports DevExpress.Skins
Imports DevExpress.XtraRichEdit.Services
Imports DevExpress.Office
Imports DevExpress.XtraRichEdit.API.Native
Imports System.Text

<ProvideProperty("IsCodeParser", GetType(RichEditControl))> _
<ProvideProperty("Language", GetType(RichEditControl))> _
<ProvideProperty("Keywords", GetType(RichEditControl))>
Public Class IndigoRichEditControl
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Fields"

    ''' <summary>
    ''' Tabla hash que contiene los controles a extender
    ''' </summary>
    Private _hashTable As Hashtable
    ''' <summary>
    ''' Contenedor
    ''' </summary>
    Private _container As IContainer

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de <see cref="IndigoRichEditControl" />
    ''' </summary>
    Public Sub New()
        Me._hashTable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de <see cref="IndigoRichEditControl" />
    ''' </summary>
    ''' <param name="container">Contenedor</param>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()
        If container IsNot Nothing Then
            container.Add(Me)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el componente
    ''' </summary>
    Private Sub InitializeComponent()
        Me._container = New Container()
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Encapsula las propiedades extendidas del control
    ''' </summary>
    Private Class ExtendedProperty

        ''' <summary>
        ''' Obtiene o asigna un valor que indica si el control realiza
        ''' analisis de sintaxis en un lenguaje especifico
        ''' </summary>
        ''' <value>Valor que indica si el control analiza la sintaxis</value>
        ''' <returns>El valor que indica si el control analiza la sintaxis</returns>
        Public IsCodeParser As Boolean

        ''' <summary>
        ''' Obtiene o asigna el lenguaje que se va a analizar
        ''' </summary>
        ''' <value>Lenguaje a analizar</value>
        ''' <returns>El lenguaje a analizar</returns>
        Public Language As DevExpress.CodeParser.ParserLanguageID

        ''' <summary>
        ''' Obtiene o asigna una lista de palabras reservadas a tener en cuenta
        ''' </summary>
        ''' <value>Lista de palabras</value>
        ''' <returns>La lista de palabras</returns>
        Public Keywords As List(Of RichEditControlString)

    End Class

    ''' <summary>
    ''' Obtiene un valor que indica si el control es un analizador de sintaxis
    ''' </summary>
    ''' <param name="obj">Objeto de tipo <see cref="DevExpress.XtraRichEdit.RichEditControl" /></param>
    ''' <returns>Un valor que indica si el control es un analizador de sintaxis</returns>
    Public Function GetIsCodeParser(ByVal obj As RichEditControl) As Boolean
        Return Me.EnsurePropertiesExists(obj).IsCodeParser
    End Function

    ''' <summary>
    ''' Asigna un valor que indica si el control es un analizador de sintaxis
    ''' </summary>
    ''' <param name="obj">Objeto de tipo <see cref="DevExpress.XtraRichEdit.RichEditControl" /></param>
    Public Sub SetIsCodeParser(ByVal obj As RichEditControl, ByVal value As Boolean)
        Me.EnsurePropertiesExists(obj).IsCodeParser = value
    End Sub

    ''' <summary>
    ''' Obtiene el lenguaje a analizar
    ''' </summary>
    ''' <param name="obj">Objeto de tipo <see cref="DevExpress.XtraRichEdit.RichEditControl" /></param>
    ''' <returns>El lenguaje a analizar</returns>
    Public Function GetLanguage(ByVal obj As RichEditControl) As DevExpress.CodeParser.ParserLanguageID
        Return Me.EnsurePropertiesExists(obj).Language
    End Function

    ''' <summary>
    ''' Asigna el lenguaje a analizar
    ''' </summary>
    ''' <param name="obj">Objeto de tipo <see cref="DevExpress.XtraRichEdit.RichEditControl" /></param>
    Public Sub SetLanguage(ByVal obj As RichEditControl, ByVal value As DevExpress.CodeParser.ParserLanguageID)
        Me.EnsurePropertiesExists(obj).Language = value
    End Sub

    ''' <summary>
    ''' Obtiene una lista de palabras reservadas a tener en cuenta
    ''' </summary>
    ''' <param name="obj">Objeto de tipo <see cref="DevExpress.XtraRichEdit.RichEditControl" /></param>
    ''' <returns>La lista de palabras reservadas</returns>
    Public Function GetKeywords(ByVal obj As RichEditControl) As List(Of RichEditControlString)
        Return Me.EnsurePropertiesExists(obj).Keywords
    End Function

    ''' <summary>
    ''' Asigna un lista de palabras reservadas a tener en cuenta
    ''' </summary>
    ''' <param name="obj">Objeto de tipo <see cref="DevExpress.XtraRichEdit.RichEditControl" /></param>
    ''' <param name="value">Lista de palabras reservadas</param>
    Public Sub SetKeywords(ByVal obj As RichEditControl, ByVal value As List(Of RichEditControlString))
        Me.EnsurePropertiesExists(obj).Keywords = value
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene o asigna las propiedades extendidas del control
    ''' </summary>
    ''' <param name="key">Clave que indica el control del cual se retorna las propiedades</param>
    ''' <returns>Objeto de tipo <see cref="ExtendedProperty" /> que encapsula las propiedades el control</returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As ExtendedProperty
        Dim p As ExtendedProperty = DirectCast(Me._hashTable(key), ExtendedProperty)
        If p Is Nothing Then
            p = New ExtendedProperty()
            p.IsCodeParser = False
            p.Keywords = New List(Of RichEditControlString)()
            p.Language = ParserLanguageID.Basic
            Me._hashTable(key) = p
        End If
        Return p
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el control puede ser extendido
    ''' </summary>
    ''' <param name="extendee">Control a extender</param>
    ''' <returns>Valor que indica si el control puede ser extendido</returns>
    Public Function CanExtend(extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
        If TypeOf (extendee) Is RichEditControl Then
            Return True
        End If
        Return False
    End Function

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub

    Public Sub EndInit() Implements ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Me._hashTable
            Dim p As RichEditControl = TryCast(de.Key, RichEditControl)
            If Me.EnsurePropertiesExists(p).IsCodeParser Then
                p.ReplaceService(Of ISyntaxHighlightService)(New MySyntaxHighlightService(p, Me.EnsurePropertiesExists(p).Language, Me.EnsurePropertiesExists(p).Keywords))
            End If
        Next
    End Sub

#End Region

End Class

''' <summary>
'''  This class implements the Execute method of the ISyntaxHighlightService interface to parse and colorize the text.
''' </summary>
Public Class MySyntaxHighlightService
    Implements ISyntaxHighlightService
    Private ReadOnly syntaxEditor As RichEditControl
    Private syntaxColors As SyntaxColors
    Private commentProperties As SyntaxHighlightProperties
    Private keywordProperties As SyntaxHighlightProperties
    Private stringProperties As SyntaxHighlightProperties
    Private xmlCommentProperties As SyntaxHighlightProperties
    Private textProperties As SyntaxHighlightProperties

    Private _myKeyWords As List(Of RichEditControlString)
    Private _language As DevExpress.CodeParser.ParserLanguageID = ParserLanguageID.Basic

    Public Sub New(ByVal syntaxEditor As RichEditControl, ByVal language As DevExpress.CodeParser.ParserLanguageID, ByVal myKeywords As List(Of RichEditControlString))
        Me.syntaxEditor = syntaxEditor
        Me._myKeyWords = myKeywords
        Me._language = language
        syntaxColors = New SyntaxColors(UserLookAndFeel.Default)
        Execute()
    End Sub

    ''' <summary>
    ''' Obtiene o asigna el lenguaje a analizar
    ''' </summary>
    ''' <value>Lenguaje</value>
    ''' <returns>El lenguaje</returns>
    Public Property Language As DevExpress.CodeParser.ParserLanguageID
        Get
            Return Me._language
        End Get
        Set(value As DevExpress.CodeParser.ParserLanguageID)
            Me._language = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de palabras reservadas
    ''' </summary>
    ''' <value>Lista de palabras reservadas</value>
    ''' <returns>La lista de palabras reservadas</returns>
    Public Property MyKeyWords As List(Of RichEditControlString)
        Get
            Return Me._myKeyWords
        End Get
        Set(value As List(Of RichEditControlString))
            Me._myKeyWords = value
        End Set
    End Property

    Private Sub HighlightSyntax(ByVal tokens As TokenCollection)
        commentProperties = New SyntaxHighlightProperties()
        commentProperties.ForeColor = syntaxColors.CommentColor

        keywordProperties = New SyntaxHighlightProperties()
        keywordProperties.ForeColor = syntaxColors.KeywordColor

        stringProperties = New SyntaxHighlightProperties()
        stringProperties.ForeColor = syntaxColors.StringColor

        xmlCommentProperties = New SyntaxHighlightProperties()
        xmlCommentProperties.ForeColor = syntaxColors.XmlCommentColor

        textProperties = New SyntaxHighlightProperties()
        textProperties.ForeColor = syntaxColors.TextColor

        If tokens Is Nothing OrElse tokens.Count = 0 Then
            Return
        End If

        Dim document As Document = syntaxEditor.Document
        'CharacterProperties cp = document.BeginUpdateCharacters(0, 1);
        Dim syntaxTokens As New List(Of SyntaxHighlightToken)(tokens.Count)
        For Each token As Token In tokens
            HighlightCategorizedToken(CType(token, CategorizedToken), syntaxTokens)
        Next token
        document.ApplySyntaxHighlight(syntaxTokens)
        'document.EndUpdateCharacters(cp);
    End Sub


    Private Sub HighlightCategorizedToken(ByVal token As CategorizedToken, ByVal syntaxTokens As List(Of SyntaxHighlightToken))
        Dim backColor As Color = syntaxEditor.ActiveView.BackColor
        Dim category As TokenCategory = token.Category
        If category = TokenCategory.Comment Then
            syntaxTokens.Add(SetTokenColor(token, commentProperties, backColor))
        ElseIf category = TokenCategory.Keyword Then
            syntaxTokens.Add(SetTokenColor(token, keywordProperties, backColor))
        ElseIf category = TokenCategory.String Then
            syntaxTokens.Add(SetTokenColor(token, stringProperties, backColor))
        ElseIf category = TokenCategory.XmlComment Then
            syntaxTokens.Add(SetTokenColor(token, xmlCommentProperties, backColor))
        Else
            syntaxTokens.Add(SetTokenColor(token, textProperties, backColor))
        End If
    End Sub
    Private Function SetTokenColor(ByVal token As Token, ByVal foreColor As SyntaxHighlightProperties, ByVal backColor As Color) As SyntaxHighlightToken
        If syntaxEditor.Document.Paragraphs.Count < token.Range.Start.Line Then
            Return Nothing
        End If
        Dim paragraphStart As Integer = DocumentHelper.GetParagraphStart(syntaxEditor.Document.Paragraphs(token.Range.Start.Line - 1))
        Dim tokenStart As Integer = paragraphStart + token.Range.Start.Offset - 1
        If token.Range.End.Line <> token.Range.Start.Line Then
            paragraphStart = DocumentHelper.GetParagraphStart(syntaxEditor.Document.Paragraphs(token.Range.End.Line - 1))
        End If

        Dim tokenEnd As Integer = paragraphStart + token.Range.End.Offset - 1
        Debug.Assert(tokenEnd > tokenStart)
        Return New SyntaxHighlightToken(tokenStart, tokenEnd - tokenStart, foreColor)
    End Function

#Region "#ISyntaxHighlightServiceMembers"
    Public Sub Execute() Implements ISyntaxHighlightService.Execute
        'Obtenemos el texto de el control rich text
        Dim newText As String = syntaxEditor.Text
        ''Determine language by file extension.
        'Dim ext As String = System.IO.Path.GetExtension(syntaxEditor.Options.DocumentSaveOptions.CurrentFileName)
        'Dim lang_ID As ParserLanguageID = ParserLanguage.FromFileExtension(ext)
        '' Do not parse HTML or XML.
        'If lang_ID = ParserLanguageID.Html OrElse lang_ID = ParserLanguageID.Xml OrElse lang_ID = ParserLanguageID.None Then
        '    Return
        'End If


        'Use DevExpress.CodeParser to parse text into tokens.
        'obtengo el lenguaje con que se parseara el codigo
        Dim tokenHelper As ITokenCategoryHelper = TokenCategoryHelperFactory.CreateHelper(ParserLanguageID.Basic)
        'declaramos la variable q contendra el tokencollection
        Dim highlightTokens As TokenCollection
        'obtenemos los token collection
        highlightTokens = tokenHelper.GetTokens(newText)
        'aplicamos los colores de la sintaxis
        HighlightSyntax(highlightTokens)
        'se dispara para buscar las palabras personalizadas
        syntaxEditor.Document.ApplySyntaxHighlight(ParseTokens())
    End Sub

    ''' <summary>
    ''' Metodo para encontrar las palabras personalizadas
    ''' </summary>
    Private Function ParseTokens() As List(Of SyntaxHighlightToken)
        Dim tokens As New List(Of SyntaxHighlightToken)()
        Dim ranges() As DocumentRange = Nothing
        'Dim variables() As String = {"SUELDO", "RETENCION", "PRIMA", "VACACIONES"}
        Dim variablesSettings As New SyntaxHighlightProperties() With {.ForeColor = Color.Blue}

        For i As Integer = 0 To Me._myKeyWords.Count - 1
            ranges = syntaxEditor.Document.FindAll(Me._myKeyWords(i).ToString(), SearchOptions.CaseSensitive Or SearchOptions.WholeWord)

            For j As Integer = 0 To ranges.Length - 1
                If (Not IsRangeInTokens(ranges(j), tokens)) Then
                    tokens.Add(New SyntaxHighlightToken(ranges(j).Start.ToInt(), ranges(j).Length, variablesSettings))
                End If
            Next j
        Next i

        Return tokens
    End Function


    Private Function IsRangeInTokens(ByVal range As DocumentRange, ByVal tokens As List(Of SyntaxHighlightToken)) As Boolean
        For i As Integer = 0 To tokens.Count - 1
            If range.Start.ToInt() >= tokens(i).Start AndAlso range.End.ToInt() <= tokens(i).End Then
                Return True
            End If
        Next i
        Return False
    End Function



    Public Sub ForceExecute() Implements ISyntaxHighlightService.ForceExecute
        Execute()
    End Sub
#End Region ' #ISyntaxHighlightServiceMembers
End Class

''' <summary>
'''  This class provides colors to highlight the tokens.
''' </summary>
Public Class SyntaxColors
    Private Shared ReadOnly Property DefaultCommentColor() As Color
        Get
            Return Color.Green
        End Get
    End Property
    Private Shared ReadOnly Property DefaultKeywordColor() As Color
        Get
            Return Color.Blue
        End Get
    End Property
    Private Shared ReadOnly Property DefaultStringColor() As Color
        Get
            Return Color.Brown
        End Get
    End Property
    Private Shared ReadOnly Property DefaultXmlCommentColor() As Color
        Get
            Return Color.Gray
        End Get
    End Property
    Private Shared ReadOnly Property DefaultTextColor() As Color
        Get
            Return Color.Black
        End Get
    End Property
    Private lookAndFeel As UserLookAndFeel

    Public ReadOnly Property CommentColor() As Color
        Get
            Return GetCommonColorByName(CommonSkins.SkinInformationColor, DefaultCommentColor)
        End Get
    End Property
    Public ReadOnly Property KeywordColor() As Color
        Get
            Return GetCommonColorByName(CommonSkins.SkinQuestionColor, DefaultKeywordColor)
        End Get
    End Property
    Public ReadOnly Property TextColor() As Color
        Get
            Return GetCommonColorByName(CommonColors.WindowText, DefaultTextColor)
        End Get
    End Property
    Public ReadOnly Property XmlCommentColor() As Color
        Get
            Return GetCommonColorByName(CommonColors.DisabledText, DefaultXmlCommentColor)
        End Get
    End Property
    Public ReadOnly Property StringColor() As Color
        Get
            Return GetCommonColorByName(CommonSkins.SkinWarningColor, DefaultStringColor)
        End Get
    End Property

    Public Sub New(ByVal lookAndFeel As UserLookAndFeel)
        Me.lookAndFeel = lookAndFeel
    End Sub

    Private Function GetCommonColorByName(ByVal colorName As String, ByVal defaultColor As Color) As Color
        Dim skin As Skin = CommonSkins.GetSkin(lookAndFeel)
        If skin Is Nothing Then
            Return defaultColor
        End If
        Return skin.Colors(colorName)
    End Function
End Class

<Serializable()>
Public Class RichEditControlString

    Private _text As String

    Public Property Text As String
        Get
            Return Me._text
        End Get
        Set(value As String)
            Me._text = value
        End Set
    End Property

    Public Sub New()
        Me._text = ""
    End Sub

    Public Overrides Function ToString() As String
        Return Me._text
    End Function

End Class
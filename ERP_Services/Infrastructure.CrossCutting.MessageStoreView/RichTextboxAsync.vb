'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.MessageStoreViewer
' Author           : Juan F. Tamayo
' Created          : 2015-10-31
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region ""

Imports System.IO
Imports System.Text
Imports System.Threading

#End Region

''' <summary>
''' RichTextbox con carga asíncrona
''' </summary>
Public Class RichTextboxAsync
    Inherits RichTextBox

#Region "Fields"

    ''' <summary>
    ''' Cuerpo del texto completo
    ''' </summary>
    Private _textBody As String

    ''' <summary>
    ''' Lector de la cadena
    ''' </summary>
    Private _stringReader As StringReader

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el cuerpo del texto completo
    ''' </summary>
    ''' <value>Cuerpo del texto</value>
    ''' <returns>El cuerpo del texto completo</returns>
    Public Overrides Property Text As String
        Get
            Return Me._textBody
        End Get
        Set(value As String)
            Me._textBody = value
            Me._stringReader = New StringReader(Me._textBody)
            MyBase.ResetText()
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Handlers"

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga el texto asíncronamente
    ''' </summary>
    ''' <param name="text">Texto a cargar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadTextAsync(ByVal text As String) As task
        Return Task.Factory.StartNew(Sub()
                                         MyBase.LoadFile(New MemoryStream(Encoding.Default.GetBytes(text)), RichTextBoxStreamType.PlainText)
                                     End Sub)
    End Function

#End Region

End Class
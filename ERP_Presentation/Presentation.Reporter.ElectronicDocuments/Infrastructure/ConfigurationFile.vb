'***********************************************************************
' Assembly         : Presentation.Reporter.Net8
' Adapted for .NET 8
'***********************************************************************

Namespace Infrastructure

    ''' <summary>
    ''' Configuración simplificada para los reportes
    ''' </summary>
    Public Class ConfigurationFile

        Private Shared _instance As ConfigurationFile

        Public Shared ReadOnly Property Instance As ConfigurationFile
            Get
                If _instance Is Nothing Then
                    _instance = New ConfigurationFile()
                End If
                Return _instance
            End Get
        End Property

        ''' <summary>
        ''' Ruta local de los reportes
        ''' </summary>
        Public Property LocalReportsPath As String = ""

        ''' <summary>
        ''' Ruta del servidor de los reportes
        ''' </summary>
        Public Property ServerReportsPath As String = ""

        ''' <summary>
        ''' Ruta de los reportes personalizados
        ''' </summary>
        Public Property ReportsPath As String = ""

    End Class

End Namespace

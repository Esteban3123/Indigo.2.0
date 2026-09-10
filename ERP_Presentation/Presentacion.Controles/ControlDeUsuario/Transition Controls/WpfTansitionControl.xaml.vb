'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 20-01-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports System.Windows.Media.Animation
Imports System.Windows.Forms.Integration

#End Region

''' <summary>
''' Clase con la funcionalidad del control para realizar la animacion con efecto de hacia la izquierda
''' </summary>
''' <remarks></remarks>
Public Class WpfTansitionControl

#Region "Eventos"
    ''' <summary>
    ''' Evento que se dispara cuando la animacion se completa
    ''' </summary>
    ''' <remarks></remarks>
    Public Event AnimationOpenComplete()
#End Region

#Region "Propiedades"
    Public Property HostControl As WindowsFormsHost
        Get
            Return Me.Host
        End Get
        Set(value As WindowsFormsHost)
            Me.Host = value
        End Set
    End Property

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo para iniciar la animacion hacia abajo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenAnimation()
        Host.Visibility = System.Windows.Visibility.Visible
        Dim storyboard As Storyboard = Resources("SlideRigthIn")
        AddHandler storyboard.Completed, AddressOf animationCompleted
        storyboard.Begin(Host)
    End Sub

    ''' <summary>
    ''' Metodo para inicar la animacion hacia arriba
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseAnimation()
        Dim storyboard As Storyboard = Resources("SlideRigthOut")
        storyboard.Begin(Host)
        Host.Visibility = System.Windows.Visibility.Hidden
    End Sub

    ''' <summary>
    ''' Metodo que cuando se completa la animacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub animationCompleted()
        System.Threading.Thread.Sleep(300)
        RaiseEvent AnimationOpenComplete()
    End Sub
#End Region

End Class

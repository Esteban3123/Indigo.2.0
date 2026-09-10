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
#End Region

''' <summary>
''' Clase con la funcionalidad del control para realizar la animacion con efecto de deslizar hacia abajo
''' </summary>
''' <remarks></remarks>
Public Class WpfAnimationPopUp

#Region "Eventos"
    ''' <summary>
    ''' Evento que dispara cuando la animacion se a completado
    ''' </summary>
    ''' <remarks></remarks>
    Public Event AnimationComplete()
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo para iniciar la animacion hacia abajo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenAnimation()
        Host.Visibility = Windows.Visibility.Visible
        Dim storyboard As Storyboard = Resources("SlideDownIn")
        AddHandler storyboard.Completed, AddressOf OpenAnimationComplete
        storyboard.Begin(Host)
    End Sub

    ''' <summary>
    ''' Metodo para inicar la animacion hacia arriba
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseAnimation()
        Dim storyboard As Storyboard = Resources("SlideDownOut")
        AddHandler storyboard.Completed, AddressOf OpenAnimationComplete
        storyboard.Begin(Host)
        Host.Visibility = Windows.Visibility.Hidden
    End Sub

    ''' <summary>
    ''' Metodo cuando se completa la animacion del storyboard
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenAnimationComplete()
        RaiseEvent AnimationComplete()
    End Sub

#End Region

End Class

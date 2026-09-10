'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Jorge Leonardo Vernaza
' Created          : 26-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Controls.MVP
#End Region

''' <summary>
''' Clase con la funcionalidad visualizacion de los mensajes
''' </summary>
Public Class CtrConversationItem

#Region "Eventos"
    ''' <summary>
    ''' Evento que indica que se hace scroll sobre algun punto del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event MouseWheelControl(sender As Object, e As MouseEventArgs)
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Obtiene o establece el nombre del usuario para el mensaje.
    ''' </summary>
    Public Property INDUserName As String
        Get
            Return INDlblName.Text
        End Get
        Set(value As String)
            INDlblName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del mensaje
    ''' </summary>
    Public Property INDDateMessage As DateTime
        Get
            Return CDate(INDlblDateMessage.Text)
        End Get
        Set(value As DateTime)
            Dim Diff As Long
            Using Model As New MformBase
                Dim DateToday As Date = Model.GetDateServer
                Diff = Microsoft.VisualBasic.DateDiff(Microsoft.VisualBasic.DateInterval.Day, value, DateToday)
            End Using
            If Diff < 1 Then
                INDlblDateMessage.Text = value.ToString("HH:mm tt")
            ElseIf Diff >= 1 And Diff <= 7 Then
                INDlblDateMessage.Text = value.ToString("DDD HH:mm tt")
            ElseIf Diff >= 8 And Diff <= 365 Then
                INDlblDateMessage.Text = value.ToString("ddd dd MMMM HH:mm tt")
            ElseIf Diff > 365 Then
                INDlblDateMessage.Text = value.ToString("ddd dd MMMM yyyy HH:mm tt")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el mensaje
    ''' </summary>
    Public Property INDMessage As String
        Get
            Return INDrchMeesage.HtmlText
        End Get
        Set(value As String)
            INDrchMeesage.HtmlText = value
            INDrchMeesage.Size = New Size(INDrchMeesage.Size.Width, RTFCalculator.CalculateTextHeight(INDrchMeesage) + 7)
            Me.Size = New Size(Me.Size.Width, RTFCalculator.CalculateTextHeight(INDrchMeesage) + INDpcTop.Size.Height + 20)
        End Set
    End Property
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo resize del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrConversationItem_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        INDrchMeesage.Size = New Size(INDrchMeesage.Size.Width, RTFCalculator.CalculateTextHeight(INDrchMeesage) + 7)
        Me.Size = New Size(Me.Size.Width, RTFCalculator.CalculateTextHeight(INDrchMeesage) + INDpcTop.Size.Height + 20)
    End Sub

#End Region
End Class

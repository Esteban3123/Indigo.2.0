'***********************************************************************'
' Assembly         : Presentation.Controls                              '
' Author           : Jorge Leonardo Vernaza                             '
' Created          : 01-11-2012                                         '
'                                                                       '
' Last Modified By :                                                    '
' Last Modified On :                                                    '
' Description      :                                                    '
'                                                                       '
' Copyright        : (c) . All rights reserved.                         '
'***********************************************************************'
#Region "Librerias Importadas"
Imports System.Threading.Tasks
#End Region
''' <summary>
''' Clase que contiene la funcionalidad del control que muestra todos los twitter encontrados
''' </summary>
Public Class CtrTwitter
#Region "Variable Globales"
    ''' <summary>
    ''' Variable que se utiliza para almacenar el listado de controles que contienen los twitters
    ''' </summary>
    Dim ControlsTwitter As List(Of CtrTwitterItem)
#End Region

#Region "Propiedades"

#End Region

#Region "Metodos - Funciones"
    Public Async Sub LoadTwittersAsync()
        Await Task.Run(AddressOf LoadTwitters)
        INDxscContainerTwitters.Controls.Clear()
        If ControlsTwitter IsNot Nothing Then
            If ControlsTwitter.Count > 0 Then
                For Each TwitterItem As CtrTwitterItem In ControlsTwitter
                    INDxscContainerTwitters.Controls.Add(TwitterItem)
                Next
            End If
        End If
    End Sub

    Private Async Function LoadTwitters() As Task
        Dim ListTwitter As List(Of Tweet)
        ControlsTwitter = New List(Of CtrTwitterItem)

        ListTwitter = Await ApiTwitter.twitterApi("Medicina")
        For Each Tweet As Tweet In ListTwitter
            ControlsTwitter.Add(New CtrTwitterItem With {.INDUserMessage = Tweet.Message, .INDUserPhoto = Tweet.ImageSource, .INDUserName = Tweet.UserName, .Dock = DockStyle.Left})
        Next
    End Function

#End Region
End Class

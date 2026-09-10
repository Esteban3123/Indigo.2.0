'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 10-Enero-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports System.Threading.Tasks
#End Region

''' <summary>
''' Clase con la logica del centro de notificaciones
''' </summary>
''' <remarks></remarks>
Public Class CtrNotificationCenter

#Region "Metodos"
    ''' <summary>
    ''' Metodo constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        AddHandler WpfSegmentControl.SelectionChange, AddressOf SelectionChange
    End Sub

    ''' <summary>
    ''' Metodo cuando se cambia el tab seleccionado
    ''' </summary>
    ''' <param name="Selecction"></param>
    ''' <remarks></remarks>
    Private Sub SelectionChange(ByVal Selecction As eNotifications)
        Select Case Selecction
            Case eNotifications.Messages
                INDxcMessages.Visible = True
                INDxcTask.Visible = False
                INDgcProcess.Visible = False
            Case eNotifications.Process
                INDxcMessages.Visible = False
                INDxcTask.Visible = False
                INDgcProcess.Visible = True
            Case eNotifications.Task
                INDxcMessages.Visible = False
                INDxcTask.Visible = True
                INDgcProcess.Visible = False
        End Select
    End Sub

    ''' <summary>
    ''' Propiedad que obtiene o establece el listado de procesos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProcessList As List(Of AsyncProcess)
        Get
            Return INDgcProcess.DataSource
        End Get
        Set(value As List(Of AsyncProcess))
            INDgcProcess.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo para limpiar las notificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        INDxcMessages.Controls.Clear()
        INDxcTask.Controls.Clear()
        INDgcProcess.DataSource = Nothing
    End Sub
#End Region

End Class

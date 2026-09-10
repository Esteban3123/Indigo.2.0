Imports System.Windows.Forms

''' <summary>
''' Singleton para manejar la instancia MDI y el patrón Observer con Windows Forms
''' </summary>
Public NotInheritable Class MDISingleInstance

#Region "Singleton"
    Private Shared _instance As MDISingleInstance
    Private Shared ReadOnly _lock As New Object()

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As MDISingleInstance
        Get
            If _instance Is Nothing Then
                SyncLock _lock
                    If _instance Is Nothing Then
                        _instance = New MDISingleInstance()
                    End If
                End SyncLock
            End If
            Return _instance
        End Get
    End Property
#End Region

#Region "MDI Reference"
    Private _instanceMDI As Form
    ''' <summary>
    ''' Obtiene o asigna la instancia del formulario principal MDI
    ''' </summary>
    ''' <value>Instancia del formulario principal</value>
    ''' <returns>La instancia del formulario principal</returns>
    Public Property InstanceMDI As Form
        Get
            Return Me._instanceMDI
        End Get
        Set(value As Form)
            Me._instanceMDI = value
        End Set
    End Property
#End Region

#Region "Notification Suscriptor (Observer Pattern)"
    Private _ObserversList As List(Of IObservador)
    ''' <summary>
    ''' Lista de observadores registrados
    ''' </summary>
    Public Property ObserversList As List(Of IObservador)
        Get
            If _ObserversList IsNot Nothing Then
                Return _ObserversList
            Else
                _ObserversList = New List(Of IObservador)
                Return _ObserversList
            End If
        End Get
        Set(value As List(Of IObservador))
            _ObserversList = value
        End Set
    End Property

    Private _handle As IntPtr
    ''' <summary>
    ''' Handle del formulario principal
    ''' </summary>
    Public Property Handle As IntPtr
        Get
            Return _handle
        End Get
        Set(value As IntPtr)
            _handle = value
        End Set
    End Property
#End Region

End Class
Public NotInheritable Class ServerSessionValues

#Region "Singleton"

    ''' <summary>
    ''' Unica instancia de la clase
    ''' </summary>
    Private Shared _currentInstance As ServerSessionValues

    ''' <summary>
    ''' Obtiene la unica instancias de la clase
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property Current As ServerSessionValues
        Get
            If _currentInstance Is Nothing Then
                _currentInstance = New ServerSessionValues()
            End If
            Return _currentInstance
        End Get
    End Property

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el nombre del actual contenedor a usar
    ''' </summary>
    ''' <value>Nombre del contenedor actual</value>
    ''' <returns>El nombre del contenedor actual</returns>
    Public Property CurrentContainer As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del actual contenedor HIS a usar
    ''' </summary>
    ''' <value>Nombre del contenedor HIS actual</value>
    ''' <returns>El nombre del contenedor HIS actual</returns>
    Public Property CurrentHISContainer As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del actual contenedor HIS a usar
    ''' </summary>
    ''' <value>Nombre del contenedor HIS actual</value>
    ''' <returns>El nombre del contenedor HIS actual</returns>
    Public Property CurrentInteropCostContainer As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Private Sub New()
        Me.CurrentContainer = String.Empty
    End Sub

#End Region

End Class

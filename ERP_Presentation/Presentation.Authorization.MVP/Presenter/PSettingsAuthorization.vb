#Region "Imports"

Imports Infrastructure.CrossCutting.Base

#End Region

Public Class PSettingsAuthorization

#Region "Fields"

    ''' <summary>
    ''' Referencia a la vista de la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Private View As ISettingsAuthorization

    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    ''' <remarks></remarks>
    Private _sessionValues As SessionValues

#End Region

#Region "Build"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal view As ISettingsAuthorization)
        Me.View = view
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"



#End Region

End Class

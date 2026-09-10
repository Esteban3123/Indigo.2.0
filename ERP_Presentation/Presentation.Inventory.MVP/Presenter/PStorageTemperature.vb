'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Andres Alarcon
' Created          : 27-11-2024
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base

#End Region

Public Class PStorageTemperature

#Region "Fields"

    ''' <summary>
    ''' Referencia a la vista de la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Private View As IStorageTemperature

    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    ''' <remarks></remarks>
    Private _sessionValues As SessionValues


#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal view As IStorageTemperature)
        Me.View = view
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 03-05-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
#End Region

''' <summary>
''' Presentador del frontal de ciudades
''' </summary>
''' <remarks></remarks>
Public Class PPhoneType

    ''' <summary>
    ''' Interfaz que representa la vista del frontal de Tipos de telefono
    ''' </summary>
    Dim View As IPhoneType

    ''' <summary>
    ''' Instancia la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Construye el presentador del frontal de Tipos de telefono
    ''' </summary>
    ''' <param name="iview">the view</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal iview As IPhoneType)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initializes()
       
    End Sub

End Class


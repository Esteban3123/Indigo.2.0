'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Kevin Garay Rodriguez
' Created          : 10-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de Departamentos
''' </summary>
Public Class PDepartaments
#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IDepartaments
    ''' </summary>
    Private _view As IDepartaments
    ''' <summary>
    ''' Variable que se utilizapa para tratar los departamentos como un Objeto
    ''' </summary>
    Private _departaments As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region
#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de Niveles de Cargos
    ''' </summary>
    ''' <param name="view">Vista de niveles de cargos</param>
    Public Sub New(ByRef view As IDepartaments)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region
End Class

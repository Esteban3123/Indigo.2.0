'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 16-04-2013
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
''' Esta presentador captura toda la logica aplicada en el frontal de Profesiones
''' </summary>
Public Class PProfessions

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IProfessions
    ''' </summary>
    Private _view As IProfessions
    ''' <summary>
    ''' Variable que se utilizapa para tratar las profesiones como un Objeto
    ''' </summary>
    Private _professions As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de Profesiones
    ''' </summary>
    ''' <param name="view">Vista de las profesiones</param>
    Public Sub New(ByRef view As IProfessions)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Inicializa los componentes necesarios
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initialize()
        Me._view.StudyLevel = EmployeeHelper.StudyHierarchy
    End Sub
    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequensePayroll(_view.MyTag)
            Me._view.Sequence = Await model.GetSequense()
        End Using
    End Sub
#End Region

End Class

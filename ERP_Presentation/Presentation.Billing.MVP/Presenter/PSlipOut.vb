'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 05/01/2016
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
Imports Presentation.Controls.MVP

#End Region

Public Class PSlipOut

#Region "Fields"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Dim View As ISlipOut

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    ''' <remarks></remarks>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview"></param>
    ''' <remarks></remarks>
    Public Sub New(ByRef iview As ISlipOut)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' inicializa la el serachlockup de admisiones
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAdmission()
        Using model As New MBusqueda
            Me.View.AdmissionXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdmissionsToReportSlipOut)
        End Using
    End Sub

#End Region

End Class

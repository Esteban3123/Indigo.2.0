'***********************************************************************
' Assembly         : Presentacion.JustificationControl.MVP
' Author           : Cristian Camilo Bahamón Castaño
' Created          : 06-02-2023
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
Imports Presentation.Payroll.MVP
#End Region


Public Class PContributorSubtype

#Region "Fields"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IContributorSubtype

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IContributorSubtype)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequensePayroll(Me.View.MyTag)
            View.Sequence = Await model.GetSequense()
        End Using
    End Sub

#End Region


End Class


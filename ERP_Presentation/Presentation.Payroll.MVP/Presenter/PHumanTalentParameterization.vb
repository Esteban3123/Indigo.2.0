'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cesar Augusto Collazos Perdomo
' Created          : 29-09-2023
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
#End Region
Public Class PHumanTalentParameterization

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IHumanTalentParameterization

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IHumanTalentParameterization)
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
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

#End Region
End Class
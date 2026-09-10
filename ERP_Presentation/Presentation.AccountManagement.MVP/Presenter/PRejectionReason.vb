'***********************************************************************
' Assembly         : Presentation.AccountManagement.MVP
' Author           : Andrés Steven Rojas
' Created          : 09-01-2025
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
Imports Presentation.Payroll.MVP
#End Region
Public Class PRejectionReason

#Region "Fields"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IRejectionReason

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
    Public Sub New(ByRef iview As IRejectionReason)
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
    ''' Obtiene la secuencia del formulario
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista los usuario del aplicativo
    ''' </summary>
    Public Sub InitializeUsers()
        Using model As New MFunctionalUnit("")
            Me.View.UserXpo = model.ListAllUser(Indigo.SecurityContainer)
        End Using
    End Sub
#End Region

End Class

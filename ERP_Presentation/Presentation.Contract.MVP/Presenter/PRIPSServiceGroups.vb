'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 05-12-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Public Class PRIPSServiceGroups

#Region "Fields"
    Dim View As IRIPSServiceGroups

    Dim Corporation As Object

    Dim Indigo As SessionValues
#End Region

#Region "Builder"
    Public Sub New(ByRef iview As IRIPSServiceGroups)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub
#End Region

#Region "Methods"
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense
        End Using
    End Sub
#End Region
End Class

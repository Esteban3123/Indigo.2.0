'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 13-12-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP

Public Class PRIPSServices
#Region "Fields"
    Dim View As IRIPSServices

    Dim Corporation As Object

    Dim Indigo As SessionValues
#End Region

#Region "Builder"
    Public Sub New(ByRef iview As IRIPSServices)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
            Indigo = SessionValues.Instance
        End If
    End Sub
#End Region

#Region "Methods"
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense
        End Using
    End Sub

    ''' <summary>
    ''' Consulta la lista de grupos de servicios RIPS que estén activos
    ''' </summary>
    Public Sub InitializeRIPSServiceGroups()
        View.RIPSServiceGroupsXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListRIPSServiceGroups()
    End Sub
#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/07/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository

#End Region

Public Class PAcceptanceAuthorization

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    Public Sub New()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewAcceptanceAuthorizationXpo(careCenterCode As String, functionalUnitId As Integer) As List(Of ViewAcceptanceAuthorizationXpo)
        Dim filter As String = "CareCenterCode = '" & careCenterCode & "' and FunctionalUnitTargetId = " & functionalUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewAcceptanceAuthorizationXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Carga los rechazos por usuario
    ''' </summary>
    Public Function InitializeAuthorizationRejection() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListAuthorizationRejectionByUserCode(Indigo.UserIndigo)
    End Function

#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/06/2020
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

Public Class PDashboardContractCoverage

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
    ''' Carga las unidades funcionales
    ''' </summary>
    Public Function InitializeCareCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
    End Function

    ''' <summary>
    ''' Lista las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewDashboardContractCoverage(careCenterCodes As String) As List(Of ViewDashboardContractCoverageXpo)
        Dim filter As String = "CareCenterCode in (" & careCenterCodes & ")"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewDashboardContractCoverageXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class

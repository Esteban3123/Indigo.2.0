'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/07/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP

#End Region

Public Class PDashboardQuoted

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
    Public Function ListViewDashboardQuotedHospitable(careCenterCodes As String, type As String) As List(Of ViewDashboardQuotedHospitableXpo)
        Dim filter As String = "CareCenterCode in (" & careCenterCodes & ") and ItemType = '" & type & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewDashboardQuotedHospitableXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class

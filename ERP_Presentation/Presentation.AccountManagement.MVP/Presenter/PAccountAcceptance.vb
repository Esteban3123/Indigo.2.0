Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent

Public Class PAccountAcceptance

#Region "Fields"
    ''' <summary>
    ''' Interfaz del formulario
    ''' </summary>
    Dim View As IAccountAcceptance

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues
#End Region

#Region "Builder"
    Public Sub New(ByRef iView As IAccountAcceptance)
        View = iView
        Indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la lista de todos los grupos de atención
    ''' </summary>
    Public Sub FillAttentionCenter()
        View.AttentionCenterXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
    End Sub

    ''' <summary>
    ''' Obtiene la lista de las razones de rechazos de traslado activas
    ''' </summary>
    Public Sub FillRejectionReasons()
        Dim res = IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ListAllRejectionReasonsAsync().Result
        View.RejectionReasonDatasource = res.ObjectEmbbeded
    End Sub
#End Region
End Class

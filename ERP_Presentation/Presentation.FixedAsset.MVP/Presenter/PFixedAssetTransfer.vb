'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/05/2016
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class PFixedAssetTransfer

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetTransfer

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetTransfer)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' lista las localizaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeLocation() As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetLocationByStatus()
    End Function

    ''' <summary>
    ''' Inicializa las ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitializeLocationXpInstantFeedBackSource() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetLocationXPInstantFeedbackSource()
    End Function

    ''' <summary>
    ''' lista los responsables
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSourceResponsible()
        View.SourceResponsibleXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleByStatus(True)
    End Sub

    ''' <summary>
    ''' lista los responsables
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeTargetResponsible()
        View.TargetResponsibleXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleByStatus(True)
    End Sub

    ''' <summary>
    ''' Lista los activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListFixedAssetPhysicalAssetXpo(TransferType As Integer, SourceLocationId As Integer?, SourceResponsibleId As Integer?) As List(Of FixedAssetPhysicalAssetXpo)
        Dim filtroConsulta As String = String.Empty
        Select Case TransferType
            Case 1 'Localización
                filtroConsulta = "LocationId = " & SourceLocationId
            Case 2 'Responsable
                filtroConsulta = "ResponsibleId = " & SourceResponsibleId
            Case 3 'Localización y Responsable
                filtroConsulta = "LocationId = " & SourceLocationId & " And ResponsibleId = " & SourceResponsibleId
        End Select

        filtroConsulta &= " And Status <> 0 "
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetPhysicalAssetXpo)(Nothing, filtroConsulta)
    End Function

#End Region

End Class

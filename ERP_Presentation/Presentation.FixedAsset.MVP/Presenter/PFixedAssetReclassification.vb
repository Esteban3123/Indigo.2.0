'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/06/2016
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

Public Class PFixedAssetReclassification

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetReclassification

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetReclassification)
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
    ''' Lista los artículos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeItemPrevious()
        View.ItemPreviousXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True, 2)
    End Sub

    ''' <summary>
    ''' Lista los catálogos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeItemCatalogPrevious()
        View.ItemCatalogPreviousXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedEquipmentCatalogByStatus(True)
    End Sub

    ''' <summary>
    ''' Lista los artículos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeItem()
        View.ItemXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True, 2)
    End Sub

    ''' <summary>
    ''' Lista los catálogos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeItemCatalog()
        View.ItemCatalogXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedEquipmentCatalogByStatus(True)
    End Sub

    ''' <summary>
    ''' Lista los activos por reclasificar
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetForReclassification(ItemId As Integer) As List(Of ViewListFixedAssetForReclassificationXpo)
        Dim filtroConsulta As String = "ItemId = " & ItemId
        Dim xpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of ViewListFixedAssetForReclassificationXpo)(Nothing, filtroConsulta)
        If xpo IsNot Nothing AndAlso xpo.Count > 0 Then
            Return xpo
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lista los activos reclasificados
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetReclassified(Id As Integer) As List(Of ViewListFixedAssetReclassifiedXpo)
        Dim filtroConsulta As String = "FixedAssetReclassificationId = " & Id
        Dim xpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of ViewListFixedAssetReclassifiedXpo)(Nothing, filtroConsulta)
        If xpo IsNot Nothing AndAlso xpo.Count > 0 Then
            Return xpo
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class

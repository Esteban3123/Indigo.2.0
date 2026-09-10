'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/08/2016
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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.BudgetRepository

#End Region

Public Class PFixedAssetEntryDevolution

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Private View As IFixedAssetEntryDevolution

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Private Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetEntryDevolution)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
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
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function InitializeFixedAssetEntry() As Task
        View.FixedAssetEntryXpo = Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetEntryByStatus(2))
    End Function

    ''' <summary>
    ''' Lista los detalles del ingreso de activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListFixedAssetEntryItemDetailByFixedAssetEntryId(FixedAssetEntryId As Integer) As List(Of ViewListFixedAssetEntryItemDetail)
        Dim filtroConsulta As String = "FixedAssetEntryId = " & FixedAssetEntryId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of ViewListFixedAssetEntryItemDetail)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Obtiene el ingreso por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetFixedAssetEntryById(FixedAssetEntryId As Integer) As Task(Of FixedAssetEntryXpo)
        Dim filtroConsulta As String = "Id = " & FixedAssetEntryId
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetXPOObject(Of FixedAssetEntryXpo)(filtroConsulta))
    End Function

    ''' <summary>
    ''' Lista los detalles de la devolución de ingreso de activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListFixedAssetEntryDevolutionDetailByFixedAssetEntryDevolutionId(FixedAssetEntryDevolutionId As Integer) As List(Of FixedAssetEntryDevolutionDetailXpo)
        Dim filtroConsulta As String = "FixedAssetEntryDevolutionId = " & FixedAssetEntryDevolutionId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetEntryDevolutionDetailXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function ListObligationsOfFixedAssetEntry(fixedAssetEntryId As Integer) As Task(Of List(Of ViewListObligationOfFixedAssetEntryXpo))
        Dim filter As String = "FixedAssetEntryId = " & fixedAssetEntryId
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of ViewListObligationOfFixedAssetEntryXpo)(Nothing, filter))
    End Function
    ''' <summary>
    ''' Metodo para traer la configuracion de activos por unidad operativa
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    Public Async Function GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer) As Task
        Using model As New MSettingFixedAsset(Me.View.MyTag)
            View.SettingsFixedAsset = Await model.GetSettingFixedAssetByOperatingUnitId(OperatingUnitId)
        End Using
    End Function

#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/05/2016
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

#End Region

Public Class PFixedAssetActiveOutputDetail

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetActiveOutputDetail

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
    Public Sub New(ByRef iview As IFixedAssetActiveOutputDetail)
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
   
    Public Sub InitializePhysicalAsset()
        View.PhysicalAssetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAssetHasOutput()
    End Sub

    Public Sub InitializePhysicalAssetPart()
        View.PhysicalAssetPartXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAssetParts()
    End Sub

    Public Sub InitializeThirdParty()
        View.ThirdPartyXpo = (XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty())
    End Sub

    ''' <summary>
    ''' lista de si Registra valor de reposición
    ''' </summary>
    Public Sub InitializeLowType()
        View.LowTypeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetRetirementTypesByStatus()
    End Sub

    ''' <summary>
    ''' Obtiene el Activo Fijo
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetPhysicalById(Id As Integer) As Task(Of FixedAssetPhysicalAssetXpo)
        Dim filtroConsulta As String = "Id = " & Id
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetPhysicalAssetXpo)(Nothing, filtroConsulta).FirstOrDefault())
    End Function

#End Region

End Class

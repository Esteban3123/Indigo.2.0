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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class PFixedAssetDepreciation

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Private View As IFixedAssetDepreciation

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Private Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetDepreciation)
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
    ''' Lista los activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetDeprececiationByMonthAndYear(DepreciateMonth As Integer, DepreciateYear As Integer) As FixedAssetDepreciationXpo
        Dim filtroConsulta As String = "ClosingMonth = " & DepreciateMonth & " And ClosingYear = " & DepreciateYear
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetXPOObject(Of FixedAssetDepreciationXpo)(filtroConsulta)
    End Function

#End Region

End Class

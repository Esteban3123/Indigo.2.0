'***********************************************************************
' Assembly         : Presentacion.FixedAssets
' Author           : Andres Alarcon
' Created          : 10-06-2024
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
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base

#End Region

Public Class PFixedAssetCatalogOfPropertyandServices

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetCatalogOfPropertyandServices

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Dim filter() As Object = {5, True}

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetCatalogOfPropertyandServices)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Devuelve los IVAs que esten activos
    ''' </summary>
    Public Function ListGeneralLedgerIvaByState() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListXPInstantFeedbackSource(Of GeneralLedgerIVAXpo)($"Status = 1", "Id;Code;Name;CodeName")
    End Function

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset("2842")
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

End Class

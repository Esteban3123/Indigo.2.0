'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/04/2016
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class PFixedAssetInitialBalanceItemParts

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetInitialBalanceItemParts

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetInitialBalanceItemParts)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializePartAccesoriesConsumables()
        View.PartAccesoriesConsumableXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPartsAccesoriesConsumibles()
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeLegalBook()
        View.LegalBookXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListBookByStatus(True)
    End Sub

    ''' <summary>
    ''' obtiene el libro contable por medio del Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetLegalBookById(Id As Integer) As Task(Of Infrastructure.Data.Xpo.AccountingRepository.BookXpo)

        Return Await Task.Factory.StartNew(Function() As Infrastructure.Data.Xpo.AccountingRepository.BookXpo
                                               Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of Infrastructure.Data.Xpo.AccountingRepository.BookXpo)($"Id = {Id}")
                                           End Function)

    End Function

#End Region

End Class

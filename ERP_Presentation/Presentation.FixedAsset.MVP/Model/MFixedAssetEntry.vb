'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports DevExpress.Xpo
Imports System.Dynamic

#End Region

Public Class MFixedAssetEntry
    Implements IDisposable


#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener el tercero 
    ''' </summary>
    ''' <param name="Nit">Codigo del tercero</param>
    ''' <returns></returns>
    Public Async Function GetThirdPartyAsync(ByVal Nit As String) As Task(Of ThirdParty)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyByNitAsync(Nit, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetFixedAssetEntryById(ByVal id As Integer) As Task(Of ActionResult(Of FixedAssetEntry))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetEntryByIdAsync(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="itemId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemCatalogByItemId(ByVal itemId As Integer) As FixedAssetItemCatalog
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetItemCatalogByItemId(itemId)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para obtener el tercero
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetThirdPartyByIdSimple(ByVal id As Integer) As ThirdParty
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyById(id, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetFixedAssetEntry(ByVal code As String) As Task(Of ActionResult(Of FixedAssetEntry))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetEntryAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveFixedAssetEntry(ByVal FixedAssetEntry As FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook),
                                 ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook),
                                 ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of FixedAssetEntryItem), ByVal idSequense As Int64) As Task(Of ActionResult(Of FixedAssetEntry))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveFixedAssetEntryAsync(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmFixedAssetEntry(ByVal FixedAssetEntry As FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook),
                                 ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook),
                                 ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of FixedAssetEntryItem), ByVal idSequense As Int64) As Task(Of ActionResult(Of FixedAssetEntry))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ConfirmFixedAssetEntryAsync(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el porcentaje de ICA que maneja la linea de distribucion
    ''' </summary>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetICARetentionConceptBySupplierDistributionLine(ByVal idSupplierDistributionLine As Integer, ByVal OperatingUnitId As Integer) As Task(Of RetentionConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetICARetentionConceptBySupplierDistributionLineAsync(idSupplierDistributionLine, OperatingUnitId, _indigoSessionValues)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

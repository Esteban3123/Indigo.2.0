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
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class MEquipmentCatalog
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

    Dim Indigo As SessionValues
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance

        Indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' lista todas las cuentas de Nivel 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccount() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListAccountsByClass()
    End Function

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListLegalBook() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListBookByStatus(True)
    End Function

    ''' <summary>
    ''' Lista todas las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountStructure() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetAccountingStructure()
    End Function

    ''' <summary>
    ''' Lista todas las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IncomeAccountPayableConcept() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PaymentsService.ListAccountPayableConceptByHandlesRetentionAndConceptType(True, False, 1)
    End Function

    ''' <summary>
    ''' Lista todas las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IncomeAccountPayableConceptDeclarantRetention() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PaymentsService.ListAccountPayableConceptByHandlesRetentionAndConceptType(True, True, 2)
    End Function

    ''' <summary>
    ''' Lista todas las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IncomeAccountPayableConceptNotDeclarantRetention() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PaymentsService.ListAccountPayableConceptByHandlesRetentionAndConceptType(True, True, 2)
    End Function

    ''' <summary>
    ''' guardar una remision
    ''' </summary>
    ''' <param name="EquipmentCatalog"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveEquipmentCatalog(EquipmentCatalog As FixedAssetItemCatalog, idSequense As Integer, ByVal sequenceC As Domain.Entities.FixedAssetSequence) As Task(Of ActionResult(Of FixedAssetItemCatalog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveEquipmentCatalogAsync(EquipmentCatalog, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para eliminar la aseguradora
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteEquipmentCatalog(ByVal Record As FixedAssetItemCatalog, idSequense As Integer, ByVal sequenceC As Domain.Entities.FixedAssetSequence) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteEquipmentCatalogAsync(Record, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetEquipmentCatalogAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetEquipmentCatalogAsync(code)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(Code As String, State As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of FixedAssetItemCatalog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.Change_StateEquipmentCatalogAsync(Code, State, Indigo)
    End Function

    ''' <summary>
    ''' lista los ingresos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Function

    ''' <summary>
    ''' Lista las definiciones de tarifa para la rejilla de importar información
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMainAccountRestriction(mainAccountId As Integer) As DevExpress.Xpo.XPCollection(Of MainAccountRestrictionsXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListMainAccountRestriction(mainAccountId)
    End Function

    Public Async Function SetFixedAssetItemCatalogDetailFromFile(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As Task(Of List(Of SP_SetFixedAssetItemCatalogDetailFromFile_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SetFixedAssetItemCatalogDetailFromFileAsync(dataImportFile, dataCopyPaste)
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

'***********************************************************************
' Assembly         : Presentacion.MedicalFees.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class MMedicalFeesLiquidation
    Implements IDisposable

#Region "Fields"

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista las causaciones por contrato o por medico para pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractIdViewXpo(listId As List(Of Integer), medicalFeesContractId As Integer?, initialDate As DateTime, endDate As DateTime, healthProfessionalCode As String) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListCausationByMedicalFeesContractIdViewXpo(listId, medicalFeesContractId, initialDate, endDate, healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato o por medico para pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractId(listId As List(Of Integer), medicalFeesContractId As Integer?, initialDate As DateTime, endDate As DateTime, healthProfessionalCode As String) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListCausationByMedicalFeesContractId(listId, medicalFeesContractId, initialDate, endDate, healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato o por medico para deducciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractIdForDeductions(listId As List(Of Integer), medicalFeesContractId As Integer?, healthProfessionalCode As String) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListCausationByMedicalFeesContractIdForDeductions(listId, medicalFeesContractId, healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato o por medico para glosas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractIdForGlosas(listId As List(Of Integer), medicalFeesContractId As Integer?, healthProfessionalCode As String) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListCausationByMedicalFeesContractIdForGlosas(listId, medicalFeesContractId, healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetMedicalFeesLiquidation(ByVal code As String, medicalFeesContractId As Integer, optionConsult As Integer, Optional ByVal healthProfeesionalCode As String = Nothing) As Task(Of Domain.Base.Entities.ActionResult(Of MedicalFeesLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.GetMedicalFeesLiquidationAsync(code, medicalFeesContractId, Me.Indigo.AuditMessageWcf, optionConsult, healthProfeesionalCode)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="MedicalFeesContractId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetMedicalFeesLiquidationByMedicalFeesContractId(ByVal MedicalFeesContractId As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of MedicalFeesLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.GetMedicalFeesLiquidationByMedicalFeesContractIdAsync(MedicalFeesContractId, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetMedicalFeesLiquidationById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of MedicalFeesLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.GetMedicalFeesLiquidationByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveMedicalFeesLiquidation(ByVal record As MedicalFeesLiquidation, ByVal idSequense As Int64) As Task(Of ActionResult(Of MedicalFeesLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.SaveMedicalFeesLiquidationAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteMedicalFeesLiquidation(ByVal record As MedicalFeesLiquidation) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.DeleteMedicalFeesLiquidationAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmMedicalFeesLiquidation(ByVal medicalFeesLiquidation As MedicalFeesLiquidation, ByVal idSequense As Int64) As Task(Of ActionResult(Of MedicalFeesLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.SaveAndConfirmMedicalFeesLiquidationAsync(medicalFeesLiquidation, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function AnnularMedicalFeesLiquidation(ByVal MedicalFeesLiquidation As MedicalFeesLiquidation) As Task(Of ActionResult(Of MedicalFeesLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.AnnularMedicalFeesLiquidationAsync(MedicalFeesLiquidation, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Valida que la cuenta contable que viene asociada a la linea de distribucion maneje centro costo
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ValidateCostCenterBySupplierDistributionLineId(ByVal supplierDistributionLineId As Integer) As Task(Of ActionResult(Of MedicalFeesLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.ValidateCostCenterBySupplierDistributionLineIdAsync(supplierDistributionLineId)
    End Function

    ''' <summary>
    ''' Obtiene el listado de contratos que tiene asociado el médico y son de tipo estandar
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode As String) As Task(Of ActionResult(Of List(Of HealthProfessionalContract)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.ListHealthProfessionalContractWithTypeStandardAsync(healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Obtiene el listado de contratos que tiene asociado el médico y son de tipo estandar
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ContractWithTypeStandardAsync(healthProfessionalCode As String) As Task(Of ActionResult(Of List(Of HealthProfessionalContract)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.ListHealthProfessionalContractWithTypeStandardAsync(healthProfessionalCode)
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

'***********************************************************************
' Assembly         : Presentacion.Payrol.MVP
' Author           : Rafael Eduardo Patiño
' Created          : 17-07-2019
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
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region

Public Class MVerificationAutoliquidation

    Inherits ModelBase
    Implements IDisposable


#Region "Variables"

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#End Region

    ''' <summary>
    ''' Lista las empresas
    ''' </summary>
    Public Async Function ListAllCompany() As Task(Of List(Of Domain.Payroll.Entities.Company))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllCompanyAsync(Indigo)
    End Function


    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult(Of BlockRecord))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecord)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function

    ''' <summary>
    ''' Listar los campos nulls de la base de datos y porder customizar 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFieldsNULL() As Task(Of DataSet)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("AgreementsC", Me.Indigo)
    End Function


    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListVerifiyAutoliquidationByWorkCenterAndPayrollDate(WorkCenterCode As String, PayrollDateLiquidated As String) As XPCollection(Of PayrollViewVerifyAutoliquidationFileXpo)

        Dim Year = PayrollDateLiquidated.Substring(0, 4)
        Dim Month = PayrollDateLiquidated.Substring(5, 2)

        Dim DateSearch = New DateTime(Year, Month, 1)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListViewVerifyAutoliquidationFile(WorkCenterCode, DateSearch)

    End Function

    Public Async Function GetVerifyAutoliquidationByID(IdVerifyAutoliquidationFile As Integer) As Task(Of VerifyAutoliquidationFile)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetVerifyAutoliquidationFileByIdAsync(IdVerifyAutoliquidationFile, Indigo)
    End Function
    ''' <summary>
    ''' Funcion para obtener las autoliquidaciones del reporte INS
    ''' </summary>
    ''' <param name="WorkCenterCode"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <returns></returns>
    Public Function ListVerifiyAutoliquidationINS(WorkCenterCode As String, PayrollDateLiquidated As String) As List(Of PayrollViewVerifyAutoliquidationFileINSXpo)
        Dim Year = PayrollDateLiquidated.Substring(0, 4)
        Dim Month = PayrollDateLiquidated.Substring(5, 2)

        Dim DateSearch = New DateTime(Year, Month, 1)
        Dim filter As String = "PayrollDateLiquidated =  #" & Format(DateSearch, "yyyy-MM-dd") & "# and WorkCenter = " & WorkCenterCode
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollectionAsList(Of PayrollViewVerifyAutoliquidationFileINSXpo)(Nothing, filter)

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

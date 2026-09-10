'***********************************************************************
' Assembly         : Presentacion.Payrol.MVP
' Author           : Rafael Eduardo Patiño
' Created          : 07-01-2014
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region
''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MForeclousure
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

#Region "Funciones"

    ''' <summary>
    ''' Lista los terceros
    ''' </summary>
    Public Function ListAllThirdPartyXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Function


    Public Function ListAllThirdPartyXpo1() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Function

    ''Lista de Ciudades
    Public Function ListCity() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).CommonService.ListAllCities(True)
    End Function

    Public Function ListCompany() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).PayrollService.GetCompany()
    End Function

    ''' <summary>
    ''' Lista las empresas
    ''' </summary>
    Public Async Function ListAllCompany() As Task(Of List(Of Domain.Payroll.Entities.Company))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllCompanyAsync(Indigo)
    End Function

    ''' <summary>
    ''' Funcion que lista los conceptos de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllConceptAsync() As Task(Of List(Of Domain.Payroll.Entities.Concept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllConceptAsync(Indigo)
    End Function

    ''' <summary>
    ''' Listar Empleados
    ''' </summary>
    ''' <returns>lista de empleados</returns>
    ''' <remarks></remarks>
    Public Async Function ListEmployee() As Task(Of List(Of Domain.Payroll.Entities.Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListEmployeeAgreementsAsync(Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un empleado por el ID
    ''' </summary>
    ''' <param name="Id">Id del empleado</param>
    ''' <returns>Un Empleado</returns>
    ''' <remarks></remarks>
    Public Async Function GetEmployee(ByVal Id As String) As Task(Of Domain.Payroll.Entities.Employee)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeByIdAsync(Id, Indigo)
    End Function

    ''' <summary>
    ''' Obtener un convenio por el consecutivo de radicado
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetForeclousure(ByVal consecutive As String) As Task(Of Foreclousure)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetForeclousureAsync(consecutive, Indigo)
    End Function
    ''' <summary>
    ''' Guardar  un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveForeclousure(ByVal Foreclousure As Foreclousure, idSequense As Integer) As Task(Of ActionResult(Of Foreclousure))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveForeclousureAsync(Foreclousure, idSequense, Indigo)
    End Function
    ''' <summary>
    ''' Eliminar un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Obj. Convenio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteForeclousure(ByVal Foreclousure As Foreclousure) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteForeclousureAsync(Foreclousure, Indigo)
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
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("Foreclousure", Me.Indigo)
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


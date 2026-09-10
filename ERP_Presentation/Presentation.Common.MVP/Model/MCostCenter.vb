'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.AccountingRepository

Public Class MCostCenter
    Inherits ModelBase
    Implements IDisposable

    'Public Shared TAG As String = "517"


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


    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
        _tagForm = Tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.GeneralLedgerSequence)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSequenseByIdFormAsync(_tagForm)
    End Function


    Public Async Function ListAllCostCenterAsync() As Task(Of List(Of CostCenter))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllCostCenterAsync(_indigoSessionValues)
    End Function

    Public Async Function GetCostCenterAsync(ByVal code As String) As Task(Of CostCenter)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCostCenterAsync(code, _indigoSessionValues)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    Public Async Function GetCostCenterById(ByVal id As Integer) As Task(Of CostCenter)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCostCenterByIdAsync(id, True, _indigoSessionValues)
    End Function

    Public Function GetCostCenterByIdSimple(ByVal id As Integer) As CostCenter
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCostCenterById(id, True, _indigoSessionValues)
    End Function

    Public Async Function SaveCostCenterAsync(ByVal costCenter As CostCenter, ByVal idSequense As Int64) As Task(Of ActionResult(Of Domain.Payroll.Entities.CostCenter))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveCostCenterAsync(costCenter, _indigoSessionValues, idSequense)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    Public Async Function DeleteCostCenterAsync(ByVal costCenter As CostCenter) As Task(Of ActionMessageResult(Of CostCenter))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteCostCenterAsync(costCenter, _indigoSessionValues)
    End Function
    Public Function GetNullFields() As DataSet
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("CostCenter", _indigoSessionValues)
    End Function

    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Payroll.Entities.CostCenter))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateCostCenterAsync(code, state, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetNumericSequenseGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Lista las unidades de medida
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostcenterReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostCenterXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(CostCenterXpo)),
                                                         "Id;Code;Name;State;CodeName", Nothing)
        Return serverMode
    End Function

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

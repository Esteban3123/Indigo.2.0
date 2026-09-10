'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 15/07/2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.Data.PLinq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic

#End Region

Public Class MBillingJustificationControl
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
    ''' Copiar y pegar para la rejilla de plantilla de procedimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Async Function CopyAndPasteJustification(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of BillingJustificationControlUser), List(Of Tuple(Of String, Integer))))
    '    Me.Indigo.AuditMessageWcf.Functional = _tagForm
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.CopyAndPasteCategoriesAsync(data)
    'End Function

    ''' <summary>
    ''' Obtiene todos las justificaciones control de cuentas hospitalario
    ''' </summary>
    Public Async Function ListAllJustificationControl() As Task(Of List(Of BillingJustificationControl))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListAllJustificationControlAsync()
    End Function

    ''' <summary>
    ''' graba una justificacion control
    ''' </summary>
    ''' <param name="justificationControl">La justificacion control</param>
    ''' <returns></returns>
    Public Async Function SaveBillingJustificationControl(ByVal record As BillingJustificationControl, Optional ByVal idSequense As Int64 = 0) As Task(Of ActionResult(Of BillingJustificationControl))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveJustificationControlAsync(record, Me.Indigo.AuditMessageWcf, idSequense)
    End Function


    ''' <summary>
    ''' Elimina una justificacion control
    ''' </summary>
    ''' <param name="justificationControl">La justificacion control </param>
    ''' <returns></returns>
    Public Async Function DeleteBillingJustificationControl(ByVal record As BillingJustificationControl) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DeleteJustificationControlAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' consulta una justificacion control
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetBillingJustificationControlByCode(ByVal code As String) As Task(Of ActionResult(Of BillingJustificationControl))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetBillingJustificationAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Devuelve una justificacion control por ID
    ''' </summary>
    ''' <param name="idJustificationControl">Id de la justificacion control</param>
    ''' <returns>La justificacion Control</returns>
    ''' <remarks></remarks>
    Public Async Function GetBillingJustificationControlById(ByVal id As Integer) As Task(Of ActionResult(Of BillingJustificationControl))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetBillingJustificationByIdAsync(id)
    End Function

    Public Function ListUsers() As LinqInstantFeedbackSource
        'Return XpoServiceEx.Instance(_indigoSessionValues.SecurityContainer).SecurityService.GetAllUser()
        Return XpoServiceEx.Instance(Indigo.SecurityContainer).SecurityService.ListUserByContainer(Indigo.IndigoContainerId)
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

'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Carlos Mario Arias Rubiano 
' Created          : 11/09/2018
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
Imports System.Configuration
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class MDispensingByPatientMedilaser
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable de sesión
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

    Public Async Function GetListProductsByCODCONCEC(CODCONCEC As String) As Task(Of ActionResult(Of List(Of SP_ListHCPRESCRDByCODCONCEC_Result)))
        Try
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SP_ListHCPRESCRDByCODCONCECAsync(CODCONCEC)
        Catch ex As Exception
            Return New ActionResult(Of List(Of SP_ListHCPRESCRDByCODCONCEC_Result)) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Lista los diagnosticos a partir del filtro de sex y edad del paciente
    ''' </summary>
    ''' <param name="patientSex"></param>
    ''' <param name="patientAge"></param>
    ''' <param name="patientAgeInMonths"></param>
    ''' <param name="patientAgeInDays"></param>
    ''' <returns></returns>
    Public Function ListINDIAGNOSPerFilter(patientSex As Integer, patientAge As Integer, patientAgeInMonths As Integer, patientAgeInDays As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.ListINDIAGNOSPerFilter(patientSex, patientAge, patientAgeInMonths, patientAgeInDays)
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

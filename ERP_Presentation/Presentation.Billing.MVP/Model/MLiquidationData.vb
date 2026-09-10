'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 01/03/2023
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

Public Class MLiquidationData
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
    ''' Obtiene los datos de liquidacion mediante el numero de ingreso
    ''' </summary>
    Public Async Function GetLiquidationDataByAdmissionNumber(ByVal _admissionNumber As String) As Task(Of ActionResult(Of LiquidationData))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetLiquidationDataByAdmissionNumberAsync(_admissionNumber, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' guarda los datos de liquididacion
    ''' </summary>
    Public Async Function SaveLiquidationData(_liquidationData As LiquidationData, Optional idSequense As Long = 0) As Task(Of ActionResult(Of LiquidationData))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveLiquidationDataAsync(_liquidationData, Me.Indigo.AuditMessageWcf, idSequense)
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

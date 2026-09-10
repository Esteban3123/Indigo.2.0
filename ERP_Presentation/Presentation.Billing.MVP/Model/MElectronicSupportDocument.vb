'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-06-2015
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
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic

#End Region

Public Class MElectronicSupportDocument
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
    '''
    ''' </summary>
    ''' <param name="Code">The reversal reason.</param>
    ''' <returns></returns>
    Public Async Function GetElectronicSupportDocumentByCode(Code As String) As Task(Of Domain.Base.Entities.ActionResult(Of ElectronicSupportDocument))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetElectronicSupportDocumentByCodeAsync(Code, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    Public Async Function SaveElectronicSupportDocument(_electronicSupportDocument As ElectronicSupportDocument, idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveConfirmElectronicSupportDocumentAsync(_electronicSupportDocument, Me._indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    Public Async Function UpdateStateElectronicSupportDocumentsAsync(Ids As List(Of Integer)) As Task(Of Domain.Base.Entities.ActionResult(Of ElectronicSupportDocument))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateStateElectronicSupportDocumentsAsync(Ids, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Diego A. Roldan
' Created          : 2021-09-09
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent
#End Region

Public Class MRawMaterialDevolution
    Inherits ModelBase
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String
#End Region
#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
        Me._tagForm = tag
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Consulta la devolucion de materia prima por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRawMaterialDevolutionByCode(code As String) As Task(Of ActionResult(Of RawMaterialDevolution))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetRawMaterialDevolutionByCodeAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene los detalles de la devolución
    ''' </summary>
    ''' <param name="rawMaterialDevolutionId"></param>
    ''' <returns></returns>
    Public Function GetRawMaterialDevolutionDetailByRawMaterialDevolutionIdAsync(rawMaterialDevolutionId As Integer) As Task(Of List(Of RawMaterialDevolutionDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetRawMaterialDevolutionDetailByRawMaterialDevolutionIdAsync(rawMaterialDevolutionId)
    End Function

    ''' <summary>
    ''' Guarda una devolución
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <returns></returns>
    Public Function SaveRawMaterialDevolutionAsync(rawMaterialDevolution As RawMaterialDevolution, idSequence As Long) As Task(Of ActionResult(Of RawMaterialDevolution))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveRawMaterialDevolutionAsync(rawMaterialDevolution, Me._sessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Guarda y confirma el documento
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveAndConfirmRawMaterialDevolutionAsync(rawMaterialDevolution As RawMaterialDevolution, operatingUniId As Integer, idSequence As Long) As Task(Of ActionResult(Of RawMaterialDevolution))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveAndConfirmRawMaterialDevolutionAsync(rawMaterialDevolution, operatingUniId, Me._sessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Anula una devolución
    ''' </summary>
    ''' <returns></returns>
    Public Function AnnulateRawMaterialDevolutionAsync(code As String) As Task(Of ActionResult(Of RawMaterialDevolution))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.AnnulateRawMaterialDevolutionAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function
#End Region


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

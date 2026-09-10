'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 31-08-2022
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
Imports DevExpress.Xpo
Imports Domain.Crystal.Entities
#End Region
Public Class MDefects
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
    ''' Lista todos defectos
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    Public Async Function ListAllDefectClassificationItem(ByVal audit As AuditMessage) As Task(Of List(Of DefectClassificationItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllDefectClassificationItemAsync(_sessionValues.AuditMessageWcf)

    End Function

    ''' <summary>
    ''' Obtiene un defecto por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetDefectClassificationItemByCode(ByVal code As String) As Task(Of ActionResult(Of DefectClassificationItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetDefectClassificationItemByCodeAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="Id_Defects">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Public Async Function ListAllDefectsUnitDoseType(ByVal Id_Defects As Integer) As Task(Of List(Of Tuple(Of Integer, String)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllDefectsUnitDoseTypeAsync(Id_Defects, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un defecto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Public Async Function GetDefectClassificationItemById(id As String, ByVal audit As AuditMessage) As Task(Of ActionResult(Of DefectClassificationItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetDefectClassificationItemByIdAsync(id, Me._sessionValues.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Validaciones para guardar un defecto
    ''' </summary>
    ''' <param name="productionLineId"></param>
    ''' <returns></returns>
    Public Async Function ValidateDefectsUnitDoseType(defectsId As Integer, unitDoseTypeId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ValidateDefectsUnitDoseTypeAsync(defectsId, unitDoseTypeId)
    End Function

    ''' <summary>
    ''' Guarda una lina de produccion
    ''' </summary>
    ''' <param name="productionLine">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Public Async Function SaveDefectClassificationItem(ByVal defects As DefectClassificationItem, Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult(Of DefectClassificationItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveDefectClassificationItemAsync(defects, Me._sessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="defectsUnitDoseType">The identifier.</param>
    Public Async Function SaveDefectsUnitDoseType(ByVal defectsUnitDoseType As DefectsUnitDoseType) As Task(Of ActionResult(Of DefectsUnitDoseType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveDefectsUnitDoseTypeAsync(defectsUnitDoseType)
    End Function

    ''' <summary>
    ''' Elimina un defecto
    ''' </summary>
    ''' <param name="defects">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Public Async Function DeleteDefectClassificationItem(ByVal defects As DefectClassificationItem) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteDefectClassificationItemAsync(defects, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of DefectClassificationItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateDefectClassificationItemAsync(code, state, Me._sessionValues.AuditMessageWcf)
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

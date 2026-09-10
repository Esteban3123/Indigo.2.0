'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 05/08/2019
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
Imports DevExpress.Xpo
Imports Domain.Crystal.Entities

#End Region
Public Class MProductionLine
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
    ''' Lista todos los tipo de dosis unitaria
    ''' </summary>
    Public Function ListAllProductionLine() As List(Of ProductionLine)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllProductionLine(Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de dosis unitaria asincrono
    ''' </summary>
    Public Async Function ListAllproductionLineAsync() As Task(Of List(Of ProductionLine))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllProductionLineAsync(Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProductionLine(ByVal code As String) As ActionResult(Of ProductionLine)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductionLine(code, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por código asincrono
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetProductionLineAsync(ByVal code As String) As Task(Of ActionResult(Of ProductionLine))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductionLineAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id 
    ''' </summary>
    ''' <param name="Id_ProductionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllProductionLineUnitDoseType(ByVal Id_ProductionLine As Integer) As List(Of Tuple(Of Integer, String))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllProductionLineUnitDoseType(Id_ProductionLine, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id asincrono
    ''' </summary>
    ''' <param name="Id_ProductionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllProductionLineUnitDoseTypeAsync(ByVal Id_ProductionLine As Integer) As Task(Of List(Of Tuple(Of Integer, String)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllProductionLineUnitDoseTypeAsync(Id_ProductionLine, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProductionLineId(ByVal id As Integer) As ActionResult(Of ProductionLine)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductionLineId(id, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function ValidateProductionLineUnitDoseTypeAsync(productionLineId As Integer, unitDoseTypeId As Integer) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ValidateProductionLineUnitDoseTypeAsync(productionLineId, unitDoseTypeId)
    End Function

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id asincrono
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetProductionLineIdAsync(ByVal id As Integer) As Task(Of ActionResult(Of ProductionLine))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductionLineIdAsync(id, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Guarda o actualiza un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveProductionLine(ByVal productionLine As ProductionLine, ByVal idSequence As Int64) As ActionResult(Of ProductionLine)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveProductionLine(productionLine, idSequence, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Guarda o actualiza un tipo de dosis unitaria asincrono
    ''' </summary>
    ''' <param name="productionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveProductionLineAsync(ByVal productionLine As ProductionLine, ByVal idSequence As Int64) As Task(Of ActionResult(Of ProductionLine))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveProductionLineAsync(productionLine, idSequence, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Guarda o actualiza un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLineUnitDoseType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveProductionLineUnitDoseType(ByVal productionLineUnitDoseType As ProductionLineUnitDoseType) As ActionResult(Of ProductionLineUnitDoseType)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveProductionLineUnitDoseType(productionLineUnitDoseType)
    End Function
    ''' <summary>
    ''' Guarda o actualiza un tipo de dosis unitaria asincrono
    ''' </summary>
    ''' <param name="productionLineUnitDoseType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveProductionLineUnitDoseTypeAsync(ByVal productionLineUnitDoseType As ProductionLineUnitDoseType) As Task(Of ActionResult(Of ProductionLineUnitDoseType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveProductionLineUnitDoseTypeAsync(productionLineUnitDoseType)
    End Function
    '***********************************************************************


    ''' <summary>
    ''' Elimina un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteProductionLine(ByVal productionLine As ProductionLine) As ActionResult
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteProductionLine(productionLine, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Elimina un tipo de dosis unitaria asincrono
    ''' </summary>
    ''' <param name="productionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteProductLineAsync(ByVal productionLine As ProductionLine) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteProductionLineAsync(productionLine, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ProductionLine))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateProductionLineAsync(code, state, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateAsync(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ProductionLine))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateProductionLineAsync(code, state, Me._sessionValues.AuditMessageWcf)
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

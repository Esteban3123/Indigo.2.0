Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports System.ServiceModel

'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Julian Andres Cardozo
' Created          : 04-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MFixedAssetInsurance
    Inherits MBlockRecordAndSequenceFixedAsset
    Implements IDisposable


    Dim Indigo As SessionValues
    Public Shared TAG As String = "557"
    Public Sub New()
        MyBase.New(TAG)
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Funcion para obtener la aseguradora
    ''' </summary>
    ''' <param name="Code">El codigo de la aseguradora.</param>
    ''' <returns></returns>
    Public Async Function GetInsurance(ByVal Code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetInsuranceAsync(Indigo.TransactionalContainer, Code)
    End Function

    ''' <summary>
    ''' Funcion para guardar la aseguradora
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveInsurance(ByVal Record As FixedAssetInsurance, IdSequense As Integer, ByVal sequenceC As Domain.Entities.FixedAssetSequence) As Task(Of ActionResult(Of FixedAssetInsurance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveInsuranceAsync(Record, IdSequense, Me.Indigo.AuditMessageWcf)

    End Function

    ''' <summary>
    ''' Funcion para eliminar la aseguradora
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteInsurance(ByVal Record As FixedAssetInsurance) As Task(Of ActionResult)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteInsuranceAsync(Indigo.TransactionalContainer, Record, Indigo.AuditMessageWcf)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteInsuranceAsync(Record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista todos los Terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListThirdParty() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Function

    ' ''' <summary>
    ' ''' Listar los campos nulls de la base de datos y porder customizar 
    ' ''' </summary>
    ' ''' <returns></returns>
    'Public Async Function GetFieldsNULL() As Task(Of DataSet)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("Insurance", Me.Indigo)
    'End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(Code As String, State As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of FixedAssetInsurance))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.Change_StateInsuranceAsync(Code, State, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.Change_StateInsuranceAsync(Code, State, Me.Indigo.AuditMessageWcf)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class



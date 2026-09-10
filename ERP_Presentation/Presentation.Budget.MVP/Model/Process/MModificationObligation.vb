'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class MModificationObligation
    Inherits ModelBaseBudget
    Implements IDisposable

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetObligationModification(Code As String, validityId As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of ObligationModification))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetObligationModificationAsync(Code, validityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por id
    ''' </summary>
    ''' <returns>La Profesion</returns>
    Public Async Function GetObligationModificationById(Id As Integer) As Task(Of ActionResult(Of ObligationModification))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetObligationModificationByIdAsync(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una modificacion de obligacion
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Async Function SaveObligationModification(ByVal ObligationModification As Domain.Entities.ObligationModification, ByVal listDetailsForDelete As System.Collections.Generic.List(Of Integer)) As Task(Of Domain.Base.Entities.ActionResult(Of ObligationModification))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveObligationModificationAsync(ObligationModification, listDetailsForDelete, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteObligationModification(ByVal obligationModification As ObligationModification) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteObligationModificationAsync(obligationModification, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista los detalles de la obligacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListObligationDetailByObligationId(ObligationId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BudgetService.ListObligationDetailByObligationId(ObligationId)
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

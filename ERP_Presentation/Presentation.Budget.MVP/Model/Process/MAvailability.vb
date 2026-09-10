'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
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

#End Region

Public Class MAvailability
    Inherits ModelBaseBudget
    Implements IDisposable

#Region "fields"

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"
    ''' <summary>
    ''' Contruct
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
        Me._tagForm = tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una disponibilidad por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAvailability(Code As String, ItemType As Byte, BudgetaryValidityId As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of Availability))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAvailabilityAsync(Code, ItemType, BudgetaryValidityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener una disponibilidad por id
    ''' </summary>
    ''' <returns>La Profesion</returns>
    Public Function GetAvailabilityById(Id As Integer) As Availability
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAvailabilityById(Id)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una disponibilidad
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Async Function SaveAvailability(ByVal availability As Availability, ByVal idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of Availability))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveAvailabilityAsync(availability, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para cerrar el mes
    ''' </summary>
    ''' <param name="validity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CloseYear(validity As BudgetaryValidity, type As Integer) As Task(Of ActionResult(Of BudgetaryValidity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.CloseValidityAsync(validity, type, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para cerrar el mes
    ''' </summary>
    ''' <param name="validity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function RecalculateBalances(validity As BudgetaryValidity, type As Integer) As Task(Of ActionResult(Of BudgetaryValidity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.RecalculateBalancesAsync(validity, type, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una disponibilidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteAvailability(ByVal availability As Availability) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteAvailabilityAsync(availability, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista los terceros por estados xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllDependencyXpo(validity As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.GetDependency(validity)
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

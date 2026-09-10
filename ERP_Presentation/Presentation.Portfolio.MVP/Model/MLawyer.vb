'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/06/2017
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

#End Region

Public Class MLawyer
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

#Region "Builder"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para guardar un abogado
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveLawyer(ByVal Lawyer As Lawyer, ByVal idSequense As Int64) As Task(Of ActionResult(Of Lawyer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SaveLawyerAsync(Lawyer, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para eliminar un abogado
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteLawyer(ByVal Lawyer As Lawyer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.DeleteLawyerAsync(Lawyer, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para obtener un abogado
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetLawyerByCode(ByVal code As String) As Task(Of ActionResult(Of Lawyer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetLawyerByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para obtener un abogado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLawyerById(id As Integer) As Task(Of ActionResult(Of Lawyer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetLawyerByIdAsync(id)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Async Function ChangeStateLawyer(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Lawyer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ChangeStateLawyerAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
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

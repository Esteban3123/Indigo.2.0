'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Domain.Base.Entities

Public Class MEconomicActivity
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

    Public Shared TAG As String = "Falta tag"

#Region "Methods"

    ''' <summary>
    ''' Obtiene una actividad economica
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Async Function GetEconomicActivityById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.EconomicActivity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetEconomicActivityByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene una actividad economica
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Async Function GetEconomicActivity(ByVal code As String) As Task(Of ActionResult(Of Domain.Entities.EconomicActivity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetEconomicActivityAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una actividad economica
    ''' </summary>
    ''' <param name="EconomicActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveEconomicActivity(ByVal EconomicActivity As Domain.Entities.EconomicActivity) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.EconomicActivity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveEconomicActivityAsync(EconomicActivity, Indigo)
    End Function

    ''' <summary>
    ''' Elimina una actividad economica
    ''' </summary>
    ''' <param name="EconomicActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteEconomicActivity(ByVal EconomicActivity As Domain.Entities.EconomicActivity) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteEconomicActivityAsync(EconomicActivity, Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Entities.EconomicActivity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ChangeStateEconomicActivityAsync(code, state, Indigo)
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

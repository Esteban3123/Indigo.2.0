'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz Mosquera
' Created          : 27-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports  Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Security.Entities

''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MResponsibleTransfer
    Implements IDisposable

    ''' <summary>
    ''' Variable para inicializar los valores de sesión
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#Region "Builders"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Sub New(Tag As String)
        Me._tagForm = Tag
    End Sub

#End Region

    ''' <summary>
    ''' Funcion para obtener lista de conceptos de glosas.
    ''' </summary>
    ''' <param name="Codes">Lista de tipos.</param>
    ''' <returns></returns>
    Public Async Function getConcepts(ByVal Codes As List(Of String)) As Task(Of List(Of Domain.Entities.ConceptGlosas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByListTypesAsync(Codes, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener Conceptos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetResponsibleALL() As Task(Of Object)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListResponsibleAllAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para reasignar responsables
    ''' </summary>
    ''' <returns></returns>
    Public Async Function TransferResponsibleMovement(ListResponsibleMovements As List(Of ResponsibleMovements), opt As Integer) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.TransferResponsibleMovementsAsync(ListResponsibleMovements, Me.Indigo, opt)
    End Function


    ''' <summary>
    ''' Funcion para obtener los responsables 
    ''' que pueden ser reasignados
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAllResponsibleTransfer(IdResponsible As String) As Task(Of List(Of ResponsibleMovements))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.listAllResponsiblesTransferAsync(IdResponsible, Me.Indigo)
    End Function

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
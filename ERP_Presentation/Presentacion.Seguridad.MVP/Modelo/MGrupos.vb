
'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 27-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Domain.Security.Entities
Imports System.Data
Imports System.Threading.Tasks
#End Region
''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional FrmGrupos
''' </summary>
Public Class MGrupos
    Implements IDisposable

    ''' <summary>
    ''' Inicializa una nueva instancia de MGrupos.
    ''' </summary>
    Sub New()
        'igualamos a los valores de sesion el nombre del formulario para poder auditar.
        Indigo.AuditMessageWcf.Functional = Eform.Grupos.ToString
    End Sub

#Region "variables"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion Consultar el nombre por codigo.
    ''' </summary>
    Friend Async Function ConsultarNombre(ByVal codigo As String) As Threading.Tasks.Task(Of Group)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetGroupAsync(codigo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion Eliminar el grupo dependiendo del codigo suministrado
    ''' </summary>
    Friend Async Function EliminarGrupo(ByVal grupo As Group) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.DeleteGroupAsync(grupo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion que sirve para Grabar un grupo.
    ''' </summary>
    Friend Async Function GrabaGrupo(ByVal grupo As Group, dtDetails As DataTable, eliminados As List(Of Integer)) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveGroupAsync(grupo, dtDetails, eliminados, Me.Indigo)
    End Function

    Friend Async Function ConsultarDetales(query As String) As Task(Of DataTable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDtAsync(query, Me.Indigo.TransactionalContainer)
    End Function

#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
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
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

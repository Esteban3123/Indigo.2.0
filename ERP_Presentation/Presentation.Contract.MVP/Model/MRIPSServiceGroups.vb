'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 05-12-2023
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
Imports Presentation.CloudAgent
#End Region

Public Class MRIPSServiceGroups
    Implements IDisposable

#Region "Fields"
    Dim Indigo As SessionValues
    Private _tagForm As String
#End Region

#Region "Builder"
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista todos los grupos de servicios RIPS
    ''' </summary>
    ''' <returns>Lista con grupos de servicios RIPS</returns>
    Public Async Function ListAllRIPSServiceGroups() As Task(Of List(Of RIPSServiceGroups))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ListAllRIPSServiceGroupsAsync()
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>Grupo de servicio RIPS</returns>
    Public Async Function GetRIPSServiceGroupById(ByVal id As Integer) As Task(Of ActionResult(Of RIPSServiceGroups))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetRIPSServiceGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <returns>Grupo de servicios RIPS</returns>
    Public Async Function GetRIPSServiceGroupByCode(ByVal code As String) As Task(Of ActionResult(Of RIPSServiceGroups))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetRIPSServiceGroupByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda un grupo de servicios RIPS.
    ''' </summary>
    ''' <param name="record">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="idSequense"></param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    Public Async Function SaveRIPSServiceGroup(ByVal record As RIPSServiceGroups, Optional ByVal idSequense As Int64 = 0) As Task(Of ActionResult(Of RIPSServiceGroups))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveRIPSServiceGroupAsync(record, Me.Indigo.AuditMessageWcf, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado del grupo de servicio RIPS entre -activo- o -inactivo-
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio RIPS</param>
    ''' <param name="state">Nuevo estado</param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    Public Async Function ChangeStateRIPSServiceGroup(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of RIPSServiceGroups))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ChangeStateRIPSServiceGroupAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina el registro de un grupo de servicios RIPS
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    Public Async Function DeleteRIPSServiceGroup(ByVal record As RIPSServiceGroups) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.DeleteRIPSServiceGroupAsync(record, Me.Indigo.AuditMessageWcf)
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

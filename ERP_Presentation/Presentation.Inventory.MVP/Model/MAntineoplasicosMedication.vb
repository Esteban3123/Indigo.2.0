'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 03-04-2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
#End Region
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.CloudAgent

Public Class MAntineoplasicosMedication
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String


#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Graba un medicamento con resistencia bacteriana en modo asincrono
    ''' </summary>
    ''' <param name="ObjAntineoplasicosMedication"></param>
    ''' <returns></returns>
    Public Async Function SaveAntineoplasicosMedicationAsync(ByVal ObjAntineoplasicosMedication As List(Of AntineoplasicoMedication)) As Task(Of ActionResult(Of AntineoplasicoMedication))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAntineoplasicoMedicationAsync(ObjAntineoplasicosMedication, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="statesAPM"></param>
    ''' <returns></returns>
    Public Async Function GetAntineoplasicosMedicationByStatesAsync(ByVal statesAPM As String) As Task(Of ActionResult(Of List(Of Domain.Entities.AntineoplasicoMedication)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetAntineoplasicoMedicationByStatesAsync(statesAPM, Indigo.AuditMessageWcf)
    End Function

    '''' <summary>
    '''' 
    '''' </summary>
    '''' <param name="statesBRM"></param>
    '''' <returns></returns>
    'Public Async Function GetAntineoplasicosMedicationAsync() As Task(Of ActionResult(Of List(Of Domain.Entities.AntineoplasicosMedication)))
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetAntineoplasicosMedicationAsync(Indigo.AuditMessageWcf)
    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetATCXpo(id As Integer) As ATCXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetATCXpo(id)
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class


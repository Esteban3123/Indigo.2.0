'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Andres Alarcon
' Created          : 25/10/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
#End Region

Public Class MRequestDashboard
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

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
    ''' carga las unidades operativas
    ''' </summary>
    Public Function ListAllOperatingUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CommonService.ListOperatingUnit()
    End Function

    ''' <summary>
    ''' Obtiene las solicitudes que requieren autorizacion
    ''' </summary>
    Public Function ListRequestDetailByOperatingUnitAndType(ByVal Operatingunits As String, ByVal RequestType As String) As XPCollection
        Return XpoServiceEx.Instance(Indigo.HisContainer).InventoryService.ListRequestDetailByOperatingUnitAndType(Operatingunits, RequestType)
    End Function

    ''' <summary>
    ''' Actualiza las cantidades del detalle de solicitud seleccionado
    ''' </summary>
    Public Function UpdateQuantityAuthorizedInventoryRequestDetail(ByVal Id As Integer, ByVal Quantity As Integer)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateQuantityAuthorizedInventoryRequestDetail(Id, Quantity, Indigo.UserIndigo)
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

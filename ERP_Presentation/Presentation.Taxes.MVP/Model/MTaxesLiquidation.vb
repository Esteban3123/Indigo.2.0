'***********************************************************************
' Assembly         : Presentacion.Taxes.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2016
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
Imports Domain.Base.Entities
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
Imports System.ServiceModel
#End Region

''' <summary>
''' Realiza la conexion con los servicios del grupo
''' </summary>
''' 
Public Class MTaxesLiquidation
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

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Function SaveTaxesLiquidation(Year As Integer, CadastralIdentification As String, CadastralIdentification2 As String, Address As String, Address2 As String, OwnerId As Integer, PropertyType As Integer) As Task(Of ActionResult(Of Tuple(Of Integer, String)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTaxes.SaveTaxesLiquidationAsync(Year, CadastralIdentification, CadastralIdentification2, Address, Address2, OwnerId, PropertyType, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ConfirmTaxesLiquidation(Year As Integer, Ids As String) As Task(Of ActionResult(Of Tuple(Of String, String, String, String)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTaxes.ConfirmTaxesLiquidationAsync(Year, Ids, Me._indigoSessionValues.AuditMessageWcf)
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

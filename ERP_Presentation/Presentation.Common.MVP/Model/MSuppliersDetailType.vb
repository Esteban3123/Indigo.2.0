'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 11-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region

''' <summary>
''' Model de conexion con los servicios distribuidos de ciudades
''' </summary>
Public Class MSuppliersDetailType
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

    Public Shared TAG As String = "513"

#Region "Methods"

    ''' <summary>
    ''' Obtiene la ciudad por codigo
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Function GetSuppliersDetailTypeByIdSupplier(ByVal id As String) As List(Of Domain.Entities.SupplierDetailType)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetSuppliersDetailTypeByIdSupplier(id, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la ciudad por codigo
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Function GetSuppliersDetailTypeById(ByVal id As Integer) As Domain.Entities.SupplierDetailType
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetSuppliersDetailTypeById(id, Indigo)
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

'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
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
Public Class MSequense
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
        _tagForm = Tag
        Indigo = SessionValues.Instance
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.CommonSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetSequenseByIdFormAsync(_tagForm, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetNumericSequenseGroupByIdAsync(id, Indigo)
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

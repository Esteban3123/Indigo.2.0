'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 04-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Realiza la conexion con los servicios
''' </summary>
Public Class MContributorType
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "537"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "Properties"
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el tipo de contribuyente de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo del tipo de contribuyente</param>
    ''' <returns>Tipo de contribuyente</returns>
    Public Async Function GetContributorTypeAsync(ByVal code As String) As Task(Of ContributorType)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContributorTypeAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios del tipo de contribuyente asincrono
    ''' </summary>
    ''' <param name="ContributorType">Tipo de contribuyente</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveContributorTypeAsync(ByVal contributorType As ContributorType) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveContributorTypeAsync(contributorType, Indigo)
    End Function


    ''' <summary>
    ''' Borra tipo de contribuyente asincrono
    ''' </summary>
    ''' <param name="ContributorType">Tipo de contribuyente</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteContributorTypeAsync(ByVal contributorType As ContributorType) As Task(Of ActionMessageResult(Of ContributorType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteContributorTypeAsync(contributorType, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de tipos de contribuyente asincrono
    ''' </summary>
    ''' <returns>Listado de tipos de contribuyente</returns>
    Public Async Function ListAllContributorTypeAsync() As Task(Of List(Of ContributorType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllContributorTypeAsync(Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("ContributorType", Indigo)
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

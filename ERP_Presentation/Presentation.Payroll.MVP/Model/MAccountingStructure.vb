'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2013
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

Public Class MAccountingStructure
    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene la Estructura Contable por Código
    ''' </summary>
    ''' <param name="code">Codigo de la Estructura Contable</param>
    ''' <returns>Estructura Contable</returns>
    Public Function GetAccountingStructure(ByVal code As String) As AccountingStructure
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetAccountingStructure(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la Estructura Contable por Código asíncrono
    ''' </summary>
    ''' <param name="code">Codigo de la Estructura Contable</param>
    ''' <returns>Estructura Contable</returns>
    Public Async Function GetAccountingStructureAsync(ByVal code As String) As Task(Of AccountingStructure)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetAccountingStructureAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Guarda los cambios de la Estructura Contable
    ''' </summary>
    ''' <param name="AccountingsStructure">AccountingsStructure</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Function SaveAccountingStructure(ByVal AccountingsStructure As AccountingStructure) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveAccountingStructure(AccountingsStructure, Indigo)
    End Function

    ''' <summary>
    ''' Guarda los cambios de la Estructura Contable asincrono
    ''' </summary>
    ''' <param name="AccountingsStructure">AccountingsStructure</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveAccountingStructureAsync(ByVal AccountingsStructure As AccountingStructure) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveAccountingStructureAsync(AccountingsStructure, Indigo)
    End Function

    ''' <summary>
    ''' Borra la Estructura Contable
    ''' </summary>
    ''' <param name="AccountingsStructure">AccountingsStructure</param>
    ''' <returns>Si se realizo o no el borrado</returns>
    Public Function DeleteAccountingStructure(ByVal AccountingsStructure As AccountingStructure) As ActionMessageResult(Of AccountingStructure)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteAccountingStructure(AccountingsStructure, Indigo)
    End Function

    ''' <summary>
    ''' Borra la Estructura Contable asíncrono
    ''' </summary>
    ''' <param name="AccountingsStructure">AccountingsStructure</param>
    ''' <returns>Si se realizo o no el borrado</returns>
    Public Async Function DeleteAccountingStructureAsync(ByVal AccountingsStructure As AccountingStructure) As Task(Of ActionMessageResult(Of AccountingStructure))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteAccountingStructureAsync(AccountingsStructure, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene los campos nulos 
    ''' </summary>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("AccountingStructure", Indigo)
    End Function

    ''' <summary>
    ''' Funcion para cargar Toda la Estructura Contable
    ''' </summary>
    ''' <returns>lista de conceptos de cartera</returns>
    ''' <remarks></remarks>
    Public Function ListAllAccountingStructure() As List(Of AccountingStructure)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllAccountingStructure(Indigo)
    End Function

    ''' <summary>
    ''' Funcion para cargar Toda la Estructura Contable Asíncrono
    ''' </summary>
    ''' <returns>lista de conceptos de cartera</returns>
    ''' <remarks></remarks>
    Public Async Function ListAllAccountingStructureAsync() As Task(Of List(Of AccountingStructure))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllAccountingStructureAsync(Indigo)
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

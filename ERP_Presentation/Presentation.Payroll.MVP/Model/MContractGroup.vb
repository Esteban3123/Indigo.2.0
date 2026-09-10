'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 05-07-2013
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
Public Class MContractGroup
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "539"

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
    ''' Obtiene el grupo de contratos de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo del grupo de contratos</param>
    ''' <returns>grupo de contratos</returns>
    Public Async Function GetContractGroupAsync(ByVal code As String) As Task(Of ContractGroup)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContractGroupAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios del grupo de contratos asincrono
    ''' </summary>
    ''' <param name="ContractGroup">grupo de contratos</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveContractGroupAsync(ByVal ContractGroup As ContractGroup) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveContractGroupAsync(ContractGroup, Indigo)
    End Function


    ''' <summary>
    ''' Borra grupo de contratos asincrono
    ''' </summary>
    ''' <param name="ContractGroup">grupo de contratos</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteContractGroupAsync(ByVal ContractGroup As ContractGroup) As Task(Of ActionMessageResult(Of ContractGroup))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteContractGroupAsync(ContractGroup, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de tipos de pensionado asincrono
    ''' </summary>
    ''' <returns>Listado de tipos de pensionado</returns>
    Public Async Function ListAllContractGroupAsync() As Task(Of List(Of ContractGroup))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllContractGroupAsync(Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("ContractGroup", Indigo)
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

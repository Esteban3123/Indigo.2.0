'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 26-09-2013
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
''' Realiza la conexion con los servicios del razones de modificacion de contratos
''' </summary>
Public Class MContractModificationReason
    Inherits ModelBase
    Implements IDisposable


#Region "Properties"

    Public Shared TAG As String = "580"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene las razones de modificacion de contratos de acuerdo al codigo de manera asincrona
    ''' </summary>
    ''' <param name="code">Codigo las razones de modificacion de contratos</param>
    ''' <returns>las razones de modificacion de contratos</returns>
    Public Async Function GetContractModificationReasonAsync(ByVal code As String) As Task(Of ContractModificationReason)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContractModificationReasonAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios dlas razones de modificacion de contratos asincrono
    ''' </summary>
    ''' <param name="ContractModificationReason">la razon de modificacion del contrato</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveContractModificationReasonAsync(ByVal ContractModificationReason As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveContractModificationReasonAsync(ContractModificationReason, Indigo)
    End Function

    ''' <summary>
    ''' Borra las razones de modificacion de contratos asincrono
    ''' </summary>
    ''' <param name="ContractModificationReason">lenguaje</param>
    ''' <returns>Si se realizo o no el borrado</returns>
    Public Async Function DeleteContractModificationReasonAsync(ByVal ContractModificationReason As Object) As Task(Of ActionMessageResult(Of ContractModificationReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteContractModificationReasonAsync(ContractModificationReason, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene los campos nulos 
    ''' </summary>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("ContractModificationReason", Indigo)
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

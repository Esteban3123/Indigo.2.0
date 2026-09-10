' Assembly         : Presentation.Payroll.MVP
' Author           : Daniel Arevalo
' Created          : 16-08-2017
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importaciones"
Imports Infrastructure.Data.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports DevExpress.Xpo
Imports System.Threading.Tasks
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports System.ServiceModel


#End Region

Public Class MBlockSchedule
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

#Region "Funciones"
    Public Function ListFunctionalUnit() As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListFunctionalUnitXpCollection(True)
    End Function

    ''' <summary>
    ''' Graba la unidad negocio
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la unidad negocio</returns>
    Public Async Function SaveBlockScheduleAsync(ByVal blockScheduleC As BlockScheduleC, ByVal ListBlockSchedule As List(Of BlockSchedule)) As Task(Of ActionResult(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveBlockScheduleAsync(blockScheduleC, ListBlockSchedule, _indigoSessionValues)
    End Function

    Public Async Function ListBlockScheduleAsync() As Task(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllBlockScheduleAsync(_indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
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

'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Andrés Steven Rojas
' Created          : 27-09-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.Text
Imports Domain.Payroll
#End Region
Public Class MLicensingConcepts
    Implements IDisposable

#Region "Construct"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigo = SessionValues.Instance
    End Sub

#End Region
#Region "Methods"
    ''' <summary>
    ''' Método para listar los conceptos de licencia
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAllLicensingConceptsAsync() As Task(Of List(Of LicensingConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.listAllLicensingConceptsAsync(Me._indigo)
    End Function
    ''' <summary>
    ''' Método para obtener un concepto por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function GetLicensingConcepts(ByVal code As String) As Task(Of LicensingConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLicensingConceptsAsync(code, Me._indigo)
    End Function
    ''' <summary>
    ''' Método para obtener un concepto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetLicensingConceptsById(ByVal Id As Integer) As Task(Of LicensingConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLicensingConceptsByIdAsync(Id, Me._indigo)
    End Function
    ''' <summary>
    ''' Método para guardar un concepto de licencia
    ''' </summary>
    ''' <param name="LicensingConcepts"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Async Function SaveLicensingConcepts(ByVal LicensingConcepts As LicensingConcepts, ByVal idSequence As Int64) As Task(Of ActionResult(Of LicensingConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveLicensingConceptsAsync(LicensingConcepts, Me._indigo, idSequence)
    End Function
    ''' <summary>
    ''' Método para eliminar un concepto de licencia
    ''' </summary>
    ''' <param name="LicensingConcepts"></param>
    ''' <returns></returns>
    Public Async Function DeleteLicensingConcepts(ByVal LicensingConcepts As LicensingConcepts) As Task(Of ActionMessageResult(Of LicensingConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteLicensingConceptsAsync(LicensingConcepts, Me._indigo)
    End Function
#End Region
#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes
    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

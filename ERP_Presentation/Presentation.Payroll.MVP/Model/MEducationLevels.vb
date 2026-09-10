'***********************************************************************
' Assembly         : Presentacion.Payroll.File.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 12-04-2013
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
Imports System.ServiceModel

#End Region
''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en el frontal Niveles de educacion
''' </summary>
Public Class MEducationLevels
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "514"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

    Private Indigo As SessionValues = SessionValues.Instance
#Region "methods"
    ''' <summary>
    ''' Obtener un Nivel de educacion
    ''' </summary>
    ''' <param name="code">El codigo del nivel de educacion.</param>
    ''' <returns>El Nivel de cargo</returns>
    Public Async Function GetEducationLevelsAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEducationLevelsAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Graba el Nivel de educacion
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el Nivel de educacion</returns>
    Public Async Function SaveEducationLevelsAsync(ByVal reg As Object, ByVal idSequense As Int64) As Task(Of ActionResult(Of EducationLevel))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveEducationLevelsAsync(reg, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveEducationLevelsAsync(reg, Indigo, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina el Nivel de educacion
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el Nivel de educacion</returns>
    Public Async Function DeleteEducationLevelsAsync(ByVal reg As Object) As Task(Of ActionResult)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteEducationLevelsAsync(reg, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteEducationLevelsAsync(reg, Indigo, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista los departamentos
    ''' </summary>
    Public Async Function ListAllEducationLevelsAsync() As Task(Of List(Of EducationLevel))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllEducationLevelsAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("EducationLevels", Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of EducationLevel))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateEducationLevelAsync(code, state, Indigo, Me.Indigo.AuditMessageWcf)
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

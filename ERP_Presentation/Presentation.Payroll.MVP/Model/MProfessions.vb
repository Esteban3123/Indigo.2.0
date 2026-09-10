'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 16-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en las profesiones
''' </summary>
Public Class MProfessions
    Inherits ModelBase
    Implements IDisposable

    'Private Indigo As SessionValues = SessionValues.Instance

    Public Sub New(tag As String)
        MyBase.New(tag)
    End Sub


#Region "Methods"
    ''' <summary>
    ''' Obtener el listado de los niveles de educacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllEducationLevels() As List(Of EducationLevel)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllEducationLevels(Indigo)
    End Function
    ''' <summary>
    ''' Obtener una Profesion por su codigo
    ''' </summary>
    ''' <param name="code">El codigo de la profesion.</param>
    ''' <returns>La Profesion</returns>
    Public Async Function GetProfessionsAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetProfessionsAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Graba la profesion
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la profesion</returns>
    Public Async Function SaveProfessionsAsync(ByVal reg As Object, ByVal idSequense As Int64) As Task(Of ActionResult(Of Profession))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveProfessionsAsync(reg, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveProfessionsAsync(reg, Indigo, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina la profesion
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la profesion</returns>
    Public Async Function DeleteProfessionAsync(ByVal reg As Profession) As Task(Of ActionResult)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteProfessionAsync(reg, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteProfessionAsync(reg, Indigo, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista las profesiones
    ''' </summary>
    Public Async Function ListAllProfessionsAsync() As Task(Of List(Of Profession))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllProfessionsAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Profession", Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Profession))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateProfessionAsync(code, state, Indigo, Me.Indigo.AuditMessageWcf)
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

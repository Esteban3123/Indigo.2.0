'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 03-07-2013
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
''' Realiza la conexion con los servicios de riesgos profesionales
''' </summary>
Public Class MProfessionalRisk
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "535"

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub


#Region "Methods"

    ''' <summary>
    ''' Obtiene el riesgo profesional de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo del riesgo profesional</param>
    ''' <returns>riesgo profesional</returns>
    Public Async Function GetProfessionalRiskAsync(ByVal code As String) As Task(Of ProfessionalRisk)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetProfessionalRiskAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios del riesgo profesional asincrono
    ''' </summary>
    ''' <param name="ProfessionalRisk">riesgo profesional</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveProfessionalRiskAsync(ByVal professionalRisk As ProfessionalRisk) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveProfessionalRiskAsync(professionalRisk, Indigo)
    End Function


    ''' <summary>
    ''' Borra riesgo profesional asincrono
    ''' </summary>
    ''' <param name="ProfessionalRisk">riesgo profesional</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteProfessionalRiskAsync(ByVal professionalRisk As ProfessionalRisk) As Task(Of ActionMessageResult(Of ProfessionalRisk))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteProfessionalRiskAsync(professionalRisk, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de tipos de pensionado asincrono
    ''' </summary>
    ''' <returns>Listado de tipos de pensionado</returns>
    Public Async Function ListAllProfessionalRiskAsync() As Task(Of List(Of ProfessionalRisk))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllProfessionalRiskAsync(Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("ProfessionalRisk", Indigo)
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

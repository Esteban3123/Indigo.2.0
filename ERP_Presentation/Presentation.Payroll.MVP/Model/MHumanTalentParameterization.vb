'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cesar Collazos
' Created          : 09-02-2024
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent
#End Region
Public Class MHumanTalentParameterization
    Inherits ModelBase
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
        Me._tagForm = tag
        _indigo = SessionValues.Instance
        'Me._indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Función para obtener la parametrización de talento humano
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetHumanTalentParameterizationAsync() As Task(Of HumanTalentParameterization)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetHumanTalentParameterizationAsync(Me._indigo)
    End Function

    ''' <summary>
    ''' Guarda la parametrización para el formulario de talento humano
    ''' </summary>
    ''' <param name="group">Grupo</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Function SaveHumanTalentParameterization(ByVal Parameterization As HumanTalentParameterization)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveHumanTalentParameterizationAsync(Parameterization, Me._indigo)
    End Function
#Region "IDiposable Support"
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


#End Region
End Class

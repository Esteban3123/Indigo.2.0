'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 05/11/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PCashFlowConcept

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As ICashFlowConcept

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim _indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As ICashFlowConcept)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me._indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await _view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(_view.MyTag)
            _view.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista los conceptos de flujo de efectivo, parameter(0) es el estado:1,0; parameter(1) es el tipo:1(ingreso),2(egreso)
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function GetCashFlowConcept(ByVal parameters As String())
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.ListCashFlowConcept(parameters)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCashFlowConceptById(ByVal id As Integer)
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.GetCashFlowConceptById(id)
    End Function


#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.Accouting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class PVieBot

#Region "Fields"
    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As IVieBot

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
    Public Sub New(ByRef view As IVieBot)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub


#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListLegalBook() As XPCollection
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListBookByStatusXpCollection(True)
    End Function

    ''' <summary>
    ''' Lista los VieBot por formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListVieBotByForm(Form As String) As XPCollection
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListVieBotByForm(Form)
    End Function

#End Region

End Class
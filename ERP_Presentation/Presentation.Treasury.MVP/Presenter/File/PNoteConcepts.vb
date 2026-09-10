'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-03-2014
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
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Presentador del frontal Conceptos de Notas
''' </summary>
Public Class PNoteConcepts

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As INoteConcepts

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As INoteConcepts)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the account accounting.
    ''' </summary>
    Public Sub InitializeAccountAccounting()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountAccountingDatasource = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using ModelCommonTreasury As New MCommonTreasury(Me.View.MyTag)
            Me.View.Sequence = Await ModelCommonTreasury.GetSequense()
        End Using
    End Sub

End Class

'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 02-06-2015
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
Imports Presentation.Controls.MVP

#End Region

Public Class PTransferOrderDevolution

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ITransferOrderDevolution

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ITransferOrderDevolution)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' cargar el datasource de las unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeTransferOrder()
        Using model As New MBusqueda
            Me.View.TransferOrderXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListTransferOrderByStatus, 2)
        End Using
    End Sub

#End Region

End Class

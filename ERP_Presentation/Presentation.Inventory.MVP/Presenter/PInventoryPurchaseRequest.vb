'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 15/04/2019
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

Public Class PPurchaseRequest

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPurchaseRequest

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IPurchaseRequest)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' cargar el datasource de las unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FunctionalUnit()
        Using model As New MBusqueda
            Me.View.FunctionalUnitXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFunctionalUnit, True)
        End Using
    End Sub

    ''' <summary>
    ''' cargar el datasource de las unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RequestType()
        Me.View.RequestTypeDataSource = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListRequestType()
    End Sub

#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez 
' Created          : 05/06/2015
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

Public Class PWarehouseStock

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IWarehouseStock

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
    Public Sub New(ByRef iview As IWarehouseStock)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    'Public Async Sub GetSequense()
    '    Using model As New MBlockRecordAndSequense(Me.View.MyTag)
    '        Me.View.Sequense = Await model.GetSequense()
    '    End Using
    'End Sub

    Public Sub LoadWarehouse()
        Me.View.ListWareHouse = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnAndConsignmentWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    Public Sub LoadProducts()
        Using Model As New MBusqueda
            Me.View.ListProducts = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInventoryProductFrmStock)
        End Using
    End Sub

#End Region

End Class

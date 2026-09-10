'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 19/03/2014
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
Imports Infrastructure.Data.Xpo

#End Region
Public Class PDevolutionMerchandising

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IDevolutionMerchandising


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
    Public Sub New(ByRef iview As IDevolutionMerchandising)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga los terceros
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListAllLoanMerchandiseByWareHouseIdAndConfirm(ByVal IdStores As Integer)
        Using model As New MBusqueda
            Me.View.DataSourceLoanMerchandising = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListAllLoanMerchandiseByWareHouseIdAndConfirm(IdStores)
        End Using
    End Sub
    ''' <summary>
    ''' Carga los almacenes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeStores()
        Using model As New MBusqueda
            Me.View.DatasourceStock = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
        End Using
    End Sub
    ''' <summary>
    ''' Obtiene la secuencia numerica del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MLendingMerchandising(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub
    ''' <summary>
    ''' Obtiene un prestamo de inventario por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetLoanMerchadiseById(Id As Integer) As InventoryRepository.InventoryLoanMerchandiseXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRepository.InventoryLoanMerchandiseXpo)(Nothing, filter).FirstOrDefault()
    End Function
#End Region


End Class

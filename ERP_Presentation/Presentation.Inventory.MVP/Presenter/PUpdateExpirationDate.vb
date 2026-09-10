'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 05/05/2015
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
Public Class PUpdateExpirationDate

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IUpdateExpirationDate


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
    Public Sub New(ByRef iview As IUpdateExpirationDate)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los Productos que manejan lote y fecha de vencimientos, adicional a que esten en estado activo y pertenescan a la clase de ITEMS, es decir todas las diferente a 1
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeProduct()
        Me.View.DatasourceProduct = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductByClassTypeAndHandlesBatch(True, 1) 'todos los diferente a 1
    End Sub

#End Region


End Class

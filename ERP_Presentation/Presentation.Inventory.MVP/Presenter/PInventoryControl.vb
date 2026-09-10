'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Henry Alejandro Vargas Polania 
' Created          : 26/03/2015
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

Public Class PInventoryControl

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IInventoryControl

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
    Public Sub New(ByRef iview As IInventoryControl)
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

    Public Sub LoadListWarehouse()
        Using Model As New MInventoryControl("")
            If Me.View.ControlType = 3 Then
                Me.View.ListWareHouse = Model.ListWarehouseByStatusUserCustodyStore(Indigo.TransactionalContainer, True, Indigo.UserIndigo)
            ElseIf Me.View.ControlType = 4 Then
                Me.View.ListWareHouse = Model.ListControlWarehouseByStatusAndUser(Indigo.TransactionalContainer, True, Indigo.UserIndigo)
            Else
                Me.View.ListWareHouse = Model.ListWarehouseByStatusAndUser(Indigo.TransactionalContainer, True, Indigo.UserIndigo)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Loads the admission number.
    ''' </summary>
    Public Sub LoadAdmissionNumber()
        'Me.View.AdmissionNumberDatasource = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetViewAdmissionOpenAndPartial()
        Me.View.AdmissionNumberDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).BillingService.ListRevenueControlActive()
    End Sub

#End Region

End Class

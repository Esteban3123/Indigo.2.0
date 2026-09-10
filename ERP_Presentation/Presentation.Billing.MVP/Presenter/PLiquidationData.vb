'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Cristian Camilo Bahamón Castaño
' Created          : 01-03-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP
#End Region

Public Class PLiquidationData

#Region "Fields"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ILiquidateData


    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

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
    Public Sub New(ByRef iview As ILiquidateData)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de la rejilla de tipos de regla
    ''' dependiendo del tipo que escojan
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeDataSourceGridControlsRulesType(type As Integer) As DevExpress.Xpo.XPCollection
        Select Case type
            Case 1 'CupsGroup   
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsGroupByStatusXpCollection(True)
            Case 2 'CupsSubGroup
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsSubGroupByStatusXpCollection(True)
            Case 3 'CUPS
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatusXpCollection(True)
            Case 4 'Productos
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductByStatusCollection(True)
            Case Else
                Return Nothing
        End Select
    End Function

#End Region

End Class

'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 28-04-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Presentador del frontal
''' </summary>
''' <remarks></remarks>
Public Class PShowDialogCCPET

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IShowDialogCCPET
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IShowDialogCCPET)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del catalogo padre
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeParent(ItemType As Integer)
        View.ParentXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetCCPETByStatusAndItemType(True, ItemType)
    End Sub

End Class

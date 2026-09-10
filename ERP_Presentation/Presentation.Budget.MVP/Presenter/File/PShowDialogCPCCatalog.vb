#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Presentador del frontal
''' </summary>
''' <remarks></remarks>
Public Class PShowDialogCPCCatalog

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IShowDialogCPCCatalog
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IShowDialogCPCCatalog)
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
    Public Sub InitializeParent()
        View.ParentXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetCPCCatalogTByStatus(True)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="CPCCatalogOwnerId"></param>
    ''' <returns></returns>
    Public Function ValidateCPCRegister(ByVal CPCCatalogOwnerId As Integer) As Boolean
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ValidateCPCRegister(CPCCatalogOwnerId)
    End Function
End Class

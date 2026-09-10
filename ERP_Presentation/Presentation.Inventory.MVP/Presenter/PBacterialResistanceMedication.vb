#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region

Public Class PBacterialResistanceMedication
#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IBacterialResistanceMedication

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IBacterialResistanceMedication)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region
    Public Sub InitializeInventoryATC()
        Using model As New MBusqueda
            'View.InventoryATC = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListATC)
            View.InventoryATC = Infrastructure.Data.Xpo.XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.CollectionATCAntibiotic
        End Using
    End Sub
End Class


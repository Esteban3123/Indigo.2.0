'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 02-09-2013
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

#End Region
''' <summary>
''' 
''' </summary>
Public Class PEquipment

    ''' <summary>s
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IEquipment

    ''' <summary>
    ''' Variable que se usa para tratar la torre como un objeto
    ''' </summary>
    Dim Equipment As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IEquipment)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Async Function Initializes() As Task
        View.StateEquipment = True
        'Dim ModelInventoryType As New MInventoryType
        'Dim ModelEquipmentType As New MEquipamentType
        ''View.InventoryTypeDataSource = Await ModelInventoryType.ListAllInventoryType
        'View.EquipmentTypeDataSource = Await ModelEquipmentType.ListAllEquipamentType
    End Function

End Class

'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 19-03-2015
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
Imports Presentation.Common.MVP
Imports Domain.Payroll.Entities
Imports Domain.Maintenance.Entities
Imports System.Runtime.CompilerServices

#End Region
Public Class PEquipmentTypePartsAccesoriesConsumables

    ''' <summary>s
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IEquipmentTypePartsAccesoriesConsumibles
    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IEquipmentTypePartsAccesoriesConsumibles)
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
        Using model As New MEquipamentType()
            Me.View.ListEquipmentType = Await model.ListAllEquipamentType()
        End Using

        Using model As New MPartsAccesoriesConsumables("1531")
            Me.View.ListPartsAccesoriesConsumibles = Await model.ListAllPartsAccesoriesConsumablesAsync()
        End Using
    End Function

End Class

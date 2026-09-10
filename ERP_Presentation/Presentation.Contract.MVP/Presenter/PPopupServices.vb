'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/10/2014
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

#End Region

Public Class PPopupServices

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPopupServices

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
    Public Sub New(ByRef iview As IPopupServices)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que inicializa el datasource de servicios ips
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeIPSServices()
        Using model As New MBusqueda
            View.IPSServiceXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListIPSServicesByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de grupos quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSurgicalGroup()
        Using model As New MBusqueda
            View.SurgicalGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSurgicalGroupByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de salario minimo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeContractMinimumWage()
        Using model As New MBusqueda
            View.ContractMinimumWageXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListContractMinimumWageByStatus, True)
        End Using
    End Sub

#End Region

End Class
